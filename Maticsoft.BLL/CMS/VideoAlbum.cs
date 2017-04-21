/**
* VideoAlbum.cs
*
* 功 能： 
* 类 名： VideoAlbum
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/22 16:28:49  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.CMS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.CMS;
namespace Maticsoft.BLL.CMS
{
	/// <summary>
	/// 视频专辑
	/// </summary>
	public partial class VideoAlbum
	{
        private readonly IVideoAlbum dal = DACMS.CreateVideoAlbum();
		public VideoAlbum()
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
		public bool Exists(int AlbumID)
		{
			return dal.Exists(AlbumID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.CMS.VideoAlbum model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.CMS.VideoAlbum model)
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
		public Maticsoft.Model.CMS.VideoAlbum GetModel(int AlbumID)
		{
			return dal.GetModel(AlbumID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.CMS.VideoAlbum GetModelByCache(int AlbumID)
		{
			
			string CacheKey = "VideoAlbumModel-" + AlbumID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AlbumID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.CMS.VideoAlbum)objModel;
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
		public List<Maticsoft.Model.CMS.VideoAlbum> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.CMS.VideoAlbum> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.CMS.VideoAlbum> modelList = new List<Maticsoft.Model.CMS.VideoAlbum>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.CMS.VideoAlbum model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.CMS.VideoAlbum();
					if(dt.Rows[n]["AlbumID"]!=null && dt.Rows[n]["AlbumID"].ToString()!="")
					{
						model.AlbumID=int.Parse(dt.Rows[n]["AlbumID"].ToString());
					}
					if(dt.Rows[n]["AlbumName"]!=null && dt.Rows[n]["AlbumName"].ToString()!="")
					{
					model.AlbumName=dt.Rows[n]["AlbumName"].ToString();
					}
					if(dt.Rows[n]["CoverVideo"]!=null && dt.Rows[n]["CoverVideo"].ToString()!="")
					{
					model.CoverVideo=dt.Rows[n]["CoverVideo"].ToString();
					}
					if(dt.Rows[n]["Description"]!=null && dt.Rows[n]["Description"].ToString()!="")
					{
					model.Description=dt.Rows[n]["Description"].ToString();
					}
					if(dt.Rows[n]["CreatedUserID"]!=null && dt.Rows[n]["CreatedUserID"].ToString()!="")
					{
						model.CreatedUserID=int.Parse(dt.Rows[n]["CreatedUserID"].ToString());
					}
					if(dt.Rows[n]["CreatedDate"]!=null && dt.Rows[n]["CreatedDate"].ToString()!="")
					{
						model.CreatedDate=DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
					}
					if(dt.Rows[n]["LastUpdateUserID"]!=null && dt.Rows[n]["LastUpdateUserID"].ToString()!="")
					{
						model.LastUpdateUserID=int.Parse(dt.Rows[n]["LastUpdateUserID"].ToString());
					}
					if(dt.Rows[n]["LastUpdatedDate"]!=null && dt.Rows[n]["LastUpdatedDate"].ToString()!="")
					{
						model.LastUpdatedDate=DateTime.Parse(dt.Rows[n]["LastUpdatedDate"].ToString());
					}
					if(dt.Rows[n]["State"]!=null && dt.Rows[n]["State"].ToString()!="")
					{
						model.State=int.Parse(dt.Rows[n]["State"].ToString());
					}
					if(dt.Rows[n]["Sequence"]!=null && dt.Rows[n]["Sequence"].ToString()!="")
					{
						model.Sequence=int.Parse(dt.Rows[n]["Sequence"].ToString());
					}
					if(dt.Rows[n]["Privacy"]!=null && dt.Rows[n]["Privacy"].ToString()!="")
					{
						model.Privacy=int.Parse(dt.Rows[n]["Privacy"].ToString());
					}
					if(dt.Rows[n]["PvCount"]!=null && dt.Rows[n]["PvCount"].ToString()!="")
					{
						model.PvCount=int.Parse(dt.Rows[n]["PvCount"].ToString());
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

        #region MethodEx
          /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere, string orderby)
        {
            return dal.GetListEx(strWhere, orderby);
        }
         /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.VideoAlbum GetModelEx(int AlbumID)
        {
            return dal.GetModelEx(AlbumID);
        }
        #region 批量处理
        /// <summary>
        /// 批量处理
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }
        #endregion
        /// <summary>
        /// 得到最大顺序
        /// </summary>
        public int GetMaxSequence()
        {
            return dal.GetMaxSequence();
        }
        #endregion
	}
}

