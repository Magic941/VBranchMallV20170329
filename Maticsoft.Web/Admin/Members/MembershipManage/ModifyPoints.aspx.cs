using System;
using System.Web.UI;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Members.MembershipManage
{
    public partial class ModifyPoints : PageBaseAdmin
    {
        public string strid = "";

        private Maticsoft.BLL.Members.SiteMessage bll = new BLL.Members.SiteMessage();
        private Maticsoft.Accounts.Bus.UserType UserType = new Maticsoft.Accounts.Bus.UserType();
        protected override int Act_PageLoad { get { return 693; } } //用户管理_会员信息管理_修改积分

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int ID = (Convert.ToInt32(strid));
                    ShowInfo(ID);
                }
            }
        }

        private void ShowInfo(int ID)
        {
            lblID.Text = ID.ToString();
            AccountsPrincipal user = new AccountsPrincipal(ID);
            User manage = new Maticsoft.Accounts.Bus.User(user);

            BLL.Members.UsersExp expBll = new BLL.Members.UsersExp();
            Model.Members.UsersExpModel model = expBll.GetUsersExpModel(ID);
            if (manage != null && model != null)
            {
                this.lblUserName.Text = manage.UserName;
                this.lblTrueName.Text = manage.TrueName;
                this.lblPhone.Text = manage.Phone;
                this.lblNickName.Text = manage.NickName;
                this.lblEmail.Text = manage.Email;
            }
            if (model != null)
            {
                this.lblPoints.Text = model.Points.ToString();
            }
            txtPoints.Text = string.Empty;
        }

        protected void btnPoints_Click(object sender, EventArgs e)
        {
            int userId = Common.Globals.SafeInt(lblID.Text, -1);
            if (userId < 1)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "账户信息非法, 请联系系统管理员!");
                return;
            }

            int points = Common.Globals.SafeInt(txtPoints.Text, 0);
            if (points == 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "充值积分不正确, 请重新输入!");
                return;
            }
            BLL.Members.PointsDetail pointsManage = new BLL.Members.PointsDetail();
            if (pointsManage.AddPointsByOffline(userId, points,
                points > 0 ? "线下充值" : "线下消费"))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTipScript(this, Resources.Site.TooltipOperateOK, "$(parent.document).find('[id$=btnSearch]').click();");
                ShowInfo(userId);
                return;
            }
            Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipOperateError);
        }
    }
}