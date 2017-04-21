/**
* CMSAreaRegistration.cs
*
* 功 能： CMS模块-区域路由注册器
* 类 名： CMSAreaRegistration
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/27 12:00:33   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.CMS
{
    public class CMSAreaRegistration : AreaRegistrationBase
    {
        public CMSAreaRegistration() : base(AreaRoute.CMS) { }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              name: string.Format("{0}_Home", AreaName),
              url: CurrentRoutePath + "Index",
              defaults: new { controller = "Home", action = "Index" }
              , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
              );
            context.MapRoute(
                name: string.Format("{0}_Category", AreaName),
                url: CurrentRoutePath + "Category/{cid}",
                defaults: new { controller = "Category", action = "List", cid = UrlParameter.Optional }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            context.MapRoute(
                name: string.Format("{0}_Rss", AreaName),
                url: CurrentRoutePath + "Rss",
                defaults: new { controller = "Rss", action = "List" }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            context.MapRoute(
                 name: string.Format("{0}_About", AreaName),
                 url: CurrentRoutePath + "About",
                 defaults: new { controller = "About", action = "Index" }
                 , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                 );
            base.RegisterArea(context);
        }
    }
}
