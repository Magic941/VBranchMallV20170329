using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// UserRank:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserRank
	{
		public UserRank()
		{}
        #region Model
        private int _rankid;
        private string _name;
        private int _ranklevel = 0;
        private int _pointmax;
        private int _pointmin;
        private bool _isdefault;
        private int _ranktype;
        private bool _ismembercreated;
        private string _description;
        private int? _creatoruserid;
        private string _pricetype;
        private string _priceoperations;
        private decimal _pricevalue;
        /// <summary>
        /// 
        /// </summary>
        public int RankId
        {
            set { _rankid = value; }
            get { return _rankid; }
        }
        /// <summary>
        /// 等级名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 等级排名
        /// </summary>
        public int RankLevel
        {
            set { _ranklevel = value; }
            get { return _ranklevel; }
        }
        /// <summary>
        /// 等级所需最高积分
        /// </summary>
        public int PointMax
        {
            set { _pointmax = value; }
            get { return _pointmax; }
        }
        /// <summary>
        /// 等级所需最低积分
        /// </summary>
        public int PointMin
        {
            set { _pointmin = value; }
            get { return _pointmin; }
        }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }
        /// <summary>
        /// 等级类型  0:普通会员  1：代理商 
        /// </summary>
        public int RankType
        {
            set { _ranktype = value; }
            get { return _ranktype; }
        }
        /// <summary>
        /// 是否成员创建
        /// </summary>
        public bool IsMemberCreated
        {
            set { _ismembercreated = value; }
            get { return _ismembercreated; }
        }
        /// <summary>
        /// 等级描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 创建者
        /// </summary>
        public int? CreatorUserId
        {
            set { _creatoruserid = value; }
            get { return _creatoruserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PriceType
        {
            set { _pricetype = value; }
            get { return _pricetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PriceOperations
        {
            set { _priceoperations = value; }
            get { return _priceoperations; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PriceValue
        {
            set { _pricevalue = value; }
            get { return _pricevalue; }
        }
        #endregion Model

	}
}

