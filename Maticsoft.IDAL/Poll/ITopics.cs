using System;
using System.Data;
namespace Maticsoft.IDAL.Poll
{
	/// <summary>
	/// 接口层Topics
	/// </summary>
	public interface ITopics
	{
		#region  成员方法
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(int FormID, string Title);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Poll.Topics model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
        void Update(Maticsoft.Model.Poll.Topics model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        void Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Poll.Topics GetModel(int ID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);

	    DataSet GetList(int Top, string strWhere, string filedOrder);

	    #endregion  成员方法
	} 
}
