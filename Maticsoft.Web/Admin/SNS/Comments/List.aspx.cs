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

namespace Maticsoft.Web.Admin.SNS.Comments
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.SNS.Comments bll = new Maticsoft.BLL.SNS.Comments();
        private int targetid = 0;
        private int type = -1;
        protected new int Act_DelData = 26;//删除评论行为

        protected override int Act_PageLoad { get { return 23; } } //社区管理_是否显示全部评论页面

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }

                if (Type == -1)
                {
                    btnBack.Visible = false;
                }

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

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
            if (bll.DeleteListEx(idlist))
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Type == 3)
            {
                Response.Redirect("/admin/SNS/UserAlbums/UserAlbums.aspx");
            }
            if (Type == 4)
            {
                Response.Redirect("/admin/SNS/Posts/PostsVideo.aspx");
            }
            if (Type >= 0 && Type < 3)
            {
                Response.Redirect("/admin/SNS/Posts/Posts.aspx");
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
                    case 0:
                        str = "动态";
                        break;

                    case 1:
                        str = "图片";
                        break;

                    case 2:
                        str = "商品";
                        break;

                    case 3:
                        str = "专辑";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public int TargetID
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Request.Params["targetid"]))
                {
                    targetid = Common.Globals.SafeInt(Request.Params["targetid"], 0);
                    if (targetid > 0 && Type > 0 && Type != 3)
                    {
                        Maticsoft.BLL.SNS.Posts bllPost = new BLL.SNS.Posts();
                        Maticsoft.Model.SNS.Posts model = bllPost.GetModel(targetid);
                        targetid = model.TargetId;
                    }
                }
                return targetid;
            }
        }

        public int Type
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    type = Common.Globals.SafeInt(Request.Params["type"], 0);
                }
                return type;
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
            if (Type >= 0)
            {
                strWhere.Append(" Type=" + Type + " ");
            }
            if (TargetID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" TargetId=" + TargetID + " ");
            }
            if (!string.IsNullOrEmpty(txtPoster.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("CreatedNickName like '%{0}%' ", txtPoster.Text.Trim());
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
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("Description like '%{0}%' ", txtKeyword.Text.Trim());
            }

            if (strWhere.Length > 0)
            {
                strWhere.Append(" and");
            }

            strWhere.Append("  1=1 order by CreatedDate desc");

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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.DeleteListEx(ID.ToString());
            gridView.OnBind();
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除评论(CommentID="+ID+")成功!", this);
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