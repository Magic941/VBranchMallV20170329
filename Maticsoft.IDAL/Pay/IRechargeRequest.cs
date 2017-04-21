/**  版本信息模板在安装目录下，可自行修改。
* RechargeRequest.cs
*
* 功 能： N/A
* 类 名： RechargeRequest
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/3 14:45:12   N/A    初版
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
	/// 接口层RechargeRequest
	/// </summary>
	public interface IRechargeRequest
	{
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(Maticsoft.Model.Pay.RechargeRequest model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Pay.RechargeRequest model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long RechargeId);
		bool DeleteList(string RechargeIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Pay.RechargeRequest GetModel(long RechargeId);
		Maticsoft.Model.Pay.RechargeRequest DataRowToModel(DataRow row);
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
        /// 更新状态
	  /// </summary>
	  /// <param name="reModel"></param>
	  /// <param name="currbalance">当前余额</param>
        /// <param name="rechargeMoney">充值后得到的金额</param>
	  /// <returns></returns>
        bool UpdateStatus(Model.Pay.RechargeRequest reModel, decimal currbalance,decimal rechargeMoney);
        /// <summary>
        /// 获得数据列表 与users表内联
        /// </summary>
        DataSet GetListEx(string strWhere, string filedOrder);
	    #endregion  MethodEx

	    int GetTotalcount(string startTime, string endTime);
	    int GetTotalAmount(string startTime, string endTime);

	    /// <summary>
	    /// 线下充值
	    /// </summary>
	    /// <param name="currbalance">当前余额</param>
	    /// <param name="rechargeMoney">充值后得到的金额</param>
	    bool OfflineRecharge(int userId, decimal currbalance, decimal rechargeMoney, string msg = "线下充值");
	} 
}
