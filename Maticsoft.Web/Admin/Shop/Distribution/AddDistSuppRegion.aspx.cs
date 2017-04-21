using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Shop.Distribution
{
    public partial class AddDistSuppRegion :PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo infoBll = new BLL.Shop.Supplier.SupplierInfo();
        private Maticsoft.BLL.Shop.Distribution.SuppDistRegion suppRegionBll = new BLL.Shop.Distribution.SuppDistRegion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlSupplier.DataSource = infoBll.GetList("Status=1");
                 ddlSupplier.DataTextField = "Name";
                 ddlSupplier.DataValueField = "SupplierId";
                 ddlSupplier.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int supplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            if (supplierId == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请选择分销商家");
                return;
            }
            int regionId = RegionID.Region_iID;
            if (regionId == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请选择分销商家");
                return;
            }
            if (suppRegionBll.Exists(supplierId, regionId))
            {
                Common.MessageBox.ShowFailTip(this, "该分销商的地区已经设置，请不要重复设置");
                return;
            }
            Maticsoft.Model.Shop.Distribution.SuppDistRegion model = new Model.Shop.Distribution.SuppDistRegion();
            model.SupplierId=supplierId;
            model.RegionId=regionId;
            if(suppRegionBll.Add(model))
            {
                 Common.MessageBox.ShowSuccessTipScript(this,"操作成功", "window.parent.location.reload();");
                return;
            }
            Common.MessageBox.ShowFailTip(this, "操作失败，请稍候再试！");
        }
    }
}