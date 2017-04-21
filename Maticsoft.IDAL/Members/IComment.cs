using System;
using System.Data;
namespace Maticsoft.IDAL.Comment
{
	/// <summary>
	/// 接口层Comment
	/// </summary>
	public interface IComment
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Comment.Comment model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Comment.Comment model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Comment.Comment GetModel(int ID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);

	    /// <summary>
	    /// 根据分页获得数据列表
	    /// </summary>
	    //DataSet GetList(int PageSize,int PageIndex,string strWhere);

        /// <summary>
        /// 更新评论审核状态
        /// </summary>
        /// <param name="strList">评论ID集合</param>
        /// <param name="bResult">状态，true：审核；false：未审核</param>
        /// <returns></returns>
	    bool UpdateStates(string strList, bool bResult);

	    #endregion  成员方法
	} 
}
