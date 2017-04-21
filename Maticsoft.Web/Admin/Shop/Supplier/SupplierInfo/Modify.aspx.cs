/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01   
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Supplier.SupplierInfo
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 537; } } //Shop_商家管理_编辑页
        Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        public int SupplierId
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
            if (null != model)
            {
                this.txtName.Text = model.Name;
                this.txtIntroduction.Text = model.Introduction;
                if (model.RegisteredCapital.HasValue)
                {
                    this.txtRegisteredCapital.Text = model.RegisteredCapital.ToString();
                }
                this.txtTelPhone.Text = model.TelPhone;
                this.txtCellPhone.Text = model.CellPhone;
                this.txtContactMail.Text = model.ContactMail;
                if (model.RegionId.HasValue)
                {
                    this.RegionID.Region_iID = model.RegionId.Value;
                }
                this.txtAddress.Text = model.Address;
                this.txtRemark.Text = model.Remark;
                this.txtContact.Text = model.Contact;
                if (model.EstablishedDate.HasValue)
                {
                    this.txtEstablishedDate.Text = model.EstablishedDate.Value.ToString("yyyy-MM-dd");
                }
                if (model.EstablishedCity.HasValue)
                {
                    this.RegionEstablishedCity.Region_iID = model.EstablishedCity.Value;
                }
                this.txtFax.Text = model.Fax;
                this.txtPostCode.Text = model.PostCode;
                this.txtHomePage.Text = model.HomePage;
                this.txtArtiPerson.Text = model.ArtiPerson;
                if (model.Rank > 0)
                {
                    this.dropEnteRank.SelectedValue = model.Rank.ToString();
                }
                if (model.CategoryId > 0)
                {
                    this.dropEnteClassID.SelectedValue = model.CategoryId.ToString();
                }
                if (model.CompanyType.HasValue)
                {
                    this.dropCompanyType.SelectedValue = model.CompanyType.ToString();
                }
                this.txtTaxNumber.Text = model.TaxNumber;
                this.txtAccountBank.Text = model.AccountBank;
                this.txtAccountInfo.Text = model.AccountInfo;
                this.txtServicePhone.Text = model.ServicePhone;
                this.txtQQ.Text = model.QQ;
                this.txtMSN.Text = model.MSN;
                this.txtSlogan.Text = model.Slogan;
                this.txtDiscount.Text = model.Discount;
                if (model.CompanyType.HasValue)
                {
                    this.radlStatus.SelectedValue = model.Status.ToString();
                }
                this.txtBalance.Text = model.Balance.ToString("F2");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Name = this.txtName.Text.Trim();
            if (Name.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "商家名称不能为空！");
                return;
            }
            if (Name.Length > 100)
            {
                MessageBox.ShowServerBusyTip(this, "商家名称请控制在1~100字符！");
                return;
            }
            if (bll.Exists(Name, SupplierId))
            {
                MessageBox.ShowServerBusyTip(this, "该商家名称已经被注册，请更换商家名称再操作！");
                return;
            }
            Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
            if (null != model)
            {
                model.Name = Name;
                model.Introduction = this.txtIntroduction.Text;
                model.RegisteredCapital = Globals.SafeInt(this.txtRegisteredCapital.Text, 0);
                model.TelPhone = this.txtTelPhone.Text;
                model.CellPhone = this.txtCellPhone.Text;
                model.ContactMail = this.txtContactMail.Text;
                model.RegionId = this.RegionID.Region_iID;
                model.Address = this.txtAddress.Text;
                model.Remark = this.txtRemark.Text;
                model.Contact = this.txtContact.Text;
                //model.UserName = this.txtUserName.Text;
                string EstablishedDate = this.txtEstablishedDate.Text;
                if (PageValidate.IsDateTime(EstablishedDate))
                {
                    model.EstablishedDate = Globals.SafeDateTime(EstablishedDate, DateTime.Now);
                }
                else
                {
                    model.EstablishedDate = null;
                }
                model.EstablishedCity = this.RegionEstablishedCity.Region_iID;
                model.LOGO = this.txtLOGO.Text;
                model.Fax = this.txtFax.Text;
                model.PostCode = this.txtPostCode.Text;
                model.HomePage = this.txtHomePage.Text;
                model.ArtiPerson = this.txtArtiPerson.Text;
                model.Rank = Globals.SafeInt(this.dropEnteRank.SelectedValue, 0);
                model.CategoryId = Globals.SafeInt(this.dropEnteClassID.SelectedValue, 0);
                model.CompanyType = Globals.SafeInt(this.dropCompanyType.SelectedValue, 0);
                model.BusinessLicense = this.txtBusinessLicense.Text;
                model.TaxNumber = this.txtTaxNumber.Text;
                model.AccountBank = this.txtAccountBank.Text;
                model.AccountInfo = this.txtAccountInfo.Text;
                model.ServicePhone = this.txtServicePhone.Text;
                model.QQ = this.txtQQ.Text;
                model.MSN = this.txtMSN.Text;
                model.Sequence = Globals.SafeInt(this.txtSequence.Text, 0);
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                
                
                model.UpdatedDate = DateTime.Now;
                model.UpdatedUserId = CurrentUser.UserID;
                model.Balance = Globals.SafeDecimal(this.txtBalance.Text, 0);
                model.AgentId = Globals.SafeInt(this.txtAgentID.Text, 0);
                model.Slogan = txtSlogan.Text;
                model.Discount = txtDiscount.Text;
                if (bll.Update(model))
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "List.aspx");
                    Cache.Remove("SuppliersModel-"+SupplierId);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                }
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
