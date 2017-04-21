using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Components.Setting;
using Maticsoft.Model.JLT;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.Model.SysManage;
using System.Web.Security;
using System.Security.Cryptography;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class HomeController : MShopControllerBase
    {
        //
        // GET: /Mobile/Home/
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private BLL.Shop.Products.BrandInfo brandInfoBll = new BLL.Shop.Products.BrandInfo();
        private BLL.CMS.ContentClass contentclassBll = new BLL.CMS.ContentClass();
        private BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
        private BLL.Ms.EntryForm FormBll = new BLL.Ms.EntryForm();

        public ActionResult Index()
        {
            ViewBag.MShopLogo = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MShop_Logo");
            ViewBag.Title = ViewBag.MShopName = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MShop_Name");
            ViewBag.RecommendUserName = RecommendUserNameID;
            ViewBag.Phone = "";
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }

            #region 微信首页销售价为1元小时购的商品
            BLL.Shop.PromoteSales.GroupBuy groupbuyBll = new BLL.Shop.PromoteSales.GroupBuy();
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> listmodel = new List<Model.Shop.PromoteSales.GroupBuy>();
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> listscu = null;
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> listsaf = null;
            DateTime dt = DateTime.Now;
            ViewBag.datehour = dt;
            string startdate = dt.ToString("yyyy-MM-dd");
            int number = DateTime.Now.Hour;
            int j = 11;
            ViewBag.IsToday = true;
            
            ViewBag.date = number;
            ViewBag.jj = j;
            DateTime today = Convert.ToDateTime(dt.ToString("yyyy-MM-dd"));
            DateTime startdate1 = today.AddHours(12);
            DateTime enddate1 = startdate1.AddHours(1);
            listmodel = groupbuyBll.GetModelListToday(string.Format(" PromotionType=1  and DATEDIFF(d,StartDate,'{0}')=0 ", startdate));

            if (listmodel != null)
            {
                if (number < 13)
                {
                    listscu = listmodel.Where(a => a.StartDate.Hour == 12).ToList();
                }
                if (number >= 13 && number < 15)
                {
                     startdate1 = today.AddHours(14);
                     enddate1 = startdate1.AddHours(1);
                    listscu = listmodel.Where(a => a.StartDate.Hour == 14).ToList();
                }
                if (number >= 15 && number < 19)
                {
                    startdate1 = today.AddHours(18);
                    enddate1 = startdate1.AddHours(1);

                    listscu = listmodel.Where(a => a.StartDate.Hour == 18).ToList();
                }
                if (number >= 19 )
                {
                    startdate1 = today.AddHours(20);
                    enddate1 = startdate1.AddHours(1);
                    listscu = listmodel.Where(a => a.StartDate.Hour == 20).ToList();
                }
            }
            ViewBag.DataList = listscu.Take(6).ToList();
            ViewBag.DataListend = listsaf;
            ViewBag.StartDate = startdate1;
            ViewBag.EndDate = enddate1;
            #endregion

            #region 限时抢购（团购）
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> listtuan = new List<Model.Shop.PromoteSales.GroupBuy>();
            listtuan = groupbuyBll.GetModelListToday(string.Format(" PromotionType=0  and DATEDIFF(s,EndDate,'{0}')<0 ", DateTime.Now));
            ViewBag.tuan = listtuan;

            #endregion
            return View("indexnew");
        }
        #region 分享首页
        public ActionResult ShareIndex(string viewName = "ShareIndex")
        {
            //Safe
            int userId = currentUser == null ? -1 : currentUser.UserID;
            if (userId == -1) return Redirect("a/l?returnUrl=" + Request.Url);
            ViewBag.Phone = currentUser.UserID;
            ViewBag.RecommendUserName = RecommendUserNameID;


            return View(viewName);
        }
        public ActionResult IndexService(string viewName = "IndexService")
        {
            ViewBag.Phone = "";
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                Model.Members.UsersExpModel exp = userEXBll.GetUsersExpModel(currentUser.UserID);

                Maticsoft.Model.Ms.EntryForm EntryModel = FormBll.GetByUserNameModel(CurrentUser.UserName);

                Maticsoft.Model.Ms.EntryForm EnterFormModel = FormBll.GetByUserNameModel(CurrentUser.UserName);

                ViewBag.EntryModel = EntryModel;
                ViewBag.EnterFormModel = EnterFormModel;
                return View(exp);
            }
            return View(viewName);

        }
        public ActionResult AboutUs(string viewName = "AboutUs")
        {
            ViewBag.Phone = "";
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            return View(viewName);
        }
        public ActionResult ContactUs(string viewName = "ContactUs")
        {
            ViewBag.Phone = "";
            if (currentUser != null)
            {
                ViewBag.Phone = currentUser.UserID;
            }
            return View(viewName);
        }

        #endregion
        #region 分享商品
        public PartialViewResult ProductRec(ProductRecType Type = ProductRecType.Share, int Cid = 0, int Top = 5, string ViewName = "_ShareProductList")
        {
            Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
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

        #region 商品列表
        public PartialViewResult ProductList(int Cid, Maticsoft.Model.Shop.Products.ProductRecType RecType = ProductRecType.IndexRec, int Top = 10, string viewName = "_ProductList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = cateList.FirstOrDefault(c => c.CategoryId == Cid);
            if (categoryInfo != null)
            {
                ViewBag.CategoryName = categoryInfo.Name;
            }
            ViewBag.RecType = RecType;
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(RecType, Cid, Top);
            #region 前台需要什么类型的商品
            switch (RecType)
            {
                case ProductRecType.Cheap:
                    ViewBag.productType = "特价商品";
                    break;
                case ProductRecType.Hot:
                    ViewBag.productType = "热卖商品";
                    break;
                case ProductRecType.Latest:
                    ViewBag.productType = "最新商品";
                    break;
                case ProductRecType.Recommend:
                    ViewBag.productType = "推荐商品";
                    break;
                default:
                    ViewBag.productType = "推荐商品";
                    break;
            }
            #endregion
            return PartialView(viewName, productList);
        }
        #endregion

        #region 获取首页楼层商品列表
        public PartialViewResult ProductListFloor(int Cid = 0, int floor = 0, Maticsoft.Model.Shop.Products.ProductRecType RecType = ProductRecType.IndexFloor, int Top = 5, string viewName = "_ProductList")
        {
            ViewBag.RecType = RecType;
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecListWithOutCatgB(RecType, floor, Top);
            #region 前台需要什么类型的商品
            switch (RecType)
            {
                case ProductRecType.Cheap:
                    ViewBag.productType = "特价商品";
                    break;
                case ProductRecType.Hot:
                    ViewBag.productType = "热卖商品";
                    break;
                case ProductRecType.Latest:
                    ViewBag.productType = "最新商品";
                    break;
                case ProductRecType.Recommend:
                    ViewBag.productType = "推荐商品";
                    break;
                case ProductRecType.IndexRec:
                    ViewBag.productType = "首页楼层";
                    break;
                case ProductRecType.IndexFloor:
                    ViewBag.productType = "楼层推荐";
                    break;
                default:
                    ViewBag.productType = "推荐商品";
                    break;
            }
            #endregion
            return PartialView(viewName, productList);
        }
        #endregion

        #region 获取首页楼层商品列表(李永琴没有楼层)
        public PartialViewResult ProductListFloorNew(int Cid = 0, int floor = 0, Maticsoft.Model.Shop.Products.ProductRecType RecType = ProductRecType.IndexFloor, int Top = 5, string viewName = "_ProductListNew")
        {
            ViewBag.RecType = RecType;
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecListWithOutCatgB(0, 0, -1);
            #region 前台需要什么类型的商品
            switch (RecType)
            {
                case ProductRecType.Cheap:
                    ViewBag.productType = "特价商品";
                    break;
                case ProductRecType.Hot:
                    ViewBag.productType = "热卖商品";
                    break;
                case ProductRecType.Latest:
                    ViewBag.productType = "最新商品";
                    break;
                case ProductRecType.Recommend:
                    ViewBag.productType = "推荐商品";
                    break;
                case ProductRecType.IndexRec:
                    ViewBag.productType = "首页楼层";
                    break;
                case ProductRecType.IndexFloor:
                    ViewBag.productType = "楼层推荐";
                    break;
                default:
                    ViewBag.productType = "推荐商品";
                    break;
            }
            #endregion
            return PartialView(viewName, productList);
        }
        #endregion

        public PartialViewResult CategoryList(int Cid = 0, int Top = 10, string ViewName = "_CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.Where(c => c.ParentCategoryId == Cid).Take(Top).ToList();
            return PartialView(ViewName, categoryInfos);
        }

        public PartialViewResult NewsList(string viewName, int ClassID, int Top)
        {
            BLL.CMS.Content conBll = new BLL.CMS.Content();
            List<Model.CMS.Content> list = conBll.GetModelList(ClassID, Top);
            ViewBag.ContentClassName = contentclassBll.GetClassnameById(ClassID);
            return PartialView(viewName, list);
        }

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

        public string GetSignature(string jsapi_ticket, string noncestr, string timestamp, string url)
        {
            Dictionary<string, string> dt = new Dictionary<string, string>();
            dt.Add("jsapi_ticket", jsapi_ticket);
            dt.Add("noncestr", noncestr);
            dt.Add("timestamp", timestamp);
            dt.Add("url", url);
            string tmpStr = FormatBizQueryParaMapForUnifiedPay(dt);
             return Sha1(tmpStr);
        }
        /// <summary>
        /// 统一支付  （参数组合）
        /// </summary>
        /// <param name="paraMap"></param>
        /// <param name="urlencode"></param>
        /// <returns></returns>
        public  string FormatBizQueryParaMapForUnifiedPay(Dictionary<string, string> paraMap)
        {
            string buff = "";
            try
            {
                var result = from pair in paraMap orderby pair.Key select pair;
                foreach (KeyValuePair<string, string> pair in result)
                {
                    if (pair.Key != "")
                    {

                        string key = pair.Key;
                        string val = pair.Value;
                        buff += key + "=" + val + "&";
                    }
                }

                if (buff.Length == 0 == false)
                {
                    buff = buff.Substring(0, (buff.Length - 1) - (0));
                }
            }
            catch (Exception e)
            {
                
            }
            return buff;
        }
        public  String Sha1(String s)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
					'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                byte[] btInput = System.Text.Encoding.Default.GetBytes(s);
                SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

                byte[] md = sha.ComputeHash(btInput);
                // 把密文转换成十六进制的字符串形式
                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[(int)(((byte)byte0) >> 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new string(str);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 考勤
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult Attendance(int userId = 0)
        {
            Maticsoft.BLL.JLT.AttendanceType typeBll = new Maticsoft.BLL.JLT.AttendanceType();
            List<Maticsoft.Model.JLT.AttendanceType> typeList = typeBll.GetAllType();
            ViewBag.UserId = userId;
            ViewBag.Title = "考勤提交";
            return View(typeList);
        }

        #region Ajax 方法
        public ActionResult AjaxAddAttendance(FormCollection Fm)
        {
            Maticsoft.Model.JLT.UserAttendance model = new UserAttendance();
            Maticsoft.BLL.JLT.UserAttendance attendanceBll = new BLL.JLT.UserAttendance();
            int userId = Common.Globals.SafeInt(Fm["UserId"], 0);
            string latitude = Fm["Latitude"];
            string longitude = Fm["Longitude"];
            int typeId = Common.Globals.SafeInt(Fm["TypeId"], 0);

            Maticsoft.Accounts.Bus.User user = new User(userId);
            model.UserID = userId;
            model.Score = 0;
            model.Status = 1;
            model.Latitude = latitude;
            model.Longitude = longitude;
            model.TypeID = typeId;
            model.TrueName = user.TrueName;
            model.UserName = user.UserName;
            model.CreatedDate = DateTime.Now;
            model.AttendanceDate = DateTime.Now.Date;
            return attendanceBll.Add(model) > 0 ? Content("True") : Content("False");
        }
        #endregion


        #region 品牌列表

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

        //网站信息
        public PartialViewResult OpertorsInfo(string viewName = "_OpertorsInfo")
        {
            ViewBag.Address = GetValueByCache("Opertors_Address");
            ViewBag.BusinessHoursEnd = GetValueByCache("Opertors_BusinessHoursEnd");
            ViewBag.BusinessHoursStart = GetValueByCache("Opertors_BusinessHoursStart");
            ViewBag.DeliveryArea = GetValueByCache("Opertors_DeliveryArea");
            ViewBag.SentPrices = GetValueByCache("Opertors_SentPrices");
            ViewBag.ServiceRadius = GetValueByCache("Opertors_ServiceRadius");
            ViewBag.Telephone = GetValueByCache("Opertors_Telephone");
            return PartialView(viewName);
        }
        public string GetValueByCache(string keyName)
        {
            return BLL.SysManage.ConfigSystem.GetValueByCache(keyName, ApplicationKeyType.Shop);
        }


        //大学生板块
        public ActionResult StudentsPlace()
        {
            return View();
        }

    }
}
