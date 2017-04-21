/**  版本信息模板在安装目录下，可自行修改。
* UserCard.cs
*
* 功 能： N/A
* 类 名： UserCard
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/17 19:10:22   N/A    初版
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
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
namespace Maticsoft.BLL.Members
{
	/// <summary>
	/// UserCard
	/// </summary>
	public partial class UserCard
	{
        private readonly IUserCard dal = DAMembers.CreateUserCard();
		public UserCard()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CardCode)
		{
			return dal.Exists(CardCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Members.UserCard model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Members.UserCard model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string CardCode)
		{
			return dal.Delete(CardCode);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CardCodelist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(CardCodelist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Members.UserCard GetModel(string CardCode)
		{
			
			return dal.GetModel(CardCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Members.UserCard GetModelByCache(string CardCode)
		{
			
			string CacheKey = "UserCardModel-" + CardCode;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CardCode);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Members.UserCard)objModel;
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
		public List<Maticsoft.Model.Members.UserCard> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Members.UserCard> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Members.UserCard> modelList = new List<Maticsoft.Model.Members.UserCard>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Members.UserCard model;
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
        /// 创建会员卡
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool AddCard(int userid)
        {
            int cardType = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("VShop_UserCard_Type");
            Maticsoft.Model.Members.UserCard cardModel = new Model.Members.UserCard();
            cardModel.CardCode = DateTime.Now.ToString("yyyyMMdd") + userid;
            cardModel.CardPwd = "";
            cardModel.CardValue = 0;
            cardModel.CreatedDate = DateTime.Now;
            cardModel.EndDate = DateTime.MaxValue;
            cardModel.Status = 1;
            cardModel.Type = cardType==-1?0:cardType;
            cardModel.UserId=userid;
            return dal.AddCard(cardModel);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteEx(string CardCode)
        {
            return dal.DeleteEx(CardCode);
        }
		#endregion  ExtensionMethod
	}
}

