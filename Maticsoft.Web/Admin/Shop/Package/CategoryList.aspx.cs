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
using Maticsoft.Web;

namespace Maticsoft.Web.Admin.Shop.Package
{
    public partial class CategoryList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 454; } } //Shop_包装类别管理_列表页
        protected new int Act_AddData = 455;    //Shop_包装类别管理_添加数据
        protected new int Act_UpdateData = 456;    //Shop_包装类别管理_编辑数据
        protected new int Act_DelData = 457;    //Shop_包装类别管理_删除数据

        //int Act_ShowInvalid = -1; //查看失效数据行为
        private Maticsoft.BLL.Shop.Package.PackageCategory bll = new Maticsoft.BLL.Shop.Package.PackageCategory();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    AddLi.Visible = false;
                }
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
 
            StringBuilder sb=new StringBuilder();
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[4].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            if (!string.IsNullOrEmpty(txtKeyword.Text))
            {
                sb.Append("Name like '%"+txtKeyword.Text+"%'");
            }

            #endregion gridView

            gridView.DataSetSource = bll.GetList(sb.ToString());
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
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            if (bll.Delete(ID))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "IsMenu")
            //{
            //    if (e.CommandArgument != null)
            //    {
            //        int Id = 0;
            //        string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            //        Id = Common.Globals.SafeInt(Args[0], 0);
            //        bool IsMenu = Common.Globals.SafeBool(Args[1], false);

            //        if (IsMenu)
            //        {
            //            bll.UpdateIsMenu(0, Id.ToString());
            //            gridView.OnBind();
            //        }
            //        else
            //        {
            //            bll.UpdateIsMenu(1, Id.ToString());
            //            gridView.OnBind();
            //        }
            //    }
            //}
            //if (e.CommandName == "MenuIsShow")
            //{
            //    if (e.CommandArgument != null)
            //    {
            //        int Id = 0;
            //        string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            //        Id = Common.Globals.SafeInt(Args[0], 0);
            //        bool MenuIsShow = Common.Globals.SafeBool(Args[1], false);

            //        if (MenuIsShow)
            //        {
            //            bll.UpdateMenuIsShow(0, Id.ToString());
            //            gridView.OnBind();
            //        }
            //        else
            //        {
            //            bll.UpdateMenuIsShow(1, Id.ToString());
            //            gridView.OnBind();
            //        }
            //    }
            //}
            //if (e.CommandName == "Status")
            //{
            //    if (e.CommandArgument != null)
            //    {
            //        int Id = 0;
            //        string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            //        Id = Common.Globals.SafeInt(Args[0], 0);
            //        int Status = Common.Globals.SafeInt(Args[1], 0);

            //        if (Status == 1)
            //        {
            //            bll.UpdateStatus(0, Id.ToString());
            //            gridView.OnBind();
            //        }
            //        else
            //        {
            //            bll.UpdateStatus(1, Id.ToString());
            //            gridView.OnBind();
            //        }
            //    }
            //}
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除包装类型(id=" + idlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除包装类型(id=" + idlist + ")失败", this);
            }
            gridView.OnBind();
        }
    }
}