/**
* Modify.cs
*
* 功 能： 网站菜单修改
* 类 名： Modify.cs
*
* Ver    变更日期                               负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年5月23日 16:39:25     孙鹏    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.UI;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Common;
using System.Web.UI.WebControls;
using System.Collections.Generic;
namespace Maticsoft.Web.Settings.MainMenus
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 388; } } //设置_导航菜单管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int MenuID = (Convert.ToInt32(Request.Params["id"]));
                    ShowInfo(MenuID);
                }

            }
        }

        private void ShowInfo(int MenuID)
        {
            Maticsoft.BLL.Settings.MainMenus bll = new Maticsoft.BLL.Settings.MainMenus();
            Maticsoft.Model.Settings.MainMenus model = bll.GetModel(MenuID);
            this.txtMenuName.Text = model.MenuName;
            this.ddlTarget.SelectedValue = model.Target.ToString();
            //this..Text = model.Target.ToString();
           // this.txtThemeName.Text = model.NavTheme; 
            this.chkIsUsed.Checked = model.IsUsed;
            this.HiddenField_Type.Value = model.MenuType.ToString();
            this.txtTile.Text = model.MenuTitle;
            this.txtSqueeze.Text = model.Sequence.ToString();
            this.ddNavType.SelectedValue = model.URLType.ToString();
            string NavArea = "CMS";
            switch (model.NavArea)
            {
                case 0:
                    NavArea = "CMS";
                    break;
                case 1:
                    NavArea = "SNS";
                    break;
                case 2:
                    NavArea = "Shop";
                    break;
                default :
                      NavArea = "CMS";
                    break;
            }
            List<Maticsoft.Model.SysManage.ConfigArea> allArea = Maticsoft.BLL.SysManage.ConfigArea.GetAllArea();
            if (allArea.Find(c => c.AreaName == NavArea) != null)
            {
                this.ddlType.SelectedValue = NavArea;
            }
         
            this.ddlTheme.DataSource = Maticsoft.Web.Components.FileHelper.GetThemeList(NavArea);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            ddlTheme.DataBind();
            ddlTheme.Items.Insert(0, new ListItem("全部", ""));
            this.ddlTheme.SelectedValue = model.NavTheme; 
            switch (model.URLType)
            {
                case 0:
                    this.txtNavURL.Visible = true;
                    this.txtNavURL.Text = model.NavURL;
                    break;
                case 1:
                    Maticsoft.BLL.CMS.ContentClass classBll = new BLL.CMS.ContentClass();
                    ddValue.DataSource = classBll.GetList(" ParentId=0");
                    this.ddValue.DataTextField = "ClassName";
                    this.ddValue.DataValueField = "ClassID";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    break;
                case 2:
                    Maticsoft.BLL.SNS.Categories productCate = new BLL.SNS.Categories();
                    ddValue.DataSource = productCate.GetList(" ParentId=0 and type=0 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    break;
                case 3:
                    Maticsoft.BLL.SNS.Categories photoCate = new BLL.SNS.Categories();
                    ddValue.DataSource = photoCate.GetList(" ParentId=0 and type=1 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    break;
                case 4:
                    Maticsoft.BLL.Shop.Products.CategoryInfo shopCate = new CategoryInfo();
                    ddValue.DataSource = shopCate.GetList(" ParentCategoryId=0 ");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    break;
                default:
                    this.txtNavURL.Visible = true;
                    this.txtNavURL.Text = model.NavURL;
                    break;
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                 Maticsoft.BLL.Settings.MainMenus bll = new Maticsoft.BLL.Settings.MainMenus();
                int MenuID = (Convert.ToInt32(Request.Params["id"]));
                if (string.IsNullOrWhiteSpace(this.txtMenuName.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.CMS.WMErrorMenuName);
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.txtSqueeze.Text) || !PageValidate.IsNumber(this.txtSqueeze.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.CMS.WMErrorSqueeze);
                    return;
                }
                string MenuName = this.txtMenuName.Text;
                string NavURL = this.txtNavURL.Text;
                bool IsUsed = this.chkIsUsed.Checked;

                Maticsoft.Model.Settings.MainMenus model = bll.GetModel(MenuID);
                model.MenuName = MenuName;
                model.MenuType = int.Parse(this.HiddenField_Type.Value);
                model.Target = int.Parse(this.ddlTarget.SelectedValue);
                model.IsUsed = IsUsed;
                model.MenuTitle = this.txtTile.Text;
                model.Sequence = int.Parse(this.txtSqueeze.Text);
                model.NavTheme = this.ddlTheme.SelectedValue;//txtThemeName.Text.Trim();


                model.NavArea = Maticsoft.BLL.SysManage.ConfigArea.GetAreaInt(ddlType.SelectedValue);
                model.URLType = Common.Globals.SafeInt(ddNavType.SelectedValue, 0);
                switch (model.URLType)
                {
                    case 0:
                        model.NavURL = this.txtNavURL.Text.Trim();
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        model.NavURL = this.ddValue.SelectedValue;
                        break;
                    default:
                        model.NavURL = this.txtNavURL.Text.Trim();
                        break;
                }
                if (string.IsNullOrWhiteSpace(model.NavURL))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.CMS.WMErrorPageUrl);
                    return;
                }
         
                if (bll.Update(model))
                {
                    this.btnSave.Enabled = false;
                    this.btnCancle.Enabled = false;
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "list.aspx");
                }
                else
                {
                    this.btnSave.Enabled = false;
                    this.btnCancle.Enabled = false;
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "list.aspx");
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        protected void ddNavType_Change(object sender, EventArgs e)
        {
            int NavType = Common.Globals.SafeInt(this.ddNavType.SelectedValue, 0);
            switch (NavType)
            {
                case 0:
                    this.txtNavURL.Visible = true;
                    this.ddValue.Visible = false;
                    break;
                case 1:
                    Maticsoft.BLL.CMS.ContentClass classBll = new BLL.CMS.ContentClass();
                    ddValue.DataSource = classBll.GetList(" ParentId=0");
                    this.ddValue.DataTextField = "ClassName";
                    this.ddValue.DataValueField = "ClassID";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    break;
                case 2:
                    Maticsoft.BLL.SNS.Categories productCate = new BLL.SNS.Categories();
                    ddValue.DataSource = productCate.GetList(" ParentId=0 and type=0 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    break;
                case 3:
                    Maticsoft.BLL.SNS.Categories photoCate = new BLL.SNS.Categories();
                    ddValue.DataSource = photoCate.GetList(" ParentId=0 and type=1 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    break;
                case 4:
                    Maticsoft.BLL.Shop.Products.CategoryInfo shopCate = new CategoryInfo();
                    ddValue.DataSource = shopCate.GetList(" ParentCategoryId=0 ");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    break;
                default:
                    this.ddValue.Visible = false;
                    this.txtNavURL.Visible = true;
                    break;

            }
        }

        protected void ddlType_Change(object sender, EventArgs e)
        {
            this.ddlTheme.DataSource = Maticsoft.Web.Components.FileHelper.GetThemeList(ddlType.SelectedItem.Text);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            ddlTheme.DataBind();
            ddlTheme.Items.Insert(0, new ListItem("全部", ""));
            this.ddValue.Visible = false;
            this.ddNavType.SelectedValue = "0";
            this.txtNavURL.Visible = true;
        }


    }
}
