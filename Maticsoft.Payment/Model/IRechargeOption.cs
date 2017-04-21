/**
* IRechargeOption.cs
*
* 功 能： 充值模块配置接口
* 类 名： IRechargeOption
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/06/13 19:41:26  Ben    初版
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
    /// 充值模块配置接口
    /// </summary>
    public interface IRechargeOption :
        IRechargeOption<RechargeRequestInfo, UserInfo>
    {

    }

    /// <summary>
    /// 充值模块配置接口
    /// </summary>
    public interface IRechargeOption<T, U>
        where T : class,IRechargeRequest, new()
        where U : class ,IUserInfo, new()
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
        /// 获取当前登录(充值)用户
        /// </summary>
        /// <returns></returns>
        U GetCurrentUser(System.Web.HttpContext context);

        /// <summary>
        /// 获取充值信息
        /// </summary>
        /// <returns></returns>
        T GetRechargeRequest(long rechargeId);

        /// <summary>
        /// 更新充值信息-完成付款
        /// </summary>
        /// <param name="rechargeRequest">充值对象</param>
        /// <returns>是否成功</returns>
        bool PayForRechargeRequest(T rechargeRequest);
    }
}
