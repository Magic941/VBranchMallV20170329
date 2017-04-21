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
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.Comments
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.CMS.Comment bll = new Maticsoft.BLL.CMS.Comment();
        protected new int Act_DelData = 589;//CMS_评论管理_删除数据

        protected override int Act_PageLoad { get { return 588; } } //CMS_评论管理_列表页

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
                txtBeginTime.Attributes["readonly"] = "true";
                txtEndTime.Attributes["readonly"] = "true";
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
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, "删除成功！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除评论(CommentID=" + idlist + ")成功!", this);
                gridView.OnBind();
            }
            else
            {
                
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除评论(CommentID=" + idlist + ")失败!", this);
                MessageBox.ShowFailTip(this, "删除失败！");
            }
        }

   
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetType(object target)
        {
            //状态 0:未审核 1：已审核 2：审核未通过
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                  

                    case 1:
                        str = "图片评论";
                        break;

                    case 2:
                        str = "视频评论";
                        break;

                    case 3:
                        str = "内容评论";
                        break;
                    case 4:
                        str = "帖子评论";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateList(idlist, 1))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                gridView.OnBind();
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
            
        }

        /// <summary>
        /// 批量未审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateState_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateList(idlist,0))
            {
               MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                gridView.OnBind();
            }
            else
            {
               MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
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

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }

            #endregion gridView

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  convert(date,CreatedDate)>='" + txtBeginTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  convert(date,CreatedDate)<='" + txtEndTime.Text.Trim() + "' ");
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("Description like '%{0}%' ", txtKeyword.Text.Trim());
            }

            //if (strWhere.Length > 0)
            //{
            //    strWhere.Append(" and");
            //}

            //strWhere.Append("  1=1 order by CreatedDate desc");

          //  ds = bll.GetList(strWhere.ToString());
            ds = bll.GetList(0, strWhere.ToString(), "  CreatedDate desc ");
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
            if (bll.Delete(ID))
            {
                gridView.OnBind();
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除评论(CommentID=" + ID + ")成功!", this);
                MessageBox.ShowSuccessTip(this, "删除成功！");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除评论(CommentID=" + ID + ")失败!", this);
                MessageBox.ShowFailTip(this, "删除失败！");
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