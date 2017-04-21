using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Poll;
using Maticsoft.Model;
using Maticsoft.Common;
namespace Maticsoft.BLL.Poll
{
	/// <summary>
	/// 业务逻辑类Users 的摘要说明。
	/// </summary>
	public class PollUsers
	{
        private readonly IPollUsers dal = DAPoll.CreatePollUsers();

		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}
        public int GetUserCount()
        {
            return dal.GetUserCount();
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserID)
		{
			return dal.Exists(UserID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Poll.PollUsers model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(Maticsoft.Model.Poll.PollUsers model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int UserID)
		{
			
			dal.Delete(UserID);
		}

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ClassIDlist"></param>
        /// <returns></returns>
        public bool DeleteList(string ClassIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ClassIDlist,0) );
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Maticsoft.Model.Poll.PollUsers GetModel(int UserID)
		{
			
			return dal.GetModel(UserID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
        public Maticsoft.Model.Poll.PollUsers GetModelByCache(int UserID)
		{
			
			string CacheKey = "UsersModel-" + UserID;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
            return (Maticsoft.Model.Poll.PollUsers)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Maticsoft.Model.Poll.PollUsers> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.PollUsers> modelList = new List<Maticsoft.Model.Poll.PollUsers>();
			int rowsCount = ds.Tables[0].Rows.Count;
			if (rowsCount > 0)
			{
                Maticsoft.Model.Poll.PollUsers model;
				for (int n = 0; n < rowsCount; n++)
				{
                    model = new Maticsoft.Model.Poll.PollUsers();
					if(ds.Tables[0].Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
					}
					model.UserName=ds.Tables[0].Rows[n]["UserName"].ToString();
					if(ds.Tables[0].Rows[n]["Password"].ToString()!="")
					{
						model.Password=(byte[])ds.Tables[0].Rows[n]["Password"];
					}
					model.TrueName=ds.Tables[0].Rows[n]["TrueName"].ToString();
					if(ds.Tables[0].Rows[n]["Age"].ToString()!="")
					{
						model.Age=int.Parse(ds.Tables[0].Rows[n]["Age"].ToString());
					}
					model.Sex=ds.Tables[0].Rows[n]["Sex"].ToString();
					model.Phone=ds.Tables[0].Rows[n]["Phone"].ToString();
					model.Email=ds.Tables[0].Rows[n]["Email"].ToString();
					model.UserType=ds.Tables[0].Rows[n]["UserType"].ToString();
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

		

		#endregion  成员方法
	}
}

