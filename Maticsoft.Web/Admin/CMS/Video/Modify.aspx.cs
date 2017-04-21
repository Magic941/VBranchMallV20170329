/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:42:46  孙鹏    提示信息修改、using引用移除
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
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Common.Video;

namespace Maticsoft.Web.CMS.Video
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 271; } } 
        string normalImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageWidth");
        string normalImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageHeight");
        string thumbImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageWidth");
        string thumbImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageHeight");
        public string strStyle = "display:none";

        Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                BindVideoAlbumData();

                BindVideoClassData();

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

        protected void BindVideoAlbumData()
        {
            Maticsoft.BLL.CMS.VideoAlbum bllVideoAlbum = new Maticsoft.BLL.CMS.VideoAlbum();

            DataSet ds = bllVideoAlbum.GetAllList();

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropAlbumID.DataSource = ds;
                this.dropAlbumID.DataTextField = "AlbumName";
                this.dropAlbumID.DataValueField = "AlbumID";
                this.dropAlbumID.DataBind();
                
            }
            this.dropAlbumID.Items.Insert(0, new ListItem(Resources.Site.PleaseSelect, "0"));
        }

        protected void BindVideoClassData()
        {
            Maticsoft.BLL.CMS.VideoClass bllVideoClass = new Maticsoft.BLL.CMS.VideoClass();

            DataSet ds = bllVideoClass.GetAllList();

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.radlVideoClassID.DataSource = ds;
                this.radlVideoClassID.DataTextField = "VideoClassName";
                this.radlVideoClassID.DataValueField = "VideoClassID";
                this.radlVideoClassID.DataBind();
                
            }
            this.radlVideoClassID.Items.Insert(0, new ListItem(Resources.CMSVideo.NoCategory, "0"));
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Video model = bll.GetModel(VideoID);
            if (null != model)
            {
                int urlType = model.UrlType;
                this.hfUrlType.Value = urlType.ToString();
                switch (urlType)
                {
                    case 0:
                        this.hfLocalVideo.Value = model.VideoUrl;
                        this.trLocalVideo.Visible = true;
                        break;
                    case 1:
                        this.txtOnlineVideo.Text = model.VideoUrl;
                        this.trOnlineVideo.Visible = true;
                        break;
                    default:
                        break;
                }

                this.txtTitle.Text = model.Title;
                this.txtDescription.Text = model.Description;

                if (model.AlbumID.HasValue)
                {
                    this.dropAlbumID.SelectedValue = model.AlbumID.ToString();
                }
                this.txtCreatedUserID.Text = model.CreatedUserID.ToString();
                this.txtCreatedDate.Text = model.CreatedDate.ToString();

                this.txtSequence.Text = model.Sequence.ToString();
                if (model.VideoClassID.HasValue)
                {
                    this.radlVideoClassID.SelectedValue = model.VideoClassID.ToString();
                }
                this.chkIsRecomend.Checked = model.IsRecomend;
                this.txtImageUrl.Text = model.ImageUrl;
                this.txtThumbImageUrl.Text = model.ThumbImageUrl;
                this.txtNormalImageUrl.Text = model.NormalImageUrl;
            
                this.txtTags.Text = model.Tags;

                this.txtVideoFormat.Text = model.VideoFormat;
                this.txtDomain.Text = model.Domain;
                string attachment = model.Attachment;
                if (!string.IsNullOrWhiteSpace(attachment))
                {
                    strStyle = "";
                    this.hfAttachment.Value = attachment;
                    this.lnkAttachment.NavigateUrl = MvcApplication.UploadFolder + attachment;
                }
                this.radlPrivacy.SelectedValue = model.Privacy.ToString();
                this.radlState.Text = model.State.ToString();
                this.txtRemark.Text = model.Remark;
                if (model.TotalTime.HasValue)
                {
                    TimeSpan ts = new TimeSpan(0, 0, model.TotalTime.Value);
                    if (ts.TotalHours < 10)
                    {
                        this.txtTotalHours.Text = "0" + (int)ts.TotalHours;
                    }
                    else
                    {
                        this.txtTotalHours.Text = ((int)ts.TotalHours).ToString();
                    }
                    if (ts.Minutes < 10)
                    {
                        this.txtMinutes.Text = "0" + ts.Minutes.ToString();
                    }
                    else
                    {
                        this.txtMinutes.Text = ts.Minutes.ToString();
                    }
                    if (ts.Seconds < 10)
                    {
                        this.txtSeconds.Text = "0" + ts.Seconds.ToString();
                    }
                    else
                    {
                        this.txtSeconds.Text = ts.Seconds.ToString();
                    }
                }
            }
        }

		public void btnSave_Click(object sender, EventArgs e)
		{
            string title = this.txtTitle.Text.Trim();
            string des = this.txtDescription.Text;
            string sequence = this.txtSequence.Text;
            string url = this.txtOnlineVideo.Text;
            string singleUrl = "";
            int type = 0;
            GetSingleUrl(url, out singleUrl, out type);

            string privacy = this.radlPrivacy.SelectedValue;

            string strErr = "";

            int urlType=-1;
            if (!PageValidate.IsNumber(this.hfUrlType.Value.Trim()))
            {
                strErr +=Resources.CMSVideo.ErrorVideoType+ "\\n";
            }
            else
            {
                urlType = int.Parse(this.hfUrlType.Value);
            }
            if (title.Length == 0)
            {
                strErr +=Resources.CMSVideo.ErrorVideoTitleNull+"\\n";
            }
            if (urlType == 1)//网络视频
            {
                if (type == 0)
                {
                    ShowMsg(Resources.CMSVideo.TooltipVideoUrl, false, true);
                    return;
                    //strErr += "网络视频地址格式不正确";
                }
                //else
                //{
                //    ShowMsg("视频播放页地址正确！", true,true);
                //}
            }
            if (sequence.Length > 0)
            {
                if (!PageValidate.IsNumber(sequence))
                {
                    strErr +=Resources.CMSVideo.ErrorOrderFormat+"\\n";
                }
            }

            if (!PageValidate.IsNumber(privacy))
            {
                strErr +=Resources.CMSVideo.ErrorPrivacyFormed+"\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            string address = "";

            Maticsoft.Model.CMS.Video model = bll.GetModel(VideoID);
            if (null == model)
            {
                return;
            }
            model.Title = title;
            model.Description = des;

            model.AlbumID = Globals.SafeInt(this.dropAlbumID.SelectedValue, 0);

            model.LastUpdateUserID = CurrentUser.UserID;
            model.LastUpdateDate = DateTime.Now;
            model.Sequence = Globals.SafeInt(sequence, 1);

            model.VideoClassID = Globals.SafeInt(this.radlVideoClassID.SelectedValue, 0);

            model.IsRecomend = this.chkIsRecomend.Checked;

            if(urlType==0)//本地视频
            {
                model.UrlType = 0;
                string videoNewUrl = this.hfNewLocalVideo.Value;
                if (!string.IsNullOrWhiteSpace(videoNewUrl))
                {
                    model.VideoUrl = videoNewUrl;
                    model.VideoFormat = Path.GetExtension(videoNewUrl);
                    string imgurl = videoNewUrl + ".jpg";
                    if (File.Exists(HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + imgurl)))
                    {
                        address = HttpContext.Current.Server.MapPath(MvcApplication.UploadFolder + imgurl);
                        model.ImageUrl = imgurl;
                    }
                    //if (File.Exists(HttpContext.Current.Server.MapPath(UploadFolder + videoNewUrl)))
                    //{
                    //    model.TotalTime = GetVideoTotalTime(HttpContext.Current.Server.MapPath(UploadFolder + videoNewUrl));
                    //}
                }
                else
                {
                    model.VideoUrl = this.hfLocalVideo.Value;
                    model.VideoFormat = this.txtVideoFormat.Text;
                    model.ImageUrl = this.txtImageUrl.Text;
                }
            }

            if(urlType==1)//网络视频
            {
                if (type == 1)
                {
                    YouKuInfo info = VideoHelper.GetYouKuInfo(singleUrl);
                    if (null != info)
                    {
                        model.VideoUrl = singleUrl; //string.Format("http://player.youku.com/player.php/sid/{0}/v.swf", info.VidEncoded);

                        string domain = "";
                        string subDomain = "";
                        UrlOper.GetDomain(singleUrl, out domain, out subDomain);
                        if (!string.IsNullOrWhiteSpace(domain))
                        {
                            model.Domain = domain;
                        }

                        address = info.Logo;
                        if (string.IsNullOrWhiteSpace(model.Title))
                        {
                            if (!string.IsNullOrWhiteSpace(info.Title))
                            {
                                model.Title = info.Title;
                            }
                        }
                        model.UrlType = 1;

                    }
                }

                if (type == 2)
                {
                    Ku6Info info = VideoHelper.GetKu6Info(singleUrl);
                    if (null != info)
                    {
                        model.VideoUrl = singleUrl;// info.flash;//播放地址

                        string domain = "";
                        string subDomain = "";
                        UrlOper.GetDomain(singleUrl, out domain, out subDomain);
                        if (!string.IsNullOrWhiteSpace(domain))
                        {
                            model.Domain = domain;
                        }

                        address = info.coverurl;
                        model.ImageUrl=address;
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

            model.Tags = this.txtTags.Text;

            model.UrlType = Globals.SafeInt(this.hfUrlType.Value, 0);

            string newAttachment = this.hfNewAttachment.Value;
            if (!string.IsNullOrWhiteSpace(newAttachment))
            {
                model.Attachment = newAttachment;
            }
            else
            {
                model.Attachment = this.hfAttachment.Value;
            }

            //隐私:0.对所有人公开 1.仅自己可见 2.仅好友可见。
            model.Privacy = Globals.SafeInt(this.radlPrivacy.SelectedValue, 0);
            //视频状态:0.转码中 1.转码失败 2.待审核 3.正常 4.被屏蔽 5.发布。
            model.State = Globals.SafeInt(this.radlState.SelectedValue, 5);

            model.Remark = this.txtRemark.Text;

            model.TotalTime = TimeParser.TimeToSecond(Globals.SafeInt(this.txtTotalHours.Text, 0), Globals.SafeInt(this.txtMinutes.Text, 0), Globals.SafeInt(this.txtSeconds.Text, 0));

            if (bll.Update(model))
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "list.aspx");
            }
            else
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }

		}

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        protected void ShowMsg(string msg, bool success, bool isWarning)
        {
            this.statusMessage.Success = success;
            this.statusMessage.IsWarning = isWarning;
            this.statusMessage.Text = msg;
            this.statusMessage.Visible = true;
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
        /// <param name="fileName"></param>
        /// <param name="IsThumbImage"></param>
        /// <param name="IsNormalImage"></param>
        /// <param name="thumbImage"></param>
        /// <param name="normalImage"></param>
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

        private static Regex regUrl = new Regex(@"(http:\/\/([\w.]+\/?)\S*)");

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
        /// 得到第一个匹配正确的网络视频url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="singleUrl"></param>
        /// <param name="type">0：匹配不成功！、1：</param>
        /// <returns></returns>
        public string GetSingleUrl(string url, out string singleUrl, out int type)
        {
            singleUrl = "";
            type = 0;
            if (IsUrl(url))
            {
                if (VideoHelper.IsYouKuVideoUrl(url))
                {
                    singleUrl = url;
                    type = 1;//优库视频地址
                    return singleUrl;
                }
                if (VideoHelper.IsKu6VideoUrl(url))
                {
                    singleUrl = url;
                    type = 2;//酷6视频
                    return singleUrl;
                }
            }

            return singleUrl;
        }
    }
}
