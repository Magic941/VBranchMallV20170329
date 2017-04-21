using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class AccountController : MPageControllerBase
    {
        //
        // GET: /Mobile/Account/
        private Maticsoft.Accounts.Bus.User userBusManage = new Maticsoft.Accounts.Bus.User();
        private BLL.Members.Users userManage = new BLL.Members.Users();
        private BLL.Members.UsersExp userExpManage = new BLL.Members.UsersExp();
        public ActionResult Index()
        {
            return View();
        }
        #region 登录
        public ActionResult Login(string returnUrl)
        {
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            ViewBag.SMSIsOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            if (IsCloseLogin)
            {
                return Redirect(ViewBag.BasePath+"Error/TurnOff");
                // return RedirectToAction("TurnOff", "Error", new { id = 1, viewname = "url" });
            }

            //string returnUrl = Request.QueryString["returnUrl"];
            if (!string.IsNullOrWhiteSpace(returnUrl))
                ViewBag.returnUrl = returnUrl;
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
                return Redirect(ViewBag.BasePath+"u");
            //return RedirectToAction("Index", "UserCenter", new { id = 1, viewname = "url" });

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "登录" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            ViewBag.SMSIsOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            if (IsCloseLogin)
            {
                return Redirect(ViewBag.BasePath+"Error/TurnOff");
                //return RedirectToAction("TurnOff", "Error", new { id = 1, viewname = "url" });
            }

            if (ModelState.IsValid)
            {
                #region SEO 优化设置
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title = "登录" + pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion
                AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(model.UserName, model.Password);
                if (userPrincipal == null)
                {
                    ModelState.AddModelError("Message", "用户名或密码不正确, 请重新输入!");
                    return View(model);
                }
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    ModelState.AddModelError("Message", "对不起，该帐号已被冻结或未激活，请联系管理员！");
                    return View(model);
                }
                HttpContext.User = userPrincipal;
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                //登录成功加积分
                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");

                //加载Shop模块的购物车
                if (CurrentThemeName == "M1")
                {
                    BLL.Shop.Products.ShoppingCartHelper.LoadShoppingCart(currentUser.UserID);
                }

                returnUrl = Server.UrlDecode(returnUrl);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    //TempData["pointer"] = pointers;
                    return Redirect(ViewBag.BasePath+"u");
                    //return RedirectToAction("Index", "UserCenter", new { id=1,viewname = "url"});  

                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AjaxLogin(string UserName, string UserPwd)
        {
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            ViewBag.SMSIsOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            if (IsCloseLogin)
            {
                return Content("-1");
            }
            if (ModelState.IsValid)
            {
                AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(UserName, UserPwd);
                if (userPrincipal != null)
                {
                    User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                    if (!currentUser.Activity)
                    {
                        ModelState.AddModelError("Message", "对不起，该帐号已被冻结，请联系管理员！");
                    }
                    //if (currentUser.UserType == "AA")
                    //{
                    //    ModelState.AddModelError("Message", "您是管理员用户，您没有权限登录后台系统！") ;                        
                    //}
                    HttpContext.User = userPrincipal;
                    FormsAuthentication.SetAuthCookie(UserName, true);
                    Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                    //登录成功加积分
                    Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                    int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");
                    return Content("1|" + pointers.ToString());
                }
                else
                {
                    return Content("0");
                }
            }
            return Content("0");
        }
        #region 退出
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(Globals.SESSIONKEY_USER);
            //Session.Clear();
            //Session.Abandon();
            return Redirect(ViewBag.BasePath);
            // return RedirectToAction("Index", "Home", new { id=1,viewname = "url"});
        }
        #endregion
        #endregion

        #region 注册
        public ActionResult Register()
        {
            bool IsCloseRegister = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");

            ViewBag.SMSIsOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            if (IsCloseRegister)
            {
                return Redirect(ViewBag.BasePath+"Error/TurnOff");
                //return RedirectToAction("TurnOff", "Error", new { id=1,viewname = "url"});
            }
            if (CurrentUser != null && CurrentUser.UserType != "AA")
                return Redirect(ViewBag.BasePath+"u");
            //return RedirectToAction("Index", "UserCenter", new { id = 1, viewname = "url" });

            RegisterModel regModel = new RegisterModel();

            ViewBag.Seconds = 0;
            if (Session["SMS_DATE"] != null && !String.IsNullOrWhiteSpace(Session["SMS_DATE"].ToString()))
            {
                DateTime smsDate = Globals.SafeDateTime(Session["SMS_DATE"].ToString(), DateTime.MinValue);
                if (smsDate != DateTime.MinValue)
                {
                    TimeSpan smsSeconds = smsDate.AddSeconds(60) - DateTime.Now;
                    ViewBag.Seconds = (int)smsSeconds.TotalSeconds;
                }
            }

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "注册" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(regModel);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "注册" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            bool IsOpen= BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            ViewBag.SMSIsOpen = IsOpen;//是否开启手机验证
            bool IsCloseRegister = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
            if (IsCloseRegister)
            {
                return Redirect(ViewBag.BasePath+"Error/TurnOff");
                //return RedirectToAction("TurnOff", "Error", new { id = 1, viewname = "url" });
            }
    
                //判断昵称是否已存在
                if (userBusManage.HasUserByNickName(model.NickName))
                {
                    ViewBag.hasnickname = "昵称已被抢先使用，换一个试试";
                    return View(model);
                }
                if (!IsOpen)
                {
                    if (userBusManage.HasUserByEmail(model.UserName))
                    {
                        ViewBag.hasemail = "该邮箱已被注册";
                        return View(model);
                    }
                }
              

                // bool IsCloseRegisterSendEmail = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_RegisterEmailCheck");
                User newUser = new User();
                //DONE: 警告DB字段未对应: Email 字段 varchar(100) UserName 字段 varchar(50) 已完成 BEN DONE 2012-11-22
                newUser.UserName = IsOpen ? model.Phone : model.UserName;
                newUser.NickName = model.NickName;  //昵称名称相同
                newUser.Password = AccountsPrincipal.EncryptPassword(model.Password);
                newUser.Email =IsOpen?"": model.UserName;
                //if (IsCloseRegisterSendEmail) //关闭
                newUser.Activity = true;
                //else //开启
                //    newUser.Activity = false;
                newUser.UserType = "UU";
                newUser.Style = 1;
                newUser.User_dateCreate = DateTime.Now;
                newUser.User_cLang = "zh-CN";
                newUser.Phone = model.Phone;
                int userid = newUser.Create();
                if (userid == -100)
                {
                    ModelState.AddModelError("Message", ErrorCodeToString(MembershipCreateStatus.DuplicateUserName));
                }
                else
                {
                    //添加用户扩展表数据
                    BLL.Members.UsersExp ue = new BLL.Members.UsersExp();
                    ue.UserID = userid;
                    ue.BirthdayVisible = 0;
                    ue.BirthdayIndexVisible = false;
                    ue.Gravatar = string.Format("/{0}/User/Gravatar/{1}", MvcApplication.UploadFolder, userid);
                    ue.ConstellationVisible = 0;
                    ue.ConstellationIndexVisible = false;
                    ue.NativePlaceVisible = 0;
                    ue.NativePlaceIndexVisible = false;
                    ue.RegionId = 0;
                    ue.AddressVisible = 0;
                    ue.AddressIndexVisible = false;
                    ue.BodilyFormVisible = 0;
                    ue.BodilyFormIndexVisible = false;
                    ue.BloodTypeVisible = 0;
                    ue.BloodTypeIndexVisible = false;
                    ue.MarriagedVisible = 0;
                    ue.MarriagedIndexVisible = false;
                    ue.PersonalStatusVisible = 0;
                    ue.PersonalStatusIndexVisible = false;
                    ue.LastAccessIP = "";
                    ue.LastAccessTime = DateTime.Now;
                    ue.LastLoginTime = DateTime.Now;
                    ue.LastPostTime = DateTime.Now;
                    if (!ue.AddUsersExp(ue))
                    {
                        userManage.Delete(userid);
                        userExpManage.DeleteUsersExp(userid);
                        ModelState.AddModelError("Message", "注册失败！");
                        return View(model);
                    }
                    //清除Session 
                    Session["SMSCode"] = null;
                    Session["SMS_DATE"] = DateTime.MinValue;
                    #region 默认数据
                    string DefaultGravatar = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
                    DefaultGravatar = string.IsNullOrEmpty(DefaultGravatar) ? "/Upload/User/Gravatar/Default.jpg" : DefaultGravatar;
                    string TargetGravatarFile = BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
                    TargetGravatarFile = string.IsNullOrEmpty(TargetGravatarFile) ? "/Upload/User/Gravatar/" : TargetGravatarFile;
                    string path = ControllerContext.HttpContext.Server.MapPath("/");
                    if (System.IO.File.Exists(path + DefaultGravatar))
                    {
                        System.IO.File.Copy(path + DefaultGravatar, path + TargetGravatarFile + userid + ".jpg", true);
                    }
                    //if (!IsCloseRegisterSendEmail) //开启了发送邮件的功能
                    //{
                    //SendEmail(model.Email, model.Email, 0);
                    //  return RedirectToAction("RegisterSuccess", "Account", new { email = model.Email });// return Redirect("/Account/RegisterSuccess");
                    //}
                    //else
                    //{
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    //注册加积分
                    Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                    int pointers = pointBll.AddPoints(2, userid, "注册成功");
                    // }
                    #endregion
                    return Redirect(ViewBag.BasePath+"u/Personal");
                    //return RedirectToAction("Personal", "UserCenter", new { id = 1, viewname = "url" });

                }
            return View(model);
        }
        #endregion

        #region 用户协议
        public ActionResult UserAgreement()
        {

            BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);
            RegisterModel regModel = new RegisterModel();
            regModel.UserAgreement = WebSiteSet.RegistStatement;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "注册协议" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(regModel);
        }
        #endregion

        #region Ajax验证

        /// <summary>
        /// 验证用户名是否已存在
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsExistUserName(string userName)
        {
            bool valid = !(userBusManage.HasUserByUserName(userName));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证昵称是否已存在
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsExistNickName(string nickName)
        {
            bool valid = !(userBusManage.HasUserByNickName(nickName));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证Phone是否已存在
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsExistPhone(string phone)
        {
            bool valid = !(userBusManage.HasUserByPhone(phone));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证Email是否已存在
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsExistEmail(string email)
        {
            bool valid = !(userBusManage.HasUserByEmail(email));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ajax发送短信
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult SendSMS(FormCollection Fm)
        {
            string Phone = Fm["Phone"];
            if (String.IsNullOrWhiteSpace(Phone))
            {
                return Content("False");
            }
            Random rnd = new Random();
            int rand = rnd.Next(100000, 999999);
            string content = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Content");
            content = content.Replace("{SMSCode}", rand.ToString());
            Session["SMSCode"] = rand;
            Session["SMS_DATE"] = DateTime.Now;
            string[] numbers = new string[] { Phone };
            bool isSuccess = Maticsoft.Web.Components.SMSHelper.SendSMS(content, numbers);
            return isSuccess ? Content("True") : Content("False");
        }

        /// <summary>
        /// 验证效验码
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult VerifiyCode(FormCollection Fm)
        {
            if (Session["SMSCode"] == null || String.IsNullOrWhiteSpace(Session["SMSCode"].ToString()))
            {
                return Content("False");
            }

            string code = Fm["SMSCode"];
            return code == Session["SMSCode"].ToString() ? Content("True") : Content("False");
        }


        #endregion

        #region 微信用户绑定

        public ActionResult UserBind(string viewName = "UserBind")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Mobile);
            ViewBag.Title = "微信用户绑定" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            ViewBag.User = Request.Params["user"];
            ViewBag.OpenId = Request.Params["open"];
            return View(viewName);
        }
        [HttpPost]
        public ActionResult AjaxBind(string UserName, string UserPwd, string User, string OpenId)
        {
            if (String.IsNullOrWhiteSpace(OpenId) || String.IsNullOrWhiteSpace(User))
            {
                return Content("-1");
            }
            Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
            Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, User);
            if (wUserModel == null)
            {
                return Content("-1");
            }
            if (!String.IsNullOrWhiteSpace(wUserModel.NickName))
            {
                return Content("3");
            }
            AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(UserName, UserPwd);
            if (userPrincipal != null)
            {
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    return Content("2");
                }
                HttpContext.User = userPrincipal;
                FormsAuthentication.SetAuthCookie(UserName, true);
                //绑定当前系统用户
                wUserModel.UserId = currentUser.UserID;
                wUserModel.NickName = currentUser.NickName;
                if (!wUserBll.Update(wUserModel))
                {
                    return Content("-1");
                }
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        public ActionResult RegBind(string viewName = "RegBind")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Mobile);
            ViewBag.Title = "新用户绑定" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            ViewBag.User = Request.Params["user"];
            ViewBag.OpenId = Request.Params["open"];
            return View(viewName);
        }

        [HttpPost]
        public ActionResult AjaxRegBind(string UserName, string UserPwd, string NickName, string User, string OpenId)
        {
            if (String.IsNullOrWhiteSpace(OpenId) || String.IsNullOrWhiteSpace(User))
            {
                return Content("0");
            }
            Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
            Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, User);
            if (wUserModel == null)
            {
                return Content("0");
            }
            if (!String.IsNullOrWhiteSpace(wUserModel.NickName))
            {
                return Content("3");
            }

            User newUser = new User();
            //DONE: 警告DB字段未对应: Email 字段 varchar(100) UserName 字段 varchar(50) 已完成 BEN DONE 2012-11-22
            newUser.UserName = UserName;
            newUser.NickName = NickName; //昵称名称相同
            newUser.Password = AccountsPrincipal.EncryptPassword(UserPwd);
            newUser.Email = UserName;
            newUser.Activity = true;
            newUser.UserType = "UU";
            newUser.Style = 1;
            newUser.User_dateCreate = DateTime.Now;
            newUser.User_cLang = "zh-CN";
            int userid = newUser.Create();
            if (userid == -100)
            {
                return Content("0");
            }

            //添加用户扩展表数据
            BLL.Members.UsersExp ue = new BLL.Members.UsersExp();
            ue.UserID = userid;
            ue.BirthdayVisible = 0;
            ue.BirthdayIndexVisible = false;
            ue.Gravatar = string.Format("/{0}/User/Gravatar/{1}", MvcApplication.UploadFolder, userid);
            ue.ConstellationVisible = 0;
            ue.ConstellationIndexVisible = false;
            ue.NativePlaceVisible = 0;
            ue.NativePlaceIndexVisible = false;
            ue.RegionId = 0;
            ue.AddressVisible = 0;
            ue.AddressIndexVisible = false;
            ue.BodilyFormVisible = 0;
            ue.BodilyFormIndexVisible = false;
            ue.BloodTypeVisible = 0;
            ue.BloodTypeIndexVisible = false;
            ue.MarriagedVisible = 0;
            ue.MarriagedIndexVisible = false;
            ue.PersonalStatusVisible = 0;
            ue.PersonalStatusIndexVisible = false;
            ue.LastAccessIP = "";
            ue.LastAccessTime = DateTime.Now;
            ue.LastLoginTime = DateTime.Now;
            ue.LastPostTime = DateTime.Now;
            if (!ue.AddUsersExp(ue))
            {
                userManage.Delete(userid);
                userExpManage.DeleteUsersExp(userid);
                return Content("0");
            }

            #region 默认数据

            string DefaultGravatar = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
            DefaultGravatar = string.IsNullOrEmpty(DefaultGravatar)
                                  ? "/Upload/User/Gravatar/Default.jpg"
                                  : DefaultGravatar;
            string TargetGravatarFile = BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
            TargetGravatarFile = string.IsNullOrEmpty(TargetGravatarFile)
                                     ? "/Upload/User/Gravatar/"
                                     : TargetGravatarFile;
            string path = ControllerContext.HttpContext.Server.MapPath("/");
            if (System.IO.File.Exists(path + DefaultGravatar))
            {
                System.IO.File.Copy(path + DefaultGravatar, path + TargetGravatarFile + userid + ".jpg", true);
            }

            #endregion

            //绑定当前系统用户
            wUserModel.UserId = userid;
            wUserModel.NickName = NickName;
            if (!wUserBll.Update(wUserModel))
            {
                return Content("0");
            }
            return Content("1");

        }
        #endregion

        //邮箱是否存在
        [HttpPost]
        public void HasEmail(FormCollection collection)
        {
            Maticsoft.Accounts.Bus.User user = new User();
            if (!String.IsNullOrWhiteSpace(collection["Email"]))
            {
                Response.ContentType = "application/text";
                if (user.HasUserByEmail(collection["Email"].Trim()))
                {
                    Response.Write("true");
                }
                else
                {
                    Response.Write("false");
                }
            }
        }


        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
            // 状态代码的完整列表。
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在。请输入不同的用户名。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "该电子邮件地址的用户名已存在。请输入不同的电子邮件地址。";

                case MembershipCreateStatus.InvalidPassword:
                    return "提供的密码无效。请输入有效的密码值。";

                case MembershipCreateStatus.InvalidEmail:
                    return "提供的电子邮件地址无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "提供的密码取回答案无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名无效。请检查该值并重试。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                default:
                    return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
        }
        #endregion


        /// <summary>
        /// Ajax 判断是否登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxIsLogin()
        {
            if (currentUser != null)
            {
                if (currentUser.UserType == "AA")
                {
                    return Content("AA");
                }
                return Content("True");
            }
            return Content("False");
        }


    }
}
