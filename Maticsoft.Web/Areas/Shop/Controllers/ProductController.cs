using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Shop.Order;
using Maticsoft.BLL.Shop.PromoteSales;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Json;
using Maticsoft.Json.Conversion;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using System.Linq;
using CategoryInfo = Maticsoft.Model.Shop.Products.CategoryInfo;
using System.Web.Script.Serialization;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class ProductController : ShopControllerBase
    {
        #region 全局变量

        protected BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        protected BLL.Shop.Products.CategoryInfo categoryManage = new BLL.Shop.Products.CategoryInfo();
        protected BLL.Shop.Order.OrderItems itemBll = new OrderItems();
        protected BLL.Shop.Products.SKUInfo skuBll = new BLL.Shop.Products.SKUInfo();
        protected BLL.Shop.Products.BrandInfo brandBll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        protected BLL.Shop.Supplier.SupplierInfo supplierBll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
        protected BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
        protected BLL.Shop.Products.ProductConsults conBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        protected readonly BLL.Shop.Products.AttributeInfo attributeManage = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected int _basePageSize = 30;
        protected int _waterfallSize = 32;
        protected int _waterfallDataCount = 1;

        #endregion

        public ProductController()
        {
            this._basePageSize = FallInitDataSize;
            this._waterfallSize = FallDataSize;
        }

        #region Index
        public virtual ActionResult Index(int cid = 0, int brandid = 0, string attrvalues = "0", string mod = "default", string price = "",
                                  int? pageIndex = 1, int pageSize = 32,
                                  string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            ProductListModel model = new ProductListModel();
            //model.CategoryList = categoryManage.MainCategoryList(null);
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;
            if (cid > 0)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                categoryInfo = cateList.FirstOrDefault(c => c.CategoryId == cid);
                if (categoryInfo != null)
                {
                    var path_arr = categoryInfo.Path.Split('|');
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysPath =
                        cateList.Where(c => path_arr.Contains(c.CategoryId.ToString())).OrderBy(c => c.Depth)
                                .ToList();
                    model.CategoryPathList = categorysPath;
                    model.CurrentCateName = categoryInfo.Name;
                  
                }
            }
            model.CurrentCid = cid;
            model.CurrentMod = mod;
            // model.CurrentCateName = cname == "all" ? "全部" : cname;

            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            ViewBag.DataParam = dataParam;
            #endregion

            if (pageSize <= 0)
            {
                pageSize = _basePageSize + _waterfallSize; //默认值
            }
            else
            {
                _basePageSize = pageSize;
            }

            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = productManage.GetProductsCountEx(cid, brandid, attrvalues, price);
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;

            List<Model.Shop.Products.ProductInfo> list = productManage.GetProductsListEx(cid, brandid, attrvalues, price, mod, startIndex, endIndex);
            List<Maticsoft.Model.Shop.Products.ProductInfo> prolist = new List<Model.Shop.Products.ProductInfo>();
            #region 是否静态化
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopProductStatic");
            string basepath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.Shop);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (IsStatic == "1")
                    {
                        item.SeoUrl = PageSetting.GetProStaticUrl(Convert.ToInt32(item.ProductId.ToString())).Replace("//", "/");
                    }
                    else if (IsStatic == "2")
                    {
                        item.SeoUrl = basepath + "Product-" + item.ProductId + ".html";
                    }
                    else
                    {
                        item.SeoUrl = basepath + "Product/Detail/" + item.ProductId;
                    }
                    prolist.Add(item);
                }
            }
            #endregion

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetCategorySetting(categoryInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            //获取总条数
            if (toalCount < 1) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = prolist.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }

        public virtual ActionResult ListWaterfall(int cid, int brandid, string attrvalues, string mod, string price, int startIndex, string viewName = "_ListWaterfall")
        {
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDataCount - 1 : _waterfallDataCount;
            int toalCount = productManage.GetProductsCountEx(cid, brandid, attrvalues, price);

            //获取总条数 并加载数据
            List<Model.Shop.Products.ProductInfo> list;

            list = productManage.GetProductsListEx(cid, brandid, attrvalues, price, mod, startIndex, endIndex);

            List<Model.Shop.Products.ProductInfo> proList = new List<Model.Shop.Products.ProductInfo>();
            #region 是否静态化
            if (list != null)
            {
                string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopProductStatic");
                string basepath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.Shop);
                foreach (var item in list)
                {
                    if (IsStatic == "1")
                    {
                        item.SeoUrl = PageSetting.GetProStaticUrl(Convert.ToInt32(item.ProductId.ToString())).Replace("//", "/");
                    }
                    else if (IsStatic == "2")
                    {
                        item.SeoUrl = basepath + "Product-" + item.ProductId + ".html";
                    }
                    else
                    {
                        item.SeoUrl = basepath + "Product/Detail/" + item.ProductId;
                    }
                    proList.Add(item);
                }
            }
            #endregion

            if (toalCount < 1) return new EmptyResult();   //NO DATA

            return View(viewName, proList);
        }
        #endregion

        #region
        public virtual ActionResult GetDetailInfo(int id = -1, string viewName = "Detail")
        {
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetModel(id);
            model.ProductSkus = skuBll.GetProductSkuInfo(id);
            JsonObject json = new JsonObject();
            if (model.ProductInfo.MarketPrice.HasValue)
            {
                json.Put("markPrice", model.ProductInfo.MarketPrice.Value.ToString("F"));
                json.Put("salePrice", model.ProductSkus[0].SalePrice.ToString("F"));
            }
            else
            {
                json.Put("markPrice", "0.00");
                json.Put("salePrice", "0.00");
            }
            return Json(json);
        }
        #endregion

        #region Detail
        public virtual ActionResult Detail(int id = -1, string viewName = "Detail")
        {
            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.id = id;
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetModel(id);
            model.ProductInfo.SupplierName = supplierBll.GetModel(model.ProductInfo.SupplierId).Name;
            BLL.Shop.Products.ProductImage imageManage = new BLL.Shop.Products.ProductImage();
            model.ProductImages = imageManage.ProductImagesList(id);

            model.ProductSkus = skuBll.GetProductSkuInfo(id);


            BLL.Shop.Products.BrandInfo brandManage = new BLL.Shop.Products.BrandInfo();
            if (model.ProductInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Model.Shop.Products.BrandInfo brandModel = brandManage.GetModelByCache(model.ProductInfo.BrandId);
            if (brandModel != null)
            {
                ViewBag.BrandName = brandModel.BrandName;
            }

            #region 分类导航

            BLL.Shop.Products.ProductCategories productCategoriesManage = new BLL.Shop.Products.ProductCategories();
            Model.Shop.Products.ProductCategories categoryModel = productCategoriesManage.GetModel(id);
            if (categoryModel != null)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> AllCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                var currentModel = AllCateList.FirstOrDefault(c => c.CategoryId == categoryModel.CategoryId);
                if (currentModel != null)
                {
                    var cateIds = currentModel.Path.Split('|');
                    List<Model.Shop.Products.CategoryInfo> list =
                        AllCateList.Where(c => cateIds.Contains(c.CategoryId.ToString())).OrderBy(c => c.Depth).ToList();
                    System.Text.StringBuilder sbPath = new System.Text.StringBuilder();
                    System.Text.StringBuilder CategoryStr = new System.Text.StringBuilder();
                    if (list != null && list.Count > 0)
                    {
                        foreach (var categoryInfo in list)
                        {
                            CategoryStr.AppendFormat("<a href='/Product/" + categoryInfo.CategoryId + "'>{0}</a> > ", categoryInfo.Name);
                        }
                    }
                    ViewBag.Cid = categoryModel.CategoryId;
                    ViewBag.PathInfo = sbPath.ToString();
                    ViewBag.CategoryStr = CategoryStr.ToString();
                }
            }



            #endregion 分类导航

            #region SEO设置
            PageSetting pageSetting = PageSetting.GetProductSetting(model.ProductInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;

            #endregion SEO设置
            ViewBag.RegisterToggle = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式   
            ViewBag.ConsultCount = conBll.GetRecordCount("Status=1 and ProductId=" + id);
            ViewBag.CommentCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + id);

            ViewBag.HasArea = MvcApplication.HasArea(AreaRoute.MShop);//是否包含手机版
            model.ProductInfo.ImageUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl") + model.ProductInfo.ImageUrl;
            return View(viewName, model);
        }
        #endregion

        #region ProSaleDetail
        public virtual ActionResult ProSaleDetail(int id, string viewName = "ProSaleDetail")
        {
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetProSaleModel(id);

            if (model.ProductInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            BLL.Shop.Products.ProductImage imageManage = new BLL.Shop.Products.ProductImage();
            model.ProductImages = imageManage.ProductImagesList(model.ProductInfo.ProductId);
            model.ProductSkus = skuBll.GetProductSkuInfo(model.ProductInfo.ProductId);

            BLL.Shop.Products.BrandInfo brandManage = new BLL.Shop.Products.BrandInfo();

            Model.Shop.Products.BrandInfo brandModel = brandManage.GetModelByCache(model.ProductInfo.BrandId);
            if (brandModel != null)
            {
                ViewBag.BrandName = brandModel.BrandName;
            }

            #region 分类导航

            BLL.Shop.Products.ProductCategories productCategoriesManage = new BLL.Shop.Products.ProductCategories();
            Model.Shop.Products.ProductCategories categoryModel = productCategoriesManage.GetModel(model.ProductInfo.ProductId);
            if (categoryModel != null)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> AllCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                var currentModel = AllCateList.FirstOrDefault(c => c.CategoryId == categoryModel.CategoryId);
                if (currentModel != null)
                {
                    var cateIds = currentModel.Path.Split('|');
                    List<Model.Shop.Products.CategoryInfo> list =
                        AllCateList.Where(c => cateIds.Contains(c.CategoryId.ToString())).OrderBy(c => c.Depth).ToList();
                    System.Text.StringBuilder sbPath = new System.Text.StringBuilder();
                    System.Text.StringBuilder CategoryStr = new System.Text.StringBuilder();
                    if (list != null && list.Count > 0)
                    {
                        foreach (var categoryInfo in list)
                        {
                            CategoryStr.AppendFormat("<a href='/Product/" + categoryInfo.CategoryId + "'>{0}</a> > ", categoryInfo.Name);
                        }
                    }
                    ViewBag.Cid = categoryModel.CategoryId;
                    ViewBag.PathInfo = sbPath.ToString();
                    ViewBag.CategoryStr = CategoryStr.ToString();
                }
            }



            #endregion 分类导航

            #region SEO设置
            PageSetting pageSetting = PageSetting.GetProductSetting(model.ProductInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;

            #endregion SEO设置
            ViewBag.RegisterToggle = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式   
            ViewBag.ConsultCount = conBll.GetRecordCount("Status=1 and ProductId=" + model.ProductInfo.ProductId);
            ViewBag.CommentCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + model.ProductInfo.ProductId);

            ViewBag.DownProduct = productManage.GetShopCountDownProductTop(model.ProductInfo.ProductId);


            return View(viewName, model);
        }
        #endregion

        #region GroupBuyDetail
        public virtual ActionResult GroupBuyDetail(int id, string viewName = "GroupBuyDetail", int Groupbase = 0)
        {
            //登录较验
            if (currentUser == null)
                return RedirectToAction("Login", "Account", new { returnUrl = "/Product/GroupBuyDetail?id=" + id + "&Groupbase=" + Groupbase });

            ViewBag.Groupbase = Groupbase;
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetGroupBuyModel(id);
            model.ProductInfo.GroupBuy.GroupBuyImage = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl") + model.ProductInfo.GroupBuy.GroupBuyImage;

            if (model.ProductInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            BLL.Shop.Products.ProductImage imageManage = new BLL.Shop.Products.ProductImage();
            model.ProductImages = imageManage.ProductImagesList(model.ProductInfo.ProductId);
            model.ProductSkus = skuBll.GetProductSkuInfo(model.ProductInfo.ProductId);
            BLL.Shop.Products.BrandInfo brandManage = new BLL.Shop.Products.BrandInfo();

            Model.Shop.Products.BrandInfo brandModel = brandManage.GetModelByCache(model.ProductInfo.BrandId);
            if (brandModel != null)
            {
                ViewBag.BrandName = brandModel.BrandName;
            }

            Maticsoft.BLL.Shop.Card.UserCardLogic ul = new BLL.Shop.Card.UserCardLogic();

            if (model.ProductInfo.PromotionType == 1)
            {
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = "/Product/GroupBuyDetail?id=" + id + "&Groupbase=" + Groupbase + "&viewName=" + viewName });//去登录
                }
                else
                {
                    ViewBag.IsMembers = ul.CheckUserMember(CurrentUser.UserName);
                }

            }
            else
            {
                ViewBag.IsMembers = false;
            }


            #region 分类导航

            BLL.Shop.Products.ProductCategories productCategoriesManage = new BLL.Shop.Products.ProductCategories();
            Model.Shop.Products.ProductCategories categoryModel = productCategoriesManage.GetModel(model.ProductInfo.ProductId);
            if (categoryModel != null)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> AllCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                var currentModel = AllCateList.FirstOrDefault(c => c.CategoryId == categoryModel.CategoryId);
                if (currentModel != null)
                {
                    var cateIds = currentModel.Path.Split('|');
                    List<Model.Shop.Products.CategoryInfo> list =
                        AllCateList.Where(c => cateIds.Contains(c.CategoryId.ToString())).OrderBy(c => c.Depth).ToList();
                    System.Text.StringBuilder sbPath = new System.Text.StringBuilder();
                    System.Text.StringBuilder CategoryStr = new System.Text.StringBuilder();
                    if (list != null && list.Count > 0)
                    {
                        foreach (var categoryInfo in list)
                        {
                            CategoryStr.AppendFormat("<a href='/Product/" + categoryInfo.CategoryId + "'>{0}</a>  ", categoryInfo.Name);
                        }
                    }
                    ViewBag.Cid = categoryModel.CategoryId;
                    ViewBag.PathInfo = sbPath.ToString();
                    ViewBag.CategoryStr = CategoryStr.ToString();
                }
            }



            #endregion 分类导航

            #region SEO设置
            PageSetting pageSetting = PageSetting.GetProductSetting(model.ProductInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;

            #endregion SEO设置
            ViewBag.RegisterToggle = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式   
            ViewBag.ConsultCount = conBll.GetRecordCount("Status=1 and ProductId=" + model.ProductInfo.ProductId);
            ViewBag.CommentCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + model.ProductInfo.ProductId);
            return View(viewName, model);
        }

        public virtual ActionResult GroupBuyDetailHaoLi(int id, string viewName = "GroupBuyDetailHaoLi", int Groupbase = 0)
        {
            ViewBag.Groupbase = Groupbase;
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetGroupBuyModel(id);
            model.ProductInfo.GroupBuy.GroupBuyImage = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl") + model.ProductInfo.GroupBuy.GroupBuyImage;

            if (model.ProductInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            BLL.Shop.Products.ProductImage imageManage = new BLL.Shop.Products.ProductImage();
            model.ProductImages = imageManage.ProductImagesList(model.ProductInfo.ProductId);
            model.ProductSkus = skuBll.GetProductSkuInfo(model.ProductInfo.ProductId);
            BLL.Shop.Products.BrandInfo brandManage = new BLL.Shop.Products.BrandInfo();

            Model.Shop.Products.BrandInfo brandModel = brandManage.GetModelByCache(model.ProductInfo.BrandId);
            if (brandModel != null)
            {
                ViewBag.BrandName = brandModel.BrandName;
            }

            Maticsoft.BLL.Shop.Card.UserCardLogic ul = new BLL.Shop.Card.UserCardLogic();

            if (model.ProductInfo.PromotionType == 1)
            {
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = "/Product/GroupBuyDetailHaoLi?id=" + id + "&Groupbase=" + Groupbase });//去登录
                }
                else
                {
                    ViewBag.IsMembers = ul.CheckUserMember(CurrentUser.UserName);
                }

            }
            else
            {
                ViewBag.IsMembers = false;
            }


            #region 分类导航

            BLL.Shop.Products.ProductCategories productCategoriesManage = new BLL.Shop.Products.ProductCategories();
            Model.Shop.Products.ProductCategories categoryModel = productCategoriesManage.GetModel(model.ProductInfo.ProductId);
            if (categoryModel != null)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> AllCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                var currentModel = AllCateList.FirstOrDefault(c => c.CategoryId == categoryModel.CategoryId);
                if (currentModel != null)
                {
                    var cateIds = currentModel.Path.Split('|');
                    List<Model.Shop.Products.CategoryInfo> list =
                        AllCateList.Where(c => cateIds.Contains(c.CategoryId.ToString())).OrderBy(c => c.Depth).ToList();
                    System.Text.StringBuilder sbPath = new System.Text.StringBuilder();
                    System.Text.StringBuilder CategoryStr = new System.Text.StringBuilder();
                    if (list != null && list.Count > 0)
                    {
                        foreach (var categoryInfo in list)
                        {
                            CategoryStr.AppendFormat("<a href='/Product/" + categoryInfo.CategoryId + "'>{0}</a>  ", categoryInfo.Name);
                        }
                    }
                    ViewBag.Cid = categoryModel.CategoryId;
                    ViewBag.PathInfo = sbPath.ToString();
                    ViewBag.CategoryStr = CategoryStr.ToString();
                }
            }



            #endregion 分类导航

            #region SEO设置
            PageSetting pageSetting = PageSetting.GetProductSetting(model.ProductInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;

            #endregion SEO设置
            ViewBag.RegisterToggle = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式   
            ViewBag.ConsultCount = conBll.GetRecordCount("Status=1 and ProductId=" + model.ProductInfo.ProductId);
            ViewBag.CommentCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + model.ProductInfo.ProductId);
            return View(viewName, model);
        }


        //检测数量
        public virtual ActionResult CkeckCount(int GroupBuyId)
        {
            Maticsoft.BLL.Shop.PromoteSales.GroupBuy buyBll = new GroupBuy();
            Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = buyBll.GetModelByCache(GroupBuyId);
            if (buyModel != null && buyModel.BuyCount < buyModel.MaxCount)
            {
                return Content("Ok");
            }
            return Content("No");
        }
        #endregion

        #region 随机商品
        public PartialViewResult ProductRan(string viewName = "_ProductRan", int top = 10)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productManage.GetProductRanList(top);
            return PartialView(viewName, productList);
        }
        #endregion


        #region 关联商品
        public virtual PartialViewResult ProductRelation(int id, int top = 12, string viewName = "_ProductRelation")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productManage.RelatedProductsList(id, top);
            List<Maticsoft.Model.Shop.Products.ProductInfo> prolist = new List<Model.Shop.Products.ProductInfo>();
            if (productList != null)
            {
                #region 是否静态化
                string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopProductStatic");
                string basepath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.Shop);
                foreach (var item in productList)
                {
                    if (IsStatic == "1")
                    {
                        item.SeoUrl = PageSetting.GetProStaticUrl(Convert.ToInt32(item.ProductId.ToString())).Replace("//", "/");
                    }
                    else if (IsStatic == "2")
                    {
                        item.SeoUrl = basepath + "Product-" + item.ProductId + ".html";
                    }
                    else
                    {
                        item.SeoUrl = basepath + "Product/Detail/" + item.ProductId;
                    }
                    prolist.Add(item);
                }
                #endregion
            }
            return PartialView(viewName, prolist);
        }
        #endregion

        #region 销售记录
        public PartialViewResult SaleRecord(int id, int pageIndex = 1, string viewName = "_SaleRecord")
        {
            int _pageSize = 15;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = itemBll.GetSaleRecordCount(id);
            if (toalCount == 0)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.ViewModel.Shop.SaleRecord> saleRecords = itemBll.GetSaleRecordByPage(id, "", startIndex, endIndex);

            PagedList<Maticsoft.ViewModel.Shop.SaleRecord> lists = new PagedList<Maticsoft.ViewModel.Shop.SaleRecord>(saleRecords, pageIndex, _pageSize, toalCount);
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        #endregion

        #region 品牌
        public virtual PartialViewResult BrandList(int Cid = 0, int top = 10, string viewName = "_BrandList")
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> brandInfos = brandBll.GetBrandsByCateId(Cid, true, top);
            ViewBag.Cid = Cid;
            return PartialView(viewName, brandInfos);
        }
        #endregion

        #region 商品评论
        public PartialViewResult ProductComments(int id, int pageIndex = 1, string viewName = "_ProductComments")
        {
            int _pageSize = 15;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int totalCount = 0;

            //获取总条数
            totalCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + id);
            ViewBag.TotalCount = totalCount;
            if (totalCount == 0)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Shop.Products.ProductReviews> productReviewses = reviewsBll.GetReviewsByPage(id, " CreatedDate desc", startIndex, endIndex);

            PagedList<Maticsoft.Model.Shop.Products.ProductReviews> lists = new PagedList<Maticsoft.Model.Shop.Products.ProductReviews>(productReviewses, pageIndex, _pageSize, totalCount);
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        #endregion



        public PartialViewResult GroupBuyHistory(int id, int pageIndex = 1, string viewName = "_GroupBuyHistory")
        {
            Maticsoft.BLL.Shop.PromoteSales.GroupBuy buy = new GroupBuy();
            var list = buy.GetGroupbuyHistory(id);
            int _pageSize = 5;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int totalCount = 0;
            totalCount = list.Count;

            if (totalCount == 0)
            {
                return PartialView(viewName);//NO DATA
            }

            var x = buy.GetGroubuyHistoryByPage(id, startIndex, endIndex);

            PagedList<Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory> lists = new PagedList<Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory>(x, pageIndex, _pageSize, totalCount);

            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }

        #region 热卖推荐 按分类
        public ActionResult HotCommand(Maticsoft.Model.Shop.Products.ProductRecType Type = Maticsoft.Model.Shop.Products.ProductRecType.Recommend, int Cid = 0, int Top = 5)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(Type, Cid, Top);
            return View(productList);
        }
        #endregion

        #region 商品咨询
        public virtual PartialViewResult ProductConsult(int id, int pageIndex = 1, string viewName = "_ProductConsult")
        {

            int _pageSize = 15;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int totalCount = 0;

            //获取总条数
            totalCount = conBll.GetRecordCount("Status=1 and ProductId=" + id);
            ViewBag.TotalCount = totalCount;
            if (totalCount == 0)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Shop.Products.ProductConsults> productConsults = conBll.GetConsultationsByPage(id, " CreatedDate desc", startIndex, endIndex);

            PagedList<Maticsoft.Model.Shop.Products.ProductConsults> lists = new PagedList<Maticsoft.Model.Shop.Products.ProductConsults>(productConsults, pageIndex, _pageSize, totalCount);
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        #endregion

        #region 最近浏览
        public PartialViewResult ProductLastView(string viewName = "_ProductLastView")
        {
            return PartialView(viewName);
        }

        #endregion

        #region 列表属性
        /// <summary>
        /// 该分类下的属性
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="Top"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public virtual PartialViewResult AttrList(int cid, int Top = -1, string viewName = "_AttrList")
        {
            Maticsoft.BLL.Shop.Products.AttributeInfo attributeBll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
            List<Maticsoft.Model.Shop.Products.AttributeInfo> lists = attributeBll.GetAttributeListByCateID(cid, true);
            return PartialView(viewName, lists);
        }
        /// <summary>
        /// 该分类下的属性值
        /// </summary>
        /// <param name="AttrId"></param>
        /// <param name="Top"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public virtual PartialViewResult AttrValues(int AttrId, int Top = -1, string viewName = "_AttrValues")
        {
            Maticsoft.BLL.Shop.Products.AttributeValue valueBll = new Maticsoft.BLL.Shop.Products.AttributeValue();
            List<Maticsoft.Model.Shop.Products.AttributeValue> lists = valueBll.GetModelList(" AttributeId=" + AttrId);
            return PartialView(viewName, lists);
        }

        #endregion

        #region 商品分类
        public PartialViewResult ProductCategory(int Cid, string viewName = "_ProductCategory", int Top = -1)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo =
                cateList.FirstOrDefault(c => c.CategoryId == Cid);
            ViewBag.CateName = categoryInfo != null ? categoryInfo.Name : "全部分类";
            ViewBag.Cid = Cid;
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = new List<CategoryInfo>();
            if (categoryInfo != null && !categoryInfo.HasChildren)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo ParentcategoryInfo =
              cateList.FirstOrDefault(c => c.CategoryId == categoryInfo.ParentCategoryId);
                if (ParentcategoryInfo != null)
                {
                    ViewBag.CateName = ParentcategoryInfo.Name;
                    ViewBag.Cid = ParentcategoryInfo.CategoryId;
                }
                categoryInfos = cateList.Where(c => c.ParentCategoryId == categoryInfo.ParentCategoryId).ToList();
            }
            else
            {
                categoryInfos = cateList.Where(c => c.ParentCategoryId == Cid).ToList();
            }

            return PartialView(viewName, categoryInfos);
        }
        public PartialViewResult ProductCatgList(int Cid, string viewName = "_ProductCategory", int Top = -1)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            ViewBag.Cid = Cid;
            return PartialView(viewName, cateList);
        }
        #endregion

        #region  批发规则
        public PartialViewResult WholeSale(int ProductId, string viewName = "_WholeSale")
        {
            Maticsoft.ViewModel.Shop.SalesModel salesModel = new Maticsoft.ViewModel.Shop.SalesModel();
            if (currentUser != null)
            {
                Maticsoft.BLL.Shop.Sales.SalesRuleProduct ruleBll = new Maticsoft.BLL.Shop.Sales.SalesRuleProduct();
                salesModel = ruleBll.GetSalesRuleByCache(ProductId, currentUser.UserID);
            }
            return PartialView(viewName, salesModel);
        }
        #endregion

        #region 商品SKU规格选择
        public virtual ActionResult OptionSKU(long productId, string viewName = "_OptionSKU")
        {
            if (productId < 1) return new EmptyResult();
            ViewModel.Shop.ProductSKUModel productSKUModel = skuBll.GetProductSKUInfoByProductId(productId);
            //NO SKU ERROR
            if (productSKUModel == null) return new EmptyResult();
            //NO SKU ERROR
            if (productSKUModel.ListSKUInfos == null || productSKUModel.ListSKUInfos.Count < 1 ||
                productSKUModel.ListSKUItems == null) return new EmptyResult();
            ViewBag.HasSKU = true;
            ViewBag.HasStock = true;
            //木有开启SKU的情况
            if (productSKUModel.ListSKUItems.Count == 0)
            {
                ViewBag.HasSKU = false;
                //判断库存是否满足
                ViewBag.HasStock = true;
                //是否开启警戒库存判断
                bool isOpenAlertStock = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock");
                if (isOpenAlertStock &&
                    productSKUModel.ListSKUInfos[0].Stock <= productSKUModel.ListSKUInfos[0].AlertStock)
                {
                    ViewBag.HasStock = false;
                }

                if (productSKUModel.ListSKUInfos[0].Stock < 1)
                {
                    ViewBag.HasStock = false;
                }

                return View(viewName, productSKUModel);
            }
            ViewBag.SKUJson = SKUInfoToJson(productSKUModel.ListSKUInfos).ToString();
            return View(viewName, productSKUModel);
        }

        public virtual ActionResult GroupDetailOptionSKU(long productId, string viewName = "_GroupDetailOptionSKU")
        {
            if (productId < 1) return new EmptyResult();
            ViewModel.Shop.ProductSKUModel productSKUModel = skuBll.GetProductSKUInfoByProductId(productId);
            //NO SKU ERROR
            if (productSKUModel == null) return new EmptyResult();
            //NO SKU ERROR
            if (productSKUModel.ListSKUInfos == null || productSKUModel.ListSKUInfos.Count < 1 ||
                productSKUModel.ListSKUItems == null) return new EmptyResult();
            ViewBag.HasSKU = true;
            ViewBag.HasStock = true;
            //木有开启SKU的情况
            if (productSKUModel.ListSKUItems.Count == 0)
            {
                ViewBag.HasSKU = false;
                //判断库存是否满足
                ViewBag.HasStock = true;
                //是否开启警戒库存判断
                bool isOpenAlertStock = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock");
                if (isOpenAlertStock &&
                    productSKUModel.ListSKUInfos[0].Stock <= productSKUModel.ListSKUInfos[0].AlertStock)
                {
                    ViewBag.HasStock = false;
                }

                if (productSKUModel.ListSKUInfos[0].Stock < 1)
                {
                    ViewBag.HasStock = false;
                }

                return View(viewName, productSKUModel);
            }
            ViewBag.SKUJson = SKUInfoToJson(productSKUModel.ListSKUInfos).ToString();
            return View(viewName, productSKUModel);
        }

        protected Json.JsonObject SKUInfoToJson(List<Model.Shop.Products.SKUInfo> list)
        {
            if (list == null || list.Count < 1) return null;
            Json.JsonObject json = new Json.JsonObject();

            Json.JsonObject jsonSKU = new Json.JsonObject();
            long[] key;
            int index;
            foreach (Model.Shop.Products.SKUInfo item in list)
            {
                if (item.SkuItems == null || item.SkuItems.Count < 1) continue;

                //无库存SKU不提供给页面
                //是否开启警戒库存判断
                bool IsOpenAlertStock = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock");
                if (IsOpenAlertStock && item.Stock <= item.AlertStock)
                {
                    continue;
                }
                if (item.Stock < 1)
                    continue;

                //组合SKU 的 ValueId
                key = new long[item.SkuItems.Count];
                index = 0;
                item.SkuItems.ForEach(xx => key[index++] = xx.ValueId);
                jsonSKU.Accumulate(string.Join(",", key), new
                {
                    sku = item.SKU,
                    count = item.Stock,
                    price = item.SalePrice
                });
            }

            //获取最小/最大价格
            list.Sort((x, y) => x.SalePrice.CompareTo(y.SalePrice));
            json.Put("Default", new
            {
                minPrice = list[0].SalePrice,
                maxPrice = list[list.Count - 1].SalePrice
            });
            json.Put("SKUDATA", jsonSKU);
            return json;
        }

        [HttpPost]
        public virtual ActionResult GetSKUInfos(long productId)
        {
            if (productId < 1) return new EmptyResult();
            ViewModel.Shop.ProductSKUModel productSKUModel = skuBll.GetProductSKUInfoByProductId(productId);
            //NO SKU ERROR
            if (productSKUModel == null) return new EmptyResult();
            //NO SKU ERROR
            if (productSKUModel.ListSKUInfos == null || productSKUModel.ListSKUInfos.Count < 1 ||
                productSKUModel.ListSKUItems == null) return new EmptyResult();

            ViewBag.HasSKU = true;

            //木有开启SKU的情况  Ajax 返回空js停止处理
            if (productSKUModel.ListSKUItems.Count == 0) return new EmptyResult();

            //DONE: 生成JsonSKU数据结构
            return Content(SKUInfoToJson(productSKUModel.ListSKUInfos).ToString());
        }

        #endregion

        #region 商品扩展属性
        public virtual ActionResult OptionAttr(long productId, string viewName = "_OptionAttr")
        {
            if (productId < 1) return new EmptyResult();
            List<Maticsoft.Model.Shop.Products.AttributeInfo> model = attributeManage.GetAttributeInfoListByProductId(productId);
            return View(viewName, model);
        }
        #endregion

        #region 商品对比
        public virtual ActionResult Compare(int type = 0, string prodidlist = "", string viewName = "Compare")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "商品对比—" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            if (type > 0 && !String.IsNullOrWhiteSpace(prodidlist))
            {
                string[] ids = prodidlist.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length <= 0)
                {
                    return Content("");
                }
                long[] convertedItems = new long[ids.Length];
                for (int i = 0; i < ids.Length; i++)
                {
                    convertedItems[i] = Globals.SafeLong(ids[i], 0);
                    if (convertedItems[i] <= 0)
                    {
                        return Content("");
                    }
                }
                Dictionary<string, Model.Shop.Products.AttributeInfo> data = productManage.GetProdValueList(convertedItems);
                return View(viewName, data);
            }
            return View(viewName);
        }
        #endregion

        #region 组合
        /// <summary>
        /// 组合列表
        /// </summary>
        /// <param name="pid">组合id</param>
        /// <param name="type">type 1 配件  2组合优惠</param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult PromotionCombo(long pid, int type, string viewName = "_PromotionCombo")
        {
            List<ProductAccessorie> listModel = new List<ProductAccessorie>();
            List<Model.Shop.Products.ProductAccessorie> list = new BLL.Shop.Products.ProductAccessorie().GetModelList(pid, type);
            if (list == null || list.Count <= 0) return PartialView(viewName);
            List<Model.Shop.Products.SKUInfo> skulist;
            List<Model.Shop.Products.SKUInfo> skuSeoUrlList;
            ProductAccessorie model;
            foreach (var item in list)
            {
                skulist = skuBll.GetSKUListByAcceId(item.AccessoriesId, 0); //SKU列表
                if (skulist != null && skulist.Count >= 2)//每组商品要保证最少有两条数据
                {
                    skuSeoUrlList = new List<Model.Shop.Products.SKUInfo>();
                    #region 是否静态化
                    string IsStatic = BLL.SysManage.ConfigSystem.GetValueByCache("ShopProductStatic");
                    string basepath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(AreaRoute.Shop);
                    if (skulist != null)
                    {
                        foreach (var skuitem in skulist)
                        {
                            if (IsStatic == "1")
                            {
                                skuitem.SeoUrl = PageSetting.GetProStaticUrl(Convert.ToInt32(skuitem.ProductId.ToString())).Replace("//", "/");
                            }
                            else if (IsStatic == "2")
                            {
                                skuitem.SeoUrl = basepath + "Product-" + skuitem.ProductId + ".html";
                            }
                            else
                            {
                                skuitem.SeoUrl = basepath + "Product/Detail/" + skuitem.ProductId;
                            }
                            skuSeoUrlList.Add(skuitem);
                        }
                    }
                    #endregion
                    model = new ProductAccessorie();
                    model.ProductAccessorieInfo = item;
                    model.SkuInfo = skuSeoUrlList;
                    listModel.Add(model);
                }
            }
            return PartialView(viewName, listModel);
        }
        #endregion
    }
}
