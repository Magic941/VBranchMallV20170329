/**
* UserAlbumsType.cs
*
* 功 能： N/A
* 类 名： UserAlbumsType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:02   N/A    初版
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
	/// 用户专辑类型
	/// </summary>
	public partial class UserAlbumsType
	{
		private readonly IUserAlbumsType dal=DASNS.CreateUserAlbumsType();
		public UserAlbumsType()
		{}
		#region  BasicMethod


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AlbumsID,int TypeID)
		{
			return dal.Exists(AlbumsID,TypeID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SNS.UserAlbumsType model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.UserAlbumsType model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int AlbumsID,int TypeID)
		{
			return dal.Delete(AlbumsID,TypeID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.UserAlbumsType GetModel(int AlbumsID,int TypeID)
		{
			return dal.GetModel(AlbumsID,TypeID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.UserAlbumsType GetModelByCache(int AlbumsID,int TypeID)
		{
			
			string CacheKey = "UserAlbumsTypeModel-" + AlbumsID+TypeID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AlbumsID,TypeID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.UserAlbumsType)objModel;
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
		public List<Maticsoft.Model.SNS.UserAlbumsType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.UserAlbumsType> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.UserAlbumsType> modelList = new List<Maticsoft.Model.SNS.UserAlbumsType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.UserAlbumsType model;
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.UserAlbumsType GetModelByUserId(int AlbumsID, int UserId)
        {
            return dal.GetModelByUserId(AlbumsID, UserId);
        }

        public bool UpdateType(Maticsoft.Model.SNS.UserAlbumsType model)
	    {
            return dal.UpdateType(model);
	    }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddEx(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            if (!Exists(model.AlbumsID, model.TypeID))
            {
                return dal.Add(model);
            }
            return false;
        }
	    #endregion  ExtensionMethod
	}
}

