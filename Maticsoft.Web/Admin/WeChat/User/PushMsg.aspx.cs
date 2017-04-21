using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Json;
using System.IO;

namespace Maticsoft.Web.Admin.WeChat.User
{
    public partial class PushMsg : PageBaseAdmin
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
             
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
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
            int hour = Common.Globals.SafeInt("WeChat_PushHour", 48);
            //只显示48小时消息用户
            strWhere.AppendFormat("    LastMsgTime>=dateadd(hh,-{0},getdate())", hour);
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
                Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
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
                if (bll.IsCanSend(UserName))
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

        #endregion
    }
}