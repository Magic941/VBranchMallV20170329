/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.ProductAccessories
{
    public partial class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductAccessorie bll = new Maticsoft.BLL.Shop.Products.ProductAccessorie();

        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (Type)
                {
                    case 1:
                        Literal2.Text = "编辑配件组合";
                        Literal3.Text = "编辑配件组合";
                        literName.Text = "配件组合名称";
                        break;
                    case 2:
                        literName.Text = "优惠组合名称";
                        Literal2.Text = "编辑优惠组合";
                        Literal3.Text = "编辑优惠组合"; 
                        break;
                }
                ShowInfo();
            }
        }
        #endregion

        #region 接收参数
        public int AccessoriesId
        {
            get
            {
                int accessoriesId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    accessoriesId = Globals.SafeInt(Request.Params["id"], 0);
                }
                return accessoriesId;
            }
        }
        public long ProductId
        {
            get
            {
                long pid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["pid"]))
                {
                    pid = Globals.SafeLong(Request.Params["pid"], 0);
                }
                return pid;
            }
        }
        /// <summary>
        /// 类型 1：配件   2：组合套装
        /// </summary>
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
        #endregion

        #region 加载
        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Products.ProductAccessorie model = bll.GetModelByCache(AccessoriesId);
            if (model != null)
            {
                this.txtName.Text = model.Name;
                this.txtMaxQuantity.Text = model.MaxQuantity.ToString();
                this.txtMinQuantity.Text = model.MinQuantity.ToString();
                this.RadioDiscountType.SelectedValue = model.DiscountType.ToString();
                this.txtDiscountAmount.Text = model.DiscountAmount.ToString("F");
                this.txtStock.Text = model.Stock.ToString();
                switch (model.Type)
                {
                    case 2:
                        trDiscountAmount.Visible = true;
                       // trStock.Visible = true;
                        break;
                }
            }
            else
            {
                MessageBox.ShowFailTipScript(this, "该记录不存在或已被删除！",
                                             string.Format("window.location.href='list.aspx?pid={0}&acctype={1}'", ProductId, Type));
            }
        }
        #endregion

        #region 保存
        public void btnSave_Click(object sender, EventArgs e)
        {

            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "组合名称不能为空！");
                return;
            }
            if (this.txtMaxQuantity.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请输入最大购买量！");
                return;
            }
            int MaxQuantity = Globals.SafeInt(this.txtMaxQuantity.Text.Trim(), 0);
            if (this.txtMinQuantity.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请输入最小购买量！");
                return;
            }
            int MinQuantity = Globals.SafeInt(this.txtMinQuantity.Text.Trim(), 0);

            int DiscountType = Globals.SafeInt(this.RadioDiscountType.SelectedValue, 1);

            if (this.txtDiscountAmount.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请填写优惠价！");
                return;
            }
            decimal DiscountAmount = Globals.SafeDecimal(this.txtDiscountAmount.Text.Trim(), 0M);

            if (this.txtStock.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请输入库存！");
                return;
            }
            int Stock = Globals.SafeInt(this.txtStock.Text.Trim(), 0);
            string Name = this.txtName.Text;  
            Model.Shop.Products.ProductAccessorie model = bll.GetModelByCache(AccessoriesId, ProductId);
            if (model != null)
            {
                model.Name = Name;
                model.MaxQuantity = MaxQuantity;
                model.MinQuantity = MinQuantity;
                model.DiscountType = DiscountType;
                model.DiscountAmount = DiscountAmount;
                model.Stock = Stock;
                if (bll.Update(model))
                {
                    MessageBox.ShowSuccessTipScript(this, "保存成功！",
                                             string.Format("window.location.href='list.aspx?pid={0}&acctype={1}'", ProductId, Type));
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败！");
                }
            }
            else
            {
                MessageBox.ShowFailTipScript(this, "该记录不存在或已被删除！",
                                           string.Format("window.location.href='list.aspx?pid={0}&acctype={1}'", ProductId, Type));
            }
        }
        #endregion

        #region 取消
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("list.aspx?pid={0}&acctype={1}", ProductId, Type));
        }
        #endregion
    }
}
