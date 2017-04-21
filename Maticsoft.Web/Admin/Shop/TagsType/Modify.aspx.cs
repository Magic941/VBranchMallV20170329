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
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.TagsType
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 544; } } //Shop_标签分类管理_编辑页
        private Maticsoft.BLL.Shop.Tags.TagCategories bll = new BLL.Shop.Tags.TagCategories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        protected int Id
        {
            get
            {
                int id = 0;
                string strId = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strId))
                {
                    id = Globals.SafeInt(strId, 0);
                }
                return id;
            }
        }

        protected int ParentCategoryId
        {
            get
            {
                int id = 0;
                string strId = Request.Params["ParentCategoryId"];
                if (!string.IsNullOrEmpty(strId))
                {
                    id = Globals.SafeInt(strId, 0);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Tags.TagCategories model = bll.GetModel(Id);
            if (null != model)
            {
                string parentName = "";
                int parentId = model.ParentCategoryId ?? 0;
                if (model.ParentCategoryId > 0)
                {
                    parentName = bll.GetFullNameByCache(parentId);
                }
                else
                {
                    parentName = "无上级标签";
                }
                this.lblParentCate.Text = parentName;
                this.lblID.Text = model.ID.ToString();
                this.txtTypeName.Text = model.CategoryName;
                this.txtRemark.Text = Globals.SafeString(model.Remark, "");

                if (model.Status.HasValue)
                {
                    this.radlStatus.SelectedValue = model.Status.ToString();
                }
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string CategoryName = this.txtTypeName.Text.Trim();

            if (string.IsNullOrWhiteSpace(CategoryName) )
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

            Maticsoft.Model.Shop.Tags.TagCategories model = bll.GetModel(Id);
            if (null != model)
            {
                model.CategoryName = CategoryName;
                model.Remark = Remark;
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);

                if (bll.Update(model))
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "List.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}