using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;
using System.Text;
using System.IO;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProductsExport_Old : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 469; } } //Shop_导出商品页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            //bind分类
            BindCategories();

            //bind品牌
            BindBrands();
        }

        #region bindData
        private void BindCategories()
        {
            Maticsoft.BLL.Shop.Products.CategoryInfo bll = new BLL.Shop.Products.CategoryInfo();
            DataSet ds = bll.GetList("  Depth = 1 ");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropCategory.DataSource = ds;
                this.dropCategory.DataTextField = "Name";
                this.dropCategory.DataValueField = "CategoryId";
                this.dropCategory.DataBind();
            }
            this.dropCategory.Items.Insert(0, string.Empty);
        }


        private void BindBrands()
        {
            Maticsoft.BLL.Shop.Products.BrandInfo bll = new BLL.Shop.Products.BrandInfo();
            DataSet ds = bll.GetList(string.Empty);

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropBrands.DataSource = ds;
                this.dropBrands.DataTextField = "BrandName";
                this.dropBrands.DataValueField = "BrandId";
                this.dropBrands.DataBind();
            }
            this.dropBrands.Items.Insert(0, string.Empty);
        }
        #endregion

        protected void ButExport_Click(object sender, EventArgs e)
        {
            try
            {
                int saleStatus = 1;
                string productName = string.Empty;
                int categoryId = -1;
                string sKU = string.Empty;
                int brandId = -1;

                //SaleStatus
                string DsaleStatus = dropSaleStatus.SelectedValue;
                if (DsaleStatus == "1")
                    saleStatus = (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale;
                else
                    saleStatus = (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock;

                //ProductName
                string TproductName = tbProdName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(TproductName))
                {
                    productName = TproductName;
                }

                //CategoryId
                categoryId = Convert.ToInt32(dropCategory.SelectedValue == string.Empty ? "-1" : dropCategory.SelectedValue);

                //SKU
                string Tsku = tbSKU.Text.Trim();
                if (!string.IsNullOrWhiteSpace(Tsku))
                {
                    sKU = Tsku;
                }

                //BrandId
                brandId = Convert.ToInt32(dropBrands.SelectedValue == string.Empty ? "-1" : dropBrands.SelectedValue);

                Maticsoft.BLL.Shop.Products.ProductInfo bll = new BLL.Shop.Products.ProductInfo();
                DataSet ds = bll.GetListByExport(saleStatus, productName, categoryId, sKU, brandId); 
                if (!DataSetTools.DataSetIsNull(ds))
                {
                    DataTable dt = ds.Tables[0];
                    StringWriter swCSV = new StringWriter();
                    swCSV.WriteLine("商品名称,简单介绍,添加时间,清晰图,市场价");
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder strText = new StringBuilder();
                        strText = AppendCSVFields(strText, dr["ProductName"].ToString());
                        strText = AppendCSVFields(strText, dr["Description"].ToString());
                        strText = AppendCSVFields(strText, dr["AddedDate"].ToString());
                        strText = AppendCSVFields(strText, dr["ImageUrl"].ToString());
                        strText = AppendCSVFields(strText, dr["MarketPrice"].ToString());

                        //去掉尾部的逗号
                        strText.Remove(strText.Length - 1, 1);
                        //写datatable的一行
                        swCSV.WriteLine(strText.ToString());
                    }

                    //下载文件
                    DownloadFile(Response, swCSV.GetStringBuilder(), "MaticsoftProducts");
                    swCSV.Close();
                    Response.End();
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "根据条件查询到0条记录！");
                }
            }
            catch (Exception ex)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, ex.Message);
            }
        }

        private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
        {
            return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
        }

        /// <summary>
        /// 弹出下载框
        /// </summary>
        /// <param name="argResp">弹出页面</param>
        /// <param name="argFileStream">文件流</param>
        /// <param name="strFileName">文件名</param>
        public void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
        {
            try
            {
                string strResHeader = string.Empty;
                if (!string.IsNullOrWhiteSpace(strFileName))
                    strResHeader = "inline; filename=" + strFileName + ".csv"; //inline 在线打开
                else
                    strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv"; //attachment 以附件下载

                argResp.AppendHeader("Content-Disposition", strResHeader);
                argResp.ContentType = "application/ms-excel";
                argResp.ContentEncoding = Encoding.GetEncoding("GB2312");
                argResp.Write(argFileStream);
            }
            catch (Exception ex)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(Page, ex.Message);
            }
        }
    }
}