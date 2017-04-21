using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Text;

namespace Maticsoft.Web.Admin.WeChat.CustomerMsg
{
    public partial class UserList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.CustUserMsg msgBll = new Maticsoft.WeChat.BLL.Core.CustUserMsg();
        Maticsoft.WeChat.BLL.Core.User userBll = new Maticsoft.WeChat.BLL.Core.User();
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected int MsgId
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }
        #region gridView


        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" MsgId={0}", MsgId);
            gridView.DataSetSource = msgBll.GetList(-1, strWhere.ToString(), " MsgId");
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

      
        protected string GetUserName(object target)
        {
            //0:取消关注、1:关注、
            string str = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
            
                string userName = target.ToString();
                string nickName = userBll.GetNickName(userName);
                str= String.IsNullOrWhiteSpace(nickName) ? userName : nickName;
            }
            return str;
        }
    }
}