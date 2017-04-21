using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using System;
using System.Data;
using Maticsoft.Json;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class ProSalesController : Maticsoft.Web.Areas.Shop.Controllers.ProSalesController
    {
        private BLL.Shop.PromoteSales.GroupBuy groupBuy = new BLL.Shop.PromoteSales.GroupBuy();
        private BLL.Ms.Regions regionsBll = new BLL.Ms.Regions();
        private BLL.Shop.Products.SKUInfo skuBll = new BLL.Shop.Products.SKUInfo();
        private BLL.Shop.Products.ProductConsults conBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        private BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";

        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";

        #region 团购

        public override System.Web.Mvc.ActionResult GroupBuy(int regionid = 0, int cid = 0, string mod = "default", int? pageIndex = 1, int pageSize = 16,
            string viewName = "GroupBuy",
            string ajaxViewName = "_BuyGroupProductList")
        {
            return base.GroupBuy(regionid, cid, mod, pageIndex, pageSize, viewName, ajaxViewName);
        }

        #endregion

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

        #region 商家信息
        public PartialViewResult StoreLayer(int suppId = 0, string viewName = "_StoreLayer")
        {
            Model.Shop.Supplier.SupplierInfo model = new BLL.Shop.Supplier.SupplierInfo().GetModelByCache(suppId);
            if (model != null && model.Status == 1 && model.StoreStatus == 1)
            {
                model.Address = model.RegionId.HasValue
                                    ? new BLL.Ms.Regions().GetAddress(model.RegionId)
                                    : "暂未设置";
                return PartialView(viewName, model);
            }
            return PartialView(viewName);
        }

        #endregion


        #region 小时购详细页面
        public ActionResult SalesGroupBuy(long ProductId = -1, int ActiveID = 0, int ActiveType = -1, string viewName = "SalesGroupBuy")
        {
            ViewBag.ActiveModel = null;
            //微信商品入口
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
            //小时购商品入口
            if (ActiveType == (int)ActiveTypeEnum.GroupBuy)
            {
                BLL.Shop.PromoteSales.GroupBuy bll = new BLL.Shop.PromoteSales.GroupBuy();
                Model.Shop.PromoteSales.GroupBuy WeiXinGroupBuyInfo = bll.GetModel(ActiveID);


                ViewBag.ActiveModel = WeiXinGroupBuyInfo;
                ProductId = WeiXinGroupBuyInfo.ProductId;
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

            ViewBag.ActiveType = Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.NoActive;

            ViewBag.ActiveType = ActiveType;

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
        #endregion


        #region 豪礼大派送整点抢购
        /// <summary>
        /// 豪礼大派送整点抢购
        /// </summary>
        /// <param name="ViewName"></param>
        /// <returns></returns>
        //public ActionResult GroupBuyMebers(string ViewName = "GroupBuyMebers")
        //{
        //    Maticsoft.Model.Shop.PromoteSales.GroupBuy buymodel = new Model.Shop.PromoteSales.GroupBuy();
        //    return View(ViewName);
        //}
        public virtual ActionResult GroupBuyMebers( string startdate="", string viewName = "GroupBuyMebers")
        {
                BLL.Shop.PromoteSales.GroupBuy groupbuyBll = new BLL.Shop.PromoteSales.GroupBuy();
                List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> listmodel = new List<Model.Shop.PromoteSales.GroupBuy>();
                
                DateTime dt = DateTime.Now; //当前时间

                ViewBag.IsToday = true;

                
               
                
                if (string.IsNullOrWhiteSpace(startdate))
                {
                    startdate = dt.ToString();
                    ViewBag.datehour = dt;
                    int number = DateTime.Now.Hour;
                    ViewBag.date = number;
                }
                else
                {
                    DateTime dt1 = Convert.ToDateTime(startdate);

                    TimeSpan ts = dt1 - dt;
                    if (ts.Days == 1)
                    {
                        ViewBag.datehour = dt1;
                        int number =10;
                        ViewBag.date = number;
                        ViewBag.IsToday = false;
                    }
                    else
                    {
                        ViewBag.datehour = dt;
                        int number = DateTime.Now.Hour;
                        ViewBag.date = number;
                    }
                }
                ViewBag.StartDate = startdate;

                listmodel = groupbuyBll.GetModelListToday(string.Format(" PromotionType=1 and DATEDIFF(d,StartDate,'{0}')=0 ", startdate));

                

                return View(viewName, listmodel);
            }

        public ActionResult GroupBuyProductLists(string startdate = "", int hours = 10, string viewName = "GroupBuyProductList")
        {
            ViewBag.Hours = hours;
            BLL.Shop.PromoteSales.GroupBuy groupbuyBll = new BLL.Shop.PromoteSales.GroupBuy();
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> listmodel = new List<Model.Shop.PromoteSales.GroupBuy>();

            DateTime dt = DateTime.Now; //当前时间

            ViewBag.IsToday = true;
            if (string.IsNullOrWhiteSpace(startdate))
            {
                startdate = dt.ToString();
                ViewBag.datehour = dt;
                ViewBag.date = hours;
            }
            else
            {
                DateTime dt1 = Convert.ToDateTime(startdate);

                TimeSpan ts = dt1 - dt;
                if (ts.Days == 1)
                {
                    ViewBag.datehour = dt1;
                    ViewBag.date = hours;
                    ViewBag.IsToday = false;
                }
                else
                {
                    ViewBag.datehour = dt;
                    ViewBag.date = hours;
                }
            }

            listmodel = groupbuyBll.GetModelListToday(string.Format(" PromotionType=1 and DATEDIFF(d,StartDate,'{0}')=0 ", startdate)).Where(a=>a.StartDate.Hour==hours).ToList();



            return View(viewName, listmodel);
        }


        #endregion
    }
}
