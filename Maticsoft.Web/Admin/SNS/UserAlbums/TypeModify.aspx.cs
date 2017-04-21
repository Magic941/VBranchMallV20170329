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
using Maticsoft.Common;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.SNS.AlbumType
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 622; } } //SNS_专辑分类管理_编辑页
        Maticsoft.BLL.SNS.AlbumType bll = new Maticsoft.BLL.SNS.AlbumType();
        protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				ShowInfo();
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
			
	private void ShowInfo()
	{
		
        Maticsoft.Model.SNS.AlbumType model = bll.GetModel(Id);
        if (null != model)
        {
            this.lblID.Text = model.ID.ToString();
            this.txtTypeName.Text = model.TypeName;
            this.chkIsMenu.Checked = model.IsMenu;
            this.chkMenuIsShow.Checked = model.MenuIsShow;
            this.txtMenuSequence.Text = model.MenuSequence.ToString();
            if (model.AlbumsCount.HasValue)
            {
                this.txtAlbumsCount.Text = model.AlbumsCount.ToString();
            }
            this.radlStatus.SelectedValue = model.Status.ToString();
            this.txtRemark.Text = model.Remark;
        }
	}

		public void btnSave_Click(object sender, EventArgs e)
		{

            string TypeName = this.txtTypeName.Text.Trim();
            if (TypeName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "专辑类型的名称不能为空！");
                return;
            }
            string Remark = this.txtRemark.Text.Trim();
            if (Remark.Length > 200)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能超过200个字符！");
                return;
            }

			 Maticsoft.Model.SNS.AlbumType model = bll.GetModel(Id);
             if (null != model)
             {
                 model.TypeName = TypeName;
                 model.IsMenu = this.chkIsMenu.Checked;
                 model.MenuIsShow = this.chkMenuIsShow.Checked;
                 model.MenuSequence = Globals.SafeInt(this.txtMenuSequence.Text, 1);
                 model.AlbumsCount = Globals.SafeInt(this.txtAlbumsCount.Text, 1);
                 model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                 model.Remark = Remark;
                 if (bll.Update(model))
                 {
                     LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改专辑类型(id=" + model.ID + ")成功", this);
                     MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "Typelist.aspx");
                 }
                 else
                 {
                     LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改专辑类型(id=" + model.ID + ")失败", this);
                     MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                 }
             }

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Typelist.aspx");
        }
    }
}
