/**
* Modify.cs
*
* 功 能： N/A
* 类 名： Modify
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
using System.Web.UI;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Categories
{
    public partial class Modify : PageBaseAdmin
    { //区分是否为商品分类还是图片分类
        public int Type
        {
            get
            {
                int type = 0;
                if (Session["CategoryType"] != null)
                {
                    type = Common.Globals.SafeInt(Session["CategoryType"].ToString(), 0);
                }
                return type;
            }
        }


        protected override int Act_PageLoad //SNS_图片分类管理_编辑页
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return 565; //SNS_商品分享分类管理_编辑页
                    case 1:
                        return 562;//SNS_图片分享分类管理_编辑页
                    default:
                        return 565;
                }
            }
        }
       

        private Maticsoft.BLL.SNS.Categories bll = new Maticsoft.BLL.SNS.Categories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (CategoryId > -1)
                {
                    ShowInfo(CategoryId);
                }

                if (Type == 1)
                {
                    this.Literal2.Text = "编辑图片分类信息";
                    this.Literal3.Text = "您可以编辑图片分类信息";
                }
                else
                {
                    this.Literal2.Text = "编辑商品分类信息";
                    this.Literal3.Text = "您可以编辑商品分类信息";
                }
            }
        }

        public int CategoryId
        {
            get
            {
                int _categoryId = -1;
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    _categoryId = Common.Globals.SafeInt(Request.Params["id"], -1);
                }
                return _categoryId;
            }
        }

     
        private void ShowInfo(int CategoryId)
        {
            Maticsoft.BLL.SNS.Categories bll = new Maticsoft.BLL.SNS.Categories();
            Maticsoft.Model.SNS.Categories model = bll.GetModel(CategoryId);
            this.txtName.Text = model.Name;
            this.txtDescription.Text = model.Description;
            this.textFontColor.Text = model.FontColor;
            this.txtSeoTitle.Text = model.Meta_Title;
            this.txtSeoKeywords.Text = model.Meta_Keywords;
            this.txtSeoDescription.Text = model.Meta_Description;
            if (model.MenuIsShow)
            {
                this.rbIsMenuShow.SelectedIndex = 0;
            }
            else
            {
                this.rbIsMenuShow.SelectedIndex = 1;
            }
            if (model.IsMenu)
            {
                this.radlState.SelectedIndex = 0;
            }
            else
            {
                this.radlState.SelectedIndex = 1;
            }
            if (model.Status == 0)
            {
                this.rbIsused.SelectedIndex = 1;
            }
            else
            {
                this.rbIsused.SelectedIndex = 0;
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "分类名称不能为空，在1至60个字符之间");
                return;
            }
            string Name = this.txtName.Text;
            string Description = this.txtDescription.Text;
            string fontColor = this.textFontColor.Text;

            Maticsoft.Model.SNS.Categories model = bll.GetModel(CategoryId);
            model.Name = Name;
            model.Description = Description;
            model.FontColor = fontColor;
            model.Status = Globals.SafeInt(rbIsused.SelectedValue, 0);
            model.MenuIsShow = Globals.SafeBool(rbIsMenuShow.SelectedValue, false);
            model.IsMenu = Globals.SafeBool(radlState.SelectedValue, false);
            if (bll.Update(model))
            {
                //清空缓存
                Cache.Remove("GetAllCateByCache-" + Type);
                if (Type ==1)
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx?type=1");
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx");
                }
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改商品类别(CateGoryID="+model.CategoryId+")成功!", this);
            }
            else
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowSuccessTip(this, "保存失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改商品类别(CateGoryID=" + model.CategoryId + ")失败!", this);
            }

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            if (Type == 1)
            {
                Response.Redirect("list.aspx?type=1");
            }
            else
            {
                Response.Redirect("list.aspx");
            }
        }
    }
}