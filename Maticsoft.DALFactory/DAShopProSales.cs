using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class DAShopProSales : DataAccessBase
    {
        /// <summary>
        /// 创建CountDown数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.PromoteSales.ICountDown CreateCountDown()
        {
            string ClassNamespace = AssemblyPath + ".Shop.PromoteSales.CountDown";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.PromoteSales.ICountDown)objType;
        }

        /// <summary>
        /// 创建GroupBuy数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.PromoteSales.IGroupBuy CreateGroupBuy()
        {
            string ClassNamespace = AssemblyPath + ".Shop.PromoteSales.GroupBuy";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.PromoteSales.IGroupBuy)objType;
        }

        /// <summary>
        /// 创建Shop_WeiXinGroupBuy数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.PromoteSales.IWeiXinGroupBuy CreateShop_WeiXinGroupBuy()
        {

            string ClassNamespace = AssemblyPath + ".Shop.PromoteSales.WeiXinGroupBuy";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.PromoteSales.IWeiXinGroupBuy)objType;
        }


    }
}
