using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Web.Components.Setting.Shop;
using System;
using Maticsoft.Json.Conversion;
using Maticsoft.ViewModel.Shop;

namespace Maticsoft.Web.Areas.Shop.Controllers
{

    public class HomeController : ShopControllerBase
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryBll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.Products.BrandInfo brandInfoBll = new Maticsoft.BLL.Shop.Products.BrandInfo();



        public ActionResult Index(string viewName = "Index")
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Settings.MainMenus meneBll = new BLL.Settings.MainMenus();
            List<Maticsoft.Model.Settings.MainMenus> NavList = meneBll.GetMenusByAreaByCacle(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop, "M1");
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



            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            ViewBag.NavList = NavList;
            #endregion
            return View(viewName);
        }

        #region 顶部广告位
        /*add by zhongyu 20140625*/
        public PartialViewResult TopAD()
        {
            //此路径可配置
            ViewBag.ImgSrc = @"\Areas\Shop\Themes\M1\Content\images\images\AD\banner1.jpg";
            return PartialView();
        }
        
        #endregion
        /*add by zhongyu 20140625*/
        #region 楼层
        public PartialViewResult MainMsg(string viewName = "_MainMsg")
        {
            BLL.CMS.Content conBll = new BLL.CMS.Content();
            List<Model.CMS.Content> list = conBll.GetModelList(9, 6);
            //ViewBag.contentclassName = contentclassBll.GetClassnameById(9);
            //获取商品分类
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.Where(c => c.ParentCategoryId == 0).ToList();
            ViewBag.TopCatg = categoryInfos;
            ViewBag.ContentList = list;          


            return PartialView(viewName);
        }

        /*楼层1*/
        public PartialViewResult Floor1(string viewName = "_Floor1")
        {
            /*推荐榜*/
            Maticsoft.Model.Shop.Products.ProductRecType type = new ProductRecType();
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecListWithOutCatg(type, 1, 5);
            ViewBag.RecommendList = productList;

            List<Maticsoft.Model.Shop.Products.ProductInfo> hotProductList = productBll.GetProductRecListWithOutCatg(type, 11, 5);
            ViewBag.HotRecommendList = hotProductList;

            return PartialView(viewName);
        }
        /*楼层2*/
        public PartialViewResult Floor2(string viewName = "_Floor2")
        {
            /*推荐榜*/
            Maticsoft.Model.Shop.Products.ProductRecType type = new ProductRecType();
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecListWithOutCatg(type, 2, 5);
            ViewBag.RecommendList = productList;
            return PartialView(viewName);
        }
        /*楼层3*/
        public PartialViewResult Floor3(string viewName = "_Floor3")
        {
            /*推荐榜*/
            Maticsoft.Model.Shop.Products.ProductRecType type = new ProductRecType();
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecListWithOutCatg(type, 3, 5);
            ViewBag.RecommendList = productList;
            return PartialView(viewName);
        }
        /*楼层4*/
        public PartialViewResult Floor4(string viewName = "_Floor4")
        {
            return PartialView(viewName);
        }
        /*楼层5*/
        public PartialViewResult Floor5(string viewName = "_Floor5")
        {
            return PartialView(viewName);
        }

        /*楼层6*/
        public PartialViewResult Floor6(string viewName = "_Floor6")
        {
            return PartialView(viewName);
        }
        #endregion


        /*楼层6*/
        public PartialViewResult ActiveTitle(string viewName = "_jinkou")
        {
            return PartialView(viewName);
        }


        /// <summary>
        /// 遍历所有楼层
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        //public PartialViewResult AllFloor(string viewName = "_AllFloor")
        //{ 
        //}
        //#region 商品分类

        //public PartialViewResult CategoryList(string viewName = "_CategoryList")
        //{
        //    return PartialView(viewName);
        //}

        //#endregion
        

        #region 热销品牌
        public PartialViewResult HotBrands(int top = 10, int productTypeId = 2, string viewName = "_HotBrands")
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> brandInfos = null;
            if (productTypeId > 0)
            {
                brandInfos = brandInfoBll.GetModelListByProductTypeId(productTypeId, top);
            }
            else
            {
                brandInfos = brandInfoBll.GetBrandList("", top);
            }
            return PartialView(viewName, brandInfos);
        }
        #endregion

        #region 品牌库
        public PartialViewResult Brands(int top = 18, int productTypeId = 2, string viewName = "_HotBrands")
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> brandInfos = null;
            if (productTypeId > 0)
            {
                brandInfos = brandInfoBll.GetModelListByProductTypeId(productTypeId, top);
                Maticsoft.BLL.Shop.Products.ProductType productTypeBll = new Maticsoft.BLL.Shop.Products.ProductType();
                Maticsoft.Model.Shop.Products.ProductType productTypeModel = productTypeBll.GetModel(productTypeId);
                string typeName = null;
                if (null != productTypeModel)
                {
                    typeName = productTypeModel.TypeName;
                }
                else
                {
                    typeName = "暂无此品牌";
                }
                Maticsoft.Model.Shop.Products.BrandInfo brandType = new Maticsoft.Model.Shop.Products.BrandInfo();
                brandType.BrandName = typeName;
                //brandInfos.Add(brandType);
                brandInfos.Add(brandType);
            }
            else
            {
                brandInfos = brandInfoBll.GetBrandList("", top);
            }
            return PartialView(viewName, brandInfos);
        } 
        #endregion

        #region 品牌列表
        public ViewResult BrandsList()
        {
            return View();
        } 
        #endregion

        #region 商品列表
        public PartialViewResult ProductList(int Cid, Maticsoft.Model.Shop.Products.ProductRecType RecType = ProductRecType.IndexRec, int Top = 10, string viewName = "_ProductList")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(RecType, Cid, Top);
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

        #region 新品商品
        public PartialViewResult ProductNewList(int Top = 7, string viewName = "_ProductNewList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = categoryBll.GetCategorysByParentId(0, Top);
            return PartialView(viewName, categoryInfos);
        }

        public PartialViewResult NewListPart(int Cid, int Top = 5, string viewName = "_NewListPart")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(ProductRecType.Latest, Cid, Top);
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


        #region 热门评论
        public PartialViewResult HotComments(int Top = 6, string viewName = "_HotComments")
        {
            Maticsoft.BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
            string str = "Status=1";//获取通过审核的评论
            List<Maticsoft.Model.Shop.Products.ProductReviews> list = reviewsBll.GetModelList(str);
            //List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = categoryBll.GetCategorysByParentId(0, Top);
            return PartialView(viewName, list);
        }
        public PartialViewResult CommentPart(int Cid, int Top = 9, string viewName = "_CommentPart")
        {
            //Maticsoft.BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
            //reviewsBll.GetModelList()
            //List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = categoryBll.GetCategorysByParentId(0, Top);
            return PartialView(viewName);
        }
        #endregion
    }
}
