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
using System.IO;
using Maticsoft.Common;
using Maticsoft.BLL.SysManage;

namespace Maticsoft.Web.Admin.Shop.Supplier.SupplierStore
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
                if (hasShopArea)
                {
                    trLogo1.Visible = true;
                    trLogo1Image.Visible = true;
                    trLogo3.Visible = true;
                    trLogo3Image.Visible = true;
                    trLogo4.Visible = true;
                    trLogo4Image.Visible = true;
                    trShop.Visible = true;
                }
                else
                {
                    ltlLogo2.Text = "Logo1";
                    ltlSquare.Text = "(用于微信商城店铺)";
                }
                if (hasMShopArea)
                {
                    trMShop.Visible = true;
                }
                else
                {
                    ltlSquare.Text = "(用于商城首页精品店铺)";
                    ltlLogo3.Text = "Logo2";
                }
                if (hasMShopArea && hasShopArea)
                {
                    ltlSquare.Text = "(用于商城首页精品店铺、微信商城店铺)";
                }
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

        public bool hasShopArea
        {
            get
            {
                return MvcApplication.HasArea(AreaRoute.Shop);//是否包含pc版 
            }
        }
        public bool hasMShopArea
        {
            get
            {
                return MvcApplication.HasArea(AreaRoute.MShop);//是否包含手机版 
            }
        }
        private void ShowInfo()
        {
            Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
            if (null != model)
            {
                if (model.StoreStatus == 2)
                {
                    lterShopClose.Visible = true;
                    btnClose.Visible = false;
                }
                if (model.StoreStatus == 1)
                {
                    btnClose.Visible = true;
                }
                this.txtShopName.Text = model.ShopName;
                this.txtIndexContent.Text = model.IndexContent;
                this.txtIndexProdTop.Text = model.IndexProdTop.ToString();

                this.radioStatus.SelectedValue = model.StoreStatus.ToString();

                this.txtQQ.Text = model.QQ;
                this.txtServicePhone.Text = model.ServicePhone;
                txtMobileCount.Text = BLL.Shop.Supplier.SupplierConfig.GetValueByCache(model.SupplierId, "MoblieIndexProdCount");
                this.Background.Text = model.Background;
                this.rdlBGType.SelectedValue = model.BGType.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ShopName = this.txtShopName.Text.Trim();
            int IndexProdTop = Globals.SafeInt(this.txtIndexProdTop.Text, 0);
            int MobileIndexCount = Globals.SafeInt(this.txtMobileCount.Text, 0);
            if (ShopName.Length == 0)
            {
                MessageBox.ShowFailTip(this, "店铺名称不能为空！");
                return;
            }
            else if (ShopName.Length > 100)
            {
                MessageBox.ShowFailTip(this, "店铺名称请控制在1~100字符！");
                return;
            }

            if (bll.ExistsShopName(ShopName, SupplierId))
            {
                MessageBox.ShowFailTip(this, "该店铺名称已经被注册，请更换店铺名称再操作！");
                return;
            }
            try
            {
                Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
                if (null != model)
                {

                    #region 移动文件
                    // 移动文件
                    string logoImagepath = "/Upload/Supplier/Logo/";
                    string logoDirPath = MapPath(logoImagepath);
                    if (!Directory.Exists(logoDirPath))
                    {
                        //不存在则自动创建文件夹
                        Directory.CreateDirectory(logoDirPath);
                    }
                    MoveFileToFTP(hfLogoUrlSearch.Value, logoImagepath, string.Format("{0}_T180X60.png", SupplierId));
                    MoveFileToFTP(hfLogoUrl.Value, logoImagepath, string.Format("{0}_T980X68.png", SupplierId));
                    MoveFileToFTP(hfLogoUrlSquare.Value, logoImagepath, string.Format("{0}_T200X200.png", SupplierId));
                    MoveFileToFTP(hfLogoUrlSale.Value, logoImagepath, string.Format("{0}_T200X400.png", SupplierId));
                    MoveFileToFTP(hfLogoUrlBackground.Value, logoImagepath, string.Format("{0}_T200X600.png", SupplierId));
                    #endregion

                    model.ShopName = ShopName;
                    model.IndexContent = this.txtIndexContent.Text;
                    model.IndexProdTop = IndexProdTop;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedUserId = CurrentUser.UserID;
                    model.StoreStatus = Common.Globals.SafeInt(this.radioStatus.SelectedValue, 0);
                    model.QQ = this.txtQQ.Text;
                    model.ServicePhone = this.txtServicePhone.Text;
                    model.BGType = Convert.ToBoolean(this.rdlBGType.SelectedValue);
                    model.Background = this.Background.Text.Trim();
                    if (bll.Update(model))
                    {
                        BLL.Shop.Supplier.SupplierConfig supplierConfig = new BLL.Shop.Supplier.SupplierConfig();
                        supplierConfig.Modify(SupplierId, "MoblieIndexProdCount", MobileIndexCount.ToString(), 1, "手机端首页显示数量");

                        DataCache.DeleteCache("SuppliersModel-" + SupplierId);//清除缓存
                        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "List.aspx");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void MoveFile(string oldFilePath, string newFilePath, string newFileName)
        {
            if (String.IsNullOrWhiteSpace(oldFilePath)) return;

            string oldFileMP = Server.MapPath(oldFilePath);
            string newFileMP = Server.MapPath(newFilePath+newFileName);

            if (File.Exists(oldFileMP))
            {
                if (File.Exists(newFileMP))
                {
                    File.Delete(newFileMP);
                }
                File.Move(oldFileMP, newFileMP);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="newFileName"></param>
        /// <param name="desPath"></param>
        private void MoveFileToFTP(string filepath, string newFilePath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                return;
            }

            MoveFile(filepath, newFilePath, fileName);

            if (File.Exists(Server.MapPath(newFilePath + "/" + fileName)))
            {

                FileManager.MoveImageForFTP(newFilePath + "/" + fileName, newFilePath);
            }

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (bll.CloseShop(SupplierId))
            {
                DataCache.DeleteCache("SuppliersModel-" + SupplierId);//清除缓存
                btnClose.Visible = false;
                txtShopName.Text = "";
                lterShopClose.Visible = true;
                MessageBox.ShowSuccessTip(this, "关闭成功！", "List.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, "关闭失败！", "List.aspx");
            }
        }
    }
}
