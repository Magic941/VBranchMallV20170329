using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using Maticsoft.Accounts;
using Maticsoft.BLL.Shop.Products;


namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class UpLoadCsvData : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 470; } } //Shop_批量上传页
        Maticsoft.BLL.Shop.Products.ProductInfo bll = new ProductInfo();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string Path = "/Upload/Shop/Files/";
            string FileName = uploadCsv.PostedFile.FileName;
            string ErrorMsg = "出现异常，请检查您的数据格式";
            uploadCsv.PostedFile.SaveAs(Server.MapPath(Path) + FileName);
            if(Csv(Path, FileName,out ErrorMsg))
            {
                Common.MessageBox.ShowSuccessTip(this,"批量插入成功");
            }
            else
            {
                 Common.MessageBox.ShowSuccessTip(this,"插入失败,信息:"+ErrorMsg+"提示：检查您填写数据的数据格式");
            }
        }
        public bool Csv(string Path,string FileName,out string  ErrorMsg)
        {
            OleDbConnection OleCon = new OleDbConnection();
            OleDbCommand OleCmd = new OleDbCommand();
            OleDbDataAdapter OleDa = new OleDbDataAdapter();
            DataSet CsvData = new DataSet();
            OleCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(Path)  + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            OleCon.Open();
            OleCmd.Connection = OleCon;
            OleCmd.CommandText = "select * From " + FileName;
            OleDa.SelectCommand = OleCmd;
            try
            {
                OleDa.Fill(CsvData, "Csv");
                List<Maticsoft.Model.Shop.Products.ProductInfo> list = bll.DataTableToList(CsvData.Tables[0]);
                foreach (Maticsoft.Model.Shop.Products.ProductInfo item in list)
                {
                    bll.Add(item);
                }
                ErrorMsg = "";
                return true;
            }
            catch(Exception ex)
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