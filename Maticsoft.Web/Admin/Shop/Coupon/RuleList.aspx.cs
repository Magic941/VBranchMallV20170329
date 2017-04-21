using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.Supplier;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class RuleList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 415; } } //Shop_优惠券规则管理_列表页
        protected new int Act_AddData =417;    //Shop_优惠券规则管理_添加数据

        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new CouponRule();
         private Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new CouponClass();
        private  Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll=new CouponInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        #region gridView

        public void BindData()
        {

            StringBuilder whereStr = new StringBuilder(" 1=1 ");
            string keyword = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                whereStr.AppendFormat(" and Name Like '%{0}%'", keyword);
            }
            if (ddlUseType.SelectedValue!="-1")
            {
                whereStr.Append(" and UseType=" + ddlUseType.SelectedValue);
            }
            if (ddlAutoType.SelectedValue!="-1")
            {
                whereStr.Append(" and AutoState=" + ddlAutoType.SelectedValue);
            }

            DataSet ds = ruleBll.GetList(whereStr.ToString());
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
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
            int ruleId = (int)gridView.DataKeys[e.RowIndex].Value;
            ruleBll.Delete(ruleId);
            gridView.OnBind();
        }
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCoupon")
            {
                if (e.CommandArgument != null)
                {
                    int ruleId = Common.Globals.SafeInt(e.CommandArgument.ToString(),0);
                    if (infoBll.DeleteEx(ruleId))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
        }
        

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
        /// <summary>
        /// 商品分类名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetCategoryName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int categoryId = Common.Globals.SafeInt(target, 0);
                Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
                Maticsoft.Model.Shop.Products.CategoryInfo categoryModel = cateBll.GetModel(categoryId);
                str = categoryModel == null ? "" : categoryModel.Name;
            }
            return str;
        }
        /// <summary>
        /// 优惠券分类名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetClassName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int classId = Common.Globals.SafeInt(target, 0);
                Maticsoft.Model.Shop.Coupon.CouponClass classModel = classBll.GetModel(classId);
                str = classModel == null ? "" : classModel.Name;
            }
            return str;
        }
        /// <summary>
        /// 获取商家名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetSupplierName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int supplierId = Common.Globals.SafeInt(target, 0);
                Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierBll = new SupplierInfo();
                Maticsoft.Model.Shop.Supplier.SupplierInfo supplierModel = supplierBll.GetModel(supplierId);
                str = supplierModel == null ? "" : supplierModel.Name;
            }
            return str;
        }

        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetUserName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int userId = Common.Globals.SafeInt(target, 0);
                Maticsoft.Accounts.Bus.User userModel = new User(userId);
                str = userModel == null ? "" : userModel.NickName;
            }
            return str;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GeStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "不启用";
                        break;
                    case 1:
                        str = "启用";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }


        #endregion
    }
}