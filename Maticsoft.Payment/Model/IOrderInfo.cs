using System;

namespace Maticsoft.Payment.Model
{
    /// <summary>
    /// 订单信息接口
    /// </summary>
    public interface IOrderInfo
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        OrderStatus OrderStatus { get; }
        /// <summary>
        /// 退款状态
        /// </summary>
        RefundStatus RefundStatus { get; }
        /// <summary>
        /// 支付状态
        /// </summary>
        PaymentStatus PaymentStatus { get; }
        /// <summary>
        /// 物流状态
        /// </summary>
        ShippingStatus ShippingStatus { get; }

        /// <summary>
        /// 订单最终支付金额
        /// </summary>
        decimal Amount { get; set; }
        /// <summary>
        /// 购买者Email
        /// </summary>
        string BuyerEmail { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        DateTime OrderDate { get; set; }
        /// <summary>
        /// 支付网关
        /// </summary>
        int PaymentTypeId { get; set; }
        /// <summary>
        /// 支付网关订单ID
        /// </summary>
        string GatewayOrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        string OrderCode { get; }
    }
}