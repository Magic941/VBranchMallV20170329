using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.Payment
{
    /// <summary>
    /// 支付相应操作接口实现
    /// 创建用户：shiyuankao
    /// 创建时间：2014-08-07 
    /// </summary>
    public abstract class PayChannel : IPayChannel
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
        public abstract PayMessage Payment(string orderId, string productName, decimal amount);

        /// <summary>
        /// 退款
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07 
        /// </summary>
        /// <param name="inParams">入参</param>
        /// <returns></returns>
        public virtual bool Refund(Dictionary<string, string> inParams)
        {
            return false;
        }

        /// <summary>
        /// 订单查询
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07 
        /// </summary>
        /// <param name="inParams">入参</param>
        /// <returns></returns>
        public virtual PayMessage QuerySingleOrder(Dictionary<string, string> inParams)
        {
            return null;
        }

        /// <summary>
        /// 支付回调验证
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07 
        /// </summary>
        /// <param name="inParams">入参</param>
        /// <returns></returns>
        public virtual bool CallbackVerify(Dictionary<string, string> inParams)
        {
            return false;
        }
    }
}
