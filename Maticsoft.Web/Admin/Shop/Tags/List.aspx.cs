/**
* List.cs
*
* 功 能： N/A
* 类 名： List
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01						   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Tags
{
    public partial class List : PageBaseAdmin
    {

        private Maticsoft.BLL.Shop.Tags.Tags bll = new BLL.Shop.Tags.Tags();
        protected override int Act_PageLoad { get { return 545; } } //Shop_标签管理_列表页

        protected new int Act_DelData = 546;    //Shop_标签管理_添加数据   
        protected new int Act_UpdateData = 547;    //Shop_标签管理_编辑数据
        protected new int Act_AddData = 548;    //Shop_标签管理_删除数据    
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    lbtnDelete.Visible = false;
                    btnDelete.Visible = false;
                }
                //if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                //{
                //    tableAdd.Visible = false;
                //}

               
            }
        }    

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除商品标签(id="+idlist+")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
        }

        #region gridView

        public void BindData()
        {
            #region

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[6].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[7].Visible = false;
            }
            #endregion gridView
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" TagName like '%{0}%'", txtKeyword.Text.Trim());
            }

            //if (strWhere.Length > 1)
            //{
            //    strWhere.Append(" and  ");
            //}
            //strWhere.AppendFormat(" TagCategoryId in (select ID from Shop_TagCategories)");

            gridView.DataSetSource = bll.GetListEx(strWhere.ToString());
            gridView.DataBind();
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
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
            MessageBox.ShowSuccessTip(this, "删除成功！");
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

        public string GetCateName(object target)
        {
            int CateId=Globals.SafeInt(target.ToString(), 0);
            if (CateId <= 0)
            {
                return "暂无分类信息";
            }
            else
            {
                BLL.Shop.Tags.TagCategories bllCate = new BLL.Shop.Tags.TagCategories();
                return bllCate.GetFullNameByCache(CateId);
            }
            
        }

        public string GetStatus(object target)
        {
            //状态 1:不可用 ：2可用
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 1:
                        str = "可用";
                        break;

                    case 0:
                        str = "不可用";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public string IsRecommand(object target)
        {
            //状态 0:推荐 ：1不推荐
            string str = string.Empty;
            bool Target = bool.Parse(target.ToString());
            if (!StringPlus.IsNullOrEmpty(target))
            {
                if (Target)
                {
                    str = "推荐";
                }
                else
                {
                    str = "不推荐";
                }
            }
            return str;
        }

        /// <summary>
        /// 推荐到分类标签页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropIsRecommand_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (this.dropIsRecommand.SelectedValue != "-1")
            {
                if (bll.UpdateIsRecommand(this.dropIsRecommand.SelectedValue, idlist))
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipBatchUpdateOK);
                    gridView.OnBind();
                }
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropStatus_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (this.dropStatus.SelectedValue != "-1")
            {
                if (bll.UpdateStatus(Globals.SafeInt(this.dropStatus.SelectedValue, 0), idlist))
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipBatchUpdateOK);
                    gridView.OnBind();
                }
            }
        }
    }
}