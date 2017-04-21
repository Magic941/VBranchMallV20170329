/**
* Modify.cs
*
* 功 能： N/A
* 类 名： Add
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01							N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.ReportType
{
    public partial class Modify : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 609; } } //SNS_举报类型管理_编辑页
        private Maticsoft.BLL.SNS.ReportType bll = new Maticsoft.BLL.SNS.ReportType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
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
            Maticsoft.Model.SNS.ReportType model = bll.GetModel(Id);
            if (null != model)
            {
                this.txtTypeName.Text = model.TypeName;
                this.radlStatus.SelectedValue = model.Status.ToString();
                this.txtRemark.Text = model.Remark;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string TypeName = this.txtTypeName.Text;
            string Remark = this.txtRemark.Text;

            if (TypeName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "举报类型的名称不能为空！");
                return;
            }
            if (bll.Exists(Id, TypeName))
            {
                MessageBox.ShowServerBusyTip(this, "举报类型的名称已存在！");
                return;
            }
            if (Remark.Length > 50)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能超过50个字符！");
                return;
            }

            Maticsoft.Model.SNS.ReportType model = bll.GetModel(Id);
            if (null != model)
            {
                model.TypeName = TypeName;
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                model.Remark = Remark;

                if (bll.Update(model))
                {
                    MessageBox.ShowSuccessTipScript(this, "编辑举报类型成功", "window.parent.location.reload();");
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新举报类型(id=" + model.ID + ")成功", this);
                }
                else
                {
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新举报类型(id=" + model.ID + ")失败", this);
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                }
            }
        }

        //public void btnCancle_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("list.aspx");
        //}
    }
}