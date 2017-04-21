using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Distribution
{
    public partial class DistSuppRegionList : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo suppBll = new BLL.Shop.Supplier.SupplierInfo();
        private Maticsoft.BLL.Ms.Regions regionBll = new BLL.Ms.Regions();
        private Maticsoft.BLL.Shop.Distribution.SuppDistRegion suppRegionBll = new BLL.Shop.Distribution.SuppDistRegion();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region gridView

        public void BindData()
        {
            gridView.DataSetSource = suppRegionBll.GetAllList();
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int SupplierId = Common.Globals.SafeInt(Args[0], 0);
                    int RegionId = Common.Globals.SafeInt(Args[1], 0);
                    if (suppRegionBll.Delete(SupplierId, RegionId))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
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
                    if (gridView.DataKeys[i].Value != null)
                    {
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
        protected string GetSuppName(object target)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            int suppId=Common.Globals.SafeInt(target,0);
            Maticsoft.Model.Shop.Supplier.SupplierInfo infoModel = suppBll.GetModel(suppId);
            return infoModel == null ? "" : infoModel.Name;
        }

        protected string GetRegionName(object target)
        { 
            //0:审核通过、1:作为草稿、2:等待审核。
            int regionId = Common.Globals.SafeInt(target, 0);
            return regionBll.GetAddress(regionId);
        }
    }
}