using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.AddTags
{
    public partial class AddTags : PageBaseAdmin
    {

         protected  StringBuilder strTagsValue = new StringBuilder();
         protected StringBuilder strSelectValue = new StringBuilder();
         Maticsoft.BLL.SNS.PhotoTags photoTags = new BLL.SNS.PhotoTags();
         Maticsoft.BLL.SNS.Tags productTags = new BLL.SNS.Tags();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTags();
            }         
        }
        private void BindTags()
        {
            if (Type == "Product")
            {
                int id = Maticsoft.Common.Globals.SafeInt(Id, 0);
                Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
                Maticsoft.Model.SNS.Products productModel = new Model.SNS.Products();
                List<Maticsoft.Model.SNS.Tags> List = new List<Model.SNS.Tags>();
                productModel = productBll.GetModel(id);
                if (productModel != null && !string.IsNullOrEmpty(productModel.Tags))
                {
                    foreach (string item in productModel.Tags.Split(','))
                    {
                        if (item.Length < 10)
                        {
                            strSelectValue.Append("<span class='SKUValue'><span class='span1'><a>" + item + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del'>删除</a></span> </span>");

                        }
                    }
                    hidValue.Value = productModel.Tags;
                }
                List = productTags.GetModelList("TypeId in (select ID from SNS_TagType where cid>=0)");
                foreach (Maticsoft.Model.SNS.Tags item in List)
                {
                    if (productModel != null && !string.IsNullOrEmpty(productModel.Tags))
                    {
                        if (!productModel.Tags.Contains(item.TagName))
                        {
                            strTagsValue.Append("<span class='SKUValue' id='span" + item.TagID + "'><span class='span1'><a id='tags" + item.TagID + "'>" + item.TagName + "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del" + item.TagID + "'>删除</a></span> </span>");

                        }
                    }
                    else
                    {
                        strTagsValue.Append("<span class='SKUValue' id='span" + item.TagID + "'><span class='span1'><a id='tags" + item.TagID + "'>" + item.TagName + "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del" + item.TagID + "'>删除</a></span> </span>");


                    }
                }
            }
            if (Type == "Photo")
            {
                int id = Maticsoft.Common.Globals.SafeInt(Id, 0);
                Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
                Maticsoft.Model.SNS.Photos photoModel = new Model.SNS.Photos();
                List<Maticsoft.Model.SNS.PhotoTags> List = new List<Model.SNS.PhotoTags>();
                photoModel = photoBll.GetModel(id);
                if (photoModel != null && !string.IsNullOrEmpty(photoModel.Tags))
                {
                    foreach (string item in photoModel.Tags.Split(','))
                    {
                        strSelectValue.Append("<span class='SKUValue'><span class='span1'><a>" + item + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del'>删除</a></span> </span>");
                    }
                    hidValue.Value = photoModel.Tags;
                }
                List = photoTags.GetModelList("");
                foreach (Maticsoft.Model.SNS.PhotoTags item in List)
                {
                    if (photoModel != null && !string.IsNullOrEmpty(photoModel.Tags))
                    {
                        if (!photoModel.Tags.Contains(item.TagName))
                        {
                            strTagsValue.Append("<span class='SKUValue' id='span" + item.TagID + "'><span class='span1'><a id='tags" + item.TagID + "'>" + item.TagName + "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del" + item.TagID + "'>删除</a></span> </span>");

                        }
                    }
                    else
                    {
                        strTagsValue.Append("<span class='SKUValue' id='span" + item.TagID + "'><span class='span1'><a id='tags" + item.TagID + "'>" + item.TagName + "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del" + item.TagID + "'>删除</a></span> </span>");


                    }
                }
            }
        }
        /// <summary>
        ///帖子的id
        /// </summary>
        public string Type
        {
            get
            {
                string type = Request.QueryString["Type"];
                return type;
            }
        }

        public string Id
        {
            get
            {
                string id = Request.QueryString["Id"];
                return id;
            }
        }

        public int GetTypeIdByProductId(int ProductId)
        {
            var bProducts = new BLL.SNS.Products();
            Model.SNS.Products model = bProducts.GetModel(ProductId);
            if (model != null)
            {
                var bCategories = new BLL.SNS.Categories();
                var mCategories = new Model.SNS.Categories();
                List<Maticsoft.Model.SNS.TagType> list=new List<Model.SNS.TagType>();
                int Cid = 0;
                if (model.CategoryID.HasValue)
                {
                     mCategories = bCategories.GetModel(model.CategoryID.Value);
                     Cid = Globals.SafeInt(mCategories.Path, 0);
                     string[] paths = mCategories.Path.Split('|');
                    if (paths.Length > 0)
                    {
                        Cid = Globals.SafeInt(paths[0], 0);
                    }
                }
                var bTagType = new BLL.SNS.TagType();
                list = bTagType.GetModelList("Cid=" + Cid + "");
                if ((list != null && list.Count <= 0)||Cid==0)
                {
                    list = bTagType.GetModelList("Cid>0");
                }
                if (list != null && list.Count > 0)
                {
                    return list[0].ID;
                }
            }
            return 0;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button btn = (Button) sender;
            if (Type == "Product")
            {
                int id = Globals.SafeInt(Id, 0);
                var productBll = new BLL.SNS.Products();
                var productModel = new Model.SNS.Products();
                productModel = productBll.GetModel(id);
                if (productModel != null)
                {
                    productModel.Tags = hidValue.Value.TrimEnd(',').TrimStart(',');
                    if (!string.IsNullOrEmpty(InputTags.Text))
                    {
                        productModel.Tags = productModel.Tags+ "," + InputTags.Text;
                    }
                    productModel.Tags = productModel.Tags.TrimEnd(',').TrimStart(',');
                    productBll.Update(productModel);
                    lblTip.Visible = true;
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "增加商品(ProductID=" + id + ")标签成功!",
                                       this);
                }
                var model = new Model.SNS.Tags();
                model.TagName = InputTags.Text;
                model.TypeId = GetTypeIdByProductId(Globals.SafeInt(Id, 0));
                if (!string.IsNullOrEmpty(model.TagName) && model.TypeId > 0)
                    productTags.Add(model);
                InputTags.Text = "";
                BindTags();
         
            }
            if (Type == "Photo")
            {
               
                    int id = Maticsoft.Common.Globals.SafeInt(Id, 0);
                    Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
                    Maticsoft.Model.SNS.Photos photoModel = new Model.SNS.Photos();
                    photoModel = photoBll.GetModel(id);
                    if (photoModel != null)
                    {
                        photoModel.Tags = hidValue.Value.TrimEnd(',').TrimStart(',');
                        if (!string.IsNullOrEmpty(InputTags.Text))
                        {
                            photoModel.Tags = photoModel.Tags+ "," + InputTags.Text;
                        }
                        photoModel.Tags = photoModel.Tags.TrimEnd(',').TrimStart(',');
                        photoBll.Update(photoModel);
                        lblTip.Visible = true;
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "增加图片（PhotoID=" + id + "）标签成功!",
                                           this);
                    }
              
                    Model.SNS.PhotoTags model = new Model.SNS.PhotoTags();
                    model.TagName = this.InputTags.Text;
                    if (!string.IsNullOrEmpty(model.TagName))
                        photoTags.Add(model);
                    this.InputTags.Text = "";
                    BindTags();
           
           
            }
        }

     
    }
}