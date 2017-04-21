/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
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
    public partial class CategoryAdd : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 458; } } //Shop_包装类别管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    MessageBox.ShowFailTip(this,"您没有此权限");
                    return;
                }
               
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
		{

            string Name = this.txtName.Text.Trim();
            if (Name.Length == 0)
			{
                MessageBox.ShowServerBusyTip(this, "包装类型的名称不能为空！");
                return;
			}
            string Remark=this.txtRemark.Text.Trim();
            if (Remark.Length > 200)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能超过200个字符！");
                return;
            }

            Maticsoft.Model.Shop.Package.PackageCategory model = new Maticsoft.Model.Shop.Package.PackageCategory();
			model.Name=Name;
            model.Remark = Remark;
			Maticsoft.BLL.Shop.Package.PackageCategory bll=new Maticsoft.BLL.Shop.Package.PackageCategory();
            int ID;
            if ((ID=bll.Add(model))> 0)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加包装类型(id=" + ID + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "CategoryList.aspx");
                Response.Redirect("CategoryList.aspx");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加包装类型(id=" + ID + ")失败", this);
                MessageBox.ShowServerBusyTip(this, Resources.Site.TooltipSaveError);
            }

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoryList.aspx");
        }
    }
}
