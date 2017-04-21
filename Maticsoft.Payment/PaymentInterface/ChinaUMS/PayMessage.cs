using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.Payment
{
    /// <summary>
    /// 支付接口响应消息
    /// 创建用户：shiyuankao
    /// 创建时间：2014-08-06
    /// </summary>
    public class PayMessage
    {
        /// <summary>
        /// 成功标志
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 消息信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 支付交互数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 其它数据（如：支付信息保存至数据库数据）
        /// </summary>
        public object OtherData { get; set; }
    }
}
