/**
* PointsLimits.cs
*
* 功 能： [N/A]
* 类 名： PointsLimits
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/20 16:32:58  Administrator    初版
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
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Points
{
    public partial class PointsLimits : PageBaseAdmin
    {
        Maticsoft.BLL.Members.PointsLimit LimitBll = new BLL.Members.PointsLimit();
        protected override int Act_PageLoad { get { return 295; } } //用户管理_积分限制管理_列表页
        protected new int Act_AddData = 298;    //用户管理_积分限制管理_添加数据
        protected new int Act_UpdateData = 299;    //用户管理_积分限制管理_编辑数据
        protected new int Act_DelData = 300;    //用户管理_积分限制管理_删除数据
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
            DataSet ds = LimitBll.GetAllList();
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
            int limitId =(int)gridView.DataKeys[e.RowIndex].Value;
            if (LimitBll.ExistsLimit(limitId))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "该限制条件已经被使用，不能删除！");
                gridView.OnBind();
                return;
            }
            LimitBll.Delete(limitId);
            gridView.OnBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected string GetUnitName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string CycleUnit = target.ToString();
                switch (CycleUnit)
                {
                    case "day":
                        return "日";
                    case "month":
                        return "月";
                    case "year":
                        return "年";
                    default:
                        return "未知";
                }
            }
            return str;
        }
        #endregion
    }
}