using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.CMS;

namespace Maticsoft.BLL.CMS
{
	/// <summary>
	/// 相册
	/// </summary>
	public class PhotoAlbum
	{
        private readonly IPhotoAlbum dal = DACMS.CreatePhotoAlbum();
		public PhotoAlbum()
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
        /// 得到最大Sequence
        /// </summary>
        public int GetMaxSequence()
        {
            return dal.GetMaxSequence();
        }

	    /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AlbumID)
		{
			return dal.Exists(AlbumID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.CMS.PhotoAlbum model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.CMS.PhotoAlbum model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int AlbumID)
		{
			
			return dal.Delete(AlbumID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string AlbumIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(AlbumIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.CMS.PhotoAlbum GetModel(int AlbumID)
		{
			
			return dal.GetModel(AlbumID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.CMS.PhotoAlbum GetModelByCache(int AlbumID)
		{
			
			string CacheKey = "PhotoAlbumModel-" + AlbumID;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AlbumID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.CMS.PhotoAlbum)objModel;
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
		public List<Model.CMS.PhotoAlbum> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.CMS.PhotoAlbum> DataTableToList(DataTable dt)
		{
			List<Model.CMS.PhotoAlbum> modelList = new List<Model.CMS.PhotoAlbum>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
			    for (int n = 0; n < rowsCount; n++)
				{
					Model.CMS.PhotoAlbum model = new Model.CMS.PhotoAlbum();
					if(dt.Rows[n]["AlbumID"]!=null && dt.Rows[n]["AlbumID"].ToString()!="")
					{
						model.AlbumID=int.Parse(dt.Rows[n]["AlbumID"].ToString());
					}
					if(dt.Rows[n]["AlbumName"]!=null && dt.Rows[n]["AlbumName"].ToString()!="")
					{
					model.AlbumName=dt.Rows[n]["AlbumName"].ToString();
					}
					if(dt.Rows[n]["Description"]!=null && dt.Rows[n]["Description"].ToString()!="")
					{
					model.Description=dt.Rows[n]["Description"].ToString();
					}
					if(dt.Rows[n]["CoverPhoto"]!=null && dt.Rows[n]["CoverPhoto"].ToString()!="")
					{
						model.CoverPhoto=int.Parse(dt.Rows[n]["CoverPhoto"].ToString());
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
					if(dt.Rows[n]["Sequence"]!=null && dt.Rows[n]["Sequence"].ToString()!="")
					{
						model.Sequence=int.Parse(dt.Rows[n]["Sequence"].ToString());
					}
					if(dt.Rows[n]["Privacy"]!=null && dt.Rows[n]["Privacy"].ToString()!="")
					{
						model.Privacy=int.Parse(dt.Rows[n]["Privacy"].ToString());
					}
					if(dt.Rows[n]["LastUpdatedDate"]!=null && dt.Rows[n]["LastUpdatedDate"].ToString()!="")
					{
						model.LastUpdatedDate=DateTime.Parse(dt.Rows[n]["LastUpdatedDate"].ToString());
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
        /// 获取记录总数
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

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

        #endregion
	}
}

