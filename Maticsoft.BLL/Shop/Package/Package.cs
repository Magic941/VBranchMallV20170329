using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Package;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Package;
namespace Maticsoft.BLL.Shop.Package
{
	/// <summary>
	/// Package
	/// </summary>
	public partial class Package
	{
        private readonly IPackage dal = DAShopPackage.CreatePackage();
		public Package()
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
		public bool Exists(int PackageId)
		{
			return dal.Exists(PackageId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Package.Package model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Package.Package model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int PackageId)
		{
			
			return dal.Delete(PackageId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string PackageIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(PackageIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Package.Package GetModel(int PackageId)
		{
			
			return dal.GetModel(PackageId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Package.Package GetModelByCache(int PackageId)
		{
			
			string CacheKey = "PackageModel-" + PackageId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(PackageId);
					if (objModel != null)
					{
						 int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Package.Package)objModel;
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
		public List<Maticsoft.Model.Shop.Package.Package> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Package.Package> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Package.Package> modelList = new List<Maticsoft.Model.Shop.Package.Package>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Package.Package model;
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


	    #region ExMethod

	    /// <summary>
	    /// 连接类别表获取相关的数据
	    /// </summary>
	    public DataSet GetListEx(string strWhere)
	    {
	        return dal.GetListEx(strWhere);
	    }


	    #endregion
	}
}

