using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Common;
using System.Text;
using System.IO;
using Maticsoft.Json;
using NPOI.HSSF.UserModel;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProductsExport :PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 469; } } //Shop_导出商品页
        Maticsoft.BLL.Shop.Products.ProductInfo bll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private BLL.Shop.Products.ProductCategories productCategory = new BLL.Shop.Products.ProductCategories();
        private BLL.Shop.Products.CategoryInfo manage = new BLL.Shop.Products.CategoryInfo();
        //private string ExFiled = "ExtendCategoryPath,MaxQuantity,MinQuantity,LineId,Meta_Title,Meta_Description,Meta_Keywords,LineId,PenetrationStatus";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // BindProductField();
               // BindCategories();
                BindBrands();
            }
        }
        private void BindBrands()
        {
            Maticsoft.BLL.Shop.Products.BrandInfo bll=new BrandInfo();
            DataSet ds = bll.GetAllList();

            if (!DataSetTools.DataSetIsNull(ds))
            {
                drpProductBrand.DataSource = ds;
                drpProductBrand.DataTextField = "BrandName";
                drpProductBrand.DataValueField = "BrandId";
                drpProductBrand.DataBind();
            }

            this.drpProductBrand.Items.Insert(0, new ListItem("请选择", string.Empty));
        }
 
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GetSelIDlist()))
            {
                string idlist = GetSelIDlist();
                if (idlist.Trim().Length == 0) return;
                bll.UpdateList(idlist, Maticsoft.Model.Shop.Products.ProductSaleStatus.Deleted);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
            else
            {
                gridView.OnBind(); return;
            }
        }
        #region DataBind
        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            int IntCate = Common.Globals.SafeInt(CategoriesDropList1.SelectedValue, 0);
            int IntBrand = Common.Globals.SafeInt(drpProductBrand.SelectedValue, 0);
            int IntRegion = Common.Globals.SafeInt(hfSelectedNode.Value, 0);
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" ProductName like '%{0}%'", txtKeyword.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" AddedDate>='" + txtBeginTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  AddedDate<='" + txtEndTime.Text.Trim() + "' ");
            }
            if (IntCate > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(
                   "  EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =Shop_Products.ProductId  ");
                strWhere.AppendFormat(
              "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
              IntCate);
                strWhere.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", IntCate);

            }
            if (IntRegion > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  RegionId=" + IntRegion + " ");
            }

            if (IntBrand > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  BrandId=" + IntBrand + " ");
            }

            ds = bll.GetList(strWhere.ToString());
            gridView.DataSetSource = ds;
        }
        
        #endregion
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HidSelectValue.Value = string.IsNullOrEmpty(HidSelectValue.Value)
                                       ? HidSelectValue.Value.TrimEnd(',').TrimStart(',')
                                       : "" + GetSelIDlist();
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");

                Literal litProductCate = (Literal)e.Row.FindControl("litProductCate");
                object productId = DataBinder.Eval(e.Row.DataItem, "ProductId");
                if (productId != null)
                {
                    litProductCate.Text = ProductCategories(Common.Globals.SafeLong(productId.ToString(), 0));
                }
            }
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





        //public void BindProductField()
        //{
           // StringBuilder strbData = new StringBuilder();
           //// DataTable dt = bll.GetTableSchemaEx().Tables[0];
           // DataTable dt = bll.GetTableHead().Tables[0];
           //// string text;
           //// StringBuilder  value;
           // ListItem item1 = new ListItem("类别ID", "CategoryId");
           // ListItem item2 = new ListItem();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (!ExFiled.Contains(dr["column"].ToString()))
            //    {
            //        //text = dr["column"].ToString() + "[" + dr["column_desc"].ToString() + "]";
            //        text = "[" + dr["column_desc"].ToString() + "]";
            //        value = new StringBuilder();
            //        value.Append(dr["column"].ToString());
            //        value.Append(" As ");
            //        value.Append(GetTrueValue(dr["column_desc"].ToString()));
            //        ListItem item = new ListItem(text, value.ToString());
            //        item.Attributes[dr["column"].ToString()] = dr["column_desc"].ToString();
            //        chkTableField.Items.Add(item);
            //    }
            //    text = "";
            //    value=new StringBuilder();

            //}
            //chkTableField.DataBind();
        //}
        

        public string GetTrueValue(string str)
        {
            if (str.Contains(" "))
            {
                return str.Split(' ')[0];
            }
            return str;
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HidSelectValue.Value))
            {
                Common.MessageBox.ShowFailTip(this,"请选择要导出的数据");
                return;
            }
            StringBuilder sbSql = new StringBuilder();
            foreach (ListItem listItem in chkTableField.Items)
            {
                if (listItem.Selected)
                {
                    sbSql.Append(listItem.Value);
                    sbSql.Append(" As "+listItem.Text);
                    sbSql.Append(",");
                }
            }
            if (sbSql.Length<=0)
            {
                Common.MessageBox.ShowFailTip(this, "请选择要导出的字段");
                return;
            }

          
            DataSet ds=new DataSet();
            string Ids = !string.IsNullOrEmpty(HidSelectValue.Value) ? HidSelectValue.Value.TrimEnd(',').TrimStart(',') : "";
            ds= bll.GetList(Ids, sbSql.ToString().TrimEnd(','));
            DataSetToExcel(ds);

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

        public string StrCate(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    BLL.Shop.Products.CategoryInfo manage = new BLL.Shop.Products.CategoryInfo();
                    return manage.GetFullNameByCache(int.Parse(obj.ToString()));
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DS"></param>
        public void DataSetToExcel(DataSet DS)
        {

            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("商品表");
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
            foreach (DataColumn column in DS.Tables[0].Columns)
            {
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }
            int rowIndex = 1;
            foreach (DataRow row in DS.Tables[0].Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in DS.Tables[0].Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                dataRow = null;
                rowIndex++;
            }
            Response.Clear();
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            headerRow = null;
            sheet = null;
            workbook = null;
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=Product.xls"));
            Response.BinaryWrite(ms.ToArray());
            ms.Close();
            ms.Dispose();
            Response.End();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnFresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductsExport.aspx");

        }

      
    }
}