/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:39:32  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;
using Maticsoft.Common;
using Maticsoft.Common.Video;
using System.Drawing;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.CMS.Video
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
        protected override int Act_PageLoad { get { return 269; } }//CMS_视频信息管理_列表页
        protected new int Act_AddData = 273;    //CMS_视频信息管理_添加数据 
        protected new int Act_UpdateData = 274;    //CMS_视频信息管理_编辑用户
        protected new int Act_DelData = 275;    //CMS_视频信息管理_删除用户
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    lbtnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                LoadVideoAlbumData();
                //this.dropClass.DataBind();

                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
            }
        }

        protected void LoadVideoAlbumData()
        {
            Maticsoft.BLL.CMS.VideoAlbum bll = new Maticsoft.BLL.CMS.VideoAlbum();

            DataSet ds = bll.GetAllList();

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropAlbum.DataSource = ds;
                this.dropAlbum.DataTextField = "AlbumName";
                this.dropAlbum.DataValueField = "AlbumID";
                this.dropAlbum.DataBind();
            }
            this.dropAlbum.Items.Insert(0, new ListItem(Resources.Site.PleaseSelect, "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        #region gridView

        public int AlbumID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["AlbumID"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                    this.dropAlbum.Enabled = false;
                    this.dropAlbum.SelectedValue = strid;
                }
                return id;
            }
        }
        public int VideoClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["VideoClassID"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                    //this.dropClass.Enabled = false;
                    this.VideoClassDropList1.SelectedValue = strid;
                }
                return id;
            }
        }


        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[7].Visible = false;

            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[8].Visible = false ;
              
            }
            
            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" Title like '%{0}%' ", txtKeyword.Text.Trim());
            }
            if (this.dropState.SelectedValue != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.AppendFormat(" AND State={0} ", this.dropState.SelectedValue);
                }
                else
                {
                    strWhere.AppendFormat(" State={0} ", this.dropState.SelectedValue);
                }
            }
            if (this.dropAlbum.SelectedValue != "" && this.dropAlbum.SelectedValue != "0")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.AppendFormat(" AND AlbumID={0} ", this.dropAlbum.SelectedValue);
                }
                else
                {
                    strWhere.AppendFormat(" AlbumID={0} ", this.dropAlbum.SelectedValue);
                }
            }
            if (AlbumID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.AppendFormat(" AND AlbumID={0} ", AlbumID);
                }
                else
                {
                    strWhere.AppendFormat(" AlbumID={0} ", AlbumID);
                }
            }
            if (this.VideoClassDropList1.SelectedValue != "" && this.VideoClassDropList1.SelectedValue != "0")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.AppendFormat(" AND VideoClassID={0} ", this.VideoClassDropList1.SelectedValue);
                }
                else
                {
                    strWhere.AppendFormat(" VideoClassID={0} ", this.VideoClassDropList1.SelectedValue);
                }
            }
            if (VideoClassID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.AppendFormat(" AND VideoClassID={0} ", VideoClassID);
                }
                else
                {
                    strWhere.AppendFormat(" VideoClassID={0} ", VideoClassID);
                }
            }
            ds = bll.GetListEx(strWhere.ToString(), "");
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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
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

        #region 得到视频信息
        /// <summary>
        /// 得到视频信息
        /// </summary>
        public int? GetVideInfo(object target, out string videourl, out int? UType, out int vid)
        {
            videourl = "";
            UType = null;
            vid = 0;
            if (!StringPlus.IsNullOrEmpty(target) && PageValidate.IsNumber(target.ToString()))
            {
                Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
                Maticsoft.Model.CMS.Video model = bll.GetModelEx(Globals.SafeInt(target.ToString(), 0));
                if (null == model)
                {
                    return null;
                }
                vid = model.VideoID;
                string vUrl = model.VideoUrl;
                UType = model.UrlType;
                if (UType.HasValue)
                {
                    switch (UType.Value)
                    {
                        case 0://本地视频
                            videourl = vUrl;
                            break;
                        case 1://网络视频
                            string flashUrl = GetFlashUrl(vUrl);
                            if (!string.IsNullOrWhiteSpace(flashUrl))
                            {
                                videourl = flashUrl;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return UType;
        }
        #endregion

        #region 根据VideoID输出HtmlCode
        /// <summary>
        /// 根据VideoID输出HtmlCode
        /// </summary>
        /// <param name="localVideoCss">本地视频样式</param>
        /// <param name="onlineVideoCss">网络视频样式</param>
        /// <param name="target">VideoID</param>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        public string OutHtmlCodeByVideoID(string localVideoCss, string onlineVideoCss, object target, string imageUrl, int width, int height)
        {
            string htmlcode = "<a class=\"{0}\" {1} href=\"{2}\"/><img src=\"{3}\" alt=\"\" width=\"{4}px\" height=\"{5}px\" style=\"border:none\"  /></a>";

            string videourl = "";
            int? urltype = null;
            int vid;
            GetVideInfo(target, out videourl, out urltype, out vid);

            if (vid > 0 && urltype.HasValue)
            {
                switch (urltype.Value)
                {
                    case 0://本地视频
                        htmlcode = string.Format(htmlcode, localVideoCss, "", "VideoPreview.aspx?id=" + vid, imageUrl, width, height);
                        break;
                    case 1://网络视频
                        htmlcode = string.Format(htmlcode, onlineVideoCss, "", videourl, imageUrl, width, height);
                        break;
                }
            }
            return htmlcode;
        }
        #endregion

        #region 秒转换为时分秒
        /// <summary>
        /// 秒转换为时分秒
        /// </summary>
        public string SecondToDateTime(object target)
        {
            string time = "00:00:00";
            if (!StringPlus.IsNullOrEmpty(target) && PageValidate.IsNumber(target.ToString()))
            {
                time = TimeParser.SecondToDateTime(Convert.ToInt32(target));
            }
            return time;
        }
        #endregion

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!string.IsNullOrWhiteSpace(this.dropType.SelectedValue) && PageValidate.IsNumber(this.dropType.SelectedValue))
            {
                if (bll.UpdateList(idlist, " State=" + this.dropType.SelectedValue))
                {
                    gridView.OnBind();
                }
            }
        }

        #region 截取字符串
        public string SubString(object target, string sign, int subLength)
        {
            return StringPlus.SubString(target, subLength, sign);
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
