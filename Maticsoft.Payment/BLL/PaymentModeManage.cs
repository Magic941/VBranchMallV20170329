/**
* PaymentModeManage.cs
*
* 功 能： 支付模块业务数据交互类
* 类 名： PaymentModeManage
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
using System.Collections.Generic;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.DAL;
using Maticsoft.Payment.Model;
using GL.Payment;

namespace Maticsoft.Payment.BLL
{
    public static class PaymentModeManage
    {
        private static PaymentModeService service = new PaymentModeService();

        #region Payment
        public static List<PaymentModeInfo> GetPaymentModes()
        {
            return service.GetPaymentModes((int)Model.DriveEnum.ALL);
        }
        public static List<PaymentModeInfo> GetPaymentModes(Model.DriveEnum driveEnum = Model.DriveEnum.ALL)
        {
            return service.GetPaymentModes((int)driveEnum);
        }

        public static PaymentModeInfo GetPaymentModeById(int modeId)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(modeId);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        [Obsolete]
        public static PaymentModeInfo GetPaymentModeByName(string gateway)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(gateway);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        [Obsolete]
        public static PaymentModeInfo GetPaymentMode(int modeId)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(modeId);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        [Obsolete]
        public static PaymentModeInfo GetPaymentMode(string gateway)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(gateway);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        public static PaymentModeActionStatus CreatePaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode == null)
            {
                return PaymentModeActionStatus.UnknowError;
            }
            EncryptPaymentMode(paymentMode);
            return service.CreateUpdateDeletePaymentMode(paymentMode, DataProviderAction.Create);
        }

        public static PaymentModeActionStatus UpdatePaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode == null)
            {
                return PaymentModeActionStatus.UnknowError;
            }
            EncryptPaymentMode(paymentMode);
            return service.CreateUpdateDeletePaymentMode(paymentMode, DataProviderAction.Update);
        }

        public static bool DeletePaymentMode(int modeId)
        {
            PaymentModeActionStatus unknowError = PaymentModeActionStatus.UnknowError;
            PaymentModeInfo paymentMode = new PaymentModeInfo
            {
                ModeId = modeId
            };
            unknowError = service.CreateUpdateDeletePaymentMode(paymentMode, DataProviderAction.Delete);
            return (unknowError == PaymentModeActionStatus.Success);
        }

        public static void AscPaymentMode(int modeId)
        {
            service.SortPaymentMode(modeId, SortAction.ASC);
        }
        public static void DescPaymentMode(int modeId)
        {
            service.SortPaymentMode(modeId, SortAction.Desc);
        }

        internal static void DecryptPaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode != null)
            {
                using (MsCryptographer cryptographer = new MsCryptographer(true, false))
                {
                    if (!string.IsNullOrEmpty(paymentMode.SecretKey))
                    {
                        paymentMode.SecretKey = cryptographer.Decrypt(paymentMode.SecretKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.SecondKey))
                    {
                        paymentMode.SecondKey = cryptographer.Decrypt(paymentMode.SecondKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Password))
                    {
                        paymentMode.Password = cryptographer.Decrypt(paymentMode.Password);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Partner))
                    {
                        paymentMode.Partner = cryptographer.Decrypt(paymentMode.Partner);
                    }
                }
            }
        }

        internal static void EncryptPaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode != null)
            {
                using (MsCryptographer cryptographer = new MsCryptographer(false, true))
                {
                    if (!string.IsNullOrEmpty(paymentMode.SecretKey))
                    {
                        paymentMode.SecretKey = cryptographer.Encrypt(paymentMode.SecretKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.SecondKey))
                    {
                        paymentMode.SecondKey = cryptographer.Encrypt(paymentMode.SecondKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Password))
                    {
                        paymentMode.Password = cryptographer.Encrypt(paymentMode.Password);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Partner))
                    {
                        paymentMode.Partner = cryptographer.Encrypt(paymentMode.Partner);
                    }
                }
            }
        }
        #endregion

        #region AccountSummary
        public static AccountSummaryInfo GetAccountSummary(int userId)
        {
            return service.GetAccountSummary(userId);
        }
        #endregion

        #region Recharge
        public static RechargeRequestInfo GetRechargeRequest(long rechargeId)
        {
            return service.GetRechargeRequest(rechargeId);
        }
        public static long AddRechargeBalance(RechargeRequestInfo rechargeRequest)
        {
            return service.AddRechargeBlance(rechargeRequest);
        }
        public static bool RemoveRechargeRequest(long rechargeId)
        {
            return (service.RemoveRechargeRequest(rechargeId) > 0);
        }
        public static bool AddBalanceDetail(BalanceDetailInfo balanceDetails)
        {
            return service.AddBalanceDetail(balanceDetails);
        }
        #endregion

        #region  银联商务支付问题
        /// <summary>
        /// 保存银联商务订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool SaveChinaUMSOrder(ChinaUMSOrder order)
        {
            return service.SaveChinaUMSOrder(order);
        }
        #endregion

        #region 微信支付

        /// <summary>
        /// 保存微信预支付订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool SaveWeiXinPrepayOrder(Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage.UnifiedPrePayMessage order)
        {
            return service.SaveWeiXinPrepayOrder(order);
        }

        /// <summary>
        /// 根据商户订单ID查询微信预支付订单
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public static Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage.UnifiedPrePayMessage GetWeiXinPrepayOrder(string outTradeNo)
        {
            return service.GetWeiXinPrepayOrder(outTradeNo);
        }

        /// <summary>
        /// 检验预支付订单是否已经存在
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public static bool WeiXinPrepayExists(string outTradeNo)
        {
            return service.WeiXinPrepayOrderExists(outTradeNo);
        }

        #endregion
    }
}