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
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.TagsType
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 543; } } //Shop_标签分类管理_添加页

        private Maticsoft.BLL.Shop.Tags.TagCategories bll = new BLL.Shop.Tags.TagCategories();
        private Maticsoft.Model.Shop.Tags.TagCategories model = new Model.Shop.Tags.TagCategories();

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
            string CategoryName = this.txtTypeName.Text.Trim();

            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                MessageBox.ShowServerBusyTip(this, "标签类型名称不能为空！");
                return;
            }
            if (CategoryName.Length > 50)
            {
                MessageBox.ShowServerBusyTip(this, "标签类型名称不能大于50个字符！");
                return;
            }
            string Remark = this.txtRemark.Text.Trim();
            if (Remark.Length > 100)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能大于100个字符！");
                return;
            }
            this.Hidden_SelectName.Value = this.txtCategoryText.Text;
            if (!string.IsNullOrWhiteSpace(this.Hidden_SelectValue.Value))
            {
                model.ParentCategoryId = Globals.SafeInt(this.Hidden_SelectValue.Value, 0);
            }
            else
            {
                model.ParentCategoryId = 0;
            }
            model.CategoryName = CategoryName;
            model.Remark = Remark;
            model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);

            if (bll.CreateCategory(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加标签类型成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加标签类型失败", this);
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}