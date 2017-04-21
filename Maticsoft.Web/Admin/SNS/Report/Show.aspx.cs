/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
*
* Ver                   变更日期             负责人      变更内容
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

namespace Maticsoft.Web.Admin.SNS.Report
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 610; } } //SNS_举报管理_详细页
        private BLL.SNS.Posts postBll = new BLL.SNS.Posts();
        private Maticsoft.BLL.SNS.Report bll = new Maticsoft.BLL.SNS.Report();
        Maticsoft.BLL.SNS.ReportType typeBll = new BLL.SNS.ReportType();
        Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
        Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Id > 0)
                {
                    ShowInfo(Id);
                }
            }
        }

        public int Id
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        private void ShowInfo(int Id)
        {
            Model.SNS.Report model = bll.GetModel(Id);

            if (model != null)
            {
                Model.SNS.ReportType typeModel = typeBll.GetModel(model.ReportTypeID);
                lblType.Text = typeModel.TypeName;
                lblDesc.Text = model.Description;
                //动态
                if (model.TargetType == 0)
                {
                    Maticsoft.Model.SNS.Posts postModel = postBll.GetModel(model.TargetID);
                    this.lblName.Text = "此信息已不存在";
                    if (postModel != null)
                    {
                        this.lblName.Text = postModel.Description;
                        if (!String.IsNullOrWhiteSpace(postModel.ImageUrl))
                        {
                            this.lblImage.ImageUrl = postModel.ImageUrl;
                            this.lblImage.Visible = true;
                        }
                    }
                }
                //图片
                if (model.TargetType == 1)
                {
                    Maticsoft.Model.SNS.Photos photoModel = photoBll.GetModel(model.TargetID);
                    this.lblName.Text = "此信息已不存在";
                    if (photoModel != null)
                    {
                        this.lblName.Text = "<a href='/Photo/Detail/" + photoModel.PhotoID + "' target='_blank'>" + photoModel.PhotoName + "</a>";
                        if (!String.IsNullOrWhiteSpace(photoModel.ThumbImageUrl))
                        {
                            this.lblImage.ImageUrl = photoModel.ThumbImageUrl;
                            this.lblImage.Visible = true;
                        }
                    }
                }
                //商品
                if (model.TargetType == 2)
                {
                    Maticsoft.Model.SNS.Products productModel = productBll.GetModel(model.TargetID);
                    this.lblName.Text = "此信息已不存在";
                    if (productModel != null)
                    {
                        this.lblName.Text = "<a href='/Product/Detail/" + productModel.ProductID + "' target='_blank'>" + productModel.ProductName + "</a>";
                        if (!String.IsNullOrWhiteSpace(productModel.ThumbImageUrl))
                        {
                            this.lblImage.ImageUrl = productModel.ThumbImageUrl;
                            this.lblImage.Visible = true;
                        }
                    }
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx");
        }

        protected void btnReportTrue_Click(object sender, EventArgs e)
        {
            postBll.DeleteListByNormalPost(Id.ToString(),true,CurrentUser.UserID);
            bll.UpdateReportStatus(1, Id);
            //Maticsoft.Common.MessageBox.ShowSuccessTip(this, "处理成功！");
            Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='Report.aspx'");
        }

        protected void btnReportFalse_Click(object sender, EventArgs e)
        {
            postBll.DeleteListByNormalPost(Id.ToString(),true,CurrentUser.UserID);
            bll.UpdateReportStatus(2, Id);
            //Maticsoft.Common.MessageBox.ShowSuccessTip(this, "处理成功！");
            Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='Report.aspx'");
        }

        protected void btnReportUnKnow_Click(object sender, EventArgs e)
        {
            postBll.DeleteListByNormalPost(Id.ToString(),true,CurrentUser.UserID);
            bll.UpdateReportStatus(3, Id);
            //Maticsoft.Common.MessageBox.ShowSuccessTip(this, "处理成功！");
            Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='Report.aspx'");
        }
    }
}