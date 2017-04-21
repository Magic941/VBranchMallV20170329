namespace Maticsoft.Payment.Model
{
    public class PayeeInfo
    {
        private string emailAddress;
        private string partner;
        private string password;
        private string primaryKey;
        private string secondKey;
        private string sellerAccount;

        public virtual string EmailAddress
        {
            get
            {
                return this.emailAddress;
            }
            set
            {
                this.emailAddress = value;
            }
        }

        public virtual string Partner
        {
            get
            {
                return this.partner;
            }
            set
            {
                this.partner = value;
            }
        }

        public virtual string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public virtual string PrimaryKey
        {
            get
            {
                return this.primaryKey;
            }
            set
            {
                this.primaryKey = value;
            }
        }

        public virtual string SecondKey
        {
            get
            {
                return this.secondKey;
            }
            set
            {
                this.secondKey = value;
            }
        }

        public virtual string SellerAccount
        {
            get
            {
                return this.sellerAccount;
            }
            set
            {
                this.sellerAccount = value;
            }
        }
    }
}
