/**
* Modify.cs
*
* 功 能： N/A
* 类 名： Modify
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01							N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.SNS.Photos
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 592; } } //SNS_图片分享管理_编辑页
        Maticsoft.BLL.SNS.Photos bll = new Maticsoft.BLL.SNS.Photos();
        Maticsoft.BLL.SNS.PhotoTags bllTags = new BLL.SNS.PhotoTags();
        protected string TagsValue;
        protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
                ShowInfo(PhotoId);
             
			}
		}

        public int PhotoId
        {
            get
            {
                int photoId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    photoId = Globals.SafeInt(Request.Params["id"], 0);
                }
                return photoId;
            }
        }

        public int type
        {
            get
            {
                int _type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    _type = Globals.SafeInt(Request.Params["type"], 0);
                }
                return _type;
            }
        }
	private void ShowInfo(int PhotoID)
	{
		Maticsoft.BLL.SNS.Photos bll=new Maticsoft.BLL.SNS.Photos();
		Maticsoft.Model.SNS.Photos model=bll.GetModel(PhotoID);
		this.txtImage.ImageUrl=model.ThumbImageUrl;
        this.radlState.SelectedValue = model.Status.ToString();
		this.txtDescription.Text=model.Description;
		this.txtCreatedNickName.Text=model.CreatedNickName;
        this.PhotoCategory.SelectedValue = model.CategoryId.ToString();
        this.rabRecomend.SelectedValue = model.IsRecomend.ToString();
		this.txtCommentCount.Text=model.CommentCount.ToString();
		this.txtFavouriteCount.Text=model.FavouriteCount.ToString();
        ddlTags.DataSource = bllTags.GetList("");
        ddlTags.DataTextField = "TagName";
        ddlTags.DataValueField = "TagName"; 
        ddlTags.DataBind();
        TagsValue = model.Tags;
     
	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			string Description=this.txtDescription.Text;
            int Status = Common.Globals.SafeInt(this.radlState.SelectedValue,0);
			string CreatedNickName=this.txtCreatedNickName.Text;
            int CategoryId =Common.Globals.SafeInt(this.PhotoCategory.SelectedValue,0);
            int IsRecomend = Common.Globals.SafeInt(this.rabRecomend.SelectedValue,0);

            Maticsoft.Model.SNS.Photos model = bll.GetModel(PhotoId);
			model.Description=Description;
			model.Status=Status;
			model.CreatedNickName=CreatedNickName;
			model.CategoryId=CategoryId;
			model.IsRecomend=IsRecomend;
            //model.Tags =HidTags.Value;
            string[]  Tags= HidTags.Value.Split(',');
            if (Tags.Length > 0)
            {
                model.Tags = "";
                foreach (var item in Tags)
                {
                    model.Tags += "'" + item + "',";
                }
                model.Tags = model.Tags.Substring(0, model.Tags.LastIndexOf(","));
            
            }
			bll.Update(model);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx?type=" + type);
		}

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx?type="+ type);
        }
    }
}
