using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;

namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class ProductGifts : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryInfoBll = new CategoryInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productInfoBll = new ProductInfo();
        private Maticsoft.BLL.Shop.PromoteSales.CountDown downBll = new CountDown();

        private Maticsoft.BLL.Shop.ActivityManage.AMBLL amBll = new BLL.Shop.ActivityManage.AMBLL();
        private Maticsoft.BLL.Shop.ActivityManage.AMPBLL ampBll = new BLL.Shop.ActivityManage.AMPBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //商品分类
                this.ddlCateList.DataSource = categoryInfoBll.GetList("Depth=1");
                ddlCateList.DataTextField = "Name";
                ddlCateList.DataValueField = "CategoryId";
                ddlCateList.DataBind();
                ddlCateList.Items.Insert(0, new ListItem("请选择", "0"));


                //this.ddlType1.DataSource = categoryInfoBll.GetList("Depth=1");
                //ddlType1.DataTextField = "Name";
                //ddlType1.DataValueField = "CategoryId";
                //ddlType1.DataBind();
                //ddlType1.Items.Insert(0, new ListItem("请选择", "0"));
            }
            Bind();
        }

        #region 绑定DropDownList 
        protected void ddlCateList_Changed(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlCateList.SelectedValue, 0);
            if (categoryId == 0)
            {
                ddlCateList2.Visible = false;
                return;
            }

            //绑定二级分类
            this.ddlCateList2.DataSource = categoryInfoBll.GetList("ParentCategoryId=" + categoryId);
            ddlCateList2.DataTextField = "Name";
            ddlCateList2.DataValueField = "CategoryId";
            ddlCateList2.DataBind();
            ddlCateList2.Items.Insert(0, new ListItem("请选择", "0"));
            ddlCateList2.Visible = true;

            ddlProduct.DataSource = productInfoBll.GetProductsByCid(categoryId);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
        }

        protected void ddlCateList2_Changed(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlCateList2.SelectedValue, 0);
            if (categoryId == 0)
            {
                categoryId = Common.Globals.SafeInt(ddlCateList.SelectedValue, 0);
            }

            ddlProduct.DataSource = productInfoBll.GetProductsByCid(categoryId);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
        }
        #endregion
        public int AMId
        {
            get
            {
                int AMId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    AMId = Common.Globals.SafeInt(Request.Params["id"], 0);
                }
                return AMId;

                
            }
        }

        //将商品添加到主商品列表
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int productid = int.Parse(ddlProduct.SelectedValue);
                int amid = int.Parse(Request.QueryString["id"]);
                Maticsoft.Model.Shop.ActivityManage.AMPModel ampModel = new Model.Shop.ActivityManage.AMPModel();
                Maticsoft.Model.Shop.ActivityManage.AMModel amModel = new Model.Shop.ActivityManage.AMModel();
                ampModel.ProductId = productid;
                ampModel.AMId = amid;
                if (ampBll.ExistsPro(int.Parse(ampModel.ProductId.ToString())))
                {
                    MessageBox.ShowFailTip(this, "该商品已经添加到活动规则,请勿重复添加！");
                }
                else
                {
                    if (ampBll.Add(ampModel).Equals(true))
                    {
                        MessageBox.ShowSuccessTip(this, "添加成功。");
                        Bind();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, "添加失败。");
            }
        }

        protected void Bind()
        {
            List<Maticsoft.Model.Shop.ActivityManage.AMPModel> ampList = ampBll.GetModelLists(AMId);
            gdv_sup.DataSource = ampList;
            gdv_sup.DataBind();

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            if (lb.CommandArgument != "")
            {
                try
                {
                    if (ampBll.DeleteByProId(int.Parse(lb.CommandArgument.ToString())))
                    {
                        MessageBox.ShowSuccessTip(this, "删除成功！");
                        Bind();
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "删除失败！");
                    }
                }
                catch (Exception)
                {
                    MessageBox.ShowFailTip(this, "程序出错，请联系技术人员！");
                }
            }
        }
        

        protected void gdv_Sup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            }

        }

        /// <summary>
        /// 获取活动名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetAMName(object obj)
        {
            if (obj == null)
                return string.Empty;
            int AMId = Common.Globals.SafeInt(obj.ToString(), 0);
            Maticsoft.Model.Shop.ActivityManage.AMModel amModel = amBll.GetModel(AMId);
            if (amModel != null)
            {
                return amModel.AMName;
            }
            return "未知活动名称";
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        public void DataBind()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtAMName.Text != "")
            {
                strWhere.AppendFormat(" AMName like '%{0}%' ", txtAMName.Text.Trim());
                //if (txtProductCode.Text != "")
                //{ 
                //    strWhere.AppendFormat(" AND AM")
                //}
            }
            ds = ampBll.GetLists(strWhere.ToString());
            gdv_sup.DataSource = ds;
            gdv_sup.DataBind();
        }



        /*
        protected void ddlType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlType1.SelectedValue, 0);
            if (categoryId == 0)
            {
                ddlType2.Visible = false;
                return;
            }

            //绑定二级分类
            this.ddlType2.DataSource = categoryInfoBll.GetList("ParentCategoryId=" + categoryId);
            ddlType2.DataTextField = "Name";
            ddlType2.DataValueField = "CategoryId";
            ddlType2.DataBind();
            ddlType2.Items.Insert(0, new ListItem("请选择", "0"));
            ddlType2.Visible = true;

            ddlPro.DataSource = productInfoBll.GetProductsByCid(categoryId);
            ddlPro.DataTextField = "ProductName";
            ddlPro.DataValueField = "ProductId";
            ddlPro.DataBind();
        }


        protected void ddlType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlType2.SelectedValue, 0);
            if (categoryId == 0)
            {
                categoryId = Common.Globals.SafeInt(ddlType1.SelectedValue, 0);
            }

            ddlPro.DataSource = productInfoBll.GetProductsByCid(categoryId);
            ddlPro.DataTextField = "ProductName";
            ddlPro.DataValueField = "ProductId";
            ddlPro.DataBind();
        }
        */
        //protected void ddlPro_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int categoryId = Common.Globals.SafeInt(ddlPro.SelectedValue, 0);
        //    if (categoryId == 0)
        //    {
        //        categoryId = Common.Globals.SafeInt(ddlPro.SelectedValue, 0);
        //    }
        //    //string proName = "";
        //    List<Model.Shop.Products.ProductInfo> pList = new List<Model.Shop.Products.ProductInfo>();
        //    pList= productInfoBll.GetProductsByCid(categoryId);

        //}



        /// <summary>
        /// 合并 赠品行
        /// </summary>
        /// <param name="gvw"></param>
        /// <param name="sCol"></param>
        /// <param name="eCol"></param>
        /*
        public static void MergeRows(GridView gvw, int sCol, int eCol)//gvw 需要合并的GridView，sCol要合并开始列（从0开始），eCol要合并的结束列 
        {

            for (int rowIndex = gvw.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {

                GridViewRow row = gvw.Rows[rowIndex];



                GridViewRow previousRow = gvw.Rows[rowIndex + 1];



                for (int i = sCol; i < eCol + 1; i++)
                {

                    if (row.Cells[i].Text != "" && row.Cells[i].Text != " ")
                    {

                        if (row.Cells[i].Text == previousRow.Cells[i].Text)
                        {

                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 1 ? 2 : previousRow.Cells[i].RowSpan + 1;



                            previousRow.Cells[i].Visible = false;

                        }

                    }

                }

            }

        }
        */

    }
}