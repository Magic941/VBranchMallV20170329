/**  版本信息模板在安装目录下，可自行修改。
* BalanceDrawRequest.cs
*
* 功 能： N/A
* 类 名： BalanceDrawRequest
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/4 15:27:03   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Pay
{
	/// <summary>
	/// 接口层BalanceDrawRequest
	/// </summary>
	public interface IBalanceDrawRequest
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long JournalNumber);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(Maticsoft.Model.Pay.BalanceDrawRequest model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Pay.BalanceDrawRequest model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long JournalNumber);
		bool DeleteList(string JournalNumberlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Pay.BalanceDrawRequest GetModel(long JournalNumber);
		Maticsoft.Model.Pay.BalanceDrawRequest DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddEx(Maticsoft.Model.Pay.BalanceDrawRequest model, decimal balance);

	    /// <summary>
	    /// 获得提现金额  sum
	    /// </summary>
	    decimal GetBalanceDraw(int userid, int Status);

	    /// <summary>
	    /// 获得数据列表 与users表内联
	    /// </summary>
	    DataSet GetListUser(string strWhere, string filedOrder);

        ///// <summary>
        ///// 批量修改状态
        ///// </summary>
        //bool UpdateStatus(string JournalNumberlist, int Status);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Status">状态</param>
        /// <param name="balance">用户账户余额</param>
        /// <returns></returns>
        bool UpdateStatus(Model.Pay.BalanceDrawRequest model, int Status, decimal balance);
	    #endregion  MethodEx

	    int GetTotalcount(string startTime, string endTime);
	    int GetTotalAmount(string startTime, string endTime);

	    /// <summary>
	    /// 获得数据列表 与商家表内联
	    /// </summary>
	    DataSet GetListSupplier(string strWhere, string filedOrder);
	} 
}
