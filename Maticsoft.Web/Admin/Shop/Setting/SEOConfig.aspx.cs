using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;
using Maticsoft.Web.Components.Setting.Shop;

namespace Maticsoft.Web.Admin.Shop.Setting
{
    public partial class SEOConfig : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 515; } } //Shop_SEO优化页
        protected new int Act_UpdateData = 516;    //Shop_SEO优化_编辑数据
        ApplicationKeyType applicationKeyType = ApplicationKeyType.Shop;
        Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
        Maticsoft.BLL.CMS.ContentClass classBll = new BLL.CMS.ContentClass();
        private string[] baseNames = new string[]
            {
                "Home",             //首页
               // "About",      //关于我们
               // "Contact",    //联系我们
            };

        //private string[] imageName = new string[]
        //{
           
        //        "ProductImage",        //产品图片
        //};
        //private string[] selfName = new string[]
        //{
        //         "ProductSelfImage",      //产品图片自定义
        //            "CategorySelf",       //产品类别自定义
        //                 "ProductSelf"          //产品自定义
        //};

        private string[] hasUrlPage = new string[] { 
                 //"CMS"   ,      //新闻列表
                 "Category",      //产品类别
                 "Product"     //产品页面
        };
        private Dictionary<string, PageSetting> pageSettings;//基本的SEO设置

        #region 加载数据
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //是否有编辑信息的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnSave.Visible = false;
                }

                ////产品详细页选项卡的分类
                //ddProduct.DataSource = cateBll.GetList(" HasChildren ='false'");
                //this.ddProduct.DataTextField = "Name";
                //this.ddProduct.DataValueField = "CategoryId";
                //this.ddProduct.DataBind();
                //ddProduct.Items.Insert(0, new ListItem("请选择", "0"));
                //产品类别页选项卡的分类
                //ddCategory.DataSource = cateBll.GetAllList();
                //this.ddCategory.DataTextField = "Name";
                //this.ddCategory.DataValueField = "CategoryId";
                //this.ddCategory.DataBind();
                //ddCategory.Items.Insert(0, new ListItem("请选择", "0"));
                BoundData();
                //CMS栏目
                //ddNewsCate.DataSource = classBll.GetAllList();
                //this.ddNewsCate.DataTextField = "ClassName";
                //this.ddNewsCate.DataValueField = "ClassID";
                //this.ddNewsCate.DataBind();
                //ddNewsCate.Items.Insert(0, new ListItem("请选择", "0"));

                //产品图片页选项卡的分类
                //this.ddCateImage.DataSource = cateBll.GetList(" HasChildren ='false'");
                //this.ddCateImage.DataTextField = "Name";
                //this.ddCateImage.DataValueField = "CategoryId";
                //this.ddCateImage.DataBind();
                //ddCateImage.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }


        private void LoadPageSetting()
        {
            pageSettings = new Dictionary<string, PageSetting>();
            foreach (string name in baseNames)
            {
                pageSettings.Add(name, new PageSetting(name, applicationKeyType));
                //if (name == "CategorySelf")
                //{
                //    int cid = Common.Globals.SafeInt(this.ParentCate.SelectedValue, 0);
                //    pageSettings.Add(name, new PageSetting(name, applicationKeyType, "Base"));
                //}
                //if (name == "ProductSelf")
                //{
                //    int cid = Common.Globals.SafeInt(this.ProductCate.SelectedValue, 0);
                //    pageSettings.Add(name, new PageSetting(name, applicationKeyType, "Base"));
                //}
            }
            //foreach (string name in imageName)
            //{
            //    pageSettings.Add(name, new PageSetting(name, applicationKeyType, "Image"));
            //}
            foreach (string name in hasUrlPage)
            {
                pageSettings.Add(name, new PageSetting(name, applicationKeyType, "Url"));
            }
        }

        private void LoadBaseTextBox(string pageName,
            TextBox txtTitle,
            TextBox txtKeyWords,
            TextBox txtDes,
            TextBox txtUrl = null)
        {
            txtTitle.Text = pageSettings[pageName].Title;
            txtKeyWords.Text = pageSettings[pageName].Keywords;
            txtDes.Text = pageSettings[pageName].Description;
            if (txtUrl != null)
            {
                txtUrl.Text = pageSettings[pageName].Url;

            }
        }

        //private void LoadImageTextBox(string pageName,
        //  TextBox txtAlt,
        //  TextBox txtImageTitle)
        //{
        //    txtAlt.Text = pageSettings[pageName].Alt;
        //    txtImageTitle.Text = pageSettings[pageName].ImageTitle;
        //}

        private string txtTitleId = "txt{0}Title";
        private string txtKeywordsId = "txt{0}Keywords";
        private string txtDesId = "txt{0}Des";
        private string txtUrl = "txt{0}Url";

       // private string txtAlt = "txt{0}Alt";
        //private string txtImageTitle = "txt{0}Title";
        private void BoundData()
        {
            LoadPageSetting();
            foreach (string pageName in pageSettings.Keys)
            {
                if (!pageSettings[pageName].IsImage)
                {
                    TextBox textUrl = null;
                    if (Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtUrl, pageName)) != null)
                    {
                        textUrl = Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtUrl, pageName)) as TextBox;
                    }
                    LoadBaseTextBox(pageName,
                                Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtTitleId, pageName)) as TextBox,
                                Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtKeywordsId, pageName)) as TextBox,
                                Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtDesId, pageName)) as TextBox,
                                   textUrl);
                }
                else
                {
                    //LoadImageTextBox(pageName, Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtAlt, pageName)) as TextBox,
                    //    Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtImageTitle, pageName)) as TextBox);
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }
        #endregion

        #region dropDown IndexChange  事件

        protected void ddProduct_IndexChange(object sender, EventArgs e)
        {
            this.TabIndex.Value = "3";
            //Maticsoft.BLL.Shop.Products.
        }

        protected void ddCategory_IndexChange(object sender, EventArgs e)
        {
            int cateId = Common.Globals.SafeInt(this.ddCategory.SelectedValue, 0);
            if (cateId > 0)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo cateModel = cateBll.GetModel(cateId);
                this.txtCategorySelfDes.Text = cateModel.Meta_Description;
                this.txtCategorySelfKeywords.Text = cateModel.Meta_Keywords;
                this.txtCategorySelfTitle.Text = cateModel.Meta_Title;
                this.txtCategorySelfUrl.Text = cateModel.SeoUrl;
                this.TabIndex.Value = "2";
            }
        }

        protected void ddNewsCate_IndexChange(object sender, EventArgs e)
        {
            int NewsCateId = Common.Globals.SafeInt(this.ddNewsCate.SelectedValue ,0);
            if(NewsCateId>0)
            {
                Maticsoft.Model.CMS.ContentClass classModel = classBll.GetModel(NewsCateId);
                this.txtCMSSelfTitle.Text = classModel.Meta_Title;
                this.txtCMSSelfDec.Text = classModel.Meta_Description;
                this.txtCMSSelfKeyword.Text = classModel.Meta_Keywords;
                this.txtCMSSelfUrl.Text = classModel.SeoUrl;
                this.TabIndex.Value = "5";
            }
        }

        protected void ddCateImage_IndexChange(object sender, EventArgs e)
        {
            int cateId = Common.Globals.SafeInt(this.ddCateImage.SelectedValue, 0);
            if (cateId > 0)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo cateModel = cateBll.GetModel(cateId);
                this.txtProductSelfImageAlt.Text = cateModel.SeoImageAlt;
                this.txtProductSelfImageTitle.Text = cateModel.SeoImageTitle;
                this.TabIndex.Value = "4";
            }
        }
        #endregion

        #region 保存数据
        private void SaveTextBox(string pageName, TextBox txtTitle, TextBox txtKeyWords, TextBox txtDes, TextBox txtUrl)
        {
            pageSettings[pageName].Title = Globals.HtmlEncode(txtTitle.Text.Trim().Replace("\n", ""));
            pageSettings[pageName].Keywords = Globals.HtmlEncode(txtKeyWords.Text.Trim().Replace("\n", ""));
            pageSettings[pageName].Description = Globals.HtmlEncode(txtDes.Text.Trim().Replace("\n", ""));
            if (txtUrl != null)
            {
                pageSettings[pageName].Url = Globals.HtmlEncode(txtUrl.Text.Trim().Replace("\n", ""));
            }
        }

        //private void SaveImageBox(string pageName, TextBox txtAlt,
        //  TextBox txtImageTitle)
        //{
        //    pageSettings[pageName].Alt = Globals.HtmlEncode(txtAlt.Text.Trim().Replace("\n", ""));
        //    pageSettings[pageName].ImageTitle = Globals.HtmlEncode(txtImageTitle.Text.Trim().Replace("\n", ""));
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 自定义类别
           // int cateId = Common.Globals.SafeInt(this.ddCategory.SelectedValue, 0);
            //if (cateId > 0)
            //{
            //    Maticsoft.Model.Shop.Products.CategoryInfo cateModel = cateBll.GetModel(cateId);
            //    cateModel.Meta_Description = this.txtCategorySelfDes.Text;
            //    cateModel.Meta_Keywords = this.txtCategorySelfKeywords.Text;
            //    cateModel.Meta_Title = this.txtCategorySelfTitle.Text;
            //    cateModel.SeoUrl = this.txtCategorySelfUrl.Text;
            //    cateBll.Update(cateModel);
            //    this.TabIndex.Value = "2";
            //}
            #endregion

            #region 自定义产品
            //int cateProductId = Common.Globals.SafeInt(this.ddCategory.SelectedValue, 0);
            //if (cateProductId > 0)
            //{
            //    Maticsoft.Model.Shop.Products.CategoryInfo cateProductModel = cateBll.GetModel(cateProductId);
            //    cateProductModel.Meta_Description = this.txtProductSelfDes.Text;
            //    cateProductModel.Meta_Keywords = this.txtProductSelfKeywords.Text;
            //    cateProductModel.Meta_Title = this.txtProductSelfTitle.Text;
            //    cateProductModel.SeoUrl = this.txtProductSelfUrl.Text;
            //    cateBll.Update(cateProductModel);
            //    this.TabIndex.Value = "3";
            //}
            #endregion

            #region 自定义新闻
            //int newsCateId = Common.Globals.SafeInt(ddNewsCate.SelectedValue, 0);
            //if (newsCateId>0)
            //{
            //    Maticsoft.Model.CMS.ContentClass classModel = classBll.GetModel(newsCateId);
            //    classModel.Meta_Title = this.txtCMSSelfTitle.Text;
            //    classModel.Meta_Keywords = this.txtCMSSelfKeyword.Text;
            //    classModel.Meta_Description = this.txtCMSSelfDec.Text;
            //    classModel.SeoUrl = this.txtCMSSelfUrl.Text;
            //    classBll.Update(classModel);
            //}
            #endregion

            #region 自定义图片优化
            //int ImageCateId = Common.Globals.SafeInt(this.ddCateImage.SelectedValue, 0);
            //if (newsCateId > 0)
            //{
            //    Maticsoft.Model.Shop.Products.CategoryInfo cateImageModel = cateBll.GetModel(ImageCateId);
            //    cateImageModel.SeoImageAlt = this.txtProductSelfImageAlt.Text;
            //    cateImageModel.SeoImageTitle = this.txtProductSelfImageTitle.Text;
            //    cateBll.Update(cateImageModel);
            //}
            #endregion
            LoadPageSetting();  //保存时, 重新加载page数据
            try
            {
                foreach (string pageName in pageSettings.Keys)
                {
                    if (!pageSettings[pageName].IsImage)
                    {
                        SaveTextBox(pageName,
                                    Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtTitleId, pageName)) as TextBox,
                                    Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtKeywordsId, pageName)) as TextBox,
                                    Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtDesId, pageName)) as TextBox,
                        Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtUrl, pageName)) as TextBox);
                    }
                    else
                    {
                       // SaveImageBox(pageName, Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtAlt, pageName)) as TextBox,
                     // Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtImageTitle, pageName)) as TextBox);
                    }
                }

                Cache.Remove("ConfigSystemHashList_" + applicationKeyType);//清除网站设置的缓存文件
                this.btnSave.Enabled = false;
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "设置SEO数据成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "SEOConfig.aspx");
            }
            catch (Exception)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "设置SEO数据失败", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "SEOConfig.aspx");
            }
        }
        #endregion

    }
}