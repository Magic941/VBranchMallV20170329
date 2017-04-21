using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Order
{
    /// <summary>
    /// 接口层OrderReturnGoods
    /// </summary>
    public interface IOrderReturnGoods
    {
        #region  成员方法
        bool UpdateInformation(Maticsoft.Model.Shop.Order.OrderReturnGoods model);
        bool UpdatePrice(Maticsoft.Model.Shop.Order.OrderReturnGoods model);
        bool Detail_Update(Maticsoft.Model.Shop.Order.OrderReturnGoods model);
        bool Detail(Maticsoft.Model.Shop.Order.OrderReturnGoods model);
        bool RefuseReturnGoods(Maticsoft.Model.Shop.Order.OrderReturnGoods model);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long Id);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        long Add(Maticsoft.Model.Shop.Order.OrderReturnGoods model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Order.OrderReturnGoods model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long Id);
        bool DeleteList(string Idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Order.OrderReturnGoods GetModel(long Id);
        Maticsoft.Model.Shop.Order.OrderReturnGoods DataRowToModel(DataRow row);
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

        bool OrderReturnRepair(Maticsoft.Model.Shop.Order.OrderReturnGoods model);

        #endregion  成员方法
        #region  MethodEx
        int ApproveReturnOrder(int status, long returnId, string peason, string remark);

        /// <summary>
        /// 退款操作
        /// </summary>
        int ReturnAccount(int status, long returnId, string accountPeason, string remark);
        #endregion  MethodEx
    }
}
