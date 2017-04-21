using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
namespace Maticsoft.Web.Admin.Options
{
    public partial class ShowCount3D : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 356; } } //客服管理_问卷管理_结果页
        public int allsum = 0;
        public string usercount = "0";

        public class OptionList
        {
            public string name;
            public string count;
            public string totalcount;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["fid"] != null && Request.Params["fid"].Trim() != "")
                {
                    string fid = Request.Params["fid"];
                    //ViewState["uid"] = uid;
                    int formid = Convert.ToInt32(fid);
                    Maticsoft.BLL.Poll.Forms bll = new Maticsoft.BLL.Poll.Forms();
                    Maticsoft.Model.Poll.Forms model = bll.GetModel(formid);
                    lblFormName.Text = model.Name;
                    lblFormID.Text = model.FormID.ToString();

                    //Maticsoft.BLL.Users bllu = new Maticsoft.BLL.Users();
                    //usercount = bllu.GetUserCount().ToString();

                    BindData(formid);

                }
            }
        }

        public void BindData(int FormID)
        {
            Maticsoft.BLL.Poll.Options blloption = new Maticsoft.BLL.Poll.Options();
            DataTable dt = blloption.GetCountList(FormID).Tables[0];
            ViewState["dtcount"] = dt;

            Maticsoft.BLL.Poll.Topics blltop = new Maticsoft.BLL.Poll.Topics();
            DataSet dstop = blltop.GetListByForm(FormID);

            gridViewTopic.DataSource = dstop;
            gridViewTopic.DataBind();
        }

        protected void gridViewTopic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridView itemsview = (GridView)e.Row.FindControl("gridViewOption");
            //if (itemsview != null)
            //{
            //    //itemsview.BorderColor = ColorTranslator.FromHtml("#d5d9d8");
            //    //itemsview.HeaderStyle.BackColor = ColorTranslator.FromHtml("#f6f6f6");
            //    BindItemData(itemsview, Int32.Parse(gridViewTopic.DataKeys[e.Row.RowIndex].Value.ToString()));
            //}
        }
        public string GetChartHtml(int TopicID)
        {
            StringBuilder xmlData = new StringBuilder();

            //Generate the chart element
            xmlData.Append("<chart caption='" + Resources.Poll.OptionsStatistics + "' subCaption='' xAxisName='" + Resources.Poll.Item + "' pieSliceDepth='30' showBorder='0' formatNumberScale='0' numberSuffix=' " + Resources.Poll.Votes + "' animation='1'>");

            DataTable dt = (DataTable)ViewState["dtcount"];
            //object objall = dt.Compute("sum(SubmitNum)", "");
            //if (objall.ToString() != "")
            //{
            //    allsum = int.Parse(objall.ToString());
            //}

            DataRow[] rows = dt.Select("TopicID=" + TopicID);
            //object obj = dt.Compute("sum(SubmitNum)", "TopicID=" + TopicID);
            //int sumnum = 0;
            //if (obj.ToString() != "")
            //{
            //    sumnum = int.Parse(obj.ToString());
            //}

            //Iterate through each record            
            for (int n = 0; n < rows.Length; n++)
            {
                string name = rows[n]["Name"].ToString();
                string SubmitNum = rows[n]["SubmitNum"].ToString();
                //OptionList poll = new OptionList();
                //poll.name = name;
                //poll.count = SubmitNum.ToString();
                //poll.totalcount = sumnum.ToString();
                //PollArray.Add(poll);
                xmlData.AppendFormat("<set label='{0}' value='{1}' />", name, SubmitNum);

            }
            //Close chart element
            xmlData.Append("</chart>");

            //Create the chart - Pie 3D Chart with data from xmlData
            return FusionCharts.RenderChart("/FusionCharts/Pie3D.swf", "", xmlData.ToString(), "PollSum" + TopicID.ToString(), "500", "300", false, false);
        }
        private void BindItemData(GridView itemsview, int TopicID)
        {
            DataTable dt = (DataTable)ViewState["dtcount"];

            object objall = dt.Compute("sum(SubmitNum)", "");
            if (objall.ToString() != "")
            {
                allsum = int.Parse(objall.ToString());
            }

            //if (recu2.Read()) SumVoteCount = recu2["SumVoteCount"].ToString();
            DataRow[] rows = dt.Select("TopicID=" + TopicID);
            object obj = dt.Compute("sum(SubmitNum)", "TopicID=" + TopicID);
            int sumnum = 0;
            if (obj.ToString() != "")
            {
                sumnum = int.Parse(obj.ToString());
            }

            //ArrayList PollArray = new ArrayList();
            //for (int n = 0; n < rows.Length; n++)
            //{
            //    string name = rows[n]["Name"].ToString();
            //    int SubmitNum = int.Parse(rows[n]["SubmitNum"].ToString());
            //    OptionList poll = new OptionList();
            //    poll.name = name;
            //    poll.count = SubmitNum.ToString();
            //    poll.totalcount = sumnum.ToString();
            //    PollArray.Add(poll);

            //}
            //itemsview.DataSource = PollArray;
            //itemsview.DataBind();
        }


        public int FormatCount(string count, string sumcount)
        {
            if (count.Length < 1 || sumcount.Length < 1)
                return (0);
            int sumcount1 = Int32.Parse(sumcount);
            int count1 = Int32.Parse(count);
            if (sumcount1 < 1)
                return (0);
            return (count1 * 100 / sumcount1);
        }

        public int FormatImage(int swidth)
        {
            return swidth * 4;
        }



    }
}
