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
using System.Web.UI;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.GradeConfig
{
    [Obsolete]
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 577; } } //SNS_会员等级管理_编辑页
        private Maticsoft.BLL.SNS.GradeConfig bll = new Maticsoft.BLL.SNS.GradeConfig();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int GradeID = (Convert.ToInt32(Request.Params["id"]));
                    ShowInfo(GradeID);
                }
            }
        }

        private void ShowInfo(int GradeID)
        {
            Maticsoft.Model.SNS.GradeConfig model = bll.GetModel(GradeID);
            this.lblGradeID.Text = model.GradeID.ToString();
            this.txtGradeName.Text = model.GradeName;
            this.txtMinRange.Text = model.MinRange.ToString();
            this.txtMaxRange.Text = model.MaxRange.ToString();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtGradeName.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级名称！");
                return;
            }
            if (Globals.SafeInt(this.txtGradeName.Text, 0) > 20)
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入0-20之间正确的等级名称！");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtMinRange.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级积分下限！");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtMaxRange.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级积分上限！");
                return;
            }
            int GradeID = int.Parse(this.lblGradeID.Text);
            string GradeName = this.txtGradeName.Text;
            int MinRange = int.Parse(this.txtMinRange.Text);
            int MaxRange = int.Parse(this.txtMaxRange.Text);

            Maticsoft.Model.SNS.GradeConfig model = new Maticsoft.Model.SNS.GradeConfig();
            model.GradeID = GradeID;
            model.GradeName = GradeName;
            model.MinRange = MinRange;
            model.MaxRange = MaxRange;

            if (bll.Update(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改用户等级（GradeID=" + model.GradeID + "）成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "GradeList.aspx");
            }
            else
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                Maticsoft.Common.MessageBox.ShowFailTip(this, "系统忙，请稍后再试！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改用户等级（GradeID=" + model.GradeID + "）失败", this);
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("GradeList.aspx");
        }
    }
}