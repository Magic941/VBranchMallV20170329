/**
* Area.cs
*
* 功 能： N/A
* 类 名： Area
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/17 19:52:38   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SysManage;
namespace Maticsoft.BLL.SysManage
{
	/// <summary>
	/// Area
	/// </summary>
	public partial class ConfigArea
	{
        private readonly IConfigArea dal = DASysManage.CreateConfigArea();
		public ConfigArea()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string AreaName)
		{
			return dal.Exists(AreaName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SysManage.ConfigArea model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SysManage.ConfigArea model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string AreaName)
		{
			
			return dal.Delete(AreaName);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string AreaNamelist )
		{
			return dal.DeleteList(AreaNamelist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SysManage.ConfigArea GetModel(string AreaName)
		{
			
			return dal.GetModel(AreaName);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SysManage.ConfigArea GetModelByCache(string AreaName)
		{
			
			string CacheKey = "AreaModel-" + AreaName;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AreaName);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SysManage.ConfigArea)objModel;
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
		public List<Maticsoft.Model.SysManage.ConfigArea> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SysManage.ConfigArea> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SysManage.ConfigArea> modelList = new List<Maticsoft.Model.SysManage.ConfigArea>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SysManage.ConfigArea model;
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

	    public bool UpdateList(string areas, int status)
	    {
	        return dal.UpdateList(areas, status);
	    }

	    public  static List<Maticsoft.Model.SysManage.ConfigArea> GetAllArea()
	    {
            string CacheKey = "AreaModel-GetAllArea";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.BLL.SysManage.ConfigArea bll=new ConfigArea();
                    objModel = bll.GetModelList("Status=1");
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SysManage.ConfigArea>)objModel;
	    }
        public  static  int GetAreaInt(string area)
        {
            int areaInt = 0;
            switch (area)
            {
                case "CMS":
                    areaInt = 0;
                    break;
                case "SNS":
                    areaInt = 1;
                    break;
                case "Shop":
                    areaInt = 2;
                    break;
                case "Tao":
                    areaInt = 3;
                    break;
                case "COM":
                    areaInt = 4;
                    break;
                default:
                    areaInt =-1;
                    break;
            }
            return areaInt;
        }
	    #endregion  ExtensionMethod
	}
}

