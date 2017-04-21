using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.ShopCategories
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 250; } } //CMS_图片分类管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BLL.CMS.PhotoClass classBll = new BLL.CMS.PhotoClass();
                BLL.Shop.Products.GoodsType goodsbll = new BLL.Shop.Products.GoodsType();
                txtSequence.Text = goodsbll.GetMaxSort().ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
		{
            //BLL.CMS.PhotoClass classBll = new BLL.CMS.PhotoClass();
            BLL.Shop.Products.GoodsType goodsbll = new BLL.Shop.Products.GoodsType();
            if (string.IsNullOrWhiteSpace(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorClassNameNull);
                return;
            }

            if (goodsbll.ExistsByTypeName(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorClassRepeat);
                return;
			}
			if(!PageValidate.IsNumber(txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorOrderFormat);
                return;
			}
            
			string ClassName=this.txtClassName.Text;
            int ParentId = 0;
            if (!string.IsNullOrWhiteSpace(ddlgoodtype.SelectedValue))
            {
                ParentId = int.Parse(this.ddlgoodtype.SelectedValue);
            }
            string Path = string.Empty;
            int Depth = 0;
			int Sequence=int.Parse(this.txtSequence.Text);
            string EntryPicPath = this.hfEntryPicPath.Value.Trim().Replace("{0}", string.Empty);
            string BannerPicPath = this.hfBannerPicPath.Value.Trim().Replace("{0}", string.Empty);
            string BgColor = this.txtbgcolor.Text.Trim();

            Model.Shop.Products.GoodsType model = new Model.Shop.Products.GoodsType
                                               {
                                                   GoodTypeName = ClassName,
                                                   PID = ParentId,
                                                   Sort = Sequence,
                                                   Path = Path,
                                                   IshasClass = Depth,
                                                   EntryPicPath = EntryPicPath,
                                                   BannerPicPath = BannerPicPath,
                                                   BgColor = BgColor
                                               };

            BLL.Shop.Products.GoodsType goodsBll = new BLL.Shop.Products.GoodsType();
            int retId = goodsBll.Add(model);
            if (retId > 0)
            {
                string tempPath = string.Empty;
                Model.Shop.Products.GoodsType parentModel = goodsbll.GetModel(ParentId);
                if (parentModel != null)
                {
                    tempPath = parentModel.Path + retId + "|";
                }
                else
                {
                    tempPath = "0|" + retId + "|";
                }
                model = goodsbll.GetModel(retId);
                model.Path = tempPath;
                if (goodsbll.Update(model))
                {
                    if (goodsbll.Update(parentModel))
                    { 
                        // 更新成功
                    }
                }
            }
            MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
		}
    }
}
