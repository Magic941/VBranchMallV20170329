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
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ProductReview
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 466; } } //Shop_商品评论管理_列表页
        protected new int Act_DelData =467;    //Shop_商品评论管理_删除数据
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.Shop.Products.ProductReviews bll = new Maticsoft.BLL.Shop.Products.ProductReviews();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
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

        #region gridView

        public void BindData()
        {
            #region

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[9].Visible = false;
            }
            #endregion gridView

            DataSet ds = new DataSet(); 
            StringBuilder strWhere = new StringBuilder();
            int status = -1;
            if (!string.IsNullOrWhiteSpace(this.ddlStatus.SelectedValue)) 
            {
                int res = Common.Globals.SafeInt(this.ddlStatus.SelectedValue, -1);
                if (res >= 0)
                {
                    status = res;
                }
            }
           // ds = bll.GetListLeftOrderItems(status);
            if (status > -1)
            {
                strWhere = strWhere.Append(" Status=" + status);
            }


            ds = bll.GetList(-1, strWhere.ToString(), " CreatedDate Desc");

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
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
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

        public string GetProudctName(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return new BLL.Shop.Products.ProductInfo().GetProductName(Common.Globals.SafeLong(obj.ToString(), 0));
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public string GetCommentStatus(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    switch (obj.ToString())
                    {
                        case "0":
                            return "未审核";
                        case "1":
                            return "已审核";
                        case "2":
                            return "审核失败";
                        default:
                            return "";
                    }
                }
            }
            return "";
        }


        protected void ddlAction_Changed(object sender, EventArgs e)
        {
            int ddlactionVal = Common.Globals.SafeInt(this.ddlAction.SelectedValue, -1);
            if (ddlactionVal > -1)
            {
                string idlist = GetSelIDlist();
                if (idlist.Trim().Length == 0) return;
                if (bll.AuditComment(idlist,ddlactionVal ))
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量操作成功！");
                    gridView.OnBind();
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "批量操作失败！");
                    gridView.OnBind();
                }
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择一个操作！");
                return;
            }
        }

        public string SubString(object target, string sign, int subLength)
        {
            return StringPlus.SubString(target, subLength, sign);
        }
    }
}