/**
* Add.cs
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
    public partial class Add : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 608; } } //SNS_举报类型管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            Maticsoft.BLL.SNS.ReportType bll = new Maticsoft.BLL.SNS.ReportType();

            string TypeName = this.txtTypeName.Text;
            string Remark = this.txtRemark.Text;

            if (TypeName.Length == 0)
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "举报类型的名称不能为空！");
                return;
            }
            if (bll.Exists(TypeName))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "举报类型的名称已存在！");
                return;
            }
            if (Remark.Length > 50)
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "备注不能超过50个字符！");
                return;
            }

            Maticsoft.Model.SNS.ReportType model = new Maticsoft.Model.SNS.ReportType();
            model.TypeName = TypeName;
            model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
            model.Remark = Remark;

            if (bll.Add(model) > 0)
            {
                MessageBox.ShowSuccessTipScript(this, "添加举报类型成功", "window.parent.location.reload();");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加举报类型成功", this);
            }
        }

        //public void btnCancle_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("list.aspx");
        //}
    }
}