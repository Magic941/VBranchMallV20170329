using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.SNS;

namespace Maticsoft.Web.Admin.SNS.Setting
{
    public partial class SEOConfig : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 611; } } //SNS_SEO优化页
        protected new int Act_UpdateData = 612;    //SNS_SEO优化_编辑数据
        ApplicationKeyType applicationKeyType = ApplicationKeyType.SNS;
        private string[] pageNames = new string[]
            {
                "Home",             //首页
                "BlogDetail",       //博客详细
                  "BlogList",       //博客列表
                   "VideoDetail",       //视频详细
                  "VideoList",       //视频列表
                "ProductList",      //商品列表
                "ProductDetail",    //商品详细
                //"Collocation",      //搭配
                //"ShareGoods",       //晒货
                "PhotoList",      //图片列表
                "PhotoDetail",      //图片详细
                "Group",            //小组首页
                "GroupList",        //小组帖子列表
                "GroupDetail",      //小组帖子详细
                "Ablum",            //专辑首页
                "AblumList",        //专辑列表
                "AblumDetail",      //专辑详细
                "Star",             //达人
                //"StarList",         //达人列表
                //"StarDetail",       //达人详细
            };

        private Dictionary<string, IPageSetting> pageSettings;

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
            }
        }

        private void LoadPageSetting()
        {
            pageSettings = new Dictionary<string, IPageSetting>();
            foreach (string name in pageNames)
            {
                pageSettings.Add(name, new PageSetting(name, applicationKeyType));
            }
        }

        private void LoadTextBox(string pageName,
            TextBox txtTitle,
            TextBox txtKeyWords,
            TextBox txtDes)
        {
            txtTitle.Text = pageSettings[pageName].Title;
            txtKeyWords.Text = pageSettings[pageName].Keywords;
            txtDes.Text = pageSettings[pageName].Description;
        }

        private string txtTitleId = "txt{0}Title";
        private string txtKeywordsId = "txt{0}Keywords";
        private string txtDesId = "txt{0}Des";

        private void BoundData()
        {
            LoadPageSetting();
            foreach (string pageName in pageSettings.Keys)
            {
                LoadTextBox(pageName,
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtTitleId, pageName)) as TextBox,
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtKeywordsId, pageName)) as TextBox,
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtDesId, pageName)) as TextBox);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }
        #endregion

        #region 保存数据
        private void SaveTextBox(string pageName,TextBox txtTitle, TextBox txtKeyWords,TextBox txtDes)
        {

            pageSettings[pageName].Title = Globals.HtmlEncode(txtTitle.Text.Trim().Replace("\n", ""));
            pageSettings[pageName].Keywords = Globals.HtmlEncode(txtKeyWords.Text.Trim().Replace("\n", ""));
            pageSettings[pageName].Description = Globals.HtmlEncode(txtDes.Text.Trim().Replace("\n", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LoadPageSetting();  //保存时, 重新加载page数据
            try
            {
                foreach (string pageName in pageSettings.Keys)
                {
                    SaveTextBox(pageName,
                                Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtTitleId, pageName)) as TextBox,
                                Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtKeywordsId, pageName)) as TextBox,
                                Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(txtDesId, pageName)) as TextBox);
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