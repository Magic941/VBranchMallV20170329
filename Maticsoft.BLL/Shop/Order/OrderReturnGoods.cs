using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Order;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Order;
namespace Maticsoft.BLL.Shop.Order
{
    /// <summary>
    /// OrderReturnGoods
    /// </summary>
    public partial class OrderReturnGoods
    {
        private readonly IOrderReturnGoods dal = DAShopOrder.CreateOrderReturnGoods();
        public OrderReturnGoods()
        { }
        #region  BasicMethod

        public bool UpdateInformation(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.UpdateInformation(model);
        }

        public bool UpdatePrice(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.UpdatePrice(model);
        }

        public bool Detail_Update(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.Detail_Update(model);
        }
        /// <summary>
        /// 拒收退货 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RefuseReturnGoods(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.RefuseReturnGoods(model);
        }

        public bool OrderReturnRepair(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.OrderReturnRepair(model);
        }


        public bool Detail(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.Detail(model);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderReturnGoods GetModel(long Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderReturnGoods GetModelByCache(long Id)
        {

            string CacheKey = "OrderReturnGoodsModel-" + Id;
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
            return (Maticsoft.Model.Shop.Order.OrderReturnGoods)objModel;
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
        public List<Maticsoft.Model.Shop.Order.OrderReturnGoods> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Order.OrderReturnGoods> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderReturnGoods> modelList = new List<Maticsoft.Model.Shop.Order.OrderReturnGoods>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Order.OrderReturnGoods model;
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


        /// <summary>
        ///  获取组合状态
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="LogisticStatus"></param>
        /// <returns></returns>
        public EnumHelper.MainStatus GetMainStatus(int? Status, int? RefundStatus, int? LogisticStatus)
        {
            EnumHelper.MainStatus type = EnumHelper.MainStatus.Auditing;
            #region 未审核
            //等待审核(退货/退款/换货/维修)
            if (Status == (int)EnumHelper.Status.UnHandle &&
                RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery)
            {
                return type = EnumHelper.MainStatus.Auditing;
            }

            //取消申请(退货/退款/换货/维修)
            if (Status == (int)EnumHelper.Status.Cancel &&
                RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery)
            {
                return type = EnumHelper.MainStatus.Cancel;
            }
            #endregion

            #region 审核未通过
            //拒绝(退货/退款/换货/维修)
            if (Status == (int)EnumHelper.Status.Refuse &&
                RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery)
            {
                return type = EnumHelper.MainStatus.Refuse;
            }
            #endregion

            #region 审核通过
            //(退货/退款/换货/维修)审核通过
            if (Status == (int)EnumHelper.Status.Handling &&
                RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery)
            {
                return type = EnumHelper.MainStatus.Kiss;
            }
            #endregion

            #region 审核 发货
            //等待买家发货
            //if (Status == (int)EnumHelper.Status.Handling &&
            //    RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
            //    LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery)
            //{
            //    return type = EnumHelper.MainStatus.Handling;
            //}
            //(退货/换货/维修)已发货
            if (Status == (int)EnumHelper.Status.Handling &&
                RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.Shipped)
            {
                return type = EnumHelper.MainStatus.Packing;
            }
            //(退货/换货/维修)已收货
            if (Status == (int)EnumHelper.Status.Handling &&
                RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt)
            {
                return type = EnumHelper.MainStatus.Returning;
            }
            //拒绝收货
            //if (Status == (int)EnumHelper.Status.Complete &&
            //    RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
            //    LogisticStatus == (int)EnumHelper.LogisticStatus.storage)
            //{
            //    return type = EnumHelper.MainStatus.Return;
            //}
            if (Status == (int)EnumHelper.Status.Handling &&
                RefundStatus == (int)EnumHelper.RefundStatus.UnRefund &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.storage)
            {
                return type = EnumHelper.MainStatus.Return;
            }
            #endregion

            #region 审核 维修 调货
            //(换货/维修)返程中
            if (Status == (int)EnumHelper.Status.Handling && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.Returnjourney)
            {
                return type = EnumHelper.MainStatus.journey;
            }
            //拒绝维修
            //if (Status == (int)EnumHelper.Status.Complete && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.RefuseRepair)
            //{
            //    return type = EnumHelper.MainStatus.RefuseRepair;
            //}
            //拒绝调货
            //if (Status == (int)EnumHelper.Status.Complete && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.RefuseAdjustable)
            //{
            //    return type = EnumHelper.MainStatus.RefuseAdjustable;
            //}
            //客户确认收货,(换货/维修)完成
            if (Status == (int)EnumHelper.Status.Complete && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.storage)
            {
                return type = EnumHelper.MainStatus.storage;
            }
            //(退款)完成
            if (Status == (int)EnumHelper.Status.Complete && RefundStatus == (int)EnumHelper.RefundStatus.Refunds && LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery)
            {
                return type = EnumHelper.MainStatus.Refunding;
            }
            //(退货)完成
            if (Status == (int)EnumHelper.Status.Complete && RefundStatus == (int)EnumHelper.RefundStatus.Refunds && LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt)
            {
                return type = EnumHelper.MainStatus.Refunding;
            }
            #endregion

            #region 已审核并确认收货进行退款
            //等待退款
            //if (Status == (int)EnumHelper.Status.Handling &&
            //    LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt &&
            //    RefundStatus == (int)EnumHelper.RefundStatus.UnRefund)
            //{
            //    return type = EnumHelper.MainStatus.WaitingRefund;
            //}

            //(退货)退款中
            if (Status == (int)EnumHelper.Status.Handling &&
                LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt &&
                RefundStatus == (int)EnumHelper.RefundStatus.Refunding)
            {
                return type = EnumHelper.MainStatus.Refunding;
            }
            //(退货)已完成
            //if (Status == (int)EnumHelper.Status.Complete &&
            //    LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt &&
            //    RefundStatus == (int)EnumHelper.RefundStatus.Refunds)
            //{
            //    return type = EnumHelper.MainStatus.Complete;
            //}

            return type;
            #endregion
        }


        public EnumHelper.MainStatus GetMainStatus(int ReturnType, int? Status, int? RefundStatus, int? LogisticStatus)
        {
            EnumHelper.MainStatus type = EnumHelper.MainStatus.Auditing;
            // 待审核
            if (Status == (int)EnumHelper.Status.UnHandle)
            {
                type = EnumHelper.MainStatus.Auditing;
            }
            // 取消
            if (Status == (int)EnumHelper.Status.Cancel)
            {
                type = EnumHelper.MainStatus.Cancel;
            }
            // 审核不通过
            if (Status == (int)EnumHelper.Status.Refuse)
            {
                type = EnumHelper.MainStatus.Refuse;
            }
            // 审核通过时各种组合状态
            if (Status == (int)EnumHelper.Status.Handling)
            {
                switch (ReturnType)
                {
                    case (int)EnumHelper.ReturnGoodsType.Goods: // 退货
                        // 拒绝退款
                        if (RefundStatus == (int)EnumHelper.RefundStatus.Refuse)
                        {
                            type = EnumHelper.MainStatus.Refuse;
                        }
                        // 未发货,未退款
                        if (LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund)
                        {
                            type = EnumHelper.MainStatus.Kiss; // 审核通过,等待买家发货
                        }
                        // 已发货,未退款
                        if (LogisticStatus == (int)EnumHelper.LogisticStatus.Shipped && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund)
                        {
                            type = EnumHelper.MainStatus.Packing; // 买家已发货,等待卖家收货
                        }
                        // 已签收,未退款
                        if (LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund)
                        {
                            type = EnumHelper.MainStatus.Returning; // 卖家已收货,等待卖家退款
                        }
                        // 已签收,退款中
                        if (LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt && RefundStatus == (int)EnumHelper.RefundStatus.Refunding)
                        {
                            type = EnumHelper.MainStatus.Returning; // 卖家已收货,卖家退款中
                        }
                        // 已签收,已退款
                        if (LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt && RefundStatus == (int)EnumHelper.RefundStatus.Refunds)
                        {
                            type = EnumHelper.MainStatus.Refunding; // 卖家已收货,卖家已退款
                        }

                        break;
                    case (int)EnumHelper.ReturnGoodsType.Money: // 退款

                        break;
                    case (int)EnumHelper.ReturnGoodsType.ExchangeGoods: //换货

                        break;
                    case (int)EnumHelper.ReturnGoodsType.Repair: // 维修

                        break;
                }


                
            }
            // 完成时各种组合状态
            if (Status == (int)EnumHelper.Status.Complete)
            {

            }
            
            return type;
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 审核操作
        /// </summary>
        /// <param name="returnApproveStatus"></param>
        public bool ApproveReturnOrder(EnumHelper.ReturnApproveStatus returnApproveStatus, long returnId, string peason, string remark)
        {
            return dal.ApproveReturnOrder((int)returnApproveStatus, returnId, peason, remark) > 0;
        }

        /// <summary>
        /// 退款操作
        /// </summary>
        public bool ReturnAccount(EnumHelper.ReturnAccountStatus returnAccountStatus, long returnId, string peason, string remark)
        {
            return dal.ReturnAccount((int)returnAccountStatus, returnId, peason, remark) > 0;
        }

        public List<Maticsoft.Model.Shop.Order.OrderReturnGoods> GetOrderReturnGoods(long userId, int startIndex, int endIndex, out int totalCount)
        {
            DataSet ds = dal.GetListByPage(string.Format(" UserID={0} ", userId), "", startIndex, endIndex);
            totalCount = dal.GetRecordCount(string.Format(" UserID={0} ", userId));
            return this.DataTableToList(ds.Tables[0]);
        }
        #endregion  ExtensionMethod
    }
}

