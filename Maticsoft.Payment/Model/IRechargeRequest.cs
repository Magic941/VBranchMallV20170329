using System;
namespace Maticsoft.Payment.Model
{
    /// <summary>
    /// 充值接口
    /// </summary>
    public interface IRechargeRequest
    {
        /// <summary>
        /// 充值流水号
        /// </summary>
        long RechargeId { get; set; }
        /// <summary>
        /// 充值时间
        /// </summary>
        DateTime TradeDate { get; set; }
        /// <summary>
        /// 支付类型ID
        /// </summary>
        int PaymentTypeId { get; set; }
        /// <summary>
        /// 支付网关
        /// </summary>
        string PaymentGateway { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        decimal RechargeBlance { get; set; }
        /// <summary>
        /// 充值用户
        /// </summary>
        int UserId { get; set; }
    }
}
