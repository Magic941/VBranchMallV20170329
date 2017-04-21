using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Shop.Order;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Json;
using Maticsoft.Model.Shop;
using Maticsoft.Model.Shop.Order;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using Maticsoft.BLL.Members;
using System.IO;
using Maticsoft.BLL.SysManage;
using Maticsoft.Services;
using Maticsoft.Json.Conversion;
using System.Data;
using System.Web.Security;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class UserCenterController : MShopControllerBaseUser
    {
        //
        // GET: /Mobile/UserCenter/
        private BLL.Members.PointsDetail detailBll = new BLL.Members.PointsDetail();
        private BLL.Members.SiteMessage bllSM = new BLL.Members.SiteMessage();
        private BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
        private BLL.Members.Users userBll = new BLL.Members.Users();
        private readonly BLL.Shop.Gift.ExchangeDetail exchangeBll = new BLL.Shop.Gift.ExchangeDetail();
        private readonly BLL.Shop.Coupon.CouponInfo infoBll = new BLL.Shop.Coupon.CouponInfo();
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new BLL.Shop.Order.OrderAction();
        private readonly BLL.Pay.BalanceDetails balanDetaBll = new BLL.Pay.BalanceDetails();
        private readonly Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new Maticsoft.BLL.Shop.Coupon.CouponRule();
        private Maticsoft.BLL.Members.UserCard cardBll = new BLL.Members.UserCard();
        private Maticsoft.BLL.Team.SalesInfo teamBll = new Maticsoft.BLL.Team.SalesInfo();
        private Maticsoft.BLL.Shop.Order.OrderItems orderItemBll = new BLL.Shop.Order.OrderItems();
        private Maticsoft.BLL.Shop.Order.Orders ordersBll = new Orders();
        private readonly BLL.Shop.Order.OrderReturnGoods returnGoodsBLL = new BLL.Shop.Order.OrderReturnGoods();
        private readonly BLL.Shop.Order.OrderReturnGoodsItem returnGoodsItemBLL = new BLL.Shop.Order.OrderReturnGoodsItem();
        private readonly BLL.Shop.Order.OrderItems itemBll = new BLL.Shop.Order.OrderItems();
        private readonly Orders _orderManage = new Orders();


        private readonly BLL.Ms.EntryForm bll = new BLL.Ms.EntryForm();
        private readonly BLL.Members.UsersExp expbll = new UsersExp();


        public ActionResult Index()
        {
            Maticsoft.Model.Members.UsersExpModel usersModel = userEXBll.GetUsersModel(CurrentUser.UserID);

            if (usersModel != null)
            {
                // Maticsoft.BLL.Members.SiteMessage msgBll = new BLL.Members.SiteMessage();
                // ViewBag.privatecount = msgBll.GetReceiveMsgNotReadCount(currentUser.UserID, -1);//未读私信的条数
                // Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
                // ViewBag.Unpaid = orderBll.GetPaymentStatusCounts(CurrentUser.UserID, (int)EnumHelper.PaymentStatus.Unpaid);//未支付订单数
                #region SEO 优化设置
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title = "个人中心";//+ pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion
                //优惠劵数量
                int toalCount = infoBll.GetRecordCount(String.Format(" UserID={0} and Status={1}", currentUser.UserID, 1));
                ViewBag.CouponTotalCount = toalCount;

                //订单未付款获取总条数
                Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
                string where = " BuyerID=" + CurrentUser.UserID + " AND OrderType=1 and OrderOptType=1 ";
                where = where + string.Format(" and PaymentStatus={0} and OrderStatus={1} ", (int)Maticsoft.Model.Shop.Order.EnumHelper.PaymentStatus.Unpaid, (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.UnHandle);

                int OrderNoPaytoalCount = orderBll.GetRecordCount(where);
                ViewBag.OrderNoPaytoalCount = OrderNoPaytoalCount;

                ViewBag.MyQRCode = "http://app.zhenhaolin.com/MShop" + currentUser.UserID + "/a/r?RUserNameID=" + currentUser.UserID;
                ViewBag.UID = currentUser.UserID;
                return View("IndexNew", usersModel);
            }
            return Redirect(ViewBag.BasePath + "a/l");
        }

        #region 用户个人资料

        public ActionResult Personal()
        {

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "个人资料";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (null != model)
            {
                return View(model);
            }
            return Redirect(ViewBag.BasePath + "a/l");
            // return RedirectToAction("Login", "Account", new { id = 1, viewname = "url" });//去登录
        }
        #endregion 用户个人资料

        #region 用户密码

        public ActionResult ChangePassword()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "修改密码"; //+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        #endregion 用户密码

        #region 检查用户原密码

        /// <summary>
        ///检查用户原密码
        /// </summary>
        [HttpPost]
        public void CheckPassword(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            string password = collection["Password"];
            if (!string.IsNullOrWhiteSpace(password))
            {
                SiteIdentity SID = new SiteIdentity(CurrentUser.UserName);
                if (SID.TestPassword(password.Trim()) == 0)
                {
                    json.Accumulate("STATUS", "ERROR");
                }
                else
                {
                    json.Accumulate("STATUS", "OK");
                }
            }
            else
            {
                json.Accumulate("STATUS", "UNDEFINED");
            }
            Response.Write(json.ToString());
        }

        #endregion 检查用户原密码

        #region 更新用户密码

        /// <summary>
        /// 更新用户密码
        /// </summary>
        [HttpPost]
        public void UpdateUserPassword(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            string newpassword = collection["NewPassword"];
            string confirmpassword = collection["ConfirmPassword"];
            if (!string.IsNullOrWhiteSpace(newpassword) && !string.IsNullOrWhiteSpace(confirmpassword))
            {
                if (newpassword.Trim() != confirmpassword.Trim())
                {
                    json.Accumulate("STATUS", "FAIL");
                }
                else
                {
                    currentUser.Password = AccountsPrincipal.EncryptPassword(newpassword);
                    if (currentUser.Update())
                    {
                        json.Accumulate("STATUS", "UPDATESUCC");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "UPDATEFAIL");
                    }
                }
            }
            else
            {
                json.Accumulate("STATUS", "UNDEFINED");
            }
            Response.Write(json.ToString());
        }

        #endregion 更新用户密码

        #region 更新用户信息

        /// <summary>
        /// 更新用户信息
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public void UpdateUserInfo(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
            model.TelPhone = collection["TelPhone"];
            string birthday = collection["Birthday"];
            if (!string.IsNullOrWhiteSpace(birthday) && PageValidate.IsDateTime(birthday))
            {
                model.Birthday = Globals.SafeDateTime(birthday, DateTime.Now);
            }
            else
            {
                model.Birthday = null;
            }
            model.Constellation = collection["Constellation"]; //星座
            model.PersonalStatus = collection["PersonalStatus"]; //职业
            model.Singature = collection["Singature"];
            model.Address = collection["Address"];
            User currentUser = new Maticsoft.Accounts.Bus.User(CurrentUser.UserID);
            currentUser.Sex = collection["Sex"];
            currentUser.Email = collection["Email"];
            currentUser.NickName = collection["NickName"];
            currentUser.Phone = collection["Phone"];
            currentUser.TrueName = collection["TrueName"];
            if (currentUser.Update() && userEXBll.UpdateUsersExp(model))
            {
                json.Accumulate("STATUS", "SUCC");
            }
            else
            {
                json.Accumulate("STATUS", "FAIL");
            }
            Response.Write(json.ToString());
        }

        #endregion 更新用户信息

        #region 检查用户输入的昵称是否被其他用户使用

        /// <summary>
        ///检查用户输入的昵称是否被其他用户使用
        /// </summary>
        [HttpPost]
        public void CheckNickName(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                string nickname = collection["NickName"];
                if (!string.IsNullOrWhiteSpace(nickname))
                {
                    BLL.Members.Users bll = new BLL.Members.Users();
                    if (bll.ExistsNickName(CurrentUser.UserID, nickname))
                    {
                        json.Accumulate("STATUS", "EXISTS");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "OK");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "NOTNULL");
                }
                Response.Write(json.ToString());
            }
            else
            {
                json.Accumulate("STATUS", "NOTNULL");
                Response.Write(json.ToString());
            }
        }

        #endregion 检查用户输入的昵称是否被其他用户使用

        #region 检查用户输入的昵称是否存在

        /// <summary>
        ///检查用户输入的昵称是否存在
        /// </summary>
        [HttpPost]
        public void ExistsNickName(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
                string nickname = collection["NickName"];
                if (!string.IsNullOrWhiteSpace(nickname))
                {
                    BLL.Members.Users bll = new BLL.Members.Users();
                    if (bll.ExistsNickName(nickname))
                    {
                        json.Accumulate("STATUS", "EXISTS");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NOTEXISTS");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "NOTNULL");
                }
                Response.Write(json.ToString());
            }
        }

        #endregion 检查用户输入的昵称是否存在

        #region 积分明细

        public ActionResult PointsDetail(int pageIndex = 1)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "积分明细";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            //首页用户数据
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (userEXModel != null)
            {
                ViewBag.UserInfo = userEXModel.Points.HasValue ? userEXModel.Points : 0;
                ViewBag.NickName = userEXModel.NickName;
            }
            int _pageSize = 8;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = detailBll.GetRecordCount(" UserID=" + CurrentUser.UserID);
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
            List<Maticsoft.Model.Members.PointsDetail> detailList = detailBll.GetListByPageEX("UserID=" + CurrentUser.UserID, "", startIndex, endIndex);
            if (detailList != null && detailList.Count > 0)
            {
                foreach (var item in detailList)
                {
                    item.RuleName = GetRuleName(item.RuleId);
                }
            }
            PagedList<Maticsoft.Model.Members.PointsDetail> lists = new PagedList<Maticsoft.Model.Members.PointsDetail>(detailList, pageIndex, _pageSize, toalCount);
            return View(lists);
        }

        public string GetRuleName(int RuleId)
        {

            Maticsoft.BLL.Members.PointsRule ruleBll = new BLL.Members.PointsRule();
            return ruleBll.GetRuleName(RuleId);
        }

        #endregion 积分明细

        #region 会员卡
        public ActionResult UserCard(int pageIndex = 1, string viewName = "UserCard")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "会员卡";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            //首页用户数据
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (userEXModel != null)
            {
                ViewBag.UserInfo = userEXModel;
                Maticsoft.Model.Members.UserCard cardModel = cardBll.GetModel(userEXModel.UserCardCode);
                ViewBag.Status = cardModel == null ? -1 : cardModel.Status;
            }

            int _pageSize = 8;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = balanDetaBll.GetRecordCount(" UserId =" + CurrentUser.UserID);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Pay.BalanceDetails> list = balanDetaBll.GetListByPage(" UserId = " + CurrentUser.UserID, startIndex, endIndex);
            PagedList<Maticsoft.Model.Pay.BalanceDetails> lists = new PagedList<Maticsoft.Model.Pay.BalanceDetails>(list, pageIndex, _pageSize, toalCount);
            return View(lists);
        }

        [HttpPost]
        public ActionResult GetUserCard()
        {

            if (cardBll.AddCard(currentUser.UserID))
            {
                return Content("True");
            }
            return Content("False");
        }

        #endregion

        #region 积分兑换明细
        /// <summary>
        /// 积分兑换明细
        /// </summary>
        /// <param name="p">pageIndex</param>
        /// <returns></returns>
        public ActionResult Exchanges(int p = 1)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "积分兑换明细" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            //首页用户数据
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (userEXModel != null)
            {
                ViewBag.UserInfo = userEXModel;
            }
            int _pageSize = 15;

            //计算分页起始索引
            int startIndex = p > 1 ? (p - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = p * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = exchangeBll.GetRecordCount(" UserID=" + CurrentUser.UserID);
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
            List<Maticsoft.Model.Shop.Gift.ExchangeDetail> detailList = exchangeBll.GetListByPageEX("UserID=" + CurrentUser.UserID, " CreatedDate desc", startIndex, endIndex);

            PagedList<Maticsoft.Model.Shop.Gift.ExchangeDetail> lists = new PagedList<Maticsoft.Model.Shop.Gift.ExchangeDetail>(detailList, p, _pageSize, toalCount);
            return View(lists);
        }


        public PartialViewResult CouponRule(int top = 4, string viewName = "_CouponRule")
        {
            List<Maticsoft.Model.Shop.Coupon.CouponRule> ruleList = ruleBll.GetModelList(" Type=1 and Status=1");
            return PartialView(viewName, ruleList);
        }
        [HttpPost]
        public ActionResult AjaxExchange(int RuleId)
        {
            Maticsoft.Model.Shop.Coupon.CouponRule ruleModel = ruleBll.GetModel(RuleId);
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (ruleModel == null)
            {
                return Content("False");
            }
            if (ruleModel.NeedPoint > userEXModel.Points)
            {
                return Content("NoPoints");
            }
            if (ruleBll.GenCoupon(ruleModel, currentUser.UserID))
            {
                return Content("True");
            }
            return Content("False");
        }

        #endregion 积分兑换明细

        #region 我的优惠券

        public ActionResult MyCoupon(int p = 1)
        {
            int status = Common.Globals.SafeInt(Request.Params["s"], 1);

            int _pageSize = 15;

            //计算分页起始索引
            int startIndex = p > 1 ? (p - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = p * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = infoBll.GetRecordCount(String.Format(" UserID={0} and Status={1} and UseType={2}", currentUser.UserID, status, 1));
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoList = infoBll.GetListByPageEX(String.Format(" UserID={0} and Status={1}  and UseType={2} ", currentUser.UserID, status, 1), " GenerateTime desc", startIndex, endIndex);
            Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new Maticsoft.BLL.Shop.Coupon.CouponClass();
            foreach (var Info in infoList)
            {
                Maticsoft.Model.Shop.Coupon.CouponClass classModel = classBll.GetModelByCache(Info.ClassId);
                Info.ClassName = classModel == null ? "" : classModel.Name;
            }
            PagedList<Maticsoft.Model.Shop.Coupon.CouponInfo> lists = new PagedList<Maticsoft.Model.Shop.Coupon.CouponInfo>(infoList, p, _pageSize, toalCount);
            return View(lists);
        }


        #endregion

        #region 我的好粉
        [HttpGet]
        public ActionResult MyFans(int pageIndex = 1, string viewName = "MyFans")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的好粉";
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            List<Maticsoft.Model.Team.AppMember> AppMemberlsit = null;
            if (CurrentUser.UserID > 0)
            {
                Maticsoft.BLL.CallApi Api = new BLL.CallApi();

                int _pageSize = 10;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;

                //分页结果
                int totalcount = 0;

                //计算的总数,通过电话获取粉丝 20141215
                AppMemberlsit = Api.getMyMembers(CurrentUser.Phone, ref totalcount, pageIndex, _pageSize);

                ViewBag.Count = totalcount;

                PagedList<Maticsoft.Model.Team.AppMember> lists = new PagedList<Maticsoft.Model.Team.AppMember>(AppMemberlsit, pageIndex, _pageSize, totalcount);

                return View(viewName, lists);
            }
            else
            {
                return Redirect(ViewBag.BasePath + "l/a");
            }
        }

        [HttpPost]
        public ActionResult MyFans(string mobile, int pageIndex = 1, string viewName = "MyFans")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的好粉";
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            List<Maticsoft.Model.Team.AppMember> AppMemberlsit = null;
            if (CurrentUser.UserID > 0)
            {
                Maticsoft.BLL.CallApi Api = new BLL.CallApi();

                int _pageSize = 10;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;

                //分页结果
                int totalcount = 0;

                //计算的总数,通过电话获取粉丝 20141215
                AppMemberlsit = Api.getMyMembers(CurrentUser.Phone, ref totalcount, 0, 0).Where(l => l.Mobel.Contains(mobile.Trim())).ToList();

                ViewBag.Count = AppMemberlsit.Count;

                AppMemberlsit = AppMemberlsit.Skip(startIndex).Take(endIndex - startIndex).ToList();

                PagedList<Maticsoft.Model.Team.AppMember> lists = new PagedList<Maticsoft.Model.Team.AppMember>(AppMemberlsit, pageIndex, _pageSize, totalcount);

                return View(viewName, lists);
            }
            else
            {
                return Redirect(ViewBag.BasePath + "l/a");
            }
        }

        [HttpGet]
        public ActionResult MyFansList(int pageIndex = 1, string viewName = "MyFansList")
        {
            List<Maticsoft.Model.Team.AppMember> AppMemberlsit = null;
            if (CurrentUser.UserID > 0)
            {
                Maticsoft.BLL.CallApi Api = new BLL.CallApi();

                int _pageSize = 10;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;

                //分页结果
                int totalcount = 0;

                //计算的总数,通过电话获取粉丝 20141215
                AppMemberlsit = Api.getMyMembers(CurrentUser.Phone, ref totalcount, pageIndex, _pageSize);

                ViewBag.Count = totalcount;

                PagedList<Maticsoft.Model.Team.AppMember> lists = new PagedList<Maticsoft.Model.Team.AppMember>(AppMemberlsit, pageIndex, _pageSize, totalcount);

                return PartialView(viewName, lists);
            }
            else
            {
                return Redirect(ViewBag.BasePath + "l/a");
            }
        }

        [HttpPost]
        public ActionResult MyFansList(string mobile,int pageIndex = 1, string viewName = "MyFansList")
        {
            List<Maticsoft.Model.Team.AppMember> AppMemberlsit = null;
            if (CurrentUser.UserID > 0)
            {
                Maticsoft.BLL.CallApi Api = new BLL.CallApi();

                int _pageSize = 10;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;

                //分页结果
                int totalcount = 0;

                //计算的总数,通过电话获取粉丝 20141215
                AppMemberlsit = Api.getMyMembers(CurrentUser.Phone, ref totalcount, pageIndex, _pageSize);

                ViewBag.Count = totalcount;

                PagedList<Maticsoft.Model.Team.AppMember> lists = new PagedList<Maticsoft.Model.Team.AppMember>(AppMemberlsit, pageIndex, _pageSize, totalcount);

                return PartialView(viewName, lists);
            }
            else
            {
                return Redirect(ViewBag.BasePath + "l/a");
            }
        }
        #endregion

        #region 我的业绩
        public ActionResult MyPerfor(string viewName = "MyPerfor")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的业绩";
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }

        public PartialViewResult MyPerformance(int pageIndex = 1, string viewName = "MyPerformance")
        {
            List<Maticsoft.Model.Team.SalesPersonIncome> MyInComerlsit = null;
            if (CurrentUser.UserID > 0)
            {
                Maticsoft.BLL.CallApi Api = new BLL.CallApi();
                int _pageSize = 10;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;

                //分页结果
                int totalcount = 0;

                //计算的总数
                string baseuri = System.Configuration.ConfigurationManager.AppSettings["B2BURL"];
                var teamApiHelper = new TeamAPI(baseuri);

                decimal totalprice = 0.00m;
                decimal totalnopay = 0.00m;
                decimal totalpay = 0.00m;
                MyInComerlsit = teamApiHelper.GetMyachievements(ref totalcount, " and MySalesPersonMobile='" + currentUser.Phone + "'", pageIndex, _pageSize, out totalprice,out totalnopay,out totalpay);

                ViewBag.Count = totalcount;
                ViewBag.Totalnopay = totalnopay;
                ViewBag.Totalpay = totalpay;
                ViewBag.TotalPrice = totalprice.ToString("F");

                PagedList<Maticsoft.Model.Team.SalesPersonIncome> lists = new PagedList<Maticsoft.Model.Team.SalesPersonIncome>(MyInComerlsit, pageIndex, _pageSize, totalcount);

                return PartialView(viewName, lists);
            }
            else
            {
                return Redirect(ViewBag.BasePath + "l/a");
            }
        }
        #endregion

        #region 发站内信

        /// <summary>
        /// 发站内信
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMessage()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "发信息";//+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            ViewBag.Name = Request.Params["name"];
            return View("SendMessage");
        }

        #endregion 发站内信

        #region 发送站内信息

        /// <summary>
        /// 发送站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void SendMsg(FormCollection collection)
        {

            JsonObject json = new JsonObject();
            string nickname = Common.InjectionFilter.Filter(collection["NickName"]);
            string title = Common.InjectionFilter.Filter(collection["Title"]);
            string content = Common.InjectionFilter.Filter(collection["Content"]);
            if (string.IsNullOrWhiteSpace(nickname))
            {
                json.Accumulate("STATUS", "NICKNAMENULL");
            }
            else if (string.IsNullOrWhiteSpace(title))
            {
                json.Accumulate("STATUS", "TITLENULL");
            }
            else if (string.IsNullOrWhiteSpace(content))
            {
                json.Accumulate("STATUS", "CONTENTNULL");
            }
            else
            {
                BLL.Members.Users bll = new BLL.Members.Users();
                if (bll.ExistsNickName(nickname))
                {
                    int ReceiverID = bll.GetUserIdByNickName(nickname);
                    Maticsoft.Model.Members.SiteMessage modeSiteMessage = new Maticsoft.Model.Members.SiteMessage();
                    modeSiteMessage.Title = title;
                    modeSiteMessage.Content = content;
                    modeSiteMessage.SenderID = CurrentUser.UserID;
                    modeSiteMessage.ReaderIsDel = false;
                    modeSiteMessage.ReceiverIsRead = false;
                    modeSiteMessage.SenderIsDel = false;
                    modeSiteMessage.ReceiverID = ReceiverID;
                    modeSiteMessage.SendTime = DateTime.Now;
                    if (bllSM.Add(modeSiteMessage) > 0)
                    {
                        json.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "NICKNAMENOTEXISTS");
                }
            }
            Response.Write(json.ToString());
        }

        #endregion 发送站内信息

        #region 回复站内信息

        /// <summary>
        /// 回复站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void ReplyMsg(int ReceiverID, string Title, string Content)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.SiteMessage modeSiteMessage = new Maticsoft.Model.Members.SiteMessage();
            modeSiteMessage.Title = Title;
            modeSiteMessage.Content = Content;
            modeSiteMessage.SenderID = CurrentUser.UserID;
            modeSiteMessage.ReaderIsDel = false;
            modeSiteMessage.ReceiverIsRead = false;
            modeSiteMessage.SenderIsDel = false;
            modeSiteMessage.ReceiverID = ReceiverID;
            modeSiteMessage.SendTime = DateTime.Now;
            if (bllSM.Add(modeSiteMessage) > 0)
                json.Accumulate("STATUS", "SUCC");
            else
                json.Accumulate("STATUS", "FAIL");
            Response.Write(json.ToString());
        }

        #endregion 回复站内信息

        #region 删除站内信息

        /// <summary>
        /// 删除收到的站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void DelReceiveMsg(int MsgID)
        {
            JsonObject json = new JsonObject();
            if (bllSM.SetReceiveMsgToDelById(MsgID) > 0)
                json.Accumulate("STATUS", "SUCC");
            else
                json.Accumulate("STATUS", "FAIL");
            Response.Write(json.ToString());
        }

        #endregion 删除站内信息

        #region 读取站内信息
        /// <summary>
        /// 读取站内信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ReadMsg(int? MsgID)
        {
            if (MsgID.HasValue)
            {
                Model.Members.SiteMessage siteModel = bllSM.GetModelByCache(MsgID.Value);
                if (siteModel != null &&
                    ((siteModel.ReceiverID.HasValue && siteModel.ReceiverID.Value == currentUser.UserID) ||
                     (siteModel.SenderID.HasValue && siteModel.SenderID.Value == currentUser.UserID)))
                {
                    if (siteModel.SenderID == -1)
                        siteModel.SenderUserName = "管理员";//senderid为-1 的消息是管理员所发
                    else
                    {
                        Model.Members.UsersExpModel userexpmodel = null;
                        if (siteModel.SenderID.HasValue)
                            userexpmodel = userEXBll.GetUsersExpModelByCache(siteModel.SenderID.Value);//得到发送者的昵称
                        if (userexpmodel != null)
                            siteModel.SenderUserName = userexpmodel.NickName;
                    }



                    if (siteModel.ReceiverIsRead == false)
                        bllSM.SetReceiveMsgAlreadyRead(siteModel.ID);//如果是消息状态是未读的，则改变消息状态
                    return View(siteModel);
                }
            }
            return RedirectToAction("Inbox", "UserCenter");
        }
        #endregion 读取站内信息

        #region 收件箱

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Inbox(int page = 1)
        {
            ViewBag.PageIndex = page;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "收件箱";//+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public PartialViewResult InboxList(int? page, string viewName = "_InboxList")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "发件箱";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            ViewBag.inboxpage = page;
            int pagesize = 10;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllReceiveMsgListByMvcPage(CurrentUser.UserID, pagesize, page.Value);
            //foreach (Maticsoft.Model.Members.SiteMessage item in list)
            //{
            //    if (item.ReceiverIsRead == false)
            //    {
            //        bllSM.SetReceiveMsgAlreadyRead(item.ID);
            //    }
            //}
            if (Request.IsAjaxRequest())
                return PartialView(viewName, list);
            return PartialView(viewName, list);
        }

        #endregion 收件箱

        #region 发件箱

        /// <summary>
        /// 发件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Outbox(int? page)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "发件箱" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            int pagesize = 8;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSendMsgListByMvcPage(CurrentUser.UserID, pagesize, page.Value);
            if (Request.IsAjaxRequest())
                return PartialView("_OutboxList", list);
            return View("OutBox", list);
        }

        #endregion 签到

        #region 申请好服务店
        public ActionResult ApplyShop(string viewName = "ApplyShop")
        {
            Maticsoft.Model.Members.Users model = userBll.GetModel(CurrentUser.UserID);
            ViewBag.ApplyType = int.Parse(Request.QueryString["ApplyType"]);
            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult SubmitEntryForm(FormCollection fm)
        {
            #region  微信用户名
            //string cookieValue = "entryform";
            //if (Session["WeChat_UserName"] != null)
            //{
            //    cookieValue += "_" + Session["WeChat_UserName"].ToString();
            //}
            #endregion

            //if (Request.Cookies["entry"] != null)
            //{
            //    if (cookieValue == Request.Cookies["entry"].Values["entry"])
            //    {
            //        return Content("isnotnull");//ERROR  "您已经报申请过健康服务店，请不要重复申请！ 
            //    }
            //}
            if (currentUser == null)
            {
                return Redirect(ViewBag.BasePath + "a/l");
            }
           
            string TrueName = Common.InjectionFilter.SqlFilter(fm["TrueName"]);
            string Phone = Common.InjectionFilter.SqlFilter(fm["Phone"]);
            int region = Common.Globals.SafeInt(fm["Region"], -1);
            string houseaddress = Common.InjectionFilter.SqlFilter(fm["Houseaddress"]);
            string Remark = Common.InjectionFilter.SqlFilter(fm["Remark"]);
            int ApplyType = Common.Globals.SafeInt(fm["state"], 0);

            Model.Ms.EntryForm model = new Model.Ms.EntryForm();
            Model.Members.UsersExpModel expmodel = new Model.Members.UsersExpModel();
            Model.Members.Users user =  userBll.GetModel(CurrentUser.UserID);
            if (region > 0)
            {
                expmodel.RegionId = region;
            }
            if (ApplyType == 3)
            {
                expmodel.UserAppType = 3;//申请的类型(分销店)
            }
            else if (ApplyType == 4)
            {
                expmodel.UserAppType = 4;//申请的类型(服务店)
            }
            expmodel.UserStatus = 0;//处理中
            expmodel.IsAppUserType = true; /// 申请单

            expmodel.UserID = CurrentUser.UserID;
            expmodel.UserAppType = ApplyType;//申请健康店类型 2 好代 3 分销店，4 服务店
            user.TrueName = TrueName;
            user.UserID = CurrentUser.UserID;
            expmodel.TelPhone = Phone;
            expmodel.Address = houseaddress;
            expmodel.Remark = Remark;
            expmodel.IsAppUserType = true;

            if (expmodel != null && user !=null)
            {
                if (expbll.UpdateApplyAgent(expmodel) && userBll.UpdateApplyAgentHao(user))
                {
                    return Content("true");
                }
            }
            return Content("true");
        }

        #endregion

        #region 申请好代
        public ActionResult ApplyAgentHao()
        {
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {

                Model.Members.UsersExpModel model = userEXBll.GetUsersExpInfo(CurrentUser.UserID);
                if (model != null)
                {
                    Maticsoft.Model.Members.Users user = userBll.GetModel(CurrentUser.UserID);

                    model.TrueName = user.TrueName;
                    model.Email = user.Email;
                    model.Phone = user.Phone;

                    if (user.Sex != null)
                    {
                        model.Sex = user.Sex.Trim();
                    }
                    model.UserID = user.UserID;
                    model.UserName = user.UserName;
                    model.UserType = user.UserType;
                    model.CardId = user.CardId;
                    if (null != model)
                    {
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "UserCenter");
                }
            }
            return RedirectToAction("Login", "Account");//去登录
        }

        /// <summary>
        /// 点击提交 申请好代
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateForm(FormCollection fm)
        {
            #region  微信用户名
            string cookieValue = "updateform";
            if (Session["WeChat_UserName"] != null)
            {
                cookieValue += "_" + Session["WeChat_UserName"].ToString();
            }
            #endregion

            //if (Request.Cookies["entry"] != null)
            //{
            //    if (cookieValue == Request.Cookies["entry"].Values["entry"])
            //    {
            //        return Content("isnotnull");//ERROR  "您已经报申请过好代，请不要重复申请！ 
            //    }
            //}

            string TrueName = Common.InjectionFilter.SqlFilter(fm["TrueName"]);
            string Email = Common.InjectionFilter.SqlFilter(fm["Email"]);
            string TelPhone = Common.InjectionFilter.SqlFilter(fm["TelPhone"]);
            string Phone = Common.InjectionFilter.SqlFilter(fm["Phone"]);
            int Region = Common.Globals.SafeInt(fm["RegionId"], -1);
            //string QQ = Common.InjectionFilter.SqlFilter(fm["QQ"]);
            string Address = Common.InjectionFilter.SqlFilter(fm["Address"]);
            string Sex = Common.InjectionFilter.SqlFilter(fm["Sex"]);
            string Remark = Common.InjectionFilter.SqlFilter(fm["Remark"]);
            string CardId = Common.InjectionFilter.SqlFilter(fm["CardId"]);
            int UserId = currentUser.UserID;
            Model.Members.Users usermodel = new Model.Members.Users();
            Model.Members.UsersExpModel userexp = new Model.Members.UsersExpModel();

            usermodel.CardId = CardId;
            usermodel.TrueName = TrueName;
            usermodel.Email = Email;
            usermodel.Phone = Phone;
            usermodel.Sex = Sex;
            usermodel.UserID = UserId;

            if (Region > 0)
            {
                userexp.RegionId = Region;
            }

            userexp.UserAppType = 2;//申请的类型(好代)
            userexp.UserStatus = 0;//处理中
            userexp.IsAppUserType = true; /// 申请单
            userexp.CardId = CardId;
            userexp.Email = Email;
            userexp.TelPhone = TelPhone;
            userexp.Phone = Phone;
            userexp.Address = Address;
            //userexp.QQ
            userexp.Remark = Remark;
            userexp.Sex = Sex;
            userexp.TrueName = TrueName;
            userexp.UserID = UserId;
            if (userEXBll.UpdateApplyAgent(userexp) && userBll.UpdateApplyAgentHao(usermodel))
            {
                //HttpCookie httpCookie = new HttpCookie("entry");
                //httpCookie.Values.Add("entry", cookieValue);
                //httpCookie.Expires = DateTime.Now.AddHours(240);
                //Response.Cookies.Add(httpCookie);
                return Content("true");
            }
            return Content("false");
        }
        #endregion

        #region 申请好代流程图
        /// <summary>
        /// 申请好代流程图
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyAgent(string ViewName = "ApplyAgent")
        {
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                Model.Members.UsersExpModel exp = userEXBll.GetUsersExpModel(currentUser.UserID);
                return View(exp);
            }
            return View(ViewName);
        }
        #endregion

        #region 用户签到
        public ActionResult SignPoint(int pageIndex = 1)
        {
            ViewBag.CanSign = true;
            string isEnable = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PointEnable");
            if (isEnable != "true")
            {
                ViewBag.CanSign = false;
            }
            PointsRule ruleBll = new PointsRule();
            Model.Members.PointsRule ruleModel = ruleBll.GetModel(10, CurrentUser.UserID);
            if (ruleModel == null)
            {
                ViewBag.CanSign = false;
            }
            if (detailBll.isLimit(ruleModel, CurrentUser.UserID))
            {
                ViewBag.CanSign = false;
            }

            //首页用户数据
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (userEXModel != null)
            {
                ViewBag.Points = userEXModel.Points.HasValue ? userEXModel.Points : 0;
                ViewBag.NickName = userEXModel.NickName;
            }
            int _pageSize = 8;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;
            //获取总条数
            toalCount = detailBll.GetSignCount(CurrentUser.UserID);
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
            List<Maticsoft.Model.Members.PointsDetail> detailList = detailBll.GetSignListByPage(CurrentUser.UserID, "", startIndex, endIndex);
            if (detailList != null && detailList.Count > 0)
            {
                foreach (var item in detailList)
                {
                    item.RuleName = GetRuleName(item.RuleId);
                }
            }
            PagedList<Maticsoft.Model.Members.PointsDetail> lists = new PagedList<Maticsoft.Model.Members.PointsDetail>(detailList, pageIndex, _pageSize, toalCount);
            return View(lists);
        }

        public ActionResult AjaxSign()
        {
            string isEnable = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PointEnable");
            if (isEnable != "true")
            {
                return Content("Enable");
            }
            PointsRule ruleBll = new PointsRule();
            Model.Members.PointsRule ruleModel = ruleBll.GetModel(10, CurrentUser.UserID);
            if (ruleModel == null)
            {
                return Content("NoRule");
            }
            if (detailBll.isLimit(ruleModel, CurrentUser.UserID))
            {
                return Content("Limit");
            }
            int points = detailBll.AddPoints(10, CurrentUser.UserID, "签到加积分");
            return Content(points.ToString());
        }
        #endregion

        #region 订单列表
        public ActionResult Orders(string viewName = "Orders", string type = "all")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的订单-订单明细";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            ViewBag.orderType = type;
            #endregion
            return View(viewName);
        }
        public PartialViewResult OrderList(string type = "all", int pageIndex = 1, string viewName = "_OrderList")
        {
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
            Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();

            int _pageSize = 8;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            string where = "OrderOptType=1 and  BuyerID=" + CurrentUser.UserID +
#if true //方案二 统一提取主订单, 然后加载子订单信息 在View中根据订单支付状态和是否有子单对应展示
                //主订单
                                           " AND OrderType=1";
#else   //方案一 提取数据时 过滤主/子单数据 View中无需对应 [由于不够灵活此方案作废]
                    //主订单 无子订单
                    " AND ((OrderType = 1 AND HasChildren = 0) " +
                    //子订单 已支付 或 货到付款/银行转账 子订单
                    "OR (OrderType = 2 AND (PaymentStatus > 1 OR (PaymentGateway='cod' OR PaymentGateway='bank')) ) " +
                    //主订单 有子订单 未支付的主订单 非 货到付款/银行转账 子订单
                    "OR (OrderType = 1 AND HasChildren = 1 AND PaymentStatus < 2 AND PaymentGateway<>'cod' AND PaymentGateway<>'bank'))";
#endif

            if (type == "nopay")
            {
                where = where + string.Format(" and PaymentStatus={0} and OrderStatus={1} ", (int)Maticsoft.Model.Shop.Order.EnumHelper.PaymentStatus.Unpaid, (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.UnHandle);
            }
            //获取总条数
            toalCount = orderBll.GetRecordCount(where);
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<OrderInfo> orderList = orderBll.GetListByPageEX(where, "", startIndex, endIndex);
            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderInfo item in orderList)
                {
                    //有子订单 已支付 或 货到付款/银行转账 子订单 - 加载子单
                    if (item.HasChildren && (item.PaymentStatus > 1 || (item.PaymentGateway == "cod" || item.PaymentGateway == "bank")))
                    {
                        item.SubOrders = orderBll.GetModelList(" ParentOrderId=" + item.OrderId);
                        item.SubOrders.ForEach(
                            info => info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId));
                        
                    }
                    else
                    {
                      item.OrderItems = itemBll.GetModelList(" OrderId=" + item.OrderId);
                    }
                }
            }

            ViewBag.Count = toalCount;
            PagedList<Maticsoft.Model.Shop.Order.OrderInfo> lists = new PagedList<Maticsoft.Model.Shop.Order.OrderInfo>(orderList, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="sps">shippingStatus</param>
        /// <param name="pas">paymentStatus</param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult GetListFromType(int pageIndex = 1, int sps = -1, int pas = -1, string viewName = "_OrderList")
        {
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
            Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();

            int _pageSize = 8;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            string where = " BuyerID=" + CurrentUser.UserID +
#if true //方案二 统一提取主订单, 然后加载子订单信息 在View中根据订单支付状态和是否有子单对应展示
                //主订单
                           " AND OrderType=1 and OrderOptType=1 ";
#else   //方案一 提取数据时 过滤主/子单数据 View中无需对应 [由于不够灵活此方案作废]
                           //主订单 无子订单
                           " AND ((OrderType = 1 AND HasChildren = 0) " +
                           //子订单 已支付 或 货到付款/银行转账 子订单
                           "OR (OrderType = 2 AND (PaymentStatus > 1 OR (PaymentGateway='cod' OR PaymentGateway='bank')) ) " +
                           //主订单 有子订单 未支付的主订单 非 货到付款/银行转账 子订单
                           "OR (OrderType = 1 AND HasChildren = 1 AND PaymentStatus < 2 AND PaymentGateway<>'cod' AND PaymentGateway<>'bank'))";
#endif
            //未支付
            // PaymentStatus < 2 AND PaymentGateway<>'cod' AND PaymentGateway<>'bank'
            if (sps > -1)//
            {
                where += " And ShippingStatus=" + sps;
            }
            if (pas > -1)//支付状态暂时都取未支付的
            {
                where += " And  PaymentStatus < 2 AND PaymentGateway<>'cod' AND PaymentGateway<>'bank' And OrderStatus<>-1 ";
            }


            //获取总条数
            toalCount = orderBll.GetRecordCount(where);
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<OrderInfo> orderList = orderBll.GetListByPageEX(where, "", startIndex, endIndex);
            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderInfo item in orderList)
                {
                    //有子订单 已支付 或 货到付款/银行转账 子订单 - 加载子单
                    if (item.HasChildren && (item.PaymentStatus > 1 || (item.PaymentGateway == "cod" || item.PaymentGateway == "bank")))
                    {
                        item.SubOrders = orderBll.GetModelList(" ParentOrderId=" + item.OrderId);
                        item.SubOrders.ForEach(
                            info => info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId));
                    }
                    else
                    {
                        item.OrderItems = itemBll.GetModelList(" OrderId=" + item.OrderId);
                    }
                }
            }

            PagedList<Maticsoft.Model.Shop.Order.OrderInfo> lists = new PagedList<Maticsoft.Model.Shop.Order.OrderInfo>(orderList, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }

        #region 辅助方法

        public static string GetOrderType(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            string str = "";
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
            EnumHelper.OrderMainStatus orderType = orderBll.GetOrderType(paymentGateway,
                                    orderStatus,
                                    paymentStatus,
                                    shippingStatus);
            switch (orderType)
            {
                //  订单组合状态 1 等待付款   | 2 等待处理 | 3 取消订单 | 4 订单锁定 | 5 等待付款确认 | 6 正在处理 |7 配货中  |8 已发货 |9  已完成
                case EnumHelper.OrderMainStatus.Paying:
                    str = "等待付款";
                    break;
                case EnumHelper.OrderMainStatus.PreHandle:
                    str = "等待处理";
                    break;
                case EnumHelper.OrderMainStatus.Cancel:
                    str = "取消订单";
                    break;
                case EnumHelper.OrderMainStatus.Locking:
                    str = "订单锁定";
                    break;
                case EnumHelper.OrderMainStatus.PreConfirm:
                    str = "等待付款确认";
                    break;
                case EnumHelper.OrderMainStatus.Handling:
                    str = "正在处理";
                    break;
                case EnumHelper.OrderMainStatus.Shipping:
                    str = "配货中";
                    break;
                case EnumHelper.OrderMainStatus.Shiped:
                    str = "已发货";
                    break;
                case EnumHelper.OrderMainStatus.Complete:
                    str = "已完成";
                    break;
                default:
                    str = "未知状态";
                    break;
            }
            return str;
        }
        public static EnumHelper.OrderMainStatus GetOrderMainStatus(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            Orders orderBll = new Orders();
            return orderBll.GetOrderType(paymentGateway,
                                    orderStatus,
                                    paymentStatus,
                                    shippingStatus);
        }

        #endregion
        #region Ajax方法
        [HttpPost]
        public ActionResult CancelOrder(FormCollection Fm)
        {
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
            long orderId = Common.Globals.SafeLong(Fm["OrderId"], 0);
            Maticsoft.Model.Shop.Order.OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
            if (orderInfo == null || orderInfo.BuyerID != currentUser.UserID)
                return Content("False");

            if (Maticsoft.BLL.Shop.Order.OrderManage.CancelOrder(orderInfo, currentUser))
                return Content("True");
            return Content("False");
        }
        [HttpPost]//完成订单
        public ActionResult CompleteOrder(FormCollection Fm)
        {
            Orders orderBll = new Orders();
            long orderId = Globals.SafeLong(Fm["OrderId"], 0);
            OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
            if (orderInfo == null || orderInfo.BuyerID != CurrentUser.UserID)
                return Content("False");
            if (BLL.Shop.Order.OrderManage.CompleteOrder(orderInfo, CurrentUser))
            {
                return Content("True");
            }
            return Content("False");
        }
        #endregion
        #endregion

        #region 收藏列表
        public ActionResult MyFavor(string viewName = "MyFavor")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的收藏";//+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }

        public PartialViewResult FavorList(int pageIndex = 1, string viewName = "_FavorList")
        {
            Maticsoft.BLL.Shop.Favorite favoBll = new BLL.Shop.Favorite();
            StringBuilder strBuilder = new StringBuilder();
            // strBuilder.AppendFormat(" UserId ={0}  and  SaleStatus in ( {1},{2} ) ", CurrentUser.UserID, (int )ProductSaleStatus.InStock, (int )ProductSaleStatus.OnSale);
            strBuilder.AppendFormat(" favo.UserId ={0} and favo.Type= {1} ", CurrentUser.UserID, (int)FavoriteEnums.Product);

            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = favoBll.GetRecordCount(" UserId =" + CurrentUser.UserID + " and Type=" + (int)FavoriteEnums.Product);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.ViewModel.Shop.FavoProdModel> favoList = favoBll.GetFavoriteProductListByPage(strBuilder.ToString(), startIndex, endIndex);
            PagedList<Maticsoft.ViewModel.Shop.FavoProdModel> lists = new PagedList<Maticsoft.ViewModel.Shop.FavoProdModel>(favoList, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        /// <summary>
        /// 移除收藏项
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult RemoveFavorItem(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ItemId"]))
            {
                string itemId = Fm["ItemId"];
                Maticsoft.BLL.Shop.Favorite favoBll = new BLL.Shop.Favorite();
                int favoriteId = Common.Globals.SafeInt(itemId, 0);
                if (favoBll.Delete(favoriteId))
                    return Content("Ok");
            }
            return Content("No");
        }
        #endregion

        #region Ajax方法
        /// <summary>
        /// 加入收藏
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddFav(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                int productId = Common.Globals.SafeInt(Fm["ProductId"], 0);
                Maticsoft.BLL.Shop.Favorite favBll = new BLL.Shop.Favorite();
                //是否已经收藏
                if (favBll.Exists(productId, currentUser.UserID, 1))
                {
                    return Content("Rep");
                }
                Maticsoft.Model.Shop.Favorite favMode = new Maticsoft.Model.Shop.Favorite();
                favMode.CreatedDate = DateTime.Now;
                favMode.TargetId = productId;
                favMode.Type = 1;
                favMode.UserId = currentUser.UserID;
                return favBll.Add(favMode) > 0 ? Content("True") : Content("False");
            }
            return Content("False");
        }
        /// <summary>
        /// 添加咨询
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddConsult(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                int productId = Common.Globals.SafeInt(Fm["ProductId"], 0);
                string content = Common.InjectionFilter.SqlFilter(Fm["Content"]);
                Maticsoft.BLL.Shop.Products.ProductConsults consultBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
                Maticsoft.Model.Shop.Products.ProductConsults consultMode = new Maticsoft.Model.Shop.Products.ProductConsults();
                consultMode.CreatedDate = DateTime.Now;
                consultMode.TypeId = 0;
                consultMode.Status = 0;
                consultMode.UserId = currentUser.UserID;
                consultMode.UserName = currentUser.NickName;
                consultMode.UserEmail = currentUser.Email;
                consultMode.IsReply = false;
                consultMode.Recomend = 0;
                consultMode.ProductId = productId;
                consultMode.ConsultationText = content;
                return consultBll.Add(consultMode) > 0 ? Content("True") : Content("False");
            }
            return Content("False");
        }
        #endregion

        #region 收货地址
        public ActionResult MyAddress()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的收货地址" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            BLL.Shop.Shipping.ShippingAddress addressManage = new BLL.Shop.Shipping.ShippingAddress();
            List<Model.Shop.Shipping.ShippingAddress> list = addressManage.GetModelList(" UserId=" + CurrentUser.UserID);

            return View(list);
        }

        /// <summary>
        /// 获取编辑页面或新增页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult ShippAddress(int id = -1, string viewName = "ShippAddress")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的收货地址" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            BLL.Shop.Shipping.ShippingAddress addressManage = new BLL.Shop.Shipping.ShippingAddress();
            Model.Shop.Shipping.ShippingAddress model = new Model.Shop.Shipping.ShippingAddress();
            if (id > 0) model = addressManage.GetModel(id);

            if (model != null && model.UserId != CurrentUser.UserID)
            {
                LogHelp.AddInvadeLog(
                    string.Format(
                        "非法获取收货人数据|当前用户:{0}|获取收货地址:{1}|_Maticsoft.Web.Areas.Shop.Controllers.UserCenterController.ShippAddress",
                        CurrentUser.UserID, id), System.Web.HttpContext.Current.Request);
                return View(viewName, new Model.Shop.Shipping.ShippingAddress());
            }
            return View(viewName, model);
        }

        /// <summary>
        /// 添加和编辑收货地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitShippAddress(Model.Shop.Shipping.ShippingAddress model)
        {
            if (CurrentUser == null || model == null) return Content("NO");

            BLL.Shop.Shipping.ShippingAddress addressManage = new BLL.Shop.Shipping.ShippingAddress();
            //Update
            if (model.ShippingId > 0)
            {
                if (addressManage.Update(model)) return Content("OK");
                return Content("NO");
            }
            //Add
            model.UserId = CurrentUser.UserID;
            model.ShippingId = addressManage.Add(model);
            if (model.ShippingId > 1) return Content("OK");
            return Content("NO");
        }

        public ActionResult SetDefaultAddress(int id = -1, bool State = false)
        {
            BLL.Shop.Shipping.ShippingAddress addressManage = new BLL.Shop.Shipping.ShippingAddress();
            Model.Shop.Shipping.ShippingAddress model = new Model.Shop.Shipping.ShippingAddress();
            if (id > 0) model = addressManage.GetModel(id);
            //Update
            if (model.ShippingId > 0)
            {
                model.IsDefault = State;
                if (addressManage.Update(model)) return Content("OK");
                return Content("NO");
            }
            return Content("NO");
        }
        #endregion

        #region 健康服务店申请页面
        public ActionResult ShopGoodservice(string ViewName = "ShopGoodservice")
        {
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                //Maticsoft.BLL.Ms.EntryForm FormBll = new BLL.Ms.EntryForm();
                //Maticsoft.Model.Ms.EntryForm model = FormBll.GetByUserNameModel(CurrentUser.UserName);

                Maticsoft.BLL.Members.UsersExp expbll = new UsersExp();
                Maticsoft.Model.Members.UsersExpModel model
                    = expbll.GetUsersExpInfo( CurrentUser.UserID);
                
                return View(ViewName, model);

            }
            return RedirectToAction("Login", "Account");//去登录
        }
        #endregion

        #region 健康分销店申请页面
        public ActionResult ShopGoodDistribution(string ViewName = "ShopGoodDistribution")
        {
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                //Maticsoft.BLL.Ms.EntryForm FormBll = new BLL.Ms.EntryForm();
                //Maticsoft.Model.Ms.EntryForm model = FormBll.GetByUserNameModel(CurrentUser.UserName);
                //Model.Members.UsersExpModel exp = userEXBll.GetUsersExpModel(currentUser.UserID);
                //ViewBag.UserExp = exp;

                Maticsoft.BLL.Members.UsersExp expbll = new UsersExp();
                Maticsoft.Model.Members.UsersExpModel model
                    = expbll.GetUsersExpInfo(CurrentUser.UserID);
                return View(ViewName, model);

            }
            return RedirectToAction("Login", "Account");//去登录

        }
        #endregion

        #region 个人专属二维码
        public ActionResult MyQRCode()
        {
            ViewBag.MyQRCode = "http://app.zhenhaolin.com/MShop/a/r?RUserNameID=" + currentUser.UserID;
            ViewBag.UID = currentUser.UserID;
            return View();
        }

        public string SaveQRCode()
        {
            String savePath = Server.MapPath("/Areas/MShop/Themes/M1/Content/images/");
            string fullPath = "/Areas/MShop/Themes/M1/Content/images/" + currentUser.UserID + ".png";

            string newPath = "/Upload/QRCode/";

            try
            {
                FileStream fs = System.IO.File.Create(savePath + "/" + currentUser.UserID + ".png");

                byte[] bytes = Convert.FromBase64String(Request["data"]);

                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                FileManager.MoveImageForFTP(fullPath, newPath);

                string UploadPath = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl") + newPath + currentUser.UserID + ".png";
                return UploadPath;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// 商品个人二维码
        /// </summary>
        /// <returns></returns>
        public ActionResult MyProductQRCode(string url)
        {
            if (url.Contains("?"))
            {
                url = url.Substring(0, url.IndexOf('?'));
            }
            ViewBag.MyQRCode = url + "?RUserNameID=" + currentUser.UserID;
            return View();
        }
        #endregion

        #region 粉丝龙虎榜
        public ActionResult LongHu()
        {
            string baseuri = System.Configuration.ConfigurationManager.AppSettings["B2BURL"];
            var teamApiHelper = new TeamAPI(baseuri);

            ViewBag.FWeek = teamApiHelper.GetNongHu(1);
            ViewBag.FMonth = teamApiHelper.GetNongHu(2);
            return View();
        }
        #endregion

        #region 业绩龙虎榜
        public ActionResult PerformanceRanking()
        {
            string baseuri = System.Configuration.ConfigurationManager.AppSettings["B2BURL"];
            var teamApiHelper = new TeamAPI(baseuri);

            ViewBag.FWeek = teamApiHelper.GetNongHu(3);
            ViewBag.FMonth = teamApiHelper.GetNongHu(4);
            return View();
        }
        #endregion

        #region 订单评论
        public ActionResult Order_Productreviews(long id = -1)
        {
            List<Maticsoft.Model.Shop.Order.OrderInfo> orderList = ordersBll.GetOrderInfo(id);//根据订单编号得到评价商品卖家的详细信息

            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderInfo item in orderList)
                {
                    if (item.BuyerID != CurrentUser.UserID || item.IsReviews || item.OrderStatus != (int)EnumHelper.OrderStatus.Complete)
                    {
                        return Redirect(ViewBag.BasePath + "U/Orders");
                    }
                    else
                    {

                    item.OrderItems = orderItemBll.GetModelList(" OrderId=" + item.OrderId);
                    }
                }
            }
            return View(orderList);
        }

        /// <summary>
        /// 提交商品评论
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjAxPReview(FormCollection fm)
        {
            string data = fm["PReviewjson"];
            if (String.IsNullOrWhiteSpace(data))
            {
                return Content("false");
            }
            Maticsoft.BLL.Shop.Products.ProductReviews PReviewBll = new BLL.Shop.Products.ProductReviews();
            List<Model.Shop.Products.ProductReviews> modelList = new List<Model.Shop.Products.ProductReviews>();
            Model.Shop.Products.ProductReviews prodRevModel;
            JsonArray jsonArray = JsonConvert.Import<JsonArray>(data);
            long orderId = -1;
            foreach (JsonObject jsonObject in jsonArray)
            {
                long pid = Globals.SafeInt(jsonObject["pid"].ToString(), -1);
                orderId = Globals.SafeInt(jsonObject["orderId"].ToString(), -1);
                string contentval = InjectionFilter.Filter(jsonObject["contentval"].ToString());
                string imagesurlPath = Globals.SafeString(jsonObject["imagesurlPath"].ToString(), "");
                string imagesurlName = Globals.SafeString(jsonObject["imagesurlName"].ToString(), "");
                string attribute = InjectionFilter.Filter(jsonObject["attribute"].ToString());
                string sku = InjectionFilter.Filter(jsonObject["sku"].ToString());

                if (pid > 0 && orderId > 0 && !String.IsNullOrWhiteSpace(contentval))
                {
                    prodRevModel = new Model.Shop.Products.ProductReviews();
                    prodRevModel.Attribute = attribute;
                    prodRevModel.CreatedDate = DateTime.Now;
                    prodRevModel.OrderId = orderId;
                    prodRevModel.ProductId = pid;
                    prodRevModel.ReviewText = contentval;
                    prodRevModel.SKU = sku;
                    prodRevModel.Status = 0;
                    prodRevModel.UserEmail = currentUser.Email;
                    prodRevModel.UserId = currentUser.UserID;
                    prodRevModel.UserName = currentUser.UserName;
                    prodRevModel.ParentId = 0;
                    if (!String.IsNullOrWhiteSpace(imagesurlPath) && !String.IsNullOrWhiteSpace(imagesurlName))
                    {
                        //创建文件夹  移动文件
                        string path = string.Format("/Upload/Shop/ProductReviews/{0}/", DateTime.Now.ToString("yyyyMM"));
                        string mapPath = Request.MapPath(path);
                        if (!Directory.Exists(mapPath))
                        {
                            Directory.CreateDirectory(mapPath);
                        }
                        string[] pathArr = imagesurlPath.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] namesArr = imagesurlName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        if (pathArr.Length != namesArr.Length)
                        {
                            throw new ArgumentOutOfRangeException("路径与文件名长度不匹配！");
                        }
                        for (int i = 0; i < pathArr.Length; i++)
                        {
                            System.IO.File.Move(Request.MapPath(pathArr[i]), mapPath + namesArr[i]);
                        }
                        prodRevModel.ImagesPath = path + "{0}";
                        prodRevModel.ImagesNames = string.Join("|", namesArr);
                    }
                    modelList.Add(prodRevModel);
                }
                else
                {
                    return Content("false");
                }
            }
            
            if (modelList.Count > 0)
            {
                int pointers;
                if (PReviewBll.AddEx(modelList, orderId, out  pointers))
                {
                    return Content(pointers.ToString());//评论成功   返回获得的积分
                }
            }
            return Content("true");
        }

        #endregion

        #region 业绩日排行
        public ActionResult Dailychart()
        {
            ViewBag.DataTime = DateTime.Now.ToString("yyyy-MM-dd");
            string baseuri = System.Configuration.ConfigurationManager.AppSettings["B2BURL"];
            var teamApiHelper = new TeamAPI(baseuri);

            ViewBag.FWeek = teamApiHelper.GetNongHu(0);
            //ViewBag.FMonth = teamApiHelper.GetNongHu(4);
            return View();
        }
        #endregion

        public static string GetUserID()
        {
            if (string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("UserNameID", "Value")))
            {
                Maticsoft.Common.Cookies.setCookie("UserNameID", "", 60 * 24 * 2);
            }
            return Maticsoft.Common.Cookies.getCookie("UserNameID", "Value");
        }

        public static string GetId()
        {
            string APPID = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, "AA");


            return APPID;
        }
        public static string GetAppSercet()
        {
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, "AA");

            return AppSecret;
           
        }
       
        public static String Sha1(String s)
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
        //public static string GetTicket(string Token, int sceneId)
        //{

        //    string access_token = Utils.GetTicket(Token, sceneId);

        //    return access_token;
        //}

        #region 退货管理模块
        /// <summary>
        /// 我的退货单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReturnApply(long Id)
        {
            Maticsoft.Model.Shop.Order.OrderItems orderItems = itemBll.GetModel(Id);
            return View(orderItems);
        }
        /// <summary>
        /// 申请退货
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubReturnApply()
        {
            string returnReson = Request["returnReason"];
            string returnDescription = Request["returnDescription"];
            int returnQuantity = 0;
            if (!string.IsNullOrEmpty(Request["quantity"]))
            {
                int.TryParse(Request["quantity"].Trim(), out returnQuantity);
            }
            //修改 Shop_OrderItems原本数量减去退货数量
            Maticsoft.Model.Shop.Order.OrderItems orderitems_update = new Maticsoft.Model.Shop.Order.OrderItems();
            orderitems_update.ReturnQty = returnQuantity;
            orderitems_update.ItemId = long.Parse(Request.Form["orderItemId"]);
            itemBll.updateOrader2(orderitems_update);
            orderitems_update = itemBll.GetModel(orderitems_update.ItemId);


            Maticsoft.BLL.Shop.Order.Orders OrderInfoBll = new BLL.Shop.Order.Orders();
            Maticsoft.Model.Shop.Order.OrderInfo orderInfo = OrderInfoBll.GetModel(orderitems_update.OrderId);

            string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
            string ImageFile = string.Format("/Upload/TuoHuo/{0}", DateTime.Now.ToString("yyyyMMdd"));

            string images = Request.Form["acttachment"];
            long orderId = long.Parse(Request.Form["orderId"]);
            long orderItemId = long.Parse(Request.Form["orderItemId"]);
            long productId = long.Parse(Request.Form["productId"]);
            Maticsoft.Model.Shop.Order.OrderItems orderItems = itemBll.GetModel(orderItemId);
            if (orderItems.Quantity - orderItems.ReturnQty >= returnQuantity && returnQuantity > 0)
            {
                Maticsoft.Model.Shop.Order.OrderReturnGoods returnGoods = new Model.Shop.Order.OrderReturnGoods();
                returnGoods.OrderId = orderId;
                returnGoods.Attachment = "";
                if (images != null)
                {
                    returnGoods.Attachment = string.Format(images, "").Replace(tempFile, ImageFile);
                }
                returnGoods.ReturnTime = DateTime.Now;
                returnGoods.UserId = (long)CurrentUser.UserID;
                returnGoods.ReturnDescription = returnDescription;
                returnGoods.ReturnAmounts = orderItems.AdjustedPrice * returnQuantity;
                returnGoods.ReturnReason = returnReson;
                returnGoods.ApproveStatus = (int)EnumHelper.ReturnApproveStatus.NoApprove;
                returnGoods.AccountStatus = (int)EnumHelper.ReturnAccountStatus.NoReturn;
                returnGoods.IsDeleted = false;
                returnGoods.ReturnOrderCode = "TH" + DateTime.Now.ToString("yyyyMMddhhMMss");

                returnGoods.Status = 0;//申请状态
                returnGoods.RefundStatus = 0;//退款状态
                returnGoods.LogisticStatus = 0;//退货状态

                returnGoods.SupplierId = orderInfo.SupplierId;//商家Id
                returnGoods.SupplierName = orderInfo.SupplierName;//商家名称
                returnGoods.CouponCode = orderInfo.CouponCode;//优惠码
                returnGoods.CouponName = orderInfo.CouponName;//优惠券名称
                returnGoods.CouponAmount = orderInfo.CouponAmount;//优惠金额
                returnGoods.CouponValueType = orderInfo.CouponValueType;//优惠值类型
                returnGoods.CouponValue = orderInfo.CouponValue;//优回值
                returnGoods.ReturnCoupon = 2;//默认值
                returnGoods.ActualSalesTotal = (decimal)orderItems.AdjustedPrice;//商品实际出售价格
                returnGoods.AmountAdjusted = (decimal)(orderItems.AdjustedPrice * orderItems.ShipmentQuantity);//调整后金额
                returnGoods.Amount = orderInfo.Amount;//实付金额
                returnGoods.ServiceType = 0;//
                returnGoods.ReturnType = 0;//退回方式
                returnGoods.OrderCode = orderInfo.OrderCode;//订单编号
                returnGoods.ApprovePeason = currentUser.UserName.ToString();//操作人
                returnGoods.ApproveRemark = "发货操作";
                returnGoods.ApproveTime = DateTime.Now;
                returnGoods.AccountTime = DateTime.Now;

                if (orderInfo.PaymentStatus == (int)Maticsoft.Model.Shop.Order.EnumHelper.PaymentStatus.Paid && orderInfo.ShippingStatus == (int)Maticsoft.Model.Shop.Order.EnumHelper.ShippingStatus.UnShipped)
                {
                    //已支付未发货的时候=申请退款
                    returnGoods.ReturnGoodsType = (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.Money;
                }
                else if (orderInfo.PaymentStatus == (int)Maticsoft.Model.Shop.Order.EnumHelper.PaymentStatus.Paid && (orderInfo.ShippingStatus == (int)Maticsoft.Model.Shop.Order.EnumHelper.ShippingStatus.Shipped || orderInfo.ShippingStatus == (int)Maticsoft.Model.Shop.Order.EnumHelper.ShippingStatus.ConfirmShip))
                {
                    //已支付已发货(已收货但不满意)的时候=申请退货
                    returnGoods.ReturnGoodsType = (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.Goods;
                }
                else
                {
                    //维修  或者  调休
                    returnGoods.ReturnGoodsType = (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.ExchangeGoods;
                }

                Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnGoodsItem = new Model.Shop.Order.OrderReturnGoodsItem();
                returnGoodsItem.ProductCode = orderItems.ProductCode;
                returnGoodsItem.ProductId = orderItems.ProductId;
                returnGoodsItem.OrderId = orderId;
                returnGoodsItem.OrderCode = orderItems.OrderCode;
                returnGoodsItem.OrderItemId = orderItems.ItemId;
                returnGoodsItem.AdjustedPrice = orderItems.AdjustedPrice;
                returnGoodsItem.Attribute = orderItems.Attribute;
                returnGoodsItem.UserID = CurrentUser.UserID;
                returnGoodsItem.Costprice = orderItems.CostPrice;
                returnGoodsItem.ProductName = orderItems.Name;
                returnGoodsItem.Quantity = returnQuantity;
                returnGoodsItem.SellPrice = orderItems.SellPrice;
                returnGoodsItem.SKU = orderItems.SKU;
                returnGoodsItem.Supplierid = orderItems.SupplierId;
                returnGoodsItem.Suppliername = orderItems.SupplierName;
                returnGoodsItem.OrderAmounts = orderItems.AdjustedPrice * orderItems.Quantity;
                returnGoodsItem.CreateTime = DateTime.Now;

                long returnId = returnGoodsBLL.Add(returnGoods);
                if (returnId > 0)
                {
                    FileManager.MoveImageForFTP(images, ImageFile);
                    returnGoodsItem.ReturnId = returnId;
                    returnGoodsItem.ReturnOrderCode = returnGoods.ReturnOrderCode;
                    long returnGoodsItemId = returnGoodsItemBLL.Add(returnGoodsItem);
                    if (returnGoodsItemId > 0)
                    {
                        // 更新订单状态为"锁定"状态
                        Maticsoft.Model.Shop.Order.OrderInfo orderModel = OrderInfoBll.GetModel(orderId);
                        if (null != orderModel)
                        {
                            orderModel.OrderStatus = (int)EnumHelper.OrderStatus.SystemLock;
                            OrderInfoBll.Update(orderModel);
                        }

                        // 更新订单
                        //_orderManage.OrderReturnUpdate(orderId);
                        //EnumHelper.OrderItemReturnStatus returnStatus = EnumHelper.OrderItemReturnStatus.Part;
                        //if (orderItems.Quantity - orderItems.ReturnQty <= returnQuantity)
                        //{
                        //    returnStatus = EnumHelper.OrderItemReturnStatus.All;
                        //}
                        // 更新订单明细
                        //itemBll.UpdateOrderItemReturnStatus(orderItemId, EnumHelper.Returnstatus.Untreated, returnQuantity, (EnumHelper.ReturnGoodsType)returnGoods.ReturnGoodsType);
                    }
                }

                return RedirectToAction("ReturnInfos");
            }
            else
            {
                ///数量不够....
                return RedirectToAction("ReturnApply", orderItemId);
            }
        }

        /// <summary>
        /// 查看退款
        /// </summary>
        public ActionResult ReturnDetail(long id)
        {
            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnGoodsItem = returnGoodsItemBLL.GetReturnGoodItemModel(id);
            if (returnGoodsItem != null)
            {
                ViewBag.OrderReturnGoods = returnGoodsBLL.GetModel(returnGoodsItem.ReturnId.Value);
            }
            return View(returnGoodsItem);

        }

        /// <summary>
        /// 审核通过添加物流信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnShippingInfo(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnGoodsItem = returnGoodsItemBLL.GetReturnGoodItemModel(id);
                if (returnGoodsItem != null)
                {
                    //快递公司名称
                    BLL.Shop.Sales.ExpressTemplate expresstempBll = new BLL.Shop.Sales.ExpressTemplate();
                    DataSet ds = expresstempBll.GetList(" IsUse=1 ");
                    ViewBag.dataset = ds;
                    //退货单信息
                    ViewBag.OrderReturnGoods = returnGoodsBLL.GetModel(returnGoodsItem.ReturnId.Value);
                }
                return View(returnGoodsItem);
            }
            return RedirectToAction("Login", "Account");//去登录
        }

        //提交物流信息
        [HttpPost]
        public void Btn_ShippingInfoOk(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null || CurrentUser.UserType == "AA")
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                long id = long.Parse(collection["Id"].ToString());
                JsonObject json = new JsonObject();

                Maticsoft.Model.Shop.Order.OrderReturnGoodsItem modelItem = returnGoodsItemBLL.GetModel(id);
                if (null != modelItem)
                {
                    Maticsoft.Model.Shop.Order.OrderReturnGoods model = returnGoodsBLL.GetModel(Globals.SafeLong(modelItem.ReturnId, 0));

                    model.Information = collection["Information"].ToString();
                    model.ExpressNO = collection["ExpressNO"];
                    User currentUser = new Maticsoft.Accounts.Bus.User(CurrentUser.UserID);
                    //model.Id = id;
                    //修改状态为发货
                    model.Status = (int)EnumHelper.Status.Handling; //处理中
                    model.LogisticStatus = (int)EnumHelper.LogisticStatus.Shipped;//已发货
                    model.RefundStatus = (int)EnumHelper.RefundStatus.UnRefund;//未退款

                    if (returnGoodsBLL.UpdateInformation(model))
                    {
                        //发货成功添加历史记录信息
                        Maticsoft.BLL.Shop.Order.Shop_ReturnOrderAction actionBll = new BLL.Shop.Order.Shop_ReturnOrderAction();
                        Maticsoft.Model.Shop.Order.Shop_ReturnOrderAction actionModel = new Model.Shop.Order.Shop_ReturnOrderAction();

                        actionModel.ReturnOrderId = model.Id;
                        actionModel.ReturnOrderCode = model.ReturnOrderCode;
                        actionModel.UserId = currentUser.UserID;
                        actionModel.UserName = currentUser.UserName.ToString();
                        actionModel.ActionCode = ((int)EnumHelper.ActionCode.DeliverReturnGoods).ToString();
                        actionModel.ActionDate = DateTime.Now;
                        actionModel.Remark = "退货方发货";
                        actionBll.Add(actionModel);

                        json.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "FAIL");
                }
                Response.Write(json.ToString());
            }
        }

        /// <summary>
        /// 我的退货单
        /// </summary>
        public ActionResult ReturnInfos(int pageSize = 10, int pageIndex = 1)
        {
            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引 
            int endIndex = pageIndex * _pageSize;
            int totalCount = 0;
            List<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem> models = returnGoodsItemBLL.GetOrderReturnGoodsItems((long)CurrentUser.UserID, startIndex, endIndex, out totalCount);
            foreach (var model in models)
            {
                model.OrderReturnGood = returnGoodsBLL.GetModel(model.ReturnId.Value);
            }
            PagedList<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem> lists = new PagedList<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem>(models, pageIndex, _pageSize, totalCount);

            return View(lists);
        }

        /// <summary>
        /// 确定收货
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void SubReceipt(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null || CurrentUser.UserType == "AA")
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                long id = long.Parse(collection["OrderId"].ToString());
                JsonObject json = new JsonObject();
                Maticsoft.Model.Shop.Order.OrderReturnGoods model = returnGoodsBLL.GetModel(id);

                User currentUser = new Maticsoft.Accounts.Bus.User(CurrentUser.UserID);
                model.Id = id;
                //修改状态为发货
                model.Status = (int)EnumHelper.Status.Complete; //审核通过
                model.LogisticStatus = 4;//已发货
                model.RefundStatus = (int)EnumHelper.RefundStatus.UnRefund;//未退款
                if (returnGoodsBLL.UpdateInformation(model))
                {
                    //发货成功添加历史记录信息
                    Maticsoft.BLL.Shop.Order.Shop_ReturnOrderAction actionBll = new BLL.Shop.Order.Shop_ReturnOrderAction();
                    Maticsoft.Model.Shop.Order.Shop_ReturnOrderAction actionModel = new Model.Shop.Order.Shop_ReturnOrderAction();

                    actionModel.ReturnOrderId = id;
                    actionModel.ReturnOrderCode = model.ReturnOrderCode;
                    actionModel.UserId = currentUser.UserID;
                    actionModel.UserName = currentUser.UserName.ToString();
                    actionModel.ActionCode = ((int)EnumHelper.ActionCode.ReceiptReturnGoods).ToString();
                    actionModel.ActionDate = DateTime.Now;
                    actionModel.Remark = "商家确认收货";
                    actionBll.Add(actionModel);

                    json.Accumulate("STATUS", "SUCC");
                }
                else
                {
                    json.Accumulate("STATUS", "FAIL");
                }
                Response.Write(json.ToString());
            }
        }


        #region  方法(李永琴)
        /// <summary>
        /// 获取订单的 退货状态
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="LogisticStatus"></param>
        /// <returns></returns>
        public static string GetMainStatusStr(int ReturnType, int? Status, int? RefundStatus, int? LogisticStatus)
        {
            Maticsoft.BLL.Shop.Order.OrderReturnGoods RerturnorderBll = new Maticsoft.BLL.Shop.Order.OrderReturnGoods();
            EnumHelper.MainStatus orderType = RerturnorderBll.GetMainStatus(Status, RefundStatus, LogisticStatus);

            switch (orderType)
            {
                case EnumHelper.MainStatus.Auditing:
                    return "等待审核";
                case EnumHelper.MainStatus.Cancel:
                    return "已取消";
                case EnumHelper.MainStatus.Refuse:
                    return "审核未通过";
                case EnumHelper.MainStatus.Kiss:
                    if (ReturnType == (int)EnumHelper.ReturnGoodsType.Money)
                        return "等待卖家退款";
                    else
                        return "等待买家发货";
                case EnumHelper.MainStatus.Handling:
                    return "等待买家发货";
                case EnumHelper.MainStatus.Packing:
                    return "等待卖家收货";
                case EnumHelper.MainStatus.Returning:
                    return "等待卖家退款";
                case EnumHelper.MainStatus.WaitingRefund:
                    return "等待卖家退款";
                case EnumHelper.MainStatus.Refunding:
                    return "卖家已退款";
                case EnumHelper.MainStatus.Return:
                    return "拒绝收货";
                case EnumHelper.MainStatus.Complete:
                    return "交易完成";
                case EnumHelper.MainStatus.RefuseAdjustable:
                    return "拒绝调货";
                case EnumHelper.MainStatus.RefuseRepair:
                    return "拒绝维修";
                case EnumHelper.MainStatus.journey:
                    return "等待买家收货";
                case EnumHelper.MainStatus.storage:
                    return "买家确定收货";
            }
            return "";
        }

        public static EnumHelper.MainStatus GetMainStatus(int? Status, int? RefundStatus, int? LogisticStatus)
        {
            Maticsoft.BLL.Shop.Order.OrderReturnGoods RerturnorderBll = new Maticsoft.BLL.Shop.Order.OrderReturnGoods();

            return RerturnorderBll.GetMainStatus(Status, RefundStatus, LogisticStatus);
        }
        #endregion

        #endregion

        #region 我的证书
        public ActionResult MyCertificate()
        {
            string name = "";
                if (currentUser != null)
                {
                    name=currentUser.TrueName;
                }
                else
                {
                     return RedirectToAction("Login", "Account");//去登录
                }
                ViewBag.Name = name;
            return View();
        }
        
        #endregion

    }
}
