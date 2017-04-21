using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Coupon;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class ImportCoupon : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new CouponRule();
        protected string UploadPath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Coupon.CouponRule ruleModel = new Model.Shop.Coupon.CouponRule();
            string name = txtName.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Common.MessageBox.ShowFailTip(this, "请填写优惠券活动名称");
                return;
            }
            decimal limitPrice = Common.Globals.SafeDecimal(txtLimitPrice.Text, -1);
            bool IsLimitPrice = chkLimitPrice.Checked;
            if (IsLimitPrice && limitPrice == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写最低消费金额");
                return;
            }
            decimal price = Common.Globals.SafeDecimal(txtPrice.Text, -1);
            bool IsPrice = chkPrice.Checked;
            if (IsPrice && price == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写消费券面值");
                return;
            }
            bool IsDate = chkDate.Checked;
            if (IsDate && (String.IsNullOrWhiteSpace(txtStartDate.Text) || String.IsNullOrWhiteSpace(txtEndDate.Text)))
            {
                Common.MessageBox.ShowFailTip(this, "请选择优惠券使用时间");
                return;
            }

            int point = Common.Globals.SafeInt(txtPoint.Text, 0);
            if (point == 0 && chkExchange.Checked)
            {
                Common.MessageBox.ShowFailTip(this, "请填写兑换所需积分");
                return;
            }
            string FileName = uploadExcel.PostedFile.FileName;
            string ErrorMsg = "出现异常，请检查您的数据格式";
            int Count = 0;
            if (!uploadExcel.HasFile)
            {
                Common.MessageBox.ShowFailTip(this, "请上传文件");
                return;
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(UploadPath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(UploadPath));
            }
            uploadExcel.PostedFile.SaveAs(Server.MapPath(UploadPath) + FileName);
            ruleModel.Name = name;
            ruleModel.NeedPoint = chkExchange.Checked ? point : 0;
            ruleModel.IsPwd = 0;
            ruleModel.IsReuse = 0;
            ruleModel.LimitPrice = IsLimitPrice ?limitPrice :0 ;
            ruleModel.PreName = "";
            ruleModel.SendCount = 0;
             ruleModel.StartDate = IsDate ?   Common.Globals.SafeDateTime(txtStartDate.Text, DateTime.Now):DateTime.Now;
            ruleModel.Status = 1;
            ruleModel.SupplierId = 0;
            ruleModel.CategoryId = 0;
            ruleModel.ClassId = 0;
            ruleModel.CouponPrice = IsPrice ?price: 0;
            ruleModel.CreateDate = DateTime.Now;
            ruleModel.CreateUserId = CurrentUser.UserID;
            ruleModel.Type = chkExchange.Checked ? 1 : 0;
            ruleModel.EndDate = IsDate ? Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.MaxValue):DateTime.Now ;
            ruleModel.CpLength = 0;
            ruleModel.PwdLength = 0;
            if (ExcelImport(UploadPath, FileName, ruleModel, IsDate, IsPrice, IsLimitPrice, out ErrorMsg, ref Count))
            {
                Common.MessageBox.ShowSuccessTip(this, "成功导入" + Count + "条优惠券数据", "CouponList.aspx");
            }
            else
            {
                Common.MessageBox.ShowSuccessTip(this, "插入失败,信息:" + ErrorMsg + "提示：请先下载上面的Excel文件模板");
            }

        }


        public bool ExcelImport(string Path, string FileName, Maticsoft.Model.Shop.Coupon.CouponRule ruleModel, bool IsDate, bool IsPrice, bool IsLimitPrice, out string ErrorMsg, ref int Count)
        {
            string sheetName = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_CouponExcel_SheetName");
            if (String.IsNullOrWhiteSpace(sheetName))
            {
                sheetName = "Sheet1";
            }
            DataSet ExcelData = new DataSet();
            using (FileStream fs = new FileStream(Server.MapPath(Path + FileName), FileMode.OpenOrCreate))
            {
                try
                {
                //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
                HSSFWorkbook workbook = new HSSFWorkbook(fs);
                //获取excel的第一个sheet
                ISheet sheet = workbook.GetSheet(sheetName);

                DataTable table = new DataTable();
                //获取sheet的首行
                IRow headerRow = sheet.GetRow(0);

                //一行最后一个方格的编号 即总的列数
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }
                if (!table.Columns.Contains("优惠券卡号"))
                {
                    ErrorMsg = "上传Excel文件内容的数据格式不正确";
                    return false;
                }
                //最后一列的标号  即总的行数
                //int rowCount = sheet.LastRowNum;
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType==CellType.NUMERIC&& DateUtil.IsCellDateFormatted(row.GetCell(j)))
                            {
                                dataRow[j] = row.GetCell(j).DateCellValue;
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }

                    }
                    table.Rows.Add(dataRow);
                }
                ExcelData.Tables.Add(table);
               
                    Count = ruleBll.ImportExcelData(ruleModel, IsDate, IsPrice, IsLimitPrice, ExcelData.Tables[0]);
                    ErrorMsg = "";
                    return true;
                }
                catch (Exception)
                {
                    ErrorMsg =" 数据格式不正确";
                    return false;
                }
            }
        }

    }
}