using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// OrderOptions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderOptions
	{
		public OrderOptions()
		{}
        #region Model
        private int _lookuplistid;
        private int _lookupitemid;
        private long _orderid;
        private string _ordercode;
        private string _listdescription;
        private string _itemdescription;
        private decimal _adjustedprice;
        private string _customertitle;
        private string _customerdescription;
        /// <summary>
        /// 订单可选项
        /// </summary>
        public int LookupListId
        {
            set { _lookuplistid = value; }
            get { return _lookuplistid; }
        }
        /// <summary>
        /// 订单可选项项内容
        /// </summary>
        public int LookupItemId
        {
            set { _lookupitemid = value; }
            get { return _lookupitemid; }
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
        /// 订单号
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }
        /// <summary>
        /// 可选项描述
        /// </summary>
        public string ListDescription
        {
            set { _listdescription = value; }
            get { return _listdescription; }
        }
        /// <summary>
        /// 可选项项描述
        /// </summary>
        public string ItemDescription
        {
            set { _itemdescription = value; }
            get { return _itemdescription; }
        }
        /// <summary>
        /// 调整价格
        /// </summary>
        public decimal AdjustedPrice
        {
            set { _adjustedprice = value; }
            get { return _adjustedprice; }
        }
        /// <summary>
        /// 客户标题
        /// </summary>
        public string CustomerTitle
        {
            set { _customertitle = value; }
            get { return _customertitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerDescription
        {
            set { _customerdescription = value; }
            get { return _customerdescription; }
        }
        #endregion Model

	}
}

