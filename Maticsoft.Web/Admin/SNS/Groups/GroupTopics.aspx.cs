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

namespace Maticsoft.Web.Admin.SNS.GroupTopics
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.SNS.GroupTopics bllTopic = new Maticsoft.BLL.SNS.GroupTopics();
        private Maticsoft.BLL.SNS.Groups bllGroup = new BLL.SNS.Groups();
        private int status = -1;
        protected int groupid = 0;
        protected override int Act_PageLoad { get { return 108; } } //社区管理_是否显示群组主题页面

        protected new int Act_DelData = 109;    //社区管理_群组主题_删除群组主题
        protected new int Act_DeleteList = 110;    //社区管理_群组主题_批量删除群组主题
        protected new int Act_ApproveList = 111;    //社区管理_群组主题_批量审核群组主题信息

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
                    ddlBatch.Visible = false;
                }
                ltlTitle.Text += GetGroupName(GroupID);
                if (GroupID <= 0)
                {
                    BtnBack.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region 小组的状态

        /// <summary>
        /// 帖子的状态码
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

        /// <summary>
        /// 得到群组的名称
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public string GetGroupName(int GroupId)
        {
            if (GroupId > 0)
            {
                Maticsoft.Model.SNS.Groups model = bllGroup.GetModel(GroupId);
                if (model != null)
                {
                    return "[" + model.GroupName + "]";
                }
            }
            return "";
        }

        /// <summary>
        /// 小组状态码
        /// </summary>
        public int GroupID
        {
            get
            {
                string strid = Request.Params["groupid"];
                if (!string.IsNullOrWhiteSpace(strid))
                {
                    groupid = Globals.SafeInt(strid, 0);
                }
                return groupid;
            }
        }

        #endregion 小组的状态

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[9].Visible = false;
            }
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (Status >= 0)
            {
                strWhere.Append(" Status=" + status + " ");
            }
            if (GroupID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" GroupID=" + GroupID + " ");
            }
            if ((Common.Globals.SafeInt(ddtRecommand.SelectedValue, -1) > -1))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("  IsAdminRecommend={0}", this.ddtRecommand.SelectedValue);
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

            ds = bllTopic.GetList(strWhere.ToString());

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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RecommendIndex" )
            {
                if (e.CommandArgument != null)
                {
                    int TopicID = 0;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    TopicID = Common.Globals.SafeInt(Args[0], 0);
                   bool IsAdmin = Common.Globals.SafeBool(Args[1], false);
                   if (bllTopic.UpdateAdminRecommand(TopicID, !IsAdmin))
                    {
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "推荐群组主题（id="+TopicID+"）成功!", this);
                        gridView.OnBind();
                    }
                }
            }
            if (e.CommandName == "RecommendChannal")
            {
                if (e.CommandArgument != null)
                {
                    int TopicID = 0;
                    int IsRecommend = 1;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    TopicID = Common.Globals.SafeInt(Args[0], 0);
                    IsRecommend = Common.Globals.SafeInt(Args[1], 1);
                    if (bllTopic.UpdateRecommand(TopicID, IsRecommend == 0 ? 1 : 0) ? true : false)
                    {
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "推荐群组主题（id="+TopicID+"）成功!", this);
                        gridView.OnBind();

                    }
                }
            }
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
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                if (null != lbtnDelete)
                {
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            if (bllTopic.DeleteListEx(ID.ToString()))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除群组主题（id="+ID+"）成功!", this);
                MessageBox.ShowSuccessTip(this, "删除成功！");
                gridView.OnBind();
            }
            else
            {
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

        #endregion gridView

        #region 获取状态

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            //状态 0:未审核 1：已审核 2：审核未通过
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "未审核";
                        break;

                    case 1:
                        str = "已审核";
                        break;

                    case 2:
                        str = "未通过";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        #endregion 获取状态

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bllTopic.DeleteListEx(idlist);
            MessageBox.ShowSuccessTip(this, "删除成功！");
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除群组主题（id=" + idlist + "）成功!", this);
            gridView.OnBind();
        }

        protected void ddlBatch_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this,"请选择主题");
                return;
            }
            if(String.IsNullOrWhiteSpace(this.ddlBatch.SelectedValue))
            {
                MessageBox.ShowFailTip(this, "请选择操作类型");
                return;
            }
            int type = Common.Globals.SafeInt(this.ddlBatch.SelectedValue, 0);
            switch (type)
            {
                //批量审核
                case  1:
                    if (bllTopic.UpdateStatusList(idlist, Maticsoft.Model.SNS.EnumHelper.TopicStatus.Checked))
                    {
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新群组主题（id=" + idlist + "）状态成功!", this);
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新群主主题（id=" + idlist + "）状态为未通过失败!", this);
                    }
                    break;
                //批量拒绝
                case 2:
                    if (bllTopic.UpdateStatusList(idlist, Maticsoft.Model.SNS.EnumHelper.TopicStatus.CheckedUnPass))
                    {
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新群主主题（id=" + idlist + "）状态为未通过成功!", this);
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                    }
                    break;
                   // 批量禁言
                case 3:
                    Maticsoft.BLL.SNS.GroupUsers groupuserBll = new BLL.SNS.GroupUsers();
                    if (groupuserBll.UpdateStatusByTopicIds(idlist, (int)Maticsoft.Model.SNS.EnumHelper.GroupUserStatus.ForbidSpeak))
                    {
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "对发表主题（id=" + idlist + "）的用户禁言成功!", this);
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                    }
                    break;
                default:
                    break;
            }
            gridView.OnBind();
        }
        

        /// <summary>
        /// 返回列表页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/SNS/Groups/Group.aspx?GroupId=" + GroupID + "");
        }
    }
}