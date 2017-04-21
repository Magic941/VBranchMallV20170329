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
using System.Collections;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 529; } } //Shop_商品分类管理_编辑页
        private Maticsoft.BLL.Shop.Products.CategoryInfo bll = new Maticsoft.BLL.Shop.Products.CategoryInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int CategoryId = (Convert.ToInt32(Request.Params["id"]));
                    ShowInfo(CategoryId);
                }
            }
        }

        #region 分类ID
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid))
                {
                    id = Globals.SafeInt(strid, 0);
                }
                return id;
            }
        }
        #endregion

        private void ShowInfo(int CategoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo model = bll.GetModel(CategoryId);
            this.ddlCateList.SelectedValue = model.ParentCategoryId.ToString();
            this.txtName.Text = model.Name;
            this.txtMeta_Description.Text = model.Meta_Description;
            this.txtMeta_Keywords.Text = model.Meta_Keywords;
            this.txtDescription.Text = model.Description;
            this.txtRewriteName.Text = model.RewriteName;
            this.txtSKUPrefix.Text = model.SKUPrefix;
            this.txtAssociatedProductType.Text = model.AssociatedProductType.ToString();
            this.txtMeta_Title.Text = model.Meta_Title;
            this.HiddenField_OldPath.Value = model.ImageUrl;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "分类名称不能为空，在1至60个字符之间");
                return;
            }
            int ParentCategoryId =Common.Globals.SafeInt(this.ddlCateList.SelectedValue,0) ;
            if (ParentCategoryId == CategoryId)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "不能选自己作为父分类");
                return;
            }
        
            string Name = this.txtName.Text;
            string Meta_Description = this.txtMeta_Description.Text;
            string Meta_Keywords = this.txtMeta_Keywords.Text;
            string Description = this.txtDescription.Text;
            string RewriteName = this.txtRewriteName.Text;
            string SKUPrefix = this.txtSKUPrefix.Text;

            Maticsoft.Model.Shop.Products.CategoryInfo model = bll.GetModel(CategoryId);
            if (bll.IsExisted(ParentCategoryId,Name,model.CategoryId ))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "该分类下已存在同名分类");
                return;
            }
            model.ParentCategoryId = ParentCategoryId;
            model.Name = Name;
            model.Meta_Description = Meta_Description;
            model.Meta_Keywords = Meta_Keywords;
            model.Description = Description;
            model.RewriteName = RewriteName;
            model.SKUPrefix = SKUPrefix;

            model.AssociatedProductType = -1;
            model.Meta_Title = this.txtMeta_Title.Text;

            //待上传的图片名称
            string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
            string ImageFile = "/Upload/Shop/Images/Categories";
            ArrayList imageList = new ArrayList();
            if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
            {
                string imageUrl = string.Format(this.HiddenField_ICOPath.Value, "");
                imageList.Add(imageUrl.Replace(tempFile, ""));
                model.ImageUrl = imageUrl.Replace(tempFile, ImageFile);
            }
            else
            {
                model.ImageUrl = this.HiddenField_OldPath.Value;
            }
            if (bll.UpdateCategory(model))
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                {
                    //将图片从临时文件夹移动到正式的文件夹下
                    FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);
                }
                //清空缓存
                Cache.Remove("GetAllCateList-CateList");
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功，正在跳转列表页...", "list.aspx");
            }
            else
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存失败", "list.aspx");
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}