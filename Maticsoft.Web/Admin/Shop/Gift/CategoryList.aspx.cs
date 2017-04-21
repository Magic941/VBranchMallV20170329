/**
* CategoryList.cs
*
* 功 能： [N/A]
* 类 名： CategoryList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/23 20:38:01  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Drawing;

namespace Maticsoft.Web.Admin.Shop.Gift
{
    public partial class CategoryList : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 428; } } //Shop_礼品分类管理_列表页
        protected new int Act_AddData = 429;    //Shop_礼品分类管理_添加数据
        protected new int Act_UpdateData =430;    //Shop_礼品分类管理__编辑数据
        protected new int Act_DelData = 431;    //Shop_礼品分类管理__删除数据

        Maticsoft.BLL.Shop.Gift.GiftsCategory bll = new BLL.Shop.Gift.GiftsCategory();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
              
            
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
                gridView.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.DataBind();
        }

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat("Name like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = bll.GetCategoryList(strWhere.ToString());
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.DataBind();
        }


        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
            int categoryId = (int)this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                bll.SwapSequence(categoryId, Model.Shop.Products.SwapSequenceIndex.Up);
            }
            if (e.CommandName == "Rise")
            {
                bll.SwapSequence(categoryId, Model.Shop.Products.SwapSequenceIndex.Down);
            }
            BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }

                int num = (int)DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl control = e.Row.FindControl("spShowImage") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    control.Visible = false;
                }
                Label label = e.Row.FindControl("lblName") as Label;
                label.Text = str;
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.DeleteCategory(ID);
            BindData();
        }
        #endregion
    }
}
