using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class Statistics : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 674; } } //移动办公_考勤统计页
        Maticsoft.BLL.JLT.AttendanceType blltype = new BLL.JLT.AttendanceType();
        Maticsoft.BLL.JLT.UserAttendance bll = new BLL.JLT.UserAttendance();
        User user = new User();

        public int UserID
        {
            get
            {
                return Globals.SafeInt(Request.Params["id"], -1);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropRevStatus.Items.Add(new ListItem("未处理", "0"));
                DropRevStatus.Items.Add(new ListItem("已处理", "1"));
                DropRevStatus.Items.Insert(0, new ListItem("--请选择--", "-1"));

                gridView.OnBind();
            }
        }

        public void BindData()
        {
            DataSet ds = new DataSet();
            DateTime startDate = Globals.SafeDateTime(txtStartDate.Text, DateTime.MinValue);
            DateTime endDate = Globals.SafeDateTime(txtEndDate.Text, DateTime.MinValue);
            int reviewedStatus = Globals.SafeInt(DropRevStatus.SelectedValue, -1);

            if (CurrentUser == null)
            {
                MessageBox.ShowFailTip(this, "用户不存在");
                return;
            }
            ds = bll.Statistics(CurrentUser, startDate, endDate, reviewedStatus);
            if (ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.ShowFailTip(this, "无考勤记录");
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