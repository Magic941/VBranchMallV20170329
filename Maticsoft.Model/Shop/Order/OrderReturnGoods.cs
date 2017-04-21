using System;
namespace Maticsoft.Model.Shop.Order
{
    /// <summary>
    /// Shop_OrderReturnGoods:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OrderReturnGoods
    {
        public OrderReturnGoods()
        { }
        #region Model
        private long _id;
        private long _orderid;
        private long _userid;
        private decimal? _returnamounts;
        private DateTime? _returntime;
        private string _returnreason;
        private string _returndescription;
        private string _returnaddress;
        private string _returnpostcode;
        private string _returntelphone;
        private string _returncontacts;
        private string _attachment;
        private int? _approvestatus;
        private string _approvepeason;
        private DateTime? _approvetime;
        private string _approveremark;
        private int? _accountstatus;
        private DateTime? _accounttime;
        private string _accountpeason;
        private bool _isdeleted;
        private string _information;
        private string _expressno;
        private decimal? _amountactual;
        private string _returnremark;
        private string _returnordercode;
        private string _ordercode;
        private int? _supplierid;
        private string _suppliername;
        private string _couponcode;
        private string _couponname;
        private decimal? _couponamount;
        private int? _couponvaluetype;
        private decimal? _couponvalue;
        private int _returngoodstype;
        private int? _returncoupon;
        private decimal? _actualsalestotal;
        private decimal? _amountadjusted;
        private decimal? _amount;
        private int? _servicetype;
        private int? _returntype;
        private int? _pickregionid;
        private string _pickregion;
        private string _pickaddress;
        private string _pickzipcode;
        private string _pickname;
        private string _picktelphone;
        private string _pickcellphone;
        private string _pickemail;
        private string _returntruename;
        private string _returnbankname;
        private string _returncard;
        private int? _returncardtype;
        private string _contactname;
        private string _contactphone;
        private int? _status;
        private int? _refundstatus;
        private int? _logisticstatus;
        private int? _customerreview;
        private string _refusereason;
        private string _Refuseremark;
        private string _Repairremark;
        private string _Adjustableremark;

        /// <summary>
        /// 拒绝调货的原因
        /// </summary>
        public string Adjustableremark
        {
            get { return _Adjustableremark; }
            set { _Adjustableremark = value; }
        }

        /// <summary>
        /// 拒绝维修的原因
        /// </summary>
        public string Repairremark
        {
            get { return _Repairremark; }
            set { _Repairremark = value; }
        }

        /// <summary>
        /// 拒绝收货的原因
        /// </summary>
        public string Refuseremark
        {
            get { return _Refuseremark; }
            set { _Refuseremark = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
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
        public long UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ReturnAmounts
        {
            set { _returnamounts = value; }
            get { return _returnamounts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReturnTime
        {
            set { _returntime = value; }
            get { return _returntime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnReason
        {
            set { _returnreason = value; }
            get { return _returnreason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnDescription
        {
            set { _returndescription = value; }
            get { return _returndescription; }
        }
        /// <summary>
        /// 退货地址
        /// </summary>
        public string ReturnAddress
        {
            set { _returnaddress = value; }
            get { return _returnaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnPostCode
        {
            set { _returnpostcode = value; }
            get { return _returnpostcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnTelphone
        {
            set { _returntelphone = value; }
            get { return _returntelphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnContacts
        {
            set { _returncontacts = value; }
            get { return _returncontacts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ApproveStatus
        {
            set { _approvestatus = value; }
            get { return _approvestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApprovePeason
        {
            set { _approvepeason = value; }
            get { return _approvepeason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ApproveTime
        {
            set { _approvetime = value; }
            get { return _approvetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApproveRemark
        {
            set { _approveremark = value; }
            get { return _approveremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AccountStatus
        {
            set { _accountstatus = value; }
            get { return _accountstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AccountTime
        {
            set { _accounttime = value; }
            get { return _accounttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccountPeason
        {
            set { _accountpeason = value; }
            get { return _accountpeason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Information
        {
            set { _information = value; }
            get { return _information; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpressNO
        {
            set { _expressno = value; }
            get { return _expressno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AmountActual
        {
            set { _amountactual = value; }
            get { return _amountactual; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnRemark
        {
            set { _returnremark = value; }
            get { return _returnremark; }
        }
        /// <summary>
        /// 退货单号
        /// </summary>
        public string ReturnOrderCode
        {
            set { _returnordercode = value; }
            get { return _returnordercode; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }
        /// <summary>
        /// 商家ID 
        /// </summary>
        public int? SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
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
        /// CouponAmount 优惠金额
        /// </summary>
        public decimal? CouponAmount
        {
            set { _couponamount = value; }
            get { return _couponamount; }
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
        /// 优惠值
        /// </summary>
        public decimal? CouponValue
        {
            set { _couponvalue = value; }
            get { return _couponvalue; }
        }
        /// <summary>
        ///  退货类型   1:退货    2.退款  3调货 4维修
        /// </summary>
        public int ReturnGoodsType
        {
            set { _returngoodstype = value; }
            get { return _returngoodstype; }
        }
        /// <summary>
        /// 默认值 0   未设置      1:退优惠劵   2:不退优惠劵
        /// </summary>
        public int? ReturnCoupon
        {
            set { _returncoupon = value; }
            get { return _returncoupon; }
        }
        /// <summary>
        /// 商品实际出售总价
        /// </summary>
        public decimal? ActualSalesTotal
        {
            set { _actualsalestotal = value; }
            get { return _actualsalestotal; }
        }
        /// <summary>
        /// 调整后金额(应退金额)
        /// </summary>
        public decimal? AmountAdjusted
        {
            set { _amountadjusted = value; }
            get { return _amountadjusted; }
        }
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal? Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// ServiceType  0 订单退货 1  分批提货申请退货
        /// </summary>
        public int? ServiceType
        {
            set { _servicetype = value; }
            get { return _servicetype; }
        }
        /// <summary>
        /// 退回方式  0 快递  | 1上门取件 
        /// </summary>
        public int? ReturnType
        {
            set { _returntype = value; }
            get { return _returntype; }
        }
        /// <summary>
        /// 取货省市区ID
        /// </summary>
        public int? PickRegionId
        {
            set { _pickregionid = value; }
            get { return _pickregionid; }
        }
        /// <summary>
        /// 取货地区
        /// </summary>
        public string PickRegion
        {
            set { _pickregion = value; }
            get { return _pickregion; }
        }
        /// <summary>
        /// 取货地址
        /// </summary>
        public string PickAddress
        {
            set { _pickaddress = value; }
            get { return _pickaddress; }
        }
        /// <summary>
        /// 取货邮编
        /// </summary>
        public string PickZipCode
        {
            set { _pickzipcode = value; }
            get { return _pickzipcode; }
        }
        /// <summary>
        /// 取货人
        /// </summary>
        public string PickName
        {
            set { _pickname = value; }
            get { return _pickname; }
        }
        /// <summary>
        /// 取货人座机
        /// </summary>
        public string PickTelPhone
        {
            set { _picktelphone = value; }
            get { return _picktelphone; }
        }
        /// <summary>
        /// PickTelPhone
        /// </summary>
        public string PickCellPhone
        {
            set { _pickcellphone = value; }
            get { return _pickcellphone; }
        }
        /// <summary>
        /// 取货人Email
        /// </summary>
        public string PickEmail
        {
            set { _pickemail = value; }
            get { return _pickemail; }
        }
        /// <summary>
        /// 开户姓名
        /// </summary>
        public string ReturnTrueName
        {
            set { _returntruename = value; }
            get { return _returntruename; }
        }
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string ReturnBankName
        {
            set { _returnbankname = value; }
            get { return _returnbankname; }
        }
        /// <summary>
        ///  银行卡号 或 支付宝号
        /// </summary>
        public string ReturnCard
        {
            set { _returncard = value; }
            get { return _returncard; }
        }
        /// <summary>
        /// 卡号类型    默认为3     1: 银行卡号   2:支付宝帐号  3:账户余额
        /// </summary>
        public int? ReturnCardType
        {
            set { _returncardtype = value; }
            get { return _returncardtype; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName
        {
            set { _contactname = value; }
            get { return _contactname; }
        }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone
        {
            set { _contactphone = value; }
            get { return _contactphone; }
        }
        /// <summary>
        /// 状态  |-3拒绝 | -2锁定 |  -1取消申请  |   0未处理  |  1处理中| 2 完成|
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 退款状态 0 未退款 | 1 申请退款 | 2 退款中 | 3 已退款 | 4 拒绝退款
        /// </summary>
        public int? RefundStatus
        {
            set { _refundstatus = value; }
            get { return _refundstatus; }
        }
        /// <summary>
        /// 发货状态:   0 未发货  |    1 已发货   |   2 已收货  |   3 返程中  |  4 |拒收退货  | 5 拒绝维修   | 6 拒绝调货 
        /// </summary>
        public int? LogisticStatus
        {
            set { _logisticstatus = value; }
            get { return _logisticstatus; }
        }
        /// <summary>
        /// 客户评价      0 未填写  1  已解决  2未解决
        /// </summary>
        public int? CustomerReview
        {
            set { _customerreview = value; }
            get { return _customerreview; }
        }
        /// <summary>
        /// 拒绝原因  (当申请被拒绝时使用)
        /// </summary>
        public string RefuseReason
        {
            set { _refusereason = value; }
            get { return _refusereason; }
        }
        #endregion Model

    }
}

