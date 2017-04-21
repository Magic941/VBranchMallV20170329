/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
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
using System.Web.UI;

namespace Maticsoft.Web.Admin.Shop.ProductReview
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 468; } } //Shop_商品评论管理_详细页
        public string strid = "";
        private Maticsoft.BLL.Shop.Products.ProductReviews PreviewBll = new BLL.Shop.Products.ProductReviews();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ReviewId > 0)
                {
                    ShowInfo(ReviewId);
                }
            }
        }

        public int ReviewId
        {
            get
            {
                int reviewid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    reviewid = Common.Globals.SafeInt(Request.Params["id"], 0);
                }
                return reviewid;
            }
        }

        private void ShowInfo(int ReviewId)
        {
            Maticsoft.BLL.Shop.Products.ProductReviews bll = new Maticsoft.BLL.Shop.Products.ProductReviews();
            Maticsoft.Model.Shop.Products.ProductReviews model = bll.GetModel(ReviewId);
            if (null != model)
            {
                this.lblReviewId.Text = model.ReviewId.ToString();
                this.lblProductId.Text = new BLL.Shop.Products.ProductInfo().GetProductName(model.ProductId);
                this.lblUserId.Text = model.UserId.ToString();
                this.lblReviewText.Text = Common.Globals.HtmlDecode(model.ReviewText);
                this.lblUserName.Text = model.UserName;
                this.lblUserEmail.Text = model.UserEmail;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblParentId.Text = model.ParentId.ToString();
                this.hidImagesNames.Value = model.ImagesNames;
                this.hidImagesPath.Value = model.ImagesPath;
                if (model.Status == 0)
                {
                    this.lblState.Text = "未审核";
                }
                else if (model.Status == 1)
                {
                    this.lblState.Text = "已审核";
                }
                else
                {
                    this.lblState.Text = "审核失败";
                }
                this.lblScore.Text = new BLL.Shop.Products.ScoreDetails().GetScore(ReviewId).ToString();
            }
        }
        //取消
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
        //审核通过
        public void btnPassApproved_Click(object sender, EventArgs e)
        {
            int ReviewId = Common.Globals.SafeInt(lblReviewId.Text, -1);
            if (ReviewId < 1)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "账户信息非法, 请联系系统管理员!");
                return;
            }
            Maticsoft.Model.Shop.Products.ProductReviews PreviewModel = PreviewBll.GetModel(ReviewId);
            PreviewModel.Status = 1;
            PreviewModel.ReviewId = ReviewId;
            if (PreviewBll.UpdateStatus(PreviewModel))
            {
                Common.MessageBox.ShowSuccessTip(this, "审核成功", "List.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "审核失败,稍后重试!");
            }
            Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipOperateError);
        }
        //审核不通过
        public void btnNoPassApproved_Click(object sender, EventArgs e)
        {
            int ReviewId = Common.Globals.SafeInt(lblReviewId.Text, -1);
            if (ReviewId < 1)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "账户信息非法, 请联系系统管理员!");
                return;
            }
            Maticsoft.Model.Shop.Products.ProductReviews PreviewModel = PreviewBll.GetModel(ReviewId);
            PreviewModel.Status = 0;
            PreviewModel.ReviewId = ReviewId;
            if (PreviewBll.UpdateStatus(PreviewModel))
            {
                Common.MessageBox.ShowSuccessTip(this, "审核成功", "List.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "审核失败,稍后重试!");
            } 
            Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipOperateError);
        }

    }
}