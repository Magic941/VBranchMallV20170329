using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ProductQA
{
    public partial class Reply : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 462; } } //Shop_商品疑问管理_回复页

        Maticsoft.BLL.Shop.Products.ProductQA QAbll = new BLL.Shop.Products.ProductQA();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Maticsoft.Model.Shop.Products.ProductQA QAModel = QAbll.GetModel(QAId);
                if (QAModel == null)
                {
                    Response.Redirect("ProductQAList.aspx");
                }
                this.lbQuestion.Text = QAModel.Question;
                this.lbUserName.Text = QAModel.UserName;
                this.lbCreatedDate.Text = QAModel.CreatedDate.ToString();
                this.tReply.Text = QAModel.ReplyContent;
            }
        }

        public int QAId
        {
            get
            {
                int qaid = 0;
                if (Request.Params["qaid"] != null && PageValidate.IsNumber(Request.Params["qaid"]))
                {
                    qaid = int.Parse(Request.Params["qaid"]);
                }
                else
                {
                    Response.Redirect("ProductQAList.aspx");
                }
                return qaid;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Products.ProductQA QAModel2 = QAbll.GetModel(QAId);
            QAModel2.ReplyContent = this.tReply.Text;
            QAModel2.ReplyDate = DateTime.Now;
            QAModel2.ReplyUserId = CurrentUser.UserID;
            QAModel2.ReplyUserName = CurrentUser.UserName;
            if (this.raTrue.Checked)
            {
                QAModel2.State = 1;
            }
            if (this.raFalse.Checked)
            {
                QAModel2.State = 2;
            }


            if (QAbll.Update(QAModel2))
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowSuccessTip(this, "回复成功,正在跳转...", "ProductQAList.aspx");
            }
            else
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowFailTip(this, "添加失败！");
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductQAList.aspx");
        }
    }
}
