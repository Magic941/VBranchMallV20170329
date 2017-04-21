namespace Maticsoft.Payment.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class PaymentModeInfo
    {
        private bool allowRecharge;
        private decimal charge;
        private string description;
        private int displaySequence;
        private string emailAddress;
        private string gateway;
        private bool isPercent;
        private string logo;
        private string merchantCode;
        private int modeId;
        private string name;
        private string partner;
        private string password;
        private string secondKey;
        private string secretKey;
        private IList<string> supportedCurrencys;
        private string drivePath;

        public string DrivePath
        {
            get { return drivePath; }
            set { drivePath = value; }
        }

        public decimal CalcPayCharge(decimal cartMoney)
        {
            if (!this.IsPercent)
            {
                return this.Charge;
            }
            return (cartMoney * (this.Charge / 100M));
        }

        public bool AllowRecharge
        {
            get
            {
                return this.allowRecharge;
            }
            set
            {
                this.allowRecharge = value;
            }
        }

        public decimal Charge
        {
            get
            {
                return this.charge;
            }
            set
            {
                this.charge = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public int DisplaySequence
        {
            get
            {
                return this.displaySequence;
            }
            set
            {
                this.displaySequence = value;
            }
        }

        public string EmailAddress
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

        public string Gateway
        {
            get
            {
                return this.gateway;
            }
            set
            {
                this.gateway = value;
            }
        }

        public bool IsBank
        {
            get
            {
                return (string.Compare(this.gateway, "Bank", true) == 0);
            }
        }

        public bool IsPercent
        {
            get
            {
                return this.isPercent;
            }
            set
            {
                this.isPercent = value;
            }
        }

        public bool IsPOD
        {
            get
            {
                return (string.Compare(this.gateway, "COD", true) == 0);
            }
        }

        public string Logo
        {
            get
            {
                return this.logo;
            }
            set
            {
                this.logo = value;
            }
        }

        public string MerchantCode
        {
            get
            {
                return this.merchantCode;
            }
            set
            {
                this.merchantCode = value;
            }
        }

        public int ModeId
        {
            get
            {
                return this.modeId;
            }
            set
            {
                this.modeId = value;
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

        public string Partner
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

        public string Password
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

        public string SecondKey
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

        public string SecretKey
        {
            get
            {
                return this.secretKey;
            }
            set
            {
                this.secretKey = value;
            }
        }

        public IList<string> SupportedCurrencys
        {
            get
            {
                if (this.supportedCurrencys == null)
                {
                    this.supportedCurrencys = new List<string>();
                }
                return this.supportedCurrencys;
            }
        }
    }
}

