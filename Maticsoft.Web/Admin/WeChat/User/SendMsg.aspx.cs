using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;
using System.IO;

namespace Maticsoft.Web.Admin.WeChat.User
{
    public partial class SendMsg : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.User userBll = new Maticsoft.WeChat.BLL.Core.User();
        private Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
        private Maticsoft.WeChat.BLL.Core.MsgItem itemBll = new Maticsoft.WeChat.BLL.Core.MsgItem();
        private Maticsoft.WeChat.BLL.Core.Group groupBll = new Maticsoft.WeChat.BLL.Core.Group();
        private Maticsoft.WeChat.BLL.Core.NoReplyMsg replyBll = new Maticsoft.WeChat.BLL.Core.NoReplyMsg();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!IsPostBack)
            {
                //商品一级分类
                this.ddCategory.DataSource = cateBll.GetCategorysByDepth(1);
                this.ddCategory.DataTextField = "Name";
                this.ddCategory.DataValueField = "CategoryId";
                this.ddCategory.DataBind();
                this.ddCategory.Items.Insert(0, new ListItem("请选择", "0"));

                if (String.IsNullOrWhiteSpace(UserName))
                {
                    List<Maticsoft.WeChat.Model.Core.Group> groupList = groupBll.GetGroupList(OpenId);
                    this.ddlGroup.DataSource = groupList;
                    this.ddlGroup.DataTextField = "GroupName";
                    this.ddlGroup.DataValueField = "GroupId";
                    this.ddlGroup.DataBind();
                    this.ddlGroup.Items.Insert(0, new ListItem("全部", "0"));
                    this.lblTipTitle.Visible = false;
                }
                else
                {
                    this.hfUserName.Value = UserName;
                    string nickName = userBll.GetNickName(UserName);
                    if (String.IsNullOrWhiteSpace(nickName))
                    {
                        nickName = UserName;
                    }
                    this.lblTipTitle.Text = "给用户：<span class='newstitle'>" + nickName + " </span>推送消息";
                    this.lblGroup.Visible = false;
                    this.ddlGroup.Visible = false;
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
        /// <summary>
        /// 编号
        /// </summary>
        protected string UserName
        {
            get
            {
                string user = "";
                if (!string.IsNullOrWhiteSpace(Request.QueryString["user"]))
                {
                    user = Request.QueryString["user"];
                }
                return user;
            }
        }

        protected int NoReplyId
        {
            get
            {
                int msgId = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["msgId"]))
                {
                    msgId = Common.Globals.SafeInt(Request.QueryString["msgId"], 0);
                }
                return msgId;
            }
        }

        protected void btnSaveText_Click(object sender, EventArgs e)
        {
            //先授权 
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            string AppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
            if (String.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.ShowFailTip(this, "请输入要发送的内容！");
                return;
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                MessageBox.ShowFailTip(this, "获取微信授权失败！请检查您的微信API设置和对应的权限");
                return;
            }
            List<string> users = new List<string>();
            if (String.IsNullOrWhiteSpace(UserName))
            {
                //获取用户列表 
                int groupId = Common.Globals.SafeInt(this.ddlGroup.SelectedValue, 0);
                List<Maticsoft.WeChat.Model.Core.User> UserList = userBll.GetUserList(OpenId, groupId,480);
                if (UserList.Count == 0)
                {
                    MessageBox.ShowFailTip(this, "没有48小时用户接收消息，请稍后再试");
                    return;
                }
                users = UserList.Select(c => c.UserName).ToList();
            }
            else
            {
                users.Add(UserName);
            }
            Maticsoft.WeChat.Model.Core.CustomerMsg msg = new Maticsoft.WeChat.Model.Core.CustomerMsg();
            msg.OpenId = openId;
            msg.CreateTime = DateTime.Now;
            msg.Description = txtContent.Text;
            msg.MsgType = "text";
            try
            {
                Maticsoft.WeChat.BLL.Core.CustomerMsg.SendCustomMsg(msg, token, users);
                if (NoReplyId > 0)
                {
                    replyBll.UpdateStatus(NoReplyId, 1);
                }
                MessageBox.ShowSuccessTip(this, "发送消息成功！");
                this.txtContent.Text = "";
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message, ex.StackTrace, this);
                MessageBox.ShowFailTip(this, "服务器繁忙，请稍候访问！");
                throw ex;
            }

        }


        protected void btnSaveVoice_Click(object sender, EventArgs e)
        {
            //先授权 
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            string AppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);

            if (String.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.ShowFailTip(this, "请输入要发送的内容！");
                return;
            }
            if (String.IsNullOrWhiteSpace(token))
            {
                MessageBox.ShowFailTip(this, "获取微信授权失败！请检查您的微信API设置和对应的权限");
                return;
            }
            List<string> users = new List<string>();
            if (String.IsNullOrWhiteSpace(UserName))
            {
                //获取用户列表 
                int groupId = Common.Globals.SafeInt(this.ddlGroup.SelectedValue, 0);
                List<Maticsoft.WeChat.Model.Core.User> UserList = userBll.GetUserList(OpenId, groupId,480);
                if (UserList.Count == 0)
                {
                    MessageBox.ShowFailTip(this, "没有48小时用户接收消息，请稍后再试");
                    return;
                }
                users = UserList.Select(c => c.UserName).ToList();
            }
            else
            {
                users.Add(UserName);
            }

            Maticsoft.WeChat.Model.Core.CustomerMsg msg = new Maticsoft.WeChat.Model.Core.CustomerMsg();
            msg.OpenId = openId;
            msg.CreateTime = DateTime.Now;
            msg.Description = txtContent.Text;
            msg.MsgType = "text";
            Maticsoft.WeChat.BLL.Core.CustomerMsg.SendCustomMsg(msg, token, users);
            MessageBox.ShowSuccessTipScript(this, "发送消息成功！", "window.parent.location.reload();");
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
                case "Publish":
                    writeText = Publish();
                    break;
                case "PublishVoice":
                    writeText = PublishVoice();
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
            int sysType = Common.Globals.SafeInt(Request.Form["SysType"], 0);
            string title = Request.Form["Title"];
            int categoryId = Common.Globals.SafeInt(Request.Form["Category"], 0);
            int action = Common.Globals.SafeInt(Request.Form["UrlType"], 0);
            string urltext = Request.Form["Url"];
            json.Put("STATUS", "FAILED");

            Maticsoft.WeChat.Model.Core.MsgItem itemModel = new Maticsoft.WeChat.Model.Core.MsgItem();
            itemModel.Description = desc;
            itemModel.OpenId = OpenId;
            itemModel.Title = title;
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
            itemModel.PicUrl = saveImg;
            switch (action)
            {
                case 0:
                    itemModel.Url = urltext;
                    break;
                case 1:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                    break;
                case 2:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MPage);
                    break;
                case 3:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u";
                    break;
                case 4:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u/Orders";
                    break;
                case 5:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "p/" + categoryId;
                    break;
                default:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                    break;
            }
            int result = itemBll.Add(itemModel);
            if (result > 0)
            {
                json.Put("STATUS", "Success");
                json.Put("Data", result);
                json.Put("Url", itemModel.Url);
                json.Put("picurl", itemModel.PicUrl);
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
            int itemId = Common.Globals.SafeInt(this.Request.Form["ItemId"], 0);
            json.Put("STATUS", "FAILED");
            if (itemBll.Delete(itemId))
            {
                json.Put("STATUS", "Success");
            }
            return json.ToString();
        }

        //发送图文消息
        private string Publish()
        {
            JsonObject json = new JsonObject();
            string ItemIds = this.Request.Form["ItemIds"];
            string AppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
            string userName = this.Request.Form["UserName"];
            int MsgId = Common.Globals.SafeInt(Request.Form["MsgId"], 0);
            
            //if (String.IsNullOrWhiteSpace(UserName))
            //{
            //    json.Put("STATUS", "NoToken");
            //    return json.ToString();
            //}
            if (String.IsNullOrWhiteSpace(token))
            {
                json.Put("STATUS", "NoToken");
                return json.ToString();
            }
            List<string> users = new List<string>();
            if (String.IsNullOrWhiteSpace(userName))
            {
                //获取用户列表 
                int groupId = Common.Globals.SafeInt(this.Request.Form["GroupId"], 0);
                List<Maticsoft.WeChat.Model.Core.User> UserList = userBll.GetUserList(OpenId, groupId);
                if (UserList.Count == 0)
                {
                    json.Put("STATUS", "NoUser");
                    return json.ToString();
                }
                users = UserList.Select(c => c.UserName).ToList();
            }
            else
            {
                users.Add(userName);
            }
            Maticsoft.WeChat.Model.Core.CustomerMsg msg = new Maticsoft.WeChat.Model.Core.CustomerMsg();
            msg.MsgItems = itemBll.GetItemList(ItemIds);
            msg.OpenId = OpenId;
            msg.CreateTime = DateTime.Now;
            msg.MsgType = "news";
            msg.ArticleCount = msg.MsgItems.Count;
            Maticsoft.WeChat.BLL.Core.CustomerMsg.SendCustomMsg(msg, token, users);
            //更改未处理 消息状态
            if (MsgId > 0)
            {
                replyBll.UpdateStatus(MsgId, 1);
            }
            json.Put("STATUS", "Success");
            return json.ToString();
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <returns></returns>
        private string PublishVoice()
        {
            JsonObject json = new JsonObject();
            string Path = this.Request.Form["Path"];
            string AppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
            string userName = this.Request.Form["UserName"];
            int MsgId = Common.Globals.SafeInt(Request.Form["MsgId"], 0);

            if (String.IsNullOrWhiteSpace(token))
            {
                json.Put("STATUS", "NoToken");
                return json.ToString();
            }
            List<string> users = new List<string>();
            if (String.IsNullOrWhiteSpace(userName))
            {
                //获取用户列表 
                int groupId = Common.Globals.SafeInt(this.Request.Form["GroupId"], 0);
                List<Maticsoft.WeChat.Model.Core.User> UserList = userBll.GetUserList(OpenId, groupId);
                if (UserList.Count == 0)
                {
                    json.Put("STATUS", "NoUser");
                    return json.ToString();
                }
                users = UserList.Select(c => c.UserName).ToList();
            }
            else
            {
                users.Add(userName);
            }
            //移动文件
            string videoName = Path.Substring(Path.LastIndexOf("/") + 1);
            string savePath = "/Upload/WeChat/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            string originalUrl = savePath + videoName;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
            }
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path)))
            {
                System.IO.File.Move(HttpContext.Current.Server.MapPath(Path), HttpContext.Current.Server.MapPath(originalUrl));
            }


            //先上传媒体文件
            string mediaId = Maticsoft.WeChat.BLL.Core.Utils.GetMediaId(token, originalUrl, "voice");
            if (String.IsNullOrWhiteSpace(mediaId))
            {
                json.Put("STATUS", "NoMediaId");
                return json.ToString();
            }
            Maticsoft.WeChat.Model.Core.CustomerMsg msg = new Maticsoft.WeChat.Model.Core.CustomerMsg();
            msg.MediaId = mediaId;
            msg.OpenId = OpenId;
            msg.CreateTime = DateTime.Now;
            msg.MsgType = "voice";
            msg.MusicUrl = originalUrl;
            Maticsoft.WeChat.BLL.Core.CustomerMsg.SendCustomMsg(msg, token, users);
            //更改未处理 消息状态
            if (MsgId > 0)
            {
                replyBll.UpdateStatus(MsgId, 1);
            }
            json.Put("STATUS", "Success");
            return json.ToString();
        }
        #endregion
    }


}