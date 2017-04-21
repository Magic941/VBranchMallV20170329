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

namespace Maticsoft.Web.Admin.SNS.Categories
{
    public partial class Add : PageBaseAdmin
    {    //区分是否为商品分类还是图片分类
        public int type
        {
            get
            {
                int _type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    _type = Globals.SafeInt(Request.Params["type"], 0);
                }
                return _type;
            }
        }

        protected override int Act_PageLoad //SNS_图片分类管理_添加页
        {
            get
            {
                switch (type)
                {
                    case 0: 
                        return 564;//SNS_商品分享分类管理_添加页
                    case 1:
                        return 561;//SNS_图片分享分类管理_添加页
                    default:
                        return 564;
                }
            }
        }
       

        private Maticsoft.BLL.SNS.Categories bll = new BLL.SNS.Categories();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (type > 0)
            {
                this.SNSCategory.Visible = false;
                this.Literal2.Text = "添加图片分类";
                this.Literal3.Text = "为不同类型的图片创建不同的分类，方便您管理也方便顾客浏览 ";
            }
            else
            {
                this.PhotoCategory.Visible = false;
                this.Literal2.Text = "添加商品分类";
                this.Literal3.Text = "为不同类型的商品创建不同的分类，方便您管理也方便顾客浏览 ";
            }
        }

    
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
            this.btnCancle.Enabled = true;
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "分类名称不能为空，在1至60个字符之间");
                return;
            }

            Maticsoft.Model.SNS.Categories model = new Model.SNS.Categories();
            model.Name = this.txtName.Text;
            model.Description = this.txtDescription.Text;
            if (type == 1)
            {
                if (!string.IsNullOrWhiteSpace(this.PhotoCategory.SelectedValue.Trim()))
                {
                    model.ParentID = int.Parse(this.PhotoCategory.SelectedValue);
                }
                else
                {
                    model.ParentID = 0;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.SNSCategory.SelectedValue.Trim()))
                {
                    model.ParentID = int.Parse(this.SNSCategory.SelectedValue);
                }
                else
                {
                    model.ParentID = 0;
                }
            }
            model.HasChildren = false;
            model.IsMenu = Globals.SafeBool(radlState.SelectedValue, false);
            model.CreatedUserID = CurrentUser.UserID;
            model.MenuIsShow = Globals.SafeBool(rbIsMenuShow.SelectedValue, false);
            model.MenuSequence = -1;
            model.Type = type;//（0:为商品分类 1：图片分类）
            model.Status = Globals.SafeInt(rbIsused.SelectedValue, 0);
            model.FontColor = this.textFontColor.Text.Trim();
            //SEO 
            model.Meta_Title = this.txtSeoTitle.Text;
            model.Meta_Keywords = this.txtSeoKeywords.Text;
            model.Meta_Description = this.txtSeoDescription.Text;

            if (bll.AddCategories(model))
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                //清空缓存
                Cache.Remove("GetAllCateByCache-" + type);
                if (Session["CategoryType"] != null && Session["CategoryType"].ToString() == "1")
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...！", "list.aspx?type=1");
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...！", "list.aspx");
                }
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加商品类别成功!", this);
            }
            else
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowSuccessTip(this, "添加失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加商品类别失败!", this);
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx?type=" + type);
        }
    }
}