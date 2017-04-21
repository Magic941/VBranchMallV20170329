/**
* PaymentOption.cs
*
* 功 能： 支付模块配置
* 类 名： PaymentOption
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/24 1:17:23  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using Maticsoft.Model.Shop.Order;
using Maticsoft.BLL.Shop.Order;
namespace Maticsoft.Web.Handlers.Shop.Pay
{
    /// <summary>
    /// 支付模块配置
    /// </summary>
    public class PaymentOption : Payment.Model.IPaymentOption<OrderInfo>
    {
        #region 成员
        private readonly BLL.Shop.Order.Orders _orderManage = new BLL.Shop.Order.Orders();
        private const string _returnUrl = "/pay/payment/{0}/return_url.aspx";
        private const string _notifyUrl = "/pay/payment/{0}/notify_url.aspx"; 
        #endregion

        #region IPaymentOption 成员
        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string NotifyUrl
        {
            get { return _notifyUrl; }
        }
        /// <summary>
        /// 支付返回地址
        /// </summary>
        public string ReturnUrl
        {
            get { return _returnUrl; }
        }

        #region 获取订单信息
        /// <summary>
        /// 在数据库中获取订单信息
        /// </summary>
        /// <param name="orderIdStr">订单ID</param>
        public OrderInfo GetOrderInfo(string orderIdStr)
        {
            long orderId = Common.Globals.SafeLong(orderIdStr, -1);
            if (orderId < 1) return null;

            //返回订单信息
            return _orderManage.GetModel(orderId);
        }
        #endregion

        /// <summary>
        /// 已验证支付网站签名 继续完成支付
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        public bool PayForOrder(OrderInfo orderInfo,string notifyid)
        {
            //更新订单为已支付 返回结果
            return OrderManage.PayForOrder(orderInfo, notifyid);
        }
        #endregion
    }
}