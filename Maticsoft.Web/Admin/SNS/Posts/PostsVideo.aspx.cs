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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.PostsVideo
{
    public partial class List : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.Posts bll = new Maticsoft.BLL.SNS.Posts();
        protected int type = -1;
        protected new int Act_DelData = 24;

        protected override int Act_PageLoad { get { return 92; } } //社区管理_是否显示视频分享页面

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ApproveList)) && GetPermidByActID(Act_ApproveList) != -1)
                {
                    btnChecked.Visible = false;
                    btnCheckedUnpass.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string Result = GetSelIDlist();
            if (bll.DeleteListByNormalPost(Result,true,CurrentUser.UserID))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除视频动态(PostID=" + Result + ")成功", this);
                MessageBox.ShowSuccessTip(this, "删除成功！");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除视频动态(PostID=" + Result + ")失败", this);
                MessageBox.ShowSuccessTip(this, "删除失败！");
            }
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[6].Visible = false;
            }
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" type=3 ");
            if ((Common.Globals.SafeInt(rdocheck.SelectedValue, -1) > -1))
            {
                strWhere.AppendFormat("  and Status={0}", this.rdocheck.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                strWhere.Append(" and convert(date,CreatedDate)>='" + txtBeginTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
            
                strWhere.Append(" and convert(date,CreatedDate)<='" + txtEndTime.Text.Trim() + "' ");
            }
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    strWhere.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
                }

            strWhere.Append("  order by CreatedDate Desc ");
            DataSet ds = bll.GetList(strWhere.ToString());
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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("linkbtnDel");
              
            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.Equals("Delete"))
            {
                if (e.CommandName == null || e.CommandName.ToString() == "")
                {
                    return;
                }
                if (bll.DeleteEx(Globals.SafeInt(e.CommandName, -1),true,CurrentUser.UserID))
                {
                    gridView.OnBind();
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "删除失败！");
                }
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

        private string GetSelIDTypelist(string type)
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Values.Count > 1)
                    {
                        if (gridView.DataKeys[i].Values[1].ToString() == type)
                        {
                            if (type == "0")
                            {
                                idlist += gridView.DataKeys[i].Values[0] + ",";
                            }
                            else
                            {
                                idlist += gridView.DataKeys[i].Values[2] + ",";
                            }
                        }
                    }
                }
            }
            if (BxsChkd)
            {
                if (idlist.Length > 0)
                {
                    idlist = idlist.Substring(0, idlist.LastIndexOf(","));
                }
            }
            return idlist;
        }

        #endregion gridView

        #region 获取类型名称

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetTypeName(object target, object TargetId)
        {
            //动态的类型(动态0：一般类型;1:图片(搭配和晒货)类型;2：商品类型。)
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal:
                        str = "文字 ";
                        break;

                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo:
                        str = " <a  target='_blank' href='/Photo/Detail/" + TargetId.ToString() + "'>图片<a>";
                        break;

                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product:
                        str = " <a  target='_blank' href='/Product/Detail/" + TargetId.ToString() + "'>商品 <a>";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        #endregion 获取类型名称

        protected string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int Status = Common.Globals.SafeInt(target.ToString(), 0);
                switch (Status)
                {
                    case 0:
                        return "未审核";
                    case 1:
                        return "已审核";
                    case 2:
                        return "未通过";
                }
            }
            return str;
        }

        /// <summary>
        ///帖子的id
        /// </summary>

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateStatusList(idlist, (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新视频动态(PostID=" + idlist + ")为已审核状态成功", this);
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新视频动态(PostID=" + idlist + ")为已审核状态失败", this);
                MessageBox.ShowFailTip(this, "操作失败！");
            }
            gridView.OnBind();
        }

        /// <summary>
        /// 批量拒绝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckedUnPass_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateStatusList(idlist, (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.CheckedUnPass))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新视频动态(PostID=" + idlist + ")为未通过状态成功", this);
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新视频动态(PostID=" + idlist + ")为未通过状态失败", this);
                MessageBox.ShowFailTip(this, "操作失败！");
            }
            gridView.OnBind();
        }
    }
}