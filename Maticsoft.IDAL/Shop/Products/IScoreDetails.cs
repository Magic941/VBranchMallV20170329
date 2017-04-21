/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IScoreDetails.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/27 14:50:44
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System.Data;

namespace Maticsoft.IDAL.Shop.Products
{
    /// <summary>
    /// 接口层评分记录表
    /// </summary>
    public interface IScoreDetails
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ScoreId);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Maticsoft.Model.Shop.Products.ScoreDetails model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Products.ScoreDetails model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ScoreId);

        bool DeleteList(string ScoreIdlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Products.ScoreDetails GetModel(int ScoreId);

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

        #endregion 成员方法

        int GetScore(int? ReviewId);

        DataSet GetList();

        DataSet GetScoreDetailInfo(long productId);
    }
}