using System;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.Photo
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 237; } } //CMS_图片管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BLL.CMS.PhotoAlbum bll = new BLL.CMS.PhotoAlbum();
                this.ddlAlbum.DataSource = bll.GetList("");
                this.ddlAlbum.DataTextField = "AlbumName";
                this.ddlAlbum.DataValueField = "AlbumID";
                this.ddlAlbum.DataBind();
                this.ddlAlbum.Items.Add(new ListItem("", "0"));

                if (PhotoID > 0)
                {
                    ShowInfo();
                }
            }
        }

        public int PhotoID
        {
            get
            {
                int id = 0;
                string strId=Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strId))
                {
                    id = Globals.SafeInt(strId, 0);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            BLL.CMS.Photo bll = new BLL.CMS.Photo();
            Model.CMS.Photo model = bll.GetModel(PhotoID);
            if (model != null)
            {
                this.txtPhotoName.Text = model.PhotoName;
                this.ShowImage.HRef = model.ImageUrl;
                this.txtDescription.Text = model.Description;
                this.ddlState.SelectedValue = model.State.ToString();
                this.lblCreatedUserID.Text = model.CreatedUserName;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblPVCount.Text = model.PVCount.ToString();
                if (model.ClassID != 1)
                {
                    this.ddlPhotoClass.SelectedValue = model.ClassID.ToString();
                }
                this.ThumbImage.ImageUrl = Components.FileHelper.GeThumbImage(model.ThumbImageUrl, "T235x1280_");
                if (model.Sequence.HasValue)
                {
                    this.txtSequence.Text = model.Sequence.ToString();
                }
                if (model.IsRecomend.HasValue)
                {
                    this.chkIsRecomend.Checked = model.IsRecomend.Value;
                }
                this.txtTags.Text = model.Tags;
                this.ddlAlbum.SelectedValue = model.AlbumID.ToString();
            } 
        }


        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtPhotoName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorImageNameNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorIntroductionNull);
                return;
            }
            if (!PageValidate.IsNumber(txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorOrderFormat);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtTags.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorLabel);
                return;
            }
            string PhotoName = this.txtPhotoName.Text;
            string Description = this.txtDescription.Text;
            int AlbumID = int.Parse(this.ddlAlbum.SelectedValue);
            int State = int.Parse(this.ddlState.SelectedValue);
            int PVCount = int.Parse(this.lblPVCount.Text);
            int ClassID = 0;
            if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
            {
                ClassID = int.Parse(this.ddlPhotoClass.SelectedValue);
            }
            int Sequence = int.Parse(this.txtSequence.Text);
            bool IsRecomend = this.chkIsRecomend.Checked;
            string Tags = this.txtTags.Text;
            BLL.CMS.Photo bll = new BLL.CMS.Photo();

            Model.CMS.Photo model = bll.GetModel(PhotoID);
            if (null != model)
            {
                model.PhotoName = PhotoName;
                model.Description = Description;
                model.AlbumID = AlbumID;
                model.State = State;
                model.PVCount = PVCount;
                model.ClassID = ClassID;
                model.Sequence = Sequence;
                model.IsRecomend = IsRecomend;
                model.Tags = Tags;

                if (bll.Update(model))
                {
                    MessageBox.ResponseScript(this, "parent.location.href='list.aspx?AlbumID=" + AlbumID + "'");
                }
            }
        }
    }
}
