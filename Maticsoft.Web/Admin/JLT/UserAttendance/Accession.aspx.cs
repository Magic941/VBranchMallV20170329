using System;
using System.Data;
using System.Web.UI;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.Model.Members;

namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class Accession : PageBaseAdmin
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
                    BindEn();
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
       

        public void BindEn()
        {
            Maticsoft.BLL.Ms.Enterprise Enbll = new Maticsoft.BLL.Ms.Enterprise();
            DataSet ds = Enbll.GetAllList();
            ddlCompanyName.DataSource = ds;
            ddlCompanyName.DataTextField = "Name";
            ddlCompanyName.DataValueField = "EnterpriseID";
            ddlCompanyName.DataBind();
            model = bll.GetModel(AttID);
            if (model != null && model.UserType == "EE")
            {
                ddlCompanyName.SelectedValue = model.DepartmentID;
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
            if (Common.Globals.SafeInt(model.DepartmentID,-1)>0)
            {
                Common.MessageBox.ShowSuccessTip(this, "此员工还没有办理离职手续，请办完离职手续之后在进行入职");
                return;
            }
            int DepartmentID = Common.Globals.SafeInt(ddlCompanyName.SelectedValue, 0);
            if (DepartmentID == 0)
            {
                 Common.MessageBox.ShowSuccessTip(this, "请选择入职的企业");
                return;
            }
            model.DepartmentID = DepartmentID.ToString();
            bll.Update(model);
            Common.MessageBox.ShowSuccessTip(this, "入职成功", "/admin/Members/MembershipManage/list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Members/MembershipManage/list.aspx");
        }
    }
}
