/**
* SupplierAreaRegistration.cs
*
* 功 能： Supplier模块-区域路由注册器
* 类 名： SupplierAreaRegistration
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/08/14 20:51:15  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.Supplier
{
    public class SupplierAreaRegistration : AreaRegistrationBase
    {
        public SupplierAreaRegistration() : base(AreaRoute.Supplier) { }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region 注册Supplier区域扩展路由
            #region 商家个人中心商品列表页路由
            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_InStock",
                url: CurrentRoutePath + "Product/InStock/{SaleStatus}",
                defaults:
                    new
                    {
                        controller = "Product",
                        action = "InStock",
                        SaleStatus = -1
                    }
                  , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            #endregion
            #endregion
            base.RegisterArea(context);
        }
    }
}
