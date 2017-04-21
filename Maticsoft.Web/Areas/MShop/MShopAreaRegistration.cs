/**
* MobileAreaRegistration.cs
*
* 功 能： Mobile模块-区域路由注册器
* 类 名： MobileAreaRegistration
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/07/01 19:37:01  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MShop
{
    public class MShopAreaRegistration : AreaRegistrationBase
    {
        public MShopAreaRegistration() : base(AreaRoute.MShop) { }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region 注册Mobile区域扩展路由

            #region 商家店铺路由 - 附加子模版引擎标识

            //context.MapRoute(
            //    name: AreaName + "_" + CustomAreaName + "_Shop", // 路由名称
            //    url: CurrentRoutePath + "ws/{id}"
            //    , namespaces: new[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
            //    ).DataTokens.Add("Tag", CurrentRoutePath + "ws/{id}");

            #endregion

            #region 商品详细路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_indexshare",
                url: CurrentRoutePath + "s",
                defaults:
                    new
                    {
                        
                        controller = "home",
                        action = "ShareIndex"
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 用户操作中心

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_indexservice",
                url: CurrentRoutePath + "service",
                defaults:
                    new
                    {
                       
                        controller = "home",
                        action = "IndexService"
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商品列表路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductList",
                url: CurrentRoutePath + "p/{cid}/{brandid}/{attrvalues}/{mod}/{price}",
                defaults:
                    new
                    {
                  
                        controller = "Product",
                        action = "Index",
                        cid = 0,
                        brandid = 0,
                        attrvalues = "0",
                        mod = "default",
                        price = ""
                        //viewname = "Index",
                        //ajaxViewName = "_ProductList"
                    },
                constraints: new
                {
                    mod = @"default|hot|new|price|pricedesc", //大小写字母/数字/下划线
                    cid = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商品列表路由(推荐商品 分享)

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductIndexNew",
                url: CurrentRoutePath + "tc/{gtype}/{GoodTypeID}/{attrvalues}/{mod}/{price}",
                defaults:
                    new
                    {

                        controller = "Product",
                        action = "ProductIndexNew",
                        gtype = 0,
                        GoodTypeID = 0,
                        attrvalues = "0",
                        mod = "default",
                        price = ""
                        //viewname = "Index",
                        //ajaxViewName = "_ProductList"
                    },
                constraints: new
                {
                    mod = @"default|hot|new|price|pricedesc", //大小写字母/数字/下划线
                    cid = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商品全品类的列表路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductListAll",
                url: CurrentRoutePath + "h/{cid}/{brandid}/{attrvalues}/{mod}/{price}/{activetype}",
                defaults:
                    new
                    {

                        controller = "Product",
                        action = "IndexOld",
                        cid = 0,
                        brandid = 0,
                        attrvalues = "0",
                        mod = "default",
                        price = "",
                        activetype=0
                        //viewname = "Index",
                        //ajaxViewName = "_ProductList"
                    },
                constraints: new
                {
                    mod = @"default|hot|new|price|pricedesc", //大小写字母/数字/下划线
                    cid = @"[\d]{0,11}", //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    activetype = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion


            #region 商品列表路由活动页面

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductListActive",
                url: CurrentRoutePath + "g/{cid}/{brandid}/{attrvalues}/{mod}/{price}",
                defaults:
                    new
                    {

                        controller = "Product",
                        action = "ActiveProductIndex",
                        cid = 0,
                        brandid = 0,
                        attrvalues = "0",
                        mod = "default",
                        price = ""
                        //viewname = "Index",
                        //ajaxViewName = "_ProductList"
                    },
                constraints: new
                {
                    mod = @"default|hot|new|price|pricedesc", //大小写字母/数字/下划线
                    cid = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商品搜索列表路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductSearch",
                url: CurrentRoutePath + "s/{cid}/{brandid}/{mod}/{price}/{keyword}",
                defaults:
                    new
                    {
                        controller = "Search",
                        action = "IndexOld",
                        cid = 0,
                        brandid = 0,
                        mod = "default",
                        price = "0-0",
                        keyword = ""
                        //,
                        //viewname = "Index",
                        //ajaxViewName = "_ProductList"
                    },
                constraints: new
                {
                    mod = @"default|hot|new|price|pricedesc", //大小写字母/数字/下划线
                    cid = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductSearchFilter",
                url: CurrentRoutePath + "s/f/{cid}/{keyword}",
                defaults:
                    new
                    {
                        controller = "Search",
                        action = "Filter",
                        cid = 0,
                        keyword = "",

                    },
                constraints: new
                {
                    cid = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商品分类路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_CategoryList",
                url: CurrentRoutePath + "p/c/{parentId}",
                defaults:
                    new
                    {
                        controller = "Product",
                        action = "CategoryList",
                        parentId = 0,

                    },
                constraints: new
                {
                    parentId = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商品全部分类路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_CategoryListAll",
                url: CurrentRoutePath + "p/t/{parentId}",
                defaults:
                    new
                    {
                        controller = "Product",
                        action = "CategoryListOld",
                        parentId = 0,

                    },
                constraints: new
                {
                    parentId = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion


            #region 登陆路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_Login",
                url: CurrentRoutePath + "a/l",
                defaults:
                    new
                    {
                        controller = "Account",
                        action = "Login"
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 注册分类路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_Register",
                url: CurrentRoutePath + "a/r",
                defaults:
                    new
                    {
                        controller = "Account",
                        action = "Register"
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 个人中心首页路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_UserCenter",
                url: CurrentRoutePath + "u/{action}/{id}",
                defaults:
                    new
                    {
                   
                        controller = "UserCenter",
                        action = "Index",
                        id = UrlParameter.Optional
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 申请健康服务店

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ApplyShop",
                url: CurrentRoutePath + "u/{action}",
                defaults:
                    new
                    {
                        controller = "UserCenter",
                        action = "ApplyShop"
                      
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商品详细路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductDetail",
                url: CurrentRoutePath + "p/d/{productId}/{ActiveID}/{ActiveType}",
                defaults:
                    new
                    {
                        controller = "Product",
                        action = "DetailNew",
                        productId = 0,
                        ActiveID= 0,
                        ActiveType=0
                    },
                constraints: new
                {
                    productId = @"[\d]{0,11}", //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    AcivieID = @"[\d]{0,11}", //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    ActiveType = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                ).DataTokens.Add("Tag", CurrentRoutePath + "p/d/{productId}/{ActiveID}/{ActiveType}");

            #endregion

            #region 商品详细路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductDetail2",
                url: CurrentRoutePath + "Product/Detail/{productId}",
                defaults:
                    new
                    {
                        controller = "Product",
                        action = "Detail",
                        productId = 0,
                    },
                constraints: new
                {
                    productId = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                ).DataTokens.Add("Tag", CurrentRoutePath + "p/d/{productId}");

            #endregion

            #region 商品详细路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ProductDetailAttr",
                url: CurrentRoutePath + "Product/Attr/{productId}",
                defaults:
                    new
                    {
                        controller = "Product",
                        action = "Consults",
                        productId = 0,
                    },
                constraints: new
                {
                    productId = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                ).DataTokens.Add("Tag", CurrentRoutePath + "p/Attr/{productId}");

            #endregion

            #region 购物车流程

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_ShoppingCart",
                url: CurrentRoutePath + "sc/ci",
                defaults:
                    new
                    {
                        controller = "ShoppingCart",
                        action = "CartInfo"

                    }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 订单详情

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_OderInfo",
                url: CurrentRoutePath + "o/oi/{orderId}",
                defaults:
                    new
                    {
                        controller = "Order",
                        action = "OrderInfo",
                        orderId = -1
                    }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 订单详情

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_OderInfonew",
                url: CurrentRoutePath + "o/{action}",
                defaults:
                    new
                    {
                        controller = "Order",
                        action = "index",
                        orderId = -1
                    }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 考勤路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_Attendance",
                url: CurrentRoutePath + "w/a/{userId}",
                defaults:
                    new
                    {
                        controller = "Home",
                        action = "Attendance",
                        userId = 0
                    }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 店铺首页路由
            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_Store_Index",
                url: CurrentRoutePath + "Store/{suppId}/{cid}/{mod}/{price}/{ky}",
                defaults:
                    new
                    {
                        controller = "Store",
                        action = "Index",
                        suppId = 0,
                        cid = 0,
                        mod = "hot",
                        price = "0-0",
                        ky = ""
                    },
                constraints: new
                {
                    suppId = @"[\d]{0,11}",//*表示数字长度11  //new { id = @"\d*" } 长度不限
                    mod = @"hot|new|pricedesc", //大小写字母/数字/下划线
                    cid = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            #endregion

            #region 商家商品列表路由
            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_StoreProductList",
                url: CurrentRoutePath + "Store/list/{suppId}/{cid}/{mod}/{pvn}/{ky}",//
                defaults:
                    new
                    {
                        controller = "Store",
                        action = "List",
                        suppId = 0,
                        cid = 0, 
                        mod = "hot",
                        pvn = "0",
                        ky = ""
                    },
                constraints: new
                {
                    suppId = @"[\d]{0,11}",
                     pvn = @"0|1",
                    mod = @"hot|new|pricedesc", //大小写字母/数字/下划线
                    cid = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            #endregion

            #region 积分兑换路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_Exchanges",
                url: CurrentRoutePath + "u/Exchanges",
                defaults:
                    new
                    {
                        controller = "UserCenter",
                        action = "Exchanges"
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 我的优惠券路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_MyCoupon",
                url: CurrentRoutePath + "u/MyCoupon",
                defaults:
                    new
                    {
                        controller = "UserCenter",
                        action = "MyCoupon"
                    },

                namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion

            #region 商家介绍路由
            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_StoreIntroduction",
                url: CurrentRoutePath + "Store/Intr/{suppId}",//
                defaults:
                    new
                    {
                        controller = "Store",
                        action = "Introduction",
                        suppId = 0
                    },
                constraints: new
                {
                    suppId = @"[\d]{0,11}"
                }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );
            #endregion

            #region 考勤路由

            context.MapRoute(
                name: AreaName + "_" + CustomAreaName + "_home",
                url: CurrentRoutePath.Replace("{rusername}","") ,
                defaults:
                    new
                    {
                        rusername = rusernameid,
                        controller = "Home",
                        action = "Index",
                    }
                , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                );

            #endregion
            #endregion

            #region 注册Mobile区域子路由 覆盖父类注册方法

            //如当前为主路由 - 区域注册不执行
            if (MvcApplication.MainAreaRoute != CurrentArea || IsRegisterArea)
            {
                context.MapRoute(
                    name: CurrentRouteName + "Base",
                    url: CurrentRoutePath + "{controller}/{action}/{id}",
                    defaults:
                        new
                        {
                         
                            rusername = rusernameid,
                            controller = "Home",
                            action = "Index",
                            id = UrlParameter.Optional
                        }
                    ,
                    constraints: new
                    {
                        id = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    }
                    , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                    );

                context.MapRoute(
                    name: CurrentRouteName + CustomAreaName,
                    url: CurrentRoutePath + "{controller}/{action}/{viewname}/{id}",
                    defaults:
                        new
                        {
                            rusername = rusernameid,
                            controller = "Home",
                            action = "Index",
                            viewname = UrlParameter.Optional,
                            id = UrlParameter.Optional
                        }
                    ,
                    constraints: new
                    {
                        viewname = @"[\w]{0,50}", //大小写字母/数字/下划线
                        id = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    }
                    , namespaces: new string[] { string.Format("Maticsoft.Web.Areas.{0}.Controllers", AreaName) }
                    );
            }
            #endregion
        }
    }
}
