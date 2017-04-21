using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using Maticsoft.BLL.Shop.Supplier;

namespace Maticsoft.Web.Areas.Supplier.Controllers
{
    public class AccountController : Web.Controllers.ControllerBase
    {
        private readonly BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
        private readonly BLL.Pay.RechargeRequest rechargeBll = new BLL.Pay.RechargeRequest();
        private readonly BLL.Pay.BalanceDrawRequest balanDrawBll = new BLL.Pay.BalanceDrawRequest();
        private readonly BLL.Pay.BalanceDetails balanDetaBll = new BLL.Pay.BalanceDetails();
        Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new SupplierInfo();

        #region 固定当前区域的基础路径
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            #region 固定当前区域的基础路径
            ViewBag.BasePath = "/SP/";
            #endregion
        } 
        #endregion

        #region 商家登录
        public ActionResult Login()
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.Shop);
            ViewBag.WebName = webSiteSet.WebName;
            bool IsCloseLogin = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            if (IsCloseLogin)
            {
                return RedirectToAction("TurnOff", "Error");
            }
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType == "SP")
            {
                return Redirect(ViewBag.BasePath + "Home/Index");
            }
            #region SEO 优化设置
            //IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            //ViewBag.Title = "商家登录" + pageSetting.Title;
            //ViewBag.Keywords = pageSetting.Keywords;
            //ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult Login(Maticsoft.ViewModel.Shop.LogOnModel model, string CheckCode)
        {
            #region 校验码
            if (String.IsNullOrWhiteSpace(CheckCode))
            {
                ModelState.AddModelError("Message", "请输入验证码!");
                return View(model);
            }
            if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
            {
                if (Session["CheckCode"].ToString().ToLower() != CheckCode.ToLower())
                {
                    ModelState.AddModelError("Message", "验证码错误!");
                    Session["CheckCode"] = null;
                    return View(model);
                }
                else
                {
                    Session["CheckCode"] = null;
                }
            }
            else
            {
                return View(model);
            }
            #endregion

            if (!ModelState.IsValid)
            {
                return View(model);
            }
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
            if (currentUser.UserType != "SP")
            {
                ModelState.AddModelError("Message", "您不是商家不能从该入口登录！");
                return View(model);
            }
            Model.Shop.Supplier.SupplierInfo suppmodel = new BLL.Shop.Supplier.SupplierInfo().GetModel(Globals.SafeInt(currentUser.DepartmentID, -1));
            if (suppmodel == null)
            {
                ModelState.AddModelError("Message", "您不是商家不能从该入口登录！");
                return View(model);
            }

            //0未审核  1正常  2冻结   3删除
            switch (suppmodel.Status)
            {
                case 0:
                    ModelState.AddModelError("Message", "您的商家还未通过审核, 请您耐心等待！");
                    return View(model);
                case 2:
                    ModelState.AddModelError("Message", "您的商家已被冻结，请联系管理员！");
                    return View(model);
                case 3:
                    ModelState.AddModelError("Message", "您的商家已被删除，请联系管理员！");
                    return View(model);
            }

            HttpContext.User = userPrincipal;
            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            Session[Maticsoft.Common.Globals.SESSIONKEY_SUPPLIER] = currentUser;
            //登录成功加积分
            // Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
            // int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");
            return Redirect(ViewBag.BasePath + "Home/Index");
        }
        public ActionResult Logout()
        {
            if (Session[Globals.SESSIONKEY_SUPPLIER] != null)
            {
                User currentUser = (User)Session[Globals.SESSIONKEY_SUPPLIER];
                LogHelp.AddUserLog(currentUser.UserName, currentUser.UserType, "退出系统");
                #region 更新最新的登录时间
                Maticsoft.BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
                Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();
                uModel = uBll.GetUsersExpModel(currentUser.UserID);
                if (uModel != null)
                {
                    uModel.LastAccessIP = Request.UserHostAddress;
                    uModel.LastLoginTime = DateTime.Now;
                    uBll.UpdateUsersExp(uModel);
                }
                #endregion
            }
            FormsAuthentication.SignOut();
            Session.Remove(Globals.SESSIONKEY_SUPPLIER);
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region 商家-结算中心
        /// <summary>
        /// 余额 
        /// </summary>
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.Supplier)]
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
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.Supplier)]
        public PartialViewResult BalanceDetail(int pageIndex = 1, string viewName = "BalanceDetail")
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
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.Supplier)]
        public PartialViewResult RechargeDetail(int pageIndex = 1, string viewName = "RechargeDetail")
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
        ///  结算列表
        /// </summary>
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.Supplier)]
        public PartialViewResult DrawDetail(int pageIndex = 1, string viewName = "DrawDetail")
        {
            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = balanDrawBll.GetRecordCount(" RequestType=2 AND UserId =" + CurrentUser.UserID);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.Model.Pay.BalanceDrawRequest> list = balanDrawBll.GetListByPage(" RequestType=2 AND UserId= " + CurrentUser.UserID, startIndex, endIndex);
            PagedList<Maticsoft.Model.Pay.BalanceDrawRequest> lists = new PagedList<Maticsoft.Model.Pay.BalanceDrawRequest>(list, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }

        #region 申请结算
        /// <summary>
        /// 申请结算
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.Supplier)]
        public ActionResult Draw(string viewName = "Draw")
        {
            ViewBag.Balance = userEXBll.GetUserBalance(CurrentUser.UserID);
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "申请结算" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;

            Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModelByUserId(currentUser.UserID);
            ViewBag.CardNo = model.AccountBank;
            #endregion
            return View(viewName);
        }

        /// <summary>
        /// 申请结算 ajax请求
        /// </summary>
        [Maticsoft.Components.Filters.TokenAuthorize(Maticsoft.Components.Filters.AccountType.Supplier)]
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
            if (amount > userEXBll.GetUserBalance(CurrentUser.UserID))//结算金额大于余额
            {
                return Content("low");//余额不足
            }
            Model.Pay.BalanceDrawRequest balanDrawModel = new Model.Pay.BalanceDrawRequest();
            balanDrawModel.Amount = amount;
            balanDrawModel.BankCard = bankcard;
            balanDrawModel.CardTypeID = typeid;
            balanDrawModel.RequestStatus = 1;
            balanDrawModel.RequestTime = DateTime.Now;
            balanDrawModel.RequestType = 2;
            balanDrawModel.TargetId = Globals.SafeInt(currentUser.DepartmentID, -1);
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
        #endregion

    }
}
