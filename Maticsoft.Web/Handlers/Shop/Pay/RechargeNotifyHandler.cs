/**
* RechargeNotifyHandler.cs
*
* 功 能： 充值异步通知处理
* 类 名： RechargeNotifyHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/13 21:56:54  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

namespace Maticsoft.Web.Handlers.Shop.Pay
{
    public class RechargeNotifyHandler : Payment.Handler.RechargeReturnHandlerBase
    {
        /// <summary>
        /// 构造回调 设置为异步模式
        /// </summary>
        public RechargeNotifyHandler() : base(new RechargeOption(), true) { }
    }
}