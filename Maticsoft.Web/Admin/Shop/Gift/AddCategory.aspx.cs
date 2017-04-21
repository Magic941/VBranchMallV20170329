/**
* AddCategory.cs
*
* 功 能： [N/A]
* 类 名： AddCategory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/23 12:23:19  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Gift
{
    public partial class AddCategory : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 432; } } //Shop_礼品分类管理_添加页
        Maticsoft.BLL.Shop.Gift.GiftsCategory bll = new BLL.Shop.Gift.GiftsCategory();
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
            Maticsoft.Model.Shop.Gift.GiftsCategory model = new Maticsoft.Model.Shop.Gift.GiftsCategory();
            model.Name = this.txtName.Text;
            model.Description = this.txtDescription.Text;
            if (!string.IsNullOrWhiteSpace(this.DropParentId.SelectedValue.Trim()))
            {
                model.ParentCategoryId = int.Parse(this.DropParentId.SelectedValue);
            }
            else
            {
                model.ParentCategoryId = 0;
            }
            model.Depth = 0;
            model.Path = "";

            if (bll.AddCategory(model))
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...", "CategoryList.aspx");
            }
            else
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowFailTip(this, "添加失败！");
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoryList.aspx");
        }
    }
}
