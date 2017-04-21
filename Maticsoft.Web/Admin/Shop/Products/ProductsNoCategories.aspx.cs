using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProductsNoCategories : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 487; } } //Shop_未分类商品管理_列表页
        protected new int Act_UpdateData = 483;    //Shop_商品管理_编辑数据
        protected new int Act_DelData = 488;    //Shop_未分类商品管理_删除数据
        private Maticsoft.BLL.Shop.Products.ProductInfo bll = new BLL.Shop.Products.ProductInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
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
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            Model.Shop.Products.ProductInfo model = new Model.Shop.Products.ProductInfo();
            model.SaleStatus = (int) ProductSaleStatus.Deleted;
            model.CategoryId = (int) ProductCategoryStatus.None;
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text.Trim()))
            {
                model.ProductName = this.txtKeyword.Text.Trim();
            }
            gridView.DataSetSource = bll.GetListByCategoryIdSaleStatus(model);

            BindDropList();
        }

        private void BindDropList()
        {
            //一级分类
            Maticsoft.BLL.Shop.Products.CategoryInfo bllc = new BLL.Shop.Products.CategoryInfo();
            DataSet dsc = bllc.GetList(" Depth = 1");

            if (!DataSetTools.DataSetIsNull(dsc))
            {
                this.dropCategories.DataSource = dsc;
                this.dropCategories.DataTextField = "Name";
                this.dropCategories.DataValueField = "CategoryId";
                this.dropCategories.DataBind();
            }
            this.dropCategories.Items.Insert(0, new ListItem(Resources.Site.PleaseSelect, "0"));
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
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //bll.Delete(ID);
            //gridView.OnBind();
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;

                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #region 批量删除

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.UpdateList(idlist, Maticsoft.Model.Shop.Products.ProductSaleStatus.Deleted);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #endregion 批量删除

        #region 转移主类

        /// <summary>
        /// 转移主类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (!string.IsNullOrWhiteSpace(idlist))
            {
                int categoryId = Globals.SafeInt(this.dropCategories.SelectedValue, 0);
                if (categoryId == 0)
                {
                    return;
                }
                if (bll.ChangeProductsCategory(idlist, categoryId))
                {
                    MessageBox.ShowSuccessTip(this, "修改成功！");
                }
            }
            gridView.OnBind();
        }


        protected long StockNum(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    long productId = Common.Globals.SafeLong(obj.ToString(), 0);
                    return bll.StockNum(productId);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        #endregion 转移主类

        #endregion gridView
    }
}