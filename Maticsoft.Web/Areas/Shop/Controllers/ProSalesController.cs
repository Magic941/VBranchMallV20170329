using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Ms;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;
using Maticsoft.Components.Setting;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using System.Data;
using Maticsoft.Json;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class ProSalesController : ShopControllerBase
    {
        //
        // GET: /Shop/ProSales/
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        private BLL.Shop.Supplier.SupplierInfo supplierInfo = new BLL.Shop.Supplier.SupplierInfo();
        private BLL.Shop.Products.BrandInfo brandInfo = new BrandInfo();
        private BLL.Shop.Products.CategoryInfo cateInfo = new CategoryInfo();
        private BLL.Shop.PromoteSales.GroupBuy groupBuy = new GroupBuy();
        private BLL.Ms.Regions regionsBll = new Regions();
        private int _basePageSize = 20;
        private int _waterfallSize = 20000;
        private int _waterfallDataCount = 1;

        #region 会员体验类型搜索
        public ActionResult ShowCountDown(int cid, int? pageIndex = 1, string viewName = "CountDown")
        {
            ProductListModel model = new ProductListModel();
            ProductListModel model2 = new ProductListModel();
            //model.CategoryList = categoryManage.MainCategoryList(null);
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;

            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = productManage.GetProSalesCount();
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;

            List<Model.Shop.Products.ProductInfo> list1 = productManage.GetProSalesList(startIndex, endIndex, 1, cid);
            List<Model.Shop.Products.ProductInfo> list2 = productManage.GetProSalesList(startIndex, endIndex, 2, cid);

            #region 获取产品类型
            DataSet ds = productManage.GetShopCountDownCategories();
            var sumcount = ds.Tables[0].AsEnumerable().Sum(p => p.Field<int>("ProdcutCount"));
            ViewBag.ProductCount = sumcount;
            ViewBag.ProductType = ds;
            #endregion

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetCategorySetting(categoryInfo);
            //pageSetting.Replace(
            //    new[] { PageSetting.RKEY_CNAME, model.CurrentCateName }, 
            //    new[] { PageSetting.RKEY_CID, model.CurrentCid.ToString() }); //分类名称
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            //获取总条数
            if (toalCount < 1) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = list1.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);
            model2.ProductPagedList = list2.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);
            ViewBag.SellOut = model2;

            return View(viewName, model);
        }

        #endregion
        #region  限时抢购


        public ActionResult CountDown(int? pageIndex = 1,
                                  string viewName = "CountDown", string ajaxViewName = "_ProductList")
        {
            ProductListModel model = new ProductListModel();
            ProductListModel model2 = new ProductListModel();
            //model.CategoryList = categoryManage.MainCategoryList(null);
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;

            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = productManage.GetProSalesCount();
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;

            List<Model.Shop.Products.ProductInfo> list1 = productManage.GetProSalesList(startIndex, endIndex, 1);
            List<Model.Shop.Products.ProductInfo> list2 = productManage.GetProSalesList(startIndex, endIndex, 2);

            #region 获取产品类型
            DataSet ds = productManage.GetShopCountDownCategories();
            var sumcount = ds.Tables[0].AsEnumerable().Sum(p => p.Field<int>("ProdcutCount"));
            ViewBag.ProductCount = sumcount;
            ViewBag.ProductType = ds;
            #endregion

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetCategorySetting(categoryInfo);
            //pageSetting.Replace(
            //    new[] { PageSetting.RKEY_CNAME, model.CurrentCateName }, 
            //    new[] { PageSetting.RKEY_CID, model.CurrentCid.ToString() }); //分类名称
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            //获取总条数
            if (toalCount < 1) return View(viewName, model); //NO DATA

            //分页获取数据
            model.ProductPagedList = list1.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);
            model2.ProductPagedList = list2.ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);
            ViewBag.SellOut = model2;
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView(ajaxViewName, model);

            return View(viewName, model);
        }

        public ActionResult ListWaterfall(int startIndex, int type = 1, string viewName = "_ListWaterfall")
        {
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDataCount - 1 : _waterfallDataCount;
            int toalCount = productManage.GetProSalesCount();

            //获取总条数 并加载数据
            List<Model.Shop.Products.ProductInfo> list;

            list = productManage.GetProSalesList(startIndex, endIndex, type);

            if (toalCount < 1) return new EmptyResult();   //NO DATA

            return View(viewName, list);
        }
        #endregion

        #region 豪礼大放送

        /// <summary>
        /// 用户限购数量
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [HttpPost]
        public int GetBuyLimit(int productid)
        {
            BLL.Shop.PromoteSales.GroupBuy GB = new BLL.Shop.PromoteSales.GroupBuy();
            return GB.GetGroupBuyLimit(currentUser.UserID, productid);
        }

        [HttpPost]
        public ActionResult GetPromotionCheck()
        {
            BLL.Shop.ActivityManage.AMPBLL _ampBLL = new BLL.Shop.ActivityManage.AMPBLL();
            JsonObject json = new JsonObject();
            BLL.Shop.PromoteSales.GroupBuy GB = new BLL.Shop.PromoteSales.GroupBuy();
            int productId = int.Parse(Request.Params["ProductId"].ToString());
            int count = int.Parse(Request.Params["count"].ToString());
            Model.Shop.PromoteSales.GroupBuy _group = GB.GetPromotionLimitQu(productId, 1);
            if (_group != null)
            {
                //若为豪礼大放送商品，则需登录后购买
                if (currentUser == null)
                {
                    //return RedirectToAction("Login", "Account");//去登录
                    json.Accumulate("result", "Login");
                    json.Accumulate("Msg", "/Account/Login");
                }
                else
                {
                    Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(currentUser.UserID);
                    int buyLimit = this.GetBuyLimit(productId);
                    //判断限购值
                    if (buyLimit > 0)
                    {
                        Maticsoft.Model.Shop.Products.ShoppingCartInfo shopcart = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart4Selected());
                        if (shopcart.Items.Count != 0 && shopcart.Items.Where(m => m.ProductId == productId).ToList().Count != 0)
                        {
                            int alreadyBuy = shopcart.Items.Where(m => m.ProductId == productId).ToList().First().Quantity;
                            if (alreadyBuy + count >= buyLimit)
                            {
                                json.Accumulate("result", "Full");
                                json.Accumulate("Msg", "已达到限购值");
                            }
                            else
                            {
                                json.Accumulate("result", "Normal");
                                json.Accumulate("g", _group.GroupBuyId);
                            }
                        }
                        else
                        {
                            json.Accumulate("result", "ProSaleNormal");
                            json.Accumulate("g", _group.GroupBuyId);
                        }
                    }
                    else
                    {
                        json.Accumulate("result", "Full");
                        json.Accumulate("Msg", "已达到限购值");
                    }

                }
            }
            else
            {
                json.Accumulate("result", "Normal");
            }
            return Content(json.ToString());
        }

        public virtual ActionResult SalesForMembers(int regionid, int cid, string curDate, string mod = "default", int? pageIndex = 1, int pageSize = 30,
                               string viewName = "GroupBuyIndexHaoLi", string ajaxViewName = "_BuyProductListGroupHaoLi")
        {
            if (regionid <= 0)
            {
                regionid = BLL.SysManage.ConfigSystem.GetIntValueByCache("Shop_GroupBuy_DefaultRegion");
            }
            regionid = regionid <= 0 ? 214 : regionid;//防止从cache中未取到参数报错
            //  regionid = 643;
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> groupList;//new List<Model.Shop.PromoteSales.GroupBuy>();
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            var cdate = string.IsNullOrEmpty(curDate) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : curDate;
            int totalCount = groupBuy.GetCountHaoLi(cdate);
            ViewBag.regionId = regionid;
            var where = " T.Status = 1  AND DateDiff(HH,StartDate,'" + cdate + "')=0";
            groupList = groupBuy.GetListByPage(where, cid, regionid, mod, 0, totalCount, 1);
            //大抢购不需要分页
            PagedList<Model.Shop.PromoteSales.GroupBuy> pagedList = new PagedList<Model.Shop.PromoteSales.GroupBuy>(groupList, 0, totalCount, totalCount);
            bool isParentRegion = regionsBll.IsParentRegion(regionid);
            ViewBag.IsForMembers = true;
            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            int parentId;
            if (isParentRegion)//如果是下边有子区域
            {
                parentId = regionid;
                dataParam += "parentId" + ":'" + parentId + "',";
            }
            else//没有子区域
            {
                parentId = regionsBll.GetCurrentParentId(regionid);
                dataParam += "parentId" + ":'" + parentId + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            RouteData.Values.Add("parentId", parentId);
            ViewBag.DataParam = dataParam;
            #endregion

            //获取总条数
            //  if (totalCount < 1) return View(viewName, pagedList); //NO DATA
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
            {
                return PartialView(ajaxViewName, pagedList);
            }
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetCategorySetting(categoryInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            //获取团购热销top10
            DataSet ds = groupBuy.GetGroupBuyHot();
            ViewBag.hotgroupbuy = ds;

            return View(viewName, pagedList);
        }

        public virtual ActionResult SalesForMembersByHours(int regionid = -1, int cid = 0, string Date="", string mod = "default", int? pageIndex = 1, int pageSize = 30,
                              string viewName = "GroupBuyIndexHaoLi", string ajaxViewName = "_BuyProductListGroupHaoLi")
        {
            if (regionid <= 0)
            {
                regionid = BLL.SysManage.ConfigSystem.GetIntValueByCache("Shop_GroupBuy_DefaultRegion");
            }
            regionid = regionid <= 0 ? 214 : regionid;//防止从cache中未取到参数报错
            //  regionid = 643;
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> groupList;//new List<Model.Shop.PromoteSales.GroupBuy>();
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            var cdate = string.IsNullOrEmpty(Date) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : Date;
            if (!string.IsNullOrEmpty(Date))
            {
                cdate = DateTime.Now.ToString("yyyy-MM-dd") + " " + Date + ":00";
            }

            int totalCount = groupBuy.GetCountHaoLi(cdate);
            ViewBag.regionId = regionid;
            var where = " T.Status = 1  AND DateDiff(HH,StartDate,'" + cdate + "')=0";
            groupList = groupBuy.GetListByPage(where, cid, regionid, mod, 0, totalCount, 1);
            //大抢购不需要分页
            PagedList<Model.Shop.PromoteSales.GroupBuy> pagedList = new PagedList<Model.Shop.PromoteSales.GroupBuy>(groupList, 0, totalCount, totalCount);
            bool isParentRegion = regionsBll.IsParentRegion(regionid);
            ViewBag.IsForMembers = true;
            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            int parentId;
            if (isParentRegion)//如果是下边有子区域
            {
                parentId = regionid;
                dataParam += "parentId" + ":'" + parentId + "',";
            }
            else//没有子区域
            {
                parentId = regionsBll.GetCurrentParentId(regionid);
                dataParam += "parentId" + ":'" + parentId + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            RouteData.Values.Add("parentId", parentId);
            ViewBag.DataParam = dataParam;
            #endregion

            //获取总条数
            //  if (totalCount < 1) return View(viewName, pagedList); //NO DATA
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
            {
                return PartialView(ajaxViewName, pagedList);
            }
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetCategorySetting(categoryInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            //获取团购热销top10
            DataSet ds = groupBuy.GetGroupBuyHot();
            ViewBag.hotgroupbuy = ds;

            return View(viewName, pagedList);
        }


        #endregion

        #region 团购 ProSales/GroupBuy/{regionid}/{cid}/{mod}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionid"></param>
        /// <param name="cid"></param>
        /// <param name="mod"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="viewName"></param>
        /// <param name="ajaxViewName"></param>
        /// <returns></returns>

        public virtual ActionResult GroupBuy(int regionid, int cid, string mod = "default", int? pageIndex = 1, int pageSize = 30,
                               string viewName = "GroupBuyIndex", string ajaxViewName = "_BuyProductListGroup")
        {
            if (regionid <= 0)
            {
                regionid = BLL.SysManage.ConfigSystem.GetIntValueByCache("Shop_GroupBuy_DefaultRegion");
            }
            ViewBag.IsForMembers = false;
            regionid = regionid <= 0 ? 214 : regionid;//防止从cache中未取到参数报错
            //  regionid = 643;
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> groupList;//new List<Model.Shop.PromoteSales.GroupBuy>();
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            int totalCount = groupBuy.GetCount(cid, regionid, false);
            ViewBag.regionId = regionid;
            groupList = groupBuy.GetListByPage(null, cid, regionid, mod, startIndex, endIndex, 0);
            PagedList<Model.Shop.PromoteSales.GroupBuy> pagedList = new PagedList<Model.Shop.PromoteSales.GroupBuy>(groupList, pageIndex.Value, pageSize, totalCount);
            bool isParentRegion = regionsBll.IsParentRegion(regionid);

            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            int parentId;
            if (isParentRegion)//如果是下边有子区域
            {
                parentId = regionid;
                dataParam += "parentId" + ":'" + parentId + "',";
            }
            else//没有子区域
            {
                parentId = regionsBll.GetCurrentParentId(regionid);
                dataParam += "parentId" + ":'" + parentId + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            RouteData.Values.Add("parentId", parentId);
            ViewBag.DataParam = dataParam;
            #endregion

            //获取总条数
            //  if (totalCount < 1) return View(viewName, pagedList); //NO DATA
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
            {
                return PartialView(ajaxViewName, pagedList);
            }
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetCategorySetting(categoryInfo);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            //获取团购热销top10
            DataSet ds = groupBuy.GetGroupBuyHot();
            ViewBag.hotgroupbuy = ds;

            return View(viewName, pagedList);
        }
        #region 团购分类left
        public PartialViewResult ProductGroupBuyCategoryList(int Cid, bool IsForMember, string viewName = "_ProductCategoryGroupBuy")
        {
            DataSet ds = groupBuy.GetGroupbyCategory("");
            ViewBag.Cid = Cid;
            ViewBag.IsForMembers = IsForMember;
            return PartialView(viewName, ds);
        }
        #endregion

        public ActionResult GetArea(int regionId, int cid, string mod = "default", string viewName = "_AreaList")
        {
            if (regionId < 1)
            {
                regionId = BLL.SysManage.ConfigSystem.GetIntValueByCache("Shop_GroupBuy_DefaultRegion");
            }
            regionId = regionId < 1 ? 643 : regionId;//防止从cache中未取到参数报错
            List<Model.Ms.Regions> regionList = regionsBll.GetListDistrictByParentId(regionId);
            int parentId;
            bool isParentRegion = regionsBll.IsParentRegion(regionId);
            if (isParentRegion)
            {
                parentId = regionId;
            }
            else
            {
                parentId = regionsBll.GetCurrentParentId(regionId);
            }
            if (regionList.Count < 1)
            {
                regionList = regionsBll.GetSamePathArea(regionId);
            }
            #region RouteDataParam
            string dataParam = "{";
            foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";
            }
            dataParam = dataParam.TrimEnd(',') + "}";
            RouteData.Values.Add("parentId", parentId);
            RouteData.Values.Add("cid", cid);
            RouteData.Values.Add("mod", mod);
            ViewBag.DataParam = dataParam;
            #endregion
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, regionList);
            }
            return View(viewName, regionList);
        }

        public ActionResult GetListCate(string strWhere, bool IsForMembers, string viewName = "_CategoryList")
        {
            List<Model.Shop.PromoteSales.GroupBuy> groupList = groupBuy.GetCategory(strWhere);
            ViewBag.IsForMembers = IsForMembers;
            return View(viewName, groupList);
        }

        public ActionResult BuyListWaterfall(int startIndex, string viewName = "_BuyListWaterfall")
        {
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDataCount - 1 : _waterfallDataCount;
            int toalCount = productManage.GetGroupBuyCount();

            //获取总条数 并加载数据
            List<Model.Shop.Products.ProductInfo> list = productManage.GetGroupBuyList(startIndex, endIndex);

            if (toalCount < 1) return new EmptyResult();   //NO DATA

            return View(viewName, list);
        }

        public ActionResult Test(string viewName = "test")
        {
            return View();
        }



        #endregion

        #region 商铺活动页
        public ActionResult SupplierList(int? pageIndex = 1, int pageSize = 15)
        {
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
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;

            Maticsoft.ViewModel.Supplier.SupplierComm supplierComm = new ViewModel.Supplier.SupplierComm();
            supplierComm.SupplierList = supplierInfo.DataTableToList(supplierInfo.GetListByPage(" StoreStatus=1 and IsOnLine=1 ", " Sequence", startIndex, endIndex).Tables[0]);
            int toalCount = supplierInfo.DataTableToList(supplierInfo.GetList(0, " StoreStatus=1 and IsOnLine=1 ", " Sequence ").Tables[0]).Count;
            supplierComm.SupplierPagedList = supplierInfo.DataTableToList(supplierInfo.GetListByPage(" StoreStatus=1 and IsOnLine=1 ", " Sequence ", startIndex, endIndex).Tables[0]).ToPagedList(
                pageIndex ?? 1,
                pageSize,
                toalCount);

            supplierComm.BrandList = brandInfo.GetBrandList("1=1", 18);
            supplierComm.ServerURL = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");


            return View(supplierComm);
        }
        #endregion
    }
}
