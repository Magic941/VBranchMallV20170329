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
    /// OrderReturnGoodsItem
    /// </summary>
    public partial class OrderReturnGoodsItem
    {
        private readonly IOrderReturnGoodsItem dal = DAShopOrder.CreateOrderReturnGoodsItem();
        public OrderReturnGoodsItem()
        { }
        #region  BasicMethod
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
        public long Add(Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model)
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
        public Maticsoft.Model.Shop.Order.OrderReturnGoodsItem GetModel(long Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderReturnGoodsItem GetModelByCache(long Id)
        {

            string CacheKey = "OrderReturnGoodsItemModel-" + Id;
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
            return (Maticsoft.Model.Shop.Order.OrderReturnGoodsItem)objModel;
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
        public List<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem> modelList = new List<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model;
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

        public Maticsoft.Model.Shop.Order.OrderReturnGoodsItem GetReturnGoodItemModel(long Id)
        {
            return dal.GetReturnGoodItemModel(Id);
        }


        #endregion  BasicMethod
        #region  ExtensionMethod

        public List<Maticsoft.Model.Shop.Order.OrderReturnGoodsItem> GetOrderReturnGoodsItems(long userId, int startIndex, int endIndex, out int totalCount)
        {
            DataSet ds = dal.GetListByPage(string.Format(" UserID={0} ", userId), "", startIndex, endIndex);
            totalCount = dal.GetRecordCount(string.Format(" UserID={0} ", userId));
            return this.DataTableToList(ds.Tables[0]);
        }
        #endregion  ExtensionMethod
    }
}

