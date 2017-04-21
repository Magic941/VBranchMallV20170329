using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Shop.Gift;
using Maticsoft.BLL.Shop.Order;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Json;
using Maticsoft.Json.Conversion;
using Maticsoft.Model.Shop;
using Maticsoft.Model.Shop.Coupon;
using Maticsoft.Model.Shop.Order;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using System.Linq;
using CouponInfo = Maticsoft.BLL.Shop.Coupon.CouponInfo;
using CouponRule = Maticsoft.BLL.Shop.Coupon.CouponRule;
using EnumHelper = Maticsoft.Model.Shop.Order.EnumHelper;
using OrderAction = Maticsoft.BLL.Shop.Order.OrderAction;
using Maticsoft.Model;
using Maticsoft.BLL.Shop.Card;
using System.Collections;
using Maticsoft.BLL.SysManage;
using Maticsoft.Model.Shop.Shipping;
using System.Data;


namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class UserCenterController : ShopControllerBaseUser
    {
        private readonly BLL.Members.PointsDetail detailBll = new BLL.Members.PointsDetail();
        private readonly BLL.Members.SiteMessage bllSM = new BLL.Members.SiteMessage();
        private readonly BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
        private readonly BLL.Members.UserBind userBind = new BLL.Members.UserBind();
        private readonly Orders _orderManage = new Orders();
        private readonly BLL.Pay.RechargeRequest rechargeBll = new BLL.Pay.RechargeRequest();
        private readonly BLL.Pay.BalanceDrawRequest balanDrawBll = new BLL.Pay.BalanceDrawRequest();
        private readonly BLL.Pay.BalanceDetails balanDetaBll = new BLL.Pay.BalanceDetails();
        private readonly BLL.Members.UserInvite inviteBll = new BLL.Members.UserInvite();
        private readonly Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new CouponRule();
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new BLL.Shop.Order.OrderAction();
        private readonly BLL.Shop.Gift.ExchangeDetail exchangeBll = new ExchangeDetail();
        private readonly BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
        private readonly BLL.Shop_CardUserInfo carduserInfoBll = new BLL.Shop_CardUserInfo();
        private readonly BLL.Shop_CardType _cardTypeBLL = new BLL.Shop_CardType();
        private readonly BLL.Shop.Order.OrderReturnGoods returnGoodsBLL = new BLL.Shop.Order.OrderReturnGoods();
        private readonly BLL.Shop.Order.OrderReturnGoodsItem returnGoodsItemBLL = new BLL.Shop.Order.OrderReturnGoodsItem();
        private readonly BLL.Shop.Order.OrderItems itemBll = new BLL.Shop.Order.OrderItems();

        #region  首页
        //
        // GET: /Shop/UserCenter/
        public ActionResult Index(string viewName = "Index")
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.WebName = webSiteSet.WebName;
            Maticsoft.Model.Members.UsersExpModel usersModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (usersModel != null)
            {
                BLL.Members.SiteMessage msgBll = new BLL.Members.SiteMessage();
                ViewBag.privatecount = msgBll.GetReceiveMsgNotReadCount(CurrentUser.UserID, -1);//未读私信的条数
                Orders orderBll = new Orders();
                ViewBag.Unpaid = orderBll.GetPaymentStatusCounts(CurrentUser.UserID, (int)EnumHelper.PaymentStatus.Unpaid);//未支付订单数
                #region SEO 优化设置
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title = "个人中心" + pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion
                return View(viewName, usersModel);
            }
            return RedirectToAction("Login", "Account");//去登录
        }
        #endregion

        #region 用户个人资料

        public ActionResult Personal()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "个人资料" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
                if (null != model)
                {
                    return View(model);
                }
            }

            return RedirectToAction("Login", "Account");//去登录
        }
        #endregion 用户个人资料

        #region 更新用户信息

        /// <summary>
        /// 更新用户信息
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public void UpdateUserInfo(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null || CurrentUser.UserType == "AA")
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
                Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
                if (null == model)
                {
                    RedirectToAction("Login", "Account");//去登录
                }
                else
                {
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
                    model.UserName = collection["UserName"];
                    model.Constellation = collection["Constellation"];//星座
                    model.PersonalStatus = collection["PersonalStatus"];//职业
                    model.Singature = collection["Singature"];
                    model.Address = collection["Address"];
                    User currentUser = new Maticsoft.Accounts.Bus.User(CurrentUser.UserID);
                    currentUser.Sex = collection["Sex"];
                    currentUser.Email = collection["Email"];
                    currentUser.NickName = collection["NickName"];
                    currentUser.Phone = collection["Phone"];
                    Maticsoft.BLL.Shop.Card.UserCardLogic u = new UserCardLogic();
                    if (currentUser.Update() && u.UpdateUserName(currentUser.UserName,model.UserName))
                    {
                        json.Accumulate("STATUS", "SUCC");
                        CurrentUser.NickName = currentUser.NickName;
                        //重新绑定form身份验证
                        System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, true);
                        Session[Globals.SESSIONKEY_USER] = model;
                        userEXBll.UpdateUsersExp(model);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                    Response.Write(json.ToString());
                }
            }
        }

        #endregion 更新用户信息

        #region 用户头像

        public ActionResult Gravatar()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "修改头像" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            if (CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");//去登录
            }
            else
            {
                ViewBag.UserID = CurrentUser.UserID;
                return View("Gravatar");
            }
        }

        #endregion 用户头像

        #region 修改用户头像

        /// <summary>
        /// 修改用户头像
        /// </summary>
        [HttpPost]
        public ActionResult Gravatar(FormCollection collection)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "修改头像" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            try
            {
                if (CurrentUser == null)
                {
                    return RedirectToAction("Login", "Account");//去登录
                }
                else
                {
                    Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
                    if (null != model)
                    {
                        model.Gravatar = collection["Gravatar"];
                        if (userEXBll.UpdateUsersExp(model))//更新扩展信息  ,后期将更新头像独立一个方法。
                        {
                            return RedirectToAction("Personal");
                        }
                        else
                        {
                            return RedirectToAction("Personal");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");//去登录
                    }
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public void CutGravatar(FormCollection collection)
        {
            string savePath = "/" + MvcApplication.UploadFolder + "/User/Gravatar/";  //头像保存路径
            // string savePath = "/Upload/User/Gravatar/";
            if (String.IsNullOrWhiteSpace(collection["x"]) || String.IsNullOrWhiteSpace(collection["y"]) || String.IsNullOrWhiteSpace(collection["w"]) || String.IsNullOrWhiteSpace(collection["h"]) || String.IsNullOrWhiteSpace(collection["filename"]))
                return;
            int x = (int)Common.Globals.SafeDecimal(collection["x"], 0);//坐标点有可能是浮点型 比如 27.5
            int y = (int)Common.Globals.SafeDecimal(collection["y"], 0);
            int w = (int)Common.Globals.SafeDecimal(collection["w"], 0);
            int h = (int)Common.Globals.SafeDecimal(collection["h"], 0);
            string filename = collection["filename"];//Request["UploadPhoto"];
            int UserId = currentUser.UserID;
            try
            {
                byte[] image = Crop(filename, w, h, x, y);
                if (!Directory.Exists(Server.MapPath(savePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(savePath));
                }
                FileStream f = new FileStream(Server.MapPath(savePath + UserId + ".jpg"), FileMode.Create);
                f.Write(image, 0, image.Length);
                f.Close();
                Response.Write("success");
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        public byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (var OriginalImage = new Bitmap(Server.MapPath(Img)))
                {
                    using (var bmp = new Bitmap(Width, Height, OriginalImage.PixelFormat))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height,
                                              GraphicsUnit.Pixel);
                            var ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        #endregion 修改用户头像

        #region 用户密码

        public ActionResult ChangePassword()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "修改密码" + pageSetting.Title;
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
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
                string password = collection["Password"];
                if (!string.IsNullOrWhiteSpace(password))
                {
                    SiteIdentity SID = new SiteIdentity(CurrentUser.UserName);
                    if (SID.TestPassword(password.Trim()) == 0)
                    {
                        var pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5").ToLower();
                        BLL.Shop_CardUserInfo card = new BLL.Shop_CardUserInfo();
                        var result = card.GetModelListByName(currentUser.UserName).Any(p => p.Password == pwd);
                        if (result)
                        {
                           json.Accumulate("STATUS", "OK"); 
                        }
                        else
                        {
                            json.Accumulate("STATUS", "ERROR");
                        }
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
        }

        #endregion 检查用户原密码

        #region 更新用户密码

        /// <summary>
        /// 更新用户密码
        /// </summary>
        [HttpPost]
        public void UpdateUserPassword(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
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
        }

        #endregion 更新用户密码

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

        #region 积分明细

        public ActionResult PointsDetail(int pageIndex = 1)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "积分明细" + pageSetting.Title;
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
            List<Maticsoft.Model.Members.PointsDetail> detailList = detailBll.GetListByPageEX("UserID=" + CurrentUser.UserID, " ", startIndex, endIndex);
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
            if (RuleId == -1)
            {
                return "积分消费";
            }
            Maticsoft.BLL.Members.PointsRule ruleBll = new BLL.Members.PointsRule();
            return ruleBll.GetRuleName(RuleId);
        }

        #endregion 积分明细

        #region 积分兑换明细

        public ActionResult Exchanges(int pageIndex = 1)
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
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = exchangeBll.GetRecordCount(" UserID=" + CurrentUser.UserID);
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
            List<Maticsoft.Model.Shop.Gift.ExchangeDetail> detailList = exchangeBll.GetListByPageEX("UserID=" + CurrentUser.UserID, " CreatedDate desc", startIndex, endIndex);

            PagedList<Maticsoft.Model.Shop.Gift.ExchangeDetail> lists = new PagedList<Maticsoft.Model.Shop.Gift.ExchangeDetail>(detailList, pageIndex, _pageSize, toalCount);
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

        public ActionResult MyCoupon(int pageIndex = 1)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的优惠券" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion


            int status = Common.Globals.SafeInt(Request.Params["Status"], 1);

            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = infoBll.GetRecordCount(String.Format(" UserID={0} and Status={1}", currentUser.UserID, status));

            ViewBag.Total = toalCount;
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoList = infoBll.GetListByPageEX(String.Format(" UserID={0} and Status={1}", currentUser.UserID, status), " GenerateTime desc", startIndex, endIndex);
            Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new Maticsoft.BLL.Shop.Coupon.CouponClass();
            foreach (var Info in infoList)
            {
                Maticsoft.Model.Shop.Coupon.CouponClass classModel = classBll.GetModelByCache(Info.ClassId);
                Info.ClassName = classModel == null ? "" : classModel.Name;
            }
            PagedList<Maticsoft.Model.Shop.Coupon.CouponInfo> lists = new PagedList<Maticsoft.Model.Shop.Coupon.CouponInfo>(infoList, pageIndex, _pageSize, toalCount);
            return View(lists);
        }


        #endregion

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

        #region 发站内信

        /// <summary>
        /// 发站内信
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMessage()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "发信息" + pageSetting.Title;
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
            string title = Common.InjectionFilter.Filter(collection["Content"]);
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
                    if (siteModel.ReceiverIsRead == false)
                        bllSM.SetReceiveMsgAlreadyRead(siteModel.ID);
                    return View(siteModel);
                }
            }
            return RedirectToAction("Inbox");
        }
        #endregion 读取站内信息

        #region 收件箱

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Inbox()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "收件箱" + pageSetting.Title;
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
            ViewBag.Title = "发件箱" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            ViewBag.inboxpage = page;
            int pagesize = 7;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllReceiveMsgListByMvcPage(CurrentUser.UserID, -1, pagesize, page.Value);
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
            int pagesize = 10;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSendMsgListByMvcPage(CurrentUser.UserID, pagesize, page.Value);
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserCenter/_OutboxList.cshtml", list);
            return View(CurrentThemeViewPath + "/UserCenter/OutBox.cshtml", list);
        }

        #endregion 发件箱

        #region 系统信息

        /// <summary>
        /// 系统信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SysInfo(int? page)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "系统消息" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            int pagesize = 10;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSystemMsgListByMvcPage(CurrentUser.UserID, -1, CurrentUser.UserType, pagesize, page.Value);
            foreach (Maticsoft.Model.Members.SiteMessage item in list)
            {
                if (item.ReceiverIsRead == false)
                    bllSM.SetSystemMsgStateToAlreadyRead(item.ID, CurrentUser.UserID, CurrentUser.UserType);
            }
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserCenter/_SysInfoList.cshtml", list);
            return View(CurrentThemeViewPath + "/UserCenter/SysInfo.cshtml", list);
        }

        #endregion 系统信息

        #region 收货地址
        public ActionResult ShippAddressList(string viewName = "ShippAddressList")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的收货地址" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            BLL.Shop.Shipping.ShippingAddress addressManage = new BLL.Shop.Shipping.ShippingAddress();
            List<Model.Shop.Shipping.ShippingAddress> list = addressManage.GetModelList(" UserId=" + CurrentUser.UserID);

            return View(viewName, list);
        }

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

        [HttpPost]
        public ActionResult DelShippAddress(int id)
        {
            if (id < 1) return Content("NOID");
            BLL.Shop.Shipping.ShippingAddress addressManage = new BLL.Shop.Shipping.ShippingAddress();
            Model.Shop.Shipping.ShippingAddress model = addressManage.GetModel(id);
            if (model != null && CurrentUser.UserID == model.UserId)
            {
                return Content(addressManage.Delete(id) ? "OK" : "NO");
            }
            return Content("ERROR");
        }
        #endregion

        #region 订单列表
        public ActionResult Orders(string viewName = "Orders")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的订单-订单明细" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }
        public PartialViewResult OrderList(int pageIndex = 1, string viewName = "_OrderList")
        {
            Orders orderBll = new Orders();
            BLL.Shop.Order.OrderItems itemBll = new BLL.Shop.Order.OrderItems();

            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            string where = " BuyerID=" + CurrentUser.UserID +
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
            PagedList<OrderInfo> lists = new PagedList<OrderInfo>(orderList, pageIndex, _pageSize, toalCount);
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
        public ActionResult GetOrderMainStatus(FormCollection Fm)
        {
            string rtn = string.Empty;
            long orderId = Globals.SafeLong(Fm["OrderId"], 0);
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
            OrderInfo orderInfo = orderBll.GetModel(orderId);
            if (null != orderInfo)
            {
                rtn = Maticsoft.Web.Areas.Shop.Controllers.UserCenterController.GetOrderType(orderInfo.PaymentGateway, orderInfo.OrderStatus, orderInfo.PaymentStatus, orderInfo.ShippingStatus);
            }
            return Content(rtn);
        }

        [HttpPost] //退货处理
        public ActionResult ProcessReturn(FormCollection Fm)
        {
            Orders orderBll = new Orders();
            long orderId = Globals.SafeLong(Fm["OrderId"], 0);

            

            return Content("True");
        }

        [HttpPost]//取消订单
        public ActionResult CancelOrder(FormCollection Fm)
        {
            Orders orderBll = new Orders();
            long orderId = Globals.SafeLong(Fm["OrderId"], 0);
            OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
            if (orderInfo == null || orderInfo.BuyerID != CurrentUser.UserID)
                return Content("False");

            if (OrderManage.CancelOrder(orderInfo, currentUser))
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

        #region 商品评论
        public ActionResult PReview(long id = -1, string viewName = "PReview")
        {
            OrderInfo orderModel = _orderManage.GetModelInfo(id);
            if (orderModel == null ||
                orderModel.BuyerID != CurrentUser.UserID || orderModel.IsReviews || orderModel.OrderStatus != (int)EnumHelper.OrderStatus.Complete
                ) return Redirect(ViewBag.BasePath + "UserCenter/Orders");


            List<Model.Shop.Order.OrderItems> list = orderModel.OrderItems;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "评论" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName, list);
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
            BLL.Shop.Products.ProductReviews prodRevBll = new BLL.Shop.Products.ProductReviews();
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
                if (prodRevBll.AddEx(modelList, orderId, out  pointers))
                {
                    return Content(pointers.ToString());//评论成功   返回获得的积分
                }
            }
            return Content("false");
        }
        #endregion

        #region 查看订单明细

        /// <summary>
        /// 查看订单明细
        /// </summary>
        public ActionResult OrderInfo(long id = -1, string viewname = "OrderInfo")
        {
            OrderInfo orderModel = _orderManage.GetModelInfo(id);
            //Safe
            if (orderModel == null ||
                orderModel.BuyerID != CurrentUser.UserID
                ) return Redirect(ViewBag.BasePath + "UserCenter/Orders");
            if (orderModel.OrderStatus == (int)EnumHelper.OrderStatus.Cancel)//已取消的订单
            {
                viewname = "OrderCanceled";
            }

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "查看订单详细信息" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(viewname, orderModel);
        }
        public PartialViewResult OrderAction(long OrderId = -1, string viewName = "_ActionList")
        {
            OrderAction actionBll = new OrderAction();
            List<Model.Shop.Order.OrderAction> actionList = actionBll.GetModelList(" OrderId=" + OrderId);
            return PartialView(viewName, actionList);
        }

        #endregion

        #region 左侧导航

        public ActionResult LeftMenu(string viewName = "_LeftMenu")
        {
            Maticsoft.BLL.Members.SiteMessage msgBll = new BLL.Members.SiteMessage();
            ViewBag.privatecount = msgBll.GetReceiveMsgNotReadCount(CurrentUser.UserID, -1);//未读私信的条数
            return View(viewName);
        }

        #endregion

        #region 收藏列表
        public ActionResult MyFavor(string viewName = "MyFavor")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的收藏" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }

        public PartialViewResult FavorList(int pageIndex = 1, string viewName = "_FavorList")
        {
            Maticsoft.BLL.Shop.Favorite favoBll = new BLL.Shop.Favorite();
            StringBuilder strBuilder = new StringBuilder();
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

        #endregion

        #region Ajax方法
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
                if (favBll.Exists(productId, CurrentUser.UserID, 1))
                {
                    return Content("Rep");
                }
                Maticsoft.Model.Shop.Favorite favMode = new Maticsoft.Model.Shop.Favorite();
                favMode.CreatedDate = DateTime.Now;
                favMode.TargetId = productId;
                favMode.Type = 1;
                favMode.UserId = CurrentUser.UserID;
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
                consultMode.UserId = CurrentUser.UserID;
                consultMode.UserName = CurrentUser.NickName;
                consultMode.UserEmail = CurrentUser.Email;
                consultMode.IsReply = false;
                consultMode.Recomend = 0;
                consultMode.ProductId = productId;
                consultMode.ConsultationText = content;
                return consultBll.Add(consultMode) > 0 ? Content("True") : Content("False");
            }
            return Content("False");
        }
        /// <summary>
        /// 用户评论
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                int productId = Common.Globals.SafeInt(Fm["ProductId"], 0);
                string productName = Fm["ProductName"];
                string content = Common.InjectionFilter.SqlFilter(Fm["Content"]);
                Maticsoft.BLL.Shop.Products.ProductReviews reviewBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
                Maticsoft.Model.Shop.Products.ProductReviews reviewMode = new Maticsoft.Model.Shop.Products.ProductReviews();
                reviewMode.CreatedDate = DateTime.Now;
                reviewMode.Status = 0;
                reviewMode.UserId = CurrentUser.UserID;
                reviewMode.UserName = CurrentUser.NickName;
                reviewMode.UserEmail = CurrentUser.Email;
                reviewMode.ParentId = 0;
                reviewMode.ProductId = productId;
                reviewMode.ReviewText = content;
                bool IsPost = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_Create_Post");
                return reviewBll.AddEx(reviewMode, productName, IsPost) ? Content("True") : Content("False");
            }
            return Content("False");
        }

        #endregion

        #region 用户绑定

        public ActionResult TestWeiBo()
        {
            return View();
        }

        public ActionResult UserBind()
        {
            ViewBag.Title = "会员中心—帐号绑定";
            Maticsoft.ViewModel.UserCenter.UserBindList model = userBind.GetListEx(CurrentUser.UserID);
            return View(model);
        }

        [HttpPost]
        public void CancelBind(FormCollection collection)
        {
            if (!String.IsNullOrWhiteSpace(collection["BindId"]))
            {
                Response.ContentType = "application/text";
                int bindId = Common.Globals.SafeInt(collection["BindId"], 0);
                if (userBind.Delete(bindId))
                {
                    Response.Write("success");
                }
            }
        }

        #endregion 用户绑定

        #region 我的推荐
        //
        // GET: /Tao/User/
        /// <summary>
        /// 我的邀请
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInvite()
        {
            BLL.CMS.Content contBll = new BLL.CMS.Content();
            ViewBag.Url = "Account/Register/" + Common.DEncrypt.Hex16.Encode(CurrentUser.UserID.ToString());
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.Shop);
            ViewBag.WebName = webSiteSet.WebName;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的邀请" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }
        /// <summary>
        ///  邀请列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult InviteList(int pageIndex = 1, string viewName = "_InviteList")
        {
            int _pageSize = 5;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = inviteBll.GetRecordCount(" InviteUserId=" + CurrentUser.UserID);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Model.Members.UserInvite> lists = inviteBll.GetListByPage(" InviteUserId=" + CurrentUser.UserID, startIndex, endIndex);
            PagedList<Model.Members.UserInvite> pagelist = new PagedList<Model.Members.UserInvite>(lists, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, pagelist);
            return PartialView(viewName, pagelist);
        }
        #endregion

        #region 充值
        /// <summary>
        /// 余额 
        /// </summary>
        public ActionResult Balance(string viewName = "Balance")
        {
            ViewBag.Activity = CurrentUser.Activity ? "有效" : "无效";
            ViewBag.Balance = userEXBll.GetUserBalance(CurrentUser.UserID);
            ViewBag.BalanceDraw = balanDrawBll.GetBalanceDraw(CurrentUser.UserID);
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "账户余额" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }
        /// <summary>
        ///  收支列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult BalanceDetList(int pageIndex = 1, string viewName = "_BalanceDetList")
        {
            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = balanDetaBll.GetRecordCount(" UserId =" + CurrentUser.UserID);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Pay.BalanceDetails> list = balanDetaBll.GetListByPage(" UserId = " + CurrentUser.UserID, startIndex, endIndex);
            PagedList<Maticsoft.Model.Pay.BalanceDetails> lists = new PagedList<Maticsoft.Model.Pay.BalanceDetails>(list, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        /// <summary>
        /// 充值明细
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult RechargeList(int pageIndex = 1, string viewName = "_RechargeList")
        {
            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = rechargeBll.GetRecordCount(" UserId =" + CurrentUser.UserID);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Pay.RechargeRequest> list = rechargeBll.GetRechargeListByPage(" UserId= " + CurrentUser.UserID, startIndex, endIndex);
            PagedList<Maticsoft.Model.Pay.RechargeRequest> lists = new PagedList<Maticsoft.Model.Pay.RechargeRequest>(list, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        /// <summary>
        ///  提现列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult DrawDetList(int pageIndex = 1, string viewName = "_DrawDetList", int conditionID = 1)
        {

            //condition 1:最近三月 2：最近一月 3：最近一周
            DateTime CurrentDate = DateTime.Now;
            DateTime AfterDate = new DateTime();
            switch (conditionID)
            {
                case 1: AfterDate = CurrentDate.AddMonths(-3); break;
                case 2: AfterDate = CurrentDate.AddMonths(-1); break;
                case 3: AfterDate = CurrentDate.AddDays(-7); break;
                default:
                    break;
            }

            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = balanDrawBll.GetRecordCount(" RequestType=1 AND UserId =" + CurrentUser.UserID + " AND RequestTime>='" + AfterDate + "'");//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Pay.BalanceDrawRequest> list = balanDrawBll.GetListByPage(" RequestType=1 AND UserId= " + CurrentUser.UserID + " AND RequestTime>='" + AfterDate + "'", startIndex, endIndex);
            PagedList<Maticsoft.Model.Pay.BalanceDrawRequest> lists = new PagedList<Maticsoft.Model.Pay.BalanceDrawRequest>(list, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult Recharge(string viewName = "Recharge")
        {
            decimal rechargeRatio = BLL.SysManage.ConfigSystem.GetDecimalValueByCache("Shop_RechargeRatio");
            if (rechargeRatio > decimal.MinusOne)
            {
                ViewBag.RechargeRatio = rechargeRatio;
            }
            ViewBag.UserName = CurrentUser.UserName;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "账户充值" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            List<Maticsoft.Payment.Model.PaymentModeInfo> list = Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Web).Where(o => o.AllowRecharge == true).ToList();//筛选出允许在线支付的
            return View(viewName, list);
        }
        /// <summary>
        /// 提交充值
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxRecharge(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["rechargmoney"]) && !String.IsNullOrWhiteSpace(Fm["payid"]))
            {
                int payid = Globals.SafeInt(Fm["payid"], 0);
                decimal rechMoney = Globals.SafeDecimal(Fm["rechargmoney"], decimal.Zero);
                if (payid > 0 && rechMoney > 0)
                {
                    #region 充值比例计算
                    decimal rechargeRadio = BLL.SysManage.ConfigSystem.GetDecimalValueByCache("Shop_RechargeRatio");
                    decimal money = rechMoney;
                    if (rechargeRadio > decimal.MinusOne)
                    {
                        money = Math.Round(rechMoney / rechargeRadio, 2);
                    }
                    #endregion

                    Model.Pay.RechargeRequest rechModel = new Model.Pay.RechargeRequest();
                    Payment.Model.PaymentModeInfo paymodel = Payment.BLL.PaymentModeManage.GetPaymentModeById(payid);
                    if (paymodel == null)
                    {
                        return Content("No");
                    }
                    rechModel.RechargeBlance = money;
                    rechModel.PaymentGateway = paymodel.Gateway;
                    rechModel.PaymentTypeId = payid;
                    rechModel.Status = 0;
                    rechModel.TradeDate = DateTime.Now;
                    rechModel.Tradetype = 1;
                    rechModel.UserId = CurrentUser.UserID;
                    long rechCode = rechargeBll.Add(rechModel);
                    if (rechCode > 0)
                    {
                        return Content(rechCode.ToString());
                    }
                }
            }
            return Content("No");
        }
        /// <summary>
        /// 确认请求 
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult RechargeConfirm(int? id, string viewName = "RechargeConfirm")
        {
            if (id.HasValue)
            {
                #region SEO 优化设置
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title = pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion
                Model.Pay.RechargeRequest rechmodel = rechargeBll.GetModelByCache(id.Value);
                if (rechmodel != null)
                {
                    ViewBag.RechargeId = id;
                    ViewBag.RechargeBlance = rechmodel.RechargeBlance;
                    return View(viewName);
                }
            }
            return Redirect(ViewBag.BasePath + "UserCenter/Recharge");
        }
        [HttpPost]
        public ActionResult DelRecharge(FormCollection fm)
        {
            long id = Globals.SafeLong(fm["Id"], 0);
            if (id <= 0)
            {
                return Content("False");
            }
            Model.Pay.RechargeRequest model = rechargeBll.GetModel(id);
            if (model != null && model.Status == 0)
            {
                if (rechargeBll.Delete(id))
                {
                    return Content("True");
                }
            }
            return Content("False");
        }

        #endregion

        #region 我的健康卡
        public ActionResult HLCard()
        {
            return View();
        }

        /// <summary>
        /// 获取用户的卡片，单个卡
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserCard()
        {
            User u = Session["UserInfo"] as User;
            List<Shop_CardUserInfo> userinfoList = carduserInfoBll.GetModelListByName(u.UserName);
            if (userinfoList != null)
            {
                var userinfoItem = userinfoList.FirstOrDefault();

                UserCardLogic uc = new UserCardLogic();

               var userinfo =  uc.GetActiveUserInfos(userinfoItem.CardNo, userinfoItem.Password);
               
               //保单文件路径
               if (typeof(V_DriveCardInfo).IsInstanceOfType(userinfo))
               {
                   return View("GetUserCardII", userinfo);
               }

               return View(userinfo);
            }
            return View(null);         

        }

        public ActionResult HLCardActive()
        {
            ViewBag.RegisterType = Request.Params["type"];
            ViewBag.SalesName = Request.Params["SalesName"];
            ViewBag.cardNum = Request.Params["CardNum"];
            ViewBag.CardSysId = Request.Params["CardSysId"];
            ViewBag.Pwd = Request.Params["password"];
            Shop_CardType cardType = Session["Shop_CardType"] as Shop_CardType;
            Shop_Card card = Session["Shop_Card"] as Shop_Card;
            if (card != null)
            {
                ViewBag.CardTypeNo = card.CardTypeNo;
                ViewBag.Batch = card.Batch;
            }
            ViewBag.Agreement = cardType == null ? "" : cardType.Agreement;
            ViewBag.ActivatePrompt = cardType == null ? "" : cardType.ActivatePrompt;
            return View();
        }

        public ActionResult HLCardActiveInstead()
        {
            ViewBag.SalesName = Request.Params["SalesName"];
            ViewBag.cardNum = Request.Params["CardNum"];
            ViewBag.CardSysId = Request.Params["CardSysId"];
            ViewBag.Pwd = Request.Params["password"];
            Shop_Card card = Session["Shop_Card"] as Shop_Card;
            if (card != null)
            {
                ViewBag.CardTypeNo = card.CardTypeNo;
                ViewBag.Batch = card.Batch;
            }
            Shop_CardType cardType = Session["Shop_CardType"] as Shop_CardType;
            ViewBag.Agreement = cardType == null ? "" : cardType.Agreement;
            ViewBag.ActivatePrompt = cardType == null ? "" : cardType.ActivatePrompt;
            return View();
        }


        public void CardUserInfoInstead(Shop_CardUserInfo userInfo)
        {
            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("代激活日志").Write("开始代激活处理.....");
            userInfo.ActiveDate = DateTime.Now;
            userInfo.CREATEDATE = DateTime.Now;
            userInfo.CREATER = currentUser.UserName;
            bool Instead = false;
            //根据身份证号查询是否之前激活过健康卡，是：将该健康卡绑定到当前代激活用户账号上，否：创建用户并绑定健康卡
            List<Shop_CardUserInfo> userInfoList = carduserInfoBll.GetModelList(" CardId='"+userInfo.CardId+"'");
            var DisList = (from rec in userInfoList select new { rec = rec.UserId }).ToList().Distinct();
            if (DisList.Count()==1)
            {
                //将该健康卡绑定到当前代激活用户账号上
                userInfo.UserId = userInfoList.First().UserId;
            }
            else if (DisList.Count() == 0)
            {
                //userInfo.UserId = userInfo.CardNo;
                Instead = true;
            }
            else
            {

            }
            string Batch = "";
            try
            {
                userInfo.CardTypeNo = Request.Form["CardTypeNo"];
                Batch = Request.Form["Batch"].ToString();
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("用户:" + currentUser.UserName + "   CardTypeNo:" + userInfo.CardTypeNo + ",Batch:" + Batch);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("用户:" + currentUser.UserName + "   错误信息:" + ex.Message);
                Response.Write("获取卡类型编号或卡批次号失败,请联系客服！");
            }
            Active(userInfo, Batch, Instead);

        }

        public void CardUserInfo(Shop_CardUserInfo userInfo)
        {
            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("开始激活处理.....");
            User u = Session["UserInfo"] as User;
            try
            {
                userInfo.ActiveDate = DateTime.Now;
                userInfo.CREATEDATE = DateTime.Now;
                userInfo.CREATER = u.UserName;
                //userInfo.UserId = u.UserName;

                string Batch = "";
                try
                {
                    userInfo.CardTypeNo = Request.Form["CardTypeNo"];
                    Batch = Request.Form["Batch"].ToString();
                    Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("用户:" + u.UserName + "   CardTypeNo:" + userInfo.CardTypeNo + ",Batch:" + Batch);
                }
                catch (Exception ex)
                {
                    Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("用户:" + u.UserName + "   错误信息:" + ex.Message);
                    Response.Write("获取卡类型编号或卡批次号失败,请联系客服！");
                }
                Active(userInfo, Batch);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("结束处理......" + ex.InnerException.Message + ",ex:" + ex);
            }
            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("结束处理......");
        }

        public void Active(Shop_CardUserInfo userInfo,string Batch,bool Instead=false)
        {
            UserCardLogic uc = new UserCardLogic();
            string result = "";
            try
            {
                result = uc.ActiveUserInfo(userInfo,Instead);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.InnerException.Message);
                Response.Write("健康卡激活异常,请联系客服！错误信息：" + ex.InnerException.Message + ",msg:" + ex);
            }
            try
            {
                string errorMsg = "";
                string content = "您的健康卡:" + userInfo.CardNo + "已经激活!";
                if (result == "1")
                {

                    if (Getcoupons(Batch, currentUser.UserID) == 0)
                    {
                        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("优惠券自动发送日志").Write("用户:" + currentUser.UserName + "   优惠劵批次号:" + Batch);
                    }
                    if (!string.IsNullOrEmpty(userInfo.CardId))
                    {
                        Maticsoft.Web.Components.SMSHelper.SendSMS(userInfo.Moble, content, out errorMsg);
                    }
                }
                Response.Write(result);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.InnerException.Message);
            }
        }


        //根据规则自动发送优惠券
        public int Getcoupons(string batchid, int userID)
        {
            Maticsoft.BLL.Shop_CouponRuleExt cop = new Maticsoft.BLL.Shop_CouponRuleExt();
            var list = cop.GetListByBatchID(batchid);
            foreach (var item in list)
            {
                if (!cop.SetUserCoupon(userID, item.CouponCount, item.ClassID))
                {
                    return 0;
                }
            }
            return 1;
        }
        [HttpPost]
        public void SetDefaultCard(string cardNo)
        {

            UserCardLogic uc = new UserCardLogic();
            Response.Write(uc.SetDefaultCard(cardNo, currentUser.UserName));
        }
        #endregion

        #region 提现
        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult Draw(string viewName = "Draw")
        {
            ViewBag.Balance = userEXBll.GetUserBalance(CurrentUser.UserID);
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "申请提现" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }

        /// <summary>
        /// 申请提现 ajax请求
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult AjaxDraw(FormCollection Fm)
        {
            decimal amount = Globals.SafeDecimal(Fm["Amount"], 0);
            int typeid = Globals.SafeInt(Fm["Type"], -1);
            string bankcard = InjectionFilter.Filter(Fm["BankCard"]);
            if (amount <= 0 || typeid <= 0 || String.IsNullOrWhiteSpace(bankcard))
            {
                return Content("no");
            }
            string trueName = "";
            string bankName = "";
            if (typeid == 1) //帐号类型为银行卡
            {
                trueName = InjectionFilter.Filter(Fm["TrueName"]);
                bankName = InjectionFilter.Filter(Fm["BankName"]);
                if (String.IsNullOrWhiteSpace(trueName) || String.IsNullOrWhiteSpace(bankName))
                {
                    return Content("no");
                }
            }
            if (amount > userEXBll.GetUserBalance(CurrentUser.UserID))//提现金额大于余额
            {
                return Content("low");//余额不足
            }
            Model.Pay.BalanceDrawRequest balanDrawModel = new Model.Pay.BalanceDrawRequest();
            balanDrawModel.Amount = amount;
            balanDrawModel.BankCard = bankcard;
            balanDrawModel.CardTypeID = typeid;
            balanDrawModel.RequestStatus = 1;
            balanDrawModel.RequestTime = DateTime.Now;
            if (typeid == 1)
            {
                balanDrawModel.BankName = bankName;
                balanDrawModel.TrueName = trueName;
            }
            balanDrawModel.UserID = CurrentUser.UserID;
            if (balanDrawBll.AddEx(balanDrawModel))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }


        #endregion

        #region  物流信息
        public PartialViewResult ExpressList(string ecode, string viewName = "_ExpressList")
        {
            Maticsoft.BLL.Shop.Shipping.Express bll = new BLL.Shop.Shipping.Express();
            List<Maticsoft.Model.Shop.Shipping.LastData> model = new List<LastData>();
            List<Maticsoft.Model.Shop.Shipping.Shop_Express> list = bll.GetListModel("ExpressCode='" + ecode + "'", "UpdateTime",0);
            if (list!=null && list.Count > 0)
            {
                model = comm.JsonToObject<List<Maticsoft.Model.Shop.Shipping.LastData>>(list[0].ExpressContent.ToString());
            }
            return PartialView(viewName, model);
        }
        #endregion

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
    }
}
