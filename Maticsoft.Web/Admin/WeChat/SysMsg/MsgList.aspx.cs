using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.IO;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.WeChat.SysMsg
{
    public partial class MsgList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 648; } } //移动营销_系统消息设置页
        protected new int Act_UpdateData = 649;    //移动营销_系统消息设置_编辑数据
        Maticsoft.WeChat.BLL.Core.SysMsg bll = new Maticsoft.WeChat.BLL.Core.SysMsg();
        Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {

                if (String.IsNullOrWhiteSpace(OpenId))
                {
                    MessageBox.ShowFailTip(this, "您还未填写微信原始ID，请在公众号配置中填写！", "/admin/WeChat/Setting/Config.aspx");
                }

                //商品一级分类
                this.ddCategory_S.DataSource = cateBll.GetCategorysByDepth(1);
                this.ddCategory_S.DataTextField = "Name";
                this.ddCategory_S.DataValueField = "CategoryId";
                this.ddCategory_S.DataBind();
                this.ddCategory_S.Items.Insert(0, new ListItem("请选择", "0"));

                this.ddCategory_R.DataSource = cateBll.GetCategorysByDepth(1);
                this.ddCategory_R.DataTextField = "Name";
                this.ddCategory_R.DataValueField = "CategoryId";
                this.ddCategory_R.DataBind();
                this.ddCategory_R.Items.Insert(0, new ListItem("请选择", "0"));

                List<Maticsoft.WeChat.Model.Core.SysMsg> Subscription = bll.GetSubscribeMsg(OpenId);
                List<Maticsoft.WeChat.Model.Core.SysMsg> ReplyMsg = bll.GetAutoReplyMsg(OpenId);
                if (Subscription != null && Subscription.Count > 0 )
                {
                    if( Subscription[0].MsgType=="text")
                    {
                           this.txtSubscription.Text = Subscription[0].Description;
                    this.msgType_S.SelectedValue="text";
                    }
                    else
                    {
                        this.msgType_S.SelectedValue="news";
                    }
                }

                if (ReplyMsg != null && ReplyMsg.Count > 0)
                {
                    if (ReplyMsg[0].MsgType == "text")
                    {
                        this.txtReplyMsg.Text = ReplyMsg[0].Description;
                        this.msgType_R.SelectedValue = "text";
                    }
                    else
                    {
                        this.msgType_R.SelectedValue = "news";
                    }
                }

            }
        }

        public string OpenId
        {
            get
            {
                return Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool IsSuccess = true;
            //保存关注文本消息
          
            Maticsoft.WeChat.Model.Core.SysMsg sysMsg=null;

            if (msgType_S.SelectedValue == "text")
            {
                sysMsg = new Maticsoft.WeChat.Model.Core.SysMsg();
                sysMsg.OpenId = OpenId;
                sysMsg.CreateTime = DateTime.Now;
                sysMsg.SysType = 1;
                sysMsg.MsgType = "text";
                sysMsg.Description = RemoveSpecifyHtml(this.txtSubscription.Text).Replace("&nbsp;", "").Replace("target=\"_self\"","").Replace("target=\"_blank\"","");
                if (bll.AddEx(sysMsg) == 0)
                {
                    IsSuccess = false;
                }
            }
            if (msgType_R.SelectedValue == "text")
            {
                sysMsg = new Maticsoft.WeChat.Model.Core.SysMsg();
                sysMsg.OpenId = OpenId;
                sysMsg.CreateTime = DateTime.Now;
                sysMsg.SysType = 2;
                sysMsg.MsgType = "text";
                sysMsg.Description = RemoveSpecifyHtml(this.txtReplyMsg.Text).Replace("&nbsp;", "").Replace("target=\"_self\"", "").Replace("target=\"_blank\"", "");
                if (bll.AddEx(sysMsg) == 0)
                {
                    IsSuccess = false;
                }
            }
            if (IsSuccess)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "操作成功！");
                Response.Redirect("MsgList.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败！");
            }

        }
        /// <summary>
        /// 过滤多余的标签，只保留a标签
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private static string RemoveSpecifyHtml(string ctx)
        {
            string[] holdTags = { "a" };//保留的 tag
            string regStr = string.Format(@"<(?!((/?\s?{0})))[^>]+>", string.Join(@"\b)|(/?\s?", holdTags));
            Regex reg = new Regex(regStr, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return reg.Replace(ctx, "");
        }

        #region Ajax 方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "AddMsg":
                    writeText = AddMsg();
                    break;
                case "DeleteMsg":
                    writeText = DeleteMsg();
                    break;
                case "GetMsgList":
                    writeText = GetMsgList();
                    break;
                default:
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }


        /// <summary>
        /// 添加回复
        /// </summary>
        /// <returns></returns>
        private string AddMsg()
        {
            JsonObject json = new JsonObject();
            string savePath = "/Upload/WeChat/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            string desc = Request.Form["Desc"];
            string imgUrl = Request.Form["ImageUrl"];
            int  sysType =Common.Globals.SafeInt(Request.Form["SysType"],0) ;
            string title = Request.Form["Title"];
            int categoryId = Common.Globals.SafeInt(Request.Form["Category"], 0);
            int action = Common.Globals.SafeInt(Request.Form["UrlType"], 0);
            string urltext = Request.Form["Url"];
            json.Put("STATUS", "FAILED");

            Maticsoft.WeChat.Model.Core.SysMsg sysMsg = new Maticsoft.WeChat.Model.Core.SysMsg();
            sysMsg.Description = desc;
            sysMsg.MsgType = "news";
            sysMsg.OpenId = OpenId;
            sysMsg.Title = title;
            sysMsg.CreateTime = DateTime.Now;
            sysMsg.SysType = sysType;

            //移动图片 
            string tempImg = imgUrl;
            string imgname = tempImg.Substring(tempImg.LastIndexOf("/") + 1);
            string saveImg = tempImg;
            if (!String.IsNullOrWhiteSpace(tempImg) && tempImg.Contains("/Upload/Temp"))
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(tempImg, "N_"))))
                {
                    string originalUrl = String.Format(savePath + imgname, "N_");
                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(tempImg, "N_")), HttpContext.Current.Server.MapPath(originalUrl));
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(tempImg, "T_"))))
                {
                    string originalUrl = String.Format(savePath + imgname, "T_");
                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(tempImg, "T_")), HttpContext.Current.Server.MapPath(originalUrl));
                }
                saveImg = savePath + imgname;
            }
            sysMsg.PicUrl = saveImg;
            switch (action)
            {
                case 0:
                    sysMsg.Url = urltext;
                    break;
                case 1:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                    break;
                case 2:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MPage);
                    break;
                case 3:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u";
                    break;
                case 4:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u/Orders";
                    break;
                case 5:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "p/" + categoryId;
                    break;
                case 6:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "account/usercard";
                    break;
                case 7:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "UserCenter/signpoint";
                    break;
                default:
                    sysMsg.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                    break;
            }
            int result=bll.AddEx(sysMsg);
            if (result>0)
            {
                json.Put("STATUS", "Success");
                json.Put("Data", result);
                json.Put("Url", sysMsg.Url);
                json.Put("picurl", sysMsg.PicUrl);
            }
            return json.ToString();
        }

        /// <summary>
        /// 删除回复
        /// </summary>
        /// <returns></returns>
        private string DeleteMsg()
        {
            JsonObject json = new JsonObject();
            int msgId = Common.Globals.SafeInt(this.Request.Form["MsgId"], 0);
            json.Put("STATUS", "FAILED");
            if (bll.Delete(msgId))
            {
                json.Put("STATUS", "Success");
            }
            return json.ToString();
        }

        private string GetMsgList()
        {
            int type = Common.Globals.SafeInt(this.Request.Form["SysType"], 0);
            List<Maticsoft.WeChat.Model.Core.SysMsg> sysList = new List<Maticsoft.WeChat.Model.Core.SysMsg>();
            if(type==1)
            {
                 sysList = bll.GetSubscribeMsg(OpenId);
            }
            else
            {
                 sysList = bll.GetAutoReplyMsg(OpenId);
            }
            sysList = sysList.Where(c => c.MsgType == "news").ToList();
             JsonObject json = new JsonObject();
                    JsonArray newsArry = new JsonArray();
                     JsonObject itemObj = null;
                    foreach (var item in sysList)
                    {
                        itemObj = new JsonObject(); 
                        itemObj.Accumulate("title", item.Title);
                        itemObj.Accumulate("desc", item.Description);
                        itemObj.Accumulate("url", item.Url);
                        itemObj.Accumulate("picurl", item.PicUrl);
                        itemObj.Accumulate("msgId", item.SysMsgId);
                        newsArry.Add(itemObj);
                    }
                    json.Accumulate("Data",newsArry.ToString());
              return json.ToString();
        }

        #endregion
    }
}