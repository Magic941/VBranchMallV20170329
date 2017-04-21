using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class UpdateCount : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 587; } } //SNS_会员管理_更新统计数量页
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        Maticsoft.BLL.Members.UsersExp UserExBll = new BLL.Members.UsersExp();
        Maticsoft.BLL.Members.Users user = new BLL.Members.Users();
        Maticsoft.BLL.SNS.StarRank StarRankBll = new Maticsoft.BLL.SNS.StarRank();
        protected void btnFans_Click(object sender, System.EventArgs e)
        {
            if (user.UpdateFansAndFellowCount())
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户粉丝数成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "更新用户粉丝数成功");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户粉丝数失败", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户粉丝数失败");
            }
        }

        protected void btnShare_Click(object sender, System.EventArgs e)
        {
            if (UserExBll.UpdateShareCount())
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户分享数成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "更新用户分享数成功");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户分享数失败", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户分享数失败");
            }
        }

        protected void btnProduct_Click(object sender, System.EventArgs e)
        {
            if (UserExBll.UpdateProductCount())
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户商品数成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "更新用户商品数成功");
            }
            else
            {
             LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户商品数失败", this);

                Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户商品数失败");
            }
        }

        protected void btnFavourites_Click(object sender, System.EventArgs e)
        {
            if (UserExBll.UpdateFavouritesCount())
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户喜欢数成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "更新用户喜欢数成功");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户喜欢数失败", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户喜欢数失败");
            }
        }

        protected void btnAblums_Click(object sender, System.EventArgs e)
        {
            if (UserExBll.UpdateAblumsCount())
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户专辑数成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "更新用户专辑数成功");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户专辑数失败", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户专辑数失败");
            }
        }

        //明星达人排行
        protected void btnHotStar_Click(object sender, EventArgs e)
        {
            if (!StarRankBll.AddHotStarRank())
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "重新获取明星达人失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "重新获取明星达人失败", this);
            }
        }
        //晒货达人排行
        protected void btnShareProduct_Click(object sender, EventArgs e)
        {
            if (!StarRankBll.AddShareProductRank())
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "重新获取晒货达人失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "重新获取晒货达人失败", this);
            }
        }
        //搭配达人排行
        protected void btnCollocation_Click(object sender, EventArgs e)
        {
            if (!StarRankBll.AddCollocationRank())
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "重新获取搭配达人失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "重新获取搭配达人失败", this);
            }
        }
        protected void btnAll_Click(object sender, System.EventArgs e)
        {
            if (this.FansCkeck.Checked)
            {
                if (!user.UpdateFansAndFellowCount())
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户粉丝数失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户粉丝数失败", this);

                }
            }
            if (this.ShareCheck.Checked)
            {
                if (!UserExBll.UpdateShareCount())
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户分享数失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户粉丝数失败", this);


                }
            }
            if (this.ProductCheck.Checked)
            {
                if (!UserExBll.UpdateProductCount())
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户商品数失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户商品数失败", this);

                }
            }
            if (this.FavouritesCheck.Checked)
            {
                if (!UserExBll.UpdateFavouritesCount())
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户喜欢数失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户喜欢数失败", this);
                }
            }
            if (this.AblumsCheck.Checked)
            {
                if (!UserExBll.UpdateAblumsCount())
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "更新用户专辑数失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新用户专辑数失败", this);
                }
            }
            if (this.HotStarCheck.Checked)
            {
                if (!StarRankBll.AddHotStarRank())
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "重新获取明星达人失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "重新获取明星达人失败", this);
                }
            }
            if (this.ShareProductCheck.Checked)
            {
                if (!StarRankBll.AddShareProductRank())
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "重新获取晒货达人失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "重新获取晒货达人失败", this);
                }
            }
            if (this.CollocationCheck.Checked)
            {
                if (!StarRankBll.AddCollocationRank())
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "重新获取搭配达人失败");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "重新获取搭配达人失败", this);
                }
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "全部更新成功");
        }
    }
}