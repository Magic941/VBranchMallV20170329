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
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Settings.SEORelation
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 392; } } //设置_SEO关联链接管理_列表页
        protected new int Act_AddData = 394;    //设置_SEO关联链接管理_添加数据
        protected new int Act_UpdateData = 395;    //设置_SEO关联链接管理_编辑数据
        protected new int Act_DelData = 396;    //设置_SEO关联链接管理_删除数据
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.Settings.SEORelation bll = new Maticsoft.BLL.Settings.SEORelation();

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
                    liAdd.Visible = false;
                }
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
            bll.DeleteList(idlist);
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
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
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat("KeyName like '%{0}%'", txtKeyword.Text.Trim());
            }
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
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("lbtnDel");
                    delbtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    LinkButton updatebtn = (LinkButton)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
            gridView.OnBind();
        }

        public void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridView.EditIndex = e.NewEditIndex;
            gridView.OnBind();
        }

        public void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridView.EditIndex = -1;
            gridView.OnBind();
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = (int)gridView.DataKeys[e.RowIndex].Value;
            string keyName = (gridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
            string linkURL = (gridView.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;

            if (string.IsNullOrWhiteSpace(keyName))
            {
                MessageBox.ShowFailTip(this, "请输入链接文字！");
                e.Cancel = true;
                gridView.OnBind();
                return;
            }
            if (string.IsNullOrWhiteSpace(linkURL))
            {
                MessageBox.ShowFailTip(this, "请输入链接地址！");
                e.Cancel = true;
                gridView.OnBind();
                return;
            }
            //if (!IsUrl(linkURL))
            //{
            //    MessageBox.ShowFailTip(this, "请输入正确的链接地址,例如：http://www.maticsoft.com！");
            //    return;
            //}

            CheckBox chbIsCMS = gridView.Rows[e.RowIndex].FindControl("IsCMS") as CheckBox;
            bool IsCMS = false;
            if (chbIsCMS != null)
            {
                IsCMS = chbIsCMS.Checked;
            }
            CheckBox chbIsShop = gridView.Rows[e.RowIndex].FindControl("IsShop") as CheckBox;
            bool IsShop = false;
            if (chbIsShop != null)
            {
                IsShop = chbIsShop.Checked;
            }
            CheckBox chbIsSNS = gridView.Rows[e.RowIndex].FindControl("IsSNS") as CheckBox;
            bool IsSNS = false;
            if (chbIsSNS != null)
            {
                IsSNS = chbIsSNS.Checked;
            }
            CheckBox chbIsComment = gridView.Rows[e.RowIndex].FindControl("IsComment") as CheckBox;
            bool IsComment = false;
            if (chbIsComment != null)
            {
                IsComment = chbIsComment.Checked;
            }
            CheckBox chbIsActive = gridView.Rows[e.RowIndex].FindControl("IsActive") as CheckBox;
            bool IsActive = false;
            if (chbIsActive != null)
            {
                IsActive = chbIsActive.Checked;
            }

            Model.Settings.SEORelation model = new Model.Settings.SEORelation();
            model.RelationID = id;
            model.KeyName = keyName;
            model.LinkURL = linkURL;
            model.IsCMS = IsCMS;
            model.IsShop = IsShop;
            model.IsSNS = IsSNS;
            model.IsComment = IsComment;
            model.IsActive = IsActive;
            bll.Update(model);
            gridView.EditIndex = -1;
            gridView.OnBind();
        }

        private bool IsUrl(string s)
        {
            string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
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
    }
}