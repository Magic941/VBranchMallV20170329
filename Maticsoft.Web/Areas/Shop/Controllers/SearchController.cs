using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Services;
using Maticsoft.Model.Shop.Products.Lucene;


namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class SearchController : ShopControllerBase
    {
        //
        // GET: /Shop/Search/
                #region 全局变量

        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        private ProductSearchAPI searchapi = new ProductSearchAPI(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ProductSearchServerIP"));
        private  BLL.Shop.Products.BrandInfo brandBll=new Maticsoft.BLL.Shop.Products.BrandInfo();
        private int _basePageSize = 2;
        private int _waterfallSize = 2;
        private int _waterfallDataCount = 1;

        #endregion

        public SearchController()
        {
            this._basePageSize = FallInitDataSize;
            this._waterfallSize = FallDataSize;
        }

        public ActionResult Index(int cid = 0, int brandid = 0, string keyword = "", string mod = "default", string price = "0-0",
                                        int? pageIndex = 1,
                                        string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            ProductListModel model = new ProductListModel();
            keyword= Maticsoft.Common.InjectionFilter.SqlFilter(keyword);
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

            int pageSize = _basePageSize + _waterfallSize;//4
            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引，0
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引 2
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            //int toalCount = productManage.GetSearchCountEx(cid, brandid, keyword, price);

            //改成远程接口搜索
           
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex =  endIndex + pageIndex * pageSize;

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

            var searchResult = searchapi.SearchInMall(keyword, mode2, price, (int)pageIndex, _basePageSize);

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
                _basePageSize,
                toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }
        /// <summary>
        /// 瀑布流
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="brandid"></param>
        /// <param name="keyword"></param>
        /// <param name="mod"></param>
        /// <param name="price"></param>
        /// <param name="startIndex">非瀑布的结束位置</param>       
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult ListWaterfall(int cid, int brandid, string keyword, string mod, string price, int pageIndex, string viewName = "_ListWaterfall")
        {
            //ErrorLogTxt.GetInstance("瀑布索引跟踪").Write("startIndex:"+startIndex);

            ViewBag.BasePageSize = _basePageSize;
            keyword = Maticsoft.Common.InjectionFilter.SqlFilter(keyword);
            //重置分页起始索引，应该是增加到pagesize才对
            //pageIndex = pageIndex > 1 ? pageIndex + 1 : 0;
           
            //计算分页结束索引
           // int endIndex = startIndex > 1 ? startIndex + _waterfallDataCount - 1 : _waterfallDataCount;
           // int toalCount = productManage.GetSearchCountEx(cid, brandid, keyword, price);

           // int pageSize = endIndex - startIndex;

            //获取总条数 并加载数据
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

            var searchResult = searchapi.SearchInMall(keyword, mode2, price, (int)pageIndex, _basePageSize);
            list = searchResult.ProductsResult;
            //没有取到数据把数据置为空
            if (list.Count < 1) return new EmptyResult();   //NO DATA

            return View(viewName, list);
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
                brandInfos = brandBll.GetBrandsByCateId(Cid, true, top).Distinct().ToList();
            }
            else
            {
                brandInfos = brandBll.GetModelListByProductTypeId(productType, top).Distinct().ToList();
            }
            return PartialView(viewName, brandInfos);
        }
        #endregion

        public PartialViewResult Shop(string keyword="", string viewName = "_Shop")
        {
            Model.Shop.Supplier.SupplierInfo suppModel = new BLL.Shop.Supplier.SupplierInfo().GetModelByShopName(Common.InjectionFilter.SqlFilter(keyword));
            return PartialView(viewName, suppModel);
        }
    }
}
