/**
* TokenAuthorizeAttribute.cs
*
* 功 能： 权限检测Attribute
* 类 名： TokenAuthorize
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/15 11:21:57  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Components.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class TokenAuthorizeAttribute : AuthorizeAttribute  //, IActionFilter, IResultFilter, IExceptionFilter
    {
        private const string LockKey = "LOCKKEY";
        public const int STATUSCODE_UNLOGON = 801;          //未登录
        public const int STATUSCODE_UNAUTHORIZED = 803;     //未授权/权限不足

        public const int STATUSCODE_ERROR_INTERNAL = 805;   //内部执行错误 - 暂未使用
        public const int STATUSCODE_ERROR_PARAM = 806;      //调用参数错误

        #region 构造
        /// <summary>
        /// 仅进行登录 Check
        /// </summary>
        public TokenAuthorizeAttribute()
        {
            RequiredType = AccountType.None;
            PermissionId = -1;
        }

#if false
        #region 备用方案
        //private const string KeyCurrentActionResult = "CurrentActionResult";
        //private const string KeyRequiredType = "RequiredType";
        //private const string KeyPermissionId = "PermissionId";
        public TokenAuthorizeAttribute(AccountType userType)
        {
            HttpContext.Current.Items[KeyRequiredType] = userType;
        }
        public TokenAuthorizeAttribute(int permissionId)
        {
            HttpContext.Current.Items[KeyPermissionId] = permissionId;
        }
        public TokenAuthorizeAttribute(AccountType userType, int permissionId)
        {
            HttpContext.Current.Items[KeyRequiredType] = userType;
            HttpContext.Current.Items[KeyPermissionId] = permissionId;
        } 
        #endregion
#else
        /// <summary>
        /// 进行登录和指定用户类型 Check
        /// </summary>
        /// <param name="userType">用户类型</param>
        public TokenAuthorizeAttribute(AccountType userType)
        {
            RequiredType = userType;
            PermissionId = -1;
        }
        /// <summary>
        /// 进行登录和指定权限 Check
        /// </summary>
        /// <param name="permissionId">指定权限码</param>
        public TokenAuthorizeAttribute(int permissionId)
        {
            RequiredType = AccountType.None;
            PermissionId = permissionId;
        }
        /// <summary>
        /// 进行登录/指定用户类型/指定权限码 Check
        /// </summary>
        /// <param name="userType">用户类型</param>
        /// <param name="permissionId">指定权限码</param>
        public TokenAuthorizeAttribute(AccountType userType, int permissionId)
        {
            RequiredType = userType;
            PermissionId = permissionId;
        }
#endif
        #endregion

        #region 属性

        public AccountType RequiredType { get; set; }
        public int PermissionId { get; set; }

        #region 备用方案
        //public AccountType RequiredType
        //{
        //    get
        //    {
        //        return HttpContext.Current.Items.Contains(KeyRequiredType)
        //                                    ? (AccountType)HttpContext.Current.Items[KeyRequiredType]
        //                                    : AccountType.None;
        //    }
        //}
        //public int PermissionId
        //{
        //    get
        //    {
        //        return HttpContext.Current.Items.Contains(KeyPermissionId)
        //                               ? (int)HttpContext.Current.Items[KeyPermissionId]
        //                               : -1;
        //    }
        //}
        //public CurrentActionResult CurrentActionResult
        //{
        //    get
        //    {
        //        return HttpContext.Current.Items.Contains(KeyCurrentActionResult)
        //                               ? (CurrentActionResult)HttpContext.Current.Items[KeyCurrentActionResult]
        //                               : null;
        //    }
        //} 
        #endregion
        #endregion

        #region 登录/权限效验

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            AccountType requiredType = this.RequiredType;
            int permissionId = this.PermissionId;

            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            //检测是否登录
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.StatusCode = STATUSCODE_UNLOGON;
                //httpContext.Response.SubStatusCode = 8;   //IIS7 子状态码, 为兼容IIS6 禁止设置
                return false;
            }

            //枚举值正误检测
            if (!Enum.IsDefined(typeof(AccountType), requiredType))
            {
                httpContext.Response.StatusCode = STATUSCODE_ERROR_PARAM;
                return false;
            }

            #region 防止跨域 - 暂不启用
            //Uri urlReferrer = httpContext.Request.UrlReferrer;  //获取来路
            //if (urlReferrer == null)
            //{
            //    httpContext.Response.StatusCode = STATUSCODE_UNLOGON;
            //    return false;
            //}
            //else
            //{
            //    Uri currentUrl = httpContext.Request.Url;  //当前请求的URL
            //    if (urlReferrer.Authority != currentUrl.Authority)
            //    {
            //        httpContext.Response.StatusCode = STATUSCODE_UNLOGON;
            //        return false;
            //    }
            //}
            #endregion

            //加载权限信息
            AccountsPrincipal userPrincipal;
            try
            {
                userPrincipal = new AccountsPrincipal(httpContext.User.Identity.Name);
            }
            catch (System.Security.Principal.IdentityNotMappedException)
            {
                //用户在DB中不存在 退出
                System.Web.Security.FormsAuthentication.SignOut();
                if (httpContext.Session != null)
                {
                    httpContext.Session.Remove(Globals.SESSIONKEY_USER);
                    httpContext.Session.Clear();
                    httpContext.Session.Abandon();
                }
                httpContext.Response.StatusCode = STATUSCODE_UNLOGON;
                return false;
            }

            #region 加载用户信息
            User currentUser = null;
            if (httpContext.Session[Globals.SESSIONKEY_USER] == null)
            {
                currentUser = new User(userPrincipal);
                httpContext.Session[Globals.SESSIONKEY_USER] = currentUser;
            }
            else
            {
                currentUser = (User)httpContext.Session[Globals.SESSIONKEY_USER];
            }
            #endregion

            #region 验证用户类型
            if (requiredType != AccountType.None) //允许不判断用户类型的情况 BEN ADD 2012-10-15
            {
                //用户权限不足
                switch (currentUser.UserType)
                {
                    case "UU":
                        if (requiredType != AccountType.User)
                        {
                            httpContext.Response.StatusCode = STATUSCODE_UNAUTHORIZED;
                            return false;
                        }
                        break;
                    case "AA":
                        if (requiredType != AccountType.Admin)
                        {
                            httpContext.Response.StatusCode = STATUSCODE_UNAUTHORIZED;
                            return false;
                        }
                        break;
                    case "EE":
                        if (requiredType != AccountType.Enterprise)
                        {
                            httpContext.Response.StatusCode = STATUSCODE_UNAUTHORIZED;
                            return false;
                        }
                        break;
                    case "SP":
                        if (requiredType != AccountType.Supplier)
                        {
                            httpContext.Response.StatusCode = STATUSCODE_UNAUTHORIZED;
                            return false;
                        }
                        break;
                    case "AG":
                        if (requiredType != AccountType.Agent)
                        {
                            httpContext.Response.StatusCode = STATUSCODE_UNAUTHORIZED;
                            return false;
                        }
                        break;
                    default:
                        httpContext.Response.StatusCode = STATUSCODE_ERROR_PARAM;
                        return false;
                }
            }
            #endregion

            #region 验证权限
            if ((permissionId != -1) && (!userPrincipal.HasPermissionID(permissionId)))
            {
                //httpContext.Response.Clear();
                //httpContext.Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipNoPermission + "');history.back();</script>");
                //httpContext.Response.End();
                //TODO: 改为页面提示
                httpContext.Response.StatusCode = STATUSCODE_UNAUTHORIZED;
                return false;
            }
            #endregion

            //终止ASP.NET的验证机制
            return true;// base.AuthorizeCore(httpContext);
        }

        internal bool PerformAuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return this.AuthorizeCore(httpContext);
        }

        #region 页面跳转
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            switch (filterContext.HttpContext.Response.StatusCode)
            {
                //未登录
                case STATUSCODE_UNLOGON:
                    filterContext.Result = RedirectToLogon(filterContext);
                    break;
                //权限不足
                case STATUSCODE_UNAUTHORIZED:
                    filterContext.Result = RedirectToUnauthorized(filterContext.HttpContext);
                    break;
                case STATUSCODE_ERROR_PARAM:
                    filterContext.Result = RedirectToErrorParam(filterContext.HttpContext);
                    break;
                default:
                    //父类处理
                    base.HandleUnauthorizedRequest(filterContext);
                    break;
            }
        }

        public virtual ActionResult RedirectToLogon(AuthorizationContext filterContext)
        {
            if (filterContext == null ||
                filterContext.HttpContext == null ||
                filterContext.HttpContext.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(STATUSCODE_UNLOGON);
            }
            string basePath = MvcApplication.GetCurrentRoutePath(filterContext.RouteData.DataTokens["area"]);
            if (filterContext.HttpContext.Request.Url != null)
            {
                return new RedirectResult(string.Concat(basePath + "Account/Login?ReturnUrl=",
                    filterContext.HttpContext.Server.UrlEncode(
                        filterContext.HttpContext.Request.Url.PathAndQuery))
                    );
            }
            return new RedirectResult(string.Concat(basePath + "Account/Login"));
        }
        public virtual ActionResult RedirectToUnauthorized(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(STATUSCODE_UNAUTHORIZED);
            }
            return new HttpUnauthorizedResult("您的权限不足, 无法访问此页面!");
        }
        public virtual ActionResult RedirectToErrorParam(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(STATUSCODE_ERROR_PARAM);
            }
            return new HttpNotFoundResult("TokenAuthorizeAttribute-IllegalUserType");
        }
        #endregion

        #region 缓存验证
        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }
        #endregion

        //ValidateInputAttribute 注入过滤器

        #region IAuthorizationFilter 成员

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //object[] obj = filterContext.ActionDescriptor.GetCustomAttributes(typeof(TokenAuthorizeAttribute), true);
            //object[] obj1 = filterContext.ActionDescriptor.GetCustomAttributes(true);

            lock (LockKey)  //线程安全
            {
                #region 优先执行ASP.NET的验证传递
                base.OnAuthorization(filterContext);
                #endregion

                #region 备用示例信息
                //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //string actionName = filterContext.ActionDescriptor.ActionName;
                //string roles = GetRoles.GetActionRoles(actionName, controllerName);
                //if (!string.IsNullOrWhiteSpace(roles))
                //{
                //    this.Roles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                //} 
                #endregion

                ////获取验证结果并输出
                //if (filterContext.HttpContext.Response.StatusCode == STATUSCODE_UNAUTHORIZED)
                //{
                //    filterContext.HttpContext.Response.StatusCode = STATUSCODE_UNAUTHORIZED;
                //    filterContext.Result = RedirectToLogin(filterContext.HttpContext);
                //}
            }
        }

        #endregion
        #endregion

        #region 停止使用的内容

        #region IActionFilter 成员

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Action({0})执行后 filterContext.ActionDescriptor.ActionName
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Action({0})执行前 filterContext.ActionDescriptor.ActionName
        }

        #endregion

        #region IResultFilter 成员

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Result执行前
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Result执行后
        }

        #endregion

        #region IExceptionFilter 成员

        public void OnException(ExceptionContext filterContext)
        {
            //异常
        }

        #endregion
        #endregion
    }

    /// <summary>
    /// 帐号类型
    /// </summary>
    [Flags]
    public enum AccountType
    {
        /// <summary>
        /// 默认空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 网站管理员
        /// </summary>
        Admin = 1,
        /// <summary>
        /// 普通用户(会员)
        /// </summary>
        User = 2,
        /// <summary>
        /// 企业用户
        /// </summary>
        Enterprise = 3,
        /// <summary>
        /// 代理商用户
        /// </summary>
        Agent = 4,
        /// <summary>
        /// 供应商用户
        /// </summary>
        Supplier = 5,
    }
}