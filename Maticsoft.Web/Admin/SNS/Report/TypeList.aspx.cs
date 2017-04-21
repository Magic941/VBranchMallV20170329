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
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.ReportType
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 150; } } //运营管理_是否显示举报类型页面

        protected new int Act_DelData = 153;    //运营管理_举报类型_删除举报类型
        protected new int Act_UpdateData = 152;    //运营管理_举报类型_编辑举报类型
        protected new int Act_AddData = 151;    //运营管理_举报类型_添加举报类型
        protected new int Act_DeleteList = 154;    //运营管理_举报类型_批量删除举报类型

        private Maticsoft.BLL.SNS.ReportType bll = new Maticsoft.BLL.SNS.ReportType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList)!=-1)
                {
                    btnDelete.Visible = false;
                }
               if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData)!=-1)
                {
                    LiAdd.Visible = false;
                }

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
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除举报类型(id="+idlist+")成功", this);
                gridView.OnBind();
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除举报类型(id=" + idlist + ")失败", this);
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

            #endregion gridView

            int Status = Globals.SafeInt(this.rdoable.SelectedValue, -1);

            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSetSource = bll.GetSearchList(Status, txtKeyword.Text.Trim());
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

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    linkbtnDel.Visible = false;
                }

                HyperLink lkEdit = (HyperLink)e.Row.FindControl("lkEdit");

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    lkEdit.Visible = false;
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
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            Maticsoft.BLL.SNS.Report reportBll=new BLL.SNS.Report();
          int count=  reportBll.GetRecordCount(" ReportTypeID=" + ID);
            if (count > 0)
            {
                MessageBox.ShowFailTip(this,"该类型下有举报数据，请先删除举报数据");
                gridView.OnBind();
                return;
            }
            if (bll.Delete(ID))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除举报类型(id=" + ID + ")成功", this);
            }
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
                    int Status = Common.Globals.SafeInt(Args[1], 0);

                    if (Status == 1)
                    {
                        bll.UpdateList(0, Id.ToString());
                        gridView.OnBind();
                    }
                    else
                    {
                        bll.UpdateList(1, Id.ToString());
                        gridView.OnBind();
                    }
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