using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    public partial class GroupBuyList :PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.PromoteSales.GroupBuy buyBll = new GroupBuy();
        private Maticsoft.BLL.Shop.Products.ProductInfo infoBll = new ProductInfo();
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
            StringBuilder whereStr = new StringBuilder();
            int status = Common.Globals.SafeInt(ddlStatus.SelectedValue, -1);
            var PromotionType = Common.Globals.SafeInt(ddlPromotionType.SelectedValue, -1);
            string endDate = txtEndDate.Text;
            string startDate = txtStartDate.Text;
            string productName = txtProductName.Text;
            //状态
            if (status != -1)
            {
                whereStr.AppendFormat(" Status ={0}", status);
            }
            if (PromotionType!=-1)
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");

                }
                whereStr.AppendFormat(" PromotionType ={0}", PromotionType);
            }
            //开始时间
            if (!String.IsNullOrWhiteSpace(startDate))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" startDate >='{0}'", startDate);
            }
            //结束时间
            if (!String.IsNullOrWhiteSpace(endDate))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" EndDate <='{0}'", endDate);
            }
            string keyword = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" Description Like '%{0}%'", keyword);
            }
            if (!string.IsNullOrWhiteSpace(productName))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" ProductName Like '%{0}%'", productName);
            }

            DataSet ds = buyBll.GetList(-1, whereStr.ToString(), " Sequence desc  ");

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

            gridView.OnBind();
        }


        /// <summary>
        /// 商品名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetProductName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                long productId = Common.Globals.SafeLong(target, 0);
                str = infoBll.GetProductName(productId);
            }
            return str;
        }

        public string GetProductStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                long productId = Common.Globals.SafeLong(target, 0);
                int status = infoBll.GetProductStatus(productId);
                switch (status)
                {
                    case 0:
                        str = "下架(仓库中)";
                break;
                case 1:
                        str = "出售中";
                    break;
                   case 2:
                        str = "回收站";
                    break;
                    default:
                    str = "商品不存在";
                    break;
                }
            }
            return str;
        }
        

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "下架";
                        break;
                    case 1:
                        str = "上架";
                        break;

                    default:
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取团购种类
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetPromotionTypeName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "团购商品";
                        break;
                    case 1:
                        str = "好礼大派送";
                        break;

                    default:
                        break;
                }
            }
            return str;
        }

        ////EditStatus
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            
            if (buyBll.EditStatus()>0)
            {
                MessageBox.ShowSuccessTip(this,"更新成功!");
            }
            //else
            //{
            //    Maticsoft.Common.MessageBox.ShowFailTip(this,"更新失败,请重试!");
            //}
            gridView.OnBind();

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
            if (buyBll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();

        }
        //批量上架
        protected void btnOn_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (buyBll.UpdateStatus(idlist, 1))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败");
            }
            gridView.OnBind();
        }

        //批量下架
        protected void btnOff_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (buyBll.UpdateStatus(idlist, 0))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败");
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
        #endregion
    }
}