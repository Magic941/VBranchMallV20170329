using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Maticsoft.Web.Users
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 365; } } //客服管理_投票用户管理_详细页
        Maticsoft.BLL.Poll.Topics blltop = new Maticsoft.BLL.Poll.Topics();
        Maticsoft.BLL.Poll.Options blloption = new Maticsoft.BLL.Poll.Options();
        Maticsoft.BLL.Poll.UserPoll bllup = new Maticsoft.BLL.Poll.UserPoll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["uid"] != null && Request.Params["uid"].Trim() != "")
                {
                    string uid = Request.Params["uid"];
                    ShowInfo(Convert.ToInt32(uid));
                }
            }
        }

        private void ShowInfo(int UserID)
        {
            DataTable dt = bllup.GetListInnerJoin(UserID).Tables[0];
            ViewState["udt"] = dt;
            rptVote.DataSource = dt;
            rptVote.DataBind();
        }

        public void rptVote_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            #region 未使用
            //string tid = ((Label)e.Item.FindControl("labID")).Text;
            //string Type = ((Label)e.Item.FindControl("labType")).Text;
            //switch (Type)
            //{
            //    case "0"://单选
            //        RadioButtonList rbChoice = (RadioButtonList)e.Item.FindControl("rbChoice");
            //        rbChoice.Visible = true;
            //        rbChoice.DataSource = blloption.GetListByTopic(Convert.ToInt32(tid)).Tables[0];
            //        rbChoice.DataBind();
            //        //string DefaultChoice = choice.GetDefaultChoice(Tid);
            //        //if (DefaultChoice != "")
            //        //    rbChoice.Items.FindByValue(DefaultChoice).Selected = true;
            //        break;
            //    case "1"://多选
            //        CheckBoxList cbChoice = (CheckBoxList)e.Item.FindControl("cbChoice");
            //        cbChoice.Visible = true;
            //        cbChoice.DataSource = blloption.GetListByTopic(Convert.ToInt32(tid)).Tables[0];
            //        cbChoice.DataBind();
            //        break;
            //    default://问题
            //        TextBox txtContent = (TextBox)e.Item.FindControl("txtContent");
            //        txtContent.Visible = true;
            //        break;
            //} 
            #endregion
        }

        public string GetTopicTitle(int TopicID)
        {
           Model.Poll.Topics  topmodel=  blltop.GetModelByCache(TopicID);
           return topmodel.Title;
        }

        public string GetOptionName(int TopicID)
        {
            StringBuilder str = new StringBuilder();
            if (ViewState["udt"] != null)
            {
                str.Append(Resources.Poll.lblSelected);
                DataTable dt = (DataTable)ViewState["udt"];
                DataRow[] rows = dt.Select(" TopicID=" + TopicID);
                for (int n = 0; n < rows.Length; n++)
                {
                    int opid = Convert.ToInt32(rows[n]["OptionID"].ToString());
                    string name = blloption.GetModelByCache(opid).Name;
                    str.Append(name + "&nbsp;&nbsp;&nbsp;&nbsp;");
                }
            }
            return str.ToString();
        }
    }
}
