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
using System.Collections.Generic;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Coupon;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Coupon;
namespace Maticsoft.BLL.Shop.Coupon
{
    /// <summary>
    /// CouponInfo
    /// </summary>
    public partial class CouponInfo
    {
        private readonly ICouponInfo dal = DAShopCoupon.CreateCouponInfo();
        public CouponInfo()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CouponCode)
        {
            return dal.Exists(CouponCode);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CouponCode)
        {

            return dal.Delete(CouponCode);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string CouponCodelist)
        {
            return dal.DeleteList(CouponCodelist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetModel(string CouponCode)
        {

            return dal.GetModel(CouponCode);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetModelByCache(string CouponCode)
        {

            string CacheKey = "CouponInfoModel-" + CouponCode;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(CouponCode);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Coupon.CouponInfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> modelList = new List<Maticsoft.Model.Shop.Coupon.CouponInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Coupon.CouponInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> GetModelFromDt(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> modelList = new List<Maticsoft.Model.Shop.Coupon.CouponInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Coupon.CouponInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.GetModelFromDataRow(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }


        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,out int total)
        {
            return dal.GetListByPage(strWhere,orderby,startIndex,endIndex,out total);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region  ExtensionMethod
        /// <summary>
        /// 转移历史数据
        /// </summary>
        /// <returns></returns>
        public bool MoveHistory()
        {
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoList = GetModelList(" EndDate<'" + DateTime.Now + "' ");
            bool IsSuccess = true;
            if (infoList != null && infoList.Count > 0)
            {
                foreach (var couponInfo in infoList)
                {
                    if (!dal.AddHistory(couponInfo))
                        IsSuccess = false;
                }
            }
            return IsSuccess;
        }

        /// <summary>
        /// 获取优惠券，(IsExpired：是否包括过期优惠券，默认为不包括)
        /// </summary>
        /// <param name="CouponCode"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, bool IsExpired = false)
        {
            return dal.GetCouponInfo(CouponCode, IsExpired);
        }
        /// <summary>
        /// 获取优惠券,需要密码，(IsExpired：是否包括过期优惠券，默认为包括)
        /// </summary>
        /// <param name="CouponCode"></param>
        /// <param name="pwd"></param>
        /// <param name="IsExpired"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, string pwd, bool IsExpired = true)
        {
            return dal.GetCouponInfo(CouponCode, pwd, IsExpired);
        }

        /// <summary>
        /// 分配优惠券给用户（与用户挂钩）
        /// </summary>
        /// <param name="CouponCode"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool UpdateUser(string couponCode, int userId, string userEmail)
        {
            return dal.UpdateUser(couponCode, userId, userEmail);
        }
        /// <summary>
        /// 分配给用户优惠券(根据优惠券规则)
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool UpdateUser(int ruleId, int userId, string userEmail)
        {
            return dal.UpdateUser(ruleId, userId, userEmail);
        }
        /// <summary>
        ///  使用优惠券
        /// </summary>
        /// <param name="couponCode"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool UseCoupon(string couponCode, int userId, string userEmail)
        {
            return dal.UseCoupon(couponCode, userId, userEmail);
        }
        public bool UseCoupon(string couponCode, int userId)
        {
            return dal.UseCoupon(couponCode, userId);
        }
        public bool UseCoupon(string couponCode)
        {
            return dal.UseCoupon(couponCode);
        }

        public bool DeleteEx(int ruleId)
        {
            return dal.DeleteEx(ruleId);
        }

        /// <summary>
        /// 用户获取优惠券
        /// </summary>
        /// <param name="CouponCode"></param>
        /// <param name="pwd"></param>
        /// <param name="IsExpired"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCoupon(int userId, int ruleId)
        {
            //随机获取剩余优惠券
            Maticsoft.Model.Shop.Coupon.CouponInfo info = GetActCoupon(ruleId, 0);
            if (info != null)
            {
                Maticsoft.Accounts.Bus.User user = new User(userId);
                UpdateUser(info.CouponCode, userId, user.Email);
            }
            return info;
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCoupon(string openId, string userName, int ruleId)
        {
            //随机获取剩余优惠券
            Maticsoft.Model.Shop.Coupon.CouponInfo info = GetActCoupon(ruleId, 0);
            if (info != null)
            {
                Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
                Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(openId, userName);
                int userId = wUserModel == null ? -1 : wUserModel.UserId;
                UpdateUser(info.CouponCode, userId, userName);
            }
            return info;
        }

        public bool ExistsByUser(int userId,int ruleId=0)
        {
            return dal.ExistsByUser(userId, ruleId);
        }

        public bool ExistsByUser(string Email,int ruleId=0)
        {
            return dal.ExistsByUser(Email, ruleId);
        }

        public int  GetRuleId(int userId)
        {
            return dal.GetRuleId(userId);
        }

        public int GetRuleId(string  UserName)
        {
            return dal.GetRuleId(UserName);
        }
        /// <summary>
        /// 随机获取各个状态下的优惠券
        /// </summary>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetActCoupon(int ruleId, int status)
        {
            //随机获取剩余优惠券
            return dal.GetActCoupon(ruleId, status);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateStatusList(string ids, int status)
        {
            return dal.UpdateStatusList(ids, status);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> GetListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> GetCouponList(int userId, int status, bool IsExpired = true)
        {
            DataSet ds = dal.GetCouponList(userId, status, IsExpired);
            return GetModelFromDt(ds.Tables[0]);
        }
        #region 微信优惠劵
        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> GetCouponList(int userId, int status,int IsWeixin, bool IsExpired = true)
        {
            DataSet ds = dal.GetCouponList(userId, status,IsWeixin, IsExpired);
            return GetModelFromDt(ds.Tables[0]);
        }
        #endregion
        //是否过期
        public bool IsEffect(string coupon)
        {
            return dal.IsEffect(coupon);
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetAwardCode(int ActivityId,bool IsExpired=true)
        {
            return dal.GetAwardCode(ActivityId, IsExpired);
        }

        /// <summary>
        /// 绑定优惠券
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="IsExpired"></param>
        /// <returns></returns>
        public bool BindCoupon(string Code, int userId)
        {
            return dal.BindCoupon(Code, userId);
        }
        
        #endregion  ExtensionMethod
    }
}  

