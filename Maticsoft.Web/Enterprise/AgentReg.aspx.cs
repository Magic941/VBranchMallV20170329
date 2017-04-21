using System;
using System.Web.Security;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Enterprise
{
    public partial class AgentReg : System.Web.UI.Page
    {
        public int newID;
        Model.Ms.Enterprise emodel = new Model.Ms.Enterprise();
        BLL.Ms.Enterprise ebll = new BLL.Ms.Enterprise();
        User newUser = new User();
        BLL.Members.UsersExp userExp = new BLL.Members.UsersExp();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgentReg.aspx");
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (newUser.HasUserByUserName(txtUserName.Text))
            {
                Maticsoft.Common.MessageBox.Show(this, "用户名已经存在");
                return;
            }
            /// 其他验证用服务端控件已完成
            if (txtPassword.Text.Length < 6)
            {
                Maticsoft.Common.MessageBox.Show(this, "密码长度不能低于6位");
                return;
            }
            if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
            {

                if (Session["CheckCode"].ToString().ToLower() != this.txtCode.Text.ToLower())
                {

                    Maticsoft.Common.MessageBox.Show(this, "验证码错误！");
                    txtCode.Text = "";
                    Session["CheckCode"] = null;

                    return;
                }
                else
                {
                    Session["CheckCode"] = null;
                }
            }
            string UserName = this.txtUserName.Text;
            string Pwd = this.txtPassword1.Text;
            newUser.UserName = UserName;
            newUser.TrueName = UserName;
            newUser.Password = Maticsoft.Accounts.Bus.AccountsPrincipal.EncryptPassword(Pwd);
            newUser.Activity = true;
            newUser.UserType = "EE";
            newUser.User_dateCreate = DateTime.Now;
            newID = newUser.Create();
            if (newID > 0)
            {
                //userExp.Add(newID);
                int DefaultEnteRoleID = BLL.SysManage.ConfigSystem.GetIntValueByCache("DefaultEnteRoleID");
                newUser.UserID = newID;
                newUser.AddToRole(DefaultEnteRoleID);

                FormsAuthentication.SetAuthCookie(newUser.UserName, false);
                Session[Globals.SESSIONKEY_ENTERPRISE] = newUser;
                Response.Redirect("index.html");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowAndRedirect(this, "系统忙请稍后再试！！", "AgentReg.aspx");
            }
        }
    }
}