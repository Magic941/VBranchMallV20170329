using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.PhotoAlbum
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 245; } } //CMS_相册管理_编辑页
        protected string strThumbImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageWidth");
        protected string strThumbImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageHeight");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.AlbumID != 0)
                {
                    ShowInfo(this.AlbumID);
                }
            }
        }

        public int AlbumID
        {
            get
            {
                int iId = 0;
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    iId = (Convert.ToInt32(Request.Params["id"]));
                }
                return iId;
            }
        }
        private void ShowInfo(int AlbumID)
        {
            BLL.CMS.PhotoAlbum bll = new BLL.CMS.PhotoAlbum();
            Model.CMS.PhotoAlbum model = bll.GetModel(AlbumID);
            this.txtAlbumName.Text = model.AlbumName;
            this.txtDescription.Text = model.Description;

            BLL.CMS.Photo photoBll = new BLL.CMS.Photo();
            Model.CMS.Photo photo = photoBll.GetModel(model.CoverPhoto.Value);
            if (photo != null)
            {
                this.imgCoverPhoto.ImageUrl = Maticsoft.Web.Components.FileHelper.GeThumbImage(photo.ThumbImageUrl, "T235x1280_");
            }
            this.radlState.SelectedValue = model.State.ToString();
            this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblPVCount.Text = model.PVCount.ToString();
            this.txtSequence.Text = model.Sequence.ToString();
            this.radlPrivacy.SelectedValue = model.Privacy.ToString();
            this.lblLastUpdatedDate.Text = model.LastUpdatedDate.ToString();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtAlbumName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorAlbumNull);
                return;
            }
            if (!PageValidate.IsNumber(txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorOrderFormat);
                return;
            }
            string AlbumName = this.txtAlbumName.Text;
            string Description = this.txtDescription.Text;
            int State = int.Parse(this.radlState.SelectedValue);
            int Sequence = int.Parse(this.txtSequence.Text);

            BLL.CMS.PhotoAlbum bll = new BLL.CMS.PhotoAlbum();
            Model.CMS.PhotoAlbum model = bll.GetModel(this.AlbumID);
            model.AlbumName = AlbumName;
            model.Description = Description;
            model.State = State;
            model.Sequence = Sequence;
            model.Privacy = int.Parse(this.radlPrivacy.SelectedValue);
            model.LastUpdatedDate = DateTime.Now;

            if (bll.Update(model))
            {
                MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
        }
    }
}
