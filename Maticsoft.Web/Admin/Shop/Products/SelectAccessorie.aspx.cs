/**
* SelectCategory.cs
*
* 功 能： 选择商品分类
* 类 名： SelectCategory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/01 11:12:07   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class SelectAccessorie : PageBaseAdmin
    {
        private BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();

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

                BindCategories();
                BindData();
            }
        }

        private void BindCategories()
        {
            Maticsoft.BLL.Shop.Products.CategoryInfo bll = new BLL.Shop.Products.CategoryInfo();
            DataSet ds = bll.GetList("  Depth = 1 ");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.drpProductCategory.DataSource = ds;
                this.drpProductCategory.DataTextField = "Name";
                this.drpProductCategory.DataValueField = "CategoryId";
                this.drpProductCategory.DataBind();
            }
            this.drpProductCategory.Items.Insert(0, new ListItem("请选择", string.Empty));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        #region gridView

        public void BindData()
        {
            //int dataCount;  //分页由GirdView处理
            //Dictionary<long, string> data = manage.GetSKU4AttrVal(txtSKU.Text, txtProductName.Text,
            //    drpProductCategory.SelectedValue, 0, 0, out dataCount);
            ////设置全选数据 供JavaScript使用
            //if (data != null && data.Count > 0)
            //{
            //    hfCurrentAllData.Value = string.Join(",", data.Keys.ToArray());
            //    hfCurrentDataCount.Value = data.Count.ToString(CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //    hfCurrentAllData.Value = string.Empty;
            //    hfCurrentDataCount.Value = string.Empty;
            //}

            //gridView.DataSource = data;
            //gridView.DataBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("style", "background:#FFF;cursor: pointer;");
                e.Row.Attributes.Add("onclick", "$(this).find('[type=checkbox]').prop('checked',!$(this).find('[type=checkbox]').prop('checked'));");
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

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gridView.PageIndex = e.NewPageIndex;
            //this.BindData();
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (-2 == e.NewPageIndex)
            {
                TextBox txtNewPageIndex = null;
                GridViewRow pagerRow = theGrid.BottomPagerRow;

                if (null != pagerRow)
                {
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (null != txtNewPageIndex)
                {
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
                }
            }
            else
            {
                newPageIndex = e.NewPageIndex;
            }
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
            theGrid.PageIndex = newPageIndex;
            this.BindData();
        }

        #endregion gridView
    }
}