/**
* ToDo.cs
*
* 功 能： N/A
* 类 名： ToDo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/25 23:52:13   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.JLT
{
    /// <summary>
    /// 接口层ToDo
    /// </summary>
    public interface IToDoInfo
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.JLT.ToDoInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.JLT.ToDoInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(int id,string fileNames,string fileDataPath);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.JLT.ToDoInfo GetModel(int ID);
        Maticsoft.Model.JLT.ToDoInfo DataRowToModel(DataRow row);
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
        /// 批复处理状态
        /// </summary>
        bool UpdateList(string IDlist, string strWhere);
        /// <summary>
        /// 批复待办信息
        /// </summary>
        bool ReplyToDoInfo(string ids, string setUpdate);

        /// <summary>
        /// 分页获取数据列表
        /// 监理通定制功能, 对应接口:获取待办列表
        /// </summary>
        DataSet GetListByPage4API(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 分页获取数据列表
        /// 监理通定制功能, 对应接口:获取待办列表
        /// </summary>
        DataSet GetListByPageEx4API(string strWhere, string orderby, int startIndex, int endIndex);

        #endregion  MethodEx

    }
}
