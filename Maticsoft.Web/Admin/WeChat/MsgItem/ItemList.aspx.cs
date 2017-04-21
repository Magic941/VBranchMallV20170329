using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.MsgItem
{
    public partial class ItemList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.MsgItem itemBll = new Maticsoft.WeChat.BLL.Core.MsgItem();
        private Maticsoft.WeChat.BLL.Core.PostMsgItem postItemBll = new Maticsoft.WeChat.BLL.Core.PostMsgItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定用户分组
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView


        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();
            //关键字
            if (!String.IsNullOrWhiteSpace(this.txtKeyword.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" Title like '%{0}%' ", this.txtKeyword.Text);
            }
            gridView.DataSetSource = itemBll.GetList(-1, strWhere.ToString(), "ItemId desc");
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

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int itemId=(int)gridView.DataKeys[e.RowIndex].Value;
           int count= postItemBll.GetItemCount(itemId);
           if (count > 0)
           {
               Maticsoft.Common.MessageBox.ShowFailTip(this, "该素材已被引用，不能删除");
               return;
           }
            if (itemBll.Delete(itemId))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
        }
        #endregion






 




    }
}