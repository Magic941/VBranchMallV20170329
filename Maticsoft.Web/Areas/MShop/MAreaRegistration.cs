/**
* MAreaRegistration.cs
*
* 功 能： M模块-区域路由注册器
* 类 名： MAreaRegistration
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

namespace Maticsoft.Web.Areas.MShop
{
    public class MShopRegistration : MShopAreaRegistration
    {
        public MShopRegistration()
        {
            base.AreaRegistration(AreaRoute.MShop, "m");
            IsRegisterArea = (MvcApplication.MainAreaRoute != CurrentArea);
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            if (!IsRegisterArea) return;
            base.RegisterArea(context);
        }
    }
}
