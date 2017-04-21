/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：GroupTags.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/10/25 11:53:32
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 小组标签
	/// </summary>
	public partial class GroupTags
	{
		private readonly IGroupTags dal=DASNS.CreateGroupTags();
		public GroupTags()
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
		public bool Exists(int TagID)
		{
			return dal.Exists(TagID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.GroupTags model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.GroupTags model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int TagID)
		{
			
			return dal.Delete(TagID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string TagIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(TagIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.GroupTags GetModel(int TagID)
		{
			
			return dal.GetModel(TagID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.GroupTags GetModelByCache(int TagID)
		{
			
			string CacheKey = "GroupTagsModel-" + TagID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TagID);
					if (objModel != null)
					{
						 int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.GroupTags)objModel;
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
		public List<Maticsoft.Model.SNS.GroupTags> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.GroupTags> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.GroupTags> modelList = new List<Maticsoft.Model.SNS.GroupTags>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.GroupTags model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.SNS.GroupTags();
					if(dt.Rows[n]["TagID"]!=null && dt.Rows[n]["TagID"].ToString()!="")
					{
						model.TagID=int.Parse(dt.Rows[n]["TagID"].ToString());
					}
					if(dt.Rows[n]["TagName"]!=null && dt.Rows[n]["TagName"].ToString()!="")
					{
					model.TagName=dt.Rows[n]["TagName"].ToString();
					}
					if(dt.Rows[n]["IsRecommand"]!=null && dt.Rows[n]["IsRecommand"].ToString()!="")
					{
						model.IsRecommand=int.Parse(dt.Rows[n]["IsRecommand"].ToString());
					}
					if(dt.Rows[n]["Status"]!=null && dt.Rows[n]["Status"].ToString()!="")
					{
						model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
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
        public DataSet GetSearchList(string Keywords)
        {
            return dal.GetList(0, string.Format("TagName like '%{0}%'", Keywords), "");
        } 
         /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string TagName)
        {
           return dal.Exists(TagName);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TagID, string TagName)
        {
            return dal.Exists(TagID, TagName);
        }
         /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            return dal.UpdateIsRecommand(IsRecommand, IdList);
        }
         /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateStatus(int Status, string IdList)
        {
            return dal.UpdateStatus(Status, IdList);
        }
        #endregion
	}
}

