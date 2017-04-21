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
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Members.ActivityUser
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        //private Maticsoft.BLL.SNS.GradeConfig bll = new Maticsoft.BLL.SNS.GradeConfig();
        Maticsoft.BLL.Members.UserRank rankBll = new BLL.Members.UserRank();
        Maticsoft.BLL.Members.Users user = new BLL.Members.Users();
        protected override int Act_PageLoad { get { return 208; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              
         
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
            //string idlist = GetSelIDlist();
            //if (idlist.Trim().Length == 0) return;
            //rankBll.DeleteList(idlist);
            //Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #region 用户批量解冻
        /// <summary>
        /// 用户批量解冻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActivity_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (user.UpdateActiveStatus(idlist, 1))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                gridView.OnBind();
            }
        } 
        #endregion

        #region 冻结用户

        /// <summary>
        /// 用户批量冻结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnActivity_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (user.UpdateActiveStatus(idlist, 0))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
                gridView.OnBind();
            }
        }
        
        #endregion

        #region gridView

        public void BindData()
        {
            #region
            #endregion gridView

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.Append(" NickName like '%"+txtKeyword.Text+"%' ");
            }
            if (Common.Globals.SafeInt(dropActive.SelectedValue, -1) > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  DATEDIFF(month,LastLoginTime,GETDATE())<" + dropActive.SelectedValue + " ");
            }
            if (Common.Globals.SafeInt(dropUnActive.SelectedValue, -1) > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  DATEDIFF(month,LastLoginTime,GETDATE())>" + dropUnActive.SelectedValue + " ");
            }
           
            //if (!string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            //{
            //    if (strWhere.Length > 0)
            //    {
            //        strWhere.Append(" and ");
            //    }
            //    strWhere.Append("  convert(date,User_dateCreate)>='" + txtBeginTime.Text.Trim() + "' ");
            //}
            //if (Common.Globals.SafeInt(dropType.SelectedValue,-1)>-1)
            //{
            //    if (strWhere.Length > 0)
            //    {
            //        strWhere.Append(" and ");
            //    }
            //    strWhere.Append("  Activity=" + dropType.SelectedValue + " ");
            //}
            //if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            //{
            //    if (strWhere.Length > 0)
            //    {
            //        strWhere.Append(" and ");
            //    }
            //    strWhere.Append("  convert(date,User_dateCreate)<='" + txtEndTime.Text.Trim() + "' ");
            //}
            ds = user.GetSearchList("UU", strWhere.ToString());
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

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //if (rankBll.Delete(ID))
            //{
            //    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "删除成功！");
            //}
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Status")
            {
                if (e.CommandArgument != null)
                {
                    int Id = 0;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Id = Common.Globals.SafeInt(Args[0], 0);
                    AccountsPrincipal user = new AccountsPrincipal(Id);
                    User currentUser = new Maticsoft.Accounts.Bus.User(user);
                    bool Status = Common.Globals.SafeBool(Args[1], false);
                    currentUser.Activity = Status ? false : true;
                    currentUser.Update();
                    gridView.OnBind();
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

        #endregion
    }
}