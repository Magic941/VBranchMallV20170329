using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Ms;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Ms;
namespace Maticsoft.BLL.Ms
{
	/// <summary>
	/// RegionRec
	/// </summary>
	public partial class RegionRec
	{
        private readonly IRegionRec dal = DAMs.CreateRegionRec();
		public RegionRec()
		{}
		#region  BasicMethod

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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Ms.RegionRec model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Ms.RegionRec model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Ms.RegionRec GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Ms.RegionRec GetModelByCache(int ID)
		{
			
			string CacheKey = "RegionRecModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Ms.RegionRec)objModel;
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
		public List<Maticsoft.Model.Ms.RegionRec> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Ms.RegionRec> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Ms.RegionRec> modelList = new List<Maticsoft.Model.Ms.RegionRec>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Ms.RegionRec model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
				}
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

	    #endregion  BasicMethod

	    #region  ExtensionMethod

	    public List<Maticsoft.Model.Ms.RegionRec> GetRecCityList(int type)
	    {
            string CacheKey = "GetRecCityList-" + type;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModelList(" type=" + type);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Ms.RegionRec>)objModel;
	    }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddEx(int regionId,int type)
        {
            Maticsoft.BLL.Ms.Regions regionBll=new Regions();
            Maticsoft.Model.Ms.Regions regionModel = regionBll.GetModel(regionId);
            Maticsoft.Model.Ms.RegionRec recModel=new Model.Ms.RegionRec();
            //添加之前先删除
            Delete(regionId, type);
            if (regionModel != null)
            {
                recModel.RegionId = regionModel.RegionId;
                recModel.RegionName = regionModel.RegionName;
                recModel.Type = type;
                recModel.DisplaySequence = 0;
                return dal.Add(recModel);
            }
            return -1;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int regionId,int type)
        {
            return dal.Delete(regionId, type);
        }
	    #endregion  ExtensionMethod
	}
}

