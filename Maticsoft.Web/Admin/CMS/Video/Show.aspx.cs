/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:45:15  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.IO;
using Maticsoft.Common;
using Maticsoft.Common.Video;

namespace Maticsoft.Web.CMS.Video
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 272; } } 
        public string localVideoUrl = "";
        public string onlineVideoUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        public int VideoID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
            Maticsoft.Model.CMS.Video model = bll.GetModelEx(VideoID);
            if (null != model)
            {
                this.lblTitle.Text = model.Title;
                this.lblDescription.Text = model.Description;
                if (model.AlbumID.HasValue)
                {
                    this.lblAlbumID.Text = model.AlbumID.ToString();
                }

                this.lblCreatedUserID.Text = model.CreatedUserName;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                if (model.LastUpdateUserID.HasValue)
                {
                    this.lblLastUpdateUserID.Text = model.LastUpdateUserName;
                }
                if (model.LastUpdateDate.HasValue)
                {
                    this.lblLastUpdateDate.Text = model.LastUpdateDate.ToString();
                }

                this.lblSequence.Text = model.Sequence.ToString();

                if (model.VideoClassID.HasValue)
                {
                    this.lblVideoClassID.Text = model.VideoClassID.ToString();
                }

                this.lblIsRecomend.Text = GetboolText(model.IsRecomend);

                this.imgImageUrl.ImageUrl = MvcApplication.UploadFolder + model.ImageUrl;
                this.imgThumbImageUrl.ImageUrl = MvcApplication.UploadFolder + model.ThumbImageUrl;
                this.imgNormalImageUrl.ImageUrl = MvcApplication.UploadFolder + model.NormalImageUrl;

                if (model.TotalTime.HasValue)
                {
                    this.lblTotalTime.Text = TimeParser.SecondToDateTime(model.TotalTime.Value);
                }

                this.lblTotalComment.Text = model.TotalComment.ToString();

                this.lblTotalFav.Text = model.TotalFav.ToString();

                this.lblTotalUp.Text = model.TotalUp.ToString();

                this.lblReference.Text = model.Reference.ToString();

                this.lblPvCount.Text = model.PvCount.ToString();

                this.lblTags.Text = model.Tags;

                string videoUrl = model.VideoUrl;
                this.lblVideoUrl.Text = videoUrl;

                int urlType = model.UrlType;
                if (!string.IsNullOrWhiteSpace(videoUrl))
                {
                    if (urlType == 0)//本地视频
                    {
                        trLocalVideo.Visible = true;
                        trVideoFormat.Visible = true;
                        localVideoUrl = videoUrl;
                    }
                    if (urlType == 1)//网络视频
                    {
                        trDomain.Visible = true;
                        string flashUrl = GetFlashUrl(model.VideoUrl);
                        if (!string.IsNullOrWhiteSpace(flashUrl))
                        {
                            trOnlineVideo.Visible = true;
                            onlineVideoUrl = flashUrl;
                        }
                    }
                }
                this.lblUrlType.Text = GetUrlType(urlType);
                this.lblVideoFormat.Text = model.VideoFormat;
                this.lblDomain.Text = model.Domain;
                this.lblGrade.Text = model.Grade.ToString();

                string attachment=model.Attachment;
                if (!string.IsNullOrWhiteSpace(attachment))
                {
                    this.lnkAttachment.NavigateUrl = MvcApplication.UploadFolder + attachment;
                }

                this.lblPrivacy.Text = GetVideoPrivacy(model.Privacy);

                this.lblState.Text = GetVideoState(model.State);

                this.lblRemark.Text = model.Remark;
            }
        }

        

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Modify.aspx?id=" + VideoID);
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        #region 获取视频类型
        /// <summary>
        /// 获取视频类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetUrlType(object target)
        {
            //0.本地视频；1.网络视频。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = Resources.CMSVideo.LocalVideo;
                        break;
                    case "1":
                        str = Resources.CMSVideo.OnlineVideo;
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取视频状态
        /// <summary>
        /// 获取视频状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoState(object target)
        {
            //视频状态:0.转码中 1.转码失败 2.待审核 3.已审核未发布 4.被屏蔽 5.已发布。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = Resources.CMSVideo.TurnCode;
                        break;
                    case "1":
                        str = Resources.CMSVideo.TranscodingFail;
                        break;
                    case "2":
                        str = Resources.CMSVideo.PendingReview;
                        break;
                    case "3":
                        str = Resources.CMSVideo.NotYetReleased;
                        break;
                    case "4":
                        str = Resources.CMSVideo.Screen;
                        break;
                    case "5":
                        str = Resources.CMSVideo.Publish;
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取视频隐私
        /// <summary>
        /// 获取视频隐私
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoPrivacy(object target)
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

        #region 获取网络视频FlashUrl

        /// <summary>
        /// 获取网络视频FlashUrl
        /// </summary>
        /// <returns></returns>
        public string GetFlashUrl(string videoUrl)
        {
            string YouKuAPI = BLL.SysManage.ConfigSystem.GetValueByCache("YouKuAPI");
            string flashUrl = "";
            if (VideoHelper.IsYouKuVideoUrl(videoUrl))
            {
                YouKuInfo info = VideoHelper.GetYouKuInfo(videoUrl);
                if (null != info)
                {
                    flashUrl = string.Format(YouKuAPI, info.VidEncoded);
                }
            }
            if (VideoHelper.IsKu6VideoUrl(videoUrl))
            {
                Ku6Info info = VideoHelper.GetKu6Info(videoUrl);
                if (null != info)
                {
                    flashUrl = info.flash;
                }
            }
            return flashUrl;
        }
        #endregion

    }
}
