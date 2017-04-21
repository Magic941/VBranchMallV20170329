/**
* CouponInfo.cs
*
* 功 能： N/A
* 类 名： CouponInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:20:59   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Coupon
{
	/// <summary>
	/// 接口层CouponInfo
	/// </summary>
	public interface ICouponInfo
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string CouponCode);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.Model.Shop.Coupon.CouponInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Coupon.CouponInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string CouponCode);
		bool DeleteList(string CouponCodelist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Coupon.CouponInfo GetModel(string CouponCode);
		Maticsoft.Model.Shop.Coupon.CouponInfo DataRowToModel(DataRow row);
        Maticsoft.Model.Shop.Coupon.CouponInfo GetModelFromDataRow(DataRow row);
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

        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, out int total);

	    /// <summary>
	    /// 根据分页获得数据列表
	    /// </summary>
	    //DataSet GetList(int PageSize,int PageIndex,string strWhere);

	    #endregion  成员方法

	    #region  MethodEx

	   bool AddHistory(Maticsoft.Model.Shop.Coupon.CouponInfo info);

	    Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, bool IsExpired);
	    Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, string pwd, bool IsExpired);

        bool UpdateUser(string couponCode, int userId, string userEmail);

        bool UpdateUser(int ruleId, int userId, string userEmail);

	    bool UseCoupon(string couponCode, int userId, string userEmail);

	    bool DeleteEx(int ruleId);

	    Maticsoft.Model.Shop.Coupon.CouponInfo GetActCoupon(int ruleId,int status);

	    bool ExistsByUser(int userId,int ruleId);

        bool ExistsByUser(string email, int ruleId);

	    bool UpdateStatusList(string ids, int status);

        bool UseCoupon(string couponCode);

        bool UseCoupon(string couponCode, int userId);

        DataSet GetCouponList(int userId, int status, bool IsExpired);
        DataSet GetCouponList(int userId, int status,int IsWeixin, bool IsExpired);
        bool IsEffect(string coupon);

        int GetRuleId(int userId);
        int GetRuleId(string UserName);
        Maticsoft.Model.Shop.Coupon.CouponInfo GetAwardCode(int activityId, bool IsExpired);

        bool BindCoupon(string Code, int userId);
	    #endregion  MethodEx
	} 
}
