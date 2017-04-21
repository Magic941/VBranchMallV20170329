using System;
using System.Text;
using System.Web;

namespace Maticsoft.Payment
{
    [Obsolete]
    public sealed class ShoppingProcessor
    {
        /// <summary>
        /// 订单ID分割符
        /// </summary>
        public const char OrderIdsSplitChar = ';';

        #region 生成订单ID
        private static readonly string LockKey = "LOCK";
        /// <summary>
        /// 生成多份订单ID
        /// </summary>
        /// <returns></returns>
        public static string[] GenerateOrderId(int maxNum)
        {
            //保护 生成一份订单ID
            if (maxNum < 2) return new string[] { GenerateOrderId() };

            string[] tmpOrderIds = new string[maxNum];
            for (int i = 0; i < tmpOrderIds.Length; )
            {
                tmpOrderIds[i] = GenerateOrderId() + "-" + ++i;
            }
            return tmpOrderIds;
        }
        /// <summary>
        /// 生成一份订单ID
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderId()
        {
            //并发线程保护
            lock (LockKey)
            {
                StringBuilder tmpOrderId = new StringBuilder(DateTime.Now.ToString("yyyyMMdd"));
                Random random = new Random();
                for (int i = 0; i < 7; i++)
                {
                    tmpOrderId.Append((char)(0x30 + ((ushort)(random.Next() % 10))));
                }
                return tmpOrderId.ToString();
            }
        }
        #endregion

        #region 从URL获取全部订单ID
        /// <summary>
        /// 从URL获取全部订单ID
        /// </summary>
        /// <param name="page">Page对象</param>
        public static string[] GetQueryStringForOrderIds(HttpRequest request)
        {
            string tmpStr = string.Empty;
            return ShoppingProcessor.GetQueryStringForOrderIds(request, out tmpStr);
        }
        /// <summary>
        /// 从URL获取全部订单ID
        /// </summary>
        /// <param name="page">Page对象</param>
        /// <param name="orderIdStr">订单ID字符串</param>
        public static string[] GetQueryStringForOrderIds(HttpRequest request, out string orderIdStr)
        {
            //获取全部订单ID
            orderIdStr = request.QueryString["OrderIds"];

            //订单ID N/A返回首页
            if (string.IsNullOrEmpty(orderIdStr))
            {
                HttpContext.Current.Response.Redirect("~/");
                return null;
            }

            //拆分订单ID
            return orderIdStr.Split(new char[] { ShoppingProcessor.OrderIdsSplitChar }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

    }
}

