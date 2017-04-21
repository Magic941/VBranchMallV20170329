using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// OrderAction:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderAction
	{
		public OrderAction()
		{}
        #region Model
        private long _actionid;
        private long _orderid;
        private string _ordercode;
        private int _userid;
        private string _username;
        private string _actioncode;
        private DateTime _actiondate;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public long ActionId
        {
            set { _actionid = value; }
            get { return _actionid; }
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
        /// 订单ID
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionCode
        {
            set { _actioncode = value; }
            get { return _actioncode; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime ActionDate
        {
            set { _actiondate = value; }
            get { return _actiondate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

	}
}

