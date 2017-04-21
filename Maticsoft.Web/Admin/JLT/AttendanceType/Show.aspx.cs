using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.AttendanceType
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 661; } } //移动办公_考勤类型管理_详细页
        Maticsoft.Model.JLT.AttendanceType model = new Model.JLT.AttendanceType();
        Maticsoft.BLL.JLT.AttendanceType bll = new BLL.JLT.AttendanceType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public void ShowInfo()
        {
            if (bll.Exists(TypeId))
            {
                model = bll.GetModel(TypeId);
                ltlTypeID.Text = model.TypeID.ToString();
                ltlTypeName.Text = model.TypeName;
                ltlSequence.Text = model.Sequence.ToString();
                ltlCreatedDate.Text = model.CreatedDate.ToString();
                ltlRemark.Text = model.Remark;
                if (model.Status == 0)
                {
                    ltlStatus.Text = "无效";
                }
                else
                {
                    ltlStatus.Text = "有效";
                }
            }
        }

        public int TypeId
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

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}