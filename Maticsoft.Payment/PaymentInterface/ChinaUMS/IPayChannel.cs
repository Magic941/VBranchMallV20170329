using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.Payment
{
    /// <summary>
    /// 支付相应操作接口
    /// 创建用户：shiyuankao
    /// 创建时间：2014-08-07
    /// </summary>
    public interface IPayChannel
    {
        /// <summary>
        /// 付款操作
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07 
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="productName">商品名称</param>
        /// <param name="amount">支付金额</param>
        /// <returns></returns>
        PayMessage Payment(string orderId, string productName, decimal amount);

        /// <summary>
        /// 退款
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07 
        /// </summary>
        /// <param name="inParams">入参</param>
        /// <returns></returns>
        bool Refund(Dictionary<string, string> inParams);

        /// <summary>
        /// 订单查询
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07 
        /// </summary>
        /// <param name="inParams">入参</param>
        /// <returns></returns>
        PayMessage QuerySingleOrder(Dictionary<string, string> inParams);

        /// <summary>
        /// 支付回调验证
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07 
        /// </summary>
        /// <param name="inParams">入参</param>
        /// <returns></returns>
        bool CallbackVerify(Dictionary<string, string> inParams);
    }
}
