using System;
using System.Data;
using System.Collections.Generic;

using Maticsoft.Common;
using Maticsoft.Model;
using Maticsoft.DALFactory;
using Maticsoft.IDAL;
using Maticsoft.IDAL.Shop.Coupon;

namespace Maticsoft.BLL
{
    /// <summary>
    /// Shop_CouponRuleExt
    /// </summary>
    public partial class Shop_CouponRuleExt
    {
        private readonly IShop_CouponRuleExt dal = DAShopCoupon.CreateShop_CouponRuleExt();
        public Shop_CouponRuleExt()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(Idlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt GetModelByCache(int Id)
        {

            string CacheKey = "Shop_CouponRuleExtModel-" + Id;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt)objModel;
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
        public List<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt> modelList = new List<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model;
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region  ExtensionMethod
        //根据卡批次号获取相应的优惠券发送规则
        public List<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt> GetListByBatchID(string BatchID)
        {
            var ds = dal.GetListByBatchID(BatchID);
            return DataTableToList(ds.Tables[0]);
        }

        //根据一条规则给特定的用户发送优惠券  
        public bool SetUserCoupon(int userID, int count, int classID)
        {
            return dal.SetUserCoupon(userID, count, classID);
        }

        //根据一条规则给特定的用户发送优惠券  
        public bool SetManualCoupon(int userID, int count, int classID, string cardNo, string Batch)
        {
            return dal.SetManualCoupon(userID, count, classID, cardNo, Batch);
        }

        public int Getcoupons(string batchid, int userID)
        {
            Maticsoft.BLL.Shop_CouponRuleExt cop = new Maticsoft.BLL.Shop_CouponRuleExt();
            var list = cop.GetListByBatchID(batchid);
            foreach (var item in list)
            {
                if (!cop.SetUserCoupon(userID, item.CouponCount, item.ClassID))
                {
                    return 0;
                }
            }
            return 1;
        }

        public DataSet GetList()
        {
            return dal.GetList();
        }
        #endregion  ExtensionMethod
    }
}

