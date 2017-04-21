using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class FreeList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 553; } } //Shop_活动规则管理_列表页
        protected new int Act_AddData = 554;    //Shop_活动规则管理_添加数据
        protected new int Act_UpdateData = 555;    //Shop_活动规则管理_编辑数据
        protected new int Act_DelData = 556;    //Shop_活动规则管理_删除数据
        Maticsoft.BLL.Shop.ActivityManage.AMBLL amBll = new BLL.Shop.ActivityManage.AMBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                //gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                //gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            amBll.DeleteListEx(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" AMName like '%{0}%'", txtKeyword.Text.Trim());

                if (ddlStatus.SelectedValue == "未开启")
                {
                    strWhere.AppendFormat(" AND AMStartDate>GETDATE() ");
                }
                else if (ddlStatus.SelectedValue == "进行中")
                {
                    strWhere.AppendFormat(" AND AMStartDate<=GETDATE() AND AMEndDate>=GETDATE() ");
                }
                else if (ddlStatus.SelectedValue == "已过期")
                {
                    strWhere.AppendFormat(" AND AMEndDate< GETDATE()");
                }
                ds = amBll.GetList(-1, strWhere.ToString(), " AMCreateDate desc");
                gridView.DataSetSource = ds;
            }
            else
            {
                if (ddlStatus.SelectedValue == "未开启")
                {
                    strWhere.AppendFormat(" AMStartDate>GETDATE() ");
                }
                else if (ddlStatus.SelectedValue == "进行中")
                {
                    strWhere.AppendFormat("  AMStartDate<=GETDATE() AND AMEndDate>=GETDATE() ");
                }
                else if (ddlStatus.SelectedValue == "已过期")
                {
                    strWhere.AppendFormat(" AMEndDate< GETDATE()");
                }
                ds = amBll.GetList(-1, strWhere.ToString(), " AMCreateDate desc");
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
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
                }

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
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            amBll.DeleteEx(ID);
            gridView.OnBind();
            Common.MessageBox.ShowSuccessTip(this, "删除成功", "AMList.aspx");
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
                    if (gridView.DataKeys[i].Value != null)
                    {
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

        public string GetAMType(object obj)
        {
            if (obj == null)
                return string.Empty;
            int amType = Common.Globals.SafeInt(obj.ToString(), -1);
            if (amType < 0) return string.Empty;
            switch (amType)
            {
                case 0:
                    return "折扣";
                case 1:
                    return "减价";
                default:
                    return "折扣";
            }
        }


        #region 应用方式

        public string GetRuleMode(object obj)
        {
            if (obj == null)
                return string.Empty;
            int RuleMode = Common.Globals.SafeInt(obj.ToString(), -1);
            if (RuleMode < 0) return string.Empty;
            switch (RuleMode)
            {
                case 0:
                    return "单个商品";
                case 1:
                    return "全场商品";
                case 2:
                    return "单个商家";
                default:
                    return "单个商品";
            }
        }

        #endregion

        #region 状态

        public string GetStatus(object obj)
        {
            if (obj == null)
                return string.Empty;
            int Status = Common.Globals.SafeInt(obj.ToString(), -1);
            if (Status < 0) return string.Empty;
            switch (Status)
            {
                //0:即将进行 1:进行中(立即申请) 2:已结束
                case 0:
                    return "启用";
                case 1:
                    return "关闭";
                default:
                    return "关闭";
            }
        }

        #endregion

        public string GetUserName(object obj)
        {
            if (obj == null)
                return string.Empty;
            int userId = Common.Globals.SafeInt(obj.ToString(), -1);
            if (userId < 0) return string.Empty;
            Maticsoft.Accounts.Bus.User userModel = new Maticsoft.Accounts.Bus.User(userId);

            return userModel.UserName;
        }

        #region AjaxCallback

        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateStatus":
                    writeText = UpdateStatus();
                    break;
                default:
                    writeText = UpdateStatus();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string UpdateStatus()
        {
            JsonObject json = new JsonObject();
            int AMId = Common.Globals.SafeInt(this.Request.Form["AMId"], 0);
            int AMStatus = Common.Globals.SafeInt(this.Request.Params["AMStatus"], 0);
            if (amBll.UpdateStatus(AMId, AMStatus))
            {
                json.Put("STATUS", "SUCCESS");
            }
            else
            {
                json.Put("STATUS", "FAILED");
            }
            return json.ToString();
        }
        #endregion AjaxCallback
    }
}
