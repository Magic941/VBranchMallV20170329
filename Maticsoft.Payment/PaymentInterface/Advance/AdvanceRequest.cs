using System.Globalization;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Advance
{
    internal class AdvanceRequest : PaymentRequest
    {
        private string orderId;
        private GatewayInfo getGateway;

        public AdvanceRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.orderId = trade.OrderId;
            this.getGateway = gateway;
        }

        public override void SendRequest()
        {
            Configuration.GatewayProvider provider = Configuration.PayConfiguration.GetConfig().Providers["advanceaccount"] as Configuration.GatewayProvider;
            if (provider != null)
            {
                this.RedirectToGateway(string.Format(CultureInfo.InvariantCulture, provider.Attributes["urlFormat"], new object[] { this.orderId, this.getGateway.Data }));
            }
        }
    }
}
