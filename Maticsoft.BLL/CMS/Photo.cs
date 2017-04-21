using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.CMS;
using Maticsoft.Common;
using Microsoft.Practices.Unity.InterceptionExtension;
using Maticsoft.BLL.SysManage;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
namespace Maticsoft.BLL.CMS
{
	/// <summary>
	/// 图片
	/// </summary>
	public class Photo
	{
        private readonly IPhoto dal = DACMS.CreatePhoto();
		public Photo()
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
        /// 得到最大ID
        /// </summary>
        public int GetMaxSequence()
        {
            return dal.GetMaxSequence();
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int PhotoID)
		{
			return dal.Exists(PhotoID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.CMS.Photo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.CMS.Photo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int PhotoID)
		{
			
			return dal.Delete(PhotoID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool DeleteList(string PhotoIDlist, out DataSet imageList)
		{
            return dal.DeleteList(Common.Globals.SafeLongFilter(PhotoIDlist, 0), out  imageList);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.CMS.Photo GetModel(int PhotoID)
		{
			
			return dal.GetModel(PhotoID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.CMS.Photo GetModelByCache(int PhotoID)
		{
			
			string CacheKey = "PhotoModel-" + PhotoID;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(PhotoID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.CMS.Photo)objModel;
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
		public List<Model.CMS.Photo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.CMS.Photo> DataTableToList(DataTable dt)
		{
			List<Model.CMS.Photo> modelList = new List<Model.CMS.Photo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
			    for (int n = 0; n < rowsCount; n++)
				{
					Model.CMS.Photo model = new Model.CMS.Photo();
					if(dt.Rows[n]["PhotoID"]!=null && dt.Rows[n]["PhotoID"].ToString()!="")
					{
						model.PhotoID=int.Parse(dt.Rows[n]["PhotoID"].ToString());
					}
					if(dt.Rows[n]["PhotoName"]!=null && dt.Rows[n]["PhotoName"].ToString()!="")
					{
					model.PhotoName=dt.Rows[n]["PhotoName"].ToString();
					}
					if(dt.Rows[n]["ImageUrl"]!=null && dt.Rows[n]["ImageUrl"].ToString()!="")
					{
					model.ImageUrl=dt.Rows[n]["ImageUrl"].ToString();
					}
					if(dt.Rows[n]["Description"]!=null && dt.Rows[n]["Description"].ToString()!="")
					{
					model.Description=dt.Rows[n]["Description"].ToString();
					}
					if(dt.Rows[n]["AlbumID"]!=null && dt.Rows[n]["AlbumID"].ToString()!="")
					{
						model.AlbumID=int.Parse(dt.Rows[n]["AlbumID"].ToString());
					}
					if(dt.Rows[n]["State"]!=null && dt.Rows[n]["State"].ToString()!="")
					{
						model.State=int.Parse(dt.Rows[n]["State"].ToString());
					}
					if(dt.Rows[n]["CreatedUserID"]!=null && dt.Rows[n]["CreatedUserID"].ToString()!="")
					{
						model.CreatedUserID=int.Parse(dt.Rows[n]["CreatedUserID"].ToString());
					}
					if(dt.Rows[n]["CreatedDate"]!=null && dt.Rows[n]["CreatedDate"].ToString()!="")
					{
						model.CreatedDate=DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
					}
					if(dt.Rows[n]["PVCount"]!=null && dt.Rows[n]["PVCount"].ToString()!="")
					{
						model.PVCount=int.Parse(dt.Rows[n]["PVCount"].ToString());
					}
					if(dt.Rows[n]["ClassID"]!=null && dt.Rows[n]["ClassID"].ToString()!="")
					{
						model.ClassID=int.Parse(dt.Rows[n]["ClassID"].ToString());
					}
					if(dt.Rows[n]["ThumbImageUrl"]!=null && dt.Rows[n]["ThumbImageUrl"].ToString()!="")
					{
					model.ThumbImageUrl=dt.Rows[n]["ThumbImageUrl"].ToString();
					}
					if(dt.Rows[n]["NormalImageUrl"]!=null && dt.Rows[n]["NormalImageUrl"].ToString()!="")
					{
					model.NormalImageUrl=dt.Rows[n]["NormalImageUrl"].ToString();
					}
					if(dt.Rows[n]["Sequence"]!=null && dt.Rows[n]["Sequence"].ToString()!="")
					{
						model.Sequence=int.Parse(dt.Rows[n]["Sequence"].ToString());
					}
					if(dt.Rows[n]["IsRecomend"]!=null && dt.Rows[n]["IsRecomend"].ToString()!="")
					{
						if((dt.Rows[n]["IsRecomend"].ToString()=="1")||(dt.Rows[n]["IsRecomend"].ToString().ToLower()=="true"))
						{
						model.IsRecomend=true;
						}
						else
						{
							model.IsRecomend=false;
						}
					}
					if(dt.Rows[n]["CommentCount"]!=null && dt.Rows[n]["CommentCount"].ToString()!="")
					{
						model.CommentCount=int.Parse(dt.Rows[n]["CommentCount"].ToString());
					}
                    if (dt.Rows[n]["FavouriteCount"] != null && dt.Rows[n]["FavouriteCount"].ToString() != "")
					{
                        model.FavouriteCount = int.Parse(dt.Rows[n]["FavouriteCount"].ToString());
					}
					if(dt.Rows[n]["Tags"]!=null && dt.Rows[n]["Tags"].ToString()!="")
					{
					model.Tags=dt.Rows[n]["Tags"].ToString();
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
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method


        #region Extension Method
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
       {
           return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
       }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public List<Model.CMS.Photo> GetListModelByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return DataTableToList(dal.GetListByPage(strWhere, orderby, startIndex, endIndex).Tables[0]);
        }

       /// <summary>
       /// 获取记录总数
       /// </summary>
       /// <param name="strWhere">查询条件</param>
       /// <returns></returns>
       public int GetRecordCount(string strWhere)
       {
           return dal.GetRecordCount(strWhere);
       }

         /// <summary>
        /// 批量修改图片所属相册
        /// </summary>
        /// <param name="AlbumID">相册ID</param>
        /// <param name="newAlbumId">新相册ID</param>
        /// <returns></returns>
        public bool UpdatePhotoAlbum(int AlbumID,int newAlbumId)
         {
             return dal.UpdatePhotoAlbum(AlbumID, newAlbumId);
         }

        public List<Model.CMS.Photo> GetListAroundPhotoId(int Top,int PhotoId,int ClassId)
        {
            return DataTableToList(dal.GetListAroundPhotoId(Top, PhotoId, ClassId).Tables[0]);
        }
        /// <summary>
        /// 获取需要重新生成缩略图的数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<int> GetListToReGen(string strWhere)
        {
            DataSet ds = dal.GetListToReGen(strWhere);
            List<int> PhotoIdList = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["PhotoID"] != null && ds.Tables[0].Rows[n]["PhotoID"].ToString() != "")
                    {
                        PhotoIdList.Add(int.Parse(ds.Tables[0].Rows[n]["PhotoID"].ToString()));
                    }
                }
            }
            return PhotoIdList;
        }
        #endregion

        /// <summary>
        /// 移动图片到ftp上
        /// </summary>
        /// <param name="ImageUrl"></param>
        /// <param name="savePath"></param>
        /// <param name="saveThumbsPath"></param>
        /// <returns></returns>
        public static string MoveImageForFtp(string ImageUrl, string savePath, string saveThumbsPath)
        {
            var ftpURI = BLL.SysManage.ConfigSystem.GetValueByCache("FtpURI");
            var ftpName = BLL.SysManage.ConfigSystem.GetValueByCache("FtpName");
            var ftpPWD = BLL.SysManage.ConfigSystem.GetValueByCache("FtpPWD");

            var ftphelper = Intercept.NewInstance<Maticsoft.BLL.SysManage.FTPHelper>(new VirtualMethodInterceptor(), new[] { new ErrorLogBehavior() }, new[] { ftpURI, ftpName, ftpPWD });
            try
            {
                
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    if (!ftphelper.Exists(savePath))
                        ftphelper.MakeDir(savePath);

                    if (!ftphelper.Exists(saveThumbsPath))
                        ftphelper.MakeDir(saveThumbsPath);

                    //看下商城缩略图有哪些？
                    List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                        Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS);

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string destImage = "";
                    string originalUrl = "";
                    string thumbUrl = saveThumbsPath + imgname;
                    
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
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                            {
                                destImage = String.Format(thumbUrl, thumbSize.ThumName);
                                //为了防止编辑时 未修改的旧图片移动会导致已存在(目标文件与源文件路径路径相同)
                                if (!ftphelper.Exists(destImage))
                                {
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

        
	}
}

