using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Ms.ThumbnailSize
{
    public partial class UpdateThumSize : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 340; } } //设置_缩略图尺寸管理_编辑页
        private Maticsoft.BLL.Ms.ThumbnailSize thumBll = new BLL.Ms.ThumbnailSize();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.Params["Name"] != null) && (Request.Params["Name"].ToString() != ""))
                {

                    string name = Request.Params["Name"];
                    Maticsoft.Model.Ms.ThumbnailSize thumModel = thumBll.GetModel(name);
                    if (thumModel == null)
                    {
                        Response.Redirect("ThumSizeList.aspx");
                    }
                    tWidth.Text = thumModel.ThumWidth.ToString();
                    tName.Text = thumModel.ThumName;
                    tHeight.Text = thumModel.ThumHeight.ToString();
                    tDesc.Text = thumModel.Remark;
                    this.ddlThumMode.SelectedValue = thumModel.ThumMode.ToString();

                    string NavArea = "CMS";
                    switch (thumModel.Type)
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
                        default:
                            NavArea = "CMS";
                            break;
                    }
                    List<Maticsoft.Model.SysManage.ConfigArea> allArea=   Maticsoft.BLL.SysManage.ConfigArea.GetAllArea();
                    if (allArea.Find(c=>c.AreaName==NavArea) != null)
                    {
                        this.ddlType.SelectedValue = NavArea;
                    }

                    this.ddlTheme.DataSource = Maticsoft.Web.Components.FileHelper.GetThemeList(NavArea);
                    this.ddlTheme.DataTextField = "Name";
                    this.ddlTheme.DataValueField = "Name";
                    ddlTheme.DataBind();
                    ddlTheme.Items.Insert(0, new ListItem("全部", ""));
                    ddlTheme.SelectedValue = thumModel.Theme;
                    chkWatermark.Checked = thumModel.IsWatermark;
                    this.tCloudSizeName.Text = thumModel.CloudSizeName;


                }
            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            Maticsoft.Model.Ms.ThumbnailSize model = thumBll.GetModel(this.tName.Text);
            model.Type = ddlType.AreaInt;
            model.ThumName = this.tName.Text.Trim();
            model.ThumWidth = Common.Globals.SafeInt(this.tWidth.Text, 1);
            model.ThumHeight = Common.Globals.SafeInt(this.tHeight.Text, 1);
            model.Remark = this.tDesc.Text.Trim();
            model.Theme = ddlTheme.SelectedValue;
            model.ThumMode = Common.Globals.SafeInt(this.ddlThumMode.SelectedValue, 0);
            model.IsWatermark = chkWatermark.Checked;
            model.CloudSizeName = this.tCloudSizeName.Text;

            if (thumBll.Update(model))
            {
                Response.Redirect("ThumSizeList.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, "更新失败！请重试。");
            }

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ThumSizeList.aspx");
        }
        protected void ddlType_Change(object sender, EventArgs e)
        {
            this.ddlTheme.DataSource = Maticsoft.Web.Components.FileHelper.GetThemeList(ddlType.SelectedItem.Text);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            ddlTheme.DataBind();
            ddlTheme.Items.Insert(0, new ListItem("全部", ""));
        }
    }
}
