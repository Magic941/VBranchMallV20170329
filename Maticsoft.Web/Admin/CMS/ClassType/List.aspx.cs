using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.CMS.ClassType
{
    public partial class List : PageBaseAdmin
    {
        Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
        Maticsoft.BLL.CMS.ContentClass classBll = new BLL.CMS.ContentClass();
        protected override int Act_PageLoad { get { return 215; } }   //CMS_类型管理_列表页
        protected new int Act_AddData = 218;    //CMS_类型管理_添加数据
        protected new int Act_UpdateData = 219;    //CMS_类型管理_编辑数据
        protected new int Act_DelData =217;    //CMS_类型管理_删除数据
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
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
            #region
           
            #endregion
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[1].Visible = false;
            }

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[2].Visible = false;
            }

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" ClassTypeName like '%{0}%'", InjectionFilter.QuoteFilter(txtKeyword.Text.Trim()));
            }
            ds = bll.GetList(strWhere.ToString());
            gridView.DataSetSource = ds;
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
            //判断该类型下是否有数据
            int typeId = (int)gridView.DataKeys[e.RowIndex].Value;
            int count = classBll.GetRecordCount(" ClassTypeID=" + typeId);
            if (count > 0)
            {
                MessageBox.ShowFailTip(this, "该类型下有数据，不能删除！");
                gridView.OnBind();
            }
            else
            {
                if (bll.Delete(typeId))
                {
                    gridView.OnBind();
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                }
            }

        }
        #endregion
    }
}
