using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;
using System.Text;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class List : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 667; } } //移动办公_考勤管理_列表页
        protected new int Act_UpdateData = 669;    //移动办公_考勤管理_编辑数据
        protected new int Act_DelData = 668;   //移动办公_考勤管理_删除数据
        Maticsoft.BLL.JLT.AttendanceType blltype = new BLL.JLT.AttendanceType();
        Maticsoft.BLL.JLT.UserAttendance bll = new BLL.JLT.UserAttendance();
        Maticsoft.Model.JLT.UserAttendance model = new Model.JLT.UserAttendance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnBatch.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }

                DropTypeName.DataSource = blltype.GetAllList();
                DropTypeName.DataTextField = "TypeName";
                DropTypeName.DataValueField = "TypeID";
                DropTypeName.DataBind();
                DropTypeName.Items.Insert(0, new ListItem("--全部--", "-1"));

                //DropStatus.Items.Add(new ListItem("无效", "0"));
                //DropStatus.Items.Add(new ListItem("有效", "1"));
                //DropStatus.Items.Insert(0, new ListItem("--请选择--", "-1"));

                DropRevStatus.Items.Add(new ListItem("未处理", "0"));
                DropRevStatus.Items.Add(new ListItem("已处理", "1"));
                DropRevStatus.Items.Insert(0, new ListItem("--请选择--", "-1"));

                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                btnBatch.Attributes.Add("onclick", "return confirm(\"" + Resources.CMS.ContentTooltipBatch + "\")");
                gridView.OnBind();
            }
        }

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[9].Visible = false;  
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[10].Visible = false;  
            }
            DataSet ds = new DataSet();
            string keyword = "";
            StringBuilder strWhere = new StringBuilder();
            if (txtTrueName.Text.Trim() != "")
            {
                keyword = txtTrueName.Text.Trim();
            }
            if (CurrentUser.UserType == "UU")
            {
                strWhere.Append(" UserID in( SELECT UserID FROM Accounts_Users WHERE EmployeeID=" + CurrentUser.UserID + " )");
            }
            if (CurrentUser.UserType == "EE")
            {
                strWhere.Append(" UserID in( SELECT UserID FROM Accounts_Users WHERE DepartmentID='" + CurrentUser.DepartmentID + "')");
            }
            if (int.Parse(DropTypeName.SelectedValue) >= 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" TypeID=" + int.Parse(DropTypeName.SelectedValue));
            }
            if (int.Parse(DropRevStatus.SelectedValue) > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" ReviewedStatus=" + int.Parse(DropRevStatus.SelectedValue));
                if (int.Parse(DropRevStatus.SelectedValue) == 0)
                {
                    strWhere.Append(" Or ReviewedStatus is null");
                }
            }
            if (!String.IsNullOrWhiteSpace(txtUserName.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" UserName like '%" + txtUserName.Text + "%'");
            }
            if (!String.IsNullOrWhiteSpace(txtDate.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" CreatedDate >= '" + txtDate.Text + "' and CreatedDate < dateadd(dd,1,'" + txtDate.Text + "') ");
            }
            if (!String.IsNullOrWhiteSpace(txtTrueName.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" TrueName like '%" + keyword + "%'");
            }

            ds = bll.GetList(0, Globals.SafeString(strWhere, ""), " ID DESC");

            gridView.DataSetSource = ds;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView


        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gridView.DataKeys[e.RowIndex].Value;
            if (bll.Delete(id))
            {
                gridView.OnBind();
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除考勤：【{0}】", id), this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
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

        #endregion gridView


        #region 批量操作

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            StringBuilder strWhere = new StringBuilder();
            string idlist = GetSelIDlist();
            string revdescription = txtRevDescription.Text;
            string remark = txtRemark.Text;
            if (idlist.Trim().Length == 0)
            {
                return;
            }
            strWhere.Append(" ReviewedStatus=1,ReviewedUserID=" + CurrentUser.UserID + ",ReviewedDate='" + DateTime.Now + "'");
            if (!String.IsNullOrWhiteSpace(revdescription))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" ,");
                }
                strWhere.Append(" ReviewedDescription='" + revdescription + "'");
            }
            if (!String.IsNullOrWhiteSpace(remark))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" ,");
                }
                strWhere.Append(" Remark='" + remark + "'");
            }
            if (bll.UpdateList(idlist, strWhere.ToString()))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量批复考勤：【{0}】", idlist), this);
                MessageBox.ShowSuccessTip(this, "批处理成功！");
                gridView.OnBind();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除考勤：【{0}】", model.ID), this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        #endregion

        public string GetTypeName(object taget)
        {
            int TypeID = Globals.SafeInt(taget.ToString(), -1);
            if (!blltype.Exists(TypeID)) return "";
            return blltype.GetModel(TypeID).TypeName;
        }

        public string GetRevStatus(object taget)
        {
            int Status = Globals.SafeInt(taget.ToString(), 0);
            string strStatus = "";
            if (Status != 1)
            {
                strStatus = "未处理";
            }
            else
            {
                strStatus = "已处理";
            }
            return strStatus;
        }

        public string GetUserName(object taget)
        {
            int userid = Globals.SafeInt(taget.ToString(), -1);
            if (userid < 0) return "";

            Maticsoft.Accounts.Bus.User user = new User(userid);
            if (user == null) return "";

            return user.UserName;
        }

        public string GetTrueName(object taget)
        {
            int userid = Globals.SafeInt(taget.ToString(), -1);
            if (userid < 0) return "";

            Maticsoft.Accounts.Bus.User user = new User(userid);
            if (user == null) return "";

            return user.TrueName;
        }
    }

}