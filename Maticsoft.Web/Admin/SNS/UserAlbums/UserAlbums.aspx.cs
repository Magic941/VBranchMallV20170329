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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.SNS.UserAlbums
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        private Maticsoft.BLL.SNS.UserAlbums bll = new Maticsoft.BLL.SNS.UserAlbums();
        protected override int Act_PageLoad { get { return 93; } } //社区管理_是否显示专辑管理页面
        protected new int Act_DelData = 94;    //社区管理_专辑管理_删除专辑信息

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    ltbnDelete.Visible = false;
                }

                BindToType();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        private void BindToType()
        {
            Maticsoft.BLL.SNS.AlbumType TypeBll = new BLL.SNS.AlbumType();
            ddlTypeList.DataSource = TypeBll.GetList("");
            ddlTypeList.DataTextField = "TypeName";
            ddlTypeList.DataValueField = "ID";
            ddlTypeList.DataBind();
            ddlTypeList.Items.Insert(0, new ListItem("选择分类", "-1"));
        }

        protected void ltbnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除专辑(id=" + idlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除专辑(id=" + idlist + ")失败", this);
            }
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

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[17].Visible = false;
            }
            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));

            System.Text.StringBuilder strWhere = new System.Text.StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text))
            {
                strWhere.AppendFormat(" AlbumName like '%{0}%'", this.txtKeyword.Text);
            }
            if ((Common.Globals.SafeInt(rdoisable.SelectedValue, -1) > -1))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("  Status={0}", this.rdoisable.SelectedValue);
            }
            if ((Common.Globals.SafeInt(rdorecommand.SelectedValue, -1) > -1))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("  IsRecommend={0}", this.rdorecommand.SelectedValue);
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

            if (Common.Globals.SafeInt(ddlTypeList.SelectedValue, -1) > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" AlbumID in (select AlbumsID from  SNS_UserAlbumsType where TypeID=" + ddlTypeList.SelectedValue + ")");
            }
            if (!string.IsNullOrWhiteSpace(strWhere.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    strWhere.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    strWhere.AppendFormat(" CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }

            gridView.DataSetSource = bll.GetUserAblumSearchList(strWhere.ToString());
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
            Maticsoft.BLL.SNS.UserAlbums bll = new BLL.SNS.UserAlbums();

            List<string> photoList = PhotoPhysicalInfo(ID);
            List<string> productsList = ProductsPhysicalInfo(ID);

            if (bll.DeleteAblumAction(ID))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除专辑(id=" + ID + ")成功", this);

                if (photoList != null && photoList.Count > 0)
                {
                    DeletePhysicalFile(photoList);
                }
                if (productsList != null && productsList.Count > 0)
                {
                    DeletePhysicalFile(productsList);
                }
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除专辑(id=" + ID + ")失败", this);

                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RecommandIndex")
            {
                if (e.CommandArgument != null)
                {
                    int ablumId = 0;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    ablumId = Common.Globals.SafeInt(Args[0], 0);
                    int IsRecommendIndex = Common.Globals.SafeInt(Args[1], 0);
                    if (IsRecommendIndex == (int)Model.SNS.EnumHelper.RecommendType.None || IsRecommendIndex == (int)Model.SNS.EnumHelper.RecommendType.Channel)
                    {

                        bll.UpdateRecommand(ablumId, Model.SNS.EnumHelper.RecommendType.Home);
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "推荐专辑(id=" + ablumId + ")到首页成功", this);
                        gridView.OnBind();
                    }
                    else
                    {
                        bll.UpdateRecommand(ablumId, Model.SNS.EnumHelper.RecommendType.None);

                        gridView.OnBind();
                    }
                }
            }
            if (e.CommandName == "RecommandAblum")
            {
                if (e.CommandArgument != null)
                {
                    int ablumId = 0;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    ablumId = Common.Globals.SafeInt(Args[0], 0);
                    int IsRecommendIndex = Common.Globals.SafeInt(Args[1], 0);
                    if (IsRecommendIndex == (int)Model.SNS.EnumHelper.RecommendType.None || IsRecommendIndex == (int)Model.SNS.EnumHelper.RecommendType.Home)
                    {
                        bll.UpdateRecommand(ablumId, Model.SNS.EnumHelper.RecommendType.Channel);
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "推荐专辑(id=" + ablumId + ")到频道页成功", this);
                        gridView.OnBind();
                    }
                    else
                    {
                        bll.UpdateRecommand(ablumId, Model.SNS.EnumHelper.RecommendType.None);
                        gridView.OnBind();
                    }
                }
            }
        }

        private List<string> PhotoPhysicalInfo(int ID)
        {
            BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
            List<Model.SNS.Photos> photoList = photoBll.UserUploadPhotoList(ID);
            if (photoList != null && photoList.Count > 0)
            {
                List<string> Pathlist = new List<string>();
                foreach (Model.SNS.Photos item in photoList)
                {
                    if (!string.IsNullOrWhiteSpace(item.PhotoUrl) &&
                        !item.PhotoUrl.StartsWith("http://"))
                    {
                        Pathlist.Add(Server.MapPath(item.PhotoUrl));
                        Pathlist.Add(Server.MapPath(item.ThumbImageUrl));
                        Pathlist.Add(Server.MapPath(item.NormalImageUrl));
                    }
                }
                if (Pathlist.Count > 0)
                {
                    //DeletePhysicalFile(Pathlist);
                    return Pathlist;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private List<string> ProductsPhysicalInfo(int ID)
        {
            BLL.SNS.Products productsBll = new BLL.SNS.Products();
            List<Model.SNS.Products> photoList = productsBll.UserUploadPhotoList(ID);
            if (photoList != null && photoList.Count > 0)
            {
                List<string> Pathlist = new List<string>();
                foreach (Model.SNS.Products item in photoList)
                {
                    if (RegURL(item.ProductUrl))
                    {
                        Pathlist.Add(Server.MapPath(item.ProductUrl));
                    }
                    if (RegURL(item.ThumbImageUrl))
                    {
                        Pathlist.Add(Server.MapPath(item.ThumbImageUrl));
                    }
                    if (RegURL(item.NormalImageUrl))
                    {
                        Pathlist.Add(Server.MapPath(item.NormalImageUrl));
                    }
                }
                if (Pathlist.Count > 0)
                {
                    //DeletePhysicalFile(Pathlist);
                    return Pathlist;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        private void DeletePhysicalFile(List<string> list)
        {
            foreach (string item in list)
            {
                Common.FileManage.DeleteFile(item);
            }
        }

        private bool RegURL(string path)
        {
            Regex regex = new Regex("^[a-zA-z]+://(//w+(-//w+)*)(//.(//w+(-//w+)*))*(//?//S*)?$");
            Match match = regex.Match(path);
            return match.Success;
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

        public string GetStatus(object target)
        {
            //状态 1:不可用 ：2可用
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

        public string GetCoverTargetType(object target)
        {
            //封面的类型 1:照片 ：2商品
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "照片";
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

        public string GetPrivacy(object target)
        {
            //隐私 0:公开 1:仅好友可见 2:仅自己可见。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "公开";
                        break;

                    case 1:
                        str = "仅好友可见";
                        break;

                    case 2:
                        str = "仅自己可见";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        protected void btnRecommand_Click(object sender, EventArgs e)
        {
            if (dropIsRecommand.SelectedValue == "-1") return;
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateIsRecommand(Globals.SafeInt(dropIsRecommand.SelectedValue, 0), idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐专辑(id=" + idlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            gridView.OnBind();
        }

        public string GetCategoryName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int albumId = Common.Globals.SafeInt(target.ToString(), 0);
                if (albumId > 0)
                {
                    Maticsoft.BLL.SNS.AlbumType typeBll = new BLL.SNS.AlbumType();
                    List<Maticsoft.Model.SNS.AlbumType> list = typeBll.GetTypeList(albumId);
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            str += item.TypeName + ",";
                        }
                        str = str.Substring(0, str.LastIndexOf(","));
                    }
                }
            }
            return str;
        }
    }
}