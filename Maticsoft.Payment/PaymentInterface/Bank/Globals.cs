using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Maticsoft.Payment.PaymentInterface.Bank
{
    internal sealed class Globals
    {
        private Globals()
        { }

        internal static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        internal static string CreateBankDirectUrl(string Amount, string Moneytype, string orderId,
                                                string Mid, string Url, string pay_ChinaBank_md5,
                                                string Md5info, string _input_charset, string GateWay,string remark2)
        {
            StringBuilder sb = new StringBuilder();
            string text = Amount + Moneytype + orderId + Mid + Url + pay_ChinaBank_md5; // 拼凑加密串
            Md5info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5").ToUpper();
            sb.Append(GateWay);
            sb.AppendFormat("v_md5info={0}&", Md5info);
            sb.AppendFormat("v_mid={0}&", Mid);
            sb.AppendFormat("v_oid={0}&", orderId);
            sb.AppendFormat("v_amount={0}&", Amount);
            sb.AppendFormat("v_moneytype={0}&", Moneytype);
            sb.AppendFormat("v_url={0}&", Url);
            sb.AppendFormat("remark2={0}", remark2);

            
            return sb.ToString();
        }
    }
}

