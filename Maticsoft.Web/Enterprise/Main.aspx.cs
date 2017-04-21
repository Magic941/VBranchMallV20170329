using System;


namespace Maticsoft.Web.Enterprise
{
    public partial class Main : PageBaseEnterprise
    {
        public string CurrentUserName = string.Empty;

        BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
        Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentUser != null)
                {
                    CurrentUserName = CurrentUser.UserName;
                    int userId = CurrentUser.UserID;
                    int departmentID = Convert.ToInt32(CurrentUser.DepartmentID);

                    uModel = uBll.GetUsersExpModel(CurrentUser.UserID);
                    if (uModel != null)
                    {
                        this.LitLastLoginTime.Text = uModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        this.LitLastLoginTime.Text = CurrentUser.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    Model.Ms.Enterprise emodel = new Model.Ms.Enterprise();
                    BLL.Ms.Enterprise ebll = new BLL.Ms.Enterprise();
                    emodel = ebll.GetModel(int.Parse(currentUser.DepartmentID));
                    if (emodel != null)
                    {
                        if (emodel.Status == 1)
                        {
                            litMsg.Text = "";
                        }
                        else
                        {
                            litMsg.Text = "您的企业正在审核中, 审核通过后才能进行相关操作！";
                        }
                    }
                    else
                    {
                        litMsg.Text = "您的企业未通过审核, 审核通过后才能进行相关操作！";
                    }
                }
            }
        }

    }
}