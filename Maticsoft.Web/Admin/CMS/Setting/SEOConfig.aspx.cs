using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.CMS;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.Setting
{

    public partial class SEOConfig : PageBaseAdmin
    {
 
        ApplicationKeyType applicationKeyType = ApplicationKeyType.CMS;
        Maticsoft.BLL.CMS.ContentClass classBll = new BLL.CMS.ContentClass();
        private string[] baseNames = new string[]
            {
                "Home",             //首页
                //"About",      //关于我们
                //"Contact",    //联系我们
            };

        private string[] imageName = new string[]
        {
                //"CMSImage",        //产品图片
        };
        private string[] selfName = new string[]
        {
                 //"ProductSelfImage",      //产品图片自定义
                //  "CategorySelf",     //产品类别自定义
                   //      "ProductSelf"          //产品自定义
        };
        
        private string[] hasUrlPage = new string[] { 
                  "CMS"   ,      //文章详细
                  "CMSSelf"      //文章列表
        };
        private Dictionary<string, PageSetting> pageSettings;//基本的SEO设置

        protected override int Act_PageLoad { get { return 116; } } //运营管理_是否显示SEO优化配置页面
        protected new int Act_UpdateData = 117;    //运营管理_SEO优化设置_编辑SEO优化信息
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
                BoundData();
                ////CMS栏目
                //ddNewsCate.DataSource = classBll.GetAllList();
                //this.ddNewsCate.DataTextField = "ClassName";
                //this.ddNewsCate.DataValueField = "ClassID";
                //this.ddNewsCate.DataBind();
                //ddNewsCate.Items.Insert(0, new ListItem("请选择", "0"));

                //设置文章图片的CMS栏目
                //this.ddCateImage.DataSource = classBll.GetAllList();
                //this.ddCateImage.DataTextField = "ClassName";
                //this.ddCateImage.DataValueField = "ClassID";
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

        private void LoadImageTextBox(string pageName,
          TextBox txtAlt,
          TextBox txtImageTitle)
        {
            txtAlt.Text = pageSettings[pageName].Alt;
            txtImageTitle.Text = pageSettings[pageName].ImageTitle;
        }

        private string txtTitleId = "txt{0}Title";
        private string txtKeywordsId = "txt{0}Keywords";
        private string txtDesId = "txt{0}Des";
        private string txtUrl = "txt{0}Url";

        private string txtAlt = "txt{0}Alt";
        private string txtImageTitle = "txt{0}Title";
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
                    LoadImageTextBox(pageName, Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtAlt, pageName)) as TextBox,
                        Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtImageTitle, pageName)) as TextBox);
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
            //Maticsoft.BLL.Shop.Products.   ddCateImage
        }

        protected void ddNewsCate_IndexChange(object sender, EventArgs e)
        {
            int NewsCateId = Common.Globals.SafeInt(this.ddNewsCate.SelectedValue, 0);
            if (NewsCateId > 0)
            {
                Maticsoft.Model.CMS.ContentClass classModel = classBll.GetModel(NewsCateId);
                this.txtCMSSelfTitle.Text = classModel.Meta_Title;
                this.txtCMSSelfDes.Text = classModel.Meta_Description;
                this.txtCMSSelfKeywords.Text = classModel.Meta_Keywords;
                this.txtCMSSelfUrl.Text = classModel.SeoUrl;
                this.TabIndex.Value = "1";
            }
        }
        protected void ddCateImage_IndexChange(object sender, EventArgs e)
        {
            int NewsCateId = Common.Globals.SafeInt(this.ddCateImage.SelectedValue, 0);
            if (NewsCateId > 0)
            {
                Maticsoft.Model.CMS.ContentClass classModel = classBll.GetModel(NewsCateId);
                this.txtCMSSelfImageAlt.Text = classModel.SeoImageAlt;
                this.txtCMSSelfImageTitle.Text = classModel.SeoImageTitle;
                this.TabIndex.Value = "3";
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

        private void SaveImageBox(string pageName, TextBox txtAlt,
          TextBox txtImageTitle)
        {
            pageSettings[pageName].Alt = Globals.HtmlEncode(txtAlt.Text.Trim().Replace("\n", ""));
            pageSettings[pageName].ImageTitle = Globals.HtmlEncode(txtImageTitle.Text.Trim().Replace("\n", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //#region 自定义新闻
            //int newsCateId = Common.Globals.SafeInt(ddNewsCate.SelectedValue, 0);
            //if (newsCateId > 0)
            //{
            //    Maticsoft.Model.CMS.ContentClass classModel = classBll.GetModel(newsCateId);
            //    classModel.Meta_Title = this.txtCMSSelfTitle.Text;
            //    classModel.Meta_Keywords = this.txtCMSSelfKeyword.Text;
            //    classModel.Meta_Description = this.txtCMSSelfDec.Text;
            //    classModel.SeoUrl = this.txtCMSSelfUrl.Text;
            //    classBll.Update(classModel);
            //}
            //#endregion
            
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
                        SaveImageBox(pageName, Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtAlt, pageName)) as TextBox,
                      Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtImageTitle, pageName)) as TextBox);
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