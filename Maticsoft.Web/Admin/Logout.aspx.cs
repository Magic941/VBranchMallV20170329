using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin
{
    public partial class Logout : System.Web.UI.Page
    {
        string defaullogin = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultLoginAdmin");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Maticsoft.Common.Globals.SESSIONKEY_ADMIN] != null)
            {
                Maticsoft.Accounts.Bus.User currentUser = (Maticsoft.Accounts.Bus.User)Session[Maticsoft.Common.Globals.SESSIONKEY_ADMIN];
                LogHelp.AddUserLog(currentUser.UserName, currentUser.UserType, "退出系统", this);

                #region 更新最新的登录时间
                Maticsoft.BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
                Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();
                uModel = uBll.GetUsersExpModel(currentUser.UserID);
                if (uModel != null)
                {
                    uModel.LastAccessIP = Request.UserHostAddress;
                    uModel.LastLoginTime = DateTime.Now;
                    uBll.UpdateUsersExp(uModel);
                }
                #endregion
            }
            FormsAuthentication.SignOut();
            Session.Remove(Globals.SESSIONKEY_ADMIN);
            Session.Clear();
            Session.Abandon();
            Response.Clear();
            Response.Redirect(defaullogin);
            Response.End();
        }
    }
}
