/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductImages.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:26
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
using Microsoft.Practices.Unity.InterceptionExtension;
using Maticsoft.BLL.SysManage;
using System.Drawing;
using System.Drawing.Imaging;

using ServiceStack.RedisCache;

namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// ProductImage
    /// </summary>
    public partial class ProductImage
    {
        private readonly IProductImage dal = DAShopProducts.CreateProductImage();
        //ServiceStack.RedisCache.Products.ProductImage productImgCache = new ServiceStack.RedisCache.Products.ProductImage();

        public ProductImage()
        {}
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ProductId,int ProductImageId)
        {
            return dal.Exists(ProductId,ProductImageId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int  Add(Maticsoft.Model.Shop.Products.ProductImage model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductImage model)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductImagesModel-" + model.ProductImageId.ToString();
                if (RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductImage>(CacheKey) != null)
                {

                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductImage>(CacheKey, model);
                }
            }

            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ProductImageId)
        {
            
            return dal.Delete(ProductImageId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductId,int ProductImageId)
        {
            
            return dal.Delete(ProductId,ProductImageId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ProductImageIdlist )
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ProductImageIdlist ,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductImage GetModel(int ProductImageId)
        {
            Maticsoft.Model.Shop.Products.ProductImage productImg = null;
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductImagesModel-" + ProductImageId.ToString();
                productImg = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductImage>(CacheKey);
            }

            if (productImg == null)
            {
                productImg= dal.GetModel(ProductImageId);
            }

            return productImg;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductImage GetModelByCache(int ProductImageId)
        {
            object objModel = null;
            string CacheKey = "ProductImagesModel-" + ProductImageId;

            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                Maticsoft.Model.Shop.Products.ProductImage productImg = new Maticsoft.Model.Shop.Products.ProductImage();
                productImg = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductImage>(CacheKey);
                objModel = productImg;
            }

            if (Maticsoft.BLL.DataCacheType.CacheType == 0)
            {
                objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            }


            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ProductImageId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        if (Maticsoft.BLL.DataCacheType.CacheType == 1)
                        {
                            RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductImage>(CacheKey, (Maticsoft.Model.Shop.Products.ProductImage)objModel);
                        }
                        if (Maticsoft.BLL.DataCacheType.CacheType == 0)
                        {
                            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                        }
                    }
                }
                catch{}
            }
            return (Maticsoft.Model.Shop.Products.ProductImage)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top,string strWhere,string filedOrder)
        {
            return dal.GetList(Top,strWhere,filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductImage> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductImage> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductImage> modelList = new List<Maticsoft.Model.Shop.Products.ProductImage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductImage model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductImage();
                    if(dt.Rows[n]["ProductImageId"]!=null && dt.Rows[n]["ProductImageId"].ToString()!="")
                    {
                        model.ProductImageId=int.Parse(dt.Rows[n]["ProductImageId"].ToString());
                    }
                    if(dt.Rows[n]["ProductId"]!=null && dt.Rows[n]["ProductId"].ToString()!="")
                    {
                        model.ProductId=long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if(dt.Rows[n]["ImageUrl"]!=null && dt.Rows[n]["ImageUrl"].ToString()!="")
                    {
                    model.ImageUrl=dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl1"]!=null && dt.Rows[n]["ThumbnailUrl1"].ToString()!="")
                    {
                    model.ThumbnailUrl1=dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl2"]!=null && dt.Rows[n]["ThumbnailUrl2"].ToString()!="")
                    {
                    model.ThumbnailUrl2=dt.Rows[n]["ThumbnailUrl2"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl3"]!=null && dt.Rows[n]["ThumbnailUrl3"].ToString()!="")
                    {
                    model.ThumbnailUrl3=dt.Rows[n]["ThumbnailUrl3"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl4"]!=null && dt.Rows[n]["ThumbnailUrl4"].ToString()!="")
                    {
                    model.ThumbnailUrl4=dt.Rows[n]["ThumbnailUrl4"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl5"]!=null && dt.Rows[n]["ThumbnailUrl5"].ToString()!="")
                    {
                    model.ThumbnailUrl5=dt.Rows[n]["ThumbnailUrl5"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl6"]!=null && dt.Rows[n]["ThumbnailUrl6"].ToString()!="")
                    {
                    model.ThumbnailUrl6=dt.Rows[n]["ThumbnailUrl6"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl7"]!=null && dt.Rows[n]["ThumbnailUrl7"].ToString()!="")
                    {
                    model.ThumbnailUrl7=dt.Rows[n]["ThumbnailUrl7"].ToString();
                    }
                    if(dt.Rows[n]["ThumbnailUrl8"]!=null && dt.Rows[n]["ThumbnailUrl8"].ToString()!="")
                    {
                    model.ThumbnailUrl8=dt.Rows[n]["ThumbnailUrl8"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
            //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method

        public List<Model.Shop.Products.ProductImage> GetModelList(long productId)
        {
            DataSet ds = dal.GetList(string.Format(" ProductId={0}", productId));
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移动,可以考虑移动到ftp
        /// </summary>
        /// <param name="ImageUrl"></param>
        /// <param name="savePath"></param>
        /// <param name="saveThumbsPath"></param>
        /// <returns></returns>
        public static string MoveImage(string ImageUrl, string savePath, string saveThumbsPath)
        {
            try
            {
                if (BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ImageStoreWay") == "1")
                {
                    return ImageUrl + "|" + ImageUrl;
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(saveThumbsPath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(saveThumbsPath));

                    List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                        Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop);

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string destImage = "";
                    string originalUrl = "";
                    string thumbUrl = saveThumbsPath + imgname;
                    //首先移动原图片

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    {
                        originalUrl = String.Format(savePath + imgname, "");
                        System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(originalUrl));

                    }
                    if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                    {
                        foreach (var thumbSize in ThumbSizeList)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                            {
                                destImage = String.Format(thumbUrl, thumbSize.ThumName);
                                //为了防止编辑时 未修改的旧图片移动会导致已存在(目标文件与源文件路径路径相同)
                                if (!File.Exists(HttpContext.Current.Server.MapPath(destImage)))
                                {
                                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName)), HttpContext.Current.Server.MapPath(destImage));
                                }                       
                            }
                        }
                    }
                    return originalUrl + "|" + thumbUrl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }


        /// <summary>
        /// 移动,可以考虑移动到ftp
        /// </summary>
        /// <param name="ImageUrl">移动的文件</param>
        /// <param name="savePath"></param>
        /// <param name="saveThumbsPath"></param>
        /// <returns></returns>
        public static string MoveImageForFtp(string ImageUrl, string savePath, string saveThumbsPath)
        {
            var ftpURI = BLL.SysManage.ConfigSystem.GetValueByCache("FtpURI");
            var ftpName = BLL.SysManage.ConfigSystem.GetValueByCache("FtpName");
            var ftpPWD = BLL.SysManage.ConfigSystem.GetValueByCache("FtpPWD");

            var ftphelper = Intercept.NewInstance<Maticsoft.BLL.SysManage.FTPHelper>(new VirtualMethodInterceptor(), new[] { new ErrorLogBehavior() },new[]{ftpURI,ftpName,ftpPWD});
            try
            {
                //图片存储方式，可以去掉，意议不大wusg
                if (BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ImageStoreWay") == "1")
                {
                    return ImageUrl + "|" + ImageUrl;
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    if(!ftphelper.Exists(Path.Combine(ftpURI,savePath)))
                          ftphelper.MakeDir(savePath);

                     if(!ftphelper.Exists(Path.Combine(ftpURI,saveThumbsPath)))
                         ftphelper.MakeDir(saveThumbsPath);
                
                    //看下商城缩略图有哪些？
                    List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                        Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop);

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string destImage = "";
                    string originalUrl = "";
                    string thumbUrl = saveThumbsPath + imgname;
                    //首先移动原图片
                    //文件存在
                    //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    //{
                    //    originalUrl = String.Format(savePath + imgname, "");
                    //System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(originalUrl));

                    //}
                    //指定的文件存在，则上传到ftp
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    {
                        //图片保存路径
                        originalUrl = String.Format(savePath + imgname, "");
                        using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                        {
                            ;
                            using (MemoryStream stream = new MemoryStream())
                            {
                                image.Save(stream, ImageFormat.Jpeg); //把图片保存到流中。
                                ftphelper.Upload(stream, originalUrl);
                            }
                        }

                    }


                    if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                    {
                        foreach (var thumbSize in ThumbSizeList)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                            {
                                destImage = String.Format(thumbUrl, thumbSize.ThumName);
                                //为了防止编辑时 未修改的旧图片移动会导致已存在(目标文件与源文件路径路径相同)
                                if (!ftphelper.Exists(destImage))
                                {
                                    //System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName)), HttpContext.Current.Server.MapPath(destImage));
                                    using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath(String.Format(String.Format(ImageUrl, thumbSize.ThumName), ""))))
                                    {
                                        using (MemoryStream stream = new MemoryStream())
                                        {
                                            image.Save(stream, ImageFormat.Jpeg); //把图片保存到流中。
                                            ftphelper.Upload(stream, destImage);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return originalUrl + "|" + thumbUrl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }


        /// <summary>
        /// 移动,可以考虑移动到ftp
        /// </summary>
        /// <param name="ImageUrl">移动的文件</param>
        /// <param name="savePath"></param>
        /// <param name="saveThumbsPath"></param>
        /// <returns></returns>
        public static string MoveImageForFtp(string ImageUrl, string savePath, string saveThumbsPath, Maticsoft.Model.Ms.EnumHelper.AreaType imgtype)
        {
            var ftpURI = BLL.SysManage.ConfigSystem.GetValueByCache("FtpURI");
            var ftpName = BLL.SysManage.ConfigSystem.GetValueByCache("FtpName");
            var ftpPWD = BLL.SysManage.ConfigSystem.GetValueByCache("FtpPWD");

            var ftphelper = Intercept.NewInstance<Maticsoft.BLL.SysManage.FTPHelper>(new VirtualMethodInterceptor(), new[] { new ErrorLogBehavior() }, new[] { ftpURI, ftpName, ftpPWD });
            try
            {
                //图片存储方式，可以去掉，意议不大wusg
                if (BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ImageStoreWay") == "1")
                {
                    return ImageUrl + "|" + ImageUrl;
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    if (!ftphelper.Exists(Path.Combine(ftpURI, savePath)))
                        ftphelper.MakeDir(savePath);

                    if (!ftphelper.Exists(Path.Combine(ftpURI, saveThumbsPath)))
                        ftphelper.MakeDir(saveThumbsPath);

                    //看下商城缩略图有哪些？ 图片类型
                    List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                        Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(imgtype);

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string destImage = "";
                    string originalUrl = "";
                    string thumbUrl = saveThumbsPath + imgname;
                    //首先移动原图片
                    //文件存在
                    //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    //{
                    //    originalUrl = String.Format(savePath + imgname, "");
                    //System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(originalUrl));

                    //}
                    //指定的文件存在，则上传到ftp
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    {
                        //图片保存路径
                        originalUrl = String.Format(savePath + imgname, "");
                        Image image = Image.FromFile(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")));
                        using (MemoryStream stream = new MemoryStream())
                        {
                            image.Save(stream, ImageFormat.Jpeg); //把图片保存到流中。
                            ftphelper.Upload(stream, originalUrl);
                        }

                    }


                    if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                    {
                        foreach (var thumbSize in ThumbSizeList)
                        {
                            
//(imagePath='E:\projectnew\Maticsoft.Web\Upload\Temp\20140729\'
// + FILENAME=201407291716500698233.png
 
// , uploadPath=E:\projectnew\Maticsoft.Web\Upload\Temp\20140729\ + thumbnailSize.ThumName=T115X115_ + fileName,
//                            thumbnailSize.ThumWidth, thumbnailSize.ThumHeight, GetThumMode(thumbnailSize.ThumMode));
                          //  ImageTools.MakeThumbnail(imagePath + fileName, uploadPath + thumbnailSize.ThumName + fileName, thumbnailSize.ThumWidth, thumbnailSize.ThumHeight, GetThumMode(thumbnailSize.ThumMode));
                            destImage = String.Format(thumbUrl, thumbSize.ThumName);
                            string path = ImageUrl.Replace("{0}","");
                            string destpath = HttpContext.Current.Server.MapPath(path.Substring(0, path.LastIndexOf("/")));
                            ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(path), string.Format(destpath + "/" + imgname, thumbSize.ThumName), thumbSize.ThumWidth, thumbSize.ThumHeight, GetThumMode(thumbSize.ThumMode));
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                            {
                               // destImage = String.Format(thumbUrl, thumbSize.ThumName);
                                //为了防止编辑时 未修改的旧图片移动会导致已存在(目标文件与源文件路径路径相同)
                                if (!ftphelper.Exists(destImage))
                                {
                                    //System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName)), HttpContext.Current.Server.MapPath(destImage));
                                    Image image = Image.FromFile(HttpContext.Current.Server.MapPath(String.Format(String.Format(ImageUrl, thumbSize.ThumName), "")));
                                    using (MemoryStream stream = new MemoryStream())
                                    {
                                        image.Save(stream, ImageFormat.Jpeg); //把图片保存到流中。
                                        ftphelper.Upload(stream, destImage);
                                    }
                                }
                            }
                        }
                    }
                    return originalUrl + "|" + thumbUrl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }

        /// <summary>
        /// 获取裁剪模式
        /// </summary>
        /// <param name="ThumMode"></param>
        /// <returns></returns>
        public static MakeThumbnailMode GetThumMode(int ThumMode)
        {
            MakeThumbnailMode mode = MakeThumbnailMode.None;
            switch (ThumMode)
            {
                case 0:
                    mode = MakeThumbnailMode.Auto;
                    break;
                case 1:
                    mode = MakeThumbnailMode.Cut;
                    break;
                case 2:
                    mode = MakeThumbnailMode.H;
                    break;
                case 3:
                    mode = MakeThumbnailMode.HW;
                    break;
                case 4:
                    mode = MakeThumbnailMode.W;
                    break;
                default:
                    mode = MakeThumbnailMode.Auto;
                    break;
            }
            return mode;
        }



        public List<Model.Shop.Products.ProductImage> ProductImagesList(long productId)
        {
            DataSet ds = dal.ProductImagesList(productId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ProductImageDtToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        public List<Maticsoft.Model.Shop.Products.ProductImage> ProductImageDtToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductImage> modelList = new List<Maticsoft.Model.Shop.Products.ProductImage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductImage model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductImage();
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl2"] != null && dt.Rows[n]["ThumbnailUrl2"].ToString() != "")
                    {
                        model.ThumbnailUrl2 = dt.Rows[n]["ThumbnailUrl2"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl3"] != null && dt.Rows[n]["ThumbnailUrl3"].ToString() != "")
                    {
                        model.ThumbnailUrl3 = dt.Rows[n]["ThumbnailUrl3"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 更新一条数据 
        /// </summary>
        public bool UpdateThumbnail(Maticsoft.Model.Shop.Products.ProductImage model)
        {
            return dal.UpdateThumbnail(model);
        }
    }
}

