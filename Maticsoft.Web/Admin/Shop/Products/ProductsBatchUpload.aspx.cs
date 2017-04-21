using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Common;
using System.Data.OleDb;
using System.IO;
using System.Data.Odbc;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProductsBatchUpload : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 470; } } //Shop_批量上传页
        Maticsoft.BLL.Shop.Products.ProductInfo bll = new ProductInfo();
        protected string UploadPath = "/Upload/Shop/Files/";
        //protected string ExField = "ExtendCategoryPath,MaxQuantity,MinQuantity,LineId,Meta_Title,Meta_Description,Meta_Keywords,LineId,PenetrationStatus";//排除在外的字段
        protected string ExField = "";

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (!DataToCSV("Product"))
            {
                Common.MessageBox.ShowFailTip(this, "下载失败，请重试");
            }
        }
        /// <summary>
        /// 生成CSV文件
        /// </summary>
        /// <param name="fileName">文件的名称</param>
        /// <returns></returns>
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
        /// <summary>
        /// 根据dataSet生成相应CSV构架
        /// </summary>
        /// <returns></returns>
        private string ExportCSV()
        {
            StringBuilder strbData = new StringBuilder();
            DataTable dt = bll.GetTableSchema().Tables[0];
            if (dt != null)
            {
                //添加列名
                foreach (DataRow dr in dt.Rows)
                {
                    if (ExField != null && !ExField.Contains(dr["column_name"].ToString()))
                    {
                        strbData.Append(dr["column_name"].ToString() + ",");
                    }
                }
                strbData.Append("\n");
                return strbData.ToString();
            }
            return "";
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {

            string FileName = uploadCsv.PostedFile.FileName;
            string ErrorMsg = "出现异常，请检查您的数据格式";
            int Count = 0;
            if (!uploadCsv.HasFile)
            {
                Common.MessageBox.ShowSuccessTip(this, "请上传文件");
                return;
            }
            uploadCsv.PostedFile.SaveAs(Server.MapPath(UploadPath) + FileName);
            if (Csv(UploadPath, FileName, out ErrorMsg, ref Count))
            {
                Common.MessageBox.ShowSuccessTip(this, "成功插入" + Count + "条数据");
            }
            else
            {
                Common.MessageBox.ShowSuccessTip(this, "插入失败,信息:" + ErrorMsg + "提示：检查您填写数据的数据格式");
            }
        }
        /// <summary>
        /// 读取文件，导入DataSet
        /// </summary>
        /// <param name="Path">csv的路径</param>
        /// <param name="FileName">文件的名称</param>
        /// <param name="ErrorMsg">错误信息</param>
        /// <returns></returns>
        public bool Csv(string Path, string FileName, out string ErrorMsg, ref int Count)
        {
            OleDbConnection OleCon = new OleDbConnection();
            OleDbCommand OleCmd = new OleDbCommand();
            OleDbDataAdapter OleDa = new OleDbDataAdapter();
            DataSet CsvData = new DataSet();
            OleCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(Path) + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            OleCon.Open();
            OleCmd.Connection = OleCon;
            OleCmd.CommandText = "  select  *  from  [" + FileName + "]  ";
            OleDa.SelectCommand = OleCmd;
            try
            {
                OleDa.Fill(CsvData);
                List<Maticsoft.Model.Shop.Products.ProductInfo> list = bll.DataTableToList(CsvData.Tables[0]);
                foreach (Maticsoft.Model.Shop.Products.ProductInfo item in list)
                {
                    bll.Add(item);
                    Count++;
                }
                ErrorMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                return false;
            }
            finally
            {
                OleCon.Close();
                OleCmd.Dispose();
                OleDa.Dispose();
                OleCon.Dispose();
            }
        }

    }
}