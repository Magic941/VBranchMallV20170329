using System;
using System.Data;
namespace Maticsoft.IDAL.Members
{
    /// <summary>
    /// 接口层PointsRule
    /// </summary>
    public interface IPointsRule
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int RuleId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Members.PointsRule model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Members.PointsRule model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int RuleId);
        bool DeleteList(string RuleIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Members.PointsRule GetModel(int RuleId);
        Maticsoft.Model.Members.PointsRule DataRowToModel(DataRow row);
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


        string GetRuleName(int ruleid);
        Maticsoft.Model.Members.PointsRule GetModel(int actionId, int targetId, int targetType);

        bool Exists(int ActionId, int targetId, int targetType);
        bool ExistsActionId(int ActionId);
    }
}
