using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.IO;

namespace Maticsoft.Web.Admin.WeChat.MsgItem
{
    public partial class AddItem : PageBaseAdmin
    {
      private  Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
      private Maticsoft.WeChat.BLL.Core.MsgItem itemBll = new Maticsoft.WeChat.BLL.Core.MsgItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
                if (String.IsNullOrWhiteSpace(openId))
                {
                    MessageBox.ShowFailTip(this, "您还未填写微信原始ID，请在公众号配置中填写！", "/admin/WeChat/Setting/Config.aspx");
                }

                //商品一级分类
                this.ddCategory.DataSource = cateBll.GetCategorysByDepth(1);
                this.ddCategory.DataTextField = "Name";
                this.ddCategory.DataValueField = "CategoryId";
                this.ddCategory.DataBind();
                this.ddCategory.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            string savePath = "/Upload/WeChat/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            Maticsoft.WeChat.Model.Core.MsgItem itemModel = new Maticsoft.WeChat.Model.Core.MsgItem();

            if (String.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                MessageBox.ShowFailTip(this, "请填写素材标题！");
                return;
            }
            string tempImg = this.ImagePath.Value;
            if (String.IsNullOrWhiteSpace(tempImg))
            {
                MessageBox.ShowFailTip(this, "请上传素材图片！");
                return;
            }
            itemModel.Description = this.txtDesc.Text;
            itemModel.OpenId = openId;
            itemModel.Title = txtTitle.Text;
            itemModel.Url=this.txtUrl.Text;
            //移动图片 
        
            string imgname = tempImg.Substring(tempImg.LastIndexOf("/") + 1);
            string saveImg = tempImg;
            if (!String.IsNullOrWhiteSpace(tempImg) && tempImg.Contains("/Upload/Temp"))
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(tempImg, "N_"))))
                {
                    string originalUrl = String.Format(savePath + imgname, "N_");
                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(tempImg, "N_")), HttpContext.Current.Server.MapPath(originalUrl));
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(tempImg, "T_"))))
                {
                    string originalUrl = String.Format(savePath + imgname, "T_");
                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(tempImg, "T_")), HttpContext.Current.Server.MapPath(originalUrl));
                }
                saveImg = savePath + imgname;
            }
            itemModel.PicUrl = saveImg;
            int action = Common.Globals.SafeInt(ddlUrl.SelectedValue, 0);
            int categoryId = Common.Globals.SafeInt(ddCategory.SelectedValue, 0);
            switch (action)
            {
                case 0:
                    itemModel.Url = this.txtUrl.Text;
                    break;
                case 1:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                    break;
                case 2:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MPage);
                    break;
                case 3:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u";
                    break;
                case 4:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u/Orders";
                    break;
                case 5:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "p/" + categoryId;
                    break;
                default:
                    itemModel.Url = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                    break;
            }

            if (itemBll.Add(itemModel) > 0)
            {
                MessageBox.ShowSuccessTip(this, "操作成功！", "ItemList.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }

        }

    }
}