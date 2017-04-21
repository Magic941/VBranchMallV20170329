using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.FeedBack
{
    public partial class ShowDetail : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 285; } } 
        Maticsoft.BLL.Members.Feedback bll = new BLL.Members.Feedback();
        private Maticsoft.BLL.Members.FeedbackType typeBll = new BLL.Members.FeedbackType();
        protected void Page_Load(object sender, EventArgs e)   
        {
            if (!Page.IsPostBack)
            {
                ShowInfo(FeedbackID);
            }
        }
        private void ShowInfo(int FeedbackID)
        {
            Maticsoft.Model.Members.Feedback model = bll.GetModel(FeedbackID);
            this.lblUserName.Text = model.UserName;
            this.lblPhone.Text = model.Phone;
            this.lblCompany.Text = model.UserCompany;
            this.lblEmail.Text = model.UserEmail;
            this.lbltxtContent.Text = model.Description;
            this.lblIsSolved.Text = model.IsSolved ? Resources.Site.lblTrue : Resources.Site.lblFalse;
            this.lblUserIP.Text = model.UserIP;
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.txtResult.Text = model.Result;
            this.lblSex.Text = model.UserSex;
            this.lblStatus.Text = model.Status == 0 ? Resources.Site.lblFalse : Resources.Site.lblTrue;
            this.lbltypeName.Text = GetTypeName(model.TypeId);
            if (model.IsSolved == true)
            {
                this.btnSolve.Visible = false;
                this.txtResult.ReadOnly = true;
            }
        }
        public int FeedbackID
        {
            get
            {
                int feedbackid = 0;
                if (Request.Params["id"] != null && PageValidate.IsNumber(Request.Params["id"]))
                {
                    feedbackid = int.Parse(Request.Params["id"]);
                }
                else
                {
                    Response.Redirect("FeedbackList.aspx");
                }
                return feedbackid;
            }
        }
        protected void btnSolve_Click(object sender, EventArgs e)
        {
            string Result = this.txtResult.Text;
            Maticsoft.Model.Members.Feedback model = bll.GetModel(FeedbackID);
            model.IsSolved = true;
            model.Result = Result;
            if (bll.Update(model))
            {
                //发送邮件给反馈用户
                Maticsoft.BLL.Ms.EmailTemplet emailBll = new BLL.Ms.EmailTemplet();
                emailBll.SendFeedbackEmail(model);

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "反馈已解决", "FeedbackList.aspx");
            }
        }


        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetTypeName(int  typeId)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
           
                Maticsoft.Model.Members.FeedbackType typeModel = typeBll.GetModel(typeId);
                if (typeModel != null)
                {
                    str = typeModel.TypeName;
                }
          
            return str;
        }
    }
}