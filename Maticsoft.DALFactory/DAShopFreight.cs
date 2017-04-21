using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public class DAShopFreight:DataAccessBase
    {
        //}
        /// <summary>
        /// 创建Shop_freefreight数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.shop.Freight.IShop_ProductsFreight CreateShop_ProductsFreight()
        {

            string ClassNamespace = AssemblyPath + ".shop.Freight.Shop_ProductsFreight";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.shop.Freight.IShop_ProductsFreight)objType;
        }

    }
}
