/**
* UpdateLimit.cs
*
* 功 能： [N/A]
* 类 名： UpdateLimit
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/20 16:34:05  Administrator    初版
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
    public partial class UpdateLimit : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 297; } } 
        private Maticsoft.BLL.Members.PointsLimit LimitBll = new BLL.Members.PointsLimit();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if ((Request.Params["limitId"] != null) && (Request.Params["limitId"].ToString() != ""))
                {
                    int limitId =Common.Globals.SafeInt(Request.Params["limitId"],0);
                  Maticsoft.Model.Members.PointsLimit  LimitModel = LimitBll.GetModel(limitId);
                    if (LimitModel == null)
                    {
                        Response.Redirect("PointsLimit.aspx");
                    }
                    tName.Text = LimitModel.Name;
                    tCycle.Text = LimitModel.Cycle.ToString();
                    tMaxTimes.Text = LimitModel.MaxTimes.ToString();
                    DropCycleUnit.SelectedValue = LimitModel.CycleUnit.ToString();

                }
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, System.EventArgs e)
        {

            if ((Request.Params["limitId"] != null) && (Request.Params["limitId"].ToString() != ""))
            {
                int limitId = Common.Globals.SafeInt(Request.Params["limitId"], 0);
                Maticsoft.Model.Members.PointsLimit LimitModel = LimitBll.GetModel(limitId);
                if (LimitBll.Exists(this.tName.Text.Trim(), LimitModel.LimitID))
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "已存在该规则限制名称，请重新填写");
                }
                else
                {
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

                    if (LimitBll.Update(LimitModel))
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

        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PointsLimit.aspx");
        }

    }
}
