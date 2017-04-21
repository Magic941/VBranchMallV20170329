using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class Approval : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 670; } } //移动办公_考勤管理_编辑页
        Maticsoft.BLL.JLT.UserAttendance bll = new BLL.JLT.UserAttendance();
        Maticsoft.Model.JLT.UserAttendance model = new Model.JLT.UserAttendance();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public int AttID
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

        public void ShowInfo()
        {
            if (bll.Exists(AttID))
            {
                model = bll.GetModel(AttID);
                ltlAttID.Text = model.ID.ToString();
                ltlUserName.Text = model.UserName;
                ltlTrueName.Text = model.TrueName;
                ltlAttDate.Text = model.AttendanceDate.ToString();
                txtScore.Text = model.Score.ToString();
                drop_Status.SelectedValue = model.Status.ToString();
                if (model.ReviewedStatus != 1)
                {
                    ltlRevStatus.Text = "未处理";
                }
                else
                {
                    ltlRevStatus.Text = "已处理";
                }
                txtRevDescription.Text = model.ReviewedDescription;
                txtRemark.Text = model.Remark;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            model = bll.GetModel(AttID);
            model.ID = AttID;
            model.Score = int.Parse(txtScore.Text);
            model.ReviewedStatus = 1;
            model.ReviewedDescription = txtRevDescription.Text;
            model.Remark = txtRemark.Text;
            model.ReviewedDate = DateTime.Now;
            model.ReviewedUserID = CurrentUser.UserID;
            if (bll.Update(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批复考勤：【{0}】", model.ID), this);

                MessageBox.ShowSuccessTip(this, "批复考勤成功！","javascript:history.go(-2)");
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>history.go(-2)</script>");
        }
    }
}