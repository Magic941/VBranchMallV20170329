using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public class DAShopCoupon : DataAccessBase
    {
        /// <summary>
        /// 创建CouponInfo数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Coupon.ICouponInfo CreateCouponInfo()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Coupon.CouponInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Coupon.ICouponInfo)objType;
        }

        /// <summary>
        /// 创建CouponClass数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Coupon.ICouponClass CreateCouponClass()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Coupon.CouponClass";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Coupon.ICouponClass)objType;
        }
        /// <summary>
        /// 创建CouponRule数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Coupon.ICouponRule CreateCouponRule()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Coupon.CouponRule";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Coupon.ICouponRule)objType;
        }

        /// <summary>
        /// 创建CouponHistory数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Coupon.ICouponHistory CreateCouponHistory()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Coupon.CouponHistory";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Coupon.ICouponHistory)objType;
        }

        /// <summary>
        /// 创建Shop_CouponRuleExt数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Coupon.IShop_CouponRuleExt CreateShop_CouponRuleExt()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Coupon.Shop_CouponRuleExt";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Coupon.IShop_CouponRuleExt)objType;
        }

    }
}
