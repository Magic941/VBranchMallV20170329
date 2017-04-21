using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Coupon
{
    /// <summary>
    /// Shop_CouponRuleExt:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Shop_CouponRuleExt
    {
        public Shop_CouponRuleExt()
        { }
        #region Model
        private int _id;
        private string _batchid;
        private int _classid;
        private int _couponcount;

        public int UserID { set; get; }

        public string CouponName{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string batchID
        {
            set { _batchid = value; }
            get { return _batchid; }
        }

        /// <summary>
        /// 优惠券类别
        /// </summary>
        public int ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }

        /// <summary>
        /// 优惠券数量
        /// </summary>
        public int CouponCount
        {
            set { _couponcount = value; }
            get { return _couponcount; }
        }
        #endregion Model

    }
}
