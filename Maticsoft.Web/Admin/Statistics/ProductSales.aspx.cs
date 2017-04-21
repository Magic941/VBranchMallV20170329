using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Data;
using System.Text;
using Maticsoft.BLL.Shop.Order;

namespace Maticsoft.Web.Admin.Statistics
{
    public partial class ProductSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ShowInfo();
                
            }
        }

        protected void btnReStatistic_OnClick(object sender, EventArgs e)
        {
            ShowInfo();
            BindData();
        }

        private void ShowInfo()
        {
            DateTime startDate = Globals.SafeDateTime(txtStartDate.Text, DateTime.Now);
            DateTime endDate = Globals.SafeDateTime(txtEndDate.Text, DateTime.Now);
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1).AddSeconds(-1);
            Model.Shop.Order.StatisticMode mode = (Model.Shop.Order.StatisticMode)Globals.SafeInt(rdoMode.SelectedValue, 0);
            if (mode.ToString() == "Day")
            {
                TimeSpan time = endDate - startDate;
                if (time.Days > 31)
                {
                    startDate = endDate.AddDays(-31);
                   
                }
            }
            DataSet ds = BLL.Shop.Order.OrderManage.ProductSales(mode, startDate, endDate);
            string str = CreateXmlStr(ds, mode);
            this.litChart.Text = FusionCharts.RenderChart("/FusionCharts/Line.swf", "", str, "FusionChartsLine", "900", "500", false, true);
        }

        private string CreateXmlStr(DataSet ds, Model.Shop.Order.StatisticMode mode)
        {
            string chartTitle = litTitle.Text;
            string chartConfig = string.Empty;
            if (DataSetTools.DataSetIsNull(ds)) return "";

            string desc, format;
            switch (mode)
            {
                case Model.Shop.Order.StatisticMode.Day:
                    desc = "天";
                    format = "yyyy-MM-dd";
                    break;
                case Model.Shop.Order.StatisticMode.Month:
                    desc = "月份";
                    format = "yyyy-MM";
                    break;
                case Model.Shop.Order.StatisticMode.Year:
                    desc = "年份";
                    format = "yyyy";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }

            StringBuilder xmlData = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                xmlData.AppendFormat(
                   "<?xml version='1.0' encoding='utf-8' ?><chart caption='{0}' xAxisName='" + desc + "' " + chartConfig + " showValues='1' showhovercap='0'  formatNumberScale='0' showBorder='0' palette='2' animation='1'  showPercentInToolTip='0' labelDisplay='Rotate' slantLabels='1' > ",
                   chartTitle);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    xmlData.AppendFormat("<set label='{0}' ", ds.Tables[0].Rows[i].Field<DateTime>("GeneratedDate").ToString(format));
                    xmlData.AppendFormat(" value='{0}' />", String.IsNullOrEmpty((ds.Tables[0].Rows[i]["ToalQuantity"].ToString())) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ToalQuantity"]));
                }
                xmlData.Append("</chart>");
            }
            return xmlData.ToString();
        }

        public void BindData()
        {
            DateTime startDate = Globals.SafeDateTime(txtStartDate.Text, DateTime.Now);
            DateTime endDate = Globals.SafeDateTime(txtEndDate.Text, DateTime.Now);
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1).AddSeconds(-1);
            Model.Shop.Order.StatisticMode mode = (Model.Shop.Order.StatisticMode)Globals.SafeInt(rdoMode.SelectedValue, 0);
            this.gridView.DataSource = BLL.Shop.Order.OrderManage.ProductSaleInfo(mode, startDate, endDate);
            this.gridView.DataBind();
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }

            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportNew_Click(object sender, EventArgs e)
        {
            BindData();
            DataSet dataSet = gridView.DataSource as DataSet;
            if (Common.DataSetTools.DataSetIsNull(dataSet))
            {
                MessageBox.ShowServerBusyTip(this, "抱歉, 当前没有可以导出的数据!");
                return;
            }
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("商品名称", typeof(string)));

            dataTable.Columns.Add(new DataColumn("销售量(份)", typeof(int)));


            DataRow tmpRow;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                //List<Maticsoft.Model.Shop.Order.OrderItems> items = GetOrderItemsList(row.Field<long>("OrderId"));
                
                //foreach (Maticsoft.Model.Shop.Order.OrderItems item in items)
                //{

                    tmpRow = dataTable.NewRow();

                    tmpRow["商品名称"] = row.Field<string>("ProductName");
                    tmpRow["销售量(份)"] = row.Field<int>("ToalQuantity");
                    
                    dataTable.Rows.Add(tmpRow);
                //}
            }
            DataSetToExcel(dataTable);
        }
        
        private List<Maticsoft.Model.Shop.Order.OrderItems> GetOrderItemsList(long orderId)
        {
            BLL.Shop.Order.OrderItems orderItemManage = new BLL.Shop.Order.OrderItems();
            List<Maticsoft.Model.Shop.Order.OrderItems> list = orderItemManage.GetModelListByCache(" OrderId=" + orderId);
            return list;
        }
        private void DataSetToExcel(DataTable data)
        {
            Response.Clear();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                string nowDate = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.HSSF.UserModel.HSSFSheet sheet = (NPOI.HSSF.UserModel.HSSFSheet)workbook.CreateSheet(string.Format("导出订单_{0}", nowDate));
                NPOI.HSSF.UserModel.HSSFCellStyle cellStyle = (NPOI.HSSF.UserModel.HSSFCellStyle)workbook.CreateCellStyle();
                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;
                cellStyle.FillPattern = NPOI.SS.UserModel.FillPatternType.SOLID_FOREGROUND;
                NPOI.SS.UserModel.IFont f = workbook.CreateFont();
                f.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
                cellStyle.SetFont(f);
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.MEDIUM;
                cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
                NPOI.HSSF.UserModel.HSSFRow headerRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(0);
                headerRow.Height = 600;

                //headerRow.RowStyle.FillBackgroundColor = 13;
                foreach (DataColumn column in data.Columns)
                {
                    NPOI.HSSF.UserModel.HSSFCell Cell = (NPOI.HSSF.UserModel.HSSFCell)headerRow.CreateCell(column.Ordinal);
                    Cell.SetCellValue(column.ColumnName);
                    Cell.CellStyle = cellStyle;
                }
                int rowIndex = 1;
                foreach (DataRow row in data.Rows)
                {
                    NPOI.HSSF.UserModel.HSSFRow dataRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in data.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    dataRow = null;
                    rowIndex++;
                }
                //自动调整列宽
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                workbook.Write(ms);
                headerRow = null;
                sheet = null;
                workbook = null;
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=ExportOrder_{0}.xls", nowDate));
                Response.BinaryWrite(ms.ToArray());
                ms.Close();
                ms.Dispose();
            }
            Response.End();
        }
        
    }
}