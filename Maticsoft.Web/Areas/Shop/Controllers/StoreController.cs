using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class StoreController : ShopControllerBase
    {
        //
        // GET: /Shop/Supplier/
        private readonly BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        private readonly BLL.Shop.Supplier.SupplierInfo suppinfoBll = new BLL.Shop.Supplier.SupplierInfo();
        public ActionResult Index(int suppId = 0, int cid = 0, string mod = "hot", string price = "0-0", string ky = "", int top = 20, string viewName = "Index")
        {
            Model.Shop.Supplier.SupplierInfo suppinfoModel = suppinfoBll.GetModelByCache(suppId);
            if (suppinfoModel != null && suppinfoModel.Status == 1 && suppinfoModel.StoreStatus == 1 )
            {
                #region RouteDataParam
                string dataParam = "{";
                foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
                {
                    dataParam += item.Key + ":'" + item.Value + "',";
                }
                dataParam = dataParam.TrimEnd(',') + "}";
                ViewBag.DataParam = dataParam;
                #endregion

                //中间显示内容
                ViewBag.SuppIndexContent = suppinfoModel.IndexContent; 
                int actTop = suppinfoModel.IndexProdTop > 0 ? suppinfoModel.IndexProdTop : top;//读取商家自己设置的top值 
                List<Model.Shop.Products.ProductInfo> list = productManage.GetSuppProductsList(actTop, cid, suppId, mod, Common.InjectionFilter.SqlFilter(ky), price);
                #region SEO 优化设置
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title = suppinfoModel.ShopName + "-" + pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion

                ViewBag.HasArea = MvcApplication.HasArea(AreaRoute.MShop);//是否包含手机版
                return View(viewName, list);
            }
            return RedirectToAction("Index", "Home", new { area = "Shop" });
        }

        #region List
        public ActionResult List(int suppId = 0, int cid = 0, string mod = "hot", string price = "0-0", string ky = "",
            int pageIndex = 1, string viewName = "List", string ajaxViewName = "_PageProductList")
        {
            Model.Shop.Supplier.SupplierInfo suppinfoModel = suppinfoBll.GetModelByCache(suppId);
            if (suppinfoModel != null && suppinfoModel.Status == 1 && suppinfoModel.StoreStatus == 1  )
            {
                #region RouteDataParam
                string dataParam = "{";
                foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
                {
                    dataParam += item.Key + ":'" + item.Value + "',";
                }
                dataParam = dataParam.TrimEnd(',') + "}";
                ViewBag.DataParam = dataParam;
                #endregion

                int _pageSize =16;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;

                //获取总条数
                int toalCount = productManage.GetSuppProductsCount(cid, suppId, ky, price);
                ViewBag.ToalCount = toalCount;
                #region SEO 优化设置
                if (cid > 0)
                {
                    List<Maticsoft.Model.Shop.Supplier.SupplierCategories> cateList =
                        Maticsoft.BLL.Shop.Supplier.SupplierCategories.GetAllCateList(suppId);
                    Maticsoft.Model.Shop.Supplier.SupplierCategories categoryInfo =
                        cateList.FirstOrDefault(c => c.CategoryId == cid);
                    ViewBag.Title = categoryInfo != null ? categoryInfo.Name + "-" : "";
                }
                else
                {
                    ViewBag.Title = "全部分类-";
                }
                if (!String.IsNullOrWhiteSpace(ky))
                {
                    ViewBag.Title += ky + "-";
                }

                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title += suppinfoModel.ShopName + "-" + pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion
                if (toalCount < 1) return View(viewName); //NO DATA
                List<Model.Shop.Products.ProductInfo> list = productManage.GetSuppProductsListEx(cid, suppId, ky, price, mod, startIndex, endIndex);
                //分页获取数据
                PagedList<Model.Shop.Products.ProductInfo> pageList = list.ToPagedList(pageIndex, _pageSize, toalCount);
                //检测Ajax请求, 进行无刷新分页
                if (Request.IsAjaxRequest())
                    return PartialView(ajaxViewName, pageList);
                return View(viewName, pageList);
            }
            return RedirectToAction("Index", "Home", new { area = "Shop" });
        }
        #endregion

        #region 申请商家
        [HttpGet]
        [Maticsoft.Components.Filters.TokenAuthorize]
        public ActionResult Apply(string viewName = "Apply")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "商家入驻 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            Model.Shop.Supplier.SupplierInfo model = suppinfoBll.GetModel(Globals.SafeInt(CurrentUser.DepartmentID, -1));
            if (model != null && model.Status == 0) //正在审核
                return RedirectToAction("ApplySuccess", "Store", new { area = "Shop" });
            else if (model != null || CurrentUser.UserType != "UU") //已审核/非普通用户 重定向到商家后台进行登录
                return Redirect("/SP/Account/Login");

            return View(viewName);
        }

        [HttpPost]
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.User)]
        [ValidateInput(false)]
        public ActionResult Apply(Maticsoft.Model.Shop.Supplier.SupplierInfo model, string viewName = "Apply")
        {
            //Safe
            if (string.IsNullOrWhiteSpace(model.Name) ||
                suppinfoBll.Exists(model.Name))
                return Content("ERROR");

            model.UserId = CurrentUser.UserID;
            model.UserName = CurrentUser.UserName;
            model.CreatedUserId = CurrentUser.UserID;
            model.CreatedDate = DateTime.Now;

            model.SupplierId = suppinfoBll.Add(model);
            if (model.SupplierId < 1) return Content("NO");

            //升级当前用户为商家
            CurrentUser.UserType = "SP";
            CurrentUser.DepartmentID = model.SupplierId.ToString();
            CurrentUser.Update();
            //商家角色
            int defaultEnteRoleId = BLL.SysManage.ConfigSystem.GetIntValueByCache("DefaultSuppRoleID");
            if (defaultEnteRoleId > 0)
            {
                CurrentUser.AddToRole(defaultEnteRoleId);
            }
            return Content("OK");
        }

        [HttpPost]
        public bool ExistsSupplierName(string name)
        {
            return suppinfoBll.Exists(name);
        }
        [HttpPost]
        public bool ExistsShopName(string name)
        {
            return suppinfoBll.ExistsShopName(name);
        }

        [HttpGet]
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.Supplier)]
        public ActionResult ApplySuccess(string viewName = "ApplySuccess")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "商家入驻 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            Model.Shop.Supplier.SupplierInfo model = suppinfoBll.GetModel(Globals.SafeInt(CurrentUser.DepartmentID, -1));
            if (model == null)
                RedirectToAction("Apply", "Store", new { area = "Shop" });

            return View(viewName, model);
        }
        #endregion

        #region 商家商品分类
        /// <summary>
        /// 根据商家id和分类id 获取对应的子分类数据 (若要读取一级分类 pcid的值为0即可)
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="suppId"></param>
        /// <param name="Top"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult CategoryList(int Pcid = 0, long suppId = 0, int Top = -1, string viewName = "_CategoryList")
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> cateList = Maticsoft.BLL.Shop.Supplier.SupplierCategories.GetAllCateList(suppId);
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> categoryInfos = null;
            if (Top > 0)
            {
                categoryInfos = cateList.Where(c => c.ParentCategoryId == Pcid).Take(Top).ToList();
            }
            else
            {
                categoryInfos = cateList.Where(c => c.ParentCategoryId == Pcid).ToList();
            }

            return PartialView(viewName, categoryInfos);
        }
        /// <summary>
        /// 根据商家id和分类id 获取对应的分类数据 (如果Cid为0,则读取一级分类，否则读取当前分类的所有子节点)
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="suppId"></param>
        /// <param name="Top"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult SecondCategoryList(int Cid = 0, long suppId = 0, int Top = 10, string viewName = "_SecondCategoryList")
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> cateList = Maticsoft.BLL.Shop.Supplier.SupplierCategories.GetAllCateList(suppId);
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> categoryInfos = null;
            if (Cid == 0)
                categoryInfos = cateList.Where(c => c.ParentCategoryId == Cid).ToList();
            else
            {
                var xxx = cateList.FirstOrDefault(c => c.CategoryId == Cid);//找到当前分类
                if (xxx != null)
                    categoryInfos = cateList.Where(c => c.Path.StartsWith(xxx.Path + "|")).ToList();//获得当前分类的所有子节点
            }
            return PartialView(viewName, categoryInfos);
        }
        #endregion

        #region Header
        public PartialViewResult Header(int suppId = 0, string viewName = "_Header")
        {
            List<Model.Shop.Supplier.SupplierMenus> list = new BLL.Shop.Supplier.SupplierMenus().GetModelList(0, suppId);
            ViewBag.SuppId = suppId;
            return PartialView(viewName, list);
        }

        #endregion

        #region 广告位

        public PartialViewResult AD(int suppId = 0, string viewName = "_AD")
        {
            List<Model.Shop.Supplier.SupplierAD> model = new BLL.Shop.Supplier.SupplierAD().GetModelList(0, suppId);
            return PartialView(viewName, model);
        }

        #endregion

        #region 推荐商品
        public PartialViewResult RecProd(int suppid, int type = 0, int top = 10, string viewName = "_RecProdList")
        {
            List<Model.Shop.Products.ProductInfo> list = productManage.GetSuppRecList(suppid, type);
            return PartialView(viewName, list);
        }
        #endregion

        #region 二维码
        /// <summary>
        ///二维码
        /// </summary>
        /// <param name="suppid"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult Code(int suppid=0, string viewName = "_Code")
        {
            string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
            string basepath = "/";
            if (area.ToLower() != AreaRoute.MShop.ToString().ToLower())
            {
                area = "Shop";
                basepath = "/MShop/";
            }
            else
            {
                area = "MShop";
            }
            string _uploadFolder = string.Format("/{0}/{1}/QR/Store/", MvcApplication.UploadFolder, area);
            string filename = string.Format("{0}.png", suppid);
            string mapPath = Request.MapPath(_uploadFolder);
            string mapPathQRImgUrl = mapPath + filename;
            ViewBag.ProductQRImgUrl = _uploadFolder + filename;
            if (System.IO.File.Exists(mapPathQRImgUrl))
            {
                return PartialView(viewName);
            }
            string baseURL = string.Format("/tools/qr/gen.aspx?margin={0}&size={1}&level={2}&format={3}&content={4}", 0, 100, "30%", "png", "{0}");
            string websiteUrl = "http://" + Globals.DomainFullName + basepath + "Store/" + suppid;
            websiteUrl = "http://" + Globals.DomainFullName + string.Format(baseURL, Common.Globals.UrlEncode(websiteUrl));
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }
            using (var webClient = new System.Net.WebClient())
            {
                webClient.DownloadFile(websiteUrl, mapPathQRImgUrl);
            }
            return PartialView(viewName);
        }
        #endregion

        #region 获取店铺信息
        public JsonResult GetStoreInfo(int suppId)
        {
            var store = suppinfoBll.GetModelList(" SupplierId="+suppId).SingleOrDefault();
            return Json(store,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
