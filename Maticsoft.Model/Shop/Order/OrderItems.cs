using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// OrderItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderItems
	{
		public OrderItems()
		{}
        #region Model
        private long _itemid;
        private long _orderid;
        private string _ordercode;
        private long _productid;
        private string _productcode;
        private string _sku;
        private string _name;
        private string _thumbnailsurl;
        private string _description;
        private int _quantity;
        private int _shipmentquantity;
        private decimal _costprice;
        private decimal _costprice2;
        private decimal _sellprice;
        private decimal _adjustedprice;
        private string _attribute;
        private string _remark;
        private int _weight;
        private decimal? _deduct;
        private int _points;
        private int? _productlineid;
        private int? _supplierid;
        private string _suppliername;
        private int _ActiveID;
        private int? _returnstatus;
        private int _returnqty;
        private int _returnordertype;


        /// <summary>
        /// 退货类型 1退货 2退款 3调货 4维修
        /// </summary>
        public int ReturnOrderType
        {
            get { return _returnordertype; }
            set { _returnordertype = value; }
        }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActiveID
        {
            get { return _ActiveID; }
            set { _ActiveID = value; }
        }
        private int _ActiveType;
        /// <summary>
        /// 活动类型
        /// </summary>
        public int ActiveType
        {
            get { return _ActiveType; }
            set { _ActiveType = value; }
        }
        /// <summary>
        /// 订单项目ID
        /// </summary>
        public long ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
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
        /// 订单自定义单号
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 商品条码
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 商品SKU
        /// </summary>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 商品缩略图
        /// </summary>
        public string ThumbnailsUrl
        {
            set { _thumbnailsurl = value; }
            get { return _thumbnailsurl; }
        }
        /// <summary>
        /// 商品说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 实际出货数量
        /// </summary>
        public int ShipmentQuantity
        {
            set { _shipmentquantity = value; }
            get { return _shipmentquantity; }
        }
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }

        /// <summary>
        /// 第二成本价
        /// </summary>
        public decimal CostPrice2
        {
            set { _costprice2 = value; }
            get { return _costprice2; }
        }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal SellPrice
        {
            set { _sellprice = value; }
            get { return _sellprice; }
        }
        /// <summary>
        /// 调整后的价格
        /// </summary>
        public decimal AdjustedPrice
        {
            set { _adjustedprice = value; }
            get { return _adjustedprice; }
        }
        /// <summary>
        /// 商品属性
        /// </summary>
        public string Attribute
        {
            set { _attribute = value; }
            get { return _attribute; }
        }
        /// <summary>
        /// 项目备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 商品重量
        /// </summary>
        public int Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 扣除金额
        /// </summary>
        public decimal? Deduct
        {
            set { _deduct = value; }
            get { return _deduct; }
        }
        /// <summary>
        /// 商品所赠的积分
        /// </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 商品线ID
        /// </summary>
        public int? ProductLineId
        {
            set { _productlineid = value; }
            get { return _productlineid; }
        }
        /// <summary>
        /// 供货商ID
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
        /// 退货状态 0未申请 1 未处理 2处理中 3完成 4拒绝  5取消
        /// </summary>
        public int? ReturnStatus
        {
            get { return _returnstatus; }
            set { _returnstatus = value; }
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        public int ReturnQty
        {
            get { return _returnqty; }
            set { _returnqty = value; }
        }

        #endregion Model
	}
}

