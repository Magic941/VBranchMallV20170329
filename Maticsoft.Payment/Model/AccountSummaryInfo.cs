namespace Maticsoft.Payment.Model
{
    public class AccountSummaryInfo
    {
        private decimal accountAmount;
        private decimal freezeBalance;
        private decimal useableBalance;

        public decimal AccountAmount
        {
            get
            {
                return this.accountAmount;
            }
            set
            {
                this.accountAmount = value;
            }
        }

        public decimal FreezeBalance
        {
            get
            {
                return this.freezeBalance;
            }
            set
            {
                this.freezeBalance = value;
            }
        }

        public decimal UseableBalance
        {
            get
            {
                return this.useableBalance;
            }
            set
            {
                this.useableBalance = value;
            }
        }
    }
}