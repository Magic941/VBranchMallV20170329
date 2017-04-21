/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
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
namespace Maticsoft.Web.Admin.Shop.Shippers
{
    public partial class Show : PageBaseAdmin
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int ShipperId=(Convert.ToInt32(strid));
					ShowInfo(ShipperId);
				}
			}
		}
		
	private void ShowInfo(int ShipperId)
	{
		Maticsoft.BLL.Shop.Shippers bll=new Maticsoft.BLL.Shop.Shippers();
		Maticsoft.Model.Shop.Shippers model=bll.GetModel(ShipperId);
		this.lblShipperId.Text=model.ShipperId.ToString();
		this.lblIsDefault.Text=model.IsDefault?"是":"否";
		this.lblShipperTag.Text=model.ShipperTag;
		this.lblShipperName.Text=model.ShipperName;
		this.lblRegionId.Text=model.RegionId.ToString();
		this.lblAddress.Text=model.Address;
		this.lblCellPhone.Text=model.CellPhone;
		this.lblTelPhone.Text=model.TelPhone;
		this.lblZipcode.Text=model.Zipcode;
		this.lblRemark.Text=model.Remark;

	}


    }

    //public void btnCancle_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("list.aspx");
    //}
}
