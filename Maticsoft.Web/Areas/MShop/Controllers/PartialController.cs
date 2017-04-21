using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Settings;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.Shop;
using CategoryInfo = Maticsoft.Model.Shop.Products.CategoryInfo;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class PartialController : MShopControllerBase
    {
        //
        // GET: /Mobile/Partial/
        private BLL.Shop.Supplier.SupplierInfo supplierBll = new BLL.Shop.Supplier.SupplierInfo();
        private BLL.Members.UsersExp UsersExpBll = new BLL.Members.UsersExp();
        private BLL.Members.Users UsersBll = new BLL.Members.Users();
        private BLL.Shop_CardUserInfo cardinfoBll = new BLL.Shop_CardUserInfo();
        

        
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Footer(string viewName = "_Footer")
        {
            ViewBag.UserID = 0;
            if (currentUser!=null)
            {
                ViewBag.usernickname = currentUser.NickName;//用户已登录
                ViewBag.UserID = currentUser.UserID;
            }
            ViewBag.OpenId=OpenId;
            ViewBag.UserOpen = UserOpen;
        
            //是否开启微信自动登录
            ViewBag.IsAutoLogin = Common.Globals.SafeBool(WeChat.BLL.Core.Config.GetValueByCache("WeChat_AutoLogin", -1, "AA"), false);
            ViewBag.IsHideMenu = Common.Globals.SafeBool(WeChat.BLL.Core.Config.GetValueByCache("WeChat_HideMenu", -1, "AA"), false);
            return PartialView(viewName);
        }

        public PartialViewResult HeadDoor(string viewName = "_HeadDoor")
        {
            string StudyPhone = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("StudyPhone");
            ViewBag.IsStudy = false;
            string userid = Maticsoft.Common.Cookies.getCookie("UserNameID", "Value");
            string ruserid = Maticsoft.Common.Cookies.getCookie("Recommend_UserNameID", "Value");
            string msg = "";
            string cardno = "";
            string baseuri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];
            var teamApiHelper = new Maticsoft.Services.TeamAPI(baseuri);
            bool flag = false;
            string TrueName = "健康微商城";
            string ShopTypeName = "总店";
            string phone = "400-021-3888";
            bool IsApp = true;
            string username = "";
            if (!string.IsNullOrWhiteSpace(userid) && userid!="0")
            {
                Maticsoft.Model.Members.UsersExpModel userinfo = UsersExpBll.GetUsersModel(Int32.Parse(userid));
                //判断当前用户
                if (userinfo != null)
                {
                    if (userinfo.UserOldType.HasValue)
                    {
                        if (userinfo.UserOldType >= 2)
                        {
                            if (userinfo.UserOldType == 22 || userinfo.Phone.Trim()==StudyPhone.Trim() )
                            {
                                ViewBag.IsStudy = true;
                            }
                            flag = true;
                            TrueName = userinfo.TrueName;
                            ShopTypeName = GetUserTypeName(userinfo.UserOldType.Value);
                            phone = userinfo.Phone;
                            IsApp = false;

                        }
                    }
                }

            }

            if (!flag)
            {
                if (!string.IsNullOrWhiteSpace(ruserid) && ruserid!="0")
                {
                    //判断推荐人是不是好代
                    Maticsoft.Model.Members.UsersExpModel userinfo = UsersExpBll.GetUsersModel(Int32.Parse(ruserid));
                    if (userinfo != null)
                    {
                        username = userinfo.UserName;
                        if (userinfo.UserOldType.HasValue)
                        {

                            if (userinfo.UserOldType >= 2)
                            {
                                if (userinfo.UserOldType == 22 || userinfo.Phone.Trim() == StudyPhone.Trim())
                                {
                                    ViewBag.IsStudy = true;
                                }

                                flag = true;
                                phone = userinfo.Phone;
                                TrueName = userinfo.TrueName;
                                ShopTypeName = GetUserTypeName(userinfo.UserOldType.Value);

                            }
                        }
                    }
                    if (!flag)
                    {

                        //推荐人的店
                        if (!string.IsNullOrWhiteSpace(username))
                        {
                            cardno = cardinfoBll.GetDefaultCardNo(username);
                            if (!string.IsNullOrWhiteSpace(cardno))
                            {
                                //好粉拿到上级的
                                Maticsoft.Model.Team.SalesPersonModel SalesPersonInfo = teamApiHelper.GetSalePersonModel(cardno);
                                if (SalesPersonInfo != null)
                                {

                                    if (SalesPersonInfo.StoreType >= 2)
                                    {
                                        if (userinfo.UserOldType == 22 || userinfo.Phone.Trim() == StudyPhone.Trim())
                                        {
                                            ViewBag.IsStudy = true;
                                        }
                                        TrueName = SalesPersonInfo.SalesName;
                                        ShopTypeName = GetUserTypeName(SalesPersonInfo.StoreType);
                                        phone = SalesPersonInfo.Mobile;

                                    }

                                }
                            }
                        }

                    }
                }
            }

            ViewBag.TrueName = TrueName;
            ViewBag.ShopTypeName = ShopTypeName;
            ViewBag.IsApp = IsApp;
            ViewBag.Phone = phone;
            return PartialView(viewName);
        }
        /// <summary>
        /// 申请的类型  0 表示会员 1 表示好粉 2 表示好代 3 分销店，4服务店,5省级旗舰店，51市级旗舰店 52县级旗舰店 ,21（VIP高级会员） ,22 大学生创业店
        /// </summary>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public string GetUserTypeName(int UserType)
        {
            string shoptypename = "会员";

            switch (UserType)
            {
                case 0:
                    shoptypename = "会员";
                    break;
                case 1:
                    shoptypename = "会员";
                    break;
                case 2:
                    shoptypename = "好店";
                    break;
                case 3:
                    shoptypename = "分销店";
                    break;
                case 4:
                    shoptypename = "服务店";
                    break;
                case 5:
                    shoptypename = "省级旗舰店";
                    break;
                case 51:
                    shoptypename = "市级旗舰店";
                    break;
                case 52:
                    shoptypename = "县级旗舰店";
                    break;
                case 21:
                    shoptypename = "好粉";
                    break;
                case 22:
                    shoptypename = "大学生创业店";
                    break;
                default:
                    shoptypename = "个人微店";
                    break;


            }
            return shoptypename;
        }
        public PartialViewResult Header(string viewName = "_Header")
        {
            ViewBag.Name = BLL.SysManage.ConfigSystem.GetValueByCache("Opertors_Name", ApplicationKeyType.Shop);
            ViewBag.MShopName = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MShop_Name");
            ViewBag.MShopLogo = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MShop_Logo");
            return PartialView(viewName);
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
            BLL.Settings.Advertisement bllAdvertisement = new Advertisement();
            Model.Settings.Advertisement model = bllAdvertisement.GetModelByAdvPositionId(AdvPositionId);
            return PartialView(viewName, model);
        }
        #endregion

        #region 菜单导航
        public PartialViewResult Navigation(string viewName = "_Navigation")
        {
            return PartialView(viewName);
        }
        #endregion 

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

        #region 推荐商品
        public PartialViewResult ProductRec(ProductRecType Type = ProductRecType.Recommend, int Cid = 0, int Top = 5, string ViewName = "_ProductRec")
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

        public PartialViewResult CategoryList(int Cid = 0, int Top = 10, string ViewName = "_CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> parentList = cateList.Where(c => c.Depth == 1).OrderBy(c=>c.DisplaySequence).ToList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos=new List<CategoryInfo>();//= cateList.Where(c => c.ParentCategoryId == Cid).ToList();
            if (parentList.Count > 0)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> sonList;
                foreach (var item in parentList)
                {
                    sonList = cateList.Where(c => c.ParentCategoryId == item.CategoryId).OrderBy(c=>c.DisplaySequence).ToList();
                    categoryInfos.Add(item);
                    categoryInfos.AddRange(sonList);
                }
            }
            return PartialView(ViewName, categoryInfos);
        }

        #region  商品分类
        public PartialViewResult CateList(int parentId = 0, string viewName = "_CateList")
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
            return PartialView(viewName, categoryInfos);
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
            List<Model.Shop.Supplier.SupplierInfo> list = supplierBll.GetList(top, rec);
            return PartialView(viewName, list);
        }
        public ActionResult SuppLogo(int id = 0, string size = "")
        {
            string pathName = string.Format("/Upload/Supplier/Logo/{0}_{1}", id, size);
            return File(pathName, "application/x-img");
        }

        /// <summary>
        /// 获取商家名称 (目前只有订餐模版使用)
        /// </summary>
        /// <returns></returns>
        public static string GetName()
        {
           return   BLL.SysManage.ConfigSystem.GetValueByCache("Opertors_Name", ApplicationKeyType.Shop);
        }
    }
}
