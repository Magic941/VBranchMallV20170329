/**
* main_index.cs
*
* 功 能： 管理员后台
* 类 名： main_index
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/24 15:21:18  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;

namespace Maticsoft.Web.Admin
{
    public partial class main_index : PageBaseAdmin
    {
        public string CurrentUserName = string.Empty;
        public string GetDateTime = string.Empty;

        private BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
        private Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(CurrentUser.TrueName))
                {
                    CurrentUserName = CurrentUser.TrueName;
                }
                else
                {
                    CurrentUserName = CurrentUser.UserName;
                }
                if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 12)
                {
                    GetDateTime = "早上好";
                }
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                {
                    GetDateTime = "下午好";
                }
                else
                {
                    GetDateTime = "晚上好";
                }
                uModel = uBll.GetUsersExpModel(CurrentUser.UserID);
                if (uModel != null)
                {
                    this.LitLastLoginTime.Text = uModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    this.LitLastLoginTime.Text = CurrentUser.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
                }

                #region 系统信息
                litProductLine.Text = MvcApplication.ProductInfo + " " + MvcApplication.Version;
                litOperatingSystem.Text = Maticsoft.Common.SystemInfo.OperatingSystemSimple;
                litServerDomain.Text = Maticsoft.Common.SystemInfo.ServerDomain;
                litDotNetVersion.Text = Maticsoft.Common.SystemInfo.DotNetVersion.ToString();
                litWebServerVersion.Text = Maticsoft.Common.SystemInfo.WebServerVersion;
                #endregion
            }
        }
    }
}