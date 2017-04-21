using System;
using System.Collections.Generic;
using System.Reflection;
using Maticsoft.Common;
namespace Maticsoft.DALFactory
{
    /// <summary>
    /// Abstract Factory base class
    /// </summary>
    public class DataAccessBase
    {

        protected static readonly string AssemblyPath = ConfigHelper.GetConfigString("DAL");
                
        #region CreateObject

        //不使用缓存
        protected static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }

        }
        //使用缓存
        protected static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
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
