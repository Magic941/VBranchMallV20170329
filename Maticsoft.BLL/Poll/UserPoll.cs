using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Poll;
using Maticsoft.Model;
namespace Maticsoft.BLL.Poll
{
	/// <summary>
	/// 业务逻辑类UserPoll 的摘要说明。
	/// </summary>
	public class UserPoll
	{
        private readonly IUserPoll dal = DAPoll.CreateUserPoll();


		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Maticsoft.Model.Poll.UserPoll model)
		{
			dal.Add(model);
		}

	    /// <summary>
	    /// 用户投票 多选题的投票
	    /// </summary>
	    public bool Add2(Model.Poll.UserPoll model)
	    {
	       return   dal.Add2(model);
	    }

	    /// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Maticsoft.Model.Poll.UserPoll model)
		{
			dal.Update(model);
		}

		
		 /// <summary>
        /// 获取参与问卷的用户数
        /// </summary>
        public int GetUserByForm(int FormID)
        {
            return dal.GetUserByForm(FormID);
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
		public List<Maticsoft.Model.Poll.UserPoll> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			List<Maticsoft.Model.Poll.UserPoll> modelList = new List<Maticsoft.Model.Poll.UserPoll>();
			int rowsCount = ds.Tables[0].Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Poll.UserPoll model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Poll.UserPoll();
					if(ds.Tables[0].Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
					}
					if(ds.Tables[0].Rows[n]["TopicID"].ToString()!="")
					{
						model.TopicID=int.Parse(ds.Tables[0].Rows[n]["TopicID"].ToString());
					}
					if(ds.Tables[0].Rows[n]["OptionID"].ToString()!="")
					{
						model.OptionID=int.Parse(ds.Tables[0].Rows[n]["OptionID"].ToString());
					}
					if(ds.Tables[0].Rows[n]["CreatTime"].ToString()!="")
					{
						model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[n]["CreatTime"].ToString());
					}
                    model.UserIP = ds.Tables[0].Rows[0]["UserIP"].ToString();
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByUser(int UserID)
        {
            return GetList(" UserID=" + UserID);
        }

	    /// <summary>
	    /// 获得数据列表
	    /// </summary>
	    //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
	    //{
	    //return dal.GetList(PageSize,PageIndex,strWhere);
	    //}
	    /// <summary>
	    /// 获得数据列表
	    /// </summary>
	    public DataSet GetListInnerJoin(int userid)
	    {
	        return dal.GetListInnerJoin(userid);
	    }

	    #endregion  成员方法
	}
}

