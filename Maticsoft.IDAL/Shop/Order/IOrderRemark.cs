using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Order
{
	/// <summary>
	/// 接口层OrderRemark
	/// </summary>
	public interface IOrderRemark
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long RemarkId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(Maticsoft.Model.Shop.Order.OrderRemark model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Order.OrderRemark model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long RemarkId);
		bool DeleteList(string RemarkIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Order.OrderRemark GetModel(long RemarkId);
		Maticsoft.Model.Shop.Order.OrderRemark DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
