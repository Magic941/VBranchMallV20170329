using System;

namespace Maticsoft.Payment.Model
{
    /// <summary>
    /// 充值信息
    /// </summary>
    public class RechargeRequestInfo : IRechargeRequest
    {
        /// <summary>
        /// 支付类型ID
        /// </summary>
        public int PaymentTypeId { get; set; }
        /// <summary>
        /// 支付网关
        /// </summary>
        public string PaymentGateway { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal RechargeBlance { get; set; }
        /// <summary>
        /// 充值流水ID
        /// </summary>
        public long RechargeId { get; set; }
        /// <summary>
        /// 充值时间
        /// </summary>
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// 充值用户ID
        /// </summary>
        public int UserId { get; set; }
    }
}

