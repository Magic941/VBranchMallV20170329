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
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.SNS.Photos
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 590; } } //SNS_图片分享管理_列表页
        protected new int Act_UpdateData = 591;    //SNS_图片分享管理_编辑数据
        protected new int Act_DelData = 21;    //社区_分享_晒货管理_删除数据

        private Maticsoft.BLL.SNS.Photos bll = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.Members.SiteMessage siteBll=new SiteMessage();

        public int  Type = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
               Type = CateType;
               if (CateType == 0)//表示是未分类图片
               {
                   this.txtCateParent.Visible = false;
                   this.PhotoCate.Visible = false;
               }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

            }
        }

        public int CateType
        {
            get
            {
                int _type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    _type = Globals.SafeInt(Request.Params["type"], 1);
                }
                return _type;
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
            int result = 0;
            DataSet ds = bll.DeleteListEx(idlist, out result,true,CurrentUser.UserID);
            if (result > 0)
            {
                if (ds != null)
                {
                    //物理删除文件
                    PhysicalFileInfo(ds.Tables[0]);
                }
             
                MessageBox.ShowSuccessTip(this, "批量删除成功");
            }
            else
            {
                MessageBox.ShowFailTip(this, "批量删除失败");
                return;
            }
            gridView.OnBind();
        }

        //批量转移
        protected void btnMove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int SNSPhotoCateId = Globals.SafeInt(this.PhotoCategory.SelectedValue, 0);
            if (!bll.UpdateCateList(idlist, SNSPhotoCateId))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量转移分类失败");
            }
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" (Type <>3 or Type is Null) ");//过滤小组主题分享的图片
            int PhotoCateId = Globals.SafeInt(this.PhotoCate.SelectedValue, 0);
            if (CateType == 0)//表示是未分类图片
            {
                PhotoCateId = -1;
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  (PhotoName like '%{0}%' or CreatedNickName like '%{0}%')", txtKeyword.Text.Trim());
            }
            if (this.chkRecomend.Checked)
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                if (this.chkRecomend2.Checked)
                {

                    strWhere.AppendFormat("  IsRecomend<>0 ");
                }
                else
                {
                    strWhere.AppendFormat("  IsRecomend=1 ");
                }
            }
            else if (chkRecomend2.Checked)
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  IsRecomend=2 ");
            }
            if (this.dropState.SelectedIndex > 0)
            {
                int state = Common.Globals.SafeInt(this.dropState.SelectedValue, 0);
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  Status =" + state);
            }
            if (!String.IsNullOrWhiteSpace(this.txtFrom.Value) && Common.PageValidate.IsDateTime(this.txtFrom.Value))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("   CreatedDate >'" + this.txtFrom.Value + "' ");
            }

            if (!String.IsNullOrWhiteSpace(this.txtTo.Value) && Common.PageValidate.IsDateTime(this.txtTo.Value))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  CreatedDate <'" + this.txtTo.Value + "' ");
            }
            ds = bll.GetListEx(strWhere.ToString(), PhotoCateId);
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


                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl btnupdate = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    btnupdate.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("linkDel");
                    linkbtnDel.Visible = false;
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
            int id = (int)gridView.DataKeys[e.RowIndex].Value;
            int result = 0;
            DataSet ds = bll.DeleteListEx(id.ToString(), out result,true,CurrentUser.UserID);
            if (result > 0)
            {
                if (ds != null)
                {
                    //物理删除文件
                    PhysicalFileInfo(ds.Tables[0]);
                }
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除图片(PhotoID=" + id + ")成功", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                return;
            }
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

        protected string GetCategoryName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                Maticsoft.BLL.SNS.Categories cateBll = new BLL.SNS.Categories();
                int SNSCateId = Common.Globals.SafeInt(target.ToString(), 0);
                Maticsoft.Model.SNS.Categories model = cateBll.GetModel(SNSCateId);
                if (model != null)
                {
                    str = model.Name;
                }
                else
                {
                    str = "";
                }
            }
            return str;
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
                        return "审核未通过";
                    case 3:
                        return "分类未明确";
                    case 4:
                        return "分类已明确";
                }
            }
            return str;
        }

        protected void radlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("listEx.aspx?type=" + CateType);
        }

        //批量推荐到首页
        protected void btnRecomend_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!bll.UpdateRecomendList(idlist, 1))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量推荐到首页失败");
            }
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + idlist + ")到首页成功", this);
            gridView.OnBind();
        }

        protected void btnRecomend2_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!bll.UpdateRecomendList(idlist, 2))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量推荐到频道首页失败");
            }
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + idlist + ")到首页成功", this);
            gridView.OnBind();
        }

        protected void btnNoRecomend_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!bll.UpdateRecomendList(idlist, 0))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量取消推荐图片(PhotoIDs=" + idlist + ")到首页成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量取消推荐失败");
            }
            gridView.OnBind();
        }

        #region 物理删除文件

        private void PhysicalFileInfo(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["TargetImageURL"] != null && dt.Rows[n]["TargetImageURL"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["TargetImageURL"].ToString());
                    }
                    if (dt.Rows[n]["ThumbImageUrl"] != null && dt.Rows[n]["ThumbImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ThumbImageUrl"].ToString());
                    }
                    if (dt.Rows[n]["NormalImageUrl"] != null && dt.Rows[n]["NormalImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["NormalImageUrl"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        private void DeletePhysicalFile(string path)
        {
            Maticsoft.Web.Components.FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, path);
        }

        #endregion
    }
}