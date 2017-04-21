using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Maticsoft.IDAL.Shop.Coupon
{
    /// <summary>
    /// 接口层Shop_CouponRuleExt
    /// </summary>
    public interface IShop_CouponRuleExt
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int Id);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int Id);
        bool DeleteList(string Idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt GetModel(int Id);
        Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt DataRowToModel(DataRow row);
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
        DataSet GetListByBatchID(string BatchID);
        bool SetUserCoupon(int userID, int count, int classID);
        bool SetManualCoupon(int userID, int count, int classID, string cardNo, string Batch);
        DataSet GetList();
        #endregion  MethodEx
    } 
}
