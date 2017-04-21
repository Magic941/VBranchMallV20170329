/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
*
* Ver             变更日期                    负责人        变更内容
* ───────────────────────────────────
* V0.01  2012年5月31日 14:33:10  孙鹏             创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.UI;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.Advertisement
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 373; } } //网站管理_广告内容管理_详细页
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int AdvertisementId = (Convert.ToInt32(strid));
                    ShowInfo(AdvertisementId);
                }
            }
        }


        public int AdPositionID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["Adid"]))
                {
                    id = Globals.SafeInt(Request.Params["Adid"], 0);
                }
                return id;
            }
        }

        private void ShowInfo(int AdvertisementId)
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
            Maticsoft.Model.Settings.Advertisement model = bll.GetModel(AdvertisementId);

            this.lblAdvertisementId.Text = model.AdvertisementId.ToString();
            this.lblAdvertisementName.Text = model.AdvertisementName;
            Maticsoft.BLL.Settings.AdvertisePosition bllPosition = new BLL.Settings.AdvertisePosition();
            Maticsoft.Model.Settings.AdvertisePosition modelPosition = bllPosition.GetModel(model.AdvPositionId.Value);
            if (modelPosition == null) return;

            this.lblAdvPositionId.Text = modelPosition.AdvPositionName;
            switch (model.ContentType.Value)
            {
                case 0:
                    this.rbTextContent.Checked = true;
                    break;
                case 1:
                    this.rbImgContent.Checked = true;
                    this.Image1.ImageUrl = model.FileUrl;
                    this.Image1.Width = modelPosition.Width.HasValue ? modelPosition.Width.Value : 0;
                    this.Image1.Height = modelPosition.Height.HasValue ? modelPosition.Height.Value : 0;
                    break;
                case 2:
                    this.rbFlashContent.Checked = true;
                    //this.Label1.Text = model.FileUrl;
                    int width=modelPosition.Width.HasValue ? modelPosition.Width.Value : 0;
                    int height = modelPosition.Height.HasValue ? modelPosition.Height.Value : 0;
                    this.litFlash.Text = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\""+width+"\" height=\""+height+"\"> <param name=\"wmode\" value=\"opaque\" /> <param name=\"movie\" value=\""+ model.FileUrl+"\" /><param name=\"quality\" value=\"high\" /><embed src=\""+model.FileUrl+"\" allowfullscreen=\"true\" quality=\"high\" width=\""+width+"\" height=\""+height+"\" align=\"middle\" wmode=\"transparent\" allowscriptaccess=\"always\" type=\"application/x-shockwave-flash\"></embed></object>";
                    break;
                case 3:
                    this.rbCodeContent.Checked = true;
                    break;
                default:
                    break;
            }
            switch (model.State.Value)
            {
                case 0:
                    this.rbStatusN.Checked = true;
                    break;
                case 1:
                    this.rbStatusY.Checked = true;
                    break;
                case -1:
                    this.rbStop.Checked = true;
                    break;
                default:
                    break;
            }
            this.lblAlternateText.Text = model.AlternateText;
            this.lblNavigateUrl.Text = model.NavigateUrl;
            this.lblAdvHtml.Text =Common.Globals.HtmlEncode( model.AdvHtml);
            this.lblImpressions.Text = model.Impressions.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
            this.lblStartDate.Text = model.StartDate.HasValue?model.StartDate.Value.ToString("yyyy-MM-dd"):"无限制";
            this.lblEndDate.Text = model.EndDate.HasValue ? model.EndDate.Value.ToString("yyyy-MM-dd") : "无限制";
            this.lblDayMaxPV.Text = model.DayMaxPV.ToString();
            this.lblDayMaxIP.Text = model.DayMaxIP.ToString();
            this.lblCPMPrice.Text = model.CPMPrice.Value.ToString("0.00");
            if (model.AutoStop.Value.Equals(0))
            {
                this.rbNoStup.Checked = true;
            }
            else if (model.AutoStop.Value.Equals(1))
            {
                this.rbAutoStop.Checked = true;
            }
            else
            {
                this.rbNoLimit.Checked = true;
            }
            this.lblSequence.Text = model.Sequence.ToString();
            this.lblEnterpriseID.Text = model.EnterpriseID.ToString();
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("SingleList.aspx?id={0}",AdPositionID));
        }
    }
}
