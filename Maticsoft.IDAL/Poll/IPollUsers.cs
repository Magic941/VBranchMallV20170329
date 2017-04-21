using System;
using System.Data;
namespace Maticsoft.IDAL.Poll
{
	/// <summary>
	/// 接口层Users
	/// </summary>
    public interface IPollUsers
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int UserID);
        int GetUserCount();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Poll.PollUsers model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
        bool Update(Maticsoft.Model.Poll.PollUsers model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		void Delete(int UserID);
        bool DeleteList(string UserIDlist);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Maticsoft.Model.Poll.PollUsers GetModel(int UserID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		
		#endregion  成员方法
	} 
}
