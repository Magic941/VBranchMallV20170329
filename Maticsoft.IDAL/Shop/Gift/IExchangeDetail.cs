using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Gift
{
	/// <summary>
	/// 接口层ExchangeDetail
	/// </summary>
	public interface IExchangeDetail
	{
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DetailID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Shop.Gift.ExchangeDetail model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Gift.ExchangeDetail model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int DetailID);
        bool DeleteList(string DetailIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Gift.ExchangeDetail GetModel(int DetailID);
        Maticsoft.Model.Shop.Gift.ExchangeDetail DataRowToModel(DataRow row);
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
        #region 扩展方法
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool SetStatus(int detailId, int status);
        /// <summary>
        /// 批量设置状态
        /// </summary>
        /// <param name="detailIds"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool SetStatusList(string  detailIds, int status);
        #endregion
    } 
}
