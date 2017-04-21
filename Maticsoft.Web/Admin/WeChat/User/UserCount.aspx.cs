using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Json;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.User
{
    public partial class UserCount : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.User userBll = new Maticsoft.WeChat.BLL.Core.User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!IsPostBack)
            {
                this.txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.txtFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            }
        }

        #region gridView
        #region 用户统计对象
        /// <summary>
        /// 用户统计对象
        /// </summary>
        protected List<Maticsoft.ViewModel.Member.UserCount> CountList
        {
            get;
            set;
        }

        #endregion 用户统计对象

        protected void btnReStatistic_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        public void BindData()
        {
            DataSet dayDs = userBll.GetDayCount(this.txtFrom.Text, this.txtTo.Text);
            DataSet cancelDs = userBll.GetCancelCount(this.txtFrom.Text, this.txtTo.Text);
            List<string> Cate = new List<string>();
            List<Maticsoft.ViewModel.Member.DayCount> DayCountList = new List<Maticsoft.ViewModel.Member.DayCount>();
            List<Maticsoft.ViewModel.Member.CancelCount> CancelList = new List<Maticsoft.ViewModel.Member.CancelCount>();
            CountList = new List<ViewModel.Member.UserCount>();
            this.hfCategory.Value = "";
            this.hfDayCount.Value = "";
            this.hfTotalCount.Value = "";
            if (dayDs.Tables[0].Rows.Count > 0)
            {
                Maticsoft.ViewModel.Member.DayCount dayCount = null;
                for (int i = 0; i < dayDs.Tables[0].Rows.Count; i++)
                {
                    dayCount = new Maticsoft.ViewModel.Member.DayCount();
                    dayCount.DateStr = dayDs.Tables[0].Rows[i]["Date"].ToString();
                    dayCount.Count = Common.Globals.SafeInt(dayDs.Tables[0].Rows[i]["DayCount"], 0);
                    DayCountList.Add(dayCount);
                }
            }
            if (cancelDs.Tables[0].Rows.Count > 0)
            {
                Maticsoft.ViewModel.Member.CancelCount cancelCount = null;
                for (int i = 0; i < cancelDs.Tables[0].Rows.Count; i++)
                {
                    cancelCount = new Maticsoft.ViewModel.Member.CancelCount();
                    cancelCount.DateStr = cancelDs.Tables[0].Rows[i]["Date"].ToString();
                    cancelCount.Count = Common.Globals.SafeInt(cancelDs.Tables[0].Rows[i]["DayCount"], 0);
                    CancelList.Add(cancelCount);
                }
            }
            Maticsoft.ViewModel.Member.UserCount count = null;
            
        
            DateTime Fdate = Common.Globals.SafeDateTime(txtFrom.Text, DateTime.Now);
            DateTime Tdate = Common.Globals.SafeDateTime(txtTo.Text, DateTime.Now);
            int totalCount = userBll.GetRecordCount(" Status=1 and CreateTime<'" + Fdate.ToString("yyyy-MM-dd") + "'");
            int days = Convert.ToInt32((Tdate - Fdate).TotalDays);
            for (int i = 0; i <= days; i++)
            {
                string dateStr = Fdate.AddDays(i).ToString("yyyy/MM/dd");
                count = new ViewModel.Member.UserCount();
                var cancel = CancelList.FirstOrDefault(c => c.DateStr == dateStr);
                count.CancelCount = cancel == null ? 0 : cancel.Count;
                count.DateStr = dateStr;
                var dayCount = DayCountList.FirstOrDefault(c => c.DateStr == dateStr);
                count.DayCount = dayCount == null ? 0 : dayCount.Count;
                totalCount = totalCount + count.DayCount ;
                count.TotalCount = totalCount;
                CountList.Add(count);
            }
            this.hfCategory.Value = String.Join(",", CountList.Select(c => c.DateStr));
            this.hfDayCount.Value = String.Join(",", CountList.Select(c => c.DayCount));
            this.hfTotalCount.Value = String.Join(",", CountList.Select(c => c.TotalCount));
            string endTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            this.lblDayCount.Text = userBll.GetRecordCount(" Status=1 and CreateTime>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'").ToString();
            this.lblTotalCount.Text = userBll.GetRecordCount(" Status=1 and CreateTime<='" + endTime + "'").ToString();
            gridView.DataSetSource = dayDs;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
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
        #endregion

        protected int GetCancelCount(object target)
        {
            //0:取消关注、1:关注、
            int count = 0;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string dateStr = target.ToString();
                Maticsoft.ViewModel.Member.UserCount model = CountList.FirstOrDefault(c => c.DateStr == dateStr);
                count = model == null ? 0 : model.CancelCount;
            }
            return count;
        }

        protected int GetTotalCount(object target)
        {
            //0:取消关注、1:关注、
            int count = 0;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string dateStr = target.ToString();
                Maticsoft.ViewModel.Member.UserCount model = CountList.FirstOrDefault(c => c.DateStr == dateStr);
                count = model == null ? 0 : model.TotalCount;
            }
            return count;
        }

        #region Ajax方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "GetDayCount":
                    writeText = GetDayCount();
                    break;
                case "GetTotalCount":
                    // writeText = GetTotalCount();
                    break;
                default:
                    writeText = GetDayCount();
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }
        /// <summary>
        /// 获取微信新增关注用户数
        /// </summary>
        /// <returns></returns>
        private string GetDayCount()
        {
            JsonObject json = new JsonObject();
            JsonArray newsArry = new JsonArray();
            // 暂时不考虑时间跨度
            DataSet ds = userBll.GetDayCount("", "");
            if (ds.Tables[0].Rows.Count > 0)
            {

            }

            return json.ToString();
        }
        #endregion
    }
}