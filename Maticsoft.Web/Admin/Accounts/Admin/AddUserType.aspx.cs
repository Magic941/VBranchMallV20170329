using System;
using Maticsoft.Accounts.Bus;
using System.Data;
namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class AddUserType : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override int Act_PageLoad { get { return 200; } }//系统管理_用户类别管理_添加页面
        public void btnSave_Click(object sender, System.EventArgs e)
        {
            UserType newUserType = new UserType();
            string userType = txtUserType.Text.Trim();
             byte[] sarr = System.Text.Encoding.GetEncoding("gb2312").GetBytes(userType);
            if (sarr.Length > 2)
            {
               Maticsoft.Common.MessageBox.ShowFailTip(this,"输入的用户类型不能超过两个字符");
                return;
            }
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(userType) || string.IsNullOrWhiteSpace(description)) return;

            string strErr = "";
            DataSet dataSet = newUserType.GetList("UserType='" + userType + "'");

            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                strErr += Resources.Site.TooltipUserTypeExist;
            }
            if (strErr != "")
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, strErr);
                return;
            }
            newUserType.Add(userType, description);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加用户类别用户类别：【{0}】", userType), this);
            Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='UserTypeAdmin.aspx'");
            //Response.Redirect("UserTypeAdmin.aspx");
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserTypeAdmin.aspx");
        }
    }
}
