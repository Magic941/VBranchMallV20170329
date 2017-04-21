using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// OrderRemark:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderRemark
	{
		public OrderRemark()
		{}
        #region Model
        private long _remarkid;
        private long _orderid;
        private string _ordercode;
        private int _userid;
        private string _username;
        private string _remark;
        private DateTime _createddate;
        /// <summary>
        /// 
        /// </summary>
        public long RemarkId
        {
            set { _remarkid = value; }
            get { return _remarkid; }
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
        /// 用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        #endregion Model

	}
}

