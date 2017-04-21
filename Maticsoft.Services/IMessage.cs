using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticsoft.Services
{
    public interface IMessage
    {
        string SendSMS(string phone, string message);

        string SendMultiSMS(string phone, string message);

        string SendSMSByTime(string phone, string message, string datatime);

        int SendSMS(string phone, string message, string sn, string pwd);
    }
}
