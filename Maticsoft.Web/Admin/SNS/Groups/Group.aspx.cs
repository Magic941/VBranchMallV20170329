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
using System.IO;

namespace Maticsoft.Web.Admin.SNS.Groups
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.SNS.Groups bll = new Maticsoft.BLL.SNS.Groups();
        private int status = -1;

        protected override int Act_PageLoad { get { return 105; } } //社区管理_是否显示群组列表页面

        protected new int Act_DeleteList = 106;    //社区管理_群组列表_批量删除群组信息
        protected new int Act_ApproveList = 107;    //社区管理_群组列表_批量审核群组信息

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList)!=-1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ApproveList)) && GetPermidByActID(Act_ApproveList)!=-1)
                {
                    btnCheckedUnpass.Visible = false;
                    btnChecked.Visible = false;
                }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

            }
        }

        #region 小组的状态

        /// <summary>
        /// 小组状态码
        /// </summary>
        public int Status
        {
            get
            {
                string strid = Request.Params["status"];
                if (!string.IsNullOrWhiteSpace(strid))
                {
                    status = Globals.SafeInt(strid, 0);
                }
                return status;
            }
        }

        #endregion 小组的状态


        #region 按钮操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

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

        protected string GetThumb(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string thumb = target.ToString();
                string thumbUrl = String.Format(thumb, "T_");
                if (File.Exists(Server.MapPath(thumbUrl)))
                {
                    return thumbUrl;
                }
                str = thumb.Replace("{0}", "T80x80_");
            }
            return str;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.DeleteListEx(idlist);
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateStatusList(idlist, Maticsoft.Model.SNS.EnumHelper.GroupStatus.Checked))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量审核群组(GroupID="+idlist+")成功!", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量审核群组(GroupID=" + idlist + ")失败!", this);
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
            if (bll.UpdateStatusList(idlist, Maticsoft.Model.SNS.EnumHelper.GroupStatus.CheckedUnPass))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量拒绝群组(GroupID=" + idlist + ")申请成功!", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量拒绝群组(GroupID=" + idlist + ")申请失败!", this);
            }
            gridView.OnBind();
        }
        #endregion


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
            if (Status >= 0)
            {
                strWhere.Append(" Status=" + status + " ");
            }
            if ((Common.Globals.SafeInt(rdorecommand.SelectedValue, -1) > -1))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("  IsRecommand={0}", this.rdorecommand.SelectedValue);
            }
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
            if (!string.IsNullOrWhiteSpace(strWhere.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    strWhere.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    strWhere.AppendFormat(" CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }

                strWhere.AppendFormat("GroupName like '%{0}%'", txtKeyword.Text.Trim());
            }

            if (strWhere.Length > 0)
            {
                strWhere.Append(" and ");
            }

            strWhere.Append(" 1=1  order by CreatedDate Desc ");

            ds = bll.GetList(strWhere.ToString());

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
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RecommendHome")
            {
                if (e.CommandArgument != null)
                {
                    int GroupID = 0;
                    int recommend;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    GroupID = Common.Globals.SafeInt(Args[0], 0);
                    recommend = Common.Globals.SafeInt(Args[1], 0) == 1 ? 0 : 1;    //设置推荐到首页
                    if (bll.UpdateRecommand(GroupID, recommend))
                    {
                        gridView.OnBind();

                        // Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                    }
                }
            }
            else if (e.CommandName == "RecommendPro")
            {
                if (e.CommandArgument != null)
                {
                    int GroupID = 0;
                    int recommend;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    GroupID = Common.Globals.SafeInt(Args[0], 0);
                    recommend = Common.Globals.SafeInt(Args[1], 0) == 2 ? 0 : 2;    //设置推荐到首页
                    if (recommend == 0)
                    {
                    }
                    if (bll.UpdateRecommand(GroupID, recommend))
                    {
                        gridView.OnBind();

                        // Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                    }
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.DeleteListEx(ID.ToString());
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