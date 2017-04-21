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
namespace Maticsoft.Web.Admin.Shop.RechargeCards
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
					int ID=(Convert.ToInt32(strid));
					ShowInfo(ID);
				}
			}
		}
		
	private void ShowInfo(int ID)
	{
		Maticsoft.BLL.Shop.RechargeCards.RechargeCards bll=new Maticsoft.BLL.Shop.RechargeCards.RechargeCards();
		Maticsoft.Model.Shop.RechargeCards model=bll.GetModel(ID);
		//this.lblID.Text=model.ID.ToString();
		this.lblNumber.Text=model.Number;
		this.lblPassword.Text=model.Password;
		this.lblAmount.Text=model.Amount.ToString();
		this.lblCreatedUserId.Text=model.CreatedUserId.ToString();
		this.lblCreatedDate.Text=model.CreatedDate.ToString();
		this.lblUsedUserId.Text=string.IsNullOrWhiteSpace(model.UsedUserId.ToString())?"未使用":model.UsedUserId.ToString();
		this.lblUsedDate.Text=string.IsNullOrWhiteSpace(model.UsedDate.ToString())?"未使用":model.UsedDate.ToString();
		this.lblStatus.Text=model.Status.ToString().Equals("1")?"已使用":"未使用";
		this.lblRemark.Text=model.Remark;

	}

    public void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }
    }

    
}
