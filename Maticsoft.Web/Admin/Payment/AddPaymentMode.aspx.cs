using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Payment.BLL;
using Maticsoft.Payment.Configuration;
using Maticsoft.Payment.Model;
using Maticsoft.Controls;
using System.Collections.Generic;

namespace Maticsoft.Web.Admin
{
    public partial class AddPaymentMode : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 348; } } //系统管理_支付方式管理_添加页
        private string errorMessage = "";
        private PaymentModeInfo item;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
            this.dropPayInterface.SelectedIndexChanged += new EventHandler(this.dropPayInterface_SelectedIndexChanged);
            if (!this.Page.IsPostBack)
            {
                this.dropPayInterface.DataBind();
                this.DisplayControls();
                if (this.item != null)
                {
                    this.dropPayInterface.SelectedValue = this.item.Gateway;
                    this.DisplayControls();
                    this.txtName.Text = Globals.HtmlDecode(this.item.Name);
                    this.txtMerchantCode.Text = this.item.MerchantCode;
                    this.radAllowRecharge.SelectedValue = this.item.AllowRecharge;
                    this.fcContent.Value = this.item.Description;
                    if (!string.IsNullOrWhiteSpace(this.item.EmailAddress))
                    {
                        this.txtEmailAddress.Text = Globals.HtmlDecode(this.item.EmailAddress);
                    }
                    this.txtDisplaySequence.Text = this.item.DisplaySequence.ToString(CultureInfo.InvariantCulture);
                    this.txtSecretKey.Text = this.item.SecretKey;
                    this.txtSecondKey.Text = this.item.SecondKey;
                    this.txtCharge.Text = this.item.Charge.ToString("F", CultureInfo.InvariantCulture);
                    this.chkIsPercent.Checked = this.item.IsPercent;
                    if (!string.IsNullOrWhiteSpace(this.item.Password))
                    {
                        this.txtPassword.Text = this.item.Password;
                    }
                    if (!string.IsNullOrWhiteSpace(this.item.Partner))
                    {
                        this.txtPartner.Text = Globals.HtmlDecode(this.item.Partner);
                    }
                    foreach (string str in this.item.SupportedCurrencys)
                    {
                        if (str == "")
                        {
                            this.chkCurrencysList.Items[0].Selected = true;
                        }
                        else
                        {
                            this.chkCurrencysList.Items.FindByValue(str).Selected = true;
                        }
                    }
                }
            }
            this.txtName.Focus();
        }
        #endregion

        #region btnCreate_Click
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!this.IsValid)
            {
                this.ShowMsg(this.ErrorMessage, false);
            }
            else
            {
                switch (PaymentModeManage.CreatePaymentMode(this.Item))
                {
                    case PaymentModeActionStatus.Success:
                        this.ShowMsg((string)HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_Message_Create_Success"), true);
                        return;

                    case PaymentModeActionStatus.DuplicateGateway:
                        this.ShowMsg((string)HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_GatewayExists"), false);
                        return;

                    case PaymentModeActionStatus.DuplicateName:
                        this.ShowMsg((string)HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_PaymentNameExitis"), false);
                        return;

                    case PaymentModeActionStatus.OutofNumber:
                        this.ShowMsg((string)HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_OutofNumber"), false);
                        return;
                }
                this.ShowMsg((string)HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_Create_UnKnowError"), false);
            }
        }
        #endregion

        #region ShowData
        private void DisplayControls()
        {
            if (string.IsNullOrWhiteSpace(this.dropPayInterface.SelectedValue))
            {
                this.HiddenAll();
            }
            else
            {
                if (((this.dropPayInterface.SelectedValue.ToLower() == "cod") ||
                    (this.dropPayInterface.SelectedValue.ToLower() == "advanceaccount")) ||
                    (this.dropPayInterface.SelectedValue.ToLower() == "bank"))
                {
                    chkWap.Enabled = chkWeb.Enabled = true;
                    this.radAllowRecharge.SelectedValue = false;
                    this.radAllowRecharge.Enabled = false;
                }
                else
                {
                    chkWap.Enabled = chkWeb.Enabled = false;
                    this.radAllowRecharge.SelectedValue = true;
                    this.radAllowRecharge.Enabled = true;
                }

                chkWeb.Checked = true;
                chkWap.Checked = false;
                if (this.dropPayInterface.SelectedValue.ToLower() == "alipaywap" ||
                    this.dropPayInterface.SelectedValue.ToLower() == "wechat")
                {
                    chkWap.Enabled = chkWeb.Enabled = false;
                    chkWeb.Checked = false;
                    chkWap.Checked = true;
                }

                GatewayProvider provider = PayConfiguration.GetConfig().Providers[this.dropPayInterface.SelectedValue] as GatewayProvider;
                this.tblrImage.Visible = provider.Attributes["emailAddress"].ToLower() == "true";
                this.tblrSecretKey.Visible = provider.Attributes["primaryKey"].ToLower() == "true";
                this.tblrSecondKey.Visible = provider.Attributes["secondKey"].ToLower() == "true";
                this.tblrPassword.Visible = provider.Attributes["password"].ToLower() == "true";
                this.tblrPartner.Visible = provider.Attributes["partner"].ToLower() == "true";
                this.tblrCurrencys.Visible = provider.SupportedCurrencys.Count > 0;
                this.tblrMerchantCode.Visible = provider.Attributes["sellerAccount"].ToLower() == "true";
                this.tblCharge.Visible = (provider.Name != "advanceaccount") && !(provider.Name == "bank");
                this.chkCurrencysList.Items.Clear();
                foreach (string str in provider.SupportedCurrencys)
                {
                    if ((provider.Name == "advanceaccount") || (provider.Name == "bank"))
                    {
                        //默认货币 - 人民币
                        string defaultCurrency = "CNY";
                        this.chkCurrencysList.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("Resources", "Currency_" + defaultCurrency), defaultCurrency));
                        continue;
                    }
                    this.chkCurrencysList.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("Resources", "Currency_" + str), str));
                }
                if ((provider.Attributes["url"] != null) && (provider.Attributes["url"].Trim().Length > 0))
                {
                    if ((provider.Attributes["logo"] != null) && (provider.Attributes["logo"].Trim().Length > 0))
                    {
                        this.hlinkImage.NavigateUrl = provider.Attributes["url"].Replace("^", "&");
                        this.lblimage.Text = string.Format(CultureInfo.InvariantCulture, "<img src=\"{0}\" border=\"0\" />", new object[] { provider.Attributes["logo"] });
                        this.lblimage.Visible = true;
                    }
                    else
                    {
                        this.lblimage.Text = provider.Attributes["url"];
                        this.lblimage.Visible = false;
                    }
                }
                else
                {
                    this.lblimage.Visible = false;
                }
            }
        }

        private void dropPayInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayControls();
        }

        private void HiddenAll()
        {
            this.chkCurrencysList.Items.Clear();
            this.lblimage.Text = string.Empty;
            this.tblrImage.Visible = false;
            this.tblrPartner.Visible = false;
            this.tblrSecondKey.Visible = false;
            this.tblrSecretKey.Visible = false;
            this.tblrPassword.Visible = false;
            this.tblrMerchantCode.Visible = false;
            this.tblrCurrencys.Visible = false;
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
        }

        public new bool IsValid
        {
            get
            {
                this.errorMessage = "";
                bool flag = true;
                if (string.IsNullOrWhiteSpace(this.dropPayInterface.SelectedValue))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_SelectPayInterface"));
                    return false;
                }
                if (this.txtName.Text.Trim().Length == 0)
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymentModeName_NotInput"));
                    flag = false;
                }
                if (this.txtName.Text.Trim().Length > 200)
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymentModeName_OutLength"));
                    flag = false;
                }
                if (this.tblrMerchantCode.Visible && (this.txtMerchantCode.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_MerchantCode_NotInput"));
                    flag = false;
                }
                if (this.tblrSecretKey.Visible && (this.txtSecretKey.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_SecretKey_NotInput"));
                    flag = false;
                }
                if (this.tblrSecondKey.Visible && (this.txtSecondKey.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_SecondKey_NotInput"));
                    flag = false;
                }
                if (this.tblrPassword.Visible && (this.txtPassword.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_Password_NotInput"));
                    flag = false;
                }
                if (this.tblrCurrencys.Visible && (this.chkCurrencysList.SelectedIndex == -1))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_Currency_NotSelect"));
                    flag = false;
                }
                if (this.tblCharge.Visible && (Globals.SafeDecimal(this.txtCharge.Text, -1M) <= -1M))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string)HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymentMode_Charge_Error"));
                    flag = false;
                }
                return flag;
            }
        }

        public PaymentModeInfo Item
        {
            get
            {
                GatewayProvider provider = PayConfiguration.GetConfig().Providers[this.dropPayInterface.SelectedValue] as GatewayProvider;
                string drivePath = "";
                List<string> pathList = new List<string>();
                if (chkWeb.Checked)
                {
                    pathList.Add("1");
                }
                if (chkWap.Checked)
                {
                    pathList.Add("2");
                }
                drivePath = "|" + String.Join("|", pathList) + "|";
                PaymentModeInfo info = new PaymentModeInfo
                {
                    ModeId = Globals.SafeInt(this.Page.Request.QueryString["modeId"], -1),
                    MerchantCode = this.txtMerchantCode.Text.Trim(),
                    EmailAddress = Globals.HtmlEncode(this.txtEmailAddress.Text.Trim()),
                    SecretKey = this.txtSecretKey.Text.Trim(),
                    SecondKey = this.txtSecondKey.Text.Trim(),
                    Password = this.txtPassword.Text.Trim(),
                    Partner = Globals.HtmlEncode(this.txtPartner.Text.Trim()),
                    Name = Globals.HtmlEncode(this.txtName.Text.Trim()),
                    Description = this.fcContent.Value.Replace("\r\n", "").Replace("\r", "").Replace("\n", ""),
                    Gateway = this.dropPayInterface.SelectedValue.ToLower(),
                    DisplaySequence = Globals.SafeInt(this.txtDisplaySequence.Text.Trim(), -1),
                    AllowRecharge = this.radAllowRecharge.SelectedValue,
                    DrivePath = drivePath
                };
                if ((provider.Name != "advanceaccount") && (provider.Name != "bank"))
                {
                    info.Charge = Globals.SafeDecimal(this.txtCharge.Text.Trim(), 0M);
                    info.IsPercent = this.chkIsPercent.Checked;
                }
                foreach (ListItem item in this.chkCurrencysList.Items)
                {
                    if (item.Selected)
                    {
                        info.SupportedCurrencys.Add(item.Value);
                    }
                }
                return info;
            }
            set
            {
                this.item = value;
                this.item.Gateway = this.item.Gateway.ToLower();
            }
        }

        protected virtual void ShowMsg(string msg, bool success)
        {
            this.ShowMsg(msg, success, false);
        }

        private void ShowMsg(string msg, bool success, bool isWarning)
        {
            this.statusMessage.Success = success;
            this.statusMessage.IsWarning = isWarning;
            this.statusMessage.Text = msg;
            this.statusMessage.Visible = true;
        }
        #endregion
    }
}