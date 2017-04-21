using System;
using System.ComponentModel;
using System.Web.UI;
using Maticsoft.Common;

namespace Maticsoft.Web.Controls
{
    public partial class ShowPaymentMode : System.Web.UI.UserControl
    {
        public bool ShowBalanceMode
        {
            get
            {
                if (ViewState["ShowRechangeMode"] != null)
                {
                    return Globals.SafeBool(ViewState["ShowRechangeMode"].ToString(), false);
                }
                return false;
            }
            set { ViewState["ShowRechangeMode"] = value; }
        }

        public string Balance
        {
            get
            {
                if (ViewState["Balance"] != null)
                {
                    return ViewState["Balance"].ToString();
                }
                return string.Empty;
            }
            set { ViewState["Balance"] = value; }
        }

        protected decimal TotalDeposits
        {
            get
            {
                if (ViewState["TotalDeposits"] != null)
                {
                    return (decimal)ViewState["TotalDeposits"];
                }
                return 0;
            }
            set { ViewState["TotalDeposits"] = value; }
        }

        protected bool IsEnableBalance
        {
            get
            {
                if (ViewState["IsEnableBalance"] != null)
                {
                    return Globals.SafeBool(ViewState["IsEnableBalance"].ToString(), false);
                }
                return false;
            }
            set { ViewState["IsEnableBalance"] = value; }
        }

        public int SelectValue
        {
            get { return Globals.SafeInt(hfShowPaymentModeSelect.Value, 1); }
            set { hfShowPaymentModeSelect.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IsEnableBalance = (Globals.SafeDecimal(Balance, 0) >= TotalDeposits);
                rptPaymentMode.DataSource = Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.ALL);
                rptPaymentMode.DataBind();
            }
        }
    }
}