using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Order
{
    /// <summary>
    /// 接口层Orders
    /// </summary>
    public interface IOrders
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long OrderId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        long Add(Maticsoft.Model.Shop.Order.OrderInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Order.OrderInfo model);

          /// <summary>
        /// 更新一条数据(liyongqin)
        /// </summary>
        bool UpdateGetaway(Maticsoft.Model.Shop.Order.OrderInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long OrderId);
        bool DeleteList(string OrderIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Order.OrderInfo GetModel(long OrderId);

        //Maticsoft.Model.Shop.Order.OrderInfo GetOrderInfoModel(long OrderId);

        Maticsoft.Model.Shop.Order.OrderInfo DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        /// <summary>
        /// 获得前几行数据,并获取支付日期
        /// </summary>
        DataSet GetList2(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);

        #endregion  成员方法

        /// <summary>
        /// 获取指定父订单号和订单详细中商品编号的订单信息
        /// </summary>
        /// <param name="OrderID">父订单ID</param>
        /// <param name="ProductID">订单详情商品ID</param>
        /// <returns></returns>
        DataTable GetChildOrder(Int64 OrderID, Int64 ProductID);

        /// <summary>
        /// 获取指定订单的商品种类是否超过一种，超过返回True，反之，返回False
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <returns></returns>
        bool GetProductTypeCount(Int64 OrderID);

        #region  MethodEx
        bool UpdateOrderStatus(long orderId, int status);

        bool ReturnStatus(long orderId);

        bool UpdateShipped(Maticsoft.Model.Shop.Order.OrderInfo orderModel);

        /// <summary>
        /// 根据条件获取对应的订单状态的数量
        /// </summary>
        /// <param name="userid">下单人 ID</param>
        /// <param name="PaymentStatus">支付状态</param>
        /// <param name="OrderStatusCancel">订单的取消状态</param>
        /// <returns></returns>
        int GetPaymentStatusCounts(int userid, int PaymentStatus, int OrderStatusCancel);

        /// <summary>
        /// 更新订单备注
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateOrderRemark(long orderId, string Remark, string strWhere);

        Maticsoft.Model.Shop.Order.OrderInfo GetOrderInfo(string ordercode);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        DataSet GetOrderInfo(long OrderId);

        /// <summary>
        /// 订单退货更新订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        bool OrderReturnUpdate(long orderId);
        #endregion  MethodEx
    }
}
