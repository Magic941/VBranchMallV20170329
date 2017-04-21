/**
* Add.cs
*
* 功 能： N/A
* 类 名： Add
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01							N/A    初版
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
namespace Maticsoft.Web.Admin.SNS.ProductSources
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 605; } } //SNS_商品来源管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
            {
                MessageBox.ShowAndBack(this, "您没有权限");
                return;
            }
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtWebSiteName.Text.Trim().Length==0)
			{
				strErr+="商品来源网站的名称不能为空！\\n";	
			}
			if(this.txtWebSiteUrl.Text.Trim().Length==0)
			{
				strErr+="商品来源网站的url不能为空！\\n";	
			}
			if(this.txtWebSiteLogo.Text.Trim().Length==0)
			{
				strErr+="网站的log,在单品也链接到此不能为空！\\n";	
			}
			if(this.txtCategoryTags.Text.Trim().Length==0)
			{
				strErr+="采集时商品类别匹配的正则表达式不能为空！\\n";	
			}
			if(this.txtPriceTags.Text.Trim().Length==0)
			{
				strErr+="采集时商品价格匹配的正则表达式不能为空！\\n";	
			}
			if(this.txtImagesTag.Text.Trim().Length==0)
			{
				strErr+="采集时图片匹配的正则表达式不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtStatus.Text))
			{
				strErr+="状态格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string WebSiteName=this.txtWebSiteName.Text;
			string WebSiteUrl=this.txtWebSiteUrl.Text;
			string WebSiteLogo=this.txtWebSiteLogo.Text;
			string CategoryTags=this.txtCategoryTags.Text;
			string PriceTags=this.txtPriceTags.Text;
			string ImagesTag=this.txtImagesTag.Text;
			int Status=int.Parse(this.txtStatus.Text);

			Maticsoft.Model.SNS.ProductSources model=new Maticsoft.Model.SNS.ProductSources();
			model.WebSiteName=WebSiteName;
			model.WebSiteUrl=WebSiteUrl;
			model.WebSiteLogo=WebSiteLogo;
			model.CategoryTags=CategoryTags;
			model.PriceTags=PriceTags;
			model.ImagesTag=ImagesTag;
			model.Status=Status;

			Maticsoft.BLL.SNS.ProductSources bll=new Maticsoft.BLL.SNS.ProductSources();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowSuccessTip(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
