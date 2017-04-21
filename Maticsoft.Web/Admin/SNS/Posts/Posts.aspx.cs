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
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Text;
using System.IO;

namespace Maticsoft.Web.Admin.SNS.Posts
{
    public partial class List : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.Posts bll = new Maticsoft.BLL.SNS.Posts();
        private Maticsoft.BLL.SNS.UserBlog blogBll = new BLL.SNS.UserBlog();
        protected int type = -1;
        protected new int Act_DelData = 25;

        protected override int Act_PageLoad { get { return 91; } } //社区管理_是否显示全部动态页面

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ApproveList)) && GetPermidByActID(Act_ApproveList) != -1)
                {
                    btnChecked.Visible = false;
                    btnCheckedUnpass.Visible = false;
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
                gridView.Columns[7].Visible = false;
            }

            #endregion gridView
            StringBuilder strWhere = new StringBuilder();
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
            if (Type > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" Type=" + Type + " ");
            }
            if (txtKeyword.Text.Length > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" Description like '%" + txtKeyword.Text + "%' ");
            }
            if (strWhere.Length > 0)
            {
                strWhere.Append(" and ");
            }
            strWhere.Append(" 1=1 order by CreatedDate desc ");
            DataSet ds = bll.GetList(strWhere.ToString());

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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("linkbtnDel");
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.Equals("Delete"))
            {
                if (e.CommandName == null || e.CommandName.ToString() == "")
                {
                    return;
                }
                int postId = Globals.SafeInt(e.CommandName, -1);
                Maticsoft.Model.SNS.Posts PostModel = bll.GetModelByCache(postId);
                Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
                Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
                
                int result = 0;
                DataSet ds = new DataSet();
                switch (PostModel.Type)
                {
                    case 0:
                        //一般动态
                        if (bll.DeleteEx(postId, true, CurrentUser.UserID))
                        {
                            if (!String.IsNullOrWhiteSpace(PostModel.ImageUrl) &&
                     !PostModel.ImageUrl.StartsWith("http://"))
                            {
                                DeletePhysicalFile(PostModel.ImageUrl);
                            }
                        }
                        break;
                    case 1:
                        //删除单个图片类型动态
                        ds = photoBll.DeleteListEx(PostModel.TargetId.ToString(), out result, true, CurrentUser.UserID);
                        if (ds != null)
                        {
                            PhysicalFileInfo(ds.Tables[0]);
                        }
                        break;
                    case 2:
                        //删除单个商品类型动态
                        ds = productBll.DeleteListEx(PostModel.TargetId.ToString(), out result, true, CurrentUser.UserID);
                        if (ds != null)
                        {
                            PhysicalFileInfo(ds.Tables[0]);
                        }
                        break;
                        //删除Blog
                    case 4:
                        blogBll.DeleteEx(PostModel.TargetId);
                        break;
                    case 5:

                        break;
                    default:
                        //一般动态
                        if (bll.DeleteEx(postId, true, CurrentUser.UserID))
                        {
                            if (!String.IsNullOrWhiteSpace(PostModel.ImageUrl) &&
                     !PostModel.ImageUrl.StartsWith("http://"))
                            {
                                DeletePhysicalFile(PostModel.ImageUrl);
                            }
                        }
                        break;

                }
                gridView.OnBind();
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除动态(PostID=" + postId + ")成功", this);
                MessageBox.ShowSuccessTip(this, "删除成功！");
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

        private string GetSelIDTypelist(string type)
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Values.Count > 1)
                    {
                        if ( type == "0")
                        {
                            idlist += gridView.DataKeys[i].Values[0] + ",";
                        }
                        else if (gridView.DataKeys[i].Values[1].ToString() == type)
                        {
                            idlist += gridView.DataKeys[i].Values[2] + ",";
                        }
                    }
                }
            }
            if (BxsChkd)
            {
                if (idlist.Length > 0)
                {
                    idlist = idlist.Substring(0, idlist.LastIndexOf(","));
                }
            }
            return idlist;
        }

        #endregion

        #region 公共方法，列信息解析
        /// <summary>
        /// 内容显示
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="TargetId"></param>
        /// <param name="Description"></param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        public string GetContent(object Type, object TargetId, object Description, object ImageUrl)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(Type))
            {
                string BasePath = MvcApplication.GetCurrentRoutePath(AreaRoute.SNS);
                switch (Globals.SafeInt(Type.ToString(), -1))
                {

                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal:
                        str = Description != null ? Description.ToString() : "";
                        break;

                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo:
                        {
                            if (!StringPlus.IsNullOrEmpty(ImageUrl))
                            {

                                str = " <a  target='_blank' href='" + BasePath + "Photo/Detail/" + TargetId.ToString() + "'><img src=\"" + Maticsoft.Web.Components.FileHelper.GeThumbImage(ImageUrl.ToString(), "T80x80_") + "\" style=\"height:64px;width:64px;border-width:0px;\"><a>";
                            }
                            else
                            {
                                str = Description != null ? Description.ToString() : "";
                            }
                        }
                        break;
                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product:
                        {
                            if (!StringPlus.IsNullOrEmpty(ImageUrl))
                            {
                                str = " <a  target='_blank' href='" + BasePath + "Product/Detail/" + TargetId.ToString() + "'><img src=\"" + Maticsoft.Web.Components.FileHelper.GeThumbImage(ImageUrl.ToString(), "T80x80_") + "\" style=\"height:64px;width:64px;border-width:0px;\"><a>";
                            }
                            else
                            {
                                str = Description != null ? Description.ToString() : "";
                            }
                        }
                        break;
                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Video:
                        str = Description != null ? Description.ToString() : "";
                        break;
                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Blog:
                        string blogUrl = BasePath + "Blog/BlogDetail?id=" + TargetId;
                        string desc = Description.ToString();

                        str = !String.IsNullOrWhiteSpace(desc) ? desc.Replace("{BlogUrl}", blogUrl) : "";
                        break;
                    default:
                        str = Description != null ? Description.ToString() : "";
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetTypeName(object Type, object TargetId)
        {
            //动态的类型(动态0：一般类型;1:图片(搭配和晒货)类型;2：商品类型。)
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(Type))
            {
                switch (Globals.SafeInt(Type.ToString(), -1))
                {
                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal:
                        str = "文字 ";
                        break;

                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo:
                        str = " <a  target='_blank' href='" + Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.SNS) + "Photo/Detail/" + TargetId.ToString() + "'>图片<a>";
                        break;

                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product:
                        str = " <a  target='_blank' href='" + Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.SNS) + "Product/Detail/" + TargetId.ToString() + "'>商品 <a>";
                        break;
                    case (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Video:
                        str = "视频 ";
                        break;
                    default:
                        str = "文字 ";
                        break;
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
                        return "未通过";
                }
            }
            return str;
        }

        /// <summary>
        ///帖子的id
        /// </summary>
        public int Type
        {
            get
            {
                string strid = dropType.SelectedValue;
                type = Globals.SafeInt(strid, 0);

                return type;
            }
        }
        #endregion

        #region 按钮操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();

            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int result1 = 0, result2 = 0;
            ///删除图片
            string PhotoListIds = GetSelIDTypelist("1");
            if (!string.IsNullOrEmpty(PhotoListIds) && PhotoListIds.Length > 0)
            {
                DataSet ds = photoBll.DeleteListEx(PhotoListIds, out result1, true, CurrentUser.UserID);
                if (ds != null)
                {
                    PhysicalFileInfo(ds.Tables[0]);
                }
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除图片动态(ProductID=" + PhotoListIds + ")成功", this);
            }
            ///删除商品
            string ProductListIds = GetSelIDTypelist("2");
            if (!string.IsNullOrEmpty(ProductListIds) && ProductListIds.Length > 0)
            {
                DataSet ds = productBll.DeleteListEx(ProductListIds, out result2, true, CurrentUser.UserID);
                if (ds != null)
                {
                    PhysicalFileInfo(ds.Tables[0]);
                }
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除商品动态(ProductID=" + ProductListIds + ")成功", this);
            }
            //用户微博

            string blogIds = GetSelIDTypelist("4");
            if (!string.IsNullOrEmpty(blogIds) && blogIds.Length > 0)
            {
                blogBll.DeleteListEx(blogIds);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除用户微博动态(" + blogIds + ")成功", this);
            }

            ///删除动态文字
            string PostIDs = GetSelIDTypelist("0");
            bool IsDelete = false;
            if (PostIDs.Length > 0)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除文字动态(PostID=" + PostIDs + ")成功", this);
                IsDelete = bll.DeleteListByNormalPost(PostIDs, true, CurrentUser.UserID);
            }

            //DataSet ds1 = productBll.DeleteListEx(GetSelIDTypelist("2"), out result2); 
            //bool  bll.DeleteListByNormalPost(GetSelIDTypelist("0"));
            if (result1 == 1 || result2 == 1 || IsDelete)
            {
                MessageBox.ShowSuccessTip(this, "删除成功！");
            }
            else
            {
                MessageBox.ShowSuccessTip(this, "删除失败！");
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

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateStatusList(idlist, (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新动态(PostIDs=" + idlist + ")为审核状态成功", this);
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新动态(PostIDs=" + idlist + ")为审核状态失败", this);
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
            if (bll.UpdateStatusList(idlist, (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.CheckedUnPass))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新动态(PostIDs=" + idlist + ")为未审核状态成功", this);
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新动态(PostIDs=" + idlist + ")为未审核状态失败", this);
                MessageBox.ShowFailTip(this, "操作失败！");
            }
            gridView.OnBind();
        }

        #endregion


    }
}