using System;
using System.Web.UI;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Members.MembershipManage
{
    public partial class Show : PageBaseAdmin
    {
        public string strid = "";

        private Maticsoft.BLL.Members.SiteMessage bll = new BLL.Members.SiteMessage();
        private Maticsoft.Accounts.Bus.UserType UserType = new Maticsoft.Accounts.Bus.UserType();
        protected override int Act_PageLoad { get { return 287; } } //用户管理_会员信息管理_详细页
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

                this.lblSex.Text = (!string.IsNullOrWhiteSpace(manage.Sex) && manage.Sex.Trim() == "0") ? "女" : "男";
                this.lblActivity.Text = manage.Activity ? "正常使用" : "已经冻结";
                this.lblCreTime.Text = manage.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (model != null)
            {
                Maticsoft.BLL.Ms.Regions RegionBll = new BLL.Ms.Regions();
                this.imageGra.ImageUrl = string.Format("/Upload/User/Gravatar/{0}.jpg", model.UserID);
                string strAddress = RegionBll.GetRegionNameByRID(Common.Globals.SafeInt(model.Address, 0));
                if (strAddress.Contains("北京北京"))
                {
                    strAddress = strAddress.Replace("北京北京", "北京");
                }
                else if (strAddress.Contains("上海上海"))
                {
                    strAddress = strAddress.Replace("上海上海", "上海");
                }
                else if (strAddress.Contains("重庆重庆"))
                {
                    strAddress = strAddress.Replace("重庆重庆", "重庆");
                }
                else if (strAddress.Contains("天津天津"))
                {
                    strAddress = strAddress.Replace("天津天津", "天津");
                }
                this.lblAddress.Text = string.IsNullOrEmpty(model.Address) ? "暂未设置" : strAddress;
                this.lblPoints.Text = model.Points.ToString();
                this.lblBalance.Text = model.Balance.HasValue ? model.Balance.Value.ToString("F") : "0.00";
                this.lblLoginDate.Text = model.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}