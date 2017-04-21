using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Maticsoft.BLL.Settings;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Model.Shop.PromoteSales;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class PartialController : ShopControllerBase
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryBll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private BLL.CMS.ContentClass contentclassBll = new BLL.CMS.ContentClass();
        private BLL.Shop.Supplier.SupplierInfo supplierBll = new BLL.Shop.Supplier.SupplierInfo();
        #region 网站共通页面
        public PartialViewResult Header(string viewName = "_Header")
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Logo = webSiteSet.LogoPath;
            ViewBag.WebName = webSiteSet.WebName;
            ViewBag.Domain = webSiteSet.WebSite_Domain;
            ViewBag.CurrentUser = CurrentUser;
            //ViewBag.CurrentUser = CurrentUser.NickName.ToString();
            return PartialView(viewName);
        }
        public PartialViewResult Search(string viewName = "_Search")
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Logo = webSiteSet.LogoPath;
            ViewBag.WebName = webSiteSet.WebName;
            ViewBag.Domain = webSiteSet.WebSite_Domain;
            return PartialView(viewName);
        }

        #region 网站导航

        public PartialViewResult Navigation(string viewName = "_Navigation", string Theme = "M1")
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Settings.MainMenus meneBll = new BLL.Settings.MainMenus();
            List<Maticsoft.Model.Settings.MainMenus> NavList = meneBll.GetMenusByAreaByCacle(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop, Theme);
            //从cookie中读取购物车内容
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            //DONE: 获取已选中内容的购物车进行 购物车 部分商品 下单 BEN Modify 20130923
            ShoppingCartModel model = new ShoppingCartModel();
            model.AllCartInfo = cartHelper.GetShoppingCart();
            model.SelectedCartInfo = cartHelper.GetShoppingCart4Selected();
            ViewBag.ShopCarPreview = model;


            //获取商品分类
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.Where(c => c.ParentCategoryId == 0).ToList();
            ViewBag.TopCatg = categoryInfos;

            return PartialView(viewName, NavList);
        }

        #endregion 网站导航
        public PartialViewResult Footer(string viewName = "_Footer")
        {
            return PartialView(viewName);
        }
        #endregion

        #region 首页购物车预览
        public PartialViewResult ShopCarPrev(string viewName = "_ShopCarPrev")
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            //从cookie中读取购物车内容
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            //DONE: 获取已选中内容的购物车进行 购物车 部分商品 下单 BEN Modify 20130923
            ShoppingCartModel model = new ShoppingCartModel();
            model.AllCartInfo = cartHelper.GetShoppingCart();
            model.SelectedCartInfo = cartHelper.GetShoppingCart4Selected();
            ViewBag.ShopCarPreview = model;
            return PartialView(viewName);
        }
        #endregion

        #region 三级页面按分类推荐
        public PartialViewResult ProductCatgRec(ProductRecType Type = ProductRecType.Recommend, int Top = 5, int Cid = 0)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(Type, Cid, Top);
            return PartialView(productList);
        } 
        #endregion

        #region 推荐商品
        public PartialViewResult ProductRec(ProductRecType Type = ProductRecType.Recommend, int Cid = 0, int Top = 5, string ViewName = "_ProductRec")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(Type, Cid, Top);
            List<Maticsoft.Model.Shop.Products.ProductInfo> prolist = new List<Model.Shop.Products.ProductInfo>();
            #region 是否静态化
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopProductStatic");
            string basepath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.Shop);
            if (productList != null)
            {
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
                return PartialView(ViewName, prolist);
            }
            else
            {
                return PartialView(ViewName, productList);
            }
            
            #endregion
           
        }

        #endregion

        public PartialViewResult SearchCart(string ViewName = "_SearchCart")
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = cartHelper.GetShoppingCart();
            ViewBag.CartCount = cartInfo.Quantity;

            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Logo = webSiteSet.LogoPath;
            return PartialView(ViewName);
        }

        #region 广告位
        public PartialViewResult AdDetail(int id, string ViewName = "_IndexAd")
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Advertisement();
            List<Maticsoft.Model.Settings.Advertisement> list = bll.GetListByAidCache(id);
            return PartialView(ViewName, list);
        }

        /// <summary>
        /// 小广告
        /// </summary>
        /// <param name="AdvPositionId"></param>
        /// <returns></returns>
        public PartialViewResult AD(int AdvPositionId, string viewName = "_AD")
        {
            BLL.Settings.Advertisement bllAdvertisement = new BLL.Settings.Advertisement();
            Model.Settings.Advertisement model = bllAdvertisement.GetModelByAdvPositionId(AdvPositionId);
            return PartialView(viewName, model);
        }
        #endregion

        #region 商品分类
        
        public PartialViewResult IndexSecondCategoryList(int Cid = 0, int Top = 10, string ViewName = "_SecondCateAll")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = null;
            if (Cid == 0)
                categoryInfos = cateList.Where(c => c.ParentCategoryId == Cid).ToList();
            else
            {
                var xxx = cateList.FirstOrDefault(c => c.CategoryId == Cid);
                if (xxx != null)
                    categoryInfos = cateList.Where(c => c.Path.StartsWith(xxx.Path + "|")).ToList();
            }
            if (Top > 0)
            {
                categoryInfos = categoryInfos.Take(Top).ToList();
            }
            return PartialView(ViewName, categoryInfos);
        }

        public PartialViewResult IndexSubCategoryList(int Cid = 0, int Top = 10, string ViewName = "_SecondCate")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = null;
            categoryInfos = cateList.Where(c => c.ParentCategoryId == Cid).ToList();
            return PartialView(ViewName, categoryInfos);
        }
        #endregion

        #region 头部登录注册导航
        public PartialViewResult Login(string notLoginView = "_NotLogin", string userLoginView = "_UserLogin")
        {
            //判断用户是否已登录  用来决定页面上显示什么(登录 或  退出)
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                ViewBag.loginnickname = CurrentUser.NickName;
                return PartialView(userLoginView);
            }
            ViewBag.RegisterToggle = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式  
            return PartialView(notLoginView);
        }
        #endregion

        #region 文章

        public PartialViewResult ContentList(string viewName, int ClassID, int Top)
        {
            BLL.CMS.Content conBll = new BLL.CMS.Content();
            List<Model.CMS.Content> list = conBll.GetModelList(ClassID, Top);
            ViewBag.contentclassName = contentclassBll.GetClassnameById(ClassID);
            return PartialView(viewName, list);
        }

        #endregion

        #region 菜单详细
        public PartialViewResult MenuDetail(int Cid = 0, int Top = 4, string ViewName = "_MenuDetail")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.Where(c => c.ParentCategoryId == Cid).ToList();
            ViewBag.Cid = Cid;
            int haschildren = 0;//子节点的个数
            categoryInfos.ForEach(x =>
                {
                    if (x.HasChildren == true)
                        haschildren++;
                });
            ViewBag.haschildren = haschildren;           
            return PartialView(ViewName, categoryInfos);
        }
        #endregion

        #region 热门关键字
        public PartialViewResult HotKeyword(int Cid = 0, int Top = 6, string ViewName = "_HotKeyword")
        {
            Maticsoft.BLL.Shop.Products.HotKeyword keywordBll = new Maticsoft.BLL.Shop.Products.HotKeyword();
            List<Maticsoft.Model.Shop.Products.HotKeyword> keywords = keywordBll.GetKeywordsList(Cid, Top);
            ViewBag.Cid = Cid;
            return PartialView(ViewName, keywords);
        }
        #endregion

        #region 百度分享脚本
        public ActionResult BaiduShare()
        {
            ViewBag.BaiduUid = BLL.SysManage.ConfigSystem.GetValueByCache("BaiduShareUserId");
            return View("_BaiduShare");
        }
        #endregion

        #region 商家浮动层
        public PartialViewResult FloatSuppLayer(int suppId=0,string viewName = "_FloatSuppLayer")
        {
            Model.Shop.Supplier.SupplierInfo model= new BLL.Shop.Supplier.SupplierInfo().GetModelByCache(suppId);
            if (model != null && model.Status == 1 && model.StoreStatus==1)
            {
                model.Address = model.RegionId.HasValue
                                    ? new BLL.Ms.Regions().GetAddress(model.RegionId)
                                    : "暂未设置";
                return PartialView(viewName, model);
            }
            return PartialView(viewName);
        }
        /// <summary>
        /// 楼层中所有商品和热销商品推荐
        /// </summary>
        /// <param name="recommendId"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult RecommendProducts(int recommendId = 0,int top=4, string viewName="RecommendProducts")
        {
            Maticsoft.Model.Shop.Products.ProductRecType type = new ProductRecType();
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecListWithOutCatg(type, recommendId, top);

            return PartialView(viewName, productList);
        }
        

        #endregion
        /// <summary>
        /// 获取推荐的店铺
        /// </summary>
        /// <param name="top"></param>
        /// <param name="rec"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult RecStore(int top = 5, int rec = 1, string viewName = "_RecStore")
        {
            List<Model.Shop.Supplier.SupplierInfo> list = supplierBll.GetList(top,rec);
            return PartialView(viewName,list);
        }
        public ActionResult SuppLogo(int id=0,string size="")
        {
            string pathName = string.Format("/Upload/Supplier/Logo/{0}_{1}", id, size);
            return File(pathName, "application/x-img");
        }

        #region 团购详情页右侧list
        public PartialViewResult GroupRec(string ViewName="_GroupRec")
        {
            Maticsoft.BLL.Shop.PromoteSales.GroupBuy groupBuyBll = new BLL.Shop.PromoteSales.GroupBuy();
            List<GroupBuy> groubuyList = groupBuyBll.GetModelList("");
            return PartialView(ViewName,groubuyList);
            
        }
        #endregion
    }
}
