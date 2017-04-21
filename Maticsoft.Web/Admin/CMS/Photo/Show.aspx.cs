using System;

namespace Maticsoft.Web.Admin.CMS.Photo
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 238; } } //CMS_图片管理_详细页
        public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int PhotoID=(Convert.ToInt32(strid));
					ShowInfo(PhotoID);
				}
			}
		}
		
	private void ShowInfo(int PhotoID)
	{
		BLL.CMS.Photo bll=new BLL.CMS.Photo();
		Model.CMS.Photo model=bll.GetModel(PhotoID);
		this.lblPhotoID.Text=model.PhotoID.ToString();
		this.lblPhotoName.Text=model.PhotoName;
		this.lblImageUrl.Text=model.ImageUrl;
		this.lblDescription.Text=model.Description;
		this.lblAlbumID.Text=model.AlbumID.ToString();
		this.lblState.Text=model.State.ToString();
		this.lblCreatedUserID.Text=model.CreatedUserID.ToString();
		this.lblCreatedDate.Text=model.CreatedDate.ToString();
		this.lblPVCount.Text=model.PVCount.ToString();
		this.lblClassID.Text=model.ClassID.ToString();
		this.lblThumbImageUrl.Text=model.ThumbImageUrl;
		this.lblNormalImageUrl.Text=model.NormalImageUrl;
		this.lblSequence.Text=model.Sequence.ToString();
		this.lblIsRecomend.Text=model.IsRecomend.Value?Resources.Site.lblTrue:Resources.Site.lblFalse;
		this.lblCommentCount.Text=model.CommentCount.ToString();
		this.lblTags.Text=model.Tags;

	}


    }
}
