/**
* SalesLine.cs
*
* 功 能： [N/A]
* 类 名： SalesLine.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using Maticsoft.Common;
using System.Data;
using System.Text;

namespace Maticsoft.Web.Admin.Statistics
{
    public partial class SalesLine : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string pageTitle;
                switch (SalesType)
                {
                    case 2:
                        pageTitle = "业绩";
                        break;
                    default:
                        pageTitle = "销量";
                        break;
                }
                litTitle.Text = string.Format(litTitle.Text, pageTitle);
                litDec.Text = string.Format(litDec.Text, pageTitle);

                txtStartDate.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ShowInfo();
            }
        }

        public int SalesType
        {
            get
            {
                return Globals.SafeInt(Request.QueryString["SalesType"], 0);
            }
        }

        private void ShowInfo()
        {
            DateTime startDate = Globals.SafeDateTime(txtStartDate.Text, DateTime.Now);
            DateTime endDate = Globals.SafeDateTime(txtEndDate.Text, DateTime.Now);

            //Reset Time
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1).AddSeconds(-1);

            Model.Shop.Order.StatisticMode mode = (Model.Shop.Order.StatisticMode)Globals.SafeInt(rdoMode.SelectedValue, 0);

            DataSet ds = BLL.Shop.Order.OrderManage.StatSales(mode, startDate, endDate);

            string str = CreateXmlStr(ds, SalesType, mode);
            this.litChart.Text = FusionCharts.RenderChart("/FusionCharts/Line.swf", "", str, "FusionChartsLine", "900", "500", false, true);
        }

        private string CreateXmlStr(DataSet ds, int type, Model.Shop.Order.StatisticMode mode)
        {
            string chartTitle = litTitle.Text;
            string chartConfig = string.Empty;
            switch (type)
            {
                case 2:
                    chartConfig = " yAxisName='金额（元）' numberPrefix ='￥' ";
                    break;
                default:
                    break;
            }

            if (DataSetTools.DataSetIsNull(ds)) return "";

            string desc,format;
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
                    switch (type)
                    {
                        case 2:
                            xmlData.AppendFormat(" value='{0}' />", ds.Tables[0].Rows[i].Field<decimal>("ToalPrice").ToString("F"));
                            break;
                        default:
                            xmlData.AppendFormat(" value='{0}' />", ds.Tables[0].Rows[i].Field<int>("ToalQuantity"));
                            break;
                    }
                }
                xmlData.Append("</chart>");
            }
            return xmlData.ToString();
        }

        protected void btnReStatistic_OnClick(object sender, EventArgs e)
        {
            ShowInfo();
        }
    }
}
