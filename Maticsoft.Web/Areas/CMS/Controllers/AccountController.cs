using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.OAuth;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class AccountController : CMSControllerBase
    {

        #region 成员变量
        private Maticsoft.Accounts.Bus.User userBusManage = new Maticsoft.Accounts.Bus.User();
        private BLL.Members.Users userManage = new BLL.Members.Users();
        private BLL.Members.UsersExp userExpManage = new BLL.Members.UsersExp();
        #endregion

        #region 登录
        public ActionResult Login()
        {
            if (CurrentUser != null) return RedirectToAction("Index", "Home", new { area = "CMS" });
            bool IsCloseLogin = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            //if (IsCloseLogin)
            //{
            //    return RedirectToAction("TurnOff", "Error");
            //}
            ViewBag.Title = "登录";
            return View();
        }

        [HttpPost]
        public ActionResult Login(ViewModel.CMS.LogOnModel model, string returnUrl)
        {
             ViewBag.Title = "登录";
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
                    return RedirectToAction("Index", "Home", new { area = "CMS" });
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AjaxLogin(string UserName, string UserPwd)
        {
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
            return RedirectToAction("Index", "Home", new { area = "CMS" });
        }
        #endregion

        #region 注册
        public ActionResult Register()
        {
            if (CurrentUser != null) RedirectToAction("Index", "Home", new { area = "CMS" });
            ViewBag.Title = "注册";
            return View();
        }
        [HttpPost]
        public ActionResult Register(ViewModel.CMS.RegisterModel model)
        {
            ViewBag.Title = "注册";
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

                    string DefaultGravatar = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
                    DefaultGravatar = string.IsNullOrEmpty(DefaultGravatar) ? "/Upload/User/Gravatar/Default.jpg" : DefaultGravatar;
                    string TargetGravatarFile = BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
                    TargetGravatarFile = string.IsNullOrEmpty(TargetGravatarFile) ? "/Upload/User/Gravatar/" : TargetGravatarFile;
                    string path = ControllerContext.HttpContext.Server.MapPath("/");
                    if (System.IO.File.Exists(path + DefaultGravatar))
                    {
                        System.IO.File.Copy(path + DefaultGravatar, path + TargetGravatarFile + userid + ".jpg", true);
                    }
                    #endregion
                    return RedirectToAction("Index", "Home", new { area = "CMS" });
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

        public ActionResult CheckUserState()
        {
            if (currentUser != null)
            {
                return Content("Yes");
            }
            return Content("No");

        }

        public ActionResult GetCurrentUser()
        {
            if (currentUser == null)
            {
                return Content("No");
            }
            else
            {
                string name = String.IsNullOrWhiteSpace(currentUser.NickName) ? currentUser.UserName : currentUser.NickName;
                return Content(name);
            }
        }
    }
}
