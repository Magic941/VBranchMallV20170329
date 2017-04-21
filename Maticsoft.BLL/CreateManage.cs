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
    public class CreateManage
    {
        public const string ASSEMBLY_PATH = "Maticsoft.BLL";

        #region CreateObject

        //不使用缓存
        public static object CreateObjectNoCache(string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(ASSEMBLY_PATH).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }

        }
        //使用缓存
        public static object CreateObject(string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(ASSEMBLY_PATH).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch//(System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
        #endregion
    }
}
