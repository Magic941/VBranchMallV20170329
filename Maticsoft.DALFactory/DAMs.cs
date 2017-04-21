using System;
using System.Reflection;
using System.Configuration;
using Maticsoft.IDAL.CMS;

namespace Maticsoft.DALFactory
{
    /// <summary>
    /// 抽象工厂模式创建DAL。
    /// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口)  
    /// DataCache类在导出代码的文件夹里
    /// <appSettings>  
    /// <add key="DAL" value="Maticsoft.SQLServerDAL.Ms" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
    /// </appSettings> 
    /// </summary>
    public sealed class DAMs:DataAccessBase
    {
        /// <summary>
        /// 创建Regions数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IRegions CreateRegions()
        {

            string ClassNamespace = AssemblyPath + ".Ms.Regions";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IRegions)objType;
        }


        /// <summary>
        /// 创建RegionRec数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IRegionRec CreateRegionRec()
        {

            string ClassNamespace = AssemblyPath + ".Ms.RegionRec";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IRegionRec)objType;
        }
        /// <summary>
        /// 创建Enterprise数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IEnterprise CreateEnterprise()
        {
            string ClassNamespace = AssemblyPath + ".Ms.Enterprise";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IEnterprise)objType;
        }

    


        /// <summary>
        /// 创建Enterprise数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IEmailTemplet CreateEmailTemplet()
        {

            string ClassNamespace = AssemblyPath + ".Ms.EmailTemplet";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IEmailTemplet)objType;
        }

        /// <summary>
        /// 创建Theme数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.ITheme CreateTheme()
        {

            string ClassNamespace = AssemblyPath + ".Ms.Theme";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.ITheme)objType;
        }


        /// <summary>
        /// 创建Theme数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IThumbnailSize CreateThumbnailSize()
        {
            string ClassNamespace = AssemblyPath + ".Ms.ThumbnailSize";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IThumbnailSize)objType;
        }

         /// <summary>
        /// 创建Area数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IRegionAreas CreateRegionAreas()
        {

            string ClassNamespace = AssemblyPath + ".Ms.RegionAreas";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IRegionAreas)objType;
        }

        /// <summary>
        /// 创建WeiBoMsg数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IWeiBoMsg CreateWeiBoMsg()
        {

            string ClassNamespace = AssemblyPath + ".Ms.WeiBoMsg";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IWeiBoMsg)objType;
        }


        /// <summary>
        /// 创建WeiBoMsg数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.IWeiBoTaskMsg CreateWeiBoTaskMsg()
        {

            string ClassNamespace = AssemblyPath + ".Ms.WeiBoTaskMsg";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IWeiBoTaskMsg)objType;
        }
        
    }
}
