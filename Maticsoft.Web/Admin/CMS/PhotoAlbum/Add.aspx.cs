using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.PhotoAlbum
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 244; } } //CMS_相册管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
            {
                MessageBox.ShowAndBack(this, "您没有权限");
                return;
            }
            BLL.CMS.PhotoAlbum bll = new BLL.CMS.PhotoAlbum();
            txtSequence.Text = bll.GetMaxSequence().ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BLL.CMS.PhotoAlbum bll = new BLL.CMS.PhotoAlbum();
            if (string.IsNullOrWhiteSpace(this.txtAlbumName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorAlbumNull);
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

            string AlbumName = this.txtAlbumName.Text;
            string Description = this.txtDescription.Text;
            int State = int.Parse(this.radlState.SelectedValue);
            int PVCount = 0;
            int Privacy = int.Parse(this.radlPrivacy.SelectedValue);

            Model.CMS.PhotoAlbum model = new Model.CMS.PhotoAlbum
                                               {
                                                   AlbumName = AlbumName,
                                                   Description = Description,
                                                   CoverPhoto = 0,
                                                   State = State,
                                                   CreatedUserID = CurrentUser.UserID,
                                                   CreatedDate = DateTime.Now,
                                                   PVCount = PVCount,
                                                   Sequence = bll.GetMaxSequence(),
                                                   Privacy = Privacy,
                                                   LastUpdatedDate = DateTime.Now
                                               };
            int AlbumID = bll.Add(model);
            MessageBox.ResponseScript(this, "parent.location.href='/Admin/CMS/Photo/add.aspx?AlbumID=" + AlbumID + "'");
        }
    }
}
