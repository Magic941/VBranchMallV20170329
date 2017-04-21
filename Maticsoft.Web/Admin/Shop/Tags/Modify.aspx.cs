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

namespace Maticsoft.Web.Admin.Shop.Tags
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 550; } } //Shop_标签管理_编辑页
        Maticsoft.BLL.Shop.Tags.Tags bll = new Maticsoft.BLL.Shop.Tags.Tags();
        Maticsoft.BLL.Shop.Tags.TagCategories bllCate = new BLL.Shop.Tags.TagCategories();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }


        protected int TagID
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

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Tags.Tags model = bll.GetModel(TagID);
            int parentId = 0;
            if (null != model)
            {
                this.lblTagID.Text = model.TagID.ToString();
                this.txtTagName.Text = model.TagName;
                this.Hidden_SelectValue.Value = model.TagCategoryId.ToString();
                parentId=Globals.SafeInt(model.TagCategoryId.ToString(), 0);
                if (parentId > 0)
                {
                    this.txtCategoryText.Text = bllCate.GetModel(parentId).CategoryName.ToString();
                }
                if (model.IsRecommand)
                {
                    this.radlIsRecommand.SelectedValue = "true";
                }
                else
                {
                    this.radlIsRecommand.SelectedValue = "false";
                }
                this.radlStatus.SelectedValue = model.Status.ToString();
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string TagName = this.txtTagName.Text.Trim();
            if (string.IsNullOrWhiteSpace(TagName))
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空");
                return;
            }

            Maticsoft.Model.Shop.Tags.Tags model = bll.GetModel(TagID);
            if (null != model)
            {
                model.TagID = TagID;
                model.TagName = TagName;
                model.TagCategoryId = Globals.SafeInt(this.Hidden_SelectValue.Value, 0);
                model.IsRecommand = bool.Parse(this.radlIsRecommand.SelectedValue);
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);

                if (bll.Update(model))
                {
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改商品标签(id="+model.TagID+")成功", this);
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "List.aspx");
                }
                else
                {
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改商品标签(id=" + model.TagID + ")失败", this);
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateError);
                }
            }

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
