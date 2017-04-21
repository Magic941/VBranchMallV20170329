/**
* SendRechargeHandler.cs
*
* 功 能： 发送充值请求
* 类 名： SendRechargeHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/13 21:55:55  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

namespace Maticsoft.Web.Handlers.Shop.Pay
{
    /// <summary>
    /// 发送充值请求
    /// </summary>
    public class SendRechargeHandler : Payment.Handler.SendRechargeHandlerBase
    {
        public SendRechargeHandler()
            : base(new RechargeOption())
        {
            #region 设置网站名称
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(
                Model.SysManage.ApplicationKeyType.System);
            base.HostName = webSiteSet.WebName;
            #endregion
        }
    }
}