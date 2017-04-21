/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:31:30  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;

namespace Maticsoft.Web.CMS.VideoAlbum
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 258; } } //CMS_视频专辑管理_详细页    
        public string strid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        public int AlbumID
        {
            get
            {
                int id = 0;
                strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.BLL.CMS.VideoAlbum bll = new Maticsoft.BLL.CMS.VideoAlbum();
            Maticsoft.Model.CMS.VideoAlbum model = bll.GetModelEx(AlbumID);
            if (null != model)
            {
                this.lblAlbumName.Text = model.AlbumName;
                this.imgCoverVideo.ImageUrl = BLL.SysManage.ConfigSystem.GetValueByCache("UploadFolder") + model.CoverVideo;
                this.lblDescription.Text = model.Description;
                this.lblCreatedUserName.Text = model.CreatedUserName;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                if (model.LastUpdateUserID.HasValue)
                {
                    this.lblLastUpdateUserName.Text = model.LastUpdateUserName;
                }
                if (model.LastUpdatedDate.HasValue)
                {
                    this.lblLastUpdatedDate.Text = model.LastUpdatedDate.ToString();
                }
                this.lblState.Text = GetVideoAlbumState(model.State);
                this.lblSequence.Text = model.Sequence.ToString();
                this.lblPrivacy.Text = GetVideoAlbumPrivacy(model.Privacy);
                this.lblPvCount.Text = model.PvCount.ToString();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Modify.aspx?id=" + AlbumID);
        }

        #region 获取专辑状态
        /// <summary>
        /// 获取专辑状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoAlbumState(object target)
        {
            //0未审核; 1待审核; 2正常。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "未审核";
                        break;
                    case "1":
                        str = "待审核";
                        break;
                    case "2":
                        str = "正常";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取专辑权限
        /// <summary>
        /// 获取专辑权限
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoAlbumPrivacy(object target)
        {
            //0.对所有人公开；1：仅自己可见；2：仅好友观看。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = Resources.CMSVideo.Open;
                        break;
                    case "1":
                        str = Resources.CMSVideo.Private;
                        break;
                    case "2":
                        str = Resources.CMSVideo.SemiOpen;
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

    }
}
