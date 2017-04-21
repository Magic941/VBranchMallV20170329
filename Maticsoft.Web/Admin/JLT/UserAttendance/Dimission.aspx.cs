using System;
using System.Data;
using System.Web.UI;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.Model.Members;

namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class Dimission : PageBaseAdmin
    {
        Maticsoft.BLL.Members.Users bll = new Maticsoft.BLL.Members.Users();
        Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
        protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
                if (AttID>0)
				{
                    ShowInfo(AttID);
				}
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

        private void ShowInfo(int id)
        {
            if (AttID > 0)
            {
                model = bll.GetModel(AttID);
                litID.Text = AttID.ToString();
                litUserName.Text = model.UserName;
                litPhone.Text = model.Phone;
                litSex.Text = model.Sex == "0" ? "女" : "男";
                if (Common.Globals.SafeInt(model.DepartmentID, -1) <= 0)
                {
                    Common.MessageBox.ShowSuccessTip(this, "此员工没有相关的单位信息，无法办理离职手续", "/admin/Members/MembershipManage/list.aspx");
                }
                Maticsoft.BLL.Ms.Enterprise enbll = new Maticsoft.BLL.Ms.Enterprise();
                var enmodel = enbll.GetModel(Common.Globals.SafeInt(model.DepartmentID, -1));
                litcommpany.Text = enmodel != null ? enmodel.Name : "";
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            
            if (AttID == 0)
            {
                Common.MessageBox.ShowSuccessTip(this,"出现异常");
                return;
            }
            model=  bll.GetModel(AttID);
            if (model == null)
            {
                Common.MessageBox.ShowSuccessTip(this, "出现异常");
                return;
            }
            if (Common.Globals.SafeInt(model.DepartmentID,-1)<=0)
            {
                Common.MessageBox.ShowSuccessTip(this, "此员工没有所单位，请检查之后在进行做离职处理");
                return;
            }
            model.DepartmentID ="-1";
            bll.Update(model);
            Common.MessageBox.ShowSuccessTip(this, "离职成功", "/admin/Members/MembershipManage/list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Members/MembershipManage/list.aspx");
        }
    }
}
