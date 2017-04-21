using System;
namespace Maticsoft.Model.Shop.Order
{
    /// <summary>
    /// OrderReturnGoodsItem:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OrderReturnGoodsItem
    {
        public OrderReturnGoodsItem()
        { }
        #region Model
        private long _id;
        private long? _returnid;
        private string _productname;
        private string _productcode;
        private string _sku;
        private long? _productid;
        private long? _productattachmentid;
        private string _productattachmentname;
        private decimal? _costprice;
        private decimal? _sellprice;
        private decimal? _adjustedprice;
        private int? _quantity;
        private long? _orderitemid;
        private long? _orderid;
        private string _attribute;
        private long? _supplierid;
        private string _suppliername;
        private string _ordercode;
        private decimal? _orderamounts;
        private DateTime? _timeout;
        private DateTime? _createtime;
        private long? _userid;
        private int? _producttype;
        private decimal? _returnprice;
        private string _thumbnailsurl;
        private string _description;
        private string _returnordercode;
        private int? _returnapptype;
        private long? _appgoodsid;
        private string _appgoodscode;

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
        public long? ReturnId
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? ProductAttachmentId
        {
            set { _productattachmentid = value; }
            get { return _productattachmentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductAttachmentName
        {
            set { _productattachmentname = value; }
            get { return _productattachmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Costprice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SellPrice
        {
            set { _sellprice = value; }
            get { return _sellprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AdjustedPrice
        {
            set { _adjustedprice = value; }
            get { return _adjustedprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? OrderItemId
        {
            set { _orderitemid = value; }
            get { return _orderitemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attribute
        {
            set { _attribute = value; }
            get { return _attribute; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? Supplierid
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Suppliername
        {
            set { _suppliername = value; }
            get { return _suppliername; }
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
        public decimal? OrderAmounts
        {
            set { _orderamounts = value; }
            get { return _orderamounts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeOut
        {
            set { _timeout = value; }
            get { return _timeout; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }


        /// <summary>
        /// 商品类型  1 正常购买    2赠送商品
        /// </summary>
        public int? ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 退回的价格
        /// </summary>
        public decimal? ReturnPrice
        {
            set { _returnprice = value; }
            get { return _returnprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailsUrl
        {
            set { _thumbnailsurl = value; }
            get { return _thumbnailsurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 退货编码
        /// </summary>
        public string ReturnOrderCode
        {
            set { _returnordercode = value; }
            get { return _returnordercode; }
        }
        /// <summary>
        /// 退货类型  0订单退货  1 表示预定退货
        /// </summary>
        public int? ReturnAppType
        {
            set { _returnapptype = value; }
            get { return _returnapptype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? AppGoodsID
        {
            set { _appgoodsid = value; }
            get { return _appgoodsid; }
        }
        /// <summary>
        /// 申请提货编号
        /// </summary>
        public string AppGoodsCode
        {
            set { _appgoodscode = value; }
            get { return _appgoodscode; }
        }

        public Maticsoft.Model.Shop.Order.OrderReturnGoods OrderReturnGood
        {
            get;
            set;
        }
        #endregion Model

    }
}

