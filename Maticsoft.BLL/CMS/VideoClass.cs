/**
* VideoClass.cs
*
* 功 能： 
* 类 名： VideoClass
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
using Maticsoft.Common.Video;
namespace Maticsoft.BLL.CMS
{
	/// <summary>
	/// 视频分类
	/// </summary>
	public partial class VideoClass
	{
        private readonly IVideoClass dal = DACMS.CreateVideoClass();
		public VideoClass()
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
		public bool Exists(int VideoClassID)
		{
			return dal.Exists(VideoClassID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.CMS.VideoClass model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.CMS.VideoClass model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int VideoClassID)
		{
			
			return dal.Delete(VideoClassID);
		}
		/// <summary>
		/// 删除数据
		/// </summary>
		public bool DeleteList(string VideoClassIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(VideoClassIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.CMS.VideoClass GetModel(int VideoClassID)
		{
			
			return dal.GetModel(VideoClassID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.CMS.VideoClass GetModelByCache(int VideoClassID)
		{
			
			string CacheKey = "VideoClassModel-" + VideoClassID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(VideoClassID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.CMS.VideoClass)objModel;
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
		public List<Maticsoft.Model.CMS.VideoClass> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.CMS.VideoClass> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.CMS.VideoClass> modelList = new List<Maticsoft.Model.CMS.VideoClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.CMS.VideoClass model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.CMS.VideoClass();
					if(dt.Rows[n]["VideoClassID"]!=null && dt.Rows[n]["VideoClassID"].ToString()!="")
					{
						model.VideoClassID=int.Parse(dt.Rows[n]["VideoClassID"].ToString());
					}
					if(dt.Rows[n]["VideoClassName"]!=null && dt.Rows[n]["VideoClassName"].ToString()!="")
					{
					model.VideoClassName=dt.Rows[n]["VideoClassName"].ToString();
					}
					if(dt.Rows[n]["ParentID"]!=null && dt.Rows[n]["ParentID"].ToString()!="")
					{
						model.ParentID=int.Parse(dt.Rows[n]["ParentID"].ToString());
					}
					if(dt.Rows[n]["Sequence"]!=null && dt.Rows[n]["Sequence"].ToString()!="")
					{
						model.Sequence=int.Parse(dt.Rows[n]["Sequence"].ToString());
					}
					if(dt.Rows[n]["Path"]!=null && dt.Rows[n]["Path"].ToString()!="")
					{
					model.Path=dt.Rows[n]["Path"].ToString();
					}
					if(dt.Rows[n]["Depth"]!=null && dt.Rows[n]["Depth"].ToString()!="")
					{
						model.Depth=int.Parse(dt.Rows[n]["Depth"].ToString());
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

        #region 扩展的成员方法
         /// <summary>
        /// 级联删除分类及子类
		/// </summary>
        public int DeleteEx(int VideoClassID)
        {
            return dal.DeleteEx(VideoClassID);
        }
          /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddEx(Maticsoft.Model.CMS.VideoClass model)
        {
            return dal.AddEx(model);
        }
            /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere, string orderby)
        {
            return dal.GetListEx(strWhere, orderby);
        }
        /// <summary>
        /// 根据ParentID得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.VideoClass GetModelByParentID(int ParentID)
        {
            return dal.GetModelByParentID(ParentID);
        }
        /// <summary>
        /// 得到最大顺序
        /// </summary>
        public int GetMaxSequence()
        {
            return dal.GetMaxSequence();
        }
        /// <summary>
        /// 对类别进行排序
        /// </summary>
        /// <param name="VideoClassId">类别ID</param>
        /// <param name="zIndex">排序方式</param>
        /// <returns></returns>
        public int SwapCategorySequence(int VideoClassId, SwapSequenceIndex zIndex)
        {
            return dal.SwapCategorySequence(VideoClassId, zIndex);
        }

        public List<Maticsoft.Model.CMS.VideoClass> GetCategorysByDepth(int depth)
        {
            //ADD Cache
            return GetModelList("Depth = " + depth);
        }

        public DataSet GetCategorysByParentIdDs(int parentCategoryId)
        {
            //ADD Cache
            return GetModelDs("ParentID = " + parentCategoryId);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetModelDs(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return ds;
        }
        #endregion
	}
}

