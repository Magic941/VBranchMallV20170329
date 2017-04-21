using System;
namespace Maticsoft.Model.Shop.Order
{

    /// <summary>
    /// Orders:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OrderInfo
    {
        public OrderInfo()
        {
            this.ActionDate = DateTime.MinValue;
        }
        #region Model
        private long _orderid;
        private string _ordercode;
        private long _parentorderid = -1;
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
        private int _shippingstatus = 0;
        private string _shipordernumber;
        private string _expresscompanyname;
        private string _expresscompanyabb;
        private int _paymenttypeid;
        private string _paymenttypename;
        private string _paymentgateway;
        private int _paymentstatus = 0;
        private int _refundstatus = 0;
        private string _paycurrencycode;
        private string _paycurrencyname;
        private decimal? _paymentfee;
        private decimal? _paymentfeeadjusted;
        private string _gatewayorderid;
        private decimal _ordertotal = 0M;
        private int _orderpoint = 0;
        private decimal? _ordercostprice;
        private decimal? _ordercostprice2;
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
        private int _activitystatus = 0;
        private int? _groupbuyid;
        private decimal? _groupbuyprice;
        private int _groupbuystatus = 0;
        private decimal _amount = 0M;
        private int _ordertype = 1;
        private int _orderstatus = 0;
        private int? _sellerid;
        private string _sellername;
        private string _selleremail;
        private string _sellercellphone;
        private int _commentstatus = 0;
        private int? _supplierid;
        private string _suppliername;
        private string _referid;
        private string _referurl;
        private string _orderip;
        private string _remark;
        private decimal _producttotal = 0M;
        private bool _haschildren = false;
        private bool _isreviews = false;
        private int? _cardsysid;
        private string _paymentnumber;
        private string _cardNo;
        private bool _hasreturn;

        /// <summary>
        /// 是否退货
        /// </summary>
        public bool HasReturn
        {
            get { return _hasreturn; }
            set { _hasreturn = value; }
        }

        //微信订单 1 正常订单0
        private int _OrderOptType;
        /// <summary>
        ///   //微信订单 1 正常订单0
        /// </summary>
        public int OrderOptType
        {
            get { return _OrderOptType; }
            set { _OrderOptType = value; }
        }
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        public string Paymentnumber
        {
            get { return _paymentnumber; }
            set { _paymentnumber = value; }
        }

        /// <summary>
        ///好邻卡系统编号 
        /// </summary>
        public int? CardSysId
        {
            set { _cardsysid = value; }
            get { return _cardsysid; }
        }

        /// <summary>
        ///好邻卡号 
        /// </summary>
        public string CardNo
        {
            set { _cardNo = value; }
            get { return _cardNo; }
        }

        /// <summary>
        /// 订单流水ID
        /// </summary>
        public long OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 订单自定义单号
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }
        /// <summary>
        /// 父订单ID
        /// </summary>
        public long ParentOrderId
        {
            set { _parentorderid = value; }
            get { return _parentorderid; }
        }
        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? UpdatedDate
        {
            set { _updateddate = value; }
            get { return _updateddate; }
        }
        /// <summary>
        /// 买家ID
        /// </summary>
        public int BuyerID
        {
            set { _buyerid = value; }
            get { return _buyerid; }
        }
        /// <summary>
        /// 买家名称
        /// </summary>
        public string BuyerName
        {
            set { _buyername = value; }
            get { return _buyername; }
        }
        /// <summary>
        /// 买家Email
        /// </summary>
        public string BuyerEmail
        {
            set { _buyeremail = value; }
            get { return _buyeremail; }
        }
        /// <summary>
        /// 买家手机
        /// </summary>
        public string BuyerCellPhone
        {
            set { _buyercellphone = value; }
            get { return _buyercellphone; }
        }
        /// <summary>
        /// 收货省市区ID
        /// </summary>
        public int? RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 收货地区
        /// </summary>
        public string ShipRegion
        {
            set { _shipregion = value; }
            get { return _shipregion; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ShipAddress
        {
            set { _shipaddress = value; }
            get { return _shipaddress; }
        }
        /// <summary>
        /// 收货邮编
        /// </summary>
        public string ShipZipCode
        {
            set { _shipzipcode = value; }
            get { return _shipzipcode; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string ShipName
        {
            set { _shipname = value; }
            get { return _shipname; }
        }
        /// <summary>
        /// 收货人座机
        /// </summary>
        public string ShipTelPhone
        {
            set { _shiptelphone = value; }
            get { return _shiptelphone; }
        }
        /// <summary>
        /// 收货人手机
        /// </summary>
        public string ShipCellPhone
        {
            set { _shipcellphone = value; }
            get { return _shipcellphone; }
        }
        /// <summary>
        /// 收货人Email
        /// </summary>
        public string ShipEmail
        {
            set { _shipemail = value; }
            get { return _shipemail; }
        }
        /// <summary>
        /// 配送ID
        /// </summary>
        public int? ShippingModeId
        {
            set { _shippingmodeid = value; }
            get { return _shippingmodeid; }
        }
        /// <summary>
        /// 配送名称
        /// </summary>
        public string ShippingModeName
        {
            set { _shippingmodename = value; }
            get { return _shippingmodename; }
        }
        /// <summary>
        /// 实际配送ID
        /// </summary>
        public int? RealShippingModeId
        {
            set { _realshippingmodeid = value; }
            get { return _realshippingmodeid; }
        }
        /// <summary>
        /// 实际配送名称
        /// </summary>
        public string RealShippingModeName
        {
            set { _realshippingmodename = value; }
            get { return _realshippingmodename; }
        }
        /// <summary>
        /// 发货人ID
        /// </summary>
        public int? ShipperId
        {
            set { _shipperid = value; }
            get { return _shipperid; }
        }
        /// <summary>
        /// 发货人名称
        /// </summary>
        public string ShipperName
        {
            set { _shippername = value; }
            get { return _shippername; }
        }
        /// <summary>
        /// 发货人地址
        /// </summary>
        public string ShipperAddress
        {
            set { _shipperaddress = value; }
            get { return _shipperaddress; }
        }
        /// <summary>
        /// 发货人手机
        /// </summary>
        public string ShipperCellPhone
        {
            set { _shippercellphone = value; }
            get { return _shippercellphone; }
        }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal? Freight
        {
            set { _freight = value; }
            get { return _freight; }
        }
        /// <summary>
        /// 调整后运费
        /// </summary>
        public decimal? FreightAdjusted
        {
            set { _freightadjusted = value; }
            get { return _freightadjusted; }
        }
        /// <summary>
        /// 实际运费
        /// </summary>
        public decimal? FreightActual
        {
            set { _freightactual = value; }
            get { return _freightactual; }
        }
        /// <summary>
        /// 重量
        /// </summary>
        public int? Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 配送状态 0 未发货 | 1 打包(配货)中 | 2 已发货 | 3 已确认收货 | 4 拒收退货中 | 5 拒收已退货
        /// </summary>
        public int ShippingStatus
        {
            set { _shippingstatus = value; }
            get { return _shippingstatus; }
        }
        /// <summary>
        /// 配送单号
        /// </summary>
        public string ShipOrderNumber
        {
            set { _shipordernumber = value; }
            get { return _shipordernumber; }
        }
        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string ExpressCompanyName
        {
            set { _expresscompanyname = value; }
            get { return _expresscompanyname; }
        }
        /// <summary>
        /// 快递公司缩写
        /// </summary>
        public string ExpressCompanyAbb
        {
            set { _expresscompanyabb = value; }
            get { return _expresscompanyabb; }
        }
        /// <summary>
        /// 支付类型接口ID
        /// </summary>
        public int PaymentTypeId
        {
            set { _paymenttypeid = value; }
            get { return _paymenttypeid; }
        }
        /// <summary>
        /// 支付类型名称
        /// </summary>
        public string PaymentTypeName
        {
            set { _paymenttypename = value; }
            get { return _paymenttypename; }
        }
        /// <summary>
        /// 支付网关名称(用于辨识付款类型)  cod 货到付款| bank 银行汇款 | [..] 在线支付
        /// </summary>
        public string PaymentGateway
        {
            set { _paymentgateway = value; }
            get { return _paymentgateway; }
        }
        /// <summary>
        /// 支付状态 0 未支付 | 1 等待确认 | 2 已支付 | 3 处理中(预留) | 4 支付异常(预留)
        /// </summary>
        public int PaymentStatus
        {
            set { _paymentstatus = value; }
            get { return _paymentstatus; }
        }
        /// <summary>
        /// 退款状态 0 未退款 | 1 请求退款 | 2 处理中 | 3 已退款 | 4 拒绝退款
        /// </summary>
        public int RefundStatus
        {
            set { _refundstatus = value; }
            get { return _refundstatus; }
        }
        /// <summary>
        /// 货币码
        /// </summary>
        public string PayCurrencyCode
        {
            set { _paycurrencycode = value; }
            get { return _paycurrencycode; }
        }
        /// <summary>
        /// 货币名称
        /// </summary>
        public string PayCurrencyName
        {
            set { _paycurrencyname = value; }
            get { return _paycurrencyname; }
        }
        /// <summary>
        /// 付款手续费
        /// </summary>
        public decimal? PaymentFee
        {
            set { _paymentfee = value; }
            get { return _paymentfee; }
        }
        /// <summary>
        /// 调整后付款手续费
        /// </summary>
        public decimal? PaymentFeeAdjusted
        {
            set { _paymentfeeadjusted = value; }
            get { return _paymentfeeadjusted; }
        }
        /// <summary>
        /// 网关订单ID
        /// </summary>
        public string GatewayOrderId
        {
            set { _gatewayorderid = value; }
            get { return _gatewayorderid; }
        }
        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal OrderTotal
        {
            set { _ordertotal = value; }
            get { return _ordertotal; }
        }
        /// <summary>
        /// 订单积分
        /// </summary>
        public int OrderPoint
        {
            set { _orderpoint = value; }
            get { return _orderpoint; }
        }
        /// <summary>
        /// 订单成本价
        /// </summary>
        public decimal? OrderCostPrice
        {
            set { _ordercostprice = value; }
            get { return _ordercostprice; }
        }

        /// <summary>
        /// 订单第二成本价
        /// </summary>
        public decimal? OrderCostPrice2
        {
            set { _ordercostprice2 = value; }
            get { return _ordercostprice2; }
        }

        /// <summary>
        /// 订单利润
        /// </summary>
        public decimal? OrderProfit
        {
            set { _orderprofit = value; }
            get { return _orderprofit; }
        }
        /// <summary>
        /// 订单其它费用
        /// </summary>
        public decimal? OrderOtherCost
        {
            set { _orderothercost = value; }
            get { return _orderothercost; }
        }
        /// <summary>
        /// 订单选项价格
        /// </summary>
        public decimal? OrderOptionPrice
        {
            set { _orderoptionprice = value; }
            get { return _orderoptionprice; }
        }
        /// <summary>
        /// 折扣名称
        /// </summary>
        public string DiscountName
        {
            set { _discountname = value; }
            get { return _discountname; }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal? DiscountAmount
        {
            set { _discountamount = value; }
            get { return _discountamount; }
        }
        /// <summary>
        /// 调整后折扣金额
        /// </summary>
        public decimal? DiscountAdjusted
        {
            set { _discountadjusted = value; }
            get { return _discountadjusted; }
        }
        /// <summary>
        /// 折扣值
        /// </summary>
        public decimal? DiscountValue
        {
            set { _discountvalue = value; }
            get { return _discountvalue; }
        }
        /// <summary>
        /// 折扣值类型
        /// </summary>
        public int? DiscountValueType
        {
            set { _discountvaluetype = value; }
            get { return _discountvaluetype; }
        }
        /// <summary>
        /// 优惠码
        /// </summary>
        public string CouponCode
        {
            set { _couponcode = value; }
            get { return _couponcode; }
        }
        /// <summary>
        /// 优惠卷名称
        /// </summary>
        public string CouponName
        {
            set { _couponname = value; }
            get { return _couponname; }
        }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal? CouponAmount
        {
            set { _couponamount = value; }
            get { return _couponamount; }
        }
        /// <summary>
        /// 优惠值
        /// </summary>
        public decimal? CouponValue
        {
            set { _couponvalue = value; }
            get { return _couponvalue; }
        }
        /// <summary>
        /// 优惠值类型
        /// </summary>
        public int? CouponValueType
        {
            set { _couponvaluetype = value; }
            get { return _couponvaluetype; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName
        {
            set { _activityname = value; }
            get { return _activityname; }
        }
        /// <summary>
        /// 活动免费金额
        /// </summary>
        public decimal? ActivityFreeAmount
        {
            set { _activityfreeamount = value; }
            get { return _activityfreeamount; }
        }
        /// <summary>
        /// 活动状态 0 未使用 | 1 已使用
        /// </summary>
        public int ActivityStatus
        {
            set { _activitystatus = value; }
            get { return _activitystatus; }
        }
        /// <summary>
        /// 团购ID
        /// </summary>
        public int? GroupBuyId
        {
            set { _groupbuyid = value; }
            get { return _groupbuyid; }
        }
        /// <summary>
        /// 团购价格
        /// </summary>
        public decimal? GroupBuyPrice
        {
            set { _groupbuyprice = value; }
            get { return _groupbuyprice; }
        }
        /// <summary>
        /// 团购状态 0 未使用 | 1 已使用
        /// </summary>
        public int GroupBuyStatus
        {
            set { _groupbuystatus = value; }
            get { return _groupbuystatus; }
        }
        /// <summary>
        /// 最终金额(支付)
        /// </summary>
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 订单类型 1 常规主单 | 2 常规子单
        /// </summary>
        public int OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
        }
        /// <summary>
        /// 订单状态 -4 系统锁定 | -3 后台锁定 | -2 用户锁定 | -1 死单(取消) | 0 未处理 | 1 活动 | 2 已完成
        /// </summary>
        public int OrderStatus
        {
            set { _orderstatus = value; }
            get { return _orderstatus; }
        }
        /// <summary>
        /// 卖家ID (B2B启用)
        /// </summary>
        public int? SellerID
        {
            set { _sellerid = value; }
            get { return _sellerid; }
        }
        /// <summary>
        /// 卖家名称
        /// </summary>
        public string SellerName
        {
            set { _sellername = value; }
            get { return _sellername; }
        }
        /// <summary>
        /// 卖家Email
        /// </summary>
        public string SellerEmail
        {
            set { _selleremail = value; }
            get { return _selleremail; }
        }
        /// <summary>
        /// 卖家手机
        /// </summary>
        public string SellerCellPhone
        {
            set { _sellercellphone = value; }
            get { return _sellercellphone; }
        }
        /// <summary>
        /// 评价状态 0 未评价 1 已评价
        /// </summary>
        public int CommentStatus
        {
            set { _commentstatus = value; }
            get { return _commentstatus; }
        }
        /// <summary>
        /// 供货商ID 备注：所有的商品为一个供应商时填值。
        /// </summary>
        public int? SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 供货商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 推广来源ID
        /// </summary>
        public string ReferID
        {
            set { _referid = value; }
            get { return _referid; }
        }
        /// <summary>
        /// 推广来源URL
        /// </summary>
        public string ReferURL
        {
            set { _referurl = value; }
            get { return _referurl; }
        }
        /// <summary>
        /// 下单IP
        /// </summary>
        public string OrderIP
        {
            set { _orderip = value; }
            get { return _orderip; }
        }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 商品总价
        /// </summary>
        public decimal ProductTotal
        {
            set { _producttotal = value; }
            get { return _producttotal; }
        }
        /// <summary>
        /// 是否有子订单
        /// </summary>
        public bool HasChildren
        {
            set { _haschildren = value; }
            get { return _haschildren; }
        }
        /// <summary>
        /// 是否已评论
        /// </summary>
        public bool IsReviews
        {
            set { _isreviews = value; }
            get { return _isreviews; }
        }
        #endregion Model

    }
}

