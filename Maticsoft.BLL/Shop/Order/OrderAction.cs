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
	/// OrderAction
	/// </summary>
	public partial class OrderAction
	{
        private readonly IOrderAction dal = DAShopOrder.CreateOrderAction();
		public OrderAction()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ActionId)
        {
            return dal.Exists(ActionId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderAction model)
        {
            return dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrderAction> GetNoCompleteOrder()
        {
            DataSet ds = dal.GetNoCompleteOrder();
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrderAction model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ActionId)
        {

            return dal.Delete(ActionId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ActionIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ActionIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderAction GetModel(long ActionId)
        {

            return dal.GetModel(ActionId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderAction GetModelByCache(long ActionId)
        {

            string CacheKey = "OrderActionModel-" + ActionId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ActionId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Order.OrderAction)objModel;
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
        public List<Maticsoft.Model.Shop.Order.OrderAction> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Order.OrderAction> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderAction> modelList = new List<Maticsoft.Model.Shop.Order.OrderAction>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Order.OrderAction model;
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

        //获取
	    #region  ExtensionMethod
      
	    public static string GetActionCode(string actionCode)
	    {   
	        switch (Globals.SafeInt(actionCode, 0))
	        {
	            case (int)EnumHelper.ActionCode.CustomersCancel:
	                return "客户取消订单";
                case (int)EnumHelper.ActionCode.CustomersComplete:
                    return "客户完成订单";
                case (int)EnumHelper.ActionCode.CustomersCreateOrder:
                    return "客户创建订单";
                case (int)EnumHelper.ActionCode.CustomersPay:
                    return "客户支付订单";
                case (int)EnumHelper.ActionCode.SellerCancel:
                    return "商家取消订单";
                case (int)EnumHelper.ActionCode.SellerComplete:
                    return "商家完成订单";
                case (int)EnumHelper.ActionCode.SellerPacking:
                    return "商家配货操作";
                case (int)EnumHelper.ActionCode.SellerShipped:
                    return "商家发货操作";
                case (int)EnumHelper.ActionCode.SellerUpdateShip:
                    return "商家修改收货信息";
                case (int)EnumHelper.ActionCode.SystemCancel:
                    return "系统取消订单";
                case (int)EnumHelper.ActionCode.SystemComplete:
                    return "系统完成订单";
                case (int)EnumHelper.ActionCode.SystemPacking:
	                return "系统配货操作";
                case (int)EnumHelper.ActionCode.SystemPay:
                    return "系统支付订单";
                case (int)EnumHelper.ActionCode.SystemShipped:
                    return "系统发货操作";
                case (int)EnumHelper.ActionCode.SystemUpdateShip:
                    return "系统修改收货信息";
                case (int)EnumHelper.ActionCode.SystemUpdateAmount:
                    return "系统变更应付金额";
                case (int)EnumHelper.ActionCode.AgentComplete:
                    return "代理商完成订单";
                case (int)EnumHelper.ActionCode.AgentPacking:
                    return "代理商配货操作";
                case (int)EnumHelper.ActionCode.AgentCancel:
                    return "代理商取消订单";
                case (int)EnumHelper.ActionCode.AgentShipped:
                    return "代理商发货操作";
                case (int)EnumHelper.ActionCode.AgentUpdateShip:
                    return "代理商修改收货信息";
	        }
	        return "";
	    }

	    #endregion  ExtensionMethod
	}
}

