/**
* Add.cs
*
* 功 能： 新增图片
* 类 名： Add
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 21:48:37  伍伟    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Controls;
using EnumHelper = Maticsoft.Model.Ms.EnumHelper;

namespace Maticsoft.Web.Admin.CMS.Photo
{
    public partial class AddPhotoInfo : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 237; } } //CMS_图片管理_编辑页
        protected string strNormalImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageWidth");
        protected string strNormalImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageHeight");
        protected string strThumbImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageWidth");
        protected string strThumbImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageHeight");

        private readonly BLL.CMS.PhotoAlbum albumBll = new BLL.CMS.PhotoAlbum();
        private readonly BLL.CMS.Photo photoBll = new BLL.CMS.Photo();
        private static int iAlbumId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(this.IdList))
                {
                    DataSet ds = photoBll.GetList("photoid in(" + this.IdList + ")");
                    this.RepeaterPhoto.DataSource = ds;
                    this.RepeaterPhoto.DataBind();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        iAlbumId = int.Parse(ds.Tables[0].Rows[0]["AlbumId"].ToString());
                    }
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.CMSPhoto.TooltipAddFail, "List.aspx");
                }
            }
        }

        public string IdList
        {
            get
            {
                string str = string.Empty;
                if (!string.IsNullOrWhiteSpace(Request.Params["idlist"]))
                {
                    str = Request.Params["idlist"].TrimEnd(',');
                }
                return str;
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(IdList))
            {
                DataSet ds;
                photoBll.DeleteList(IdList.TrimEnd(','), out ds);
                if (ds != null && ds.Tables[0].Rows.Count > 0) PhysicalFileInfo(ds.Tables[0]);
            }
            Response.Redirect("list.aspx");
        }

        #region 物理删除文件

        private void PhysicalFileInfo(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ImageUrl"].ToString());
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
            try
            {
                Maticsoft.Web.Components.FileHelper.DeleteFile(EnumHelper.AreaType.CMS, path);
            }
            catch (Exception)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除文件:{0}失败！", path), this);
            }
        }

        #endregion 物理删除文件

        protected void RepeaterPhoto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                PhotoClassDropList ddlClass = (PhotoClassDropList)e.Item.FindControl("ddlPhotoClass");
                ddlClass.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RepeaterPhoto.Items.Count; i++)
            {
                TextBox txtPhoneName = RepeaterPhoto.Items[i].FindControl("txtPhotoName") as TextBox;
                PhotoClassDropList ddlClass = RepeaterPhoto.Items[i].FindControl("ddlPhotoClass") as PhotoClassDropList;
                CheckBox ckRecomend = RepeaterPhoto.Items[i].FindControl("chkIsRecomend") as CheckBox;
                TextBox txtTags = RepeaterPhoto.Items[i].FindControl("txtTags") as TextBox;
                TextBox txtDescription = RepeaterPhoto.Items[i].FindControl("txtDescription") as TextBox;

                int iPhotoId = 0;
                if (!string.IsNullOrWhiteSpace(this.IdList))
                {
                    iPhotoId = int.Parse(IdList.Split(',')[i]);
                }
                Model.CMS.Photo photo = photoBll.GetModel(iPhotoId);
                if (photo == null) continue;
                if (txtPhoneName != null && !string.IsNullOrWhiteSpace(txtPhoneName.Text)) photo.PhotoName = txtPhoneName.Text;
                if (!string.IsNullOrWhiteSpace(ddlClass.SelectedValue)) photo.ClassID = int.Parse(ddlClass.SelectedValue);
                if (ckRecomend != null) photo.IsRecomend = ckRecomend.Checked;
                if (txtTags != null) photo.Tags = txtTags.Text;
                if (txtDescription != null) photo.Description = txtDescription.Text;
                photoBll.Update(photo);
                iAlbumId = photo.AlbumID;
            }
            MessageBox.ShowSuccessTip(this, Resources.CMSPhoto.TooltipEditedSuccess);
            Response.Redirect("list.aspx?AlbumID=" + iAlbumId);
        }

        public string GetPhotoCover(object objCoverPhoto, object objPhoto)
        {
            string str = Resources.CMSPhoto.lblSetToCover;
            if (objPhoto != null && objCoverPhoto != null)
            {
                string strPhoto = objPhoto.ToString();
                string strCoverPhoto = objCoverPhoto.ToString();
                if (strPhoto == strCoverPhoto)
                {
                    str = Resources.CMSPhoto.lblFrontCover;
                }
            }
            return str;
        }
    }
}