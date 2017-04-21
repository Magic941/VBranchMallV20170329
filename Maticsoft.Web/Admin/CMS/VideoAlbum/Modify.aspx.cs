/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:28:55  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web;
using Maticsoft.Common;
namespace Maticsoft.Web.CMS.VideoAlbum
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 257; } } //CMS_视频专辑管理_编辑页    
        Maticsoft.BLL.CMS.VideoAlbum bll = new Maticsoft.BLL.CMS.VideoAlbum();

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
            Maticsoft.Model.CMS.VideoAlbum model = bll.GetModel(AlbumID);

            if (null != model)
            {
                this.txtAlbumName.Text = model.AlbumName;
                this.hfCoverVideo.Value = model.CoverVideo;
                this.imgCoverVideo.ImageUrl = BLL.SysManage.ConfigSystem.GetValueByCache("UploadFolder") + model.CoverVideo;
                this.txtDescription.Text = model.Description;
                this.radlState.SelectedValue = model.State.ToString();
                this.txtSequence.Text = model.Sequence.ToString();
                if (model.Privacy.HasValue)
                {
                    this.radlPrivacy.SelectedValue = model.Privacy.ToString();
                }
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string albumname = this.txtAlbumName.Text.Trim();

            string covervideo = this.hfNewCoverVideo.Value.Trim();

            if (string.IsNullOrWhiteSpace(covervideo))
            {
                covervideo = this.hfCoverVideo.Value.Trim();
            }
            else
            {
                string thumbImage = "T_" + covervideo;
                string thumbImagePath = HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + thumbImage);
                if (System.IO.File.Exists(thumbImagePath))
                {
                    ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + covervideo), thumbImagePath, 120, 120, MakeThumbnailMode.Auto);
                    covervideo = thumbImage;
                }
            }

            string description = this.txtDescription.Text.Trim();
            string state = this.radlState.SelectedValue;
            string sequence = this.txtSequence.Text.Trim();
            string privacy = this.radlPrivacy.SelectedValue;

            if (string.IsNullOrWhiteSpace(albumname))
            {
                MessageBox.ShowFailTip(this, Resources.CMSVideo.ErrorVideoNameNull);
                return;
            }

            if (!PageValidate.IsNumber(state))
            {
                MessageBox.ShowFailTip(this, Resources.CMSVideo.ErrorStartFormat);
                return;
            }
            if (sequence.Length > 0)
            {
                if (!PageValidate.IsNumber(sequence))
                {
                    MessageBox.ShowFailTip(this, Resources.CMSVideo.ErrorOrderFormat);
                    return;
                }
            }
            if (!PageValidate.IsNumber(privacy))
            {
                MessageBox.ShowFailTip(this, Resources.CMSVideo.ErrorPrivacyFormed);
                return;
            }

            Maticsoft.Model.CMS.VideoAlbum model = bll.GetModel(AlbumID);
            if (null == model)
            {
                return;
            }
            model.AlbumName = albumname;
            model.CoverVideo = covervideo;
            model.Description = description;
            model.LastUpdateUserID = CurrentUser.UserID;
            model.LastUpdatedDate = DateTime.Now;
            model.State = Globals.SafeInt(state, 2);//0未审核; 1待审核; 2正常。
            model.Sequence = Globals.SafeInt(sequence, 1);
            model.Privacy = Globals.SafeInt(privacy, 0); //0.对所有人公开；1：仅自己可见；2：仅好友观看。

            if (bll.Update(model))
            {
                MessageBox.ResponseScript(this, "parent.location.href='list.aspx'");
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
        }
    }
}
