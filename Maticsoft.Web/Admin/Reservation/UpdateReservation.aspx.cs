using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Data;

namespace Maticsoft.Web.Admin.Members.Reservation
{
    public partial class UpdateReservation : System.Web.UI.Page
    {
        BLL.Appt.Reservation bll = new BLL.Appt.Reservation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Maticsoft.Model.Appt.Reservation model = bll.GetModel(ReservaID);
                if (model == null)
                {
                    Response.Redirect("ReservationLists.aspx");
                }
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
                drpSupplier.SelectedValue = model.SupplierId.ToString();
                ddlType.Value = model.ServiceId.ToString();
                txtEndDate.Text = model.ReservalDate.ToString("yyyy-MM-dd");
                txtName.Text = model.Name.ToString();
                txtContent.Text = model.Content.ToString();
                txtAddress.Text = model.Address.ToString();
                txtContactName.Text = model.ContactName.ToString();
                txtContactPhone.Text = model.ContactPhone.ToString();
                txtEmail.Text = model.ContactEmail.ToString();
                txtRemark.Text = model.Remark.ToString();
                hfSelectedNode.Value = model.RegionId.ToString();
                ddlStatus.Value = model.Status.ToString();
            }
        }
        protected int ReservaID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Appt.Reservation model = bll.GetModel(ReservaID);
            model.Name = txtName.Text.Trim();
            model.RegionId = Globals.SafeInt(hfSelectedNode.Value, -1);
            model.ContactName = txtContactName.Text.Trim();
            model.ContactPhone = txtContactPhone.Text.Trim();
            model.ReservalDate = Globals.SafeDateTime(txtEndDate.Text.Trim().ToString(), DateTime.Now);
            model.Content = txtContent.Text.Trim();
            model.Address = txtAddress.Text.Trim();
            model.ContactEmail = txtEmail.Text.Trim();
            model.SupplierId = Globals.SafeInt(drpSupplier.SelectedValue, -1);
            model.ServiceId = Globals.SafeInt(ddlType.Value, -1);
            model.Remark = txtRemark.Text;
            model.Status = Globals.SafeInt(ddlStatus.Value, 0);

            if (bll.Update(model))
            {
                Response.Redirect("ReservationLists.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "修改失败！");
            }
        }
    }
}