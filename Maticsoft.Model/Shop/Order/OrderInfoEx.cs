
using System.Collections.Generic;
using Maticsoft.Payment.Model;

namespace Maticsoft.Model.Shop.Order
{
    /// <summary>
    /// Orders:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public partial class OrderInfo : IOrderInfo
    {
        #region 克隆构造
        /// <summary>
        /// 克隆构造
        /// </summary>
        /// <remarks>不会克隆 订单项目 和 子订单集合</remarks>
        public OrderInfo(OrderInfo orderInfo)
        {
            _orderid = orderInfo.OrderId;
            _ordercode = orderInfo.OrderCode;
            _parentorderid = orderInfo.ParentOrderId;
            _createddate = orderInfo.CreatedDate;
            _updateddate = orderInfo.UpdatedDate;
            _buyerid = orderInfo.BuyerID;
            _buyername = orderInfo.BuyerName;
            _buyeremail = orderInfo.BuyerEmail;
            _buyercellphone = orderInfo.BuyerCellPhone;
            _regionid = orderInfo.RegionId;
            _shipregion = orderInfo.ShipRegion;
            _shipaddress = orderInfo.ShipAddress;
            _shipzipcode = orderInfo.ShipZipCode;
            _shipname = orderInfo.ShipName;
            _shiptelphone = orderInfo.ShipTelPhone;
            _shipcellphone = orderInfo.ShipCellPhone;
            _shipemail = orderInfo.ShipEmail;
            _shippingmodeid = orderInfo.ShippingModeId;
            _shippingmodename = orderInfo.ShippingModeName;
            _realshippingmodeid = orderInfo.RealShippingModeId;
            _realshippingmodename = orderInfo.RealShippingModeName;
            _shipperid = orderInfo.ShipperId;
            _shippername = orderInfo.ShipperName;
            _shipperaddress = orderInfo.ShipperAddress;
            _shippercellphone = orderInfo.ShipperCellPhone;
            _freight = orderInfo.Freight;
            _freightadjusted = orderInfo.FreightAdjusted;
            _freightactual = orderInfo.FreightActual;
            _weight = orderInfo.Weight;
            _shippingstatus = orderInfo.ShippingStatus;
            _shipordernumber = orderInfo.ShipOrderNumber;
            _expresscompanyname = orderInfo.ExpressCompanyName;
            _expresscompanyabb = orderInfo.ExpressCompanyAbb;
            _paymenttypeid = orderInfo.PaymentTypeId;
            _paymenttypename = orderInfo.PaymentTypeName;
            _paymentgateway = orderInfo.PaymentGateway;
            _paymentstatus = orderInfo.PaymentStatus;
            _refundstatus = orderInfo.RefundStatus;
            _paycurrencycode = orderInfo.PayCurrencyCode;
            _paycurrencyname = orderInfo.PayCurrencyName;
            _paymentfee = orderInfo.PaymentFee;
            _paymentfeeadjusted = orderInfo.PaymentFeeAdjusted;
            _gatewayorderid = orderInfo.GatewayOrderId;
            _ordertotal = orderInfo.OrderTotal;
            _orderpoint = orderInfo.OrderPoint;
            _ordercostprice = orderInfo.OrderCostPrice;
            _orderprofit = orderInfo.OrderProfit;
            _orderothercost = orderInfo.OrderOtherCost;
            _orderoptionprice = orderInfo.OrderOptionPrice;
            _discountname = orderInfo.DiscountName;
            _discountamount = orderInfo.DiscountAmount;
            _discountadjusted = orderInfo.DiscountAdjusted;
            _discountvalue = orderInfo.DiscountValue;
            _discountvaluetype = orderInfo.DiscountValueType;
            _couponcode = orderInfo.CouponCode;
            _couponname = orderInfo.CouponName;
            _couponamount = orderInfo.CouponAmount;
            _couponvalue = orderInfo.CouponValue;
            _couponvaluetype = orderInfo.CouponValueType;
            _activityname = orderInfo.ActivityName;
            _activityfreeamount = orderInfo.ActivityFreeAmount;
            _activitystatus = orderInfo.ActivityStatus;
            _groupbuyid = orderInfo.GroupBuyId;
            _groupbuyprice = orderInfo.GroupBuyPrice;
            _groupbuystatus = orderInfo.GroupBuyStatus;
            _amount = orderInfo.Amount;
            _ordertype = orderInfo.OrderType;
            _orderstatus = orderInfo.OrderStatus;
            _sellerid = orderInfo.SellerID;
            _sellername = orderInfo.SellerName;
            _selleremail = orderInfo.SellerEmail;
            _sellercellphone = orderInfo.SellerCellPhone;
            _commentstatus = orderInfo.CommentStatus;
            _supplierid = orderInfo.SupplierId;
            _suppliername = orderInfo.SupplierName;
            _referid = orderInfo.ReferID;
            _referurl = orderInfo.ReferURL;
            _orderip = orderInfo.OrderIP;
            _remark = orderInfo.Remark;
            _producttotal = orderInfo.ProductTotal;
            _haschildren = orderInfo.HasChildren;
            _isreviews = orderInfo.IsReviews;
            _cardsysid = orderInfo.CardSysId;
        }
        #endregion

        private List<OrderItems> _orderItems = new List<OrderItems>();
        /// <summary>
        /// 订单项目
        /// </summary>
        public List<OrderItems> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }

        #region 子订单集合
        private List<OrderInfo> _subOrders = new List<OrderInfo>();
        /// <summary>
        /// 子订单集合
        /// </summary>
        public List<OrderInfo> SubOrders
        {
            get { return _subOrders; }
            set { _subOrders = value; }
        }
        #endregion

        #region IOrderInfo 成员

        public System.DateTime OrderDate
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
       
        OrderStatus IOrderInfo.OrderStatus
        {
            get
            {
                //订单状态 -4 系统锁定 | -3 后台锁定 | -2 用户锁定 | -1 死单(取消) | 0 未处理 | 1 活动 | 2 已完成
                switch (OrderStatus)
                {
                    case 0:
                        return Payment.Model.OrderStatus.InProgress;
                    case 1:
                    case 2:
                        return Payment.Model.OrderStatus.Successed;
                    default:
                        return Payment.Model.OrderStatus.Closed;
                }
            }
        }

        PaymentStatus IOrderInfo.PaymentStatus
        {
            get
            {
                //支付状态 0 未支付 | 1 等待确认 | 2 已支付 | 3 处理中(预留) | 4 支付异常(预留)
                switch (PaymentStatus)
                {
                    case 0:
                        return Payment.Model.PaymentStatus.NotYet;
                    case 2:
                        return Payment.Model.PaymentStatus.Prepaid;
                    default:
                        return Payment.Model.PaymentStatus.All;
                }
            }
        }

        RefundStatus IOrderInfo.RefundStatus
        {
            get
            {
                //退款状态 0 未退款 | 1 请求退款 | 2 处理中 | 3 已退款 | 4 拒绝退款
                switch (RefundStatus)
                {
                    case 0:
                        return Payment.Model.RefundStatus.None;
                    case 1:
                        return Payment.Model.RefundStatus.Requested;
                    case 2:
                        return Payment.Model.RefundStatus.Requested;
                    case 3:
                        return Payment.Model.RefundStatus.Refund;
                    case 4:
                        return Payment.Model.RefundStatus.Reject;
                    default:
                        return Payment.Model.RefundStatus.All;
                }
            }
        }

        ShippingStatus IOrderInfo.ShippingStatus
        {
            get
            {
                //配送状态 0 未发货 | 1 打包中 | 2 已发货 | 3 已确认收货 | 4 拒收退货中 | 5 拒收已退货
                switch (ShippingStatus)
                {
                    case 0:
                        return Payment.Model.ShippingStatus.NotYet;
                    case 1:
                        return Payment.Model.ShippingStatus.Packing;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    default:
                        return Payment.Model.ShippingStatus.Delivered;
                }
            }
        }

        #endregion
    }
}

