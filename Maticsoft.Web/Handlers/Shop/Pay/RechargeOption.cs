/**
* RechargeOption.cs
*
* 功 能： 充值模块配置
* 类 名： RechargeOption
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/06/13 21:17:23  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using Maticsoft.Payment.Model;
using Maticsoft.Payment.BLL;
using System.Web;
namespace Maticsoft.Web.Handlers.Shop.Pay
{
    /// <summary>
    /// 充值模块配置
    /// </summary>
    public class RechargeOption : Payment.Model.IRechargeOption
    {
        #region 成员
        private const string _returnUrl = "/pay/recharge/{0}/return_url.aspx";
        private const string _notifyUrl = "/pay/recharge/{0}/notify_url.aspx";
        #endregion

        #region IRechargeOption 成员
        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string NotifyUrl
        {
            get { return _notifyUrl; }
        }

        /// <summary>
        /// 支付返回地址
        /// </summary>
        public string ReturnUrl
        {
            get { return _returnUrl; }
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        public UserInfo GetCurrentUser(HttpContext context)
        {
            #region 获取当前操作用户
            Maticsoft.Accounts.Bus.User currentUser;
            Maticsoft.Accounts.Bus.AccountsPrincipal userPrincipal;
            if (!context.User.Identity.IsAuthenticated)
            {
                return null;
            }
            try
            {
                userPrincipal = new Maticsoft.Accounts.Bus.AccountsPrincipal(context.User.Identity.Name);
            }
            catch (System.Security.Principal.IdentityNotMappedException)
            {
                //用户在DB中不存在 退出
                System.Web.Security.FormsAuthentication.SignOut();
                context.Session.Remove(Common.Globals.SESSIONKEY_USER);
                context.Session.Clear();
                context.Session.Abandon();
                return null;
            }
            if (context.Session[Common.Globals.SESSIONKEY_USER] == null)
            {
                currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                context.Session[Common.Globals.SESSIONKEY_USER] = currentUser;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)context.Session[Common.Globals.SESSIONKEY_USER];
            }
            #endregion

            return new UserInfo { UserId = currentUser.UserID, Email = currentUser.Email };
        }

        /// <summary>
        /// 获取充值信息
        /// </summary>
        public RechargeRequestInfo GetRechargeRequest(long rechargeId)
        {
            BLL.Pay.RechargeRequest reBll = new BLL.Pay.RechargeRequest();
            Model.Pay.RechargeRequest reModel = reBll.GetModel(rechargeId);
            RechargeRequestInfo info =new RechargeRequestInfo ();
            if (reModel!=null)
            {
                info.PaymentGateway = reModel.PaymentGateway;
                info.PaymentTypeId = reModel.PaymentTypeId;
                info.RechargeBlance = reModel.RechargeBlance;
                info.RechargeId = reModel.RechargeId;
                info.TradeDate = reModel.TradeDate;
                info.UserId = reModel.UserId;
            }       
            return  info;
            //RechargeRequestInfo info = new RechargeRequestInfo {};
           // return PaymentModeManage.GetRechargeRequest(rechargeId);
        }
        /// <summary>
        /// 完成充值
        /// </summary>
        public bool PayForRechargeRequest(RechargeRequestInfo rechargeRequest)
        {
            #region 更新DB 完成充值
            BLL.Pay.RechargeRequest reBll = new BLL.Pay.RechargeRequest();
            Model.Pay.RechargeRequest reModel = new Model.Pay.RechargeRequest();
            if (rechargeRequest != null)
            {
                reModel.PaymentGateway = rechargeRequest.PaymentGateway;
                reModel.PaymentTypeId = rechargeRequest.PaymentTypeId;
                reModel.RechargeBlance = rechargeRequest.RechargeBlance;
                reModel.RechargeId = rechargeRequest.RechargeId;
                reModel.TradeDate = rechargeRequest.TradeDate;
                reModel.UserId = rechargeRequest.UserId;
            }
            reModel.Status = 1;
            return reBll.UpdateStatus(reModel);
            #endregion
        }
        #endregion
    }
}