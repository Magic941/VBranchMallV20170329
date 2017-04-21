using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Data;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.WeChat.Activity
{
    public partial class InfoList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll = new Maticsoft.WeChat.BLL.Activity.ActivityInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        #region gridView
        /// <summary>
        /// 活动类型
        /// </summary>
        protected int Type
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    id = Globals.SafeInt(Request.Params["type"], 0);
                }
                return id;
            }
        }

        public void BindData()
        {

            StringBuilder whereStr = new StringBuilder();
            string keyword = this.txtKeyword.Text;
                whereStr.AppendFormat(" Type={0}", Type);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
              
                whereStr.AppendFormat(" and  Name Like '%{0}%'", keyword);
            }
            DataSet ds = infoBll.GetList(whereStr.ToString());
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
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
            int activityId = (int)gridView.DataKeys[e.RowIndex].Value;
            infoBll.Delete(activityId);
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "StartActivity")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int status = Common.Globals.SafeInt(Args[1], 0);
                    if (status>0)
                    {
                        MessageBox.ShowSuccessTip(this, "该活动已经启用！请不要重复启用");
                        return;
                    }
                    int activityId = Common.Globals.SafeInt(Args[0], 0);
                    if (infoBll.StartActivity(activityId))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
            if (e.CommandName == "CloseActivity")
            {
                if (e.CommandArgument != null)
                {
                    int activityId = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    if (infoBll.UpdateStatus(activityId,2))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetUserName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int userId = Common.Globals.SafeInt(target, 0);
                Maticsoft.Accounts.Bus.User userModel = new Maticsoft.Accounts.Bus.User(userId);
                str = userModel == null ? "" : userModel.NickName;
            }
            return str;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GeStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "不启用";
                        break;
                    case 1:
                        str = "启用";
                        break;
                    case 2:
                        str = "关闭";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取活动类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GeTypeName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "刮刮卡";
                        break;
                    case 1:
                        str = "大转盘";
                        break;
                    case 2:
                        str = "砸金蛋";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public string GeAwardType(object target)
        {
            string str = "自定义奖项";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "自定义奖项";
                        break;
                    case 1:
                        str = "商城优惠券";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        


        #endregion

        #region Ajax 方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "StartActivity":
                    writeText = StartActivity();
                    break;
                default:
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        /// <summary>
        /// 删除回复
        /// </summary>
        /// <returns></returns>
        private string StartActivity()
        {
            JsonObject json = new JsonObject();
            int activityId = Common.Globals.SafeInt(this.Request.Form["ActivityId"], 0);
            json.Put("STATUS", "FAILED");
            //查询礼品
            Maticsoft.WeChat.BLL.Activity.ActivityAward awardBll = new Maticsoft.WeChat.BLL.Activity.ActivityAward();
            int count= awardBll.GetRecordCount(" ActivityId=" + activityId);
            if (count == 0)
            {
                json.Put("STATUS", "NoAward");
                return json.ToString();
            }
            if (infoBll.StartActivity(activityId))
            {
                json.Put("STATUS", "Success");
            }
            return json.ToString();
        }
        #endregion 
    }
}