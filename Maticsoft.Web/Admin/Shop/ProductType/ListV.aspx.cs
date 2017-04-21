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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Shop.ProductType
{
    public partial class ListV : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 499; } } //Shop_扩展数据_编辑页（属性值列表页）
        protected new int Act_AddData = 500;    //Shop_扩展属性值_添加数据
        protected new int Act_UpdateData = 501;    //Shop_扩展属性值_编辑数据
        protected new int Act_DelData = 502;    //Shop_扩展属性值_删除数据
        
        //int Act_ShowInvalid = -1; //查看失效数据行为
        Maticsoft.BLL.Shop.Products.AttributeValue bll = new Maticsoft.BLL.Shop.Products.AttributeValue();
        BLL.Shop.Products.ProductType ptypeBll = new BLL.Shop.Products.ProductType();

        #region GetUrlParameters
        /// <summary>
        /// 类别ID
        /// </summary>
        private int ProductTypeId
        {
            get
            {
                int producrTypeId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["tid"]))
                {
                    producrTypeId = Maticsoft.Common.Globals.SafeInt(Request.Params["tid"], 0);
                }
                return producrTypeId;
            }
        }

        /// <summary>
        /// 属性ID
        /// </summary>
        private long AttId
        {
            get
            {
                long action = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["ed"]))
                {
                    action = Globals.SafeInt(Request.Params["ed"], 0);
                }
                return action;
            }
        }


        private int AddOrModify
        {
            get
            {
                int res = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["a"]))
                {
                    res = Globals.SafeInt(Request.Params["a"], 0);
                }
                return res;
            }
        }
        #endregion

        #region PageLoadingEvent
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                if (ProductTypeId == 0 || AttId == 0)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                    return;
                }

                ShowInfo();

               if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData)!=-1)
                {
                    btnDelete.Visible = false;
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData)!=-1)
                {
                    liAdd.Visible = false;
                }
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

            }
        } 
        #endregion

        private void ShowInfo()
        {
            Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
            Maticsoft.Model.Shop.Products.AttributeInfo model = bll.GetModel(AttId);
            if (model != null)
            {
                this.Literal1.Text = model.AttributeName;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            #region
            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    return;
            //}
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            //if (user.HasPermissionID(PermId_Modify))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}
            #endregion

            DataSet ds = new DataSet();
            ds = bll.GetListByAttribute(AttId);
            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
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
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) &&
                    GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl lbtnModify = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    lbtnModify.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("linkDel");
                    linkbtnDel.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long ID = (long)gridView.DataKeys[e.RowIndex].Value;
            ptypeBll.DeleteManage(null, null, ID);
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
            long ValueId = (long)this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                ptypeBll.SwapSeqManage(null, AttId, ValueId, Model.Shop.Products.SwapSequenceIndex.Down, false);
            }
            if (e.CommandName == "Rise")
            {
                ptypeBll.SwapSeqManage(null, AttId, ValueId, Model.Shop.Products.SwapSequenceIndex.Up, false);
            }
            gridView.OnBind();
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (AddOrModify > 0)
            {
                Response.Redirect("Step2.aspx?tid=" + ProductTypeId );
            }
            else
            {
                Response.Redirect("Modify2.aspx?tid=" + ProductTypeId + "&ed=" + AttId);
            }
        }
    }
}
