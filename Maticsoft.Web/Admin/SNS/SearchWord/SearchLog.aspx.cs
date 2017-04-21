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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.SNS.SearchWordLog
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.SNS.SearchWordLog bllSearchWordLog = new Maticsoft.BLL.SNS.SearchWordLog();
        private Maticsoft.BLL.SNS.HotWords bllHotWords = new Maticsoft.BLL.SNS.HotWords();

        protected override int Act_PageLoad { get { return 144; } } //运营管理_是否显示搜索日志管理页面
        protected new int Act_DeleteList = 145;    //运营管理_搜索日志管理_批量删除搜索日志
        protected new int Act_DelData = 145;    //运营管理_搜索日志管理_批量删除搜索日志

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    liDel.Visible = false;
                    lbtnDelete.Visible = false;
                    btnDelete.Visible = false;
                }


                //gridView2.OnBind();
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
            if (bllSearchWordLog.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除搜索日志(id=" + idlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            #region

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[6].Visible = false;
            }
            StringBuilder strWhere = new StringBuilder();

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
            if (!string.IsNullOrEmpty(txtPoster.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("CreatedNickName like '%{0}%' ", txtPoster.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("SearchWord like '%{0}%' ", txtKeyword.Text.Trim());
            }

            if (PageValidate.IsDateTime(txtBeginTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  convert(date,CreatedDate)>='" + txtBeginTime.Text.Trim() + "' ");
            }
            if (PageValidate.IsDateTime(txtEndTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  convert(date,CreatedDate)<='" + txtEndTime.Text.Trim() + "' ");
            }
            #endregion gridView

            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSetSource = bllSearchWordLog.GetList(-1, strWhere.ToString(), " CreatedDate desc");
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
            bllSearchWordLog.Delete(ID);
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

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            //状态(0.不可用,1.可用)
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "不可用";
                        break;

                    case 1:
                        str = "可用";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        protected void btnPush_Click(object sender, EventArgs e)
        {
            if (this.chkDelete.Checked)
            {
                //先清空热门搜索管理所有数据
                bllHotWords.Delete();
            }

            //  gridView2.OnBind();
            int Top = 0;
            if (this.dropTop.SelectedValue == "0")
            {
                Top = Globals.SafeInt(this.txtTop.Text, 0);
            }
            else
            {
                Top = Globals.SafeInt(this.dropTop.SelectedValue, 0);
            }
            if (bllSearchWordLog.GetHotHotWordssList(Top))
            {
                //if (!DataSetTools.DataSetIsNull(ds))
                //{
                //    foreach (DataRow dr in ds.Tables[0].Rows)
                //    {
                //        string SearchWord = Globals.SafeString(dr["SearchWord"], "");
                //        if (!string.IsNullOrWhiteSpace(SearchWord))
                //        {
                //            Maticsoft.Model.SNS.HotWords model = new Model.SNS.HotWords();
                //            model.IsRecommend = true;
                //            model.KeyWord = SearchWord;
                //            model.Sequence = bllHotWords.GetMaxSequence();
                //            model.Status = 1;
                //            model.CreatedDate = DateTime.Now;
                //            bllHotWords.Add(model);
                //        }
                //    }
                //}
                this.txtTop.Text = "";
                this.dropTop.SelectedValue = "0";
                Response.Redirect("/admin/SNS/SearchWord/SearchTop.aspx");
            }

            //gridView2.OnBind();
        }

        //public void BindHotSearchData()
        //{
        //    gridView2.DataSetSource = bllHotWords.GetList("");
        //}
        //protected void gridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gridView2.PageIndex = e.NewPageIndex;
        //    gridView2.OnBind();
        //}

        protected void gridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
        }

        //protected void gridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int ID = (int)gridView2.DataKeys[e.RowIndex].Value;
        //    bllHotWords.Delete(ID);
        //    gridView2.OnBind();
        //}

        //private string GetSelIDList()
        //{
        //    string idlist = "";
        //    bool BxsChkd = false;
        //    for (int i = 0; i < gridView.Rows.Count; i++)
        //    {
        //        CheckBox ChkBxItem = (CheckBox)gridView2.Rows[i].FindControl(gridView2.CheckBoxID);
        //        if (ChkBxItem != null && ChkBxItem.Checked)
        //        {
        //            BxsChkd = true;

        //            if (gridView2.DataKeys[i].Value != null)
        //            {
        //                //idlist += gridView2.Rows[i].Cells[1].Text + ",";
        //                idlist += gridView2.DataKeys[i].Value.ToString() + ",";
        //            }
        //        }
        //    }
        //    if (BxsChkd)
        //    {
        //        idlist = idlist.Substring(0, idlist.LastIndexOf(","));
        //    }
        //    return idlist;
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetState(object target)
        {
            //状态 0:不可用 1：可用 2:被推荐到逛宝贝的热搜词中,3 被推荐到搜索框下面的热搜词中
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "不可用";
                        break;

                    case 1:
                        str = "可用";
                        break;

                    case 3:
                        str = "被推荐到逛宝贝的热搜词中";
                        break;

                    case 4:
                        str = "被推荐到搜索框下面的热搜词中";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
    }
}