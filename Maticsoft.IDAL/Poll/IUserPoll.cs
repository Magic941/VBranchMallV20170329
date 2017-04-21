using System;
using System.Data;
namespace Maticsoft.IDAL.Poll
{
	/// <summary>
	/// 接口层UserPoll
	/// </summary>
	public interface IUserPoll
	{
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
        void Add(Maticsoft.Model.Poll.UserPoll model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
        void Update(Maticsoft.Model.Poll.UserPoll model);
        int GetUserByForm(int FormID);
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);

	    /// <summary>
	    /// 用户投票 多选题的投票
	    /// </summary>
	    bool Add2(Model.Poll.UserPoll model);

	    /// <summary>
	    /// 获得数据列表
	    /// </summary>
	    DataSet GetListInnerJoin(int userid);

	    #endregion  成员方法
	} 
}
