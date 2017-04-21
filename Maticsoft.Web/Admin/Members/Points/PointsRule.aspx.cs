/**
* PointsRule.cs
*
* 功 能： [N/A]
* 类 名： PointsRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/20 16:06:01  Administrator    初版
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
using System.Data;

namespace Maticsoft.Web.Admin.Members.Points
{
    public partial class PointsRule : PageBaseAdmin
    {
        Maticsoft.BLL.Members.PointsRule RuleBll = new BLL.Members.PointsRule();

        protected override int Act_PageLoad { get { return 289; } } //用户管理_积分规则管理_列表页
        protected new int Act_AddData = 292;    //用户管理_积分规则管理_添加数据
        protected new int Act_UpdateData = 293;    //用户管理_积分规则管理_编辑数据
        protected new int Act_DelData = 294;    //用户管理_积分规则管理_删除数据

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liadd.Visible = false;
                }

            }
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[6].Visible = false;
            }
            //获取积分规则
            string keyword = this.txtKeyword.Text;
            DataSet ds = RuleBll.GetListByKeyWord(keyword);
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ruleId = Common.Globals.SafeInt(gridView.DataKeys[e.RowIndex].Value.ToString(),0);
            RuleBll.Delete(ruleId);
            gridView.OnBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
        Maticsoft.BLL.Members.PointsLimit limitBll = new BLL.Members.PointsLimit();
        public string GetLimitName(int limitId)
        {
            if (limitId == -1)
            {
                return "无限制";
            }
            else
            {
                Maticsoft.Model.Members.PointsLimit limitModel = limitBll.GetModel(limitId);
                if (limitModel != null)
                {
                    return limitModel.Name;
                }
                else
                {
                    return "无限制";
                }
            }
        }


        public string GetActionName(int actionId)
        {
            if (actionId == -1)
            {
                return "未知操作";
            }
            Maticsoft.Model.Members.PointsAction actionModel = Maticsoft.BLL.Members.PointsAction.GetAction(actionId);
            if (actionModel != null)
            {
                return actionModel.Name;
            }
            return "未知操作";
        }

        #endregion
    }
}