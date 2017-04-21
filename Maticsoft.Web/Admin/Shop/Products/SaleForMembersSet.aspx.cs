/**
* SelectCategory.cs
*
* 功 能： 选择商品分类
* 类 名： SelectCategory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/01 11:12:07   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class SaleForMembersSet : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 479; } } //Shop_商品推荐管理_列表页
        protected new int Act_AddData = 480;    //Shop_商品推荐管理_添加数据
        protected new int Act_DelData = 481;    //Shop_商品推荐管理_删除数据

        private BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        private BLL.Shop.PromoteSales.GroupBuy groupMenage = new BLL.Shop.PromoteSales.GroupBuy();
        BLL.Shop.Products.ProductStationMode stationModeManage = new BLL.Shop.Products.ProductStationMode();


        protected void Page_Load(object sender, EventArgs e)
        {
            //绑定HiddenField中的数据
            if (hfSelectedData.Value != "")
            {
                BindAddProduct();
            }


            if (!Page.IsPostBack)
            {
                BindCategories();
                BindSupplier();

                if (SelectType == 0)
                {
                    this.litDesc.Text = "需要推荐的商品";
                }

                Add_AccountLimitQu.Visible = false;
                Add_TotalLimitQu.Visible = false;
                //Add_UploadImg.Visible = false;
            }
        }

        public long ProductId
        {
            get
            {
                long pid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["pid"]))
                {
                    pid = Globals.SafeLong(Request.Params["pid"], 0);
                }
                return pid;
            }
        }

        public int SelectType
        {
            get
            {
                int type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    type = Globals.SafeInt(Request.Params["type"], 0);

                }
                return type;
            }
        }

        private void BindExistReleatedProducts()
        {
            BLL.Shop.Products.RelatedProduct manage = new BLL.Shop.Products.RelatedProduct();
            List<Model.Shop.Products.RelatedProduct> list = manage.GetModelList(ProductId);
            if (list != null && list.Count > 0)
            {
                StringBuilder strExistInfo = new StringBuilder();
                list.ForEach(info =>
                {
                    strExistInfo.Append(info.RelatedId);
                    strExistInfo.Append(",");
                });
                this.hfSelectedData.Value = strExistInfo.ToString();
            }
        }

        private void BindCategories()
        {
            Maticsoft.BLL.Shop.Products.CategoryInfo bll = new BLL.Shop.Products.CategoryInfo();
            DataSet ds = bll.GetList("  Depth = 1 ");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.drpProductCategory.DataSource = ds;
                this.drpProductCategory.DataTextField = "Name";
                this.drpProductCategory.DataValueField = "CategoryId";
                this.drpProductCategory.DataBind();
            }
            this.drpProductCategory.Items.Insert(0, new ListItem("", string.Empty));
        }

        /// <summary>
        /// 供应商
        /// </summary>
        private void BindSupplier()
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo infoBll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = infoBll.GetList("  Status = 1 ");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlSupplier.DataSource = ds;
                this.ddlSupplier.DataTextField = "Name";
                this.ddlSupplier.DataValueField = "SupplierId";
                this.ddlSupplier.DataBind();
            }
            this.ddlSupplier.Items.Insert(0, new ListItem("", string.Empty));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(false);
        }
        #region BindData

        public void BindData(bool isClear)
        {
            int SelectValue = int.Parse(ddlOperation.SelectedValue);
            BindSearchProduct(isClear, SelectValue);
        }

        private void BindAddProduct()
        {

            //获取已选择数据
            int categoryId = Maticsoft.Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0);
            int supplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            string selectedskus = hfSelectedData.Value;
            if (anpAddedProducts.RecordCount == 0)
            {
                anpAddedProducts.RecordCount = anpAddedProducts.PageSize;
            }
            ////分页数据重置
            //string selectedData = ViewState["OldSelectedData"] as string;
            //if (selectedskus != selectedData)
            //{
            //    ViewState["OldSelectedData"] = selectedskus;
            //    anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            //}
            //如未选择数据, 执行清空操作
            if (string.IsNullOrWhiteSpace(selectedskus))
            {
                dlstAddedProducts.DataSource = null;
                dlstAddedProducts.DataBind();
                return;
            }

            //Check Data
            string[] skus = selectedskus.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (skus.Length < 0) return;

            int recordCount = skus.Length; //productManage.GetProductRecListCount(skus.Distinct().ToArray());

            //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
            //anpAddedProducts.PageSize, out recordCount);
            int SelectValue = int.Parse(this.ddlOperation.SelectedValue);
            int endIndex = anpAddedProducts.StartRecordIndex + anpAddedProducts.PageSize;
            List<Model.Shop.Products.ProductInfo> prodList = null;
            if (SelectValue == 2)
            {
                prodList = productManage.GetSelectedProducts(selectedskus.Substring(0, selectedskus.Length));
            }
            else
            {
                prodList = productManage.GetModelList2(string.Format(" x.productid in ({0})", selectedskus.Substring(0, selectedskus.Length).Trim(',')));
            }
            anpAddedProducts.RecordCount = recordCount;
            dlstAddedProducts.DataSource = prodList;
            dlstAddedProducts.DataBind();
        }

        private void BindSearchProduct(bool isClear, int SelectValue)
        {
            if (anpSearchProducts.RecordCount == 0)
            {
                anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            }
            DateTime _S_StartDate = DateTime.MinValue;
            DateTime _S_EndDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(S_StartDate.Text))
            {
                _S_StartDate = DateTime.Parse(S_StartDate.Text);
            }
            if (!string.IsNullOrEmpty(S_EndDate.Text))
            {
                _S_EndDate = DateTime.Parse(S_EndDate.Text);
            }
            int endIndex = anpSearchProducts.StartRecordIndex + anpSearchProducts.PageSize-1;
            int categoryId = Maticsoft.Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0);
            int supplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            string pName = txtProductName.Text.Trim();
            int totalCount = 0;
            if (SelectValue == 2)
            {
                totalCount = productManage.GetGiftDistinctListCount(categoryId, supplierId, pName, _S_StartDate, _S_EndDate);
            }
            else
            {
                totalCount = productManage.GetProductNoGroupBuyCount(categoryId, pName, supplierId);
            }

            List<Model.Shop.Products.ProductInfo> prodList = new List<ProductInfo>();
            if (totalCount > 0)
            {
                if (SelectValue == 2)
                {
                    prodList = productManage.GetGiftDistinctList(categoryId, supplierId, pName, anpSearchProducts.StartRecordIndex, endIndex, _S_StartDate, _S_EndDate);
                }
                else
                {
                    prodList = productManage.GetProductNoGroupBuyList(categoryId, supplierId, pName, anpSearchProducts.StartRecordIndex, endIndex);
                }

            }
            anpSearchProducts.RecordCount = totalCount;

            //设置全选数据 供JavaScript使用

            if (prodList != null && prodList.Count > 0)
            {
                StringBuilder tmpSkuIds = new StringBuilder();
                prodList.ForEach(info =>
                {
                    if (info.ProductId != ProductId)
                    {
                        tmpSkuIds.Append(info.ProductId);
                    }
                    tmpSkuIds.Append(",");
                });
                hfCurrentAllData.Value = tmpSkuIds.ToString();
            }
            else
            {
                hfCurrentAllData.Value = string.Empty;
            }

            dlstSearchProducts.DataSource = prodList;
            dlstSearchProducts.DataBind();
        }
        protected void dlstAddedProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
            {
                HtmlGenericControl lbtnDel = (HtmlGenericControl)e.Item.FindControl("lbtnDel");
                lbtnDel.Visible = false;
            }
        }
        protected void dlstSearchProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
            {
                HtmlGenericControl lbtnAdd = (HtmlGenericControl)e.Item.FindControl("lbtnAdd");
                lbtnAdd.Visible = false;
            }

        }
        #endregion BindData

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            BindData(false);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            int categoryId = Maticsoft.Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0);
            stationModeManage.DeleteByType(SelectType, categoryId);

            hfSelectedData.Value = "";
            BindData(true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int SelectValue = int.Parse(ddlOperation.SelectedValue);
            if (SelectValue == 2)
            {
                CloneData();
            }
            else if (SelectValue == 3)
            {
                InsertData();
            }

        }

        protected void dlstSearchProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectValue = int.Parse(ddlOperation.SelectedValue);
            Add_AccountLimitQu.Visible = SelectValue == 3 ? true : false;
            Add_TotalLimitQu.Visible = SelectValue == 3 ? true : false;
            //Add_UploadImg.Visible = SelectValue == 3 ? true : false;
            Clone_Date.Visible = SelectValue == 2 ? true : false;
            temp.Visible = SelectValue == 2 ? true : false;
            dlstSearchProducts.DataSource = "";
            dlstSearchProducts.DataBind();
            hfSelectedData.Value = "";
        }

        #region 克隆和新增豪礼大放送数据
        public void CloneData()
        {
            try
            {
                DateTime _StartDate = DateTime.Parse(StartDate.Text);
                DateTime _EndDate = DateTime.Parse(EndDate.Text);
                List<string> GroupBuyIdList = hfSelectedData.Value.Split(',').ToList<string>();
                if (GroupBuyIdList.Count > 1)
                {
                    int EffectRow = groupMenage.Insert2GroupBuy(GroupBuyIdList, _StartDate, _EndDate);
                    if (EffectRow == GroupBuyIdList.Count - 1)
                    {
                        hfSelectedData.Value = "";
                        dlstAddedProducts.DataSource = null;
                        dlstAddedProducts.DataBind();
                        MessageBox.ShowSuccessTip(this, "操作成功");
                    }
                }
            }
            catch (Exception)
            {
                hfSelectedData.Value = "";
                dlstAddedProducts.DataSource = null;
                dlstAddedProducts.DataBind();
                MessageBox.ShowFailTip(this, "操作失败");
            }
        }

        public void InsertData()
        {
            try
            {
                List<Maticsoft.Model.Shop.Products.ProductInfo> _ProductInfo = (List<Maticsoft.Model.Shop.Products.ProductInfo>)(dlstAddedProducts.DataSource);
                DateTime _StartDate = DateTime.Parse(StartDate.Text);
                DateTime _EndDate = DateTime.Parse(EndDate.Text);
                List<string> GroupBuyIdList = hfSelectedData.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                //decimal Price = 0;
                int TotalLimitQu = 0;
                int AccountLimitQu = 0;
                //string UploadImg = "";

                if (string.IsNullOrEmpty(this.TotalLimitQu.Text))
                {
                    MessageBox.ShowFailTip(this, "请填写最大限购值");
                    return;
                }
                if (string.IsNullOrEmpty(this.AccountLimitQu.Text))
                {
                    MessageBox.ShowFailTip(this, "请填写账户限购值");
                    return;
                }

                TotalLimitQu = int.Parse(this.TotalLimitQu.Text);
                AccountLimitQu = int.Parse(this.AccountLimitQu.Text);
                //UploadImg = this.hfFileUrl.Value;
                List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> GroupBuyList = new List<Model.Shop.PromoteSales.GroupBuy>();

                string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                string savePath = string.Format("/Upload/Shop/Images/Product/GroupBy/{0}/", DateTime.Now.ToString("yyyyMMdd"));

                //if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
                //{
                //    Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择要上传的图片！");
                //    return;
                //}
                
                //Maticsoft.BLL.SysManage.FileManager.MoveImageForFTP(tempFile + this.hfFileUrl.Value, savePath);
                if (GroupBuyIdList.Count >= 1)
                {
                    foreach (var item in GroupBuyIdList)
                    {
                        Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = new Model.Shop.PromoteSales.GroupBuy();
                        buyModel.StartDate = _StartDate;
                        buyModel.EndDate = _EndDate;
                        buyModel.MaxCount = TotalLimitQu;
                        buyModel.PromotionLimitQu = AccountLimitQu;
                        buyModel.PromotionType = 1;
                        buyModel.Status = 0;
                        buyModel.GroupBase = 0;
                        buyModel.Sequence = 1;
                        buyModel.BuyCount = 0;
                        buyModel.GroupBuyImage = _ProductInfo.Where(m => m.ProductId == long.Parse(item)).First().ImageUrl;//this.hfFileUrl.Value;
                        buyModel.ProductName = _ProductInfo.Where(m => m.ProductId == long.Parse(item)).First().ProductName;
                        buyModel.ProductId = int.Parse(item);
                        buyModel.ProductCategory = "/";
                        buyModel.Price = Convert.ToDecimal(_ProductInfo.Where(m => m.ProductId == long.Parse(item)).First().LowestSalePrice);
                        GroupBuyList.Add(buyModel);
                    }
                }

                int EffectRow = groupMenage.BulkInsert2ShopGroup(GroupBuyList);
                if (EffectRow == GroupBuyIdList.Count)
                {
                    hfSelectedData.Value = "";
                    dlstAddedProducts.DataSource = null;
                    dlstAddedProducts.DataBind();
                    MessageBox.ShowSuccessTip(this, "操作成功");
                }
            }
            catch (Exception)
            {
                hfSelectedData.Value = "";
                dlstAddedProducts.DataSource = null;
                dlstAddedProducts.DataBind();
                MessageBox.ShowFailTip(this, "操作失败");
            }
        }
        #endregion
    }
}