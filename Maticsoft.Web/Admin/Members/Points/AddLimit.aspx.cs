/**
* AddLimit.cs
*
* 功 能： [N/A]
* 类 名： AddLimit
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/20 16:33:34  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Maticsoft.Web.Admin.Members.Points
{
    public partial class AddLimit : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 296; } } 
        private Maticsoft.BLL.Members.PointsLimit LimitBll = new BLL.Members.PointsLimit();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropCycleUnit.DataBind();
            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {

            if (LimitBll.Exists(this.tName.Text.Trim()))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "已存在该规则限制名称，请重新填写");
            }
            else
            {
                Maticsoft.Model.Members.PointsLimit LimitModel = new Model.Members.PointsLimit();
                LimitModel.Name = this.tName.Text.Trim();
                if (Common.PageValidate.IsNumber(this.tCycle.Text.Trim()))
                {
                    LimitModel.Cycle = Common.Globals.SafeInt(this.tCycle.Text.Trim(), 0);
                }
                if (Common.PageValidate.IsNumber(this.tMaxTimes.Text.Trim()))
                {
                    LimitModel.MaxTimes = Common.Globals.SafeInt(this.tMaxTimes.Text.Trim(), 0);
                }
                LimitModel.CycleUnit = DropCycleUnit.SelectedValue;
                LimitModel.TargetId =0;
                LimitModel.TargetType = 0;
                if (LimitBll.Add(LimitModel)>0)
                {
                    Response.Redirect("PointsLimit.aspx");
                }
                else
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = "添加条件限制出错！";
                }
            }

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PointsLimit.aspx");
        }
    }
}
