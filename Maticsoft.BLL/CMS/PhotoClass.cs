using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.CMS;

namespace Maticsoft.BLL.CMS
{
	/// <summary>
	/// 图片分类
	/// </summary>
	public class PhotoClass
	{
        private readonly IPhotoClass dal = DACMS.CreatePhotoClass();
		public PhotoClass()
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
		public bool Exists(int ClassID)
		{
			return dal.Exists(ClassID);
		}

	    /// <summary>
	    /// 是否存在该记录
	    /// </summary>
	    public bool ExistsByClassName(string ClassName)
	    {
            return dal.ExistsByClassName(ClassName);
	    }

	    /// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Model.CMS.PhotoClass model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.CMS.PhotoClass model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ClassID)
		{
			
			return dal.Delete(ClassID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ClassIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(ClassIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.CMS.PhotoClass GetModel(int ClassID)
		{
			
			return dal.GetModel(ClassID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.CMS.PhotoClass GetModelByCache(int ClassID)
		{
			
			string CacheKey = "PhotoClassModel-" + ClassID;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ClassID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.CMS.PhotoClass)objModel;
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
		public List<Model.CMS.PhotoClass> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.CMS.PhotoClass> DataTableToList(DataTable dt)
		{
			List<Model.CMS.PhotoClass> modelList = new List<Model.CMS.PhotoClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
			    for (int n = 0; n < rowsCount; n++)
				{
					Model.CMS.PhotoClass model = new Model.CMS.PhotoClass();
					if(dt.Rows[n]["ClassID"]!=null && dt.Rows[n]["ClassID"].ToString()!="")
					{
						model.ClassID=int.Parse(dt.Rows[n]["ClassID"].ToString());
					}
					if(dt.Rows[n]["ClassName"]!=null && dt.Rows[n]["ClassName"].ToString()!="")
					{
					model.ClassName=dt.Rows[n]["ClassName"].ToString();
					}
					if(dt.Rows[n]["ParentId"]!=null && dt.Rows[n]["ParentId"].ToString()!="")
					{
						model.ParentId=int.Parse(dt.Rows[n]["ParentId"].ToString());
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
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method

        #region ExMethod
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.CMS.PhotoClass> GetTopList(int Top, string strWhere, string filedOrder)
        {
            return DataTableToList(dal.GetList(Top, strWhere, filedOrder).Tables[0]);
        }
        #endregion
	}
}

