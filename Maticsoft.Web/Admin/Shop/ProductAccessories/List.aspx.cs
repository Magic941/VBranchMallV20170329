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
namespace Maticsoft.Web.ProductAccessories
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为


       private  Maticsoft.BLL.Shop.Products.ProductAccessorie bll = new Maticsoft.BLL.Shop.Products.ProductAccessorie();
      // private Maticsoft.BLL.Shop.Products.ProductInfo prodBll = new Maticsoft.BLL.Shop.Products.ProductInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (Type)
                {
                    case 1:
                        Literal1.Text="配件组合";
                        Literal2.Text = "配件组合";
                        Literal3.Text = "配件组合";
                     
                        break;
                    case 2:
                        Literal1.Text = "优惠组合";
                        Literal2.Text = "优惠组合";
                        Literal3.Text = "优惠组合";
                        break;
                }

                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)))
                {
                    btnDelete.Visible = false;
                }
                

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

                gridView.OnBind();
            }
        }

        #region 接收参数
        public long ProductId
        {
            get
            {
                long pid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["pid"]))
                {
                    pid = Globals.SafeLong(Request.Params["pid"], 0);
                }
                return pid;
            }
        }
        /// <summary>
        /// 类型 1：配件   2：组合套装
        /// </summary>
        public int  Type
        {
            get
            {
                int  type = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["acctype"]))
                {
                    type = Globals.SafeInt(Request.QueryString["acctype"], 0);
                }
                return type;
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Products/ProductsInStock.aspx?SaleStatus=1");
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
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelError);
            }
        }
        
        #region gridView
                        
        public void BindData()
        { 
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" ProductId ={0} and  type = {1} ",ProductId, Type);
            if (txtKeyword.Text.Trim() != "")
            {      
                strWhere.AppendFormat(" name like '%{0}%'", txtKeyword.Text.Trim());
            }            
            ds = bll.GetList(strWhere.ToString()); 
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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("linkbtnDel");
                linkbtnDel.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                object obj2 = DataBinder.Eval(e.Row.DataItem, "DiscountType");
                if ((obj2 != null) && ((obj2.ToString() != "")))
                {
                    switch (obj2.ToString())
                    {
                        case "1":
                            obj2 = "金额";
                            break;
                        case "2":
                            obj2 = "折扣";
                            break;
                    }
                    e.Row.Cells[2].Text = obj2.ToString();
                }
               
            }
        }
        
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (bll.DeleteEx((int)gridView.DataKeys[e.RowIndex].Value))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
            else
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelError);
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
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #endregion





    }
}
