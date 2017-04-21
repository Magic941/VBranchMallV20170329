/**
* EntryForm.cs
*
* 功 能： [N/A]
* 类 名： EntryForm
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Ms.EntryForm
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 276; } } //客服管理_报名用户管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BindData()
        {
            for (int i = 0; i <= 119; i++)
            {
                this.dropAge.Items.Add(i.ToString());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string username = this.txtUserName.Text.Trim();
            string remark = this.txtRemark.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.ShowFailTip(this, Resources.MsEntryForm.ErrorNameNotNull);
                return;
            }
            if (remark.Length > 300)
            {
                MessageBox.ShowFailTip(this, Resources.MsEntryForm.ErrorRemarkoverlength);
                return;
            }

            Maticsoft.Model.Ms.EntryForm model = new Maticsoft.Model.Ms.EntryForm();
            model.UserName = this.txtUserName.Text.Trim();
            model.Age = int.Parse(this.dropAge.SelectedValue);
            model.Email = this.txtEmail.Text.Trim();
            model.TelPhone = this.txtTelPhone.Text.Trim();
            model.Phone = this.txtPhone.Text.Trim();
            model.QQ = this.txtQQ.Text.Trim();
            model.MSN = this.txtMSN.Text.Trim();
            model.HouseAddress = this.txtHouseAddress.Text.Trim();
            model.CompanyAddress = this.txtCompanyAddress.Text.Trim();
            model.RegionId = Convert.ToInt32(this.dropProvince.Area_iID);
            model.Sex = this.dropSex.SelectedValue;
            model.Description = this.txtDescription.Text.Trim();
            model.Remark = remark;
            model.State = int.Parse(this.dropState.SelectedValue);

            Maticsoft.BLL.Ms.EntryForm bll = new Maticsoft.BLL.Ms.EntryForm();
            if (bll.Add(model) > 0)
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "list.aspx");
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
