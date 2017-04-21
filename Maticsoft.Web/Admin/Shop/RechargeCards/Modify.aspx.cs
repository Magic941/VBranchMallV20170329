/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify.cs
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
using Maticsoft.Common;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Shop.RechargeCards
{
    public partial class Modify : PageBaseAdmin
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int ID=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(ID);
				}
			}
		}
			
	private void ShowInfo(int ID)
	{
		Maticsoft.BLL.Shop.RechargeCards.RechargeCards bll=new Maticsoft.BLL.Shop.RechargeCards.RechargeCards();
		Maticsoft.Model.Shop.RechargeCards model=bll.GetModel(ID);
		this.lblID.Text=model.ID.ToString();
		this.txtNumber.Text=model.Number;
		this.txtPassword.Text=model.Password;
		this.txtAmount.Text=model.Amount.ToString();
		this.txtCreatedUserId.Text=model.CreatedUserId.ToString();
		this.txtCreatedDate.Text=model.CreatedDate.ToString();
		this.txtUsedUserId.Text=model.UsedUserId.ToString();
		this.txtUsedDate.Text=model.UsedDate.ToString();
		this.txtStatus.Text=model.Status.ToString();
		this.txtRemark.Text=model.Remark;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtNumber.Text.Trim().Length==0)
			{
				strErr+="Number不能为空！\\n";	
			}
			if(this.txtPassword.Text.Trim().Length==0)
			{
				strErr+="Password不能为空！\\n";	
			}
			if(!PageValidate.IsDecimal(txtAmount.Text))
			{
				strErr+="Amount格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtCreatedUserId.Text))
			{
				strErr+="CreatedUserId格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtCreatedDate.Text))
			{
				strErr+="CreatedDate格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtUsedUserId.Text))
			{
				strErr+="UsedUserId格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtUsedDate.Text))
			{
				strErr+="UsedDate格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtStatus.Text))
			{
				strErr+="Status格式错误！\\n";	
			}
			if(this.txtRemark.Text.Trim().Length==0)
			{
				strErr+="Remark不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int ID=int.Parse(this.lblID.Text);
			string Number=this.txtNumber.Text;
			string Password=this.txtPassword.Text;
			decimal Amount=decimal.Parse(this.txtAmount.Text);
			int CreatedUserId=int.Parse(this.txtCreatedUserId.Text);
			DateTime CreatedDate=DateTime.Parse(this.txtCreatedDate.Text);
			int UsedUserId=int.Parse(this.txtUsedUserId.Text);
			DateTime UsedDate=DateTime.Parse(this.txtUsedDate.Text);
			int Status=int.Parse(this.txtStatus.Text);
			string Remark=this.txtRemark.Text;


			Maticsoft.Model.Shop.RechargeCards model=new Maticsoft.Model.Shop.RechargeCards();
			model.ID=ID;
			model.Number=Number;
			model.Password=Password;
			model.Amount=Amount;
			model.CreatedUserId=CreatedUserId;
			model.CreatedDate=CreatedDate;
			model.UsedUserId=UsedUserId;
			model.UsedDate=UsedDate;
			model.Status=Status;
			model.Remark=Remark;

			Maticsoft.BLL.Shop.RechargeCards.RechargeCards bll=new Maticsoft.BLL.Shop.RechargeCards.RechargeCards();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
