using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;
using System.Data.OleDb;
using System.IO;
using System.Data.Odbc;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProductsBatchUpload_Old : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 470; } } //Shop_批量上传页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        string dropItem0Value = "请选择";
        public void BindData()
        {
            //bind分类
            //BindCategories();

            //bind商品类型
            BindProductTypes();

            //bind品牌
            BindBrands();

            //bind商家
            BindEnterprise();
        }

        #region bindData
        //private void BindCategories()
        //{
        //    Maticsoft.BLL.Shop.Products.CategoryInfo bll = new BLL.Shop.Products.CategoryInfo();
        //    DataSet ds = bll.GetList("  Depth = 1 ");

        //    if (!DataSetTools.DataSetIsNull(ds))
        //    {
        //        this.dropCategory.DataSource = ds;
        //        this.dropCategory.DataTextField = "Name";
        //        this.dropCategory.DataValueField = "CategoryId";
        //        this.dropCategory.DataBind();
        //    }
        //    this.dropCategory.Items.Insert(0, dropItem0Value);
        //}

        private void BindProductTypes()
        {
            Maticsoft.BLL.Shop.Products.ProductType bll = new BLL.Shop.Products.ProductType();
            DataSet ds = bll.GetList(string.Empty);

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropProductTypes.DataSource = ds;
                this.dropProductTypes.DataTextField = "TypeName";
                this.dropProductTypes.DataValueField = "TypeId";
                this.dropProductTypes.DataBind();
            }
            //this.dropProductTypes.Items.Insert(0, dropItem0Value);
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
            this.dropBrands.Items.Insert(0, dropItem0Value);
        }

        private void BindEnterprise()
        {
            Maticsoft.BLL.Ms.Enterprise bll = new BLL.Ms.Enterprise();
            DataSet ds = bll.GetList(string.Empty);

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropEnterprise.DataSource = ds;
                this.dropEnterprise.DataTextField = "Name";
                this.dropEnterprise.DataValueField = "EnterpriseID";
                this.dropEnterprise.DataBind();
            }
            this.dropEnterprise.Items.Insert(0, dropItem0Value);
        }
        #endregion

        protected void ButUpload_Click(object sender, EventArgs e)
        {
            
            #region
            try
            {
                if (FileUploadProducts.PostedFile != null && FileUploadProducts.PostedFile.ContentLength > 0)
                {
                    string ext = System.IO.Path.GetExtension(FileUploadProducts.PostedFile.FileName).ToLower();
                    if (".csv".IndexOf(ext) == -1)
                    {
                    }
                    if (!CheckBox1.Checked && !CheckBox2.Checked && !CheckBox3.Checked && !CheckBox4.Checked)
                    {
                        Maticsoft.Common.MessageBox.ShowFailTip(this, "请至少选择一种商品推荐方式！");
                        return;
                    }

                    int categId = Convert.ToInt32(ProductsBatchUploadDropList.SelectedValue == dropItem0Value ? "-1" : ProductsBatchUploadDropList.SelectedValue);
                    int typesId = Convert.ToInt32(dropProductTypes.SelectedValue == dropItem0Value ? "-1" : dropProductTypes.SelectedValue);
                    int brandId = Convert.ToInt32(dropBrands.SelectedValue == dropItem0Value ? "-1" : dropBrands.SelectedValue);
                    int enterId = Convert.ToInt32(dropEnterprise.SelectedValue == dropItem0Value ? "-1" : dropEnterprise.SelectedValue);

                    //.XLS
                    //string tabName = FileUploadProducts.FileName;
                    //string strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", FileUploadProducts.PostedFile.FileName);
                    //OleDbConnection conn = new OleDbConnection(strConn);
                    //OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + tabName+ "$]", strConn);
                    //DataSet ds = new DataSet();
                    //oada.Fill(ds);
                    //conn.Close();

                    //.CSV
                    //OdbcConnection
                    //string strConn = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=";
                    //strConn += FileUploadProducts.PostedFile.FileName.Replace(FileUploadProducts.FileName, string.Empty);//路径
                    //strConn += ";Extensions=asc,csv,tab,txt;";
                    //OdbcConnection conn = new OdbcConnection(strConn);

                    //OleDbConnection
                    string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
                    strConn += FileUploadProducts.PostedFile.FileName.Replace(FileUploadProducts.FileName, string.Empty);//路径
                    strConn += ";Extended Properties=\"text;HDR=Yes;FMT=Delimited\"";

                    OleDbConnection conn = new OleDbConnection(strConn);

                    DataSet ds = new DataSet();
                    string strSQL = "select * from " + FileUploadProducts.FileName;//文件名
                    OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
                    da.Fill(ds);
                    conn.Close();

                    int count = 0;
                    Maticsoft.Model.Shop.Products.ProductInfo model = new Model.Shop.Products.ProductInfo();
                    Maticsoft.BLL.Shop.Products.ProductInfo bll = new BLL.Shop.Products.ProductInfo();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        model.CategoryId = categId;
                        model.TypeId = typesId;
                        model.BrandId = brandId;
                        model.SupplierId = enterId;
                        model.SaleStatus = CheckBoxSaleStatus.Checked ? (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale : (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock;

                        model.ProductName = ds.Tables[0].Rows[i]["商品名称"].ToString();
                        model.Description = ds.Tables[0].Rows[i]["简单介绍"].ToString();
                        model.AddedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["添加时间"]);
                        model.ImageUrl = ds.Tables[0].Rows[i]["清晰图"].ToString();
                        model.MarketPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["市场价"].ToString());
                        model.LineId = 1;

                        int retProductId = Convert.ToInt32(bll.Add(model));
                        if (retProductId != 0)
                        {
                            Maticsoft.Model.Shop.Products.ProductStationMode psModel = new Model.Shop.Products.ProductStationMode();
                            Maticsoft.BLL.Shop.Products.ProductStationMode psBll = new BLL.Shop.Products.ProductStationMode();

                            int psCount = 0;
                            for (int j = 1; j <= 4; j++)
                            {
                                CheckBox checkBox = (CheckBox)this.divCheckBox.FindControl("CheckBox" + j);
                                if (checkBox.Checked)
                                {
                                    psModel.ProductId = retProductId;
                                    psModel.DisplaySequence = psBll.GetRecordCount(" ProductId = " + retProductId) + 1;
                                    psModel.Type = Convert.ToInt32(checkBox.SkinID);
                                    if (psBll.Add(psModel) != 0)
                                    {
                                        psCount = 1;
                                    }
                                }
                            }
                            if (psCount == 1)
                            {
                                psCount = 0;
                                count++;
                            }
                        }
                    }

                    if (count > 0)
                    {
                        Maticsoft.Common.MessageBox.ShowAndBack(this, "已成功导入 " + count.ToString() + " 个商品！");
                    }
                    else
                    {
                        Maticsoft.Common.MessageBox.ShowFailTip(this, "导入失败！");
                    }
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择上传文件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, ex.Message);
            }
            #endregion
        }
    }
}