/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
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
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Supplier.SupplierInfo
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 538; } } //Shop_商家管理_详细页
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
            Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
            Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
            if (null != model)
            {
                this.lblSupplierId.Text = model.SupplierId.ToString();
                this.lblName.Text = model.Name;
                this.lblIntroduction.Text = model.Introduction;
                if (model.RegisteredCapital.HasValue)
                {
                    this.lblRegisteredCapital.Text = model.RegisteredCapital.ToString();
                }
                this.lblTelPhone.Text = model.TelPhone;
                this.lblCellPhone.Text = model.CellPhone;
                this.lblContactMail.Text = model.ContactMail;
                if (model.RegionId.HasValue)
                {
                    this.RegionID.Region_iID = model.RegionId.Value;
                }
                this.lblAddress.Text = model.Address;
                this.lblRemark.Text = model.Remark;
                this.lblContact.Text = model.Contact;
                this.lblUserName.Text = model.UserName;
                if (model.EstablishedDate.HasValue)
                {
                    this.lblEstablishedDate.Text = model.EstablishedDate.ToString();
                }
                if (model.EstablishedCity.HasValue)
                {
                    this.RegionEstablishedCity.Region_iID = model.EstablishedCity.Value;
                }
                this.lblLOGO.Text = model.LOGO;
                this.lblFax.Text = model.Fax;
                this.lblPostCode.Text = model.PostCode;
                this.lblHomePage.Text = model.HomePage;
                this.lblArtiPerson.Text = model.ArtiPerson;
                this.lblEnteRank.Text = GetSuppRank(model.Rank);
                this.lblEnteClassName.Text = GetEnteClassName(model.CategoryId);
                if (model.CompanyType.HasValue)
                {
                    this.lblCompanyType.Text = GetCompanyType(model.CompanyType);
                }
                this.lblBusinessLicense.Text = model.BusinessLicense;
                this.lblTaxNumber.Text = model.TaxNumber;
                this.lblAccountBank.Text = model.AccountBank;
                this.lblAccountInfo.Text = model.AccountInfo;
                this.lblServicePhone.Text = model.ServicePhone;
                this.lblQQ.Text = model.QQ;
                this.lblMSN.Text = model.MSN;
                // 0未审核  1正常  2冻结   3删除
                this.lblStatus.Text = GetStatus(model.Status);
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblCreatedUserID.Text = model.UserName;
                if (model.UpdatedDate.HasValue)
                {
                    this.lblUpdatedDate.Text = model.UpdatedDate.ToString();
                }
                Maticsoft.Accounts.Bus.User userbll = new Maticsoft.Accounts.Bus.User();
                if (model.UpdatedUserId.HasValue)
                {
                    this.lblUpdatedUserID.Text = userbll.GetUserNameByCache(model.UpdatedUserId.Value);
                }
                this.lblBalance.Text = model.Balance.ToString("F2");
                this.lblAgentID.Text = model.AgentId.ToString();
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        #region 获取商家分类名称
        /// <summary>
        /// 获取商家分类名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetEnteClassName(object target)
        {
            //合资、独资、国有、私营、全民所有制、集体所有制、股份制、有限责任制
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "合资";
                        break;
                    case "2":
                        str = "独资";
                        break;
                    case "3":
                        str = "国有";
                        break;
                    case "4":
                        str = "私营";
                        break;
                    case "5":
                        str = "全民所有制";
                        break;
                    case "6":
                        str = "集体所有制";
                        break;
                    case "7":
                        str = "股份制";
                        break;
                    case "8":
                        str = "有限责任制";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家性质
        /// <summary>
        /// 获取商家性质
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetCompanyType(object target)
        {
            //0:个体工商; 1:私营独资商家; 2:国营商家。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "个体工商";
                        break;
                    case "2":
                        str = "私营独资商家";
                        break;
                    case "3":
                        str = "国营商家";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家审核状态
        /// <summary>
        /// 获取商家审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            //0:未审核; 1:正常;2:冻结;3:删除
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "未审核";
                        break;
                    case "1":
                        str = "正常";
                        break;
                    case "2":
                        str = "冻结";
                        break;
                    case "3":
                        str = "删除";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家等级
        /// <summary>
        /// 获取商家等级
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetSuppRank(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "一星级";
                        break;
                    case "2":
                        str = "二星级";
                        break;
                    case "3":
                        str = "三星级";
                        break;
                    case "4":
                        str = "四星级";
                        break;
                    case "5":
                        str = "五星级";
                        break;
                    default:
                        str = "无";
                        break;
                }
            }
            return str;
        }
        #endregion

    }
}
