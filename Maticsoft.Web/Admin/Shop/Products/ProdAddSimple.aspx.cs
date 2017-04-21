using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Package;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using Maticsoft.Model.Shop.Products;
using AttributeValue = Maticsoft.Model.Shop.Products.AttributeValue;
using ProductAccessorie = Maticsoft.Model.Shop.Products.ProductAccessorie;
using ProductImage = Maticsoft.Model.Shop.Products.ProductImage;
using Maticsoft.BLL.Members;
using System.Linq;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProdAddSimple : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 485; } } //Shop_商品管理_添加页
        //待上传的SKU图片名称
        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        private string skuImageFile = string.Format("/Upload/Shop/Images/ProductsSkuImages/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        private ArrayList skuImageList = new ArrayList();
        private ArrayList salePriceList = new ArrayList();
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ProductImagesThumbSize
                Size thumbSize = Maticsoft.Common.StringPlus.SplitToSize(
                    BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                    '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                hfProductImagesThumbSize.Value = thumbSize.Width + "," + thumbSize.Height;
                //获取是否开启相关选项卡
                this.hfIsOpenSku.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_AddProduct_OpenSku");
                //this.hfIsOpenFit.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_AddProduct_OpenFit");
                this.hfIsOpenRelated.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_AddProduct_OpenRelated");
                this.hfIsOpenSEO.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_AddProduct_OpenSEO");

                this.txtDisplaySequence.Text = (productBll.MaxSequence() + 1).ToString();
                this.BindPackageInfo();

                #region 商家
                Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                drpSupplier.DataSource = supplierManage.GetModelList("");
                DataSet ds = supplierManage.GetAllList();
                if (!DataSetTools.DataSetIsNull(ds))
                {
                    this.drpSupplier.DataSource = ds;
                    this.drpSupplier.DataTextField = "Name";
                    this.drpSupplier.DataValueField = "SupplierId";
                    this.drpSupplier.DataBind();
                }
                this.drpSupplier.Items.Insert(0, new ListItem("无", "0"));
                this.drpSupplier.SelectedIndex = 0;
                #endregion
            }
        }

        public void BindPackageInfo()
        {
            Maticsoft.BLL.Shop.Package.PackageCategory packageCategoryManager = new PackageCategory();

            DataSet ds = packageCategoryManager.GetList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlSelectPackageCategory.DataSource = ds;
                this.ddlSelectPackageCategory.DataTextField = "Name";
                this.ddlSelectPackageCategory.DataValueField = "CategoryId";

                this.ddlSelectPackageCategory.DataBind();
            }
            this.ddlSelectPackageCategory.Items.Insert(0, new ListItem("请选择类别", "0"));

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            SaveProductInfo();
        }

        private void SaveProductInfo()
        {
            #region Get PageData

            //分类
            if (string.IsNullOrWhiteSpace(this.Hidden_SelectValue.Value))
            {
                MessageBox.ShowFailTip(this, "请选择产品分类！");
                return;
            }

            //基本信息
            string productName = txtProductName.Text;
            int selectedProductType = Globals.SafeInt(hfCurrentProductType.Value, -1);
            int selectedProductBrand = Globals.SafeInt(hfCurrentProductBrand.Value, -1);
            int supplierId = Globals.SafeInt(drpSupplier.SelectedValue, -1);
            int? selectedRegionId = Globals.SafeInt(ajaxRegion.SelectedValue, -1);
            string unit = txtUnit.Text;
            decimal? marketPrice = Globals.SafeDecimal(txtMarketPrice.Text, -1);
            int displaySequence = Globals.SafeInt(txtDisplaySequence.Text, -1);

            //描述
            string shortDescription = txtShortDescription.Text;
            string description = txtDescription.Text;

            //SEO
            string urlRule = txtUrlRule.Text;
            string metaTitle = txtMeta_Title.Text;
            string metaDescription = txtMeta_Description.Text;
            string metaKeywords = txtMeta_Keywords.Text;
            string seoImageAlt = txtSeoImageAlt.Text;
            string seoImageTitle = txtSeoImageTitle.Text;

            //图片
            string splitProductImages = hfProductImages.Value;

            //属性
            string attributeInfoJson = hfCurrentAttributes.Value;

            //SKU
            string skuBaseJson = hfCurrentBaseProductSKUs.Value;
            string skuJson = hfCurrentProductSKUs.Value;
            bool hasSKU = false;

            //配件
           // string productAccessorieJson = hfSelectedAccessories.Value;

            //相关商品
            string relatedProductJson = this.HiddenField_RelatedProductInfo.Value;

            #endregion Get PageData

            #region Data Proc

            //CategoryId
            string[] productImages = new string[0];

            //商品状态
            int saleStatus = Globals.SafeInt(rblUpselling.SelectedValue, 0);

            //简介信息去除换行符号处理
            if (!string.IsNullOrWhiteSpace(shortDescription))
            {
                shortDescription = Globals.HtmlEncodeForSpaceWrap(shortDescription);
            }
            if (!string.IsNullOrWhiteSpace(splitProductImages))
            {
                productImages = splitProductImages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }

            #region Attribute

            if (string.IsNullOrWhiteSpace(attributeInfoJson))
            {
                MessageBox.Show(this, "属性信息不存在,请检查已填数据是否正确!");
                return;
            }
            List<Model.Shop.Products.AttributeInfo> attributeList = GetAttributeInfo4Json(LitJson.JsonMapper.ToObject(attributeInfoJson));

            #endregion Attribute

            #region SKU

            //SKU
            if (string.IsNullOrWhiteSpace(skuBaseJson))
            {
                MessageBox.Show(this, "基础SKU信息不存在,请检查已填数据是否正确!");
                return;
            }
            List<Model.Shop.Products.SKUInfo> skuList = null;


            decimal? lowestSalePrice = 0M;
            //是否启用SKU
            hasSKU = !string.IsNullOrWhiteSpace(skuJson);
            if (hasSKU)
            {
                //已启用SKU
                skuList = GetSKUInfo4Json(LitJson.JsonMapper.ToObject(skuJson));
                if (salePriceList.Count > 0)
                {
                    salePriceList.Sort();
                    lowestSalePrice = Convert.ToDecimal(salePriceList[0]);
                }

            }
            else
            {
                //未启用SKU
                skuList = GetSKUInfo4Json(LitJson.JsonMapper.ToObject(skuBaseJson));
                lowestSalePrice = Globals.SafeDecimal(txtSalePrice.Text, -1);
            }


            #endregion SKU

            #endregion Data Proc

            #region Set ProductInfo

            Model.Shop.Products.ProductInfo productInfo = new Model.Shop.Products.ProductInfo();

            //产品分类
            string[] productCategoriesArray = Hidden_SelectValue.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            productInfo.Product_Categories = productCategoriesArray;

            //基本信息
            productInfo.ProductName = productName;
            productInfo.CategoryId = 0;
            productInfo.TypeId = selectedProductType;
            productInfo.BrandId = selectedProductBrand;
            //商家
            if (supplierId > 0)
            {
                productInfo.SupplierId = supplierId;
            }
            else
            {
                productInfo.SupplierId = -1;
            }
            productInfo.RegionId = selectedRegionId == -1 ? null : selectedRegionId;
            productInfo.Unit = unit;
            productInfo.MarketPrice = marketPrice == -1 ? 0 : marketPrice;
            productInfo.LowestSalePrice = lowestSalePrice.Value;
            productInfo.DisplaySequence = displaySequence;
            productInfo.ProductCode = this.txtProductSKU.Text;

            //productInfo.MainCategoryPath = categoryInfo.Path;
            productInfo.AddedDate = DateTime.Now;
            productInfo.SaleStatus = saleStatus;

            //描述
            productInfo.ShortDescription = shortDescription;
            productInfo.Description = description;

            //SEO
            productInfo.SeoUrl = urlRule;
            productInfo.Meta_Title = metaTitle;
            productInfo.Meta_Description = metaDescription;
            productInfo.Meta_Keywords = metaKeywords;
            productInfo.SeoImageAlt = seoImageAlt;
            productInfo.SeoImageTitle = seoImageTitle;

            productInfo.Points = Globals.SafeDecimal(this.txtPoints.Text, 0);

            //Tags
            productInfo.Tags = "";

            if (productImages.Length == 0)
            {
                productInfo.ImageUrl =
                    productInfo.ThumbnailUrl1 = productInfo.ThumbnailUrl2 = "/Content/themes/base/Shop/images/none.png";
            }

            //待上传的图片名称
            string savePath = string.Format("/Upload/Shop/Images/Product/{0}/", DateTime.Now.ToString("yyyyMMdd"));
            string saveThumbsPath = "/Upload/Shop/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            ArrayList imageList = new ArrayList();
            for (int i = 0; i < productImages.Length; i++)
            {
                if (i == 0)
                {
                    //主图片
                    string imageUrl = string.Format(productImages[i], "");
                    string MainThumbnailUrl1 = productImages[i];
                    //string MainThumbnailUrl2 = string.Format(productImages[i], "N_");
                    productInfo.ImageUrl = imageUrl.Replace(tempFile, savePath);
                    productInfo.ThumbnailUrl1 = MainThumbnailUrl1.Replace(tempFile, saveThumbsPath);
                    //productInfo.ThumbnailUrl2 = MainThumbnailUrl2.Replace(tempFile, ImageFile);
                    imageList.Add(imageUrl.Replace(tempFile, ""));
                    imageList.Add(MainThumbnailUrl1.Replace(tempFile, ""));
                    // imageList.Add(MainThumbnailUrl2.Replace(tempFile, ""));
                }
                else
                {
                    //附图片
                    string AttachImageUrl = string.Format(productImages[i], "");
                    string AttachThumbnailUrl1 = productImages[i];
                    // string AttachThumbnailUrl2 = string.Format(productImages[i], "N_");
                    productInfo.ProductImages.Add(
                        new ProductImage
                            {
                                ImageUrl = AttachImageUrl.Replace(tempFile, savePath),
                                ThumbnailUrl1 = AttachThumbnailUrl1.Replace(tempFile, saveThumbsPath),
                                //  ThumbnailUrl2 = AttachThumbnailUrl2.Replace(tempFile, ImageFile)
                            }
                        );
                    imageList.Add(AttachImageUrl.Replace(tempFile, ""));
                    imageList.Add(AttachThumbnailUrl1.Replace(tempFile, ""));
                    //  imageList.Add(AttachThumbnailUrl2.Replace(tempFile, ""));
                }
                Maticsoft.BLL.Shop.Products.ProductImage.MoveImage(productImages[i], savePath, saveThumbsPath);
            }

            //属性
            productInfo.AttributeInfos = attributeList;

            //SKU
            productInfo.HasSKU = hasSKU;
            productInfo.SkuInfos = skuList;

            //配件
            //string[] strSkuIds = productAccessorieJson.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //List<ProductAccessorie> acList = new List<ProductAccessorie>();
            //foreach (string item in strSkuIds)
            //{
            //    ProductAccessorie Acmodel = new ProductAccessorie();
            //    Acmodel.Name = this.txtAccessorieName.Text;
            //    Acmodel.DiscountAmount = Globals.SafeDecimal(this.txtDiscountAmount.Text, 0);
            //    Acmodel.DiscountType = this.rdoDiscountType4D.Checked ? 0 : 1;
            //    Acmodel.MaxQuantity = Globals.SafeInt(this.txtMaxQuantity.Text, 1);
            //    Acmodel.MinQuantity = Globals.SafeInt(this.txtMinQuantity.Text, 1);
            //    Acmodel.SkuId = item;
            //    acList.Add(Acmodel);
            //}
            //productInfo.ProductAccessories = acList;// productAccessorieJson;

            //相关商品
            string[] strRelatedProductIds = relatedProductJson.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            productInfo.RelatedProductId = strRelatedProductIds;

            #endregion Set ProductInfo

            //商品推荐
            productInfo.isRec = this.chbRec.Checked;
            productInfo.isNow = this.chbNew.Checked;
            productInfo.isHot = this.chbHot.Checked;
            productInfo.isLowPrice = this.chbLowPrice.Checked;

            #region Set Package
            if (!string.IsNullOrEmpty(Hidden_SelectPackage.Value))
            {
                Maticsoft.BLL.Shop.Package.Package Packagebll = new BLL.Shop.Package.Package();
                List<int> PackageId = new List<int>();
                string[] PackageIds = Hidden_SelectPackage.Value.Split(',');
                int id = 0;
                foreach (var packageId in PackageIds)
                {
                    id = Common.Globals.SafeInt(packageId, 0);
                    if (Packagebll.Exists(id))
                    {
                        PackageId.Add(id);
                        //  model

                    }
                }
                productInfo.PackageId = PackageId;
            }
            #endregion

            long ProductId = 0;
            if (BLL.Shop.Products.ProductManage.AddProduct(productInfo, out ProductId))
            {
                //将图片从临时文件夹移动到正式的文件夹下
                //FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);

                if (skuImageList.Count > 0)
                {
                    FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(skuImageFile), skuImageList);
                }
                #region  同步到微博
                string mediaIDs = "";
                mediaIDs = this.chkSina.Checked ? "3" : "";
                if (chkQQ.Checked)
                {
                    mediaIDs = mediaIDs + (String.IsNullOrWhiteSpace(mediaIDs) ? "13" : ",13");
                }
                Maticsoft.BLL.Members.UserBind bindBll = new UserBind();
                string url = "http://" + Common.Globals.DomainFullName + "/Product/Detail/" + ProductId;
                bindBll.SendWeiBo(-1, mediaIDs, productInfo.ProductName, url, productInfo.ImageUrl);
                #endregion

                Response.Redirect(string.Format("ProductsInStock.aspx?SaleStatus={0}", saleStatus));
            }
            else
            {
                MessageBox.Show(this, "保存失败! 请重试.");
                return;
            }
        }

        #region Json处理

        #region Attribute

        private List<Model.Shop.Products.AttributeInfo> GetAttributeInfo4Json(LitJson.JsonData jsonData)
        {
            List<Model.Shop.Products.AttributeInfo> list = new List<Model.Shop.Products.AttributeInfo>();
            if (!jsonData.IsArray || jsonData.Count < 1) return list;

            foreach (LitJson.JsonData item in jsonData)
            {
                Model.Shop.Products.AttributeInfo modelAtt = GetAttributeInfo4Obj(item);
                if (modelAtt != null)
                {
                    list.Add(modelAtt);
                }

                //list.Add(GetAttributeInfo4Obj(item));
            }
            return list;
        }

        private Model.Shop.Products.AttributeInfo GetAttributeInfo4Obj(LitJson.JsonData jsonData)
        {
            Model.Shop.Products.AttributeInfo attributeInfo = null;
            if (!jsonData.IsObject) return null;

            attributeInfo = new Model.Shop.Products.AttributeInfo();

            //Base Info
            attributeInfo.AttributeId = Globals.SafeInt(jsonData["AttributeId"].ToString(), -1);
            string attributeModeStr = jsonData["AttributeMode"].ToString();
            ProductAttributeModel attributeModel =
                (ProductAttributeModel)Enum.Parse(typeof(ProductAttributeModel), attributeModeStr);
            attributeInfo.UsageMode = (int)attributeModel;
            switch (attributeModel)
            {
                //单选
                case ProductAttributeModel.One:
                    attributeInfo.AttributeValues.Add(new AttributeValue
                                                          {
                                                              AttributeId = attributeInfo.AttributeId,
                                                              ValueId =
                                                                  Globals.SafeInt(jsonData["ValueItem"].ToString(), -1)
                                                          });
                    break;

                //多选
                case ProductAttributeModel.Any:
                    foreach (LitJson.JsonData item in jsonData["ValueItem"])
                    {
                        attributeInfo.AttributeValues.Add(new AttributeValue
                        {
                            AttributeId = attributeInfo.AttributeId,
                            ValueId = Globals.SafeInt(item.ToString(), -1)
                        });
                    }
                    break;

                //自定义
                case ProductAttributeModel.Input:
                    attributeInfo.AttributeValues.Add(new AttributeValue
                                                          {
                                                              AttributeId = attributeInfo.AttributeId,
                                                              ValueStr = jsonData["ValueItem"].ToString()
                                                          });
                    break;
                default:
                    break;
            }
            return attributeInfo;
        }

        #endregion Attribute

        #region SKU

        private List<Model.Shop.Products.SKUInfo> GetSKUInfo4Json(LitJson.JsonData jsonData)
        {
            List<Model.Shop.Products.SKUInfo> list = new List<Model.Shop.Products.SKUInfo>();
            if (jsonData.IsArray && jsonData.Count > 0)
            {
                //开启SKU时
                foreach (LitJson.JsonData item in jsonData)
                {
                    list.Add(GetSKUInfo4Obj(item));
                }
            }
            else if (jsonData.IsObject)
            {
                //关闭SKU时
                list.Add(GetSKUInfo4Obj(jsonData));
            }
            return list;
        }

        private Model.Shop.Products.SKUInfo GetSKUInfo4Obj(LitJson.JsonData jsonData)
        {
            Model.Shop.Products.SKUInfo skuInfo = null;
            if (!jsonData.IsObject) return null;

            skuInfo = new Model.Shop.Products.SKUInfo();

            //Base Info
            skuInfo.SKU = jsonData["SKU"].ToString();

            //CostPrice 允许为空
            string tmpCostPrice = jsonData["CostPrice"].ToString();
            if (!string.IsNullOrWhiteSpace(tmpCostPrice))
            {
                skuInfo.CostPrice = Globals.SafeDecimal(tmpCostPrice, decimal.MinusOne);
            }
            skuInfo.SalePrice = Globals.SafeDecimal(jsonData["SalePrice"].ToString(), decimal.MinusOne);
            salePriceList.Add(skuInfo.SalePrice);
            skuInfo.Stock = Globals.SafeInt(jsonData["Stock"].ToString(), -1);
            skuInfo.AlertStock = Globals.SafeInt(jsonData["AlertStock"].ToString(), -1);
            skuInfo.Weight = Globals.SafeInt(jsonData["Weight"].ToString(), -1);
            skuInfo.Upselling = Globals.SafeBool(jsonData["Upselling"].ToString(), false);

            //SKU Info
            if (jsonData["SKUItems"].IsArray && jsonData["SKUItems"].Count > 0)
            {
                foreach (LitJson.JsonData item in jsonData["SKUItems"])
                {
                    string skuImagepath = string.Empty;
                    if (!string.IsNullOrWhiteSpace(item["ImageUrl"].ToString()))
                    {
                        skuImagepath = item["ImageUrl"].ToString().Replace(tempFile, skuImageFile);
                        string BaseImage = item["ImageUrl"].ToString().Replace(tempFile, "");
                        if (!skuImageList.Contains(String.Format(BaseImage, "T32X32_")))
                        {
                            skuImageList.Add(String.Format(BaseImage, "T32X32_"));
                            skuImageList.Add(String.Format(BaseImage, "T130X130_"));
                            skuImageList.Add(String.Format(BaseImage, "T300X390_"));
                            skuImageList.Add(String.Format(BaseImage, "T350X350_"));
                        }
                    }
                    skuInfo.SkuItems.Add(
                            new Model.Shop.Products.SKUItem
                            {
                                AttributeId = Globals.SafeLong(item["AttributeId"].ToString(), -1),
                                ValueId = Globals.SafeLong(item["ValueId"].ToString(), -1),
                                ImageUrl = skuImagepath,
                                ValueStr = item["ValueStr"].ToString()
                            }
                        );
                }
            }
            return skuInfo;
        }

        #endregion SKU

        #endregion Json处理
    }
}