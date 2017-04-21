
namespace Maticsoft.Payment.Model
{
    public class CurrencyInfo
    {
        private string code;
        private decimal exchangeRate;
        private string name;
        private string symbol;

        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public decimal ExchangeRate
        {
            get
            {
                return this.exchangeRate;
            }
            set
            {
                this.exchangeRate = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Symbol
        {
            get
            {
                return this.symbol;
            }
            set
            {
                this.symbol = value;
            }
        }
    }
}
