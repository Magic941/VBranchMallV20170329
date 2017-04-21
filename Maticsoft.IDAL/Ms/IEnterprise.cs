using System;
using System.Data;
namespace Maticsoft.IDAL.Ms
{
	/// <summary>
	/// 接口层Enterprise
	/// </summary>
	public interface IEnterprise
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int EnterpriseID);
        /// <summary>
        /// 企业名称是否已存在
        /// </summary>
        bool Exists(string Name);
         /// <summary>
        /// 企业名称是否已存在
        /// </summary>
        bool Exists(string Name, int EnterpriseID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Ms.Enterprise model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Ms.Enterprise model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int EnterpriseID);
        /// <summary>
        /// 批量删除数据
        /// </summary>
		bool DeleteList(string EnterpriseIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Ms.Enterprise GetModel(int EnterpriseID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
		int GetRecordCount(string strWhere);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法

        #region 新增的成员方法
        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere);
        #endregion
	} 
}
