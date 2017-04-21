/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IGradeConfig.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/11/12 14:54:12
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System.Data;

namespace Maticsoft.IDAL.SNS
{
    /// <summary>
    /// 接口层GradeConfig
    /// </summary>
    public interface IGradeConfig
    {
        #region 成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int GradeID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.SNS.GradeConfig model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.GradeConfig model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int GradeID);

        bool DeleteList(string GradeIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.GradeConfig GetModel(int GradeID);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        int GetRecordCount(string strWhere);

        /// <summary>
        /// 根据用户分数获取等级
        /// </summary>
        /// <param name="grades">用户分数</param>
        /// <returns></returns>
        Maticsoft.Model.SNS.GradeConfig GetUserLevel(int? grades);

        #endregion 成员方法
    }
}