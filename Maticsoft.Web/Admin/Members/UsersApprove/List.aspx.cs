/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01 2012年10月25日 15:26:52 Rock 初版
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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Members.UsersApprove
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 175; } } //用户管理_是否显示实名认证管理页面

        protected new int Act_ApproveList = 177;    //用户管理_实名认证_批量审核认证信息
        protected new int Act_DeleteList = 176;    //用户管理_实名认证_批量删除认证信息

        private Maticsoft.BLL.Members.UsersApprove bll = new Maticsoft.BLL.Members.UsersApprove();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList)!=-1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
               if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ApproveList)) && GetPermidByActID(Act_ApproveList) != -1)
               {
                   btnBatchAccess.Visible = false;
                   btnBatchUnAcc.Visible = false;
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
            ds = bll.GetApproveList(this.ddlApproveStatus.SelectedValue, this.txtKeyword.Text.Trim());

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
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //#warning 代码生成警告：请检查确认真实主键的名称和类型是否正确
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //bll.Delete(ID);
            //gridView.OnBind();
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
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        protected string GetApproveStatus(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    switch (obj.ToString())
                    {
                        case"0":
                            return "未 审 核";
                        case "1":
                            return "审核通过";
                        case "2":
                            return "审核失败";
                        default:
                            return "审核失败";
                    }
                }
                else
                {
                    return "审核失败";
                }
            }
            else
            {
                return "审核失败";
            }
        }

        protected void btnBatchAccess_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.BatchUpdate(idlist.TrimEnd(','), "1"))
            {
                BLL.Members.UsersExp userExpBll = new BLL.Members.UsersExp();
                userExpBll.UpdateIsDPI(idlist.TrimEnd(','), 1);
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量审核成功！");
            gridView.OnBind();
        }

        protected void btnBatchUnAcc_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.BatchUpdate(idlist.TrimEnd(','), "0"))
            {
                BLL.Members.UsersExp userExpBll = new BLL.Members.UsersExp();
                userExpBll.UpdateIsDPI(idlist.TrimEnd(','), 0);
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量拒绝成功！");
            gridView.OnBind();
        }
    }
}