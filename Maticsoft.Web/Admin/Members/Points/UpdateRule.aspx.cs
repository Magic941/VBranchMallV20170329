/**
* UpdateRule.cs
*
* 功 能： [N/A]
* 类 名： UpdateRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/20 16:32:23  Administrator    初版
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
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Points
{
    public partial class UpdateRule : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.PointsRule RuleBll = new BLL.Members.PointsRule();
        private Maticsoft.BLL.Members.PointsLimit LimitBll = new BLL.Members.PointsLimit();
        private Maticsoft.BLL.Members.PointsAction actionBll = new BLL.Members.PointsAction();
        protected override int Act_PageLoad { get { return 291; } } 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Maticsoft.Model.Members.PointsRule RuleModel = RuleBll.GetModel(RuleId);
                    if (RuleModel == null)
                    {
                        Response.Redirect("PointsRule.aspx");
                    }

                    DropLimitName.DataSource = LimitBll.GetAllList();
                    DropLimitName.DataTextField = "Name";
                    DropLimitName.DataValueField = "LimitID";
                    DropLimitName.DataBind();
                    DropLimitName.Items.Insert(0, new ListItem("无限制", "-1"));

                    ddlAction.DataSource = actionBll.GetAllList();
                    ddlAction.DataTextField = "Name";
                    ddlAction.DataValueField = "ActionId";
                    ddlAction.DataBind();
                    ddlAction.Items.Insert(0, new ListItem("请选择", "0"));

                    ddlAction.SelectedValue = RuleModel.ActionId.ToString();
                    tName.Text = RuleModel.Name;
                    tScore.Text = RuleModel.Score.ToString();
                    tDesc.Text = RuleModel.Description;
                    DropLimitName.SelectedValue = RuleModel.LimitID.ToString();

                }
            
        }


        #region 编号

        /// <summary>
        /// 编号
        /// </summary>
        protected int RuleId
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        #endregion

       /// <summary>
       /// 保存操作
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void btnSave_Click(object sender, System.EventArgs e)
        {

            Maticsoft.Model.Members.PointsRule RuleModel = RuleBll.GetModel(RuleId);
                RuleModel.Name = this.tName.Text.Trim();
                RuleModel.LimitID = Convert.ToInt32(DropLimitName.SelectedValue);
                RuleModel.Description = this.tDesc.Text.Trim();
                if (Common.PageValidate.IsNumber(this.tScore.Text.Trim()))
                {
                    RuleModel.Score = Common.Globals.SafeInt(this.tScore.Text.Trim(), 0);
                }
                if (RuleBll.Update(RuleModel))
                {
                    Response.Redirect("PointsRule.aspx");
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "修改规则失败！");
                }
         
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PointsRule.aspx");
        }

    }
}
