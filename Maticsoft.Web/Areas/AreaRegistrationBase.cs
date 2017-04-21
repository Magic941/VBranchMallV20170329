/**
* AreaRegistrationBase.cs
*
* 功 能： 区域路由注册基类
* 类 名： AreaRegistrationBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/23 14:28:00  Ben    初版
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

namespace Maticsoft.Web.Areas
{
    public abstract class AreaRegistrationBase : AreaRegistration
    {
        #region 属性
        /// <summary> 
        /// 页面缓存过期时间(秒)
        /// </summary>
        public const int OutputCacheDuration = 5 * 60;

        /// <summary>
        /// 当前区域
        /// </summary>
        protected AreaRoute CurrentArea;

        /// <summary>
        /// 自定义区域名称
        /// </summary>
        /// <remarks>方便M/SP这种区域的注册</remarks>
        protected string CustomAreaName;

        /// <summary>
        /// 当前路由基础路径
        /// </summary>
        /// <remarks>用于子路由URL前缀</remarks>
        protected string CurrentRoutePath;

        /// <summary>
        /// 当前区域路由名称
        /// </summary>
        /// <remarks>用于子路由URL前缀</remarks>
        protected string CurrentRouteName;

        /// <summary>
        /// 是否注册区域路由
        /// </summary>
        /// <remarks>仅在M/SP这种情况才设置为True</remarks>
        protected bool IsRegisterArea;

     
        /// <summary>
        /// 推荐人ID
        /// </summary>
        public  string rusernameid
        {
            get
            {
                //if (!string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("Recommend_UserNameID", "Value")))
                //{

                //    return Maticsoft.Common.Cookies.getCookie("Recommend_UserNameID", "Value");
                //}

                return "0"; 
            }
        }

        /// <summary>
        ///  当前用户登录ID
        /// </summary>
        public string usernameid
        {
            get
            {
                //if (!string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("UserNameID", "Value")))
                //{

                //    return Maticsoft.Common.Cookies.getCookie("UserNameID", "Value");
                //}

                return "0"; 
            }
        }

        /// <summary>
        /// 上传文件基础路径
        /// </summary>
        /// <example>/Upload/Shop/</example>
        protected static string PathUploadFolderBase(AreaRoute currentArea)
        {
            return string.Format("/{0}/{1}/", MvcApplication.UploadFolder, currentArea);
        }

        /// <summary>
        /// 区域名称
        /// </summary>
        public override sealed string AreaName
        {
            get
            {
                return CurrentArea.ToString(); //Enum.GetName(typeof(AreaRoute), CurrentArea);
            }
        }
        #endregion

        /// <summary>
        /// 构造区域路由
        /// </summary>
        /// <param name="currentArea">当前区域枚举</param>
        /// <param name="customAreaName">自定义区域名称</param>
        protected AreaRegistrationBase(AreaRoute currentArea, string customAreaName = null)
        {
            AreaRegistration(currentArea, customAreaName);
        }

        /// <summary>
        /// 构造区域路由
        /// </summary>
        /// <param name="currentArea">当前区域枚举</param>
        /// <param name="customAreaName">自定义区域名称</param>
        protected void AreaRegistration(AreaRoute currentArea, string customAreaName = null)
        {
            CurrentArea = currentArea;
            CustomAreaName = customAreaName;
            if (string.IsNullOrWhiteSpace(CustomAreaName))
            {
                CurrentRouteName = string.Format("{0}_Default", AreaName);
                CurrentRoutePath = MvcApplication.MainAreaRoute == currentArea ? "" : AreaName + "/";
                if (AreaRoute.MShop == currentArea)
                {
                    CurrentRoutePath = MvcApplication.MainAreaRoute == currentArea ? "" : AreaName + "{rusername}/";
                }
                CustomAreaName = CurrentArea.ToString();
            }
            else
            {
                CurrentRouteName = string.Format("{0}_{1}_Default", AreaName, CustomAreaName);
                CurrentRoutePath = CustomAreaName + "/";
                if (AreaRoute.MShop == currentArea)
                {
                    CurrentRoutePath = CustomAreaName + "{rusername}/";
               
                }
            }
        }

        /// <summary>
        /// 注册区域路由
        /// </summary>
        /// <remarks>如子类重写此方法, 必须调用父类此方法, 以完成区域路由注册</remarks>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Namespaces.Clear();
            context.Namespaces.Add(string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName));

            #region 全区域子路由注册
            //DONE: RSSDemo全区域路由注册 - 示例 BEN ADD 2012-10-23
            context.MapRoute(
                name: string.Format("{0}_{1}_RSSDemo", AreaName, CustomAreaName),
                url: CurrentRoutePath + "RSSDemo",
                defaults: new { controller = "RSS", action = "Index", AlbumID = UrlParameter.Optional }
                , namespaces: new string[] { "Maticsoft.Web.Controllers" }
                );
            #endregion

            //如当前为主路由 - 区域注册不执行
            if (MvcApplication.MainAreaRoute != CurrentArea || IsRegisterArea)
            {
                context.MapRoute(
                    name: CurrentRouteName,
                    url: CurrentRoutePath + "{controller}/{action}/{viewname}/{id}",
                    defaults: new
                    {
                        
                
                        rusername=rusernameid,
                        controller = "Home",
                        action = "Index",
                        viewname = UrlParameter.Optional,
                        id = UrlParameter.Optional
                    },
                
                    constraints: new
                    {
                        viewname = @"^[A-Za-z_]+${0,50}", //大小写字母/下划线
                        id = @"[\d]{0,11}", //*表示数字长度11  //new { id = @"\d*" } 长度不限
                        rusername=@"\d*"
                    }
                    , namespaces: new[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                    );

                context.MapRoute(
                    name: CurrentRouteName + "_Base",
                    url: CurrentRoutePath + "{controller}/{action}/{id}",
                    defaults: new { area = AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional }
                    , namespaces: new[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                    );
            }
        }
    }
}
