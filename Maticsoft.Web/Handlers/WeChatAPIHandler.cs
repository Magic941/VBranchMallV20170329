using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Maticsoft.Web.Components;
using System.Web.Security;
using Maticsoft.WeChat.Model.Core;

namespace Maticsoft.Web.Handlers
{
    public class WeChatAPIHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string postStr = Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    try
                    {
                        //解析用户数据信息
                        Maticsoft.WeChat.Model.Core.RequestMsg msgModel = Maticsoft.WeChat.BLL.Core.RequestMsg.GetRequestMsg(postStr);
                        if (msgModel == null)
                        {
                            return;
                        }
                        //处理图片消息，下载微信图片
                        if (!String.IsNullOrWhiteSpace(msgModel.PicUrl))
                        {
                            System.Net.WebClient webclient = new System.Net.WebClient();
                            string savePath = "/Upload/WeChat/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                            }
                            string WeChatImage = savePath + CreateIDCode() + ".jpg";
                            webclient.DownloadFile(msgModel.PicUrl, HttpContext.Current.Server.MapPath(WeChatImage));
                            msgModel.PicUrl = WeChatImage;
                        }
                        //添加用户消息
                        Maticsoft.WeChat.BLL.Core.RequestMsg.AddMsg(msgModel);
                        //关注事件，是否发送邮件
                        if (msgModel.Event == "subscribe")
                        {
                            //获取用户详细信息
                            Maticsoft.WeChat.BLL.Core.User userBll = new WeChat.BLL.Core.User();
                            string appId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", msgModel.OpenId);
                            string appSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", msgModel.OpenId);
                            //Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero); //??? 放入缓存中
                            if (!String.IsNullOrWhiteSpace(appId) && !String.IsNullOrWhiteSpace(appSecret))
                            {
                                string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(appId, appSecret);
                                var userModel = userBll.GetWcInfo(token, msgModel.UserName);
                                if (userModel != null)
                                {
                                    //处理微信用户图片
                                    userModel.Headimgurl = GetAvatarImg(userModel.Headimgurl, msgModel.UserName);
                                    userModel.UserName = msgModel.UserName;
                                    userBll.UpdateUser(userModel);
                                    //渠道推广记录
                                    if (!String.IsNullOrWhiteSpace(msgModel.EventKey))
                                    {
                                        var arry = msgModel.EventKey.Split('_');
                                        if (arry.Length > 1)
                                        {
                                            userModel.UserName = msgModel.UserName;
                                            userModel.OpenId = msgModel.OpenId;
                                            int sceneId = Common.Globals.SafeInt(arry[1], 0);
                                            Maticsoft.WeChat.BLL.Core.SceneDetail.AddDetail(sceneId, userModel);
                                        }
                                    }
                                }
                            }
                            bool isSendEmail = Maticsoft.WeChat.BLL.Core.Config.isSendEmail(msgModel.OpenId, "WeChat_ChkSubscribe");
                            if (isSendEmail)
                            {
                                Maticsoft.Web.Components.PostMsgHelper.SendSubscribeEmail(msgModel);
                            }
                        }
                        string msgXml = "";
                        #region 处理微信推送的地理位置信息
                        if (msgModel.MsgType == "event" && msgModel.Event == "LOCATION")
                        {
                            msgXml = Maticsoft.Web.Components.PostMsgHelper.GetLocationMsg(msgModel);
                            context.Response.Write(msgXml);
                            return;
                        }
                        #endregion
                        //if (msgModel.MsgType == "event" && msgModel.EventKey.Contains("qrscene_"))
                        //{

                        string content = "二维码参数：\n" + msgModel.EventKey + " EventKey " + msgModel.EventKey.Replace("qrscene_", "");
                            //添加扫描日志二维码参数
                            Maticsoft.Common.ErrorLogTxt.GetInstance("微信扫描日志二维码参数").Write(content);
                           
                       // }

                        //优先处理自定义事件信息
                        if (msgModel.Event == "CLICK")
                        {
                            msgXml = Maticsoft.Web.Components.PostMsgHelper.GetEventMsg(msgModel);
                        }
                        else
                        {
                            //优先处理指令消息
                            Maticsoft.WeChat.Model.Core.Command command = Maticsoft.WeChat.BLL.Core.Command.MatchCommand(msgModel);
                            Maticsoft.WeChat.Model.Core.PostMsg postMsg = new PostMsg();
                            if (command != null)
                            {
                                msgXml = Maticsoft.Web.Components.PostMsgHelper.GetCommandMsg(command, msgModel);
                            }
                            else
                            {
                                //处理连续指令信息
                                int actionId = PostMsgHelper.GetActionCache(msgModel.UserName);
                                if (actionId > 0)
                                {
                                    msgXml = Maticsoft.Web.Components.PostMsgHelper.GetContinueMsg(actionId, msgModel);
                                }
                                //然后处理关键字消息
                                else
                                {
                                    msgXml = Maticsoft.WeChat.BLL.Core.PostMsg.SendPostMsg(msgModel);
                                }
                                //没有消息 只能获取自动回复消息
                                if (String.IsNullOrWhiteSpace(msgXml))
                                {
                                    //是否转发多客服
                                    bool IsTransfer = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_CustomTransfer", msgModel.OpenId), false);
                                    msgXml = Maticsoft.Web.Components.PostMsgHelper.GetAutoMsg(msgModel, IsTransfer);
                                }
                            }
                        }
                        context.Response.Write(msgXml);
                    }
                    catch (Exception ex)
                    {
                        //添加错误日志
                        Maticsoft.Web.LogHelp.AddErrorLog(ex.Message, ex.StackTrace, context.Request);
                    }
                    context.Response.End();
                }
            }
            else
            {
                //验证信息
                Verification(context);
            }
        }
        #endregion
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature(HttpContext context)
        {
            //获取微信设置的Token
            string Token = BLL.SysManage.ConfigSystem.GetValueByCache("System_WeChat_Token");	//与那边填写的token一致
            // string Token = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_Token");
            string signature = context.Request.QueryString["signature"];
            string timestamp = context.Request.QueryString["timestamp"];
            string nonce = context.Request.QueryString["nonce"];
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        private void Verification(HttpContext context)
        {
            string echoStr = context.Request.QueryString["echoStr"];
            if (CheckSignature(context))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    context.Response.Write(echoStr);
                    context.Response.End();
                }
            }
        }

        /// <summary>
        /// 形成时间戳，组成图片名字
        /// </summary>
        /// <returns></returns>
        private string CreateIDCode()
        {
            DateTime Time1 = DateTime.Now.ToUniversalTime();
            DateTime Time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = Time1 - Time2;   //span就是两个日期之间的差额   
            string t = span.TotalMilliseconds.ToString("0");
            return t;
        }

        private string GetAvatarImg(string img, string username)
        {
            if (String.IsNullOrWhiteSpace(img))
            {
                return "";
            }
            string weChatImage;

            using (System.Net.WebClient webclient = new System.Net.WebClient())
            {
                string savePath = "/Upload/WeChat/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                }
                //TODO: 文件格式写死将导致图片显示不正确, 如PNG透明色变黑 TO涂 BEN ADD 20140218
                weChatImage = savePath + username + ".jpg";
                try
                {
                    webclient.DownloadFile(img, HttpContext.Current.Server.MapPath(weChatImage));
                }
                catch
                {
                    return img;
                }
            }
            return weChatImage;
        }

    }
}