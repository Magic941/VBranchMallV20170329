using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.AttendanceType
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 659; } } //移动办公_考勤类型管理_添加页
        Maticsoft.BLL.JLT.AttendanceType bll = new BLL.JLT.AttendanceType();
        Maticsoft.Model.JLT.AttendanceType model = new Model.JLT.AttendanceType();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            model.TypeName = txtTypeName.Text;
            model.Status = int.Parse(dropStatus.SelectedValue);
            model.Remark = txtRemark.Text;
            model.CreatedDate = DateTime.Now;
            model.Sequence = Globals.SafeInt(txtSequence.Text, 0);
            if (bll.Add(model) > 0)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("新增考勤类型：【{0}】", model.TypeName), this);
                MessageBox.ShowSuccessTip(this, "新增考勤类型成功", "list.aspx");
            }
            
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}