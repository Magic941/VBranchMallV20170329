/**
* OrdersHistory.cs
*
* 功 能： N/A
* 类 名： OrdersHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/7 12:05:24   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// OrdersHistory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrdersHistory
	{
		public OrdersHistory()
		{}
        #region Model
        private long _orderid;
        private string _ordercode;
        private long _parentorderid;
        private DateTime _createddate;
        private DateTime? _updateddate;
        private int _buyerid;
        private string _buyername;
        private string _buyeremail;
        private string _buyercellphone;
        private int? _regionid;
        private string _shipregion;
        private string _shipaddress;
        private string _shipzipcode;
        private string _shipname;
        private string _shiptelphone;
        private string _shipcellphone;
        private string _shipemail;
        private int? _shippingmodeid;
        private string _shippingmodename;
        private int? _realshippingmodeid;
        private string _realshippingmodename;
        private int? _shipperid;
        private string _shippername;
        private string _shipperaddress;
        private string _shippercellphone;
        private decimal? _freight;
        private decimal? _freightadjusted;
        private decimal? _freightactual;
        private int? _weight;
        private int _shippingstatus;
        private string _shipordernumber;
        private string _expresscompanyname;
        private string _expresscompanyabb;
        private int _paymenttypeid;
        private string _paymenttypename;
        private string _paymentgateway;
        private int _paymentstatus;
        private int _refundstatus;
        private string _paycurrencycode;
        private string _paycurrencyname;
        private decimal? _paymentfee;
        private decimal? _paymentfeeadjusted;
        private string _gatewayorderid;
        private decimal _ordertotal;
        private int _orderpoint;
        private decimal? _ordercostprice;
        private decimal? _orderprofit;
        private decimal? _orderothercost;
        private decimal? _orderoptionprice;
        private string _discountname;
        private decimal? _discountamount;
        private decimal? _discountadjusted;
        private decimal? _discountvalue;
        private int? _discountvaluetype;
        private string _couponcode;
        private string _couponname;
        private decimal? _couponamount;
        private decimal? _couponvalue;
        private int? _couponvaluetype;
        private string _activityname;
        private decimal? _activityfreeamount;
        private int _activitystatus;
        private int? _groupbuyid;
        private decimal? _groupbuyprice;
        private int _groupbuystatus;
        private decimal _amount;
        private int _ordertype;
        private int _orderstatus;
        private int? _sellerid;
        private string _sellername;
        private string _selleremail;
        private string _sellercellphone;
        private int? _supplierid;
        private string _suppliername;
        private string _referid;
        private string _referurl;
        private string _orderip;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public long OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ParentOrderId
        {
            set { _parentorderid = value; }
            get { return _parentorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdatedDate
        {
            set { _updateddate = value; }
            get { return _updateddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BuyerID
        {
            set { _buyerid = value; }
            get { return _buyerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuyerName
        {
            set { _buyername = value; }
            get { return _buyername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuyerEmail
        {
            set { _buyeremail = value; }
            get { return _buyeremail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuyerCellPhone
        {
            set { _buyercellphone = value; }
            get { return _buyercellphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipRegion
        {
            set { _shipregion = value; }
            get { return _shipregion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipAddress
        {
            set { _shipaddress = value; }
            get { return _shipaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipZipCode
        {
            set { _shipzipcode = value; }
            get { return _shipzipcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipName
        {
            set { _shipname = value; }
            get { return _shipname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipTelPhone
        {
            set { _shiptelphone = value; }
            get { return _shiptelphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipCellPhone
        {
            set { _shipcellphone = value; }
            get { return _shipcellphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipEmail
        {
            set { _shipemail = value; }
            get { return _shipemail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ShippingModeId
        {
            set { _shippingmodeid = value; }
            get { return _shippingmodeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShippingModeName
        {
            set { _shippingmodename = value; }
            get { return _shippingmodename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RealShippingModeId
        {
            set { _realshippingmodeid = value; }
            get { return _realshippingmodeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RealShippingModeName
        {
            set { _realshippingmodename = value; }
            get { return _realshippingmodename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ShipperId
        {
            set { _shipperid = value; }
            get { return _shipperid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipperName
        {
            set { _shippername = value; }
            get { return _shippername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipperAddress
        {
            set { _shipperaddress = value; }
            get { return _shipperaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipperCellPhone
        {
            set { _shippercellphone = value; }
            get { return _shippercellphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Freight
        {
            set { _freight = value; }
            get { return _freight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FreightAdjusted
        {
            set { _freightadjusted = value; }
            get { return _freightadjusted; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FreightActual
        {
            set { _freightactual = value; }
            get { return _freightactual; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShippingStatus
        {
            set { _shippingstatus = value; }
            get { return _shippingstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipOrderNumber
        {
            set { _shipordernumber = value; }
            get { return _shipordernumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpressCompanyName
        {
            set { _expresscompanyname = value; }
            get { return _expresscompanyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpressCompanyAbb
        {
            set { _expresscompanyabb = value; }
            get { return _expresscompanyabb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PaymentTypeId
        {
            set { _paymenttypeid = value; }
            get { return _paymenttypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentTypeName
        {
            set { _paymenttypename = value; }
            get { return _paymenttypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentGateway
        {
            set { _paymentgateway = value; }
            get { return _paymentgateway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PaymentStatus
        {
            set { _paymentstatus = value; }
            get { return _paymentstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RefundStatus
        {
            set { _refundstatus = value; }
            get { return _refundstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayCurrencyCode
        {
            set { _paycurrencycode = value; }
            get { return _paycurrencycode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayCurrencyName
        {
            set { _paycurrencyname = value; }
            get { return _paycurrencyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PaymentFee
        {
            set { _paymentfee = value; }
            get { return _paymentfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PaymentFeeAdjusted
        {
            set { _paymentfeeadjusted = value; }
            get { return _paymentfeeadjusted; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GatewayOrderId
        {
            set { _gatewayorderid = value; }
            get { return _gatewayorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrderTotal
        {
            set { _ordertotal = value; }
            get { return _ordertotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrderPoint
        {
            set { _orderpoint = value; }
            get { return _orderpoint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OrderCostPrice
        {
            set { _ordercostprice = value; }
            get { return _ordercostprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OrderProfit
        {
            set { _orderprofit = value; }
            get { return _orderprofit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OrderOtherCost
        {
            set { _orderothercost = value; }
            get { return _orderothercost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OrderOptionPrice
        {
            set { _orderoptionprice = value; }
            get { return _orderoptionprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiscountName
        {
            set { _discountname = value; }
            get { return _discountname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DiscountAmount
        {
            set { _discountamount = value; }
            get { return _discountamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DiscountAdjusted
        {
            set { _discountadjusted = value; }
            get { return _discountadjusted; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DiscountValue
        {
            set { _discountvalue = value; }
            get { return _discountvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DiscountValueType
        {
            set { _discountvaluetype = value; }
            get { return _discountvaluetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CouponCode
        {
            set { _couponcode = value; }
            get { return _couponcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CouponName
        {
            set { _couponname = value; }
            get { return _couponname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CouponAmount
        {
            set { _couponamount = value; }
            get { return _couponamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CouponValue
        {
            set { _couponvalue = value; }
            get { return _couponvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CouponValueType
        {
            set { _couponvaluetype = value; }
            get { return _couponvaluetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ActivityName
        {
            set { _activityname = value; }
            get { return _activityname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ActivityFreeAmount
        {
            set { _activityfreeamount = value; }
            get { return _activityfreeamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ActivityStatus
        {
            set { _activitystatus = value; }
            get { return _activitystatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GroupBuyId
        {
            set { _groupbuyid = value; }
            get { return _groupbuyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? GroupBuyPrice
        {
            set { _groupbuyprice = value; }
            get { return _groupbuyprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GroupBuyStatus
        {
            set { _groupbuystatus = value; }
            get { return _groupbuystatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrderStatus
        {
            set { _orderstatus = value; }
            get { return _orderstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SellerID
        {
            set { _sellerid = value; }
            get { return _sellerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellerName
        {
            set { _sellername = value; }
            get { return _sellername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellerEmail
        {
            set { _selleremail = value; }
            get { return _selleremail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellerCellPhone
        {
            set { _sellercellphone = value; }
            get { return _sellercellphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReferID
        {
            set { _referid = value; }
            get { return _referid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReferURL
        {
            set { _referurl = value; }
            get { return _referurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderIP
        {
            set { _orderip = value; }
            get { return _orderip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

	}
}

