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

namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class SuppliersAM : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierBll = new BLL.Shop.Supplier.SupplierInfo();

        private Maticsoft.BLL.Shop.ActivityManage.AMBLL amBll = new BLL.Shop.ActivityManage.AMBLL();
        private Maticsoft.BLL.Shop.ActivityManage.AMPBLL ampBll = new BLL.Shop.ActivityManage.AMPBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSupplier();
                Bind();
            }
        }

        public int AMId
        {
            get 
            {
                int AMId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["AMId"]))
                {
                    AMId = Common.Globals.SafeInt(Request.Params["AMId"], 0);
                }
                return AMId;
            }
        }
        protected int SupplierId
        {
            get
            {
                int supplierId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["sid"]))
                {
                    supplierId = Common.Globals.SafeInt(Request.Params["sid"], 0);
                }
                return supplierId;
            }
        }
        /// <summary>
        /// 供应商 绑定DropDownList
        /// </summary>
        private void BindSupplier()
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo infoBll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = infoBll.GetList("  Status = 1 ");// status =1 为正常

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlSupplier.DataSource = ds;
                this.ddlSupplier.DataTextField = "Name";
                this.ddlSupplier.DataValueField = "SupplierId";
                this.ddlSupplier.DataBind();
            }

            this.ddlSupplier.Items.Insert(0, new ListItem("全　部", string.Empty));
            if (SupplierId != 0)
            {
                ddlSupplier.SelectedValue = SupplierId.ToString();
            }
            else
            {
                ddlSupplier.SelectedIndex = 0;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int supplierId = int.Parse(ddlSupplier.SelectedValue);
                int amid = int.Parse(Request.QueryString["AMId"]);
                Maticsoft.Model.Shop.ActivityManage.AMPModel ampModel = new Model.Shop.ActivityManage.AMPModel();
                Maticsoft.Model.Shop.ActivityManage.AMModel amModel = new Model.Shop.ActivityManage.AMModel();
                ampModel.SupplierId = supplierId;
                ampModel.AMId = amid;
                if (ampBll.ExistsSup(int.Parse(ampModel.SupplierId.ToString())))
                {
                    MessageBox.ShowFailTip(this, "本活动已添加该商铺");
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
            List<Maticsoft.Model.Shop.ActivityManage.AMPModel> ampList = ampBll.GetModelLists();
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
                    if (ampBll.Delete(int.Parse(lb.CommandArgument.ToString())))
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
        /// 获取商家名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetSupplier(object obj)
        {
            int supplierId = Common.Globals.SafeInt(obj, 0);
            Maticsoft.Model.Shop.Supplier.SupplierInfo infoModel = supplierBll.GetModel(supplierId);
            return infoModel == null ? "" : infoModel.Name;
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
                strWhere.AppendFormat(" AMName like '%{0}%' ",txtAMName.Text.Trim());
            }
            ds = ampBll.GetLists(strWhere.ToString());
            gdv_sup.DataSource = ds;
            gdv_sup.DataBind();
        }

    }
}