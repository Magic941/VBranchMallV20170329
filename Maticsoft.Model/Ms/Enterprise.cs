using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// Enterprise:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Enterprise
	{
		public Enterprise()
		{}
		#region Model
		private int _enterpriseid;
		private string _name;
		private string _introduction;
		private int? _registeredcapital;
		private string _telphone;
		private string _cellphone;
		private string _contactmail;
		private int? _regionid;
		private string _address;
		private string _remark;
		private string _contact;
		private string _username;
		private DateTime? _establisheddate;
		private int? _establishedcity;
		private string _logo;
		private string _fax;
		private string _postcode;
		private string _homepage;
		private string _artiperson;
		private int? _enterank;
		private int? _enteclassid;
		private int? _companytype;
		private string _businesslicense;
		private string _taxnumber;
		private string _accountbank;
		private string _accountinfo;
		private string _servicephone;
		private string _qq;
		private string _msn;
		private int? _status;
		private DateTime? _createddate;
		private int? _createduserid;
		private DateTime? _updateddate;
		private int? _updateduserid;
		private int _agentid;
        private string _createdusername;
        private string _updatedusername;
        private decimal _balance;

		/// <summary>
		/// 企业编号
		/// </summary>
		public int EnterpriseID
		{
			set{ _enterpriseid=value;}
			get{return _enterpriseid;}
		}
		/// <summary>
		/// 企业名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 企业介绍
		/// </summary>
		public string Introduction
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 注册资本
		/// </summary>
		public int? RegisteredCapital
		{
			set{ _registeredcapital=value;}
			get{return _registeredcapital;}
		}
		/// <summary>
		/// 电话
		/// </summary>
		public string TelPhone
		{
			set{ _telphone=value;}
			get{return _telphone;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string CellPhone
		{
			set{ _cellphone=value;}
			get{return _cellphone;}
		}
		/// <summary>
		/// 联系邮箱
		/// </summary>
		public string ContactMail
		{
			set{ _contactmail=value;}
			get{return _contactmail;}
		}
		/// <summary>
		/// 省/市
		/// </summary>
		public int? RegionID
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string Contact
		{
			set{ _contact=value;}
			get{return _contact;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 成立时间
		/// </summary>
		public DateTime? EstablishedDate
		{
			set{ _establisheddate=value;}
			get{return _establisheddate;}
		}
		/// <summary>
		/// 注册地
		/// </summary>
		public int? EstablishedCity
		{
			set{ _establishedcity=value;}
			get{return _establishedcity;}
		}
		/// <summary>
		/// 标志
		/// </summary>
		public string LOGO
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		/// <summary>
		/// 传真
		/// </summary>
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string PostCode
		{
			set{ _postcode=value;}
			get{return _postcode;}
		}
		/// <summary>
		/// 主页
		/// </summary>
		public string HomePage
		{
			set{ _homepage=value;}
			get{return _homepage;}
		}
		/// <summary>
		/// 法人
		/// </summary>
		public string ArtiPerson
		{
			set{ _artiperson=value;}
			get{return _artiperson;}
		}
		/// <summary>
		/// 企业等级
		/// </summary>
		public int? EnteRank
		{
			set{ _enterank=value;}
			get{return _enterank;}
		}
		/// <summary>
		/// 企业分类
		/// </summary>
		public int? EnteClassID
		{
			set{ _enteclassid=value;}
			get{return _enteclassid;}
		}
		/// <summary>
		/// 公司性质：
        ///个体工商，
        ///私营独资企业，
        ///国营企业。
		/// </summary>
		public int? CompanyType
		{
			set{ _companytype=value;}
			get{return _companytype;}
		}
		/// <summary>
		/// 营业执照
		/// </summary>
		public string BusinessLicense
		{
			set{ _businesslicense=value;}
			get{return _businesslicense;}
		}
		/// <summary>
		/// 税务登记
		/// </summary>
		public string TaxNumber
		{
			set{ _taxnumber=value;}
			get{return _taxnumber;}
		}
		/// <summary>
		/// 开户银行
		/// </summary>
		public string AccountBank
		{
			set{ _accountbank=value;}
			get{return _accountbank;}
		}
		/// <summary>
		/// 账号信息
		/// </summary>
		public string AccountInfo
		{
			set{ _accountinfo=value;}
			get{return _accountinfo;}
		}
		/// <summary>
		/// 客服电话
		/// </summary>
		public string ServicePhone
		{
			set{ _servicephone=value;}
			get{return _servicephone;}
		}
		/// <summary>
		/// QQ
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// MSN
		/// </summary>
		public string MSN
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		/// <summary>
		/// 0未审核  1正常  2冻结   3删除
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 创建用户
		/// </summary>
		public int? CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 更新日期
		/// </summary>
		public DateTime? UpdatedDate
		{
			set{ _updateddate=value;}
			get{return _updateddate;}
		}
		/// <summary>
		/// 更新用户
		/// </summary>
		public int? UpdatedUserID
		{
			set{ _updateduserid=value;}
			get{return _updateduserid;}
		}
		/// <summary>
		/// 代理商ID
		/// </summary>
		public int AgentID
		{
			set{ _agentid=value;}
			get{return _agentid;}
		}
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedUserName
        {
            get { return _createdusername; }
            set { _createdusername = value; }
        }
        /// <summary>
        /// 编辑者
        /// </summary>
        public string UpdatedUserName
        {
            get { return _updatedusername; }
            set { _updatedusername = value; }
        }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
		#endregion Model

	}
}

