using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maticsoft.BLL.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class DownCsvPattern : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 470; } } //Shop_批量上传页
        Maticsoft.BLL.Shop.Products.ProductInfo bll=new ProductInfo();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (!DataToCSV("Product"))
            {
                Common.MessageBox.ShowFailTip(this,"");
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
           Response.Redirect("UpLoadCsvData.aspx");
        }
        public bool DataToCSV(string fileName)
        {
            try
            {
                string data = ExportCSV();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Expires = 0;
                HttpContext.Current.Response.BufferOutput = true;
                HttpContext.Current.Response.Charset = "GB2312";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                HttpContext.Current.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.csv", System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)));
                HttpContext.Current.Response.ContentType = "text/h323;charset=gbk";
                HttpContext.Current.Response.Write(data);
                HttpContext.Current.Response.End();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private string ExportCSV()
        {
            StringBuilder strbData = new StringBuilder();
            DataTable dt = bll.GetTableSchema().Tables[0];
            if (dt != null)
            {
                //添加列名
                foreach (DataRow dr in dt.Rows)
                {
                    strbData.Append(dr["column_name"].ToString() + ",");
               
                }
                strbData.Append("\n");
                return strbData.ToString();
            }
            return "";
        }
    }
}