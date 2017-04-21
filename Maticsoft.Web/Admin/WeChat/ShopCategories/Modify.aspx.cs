using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.ShopCategories
{
    public partial class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.GoodsType bll = new BLL.Shop.Products.GoodsType();

        protected override int Act_PageLoad { get { return 251; } } //CMS_图片分类管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.GoodTypeID != 0)
                {
                    ShowInfo(this.GoodTypeID);
                }
            }
        }

        public int GoodTypeID
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

        private void ShowInfo(int GoodTypeID)
        {
            //BLL.CMS.PhotoClass bll = new BLL.CMS.PhotoClass();
            //Model.CMS.PhotoClass model = bll.GetModel(ClassID);
            Model.Shop.Products.GoodsType model = bll.GetModel(GoodTypeID);
            this.txtGoodtypeName.Text = model.GoodTypeName;
            this.txtSequence.Text = model.Sort.ToString();
            this.ddlPhotoClass.SelectedValue = model.PID.ToString();
            this.hfEntryPicPath.Value = string.Empty;
            this.hfEntryPicPathOriginal.Value = model.EntryPicPath;
            this.hfBannerPicPath.Value = string.Empty;
            this.hfBannerPicPathOriginal.Value = model.BannerPicPath;
            this.txtbgcolor.Text = model.BgColor;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtGoodtypeName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorClassNameNull);
                return;
            }
            if (!PageValidate.IsNumber(txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorOrderFormat);
                return;
            }
            //BLL.CMS.PhotoClass bll = new BLL.CMS.PhotoClass();
            string GoodtypeName = this.txtGoodtypeName.Text;
            int ParentId = 0;
            if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
            {
                ParentId = int.Parse(this.ddlPhotoClass.SelectedValue);
            }

            string Path = string.Empty;
            int Sequence = int.Parse(this.txtSequence.Text);
            string EntryPicPath = string.IsNullOrWhiteSpace(this.hfEntryPicPath.Value.Trim()) ? this.hfEntryPicPathOriginal.Value : this.hfEntryPicPath.Value.Trim().Replace("{0}", string.Empty);
            string BannerPicPath = string.IsNullOrWhiteSpace(this.hfBannerPicPath.Value.Trim()) ? this.hfBannerPicPathOriginal.Value : this.hfBannerPicPathOriginal.Value + "|" + this.hfBannerPicPath.Value.Trim().Replace("{0}", string.Empty);
            string BgColor = this.txtbgcolor.Text.Trim();
            
            Model.Shop.Products.GoodsType model = bll.GetModel(this.GoodTypeID);
            model.GoodTypeName = GoodtypeName;
            model.Sort = Sequence;
            model.PID = ParentId;
            string strTempPath = string.Empty;
            Model.Shop.Products.GoodsType goodtype = bll.GetModel(ParentId);
            if (null != goodtype)
            {
                strTempPath = goodtype.Path + model.GoodTypeID + "|";
            }
            else
            {
                strTempPath = "0|" + model.GoodTypeID + "|";
            }
            model.Path = strTempPath;
            model.EntryPicPath = EntryPicPath;
            model.BannerPicPath = BannerPicPath;
            model.BgColor = BgColor;
            if (bll.Update(model))
            {
                MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
            }
            else
            {
                MessageBox.ShowFailTip(this,Resources.Site.TooltipSaveError);
            }
        }
    }
}
