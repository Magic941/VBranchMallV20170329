/**
* GroupBuy.cs
*
* 功 能： N/A
* 类 名： GroupBuy
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/14 15:51:55   N/A    初版
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
using Maticsoft.Common;
using Maticsoft.Model.Shop.PromoteSales;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.PromoteSales;
using System.Text;
using System.Linq;
using Maticsoft.Model.Shop.Order;

using ServiceStack.RedisCache;

namespace Maticsoft.BLL.Shop.PromoteSales
{
    /// <summary>
    /// GroupBuy
    /// </summary>
    public partial class GroupBuy
    {
        private readonly IGroupBuy dal = DAShopProSales.CreateGroupBuy();
        //ServiceStack.RedisCache.Products.GroupBuy groupBuyCache = new ServiceStack.RedisCache.Products.GroupBuy();

        public GroupBuy()
        { }

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public int Insert2GroupBuy(List<string> GroupBuyIdList, DateTime StartDate, DateTime EndDate)
        {
            return dal.Insert2GroupBuy(GroupBuyIdList, StartDate, EndDate);
        }
        public int BulkInsert2ShopGroup(List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> GroupBuyList)
        {
            return dal.BulkInsert2ShopGroup(GroupBuyList);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GroupBuyId)
        {
            return dal.Exists(GroupBuyId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.PromoteSales.GroupBuy model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.PromoteSales.GroupBuy model)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductImagesModel-" + model.GroupBuyId.ToString();
                if (RedisBase.Item_Get<Maticsoft.Model.Shop.PromoteSales.GroupBuy>(CacheKey) != null)
                {

                    RedisBase.Item_Set<Maticsoft.Model.Shop.PromoteSales.GroupBuy>(CacheKey, model);
                }
            }

            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupBuyId)
        {

            return dal.Delete(GroupBuyId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string GroupBuyIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(GroupBuyIdlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GetModel(int GroupBuyId)
        {
            Maticsoft.Model.Shop.PromoteSales.GroupBuy groupBuy = null;
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductImagesModel-" + GroupBuyId.ToString();
                groupBuy = RedisBase.Item_Get<Maticsoft.Model.Shop.PromoteSales.GroupBuy>(CacheKey);
            }

            if (groupBuy == null)
            {
                groupBuy = dal.GetModel(GroupBuyId);
            }

            return groupBuy;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GetModelByCache(int GroupBuyId)
        {
            object objModel = null;
            string CacheKey = "GroupBuyModel-" + GroupBuyId;

            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                Maticsoft.Model.Shop.PromoteSales.GroupBuy groupBuy = new Maticsoft.Model.Shop.PromoteSales.GroupBuy();
                groupBuy = RedisBase.Item_Get<Maticsoft.Model.Shop.PromoteSales.GroupBuy>(CacheKey);
                objModel = groupBuy;
            }

            if (Maticsoft.BLL.DataCacheType.CacheType == 0)
            {
                objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            }

            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GroupBuyId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        if (Maticsoft.BLL.DataCacheType.CacheType == 1)
                        {
                            RedisBase.Item_Set<Maticsoft.Model.Shop.PromoteSales.GroupBuy>(CacheKey, (Maticsoft.Model.Shop.PromoteSales.GroupBuy)objModel);
                        }
                        if (Maticsoft.BLL.DataCacheType.CacheType == 0)
                        {
                            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                        }
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.PromoteSales.GroupBuy)objModel;
        }

        /// <summary>
        /// 需要及时获取最新的信息，在抢购过程中
        /// </summary>
        /// <param name="groupBuyID"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GetModelByGroupID(int groupBuyID)
        {
            var objModel = dal.GetModel(groupBuyID);
            return objModel;
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
        public List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> GetModelListToday(string strWhere)
        {
            string CacheKey = strWhere;

            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);

            if (objModel == null)
            {
                try
                {
                    DataSet ds = dal.GetListToday(strWhere);
                    objModel = DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("Cache_HorsTime");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch {}
            }
            return (List<Maticsoft.Model.Shop.PromoteSales.GroupBuy>)objModel;
            //DataSet ds = dal.GetListToday(strWhere);
            //return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> modelList = new List<Maticsoft.Model.Shop.PromoteSales.GroupBuy>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.PromoteSales.GroupBuy model;
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

        public List<Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory> DataTable2List(DataTable dt)
        {
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory> modelList = new List<Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRow2Model(dt.Rows[n]);
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


        public int MaxSequence()
        {
            return dal.MaxSequence();
        }
        /// <summary>
        /// 是否在活动期间内
        /// </summary>
        /// <param name="countId"></param>
        /// <returns></returns>
        public bool IsActivity(int buyId)
        {
            Maticsoft.Model.Shop.PromoteSales.GroupBuy model = GetModelByCache(buyId);
            if (model == null)
                return false;
            return model.EndDate >= DateTime.Now && model.StartDate <= DateTime.Now;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool IsExists(long ProductId)
        {
            return dal.IsExists(ProductId);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(string ids, int status)
        {
            return dal.UpdateStatus(ids, status);
        }
        /// <summary>
        /// 将超过结束时间的团购商品状态改为下架
        /// </summary>
        /// <returns></returns>
        public int EditStatus()
        {
            return dal.EditStatus();
        }
        /// <summary>
        /// 更新购买的数量
        /// </summary>
        /// <param name="buyId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool UpdateBuyCount(int buyId, int count)
        {
            return dal.UpdateBuyCount(buyId, count);
        }

        public bool UpdateGroupBuyCountByProductID(long productid, int qty)
        {
            return dal.UpdateGroupBuyCountByProductID(productid, qty);
        }


        public int GetCount(int cid, int regionId, bool IsForMembers)
        {
            StringBuilder sb = new StringBuilder();

            if (IsForMembers)
            {
                sb.Append("   T.Status = 1 ");
            }
            else
            {
                sb.Append("   T.Status = 1 AND T.EndDate>=GETDATE()  AND T.StartDate<=GETDATE() ");
            }

            if (cid > 0)//有选择分类
            {
                //  sb.AppendFormat(" ProductCategory='{0}'", cate);
                sb.AppendFormat(" and (CategoryPath LIKE '%|{0}' ", cid);
                sb.AppendFormat(" OR T.CategoryId = {0})", cid);
            }
            //if (regionId > 0)//有选择地区
            //{
            //    if (!String.IsNullOrWhiteSpace(sb.ToString()))
            //    {
            //        sb.Append(" And ");
            //    }
            //    sb.AppendFormat(" (T.RegionId={0} or ParentId={0} )", regionId);
            //}
            return dal.GetCount(sb.ToString(), regionId, IsForMembers);
        }

        /// <summary>
        /// 只获取日期相关的东西，过了日期就不用获取了
        /// </summary>
        /// <param name="curDate"></param>
        /// <returns></returns>
        public int GetCountHaoLi(string curDate)
        {
            StringBuilder sb = new StringBuilder();
            //未下架且是好礼大派送
            sb.Append(" T.Status = 1  AND DateDiff(HH,StartDate,'" + curDate + "')=0" + " AND PromotionType=1");

            return dal.GetCountHaoLi(sb.ToString());
        }

        public List<Model.Shop.PromoteSales.GroupBuy> GetListByPage(string strWhere, int cid, int regionId, string orderby, int startIndex, int endIndex, int promotionType)
        {
            switch (orderby)
            {
                case "default":
                    orderby = " Sequence DESC";
                    break;
                case "hot":
                    orderby = " BuyCount DESC ";
                    break;
                case "new":
                    orderby = "StartDate DESC ";
                    break;
                case "price":
                    orderby = "Price ASC";
                    break;
                default:
                    orderby = "ProductId DESC";
                    break;
            }
            DataSet ds = dal.GetListByPage(strWhere, cid, regionId, orderby, startIndex, endIndex, promotionType);
            return DataTableToList(ds.Tables[0]);
        }

        public List<Model.Shop.PromoteSales.GroupBuy> GetCategory(string strWhere)
        {
            DataSet ds = dal.GetCategory(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        #region 为了取团购分类数据
        public DataSet GetGroupbyCategory(string strWhere)
        {
            return dal.GetGroupbyCategory(strWhere);
        }
        #endregion
        #region 获取团购热销产品 top 10
        public DataSet GetGroupBuyHot()
        {
            return dal.GetGroupBuyHot();
        }
        #endregion

        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GetPromotionLimitQu(int productId, int PromotionType = 1)
        {
            return dal.GetPromotionLimitQu(productId, PromotionType);
        }


        //判断小时购
        public int GetGroupBuyLimt4Hour(int userid)
        {
            var orderbll = new BLL.Shop.Order.Orders();
            var itemBll = new BLL.Shop.Order.OrderItems();

            string strwhere = " BuyerID =" + userid + " AND OrderType=1 AND OrderStatus>-1 AND DateDiff(hh,CreatedDate,getdate())=0 ";


            var orderList = orderbll.GetModelList(strwhere).ToList();

            if (orderList == null)
            {
                return -1;
            }

            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderInfo item in orderList)
                {
                    //有子订单 已支付 或 货到付款/银行转账 子订单 - 加载子单
                    if (item.HasChildren)
                    {
                        item.SubOrders = orderbll.GetModelList(" ParentOrderId=" + item.OrderId);
                        item.SubOrders.ForEach(
                            info => info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId));
                    }
                    else
                    {
                        item.OrderItems = itemBll.GetModelList(" OrderId=" + item.OrderId);
                    }
                }
            }

            var r = (from c in orderList.SelectMany(x => x.OrderItems)
                     select c).ToList();

            var rs = GetGroupBuyLimt4Hour();

            var q = (from c in r
                     from b in rs
                     where c.ProductId == b.ProductId
                     select b);

            if (q.Count() > 0)
            {
                return 1;//已经购买过小时的抢购产品
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 1元限购
        /// </summary>
        /// <param name="shipName"></param>
        /// <param name="shipTelephone"></param>
        /// <returns>true 不能再买了，false 可以买</returns>
        public bool GetGroupBuyLimit(string shipName, string shipTelephone)
        {
            var orderbll = new BLL.Shop.Order.Orders();
            string strwhere = string.Format("ShipCellPhone='{0}' and DateDiff(dd,CreatedDate,getdate())=0 and OrderTotal<=1.1", shipTelephone);
            var orderList = orderbll.GetModelList(strwhere);
            if (orderList != null && orderList.Count > 0)
            {
                return true;
            }
            return false;
        }




        public int GetGroupBuyLimit(int userid, int productId)
        {
            var orderbll = new BLL.Shop.Order.Orders();
            var itemBll = new BLL.Shop.Order.OrderItems();

            try
            {
                var limtqu = GetPromotionLimitQu(productId);
                string strwhere = " BuyerID =" + userid + " AND OrderType=1 AND OrderStatus>-1 AND DateDiff(dd,CreatedDate,getdate())=0 ";
                //string strwhere = " BuyerID =" + userid + " AND OrderType=1 AND PaymentStatus>1";
                //string strwhere = " BuyerID =" + userid;
                var orderList = orderbll.GetModelList(strwhere).Where(p => p.BuyerID == userid).ToList();
                //orderList = null;
                if (orderList != null && orderList.Count > 0)
                {
                    foreach (OrderInfo item in orderList)
                    {
                        //有子订单 已支付 或 货到付款/银行转账 子订单 - 加载子单
                        if (item.HasChildren)
                        {
                            item.SubOrders = orderbll.GetModelList(" ParentOrderId=" + item.OrderId);
                            item.SubOrders.ForEach(
                                info => info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId));
                        }
                        else
                        {
                            item.OrderItems = itemBll.GetModelList(" OrderId=" + item.OrderId);
                        }
                    }
                }

                //var result = orderList.Select(c => c.OrderItems.Where(p => p.ProductId == productId));
                //var result=orderList.SelectMany(x => x.OrderItems);
                if (orderList == null)
                {
                    if (limtqu != null)
                    {
                        return limtqu.PromotionLimitQu;
                    }
                    return -1;
                }

                var z = orderList.SelectMany(x => x.OrderItems.Where(q => q.ProductId == productId));
                var r = (from c in orderList.SelectMany(x => x.OrderItems)
                         where c.ProductId == productId
                         select c.Quantity);

                if (limtqu != null)
                {
                    if (limtqu.PromotionLimitQu > r.Sum())
                    {
                        return limtqu.PromotionLimitQu - r.Sum();
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            // return r.Sum(); ;
        }

        public List<Model.Shop.PromoteSales.GroupBuy> GetGroupBuyLimt4Hour()
        {
            DataSet ds = dal.GetGroupBuyLimt4Hour();
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory> GetGroupbuyHistory(int groupbuyid)
        {
            DataSet ds = dal.GetGroupbuyHistory(groupbuyid);
            return DataTable2List(ds.Tables[0]);
        }


        public List<Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory> GetGroubuyHistoryByPage(int groupbuyid, int startindex, int endindex)
        {
            DataSet ds = dal.GetGroupBuyListByPage(groupbuyid, startindex, endindex);
            return DataTable2List(ds.Tables[0]);
        }



        #endregion  ExtensionMethod
    }
}

