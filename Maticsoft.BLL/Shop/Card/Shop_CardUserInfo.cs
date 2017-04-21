/**  版本信息模板在安装目录下，可自行修改。
* Shop_CardUserInfo.cs
*
* 功 能： N/A
* 类 名： Shop_CardUserInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/6/28 16:15:20   lcy    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海真好邻居电子商务有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model;
using Maticsoft.DALFactory;
using Maticsoft.IDAL;
namespace Maticsoft.BLL
{
	/// <summary>
	/// Shop_CardUserInfo
	/// </summary>
	public partial class Shop_CardUserInfo
	{
		private readonly IShop_CardUserInfo dal=DAShopCard.CreateShop_CardUserInfo();
		public Shop_CardUserInfo()
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
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop_CardUserInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop_CardUserInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			//return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(Idlist,0) );
            return dal.DeleteList(Common.Globals.SafeLongFilter(Idlist, 0));
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop_CardUserInfo GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}
        public Maticsoft.Model.Shop_CardUserInfo GetActicedCardUser(string cardId)
        {
            return dal.GetActicedCardUser(cardId);
        }

        public Maticsoft.Model.Shop_CardUserInfo GetModelByCard(string CardNo)
        {
            DataSet cardList = GetList("CardNo like '%" + CardNo + "%'");
            if (cardList.Tables[0].Rows.Count > 0)
            {
                return dal.DataRowToModel(cardList.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }

        }

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop_CardUserInfo GetModelByCache(int Id)
		{
			
			string CacheKey = "Shop_CardUserInfoModel-" + Id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop_CardUserInfo)objModel;
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
		public List<Maticsoft.Model.Shop_CardUserInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop_CardUserInfo> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop_CardUserInfo> modelList = new List<Maticsoft.Model.Shop_CardUserInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop_CardUserInfo model;
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
        /// 根据用户名查询会员卡信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop_CardUserInfo> GetModelListByName(string userName)
        {
            DataSet ds = dal.GetModelList(userName);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 根据用户名查询默认卡号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetDefaultCardNo(string username)
        {
           return dal.GetDefaultCardNo(username);
        }

        public  int GetDefaultCardsysID(string username)
        {
            return dal.GetDefaultCardsysID(username);
        }

        public bool SetDefaultCard(string username,string cardNo)
        {
            return dal.SetDefaultCard(username, cardNo);
        }

        /// <summary>
        /// 插入本地数据中，并创建用户数据，如何用户数据存在则不用创建用户数据
        /// 一张卡创建一个账号
        /// </summary>
        /// <param name="user"></param>
        /// <param name="carduserinfo"></param>
        /// <returns></returns>
         public bool AddUser4Card(Maticsoft.Accounts.Bus.User user,Maticsoft.Model.Shop_CardUserInfo carduserinfo)
        {
            user.UserName = carduserinfo.CardNo;
            carduserinfo.UserName = carduserinfo.CardNo;

            int id = user.Create();//创建用户

            carduserinfo.UserId = id;             

            Add(carduserinfo);
          
            //未处理用户扩展表            
            BLL.Members.UsersExp userExpManage = new BLL.Members.UsersExp();
            BLL.Members.Users userManage = new BLL.Members.Users();

            BLL.Members.UsersExp ue = new BLL.Members.UsersExp();
            ue.NickName = user.NickName;
            ue.UserID = id;
            ue.BirthdayVisible = 0;
            ue.BirthdayIndexVisible = false;
            ue.Gravatar = "";
            ue.ConstellationVisible = 0;
            ue.ConstellationIndexVisible = false;
            ue.NativePlaceVisible = 0;
            ue.NativePlaceIndexVisible = false;
            ue.RegionId = 0;
            ue.AddressVisible = 0;
            ue.AddressIndexVisible = false;
            ue.BodilyFormVisible = 0;
            ue.BodilyFormIndexVisible = false;
            ue.BloodTypeVisible = 0;
            ue.BloodTypeIndexVisible = false;
            ue.MarriagedVisible = 0;
            ue.MarriagedIndexVisible = false;
            ue.PersonalStatusVisible = 0;
            ue.PersonalStatusIndexVisible = false;
            ue.LastAccessIP = "";
            ue.LastAccessTime = DateTime.Now;
            ue.LastLoginTime = DateTime.Now;
            ue.LastPostTime = DateTime.Now;
            ue.NickName = user.NickName;
            ue.Activity = true;         

            if (!ue.AddExp(ue, -1))
            {
                userManage.Delete(id);
                userExpManage.DeleteUsersExp(id);
                return false;
            }

            return true;
        }

        public bool UpdateUserName(string oldusername, string newusername)
        {
            return dal.UpdateUserName(oldusername,newusername);
        }

		#endregion  ExtensionMethod
	}
}

