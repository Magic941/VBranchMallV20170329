/**
* OrderProcessor.cs
*
* 功 能： 订单辅助处理类
* 类 名： OrderProcessor
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System;
using System.Text;
using System.Web;
using Maticsoft.Payment.Model;
using Maticsoft.Payment.Core;

namespace Maticsoft.Payment
{
    /// <summary>
    /// 订单辅助处理类
    /// </summary>
    public static class OrderProcessor
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
            //生成一份订单ID
            if (maxNum < 2) return new string[] { GenerateOrderId() };

            //生成多份订单ID
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
        /// <remarks>线程安全</remarks>
        /// <returns></returns>
        public static string GenerateOrderId()
        {
            //线程安全 -> 并发线程保护
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
        /// <param name="request">HttpRequest对象</param>
        public static string[] GetQueryString4OrderIds(HttpRequest request)
        {
            string tmpStr;
            return GetQueryString4OrderIds(request, out tmpStr);
        }

        /// <summary>
        /// 从URL获取全部订单ID，全部在这里加前订单前辍，以区别不同的平台
        /// </summary>
        /// <param name="request">HttpRequest对象</param>
        /// <param name="orderIdStr">订单ID字符串</param>
        public static string[] GetQueryString4OrderIds(HttpRequest request, out string orderIdStr)
        {
            //获取全部订单ID,以 ;分隔的系统订单，可以多订单支付
            orderIdStr = request.QueryString["OrderIds"];

            //订单ID N/A返回首页
            if (String.IsNullOrEmpty(orderIdStr))
            {
                return null;
            }

            //拆分订单ID
            var orderAry = orderIdStr.Split(new char[] { OrderIdsSplitChar }, StringSplitOptions.RemoveEmptyEntries);
            //wusg 增加订单前辍 20140802，以区别不同的平台
            StringBuilder newOrderIdStr = new StringBuilder();
            if (orderAry.Length > 0)
            {
                foreach (var order in orderAry)
                {
                    newOrderIdStr.Append(Globals.OrderPrefix).Append("_").Append(order).Append(OrderIdsSplitChar);
                }
            }
            orderIdStr = newOrderIdStr.ToString();
            return orderAry;
        }

        #endregion

        #region 检查订单状态是否允许当前操作
        /// <summary>
        /// 检查订单状态是否允许当前操作
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="action">操作</param>
        /// <returns>是否允许</returns>
        public static bool CheckAction<T>(T order, OrderActions action) where T : IOrderInfo
        {
            if (order.OrderStatus == OrderStatus.InProgress)
            {
                switch (action)
                {
                    case OrderActions.BUYER_PAY:
                    case OrderActions.BUYER_MODIFY_DELIVER_ADDRESS:
                    case OrderActions.BUYER_MODIFY_PAYMENT_MODE:
                    case OrderActions.BUYER_MODIFY_SHIPPING_MODE:
                    case OrderActions.BUYER_CANCEL:
                    case OrderActions.SELLER_CONFIRM_PAY:
                    case OrderActions.SELLER_CLOSE_TRADE:
                    case OrderActions.SELLER_MODIFY_TRADE:
                        return (order.PaymentStatus == PaymentStatus.NotYet);

                    case OrderActions.BUYER_REFUND:
                        if (order.PaymentStatus != PaymentStatus.Prepaid)
                        {
                            return false;
                        }
                        return (order.RefundStatus == RefundStatus.None);

                    case OrderActions.BUYER_CANCEL_REFUND:
                    case OrderActions.SELLER_REJECT_REFUND:
                    case OrderActions.SELLER_ACCEPT_REFUND:
                        if (order.PaymentStatus != PaymentStatus.Prepaid)
                        {
                            return false;
                        }
                        return (order.RefundStatus == RefundStatus.Requested);

                    case OrderActions.BUYER_CONFIRM_GOODS:
                    case OrderActions.SELLER_FINISH_TRADE:
                        if (order.ShippingStatus != ShippingStatus.Delivered)
                        {
                            return false;
                        }
                        return (order.RefundStatus == RefundStatus.None);

                    case OrderActions.SELLER_SEND_GOODS:
                        if (order.RefundStatus != RefundStatus.None)
                        {
                            return false;
                        }
                        return ((order.ShippingStatus == ShippingStatus.NotYet) || (order.ShippingStatus == ShippingStatus.Packing));

                    case OrderActions.SELLER_PACK_GOODS:
                        if (order.RefundStatus != RefundStatus.None)
                        {
                            return false;
                        }
                        return (order.ShippingStatus == ShippingStatus.NotYet);
                }
            }
            return false;
        }
        #endregion
    }
}