using System;

namespace Maticsoft.Payment.Model
{
    public class BalanceDetailInfo
    {
        private decimal balance;
        private decimal? expenses;
        private decimal? income;
        private long journalNumber;
        private int? payee;
        private int? payer;
        private string remark;
        private DateTime tradeDate;
        private TradeTypes tradeType;
        private int userId;

        public decimal Balance
        {
            get
            {
                return this.balance;
            }
            set
            {
                this.balance = value;
            }
        }

        public decimal? Expenses
        {
            get
            {
                return this.expenses;
            }
            set
            {
                this.expenses = value;
            }
        }

        public decimal? Income
        {
            get
            {
                return this.income;
            }
            set
            {
                this.income = value;
            }
        }

        public long JournalNumber
        {
            get
            {
                return this.journalNumber;
            }
            set
            {
                this.journalNumber = value;
            }
        }

        public int? Payee
        {
            get
            {
                return this.payee;
            }
            set
            {
                this.payee = value;
            }
        }

        public int? Payer
        {
            get
            {
                return this.payer;
            }
            set
            {
                this.payer = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }

        public DateTime TradeDate
        {
            get
            {
                return this.tradeDate;
            }
            set
            {
                this.tradeDate = value;
            }
        }

        public TradeTypes TradeType
        {
            get
            {
                return this.tradeType;
            }
            set
            {
                this.tradeType = value;
            }
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }
    }
}