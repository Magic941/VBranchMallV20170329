using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Reservation
{
    public partial class AddReservation : PageBaseAdmin
    {
        BLL.Appt.Reservation bll = new BLL.Appt.Reservation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtStartDate.Text = DateTime.Now.ToString("yyyy-mm-dd");
                #region 商家
                Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                drpSupplier.DataSource = supplierManage.GetModelList("");
                DataSet ds = supplierManage.GetAllList();
                if (!DataSetTools.DataSetIsNull(ds))
                {
                    this.drpSupplier.DataSource = ds;
                    this.drpSupplier.DataTextField = "Name";
                    this.drpSupplier.DataValueField = "SupplierId";
                    this.drpSupplier.DataBind();
                }
                this.drpSupplier.Items.Insert(0, new ListItem("无", "0"));
                this.drpSupplier.SelectedIndex = 0;
                #endregion
            }
        }

        #region 保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Appt.Reservation model = new Model.Appt.Reservation();
            model.Name = txtName.Text.Trim();
            model.RegionId = Globals.SafeInt(ajaxRegion.SelectedValue, -1);
            model.ContactName = txtContactName.Text.Trim();
            model.ContactPhone = txtContactPhone.Text.Trim();
            model.ReservalDate =Globals.SafeDateTime(txtEndDate.Text.Trim().ToString(),DateTime.Now);
            model.Content = txtContent.Text.Trim();
            model.Address = txtAddress.Text.Trim();
            model.ContactEmail = txtEmail.Text.Trim();
            model.Status = 0;
            model.CreatedDate = DateTime.Now;
            model.CreatedUserId = CurrentUser.UserID;
            model.SupplierId = Globals.SafeInt(drpSupplier.SelectedValue, -1);
            model.ServiceId = Globals.SafeInt(ddlType.Value, -1);
            model.OrderCode = Guid.NewGuid().ToString().Substring(0, 8);
            model.Remark = txtRemark.Text;
            if (bll.Add(model)>0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "ReservationLists.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "系统忙，请稍后再试！");
            }
        }
        #endregion

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReservationLists.aspx");
        }
    }
}