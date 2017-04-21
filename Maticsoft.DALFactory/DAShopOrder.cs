using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class DAShopOrder : DataAccessBase
    {
        /// <summary>
        /// 创建OrderAction数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderAction CreateOrderAction()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderAction";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderAction)objType;
        }

        /// <summary>
        /// 创建OrderItem数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderItems CreateOrderItem()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderItems";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderItems)objType;
        }

        /// <summary>
        /// 创建OrderLookupItems数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderLookupItems CreateOrderLookupItems()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderLookupItems";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderLookupItems)objType;
        }


        /// <summary>
        /// 创建OrderLookupList数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderLookupList CreateOrderLookupList()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderLookupList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderLookupList)objType;
        }

        /// <summary>
        /// 创建OrderOptions数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderOptions CreateOrderOptions()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderOptions";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderOptions)objType;
        }

        /// <summary>
        /// 创建OrderRemark数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderRemark CreateOrderRemark()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderRemark";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderRemark)objType;
        }

        /// <summary>
        /// 创建OrderRemark数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrders CreateOrders()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.Orders";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrders)objType;
        }

        /// <summary>
        /// 创建OrderService数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderService CreateOrderService()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderService";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderService)objType;
        }

        /// <summary>
        /// 创建OrdersHistory数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrdersHistory CreateOrdersHistory()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Order.OrdersHistory";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrdersHistory)objType;
        }
        /// <summary>
        /// 创建Shop_PaymentNumber数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.PaymentNumber.IShop_PaymentNumber CreateShop_PaymentNumber()
        {

            string ClassNamespace = AssemblyPath + ".Shop.PaymentNumber.Shop_PaymentNumber";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.PaymentNumber.IShop_PaymentNumber)objType;
        }


        /// <summary>
        /// 创建OrderReturnGoods数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderReturnGoods CreateOrderReturnGoods()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderReturnGoods";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderReturnGoods)objType;
        }


        /// <summary>
        /// 创建OrderReturnGoodsItem数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IOrderReturnGoodsItem CreateOrderReturnGoodsItem()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Order.OrderReturnGoodsItem";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IOrderReturnGoodsItem)objType;
        }

        /// <summary>
        /// 创建Shop_ReturnOrderAction数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Order.IShop_ReturnOrderAction CreateShop_ReturnOrderAction()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Order.Shop_ReturnOrderAction";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Order.IShop_ReturnOrderAction)objType;
        }
    }
}
