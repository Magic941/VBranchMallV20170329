using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class DAShopCard:DataAccessBase
    {

        /// <summary>
        /// 创建Shop_CardType数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.IShop_CardType CreateShop_CardType()
        {

            string ClassNamespace = AssemblyPath + ".Card.Shop_CardType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.IShop_CardType)objType;
        }


        /// <summary>
        /// 创建Shop_Card数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.IShop_Card CreateShop_Card()
        {

            string ClassNamespace = AssemblyPath + ".Card.Shop_Card";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.IShop_Card)objType;
        }


        /// <summary>
        /// 创建Shop_CardUserInfo数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.IShop_CardUserInfo CreateShop_CardUserInfo()
        {

            string ClassNamespace = AssemblyPath + ".Card.Shop_CardUserInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.IShop_CardUserInfo)objType;
        }
    }
}
