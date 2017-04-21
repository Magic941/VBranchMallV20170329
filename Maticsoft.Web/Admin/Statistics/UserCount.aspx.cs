using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Data;
using System.Text;

namespace Maticsoft.Web.Admin.Statistics
{
    public partial class UserCount : System.Web.UI.Page
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

    
        protected void btnReStatistic_Click(object sender, EventArgs e)
        {
            ShowInfo();   
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
            Maticsoft.BLL.Members.Users user = new BLL.Members.Users();
            DataSet ds = user.GetUserCount(mode, startDate, endDate);
            string str = CreateXmlStr(ds, mode);
            this.litChart.Text = FusionCharts.RenderChart("/FusionCharts/Line.swf", "", str, "FusionChartsLine", "900", "500", false, true);
        }
        private string CreateXmlStr(DataSet ds, Model.Shop.Order.StatisticMode mode)
        {
            string chartTitle = LiteralRT.Text;
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
                    //xmlData.AppendFormat(" value='{0}' />", ds.Tables[0].Rows[i].Field<Int32>("ToalQuantity"));
                    xmlData.AppendFormat(" value='{0}' />", String.IsNullOrEmpty((ds.Tables[0].Rows[i]["Users"].ToString()))?0:Convert.ToInt32(ds.Tables[0].Rows[i]["Users"]));
                }
                xmlData.Append("</chart>");
            }
            return xmlData.ToString();
        }
    }
}