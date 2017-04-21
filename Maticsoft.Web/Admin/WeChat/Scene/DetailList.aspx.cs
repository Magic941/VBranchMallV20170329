using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Scene
{
    public partial class DetailList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.Scene sceneBll = new Maticsoft.WeChat.BLL.Core.Scene();
        private Maticsoft.WeChat.BLL.Core.SceneDetail detailBll = new Maticsoft.WeChat.BLL.Core.SceneDetail();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

                //加载渠道推广场景
                ddlScene.DataSource = sceneBll.GetAllList();
                ddlScene.DataTextField = "Name";
                ddlScene.DataValueField = "SceneId";
                ddlScene.DataBind();
                ddlScene.Items.Insert(0, new ListItem("全部", "0"));
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
            int sceneId = Common.Globals.SafeInt(ddlScene.SelectedValue, 0);
            if (sceneId > 0)
            {
                strWhere.AppendFormat("SceneId={0}", sceneId);
            }

            if (!String.IsNullOrWhiteSpace(this.txtFrom.Text) && Common.PageValidate.IsDateTime(this.txtFrom.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreateTime >='" + this.txtFrom.Text + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(this.txtTo.Text) && Common.PageValidate.IsDateTime(this.txtTo.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("  CreateTime< dateadd(day,1,'{0}')", txtTo.Text.Trim());
            }
            gridView.DataSetSource = detailBll.GetList(-1, strWhere.ToString(), " CreateTime desc");
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



        protected string GetName(object target)
        {
            //0:取消关注、1:关注、
            string str = "未知";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int sceneId = Common.Globals.SafeInt(target, -1);
                Maticsoft.WeChat.Model.Core.Scene sceneModel = sceneBll.GetModelByCache(sceneId);
                str = sceneModel == null ? str : sceneModel.Name;
            }
            return str;
        }
        #endregion

    }
}