using System;
using System.Data;
namespace Maticsoft.IDAL.Members
{
	/// <summary>
	/// 接口层PointsLimit
	/// </summary>
	public interface IPointsLimit
	{
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int LimitID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Members.PointsLimit model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Members.PointsLimit model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int LimitID);
        bool DeleteList(string LimitIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Members.PointsLimit GetModel(int LimitID);
        Maticsoft.Model.Members.PointsLimit DataRowToModel(DataRow row);
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
        /// 删除一条数据
        /// </summary>
        bool DeleteEX(int PointsLimitID);

        bool ExistsLimit(int limitid);

        bool ExistsName(string name);
        #endregion
    } 
}
