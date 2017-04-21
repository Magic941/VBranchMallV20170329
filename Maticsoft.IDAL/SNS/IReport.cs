/**
* Report.cs
*
* 功 能： N/A
* 类 名： Report
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Data;

namespace Maticsoft.IDAL.SNS
{
    /// <summary>
    /// 接口层举报表
    /// </summary>
    public interface IReport
    {
        #region 成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.SNS.Report model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.Report model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ID);

        bool DeleteList(string IDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.Report GetModel(int ID);

        Maticsoft.Model.SNS.Report DataRowToModel(DataRow row);

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

        #region MethodEx

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetListEx(int Top, string strWhere, string filedOrder);

        
        /// <summary>
        /// 更新举报内容状态
        /// </summary>
        bool UpdateReportStatus(int status, int reportId);
        /// <summary>
        /// 更新举报内容状态
        /// </summary>
        bool UpdateReportStatus(int status, string reportIds);
        #endregion MethodEx
    }
}