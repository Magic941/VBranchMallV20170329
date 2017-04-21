using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;
using Webdiyer.WebControls.Mvc;
using System.Data;
using Maticsoft.Model.Shop.Supplier;
using Maticsoft.Json;
using Maticsoft.BLL.SysManage;
using ServiceStack.RedisCache;

namespace Maticsoft.Web.Areas.Supplier.Controllers
{
    public class ProductController : SupplierControllerBase
    {
        //
        // GET: /Shop/SPProduct/
        private Maticsoft.BLL.Shop.Supplier.SupplierCategories suppcateBll =
            new Maticsoft.BLL.Shop.Supplier.SupplierCategories();
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryManage = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();
        public ActionResult Index()
        {
            return View();
        }

        #region 商品分类

        #region 默认列表和搜索
        public ActionResult ProCategory(int id = 0, int pageIndex = 1, int pageSize = 10, string orderby = "displaySequence", string viewName = "ProCategory", string ajaxViewName = "_ProCategoryList")
        {
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;
            int endIndex = pageIndex * pageSize;
            //  StringBuilder sb=new StringBuilder();
            // sb.Append("Depth=1");以下解决方案为临时解决方案 效率比较低 有时间优化
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> supList;
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> supCateList;
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> OrderedList = new List<Maticsoft.Model.Shop.Supplier.SupplierCategories>();
            if (id == 0)//默认过来的
            {
                supList = suppcateBll.GetModelList(SupplierId);
                if (null == supList)
                {
                    supList = new List<Maticsoft.Model.Shop.Supplier.SupplierCategories>();
                }
                string str = string.Format("SupplierId={0}", SupplierId);
                supCateList = suppcateBll.GetAllModelList(str).OrderBy(o=>o.DisplaySequence).ToList();//GetListByPageEx("", "", startIndex, endIndex);
                List<Maticsoft.Model.Shop.Supplier.SupplierCategories> list = supCateList.Where(c => c.Depth == 1).ToList();
                List<Maticsoft.Model.Shop.Supplier.SupplierCategories> firstList = list.Skip(startIndex).Take(pageSize).ToList();
                foreach (var item in firstList)
                {
                    var secondList = supCateList.Where(c => c.ParentCategoryId == item.CategoryId).OrderBy(c=>c.DisplaySequence);
                    OrderedList.Add(item);
                    foreach (var sonitem in secondList)
                    {
                        OrderedList.Add(sonitem);
                    }
                }
            }
            else//搜索过来的
            {
                supList = suppcateBll.GetModelList(SupplierId, id);
                if (null == supList)
                {
                    supList = new List<Maticsoft.Model.Shop.Supplier.SupplierCategories>();
                }
                //supCateList = suppcateBll.GetListByPageEx("", "", startIndex, endIndex, id);
           var   firstList = supList.Where(c => c.Depth == 1).OrderBy(o => o.DisplaySequence).Skip(startIndex).Take(pageSize); 
                // var secondList = supList.Where(c => c.Depth != 1);
                foreach (var item in firstList)
                {
                    var secondList = supList.Where(c => c.ParentCategoryId == item.CategoryId).OrderBy(c => c.DisplaySequence);
                    OrderedList.Add(item);
                    foreach (var sonitem in secondList)
                    {
                        OrderedList.Add(sonitem);
                    }
                }
            }
            PagedList<Maticsoft.Model.Shop.Supplier.SupplierCategories> pagedList = new PagedList<Maticsoft.Model.Shop.Supplier.SupplierCategories>(OrderedList, pageIndex, pageSize, supList.Count);
           
            if (Request.IsAjaxRequest())
            {
                return PartialView(ajaxViewName, pagedList);
            }
            return View(viewName, pagedList);
        }
        #endregion

        #region 分类添加
        public ActionResult AddProCate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Depth=1 And SupplierId={0}",SupplierId);
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> proCateList = suppcateBll.GetModelList(-1,
                                                                                                          sb.ToString(),
                                                                                                          "DisplaySequence");
            return View(proCateList);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProCateSubmit()
        {
            Model.Shop.Supplier.SupplierCategories model = new Model.Shop.Supplier.SupplierCategories();
            model.Name = Request.Form["Name"];
            model.Meta_Description = Request.Form["Meta_Description"];
            model.CreatedUserId = CurrentUser.UserID;
            model.Meta_Keywords = Request.Form["Meta_Keywords"];
            model.Description = Request.Form["Description"];
            model.Meta_Title = Request.Form["Meta_Title"];
            model.ParentCategoryId = Globals.SafeInt(Request.Form["CategoryId"], 0);
            string imageUrlValue = Request.Form["ImageUrl"];
            if (suppcateBll.IsExisted(model.ParentCategoryId.Value, model.Name, SupplierId))
            {
                return Content("exit");
            }
            //待上传的图片名称
            string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
            string ImageFile = "/Upload/Supplier/Categories";
            ArrayList imageList = new ArrayList();
            if (!string.IsNullOrWhiteSpace(imageUrlValue))
            {
                string imageUrl = string.Format(imageUrlValue, "");
                imageList.Add(imageUrl.Replace(tempFile, ""));
                model.ImageUrl = imageUrl.Replace(tempFile, ImageFile);
            }
            else
            {
                model.ImageUrl = "/Content/themes/base/Shop/images/none.png";
            }
            model.HasChildren = false;
            model.SupplierId = SupplierId;
            if (suppcateBll.CreateCategory(model))
            {
                DataCache.DeleteCache("GetSuppCateList-CateList-" + SupplierId);
                if (!string.IsNullOrWhiteSpace(imageUrlValue))
                {
                    //将图片从临时文件夹移动到正式的文件夹下
                    FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);
                }
                return Content("ok");
                
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 分类编辑
        public ActionResult ProCateEdit(int id)
        {
            Maticsoft.Model.Shop.Supplier.SupplierCategories supplierCategories = suppcateBll.GetModelByCache(id);
            if (supplierCategories.Depth == 2)
            {
                int parentId = supplierCategories.ParentCategoryId.HasValue ? supplierCategories.ParentCategoryId.Value : 0;
                Maticsoft.Model.Shop.Supplier.SupplierCategories parentCate =
                    suppcateBll.GetModelByCache(parentId);
                ViewBag.parentName = parentCate.Name;
            }
            return View(supplierCategories);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProCateEditSubmit(int id)
        {
            Maticsoft.Model.Shop.Supplier.SupplierCategories model = suppcateBll.GetModel(id);
            if (null == model)
            {
                return Content("no");
            }
            int parentId = Globals.SafeInt(Request.Form["parentId"],0);
            string name = Request.Form["Name"];
            if (suppcateBll.IsExisted(parentId, name, SupplierId, id))
            {
                return Content("exits");
            }
            model.Name = Request.Form["Name"];
            model.Meta_Description = Request.Form["Meta_Description"];
            model.Meta_Keywords = Request.Form["Meta_Keywords"];
            model.Description = Request.Form["Description"];
            model.Meta_Title = Request.Form["Meta_Title"];
            string imageUrlValue = Request.Form["ImageUrl"];
            //待上传的图片名称
            string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
            string ImageFile = "/Upload/Supplier/Categories";
            ArrayList imageList = new ArrayList();
            if (!string.IsNullOrWhiteSpace(imageUrlValue))
            {
                string imageUrl = string.Format(imageUrlValue, "");
                imageList.Add(imageUrl.Replace(tempFile, ""));
                model.ImageUrl = imageUrl.Replace(tempFile, ImageFile);
            }
            //else
            //{
            //    model.ImageUrl = imageUrlValue;
            //}
            if (suppcateBll.Update(model))
            {
                DataCache.DeleteCache("SupplierCategoriesModel-" + model.CategoryId); 
                DataCache.DeleteCache("GetSuppCateList-CateList-" + SupplierId);
                if (!string.IsNullOrWhiteSpace(imageUrlValue))
                {
                    //将图片从临时文件夹移动到正式的文件夹下
                    FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);
                }
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 分类删除
        public ActionResult DeleteProCate(int id)
        {
            Model.Shop.Supplier.SupplierCategories model = suppcateBll.GetModel(id);
            if (model != null)
            {
                if (model.HasChildren)//  是否有子分类  
                {
                    return Content("hasChild");
                }
                if (suppcateBll.IsExistsProd(id))//是否有商品
                {
                    return Content("hasProduct");
                }
                if (suppcateBll.Delete(model))
                {
                    DataCache.DeleteCache("SupplierCategoriesModel-" + model.CategoryId);
                    DataCache.DeleteCache("GetSuppCateList-CateList-" + SupplierId);
                    if (!String.IsNullOrWhiteSpace(model.ImageUrl))
                    {
                        DeletePhysicalFile(model.ImageUrl);
                    }
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }

            return Content("no");
        }
        /// <summary>
        /// 删除物理文件
        /// </summary>
        private void DeletePhysicalFile(string path)
        {
            Maticsoft.Web.Components.FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop, path);
        }
        #endregion

        #region 保存顺序
        public ActionResult UpdateSeqNum()
        {
            string status = string.Empty;
            int CategoryId = Common.Globals.SafeInt(this.Request.Form["CategoryId"], 0);
            int UpdateValue = Common.Globals.SafeInt(this.Request.Form["UpdateValue"], 0);
            if (CategoryId == 0 || UpdateValue == 0)
            {
                status = "FAILED";
            }
            else
            {
                
                if (suppcateBll.UpdateSeqByCid(UpdateValue, CategoryId, SupplierId))
                {
                    DataCache.DeleteCache("GetSuppCateList-CateList-" + SupplierId);
                    status = "SUCCESS";
                }
                else
                {
                    status = "FAILED";
                }
            }
            return Content(status);
        }
        #endregion
        #endregion

        #region 添加修改商品


        //待上传的SKU图片名称
        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        private string skuImageFile = string.Format("/Upload/Shop/Images/ProductsSkuImages/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        private ArrayList skuImageList = new ArrayList();
        private ArrayList salePriceList = new ArrayList();

        [HttpGet]
        public ActionResult ProductAdd(string viewName = "ProductAdd")
        {
            //ProductImagesThumbSize
            System.Drawing.Size thumbSize = Maticsoft.Common.StringPlus.SplitToSize(
                BLL.SysManage.ConfigSystem.GetValueByCache(Model.Settings.SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                '|', Model.Settings.SettingConstant.ProductThumbSize.Width, Model.Settings.SettingConstant.ProductThumbSize.Height);
            ViewBag.ProductImagesThumbSize = thumbSize.Width + "," + thumbSize.Height;
            //string productCode = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ProductCodePre"); 
            //ViewBag.BaseSKU = string.Format("{0}-{1}-{2}", productCode, currentUser.UserID,DateTime.Now.ToString("HHssyyyymmMMfffdd"));
            ViewBag.hfIsOpenSku = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_AddProduct_OpenSku");
            ViewBag.SuppID = SupplierId;
            ViewModel.Shop.ProductModel model = new ViewModel.Shop.ProductModel();
            model.ProductInfo = new ProductInfo();
            model.ProductInfo.DisplaySequence = (productManage.MaxSequence() + 1);
            return View(viewName, model);
        }

        #region SubmitProduct

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitProduct(ViewModel.Shop.ProductModel model)
        {
            #region 初始值
            ViewBag.hfIsOpenSku = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_AddProduct_OpenSku");
            ViewBag.SuppID = SupplierId;
            #endregion

            #region viewName
            string viewName = string.Empty;
            if (model.ProductInfo.ProductId > 0)
            {
                viewName = "ProductModify";
            }
            else
            {
                viewName = "ProductAdd";
            }
            #endregion
            #region Get PageData

            #region 店铺分类

            int SuppCateId = model.ProductInfo.SuppCategoryId;// Globals.SafeInt(Request.Form["hidSuppCategoryId"], 0);//店铺分类id
            if (SuppCateId <= 0)
            {
                ModelState.AddModelError("Message", "请选择店铺分类！");
                return View(viewName, model);
            }
            Model.Shop.Supplier.SupplierCategories suppcateModel = suppcateBll.GetModelByCache(SuppCateId);//获取当前所选店铺分类的path(路径)
            string suppCatePath = "";
            if (suppcateModel != null)
            {
                suppCatePath = suppcateModel.Path;
            }
            else
            {
                ModelState.AddModelError("Message", "请店铺分类不存在，或已被删除，请重新选择！");
                return View(viewName, model);
            }

            #endregion

            //int displaySequence = (productManage.MaxSequence() + 1);
            //图片
            string splitProductImages = Request.Form["hfProductImages"];

            //属性
            string attributeInfoJson = Request.Form["hfCurrentAttributes"];

            //SKU
            string skuBaseJson = Request.Form["hfCurrentBaseProductSKUs"];
            string skuJson = Request.Form["hfCurrentProductSKUs"];
            bool hasSKU = false;

            //分类
            if (model.ProductInfo.CategoryId < 1)
            {
                ModelState.AddModelError("Message", "请选择产品分类！");
                return View(viewName, model);
            }
            if (model.ProductInfo.TypeId < 1)
            {
                ModelState.AddModelError("Message", "请选择商品类型！");
                return View(viewName, model);
            }
            if (String.IsNullOrWhiteSpace(model.ProductInfo.ProductName))
            {
                ModelState.AddModelError("Message", "请填写商品名称！");
                return View(viewName, model);
            }

            #endregion Get PageData

            #region Data Proc

            //CategoryId
            string[] productImages = new string[0];

            //商品状态
            int saleStatus = -1;    //未审核

            //简介信息去除换行符号处理
            if (!string.IsNullOrWhiteSpace(model.ProductInfo.ShortDescription))
            {
                model.ProductInfo.ShortDescription = Globals.HtmlEncodeForSpaceWrap(model.ProductInfo.ShortDescription);
            }

            //商品图片分割处理
            if (!string.IsNullOrWhiteSpace(splitProductImages))
            {
                productImages = splitProductImages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }

            #region Attribute
            List<Model.Shop.Products.AttributeInfo> attributeList = new List<Model.Shop.Products.AttributeInfo>();
            if (!string.IsNullOrWhiteSpace(attributeInfoJson) && attributeInfoJson.Length > 2)
            {
                attributeList = GetAttributeInfo4Json(LitJson.JsonMapper.ToObject(attributeInfoJson));
            }
            #endregion Attribute

            #region SKU

            //SKU
            if (string.IsNullOrWhiteSpace(skuBaseJson))
            {
                ModelState.AddModelError("Message", "基础SKU信息不存在,请检查已填数据是否正确!");
                return View(viewName, model);
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
                lowestSalePrice = Globals.SafeDecimal(model.ProductInfo.LowestSalePrice, -1);
            }
            #endregion SKU

            #endregion Data Proc

            #region Set ProductInfo

            Model.Shop.Products.ProductInfo productInfo = null;// model.ProductInfo;

            int status = -1;
            if (model.ProductInfo.ProductId > 0)
            {
                productInfo = productManage.GetModel(model.ProductInfo.ProductId);
                status = productInfo.SaleStatus;
                if (productInfo.SupplierId <= 0 || productInfo.SupplierId != SupplierId)
                {
                    ModelState.AddModelError("Message", "您没有权限编辑该商品！");
                    return View(viewName, model);
                }
                productInfo.BrandId = model.ProductInfo.BrandId;
                productInfo.ProductName = model.ProductInfo.ProductName;
                productInfo.TypeId = model.ProductInfo.TypeId;
                productInfo.ProductCode = model.ProductInfo.ProductCode;
                productInfo.DisplaySequence = model.ProductInfo.DisplaySequence;
                productInfo.Unit = model.ProductInfo.Unit;
                productInfo.RegionId = model.ProductInfo.RegionId;
                productInfo.MarketPrice = model.ProductInfo.MarketPrice;
                productInfo.Description = model.ProductInfo.Description;
                productInfo.ShortDescription = model.ProductInfo.ShortDescription;
                productInfo.CategoryId = model.ProductInfo.CategoryId;
                productInfo.Subhead = model.ProductInfo.Subhead;
            }
            else
            {
                productInfo = model.ProductInfo;
            }

            //产品分类
            productInfo.Product_Categories = new[] { string.Format("{0}_{1}", 
                productInfo.CategoryId,
                categoryManage.GetModelByCache(productInfo.CategoryId).Path) };

            #region 商家及店铺分类信息
            productInfo.SupplierId = SupplierId;
            productInfo.SuppCategoryId = SuppCateId;
            productInfo.SuppCategoryPath = suppCatePath;
            #endregion

            //基本信息
            productInfo.CategoryId = 0; //重置产品分类, 使用多对多关系表
            productInfo.LowestSalePrice = lowestSalePrice.Value;
            productInfo.AddedDate = DateTime.Now;
            productInfo.SaleStatus = saleStatus;

            if (productImages.Length == 0)
            {
                productInfo.ImageUrl =
                    productInfo.ThumbnailUrl1 = productInfo.ThumbnailUrl2 = "/Content/themes/base/Shop/images/none.png";
            }
            //待上传的图片名称
            string savePath = string.Format("/Upload/Shop/Images/Product/{0}/", DateTime.Now.ToString("yyyyMMdd"));
            string saveThumbsPath = "/Upload/Shop/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            for (int i = 0; i < productImages.Length; i++)
            {
                if (i == 0)
                {
                    //主图片
                    if (productImages[i].Contains(tempFile))
                    {
                        string imageUrl = string.Format(productImages[i], "");
                        string MainThumbnailUrl1 = productImages[i];
                        productInfo.ImageUrl = imageUrl.Replace(tempFile, savePath);
                        productInfo.ThumbnailUrl1 = MainThumbnailUrl1.Replace(tempFile, saveThumbsPath);
                        //BLL.Shop.Products.ProductImage.MoveImage(productImages[i], savePath, saveThumbsPath);
                        Maticsoft.BLL.Shop.Products.ProductImage.MoveImageForFtp(productImages[i], savePath, saveThumbsPath);
                    }
                }
                else
                {
                    if (productImages[i].Contains(tempFile))//新文件
                    {
                        string AttachImageUrl = string.Format(productImages[i], "");
                        string AttachThumbnailUrl1 = productImages[i];
                        productInfo.ProductImages.Add(
                            new ProductImage
                            {
                                ImageUrl = AttachImageUrl.Replace(tempFile, savePath),
                                ThumbnailUrl1 = AttachThumbnailUrl1.Replace(tempFile, saveThumbsPath),
                            }
                            );
                        //Maticsoft.BLL.Shop.Products.ProductImage.MoveImage(productImages[i], savePath, saveThumbsPath);
                        Maticsoft.BLL.Shop.Products.ProductImage.MoveImageForFtp(productImages[i], savePath, saveThumbsPath);
                    }
                    else//旧文件
                    {
                        //新增，原因修改商品信息时，已经上传的图片前会加上图片服务器的地址，导致图片路径出错，故用此方法去除服务器地址
                        string tempUrl = productImages[i].Substring(7);
                        tempUrl = tempUrl.Substring(tempUrl.IndexOf('/') + 1);

                        string tempFileSwap = "/Upload/Shop/Images/ProductThumbs/";
                        string savePathSwap = "/Upload/Shop/Images/Product/";
                        string AttachImageUrl = string.Format(tempUrl, "");
                        string AttachThumbnailUrl1 = tempUrl;

                        productInfo.ProductImages.Add(
                          new ProductImage
                          {
                              ImageUrl = AttachImageUrl.Replace(tempFileSwap, savePathSwap),
                              ThumbnailUrl1 = AttachThumbnailUrl1.Replace(tempFile, saveThumbsPath),
                          });
                    }
                }
            }
            //属性
            productInfo.AttributeInfos = attributeList;

            //SKU
            productInfo.HasSKU = hasSKU;
            productInfo.SkuInfos = skuList;

            #endregion Set ProductInfo

            long ProductId = 0;
            //编辑操作
            if (productInfo.ProductId > 0)
            {
                Maticsoft.BLL.Shop.Products.ProductManage.InsertOrUpdateIndex(status, (int)ProductSaleStatus.UnCheck, productInfo.ProductId);
                if (!BLL.Shop.Products.ProductManage.ModifySuppProduct(productInfo))
                {

                    ModelState.AddModelError("Message", "保存失败! 请重试.");
                    return View(viewName, model);
                }
            }
            else
            {
                if (!BLL.Shop.Products.ProductManage.AddSuppProduct(productInfo, out ProductId))
                {
                    ModelState.AddModelError("Message", "保存失败! 请重试.");
                    return View(viewName, model);
                }
            }
            //将图片从临时文件夹移动到正式的文件夹下
            if (skuImageList.Count > 0)
            {
                
                //FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(skuImageFile), skuImageList);
                foreach (var item in skuImageList)
                {
                    if (System.IO.File.Exists(Server.MapPath(tempFile + item)))
                    {

                        FileManager.MoveImageForFTP(tempFile + item, skuImageFile);
                    }

                }
            }
            string CacheKey = "ProductsModel-" + productInfo.ProductId.ToString();
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, productInfo);
            }

            //string CacheKey = "ProductsModel-" + productInfo.ProductId.ToString();
            //if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            //{
            //    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, productInfo);
            //}

            ModelState.AddModelError("Message", "OK");
            return View(viewName, model);
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
                    attributeInfo.AttributeValues.Add(new Model.Shop.Products.AttributeValue
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
                        attributeInfo.AttributeValues.Add(new Model.Shop.Products.AttributeValue
                        {
                            AttributeId = attributeInfo.AttributeId,
                            ValueId = Globals.SafeInt(item.ToString(), -1)
                        });
                    }
                    break;

                //自定义
                case ProductAttributeModel.Input:
                    attributeInfo.AttributeValues.Add(new Model.Shop.Products.AttributeValue
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
                        if (item["ImageUrl"].ToString().Contains(tempFile))
                        {
                            string BaseImage = item["ImageUrl"].ToString().Replace(tempFile, "");
                            if (!skuImageList.Contains(String.Format(BaseImage, "T32X32_")))
                            {
                                skuImageList.Add(String.Format(BaseImage, "T32X32_"));
                                skuImageList.Add(String.Format(BaseImage, "T130X130_"));
                                //skuImageList.Add(String.Format(BaseImage, "T300X390_"));
                                skuImageList.Add(String.Format(BaseImage, "T400X400_"));
                                skuImageList.Add(String.Format(BaseImage, "T350X350_"));
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(skuImagepath))
                    {
                        skuImagepath = skuImagepath.Substring(skuImagepath.IndexOf("/Upload"));
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
        #endregion

        //修改商品
        public ActionResult ProductModify(int pid = 0, string viewName = "ProductModify")
        {
            System.Drawing.Size thumbSize = StringPlus.SplitToSize(
                BLL.SysManage.ConfigSystem.GetValueByCache(Model.Settings.SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                '|', Model.Settings.SettingConstant.ProductThumbSize.Width, Model.Settings.SettingConstant.ProductThumbSize.Height);
            ViewBag.ProductImagesThumbSize = thumbSize.Width + "," + thumbSize.Height;
            ViewBag.hfIsOpenSku = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_AddProduct_OpenSku");
            ViewBag.SuppID = SupplierId;

            BLL.Shop.Products.ProductInfo manage = new BLL.Shop.Products.ProductInfo();
            Model.Shop.Products.ProductInfo info = manage.GetModel(pid);
            if (info == null) return null;
            if (info.SupplierId <= 0 || info.SupplierId != SupplierId) return null;
            ViewModel.Shop.ProductModel model = new ViewModel.Shop.ProductModel();
            model.ProductInfo = info;

            #region  商城分类
            BLL.Shop.Products.ProductCategories productCate = new BLL.Shop.Products.ProductCategories();
            Model.Shop.Products.ProductCategories prodcutCateModel = productCate.GetModel(info.ProductId);
            if (prodcutCateModel != null)
            {
                model.ProductInfo.CategoryId = prodcutCateModel.CategoryId;
            }
            #endregion

            #region 获取店铺分类信息
            Model.Shop.Supplier.SupplierCategories suppCateModel = suppcateBll.GetModelByProductId(info.ProductId);
            if (suppCateModel != null)
            {
                model.ProductInfo.SuppCategoryId = suppCateModel.CategoryId;
                model.ProductInfo.SuppCategoryPath = suppCateModel.Path;
                if (suppCateModel.ParentCategoryId.HasValue && suppCateModel.ParentCategoryId.Value > 0) //二级分类
                {
                    model.ProductInfo.SuppParentCategoryId = suppCateModel.ParentCategoryId.Value;
                }
            }
            #endregion
            BLL.Shop.Products.ProductInfo bll = new BLL.Shop.Products.ProductInfo();
            if (bll.GetFreightList(info.ProductCode, "").Tables[0].Rows.Count > 0)
            {
                ViewBag.Freight = bll.GetFreightList(info.ProductCode, "").Tables[0].Rows[0]["Freight"].ToString();
            }
            else
            {
                ViewBag.Freight = 0;
            }

            return View(viewName, model);
        }
        #endregion

        #region 商品列表
        public ActionResult InStock(int SaleStatus = -1)
        {
            switch (SaleStatus)
            {
                //出售中
                case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale:
                    ViewBag.strTitle = "您可以对出售中的商品进行编辑和下架操作";
                    break;
                //未审核
                case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.UnCheck:
                    ViewBag.strTitle = "您可以对未审核中的商品进行编辑操作";
                    break;
                case (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock:
                    ViewBag.strTitle = "您可以对仓库中的商品进行编辑和上架操作";
                    break;
            }
            ViewBag.SaleStatus = SaleStatus;
            ViewModel.Shop.ProductListModel model = new ViewModel.Shop.ProductListModel();
            model.SuppCategoryList = suppcateBll.GetModelList(SupplierId);
            model.CategoryList = categoryManage.GetModelList();
            return View("InStock", model);
        }

        /// <summary>
        /// 商品列表 ajax分页
        /// </summary>
        /// <param name="s">销售状态</param>
        /// <param name="cate">商品分类</param>
        /// <param name="suppcate">店铺商品分类</param>
        /// <param name="prodName">商品名称</param>
        /// <param name="code">商品编号</param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult ProdList(int s = -1, int cate = 0, int suppcate = 0, string prodName = "", string code = "", int p = 1, string viewName = "_ProdList")
        {
            ProductInfo model = new ProductInfo();
            model.SupplierId = SupplierId;
            model.SaleStatus = s;
            model.CategoryId = cate;
            model.SuppCategoryId = suppcate;
            model.ProductName = prodName;
            model.ProductCode = code;
            int _pageSize =5;

            //计算分页起始索引
            int startIndex = p > 1 ? (p - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = p * _pageSize;
            int toalCount;//获取总条数 
            List<ProductInfo> list = productManage.GetProdListByPage(model, startIndex, endIndex, out toalCount);
            if (list == null)
                return PartialView(viewName);
            PagedList<ProductInfo> lists = new PagedList<ProductInfo>(list, p, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }

        #region 批量上 下架 删除
        [HttpPost]
        public ActionResult Batch(FormCollection fm)
        {
            string action = fm["action"];
            int saleStatus = Globals.SafeInt(fm["saleStatus"], -10);
            string idlist = InjectionFilter.SqlFilter(fm["ids"]);
            string[] ids = idlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (ids.Length <= 0)
            {
                return Content("NULL");
            }
            long[] convertedItems = new long[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                convertedItems[i] = Globals.SafeLong(ids[i], 0);
                if (convertedItems[i] <= 0)
                {
                    return Content("NO");
                }
            }
            idlist = string.Join(",", convertedItems);
            if (saleStatus == -10 || String.IsNullOrWhiteSpace(idlist) || idlist.Trim().Length == 0) return Content("NO");
            ProductSaleStatus status;
            switch (action)
            {
                case "del"://删除
                    status = ProductSaleStatus.Deleted;
                    break;
                case "sale"://上下架
                    switch (saleStatus)
                    {
                        // 当前状态是上架
                        case (int)ProductSaleStatus.OnSale:
                            status = ProductSaleStatus.InStock;
                            break;
                        // 当前状态是下架  
                        case (int)ProductSaleStatus.InStock:
                            status = ProductSaleStatus.OnSale;
                            break;
                        default:
                            return Content("NO");
                    }
                    break;
                default:
                    return Content("NO");
            }
            foreach (string s in ids)
            {
                Maticsoft.BLL.Shop.Products.ProductManage.InsertOrUpdateIndex(saleStatus, (int)status, long.Parse(s));
            }
            productManage.UpdateList(idlist, status);
            return Content("OK");
        }
        #endregion 批量上 下架
        #endregion

        #region 商品推荐

        BLL.Shop.Supplier.SuppProductStatModes stationModeManage = new BLL.Shop.Supplier.SuppProductStatModes();

        public ActionResult ProductsStationMode(int categoryId = 0, int selectType = 0,
            string productName = null, int searchPageIndex = 1, int addPageIndex = 1, int pageSize = 10,
            string ajaxViewName = "_ProdStatMode", string viewName = "ProductsStationMode")
        {
            Maticsoft.ViewModel.Supplier.ProductsStationMode model = new ViewModel.Supplier.ProductsStationMode();

            //加载分类
            Maticsoft.BLL.Shop.Supplier.SupplierCategories bllCategoryInfo = new BLL.Shop.Supplier.SupplierCategories();
            List<SupplierCategories> categoryInfoList = bllCategoryInfo.GetModelList("  Depth = 1 and SupplierId="+SupplierId);
            if (categoryInfoList != null && categoryInfoList.Count > 0)
            {
                model.DrpProductCategory = new SelectList(categoryInfoList, "CategoryId", "Name");
            }

            //加载搜索数据
            BindSearchProduct(model, categoryId, selectType, productName, searchPageIndex, pageSize);

            //加载选择数据
            BindAddProduct(model, categoryId, selectType, productName, addPageIndex, pageSize);

            if (selectType == 0)
            {
                ViewBag.Msg = "需要推荐的商品";
            }
            if (selectType == 1)
            {
                ViewBag.Msg = "需要热卖的商品";
            }
            if (selectType == 2)
            {
                ViewBag.Msg = "需要特价的商品";
            }
            if (selectType == 3)
            {
                ViewBag.Msg = "最新商品推荐";
            }
            if (Request.IsAjaxRequest())
                return View(ajaxViewName, model);

            return View(viewName, model);
        }
        #region Ajax
        [HttpPost]
        public ActionResult ClearProductsStatMode(int categoryId = 0, int selectType = 0)
        {
            return Content(stationModeManage.DeleteByType(SupplierId, selectType, categoryId).ToString());
        }
        [HttpPost]
        public ActionResult AddProductStationMode(long productId, int type = 0)
        {
            JsonObject json = new JsonObject();
            if (SupplierId < 1 || productId < 1)
            {
                json.Put("STATUS", "ERROR");
                return Content(json.ToString());
            }

            if (stationModeManage.Exists(SupplierId, productId, type))
            {
                json.Put("STATUS", "Presence");
                return Content(json.ToString());
            }
            SuppProductStatModes productStationMode = new SuppProductStatModes();
            productStationMode.ProductId = productId;
            productStationMode.DisplaySequence = stationModeManage.GetMaxId() + 1;
            productStationMode.Type = type;
            productStationMode.SupplierId = SupplierId;

            if (stationModeManage.Add(productStationMode) > 0)
            {
                json.Put("STATUS", "SUCCESS");
                json.Put("DATA", "Approve");
            }
            else
            {
                json.Put("STATUS", "NODATA");
                return Content(json.ToString());
            }
            return Content(json.ToString());
        }

        [HttpPost]
        public ActionResult RemoveProductStationMode(long productId, int type = 0)
        {
            JsonObject json = new JsonObject();
            if (SupplierId < 1 || productId < 1)
            {
                json.Put("STATUS", "ERROR");
                return Content(json.ToString());
            }

            if (stationModeManage.Delete(SupplierId, productId, type))
            {
                json.Put("STATUS", "SUCCESS");
            }
            else
            {
                json.Put("STATUS", "NODATA");
                return Content(json.ToString());
            }
            return Content(json.ToString());
        }
        #endregion

        private void BindAddProduct(ViewModel.Supplier.ProductsStationMode model, int categoryId = 0,
            int selectType = 0, string productName = null, int pageIndex = 1, int pageSize = 10)
        {
            DataSet dsAddProduct = stationModeManage.GetStationMode(SupplierId, selectType, categoryId, productName);
            if (dsAddProduct != null && dsAddProduct.Tables[0].Rows.Count > 0)
            {
                StringBuilder strPIds = new StringBuilder();
                for (int i = 0; i < dsAddProduct.Tables[0].Rows.Count; i++)
                {
                    strPIds.Append(dsAddProduct.Tables[0].Rows[i]["ProductId"]);
                    strPIds.Append(",");
                }
                ViewBag.hfSelectedData = strPIds.ToString().TrimEnd(',');
            }
            else
            {
                ViewBag.hfSelectedData = "";
            }
            string selectedData = ViewBag.hfSelectedData;
            if (ViewBag.AddProdRecordCount == 0)
            {
                ViewBag.AddProdRecordCount = ViewBag.AddProdPageSize;
            }

            //如未选择数据, 执行清空操作
            if (string.IsNullOrWhiteSpace(selectedData))
            {
                model.AddedProductList = null;
                return;
            }

            //Check Data
            string[] pids = selectedData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (pids.Length < 0) return;

            //总数据 根据ProductId获取
            int recordCount = productManage.GetProductRecListCount(pids.Distinct().ToArray());
            if (recordCount < 1) return;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * pageSize;
            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductRecListByPage(pids.Distinct().ToArray(), startIndex, endIndex);
            if (prodList == null || prodList.Count < 1) return;
            model.AddedProductList = new PagedList<ProductInfo>(prodList, pageIndex, pageSize, recordCount);
        }

        private void BindSearchProduct(ViewModel.Supplier.ProductsStationMode model, int categoryId = 0, int selectType = 0, string productName = null, int pageIndex = 1, int pageSize = 10)
        {
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * pageSize;

            int totalCount = stationModeManage.GetProductNoRecCount(SupplierId, categoryId, productName, selectType);
            List<Model.Shop.Products.ProductInfo> prodList = new List<ProductInfo>();
            if (totalCount > 0)
            {
                prodList = stationModeManage.GetProductNoRecList(SupplierId, categoryId, productName, selectType, startIndex, endIndex);
            }

            //设置全选数据 供JavaScript使用
            if (prodList != null && prodList.Count > 0)
            {
                StringBuilder tmpSkuIds = new StringBuilder();
                prodList.ForEach(info =>
                {
                    tmpSkuIds.Append(info.ProductId);
                    tmpSkuIds.Append(",");
                });
                ViewBag.hfCurrentAllData = tmpSkuIds.ToString();
            }
            else
            {
                ViewBag.hfCurrentAllData = string.Empty;
            }

            if (prodList == null || prodList.Count < 1) return;
            model.SearchProductList = new PagedList<ProductInfo>(prodList, pageIndex, pageSize, totalCount);
        }

        #endregion

    }
}
