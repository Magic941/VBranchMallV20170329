using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;
using Maticsoft.WeChat.BLL;
using System.IO;

namespace Maticsoft.Web.Admin.WeChat.User
{
    public partial class UserList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 650; } } //移动营销_微信用户管理_列表页
        protected new int Act_DelData = 651;  //移动营销_微信用户管理_删除数据
        Maticsoft.WeChat.BLL.Core.User bll = new Maticsoft.WeChat.BLL.Core.User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                        Maticsoft.WeChat.BLL.Core.Group groupBll = new Maticsoft.WeChat.BLL.Core.Group();
                       DataSet ds= groupBll.GetAllList();
                        ddGroup.DataSource = ds;
                        ddGroup.DataTextField = "GroupName";
                        ddGroup.DataValueField = "GroupId";
                        ddGroup.DataBind();
                        ddGroup.Items.Insert(0, new ListItem("移动到...", "0"));
                    }
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView
        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();
            //关注状态
            if (!String.IsNullOrWhiteSpace(this.ddStatus.SelectedValue))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("Status={0}", this.ddStatus.SelectedValue);
            }
            if (!String.IsNullOrWhiteSpace(this.txtFrom.Text) && Common.PageValidate.IsDateTime(this.txtFrom.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreateTime >='" + this.txtFrom.Text + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(this.txtTo.Text) && Common.PageValidate.IsDateTime(this.txtTo.Text))
            {

                string endTime = Common.Globals.SafeDateTime(this.txtTo.Text, DateTime.Now).AddDays(1).ToString("yyyy-MM-dd");
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreateTime <='" + endTime + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(this.txtKeyword.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" (NickName like '%{0}%' or UserName  like '%{0}%')", this.txtKeyword.Text);
            }
            //只显示24小时消息用户
            bool IsChkHours = chkHours.Checked;
            if (IsChkHours)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("    LastMsgTime>='{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
            }
            gridView.DataSetSource = bll.GetList(-1, strWhere.ToString(), "CreateTime desc");
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (bll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
        }
        //批量分组
        protected void ddGroup_Changed(object sender, EventArgs e)
        {
            int groupId = Common.Globals.SafeInt(ddGroup.SelectedValue, 0);
            if (groupId == 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this,"请选择分组！");
                return;
            }
            string ids= GetSelIDlist();
            if (String.IsNullOrWhiteSpace(ids))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择用户！");
                return; 
            }
            if (bll.UpdateGroup(groupId, ids))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "分组成功！");
            }
            gridView.OnBind();
        }
        
        
                    //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
           
            string ids= GetSelIDlist();
            if (string.IsNullOrWhiteSpace(ids))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请先选择用户！"); 
                return;
            }
            if (bll.DeleteList( ids))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量删除成功！");
            }
            gridView.OnBind();
        }

        protected void btnGetUserInfo_Click(object sender, EventArgs e)
        {
              string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            //先授权 
            string AppId =Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
            if (String.IsNullOrWhiteSpace(token))
            {
                MessageBox.ShowFailTip(this, "获取微信授权失败！请检查您的微信API设置和对应的权限");
                return;
            }
            string ids= GetSelIDlist();
            if (String.IsNullOrWhiteSpace(ids))
            {
                MessageBox.ShowFailTip(this, "请选择要获取数据的用户");
                return;
            }
            List<Maticsoft.WeChat.Model.Core.User> UserList = bll.GetUserList(ids,openId);
            Maticsoft.WeChat.Model.Core.User userModel= null;
            foreach (var item in UserList)
            {
                userModel = bll.GetWcInfo(token, item.UserName);
                if (userModel == null)
                    continue;
                item.NickName = userModel.NickName;
                item.Province = userModel.Province;
                item.City = userModel.City;
                item.Country = userModel.Country;
                //处理微信用户图片

                item.Headimgurl = GetHeadImg(userModel.Headimgurl, item.UserName);
                item.Language = userModel.Language;
                item.Sex = userModel.Sex;
                bll.Update(item);
            }   

            MessageBox.ShowSuccessTip(this, "获取用户信息成功！");
            gridView.OnBind();
        }

        protected string GetUserStatus(object target)
        {
            //0:取消关注、1:关注、
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "取消关注";
                        break;
                    case "1":
                        str = "关注";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        /// <summary>
        /// 获取分组名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetGroupName(object target)
        {
            //0:取消关注、1:关注、
            string str = "未分组";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int groupId = Common.Globals.SafeInt(target, -1);
                Maticsoft.WeChat.BLL.Core.Group groupBll = new Maticsoft.WeChat.BLL.Core.Group();
                Maticsoft.WeChat.Model.Core.Group groupModel = groupBll.GetModelByCache(groupId);
                str = groupModel == null ? str : groupModel.GroupName;
            }
            return str;
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetUserName(object target)
        {
            //0:取消关注、1:关注、
            string str = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int userId = Common.Globals.SafeInt(target, -1);
                Maticsoft.BLL.Members.Users userBll=new BLL.Members.Users();
                Maticsoft.Model.Members.Users userModel = userBll.GetModelByCache(userId);
                str = userModel == null ? str : userModel.UserName;
            }
            return str;
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;

                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }
        #endregion

        #region  Ajax 方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateUserName":
                    writeText = UpdateUserName();
                    break;
                case "IsCanSend":
                    writeText = IsCanSend();
                    break;
                case "SendMsg":
                    writeText = SendMsg();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string UpdateUserName()
        {
            JsonObject json = new JsonObject();
            int Id = Common.Globals.SafeInt(this.Request.Form["ID"], 0);
            string UserName = this.Request.Params["UpdateValue"];
            if (string.IsNullOrWhiteSpace(UserName))
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                if (bll.UpdateNick(Id, UserName))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }

        private string IsCanSend()
        {
            JsonObject json = new JsonObject();
            string UserName = this.Request.Params["User"];
            if (string.IsNullOrWhiteSpace(UserName))
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                   int hour = Common.Globals.SafeInt("WeChat_PushHour", 48);
                   if (bll.IsCanSend(UserName, hour))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }

        private string SendMsg()
        {
            JsonObject json = new JsonObject();
            string UserName = this.Request.Params["User"];
            string content = Request.Params["Content"];
            if (string.IsNullOrWhiteSpace(UserName))
            {
                json.Put("STATUS", "FAILED");
                return json.ToString();
            }
            else
            {
                string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
                string AppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
                string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
                string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
             
                if (String.IsNullOrWhiteSpace(token))
                {
                    json.Put("STATUS", "NoToken");
                    return json.ToString();
                }
                List<string> users = new List<string>();
                users.Add(UserName);
                Maticsoft.WeChat.Model.Core.CustomerMsg msg = new Maticsoft.WeChat.Model.Core.CustomerMsg();
                msg.OpenId = openId;
                msg.CreateTime = DateTime.Now;
                msg.Description = content;
                msg.MsgType = "text";
                Maticsoft.WeChat.BLL.Core.CustomerMsg.SendCustomMsg(msg, token, users);
                json.Put("STATUS", "SUCCESS");
            }
            return json.ToString();
        }

        private string GetHeadImg(string img,string username)
        {
            if (String.IsNullOrWhiteSpace(img))
            {
                return "/Images/weixin.jpg";
            }
            System.Net.WebClient webclient = new System.Net.WebClient();
            string savePath = "/Upload/WeChat/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
            }
            string WeChatImage = savePath + username + ".jpg";
            webclient.DownloadFile(img, HttpContext.Current.Server.MapPath(WeChatImage));
           return WeChatImage;
        }
        #endregion
    }
}