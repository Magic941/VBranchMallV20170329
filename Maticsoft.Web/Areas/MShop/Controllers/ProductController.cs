using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Model.Shop.Products.Lucene;
using Maticsoft.Json;
using System.Data;
namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class ProductController : Maticsoft.Web.Areas.Shop.Controllers.ProductController
    {
        private int _basePageSize = 2;
        private int _waterfallSize = 2;
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";
        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";
        private Maticsoft.BLL.Shop.Products.GoodsType GoodsBll = new BLL.Shop.Products.GoodsType();
        private Maticsoft.BLL.Shop.PromoteSales.WeiXinGroupBuy WeixinGroupBuyBll = new BLL.Shop.PromoteSales.WeiXinGroupBuy();
        Maticsoft.BLL.Shop.Products.ProductCategories productcatebll = new BLL.Shop.Products.ProductCategories();
        Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();

        //
        // GET: /Mobile/Product/

        #region  商品列表
        public ActionResult IndexOld(int cid = 0, int brandid = 0, string attrvalues = "0", string mod = "hot", string price = "", int activetype = 0,
                                  int? pageIndex = 1, int pageSize = 16,
                                  string viewName = "ProductAll_Image", string ajaxViewName = "_ProductListAll_Image")
        {
            ViewBag.IsOpenSku = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_AddProduct_OpenSku");

            ProductListModel model = new ProductListModel();
            //model.CategoryList = categoryManage.MainCategoryList(null);
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;
            ViewBag.ParentId = 0;
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
                    if (categoryInfo.ParentCategoryId != 0)
                    {
                        ViewBag.parentCateName = categoryManage.GetFullNameByCache(categoryInfo.ParentCategoryId);
                    }
                    ViewBag.ParentId = categoryInfo.ParentCategoryId;
                }


            }
            if (brandid > 0)//有品牌过来
            {
                Model.Shop.Products.BrandInfo brandInfo = brandBll.GetModelByCache(brandid);
                ViewBag.BrandName = brandInfo.BrandName;
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

            // int pageSize =15;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            int toalCount = productManage.GetProductsCountEx(cid, brandid, attrvalues, price);
            ViewBag.totalCount = toalCount.ToString();
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;

            ViewBag.ActiveType = (int)ActiveTypeEnum.NoActive;

            List<Model.Shop.Products.ProductInfo> list;
            list = productManage.GetProductsListEx(cid, brandid, attrvalues, price, mod, startIndex, endIndex);
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetCategorySetting(categoryInfo);
            //pageSetting.Replace(
            //    new[] { PageSetting.RKEY_CNAME, model.CurrentCateName }, 
            //    new[] { PageSetting.RKEY_CID, model.CurrentCid.ToString() }); //分类名称
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            ViewBag.ActiveType = (int)ActiveTypeEnum.WeiXin;

            //获取总条数
            if (toalCount < 1 || list == null || list.Count == 0) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = list.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }

        /// <summary>
        /// 瀑布流，商品列表
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="brandid"></param>
        /// <param name="keyword"></param>
        /// <param name="mod"></param>
        /// <param name="price"></param>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult ListWaterfallProductAll(int cid, int brandid, string attrvalues = "0", string mod = "hot", string price = "", int activetype = 0, int? pageIndex = 1, int pageSize = 16, string viewName = "_ProductListAll_Image")
        {
            ViewBag.BasePageSize = _basePageSize;

            attrvalues = Maticsoft.Common.InjectionFilter.SqlFilter(attrvalues);

            var mode2 = ProductIndexEnum.EnumSearchSortType.Default;

            switch (mod)
            {
                case "default":
                    mode2 = ProductIndexEnum.EnumSearchSortType.Default;
                    break;
                case "hot":
                    mode2 = ProductIndexEnum.EnumSearchSortType.SaleCountDown;
                    break;
                case "new":
                    mode2 = ProductIndexEnum.EnumSearchSortType.AddedDateDown;
                    break;
                case "price":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceUp;
                    break;
                case "pricedesc":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceDown;
                    break;
                default:
                    mod = null;
                    break;
            }
            int type = (int)ProductRecType.Share; //表示分享商品

            string path = "";
            //获取分类列表页path
            Maticsoft.Model.Shop.Products.GoodsType GoodsModel = GoodsBll.GetModel(cid);
            if (GoodsModel != null)
            {
                path = GoodsModel.Path;
            }

            pageSize = 16;//4
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex > 1 ? startIndex + pageSize - 1 : pageSize;

            List<Model.Shop.Products.ProductInfo> list;
            list = productManage.GetProductsListEx(cid, brandid, attrvalues, price, mod, startIndex, endIndex);
            //没有取到数据把数据置为空

            JsonObject json = new JsonObject();


            if (list != null && list.Count > 0)
            {
                JsonArray data = new JsonArray();
                list.ForEach(
                    info =>
                        data.Add(new JsonObject(new string[] { "ProductId", "ThumbnailUrl1", "ProductName", "LowestSalePrice", "MarketPrice", "SaleCounts", "activeid", "activetype" },
                            new object[] { info.ProductId, Maticsoft.Web.Components.FileHelper.GeThumbImage(info.ThumbnailUrl1, "T350X350_"), info.ProductName, info.LowestSalePrice.ToString("F"), info.MarketPrice.Value.ToString("F"), info.SaleCounts + info.FalseSaleCount, 0, activetype })));
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }

            return Content(json.ToString());


        }

        #endregion

        #region  分享商品列表
        public override ActionResult Index(int cid = 0, int brandid = 0, string attrvalues = "0", string mod = "hot", string price = "",
                                  int? pageIndex = 1, int pageSize = 16,
                                  string viewName = "Product_Image", string ajaxViewName = "_ProductList_Image")
        {
            ViewBag.IsOpenSku = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_AddProduct_OpenSku");

            ProductListModel model = new ProductListModel();
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            else
            {
                ViewBag.Phone = "";
            }

            ViewBag.ParentId = 0;
            model.CurrentCateName = "全部";
            if (cid > 0)
            {
                BLL.Shop.Products.GoodsType goodstypebll = new BLL.Shop.Products.GoodsType();
                Maticsoft.Model.Shop.Products.GoodsType goodtypeinfo = goodstypebll.GetModel(cid);
                if (goodtypeinfo != null)
                {
                    model.CurrentCateName = goodtypeinfo.GoodTypeName;
                }
            }

            model.CurrentCid = cid;
            model.CurrentMod = mod;

            string path = "";
            //获取分类列表页path
            Maticsoft.Model.Shop.Products.GoodsType GoodsModel = GoodsBll.GetModel(cid);
            if (GoodsModel != null)
            {
                path = GoodsModel.Path;
            }
            #region 轮播图
            string strwhere = " GoodTypeID=" + cid;
            BLL.Shop.Products.GoodsType goodsBll = new BLL.Shop.Products.GoodsType();
            List<Model.Shop.Products.GoodsType> goodsModel = goodsBll.GetModelList(strwhere);

            ViewBag.bannerImag = goodsModel;
            //Maticsoft.BLL.Settings.Advertisement bll = new Advertisement();
            //List<Maticsoft.Model.Settings.Advertisement> list = bll.GetListByAidCache(id);
            //return PartialView(ViewName, list);

            #endregion

            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            ViewBag.DataParam = dataParam;
            #endregion

            int type = (int)ProductRecType.Share; //表示分享商品
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            int toalCount = productManage.GetProductsCountExShareB(cid, brandid, attrvalues, price, type, path);
            ViewBag.totalCount = toalCount.ToString();
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;
            ViewBag.ActiveType = (int)ActiveTypeEnum.WeiXin;

            List<Model.Shop.Products.ProductInfo> list;

            list = productManage.GetProductsListExShareB(cid, brandid, attrvalues, price, mod, type, startIndex, endIndex, path);

            //获取总条数
            if (toalCount < 1 || list == null || list.Count == 0) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = list.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }
        #endregion

        #region 设置分享商品(Liyongqin)
        public ActionResult ProductIndexNew(int Gtype = 0, int GoodTypeID = 0, string attrvalues = "0", string mod = "hot", string price = "",
                                   int? pageIndex = 1, int pageSize = 16,
                                   string viewName = "Product_ImageNew", string ajaxViewName = "_ProductList_Imagenew")
        {
            ViewBag.IsOpenSku = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_AddProduct_OpenSku");

            ProductListModel model = new ProductListModel();
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            else
            {
                ViewBag.Phone = "";
            }

            ViewBag.ParentId = 0;
            model.CurrentCateName = "全部";
            #region

            if (Gtype > 0)
            {
                string Gwhere = "type=" + Gtype + " And GoodTypeID=" + GoodTypeID;
                BLL.Shop.Products.ProductStationMode PBll = new BLL.Shop.Products.ProductStationMode();
                List<Maticsoft.Model.Shop.Products.ProductStationMode> pmodel = PBll.GetList2(Gwhere);
                foreach (var item in pmodel)
                {
                    GoodTypeID = item.GoodTypeID;
                }
            }

            ViewBag.bannerImag = null;
            if (GoodTypeID > 0)
            {
                BLL.Shop.Products.GoodsType goodstypebll = new BLL.Shop.Products.GoodsType();
                Maticsoft.Model.Shop.Products.GoodsType goodtypeinfo = goodstypebll.GetModel(GoodTypeID);
                if (goodtypeinfo != null)
                {
                    model.CurrentCateName = goodtypeinfo.GoodTypeName;
                    ViewBag.bannerImag = goodtypeinfo;
                }
            }

            model.CurrentCid = Gtype;
            model.CurrentMod = mod;

            string path = "";
            //获取分类列表页path
            #endregion

            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            ViewBag.DataParam = dataParam;
            #endregion

            int type = (int)ProductRecType.Share; //表示分享商品
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            int toalCount = productManage.GetProductsCountExShareC(Gtype, GoodTypeID, attrvalues, price, type, path);
            ViewBag.totalCount = toalCount.ToString();
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;
            ViewBag.ActiveType = (int)ActiveTypeEnum.WeiXin;

            List<Model.Shop.Products.ProductInfo> list;

            list = productManage.GetProductsListExShareC(Gtype, GoodTypeID, attrvalues, price, mod, type, startIndex, endIndex, path);

            //获取总条数
            if (toalCount < 1 || list == null || list.Count == 0) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = list.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }


        #endregion

        /// <summary>
        /// 瀑布流，商品列表
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="brandid"></param>
        /// <param name="keyword"></param>
        /// <param name="mod"></param>
        /// <param name="price"></param>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult ListWaterfallNew(int cid, int brandid, string attrvalues = "0", string mod = "hot", string price = "", int? pageIndex = 1, int pageSize = 16, string viewName = "_ProductList_Image")
        {
            ViewBag.BasePageSize = _basePageSize;

            attrvalues = Maticsoft.Common.InjectionFilter.SqlFilter(attrvalues);

            var mode2 = ProductIndexEnum.EnumSearchSortType.Default;

            switch (mod)
            {
                case "default":
                    mode2 = ProductIndexEnum.EnumSearchSortType.Default;
                    break;
                case "hot":
                    mode2 = ProductIndexEnum.EnumSearchSortType.SaleCountDown;
                    break;
                case "new":
                    mode2 = ProductIndexEnum.EnumSearchSortType.AddedDateDown;
                    break;
                case "price":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceUp;
                    break;
                case "pricedesc":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceDown;
                    break;
                default:
                    mod = null;
                    break;
            }
            int type = (int)ProductRecType.Share; //表示分享商品

            string path = "";
            //获取分类列表页path
            Maticsoft.Model.Shop.Products.GoodsType GoodsModel = GoodsBll.GetModel(cid);
            if (GoodsModel != null)
            {
                path = GoodsModel.Path;
            }

            pageSize = 16;//4
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex > 1 ? startIndex + pageSize - 1 : pageSize;

            List<Model.Shop.Products.ProductInfo> list;
            list = productManage.GetProductsListExShareB(cid, brandid, attrvalues, price, mod, type, startIndex, endIndex, path);
            //没有取到数据把数据置为空

            JsonObject json = new JsonObject();


            if (list != null && list.Count > 0)
            {
                JsonArray data = new JsonArray();
                list.ForEach(
                    info =>
                        data.Add(new JsonObject(new string[] { "ProductId", "ThumbnailUrl1", "ProductName", "LowestSalePrice", "MarketPrice", "SaleCounts" },
                            new object[] { info.ProductId, Maticsoft.Web.Components.FileHelper.GeThumbImage(info.ThumbnailUrl1, "T350X350_"), info.ProductName, info.LowestSalePrice.ToString("F"), info.MarketPrice.Value.ToString("F"), info.SaleCounts })));
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }

            return Content(json.ToString());


        }

        /// <summary>
        /// 瀑布流，商品列表(liyongqin)
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="brandid"></param>
        /// <param name="keyword"></param>
        /// <param name="mod"></param>
        /// <param name="price"></param>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult ListWaterfallProductIndexNew(int Gtype, int GoodTypeID, string attrvalues = "0", string mod = "hot", string price = "", int? pageIndex = 1, int pageSize = 16, string viewName = "_ProductList_ImageNew")
        {
            ViewBag.BasePageSize = _basePageSize;

            attrvalues = Maticsoft.Common.InjectionFilter.SqlFilter(attrvalues);

            var mode2 = ProductIndexEnum.EnumSearchSortType.Default;

            switch (mod)
            {
                case "default":
                    mode2 = ProductIndexEnum.EnumSearchSortType.Default;
                    break;
                case "hot":
                    mode2 = ProductIndexEnum.EnumSearchSortType.SaleCountDown;
                    break;
                case "new":
                    mode2 = ProductIndexEnum.EnumSearchSortType.AddedDateDown;
                    break;
                case "price":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceUp;
                    break;
                case "pricedesc":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceDown;
                    break;
                default:
                    mod = null;
                    break;
            }
            int type = (int)ProductRecType.Share; //表示分享商品
            string path = "";
            pageSize = 16;//4
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex > 1 ? startIndex + pageSize - 1 : pageSize;

            List<Model.Shop.Products.ProductInfo> list;
            list = productManage.GetProductsListExShareC(Gtype, GoodTypeID, attrvalues, price, mod, type, startIndex, endIndex, path);
            //没有取到数据把数据置为空

            JsonObject json = new JsonObject();


            if (list != null && list.Count > 0)
            {
                JsonArray data = new JsonArray();
                list.ForEach(
                    info =>
                        data.Add(new JsonObject(new string[] { "ProductId", "ThumbnailUrl1", "ProductName", "LowestSalePrice", "MarketPrice", "SaleCounts" },
                            new object[] { info.ProductId, Maticsoft.Web.Components.FileHelper.GeThumbImage(info.ThumbnailUrl1, "T350X350_"), info.ProductName, info.LowestSalePrice.ToString("F"), info.MarketPrice.Value.ToString("F"), info.SaleCounts })));
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }

            return Content(json.ToString());


        }

        #region  商品分类
        public ActionResult CategoryList(int parentId = 0, string viewName = "CategoryListB")
        {
            ViewBag.Phone = "";
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            return View(viewName);
        }


        public ActionResult CategoryListOld(int parentId = 0, string viewName = "CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.Where(c => c.ParentCategoryId == parentId).OrderBy(c => c.DisplaySequence).ToList();

            Maticsoft.Model.Shop.Products.CategoryInfo parentInfo =
               cateList.FirstOrDefault(c => c.CategoryId == parentId);
            ViewBag.ParentId = -1;
            if (parentInfo != null)
            {
                ViewBag.CurrentName = parentInfo.Name;
                ViewBag.ParentId = parentInfo.ParentCategoryId;
            }
            ViewBag.Cid = parentId;
            ViewBag.Name = "全部分类";
            return View(viewName, categoryInfos);
        }
        public ActionResult CategoryListThree(int parentId = 0, string viewName = "CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.Where(c => c.ParentCategoryId == parentId).OrderBy(c => c.DisplaySequence).ToList();
            Maticsoft.Model.Shop.Products.CategoryInfo parentInfo =
                cateList.FirstOrDefault(c => c.CategoryId == parentId);

            ViewBag.ParentId = -1;
            if (parentInfo != null)
            {
                ViewBag.CurrentName = parentInfo.Name;
                ViewBag.ParentId = parentInfo.ParentCategoryId;
            }
            ViewBag.Cid = parentId;
            ViewBag.Name = "全部分类";
            return View(viewName, categoryInfos);
        }
        #endregion

        #region DetailNew 商品活动详细页面
        public ActionResult DetailNew(long ProductId = -1, int ActiveID = 0, int ActiveType = -1, string viewName = "DetailNew")
        {
            if (ActiveType == (int)ActiveTypeEnum.NoActive)
            {

            }
            if (ActiveType == (int)ActiveTypeEnum.WeiXin)
            {
                BLL.Shop.PromoteSales.WeiXinGroupBuy bll = new BLL.Shop.PromoteSales.WeiXinGroupBuy();
                Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinGroupBuyInfo = bll.GetModel(ActiveID);
                if (WeiXinGroupBuyInfo == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ActiveModel = WeiXinGroupBuyInfo;
                ProductId = WeiXinGroupBuyInfo.ProductId;
            }
            if (ActiveType == (int)ActiveTypeEnum.GroupBuy)
            {
                BLL.Shop.PromoteSales.GroupBuy bll = new BLL.Shop.PromoteSales.GroupBuy();
                Model.Shop.PromoteSales.GroupBuy WeiXinGroupBuyInfo = bll.GetModel(ActiveID);

                ViewBag.ActiveModel = WeiXinGroupBuyInfo;
                ProductId = WeiXinGroupBuyInfo.ProductId;

                return RedirectToAction("SalesGroupBuy", "ProSales", new { ProductId = ProductId, ActiveID = ActiveID, ActiveType = ActiveType, ViewName = "SalesGroupBuy" });
            }

            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetModel(ProductId);
            BLL.Shop.Products.ProductImage imageManage = new BLL.Shop.Products.ProductImage();
            model.ProductImages = imageManage.ProductImagesList(ProductId);
            model.ProductSkus = skuBll.GetProductSkuInfo(ProductId);

            BLL.Shop.Products.BrandInfo brandManage = new BLL.Shop.Products.BrandInfo();
            if (model.ProductInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ActiveType = ActiveTypeEnum.NoActive;
            ViewBag.ActiveModel = null;
            ViewBag.ActiveType = ActiveType;
            if (ActiveType == (int)ActiveTypeEnum.WeiXin)
            {
                BLL.Shop.PromoteSales.WeiXinGroupBuy bll = new BLL.Shop.PromoteSales.WeiXinGroupBuy();
                Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinGroupBuyInfo = bll.GetModel(ActiveID);
                ViewBag.ActiveModel = WeiXinGroupBuyInfo;
            }

            Model.Shop.Products.BrandInfo brandModel = brandManage.GetModelByCache(model.ProductInfo.BrandId);
            if (brandModel != null)
            {
                ViewBag.BrandName = brandModel.BrandName;
            }
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            else
            {
                ViewBag.Phone = "";
            }
            #region 分类导航

            BLL.Shop.Products.ProductCategories productCategoriesManage = new BLL.Shop.Products.ProductCategories();
            Model.Shop.Products.ProductCategories categoryModel = productCategoriesManage.GetModel(ProductId);
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

            ViewBag.ConsultCount = conBll.GetRecordCount("Status=1 and ProductId=" + ProductId);
            ViewBag.CommentCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + ProductId);//商品评论条数
            return View(viewName, model);
        }
        /// <summary>
        /// 商品活动类型
        /// </summary>
        public enum ActiveTypeEnum
        {
            /// <summary>
            /// 没有活动
            /// </summary>
            NoActive = -1,
            /// <summary>
            /// 限购
            /// </summary>
            CountDown = 0,
            /// <summary>
            /// 团购
            /// </summary>
            GroupBuy = 1,
            /// <summary>
            /// 预订
            /// </summary>
            Book = 2,
            /// <summary>
            /// 组合优惠套装
            /// </summary>
            Combination = 3,
            /// <summary>
            /// 微信
            /// </summary>
            WeiXin = 4


        }


        #endregion

        #region Detail
        public ActionResult Detail(int ProductId = -1, string viewName = "Detail")
        {
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetModel(ProductId);
            BLL.Shop.Products.ProductImage imageManage = new BLL.Shop.Products.ProductImage();
            model.ProductImages = imageManage.ProductImagesList(ProductId);
            model.ProductSkus = skuBll.GetProductSkuInfo(ProductId);


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
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            else
            {
                ViewBag.Phone = "";
            }
            #region 分类导航

            BLL.Shop.Products.ProductCategories productCategoriesManage = new BLL.Shop.Products.ProductCategories();
            Model.Shop.Products.ProductCategories categoryModel = productCategoriesManage.GetModel(ProductId);
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

            ViewBag.ConsultCount = conBll.GetRecordCount("Status=1 and ProductId=" + ProductId);
            ViewBag.CommentCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + ProductId);
            return View(viewName, model);
        }
        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ProductDesc(int id)
        {
            Maticsoft.Model.Shop.Products.ProductInfo productInfo = productManage.GetModel(id);
            if (productInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            #region SEO设置
            PageSetting pageSetting = PageSetting.GetProductSetting(productInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion SEO设置
            return View(productInfo);
        }

        #endregion

        #region 关联商品
        public override PartialViewResult ProductRelation(int id, int top = 12, string viewName = "_ProductRelation")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productManage.RelatedProductsList(id, top);
            return PartialView(viewName, productList);
        }
        #endregion

        #region 商品SKU规格选择

        public override ActionResult OptionSKU(long productId, string viewName = "_OptionSKU")
        {
            if (productId < 1) return new EmptyResult();
            ViewModel.Shop.ProductSKUModel productSKUModel = skuBll.GetProductSKUInfoByProductId(productId);
            //NO SKU ERROR
            if (productSKUModel == null) return new EmptyResult();
            //NO SKU ERROR
            if (productSKUModel.ListSKUInfos == null || productSKUModel.ListSKUInfos.Count < 1 ||
                productSKUModel.ListSKUItems == null) return new EmptyResult();

            ViewBag.HasSKU = true;

            //木有开启SKU的情况
            if (productSKUModel.ListSKUItems.Count == 0)
            {
                ViewBag.HasSKU = false;
                return View(viewName, productSKUModel);
            }

            ViewBag.SKUJson = SKUInfoToJson(productSKUModel.ListSKUInfos).ToString();

            return View(viewName, productSKUModel);
        }

        #endregion

        #region 商品评论
        public ActionResult Comments(int id, int pageIndex = 1, string viewName = "Comments")
        {
            int _pageSize = 15;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;
            ViewBag.ProductName = productManage.GetProductName(id);
            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int totalCount = 0;

            //获取总条数
            totalCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + id);
            ViewBag.TotalCount = totalCount;

            if (totalCount == 0)
            {
                return View(viewName);//NO DATA
            }
            List<Maticsoft.Model.Shop.Products.ProductReviews> productReviewses = reviewsBll.GetReviewsByPage(id, " CreatedDate desc", startIndex, endIndex);

            PagedList<Maticsoft.Model.Shop.Products.ProductReviews> lists = new PagedList<Maticsoft.Model.Shop.Products.ProductReviews>(productReviewses, pageIndex, _pageSize, totalCount);
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return View(viewName, lists);
            return View(viewName, lists);
        }
        #endregion

        //#region 商品咨询
        //public ActionResult Consults(int id, int pageIndex = 1, string viewName = "Consults")
        //{

        //    int _pageSize = 4;
        //    ViewBag.ProductName = productManage.GetProductName(id);
        //    //计算分页起始索引
        //    int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

        //    //计算分页结束索引
        //    int endIndex = pageIndex * _pageSize;
        //    int totalCount = 0;

        //    //获取总条数
        //    totalCount = conBll.GetRecordCount("Status=1 and ProductId=" + id);
        //    ViewBag.TotalCount = totalCount;
        //    if (totalCount == 0)
        //    {
        //        return PartialView(viewName);//NO DATA
        //    }
        //    List<Maticsoft.Model.Shop.Products.ProductConsults> productConsults = conBll.GetConsultationsByPage(id, " CreatedDate desc", startIndex, endIndex);

        //    PagedList<Maticsoft.Model.Shop.Products.ProductConsults> lists = new PagedList<Maticsoft.Model.Shop.Products.ProductConsults>(productConsults, pageIndex, _pageSize, totalCount);
        //    //检测Ajax请求, 进行无刷新分页
        //    if (Request.IsAjaxRequest())
        //        return PartialView(viewName, lists);
        //    return PartialView(viewName, lists);
        //}
        //#endregion

        #region 规格参数 详情页点击显示页面
        public ActionResult Consults(long ProductId, string viewName = "Consults")
        {
            if (ProductId < 1) return new EmptyResult();
            List<Maticsoft.Model.Shop.Products.AttributeInfo> model = attributeManage.GetAttributeInfoListByProductId(ProductId);
            return View(viewName, model);
        }
        #endregion

        #region 条件筛选
        public ActionResult Filter(int id = 0)
        {
            #region SEO 优化设置
            ViewBag.Title = "商品筛选";
            #endregion
            return View();
        }
        #endregion

        #region 商品扩展属性
        public override ActionResult OptionAttr(long productId, string viewName = "_OptionAttr")
        {
            if (productId < 1) return new EmptyResult();
            List<Maticsoft.Model.Shop.Products.AttributeInfo> model = attributeManage.GetAttributeInfoListByProductId(productId);
            return View(viewName, model);
        }
        #endregion

        #region 得到推荐商品数据
        public ActionResult ProductList(int Cid = 0, Maticsoft.Model.Shop.Products.ProductRecType RecType = ProductRecType.IndexRec, int pageIndex = 1, int pageSize = 15, string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            ProductListModel model = new ProductListModel();
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = cateList.FirstOrDefault(c => c.CategoryId == Cid);
            if (categoryInfo != null)
            {
                ViewBag.CategoryName = categoryInfo.Name;
            }
            ViewBag.RecType = RecType;
            int toalCount = productManage.GetProductRecCount(RecType, Cid);
            ViewBag.totalCount = toalCount.ToString();

            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productManage.GetProductRecList(RecType, Cid, -1);
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = productList.Skip(startIndex).Take(pageSize).ToList();
            //分页获取数据
            model.ProductPagedList = list.ToPagedList(
                pageIndex,
                pageSize,
                toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }
        #endregion

        #region 商品分类列表页
        public ActionResult ProductCategoryList(int PID, string viewName = "ProductCategoryList")
        {
            if (PID == 1)
            {
                ViewBag.Name = "服饰鞋包";
            }
            else if (PID == 2)
            {
                ViewBag.Name = "护肤美妆";
            }
            else if (PID == 3)
            {
                ViewBag.Name = "母婴玩具";
            }
            else if (PID == 4)
            {
                ViewBag.Name = "家居用品";
            }
            else if (PID == 5)
            {
                ViewBag.Name = "食品保健";
            }
            else if (PID == 6)
            {
                ViewBag.Name = "数码家电";
            }

            List<Maticsoft.Model.Shop.Products.GoodsType> GoodsModel = GoodsBll.GetModelList(" PID=" + PID);

            return View(viewName, GoodsModel);
        }
        #endregion

        #region  精品特卖
        /// <summary>
        /// 做成动态的
        /// </summary>
        /// <returns></returns>
        public ActionResult BoutiqueSale(int Type = 0, int Cid = 0, int Top = 0, string ViewName = "_BoutiqueSale")
        {
            // 0:推荐 1:热卖 2:特价 3:最新 4:分类首页推荐 7:主题馆1 8:主题馆2 9:主题馆3 10:主题馆 11:微商城首页活动
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList2(Type, Cid, Top);
            ViewBag.Imgurl = Type;
            return View(ViewName, productList);
        }
        #endregion

        #region  首页新版专栏
        public ActionResult BagSpecialColumn(int Type = 0, int Cid = 0, int Top = 0, string ViewName = "BagSpecialColumn")
        {
            // 0:推荐 1:热卖 2:特价 3:最新 7:主题馆1 8:主题馆2 9:主题馆3 10:主题馆4
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList2(Type, Cid, Top);
            ViewBag.Imgurl = Type;
            return View(ViewName, productList);

            // return View(ViewName);
        }


        #region  新品服装
        public ActionResult NewClothes(string viewname = "NewClothes")
        {
            return View(viewname);
        }
        #endregion

        #region  品质男女鞋
        public ActionResult NQualityShoes(string viewname = "NQualityShoes") 
        {
            return View(viewname);
        }

        #endregion

        #region  包包专卖
        public ActionResult NBagMonopoly(string viewname = "NBagMonopoly") 
        { 
         return View(viewname);
        }
        #endregion

        #region  母婴用品
        public ActionResult NMaternalSupplies(string viewname = "NMaternalSupplies")
        {
            return View(viewname);
        }
        #endregion

        #endregion


        #region 获取活动分类商品
        #region  分享商品列表
        public ActionResult ActiveProductIndex(int cid = 0, int brandid = 0, string attrvalues = "0", string mod = "hot", string price = "",
                                  int? pageIndex = 1, int pageSize = 16,
                                  string viewName = "ProductActive_Image", string ajaxViewName = "_ProductListActive_Image")
        {
            ViewBag.IsOpenSku = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_AddProduct_OpenSku");

            ProductListModel model = new ProductListModel();
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            else
            {
                ViewBag.Phone = "";
            }

            ViewBag.ParentId = 0;
            model.CurrentCateName = "全部";
            if (cid > 0)
            {
                BLL.Shop.Products.GoodsType goodstypebll = new BLL.Shop.Products.GoodsType();
                DataSet ds = goodstypebll.GetGoodsActiveTypeList("ID=" + cid);
                if (ds != null)
                {
                    model.CurrentCateName = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }

            model.CurrentCid = cid;
            model.CurrentMod = mod;

            string path = "";
            //获取分类列表页path
            Maticsoft.Model.Shop.Products.GoodsType GoodsModel = GoodsBll.GetModel(cid);
            if (GoodsModel != null)
            {
                path = GoodsModel.Path;
            }

            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            ViewBag.DataParam = dataParam;
            #endregion
            int type = (int)ProductRecType.Share; //表示分享商品
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            int toalCount = productManage.GetProductsCountExActiveB(cid, brandid, attrvalues, price, type);
            ViewBag.totalCount = toalCount.ToString();
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;

            List<Model.Shop.Products.ProductInfo> list;

            list = productManage.GetProductsListExActiveB(cid, brandid, attrvalues, price, mod, type, startIndex, endIndex);

            //获取总条数
            if (toalCount < 1 || list == null || list.Count == 0) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = list.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }
        #endregion

        /// <summary>
        /// 瀑布流，商品列表
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="brandid"></param>
        /// <param name="keyword"></param>
        /// <param name="mod"></param>
        /// <param name="price"></param>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult ListWaterfallActiveProduct(int cid, int brandid, string attrvalues = "0", string mod = "hot", string price = "", int? pageIndex = 1, int pageSize = 16, string viewName = "_ProductListActive_Image")
        {
            ViewBag.BasePageSize = _basePageSize;

            attrvalues = Maticsoft.Common.InjectionFilter.SqlFilter(attrvalues);

            var mode2 = ProductIndexEnum.EnumSearchSortType.Default;

            switch (mod)
            {
                case "default":
                    mode2 = ProductIndexEnum.EnumSearchSortType.Default;
                    break;
                case "hot":
                    mode2 = ProductIndexEnum.EnumSearchSortType.SaleCountDown;
                    break;
                case "new":
                    mode2 = ProductIndexEnum.EnumSearchSortType.AddedDateDown;
                    break;
                case "price":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceUp;
                    break;
                case "pricedesc":
                    mode2 = ProductIndexEnum.EnumSearchSortType.PriceDown;
                    break;
                default:
                    mod = null;
                    break;
            }
            int type = (int)ProductRecType.Share; //表示分享商品



            pageSize = 16;//4
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex > 1 ? startIndex + pageSize - 1 : pageSize;

            List<Model.Shop.Products.ProductInfo> list;
            list = productManage.GetProductsListExActiveB(cid, brandid, attrvalues, price, mod, type, startIndex, endIndex);
            //没有取到数据把数据置为空

            JsonObject json = new JsonObject();


            if (list != null && list.Count > 0)
            {
                JsonArray data = new JsonArray();
                list.ForEach(
                    info =>
                        data.Add(new JsonObject(new string[] { "ProductId", "ThumbnailUrl1", "ProductName", "LowestSalePrice", "MarketPrice", "SaleCounts" },
                            new object[] { info.ProductId, Maticsoft.Web.Components.FileHelper.GeThumbImage(info.ThumbnailUrl1, "T350X350_"), info.ProductName, info.LowestSalePrice.ToString("F"), info.MarketPrice.Value.ToString("F"), info.SaleCounts + info.FalseSaleCount })));
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }

            return Content(json.ToString());


        }

        #endregion


        #region  获取活动页面
        public ActionResult ShopActive(int Type = 0, int Cid = 0, string ViewName = "PhoneActive")
        {
         
            return View(ViewName);

         
        }
        public ActionResult ShopActiveProductList(int GoodTypeID, int BrandId = 0, int Floor = 0, int type = 0, int top = 0, string mod = "", string ViewName = "PhoneActiveList")
        {
            // 0:推荐 1:热卖 2:特价 3:最新 7:主题馆1 8:主题馆2 9:主题馆3 10:主题馆4
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductsActiveList( GoodTypeID,  BrandId , Floor = 0,  type ,  top , mod );
           
            return View(ViewName,productList);

           
        }
        #endregion
    }
}
