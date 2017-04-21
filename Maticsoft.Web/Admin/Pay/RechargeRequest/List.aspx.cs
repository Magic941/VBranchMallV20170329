/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Pay.RechargeRequest
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        protected override int Act_PageLoad { get { return 690; } } //Shop_充值管理_列表页   
        protected new int Act_DelData = 691;    //Shop_充值管理_删除数据
		Maticsoft.BLL.Pay.RechargeRequest bll = new Maticsoft.BLL.Pay.RechargeRequest();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
             
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                    liDel.Visible = false;
                }
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
             

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

                gridView.OnBind();
            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if(idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
        }
        
        #region gridView
                        
        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" UserName like '%{0}%'", Common.InjectionFilter.Filter(txtKeyword.Text.Trim()));
            }
            ds = bll.GetListEx(strWhere.ToString(), " RechargeId desc ");  
            gridView.DataSetSource = ds;
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
               
               
            }
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.TrimEnd(',');
            }
            return idlist;
        }

        #endregion



        #region 获取交易类型
        /// <summary>
        /// 获取交易类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetTradetype(int target)
        {
            //1:充值 
            string str = string.Empty;
            switch (target)
            {
                case 1:
                    str = "充值";
                    break;
                default:
                    break;
            }
            return str;
        }
        #endregion

    }
}
