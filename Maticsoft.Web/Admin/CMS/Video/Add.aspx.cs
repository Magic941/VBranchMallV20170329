/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:39:11  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Common.Video;

namespace Maticsoft.Web.CMS.Video
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 270; } } 
        string ffmpegTools = BLL.SysManage.ConfigSystem.GetValueByCache("FFmpeg");
        string normalImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageWidth");
        string normalImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageHeight");
        string thumbImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageWidth");
        string thumbImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageHeight");
        private const string SavePath = "/Upload/CMS/Videos/{0}/";
        private const string TempPath = "/Upload/Temp/{0}/";

        Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                LoadVideoAlbumData();
                LoadVideoClassData();
                this.txtSequence.Text = bll.GetMaxSequence().ToString();
            }
        }

        protected void LoadVideoAlbumData()
        {
            Maticsoft.BLL.CMS.VideoAlbum bll = new Maticsoft.BLL.CMS.VideoAlbum();
            DataSet ds = bll.GetAllList();
            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropAlbumID.DataSource = ds;
                this.dropAlbumID.DataTextField = "AlbumName";
                this.dropAlbumID.DataValueField = "AlbumID";
                this.dropAlbumID.DataBind();
            }
            this.dropAlbumID.Items.Insert(0, new ListItem(Resources.Site.PleaseSelect, "0"));
            this.dropAlbumID.SelectedValue = "0";
        }

        protected void LoadVideoClassData()
        {
            Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
            DataSet ds = bll.GetAllList();
            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.radlVideoClassID.DataSource = ds;
                this.radlVideoClassID.DataTextField = "VideoClassName";
                this.radlVideoClassID.DataValueField = "VideoClassID";
                this.radlVideoClassID.DataBind();
            }
            this.radlVideoClassID.Items.Insert(0, new ListItem(Resources.CMSVideo.NoCategory, "0"));
            this.radlVideoClassID.SelectedValue = "0";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            int uid = CurrentUser.UserID;
            DateTime dt = DateTime.Now;
            string title = this.txtTitle.Text.Trim();
            string des = this.txtDescription.Text;
            string privacy = this.radlPrivacy.SelectedValue;
            string sequence = this.txtSequence.Text;
            string videoUrl = this.hfLocalVideo.Value;
            string url = this.txtOnlineVideo.Text;
            int type = GetType(url);

            if (!this.radOnlineVideo.Checked && !this.radLocalVideo.Checked)
            {
                MessageBox.ShowFailTip(this, Resources.CMSVideo.TooltipSwitchType);
                return;
            }
            if (title.Length == 0)
            {
                MessageBox.ShowFailTip(this, Resources.CMSVideo.ErrorTitleNull);
                return;
            }
            if (radLocalVideo.Checked)
            {
                if (videoUrl.Length == 0)
                {
                    MessageBox.ShowFailTip(this, Resources.CMSVideo.ErrorNoVideo);
                    return;
                }
            }
            if (radOnlineVideo.Checked)
            {
                if (!IsUrl(url) || type == 0)
                {
                    //strErr += "网络视频地址格式不正确";
                    return;
                }
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

            string savePath = String.Format(SavePath, DateTime.Now.ToString("yyyyMMdd"));
            string tempPath= String.Format(TempPath, DateTime.Now.ToString("yyyyMMdd"));
            Maticsoft.Model.CMS.Video model = new Maticsoft.Model.CMS.Video();
            model.Title = title;
            model.Description = des;
            model.AlbumID = Globals.SafeInt(this.dropAlbumID.SelectedValue, 0);
            model.CreatedUserID = uid;
            model.CreatedDate = dt;
            model.LastUpdateUserID = uid;
            model.LastUpdateDate = dt;
            model.Sequence = Globals.SafeInt(sequence, 1);
            model.VideoClassID = Globals.SafeInt(this.radlVideoClassID.SelectedValue, 0);
            model.IsRecomend = false;
            model.TotalTime = null;
            model.TotalComment = 0;
            model.TotalFav = 0;
            model.TotalUp = 0;
            model.Reference = 0;
            model.Tags = this.txtTags.Text;
            string address = "";
            //0.本地视频；1：网络视频。
            if (radLocalVideo.Checked)
            {
                model.UrlType = 0;

                model.VideoUrl = videoUrl.Replace(tempPath,savePath);

                model.VideoFormat = Path.GetExtension(videoUrl);

                string imgurl = videoUrl + ".jpg";
                if (File.Exists(HttpContext.Current.Server.MapPath( imgurl)))
                {
                    address = HttpContext.Current.Server.MapPath(imgurl);
                    model.ImageUrl = imgurl.Replace(tempPath,savePath);
                }
                if (File.Exists(HttpContext.Current.Server.MapPath(videoUrl)))
                {
                    model.TotalTime = GetVideoTotalTime(HttpContext.Current.Server.MapPath(videoUrl));
                }
            }

            if (radOnlineVideo.Checked)
            {
                string domain = "";
                string subDomain = "";
                UrlOper.GetDomain(url, out domain, out subDomain);
                if (!string.IsNullOrWhiteSpace(domain))
                {
                    model.Domain = domain;
                }
                if (type == 1)
                {
                    YouKuInfo info = VideoHelper.GetYouKuInfo(url);
                    if (null != info)
                    {
                        model.VideoUrl = url; //string.Format("http://player.youku.com/player.php/sid/{0}/v.swf", info.VidEncoded);
                        address = info.Logo;
                        if (!string.IsNullOrWhiteSpace(model.Title))
                        {
                            if (string.IsNullOrWhiteSpace(info.Title))
                            {
                                model.Title = info.Title;
                            }
                        }
                        model.UrlType = 1;
                    }
                }
                if (type == 2)
                {
                    Ku6Info info = VideoHelper.GetKu6Info(url);
                    if (null != info)
                    {
                        model.VideoUrl = url;// info.flash;//播放地址
                        address = info.coverurl;
                        if (string.IsNullOrWhiteSpace(model.Title))
                        {
                            if (!string.IsNullOrWhiteSpace(info.title))
                            {
                                model.Title = info.title;
                            }
                        }
                        model.UrlType = 1;
                    }
                }
            }

            string imageUrl = "";
            string thumbImage = "";
            string normalImage = "";
            Thumbnail(model.UrlType, address, true, true, out imageUrl, out thumbImage, out normalImage);
            if (model.UrlType == 1)
            {
                if (!string.IsNullOrWhiteSpace(imageUrl) && File.Exists(HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + imageUrl)))
                {
                    model.ImageUrl = imageUrl;
                }
            }
            if (!string.IsNullOrWhiteSpace(thumbImage) && File.Exists(HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + thumbImage)))
            {
                model.ThumbImageUrl = thumbImage;
            }
            if (!string.IsNullOrWhiteSpace(normalImage) && File.Exists(HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + normalImage)))
            {
                model.NormalImageUrl = normalImage;
            }

            model.Grade = 0;
            model.Attachment = null;
            model.IsRecomend = this.chkIsRecomend.Checked;
            //隐私:0.对所有人公开 1.仅自己可见 2.仅好友可见。
            model.Privacy = Globals.SafeInt(privacy, 0);
            //视频状态:0.转码中 1.转码失败 2.待审核 3.审核未发布 4.被屏蔽 5.发布。
            model.State = 5;
            model.Remark = "";
            model.PvCount = 0;
            if (bll.Add(model) > 0)
            {
                if (chkAddContinue.Checked)
                {
                    this.radLocalVideo.Checked = false;
                    this.radOnlineVideo.Checked = false;
                    this.txtTitle.Text = "";
                    this.txtTags.Text = "";
                    this.radlPrivacy.SelectedValue = "0";
                    LoadVideoAlbumData();
                    LoadVideoClassData();
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                }
                else
                {
                    Common.MessageBox.ShowLoadingTip(this, "新增成功，正在跳转...", "list.aspx");
                }
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
            }
        }

        /// <summary>
        /// 获得视频总时长
        /// </summary>
        private int GetVideoTotalTime(string videoPath)
        {
            ConvertVideo cv = new ConvertVideo();
            TimeSpan ts = cv.GetVideoTotalTime(videoPath);
            int i = TimeParser.TimeToSecond(ts.Hours, ts.Minutes, ts.Seconds);
            return i;
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        protected void Thumbnail(int urlType, string address, bool IsThumbImage, bool IsNormalImage, out string imageUrl, out string thumbImage, out string normalImage)
        {
            string ServerMapPath = Server.MapPath(MvcApplication.UploadFolder);
            if (!Directory.Exists(ServerMapPath))
            {
                Directory.CreateDirectory(ServerMapPath);
            }
            string fileName = "";
            if (urlType == 0)//网络视频
            {
                fileName = address;
            }
            imageUrl = Guid.NewGuid() + ".jpg";
            if (urlType == 1)//网络视频
            {
                fileName = HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + imageUrl);
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadFile(address, fileName);
            }
            thumbImage = "T_" + imageUrl;
            normalImage = "N_" + imageUrl;
            if (File.Exists(fileName))
            {
                if (IsThumbImage)
                {
                    ImageTools.MakeThumbnail(fileName, HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + thumbImage), Globals.SafeInt(thumbImageWidth, 120), Globals.SafeInt(thumbImageHeight, 90), MakeThumbnailMode.Auto);
                }
                if (IsNormalImage)
                {
                    ImageTools.MakeThumbnail(fileName, HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + normalImage), Globals.SafeInt(normalImageHeight, 240), Globals.SafeInt(normalImageHeight, 180), MakeThumbnailMode.Auto);
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            Common.MessageBox.ShowLoadingTip(this, "正在跳转...", "list.aspx");
        }

        Regex regUrl = new Regex(@"(http:\/\/([\w.]+\/?)\S*)");

        /// <summary>
        /// 是否是链接
        /// </summary>
        /// <returns></returns>
        public bool IsUrl(string content)
        {
            Match m = regUrl.Match(content);
            return m.Success;
        }

        /// <summary>
        /// 获取视频类型
        /// </summary>
        public int GetType(string url)
        {
            int type = 0;
            if (VideoHelper.IsYouKuVideoUrl(url))
            {
                type = 1;//优库视频地址
            }
            if (VideoHelper.IsKu6VideoUrl(url))
            {
                type = 2;//酷6视频
            }
            return type;
        }
    }
}
