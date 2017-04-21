using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GL.Payment.UnionPay
{
    /// <summary>
    /// 银联支付配置信息
    /// 创建用户：shiyuankao
    /// 创建时间：2014-07-18
    /// </summary>
    internal class UnionPayConfig
    {
        #region 字段定义

        /// <summary>
        /// 商户私钥
        /// </summary>
        private static string _privateKey = string.Empty;

        /// <summary>
        /// 银商公钥
        /// </summary>
        private static string _publicKey = string.Empty;

        /// <summary>
        /// 下单地址
        /// </summary>
        private static string _orderUrl = string.Empty;

        /// <summary>
        /// 支付地址
        /// </summary>
        private static string _paymentUrl = string.Empty;

        /// <summary>
        /// 订单查询地址
        /// </summary>
        private static string _queryOrderUrl = string.Empty;

        /// <summary>
        /// 商户后台接收支付结果成功通知地址
        /// </summary>
        private static string _notifyUrl = string.Empty;

        /// <summary>
        /// 商户号
        /// </summary>
        private static string _merId = string.Empty;

        /// <summary>
        /// 终端号
        /// </summary>
        private static string _merTermId = string.Empty;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        static UnionPayConfig()
        {
            Init();
        }

        /// <summary>
        /// 初始化支付参数信息
        /// </summary>
        private static void Init()
        {
            // 加载Config文件
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (basePath.IndexOf("bin", System.StringComparison.Ordinal) == -1)
            {
                basePath = basePath + "bin";
            }
            var fullPath = string.Concat(basePath, "\\PayConfig\\", "UnionPay.config");
            if (!System.IO.File.Exists(fullPath))
                throw new Exception("未能找到银联支付对应【UnionPay.config】配置文件。");
            var doc = new XmlDocument();
            doc.Load(fullPath);
            var privateKeyUrl = doc.SelectSingleNode(@"root/private_key_url");
            var publicKeyUrl = doc.SelectSingleNode(@"root/public_key_url");
            var orderUrl = doc.SelectSingleNode(@"root/order_url");
            var queryOrderUrl = doc.SelectSingleNode(@"root/query_order_url");
            //var notifyUrl = doc.SelectSingleNode(@"root/notify_url");
            var merId = doc.SelectSingleNode(@"root/merId");
            var merTermId = doc.SelectSingleNode(@"root/merTermId");
            var _privateKeyUrl = privateKeyUrl == null ? string.Empty : privateKeyUrl.InnerText;
            var _publicKeyUrl = publicKeyUrl == null ? string.Empty : publicKeyUrl.InnerText;
            _paymentUrl = doc.SelectSingleNode(@"root/payment_url").InnerText;
            _orderUrl = orderUrl == null ? string.Empty : orderUrl.InnerText;
            _queryOrderUrl = queryOrderUrl == null ? string.Empty : queryOrderUrl.InnerText;
            //_notifyUrl = notifyUrl == null ? string.Empty : notifyUrl.InnerText;
            _notifyUrl = Maticsoft.Payment.Core.Globals.FullPath(string.Format(Maticsoft.Payment.Core.Globals.PAYMENT_NOTIFY_URL, Maticsoft.Payment.Core.Globals.EncodeData4Url("chinaums")));
            fullPath = string.Concat(basePath, _privateKeyUrl);
            if (!System.IO.File.Exists(fullPath))
                throw new Exception("未能找到银联支付对应【private_key.pem】商户私钥文件。");
            _privateKey = Common.Common.ReadFile(fullPath);
            fullPath = string.Concat(basePath, _publicKeyUrl);
            if (!System.IO.File.Exists(fullPath))
                throw new Exception("未能找到银联支付对应【public_key.pem】银商公钥文件。");
            _publicKey = Common.Common.ReadFile(fullPath);
            _merId = merId == null ? string.Empty : merId.InnerText;
            _merTermId = merTermId == null ? string.Empty : merTermId.InnerText;
           
        }

        #region 属性

        /// <summary>
        /// 商户私钥
        /// </summary>
        public static string PrivateKey
        {
            get { return _privateKey; }
        }

        /// <summary>
        /// 银商公钥
        /// </summary>
        public static string PublicKey
        {
            get { return _publicKey; }
        }

        /// <summary>
        /// 下单地址
        /// </summary>
        public static string OrderUrl
        {
            get { return _orderUrl; }
        }

        /// <summary>
        /// 订单查询地址
        /// </summary>
        public static string QueryOrderUrl
        {
            get { return _queryOrderUrl; }
        }

        /// <summary>
        /// 商户后台接收支付结果成功通知地址
        /// </summary>
        public static string NotifyUrl
        {
            get { return _notifyUrl; }
        }


        /// <summary>
        /// 商户收银台地址
        /// </summary>
        public static string PaymentURL
        {
            get { return _paymentUrl; }
            set { _paymentUrl = value; }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public static string MerId
        {
            get { return _merId; }
        }

        /// <summary>
        /// 终端号
        /// </summary>
        public static string MerTermId
        {
            get { return _merTermId; }
        }

        #endregion
    }
}
