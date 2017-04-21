using System.Globalization;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Bank
{
    internal class BankRequest : PaymentRequest
    {
        private string orderId;
        private GatewayInfo getGateway;

        public BankRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.orderId = trade.OrderId;
            this.getGateway = gateway;
        }

        public override void SendRequest()
        {
            Configuration.GatewayProvider provider = Configuration.PayConfiguration.GetConfig().Providers["bank"] as Configuration.GatewayProvider;
            if (provider != null)
            {
                this.RedirectToGateway(string.Format(CultureInfo.InvariantCulture, provider.Attributes["urlFormat"], new object[] { this.orderId, this.getGateway.Data }));
            }
        }
    }
}
