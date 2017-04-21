using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Services;
using Maticsoft.Model.Shop.Products.Lucene;
using Maticsoft.Json;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class SearchController : MShopControllerBase
    {
        //
        // GET: /Shop/Search/
        #region 全局变量

        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        private ProductSearchAPI searchapi = new ProductSearchAPI(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ProductSearchServerIP"));
        private  BLL.Shop.Products.BrandInfo brandBll=new Maticsoft.BLL.Shop.Products.BrandInfo();
        private int _basePageSize = 2;
        private int _waterfallSize = 2;
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";

        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";
        private BLL.CMS.Content contBll = new BLL.CMS.Content();
        private Maticsoft.BLL.Shop.Products.GoodsType GoodsBll = new BLL.Shop.Products.GoodsType();
        #endregion

        public SearchController()
        {
            this._basePageSize = 2;
            this._waterfallSize = 2;
        }
        public ActionResult IndexOld(int cid = 0, int brandid = 0, string keyword = "", string mod = "default", string price = "0-0",
                                        int? pageIndex = 1,
                                        string viewName = "IndexAll", string ajaxViewName = "_ProductListAll")
        {
            ProductListModel model = new ProductListModel();
            keyword = Maticsoft.Common.InjectionFilter.SqlFilter(keyword);
            if (String.IsNullOrWhiteSpace(keyword))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            //model.CategoryList = categoryManage.MainCategoryList(null);
            if (cid > 0)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo =
                    cateList.FirstOrDefault(c => c.CategoryId == cid);
                if (categoryInfo != null)
                {
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysPath =
                        cateList.Where(c => categoryInfo.Path.Contains(c.Path + "|") || c.Path == categoryInfo.Path)
                                .ToList();
                    model.CategoryPathList = categorysPath;
                }
            }
            model.CurrentCid = cid;
            model.CurrentMod = mod;
            //  model.CurrentCateName = cname == "all" ? "全部" : cname;
            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            ViewBag.DataParam = dataParam;
            #endregion

            int pageSize =16;//4
            // int pageSize =15;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            //int toalCount = productManage.GetSearchCountEx(cid, brandid, keyword, price);

            //改成远程接口搜索

            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = endIndex + pageIndex * pageSize;

            List<Model.Shop.Products.ProductInfoForProductIndex> list;
            //list = productManage.GetSearchListEx(cid, brandid, keyword, price, mod, startIndex, endIndex);
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

            var searchResult = searchapi.SearchInMall(keyword, mode2, price, (int)pageIndex, pageSize);

            int toalCount = searchResult.SearchCount;
            ViewBag.TotalCount = searchResult.SearchCount;
            list = searchResult.ProductsResult;


            #region SEO 优化设置


            IPageSetting pageSetting = PageSetting.GetPageSetting("Category", ApplicationKeyType.Shop);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, model.CurrentCateName }); //分类名称
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            //获取总条数
            if (toalCount < 1) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedListForProductIndex = list.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }

        public ActionResult Index(int cid = 0, int brandid = 0, string keyword = "", string mod = "default", string price = "0-0",
                                 int? pageIndex = 1,
                                 string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            ProductListModel model = new ProductListModel();
            keyword = Maticsoft.Common.InjectionFilter.SqlFilter(keyword);
            if (String.IsNullOrWhiteSpace(keyword))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            if (cid > 0) {
                //List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                //Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo =
                //    cateList.FirstOrDefault(c => c.CategoryId == cid);
                //if (categoryInfo != null)
                //{
                //    List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysPath =
                //        cateList.Where(c => categoryInfo.Path.Contains(c.Path + "|") || c.Path == categoryInfo.Path)
                //                .ToList();
                //    model.CategoryPathList = categorysPath;
                //}
            }
            model.CurrentCid = cid;
            model.CurrentMod = mod;
            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            ViewBag.DataParam = dataParam;
            #endregion

            string path = "";
            //获取分类列表页path
            Maticsoft.Model.Shop.Products.GoodsType GoodsModel = GoodsBll.GetModel(cid);
            if (GoodsModel != null)
            {
                path = GoodsModel.Path;
            }


            int pageSize = 4;//4
            ViewBag.BasePageSize = _basePageSize;

            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引，0
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;

            int toalCount = productManage.GetSearchCountExShareB(cid, brandid, keyword, price,path);

            ViewBag.TotalCount = toalCount;
           

            List<Model.Shop.Products.ProductInfo> list;
            list = productManage.GetSearchListExShareB(cid, brandid, keyword, price, mod, startIndex, endIndex,path);

            #region
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
            #endregion
            //获取总条数
            if (toalCount < 1) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = list.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);
             //瀑布流Index
            ViewBag.CurrentPageAjaxStartPageIndex = pageIndex;
            ViewBag.CurrentPageAjaxEndPageIndex = model.ProductPagedList.TotalPageCount;
            
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }

        public ActionResult ListWaterfallAllproduct(int cid, int brandid, string keyword, string mod , string price, int pageIndex, string viewName = "_ListWaterfall")
        {
            ViewBag.BasePageSize = _basePageSize;

            keyword = Maticsoft.Common.InjectionFilter.SqlFilter(keyword);

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
             string path = "";
            //获取分类列表页path
            //Maticsoft.Model.Shop.Products.GoodsType GoodsModel = GoodsBll.GetModel(cid);
            //if (GoodsModel != null)
            //{
            //    path = GoodsModel.Path;
            //}
            int pageSize = 16;//4
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex > 1 ? startIndex + pageSize - 1 : pageSize;

            List<Model.Shop.Products.ProductInfoForProductIndex> list;
           // list = productManage.GetSearchListExShareB(cid, brandid, keyword, price, mod, (int)startIndex, endIndex,path);
            var searchResult = searchapi.SearchInMall(keyword, mode2, price, (int)pageIndex, pageSize);
            list = searchResult.ProductsResult;

           

            JsonObject json = new JsonObject();
            if (list != null && list.Count > 0)
            {
                JsonArray data = new JsonArray();
                list.ForEach(  
                    info =>{
                        Model.Shop.Products.ProductInfo ProductInfo = productManage.GetModel(info.ProductId);
                        int  FalseSaleCount=0;
                        int SaleCounts = 0;
                        if(ProductInfo!=null)
                        {
                          FalseSaleCount= ProductInfo.FalseSaleCount;
                          SaleCounts = ProductInfo.SaleCounts;
                        }
                        data.Add(new JsonObject(new string[] { "ProductId", "ThumbnailUrl1", "ProductName", "LowestSalePrice", "MarketPrice", "SaleCounts" },
                            new object[] { info.ProductId, Maticsoft.Web.Components.FileHelper.GeThumbImage(info.ThumbnailUrl1, "T115X115_"), info.ProductName, info.LowestSalePrice.ToString("F"), info.MarketPrice.Value.ToString("F"), SaleCounts + FalseSaleCount }));
                    }
                        );
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }

            return Content(json.ToString());
        }

        public ActionResult ListWaterfallNew(int cid, int brandid, string keyword, string mod, string price, int pageIndex, string viewName = "_ListWaterfall")
        {
            ViewBag.BasePageSize = _basePageSize;

            keyword = Maticsoft.Common.InjectionFilter.SqlFilter(keyword);

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

            string path = "";
            //获取分类列表页path
            Maticsoft.Model.Shop.Products.GoodsType GoodsModel = GoodsBll.GetModel(cid);
            if (GoodsModel != null)
            {
                path = GoodsModel.Path;
            }

            int pageSize =16;//4
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex > 1 ? startIndex + pageSize - 1 : pageSize;

            List<Model.Shop.Products.ProductInfo> list;
            list = productManage.GetSearchListExShareB(cid, brandid, keyword, price, mod, (int)startIndex, endIndex,path);
            //没有取到数据把数据置为空
          

            JsonObject json = new JsonObject();
           
         
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(
                        info =>
                            data.Add(new JsonObject(new string[] { "ProductId", "ThumbnailUrl1", "ProductName", "LowestSalePrice", "MarketPrice", "SaleCounts" },
                                new object[] { info.ProductId, Maticsoft.Web.Components.FileHelper.GeThumbImage(info.ThumbnailUrl1, "T115X115_"), info.ProductName, info.LowestSalePrice.ToString("F"), info.MarketPrice.Value.ToString("F"),info.SaleCounts })));
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }

                return Content(json.ToString());

           
        }
        
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
        #endregion

        #region 品牌
        public PartialViewResult BrandList(int Cid = 0, int productType = 0, int top = 10, string viewName = "_BrandList")
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> brandInfos =
                new List<Maticsoft.Model.Shop.Products.BrandInfo>();
            if (Cid > 0)
            {
                brandInfos = brandBll.GetBrandsByCateId(Cid, true, top);
            }
            else
            {
                brandInfos = brandBll.GetModelListByProductTypeId(productType, top);
            }
            return PartialView(viewName, brandInfos);
        }
        #endregion 

        #region 条件筛选
        public ActionResult Filter(int cid = 0, string keyword="")
        {
            return View();
        }
        #endregion

    }
}
