/**
* AddRule.cs
*
* 功 能： [N/A]
* 类 名： AddRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/20 16:31:59  Administrator    初版
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
    public partial class AddRule : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 290; } } 
        private Maticsoft.BLL.Members.PointsRule RuleBll = new BLL.Members.PointsRule();
        private Maticsoft.BLL.Members.PointsLimit LimitBll = new BLL.Members.PointsLimit();
        private Maticsoft.BLL.Members.PointsAction actionBll = new BLL.Members.PointsAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
           int actionId=Common.Globals.SafeInt(ddlAction.SelectedValue,0);
               if(actionId==0)
               {
                   Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择该规则对应的操作！");
                   return;
               }
           if (RuleBll.Exists(actionId))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "已存在该规则操作，请重新选择");
                 return;
            }
           
                Maticsoft.Model.Members.PointsRule RuleModel = new Model.Members.PointsRule();
                RuleModel.ActionId = actionId;
                RuleModel.Name = this.tName.Text.Trim();
                RuleModel.LimitID =Convert.ToInt32(DropLimitName.SelectedValue);
                RuleModel.Description = this.tDesc.Text.Trim();
                RuleModel.TargetId = 0;
                RuleModel.TargetType = 0;
                if (Common.PageValidate.IsNumber(this.tScore.Text.Trim()))
                {
                    RuleModel.Score = Common.Globals.SafeInt(this.tScore.Text.Trim(), 0);
                }
                if (RuleBll.Add(RuleModel)>0)
                {
                    Response.Redirect("PointsRule.aspx");
                }
                else
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = "添加规则出错！";
                }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PointsRule.aspx");
        }
    }
}
