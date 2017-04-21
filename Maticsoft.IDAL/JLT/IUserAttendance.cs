/**
* UserAttendance.cs
*
* 功 能： N/A
* 类 名： UserAttendance
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/27 19:04:56   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Data;
namespace Maticsoft.IDAL.JLT
{
    /// <summary>
    /// 接口层考勤信息表
    /// </summary>
    public interface IUserAttendance
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int AttendanceID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.JLT.UserAttendance model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.JLT.UserAttendance model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int AttendanceID);
        bool DeleteList(string AttendanceIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.JLT.UserAttendance GetModel(int AttendanceID);
        Maticsoft.Model.JLT.UserAttendance DataRowToModel(DataRow row);
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
        /// 批量处理
        /// </summary>
        bool UpdateList(string IDlist, string strWhere);

        /// <summary>
        /// 统计考勤
        /// </summary>
        /// <param name="where">条件</param>
        DataSet Statistics(string strWhere);

        /// <summary>
        /// 获取用户考勤数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        DataSet GetCollectAttendance(string strWhere);

        #endregion  MethodEx
    }
}
