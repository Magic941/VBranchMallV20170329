/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IFilterWords.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/08/24 11:00:36
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Settings
{
	/// <summary>
	/// 接口层FilterWords
	/// </summary>
	public interface IFilterWords
	{
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int FilterId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Settings.FilterWords model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Settings.FilterWords model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int FilterId);
        bool DeleteList(string FilterIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Settings.FilterWords GetModel(int FilterId);
        Maticsoft.Model.Settings.FilterWords DataRowToModel(DataRow row);
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
	    Model.Settings.FilterWords GetByWordPattern(string wordPattern);

        bool UpdateActionType(string ids, int type,string replace);
         
	    bool Exists(string word);

	    #endregion
	} 
}
