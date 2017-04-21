/**
* MvcApplication.cs
*
* 功 能： Global
* 类 名： MvcApplication
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/27 12:00:33   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Maticsoft.Components;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewEngine;
using Maticsoft.BLL.SysManage;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Maticsoft.BLL.Products.Lucene;
using System.Text;
using Maticsoft.BLL.Shop.Coupon;
using System.Web.Http;
using System.IO;


namespace Maticsoft.Web
{
    #region IApplicationOption

    public class ApplicationOption : IApplicationOption
    {
        private static readonly WebSiteSet WebSiteSet = new WebSiteSet(ApplicationKeyType.System);

        #region IApplicationOption 成员

        public string AuthorizeCode
        {
            get
            {
                return ConfigSystem.GetValueByCache("AuthorizeCode");
            }
        }


        public string PageFootJs
        {
            get { return WebSiteSet.PageFootJs; }
        }

        public string SiteName
        {
            get { return WebSiteSet.WebName; }
        }

        public string ThemeName
        {
            get
            {
                return ConfigSystem.GetValueByCache("ThemeCurrent");
            }
        }

        public string WebPowerBy
        {
            get { return WebSiteSet.WebPowerBy; }
        }

        public string WebRecord
        {
            get { return WebSiteSet.WebRecord; }
        }

        #endregion
    }

    #endregion

    #region 子区域模版引擎

    public class SubAreaViewEngine : ISubAreaViewEngine
    {
        //TODO: 使用KeyValuePar将ky改造成ko对象集合 BEN ADD 20131122
        public SubAreaViewEngine()
        {
            _subEngineMap.Add(AreaRoute.COM,
              (context, baseLocationFormat, themeName) => new string[]
              {
                  string.Format(baseLocationFormat, "Default"),
                  string.Format(baseLocationFormat, "Default").Replace("{1}","Shared")
              });
            _subEngineMap.Add(AreaRoute.MShop,
                (context, baseLocationFormat, themeName) => new string[]
                {
                    string.Format(baseLocationFormat, themeName),
                    string.Format(baseLocationFormat, themeName).Replace("{1}","Shared")
                });

            _subEngineMap.Add(AreaRoute.MPage,
                (context, baseLocationFormat, themeName) => new string[]
                {
                    string.Format(baseLocationFormat, themeName),
                    string.Format(baseLocationFormat, themeName).Replace("{1}","Shared")
                });
            _subEngineMap.Add(AreaRoute.Supplier,
           (context, baseLocationFormat, themeName) => new string[]
           {
               string.Format(baseLocationFormat, themeName),
               string.Format(baseLocationFormat, themeName).Replace("{1}","Shared")
           });
        }

        #region ISubAreaViewEngine 成员

        private Dictionary<AreaRoute, Func<ControllerContext, string, string, string[]>> _subEngineMap =
            new Dictionary<AreaRoute, Func<ControllerContext, string, string, string[]>>();
        public Dictionary<AreaRoute, Func<ControllerContext, string, string, string[]>> SubEngineMap
        {
            get { return _subEngineMap; }
            set { _subEngineMap = value; }
        }

        public string GetTagString(AreaRoute areaRoute, ControllerContext context)
        {
            if (areaRoute != AreaRoute.MShop && areaRoute != AreaRoute.MPage && areaRoute != AreaRoute.Supplier && areaRoute != AreaRoute.MShop) return string.Empty;
            bool UseCustomTheme = context.RouteData.DataTokens.ContainsKey("Tag");

            switch (areaRoute)
            {
                case AreaRoute.MPageSP:
                    //Check 是否使用自定义引擎
                    if (!UseCustomTheme) return string.Empty;
                    //TODO: 目前锁定 /m/p/d/{productId} 进行联调测试 BEN ADD 20131123
                    string openId = context.RouteData.Values.ContainsKey("productId")
                        ? context.RouteData.Values["productId"].ToString()
                        : string.Empty;

                    //无OpenId 使用默认引擎
                    if (string.IsNullOrWhiteSpace(openId)) return string.Empty;
                    //OpenId未设置模版的, 使用默认模版
                    return OpenIdThemes.ContainsKey(openId) ? OpenIdThemes[openId] : MvcApplication.ThemeName;
                case AreaRoute.MPage:
                    string name = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("MPage_Theme");
                    return string.IsNullOrWhiteSpace(name) ? "Wap21" : name;
                case AreaRoute.Supplier:
                    string suptheme = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Supplier_Theme");
                    return string.IsNullOrWhiteSpace(suptheme) ? "M1" : suptheme;
                case AreaRoute.MShop:
                    string shoptheme = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("MShop_Theme");
                    return string.IsNullOrWhiteSpace(shoptheme) ? "M1" : shoptheme;
                default:
                    return MvcApplication.ThemeName;
            }
        }

        #endregion

        /// <summary>
        /// TODO: 微信OpenId 获取
        /// </summary>
        Dictionary<string, string> OpenIdThemes = new Dictionary<string, string>();

    }

    public class WeChatTheme
    {
        public string OpenId { set; get; }
        public string ThemeName { set; get; }
        public int targetId { get; set; }
        public string UserType { get; set; }
    }
    #endregion

    public class MvcApplication : Maticsoft.Components.MvcApplication
    {
        #region 构造
        /// <summary>
        /// 静态构造
        /// </summary>
        static MvcApplication()
        {
            ApplicationOption = new ApplicationOption();
        }
        /// <summary>
        /// 动态构造
        /// </summary>
        public MvcApplication()
        {
            SubAreaViewEngine = new SubAreaViewEngine();
        }
        #endregion

        #region 子区域模版引擎

        /// <summary>
        /// 子模版引擎
        /// </summary>
        protected static ISubAreaViewEngine SubAreaViewEngine;

        #endregion

        #region 注册忽略路由
        /// <summary>
        /// 注册忽略路由
        /// </summary>
        /// <remarks>
        /// 最高优先级
        /// </remarks>
        public override void RegisterIgnoreRoutes(RouteCollection routes)
        {
            //WEB API
           // GlobalConfiguration.Configure(WebApiConfig.Register);

            routes.IgnoreRoute("pay/{*pathInfo}");
            routes.IgnoreRoute("tools/{*pathInfo}");
            base.RegisterIgnoreRoutes(routes);
        }
        #endregion

        protected override void ApplicationStart()
        {
            //Maticsoft.Services.ErrorLogTxt.errorLogFolder =
            var errFolder = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ErrorFolder");
            if (string.IsNullOrEmpty(errFolder))
            {
                // var curDir = Directory.GetCurrentDirectory();
                var curDir2 = System.AppDomain.CurrentDomain.BaseDirectory;
                var aryCur = curDir2.Split(new char[] { '\\' });
                Maticsoft.Services.ErrorLogTxt.errorLogFolder = "c://" + aryCur[aryCur.Length - 2] +"//";
            }
            else {
                Maticsoft.Services.ErrorLogTxt.errorLogFolder = errFolder;            
            }

            GlobalFilters.Filters.Add(new Maticsoft.Web.MyMVCExceptionFilter());
            //商品索引同步服务
            ProductIndexManagerLocal.productIndex.StartNewThread();
            //CouponQueue.queue.StartThreads();
            
            
            #region 获取程序集版本号
            Version assemblyVersion = AssemblyVersion;
            Version = assemblyVersion.Major + "." + assemblyVersion.Minor +
                      (assemblyVersion.Build > 0 ? "." + assemblyVersion.Build : string.Empty);
            #endregion

            #region 获取产品信息
            ProductInfo = AssemblyProduct;
            #endregion

            #region 注册模版引擎
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeViewEngine(ThemeName, SubAreaViewEngine));
            #endregion

            //不如我写的时间日志好
            //OrderAutoAction.GetInstance();
        }

        protected override void Application_BeginRequest(object sender, EventArgs e)
        {            
            
        }
        public bool IsNumeric(string str)
        {
            string pattern = "^[0-9]*$";
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(pattern);
            return rx.IsMatch(str);
        }
       
        #region 获取产品信息
        public static Version AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
        public static string AssemblyDescription
        {
            get
            {
                var descriptionAttribute = System.Reflection.Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(System.Reflection.AssemblyDescriptionAttribute), false)
                    .OfType<System.Reflection.AssemblyDescriptionAttribute>()
                    .FirstOrDefault();
                if (descriptionAttribute != null)
                    return descriptionAttribute.Description;
                return string.Empty;
            }
        }
        public static string AssemblyProduct
        {
            get
            {
                // 获取此程序集上的所有 Product 属性
                object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false);
                // 如果 Product 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return string.Empty;
                // 如果有 Product 属性，则返回该属性的值
                return ((System.Reflection.AssemblyProductAttribute)attributes[0]).Product;
            }
        }
        #endregion

        protected override string MainArea
        {
            get
            {
                return ConfigSystem.GetValue("MainArea");
            }
        }
    }

    /// <summary>
    /// 自定义ajax异常处理机制，用来返回信息
    /// </summary>
    public class MyMVCExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            if (!filterContext.ExceptionHandled && filterContext.Result is JsonResult)
            {
                //ajax请求和页面是有区别的
                filterContext.Result = new JsonResult()
                {
                    Data = new { IsSuccess = false, Message = filterContext.Exception.Message }                  
                };

                filterContext.ExceptionHandled = true;
               
                filterContext.HttpContext.Response.StatusCode = 200;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Server.ClearError();
                filterContext.HttpContext.Response.Clear();
                return;
            }
        }

    }

}