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
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Consultations
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.Shop.Products.ProductConsults bll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        protected override int Act_PageLoad { get { return 404; } } //Shop_商品咨询管理_列表页
        protected new int Act_UpdateData = 407;    //Shop_商品咨询管理_编辑数据
        protected new int Act_DelData = 408;    //Shop_商品咨询管理_删除数据
        private Maticsoft.BLL.Shop.Products.ProductInfo infoBll = new ProductInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                    liDel.Visible = false;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }
       protected void btnStatus_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.UpdateStatusList(idlist,1);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量审核成功");
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

            #endregion gridView

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(ddlStatus.SelectedValue))
            {
                strWhere.AppendFormat(" IsReply ={0} ", ddlStatus.SelectedValue);
            }
            ds = bll.GetList(-1, strWhere.ToString(), " CreatedDate desc");

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
                Literal litReplay = (Literal)e.Row.FindControl("Literal3");

                HiddenField hfCid = (HiddenField)e.Row.FindControl("hfCid");


                object IsReply = DataBinder.Eval(e.Row.DataItem, "IsReply");
                if ((bool)IsReply)
                {
                    litReplay.Text = "";
                }
                else
                {
                    if (UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) ||  GetPermidByActID(Act_UpdateData) == -1)
                    {
                        litReplay.Text = "<a class=\"iframe\" href=\"Modify.aspx?id=" + hfCid.Value + "\">回复</a>";
                    }
                }

            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //#warning 代码生成警告：请检查确认真实主键的名称和类型是否正确
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //bll.Delete(ID);
            //gridView.OnBind();
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

                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
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
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetProductName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                long productId = Common.Globals.SafeLong(target, 0);
                str = infoBll.GetProductName(productId);
            }
            return str;
        }
        #endregion
    }
}