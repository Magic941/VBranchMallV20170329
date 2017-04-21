/**
* WeChatNotify.cs
*
* 功 能： 微信支付通知扩展
* 类 名： WeChatNotify
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/21 3:24:17  Ben    初版
*
* Copyright (c) 2014 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Collections.Specialized;
using Maticsoft.Payment.PaymentInterface.WeChat.Utils;
using Maticsoft.Model.Shop.Order;
using System.Web;
using System.Web.SessionState;
using Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage;
using System.Xml;
using System.Collections.Generic;
using System;
using Maticsoft.Payment.Model;

namespace Maticsoft.Web.Handlers.Shop.Pay.WeChat
{
    //public class WeChatNotify : Maticsoft.Payment.PaymentInterface.WeChat.WeChatNotify //Maticsoft.Payment.Handler
    public class WeChatNotify : Maticsoft.Web.Handlers.Shop.Pay.PaymentReturnHandler //IHttpHandler, IRequiresSessionState
    {
        //private const string URL_DELIVER = "https://api.weixin.qq.com/pay/delivernotify?access_token={0}";

        //protected override void AutoDeliverNotify()
        //{
            //TODO: 是否开启自动发货, 默认开启

            //WxPayHelper wxPayHelper = new WxPayHelper();
            //string wechatData = SetPayHelperBase(wxPayHelper);

            //string AppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, "AA");
            //string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, "AA");
            //string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
            //if (string.IsNullOrWhiteSpace(token)) return;


            //string jsonStr = GetResponse(string.Format(URL_DELIVER, token), wechatData);
            //LogHelp.AddErrorLog(jsonStr, "", "AutoDeliverNotify");
            #region TODO: 自动发货成功相关操作
            //if (string.IsNullOrWhiteSpace(jsonStr)) return;

            //try
            //{
            //    Maticsoft.Json.JsonObject jsonObject = JsonConvert.Import<JsonObject>(jsonStr);
            //    if (jsonObject["errcode"] != null && jsonObject["errcode"].ToString() == "0")
            //    {
            //        //发货成功
            //    }
            //}
            //catch (Exception)
            //{
            //} 
            #endregion
        //}
        //public bool IsReusable
        //{
        //    get { return false; }
        //}

        //public void ProcessRequest(HttpContext context)
        //{
        //    ReturnMessage returnMsg = new ReturnMessage() { Return_Code = "SUCCESS", Return_Msg = "" };
        //    string xmlString = GetXmlString(context.Request);
        //    NotifyMessage message = null;
        //    try
        //    {
        //        此处应记录日志
        //        message = HttpClientHelper.XmlDeserialize<NotifyMessage>(xmlString);

        //        #region 验证签名并处理通知
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(xmlString);

        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        string sign = string.Empty;
        //        foreach (XmlNode node in doc.FirstChild.ChildNodes)
        //        {
        //            if (node.Name.ToLower() != "sign")
        //                dic.Add(node.Name, node.InnerText);
        //            else
        //                sign = node.InnerText;
        //        }

        //        UnifiedWxPayHelper model = UnifiedWxPayHelper.CreateUnifiedHelper(WeiXinConst.AppId, WeiXinConst.PartnerId, WeiXinConst.PartnerKey);
        //        if (model.ValidateMD5Signature(dic, sign))
        //        {
        //            处理通知
        //            if (OrderProcessor.CheckAction(this.Order, OrderActions.BUYER_PAY) && Option.PayForOrder(this.Order, this.Notify.GetNotifyId()))
        //        }
        //        else
        //        {
        //            throw new Exception("签名未通过！");
        //        }

        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        此处记录异常日志
        //        returnMsg.Return_Code = "FAIL";
        //        returnMsg.Return_Msg = ex.Message;
        //    }
        //    HttpContext.Current.Response.Write(returnMsg.ToXmlString());
        //    HttpContext.Current.Response.End();

        //}

        /// <summary>
        /// 获取Post Xml数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //private string GetXmlString(HttpRequest request)
        //{
        //    using (System.IO.Stream stream = request.InputStream)
        //    {
        //        Byte[] postBytes = new Byte[stream.Length];
        //        stream.Read(postBytes, 0, (Int32)stream.Length);
        //        return System.Text.Encoding.UTF8.GetString(postBytes);
        //    }
        //}
    }
}