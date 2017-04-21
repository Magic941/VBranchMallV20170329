using Maticsoft.BLL.Shop.Products;
using Maticsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppServices.Controllers
{
    public class AccountController : ApiController
    {
        

        #region 成员变量

        private Maticsoft.Accounts.Bus.User userBusManage = new Maticsoft.Accounts.Bus.User();
        private Maticsoft.BLL.Members.Users userManage = new Maticsoft.BLL.Members.Users();
        private Maticsoft.BLL.Members.UsersExp userExpManage = new Maticsoft.BLL.Members.UsersExp();
        private readonly Maticsoft.BLL.Shop_CardUserInfo carduserInfoBll = new Maticsoft.BLL.Shop_CardUserInfo();


        #endregion
   

        //#region Ajax验证

        ///// <summary>
        ///// 验证用户名是否已存在
        ///// </summary>
        //[HttpGet]
        //public JsonObject IsExistUserName(string userName)
        //{
        //    JsonObject json = new JsonObject();
        //    bool valid = !(userBusManage.HasUserByUserName(userName));
        //    return Json(valid, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 验证昵称是否已存在
        ///// </summary>
        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        //public JsonResult IsExistNickName(string nickName)
        //{
        //    bool valid = !(userBusManage.HasUserByNickName(nickName));
        //    return Json(valid, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 验证Phone是否已存在
        ///// </summary>
        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        //public JsonResult IsExistPhone(string phone)
        //{
        //    var userbll = new BLL.Shop.Account.User();
        //    bool valid = !(userbll.CheckPhoneExits(phone));
        //    //bool valid = !(userBusManage.HasUserByPhone(phone));
        //    return Json(valid, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 设置手机号为已验证
        ///// </summary>
        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        //public bool SetPhoneMark()
        //{
        //    string UserName = Request.Params["UserName"];
        //    string Phone = Request.Params["Phone"];
        //    Maticsoft.BLL.Shop.Account.User _bllUser = new BLL.Shop.Account.User();
        //    return _bllUser.SetPhoneMark(UserName, Phone);
        //}
        ///// <summary>
        ///// 判断手机号是否已经被验证过
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public bool CheckPhoneVerify()
        //{
        //    string Phone = Request.Params["Phone"];
        //    Maticsoft.BLL.Shop.Account.User _bllUser = new BLL.Shop.Account.User();
        //    return _bllUser.CheckPhoneVerify(Phone);
        //}

        ///// <summary>
        ///// 验证Email是否已存在
        ///// </summary>
        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        //public JsonResult IsExistEmail(string email)
        //{
        //    bool valid = !(userBusManage.HasUserByEmail(email));
        //    return Json(valid, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Ajax发送短信
        ///// </summary>
        ///// <param name="Fm"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult SendSMS(string phone, string SmsValCode)
        //{
        //    if (string.IsNullOrWhiteSpace(SmsValCode))
        //    {
        //        return Content("False");
        //    }
        //    string SmsCheckCode = Session["CheckCode"].ToString();
        //    if (String.IsNullOrWhiteSpace(SmsCheckCode))
        //    {
        //        return Content("SmsFalse");
        //    }
        //    else
        //    {
        //        if (SmsCheckCode.ToLower() != SmsValCode.ToLower())
        //        {
        //            return Content("SmsFalse");
        //        }
        //    }
        //    string Phone = phone;
        //    if (String.IsNullOrWhiteSpace(Phone))
        //    {
        //        return Content("False");
        //    }
        //    if (!IsSendSmsMaxCount())
        //    {
        //        return Content("False");
        //    }
        //    Random rnd = new Random();
        //    int rand = rnd.Next(100000, 999999);
        //    string content = "尊敬的会 员,您好邻商城的注册验证码为：" + rand.ToString() + ",请及时输入.";
        //    Session["SMSCode"] = rand;
        //    Session["SMS_DATE"] = DateTime.Now;
        //    string[] numbers = new string[] { Phone };
        //    string errorMsg = "";
        //    bool isSuccess = Maticsoft.Web.Components.SMSHelper.SendSMS(Phone, content, out errorMsg);
        //    return isSuccess ? Content("True") : Content("False");
        //    //return Content("True");
        //}



        ///// <summary>
        ///// Ajax发送短信
        ///// </summary>
        ///// <param name="Fm"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult SendSMSForPwd(string phone, string Code)
        //{
        //    string code = Code;
        //    string SmsCheckCode = Session["CheckCode"].ToString();
        //    if (String.IsNullOrWhiteSpace(code))
        //    {
        //        return Content("SmsValCode");
        //    }
        //    else
        //    {
        //        if (code.ToLower() != SmsCheckCode.ToLower())
        //        {
        //            return Content("SmsValCode");
        //        }
        //    }
        //    if (!IsSendSmsMaxCount())
        //    {
        //        return Content("False");
        //    }
        //    string Phone = phone;
        //    if (String.IsNullOrWhiteSpace(Phone))
        //    {
        //        return Content("False");
        //    }
        //    if (!IsSendSmsMaxCount())
        //    {
        //        return Content("False");
        //    }
        //    Random rnd = new Random();
        //    int rand = rnd.Next(100000, 999999);
        //    string content = "您好邻商城正在进行重置密码操作,验证码为：" + rand.ToString();
        //    Session["SMSCode"] = rand;
        //    Session["SMS_DATE"] = DateTime.Now;
        //    string[] numbers = new string[] { Phone };
        //    string errorMsg = "";
        //    bool isSuccess = Maticsoft.Web.Components.SMSHelper.SendSMS(Phone, content, out errorMsg);
        //    return isSuccess ? Content("True") : Content("False");
        //    //return Content("True");
        //}

        ///// <summary>
        ///// 比较一个IP一天最多可以发生多少条信息如果超过不发送
        ///// </summary>
        ///// <returns></returns>
        //public bool IsSendSmsMaxCount()
        //{
        //    bool flag = true;
        //    string IP = GetClientIp();
        //    string CacheKey = "SMSIP-" + IP;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(23).AddMinutes(59);
        //    if (objModel == null)
        //    {
        //        objModel = 0;
        //    }
        //    Maticsoft.Common.DataCache.SetCache(CacheKey, int.Parse(objModel.ToString()) + 1, today, TimeSpan.Zero);

        //    int SendCount = int.Parse(objModel.ToString()) + 1;
        //    int SmsMaxCount = int.Parse(ConfigurationManager.AppSettings["SmsMaxCount"]);
        //    if (SendCount > SmsMaxCount)
        //    {
        //        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("短信发送日志超过条数的IP").Write("C网IP为:" + IP);
        //        flag = false;
        //    }
        //    return flag;
        //}
        ///// <summary>
        ///// 获取客户端Ip
        ///// </summary>
        ///// <returns></returns>
        //public String GetClientIp()
        //{

        //    HttpRequest request = System.Web.HttpContext.Current.Request;
        //    string clientIP = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(clientIP))
        //    { clientIP = request.ServerVariables["REMOTE_ADDR"]; }
        //    if (string.IsNullOrEmpty(clientIP)) { clientIP = request.UserHostAddress; }
        //    if (string.IsNullOrEmpty(clientIP)) { clientIP = "0.0.0.0"; }

        //    return clientIP;
        //}


        ///// <summary>
        ///// 发送密码短信
        ///// </summary>
        ///// <param name="randCode">密码</param>
        ///// <param name="phone">手机号</param>
        ///// <returns></returns>
        //private bool SendPwd(ref string randCode, string phone)
        //{
        //    string Phone = phone;
        //    if (String.IsNullOrWhiteSpace(Phone))
        //    {
        //        return false;
        //    }
        //    Random rnd = new Random();
        //    int rand = rnd.Next(100000, 999999);
        //    string content = "您正在进行找回密码操作，新密码为：" + rand.ToString() + "。如非本人操作请忽略【好邻商城】：";
        //    Session["SMSCode"] = rand;
        //    Session["SMS_DATE"] = DateTime.Now;
        //    string[] numbers = new string[] { Phone };
        //    string errorMsg = "";
        //    bool isSuccess = Maticsoft.Web.Components.SMSHelper.SendSMS(Phone, content, out errorMsg);
        //    if (isSuccess)
        //    {
        //        randCode = rand.ToString();
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        ///// <summary>
        ///// 验证效验码
        ///// </summary>
        ///// <param name="Fm"></param>
        ///// <returns></returns>
        //public ActionResult VerifiyCode(FormCollection Fm)
        //{
        //    if (Session["SMSCode"] == null || String.IsNullOrWhiteSpace(Session["SMSCode"].ToString()))
        //    {
        //        return Content("False");
        //    }

        //    string code = Fm["SMSCode"];
        //    return code == Session["SMSCode"].ToString() ? Content("True") : Content("False");
        //}


        //#endregion


        //#region 登录
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.RegisterToggle = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式
        //    BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.Shop);
        //    ViewBag.WebName = webSiteSet.WebName;
        //    bool IsCloseLogin = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
        //    if (IsCloseLogin)
        //    {
        //        return RedirectToAction("TurnOff", "Error");
        //    }
        //    if (!string.IsNullOrWhiteSpace(returnUrl))
        //        ViewBag.returnUrl = returnUrl;
        //    if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
        //    {
        //        //使用returnUrl跳转
        //        if (!string.IsNullOrWhiteSpace(returnUrl)) return Redirect(returnUrl);
        //        return RedirectToAction("Orders", "UserCenter");
        //    }

        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
        //    ViewBag.Title = "登录" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(LogOnModel model, string returnUrl)
        //{
        //    string regStr = string.Empty;
        //    const string regPattern = @"^1[34578]\d{9}$";
        //    const string regUserName = @"^[a-zA-Z0-9_]{3,10}$";
        //    if (model.UserName.Contains("@"))
        //    {
        //        regStr = "Email";
        //    }
        //    else if (Regex.IsMatch(model.UserName, regUserName) && model.UserName.Length < 11)
        //    {
        //        regStr = "UserName";
        //    }
        //    else if (Regex.IsMatch(model.UserName, regPattern))
        //    {
        //        regStr = "Phone";
        //    }
        //    else
        //    {
        //        regStr = "HaoLinCard";
        //    }

        //    bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
        //    if (IsCloseLogin)
        //    {
        //        return RedirectToAction("TurnOff", "Error");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        #region SEO 优化设置
        //        IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //        ViewBag.Title = "登录" + pageSetting.Title;
        //        ViewBag.Keywords = pageSetting.Keywords;
        //        ViewBag.Description = pageSetting.Description;
        //        #endregion

        //        AccountsPrincipal userPrincipal = null;
        //        if (regStr == "Phone")
        //        {
        //            BLL.Shop.Account.User UserManage = new BLL.Shop.Account.User();
        //            User user = new User();
        //            user = UserManage.GetPhoneUser(model.UserName, model.Password);
        //            if (user != null)
        //                userPrincipal = new AccountsPrincipal(user.UserID);
        //        }
        //        else if (regStr == "Email")
        //        {
        //            userPrincipal = AccountsPrincipal.ValidateLogin4Email(model.UserName, model.Password);
        //        }
        //        else if (regStr == "UserName")
        //        {
        //            userPrincipal = AccountsPrincipal.ValidateLogin(model.UserName, model.Password);
        //        }
        //        else
        //        {
        //            Maticsoft.BLL.Shop_CardUserInfo cardUserInfo = new BLL.Shop_CardUserInfo();
        //            string pwdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "md5").ToLower();
        //            List<Model.Shop_CardUserInfo> list = cardUserInfo.GetModelList("CardNo ='" + model.UserName + "' and [Password]='" + pwdStr + "'");
        //            if (list.Count > 0)
        //            {
        //                string uid = list[0].UserId;
        //                userPrincipal = new AccountsPrincipal(uid);
        //            }
        //        }
        //        if (userPrincipal == null)
        //        {
        //            ModelState.AddModelError("Message", "用户名或密码不正确, 请重新输入!");
        //            return View(model);
        //        }
        //        User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
        //        if (!currentUser.Activity)
        //        {
        //            ModelState.AddModelError("Message", "对不起，该帐号已被冻结或未激活，请联系管理员！");
        //            return View(model);
        //        }
        //        HttpContext.User = userPrincipal;
        //        FormsAuthentication.SetAuthCookie(currentUser.UserName, model.RememberMe);
        //        Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
        //        //登录成功加积分
        //        Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
        //        int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");

        //        //加载Shop模块的购物车
        //        if (MvcApplication.MainAreaRoute == AreaRoute.Shop)
        //        {
        //            BLL.Shop.Products.ShoppingCartHelper.LoadShoppingCart(currentUser.UserID);
        //        }

        //        //从卡激活中过来的登录
        //        if (model.Source == "1")
        //        {
        //            //如果为空则返回空的视图
        //            if (string.IsNullOrEmpty(returnUrl)) return new EmptyResult();
        //        }

        //        returnUrl = Server.UrlDecode(returnUrl);
        //        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Orders", "UserCenter");
        //        }
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult AjaxLogin(string UserName, string UserPwd)
        //{
        //    bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
        //    if (IsCloseLogin)
        //    {
        //        return Content("-1");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(UserName, UserPwd);
        //        if (userPrincipal != null)
        //        {
        //            User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
        //            if (!currentUser.Activity)
        //            {
        //                ModelState.AddModelError("Message", "对不起，该帐号已被冻结，请联系管理员！");
        //                return Content("NotActivity");
        //            }
        //            else
        //            {
        //                //if (currentUser.UserType == "AA")
        //                //{
        //                //    ModelState.AddModelError("Message", "您是管理员用户，您没有权限登录后台系统！") ;                        
        //                //}
        //                HttpContext.User = userPrincipal;
        //                FormsAuthentication.SetAuthCookie(UserName, true);
        //                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
        //                //登录成功加积分
        //                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
        //                int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");
        //                return Content("1|" + pointers.ToString());
        //            }
        //        }
        //        else
        //        {
        //            return Content("0");
        //        }
        //    }
        //    return Content("0");
        //}
        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Remove(Globals.SESSIONKEY_USER);
        //    Session.Clear();
        //    Session.Abandon();
        //    return RedirectToAction("Index", "Home");
        //}
        //#endregion

        //#region 注册
        //public ActionResult Register(string id)
        //{
        //    ViewBag.type = Request.Params["type"];
        //    ViewBag.InviteID = id;
        //    BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.WebName = webSiteSet.WebName;
        //    bool IsCloseRegister = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
        //    ViewBag.SMSIsOpen = true; //BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
        //    if (IsCloseRegister)
        //    {
        //        return RedirectToAction("TurnOff", "Error");
        //    }
        //    if (CurrentUser != null && CurrentUser.UserType != "AA") return RedirectToAction("Index", "UserCenter");

        //    ViewBag.RegisterToggle = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式

        //    BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);
        //    RegisterModel regModel = new RegisterModel();
        //    regModel.UserAgreement = WebSiteSet.RegistStatement;
        //    ViewBag.Seconds = 0;
        //    if (Session["SMS_DATE"] != null && !String.IsNullOrWhiteSpace(Session["SMS_DATE"].ToString()))
        //    {
        //        DateTime smsDate = Globals.SafeDateTime(Session["SMS_DATE"].ToString(), DateTime.MinValue);
        //        if (smsDate != DateTime.MinValue)
        //        {
        //            TimeSpan smsSeconds = smsDate.AddSeconds(60) - DateTime.Now;
        //            ViewBag.Seconds = (int)smsSeconds.TotalSeconds;
        //        }
        //    }

        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "注册" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion
        //    return View(regModel);
        //}

        //public ActionResult PhoneRegister(RegisterModel model)
        //{
        //    return View();
        //}

        //public ActionResult EmailRegister()
        //{
        //    return View();
        //}

        //#region 好邻卡激活相关
        //public ActionResult HLCard()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public bool ExistsUserName()
        //{
        //    UserCardLogic u = new UserCardLogic();
        //    if (u.CheckUserName(Request.Params["UserName"]))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //public ActionResult HLRegAgreement()
        //{
        //    return View();
        //}

        //public ActionResult ConfirmAgreement(Shop_CardUserInfo userinfo)
        //{
        //    Shop_CardType cardType = Session["Shop_CardType"] as Shop_CardType;
        //    ViewBag.Agreement = cardType.Agreement;
        //    ViewBag.ActivatePrompt = cardType.ActivatePrompt;

        //    Session["CardUserInfo"] = userinfo;

        //    return View();
        //}

        //public ActionResult ActiveSuccess()
        //{
        //    var userInfo = Session["CardUserInfo"] as Shop_CardUserInfo;
        //    if (userInfo.CardId == null)
        //    {
        //        //模拟登录
        //        Login(new LogOnModel() { UserName = userInfo.UserName, Password = userInfo.Password }, string.Empty);
        //    }
        //    else
        //    {
        //        Login(new LogOnModel() { UserName = userInfo.CardNo, Password = userInfo.Password }, string.Empty);
        //    }
        //    return View();
        //}

        //public ActionResult HLCardActive()
        //{
        //    Shop_CardType cardType = Session["Shop_CardType"] as Shop_CardType;

        //    Shop_Card card = Session["Shop_Card"] as Shop_Card;
        //    if (card != null)
        //    {
        //        ViewBag.CardTypeNo = card.CardTypeNo;
        //        ViewBag.Batch = card.Batch;
        //        ViewBag.SalesName = card.SalesName;
        //        ViewBag.cardNum = card.CardNo;
        //        ViewBag.CardSysId = card.Id;
        //        ViewBag.Pwd = card.Password;
        //    }
        //    if (cardType != null)
        //    {
        //        ViewBag.RegisterType = cardType.RegisterType ? "simple" : "normal";
        //        ViewBag.PersonNum = cardType.PersonNum;
        //    }

        //    return View();
        //}

        ///// <summary>
        ///// 最后一步开始激活，信息从上一级传递下来
        ///// </summary>
        //public void CardUserInfo()
        //{
        //    var userInfo = Session["CardUserInfo"] as Shop_CardUserInfo;

        //    Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("开始激活处理.....,用户身份证为：" + userInfo.CardId);
        //    User u = Session["UserInfo"] as User;
        //    Shop_Card card = Session["Shop_Card"] as Shop_Card;
        //    //add cardsysid 
        //    userInfo.CardSysId = card.Id;
        //    userInfo.CardTypeNo = card.CardTypeNo;
        //    bool Instead = false;
        //    try
        //    {
        //        userInfo.ActiveDate = DateTime.Now;
        //        userInfo.CREATEDATE = DateTime.Now;

        //        if (userInfo.CardId == null)
        //        {
        //            //简单激活 以卡号创建账户
        //            userInfo.UserId = userInfo.UserName;
        //            userInfo.Password = userInfo.S_Password;
        //            Instead = true;
        //        }
        //        else
        //        {
        //            //根据身份证号查询是否之前激活过信客卡，是：将该信客卡绑定到当前代激活用户账号上，否：创建用户并绑定信客卡
        //            List<Shop_CardUserInfo> userInfoList = carduserInfoBll.GetModelList(" CardId='" + userInfo.CardId + "'");
        //            var DisList = (from rec in userInfoList select new { rec = rec.UserId }).ToList().Distinct();
        //            if (DisList.Count() >= 1)
        //            {
        //                //将该信客卡绑定到当前代激活用户账号上
        //                userInfo.UserId = userInfoList.First().UserId;
        //            }
        //            else if (DisList.Count() == 0)
        //            {
        //                userInfo.UserId = userInfo.CardNo;
        //                Instead = true;
        //            }

        //        }

        //        Active(userInfo, card.Batch, Instead);
        //    }
        //    catch (Exception ex)
        //    {
        //        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志-异常").Write("结束处理......" + ex.InnerException.Message + ",ex:" + ex);
        //    }
        //    Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("结束处理......");
        //}

        ///// <summary>
        ///// 最后一步简单卡,由于没有用户资料，所以需要从基本资料中直接传递
        ///// </summary>
        ///// <param name="userinfo"></param>
        //public void CardUserInfoSimple(Shop_CardUserInfo userInfo)
        //{
        //    //var userInfo = Session["CardUserInfo"] as Shop_CardUserInfo;

        //    Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("开始激活处理.....,用户身份证为：" + userInfo.CardId);
        //    User u = Session["UserInfo"] as User;
        //    Shop_Card card = Session["Shop_Card"] as Shop_Card;


        //    //add cardsysid 
        //    userInfo.CardSysId = card.Id;
        //    userInfo.CardTypeNo = card.CardTypeNo;
        //    bool Instead = false;
        //    try
        //    {
        //        userInfo.ActiveDate = DateTime.Now;
        //        userInfo.CREATEDATE = DateTime.Now;

        //        if (userInfo.CardId == null)
        //        {
        //            //简单激活 以卡号创建账户
        //            userInfo.UserId = userInfo.UserName;
        //            userInfo.Password = userInfo.S_Password;
        //            userInfo.S_Password = card.Password;
        //            Instead = true;
        //        }
        //        else
        //        {

        //            userInfo.S_Password = card.Password;
        //            //根据身份证号查询是否之前激活过信客卡，是：将该信客卡绑定到当前代激活用户账号上，否：创建用户并绑定信客卡
        //            List<Shop_CardUserInfo> userInfoList = carduserInfoBll.GetModelList(" CardId='" + userInfo.CardId + "'");
        //            var DisList = (from rec in userInfoList select new { rec = rec.UserId }).ToList().Distinct();
        //            if (DisList.Count() >= 1)
        //            {
        //                //将该信客卡绑定到当前代激活用户账号上
        //                userInfo.UserId = userInfoList.First().UserId;
        //            }
        //            else if (DisList.Count() == 0)
        //            {
        //                userInfo.UserId = userInfo.CardNo;
        //                Instead = true;
        //            }

        //        }

        //        Active(userInfo, card.Batch, Instead);
        //        Session["CardUserInfo"] = userInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志-异常").Write("结束处理......" + ex.InnerException.Message + ",ex:" + ex);
        //    }
        //    Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("结束处理......");
        //}

        //public void Active(Shop_CardUserInfo userInfo, string Batch, bool Instead = false)
        //{
        //    UserCardLogic uc = new UserCardLogic();
        //    Shop_Card card = Session["Shop_Card"] as Shop_Card;

        //    userInfo.S_Password = card.Password;

        //    string result = "";
        //    try
        //    {

        //        result = uc.ActiveUserInfo(userInfo, Instead);
        //    }
        //    catch (Exception ex)
        //    {
        //        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.InnerException.Message);
        //        Response.Write("好邻卡激活异常,请联系客服！错误信息：" + ex.Message + ",msg:" + ex);
        //    }
        //    try
        //    {
        //        string errorMsg = "";
        //        string content = "您的好邻卡:" + userInfo.CardNo + "已经激活!";
        //        if (result == "1")
        //        {
        //            //根据用户名查找 userInfo.UserId
        //            Maticsoft.Accounts.Bus.User u = new Maticsoft.Accounts.Bus.User();
        //            int userid = u.GetUserByName(userInfo.UserId).UserID;
        //         //  Maticsoft.Common.ErrorLogTxt.GetInstance("线程调试日志").Write("主线程的id为:" + Thread.CurrentThread.ManagedThreadId);


        //              Getcoupons(Batch, userid,userInfo.CardNo);


        //            if (!string.IsNullOrEmpty(userInfo.CardId))
        //            {
        //                Maticsoft.Web.Components.SMSHelper.SendSMS(userInfo.Moble, content, out errorMsg);
        //                //Task.Run(() =>
        //                //{
        //                //    MailSender(userInfo.UserName, userInfo.Email, userInfo.CardNo);
        //                //});
        //            }
        //        }
        //        Response.Write(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.Message);
        //    }
        //}

        //public async void MailSender(string username,string EmailUrl,string CardNo)
        //{
        //    var el = new EmailTemplet();
        //    el.SendHaolinCardEmail(username, EmailUrl, CardNo);
        //    await Task.Delay(1000);
        //}


        ////根据规则自动发送优惠券
        //public  void Getcoupons(string batchid, int userID,string cardno)
        //{
        //  //  Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("线程调试日志").Write("coupons的线程id为:" + Thread.CurrentThread.ManagedThreadId);
        //    Maticsoft.BLL.Shop_CouponRuleExt cop = new Maticsoft.BLL.Shop_CouponRuleExt();
        //    var list = cop.GetListByBatchID(batchid);
        //    var sc = CouponServices.GetInstance();


        //    list.ForEach(a => a.UserID = userID);

        //    Maticsoft.Services.ErrorLogTxt.GetInstance("进来过11").Write("数量为：" + list.Count);
        //    sc.SendMessageQuque<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>(list, path);
        //    //backup queue
        //    sc.SendMessageQuque<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>(list, card_Path,cardno);
        //}

        //#endregion

        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "注册" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion
        //    int uid = -1;
        //    if (!String.IsNullOrWhiteSpace(Request.Form["inviteid"]))
        //    {
        //        string id = Common.DEncrypt.Hex16.Decode(Request.Form["inviteid"]);
        //        uid = Globals.SafeInt(id, -1);
        //    }
        //    bool IsCloseRegister = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
        //    if (IsCloseRegister)
        //    {
        //        return RedirectToAction("TurnOff", "Error");
        //    }


        //    string regStr = model.UserName.Contains("@") ? "Email" : "Phone";//BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式         
        //    ViewBag.RegisterToggle = regStr;
        //    bool isOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//手机验证
        //    ViewBag.SMSIsOpen = isOpen;

        //    var userbll = new BLL.Shop.Account.User();
        //    //if (ModelState.IsValid)
        //    //{
        //        //判断昵称是否已存在
        //        if (userBusManage.HasUserByNickName(model.NickName))
        //        {
        //            ViewBag.hasnickname = "昵称已被抢先使用，换一个试试";
        //            ModelState.AddModelError("nickname", "昵称已被抢先使用，换一个试试");
        //            return View(model);
        //        }

        //        if (regStr == "Phone")
        //        {
        //            if (userbll.CheckPhoneExits(model.UserName))
        //            {
        //                ViewBag.hasemail = "该手机已被注册";
        //                return View(model);
        //            }
        //        }
        //        else
        //        {
        //            if (userBusManage.HasUserByUserName(model.UserName))
        //            {
        //                ViewBag.hasemail = "该邮箱已被注册";
        //                return View(model);
        //            }
        //        }


        //        bool IsCloseRegisterSendEmail = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_RegisterEmailCheck");
        //        User newUser = new User();
        //        //DONE: 警告DB字段未对应: Email 字段 varchar(100) UserName 字段 varchar(50) 已完成 BEN DONE 2012-11-22
        //        if (regStr == "Phone")
        //        {
        //            newUser.UserName = model.UserName + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //        }
        //        else
        //        {
        //            newUser.UserName = model.UserName;
        //        }


        //        newUser.NickName = model.NickName;  //昵称名称相同
        //        if (model.Password != null)
        //        {
        //            newUser.Password = AccountsPrincipal.EncryptPassword(model.Password);
        //        }
        //        if (regStr == "Phone")
        //        {
        //            newUser.Phone = model.UserName;
        //        }
        //        else
        //        {
        //            newUser.Email = model.UserName;
        //        }
        //        //if (regStr == "Phone" ||  IsCloseRegisterSendEmail) //手机号码注册   或者  关闭邮箱验证
        //        newUser.Activity = true;
        //        //else //开启
        //        //    newUser.Activity = false;
        //        newUser.UserType = "UU";
        //        newUser.Style = 1;
        //        newUser.User_dateCreate = DateTime.Now;
        //        newUser.User_cLang = "zh-CN";
        //        int userid = newUser.Create();
        //        if (userid == -100)
        //        {
        //            ModelState.AddModelError("Message", ErrorCodeToString(MembershipCreateStatus.DuplicateUserName));
        //        }
        //        else
        //        {
        //            if (regStr == "Phone")
        //            {
        //                var u = new BLL.Shop.Account.User();
        //                u.SetPhoneMark(newUser.UserName, newUser.Phone);
        //                FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
        //                Session[Globals.SESSIONKEY_USER] = model;
        //            }
        //            //添加用户扩展表数据
        //            BLL.Members.UsersExp ue = new BLL.Members.UsersExp();
        //            ue.UserID = userid;
        //            ue.BirthdayVisible = 0;
        //            ue.BirthdayIndexVisible = false;
        //            ue.Gravatar = string.Format("/{0}/User/Gravatar/{1}", MvcApplication.UploadFolder, userid);
        //            ue.ConstellationVisible = 0;
        //            ue.ConstellationIndexVisible = false;
        //            ue.NativePlaceVisible = 0;
        //            ue.NativePlaceIndexVisible = false;
        //            ue.RegionId = 0;
        //            ue.AddressVisible = 0;
        //            ue.AddressIndexVisible = false;
        //            ue.BodilyFormVisible = 0;
        //            ue.BodilyFormIndexVisible = false;
        //            ue.BloodTypeVisible = 0;
        //            ue.BloodTypeIndexVisible = false;
        //            ue.MarriagedVisible = 0;
        //            ue.MarriagedIndexVisible = false;
        //            ue.PersonalStatusVisible = 0;
        //            ue.PersonalStatusIndexVisible = false;
        //            ue.LastAccessIP = "";
        //            ue.LastAccessTime = DateTime.Now;
        //            ue.LastLoginTime = DateTime.Now;
        //            ue.LastPostTime = DateTime.Now;
        //            ue.NickName = model.NickName;
        //            ue.Activity = true;//不需要后台激活
        //            if (!ue.AddExp(ue, uid))
        //            {
        //                userManage.Delete(userid);
        //                userExpManage.DeleteUsersExp(userid);
        //                ModelState.AddModelError("Message", "注册失败！");
        //                return View(model);
        //            }
        //            //清除Session 
        //            Session["SMSCode"] = null;
        //            Session["SMS_DATE"] = DateTime.MinValue;

        //            #region
        //            string DefaultGravatar = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
        //            DefaultGravatar = string.IsNullOrEmpty(DefaultGravatar) ? "/Upload/User/Gravatar/Default.jpg" : DefaultGravatar;
        //            string TargetGravatarFile = BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
        //            TargetGravatarFile = string.IsNullOrEmpty(TargetGravatarFile) ? "/Upload/User/Gravatar/" : TargetGravatarFile;
        //            string path = ControllerContext.HttpContext.Server.MapPath("/");
        //            if (System.IO.File.Exists(path + DefaultGravatar))
        //            {
        //                System.IO.File.Copy(path + DefaultGravatar, path + TargetGravatarFile + userid + ".jpg", true);
        //            }
        //            if (regStr == "Phone" || IsCloseRegisterSendEmail)  //手机号码注册   或者  关闭邮箱验证
        //            {
        //                FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
        //                //注册加积分
        //                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
        //                pointBll.AddPoints(2, userid, "注册成功");
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    SendEmail(model.UserName, model.UserName, 0);
        //                    //return RedirectToAction("RegisterSuccess", "Account", new { email = model.UserName });// return Redirect("/Account/RegisterSuccess");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Model.SysManage.ErrorLog Errormodel = new Model.SysManage.ErrorLog();
        //                    Errormodel.Loginfo = ex.Message;
        //                    Errormodel.StackTrace = ex.StackTrace;
        //                    Errormodel.Url = Request.Url.AbsoluteUri;
        //                    BLL.SysManage.ErrorLog.Add(Errormodel);
        //                    ModelState.AddModelError("", "邮件发送过程中出现网络异常，请稍后再试！");
        //                }
        //            }
        //            #endregion



        //            //  Response.Write("<script>alert('注册成功');window.location.href='"+ViewBag.BasePath+"UserCenter/Personal"+"';</script>");
        //            return Redirect(ViewBag.BasePath + "UserCenter/Personal");
        //        }
        //    //}

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult MRegister(RegisterModel model)
        //{
        //    return View(model);
        //}

        //public ActionResult MRegister()
        //{
        //    return View();
        //}

        //#endregion

        //#region 注册邮件验证
        ////注册邮件验证页面
        //public ActionResult ValidateEmail()
        //{
        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "填写密码" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion

        //    string SecretKey = Request.QueryString["SecretKey"];
        //    Maticsoft.BLL.SysManage.VerifyMail bll = new Maticsoft.BLL.SysManage.VerifyMail();
        //    Maticsoft.Model.SysManage.VerifyMail model = bll.GetModel(SecretKey);
        //    User user = new User(model.UserName);
        //    ViewBag.Email = user.Email;
        //    ViewBag.SecretKey = SecretKey;
        //    return View();
        //}

        //[HttpPost]
        //public void FillPassword(string SecretKey, string password, string Email)
        //{
        //    string result = string.Empty;
        //    Maticsoft.BLL.SysManage.VerifyMail bll = new Maticsoft.BLL.SysManage.VerifyMail();
        //    Maticsoft.Model.SysManage.VerifyMail model = bll.GetModel(SecretKey);
        //    if (!string.IsNullOrEmpty(SecretKey) && bll.Exists(SecretKey) && model != null &&
        //        model.ValidityType.HasValue && model.ValidityType.Value == 0)
        //    {
        //        switch (model.Status)
        //        {
        //            case 0:
        //                TimeSpan ts = DateTime.Now - model.CreatedDate;
        //                if (ts.TotalHours > 24)
        //                {
        //                    model.Status = 2; // 0:邮箱验证未通过1：邮箱验证通过2：已过期
        //                    bll.Update(model);
        //                    result = "{success:false,msg:'注册验证已过期！'}";
        //                    // ModelState.AddModelError("Error", "注册验证已过期！");
        //                }
        //                User user = new User(model.UserName);
        //                if (user != null)
        //                {
        //                    //更新用户状态
        //                    user.UpdateActivity(user.UserID, true);
        //                    user.SetPassword(Email, password);
        //                    ViewBag.Email = user.Email;
        //                }

        //                model.Status = 1; // 0:邮箱验证未通过1：邮箱验证通过2：已过期
        //                bll.Update(model);
        //                ViewBag.Msg = "Success";
        //                ViewBag.email = model.UserName;
        //                result = "{success:true,msg:''}";
        //                break;
        //            case 1:
        //                model.Status = 2;
        //                bll.Update(model);
        //                result = "{success:false,msg:'注册验证已通过！'}";
        //                //  ModelState.AddModelError("Error", "注册验证已通过！");
        //                break;
        //            case 2:
        //                result = "{success:false,msg:'注册验证已过期！'}";
        //                // ModelState.AddModelError("Error", "注册验证已过期！");
        //                break;
        //            default:
        //                result = "{success:false,msg:'无效的邮箱验证码！'}";
        //                //ModelState.AddModelError("Error", "无效的邮箱验证码！");
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        result = "{success:false,msg:'无效的邮箱验证码！'}";
        //    }
        //    Response.Write(result);
        //}

        //public ActionResult RegisterSuccess(string email)
        //{
        //    ViewBag.Email = email;
        //    ViewBag.EmailUrl = EmailUrl(email);
        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "注册成功" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion
        //    return View();
        //}

        //#endregion

        //#region Status Codes
        //private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        //{
        //    // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
        //    // 状态代码的完整列表。
        //    switch (createStatus)
        //    {
        //        case MembershipCreateStatus.DuplicateUserName:
        //            return "用户名已存在。请输入不同的用户名。";

        //        case MembershipCreateStatus.DuplicateEmail:
        //            return "该电子邮件地址的用户名已存在。请输入不同的电子邮件地址。";

        //        case MembershipCreateStatus.InvalidPassword:
        //            return "提供的密码无效。请输入有效的密码值。";

        //        case MembershipCreateStatus.InvalidEmail:
        //            return "提供的电子邮件地址无效。请检查该值并重试。";

        //        case MembershipCreateStatus.InvalidAnswer:
        //            return "提供的密码取回答案无效。请检查该值并重试。";

        //        case MembershipCreateStatus.InvalidQuestion:
        //            return "提供的密码取回问题无效。请检查该值并重试。";

        //        case MembershipCreateStatus.InvalidUserName:
        //            return "提供的用户名无效。请检查该值并重试。";

        //        case MembershipCreateStatus.ProviderError:
        //            return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

        //        case MembershipCreateStatus.UserRejected:
        //            return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

        //        default:
        //            return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
        //    }
        //}
        //#endregion


        //#region 忽略SSL签名验证
        //internal class AcceptAllCertificatePolicy : System.Net.ICertificatePolicy
        //{
        //    public AcceptAllCertificatePolicy()
        //    {
        //    }

        //    public bool CheckValidationResult(System.Net.ServicePoint sPoint,
        //       System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Net.WebRequest wRequest, int certProb)
        //    {
        //        // Always accept
        //        return true;
        //    }
        //}
        //#endregion

        //#region 找回密码
        //public ActionResult FindPwd()
        //{
        //    if (CurrentUser != null && CurrentUser.UserType != "AA") return RedirectToAction("Index", "UserCenter");

        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "找回密码" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult FindPwd(FormCollection collection)
        //{
        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "找回密码" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion

        //    string email = collection["Email"].Trim();
        //    ViewData["Email"] = email;
        //    if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
        //    {
        //        if (Session["CheckCode"].ToString().ToLower() != collection["CheckCode"].Trim().ToLower())
        //        {
        //            ModelState.AddModelError("Error", "验证码错误！");
        //            Session["CheckCode"] = null;
        //            return View(ViewData["Email"]);
        //        }
        //        else
        //        {
        //            Session["CheckCode"] = null;
        //        }
        //    }
        //    else
        //    {
        //        return View(ViewData["Email"]);
        //    }
        //    Maticsoft.Accounts.Bus.User userinfo = new User(email);
        //    if (String.IsNullOrWhiteSpace(userinfo.NickName))
        //    {
        //        ModelState.AddModelError("Error", "该邮箱用户不存在！");
        //        return View(ViewData["Email"]);
        //    }
        //    //if (!(bool)userinfo.Activity)
        //    //{
        //    //    ModelState.AddModelError("Error", "您的帐号尚未通过邮箱验证,请重新发送确认邮件或者登录邮箱查看邮件！");
        //    //    return RedirectToAction("RegisterEmail", "Account", new { id = userinfo.UserID });
        //    //}
        //    try
        //    {
        //        SendEmail(userinfo.UserName, email, 1);
        //        return RedirectToAction("FindPwdEmail", "Account", new { email = ViewData["Email"] });
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("Error", "邮件发送过程中出现网络异常，请稍后再试！");
        //    }
        //    finally
        //    {

        //    }
        //    return View(ViewData["Email"]);
        //}

        ///// <summary>
        ///// 手机找回密码
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult FindPwdByPhone()
        //{
        //    if (CurrentUser != null && CurrentUser.UserType != "AA") return RedirectToAction("Index", "UserCenter");

        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "找回密码" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion
        //    return View();
        //}

        ///// <summary>
        ///// 手机找回密码
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult FindPwdByPhone(FormCollection collection)
        //{
        //    try
        //    {
        //        Session["Phone"] = collection["UserName"].ToString();
        //        if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
        //        {
        //            if (Session["CheckCode"].ToString().ToLower() == collection["CheckCode"].Trim().ToLower())
        //            {
        //                Session["CheckCode"] = null;
        //                return RedirectToAction("ReSetPwd", "Account");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("Error", "验证时时出现网络异常，请稍后再试！");
        //    }
        //    ModelState.AddModelError("Error", "验证码错误！");
        //    Session["CheckCode"] = null;
        //    ViewBag.CheckResult = "验证码错误";
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult ReSetPwd()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ReSetPwd(FormCollection collection)
        //{
        //    try
        //    {
        //        if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
        //        {
        //            if (Session["CheckCode"].ToString().ToLower() == collection["CheckCode"].Trim().ToLower())
        //            {
        //                Session["CheckCode"] = null;
        //                if (Session["Phone"] != null && Session["Phone"].ToString() != "")
        //                {
        //                    BLL.Shop.Account.User UserManage = new BLL.Shop.Account.User();
        //                    string randCode = string.Empty;
        //                    string phone = Session["Phone"].ToString();
        //                    Session["Phone"] = null;
        //                    string pwd = collection["Password"].ToString();
        //                    User User = new User();
        //                    string UserName = UserManage.GetPhoneUser(phone).UserName;

        //                    if (UserName != null)
        //                    {
        //                        User.SetPassword(UserName, pwd);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.CheckResult = "验证码不正确";
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.CheckResult = "验证码不正确";
        //            return View();
        //        }

        //    }
        //    catch (Exception) { ModelState.AddModelError("Error", "重置密码时时出现网络异常，请稍后再试！"); }
        //    return RedirectToAction("Login", "Account");
        //}

        ///// 找回密码邮箱验证页面
        ///// </summary>
        ///// <param name="email"></param>
        ///// <returns></returns>
        //public ActionResult FindPwdEmail(string email)
        //{
        //    ViewBag.Email = email;
        //    ViewBag.EmailUrl = EmailUrl(email);
        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "找回密码邮箱验证" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion
        //    return View();
        //}

        ///// <summary>
        ///// 激活密码找回
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult VerifyPassword()
        //{
        //    #region SEO 优化设置
        //    IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
        //    ViewBag.Title = "找回密码" + pageSetting.Title;
        //    ViewBag.Keywords = pageSetting.Keywords;
        //    ViewBag.Description = pageSetting.Description;
        //    #endregion

        //    string SecretKey = ViewBag.SecretKey = Request.QueryString["SecretKey"];

        //    if (!string.IsNullOrEmpty(SecretKey))
        //    {
        //        Maticsoft.BLL.SysManage.VerifyMail bll = new Maticsoft.BLL.SysManage.VerifyMail();
        //        if (bll.Exists(SecretKey))
        //        {
        //            Maticsoft.Model.SysManage.VerifyMail model = bll.GetModel(SecretKey);
        //            if (model != null && model.ValidityType.HasValue)
        //            {
        //                if (model.ValidityType.Value == 1)
        //                {
        //                    //0:邮箱验证未通过1：邮箱验证通过2：已过期
        //                    if (model.Status == 0)
        //                    {
        //                        TimeSpan ts = DateTime.Now - model.CreatedDate;
        //                        if (ts.TotalHours > 24)
        //                        {
        //                            model.Status = 2;// 0:邮箱验证未通过1：邮箱验证通过2：已过期
        //                            bll.Update(model);
        //                            ViewBag.Msg = "找回密码的验证码已过期！";
        //                            ModelState.AddModelError("Error", "找回密码的验证码已过期！");

        //                        }

        //                        User user = new User(model.UserName);
        //                        if (user != null)
        //                        {
        //                            ViewBag.Email = user.Email;
        //                        }
        //                        model.Status = 1;// 0:邮箱验证未通过1：邮箱验证通过2：已过期
        //                        bll.Update(model);
        //                        ViewBag.Msg = "Success";
        //                    }
        //                    else if (model.Status == 1)
        //                    {
        //                        model.Status = 2;
        //                        bll.Update(model);
        //                        ViewBag.Msg = "找回密码的验证码已通过邮箱验证！";
        //                        ModelState.AddModelError("Error", "找回密码的验证码已通过邮箱验证！");

        //                    }
        //                    else if (model.Status == 2)
        //                    {
        //                        ViewBag.Msg = "找回密码的验证码已过期！";
        //                        ModelState.AddModelError("Error", "找回密码的验证码已过期！");

        //                    }
        //                    else
        //                    {
        //                        ViewBag.Msg = "无效的邮箱验证码！";
        //                        ModelState.AddModelError("Error", "无效的邮箱验证码！");
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    return View();
        //}

        ////邮箱是否存在
        //[HttpPost]
        //public void HasEmail(FormCollection collection)
        //{
        //    Maticsoft.Accounts.Bus.User user = new User();
        //    if (!String.IsNullOrWhiteSpace(collection["Email"]))
        //    {
        //        Response.ContentType = "application/text";
        //        if (user.HasUserByEmail(collection["Email"].Trim()))
        //        {
        //            Response.Write("true");
        //        }
        //        else
        //        {
        //            Response.Write("false");
        //        }
        //    }
        //}


        //[HttpPost]
        //public ActionResult VerifyPassword(FormCollection collection)
        //{
        //    if (!String.IsNullOrWhiteSpace(collection["Email"]) && !String.IsNullOrWhiteSpace(collection["NewPwd"]))
        //    {
        //        string secretKey = collection["SecretKey"];
        //        string username = collection["Email"].Trim();
        //        string password = collection["NewPwd"];

        //        Maticsoft.BLL.SysManage.VerifyMail bll = new Maticsoft.BLL.SysManage.VerifyMail();

        //        Maticsoft.Model.SysManage.VerifyMail model = bll.GetModel(secretKey);
        //        if (model == null || !model.ValidityType.HasValue || model.ValidityType.Value != 1 ||
        //            model.UserName != username)
        //        {
        //            //非法修改密码
        //            LogHelp.AddInvadeLog("Areas.Shop.Controllers-HttpPost-VerifyPassword", System.Web.HttpContext.Current.Request);
        //            return HttpNotFound();
        //        }

        //        User currentUser = new User(username);
        //        if (String.IsNullOrWhiteSpace(password))
        //        {
        //            ModelState.AddModelError("Error", "该用户不存在！");
        //            return View();
        //        }
        //        currentUser.Password = AccountsPrincipal.EncryptPassword(Maticsoft.Common.PageValidate.InputText(password, 30));
        //        if (!currentUser.Update())
        //        {
        //            ModelState.AddModelError("Error", "密码重置失败，请检查输入的信息是否正确或者联系管理员！");
        //            return View();
        //        }
        //        else
        //        {
        //            AccountsPrincipal newUser = AccountsPrincipal.ValidateLogin(username, password);
        //            FormsAuthentication.SetAuthCookie(username, false);
        //            Session[Globals.SESSIONKEY_USER] = currentUser;
        //            Session["Style"] = currentUser.Style;
        //            Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
        //            pointBll.AddPoints(1, currentUser.UserID, "登录操作");
        //            if (Session["returnPage"] != null)
        //            {
        //                string returnpage = Session["returnPage"].ToString();
        //                Session["returnPage"] = null;
        //                return Redirect(returnpage);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "UserCenter");
        //            }
        //        }
        //    }
        //    return View();
        //}
        ///// <summary>
        ///// 重新发送邮件
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public void SendEmail(FormCollection collection)
        //{
        //    if (!String.IsNullOrWhiteSpace(collection["Email"]))
        //    {
        //        User user = new User(collection["Email"]);
        //        int type = -1;//int.Parse(collection["Type"].ToString());
        //        //string emailtype = "RegisterEmail";
        //        if (!String.IsNullOrWhiteSpace(collection["Type"]) && Common.PageValidate.IsNumber(collection["Type"]))
        //        {
        //            type = Common.Globals.SafeInt(collection["Type"], -1);
        //            // emailtype = type == 0 ? "RegisterEmail" : "FindPwdEmail";
        //        }
        //        if (!String.IsNullOrWhiteSpace(user.NickName))
        //        {
        //            SendEmail(user.UserName, user.Email, type);
        //            Response.ContentType = "application/text";
        //            Response.Write("success");
        //        }
        //    }
        //}

        //#endregion

        //#region 发送邮件
        ///// <summary>
        ///// 根据用户id得到用户的邮箱
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <returns></returns>
        //protected string EmailSuffix(string sUserID)
        //{
        //    int userID = Globals.SafeInt(Globals.UrlDecode(sUserID), 0);
        //    BLL.Members.Users bll = new BLL.Members.Users();
        //    Model.Members.Users model = bll.GetModel(userID);
        //    if (model != null)
        //    {
        //        return model.Email;
        //    }
        //    else
        //        return "";
        //}
        //public string EmailUrl(string email)
        //{
        //    string emailUrl = "";
        //    string emailStr = email.Substring(email.LastIndexOf('@') + 1);
        //    //谷歌邮箱特殊处理
        //    if (emailStr.Contains("gmail"))
        //    {
        //        emailStr = "google.com";
        //    }
        //    emailUrl = "http://mail." + emailStr;
        //    return emailUrl;
        //}
        ///// <summary>
        /////         发送邮件 type 0:表示注册激活邮件，1：表示找回密码邮件
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="email"></param>
        ///// <param name="type"></param>
        //protected bool SendEmail(string username, string email, int type)
        //{
        //    Maticsoft.BLL.Ms.EmailTemplet emailBll = new EmailTemplet();
        //    switch (type)
        //    {
        //        case 0:
        //            return emailBll.SendRegisterEmail(username, email);
        //        case 1:
        //            return emailBll.SendFindPwdEmail(username, email);
        //    }
        //    return false;
        //}
        //#endregion

        //#region 微博帐号绑定
        //public ActionResult ToBind()
        //{
        //    string pName = Request["pName"];
        //    if (!String.IsNullOrWhiteSpace(pName))
        //    {
        //        String url = ViewBag.BasePath + "social/qq";
        //        switch (pName)
        //        {
        //            case "QZone":
        //                url = ViewBag.BasePath + "social/qq";
        //                break;
        //            case "Sina":
        //                url = ViewBag.BasePath + "social/sina";
        //                break;
        //            default:
        //                url = ViewBag.BasePath + "social/sina";
        //                break;
        //        }
        //        System.Web.HttpContext.Current.Response.Redirect(url);
        //    }
        //    return RedirectToAction("UserBind", "UserCenter");
        //}
        //#endregion



        ///// <summary>
        ///// Ajax 判断是否登录
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult AjaxIsLogin()
        //{
        //    if (currentUser != null)
        //    {
        //        if (currentUser.UserType == "AA")
        //        {
        //            return Content("AA");
        //        }
        //        return Content("True");
        //    }
        //    return Content("False");
        //}

    }
}
