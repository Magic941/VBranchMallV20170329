/**
* SNSAreaRegistration.cs
*
* 功 能： SNS模块-区域路由注册器
* 类 名： SNSAreaRegistration
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

using System;
using System.Web.Mvc;
using Maticsoft.Common;

namespace Maticsoft.Web.Areas.SNS
{
    public class SNSAreaRegistration : AreaRegistrationBase
    {
        /// <summary>
        /// SNS页面缓存过期时间(秒)
        /// </summary>
        public new const int OutputCacheDuration = 5 * 60;

        public SNSAreaRegistration() : base(AreaRoute.SNS) { }

        /// <summary>
        /// 上传基础路径 - 未格式
        /// </summary>
        /// <example>/Upload/SNS/{0}/{1}/{2}/</example>
        private new static string PathUploadFolderBase
        {
            get
            {
                return AreaRegistrationBase.PathUploadFolderBase(AreaRoute.SNS) + "{0}/{1}/{2}/";
            }
        }

        #region SNS上传目录
        /// <summary>
        /// 小组上传图片目录
        /// </summary>
        public static string PathUploadImgGroup
        {
            get
            {
                return string.Format(PathUploadFolderBase,
                                     "Images",
                                     "Group",
                                     DateTime.Now.ToString("yyyyMMdd"));
            }
        }
        /// <summary>
        /// 小组上传图片缩略图目录
        /// </summary>
        public static string PathUploadImgGroupThumb
        {
            get
            {
                return string.Format(PathUploadFolderBase,
                                     "Images",
                                     "GroupThumbs",
                                     DateTime.Now.ToString("yyyyMMdd"));
            }
        }
        #endregion

        #region RegisterArea
        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region SNS扩展路由注册
            //SNS相册路由
            context.MapRoute(
                name: AreaName + "_Album",
                url: CurrentRoutePath + "Album/Details/{AlbumID}",
                defaults: new { controller = "Album", action = "Details", AlbumID = UrlParameter.Optional }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            context.MapRoute(
            name: AreaName + "_SearchUserGroup",
            url: CurrentRoutePath + "Search/{action}/{q}/{page}",
            defaults: new { controller = "Search", action = "User", q = UrlParameter.Optional, page = UrlParameter.Optional },
             constraints: new { action = "User|Groups" },
            namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
            );
            context.MapRoute(
            name: AreaName + "_Search",
            url: CurrentRoutePath + "Search/{action}/{sequence}/{q}/{pageIndex}",
            defaults: new { controller = "Search", action = "Albums", pageIndex = UrlParameter.Optional, sequence = "hot", q = UrlParameter.Optional },
            constraints: new { action = "Albums|Product|Topics|Photo" },
            namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
            );

            context.MapRoute(
              name: AreaName + "_AlbumEdit",
              url: CurrentRoutePath + "Profile/AlbumEdit/{AlbumID}",
              defaults: new { controller = "Profile", action = "AlbumEdit", AlbumID = UrlParameter.Optional }
              , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
              );
            context.MapRoute(
               name: AreaName + "_User",
               url: CurrentRoutePath + "User/Posts/{Uid}",
               defaults: new { controller = "User", action = "Posts", uid = UrlParameter.Optional }
               , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
               );

            context.MapRoute(
               name: AreaName + "_Video",
               url: CurrentRoutePath + "Video/{action}",
               defaults: new { controller = "Video", action = "Index", }
               , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
               );
            context.MapRoute(
              name: AreaName + "_ShopCate",
              url: CurrentRoutePath + "Product/{cname}/{cid}/{topcid}/{minprice}-{maxprice}-{sequence}-{color}",
              defaults: new { controller = "Product", action = "Index", cname = UrlParameter.Optional, topcid = UrlParameter.Optional, cid = 0, minprice = 0, maxprice = 0, sequence = "new", color = "all" }
              , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
              );
            context.MapRoute(
                name: AreaName + "_Collocation",
                url: CurrentRoutePath + "Collocation/{orderby}",
                defaults: new { controller = "Collocation", action = "Index", orderby = "popular" },
                constraints: new { orderby = "popular|new|hot" }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            #region SNS历史路由, 等待全部规范后移除
            context.MapRoute(
                 name: AreaName + "_ProductDetail_Old",
                 url: CurrentRoutePath + "Detail/{controller}/{pid}",
                 defaults: new { controller = "Product", action = "Detail", pid = 0 }
                 , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                 );
            context.MapRoute(
                        name: AreaName + "_PhotoDetail_Old",
                        url: CurrentRoutePath + "Detail/Photo/{pid}",
                        defaults: new { controller = "Photo", action = "Detail", pid = 0 }
                        , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                        );
            #endregion
            context.MapRoute(
             name: AreaName + "_ProductDetail",
             url: CurrentRoutePath + "Product/Detail/{pid}",
             defaults: new { controller = "Product", action = "Detail", pid = 0 }
             , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
             );
            context.MapRoute(
                    name: AreaName + "_PhotoList",
                    url: CurrentRoutePath + "Photo/Index/{type}/{categoryId}/{address}/{orderby}",
                    defaults: new { controller = "Photo", action = "Index", type = -1, categoryId = 0, address = "all", orderby = "hot" }
                    , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                    );
            context.MapRoute(
                        name: AreaName + "_PhotoDetail",
                        url: CurrentRoutePath + "Photo/Detail/{pid}",
                        defaults: new { controller = "Photo", action = "Detail", pid = 0 }
                        , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                        );
            context.MapRoute(
                name: AreaName + "_ShareGoods",
                url: CurrentRoutePath + "ShareGoods/{orderby}",
                defaults: new { controller = "ShareGoods", action = "Index", orderby = "popular" },
                constraints: new { orderby = "popular|new|hot" }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            context.MapRoute(
                name: AreaName + "_Star",
                url: CurrentRoutePath + "Star",
                defaults: new { controller = "Star", action = "Pioneer" }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            context.MapRoute(
              name: AreaName + "_ArticleList",
              url: CurrentRoutePath + "Article/Index/{classId}",
              defaults: new { controller = "Article", action = "Index", classId = 0 }
              , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
              );
            #endregion
            base.RegisterArea(context);
        }
        #endregion

    }
}
