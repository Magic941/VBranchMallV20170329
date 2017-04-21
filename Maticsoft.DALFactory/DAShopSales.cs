using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class DAShopSales : DataAccessBase
    {
        /// <summary>
        /// 创建SalesRule数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Sales.ISalesRule CreateSalesRule()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Sales.SalesRule";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Sales.ISalesRule)objType;
        }


        /// <summary>CreateSalesItem
        /// 创建SalesItem数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Sales.ISalesItem CreateSalesItem()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Sales.SalesItem";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Sales.ISalesItem)objType;
        }
        /// <summary>
        /// 创建SalesUserRank数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Sales.ISalesUserRank CreateSalesUserRank()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Sales.SalesUserRank";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Sales.ISalesUserRank)objType;
        }
        /// <summary>
        /// 创建SalesRuleProduct数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Sales.ISalesRuleProduct CreateSalesRuleProduct()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Sales.SalesRuleProduct";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Sales.ISalesRuleProduct)objType;
        }


        /// <summary>
        /// 创建ExpressTemplate数据层接口。
        /// </summary>
        public static IDAL.Shop.Sales.IExpressTemplate CreateExpressTemplate()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Sales.ExpressTemplate";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAL.Shop.Sales.IExpressTemplate)objType;
        }

        /// <summary>
        /// 创建ShipperInfo数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Sales.IShipperInfo CreateShipperInfo()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Sales.ShipperInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Sales.IShipperInfo)objType;
        }

    }
}
