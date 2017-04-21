/**
* CreateManage.cs
*
* 功 能： 业务层反射实例化
* 类 名： CreateManage
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/31 10:30:05   Ben     初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Reflection;
using Maticsoft.Common;

namespace Maticsoft.BLL
{
    public class DataCacheType
    {
        //0为.NET缓存，1为Redis缓存，2为membcache(但未被使用)
        public static int CacheType
        {
            get
            {
                return System.Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CacheType"]);
            }
        }
    }
}
