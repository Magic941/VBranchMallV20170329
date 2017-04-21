/**
* AreaRouteFilter.cs
*
* 功 能： 主路由->区域路由检测-过滤器
* 类 名： AreaRouteFilter
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/27 22:10:45   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web.Mvc;

namespace Maticsoft.Components.Filters
{
    public class AreaRouteFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //RouteData routeData = filterContext.RequestContext.RouteData;
            //RouteValueDictionary dataTokens = routeData.DataTokens;
            //if (routeData.Values.ContainsKey("area"))
            //{
            //    dataTokens.Add("area", routeData.Values["area"]);
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}