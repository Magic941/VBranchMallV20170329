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
    /// <add key="DAL" value="Maticsoft.SQLServerDAL.CMS" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
    /// </appSettings> 
    /// </summary>
    public sealed class DACMS:DataAccessBase
    {
        #region Content
        /// <summary>
        /// 创建ClassType数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.CMS.IClassType CreateClassType()
        {

            string ClassNamespace = AssemblyPath + ".CMS.ClassType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.CMS.IClassType)objType;
        }


        /// <summary>
        /// 创建Content数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.CMS.IContent CreateContent()
        {

            string ClassNamespace = AssemblyPath + ".CMS.Content";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.CMS.IContent)objType;
        }


        /// <summary>
        /// 创建ContentClass数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.CMS.IContentClass CreateContentClass()
        {

            string ClassNamespace = AssemblyPath + ".CMS.ContentClass";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.CMS.IContentClass)objType;
        }
        #endregion


        #region Photo

        /// <summary>
        /// 创建Guestbook数据层接口。
        /// </summary>
        public static IPhoto CreatePhoto()
        {

            string ClassNamespace = AssemblyPath + ".CMS.Photo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IPhoto)objType;
        }

        /// <summary>
        /// 创建Guestbook数据层接口。
        /// </summary>
        public static IPhotoAlbum CreatePhotoAlbum()
        {

            string ClassNamespace = AssemblyPath + ".CMS.PhotoAlbum";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IPhotoAlbum)objType;
        }

        /// <summary>
        /// 创建Guestbook数据层接口。
        /// </summary>
        public static IPhotoClass CreatePhotoClass()
        {

            string ClassNamespace = AssemblyPath + ".CMS.PhotoClass";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IPhotoClass)objType;
        }
        #endregion


        #region Video
        /// <summary>
        /// 创建VideoAlbum数据层接口。视频专辑表
        /// </summary>
        public static Maticsoft.IDAL.CMS.IVideoAlbum CreateVideoAlbum()
        {

            string ClassNamespace = AssemblyPath + ".CMS.VideoAlbum";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.CMS.IVideoAlbum)objType;
        }


        /// <summary>
        /// 创建VideoClass数据层接口。视频分类表
        /// </summary>
        public static Maticsoft.IDAL.CMS.IVideoClass CreateVideoClass()
        {

            string ClassNamespace = AssemblyPath + ".CMS.VideoClass";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.CMS.IVideoClass)objType;
        }


        /// <summary>
        /// 创建Video数据层接口。视频信息表
        /// </summary>
        public static Maticsoft.IDAL.CMS.IVideo CreateVideo()
        {

            string ClassNamespace = AssemblyPath + ".CMS.Video";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.CMS.IVideo)objType;
        }
        #endregion

        #region comment

        /// <summary>
        /// 创建ContentClass数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.CMS.IContentClass CreateComment()
        {

            string ClassNamespace = AssemblyPath + ".CMS.Comment";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.CMS.IContentClass)objType;
        } 
        #endregion

    }
}
