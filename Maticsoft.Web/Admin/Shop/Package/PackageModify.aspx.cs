/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
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
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Shop.Package;
using Maticsoft.Common;
using Maticsoft.Web;

namespace Maticsoft.Web.Admin.Shop.Package
{
    public partial class PackageModify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 453; } } //Shop_包装管理_编辑页
        Maticsoft.BLL.Shop.Package.Package bll=new BLL.Shop.Package.Package();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryData();
                ShowInfo(Id);
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    MessageBox.ShowFailTip(this,"您没有此权限");
                    return;
                }
               
            }
        }


        public int Id
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }
        private void ShowInfo(int packageId)
        {
            Maticsoft.BLL.Shop.Package.Package bll = new Maticsoft.BLL.Shop.Package.Package();
            Maticsoft.Model.Shop.Package.Package model = bll.GetModel(packageId);
            this.txtName.Text = model.Name;
            this.txtRemark.Text = model.Remark;
            this.txtDescription.Text = model.Description;
            this.ddlCategory.SelectedValue = model.CategoryId.ToString();

        }

        private void BindCategoryData()
        {
            Maticsoft.BLL.Shop.Package.PackageCategory catebll = new PackageCategory();
            DataSet ds = catebll.GetList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlCategory.DataSource = ds;
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "CategoryId";
                this.ddlCategory.DataBind();
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
		{
            string Path = "/Upload/Shop/Files/";
            int RestrictSize = 10240000;
            string FileName = uploadPhoto.PostedFile.FileName;
            string Savepath = "";
            if (uploadPhoto.HasFile)
            {
                int filelength = uploadPhoto.PostedFile.ContentLength;
                if (filelength > RestrictSize)
                {

                    Common.MessageBox.ShowSuccessTip(this, "您上传的图片过大，请上传较小的文件");
                    return;
                }
                Savepath = Server.MapPath(Path) + FileName;
                uploadPhoto.PostedFile.SaveAs(Savepath);
            }
            string Name = this.txtName.Text.Trim();
            string Description = this.txtDescription.Text.Trim();
            string Remark = this.txtRemark.Text.Trim();
            if (Name.Length == 0)
			{
                MessageBox.ShowServerBusyTip(this, "包装的名称不能为空！");
                return;
			}
         
            if (Remark.Length > 200)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能超过200个字符！");
                return;
            }
            if (Description.Length > 200)
            {
                MessageBox.ShowServerBusyTip(this, "描述不能超过200个字符！");
                return;
            }
            Maticsoft.Model.Shop.Package.Package model =bll.GetModel(Id);
			model.Name=Name;
            model.Remark = Remark;
            model.PhotoUrl = string.IsNullOrEmpty(Savepath) ? model.PhotoUrl : model.PhotoUrl = Path + FileName; ;
            model.Description = Description;
            model.CategoryId = Common.Globals.SafeInt(ddlCategory.SelectedValue, 0);
            if (bll.Update(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新包装(id=" + model.PackageId + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "PackageList.aspx");
                Response.Redirect("PackageList.aspx");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新包装(id=" + ID + ")失败", this);
                MessageBox.ShowServerBusyTip(this, Resources.Site.TooltipSaveError);
            }
		}
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PackageList.aspx");
        }
    }
}
