using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;
using System.IO;

namespace Maticsoft.Web.Admin.WeChat.Push
{
    public partial class AddMsg : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.User userBll = new Maticsoft.WeChat.BLL.Core.User();
        private Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
        private Maticsoft.WeChat.BLL.Core.MsgItem itemBll = new Maticsoft.WeChat.BLL.Core.MsgItem();
        private Maticsoft.WeChat.BLL.Core.Group groupBll = new Maticsoft.WeChat.BLL.Core.Group();
        private Maticsoft.WeChat.BLL.Push.TaskMsg msgBll = new Maticsoft.WeChat.BLL.Push.TaskMsg();
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
                    string nickName = userBll.GetNickName(UserName);
                    if (String.IsNullOrWhiteSpace(nickName))
                    {
                        nickName = UserName;
                    }
                    this.lblTipTitle.Text = "给用户：<span class='newstitle'>" + nickName + " </span>推送任务消息";
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

        protected void btnSaveText_Click(object sender, EventArgs e)
        {
            //先授权 
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            if (String.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.ShowFailTip(this, "请输入要发送的内容！");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtPublishDate.Text))
            {
                MessageBox.ShowFailTip(this, "请输入发送时间！");
                return;
            }
            int groupId = 0;
            if (String.IsNullOrWhiteSpace(UserName))
            {
                //获取用户列表 
                groupId = Common.Globals.SafeInt(this.ddlGroup.SelectedValue, 0);
            }
            Maticsoft.WeChat.Model.Push.TaskMsg msg = new Maticsoft.WeChat.Model.Push.TaskMsg();
            msg.OpenId = openId;
            msg.GroupId = groupId;
            msg.UserName = UserName;
            msg.PublishDate = Common.Globals.SafeDateTime(txtPublishDate.Text, DateTime.Now);
            msg.CreatedDate = DateTime.Now;
            msg.Description = txtContent.Text;
            msg.MsgType = "text";
            if (msgBll.Add(msg) > 0)
            {
                MessageBox.ShowSuccessTip(this, "添加任务消息成功！", "MsgList.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }

            this.txtContent.Text = "";
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
                case "AddItemMsg":
                    writeText = AddItemMsg();
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
        private string AddItemMsg()
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
            DateTime publishDate = Common.Globals.SafeDateTime(Request.Form["Date"], DateTime.Now);
            //获取用户列表 
            int groupId = Common.Globals.SafeInt(this.ddlGroup.SelectedValue, 0);
            Maticsoft.WeChat.Model.Push.TaskMsg msg = new Maticsoft.WeChat.Model.Push.TaskMsg();
            msg.OpenId = OpenId;
            msg.CreatedDate = DateTime.Now;
            msg.MsgType = "news";
            msg.PublishDate = publishDate;
            msg.UserName = UserName;
            msg.GroupId = groupId;
            msg.MsgItems = itemBll.GetItemList(ItemIds);
            msg.ArticleCount = msg.MsgItems.Count;
            if (msgBll.AddEx(msg) > 0)
            {
                json.Put("STATUS", "Success");
            }
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

            if (String.IsNullOrWhiteSpace(token))
            {
                json.Put("STATUS", "NoToken");
                return json.ToString();
            }
            //获取用户列表 
            int groupId = Common.Globals.SafeInt(this.ddlGroup.SelectedValue, 0);

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
            Maticsoft.WeChat.Model.Push.TaskMsg msg = new Maticsoft.WeChat.Model.Push.TaskMsg();
            msg.MediaId = mediaId;
            msg.OpenId = OpenId;
            msg.CreatedDate = DateTime.Now;
            msg.PublishDate = Common.Globals.SafeDateTime(Request.Form["Date"], DateTime.Now);
            msg.MsgType = "voice";
            msg.VoiceUrl = originalUrl;
            msg.UserName = UserName;
            msg.GroupId = groupId;
            if (msgBll.AddEx(msg) > 0)
            {
                json.Put("STATUS", "Success");
            }
            return json.ToString();
        }
        #endregion
    }


}