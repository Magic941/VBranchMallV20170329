using System;
namespace Maticsoft.Model.Shop.Gift
{
	/// <summary>
	/// ExchangeDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ExchangeDetail
	{
		public ExchangeDetail()
		{}
        #region Model
        private int _detailid;
        private int _type;
        private int _giftid;
        private int _userid;
        private int? _orderid;
        private string _giftname;
        private decimal? _price;
        private string _couponcode;
        private int _costscore;
        private int _status;
        private string _description;
        private DateTime _createddate;
        /// <summary>
        /// 兑换明细ID
        /// </summary>
        public int DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
        /// <summary>
        /// 兑换类型 1：表示优惠券兑换  2：表示礼品兑换
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 礼品ID
        /// </summary>
        public int GiftID
        {
            set { _giftid = value; }
            get { return _giftid; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 订单ID 备注：可为空，比如优惠券就不要订单流程
        /// </summary>
        public int? OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 礼品名称
        /// </summary>
        public string GiftName
        {
            set { _giftname = value; }
            get { return _giftname; }
        }
        /// <summary>
        /// 兑换卷面值
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 优惠券
        /// </summary>
        public string CouponCode
        {
            set { _couponcode = value; }
            get { return _couponcode; }
        }
        /// <summary>
        /// 兑换花费积分
        /// </summary>
        public int CostScore
        {
            set { _costscore = value; }
            get { return _costscore; }
        }
        /// <summary>
        /// 0:未审核，1：审核通过，2：已发货，3：已完成
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 兑换详情
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 兑换申请时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        #endregion Model

	}
}

