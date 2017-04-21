using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Order
{
    /// <summary>
    /// 接口层OrderItem
    /// </summary>
    public interface IOrderItems
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ItemId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        long Add(Maticsoft.Model.Shop.Order.OrderItems model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Order.OrderItems model);
        bool updateOrader2(Maticsoft.Model.Shop.Order.OrderItems model);
        /// <summary>
        /// 更新订单的退货状态和退货数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateStatus(Maticsoft.Model.Shop.Order.OrderItems model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long ItemId);
        bool DeleteList(string ItemIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Order.OrderItems GetModel(long ItemId);
        Maticsoft.Model.Shop.Order.OrderItems DataRowToModel(DataRow row);
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
        DataSet GetSaleRecordByPage(long productId, string orderby, int startIndex, int endIndex);

        int GetSaleRecordCount(long productId);
        #endregion  MethodEx

        DataSet GetCommission(decimal DErate, decimal CPrate);

        #region 查询商品的SKU对应的商品属性和值
        /// <summary>
        /// 查询商品的SKU对应的商品属性和值
        /// </summary>
        /// <param name="SKU"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        DataSet GetProductSkuItemAttributeValues(string SKU, long ProductId);

        #endregion

        bool UpdateOrderRereturnStatus(long orderItemId, Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus itemReturnStatus, int returnQty, Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType returnGoodsType);
    }
}
