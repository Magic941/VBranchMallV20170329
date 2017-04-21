/**
* MobileAreaRegistration.cs
*
* 功 能： Mobile模块-区域路由注册器
* 类 名： MobileAreaRegistration
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/07/01 19:37:01  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MPage
{
    public class MPageAreaRegistration : AreaRegistrationBase
    {
        protected string RouteName = AreaRoute.MPage.ToString();
        protected bool IsRegisterMArea;

        public MPageAreaRegistration() : base(AreaRoute.MPage) { }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region 注册MPage区域扩展路由

            #region 登陆路由

            context.MapRoute(
                name: AreaName + "_" + RouteName + "_Login",
                url: CurrentRoutePath + "a/l",
                defaults:
                    new
                    {
                        controller = "Account",
                        action = "Login"
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 个人中心首页路由

            context.MapRoute(
                name: AreaName + "_" + RouteName + "_UserCenter",
                url: CurrentRoutePath + "u/{action}/{id}",
                defaults:
                    new
                    {
                        controller = "UserCenter",
                        action = "Index",
                        id = UrlParameter.Optional
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 考勤路由

            context.MapRoute(
                name: AreaName + "_" + RouteName + "_Attendance",
                url: CurrentRoutePath + "w/a/{userId}",
                defaults:
                    new
                    {
                        controller = "Home",
                        action = "Attendance",
                        userId = 0
                    }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #endregion

            #region 注册Mobile区域子路由
            //如当前为主路由 - 区域注册不执行
            if (MvcApplication.MainAreaRoute != CurrentArea || IsRegisterMArea)
            {
                context.MapRoute(
                    name: CurrentRouteName + "Base",
                    url: CurrentRoutePath + "{controller}/{action}/{id}",
                    defaults:
                        new
                        {
                            controller = "Home",
                            action = "Index",
                            id = UrlParameter.Optional
                        }
                    ,
                    constraints: new
                    {
                        id = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    }
                    , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                    );

                context.MapRoute(
                    name: CurrentRouteName + RouteName,
                    url: CurrentRoutePath + "{controller}/{action}/{viewname}/{id}",
                    defaults:
                        new
                        {
                            controller = "Home",
                            action = "Index",
                            viewname = UrlParameter.Optional,
                            id = UrlParameter.Optional
                        }
                    ,
                    constraints: new
                    {
                        viewname = @"[\w]{0,50}", //大小写字母/数字/下划线
                        id = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    }
                    , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                    );
            }
            #endregion
        }
    }
}
