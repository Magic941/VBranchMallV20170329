using System;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.SNS.ViewConfig
{
    public partial class ViewConfig : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 118; } } //运营管理_是否显示界面设置页面

        protected new int Act_UpdateData = 119;    //运营管理_界面设置_编辑界面配置信息

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //是否有编辑信息的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnSave.Visible = false;
                }
                BoundData();
            }
        }

        private BLL.SNS.TaoBaoConfig TaoBaoConfig = new BLL.SNS.TaoBaoConfig(ApplicationKeyType.OpenAPI);

        private void BoundData()
        {
            string StrFallDataSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallDataSize",ApplicationKeyType.SNS);
            this.ddlFallDataSize.SelectedValue = StrFallDataSize == null ? "" : StrFallDataSize;

            string StrPostDataSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS);
            this.ddlPostDataSize.SelectedValue = StrFallDataSize == null ? "" : StrPostDataSize;

            string StrCommentDataSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS);
            this.ddlCommentDataSize.SelectedValue = StrCommentDataSize == null ? "" : StrCommentDataSize;

            string StrFallInitDataSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS);
            this.ddlFallInitDataSize.SelectedValue = StrFallInitDataSize == null ? "" : StrFallInitDataSize;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_FallInitDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_FallInitDataSize", ddlFallInitDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_FallInitDataSize", ddlFallInitDataSize.SelectedValue, "瀑布流数次初次加载的数量", ApplicationKeyType.SNS);
                }

                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_FallDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_FallDataSize", ddlFallDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_FallDataSize", ddlFallDataSize.SelectedValue, "瀑布流每页的数量", ApplicationKeyType.SNS);
                }

                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_PostDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_PostDataSize", ddlPostDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_PostDataSize", ddlPostDataSize.SelectedValue, "动态每页显示的数量", ApplicationKeyType.SNS);
                }

                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_CommentDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_CommentDataSize", ddlCommentDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_CommentDataSize", ddlCommentDataSize.SelectedValue, "评论显示的条数", ApplicationKeyType.SNS);
                }

                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.SNS);//清除网站设置的缓存文件
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "设置全局配置成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "ViewConfig.aspx");
            }
            catch (Exception)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "设置全局配置出现异常", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "ViewConfig.aspx");
            }
        }

        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    BoundData();
        //}
    }
}