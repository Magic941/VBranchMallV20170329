using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Json;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProductsInStock : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 482; } } //Shop_商品管理_列表页
        protected new int Act_UpdateData = 483;    //Shop_商品管理_编辑数据
        protected new int Act_DelData = 484;    //Shop_商品管理_删除数据
        private BLL.Shop.Products.ProductInfo bll = new BLL.Shop.Products.ProductInfo();
        private BLL.Shop.Products.ProductCategories productCategory = new BLL.Shop.Products.ProductCategories();
        private BLL.Shop.Products.CategoryInfo manage = new BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierBll = new BLL.Shop.Supplier.SupplierInfo();
        BLL.Shop.Products.SKUInfo SUKManage = new BLL.Shop.Products.SKUInfo();

        public string strTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }

            if (!Page.IsPostBack)
            {

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }

                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
                if (!string.IsNullOrWhiteSpace(Request.Params["SaleStatus"]))
                {
                    int status = Common.Globals.SafeInt(Request.Params["SaleStatus"], -1);
                    hidstatus.Value = status.ToString();
                }
                BindCategories();
                BindSupplier();
            }
        }

        #region AjaxCallback

        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateProductName":

                    writeText = UpdateProductName();
                    break;
                case "UpdateStockNum":
                    writeText = UpdateStockNum();
                    break;
                case "UpdateMarketPrice":
                    writeText = UpdateMarketPrice();
                    break;
                case "UpdateLowestSalePrice":
                    writeText = UpdateLowestSalePrice();
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string UpdateProductName()
        {
            JsonObject json = new JsonObject();
            long productId = Common.Globals.SafeLong(this.Request.Form["ProductId"], 0);
            string productName = this.Request.Params["UpdateValue"];
            if (string.IsNullOrWhiteSpace(productName))
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                if (bll.UpdateProductName(productId, productName))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }


        private string UpdateStockNum()
        {
            JsonObject json = new JsonObject();
            long productId = Common.Globals.SafeLong(this.Request.Form["ProductId"], 0);
            int StockNum = Common.Globals.SafeInt(Request.Params["UpdateValue"], 0);
            if (StockNum == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                //if (bll.UpdateStockNum(productId, StockNum))
                //{
                //    json.Put("STATUS", "SUCCESS");
                //}
                //else
                //{
                //    json.Put("STATUS", "FAILED");
                //}
            }
            return json.ToString();
        }


        private string UpdateMarketPrice()
        {
            JsonObject json = new JsonObject();
            long productId = Common.Globals.SafeLong(this.Request.Form["ProductId"], 0);
            decimal price = Common.Globals.SafeDecimal(Request.Params["UpdateValue"], 0);
            if (price == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                if (bll.UpdateMarketPrice(productId, price))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }

        private string UpdateLowestSalePrice()
        {
            JsonObject json = new JsonObject();
            long productId = Common.Globals.SafeLong(this.Request.Form["ProductId"], 0);
            decimal price = Common.Globals.SafeDecimal(Request.Params["UpdateValue"], 0);
            if (price == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                if (bll.UpdateLowestSalePrice(productId, price))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }
        #endregion AjaxCallback

        protected int SaleStatus
        {
            get
            {
                int status = -1;
                if (!string.IsNullOrWhiteSpace(Request.Params["SaleStatus"]))
                {
                    status = Common.Globals.SafeInt(Request.Params["SaleStatus"], -1);
                }
                return status;
            }
        }

        protected int SupplierId
        {
            get
            {
                int supplierId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["sid"]))
                {
                    supplierId = Common.Globals.SafeInt(Request.Params["sid"], 0);
                }
                return supplierId;
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            //this.aspnetpager.CurrentPageIndex = this.aspnetpager.CurrentPageIndex + 1;
            BindData();
        }


        public void BindData()
        {
            int total = 0;
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[10].Visible = false;
            }
            switch (SaleStatus)
            {
                //出售中
                case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale:
                    strTitle = "您可以对出售中的商品进行编辑、删除和下架以及查询库存低于警戒库存的商品操作";
                    break;
                //未审核
                case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.UnCheck:
                    btnCheck.Visible = true;
                    strTitle = "您可以对未审核中的商品进行编辑、删除和审核操作";
                    this.btnInverseApprove.Visible = false;
                    btnInverseApprove2.Visible = false;
                    break;
                case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock:
                    strTitle = "您可以对仓库中的商品进行删除和上架功能";
                    this.btnInverseApprove.Text = "批量上架";
                    btnInverseApprove2.Text = "批量上架";
                    break;
            }

            Model.Shop.Products.ProductInfo model = new Model.Shop.Products.ProductInfo();
            model.SaleStatus = SaleStatus;
            if (!string.IsNullOrWhiteSpace(txtKeyword.Text.TrimEnd()))
            {
                model.ProductName = InjectionFilter.SqlFilter(this.txtKeyword.Text);
            }
            if (!string.IsNullOrWhiteSpace(drpProductCategory.SelectedValue))
            {
                model.CategoryId = Globals.SafeInt(this.drpProductCategory.SelectedValue, 0);
            }
            if (!string.IsNullOrWhiteSpace(this.txtProductNum.Text))
            {
                model.ProductCode = InjectionFilter.SqlFilter(this.txtProductNum.Text);
            }
            if (!string.IsNullOrWhiteSpace(this.txtSKU.Text))
            {
                model.ShortDescription = InjectionFilter.SqlFilter(this.txtSKU.Text);
            }

            model.SupplierId = Common.Globals.SafeInt(this.ddlSupplier.SelectedValue, 0);
            if (model.SupplierId == 0)
            {
                model.SupplierId = SupplierId;
            }
            int startIndex = aspnetpager.CurrentPageIndex > 1 ? (aspnetpager.CurrentPageIndex - 1) * aspnetpager.PageSize + 1 : 0;



            var x = bll.GetProdListByPage(model, startIndex, aspnetpager.PageSize * aspnetpager.CurrentPageIndex, out total);
            aspnetpager.RecordCount = total;
            gridView.DataSource = x;
            gridView.DataBind();

            gridView.Columns[10].Visible = Maticsoft.Components.MvcApplication.HasArea(AreaRoute.Shop);
        }

        #region
        //public void BindData()
        //{
        //    if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
        //    {
        //       gridView.Columns[10].Visible = false;
        //    }
        //    switch (SaleStatus)
        //    {
        //            //出售中
        //        case  (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale:
        //            strTitle = "您可以对出售中的商品进行编辑、删除和下架以及查询库存低于警戒库存的商品操作";
        //            break;
        //            //未审核
        //        case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.UnCheck:
        //            btnCheck.Visible = true;
        //            strTitle = "您可以对未审核中的商品进行编辑、删除和审核操作";
        //            this.btnInverseApprove.Visible = false;
        //            btnInverseApprove2.Visible = false;
        //            break;
        //        case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock:
        //               strTitle = "您可以对仓库中的商品进行删除和上架功能";
        //        this.btnInverseApprove.Text = "批量上架";
        //        btnInverseApprove2.Text = "批量上架";
        //            break;
        //    }

        //    Model.Shop.Products.ProductInfo model = new Model.Shop.Products.ProductInfo();
        //    model.SaleStatus = SaleStatus;
        //    if (!string.IsNullOrWhiteSpace(txtKeyword.Text.TrimEnd()))
        //    {
        //        model.ProductName = InjectionFilter.SqlFilter(this.txtKeyword.Text);
        //    }
        //    if (!string.IsNullOrWhiteSpace(drpProductCategory.SelectedValue))
        //    {
        //        model.CategoryId = Globals.SafeInt(this.drpProductCategory.SelectedValue,0);
        //    }
        //    if (!string.IsNullOrWhiteSpace(this.txtProductNum.Text))
        //    {
        //        model.ProductCode = InjectionFilter.SqlFilter(this.txtProductNum.Text);
        //    }
        //    model.SupplierId = Common.Globals.SafeInt(this.ddlSupplier.SelectedValue, 0);
        //    if (model.SupplierId == 0)
        //    {
        //        model.SupplierId = SupplierId;
        //    }
        //    DataSet ds = new DataSet();
        //    ds = bll.GetProductInfo(model, this.txtSKU.Text.Trim(), chkAlert.Checked);

        //    //ds.Tables[0].Columns.Add("CostPrice");
        //    //ds.Tables[0].Columns.Add("SalePrice");
        //    //ds.Tables[0].Columns.Add("IsMany");

        //    //foreach (DataRow dr in ds.Tables[0].Rows)
        //    //{
        //    //    DataSet SUKsCostPrice = SUKManage.GetList(" ProductId= " + dr["ProductId"].ToString() + " order by CostPrice ");
        //    //    DataSet SUKsSalePrice = SUKManage.GetList(" ProductId= " + dr["ProductId"].ToString() + " order by SalePrice ");
        //    //    if (SUKsCostPrice.Tables[0].Rows.Count > 0)
        //    //    {
        //    //        dr["CostPrice"] = SUKsCostPrice.Tables[0].Rows[0]["CostPrice"];
        //    //        dr["SalePrice"] = SUKsSalePrice.Tables[0].Rows[0]["SalePrice"];
        //    //    }

        //    //    if (SUKsCostPrice.Tables[0].Rows.Count > 1)
        //    //    {
        //    //        dr["IsMany"] = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        dr["IsMany"] = false;
        //    //    }
        //    //    dr.AcceptChanges();
        //    //}
        //    //ds.Tables[0].AcceptChanges();
        //    gridView.DataSetSource = ds;

        //    gridView.Columns[10].Visible =  Maticsoft.Components.MvcApplication.HasArea(AreaRoute.Shop);
        //}
        #endregion

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
            this.drpProductCategory.Items.Insert(0, new ListItem(string.Empty, string.Empty));
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

            this.ddlSupplier.Items.Insert(0, new ListItem("平　台", "-1"));
            this.ddlSupplier.Items.Insert(0, new ListItem("全　部", string.Empty));
            this.ddlSupplier.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            if (SupplierId != 0)
            {
                ddlSupplier.SelectedValue = SupplierId.ToString();
            }
            else
            {
                ddlSupplier.SelectedIndex = 0;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist(Maticsoft.Model.Shop.Products.ProductSaleStatus.Deleted);
            if (!string.IsNullOrWhiteSpace(idlist))
            {

                if (idlist.Trim().Length == 0) return;
                bll.UpdateList(idlist, Maticsoft.Model.Shop.Products.ProductSaleStatus.Deleted);
                //上架商品删除索引
                if (SaleStatus == 1)
                {
                    Array productIdList = idlist.Split(',');
                    foreach (string productid in productIdList)
                    {
                        Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Del(Convert.ToInt64(productid));
                    }
                }

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            gridView.OnBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litProductCate = (Literal)e.Row.FindControl("litProductCate");
                object productId = DataBinder.Eval(e.Row.DataItem, "ProductId");
                if (productId != null)
                {
                    litProductCate.Text = ProductCategories(Common.Globals.SafeLong(productId.ToString(), 0));
                }
            }
        }

        private string GetSelIDlist(Maticsoft.Model.Shop.Products.ProductSaleStatus newstatus)
        {

            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                TextBox txtSaleStatus = (TextBox)gridView.Rows[i].FindControl("txtSaleStatus");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";

                        Maticsoft.BLL.Shop.Products.ProductManage.InsertOrUpdateIndex(int.Parse(txtSaleStatus.Text.Trim()), (int)newstatus, long.Parse(gridView.DataKeys[i].Value.ToString()));
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }
        private string GetSelIDlist()
        {

            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #region 批量上 下架

        /// <summary>
        /// 批量上 下架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {

            Maticsoft.Model.Shop.Products.ProductSaleStatus status;
            if (SaleStatus == (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale)
                status = Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock;
            else
                status = Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale;

            string idlist = GetSelIDlist(status);
            if (idlist.Trim().Length == 0) return;

            Array list = idlist.Split(',');
            foreach (string item in list)
            {
                BLL.Shop.Products.ProductInfo manage = new BLL.Shop.Products.ProductInfo();
                BLL.Shop.Products.ProductCategories productCate = new BLL.Shop.Products.ProductCategories();
                List<Model.Shop.Products.ProductInfo> info = manage.GetModelList(" SupplierId in(select SupplierId from Shop_Suppliers) and ProductId=" + item);
                string cid = ProductCategories(Common.Globals.SafeLong(item, 0));
                if (info.Count <= 0)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "没有指定商家无法上架！");
                    return;
                }
                if (info[0].LowestSalePrice <= 0)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "价格为0无法上架！");
                    return;
                }
                if (string.IsNullOrEmpty(cid))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "没有分类无法上架！");
                    return;
                }

            }
            bll.UpdateList(idlist, status);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            gridView.OnBind();
        }


        #endregion 批量上 下架

        /// <summary>
        /// 批量审核操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist(Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale);
            if (idlist.Trim().Length == 0) return;
            bll.UpdateList(idlist, Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale);

            //未审核(-1)、下架(0)商品加入索引
            if (SaleStatus == 0 || SaleStatus == -1)
            {
                Array productIdList = idlist.Split(',');
                foreach (string productid in productIdList)
                {
                    Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Add(Convert.ToInt64(productid));
                }
            }


            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            gridView.OnBind();
        }

        protected void btnFresh_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        /// <summary>
        /// 获取商品所在分类信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private string ProductCategories(long productId)
        {
            List<Model.Shop.Products.ProductCategories> list = productCategory.GetModelList(productId);
            StringBuilder strName = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (Model.Shop.Products.ProductCategories productCategoriese in list)
                {
                    strName.Append(manage.GetFullNameByCache(productCategoriese.CategoryId));
                    strName.Append("</br>");
                }
            }
            return strName.ToString();
        }

        protected long StockNum(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    long productId = Common.Globals.SafeLong(obj.ToString(), 0);
                    return bll.StockNum(productId);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        protected string GetSupplier(object obj)
        {
            int supplierId = Common.Globals.SafeInt(obj, 0);
            Maticsoft.Model.Shop.Supplier.SupplierInfo infoModel = supplierBll.GetModel(supplierId);
            return infoModel == null ? "" : infoModel.Name;
        }

        /// <summary>
        /// 批量更改刷新时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateDate_Click(object sender, EventArgs e)
        {

            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            Array list = idlist.Split(',');

            string datetime = DateTime.Now.ToString();

            bll.UpdateListDate(idlist, datetime);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            gridView.OnBind();
        }

        /// <summary>
        /// 是否是进口商品
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetImportPro(object target)
        {
            string str = string.Empty;
            int productid = Common.Globals.SafeInt(target, 0);
            Maticsoft.Model.Shop.Products.ProductInfo infoModel = bll.GetModelNew(productid);
            if (infoModel.ImportPro == 1)
            {
                str = "是";
            }
            else
            {
                str = "否";
            }
            return str;
        }

        /// <summary>
        /// 进口商品板块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateImportPro_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            Array list = idlist.Split(',');

            BLL.Shop.Products.ProductInfo manage = new BLL.Shop.Products.ProductInfo();
            List<Model.Shop.Products.ProductInfo> info = manage.GetModelList(" ProductId in (" + idlist + ")");

            if (info[0].ImportPro >= 1)
            {
                for (int i = 0; i < info[0].ImportPro; i++)
                {
                    int ImportPro = 1; //不是进口商品

                    if (bll.UpdateListImportPro(idlist, ImportPro))
                    {
                        Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                        gridView.OnBind();
                    }
                }
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "更改商品为进口商品失败,请稍后再试!");
                return;
            }
        }

        /// <summary>
        /// 取消进口商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteImportPro_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            Array list = idlist.Split(',');

            BLL.Shop.Products.ProductInfo manage = new BLL.Shop.Products.ProductInfo();
            List<Model.Shop.Products.ProductInfo> info = manage.GetModelList(" ProductId in (" + idlist + ")");

            if (info[0].ImportPro >= 1)
            {
                for (int i = 0; i < info[0].ImportPro; i++)
                {
                    int ImportPro = 0; //不是进口商品

                    if (bll.UpdateListImportPro(idlist, ImportPro))
                    {
                        Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                        gridView.OnBind();
                    }
                }
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "商品本身不属于进口商品，无法取消进口商品状态!");
                return;
            }
        }
    }
}