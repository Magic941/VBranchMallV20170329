using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Maticsoft.Web.Admin.Ms.EmailTemplet
{
    public partial class TempletList : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 307; } } //设置_邮件模版管理_列表页
        protected new int Act_AddData = 308;    //设置_邮件模版管理_添加数据 
        protected new int Act_UpdateData = 309;    //设置_邮件模版管理_编辑数据
        protected new int Act_DelData = 310;    //设置_邮件模版管理_删除数据
 
        Maticsoft.BLL.Ms.EmailTemplet bll = new BLL.Ms.EmailTemplet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liadd.Visible = false;
                }
                
                
            }

        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[4].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            DataSet ds = new DataSet();
            ds = bll.GetAllList();
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
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
            int type = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(type);
            gridView.OnBind();
        }
    }
}