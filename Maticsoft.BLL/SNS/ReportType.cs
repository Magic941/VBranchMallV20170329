/**
* ReportType.cs
*
* 功 能： N/A
* 类 名： ReportType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:52   N/A    初版
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
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 举报类型
	/// </summary>
	public partial class ReportType
	{
		private readonly IReportType dal=DASNS.CreateReportType();
		public ReportType()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string TypeName)
		{
            return dal.Exists(TypeName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.ReportType model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.ReportType model)
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
		public Maticsoft.Model.SNS.ReportType GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.ReportType GetModelByCache(int ID)
		{
			
			string CacheKey = "ReportTypeModel-" + ID;
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
			return (Maticsoft.Model.SNS.ReportType)objModel;
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
		public List<Maticsoft.Model.SNS.ReportType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.ReportType> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.ReportType> modelList = new List<Maticsoft.Model.SNS.ReportType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.ReportType model;
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
          /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID, string TypeName)
        {
            return dal.Exists(ID, TypeName);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateList(int Status, string IdList)
        {
            return dal.UpdateList(Status, IdList);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSearchList(int Status,string Keywords)
        {
            string strWhere = "";
            if (Status != -1)
            {
                strWhere = string.Format(" Status={0} ", Status);
            }
            if (Keywords.Length > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere += " AND ";
                }
                strWhere += string.Format(" TypeName like '%{0}%'", Keywords);
            }
            return dal.GetList(0,strWhere," ID DESC ");
        }
		#endregion  ExtensionMethod
	}
}

