using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.AttendanceType
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 660; } } //移动办公_考勤类型管理_编辑页
        Maticsoft.BLL.JLT.AttendanceType bll = new BLL.JLT.AttendanceType();
        Maticsoft.Model.JLT.AttendanceType model = new Model.JLT.AttendanceType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public int TypeID
        {
            get
            {
                int id = 0;
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
            if (bll.Exists(TypeID))
            {
                model = bll.GetModel(TypeID);
                txtTypeName.Text = model.TypeName;
                txtSequence.Text = model.Sequence.ToString();
                txtRemark.Text = model.Remark;
                dropStatus.SelectedValue = model.Status.ToString();
                hdCreatedDate.Value = model.CreatedDate.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            model.TypeID = TypeID;
            model.TypeName = txtTypeName.Text;
            model.Status = int.Parse(dropStatus.SelectedValue);
            model.Sequence = int.Parse(txtSequence.Text);
            model.Remark = txtRemark.Text;
            model.CreatedDate = Globals.SafeDateTime(hdCreatedDate.Value, DateTime.Now);
            if (bll.Update(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑考勤类型：【{0}】", model.TypeName), this);
                MessageBox.ShowSuccessTip(this, "修改考勤类型成功！", "list.aspx");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}