using System;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.Web
{
    [Obsolete]
    public abstract class RechargeReturn<T> : RechargeReturnTemplatedPage<T> where T : class,IRechargeRequest, new()
    {
        //protected Literal litMessage;

        public RechargeReturn()
            : base(false)
        {
        }

        //protected override void DisplayMessage(string status)
        //{
        //    switch (status)
        //    {
        //        case "gatewaynotfound":
        //            this.litMessage.Text = string.Format((string)HttpContext.GetGlobalResourceObject("Resources", "IDS_RechargeStatus_GatewayNotFound"), base.GatewayName);
        //            return;

        //        case "waitconfirm":
        //            this.litMessage.Text = (string)HttpContext.GetGlobalResourceObject("Resources", "IDS_RechargeStatus_WaitConfirm");
        //            return;

        //        case "success":
        //            this.litMessage.Text = string.Format((string)HttpContext.GetGlobalResourceObject("Resources", "IDS_RechargeStatus_Success"), this.Amount.ToString("F"));
        //            return;

        //        case "fail":
        //            this.litMessage.Text = (string)HttpContext.GetGlobalResourceObject("Resources", "IDS_RechargeStatus_Fail");
        //            return;

        //        case "verifyfaild":
        //            this.litMessage.Text = (string)HttpContext.GetGlobalResourceObject("Resources", "IDS_RechargeStatus_VerifyFaild");
        //            return;
        //    }
        //    this.litMessage.Text = (string)HttpContext.GetGlobalResourceObject("Resources", "IDS_RechargeStatus_Unknow");
        //}


        protected override void DisplayMessage(string status)
        {
            throw new NotImplementedException();
        }
    }
}

