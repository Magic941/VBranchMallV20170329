using System;
using System.Data;
using System.Collections.Generic;
namespace Maticsoft.IDAL.Settings
{
	/// <summary>
	/// 接口层FLinks
	/// </summary>
    public interface IFriendlyLink
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
		int Add(Maticsoft.Model.Settings.FriendlyLink model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Settings.FriendlyLink model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Settings.FriendlyLink GetModel(int ID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
         /// <summary>
        /// 获得数据列表
        /// </summary>
        Maticsoft.Model.Settings.FriendlyLink DataRowToModel(DataRow row);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
        /// <summary>
        /// <summary>
        /// 批量处理审核状态
        /// </summary>
        /// <param name="IDlsit"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist,string strWhere);
		#endregion  成员方法
	} 
}
