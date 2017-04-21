using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 671; } } //移动办公_考勤管理_详细页
        Maticsoft.BLL.JLT.AttendanceType blltype = new BLL.JLT.AttendanceType();
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
                int id = 0;
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

        public void ShowInfo()
        {
            if (bll.Exists(AttID))
            {
                model = bll.GetModel(AttID);
                ltlAttID.Text = model.ID.ToString();
                ltlUserID.Text = model.UserID.ToString();
                ltlUserName.Text = model.UserName;
                ltlTrueName.Text = model.TrueName;
                ltlLatitude.Text = model.Latitude;
                ltlLongitude.Text = model.Longitude;
                ltlKiloMeters.Text = model.Kilometers.ToString();
                if (blltype.Exists(model.TypeID))
                {
                    ltlTypeName.Text = blltype.GetModel(model.TypeID).TypeName;
                }
                ltlCreatedDate.Text = model.CreatedDate.ToString();
                ltlAttDate.Text = model.AttendanceDate.ToShortDateString();
                ltlDescription.Text = model.Description;
                ltlImagePath.Text = model.ImagePath;
                ltlScore.Text = model.Score.ToString();
                switch(model.Status)
                {
                    case 0:
                        ltlStatus.Text="无效";
                        break;
                    case 1:
                        ltlStatus.Text="有效";
                        break;
                    default:
                        ltlStatus.Text="";
                        break;
                }
                User user = new User(model.ReviewedUserID ?? 0);
                if (!String.IsNullOrWhiteSpace(user.UserType))
                {
                    ltlRevUserName.Text = user.UserName;
                }
                ltlRevDescription.Text = model.ReviewedDescription;
                ltlRevDate.Text = model.ReviewedDate.ToString();
                if (model.ReviewedStatus != 1)
                {
                    ltlRevStatus.Text = "未处理";
                }
                else
                {
                    ltlRevStatus.Text = "已处理";
                }
                ltlRemark.Text = model.Remark;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>history.go(-2)</script>");
        }
    }
}