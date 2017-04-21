using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class DAShopSupplier : DataAccessBase
    {

        /// <summary>
        /// 创建数据层接口
        /// </summary>
        //public static t Create(string ClassName)
        //{
        //string ClassNamespace = AssemblyPath +"."+ ClassName;
        //object objType = CreateObject(AssemblyPath, ClassNamespace);
        //return (t)objType;
        //}
        /// <summary>
        /// 创建SupplierCategories数据层接口。供应商(店铺)分类
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierCategories CreateSupplierCategories()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierCategories";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierCategories)objType;
        }


        /// <summary>
        /// 创建SupplierConfig数据层接口。供应商(店铺)配置
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierConfig CreateSupplierConfig()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierConfig";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierConfig)objType;
        }


        /// <summary>
        /// 创建SupplierMenus数据层接口。供应商(店铺)导航
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierMenus CreateSupplierMenus()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierMenus";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierMenus)objType;
        }


        /// <summary>
        /// 创建SupplierRank数据层接口。供应商(店铺)等级
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierRank CreateSupplierRank()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierRank";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierRank)objType;
        }


        /// <summary>
        /// 创建SupplierRankThemes数据层接口。供应商(店铺)等级与
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierRankThemes CreateSupplierRankThemes()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierRankThemes";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierRankThemes)objType;
        }


        /// <summary>
        /// 创建Suppliers数据层接口。供应商
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierInfo CreateSupplierInfo()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierInfo)objType;
        }


        /// <summary>
        /// 创建SupplierScoreDetails数据层接口。供应商(店铺)评分明
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierScoreDetails CreateSupplierScoreDetails()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierScoreDetails";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierScoreDetails)objType;
        }


        /// <summary>
        /// 创建SupplierThemes数据层接口。供应商(店铺)模版
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierThemes CreateSupplierThemes()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierThemes";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierThemes)objType;
        }


        /// <summary>
        /// 创建SuppProductCategories数据层接口。供应商(店铺)分类与
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISuppProductCategories CreateSuppProductCategories()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SuppProductCategories";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISuppProductCategories)objType;
        }
        /// <summary>
        /// 创建SupplierAD数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISupplierAD CreateSupplierAD()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SupplierAD";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISupplierAD)objType;
        }
        

        /// <summary>
        /// 创建SuppProductStatModes数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Supplier.ISuppProductStatModes CreateSuppProductStatModes()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Supplier.SuppProductStatModes";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Supplier.ISuppProductStatModes)objType;
        }
    }
}
