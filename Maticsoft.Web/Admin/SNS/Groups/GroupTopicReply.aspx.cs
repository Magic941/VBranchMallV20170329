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
namespace Maticsoft.Web.Admin.SNS.GroupTopicReply
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 567; } } //SNS_群组主题回复管理_列表页
        protected new int Act_DelData = 568;    //SNS_群组主题回复管理_删除数据
        protected new int Act_ApproveList =569;    //SNS_群组主题回复管理_批量审核数据

		Maticsoft.BLL.SNS.GroupTopicReply replyBll = new Maticsoft.BLL.SNS.GroupTopicReply();
        private int status = -1;
        protected int groupid = 0;
        protected int topicid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            topicid = TopicID;
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ApproveList)) && GetPermidByActID(Act_ApproveList) != -1)
                {
                    btnCheckedUnpass.Visible = false;
                    btnChecked.Visible = false;
                    btnForbidSpeak.Visible = false;
                }

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        
        #region gridView
                        
        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[8].Visible = false;
            }
            
            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();

            if (Status >= 0)
            {
                strWhere.Append("Status="+status+"");
            
            }
            if (TopicID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("TopicID=" + TopicID + "");

            }
            if (GroupID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("GroupID=" + GroupID + "");

            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("ReplyNickName like '%{0}%' or Description like '%{0}%'", txtKeyword.Text.Trim());
            }            
            ds = replyBll.GetListEx(strWhere.ToString());            
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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");

            }
        }
        
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {            
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            if (replyBll.Delete(ID))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除主题回复(RePlyId="+ID+")成功!", this);
            }
            gridView.OnBind();
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
            replyBll.DeleteListEx(idlist);
            MessageBox.ShowSuccessTip(this, "删除成功！");
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除主题回复(RePlyIds=" + idlist + ")成功!", this);
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
            if (replyBll.UpdateStatusList(idlist, Maticsoft.Model.SNS.EnumHelper.TopicStatus.Checked))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新主题状态(RePlyIds=" + idlist + ")成功!", this);
            }
            else
            {
                MessageBox.ShowSuccessTip(this, "操作失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新主题状态(RePlyIds=" + idlist + ")失败!", this);

            }
            gridView.OnBind();
        }
        /// <summary>
        /// 批量禁言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnForbidSpeak_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            Maticsoft.BLL.SNS.GroupUsers groupuserBll = new BLL.SNS.GroupUsers();
            if (groupuserBll.UpdateStatusByTopicReplyIds(idlist, (int)Maticsoft.Model.SNS.EnumHelper.GroupUserStatus.ForbidSpeak))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量禁言用户成功!", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量禁言用户失败!", this);
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
            if (replyBll.UpdateStatusList(idlist, Maticsoft.Model.SNS.EnumHelper.TopicStatus.CheckedUnPass))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量审核主题(RePlyIds=" + idlist + ")成功!", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量审核主题(RePlyIds=" + idlist + ")失败!", this);
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
        ///帖子的id
        /// </summary>
        public int TopicID
        {
            get
            {
                string strid = Request.Params["topicid"];
                if (!string.IsNullOrWhiteSpace(strid))
                {
                    topicid = Globals.SafeInt(strid, 0);
                }
                return topicid;
            }
        }




        ///// <summary>
        ///// 得到群组的名称
        ///// </summary>
        ///// <param name="GroupId"></param>
        ///// <returns></returns>
        //public string GetGroupName(int GroupId)
        //{
        //    if (GroupId > 0)
        //    {
        //        Maticsoft.Model.SNS.Groups model = bllGroup.GetModel(GroupId);
        //        if (model != null)
        //        {
        //            return "[" + model.GroupName + "]";
        //        }
        //    }
        //    return "";
        //}


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
                        str = "审核未通过";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion




    }
}
