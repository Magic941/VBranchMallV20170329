using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Order;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Order;
namespace Maticsoft.BLL.Shop.Order
{
    /// <summary>
    /// Orders
    /// </summary>
    public partial class Orders
    {
        private readonly IOrders dal = DAShopOrder.CreateOrders();

        public Orders()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long OrderId)
        {
            return dal.Exists(OrderId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrderInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新一条数据(liyongqin)
        /// </summary>
        public bool UpdateGetaway(Maticsoft.Model.Shop.Order.OrderInfo model)
        {
            return dal.UpdateGetaway(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long OrderId)
        {

            return dal.Delete(OrderId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string OrderIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(OrderIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderInfo GetModel(long OrderId)
        {

            return dal.GetModel(OrderId);
        }

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Maticsoft.Model.Shop.Order.OrderInfo GetOrderInfoModel(long OrderId)
        //{

        //    return dal.GetOrderInfoModel(OrderId);
        //}

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderInfo GetModelByCache(long OrderId)
        {

            string CacheKey = "OrdersModel-" + OrderId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(OrderId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Order.OrderInfo)objModel;
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
        /// 获得前几行数据,并获取支付日期
        /// </summary>
        public DataSet GetList2(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList2(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Order.OrderInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Order.OrderInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderInfo> modelList = new List<Maticsoft.Model.Shop.Order.OrderInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Order.OrderInfo model;
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

        /// <summary>
        /// 获取指定父订单号和订单详细中商品编号的订单信息
        /// </summary>
        /// <param name="OrderID">父订单ID</param>
        /// <param name="ProductID">订单详情商品ID</param>
        /// <returns></returns>
        public DataTable GetChildOrder(Int64 OrderID, Int64 ProductID)
        {
            return dal.GetChildOrder(OrderID,ProductID);
        }

        /// <summary>
        /// 获取指定订单的商品种类是否超过一种，超过返回True，反之，返回False
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <returns></returns>
        public bool GetProductTypeCount(Int64 OrderID)
        {
            return dal.GetProductTypeCount(OrderID);
        }

        #region  ExtensionMethod
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(long orderId, int status)
        {
            return dal.UpdateOrderStatus(orderId, status);
        }

        /// <summary>
        /// 退货操作
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool ReturnStatus(long orderId)
        {
            return dal.ReturnStatus(orderId);
        }


        /// <summary>
        /// 根据订单组合状态获取查询订单的条件
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public string GetWhereByStatus(Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus orderType)
        {
            string strWhere = "";
            switch (orderType)
            {
                //等待付款
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Paying:
                    strWhere =
                        String.Format(
                            " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'",
                            (int)EnumHelper.OrderStatus.UnHandle, (int)EnumHelper.PaymentStatus.Unpaid,
                            (int)EnumHelper.ShippingStatus.UnShipped, EnumHelper.PaymentGateway.cod.ToString());
                    break;
                //等待处理
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreHandle:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway='{3}'",
                          (int)EnumHelper.OrderStatus.UnHandle, (int)EnumHelper.PaymentStatus.Unpaid,
                          (int)EnumHelper.ShippingStatus.UnShipped, EnumHelper.PaymentGateway.cod.ToString());
                    break;
                //取消订单
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Cancel:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} ",
                          (int)EnumHelper.OrderStatus.Cancel, (int)EnumHelper.PaymentStatus.Unpaid,
                          (int)EnumHelper.ShippingStatus.UnShipped);
                    break;
                //订单锁定
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Locking:
                    strWhere =
                      String.Format(
                          "  PaymentStatus={1} and ShippingStatus={2}  and (OrderStatus={0} or OrderStatus={3})  ",
                          (int)EnumHelper.OrderStatus.AdminLock, (int)EnumHelper.PaymentStatus.Unpaid,
                          (int)EnumHelper.ShippingStatus.UnShipped, (int)EnumHelper.OrderStatus.UserLock);
                    break;
                //等待付款确认
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreConfirm:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'",
                          (int)EnumHelper.OrderStatus.UnHandle, (int)EnumHelper.PaymentStatus.Paid,
                          (int)EnumHelper.ShippingStatus.UnShipped, EnumHelper.PaymentGateway.bank.ToString());
                    break;
                //正在处理
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Handling:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and ShippingStatus={1} ",
                          (int)EnumHelper.OrderStatus.Handling,
                          (int)EnumHelper.ShippingStatus.UnShipped);
                    break;
                //配货中
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shipping:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and  ShippingStatus={1}",
                          (int)EnumHelper.OrderStatus.Handling,
                          (int)EnumHelper.ShippingStatus.Packing);
                    break;
                //已发货
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shiped:
                    strWhere =
                     String.Format(
                         " OrderStatus={0}  and  ShippingStatus={1}",
                         (int)EnumHelper.OrderStatus.Handling,
                         (int)EnumHelper.ShippingStatus.Shipped);
                    break;
                //已完成
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Complete:
                    strWhere =
                  String.Format(
                      " OrderStatus={0}  and  ShippingStatus={1}",
                      (int)EnumHelper.OrderStatus.Complete,
                      (int)EnumHelper.ShippingStatus.ConfirmShip);
                    break;
                default:
                    break;
            }
            return strWhere;
        }

        /// <summary>
        /// 根据订单组合状态获取查询订单的条件
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public string GetWhereByStatus(int orderType)
        {
            string strWhere = "";
            switch (orderType)
            {
                //等待付款
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Paying:
                    strWhere =
                        String.Format(
                            " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'",
                            (int)EnumHelper.OrderStatus.UnHandle, (int)(int)EnumHelper.PaymentStatus.Unpaid,
                            (int)(int)EnumHelper.ShippingStatus.UnShipped, EnumHelper.PaymentGateway.cod.ToString());
                    break;
                //等待处理
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreHandle:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway='{3}'",
                          (int)EnumHelper.OrderStatus.UnHandle, (int)(int)EnumHelper.PaymentStatus.Unpaid,
                          (int)(int)EnumHelper.ShippingStatus.UnShipped, EnumHelper.PaymentGateway.cod.ToString());
                    break;
                //取消订单
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Cancel:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} ",
                          (int)EnumHelper.OrderStatus.Cancel, (int)EnumHelper.PaymentStatus.Unpaid,
                          (int)EnumHelper.ShippingStatus.UnShipped);
                    break;
                //订单锁定
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Locking:
                    strWhere =
                      String.Format(
                          "  PaymentStatus={1} and ShippingStatus={2}  and (OrderStatus={0} or OrderStatus={3})  ",
                          (int)EnumHelper.OrderStatus.AdminLock, (int)EnumHelper.PaymentStatus.Unpaid,
                          (int)EnumHelper.ShippingStatus.UnShipped, (int)EnumHelper.OrderStatus.UserLock);
                    break;
                //等待付款确认
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreConfirm:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'",
                          (int)EnumHelper.OrderStatus.UnHandle, (int)EnumHelper.PaymentStatus.Paid,
                          (int)EnumHelper.ShippingStatus.UnShipped, EnumHelper.PaymentGateway.bank.ToString());
                    break;
                //正在处理
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Handling:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and ShippingStatus={1} ",
                          (int)EnumHelper.OrderStatus.Handling,
                          (int)EnumHelper.ShippingStatus.UnShipped);
                    break;
                //配货中
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shipping:
                    strWhere =
                      String.Format(
                          " OrderStatus={0}  and  ShippingStatus={1}",
                          (int)EnumHelper.OrderStatus.Handling,
                          (int)EnumHelper.ShippingStatus.Packing);
                    break;
                //已发货
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shiped:
                    strWhere =
                     String.Format(
                         " OrderStatus={0}  and  ShippingStatus={1}",
                         (int)EnumHelper.OrderStatus.Handling,
                         (int)EnumHelper.ShippingStatus.Shipped);
                    break;
                //已完成
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Complete:
                    strWhere =
                  String.Format(
                      " OrderStatus={0}  and  ShippingStatus={1}",
                      (int)EnumHelper.OrderStatus.Complete,
                      (int)EnumHelper.ShippingStatus.ConfirmShip);
                    break;
                default:
                    break;
            }
            return strWhere;
        }
        /// <summary>
        /// 根据订单组合状态获取订单
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Order.OrderInfo> GetListByStatus(
            Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus orderType)
        {
            string strWhere = GetWhereByStatus(orderType) + " order by CreatedDate desc ";
            return GetModelList(strWhere);
        }
        /// <summary>
        /// 根据各种状态返回组合订单状态
        /// </summary>
        /// <returns></returns>
        public EnumHelper.OrderMainStatus GetOrderType(EnumHelper.PaymentGateway paymentGateway, EnumHelper.OrderStatus orderStatus, EnumHelper.PaymentStatus paymentStatus, EnumHelper.ShippingStatus shippingStatus)
        {
            EnumHelper.OrderMainStatus orderType = EnumHelper.OrderMainStatus.PreHandle;
            switch (paymentGateway)
            {
                case EnumHelper.PaymentGateway.cod:
                    //等待处理
                    if (orderStatus == EnumHelper.OrderStatus.UnHandle &&
                        paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                        shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.PreHandle;
                    }
                    //取消订单
                    if (orderStatus == EnumHelper.OrderStatus.Cancel &&
                     paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Cancel;
                    }
                    //订单锁定
                    if ((orderStatus == EnumHelper.OrderStatus.UserLock || orderStatus == EnumHelper.OrderStatus.AdminLock) &&
                    paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                    shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    //正在处理
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //配货中
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == EnumHelper.ShippingStatus.Packing)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shipping;
                    }
                    //已发货
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == EnumHelper.ShippingStatus.Shipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shiped;
                    }
                    //已完成
                    if (orderStatus == EnumHelper.OrderStatus.Complete &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Complete;
                    }
                    break;
                case EnumHelper.PaymentGateway.bank:
                    //等待付款
                    if (orderStatus == EnumHelper.OrderStatus.UnHandle &&
                        paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                        shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Paying;
                    }
                    //取消订单
                    if (orderStatus == EnumHelper.OrderStatus.Cancel &&
                     paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Cancel;
                    }
                    //订单锁定
                    if ((orderStatus == EnumHelper.OrderStatus.UserLock || orderStatus == EnumHelper.OrderStatus.AdminLock) &&
                    paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                    shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    //等待付款确认
                    if (orderStatus == EnumHelper.OrderStatus.UnHandle &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.PreConfirm;
                    }
                    //正在处理
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //配货中
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.Packing)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shipping;
                    }
                    //已发货
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.Shipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shiped;
                    }
                    //已完成
                    if (orderStatus == EnumHelper.OrderStatus.Complete &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Complete;
                    }
                    break;
                default:
                    //等待付款
                    if (orderStatus == EnumHelper.OrderStatus.UnHandle &&
                        paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                        shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Paying;
                    }
                    //取消订单
                    if (orderStatus == EnumHelper.OrderStatus.Cancel &&
                     paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Cancel;
                    }
                    //订单锁定
                    if ((orderStatus == EnumHelper.OrderStatus.UserLock || orderStatus == EnumHelper.OrderStatus.AdminLock) &&
                    paymentStatus == EnumHelper.PaymentStatus.Unpaid &&
                    shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    //正在处理
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //配货中
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.Packing)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shipping;
                    }
                    //已发货
                    if (orderStatus == EnumHelper.OrderStatus.Handling &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.Shipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shiped;
                    }
                    //已完成
                    if (orderStatus == EnumHelper.OrderStatus.Complete &&
                     paymentStatus == EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Complete;
                    }
                    break;
            }
            return orderType;

        }

        /// <summary>
        /// 根据各种状态返回组合订单状态
        /// </summary>
        /// <returns></returns>
        public EnumHelper.OrderMainStatus GetOrderType(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            EnumHelper.OrderMainStatus orderType = EnumHelper.OrderMainStatus.PreHandle;
            switch (paymentGateway)
            {
                case "cod":
                    //等待处理
                    if (orderStatus == (int)EnumHelper.OrderStatus.UnHandle &&
                        paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                        shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.PreHandle;
                    }
                    //取消订单
                    if (orderStatus == (int)EnumHelper.OrderStatus.Cancel &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Cancel;
                    }
                    //订单锁定
                    if ((orderStatus == (int)EnumHelper.OrderStatus.UserLock || orderStatus == (int)EnumHelper.OrderStatus.AdminLock) &&
                    paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                    shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    //正在处理
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //配货中
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.Packing)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shipping;
                    }
                    //已发货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.Shipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shiped;
                    }
                    //退货中
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.RejectedReturning)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //已退货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.RejectedReturned)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //已完成
                    if (orderStatus == (int)EnumHelper.OrderStatus.Complete &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Complete;
                    }
                    break;
                case "bank":
                    //等待付款
                    if (orderStatus == (int)EnumHelper.OrderStatus.UnHandle &&
                        paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                        shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Paying;
                    }
                    //取消订单
                    if (orderStatus == (int)EnumHelper.OrderStatus.Cancel &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Cancel;
                    }
                    //订单锁定
                    if ((orderStatus == (int)EnumHelper.OrderStatus.UserLock || orderStatus == (int)EnumHelper.OrderStatus.AdminLock) &&
                    paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                    shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    // 订单锁定
                    if (orderStatus == (int)EnumHelper.OrderStatus.SystemLock)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    //等待付款确认
                    if (orderStatus == (int)EnumHelper.OrderStatus.UnHandle &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.PreConfirm;
                    }
                    //正在处理
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //配货中
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.Packing)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shipping;
                    }
                    //已发货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.Shipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shiped;
                    }
                    //已确认收货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //退货中
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.RejectedReturning)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //已退货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.RejectedReturned)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //已完成
                    if (orderStatus == (int)EnumHelper.OrderStatus.Complete &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Complete;
                    }
                    break;
                default:
                    //订单已取消
                    if (orderStatus == (int)EnumHelper.OrderStatus.Cancel)
                    {
                        orderType = EnumHelper.OrderMainStatus.Cancel;
                        break;
                    }
                    //等待付款
                    if (orderStatus == (int)EnumHelper.OrderStatus.UnHandle &&
                        paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                        shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Paying;
                    }
                    //取消订单
                    if (orderStatus == (int)EnumHelper.OrderStatus.Cancel &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Cancel;
                    }
                    //订单锁定
                    if ((orderStatus == (int)EnumHelper.OrderStatus.UserLock || orderStatus == (int)EnumHelper.OrderStatus.AdminLock) &&
                    paymentStatus == (int)EnumHelper.PaymentStatus.Unpaid &&
                    shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    // 订单锁定
                    if (orderStatus == (int)EnumHelper.OrderStatus.SystemLock)
                    {
                        orderType = EnumHelper.OrderMainStatus.Locking;
                    }
                    //正在处理
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.UnShipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //配货中
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.Packing)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shipping;
                    }
                    //已发货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.Shipped)
                    {
                        orderType = EnumHelper.OrderMainStatus.Shiped;
                    }
                    //已确认收货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //退货中
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.RejectedReturning)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //已退货
                    if (orderStatus == (int)EnumHelper.OrderStatus.Handling &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.RejectedReturned)
                    {
                        orderType = EnumHelper.OrderMainStatus.Handling;
                    }
                    //已完成
                    if (orderStatus == (int)EnumHelper.OrderStatus.Complete &&
                     paymentStatus == (int)EnumHelper.PaymentStatus.Paid &&
                     shippingStatus == (int)EnumHelper.ShippingStatus.ConfirmShip)
                    {
                        orderType = EnumHelper.OrderMainStatus.Complete;
                    }
                    break;
            }
            return orderType;

        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Order.OrderInfo> GetListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 得到一个对象实体(包括订单项)
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderInfo GetModelInfo(long OrderId)
        {
            Maticsoft.Model.Shop.Order.OrderInfo model = GetModel(OrderId);
            if (model != null)
            {
                Maticsoft.BLL.Shop.Order.OrderItems itemBll = new OrderItems();
                model.OrderItems = itemBll.GetModelList(" OrderId=" + OrderId);
            }
            return model;
        }


        /// <summary>
        /// 得到一个对象实体(包括订单项) 缓存
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderInfo GetModelInfoByCache(long OrderId)
        {

            string CacheKey = "GetModelInfoByCache-" + OrderId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModelInfo(OrderId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Order.OrderInfo)objModel;

        }

        public void RemoveModelInfoCache(long OrderId)
        {
            Maticsoft.Common.DataCache.DeleteCache("OrdersModel-" + OrderId);
            Maticsoft.Common.DataCache.DeleteCache("GetModelInfoByCache-" + OrderId);
        }

        //更新发货数量
        public bool UpdateShipped(Maticsoft.Model.Shop.Order.OrderInfo orderModel)
        {
            return dal.UpdateShipped(orderModel);
        }

        /// <summary>
        /// 根据条件获取对应的订单状态的数量
        /// </summary>
        /// <param name="userid">下单人 ID</param>
        /// <param name="PaymentStatus">支付状态</param>
        /// <returns></returns>
        public int GetPaymentStatusCounts(int userid, int PaymentStatus)
        {
            return dal.GetPaymentStatusCounts(userid, PaymentStatus, (int) EnumHelper.OrderStatus.Cancel);
        } 
       /// <summary>
        /// 获得商家数据列表
       /// </summary>
       /// <param name="supplierid"></param>
       /// <param name="orderStatus"></param>
       /// <param name="orderCode"></param>
       /// <param name="shipName"></param>
       /// <param name="buyerName"></param>
       /// <param name="paymentStatus"></param>
       /// <param name="shippingStatus"></param>
       /// <param name="dateRange"></param>
       /// <param name="startIndex"></param>
       /// <param name="endIndex"></param>
       /// <param name="toalCount"></param>
       /// <returns></returns>
        public List<Maticsoft.Model.Shop.Order.OrderInfo> GetListByPage(int supplierid,int orderStatus, string orderCode, string shipName,
            string buyerName, int paymentStatus, int shippingStatus, string dateRange, int startIndex, int endIndex, out int toalCount)
        {
            StringBuilder strWhere = new StringBuilder();

            if (orderStatus > 0)
            {
                strWhere.Append(GetWhereByOrderStatus(orderStatus));
            }
            if (!string.IsNullOrWhiteSpace(orderCode))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" OrderCode like '%{0}%'", InjectionFilter.SqlFilter(orderCode));
            }
            if (!string.IsNullOrWhiteSpace(shipName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShipName like '%{0}%'", InjectionFilter.SqlFilter(shipName));
            }
            if (!string.IsNullOrWhiteSpace(buyerName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" BuyerName like '%{0}%'", InjectionFilter.SqlFilter(buyerName));
            }
            if (paymentStatus !=  -1 )
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" PaymentStatus = {0}", paymentStatus);
            }
            if (shippingStatus !=  -1 )
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShippingStatus = {0}", shippingStatus);
            }
            if (!string.IsNullOrEmpty(dateRange))
            {
                string[] date = dateRange.Split('_');
                if (date.Length >0)
                {
                    DateTime? dateStart = Globals.SafeDateTime(date[0],null); 
                    if (dateStart.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate >= CONVERT(DATETIME, '{0}') ", dateStart.Value);
                    }
                }
                if (date.Length >1)
                {
                    DateTime? dateEnd = Globals.SafeDateTime(date[1], null);  
                    if (dateEnd.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate <= CONVERT(DATETIME, '{0}')", dateEnd.Value);
                    }
                }
           }
          

            #region 商家
            if (strWhere.Length > 1) strWhere.Append(" and ");
            strWhere.AppendFormat(" SupplierId = {0}", supplierid);
            #endregion

            toalCount = GetRecordCount(strWhere.ToString());
            DataSet ds = dal.GetListByPage(strWhere.ToString(), " CreatedDate desc ", startIndex,endIndex);
            return DataTableToList(ds.Tables[0]);
        }

       /// <summary>
        /// 商家更新订单备注
       /// </summary>
       /// <param name="orderId"></param>
       /// <param name="Remark"></param>
       /// <param name="supplierid"></param>
       /// <returns></returns>
        public bool UpdateOrderRemark(long orderId, string Remark, int supplierid)
        {
            return dal.UpdateOrderRemark(orderId, InjectionFilter.SqlFilter(Remark), string.Format(" and SupplierId= {0}", supplierid));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="agentId">代理商ID</param>
        /// <param name="orderStatus"></param>
        /// <param name="orderCode"></param>
        /// <param name="shipName"></param>
        /// <param name="buyerName"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="shippingStatus"></param>
        /// <param name="dateRange"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="toalCount"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Order.OrderInfo> GetAgentOrderListByPage(int agentId, int orderStatus, string orderCode, string shipName,
            string buyerName, int paymentStatus, int shippingStatus, string dateRange, int startIndex, int endIndex, out int toalCount)
        {
            StringBuilder strWhere = new StringBuilder();

            if (orderStatus > 0)
            {
                strWhere.Append(GetWhereByOrderStatus(orderStatus));
            }
            if (!string.IsNullOrWhiteSpace(orderCode))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" OrderCode like '%{0}%'", InjectionFilter.SqlFilter(orderCode));
            }
            if (!string.IsNullOrWhiteSpace(shipName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShipName like '%{0}%'", InjectionFilter.SqlFilter(shipName));
            }
            if (!string.IsNullOrWhiteSpace(buyerName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" BuyerName like '%{0}%'", InjectionFilter.SqlFilter(buyerName));
            }
            if (paymentStatus != -1)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" PaymentStatus = {0}", paymentStatus);
            }
            if (shippingStatus != -1)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShippingStatus = {0}", shippingStatus);
            }
            if (!string.IsNullOrEmpty(dateRange))
            {
                string[] date = dateRange.Split('_');
                if (date.Length > 0)
                {
                    DateTime? dateStart = Globals.SafeDateTime(date[0], null);
                    if (dateStart.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate >= CONVERT(DATETIME, '{0}') ", dateStart.Value);
                    }
                }
                if (date.Length > 1)
                {
                    DateTime? dateEnd = Globals.SafeDateTime(date[1], null);
                    if (dateEnd.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate <= CONVERT(DATETIME, '{0}')", dateEnd.Value);
                    }
                }
            }

            #region 代理商

            if (strWhere.Length > 1)
            { 
                strWhere.Append(" and ");
            }
            strWhere.AppendFormat(
                "  EXISTS(SELECT * FROM  Shop_Suppliers WHERE SupplierId= T.SupplierId  AND  AgentId={0} ) ", agentId);
            #endregion
            toalCount = GetRecordCount(strWhere.ToString());
            DataSet ds = dal.GetListByPage(strWhere.ToString(), " CreatedDate desc ", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得商家数据列表
        /// </summary>
        public DataSet GetList(int supplierid,int orderStatus, string orderCode, string shipName,
            string buyerName, int paymentStatus, int shippingStatus, string dateRange)
        {
            StringBuilder strWhere = new StringBuilder();
            if (orderStatus > 0)
            {
                strWhere.Append(GetWhereByOrderStatus(orderStatus));
            }
            if (!string.IsNullOrWhiteSpace(orderCode))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" OrderCode like '%{0}%'", InjectionFilter.SqlFilter(orderCode));
            }
            if (!string.IsNullOrWhiteSpace(shipName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShipName like '%{0}%'", InjectionFilter.SqlFilter(shipName));
            }
            if (!string.IsNullOrWhiteSpace(buyerName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" BuyerName like '%{0}%'", InjectionFilter.SqlFilter(buyerName));
            }
            if (paymentStatus != -1)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" PaymentStatus = {0}", paymentStatus);
            }
            if (shippingStatus != -1)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShippingStatus = {0}", shippingStatus);
            }
            if (!string.IsNullOrEmpty(dateRange))
            {
                string[] date = dateRange.Split('_');
                if (date.Length > 0)
                {
                    DateTime? dateStart = Globals.SafeDateTime(date[0], null);
                    if (dateStart.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate >= CONVERT(DATETIME, '{0}') ", dateStart.Value);
                    }
                }
                if (date.Length > 1)
                {
                    DateTime? dateEnd = Globals.SafeDateTime(date[1], null);
                    if (dateEnd.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate <= CONVERT(DATETIME, '{0}')", dateEnd.Value);
                    }
                }
            }
            #region 商家
            if (strWhere.Length > 1) strWhere.Append(" and ");
            strWhere.AppendFormat(" SupplierId = {0}", supplierid);
            #endregion
           return dal.GetList(0, strWhere.ToString(), " CreatedDate desc ");  
        }

        /// <summary>
        /// 获得代理商数据
        /// </summary>
        public DataSet GetListByAgent(int agentId, int orderStatus, string orderCode, string shipName,
            string buyerName, int paymentStatus, int shippingStatus, string dateRange)
        {
            StringBuilder strWhere = new StringBuilder();
            if (orderStatus > 0)
            {
                strWhere.Append(GetWhereByOrderStatus(orderStatus));
            }
            if (!string.IsNullOrWhiteSpace(orderCode))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" OrderCode like '%{0}%'", InjectionFilter.SqlFilter(orderCode));
            }
            if (!string.IsNullOrWhiteSpace(shipName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShipName like '%{0}%'", InjectionFilter.SqlFilter(shipName));
            }
            if (!string.IsNullOrWhiteSpace(buyerName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" BuyerName like '%{0}%'", InjectionFilter.SqlFilter(buyerName));
            }
            if (paymentStatus != -1)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" PaymentStatus = {0}", paymentStatus);
            }
            if (shippingStatus != -1)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShippingStatus = {0}", shippingStatus);
            }
            if (!string.IsNullOrEmpty(dateRange))
            {
                string[] date = dateRange.Split('_');
                if (date.Length > 0)
                {
                    DateTime? dateStart = Globals.SafeDateTime(date[0], null);
                    if (dateStart.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate >= CONVERT(DATETIME, '{0}') ", dateStart.Value);
                    }
                }
                if (date.Length > 1)
                {
                    DateTime? dateEnd = Globals.SafeDateTime(date[1], null);
                    if (dateEnd.HasValue)
                    {
                        if (strWhere.Length > 1)
                        {
                            strWhere.Append(" and ");
                        }
                        strWhere.AppendFormat(" CreatedDate <= CONVERT(DATETIME, '{0}')", dateEnd.Value);
                    }
                }
            }
            #region 代理商

            if (strWhere.Length > 1)
            {
                strWhere.Append(" and ");
            }
            strWhere.AppendFormat(
                "  EXISTS ( SELECT * FROM  Shop_Suppliers WHERE SupplierId= T.SupplierId  AND  AgentId={0} ) ", agentId);
            #endregion
            return dal.GetList(0, strWhere.ToString(), " CreatedDate desc ");
        }


        /// <summary>
        /// 根据商家订单状态获取查询订单的条件
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public string GetWhereByOrderStatus(int orderType)
        {
            string strWhere = "";
            switch (orderType)
            {
                //等待处理    未处理订单：
//A. 在线支付: 已支付/未配货/未发货
//B. 货到付款: 未支付/未配货/未发货
                case (int)EnumHelper.StoreOrderStatus.PreHandle:
                    strWhere  = 
                      String.Format(
                          " OrderStatus={0}  and PaymentStatus in ( {1},{2} ) and ShippingStatus={3} ",
                          (int)EnumHelper.OrderStatus.UnHandle, (int)EnumHelper.PaymentStatus.Unpaid,(int)EnumHelper.PaymentStatus.Paid,
                          (int)EnumHelper.ShippingStatus.UnShipped );
                    break;
                //未完成
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.StoreOrderStatus.NotComplete:
                    strWhere =
                      String.Format(
                      "( OrderStatus!={0}  or  ShippingStatus!={1})",
                      (int)EnumHelper.OrderStatus.Complete,
                      (int)EnumHelper.ShippingStatus.ConfirmShip);
                    break;
                //已完成
                case (int)Maticsoft.Model.Shop.Order.EnumHelper.StoreOrderStatus.Complete:
                    strWhere =
                  String.Format(
                      " OrderStatus={0}  and  ShippingStatus={1}",
                      (int)EnumHelper.OrderStatus.Complete,
                      (int)EnumHelper.ShippingStatus.ConfirmShip);
                    break;
                default:
                    break;
            }
            return strWhere;
        }

        public Maticsoft.Model.Shop.Order.OrderInfo GetOrderInfo(string ordercode)
        {
            return dal.GetOrderInfo(ordercode);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public List<Maticsoft.Model.Shop.Order.OrderInfo> GetOrderInfo(long OrderId)
        {
            DataSet ds = dal.GetOrderInfo(OrderId);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            return null;
        }

        /// <summary>
        /// 订单退货更新
        /// </summary>
        /// <returns></returns>
        public bool OrderReturnUpdate(long orderId)
        {
            return dal.OrderReturnUpdate(orderId);
        }

        #endregion  ExtensionMethod
    }
}

