/**
* UserShip.cs
*
* 功 能： N/A
* 类 名： UserShip
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:06   N/A    初版
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
using System.Linq;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 用户的关注
	/// </summary>
	public partial class UserShip
	{
		private readonly IUserShip dal=DASNS.CreateUserShip();
		public UserShip()
		{}

		#region  Method

		


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ActiveUserID,int PassiveUserID)
		{
			return dal.Exists(ActiveUserID,PassiveUserID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SNS.UserShip model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.UserShip model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ActiveUserID,int PassiveUserID)
		{
			
			return dal.Delete(ActiveUserID,PassiveUserID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.UserShip GetModel(int ActiveUserID,int PassiveUserID)
		{
			
			return dal.GetModel(ActiveUserID,PassiveUserID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.UserShip GetModelByCache(int ActiveUserID,int PassiveUserID)
		{
			
			string CacheKey = "UserShipModel-" + ActiveUserID+PassiveUserID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ActiveUserID,PassiveUserID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.UserShip)objModel;
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
		public List<Maticsoft.Model.SNS.UserShip> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
			return dal.DataTableToList(ds.Tables[0]);
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

		#region  MethodEx
        /// <summary>
        /// 关注某人
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="FellowUserId"></param>
        /// <returns></returns>
        public bool FellowUser(int Userid, int FellowUserId)
        {
            Maticsoft.Model.SNS.UserShip UserShipModel = new Model.SNS.UserShip();
            UserShipModel.ActiveUserID = Userid;
            UserShipModel.PassiveUserID = FellowUserId;
            UserShipModel.CreatedDate = DateTime.Now;
            UserShipModel.IsRead = false;
            UserShipModel.State = 1;
            UserShipModel.Type = (int)Maticsoft.Model.SNS.EnumHelper.FansType.Normal;
            return dal.FellowUser(UserShipModel);
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="FellowUserId"></param>
        /// <returns></returns>
        public bool UnFellowUser(int Userid, int FellowUserId)
        {
           return dal.UnFellowUser(Userid,FellowUserId);
        }

        #region 分页获取用户
        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByFellowsPage(int userid, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByFellowsPage(userid, orderby, startIndex, endIndex);
        }
        #endregion

        #region 分页获取用户
        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByFansPage(int userid, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByFansPage(userid, orderby, startIndex, endIndex);
        }
        #endregion

        #region 分页获取用户
        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserShip> GetToListByFellowsPage(int userid, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByFellowsPage(userid, orderby, startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return dal.DataTableToListEx(ds.Tables[0]);
        } 
        #endregion

        #region My分页获取用户Region
        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserShip> GetToListByFansPage(int userid, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByFansPage(userid, orderby, startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return dal.DataTableToListEx(ds.Tables[0]);
        } 
        #endregion

        #region 获取用户所有的粉丝记录总数
        /// <summary>
        /// 获取用户所有的粉丝记录总数
        /// </summary>
        public int GetUsersAllFansRecordCount(int userid)
        {
            return dal.GetRecordCount(string.Format(" PassiveUserID={0}", userid));
        } 
        #endregion

        #region 获取用户所有的粉丝数据列表
        /// <summary>
        /// 获取用户所有的粉丝数据列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserShip> GetUsersAllFansByPage(int userid, int startIndex, int endIndex,int CurrentUserId)
        {
            List<Maticsoft.Model.SNS.UserShip> list= GetToListByFansPage(userid, "", startIndex, endIndex);
            ///当前用户是否关注此粉丝列表中的用户
            if (CurrentUserId != 0 && list != null && list.Count > 0)
            {
                List<Maticsoft.Model.SNS.UserShip> shipList = GetUserFellowList(CurrentUserId);
                if (shipList != null && shipList.Count > 0)
                {
                    list.ForEach(item => item.IsFellow = UserIsFellow(item.ActiveUserID, shipList));
                }
            }
            return list;
        } 

        
        #endregion

        #region 获取用户关注的所有用户记录总数
        /// <summary>
        ///获取用户关注的所有用户记录总数
        /// </summary>
        public int GetUsersAllFellowsRecordCount(int userid)
        {
            return dal.GetRecordCount(string.Format(" ActiveUserID={0}", userid));
        } 
        #endregion

        #region 获取用户关注的所有用户数据列表
        /// <summary>
        /// 获取用户关注的所有用户数据列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserShip> GetUsersAllFellowsByPage(int userid, int startIndex, int endIndex, int CurrentUserId)
        {
            List<Maticsoft.Model.SNS.UserShip> list = GetToListByFellowsPage(userid, "", startIndex, endIndex);
            ///当前用户是否关注此关注列表中的用户
            if (CurrentUserId != 0&&list!=null&&list.Count>0)
            {
                List<Maticsoft.Model.SNS.UserShip> shipList = GetUserFellowList(CurrentUserId);
                if (shipList != null && shipList.Count > 0)
                {
                    list.ForEach(item => item.IsFellow = UserIsFellow(item.PassiveUserID, shipList));
                }
            }
            return list;
        } 
        #endregion

        #region 添加关注
        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="ActiveUserID"></param>
        /// <param name="PassiveUserID"></param>
        /// <returns></returns>
        public bool AddAttention(int ActiveUserID, int PassiveUserID)
        {
            return dal.AddAttention(ActiveUserID, PassiveUserID);
        }
        #endregion

        #region 取消关注
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="ActiveUserID"></param>
        /// <param name="PassiveUserID"></param>
        /// <returns></returns>
        public bool CancelAttention(int ActiveUserID, int PassiveUserID)
        {
            return dal.CancelAttention(ActiveUserID, PassiveUserID);
        }
        #endregion


        public bool UserIsFellow(int UserId, List<Maticsoft.Model.SNS.UserShip> shipList)
        {
            if (shipList!=null&&shipList.Count(item => item.PassiveUserID == UserId) > 0)
            {

                return true;
            }
            return false;
        }
        /// <summary>
        ///得到某个用户的关注列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserShip> GetUserFellowList(int UserId)
        {
            return GetModelList("ActiveUserID = " + UserId + "");
        }

        /// <summary>
        /// 注册成功，自动给10个粉丝
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Top"></param>
        public void GiveUserFellow(int UserId, int Top)
        {
            Maticsoft.BLL.Members.Users userBll = new Members.Users();
            List<Maticsoft.Model.Members.Users> list = new List<Maticsoft.Model.Members.Users>();
            list = userBll.DataTableToList(userBll.GetList(Top, "UserType='UU' and UserID<>"+UserId+" ", "newid()").Tables[0]);
            foreach (Maticsoft.Model.Members.Users item in list)
            {
                AddAttention(item.UserID, UserId);
            }
        }

        public void GiveUserFellow(int UserId)
        {
            string Fans = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FansList");
            Maticsoft.BLL.Members.Users userBll = new Members.Users();
            if (!string.IsNullOrEmpty(Fans))
            {
                string[] list = Fans.Split(',');
                foreach (string item in list)
                {
                    int FansUserID = Common.Globals.SafeInt(item, 0);
                    if (userBll.Exists(FansUserID))
                    {
                        AddAttention(FansUserID, UserId);
                    }
                }
            }
        }
		#endregion  ExtensionMethod
	}
}

