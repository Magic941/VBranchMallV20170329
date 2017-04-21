/**
* Groups.cs
*
* 功 能： N/A
* 类 名： Groups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:42   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.SNS
{
    /// <summary>
    /// 接口层小组表
    /// </summary>
    public interface IGroups
    {
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string GroupName);
        bool Exists(string GroupName,int groupId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.SNS.Groups model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.Groups model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int GroupID);
        bool DeleteList(string GroupIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.Groups GetModel(int GroupID);
        Maticsoft.Model.SNS.Groups DataRowToModel(DataRow row);
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
        /// <summary>
        /// 批量审核
        /// </summary>
        bool UpdateStatusList(string IdsStr, Maticsoft.Model.SNS.EnumHelper.GroupStatus status);
        /// <summary>
        /// 删除小组和相应的下面的主题和主题的回复
        /// </summary>
        bool DeleteListEx(string GroupIDlist);
        
        /// <summary>
        /// 更新小组推荐的状态
        /// </summary>
       bool  UpdateRecommand(int GroupId,int Recommand);
        #endregion  MethodEx
    }
}
