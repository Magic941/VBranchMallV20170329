/**
* NotifyQuery.cs
*
* 功 能： 支付通知处理
* 类 名： NotifyQuery
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
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Maticsoft.Payment.Handler;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.Core
{
    public abstract class NotifyQuery
    {
        public event NotifyEventHandler NotifyVerifyFaild;

        public event NotifyEventHandler PaidToIntermediary;

        public event NotifyEventHandler PaidToMerchant;

        protected NotifyQuery()
        {
        }

        public virtual string GetGatewayOrderId()
        {
            return string.Empty;
        }

        public abstract decimal GetOrderAmount();
        public abstract string GetOrderId();
        public abstract string GetNotifyId();
        public virtual string GetRemark1()
        {
            return string.Empty;
        }

        public virtual string GetRemark2()
        {
            return string.Empty;
        }

        /// <summary>
        /// 是否是来自阿里的请求，否则怕是外来请求也可以改变订单状态就是一个漏洞
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected virtual string GetResponse(string url, int? timeout = null)
        {
            string str;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (timeout.HasValue)
                {
                    request.Timeout = timeout.Value;
                }
                using (Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    StringBuilder builder = new StringBuilder();
                    while (-1 != reader.Peek())
                    {
                        builder.Append(reader.ReadLine());
                    }
                    str = builder.ToString();
                    reader.Close();
                    responseStream.Close();
                }
            }
            catch (Exception exception)
            {
                str = "Error:" + exception.Message;
            }
            return str;
        }

        public static NotifyQuery Instance(string notifyType, NameValueCollection parameters,
            NotifyMode mode = NotifyMode.None)
        {
            if (string.IsNullOrEmpty(notifyType))
            {
                return null;
            }
            object[] args = mode == NotifyMode.None ?
                new object[] { parameters } :           //回调和通知均使用同一种模式
                new object[] { parameters, mode };      //DONE: 使用差异通知模式 BEN NEW MODE 20131016
            return (Activator.CreateInstance(Type.GetType(notifyType), args) as NotifyQuery);
        }

        protected virtual void OnNotifyVerifyFaild()
        {
            if (this.NotifyVerifyFaild != null)
            {
                this.NotifyVerifyFaild(this);
            }
        }

        protected virtual void OnPaidToIntermediary()
        {
            if (this.PaidToIntermediary != null)
            {
                this.PaidToIntermediary(this);
            }
        }

        protected virtual void OnPaidToMerchant()
        {
            if (this.PaidToMerchant != null)
            {
                this.PaidToMerchant(this);
            }
        }

        public abstract void VerifyNotify(int timeout, PayeeInfo payee);
        public abstract void WriteBack(HttpContext context, bool success);

        public string ReturnUrl { get; set; }
    }

    /// <summary>
    /// 通知模式
    /// </summary>
    public enum NotifyMode
    {
        /// <summary>
        /// 未知
        /// </summary>
        None,
        /// <summary>
        /// 回调
        /// </summary>
        Callback,
        /// <summary>
        /// 通知
        /// </summary>
        Notify
    }
}

