/**
* User.cs
*
* 功 能： N/A
* 类 名： User
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:22   N/A    初版
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
using Maticsoft.WeChat.IDAL.Core;
using System.IO;
using System.Net;
using System.Text;
using Maticsoft.Json.Conversion;
using Maticsoft.Json;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// User
	/// </summary>
	public partial class User
	{
        private readonly IUser dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IUser)new WeChat.SQLServerDAL.Core.User() : null;
		public User()
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
        public int Add(Maticsoft.WeChat.Model.Core.User model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.User model)
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
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.User GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.User GetModelByCache(int ID)
        {

            string CacheKey = "UserModel-" + ID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.User)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.User> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.User> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.User> modelList = new List<Maticsoft.WeChat.Model.Core.User>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.User model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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
		public bool DeleteUser(string OpenId, string userName)
		{
			return dal.DeleteUser(OpenId, userName);
		}
		/// <summary>
		/// 该用户是否存在
		/// </summary>
		/// <param name="OpenId"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public bool Exists(string OpenId, string userName)
		{
			return dal.Exists(OpenId, userName);
		}
		/// <summary>
		/// 添加关注用户
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool AddUser(Maticsoft.WeChat.Model.Core.User user)
		{
            bool IsSuccess = true;
			//如果存在就执行更新动作
			if (Exists(user.OpenId, user.UserName))
			{
				return dal.UpdateEx(user);
			}
			else
			{
                return dal.Add(user) > 1;
			}
		}
		/// <summary>
		/// 取消关注
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool CancelUser(string OpenId, string userName)
		{
			return dal.CancelUser(OpenId, userName);
		}
        /// <summary>
        /// 更新用户分组
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
	    public bool UpdateGroup(int groupId, string ids)
	    {
	        return dal.UpdateGroup(groupId, ids);
	    }

        /// <summary>
        /// 获取用户昵称
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetNickName(string userName, string OpenId)
        {
            return dal.GetNickName(userName, OpenId);
        }
        /// <summary>
        /// 更新用户昵称
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UpdateNick(string userName,string OpenId, string  nickName)
        {
            if (!Exists(OpenId, userName))
            {
                Maticsoft.WeChat.Model.Core.User userModel = new Model.Core.User();
                 userModel.OpenId = OpenId;
                userModel.UserName = userName;
                userModel.CreateTime = DateTime.Now;
                userModel.Status = 1;
                userModel.NickName = nickName;
              return  Add(userModel)>1;
            }
            return dal.UpdateNick(userName, OpenId,nickName);
        }

        /// <summary>
        /// 更新用户昵称
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UpdateNick(int  Id, string nickName)
        {
            return dal.UpdateNick(Id, nickName);
        }

	    public Maticsoft.WeChat.Model.Core.User GetUser(string OpenId, string userName)
	    {
            if (String.IsNullOrWhiteSpace(OpenId) || String.IsNullOrWhiteSpace(userName))
            {
                return null;
            }
            Maticsoft.WeChat.Model.Core.User userModel= dal.GetUser(OpenId, userName);
            //如果该用户不存在插入一条数据
	        if (userModel == null)
	        {
                userModel = new Model.Core.User();
                userModel.OpenId = OpenId;
                userModel.UserName = userName;
                userModel.CreateTime = DateTime.Now;
                userModel.Status = 1;
	            Add(userModel);
	        }
	        return userModel;
	    }
        /// <summary>
        /// 获取微信信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public Maticsoft.WeChat.Model.Core.User GetWcInfo(string access_token, string userId)
        {
            StreamReader reader = null;
            string posturl = String.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", access_token, userId);
            Maticsoft.WeChat.Model.Core.User userModel = null;
            //获取用户信息
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(posturl);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();//得到结果
                //解析数据
                Maticsoft.Json.JsonObject jsonObject = JsonConvert.Import<JsonObject>(content);
              
                if (jsonObject["errcode"]!=null)
                {
                    return null;
                }
                userModel = new Model.Core.User();
                userModel.NickName = jsonObject["nickname"] == null ? "" : jsonObject["nickname"].ToString();
                userModel.City = jsonObject["city"] == null ? "" : jsonObject["city"].ToString();
                userModel.Country = jsonObject["country"] == null ? "" : jsonObject["country"].ToString();
                userModel.Province = jsonObject["province"] == null ? "" : jsonObject["province"].ToString();
                userModel.Language = jsonObject["language"] == null ? "" : jsonObject["language"].ToString();
                userModel.Headimgurl = jsonObject["headimgurl"] == null ? "" : jsonObject["headimgurl"].ToString();
                userModel.Sex = jsonObject["sex"] == null ? 0 :Common.Globals.SafeInt(jsonObject["sex"].ToString(),0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return userModel;
        }


        public List<Maticsoft.WeChat.Model.Core.User> GetUserList(string ids ,string openId)
        {
            return GetModelList(" ID in (" + ids + ") and OpenId ='" + openId+"'");
        }
          
        public List<Maticsoft.WeChat.Model.Core.User> GetUsers(string openId)
        {
            return GetModelList("  OpenId='" + openId+"'");
        }

        public List<Maticsoft.WeChat.Model.Core.User> GetListByPageEx(string openId,int status,string startdate,string enddate,string userName, int startIndex,int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'" , openId);
            //关注状态
            if (status!=-1)
            {
                strWhere.AppendFormat(" and Status={0}", status);
            }
            if (!String.IsNullOrWhiteSpace(startdate) && Common.PageValidate.IsDateTime(startdate))
            {

                strWhere.AppendFormat(" and CreateTime >='" + startdate + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(enddate) && Common.PageValidate.IsDateTime(enddate))
            {
            
                strWhere.AppendFormat(" and CreateTime <='" + enddate + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(userName))
            {
                strWhere.AppendFormat(" and UserName like '%{0}%'", userName);
            }
           DataSet ds= GetListByPage(strWhere.ToString(), "CreateTime desc", startIndex, endIndex);//此方法GetListByPageEx需补充
           return DataTableToList(ds.Tables[0]);
        }

        public int GetCount(string openId, int status, string startdate, string enddate, string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            //关注状态
            if (status != -1)
            {
                strWhere.AppendFormat(" and Status={0}", status);
            }
            if (!String.IsNullOrWhiteSpace(startdate) && Common.PageValidate.IsDateTime(startdate))
            {

                strWhere.AppendFormat(" and CreateTime >='" + startdate + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(enddate) && Common.PageValidate.IsDateTime(enddate))
            {

                strWhere.AppendFormat(" and CreateTime <='" + enddate + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" and NickName like '%{0}%'", keyword);
            }
            return GetRecordCount(strWhere.ToString());
        }

        public  bool UpdateMsgTime(string openId, string userName, DateTime date)
        {
            return dal.UpdateMsgTime(openId, userName, date);
        }
        /// <summary>
        /// 48小时消息推送
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="groupId"></param>
        /// <param name="lastMsgTime"></param>
        /// <returns></returns>
        public List<Maticsoft.WeChat.Model.Core.User> GetUserList(string openId, int groupId, int hours=48)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Status=1 and OpenId='{0}'", openId);
            if (groupId > 0)
            {
                strWhere.AppendFormat("  and  groupId={0}", groupId);
            }
            strWhere.AppendFormat("  and  LastMsgTime>=dateadd(hh,-{0},getdate())", hours);
            return GetModelList(strWhere.ToString());
        }


        public bool IsCanSend(string user,int hours=48)
        {
            return dal.IsCanSend(user, hours);
        }

        public string GetNickName(string userName)
        {
            return dal.GetNickName(userName);
        }

        public static Maticsoft.WeChat.Model.Core.User GetUserInfo(string openId,string userName)
        {
            Maticsoft.WeChat.BLL.Core.User userBll = new User();
            return userBll.GetUser(openId, userName);
        }

        public bool UpdateUser(Maticsoft.WeChat.Model.Core.User model)
        {
            return dal.UpdateUser(model);
        }

        public DataSet GetDayCount(string startDate, string endDate)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(startDate) && PageValidate.IsDateTime(startDate))
            {
                stringBuilder.AppendFormat(" CreateTime >='" + startDate + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(endDate) && PageValidate.IsDateTime(endDate))
            {
                string str = Globals.SafeDateTime(endDate, DateTime.Now).AddDays(1.0).ToString("yyyy-MM-dd");
                if (stringBuilder.Length > 1)
                {
                    stringBuilder.Append(" and  ");
                }
                stringBuilder.AppendFormat(" CreateTime <='" + str + "' ", new object[0]);
            }
            return this.dal.GetDayCount(stringBuilder.ToString());
        }
        public DataSet GetCancelCount(string startDate, string endDate)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(startDate) && PageValidate.IsDateTime(startDate))
            {
                stringBuilder.AppendFormat(" CancelTime >='" + startDate + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(endDate) && PageValidate.IsDateTime(endDate))
            {
                string str = Globals.SafeDateTime(endDate, DateTime.Now).AddDays(1.0).ToString("yyyy-MM-dd");
                if (stringBuilder.Length > 1)
                {
                    stringBuilder.Append(" and  ");
                }
                stringBuilder.AppendFormat(" CancelTime <='" + str + "' ", new object[0]);
            }
            return this.dal.GetCancelCount(stringBuilder.ToString());
        }
	    #endregion  ExtensionMethod
	}
}

