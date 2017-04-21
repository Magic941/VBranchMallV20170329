using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.Payment
{
     /// <summary>
     /// 银联商务支付订单
     /// </summary>
    public class ChinaUMSOrder
    {
        /// <summary>
        /// 成功标志
        /// </summary>
        public int IsSuccess { get; set; }
        public string TransCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string MerOrderId { get; set; }
        public string TransType { get; set; }
        public decimal TransAmt { get; set; }
        public string MerId { get; set; }
        public string MerTermId { get; set; }
        public string NotifyUrl { get; set; }
        public string Reserve { get; set; }
        public string OrderDesc { get; set; }
        public int EffectiveTime { get; set; }
        public string RespCode { get; set; }
        public string Msg { get; set; }
        public string ChrCode { get; set; }
        public string TransId { get; set; }
        public string OrderId { get; set; }      
      
    }
}
