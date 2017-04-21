using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Ms.ThumbnailSize
{
    public partial class ThumSizeList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 335; } } //设置_缩略图尺寸管理_列表页
        protected new int Act_AddData = 336;    //设置_缩略图尺寸管理_添加数据
        protected new int Act_UpdateData = 337;    //设置_缩略图尺寸管理_编辑数据
        protected new int Act_DelData = 338;    //设置_缩略图尺寸管理_删除数据

        Maticsoft.BLL.Ms.ThumbnailSize thumBll = new BLL.Ms.ThumbnailSize();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    btnSave.Visible = false;
                    tradd.Visible = false;
                }

                this.ddlTheme.DataSource = Maticsoft.Web.Components.FileHelper.GetThemeList("CMS");
                this.ddlTheme.DataTextField = "Name";
                this.ddlTheme.DataValueField = "Name";
                ddlTheme.DataBind();
                ddlTheme.Items.Insert(0, new ListItem("全部", ""));

            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[8].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[9].Visible = false;
            }
            DataSet ds = new DataSet();
            int areaType = ddAreaType.AreaInt;
            string strWhere = "";
            if (areaType > -1)
            {
                strWhere = " Type=" + areaType;
            }
            string theme = ddlTheme2.SelectedValue;
            if (!String.IsNullOrWhiteSpace(theme))
            {
                if (!String.IsNullOrWhiteSpace(strWhere))
                {
                    strWhere += " and ";
                }
                strWhere += " Theme='" + theme + "' ";
            }
            ds = thumBll.GetList(strWhere);
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string type = gridView.DataKeys[e.RowIndex].Value.ToString();
            thumBll.Delete(type);
            gridView.OnBind();
        }

        protected string GetTypeName(object target)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "CMS";
                        break;
                    case "1":
                        str = "SNS";
                        break;
                    case "2":
                        str = "Shop";
                        break;
                    case "3":
                        str = "Tao";
                        break;
                    case "4":
                        str = "COM";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }


        protected string GetModeName(object target)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "Auto自动缩放";
                        break;
                    case "1":
                        str = "指定高宽裁减（不变形）";
                        break;
                    case "2":
                        str = "指定高，宽按比例";
                        break;
                    case "3":
                        str = "指定高宽缩放（可能变形）";
                        break;
                    case "4":
                        str = "指定宽，高按比例";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.tName.Text))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "请填写缩略图尺寸名称");
                return;
            }
            if (thumBll.Exists(this.tName.Text.Trim()))//, ddlType.AreaInt,this.ddlTheme.SelectedValue.Trim()
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "已存在该缩略图尺寸名称，请重新填写");
                return;
            }

            Maticsoft.Model.Ms.ThumbnailSize model = new Model.Ms.ThumbnailSize();
            model.Type = ddlType.AreaInt;
            model.ThumName = this.tName.Text.Trim();
            model.ThumWidth = Common.Globals.SafeInt(this.tWidth.Text, 1);
            model.ThumHeight = Common.Globals.SafeInt(this.tHeight.Text, 1);
            model.Remark = this.tDesc.Text.Trim();
            model.CloudSizeName = this.tCloudSizeName.Text;
            model.CloudType = 0;
            model.Theme = this.ddlTheme.SelectedValue.Trim();
            model.ThumMode = Common.Globals.SafeInt(this.ddlThumMode.SelectedValue, 0);
            model.IsWatermark = false;
            if (thumBll.Add(model))
            {
                Response.Redirect("ThumSizeList.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, "添加失败！请重试。");
            }

        }

        protected void ddlType_Change(object sender, EventArgs e)
        {
            this.ddlTheme.DataSource = Maticsoft.Web.Components.FileHelper.GetThemeList(ddlType.AreaName);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            ddlTheme.DataBind();
            ddlTheme.Items.Insert(0, new ListItem("全部", ""));
        }

        protected void ddlType_Change2(object sender, EventArgs e)
        {

            this.ddlTheme2.DataSource = Maticsoft.Web.Components.FileHelper.GetThemeList(ddAreaType.AreaName);
            this.ddlTheme2.DataTextField = "Name";
            this.ddlTheme2.DataValueField = "Name";
            ddlTheme2.DataBind();
            ddlTheme2.Items.Insert(0, new ListItem("全部", ""));
        }

    }
}