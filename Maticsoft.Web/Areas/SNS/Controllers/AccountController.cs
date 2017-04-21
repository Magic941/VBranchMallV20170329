using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Ms;
using Maticsoft.Common;
using Maticsoft.OAuth;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class AccountController : SNSControllerBase
    {

        #region 成员变量
        private Maticsoft.Accounts.Bus.User userBusManage = new Maticsoft.Accounts.Bus.User();
        private BLL.Members.Users userManage = new BLL.Members.Users();
        private BLL.Members.UsersExp userExpManage = new BLL.Members.UsersExp();
        #endregion

        #region 登录

        public ActionResult Login()
        {
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            if (IsCloseLogin)
            {
                return RedirectToAction("TurnOff", "Error");
            }
            if (CurrentUser != null && CurrentUser.UserType != "AA")
                return RedirectToAction("Posts", "Profile");
            //三方登录Key
            ViewBag.Title = "登录";
            return View();
        }


        [HttpPost]
        public ActionResult Login(ViewModel.SNS.LogOnModel model, string returnUrl)
        {
            ViewBag.Title = "登录";
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            if (IsCloseLogin)
            {
                return RedirectToAction("TurnOff", "Error");
            }
            if (ModelState.IsValid)
            {
                AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(model.Email, model.Password);
                if (userPrincipal == null)
                {
                    ModelState.AddModelError("Message", "用户名或密码不正确, 请重新输入!");
                    return View(model);
                }

                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    ModelState.AddModelError("Message", "对不起，该帐号已被冻结，请联系管理员！");
                    return View(model);
                }
                HttpContext.User = userPrincipal;
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                //登录成功加积分
                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");
                
                if (Session["ReturnUrl"] != null && !String.IsNullOrWhiteSpace(Session["ReturnUrl"].ToString()))
                {
                    returnUrl = Session["ReturnUrl"].ToString();
                    Session.Remove("ReturnUrl");
                    return Redirect(returnUrl);
                }
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    TempData["pointer"] = pointers;
                    return RedirectToAction("Posts", "Profile");
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AjaxLogin(string UserName, string UserPwd)
        {
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
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
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(Globals.SESSIONKEY_USER);
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region 注册
        public ActionResult Register()
        {
            bool IsCloseRegister = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
            if (IsCloseRegister)
            {
                return RedirectToAction("TurnOff", "Error");
            }
            if (CurrentUser != null) return RedirectToAction("Posts", "Profile");
            ViewBag.Title = "注册";
            return View();
        }
        [HttpPost]
        public ActionResult Register(ViewModel.SNS.RegisterModel model)
        {
            ViewBag.Title = "注册";
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
            if (IsCloseLogin)
            {
                return RedirectToAction("TurnOff", "Error");
            }
            if (ModelState.IsValid)
            {
                //判断昵称是否已存在
                //判断邮箱是否已存在

                User newUser = new User();
                //DONE: 警告DB字段未对应: Email 字段 varchar(100) UserName 字段 varchar(50) 已完成 BEN DONE 2012-11-22
                newUser.UserName = model.Email;
                newUser.NickName = model.NickName;  //昵称名称相同
                newUser.Password = AccountsPrincipal.EncryptPassword(model.Password);
                newUser.Email = model.Email;
                newUser.Activity = true;
                newUser.UserType = "UU";
                newUser.Style = 1;
                newUser.User_dateCreate = DateTime.Now;
                newUser.User_cLang = "zh-CN";
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
                    FormsAuthentication.SetAuthCookie(model.Email, false /* createPersistentCookie */);
                    #region
                    //注册加积分
                    Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                    pointBll.AddPoints(2, userid, "注册成功");

                    Maticsoft.Model.SNS.UserAlbums AlbumsModel = new Model.SNS.UserAlbums();
                    Maticsoft.BLL.SNS.UserAlbums AlbumsBll = new BLL.SNS.UserAlbums();
                    Maticsoft.BLL.SNS.UserShip ShipBll = new BLL.SNS.UserShip();
                    AlbumsModel.AlbumName = "默认专辑";
                    AlbumsModel.CreatedDate = DateTime.Now;
                    AlbumsModel.CreatedNickName = model.NickName;
                    AlbumsModel.CreatedUserID = userid;
                    AlbumsBll.AddEx(AlbumsModel, 1);

                    string DefaultGravatar = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
                    DefaultGravatar = string.IsNullOrEmpty(DefaultGravatar) ? "/Upload/User/Gravatar/Default.jpg" : DefaultGravatar;
                    string TargetGravatarFile = BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
                    TargetGravatarFile = string.IsNullOrEmpty(TargetGravatarFile) ? "/Upload/User/Gravatar/" : TargetGravatarFile;
                    string path = ControllerContext.HttpContext.Server.MapPath("/");
                    if (System.IO.File.Exists(path + DefaultGravatar))
                    {
                        System.IO.File.Copy(path + DefaultGravatar, path + TargetGravatarFile + userid + ".jpg", true);
                    }
                    //自动给是粉丝
                    ShipBll.GiveUserFellow(userid);
                    #endregion
                    ////return Content("<script >alert('注册成功！');</script >", "text/html");   //通用后，放到基类里
                    //string script = String.Format("<script defer>alert('注册成功！');location.href='{0}'</script>", Url.Action("Login"));
                    //return Content(script, "text/html");

                    return Redirect("/UserCenter/Personal");
                }
            }

            return View(model);
        }
        #endregion

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

        #region 发送邮件
        /// <summary>
        /// 根据用户id得到用户的邮箱
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string EmailSuffix(string sUserID)
        {
            int userID = Globals.SafeInt(Globals.UrlDecode(sUserID), 0);
            BLL.Members.Users bll = new BLL.Members.Users();
            Model.Members.Users model = bll.GetModel(userID);
            if (model != null)
            {
                return model.Email;
            }
            else
                return "";
        }
        public string EmailUrl(string email)
        {
            string emailUrl = "";
            string emailStr = email.Substring(email.LastIndexOf('@') + 1);
            //谷歌邮箱特殊处理
            if (emailStr.Contains("gmail"))
            {
                emailStr = "google.com";
            }
            emailUrl = "http://mail." + emailStr;
            return emailUrl;
        }


        /// <summary>
        ///         发送邮件 type 0:表示注册激活邮件，1：表示找回密码邮件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="type"></param>
        protected void SendEmail(string username, string email, int type)
        {
            Maticsoft.BLL.Ms.EmailTemplet emailBll = new EmailTemplet();
            switch (type)
            {
                case 1:
                    emailBll.SendFindPwdEmail(username, email);
                    break;
            }
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
        /// 验证Email是否已存在
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsExistEmail(string email)
        {
            bool valid = !(userBusManage.HasUserByEmail(email));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult SendSMS(FormCollection Fm)
        {
            string Phone = Fm["Phone"];
            if (String.IsNullOrWhiteSpace(Phone))
            {
                return Content("False");
            }
              Random rnd = new Random();
              int rand = rnd.Next(100000, 999999);
            string content = "您的的手机效验码是：" + rand;
            Session["SMSCode"] = rand;
            string[] numbers=new string[]{Phone};
           bool isSuccess= Maticsoft.Web.Components.SMSHelper.SendSMS(content, numbers);
           return isSuccess ? Content("True") : Content("False");
        }


        #endregion

        #region 三方登陆

        private static string ToMediaName(int MediaId)
        {

            switch (MediaId)
            {
                case 1:
                    return "Google";//谷歌
                case 2:
                    return "Windows Live";//Windows Live

                case 3:
                    return "sina";//新浪

                case 4:
                    return "tencent";//腾讯微博
                case 5:
                    return "sohu";//搜狐

                case 6:
                    return "163";//网易
                case 7:
                    return "renren";//人人网
                case 8:
                    return "kaixin";//开心网

                case 9:
                    return "douban";//豆瓣
                case 12:
                    return "yahoo";//雅虎
                case 13:
                    return "QQ";//QQ空间

                case 16:
                    return "taobao";//淘宝
                case 17:
                    return "tianya";//天涯
                case 18:
                    return "alipay";//支付宝

                case 19:
                    return "baidu";//百度

                default:
                    return "maticsoft";
            }
        }
        #endregion

        #region 找回密码
        public ActionResult FindPwd()
        {
            if (CurrentUser != null) return RedirectToAction("Posts", "Profile");
            //三方登录Key
            ViewBag.Title = "找回密码";
            return View();
        }

        [HttpPost]
        public ActionResult FindPwd(FormCollection collection)
        {
            string email = collection["Email"].Trim();
            ViewData["Email"] = email;
            if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
            {
                if (Session["CheckCode"].ToString().ToLower() != collection["CheckCode"].Trim().ToLower())
                {
                    ModelState.AddModelError("Error", "验证码错误！");
                    Session["CheckCode"] = null;
                    return View(ViewData["Email"]);
                }
                else
                {
                    Session["CheckCode"] = null;
                }
            }
            else
            {
                return View(ViewData["Email"]);
            }
            Maticsoft.Accounts.Bus.User userinfo = new User(email);
            if (String.IsNullOrWhiteSpace(userinfo.NickName))
            {
                ModelState.AddModelError("Error", "该邮箱用户不存在！");
                return View(ViewData["Email"]);
            }
            //if (!(bool)userinfo.Activity)
            //{
            //    ModelState.AddModelError("Error", "您的帐号尚未通过邮箱验证,请重新发送确认邮件或者登录邮箱查看邮件！");
            //    return RedirectToAction("RegisterEmail", "Account", new { id = userinfo.UserID });
            //}
            try
            {
                SendEmail(userinfo.UserName, email, 1);
                return RedirectToAction("FindPwdEmail", "Account", new { email = ViewData["Email"] });
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "邮件发送过程中出现网络异常，请稍后再试！");
            }
            finally
            {

            }
            return View(ViewData["Email"]);
        }

        /// 找回密码邮箱验证页面
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ActionResult FindPwdEmail(string email)
        {
            ViewBag.Email = email;
            ViewBag.EmailUrl = EmailUrl(email);
            return View();
        }


        /// <summary>
        /// 激活密码找回
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyPassword()
        {
            string SecretKey = ViewBag.SecretKey = Request.QueryString["SecretKey"];
            if (!string.IsNullOrEmpty(SecretKey))
            {
                Maticsoft.BLL.SysManage.VerifyMail bll = new Maticsoft.BLL.SysManage.VerifyMail();
                if (bll.Exists(SecretKey))
                {
                    Maticsoft.Model.SysManage.VerifyMail model = bll.GetModel(SecretKey);
                    if (model != null && model.ValidityType.HasValue)
                    {
                        if (model.ValidityType.Value == 1)
                        {
                            // 0:邮箱验证未通过1：邮箱验证通过2：已过期
                            if (model.Status == 0)
                            {
                                TimeSpan ts = DateTime.Now - model.CreatedDate;
                                if (ts.TotalHours > 24)
                                {
                                    model.Status = 2;// 0:邮箱验证未通过1：邮箱验证通过2：已过期
                                    bll.Update(model);
                                    ViewBag.Msg = "找回密码的验证码已过期！";
                                    ModelState.AddModelError("Error", "找回密码的验证码已过期！");

                                }

                                User user = new User(model.UserName);
                                if (user != null)
                                {
                                    ViewBag.Email = user.Email;
                                }
                                model.Status = 1;// 0:邮箱验证未通过1：邮箱验证通过2：已过期
                                bll.Update(model);
                                ViewBag.Msg = "Success";
                            }
                            else if (model.Status == 1)
                            {
                                model.Status = 2;
                                bll.Update(model);
                                ViewBag.Msg = "找回密码的验证码已通过邮箱验证！";
                                ModelState.AddModelError("Error", "找回密码的验证码已通过邮箱验证！");

                            }
                            else if (model.Status == 2)
                            {
                                ViewBag.Msg = "找回密码的验证码已过期！";
                                ModelState.AddModelError("Error", "找回密码的验证码已过期！");

                            }
                            else
                            {
                                ViewBag.Msg = "无效的邮箱验证码！";
                                ModelState.AddModelError("Error", "无效的邮箱验证码！");
                            }
                        }
                    }

                }
            }
            return View();
        }


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


        [HttpPost]
        public ActionResult VerifyPassword(FormCollection collection)
        {
            if (!String.IsNullOrWhiteSpace(collection["Email"]) && !String.IsNullOrWhiteSpace(collection["NewPwd"]))
            {
                string secretKey = collection["SecretKey"];
                string username = collection["Email"].Trim();
                string password = collection["NewPwd"];

                Maticsoft.BLL.SysManage.VerifyMail bll = new Maticsoft.BLL.SysManage.VerifyMail();

                Maticsoft.Model.SysManage.VerifyMail model = bll.GetModel(secretKey);
                if (model == null || !model.ValidityType.HasValue || model.ValidityType.Value != 1 ||
                    model.UserName != username)
                {
                    //非法修改密码
                    LogHelp.AddInvadeLog("Areas.SNS.Controllers-HttpPost-VerifyPassword", System.Web.HttpContext.Current.Request);
                    return HttpNotFound();
                }

                User currentUser = new User(username);
                if (String.IsNullOrWhiteSpace(password))
                {
                    ModelState.AddModelError("Error", "该用户不存在！");
                    return View();
                }
                currentUser.Password = AccountsPrincipal.EncryptPassword(Maticsoft.Common.PageValidate.InputText(password, 30));
                if (!currentUser.Update())
                {
                    ModelState.AddModelError("Error", "密码重置失败，请检查输入的信息是否正确或者联系管理员！");
                    return View();
                }
                else
                {
                    AccountsPrincipal newUser = AccountsPrincipal.ValidateLogin(username, password);
                    FormsAuthentication.SetAuthCookie(username, false);
                    Session[Globals.SESSIONKEY_USER] = currentUser;
                    Session["Style"] = currentUser.Style;
                    Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                    pointBll.AddPoints(1, currentUser.UserID, "登录操作");
                    if (Session["returnPage"] != null)
                    {
                        string returnpage = Session["returnPage"].ToString();
                        Session["returnPage"] = null;
                        return Redirect(returnpage);
                    }
                    else
                    {
                        return RedirectToAction("Posts", "Profile");
                    }
                }
            }
            return View();
        }
        /// <summary>
        /// 重新发送邮件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void SendEmail(FormCollection collection)
        {
            if (!String.IsNullOrWhiteSpace(collection["Email"]))
            {
                User user = new User(collection["Email"]);
                int type = 1;
                //string emailtype = "RegisterEmail";
                //if (!String.IsNullOrWhiteSpace(collection["Type"]) && Common.PageValidate.IsNumber(collection["Type"]))
                //{
                //    type = Common.Globals.SafeInt(collection["Type"], 0);
                //    emailtype = type == 0 ? "RegisterEmail" : "FindPwdEmail";
                //}
                if (!String.IsNullOrWhiteSpace(user.NickName))
                {
                    SendEmail(user.UserName, user.Email, type);
                    Response.ContentType = "application/text";
                    Response.Write("success");
                }
            }
        }
        #endregion

        #region 微博帐号绑定
        public ActionResult ToBind()
        {
            string pName = Request["pName"];
            if (!String.IsNullOrWhiteSpace(pName))
            {
                String url =ViewBag.BasePath+"social/qq";
                switch (pName)
                {
                    case "QZone":
                        url = ViewBag.BasePath + "social/qq";
                        break;
                    case "Sina":
                        url = ViewBag.BasePath + "social/sina";
                        break;
                    default:
                           url = ViewBag.BasePath + "social/sina";
                        break;
                }
                System.Web.HttpContext.Current.Response.Redirect(url);
            }
            return RedirectToAction("UserBind", "UserCenter");
        }

        #endregion

    }
}

