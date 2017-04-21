/**
* IPaymentOption.cs
*
* 功 能： 支付模块配置接口
* 类 名： IPaymentOption
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/24 0:28:53  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

namespace Maticsoft.Payment.Model
{
    /// <summary>
    /// 支付模块配置接口
    /// </summary>
    public interface IPaymentOption<T> where T : class, IOrderInfo
    {
        /// <summary>
        /// 网关浏览器返回地址
        /// </summary>
        string ReturnUrl { get; }
        /// <summary>
        /// 网关异步通知回调地址
        /// </summary>
        string NotifyUrl { get; }

        /// <summary>
        /// 根据订单ID 获取订单信息
        /// </summary>
        /// <param name="orderId">网关回传订单ID</param>
        /// <returns>订单对象</returns>
        T GetOrderInfo(string orderId);

        /// <summary>
        /// 更新订单-完成付款
        /// </summary>
        /// <param name="order">订单</param>
        /// <returns>是否成功</returns>
        bool PayForOrder(T order,string notifyid);
    }
}
