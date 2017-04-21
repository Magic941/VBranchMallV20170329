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
using Webdiyer.WebControls.Mvc;
using System.Text.RegularExpressions;
using Maticsoft.Model;
using System.Data;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.Services;
using Maticsoft.BLL.Shop.Card;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.Accounts;


namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class AccountController : MShopControllerBase
    {
        //
        // GET: /Mobile/Account/
        private static readonly string card_Path = "HaolinShop.TestCardQueue";
        private static string path = ConfigHelper.GetConfigString("CouponQueue");

        private Maticsoft.Accounts.Bus.User userBusManage = new Maticsoft.Accounts.Bus.User();
        private BLL.Members.Users userManage = new BLL.Members.Users();
        private BLL.Members.UsersExp userExpManage = new BLL.Members.UsersExp();
        private readonly BLL.Shop_CardUserInfo carduserInfoBll = new BLL.Shop_CardUserInfo();
        public ActionResult Index()
        {
            return View();
        }
        #region 登录
        public ActionResult Login(string returnUrl)
        {
            ViewBag.RegisterToggle = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式     
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            ViewBag.SMSIsOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            if (IsCloseLogin)
            {
                return Redirect(ViewBag.BasePath + "Error/TurnOff");
                // return RedirectToAction("TurnOff", "Error", new { id = 1, viewname = "url" });
            }

            //string returnUrl = Request.QueryString["returnUrl"];
            if (!string.IsNullOrWhiteSpace(returnUrl))
                ViewBag.returnUrl = returnUrl;
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
                return Redirect(ViewBag.BasePath + "u");
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
            string regStr = string.Empty;
            const string regPattern = @"^1[34578]\d{9}$";
            if (model.UserName.Contains("@"))
            {
                regStr = "Email";
            }
            else if (Regex.IsMatch(model.UserName, regPattern))
            {
                regStr = "Phone";
            }
            else
            {
                regStr = "HaoLinCard";
            }

            ViewBag.RegisterToggle = regStr;//注册方式     
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            ViewBag.SMSIsOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            if (IsCloseLogin)
            {
                return Redirect(ViewBag.BasePath + "Error/TurnOff");
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
                AccountsPrincipal userPrincipal = null;
                if (regStr == "Phone")
                {
                    BLL.Shop.Account.User UserManage = new BLL.Shop.Account.User();
                    User user = new User();
                    user = UserManage.GetPhoneUser(model.UserName, model.Password);
                    if (user != null)
                        userPrincipal = new AccountsPrincipal(user.UserID);
                }
                else if (regStr == "Email")
                {
                    userPrincipal = AccountsPrincipal.ValidateLogin4Email(model.UserName, model.Password);
                }
                else
                {
                    Maticsoft.BLL.Shop_CardUserInfo cardUserInfo = new BLL.Shop_CardUserInfo();
                    string pwdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "md5").ToLower();
                    List<Model.Shop_CardUserInfo> list = cardUserInfo.GetModelList("CardNo ='" + model.UserName + "' and [Password]='" + pwdStr + "'");
                    if (list.Count > 0)
                    {
                        string uid = list[0].UserName;
                        userPrincipal = new AccountsPrincipal(uid);

                    }
                }
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
                FormsAuthentication.SetAuthCookie(currentUser.UserName, model.RememberMe);
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;

                #region 将用户的OpenId存入缓存中
                //if (null != currentUser && !string.IsNullOrWhiteSpace(this.UserOpen))
                //{
                //    if (null != DataCache.GetCache(currentUser.UserID.ToString())) DataCache.DeleteCache(currentUser.UserID.ToString());
                //    DataCache.SetCache(currentUser.UserID.ToString(), this.UserOpen);
                //}
                #endregion

                #region 把用户登录的ID 写进Cookies 用于分享

                if (string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("UserNameID", "Value")))
                {
                    Maticsoft.Common.Cookies.setCookie("UserNameID", "", 60 * 24 * 30);
                    Maticsoft.Common.Cookies.setCookie("UserNamePhone", "", 60 * 24 * 30);
                }
                if (currentUser != null)
                {
                    Maticsoft.Common.Cookies.updateCookies("UserNamePhone", currentUser.Phone, 60 * 24 * 30);
                    int userid = currentUser.UserID;

                    HttpCookie acookie = Request.Cookies["UserNameID"];
                    acookie.Values.Remove("Value");
                    acookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(acookie);
                    acookie.Values["Value"] = userid.ToString();
                    acookie.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Add(acookie);


                }

                #endregion
                #region 把推荐人的改成自己的ID

                if (string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("Recommend_UserNameID", "Value")))
                {
                    Maticsoft.Common.Cookies.setCookie("Recommend_UserNameID", "", 60 * 24 * 30);
                }
                if (currentUser != null)
                {
                    int userid = currentUser.UserID;

                    HttpCookie acookie = Request.Cookies["Recommend_UserNameID"];
                    acookie.Values.Remove("Value");
                    acookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(acookie);
                    acookie.Values["Value"] = userid.ToString();
                    acookie.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Add(acookie);


                }

                #endregion
                //登录成功加积分
                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();

                int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");


                BLL.Shop.Products.ShoppingCartHelper.LoadShoppingCart(currentUser.UserID);

                returnUrl = Server.UrlDecode(returnUrl);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    //TempData["pointer"] = pointers;
                    return Redirect("/m" + currentUser.UserID + "/u");
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

                    #region 将用户的OpenId存入缓存中
                    //if (null != currentUser && !string.IsNullOrWhiteSpace(this.UserOpen))
                    //{
                    //    if (null != DataCache.GetCache(currentUser.UserID.ToString())) DataCache.DeleteCache(currentUser.UserID.ToString());
                    //    DataCache.SetCache(currentUser.UserID.ToString(), this.UserOpen);
                    //}
                    #endregion

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

            ViewBag.SMSIsOpen = true;//BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            if (IsCloseRegister)
            {
                return Redirect(ViewBag.BasePath + "Error/TurnOff");
                //return RedirectToAction("TurnOff", "Error", new { id=1,viewname = "url"});
            }

            if (CurrentUser != null && CurrentUser.UserType != "AA")
                return Redirect(ViewBag.BasePath + "u");
            //return RedirectToAction("Index", "UserCenter", new { id = 1, viewname = "url" });
            ViewBag.RegisterToggle = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式
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
            ViewBag.IP = GetClientIp();
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
            bool IsOpen = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");//是否开启手机验证
            ViewBag.SMSIsOpen = IsOpen;//是否开启手机验证
            bool IsCloseRegister = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
            if (IsCloseRegister)
            {
                return Redirect(ViewBag.BasePath + "Error/TurnOff");
                //return RedirectToAction("TurnOff", "Error", new { id = 1, viewname = "url" });
            }

            string regStr = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");//注册方式         
            ViewBag.RegisterToggle = regStr;

            //判断昵称是否已存在
            if (userBusManage.HasUserByNickName(model.NickName + "weishangcheng"))
            {
                ViewBag.hasnickname = "昵称已被抢先使用，换一个试试";
                return View(model);
            }
            if (userBusManage.HasUserByUserName(model.UserName + "weishangcheng"))
            {
                ViewBag.hasemail = "该手机已被注册";
                return View(model);
            }
            //{
            //    if (regStr == "Phone")
            //    {
            //        ViewBag.hasemail = "该手机已被注册";
            //    }
            //    else
            //    {
            //        ViewBag.hasemail = "该邮箱已被注册";
            //    }
            //    return View(model);
            //}
            //wusg 20141219 主要较验是否有相同的手机号,已验证的,username 随机分配一个guid  model.UserName是电话来着
            if (userBusManage.HasUserByPhone(model.UserName))
            {
                ViewBag.hasemail = "该手机已被注册";
                return View(model);
            }

            // bool IsCloseRegisterSendEmail = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_RegisterEmailCheck");
            User newUser = new User();
            //DONE: 警告DB字段未对应: Email 字段 varchar(100) UserName 字段 varchar(50) 已完成 BEN DONE 2012-11-22
            newUser.UserName = model.UserName + "_weishangcheng";
            newUser.NickName = model.NickName;  //昵称名称相同
            newUser.Password = AccountsPrincipal.EncryptPassword(model.Password);
            if (regStr == "Phone")
            {
                newUser.Phone = model.UserName;
                model.Phone = model.UserName;
            }
            else
            {
                newUser.Email = model.UserName;
            }
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
                BLL.Members.UsersExp bll = new BLL.Members.UsersExp();
                Model.Members.UsersExpModel ue = new Model.Members.UsersExpModel();
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
                //无推荐号
                if (string.IsNullOrWhiteSpace(RecommendUserNameID))
                {
                    ue.RecommendUserID = 0;
                }
                else
                {
                    ue.RecommendUserID = int.Parse(RecommendUserNameID);
                }

                if (!bll.AddUsersExp(ue))
                {
                    userManage.Delete(userid);
                    userExpManage.DeleteUsersExp(userid);
                    ModelState.AddModelError("Message", "注册失败！");
                    return View(model);
                }
                else
                {
                    if (regStr == "Phone")
                    {
                        var u = new BLL.Shop.Account.User();
                        //更改手机号为已验证，这里没有事务性，如果同时有两个人输入一个手机号就可能有问题
                        u.SetPhoneMark(newUser.UserName, newUser.Phone);

                        // 调用用户注册 分配卡，找营销员（好代） 
                        //Maticsoft.BLL.CallApi api = new BLL.CallApi();
                        //if (!string.IsNullOrWhiteSpace(RecommendUserNameID))
                        //{
                        //    Maticsoft.Model.Members.Users userinfo = userManage.GetModel(int.Parse(RecommendUserNameID));
                        //    if (userinfo != null)
                        //    {
                        //        string msg = "";
                        //        api.AutoActive(ref msg, userinfo.Phone, newUser.Phone, newUser.NickName, newUser.Phone);
                        //    }
                        //}
                        //else
                        //{
                        //    string msg = "";
                        //    api.AutoActive(ref msg, "", newUser.Phone, newUser.NickName, newUser.Phone);
                        //}
                    }
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

                userPrincipal = new AccountsPrincipal(userid);
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);

                HttpContext.User = userPrincipal;
                FormsAuthentication.SetAuthCookie(currentUser.UserName, true /* createPersistentCookie */);
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                //注册加积分
                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                int pointers = pointBll.AddPoints(2, userid, "注册成功");
                // }
                #endregion
                #region 把用户登录的ID 写进Cookies 用于分享

                if (string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("UserNameID", "Value")))
                {
                    Maticsoft.Common.Cookies.setCookie("UserNameID", "", 60 * 24 * 30);
                }
                if (userid > 0)
                {
                    HttpCookie acookie = Request.Cookies["UserNameID"];
                    acookie.Values.Remove("Value");
                    acookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(acookie);
                    acookie.Values["Value"] = userid.ToString();
                    acookie.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Add(acookie);

                }


                #endregion

                #region 将用户的OpenId存入缓存中
                //if (userid > 0 && !string.IsNullOrWhiteSpace(this.UserOpen))
                //{
                //    if (null != DataCache.GetCache(userid.ToString())) DataCache.DeleteCache(userid.ToString());
                //    DataCache.SetCache(userid.ToString(), this.UserOpen);
                //}
                #endregion

                #region 把推荐人的改成自己的ID

                if (string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("Recommend_UserNameID", "Value")))
                {
                    Maticsoft.Common.Cookies.setCookie("Recommend_UserNameID", "", 60 * 24 * 30);
                }
                if (currentUser != null)
                {
                    HttpCookie acookie = Request.Cookies["Recommend_UserNameID"];
                    acookie.Values.Remove("Value");
                    acookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(acookie);
                    acookie.Values["Value"] = userid.ToString();
                    acookie.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Add(acookie);


                }

                #endregion

                string img = "/Areas/MShop/Themes/M1/Content/img/y_03.jpg";
                string tips = "用户中心";
                string msg1 = "恭喜您已注册成为好粉";
                string msg2 = "";
                string url = ViewBag.BasePath + "u";


                return ReturnTips(Maticsoft.Model.Shop.Order.EnumHelper.ReturnTipsType.Success, url, tips, msg1, msg2, "ReturnTips");

                // return Redirect(ViewBag.BasePath+"m");
                // return Redirect(ViewBag.BasePath+"u");
                // return RedirectToAction("Personal", "UserCenter", new { id = 1, viewname = "url" });

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
            bool valid = true;
            DataSet ds = userManage.GetList(string.Format(" Phone='{0}' and IsPhoneVerify=1 ", userName));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    valid = false;
                    //手机号码存在 
                    Maticsoft.BLL.Members.UsersExp userBll = new BLL.Members.UsersExp();
                    //ds.Table[索引].Rows[索引][列名]
                    int userid = (int)ds.Tables[0].Rows[0]["UserID"];
                    Maticsoft.Model.Members.UsersExpModel userexpmodel = userBll.GetUsersExpInfo(userid);

                    Session["userexpmodel"] = userexpmodel;
                }
            }
            // bool valid = !(userBusManage.HasUserByUserName(userName));
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
            //  bool valid = !(userBusManage.HasUserByPhone(phone));
            bool valid = true;
            DataSet ds = userManage.GetList(string.Format(" Phone='{0}' and IsPhoneVerify=1 ", phone));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    valid = false;
                }
            }
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
        [HttpPost]
        public ActionResult SendSMSForPwd(string phone, string Code)
        {
            string Phone = phone;
            string code = Code;
            string SmsCheckCode = Session["SmsCheckCode"].ToString();
            if (String.IsNullOrWhiteSpace(Phone))
            {
                return Content("False");
            }

            if (String.IsNullOrWhiteSpace(code))
            {
                return Content("SmsValCode");
            }
            else
            {
                if (code.ToLower() != SmsCheckCode.ToLower())
                {
                    return Content("SmsValCode");
                }
            }
            if (!IsSendSmsMaxCount())
            {
                return Content("False");
            }
            Random rnd = new Random();
            int rand = rnd.Next(100000, 999999);
            string content = "您健康微商城正在进行重置密码操作,验证码为：" + rand.ToString();
            Session["SMSCode"] = rand;
            Session["SMS_DATE"] = DateTime.Now;
            string[] numbers = new string[] { Phone };
            string errorMsg = "";
            bool isSuccess = Maticsoft.Web.Components.SMSHelper.SendSMS(Phone, content, out errorMsg);
            return isSuccess ? Content("True") : Content("False");
            //return Content("True");
        }

        /// <summary>
        /// Ajax发送短信
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult SendSMS(FormCollection Fm)
        {
            string Phone = Fm["Phone"];
            string SmsValCode = Fm["SmsValCode"];
            string SmsCheckCode = Session["SmsCheckCode"].ToString();

            Session["SmsValCode"] = SmsValCode;

            if (String.IsNullOrWhiteSpace(SmsCheckCode))
            {
                return Content("SmsFalse");
            }
            else
            {
                if (SmsCheckCode.ToLower() != SmsValCode.ToLower())
                {
                    return Content("SmsFalse");
                }
            }
            if (String.IsNullOrWhiteSpace(Phone))
            {
                return Content("False");
            }
            if (!IsSendSmsMaxCount())
            {
                return Content("False");
            }
            Random rnd = new Random();
            int rand = rnd.Next(100000, 999999);
            string content = "尊敬的会 员,您微商城的注册验证码为：" + rand.ToString() + ",请及时输入.";
            Session["SMSCode"] = rand;
            Session["SMS_DATE"] = DateTime.Now;
            string[] numbers = new string[] { Phone };

            //if (String.IsNullOrWhiteSpace(Phone))
            //{
            //    return Content("False");
            //}
            //Random rnd = new Random();
            //int rand = rnd.Next(100000, 999999);
            //string content = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Content");
            //content = content.Replace("{SMSCode}", rand.ToString());
            //Session["SMSCode"] = rand;
            //Session["SMS_DATE"] = DateTime.Now;
            //string[] numbers = new string[] { Phone };
            string errorMsg = "";
            bool isSuccess = Maticsoft.Web.Components.SMSHelper.SendSMS(Phone, content, out errorMsg);
            return isSuccess ? Content("True") : Content("False");

        }
        /// <summary>
        /// 比较一个IP一天最多可以发生多少条信息如果超过不发送
        /// </summary>
        /// <returns></returns>
        public bool IsSendSmsMaxCount()
        {
            bool flag = true;
            string IP = GetClientIp();
            string CacheKey = "SMSIP-" + IP;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(23).AddMinutes(59);
            if (objModel == null)
            {
                objModel = 0;
            }
            Maticsoft.Common.DataCache.SetCache(CacheKey, int.Parse(objModel.ToString()) + 1, today, TimeSpan.Zero);

            int SendCount = int.Parse(objModel.ToString()) + 1;
            int SmsMaxCount = int.Parse(ConfigurationManager.AppSettings["SmsMaxCount"]);
            if (SendCount > SmsMaxCount)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("短信发送日志超过条数的IP").Write("微信商城IP为:" + IP);
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 获取客户端Ip
        /// </summary>
        /// <returns></returns>
        public String GetClientIp()
        {

            HttpRequest request = System.Web.HttpContext.Current.Request;
            string clientIP = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(clientIP))
            { clientIP = request.ServerVariables["REMOTE_ADDR"]; }
            if (string.IsNullOrEmpty(clientIP)) { clientIP = request.UserHostAddress; }
            if (string.IsNullOrEmpty(clientIP)) { clientIP = "0.0.0.0"; }

            return clientIP;
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

                #region 将用户的OpenId存入缓存中
                //if (null != currentUser && !string.IsNullOrWhiteSpace(this.UserOpen))
                //{
                //    if (null != DataCache.GetCache(currentUser.UserID.ToString())) DataCache.DeleteCache(currentUser.UserID.ToString());
                //    DataCache.SetCache(currentUser.UserID.ToString(), this.UserOpen);
                //}
                #endregion

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

            string returnUrl = Common.Globals.UrlDecode(Request.Params["returnUrl"]);
            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = ViewBag.BasePath + "u";
            }
            ViewBag.ReturnUrl = returnUrl;
            if (currentUser != null)
            {
                return Redirect(returnUrl);
            }
            //直接登录
            if (userBusManage.HasUserByUserName(UserOpen))
            {
                #region  自动登陆
                Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
                Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, UserOpen);
                if (wUserModel.UserId <= 0)
                {
                    return View(viewName);
                }
                AccountsPrincipal userPrincipal = new AccountsPrincipal(wUserModel.UserId);
                if (userPrincipal == null)
                {
                    return View(viewName);
                }
                currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    return View(viewName);
                }
                HttpContext.User = userPrincipal;
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                FormsAuthentication.SetAuthCookie(UserOpen, true);

                #region 将用户的OpenId存入缓存中
                //if (null != currentUser && !string.IsNullOrWhiteSpace(this.UserOpen))
                //{
                //    if (null != DataCache.GetCache(currentUser.UserID.ToString())) DataCache.DeleteCache(currentUser.UserID.ToString());
                //    DataCache.SetCache(currentUser.UserID.ToString(), this.UserOpen);
                //}
                #endregion

                return Redirect(returnUrl);
                #endregion
            }
            return View(viewName);
        }

        [HttpPost]
        public ActionResult AjaxRegBind(string NickName)
        {
            if (String.IsNullOrWhiteSpace(UserOpen))
            {
                return Content("0");
            }
            Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
            Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, UserOpen);
            //如果存在该用户名，先直接绑定，然后登录
            if (userBusManage.HasUserByUserName(UserOpen))
            {
                Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User(UserOpen);
                if (user == null || user.UserID <= 0)
                {
                    return Content("0");
                }
                wUserModel.UserId = user.UserID;
                wUserModel.NickName = NickName;

                #region 将用户的OpenId存入缓存中
                //if (null != user && !string.IsNullOrWhiteSpace(this.UserOpen))
                //{
                //    if (null != DataCache.GetCache(user.UserID.ToString())) DataCache.DeleteCache(user.UserID.ToString());
                //    DataCache.SetCache(user.UserID.ToString(), this.UserOpen);
                //}
                #endregion

                if (!wUserBll.Update(wUserModel))
                {
                    return Content("0");
                }
                return Content("1");
            }

            if (wUserModel == null)
            {
                return Content("0");
            }

            User newUser = new User();
            //DONE: 警告DB字段未对应: Email 字段 varchar(100) UserName 字段 varchar(50) 已完成 BEN DONE 2012-11-22
            newUser.UserName = UserOpen;
            newUser.NickName = NickName; //昵称名称相同
            newUser.Password = AccountsPrincipal.EncryptPassword(UserOpen);
            newUser.Email = "";
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

            #region 将用户的OpenId存入缓存中
            //if (userid > 0 && !string.IsNullOrWhiteSpace(this.UserOpen))
            //{
            //    if (null != DataCache.GetCache(userid.ToString())) DataCache.DeleteCache(userid.ToString());
            //    DataCache.SetCache(userid.ToString(), this.UserOpen);
            //}
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

        #region 会员卡
        public ActionResult UserCard(int pageIndex = 1, string viewName = "UserCard")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "会员卡";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
            Maticsoft.BLL.Members.UserCard cardBll = new BLL.Members.UserCard();
            BLL.Pay.BalanceDetails balanDetaBll = new BLL.Pay.BalanceDetails();
            int userid = 0;
            if (CurrentUser == null)
            {
                Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
                if (String.IsNullOrWhiteSpace(OpenId) || String.IsNullOrWhiteSpace(UserOpen))
                {
                    return View();
                }
                Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, UserOpen);
                if (wUserModel.UserId <= 0)
                {
                    return View();
                }
                AccountsPrincipal userPrincipal = new AccountsPrincipal(wUserModel.UserId);
                if (userPrincipal == null)
                {
                    return View();
                }
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    return View();
                }
                HttpContext.User = userPrincipal;
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                userid = currentUser.UserID;
                FormsAuthentication.SetAuthCookie(UserOpen, true);
            }
            else
            {
                userid = CurrentUser.UserID;
            }
            //首页用户数据
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(userid);
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
            toalCount = balanDetaBll.GetRecordCount(" UserId =" + userid);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Pay.BalanceDetails> list = balanDetaBll.GetListByPage(" UserId = " + userid, startIndex, endIndex);
            PagedList<Maticsoft.Model.Pay.BalanceDetails> lists = new PagedList<Maticsoft.Model.Pay.BalanceDetails>(list, pageIndex, _pageSize, toalCount);
            return View(lists);
        }

        [HttpPost]
        public ActionResult GetUserCard()
        {
            Maticsoft.BLL.Members.UserCard cardBll = new BLL.Members.UserCard();

            if (CurrentUser == null)
            {
                if (String.IsNullOrWhiteSpace(OpenId) || String.IsNullOrWhiteSpace(UserOpen))
                {
                    return Content("NoOpen");
                }
                return Content("NoLogin");
            }
            if (cardBll.AddCard(currentUser.UserID))
            {
                return Content("True");
            }
            return Content("False");
        }

        [HttpPost]
        public ActionResult BindUserCard(FormCollection collection)
        {
            Maticsoft.BLL.Members.UserCard cardBll = new BLL.Members.UserCard();
            string name = collection["Name"];
            string phone = collection["Phone"];
            if (String.IsNullOrWhiteSpace(UserOpen))
            {
                return Content("0");
            }
            Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
            Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, UserOpen);
            //如果存在该用户名，先直接绑定，然后登录
            if (userBusManage.HasUserByUserName(UserOpen))
            {
                Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User(UserOpen);
                if (user == null || user.UserID <= 0)
                {
                    return Content("0");
                }
                wUserModel.UserId = user.UserID;
                wUserModel.NickName = name;
                if (!wUserBll.Update(wUserModel))
                {
                    return Content("0");
                }
                return Content("1");
            }

            if (wUserModel == null)
            {
                return Content("0");
            }

            User newUser = new User();
            //DONE: 警告DB字段未对应: Email 字段 varchar(100) UserName 字段 varchar(50) 已完成 BEN DONE 2012-11-22
            newUser.UserName = UserOpen;
            newUser.NickName = name; //昵称名称相同
            newUser.TrueName = name;
            newUser.Phone = phone;
            newUser.Password = AccountsPrincipal.EncryptPassword(UserOpen);
            newUser.Email = "";
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
            wUserModel.NickName = name;
            if (!wUserBll.Update(wUserModel))
            {
                return Content("0");
            }
            //添加会员卡
            if (!cardBll.AddCard(userid))
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

        #region 健康卡
        /// <summary>
        /// 健康卡
        /// </summary>
        /// <returns></returns>
        public ActionResult HLCard()
        {
            return View();
        }
        public ActionResult HLRegAgreement(int Type)
        {
            return View();
        }
        public ActionResult HLCardActive()
        {
            Maticsoft.Model.Shop_CardType cardType = Session["Shop_CardType"] as Shop_CardType;

            Maticsoft.Model.Shop_Card card = Session["Shop_Card"] as Shop_Card;
            if (card != null)
            {
                ViewBag.CardTypeNo = card.CardTypeNo;
                ViewBag.Batch = card.Batch;
                ViewBag.SalesName = card.SalesName;
                ViewBag.cardNum = card.CardNo;
                ViewBag.CardSysId = card.Id;
                ViewBag.Pwd = card.Password;
            }
            if (cardType != null)
            {
                ViewBag.RegisterType = cardType.RegisterType ? "simple" : "normal";
                ViewBag.PersonNum = cardType.PersonNum;
            }

            return View();
        }

        public ActionResult ConfirmAgreement(Shop_CardUserInfo userinfo)
        {
            Shop_CardType cardType = Session["Shop_CardType"] as Shop_CardType;
            ViewBag.Agreement = cardType.Agreement;
            ViewBag.ActivatePrompt = cardType.ActivatePrompt;

            Session["CardUserInfo"] = userinfo;

            return View();
        }

        public ActionResult ActiveSuccess()
        {
            var userInfo = Session["CardUserInfo"] as Shop_CardUserInfo;
            if (userInfo.CardId == null)
            {
                //模拟登录
               // Login(new LogOnModel() { UserName = userInfo.UserName, Password = userInfo.Password }, string.Empty);
            }
            else
            {
               // Login(new LogOnModel() { UserName = userInfo.CardNo, Password = userInfo.Password }, string.Empty);
            }
            return View();
        }

        public void CardUserInfo()
        {
            var userInfo = Session["CardUserInfo"] as Shop_CardUserInfo;

            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("开始激活处理.....,用户身份证为：" + userInfo.CardId);
            User u = Session["UserInfo"] as User;
            Shop_Card card = Session["Shop_Card"] as Shop_Card;
            //add cardsysid 
            userInfo.CardSysId = card.Id;
            userInfo.CardTypeNo = card.CardTypeNo;
            bool Instead = false;
            try
            {
                userInfo.ActiveDate = DateTime.Now;
                userInfo.CREATEDATE = DateTime.Now;

                if (userInfo.CardId == null)
                {
                    //简单激活 以卡号创建账户
                   // userInfo.UserId = userInfo.UserName;
                  //  userInfo.Password = userInfo.S_Password;
                    Instead = true;
                }
                else
                {
                    //根据身份证号查询是否之前激活过信客卡，是：将该信客卡绑定到当前代激活用户账号上，否：创建用户并绑定信客卡
                    List<Shop_CardUserInfo> userInfoList = carduserInfoBll.GetModelList(" CardId='" + userInfo.CardId + "'");
                    var DisList = (from rec in userInfoList select new { rec = rec.UserId }).ToList().Distinct();
                    if (DisList.Count() >= 1)
                    {
                        //将该信客卡绑定到当前代激活用户账号上
                        userInfo.UserId = userInfoList.First().UserId;
                    }
                    else if (DisList.Count() == 0)
                    {
                       // userInfo.UserId = userInfo.CardNo;
                        Instead = true;
                    }

                }

                Active(userInfo, card.Batch, Instead);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志-异常").Write("结束处理......" + ex.InnerException.Message + ",ex:" + ex);
            }
            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("结束处理......");
        }

        public void Active(Shop_CardUserInfo userInfo, string Batch, bool Instead = false)
        {
            UserCardLogic uc = new UserCardLogic();
            Shop_Card card = Session["Shop_Card"] as Shop_Card;

            //userInfo.S_Password = card.Password;

            string result = "";
            try
            {

                result = uc.ActiveUserInfo(userInfo, Instead);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.InnerException.Message);
                Response.Write("健康卡激活异常,请联系客服！错误信息：" + ex.Message + ",msg:" + ex);
            }
            try
            {
                string errorMsg = "";
                string content = "您的健康卡:" + userInfo.CardNo + "已经激活!";
                if (result == "1")
                {
                    //根据用户名查找 userInfo.UserId
                    Maticsoft.Accounts.Bus.User u = new Maticsoft.Accounts.Bus.User();
                    int userid = u.GetUserByName(userInfo.UserName).UserID;
                    //  Maticsoft.Common.ErrorLogTxt.GetInstance("线程调试日志").Write("主线程的id为:" + Thread.CurrentThread.ManagedThreadId);


                    Getcoupons(Batch, userid, userInfo.CardNo);


                    if (!string.IsNullOrEmpty(userInfo.CardId))
                    {
                        Maticsoft.Web.Components.SMSHelper.SendSMS(userInfo.Moble, content, out errorMsg);
                        //Task.Run(() =>
                        //{
                        //    MailSender(userInfo.UserName, userInfo.Email, userInfo.CardNo);
                        //});
                    }
                }
                Response.Write(result);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.Message);
            }
        }

        //根据规则自动发送优惠券
        public void Getcoupons(string batchid, int userID, string cardno)
        {
            //  Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("线程调试日志").Write("coupons的线程id为:" + Thread.CurrentThread.ManagedThreadId);
            Maticsoft.BLL.Shop_CouponRuleExt cop = new Maticsoft.BLL.Shop_CouponRuleExt();
            var list = cop.GetListByBatchID(batchid);
            var sc = CouponServices.GetInstance();


            list.ForEach(a => a.UserID = userID);

            Maticsoft.Services.ErrorLogTxt.GetInstance("进来过11").Write("数量为：" + list.Count);
            sc.SendMessageQuque<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>(list, path);
            //backup queue
            sc.SendMessageQuque<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>(list, card_Path, cardno);
        }
        #endregion

        #region 校园创业卡(liyongqin)
        public ActionResult Campuscy()
        {
            return View();
        }

        public ActionResult CampusAgreement()
        {
            return View();
        }

        public ActionResult CampuscyCardActive()
        {
            ViewBag.userexpmodel = Session["userexpmodel"];
            ViewBag.SMSIsOpen = true;
            Maticsoft.Model.Shop_CardType cardType = Session["Shop_CardType"] as Shop_CardType;

            Maticsoft.Model.Shop_Card card = Session["Shop_Card"] as Shop_Card;

            if (card != null)
            {
                ViewBag.CardTypeNo = card.CardTypeNo;
                ViewBag.Batch = card.Batch;
                ViewBag.SalesName = card.SalesName;
                ViewBag.cardNum = card.CardNo;
                ViewBag.CardSysId = card.Id;
                ViewBag.Pwd = card.Password;
            }
            if (cardType != null)
            {
                ViewBag.RegisterType = "CardCampus";
                ViewBag.PersonNum = cardType.PersonNum;
            }

            return View();
        }

        public void CardCampusInfo(Shop_CardUserInfo userinfo)
        {
            Session["CardUserInfo"] = userinfo;

            var userInfo = Session["CardUserInfo"] as Shop_CardUserInfo;

            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("开始激活处理.....,用户身份证为：" + userInfo.CardId);
            User u = Session["UserInfo"] as User;
            Shop_Card card = Session["Shop_Card"] as Shop_Card;

            userInfo.CardSysId = card.Id;
            userInfo.CardTypeNo = card.CardTypeNo;
            bool Instead = false;
            try
            {
                userInfo.ActiveDate = DateTime.Now;
                userInfo.CREATEDATE = DateTime.Now;

                if (userInfo.CardId == null)
                {
                    //简单激活 以卡号创建账户
                    //userInfo.UserId = userInfo.UserName;
                    //userInfo.Password = userInfo.S_Password;
                    Instead = true;
                }
                else
                {
                    //根据身份证号查询是否之前激活过校园卡，是：将该校园卡绑定到当前代激活用户账号上，否：创建用户并绑定校园卡
                    List<Shop_CardUserInfo> userInfoList = carduserInfoBll.GetModelList(" CardId='" + userInfo.CardId + "'");
                    var DisList = (from rec in userInfoList select new { rec = rec.UserId }).ToList().Distinct();
                    if (DisList.Count() >= 1)
                    {
                        //将该校园卡绑定到当前代激活用户账号上
                        userInfo.UserId = userInfoList.First().UserId;
                    }
                    else if (DisList.Count() == 0)
                    {
                        //userInfo.UserId = userInfo.CardNo;
                        Instead = true;
                    }
                }
                ActiveCampus(userInfo, card.Batch, Instead);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志-异常").Write("结束处理......" + ex.InnerException.Message + ",ex:" + ex);
            }
            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("卡激活日志").Write("结束处理......");
        }

        //激活校园卡
        public void ActiveCampus(Shop_CardUserInfo userInfo, string Batch, bool Instead = false)
        {
            UserCardLogic uc = new UserCardLogic();
            Shop_Card card = Session["Shop_Card"] as Shop_Card;

           // userInfo.S_Password = card.Password;

            string result = "";
            #region
            try
            {
                result = uc.ActiveUserInfo(userInfo, Instead);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.InnerException.Message);
                Response.Write("校园卡激活异常,请联系客服！错误信息：" + ex.Message + ",msg:" + ex);
            }
            #endregion

            #region
            try
            {
                string errorMsg = "";
                string content = "恭喜您已激活健康商城大学生校园创业卡【" + userInfo.CardNo + "】,您已成为健康商城的一份子，同时您已拥有 大学生创业微店的尊贵 身份，现在开始您的健康商城梦想之旅吧！";
                if (result == "1")
                {
                    Maticsoft.Accounts.Bus.User u = new Maticsoft.Accounts.Bus.User();
                    int userid = u.GetUserByName(userInfo.UserName).UserID;
                    //更改手机激活 IsPhoneVerify=1
                    UpdateIsPhoneVerify(userid, 1);
                    //更改 用户类型为大学生创业卡 UserOldType=22
                    Maticsoft.BLL.Members.UsersExp usersbll = new BLL.Members.UsersExp();
                    Maticsoft.Model.Members.UsersExpModel userexpmodel = usersbll.GetUsersExpInfo(userid);
                    if (userexpmodel.UserOldType == null || userexpmodel.UserOldType < 2)
                    {
                        userexpmodel.UserOldType = 22;
                        userexpmodel.UserID = userid;
                        usersbll.UpdateUsersExp(userexpmodel);
                    }
                    //Getcoupons(Batch, userid, userInfo.CardNo);

                    if (!string.IsNullOrEmpty(userInfo.CardId))
                    {
                        Maticsoft.Web.Components.SMSHelper.SendSMS(userInfo.Moble, content, out errorMsg);
                    }
                }
                Response.Write(result);
            }
            catch (Exception ex)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("注册激活日志").Write("错误......" + ex.Message);
            }
            #endregion
        }
        /// <summary>
        /// 修改手机激活状态
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpdateIsPhoneVerify(int UserID, int IsPhoneVerify)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Users set ");
            strSql.Append("IsPhoneVerify=" + IsPhoneVerify);
            strSql.Append(" where UserID= " + UserID);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), "");
            if (rows > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //激活成功页面
        public ActionResult CampusSuccess()
        {
            var userInfo = Session["CardUserInfo"] as Shop_CardUserInfo;
            ViewBag.userInfo = userInfo;
            if (userInfo.CardId == null)
            {
                //模拟登录
              //  Login(new LogOnModel() { UserName = userInfo.UserName, Password = userInfo.Password }, string.Empty);
            }
            else
            {
                //Login(new LogOnModel() { UserName = userInfo.CardNo, Password = userInfo.Password }, string.Empty);
            }
            return View();
        }

        //如果手机号码存在 查询该手机号码UserOldType<2 是:普通会员 否:高级会员
        public ActionResult UpdateCampusType(string userName)
        {
            DataSet ds = userManage.GetList(string.Format(" Phone='{0}' and IsPhoneVerify=1 ", userName));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //手机号码存在 
                    Maticsoft.BLL.Members.UsersExp userBll = new BLL.Members.UsersExp();
                    int userid = (int)ds.Tables[0].Rows[0]["UserID"];
                    Maticsoft.BLL.Members.UsersExp usersbll = new BLL.Members.UsersExp();
                    Maticsoft.Model.Members.UsersExpModel userexpmodel = userBll.GetUsersExpInfo(userid);

                    if (userexpmodel.UserOldType == null || userexpmodel.UserOldType < 2)
                    {
                        return Content("true");
                    }
                    else
                    {
                        return Content("false");
                    }
                }
                else
                {
                    return Content("Undefined");
                }
            }
            else
            {
                return Content("Undefined");
            }
        }
        #endregion

        #region
        /// <summary>
        /// 手机找回密码
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPwdByPhone()
        {
            if (CurrentUser != null && CurrentUser.UserType != "AA") return RedirectToAction("Index", "UserCenter");

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "找回密码" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        /// <summary>
        /// 手机找回密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindPwdByPhone(FormCollection collection)
        {
            try
            {
                Session["Phone"] = collection["UserName"].ToString();
                if ((Session["SmsCheckCode"] != null) && (Session["SmsCheckCode"].ToString() != ""))
                {
                    if (Session["SmsCheckCode"].ToString().ToLower() == collection["CheckCode"].Trim().ToLower())
                    {
                        Session["SmsCheckCode"] = null;
                        return RedirectToAction("ReSetPwd", "Account");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "验证时时出现网络异常，请稍后再试！");
            }
            ModelState.AddModelError("Error", "验证码错误！");
            Session["SmsCheckCode"] = null;
            ViewBag.CheckResult = "验证码错误";
            return View();
        }

        [HttpGet]
        public ActionResult ReSetPwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReSetPwd(FormCollection collection)
        {
            try
            {
                if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
                {
                    if (Session["CheckCode"].ToString().ToLower() == collection["CheckCode"].Trim().ToLower())
                    {
                        Session["CheckCode"] = null;
                        if (Session["Phone"] != null && Session["Phone"].ToString() != "")
                        {
                            BLL.Shop.Account.User UserManage = new BLL.Shop.Account.User();
                            string randCode = string.Empty;
                            string phone = Session["Phone"].ToString();
                            Session["Phone"] = null;
                            string pwd = collection["Password"].ToString();
                            User User = new User();
                            string UserName = UserManage.GetPhoneUser(phone).UserName;

                            if (UserName != null)
                            {
                                User.SetPassword(UserName, pwd);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.CheckResult = "验证码不正确";
                        return View();
                    }
                }
                else
                {
                    ViewBag.CheckResult = "验证码不正确";
                    return View();
                }

            }
            catch (Exception) { ModelState.AddModelError("Error", "重置密码时时出现网络异常，请稍后再试！"); }

            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region 商城通用提示页面
        public ActionResult ReturnTips(
            Maticsoft.Model.Shop.Order.EnumHelper.ReturnTipsType returnTipsType = Maticsoft.Model.Shop.Order.EnumHelper.ReturnTipsType.Fail, string url = "", string tips = "", string msg1 = "", string msg2 = "", string ViewName = "ReturnTips")
        {
            string img = "";
            switch (returnTipsType)
            {
                case Maticsoft.Model.Shop.Order.EnumHelper.ReturnTipsType.Success:
                    img = "/Areas/MShop/Themes/M1/Content/img/y_03.jpg";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.ReturnTipsType.Fail:
                    img = "/Areas/MShop/Themes/M1/Content/img/1_06.jpg";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.ReturnTipsType.Warning:
                    img = "/Areas/MShop/Themes/M1/Content/img/1_05.jpg";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.ReturnTipsType.Doubt:
                    img = "/Areas/MShop/Themes/M1/Content/img/3_03.jpg";
                    break;

            }
            ViewBag.img = img;
            ViewBag.url = url;
            ViewBag.tips = tips;
            ViewBag.msg1 = msg1;
            ViewBag.msg2 = msg2;

            return View(ViewName);
        }
        #endregion


    }
}
