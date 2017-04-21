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
	/// 图片标签
	/// </summary>
	public partial class PhotoTags
	{
        private readonly IPhotoTags dal = DASNS.CreatePhotoTags();
		public PhotoTags()
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
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.PhotoTags model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.PhotoTags model)
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
		public Maticsoft.Model.SNS.PhotoTags GetModel(int TagID)
		{
			
			return dal.GetModel(TagID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.PhotoTags GetModelByCache(int TagID)
		{
			
			string CacheKey = "PhotoTagsModel-" + TagID;
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
			return (Maticsoft.Model.SNS.PhotoTags)objModel;
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
		public List<Maticsoft.Model.SNS.PhotoTags> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.PhotoTags> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.PhotoTags> modelList = new List<Maticsoft.Model.SNS.PhotoTags>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.PhotoTags model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateStatus(int Status, string IdList)
        {
            return dal.UpdateStatus(Status, IdList);
        }

        public List<Maticsoft.Model.SNS.PhotoTags> GetHotTags(int top)
        {
            return DataTableToList(dal.GetHotTags(top).Tables[0]);
        }

        //public List<string> GetTagsList(string PhotoTags)
        //{
        //    List<string> list = new List<string>();
        //    if (!string.IsNullOrEmpty(PhotoTags))
        //    {

        //        string[] tags = PhotoTags.Split(',');
        //        foreach (string item in tags)
        //        { 
        //          list.Add(PhotoTags.)
                
        //        }
              
        //    }

        //}
        
        #endregion
	}
}

