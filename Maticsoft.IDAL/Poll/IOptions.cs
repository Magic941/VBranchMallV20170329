using System;
using System.Data;
namespace Maticsoft.IDAL.Poll
{
	/// <summary>
	/// 接口层Options
	/// </summary>
	public interface IOptions
	{
		#region  成员方法
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(int TopicID, string Name);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Poll.Options model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
        void Update(Maticsoft.Model.Poll.Options model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        void Delete(int ID);
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Poll.Options GetModel(int ID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
        DataSet GetCountList(int FormID);

        bool DeleteList(string ClassIDlist);

	    /// <summary>
	    /// 得到问卷投票统计
	    /// </summary>
	    /// <param name="strwhere"></param>
	    /// <returns></returns>
	    DataSet GetCountList(string strwhere);

	    #endregion  成员方法
	} 
}
