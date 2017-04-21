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

namespace Maticsoft.Web.Admin.Shop.Tags
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 549; } } //Shop_标签管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private Maticsoft.BLL.Shop.Tags.Tags bll = new BLL.Shop.Tags.Tags();

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string TagName = this.txtTagName.Text.Trim();
            if (string.IsNullOrWhiteSpace(TagName))
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空！");
                return;
            }

            Maticsoft.Model.Shop.Tags.Tags model = new Model.Shop.Tags.Tags();
            this.Hidden_SelectName.Value = this.txtCategoryText.Text;
            if (!string.IsNullOrWhiteSpace(this.Hidden_SelectValue.Value))
            {
                model.TagCategoryId = Globals.SafeInt(this.Hidden_SelectValue.Value, 0);
            }
            else
            {
                model.TagCategoryId = 0;
            }
            model.TagName = TagName;
            model.IsRecommand = bool.Parse(this.radlIsRecommand.SelectedValue);
            model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
            if (bll.Add(model) > 0)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "增加商品标签成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}