using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Order
{
	/// <summary>
	/// 接口层OrderAction
	/// </summary>
	public interface IOrderAction
	{
        #region  成员方法
        DataSet GetNoCompleteOrder();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ActionId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        long Add(Maticsoft.Model.Shop.Order.OrderAction model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Order.OrderAction model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long ActionId);
        bool DeleteList(string ActionIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Order.OrderAction GetModel(long ActionId);
        Maticsoft.Model.Shop.Order.OrderAction DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
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
