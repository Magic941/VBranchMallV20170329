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
using Maticsoft.Web;

namespace Maticsoft.Web.Admin.Shop.Package
{
    public partial class CategoryModify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 447; } } //Shop_包装类别管理_编辑页
        Maticsoft.BLL.Shop.Package.PackageCategory bll = new Maticsoft.BLL.Shop.Package.PackageCategory();
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!Page.IsPostBack)
            {
              
                    ShowInfo(Id);
            
            }
		}


        public int Id
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }
      private void ShowInfo(int CategoryId)
	{
        Maticsoft.BLL.Shop.Package.PackageCategory bll = new Maticsoft.BLL.Shop.Package.PackageCategory();
        Maticsoft.Model.Shop.Package.PackageCategory model = bll.GetModel(CategoryId);
        this.txtName.Text = model.Name;
        this.txtRemark.Text = model.Remark;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{

            string Name = this.txtName.Text.Trim();
            if (Name.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "专辑类型的名称不能为空！");
                return;
            }
            string Remark = this.txtRemark.Text.Trim();
            if (Remark.Length > 200)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能超过200个字符！");
                return;
            }
			 Maticsoft.Model.Shop.Package.PackageCategory model = bll.GetModel(Id);
             if (null != model)
             {
                 model.Name = Name;
                 model.Remark = Remark;             
                 if (bll.Update(model))
                 {
                     LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改包装类型(id=" + model.CategoryId + ")成功", this);
                     MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "Categorylist.aspx");
                 }
                 else
                 {
                     LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改专辑类型(id=" + model.CategoryId + ")失败", this);
                     MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                 }
             }

		}
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categorylist.aspx");
        }
    }
}
