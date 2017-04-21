/**
* COMAreaRegistration.cs
*
* 功 能： 共用模块-区域路由注册器
* 类 名： COMAreaRegistration
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年11月26日 16:30:37   Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.COM
{
    public class COMAreaRegistration : AreaRegistrationBase
    {
        public COMAreaRegistration() : base(AreaRoute.COM) { }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            base.RegisterArea(context);
        }
    }
}
