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
    public partial class CouponHistorys : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 420; } } //Shop_历史优惠券管理_列表页
        protected new int Act_DelData = 421;    //Shop_历史优惠券管理_删除数据
        private Maticsoft.BLL.Shop.Coupon.CouponHistory couponBll = new CouponHistory();
        private Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new CouponClass();
        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new CouponRule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    btnDelete.Visible = false;
                }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

                //获取优惠券分类
                ddlClass.DataSource = classBll.GetList("Status=1");
                ddlClass.DataTextField = "Name";
                ddlClass.DataValueField = "ClassId";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0, new ListItem("请选择", "0"));

                //获取规则名称
                ddlRule.DataSource = ruleBll.GetList("Status=1");
                ddlRule.DataTextField = "Name";
                ddlRule.DataValueField = "RuleId";
                ddlRule.DataBind();
                ddlRule.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }

        #region gridView

        public void BindData()
        {
            StringBuilder whereStr = new StringBuilder();
            int classId = Common.Globals.SafeInt(ddlClass.SelectedValue, 0);
            int ruleId = Common.Globals.SafeInt(ddlRule.SelectedValue, 0);
            int status = Common.Globals.SafeInt(ddlStatus.SelectedValue, -1);
            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            //状态
            if (status != -1)
            {
                whereStr.AppendFormat(" Status ={0}", status);
            }
            //分类
            if (classId > 0)
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" ClassId ={0}", classId);
            }
            //规则
            if (ruleId > 0)
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" RuleId ={0}", ruleId);
            }
            //开始时间
            if (!String.IsNullOrWhiteSpace(endDate))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" EndDate <='{0}'", endDate);
            }
            //结束时间
            if (!String.IsNullOrWhiteSpace(startDate))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" StartDate >='{0}'", startDate);
            }
            string keyword = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" CouponCode Like '%{0}%'", keyword);
            }
            DataSet ds = couponBll.GetList(whereStr.ToString());
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

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.Columns[2].Visible = chkRule.Checked;
            gridView.Columns[8].Visible = chkCategory.Checked;
            gridView.Columns[4].Visible = chkSupplier.Checked;
            gridView.Columns[9].Visible = chkUser.Checked;
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
                        str = "未分配";
                        break;
                    case 1:
                        str = "未使用";
                        break;
                    case 2:
                        str = "已使用";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (couponBll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();

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

                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += "'" + gridView.DataKeys[i].Value.ToString() + "',";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }
        #endregion
    }
}