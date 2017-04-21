using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Common;
using System.Text;
using System.Data;
namespace Maticsoft.Web.Admin.JLT.AttendanceType
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 655; } } //移动办公_考勤类型管理_列表页
        protected new int Act_AddData = 656;    //移动办公_考勤类型管理_添加数据
        protected new int Act_UpdateData = 657;    //移动办公_考勤类型管理_编辑数据
        protected new int Act_DelData = 658;   //移动办公_考勤类型管理_删除数据
        

        Maticsoft.BLL.JLT.AttendanceType bll = new BLL.JLT.AttendanceType();
        Maticsoft.Model.JLT.AttendanceType model = new Model.JLT.AttendanceType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
                gridView.OnBind();
                DropStatus.Items.Add(new ListItem("无效", "0"));
                DropStatus.Items.Add(new ListItem("有效", "1"));
                DropStatus.Items.Insert(0, new ListItem("--请选择--", "-1"));

                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
            }
        }

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[7].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[8].Visible = false;
            }
            DataSet ds = new DataSet();
            string keyword = "";
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                keyword = txtKeyword.Text.Trim();
            }
            if (Globals.SafeInt(DropStatus.SelectedValue, -1) >= 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" Status=" + int.Parse(DropStatus.SelectedValue));
            }
            if (!String.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" TypeName like '%" + keyword + "%' ");
            }

            ds = bll.GetList(0, Globals.SafeString(strWhere, ""), " Sequence ASC ");

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

                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                linkbtnDel.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
            }
        }
    

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            List<string> UserIDlist = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("AdminUserID"), ',', true);
            if (UserIDlist.Contains(ID))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.ErrorCannotDeleteID);
                return;
            }
            try
            {

                model = bll.GetModel(int.Parse(ID));
                string typename = model.TypeName;
                bll.Delete(int.Parse(ID));
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除考勤类型：【{0}】", typename), this);
                Common.MessageBox.ShowSuccessTip(this, "删除成功！");
                gridView.OnBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.ErrorCannotDeleteUser);
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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量删除考勤类型：【{0}】", idlist), this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        protected void drop_Status_Changed(object sender, EventArgs e)
        {
            StringBuilder strWhere = new StringBuilder();
            string idlist = GetSelIDlist();
            int status = int.Parse(drop_Status.SelectedValue);
            if (idlist.Trim().Length == 0)
            {
                return;
            }
            if (status >= 0)
            {
                strWhere.Append(" Status=" + status);
            }
            if (strWhere.Length <= 0) return;
            if (bll.UpdateList(idlist, strWhere.ToString()))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量处理考勤类型：【{0}】", idlist), this);
                MessageBox.ShowSuccessTip(this, "批处理成功！");
                gridView.OnBind();
                drop_Status.SelectedValue = "-1";
            }
        }
        #endregion
        public string GetStatus(object taget)
        {
            int Status = Convert.ToInt32(taget);
            string strStatus = "";
            switch (Status)
            {
                case 0:
                    strStatus = "无效";
                    break;
                case 1:
                    strStatus = "有效";
                    break;
            }
            return strStatus;
        }

    }
}