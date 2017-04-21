/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IUsersApprove.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/10/25 15:36:34
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System.Data;

namespace Maticsoft.IDAL.Members
{
    /// <summary>
    /// 接口层UsersApprove
    /// </summary>
    public interface IUsersApprove
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ApproveID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Members.UsersApprove model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Members.UsersApprove model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ApproveID);

        bool DeleteList(string ApproveIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Members.UsersApprove GetModel(int ApproveID);

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

        #endregion 成员方法

        #region ExMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetApproveList(string strWhere);

        /// <summary>
        /// 批量更新认证信息
        /// </summary>
        /// <param name="ids">待更新的ID</param>
        /// <param name="status">更新状态</param>
        /// <returns>是否更新成功</returns>
        bool BatchUpdate(string ids, string status);


        Maticsoft.Model.Members.UsersApprove GetModelByUserID(int UserID);

        bool DeleteByUserId(int userId);
        #endregion ExMethod
    }
}