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
    public partial class PersonalList : PageBaseAdmin
    {
        Maticsoft.BLL.JLT.AttendanceType blltype = new BLL.JLT.AttendanceType();
        Maticsoft.BLL.JLT.UserAttendance bll = new BLL.JLT.UserAttendance();
        Maticsoft.Model.JLT.UserAttendance model = new Model.JLT.UserAttendance();
        User user = new User();
        public int UserID
        {
            get
            {
                int id = -1;
                string strid = Request.Params["id"];
                if (!String.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }
        public int TypeID
        {
            get
            {
                int id = -1;
                string strid = Request.Params["type"];
                if (!String.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (TypeID == 1)
                {
                    a_return.Visible = false;
                    appControl.Visible = false;
                }
                DropTypeName.DataSource = blltype.GetAllList();
                DropTypeName.DataTextField = "TypeName";
                DropTypeName.DataValueField = "TypeID";
                DropTypeName.DataBind();
                DropTypeName.Items.Insert(0, new ListItem("--全部--", "-1"));

                DropRevStatus.Items.Add(new ListItem("未处理", "0"));
                DropRevStatus.Items.Add(new ListItem("已处理", "1"));
                DropRevStatus.Items.Insert(0, new ListItem("--请选择--", "-1"));

                btnBatch.Attributes.Add("onclick", "return confirm(\"" + Resources.CMS.ContentTooltipBatch + "\")");
                gridView.OnBind();
            }
        }

        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (int.Parse(DropTypeName.SelectedValue) >= 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" TypeID=" + int.Parse(DropTypeName.SelectedValue));
            }
            if (int.Parse(DropRevStatus.SelectedValue) >= 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" ReviewedStatus=" + int.Parse(DropRevStatus.SelectedValue));
            }
            if (!String.IsNullOrWhiteSpace(txtDate.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" CreatedDate >= '" + txtDate.Text + "' and CreatedDate < dateadd(dd,1,'" + txtDate.Text + "') ");
            }
            if (UserID != -1)
            {
                user = new User(UserID);
            }
            else
            {
                user = new User(CurrentUser.UserID);
            }
            if (user == null)
            {
                MessageBox.ShowFailTip(this, "用户不存在");
                return;
            }
            if (strWhere.Length > 0)
            {
                strWhere.Append(" and");
            }
            strWhere.Append(" UserID=" + user.UserID);
            ds = bll.GetList(0, Globals.SafeString(strWhere, ""), " ID DESC");
            if (ds.Tables[0].Rows.Count < 1)
            {
                if (TypeID == 1)
                {
                    MessageBox.ShowFailTip(this, "无考勤记录");
                }
                if (TypeID == 2)
                {
                    MessageBox.ShowFailTip(this, "无考勤记录", "/Admin/Members/MembershipManage/List.aspx");
                }
            }
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
            int revstatus=int.Parse(drop_RevStatus.SelectedValue);
            string revdescription = txtRevDescription.Text;
            string remark = txtRemark.Text;
            if (idlist.Trim().Length == 0)
            {
                return;
            }
            if (revstatus >= 0)
            { 
                if(strWhere.Length>0)
                {
                    strWhere.Append(" ,");
                }
                strWhere.Append(" ReviewedStatus=" + revstatus);
            }
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
            if (strWhere.Length > 0)
            {
                strWhere.Append(" ,");
            }
            strWhere.AppendFormat(" ReviewedUserID={0},ReviewedDate='{1}'", CurrentUser.UserID, DateTime.Now);
            if (strWhere.Length <= 0) return;
            if (bll.UpdateList(idlist, strWhere.ToString()))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量批复考勤：【{0}】", idlist), this);
                MessageBox.ShowSuccessTip(this, "批处理成功！");
                gridView.OnBind();
            }
        }


        #endregion

        public string GetTypeName(object taget)
        {
            int TypeID = Convert.ToInt32(taget);
            string typeName = "";
            if (blltype.Exists(TypeID))
            {
                typeName = blltype.GetModel(TypeID).TypeName;
            }
            return typeName;
        }

        public string GetRevStatus(object taget)
        {
            int Status = Globals.SafeInt(taget.ToString(), 0);
            string strStatus = "";
            switch (Status)
            {
                case 0:
                    strStatus = "未处理";
                    break;
                case 1:
                    strStatus = "已处理";
                    break;
            }
            return strStatus;
        }

        public string GetUserName(object taget)
        {
            int userid = Globals.SafeInt(taget.ToString(), 0);
            if (userid < 1) return "";

            Maticsoft.Accounts.Bus.User user = new User(userid);
            if (user == null) return "";

            return user.UserName;
                
        }

        public string GetTrueName(object taget)
        {
            int userid = Globals.SafeInt(taget.ToString(), -1);
            if (userid < 1) return "";

            Maticsoft.Accounts.Bus.User user = new User(userid);
            if (user == null) return "";

            return user.TrueName;
        }
    }

}