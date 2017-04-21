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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ProductAccessories
{
    public partial class SelectAccessorieNew : PageBaseAdmin
    {
        private BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
        private BLL.Shop.Products.ProductAccessorie prodAcceBll = new BLL.Shop.Products.ProductAccessorie();
        private BLL.Shop.Products.AccessoriesValue acceValueBll = new BLL.Shop.Products.AccessoriesValue();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (Type)
                {
                    case 1:
                        littitle.Text = "选择配件商品";
                        break;
                    case 2:
                        littitle.Text = "选择优惠商品";
                        break;
                }
                BindCategories();
                //加载选择数据
                BindAddProduct();
            }
           
        }

        #region 接收参数
        public int Type
        {
            get
            {
                int type = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["acctype"]))
                {
                    type = Globals.SafeInt(Request.QueryString["acctype"], 0);
                }
                return type;
            }
        }
        public long ProductId
        {
            get
            {
                long pid = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["pid"]))
                {
                    pid = Globals.SafeLong(Request.QueryString["pid"], 0);
                }
                return pid;
            }
        }
        /// <summary>
        /// 组合id
        /// </summary>
        public int  AccessoriesId
        {
            get
            {
                int accessid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    accessid = Globals.SafeInt(Request.Params["id"], 0);
                }
                return accessid;
            }
        }
        #endregion
 
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
            //加载搜索数据
            BindData();
        }

        #region BindData

        public void BindData()
        {
            //加载搜索数据
            BindSearchProduct();

            //加载选择数据
            BindAddProduct();
        }

        private void BindAddProduct()
        {
            if (anpAddedProducts.RecordCount == 0)
            {
                anpAddedProducts.RecordCount = anpAddedProducts.PageSize;
            }

            #region 为处理添加成功之后anpAddedProducts.EndRecordIndex的值没有增加，始终让EndRecordIndex为当前PageSize的倍数
            int index = anpAddedProducts.EndRecordIndex % anpAddedProducts.PageSize;
            int endRecordIndex=anpAddedProducts.EndRecordIndex;
            if (index != 0)
            {
                endRecordIndex = anpAddedProducts.EndRecordIndex+(anpAddedProducts.PageSize - index);
            }
            #endregion

            int recordCount;
            List<Model.Shop.Products.SKUInfo> skuList = manage.GetSKUListByPageAndAcceId(AccessoriesId,anpAddedProducts.StartRecordIndex,
                //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
             endRecordIndex, out recordCount);
            anpAddedProducts.RecordCount = recordCount;
            dlstAddedProducts.DataSource = skuList;
            dlstAddedProducts.DataBind();
        }

        private void BindSearchProduct()
        {
            if (anpSearchProducts.RecordCount == 0)
            {
                anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            }

            int recordCount;
            List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetNotAcceSKUListByPage(AccessoriesId,txtProductName.Text, drpProductCategory.SelectedValue, anpSearchProducts.StartRecordIndex, anpSearchProducts.EndRecordIndex, out recordCount, ProductId);
            anpSearchProducts.RecordCount = recordCount;
            dlstSearchProducts.DataSource = skuList;
            dlstSearchProducts.DataBind();
        }

        #endregion BindData

        protected void AspNetPagerAddedProducts_PageChanged(object sender, EventArgs e)
        {
            //加载选择数据
            BindAddProduct();
        }
        protected void AspNetPageranpSearchProducts_PageChanged(object sender, EventArgs e)
        {
            //加载搜索数据
            BindSearchProduct();
        }
        protected void dlstSearch_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptSKUItems = e.Item.FindControl("rptSKUItems") as Repeater;
                rptSKUItems.DataSource = ((Model.Shop.Products.SKUInfo)e.Item.DataItem).SkuItems;
                rptSKUItems.DataBind();
            }
        }
 
        #region 添加
        protected void dlstSearchProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                if (e.CommandArgument != null)
                {
                  #region 验证
                    string sku=e.CommandArgument.ToString();
                    if (String.IsNullOrWhiteSpace(sku))
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                        return;
                    }
                  #endregion

                    if (!prodAcceBll.Exists(ProductId,AccessoriesId))
                    {
                        MessageBox.ShowFailTip(this, "当前组合不存在或已被删除！");
                        return;
                    }
                    if (acceValueBll.Exists(AccessoriesId,sku))
                    {
                        MessageBox.ShowFailTip(this, "该商品Sku在该组中已存在！");
                        return;
                    }
                    Model.Shop.Products.AccessoriesValue model = new Model.Shop.Products.AccessoriesValue();
                    model.AccessoriesId = AccessoriesId;      
                    model.SKU = sku;
                    if (acceValueBll.Add(model) > 0)
                    {
                       MessageBox.ShowSuccessTip(this, Resources.Site.TooltipAddSuccess);
                       BindData();
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Resources.Site.ErrorAddFailure);  
                    }
                }
            }
        }
        #endregion

        #region 删除
        protected void dlstAddedProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (e.CommandArgument != null)
                {
                    #region 验证
                    string  sku =e.CommandArgument.ToString();
                    if (String.IsNullOrWhiteSpace(sku))
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                        return;
                    }
                    #endregion
                    if(acceValueBll.GetRecordCount(string.Format(" AccessoriesId={0} ",AccessoriesId))<=2)
                    {
                        MessageBox.Show(this, "由于每组至少有两个商品因此您不能再继续删除，若要继续删除该组数据请将该组删除！");
                        return;
                    }
                
                    if (acceValueBll.Delete(AccessoriesId, sku))
                    {
                        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                        BindData();
                    } 
                    else
                    {
                        MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                    }
                }
              
            } 
        }
        #endregion
        protected void dlstAddedProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            LinkButton delbtn = (LinkButton)e.Item.FindControl("lbtnDel");
            object obj2 = DataBinder.Eval(e.Item.DataItem, "ProductId");
            if ((obj2 != null) && ((obj2.ToString() != "")))
            {
                if (obj2.ToString() == ProductId.ToString())//将主商品的删除按钮隐藏
                {
                    delbtn.Visible = false;
                    HtmlGenericControl spanMain = (HtmlGenericControl)e.Item.FindControl("spanMain");
                    spanMain.Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptSKUItems = e.Item.FindControl("rptSKUItems") as Repeater;
                rptSKUItems.DataSource = ((Model.Shop.Products.SKUInfo)e.Item.DataItem).SkuItems;
                rptSKUItems.DataBind();
            }
        }
    }
}