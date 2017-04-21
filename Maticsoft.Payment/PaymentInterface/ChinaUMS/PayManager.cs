using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GL.Payment.UnionPay;

namespace GL.Payment
{
    /// <summary>
    /// 支付渠道管理
    /// 创建用户：shiyuankao
    /// 创建时间：2014-08-06
    /// </summary>
    public class PayManager
    {
        /// <summary>
        /// 支付渠道集合
        /// </summary>
        private static Dictionary<string, IPayChannel> dictChannels = new Dictionary<string, IPayChannel>();

        /// <summary>
        /// 
        /// </summary>
        static PayManager()
        {
            Init();
        }

        /// <summary>
        /// 初始化各个支付渠道
        /// </summary>
        private static void Init()
        {
            dictChannels = new Dictionary<string, IPayChannel>
            {
                {"0001", new UnionPayChannel()}
           
            };
        }

        /// <summary>
        /// 根据支付渠道获取支付渠道实例
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-06
        /// </summary>
        /// <param name="channelNo"></param>
        /// <returns></returns>
        public static IPayChannel GetPayChannel(string channelNo)
        {
            var channel = dictChannels[channelNo];
            return channel;
        }
    }
}
