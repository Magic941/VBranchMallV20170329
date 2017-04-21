/**
* Suppliers.cs
*
* 功 能： N/A
* 类 名： Suppliers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:50   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Supplier
{
	/// <summary>
	/// 供应商
	/// </summary>
	[Serializable]
	public partial class SupplierInfo
	{
		public SupplierInfo()
		{}
        #region Model
        private int _supplierid;
        private string _name;
        private string _shopname;
        private int _storestatus = 0;
        private int _categoryid = -1;
        private int _rank = 0;
        private int _userid;
        private string _username;
        private string _telphone;
        private string _cellphone;
        private string _contactmail;
        private string _introduction;
        private int? _registeredcapital;
        private int? _regionid;
        private string _address;
        private string _contact;
        private DateTime? _establisheddate;
        private int? _establishedcity;
        private string _logo;
        private string _fax;
        private string _postcode;
        private string _homepage;
        private string _artiperson;
        private int? _companytype;
        private string _businesslicense;
        private string _taxnumber;
        private string _accountbank;
        private string _accountinfo;
        private string _servicephone;
        private string _qq;
        private string _msn;
        private int _status;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private DateTime? _updateddate;
        private int? _updateduserid;
        private DateTime? _expirationdate;
        private decimal _balance = 0M;
        private bool _isuserapprove = false;
        private bool _issuppapprove = false;
        private decimal _scoredesc = 0M;
        private decimal _scoreservice = 0M;
        private decimal _scorespeed = 0M;
        private int _recomend = 0;
        private int _sequence = 0;
        private int _productcount = 0;
        private int _photocount = 0;
        private int _themeid = 0;
        private string _remark;
        private int _agentid = -1;
        private int _indexprodtop = 0;
        private string _indexcontent;
        private decimal _latitude;
        private decimal _longitude;
        private bool _isOnLine = false;
        private string _slogan = string.Empty;
        private string _discount = string.Empty;
        private bool _bGType = true;
        private string _background = string.Empty;

        
        /// <summary>
        /// 供应商编号
        /// </summary>
        public int SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName
        {
            set { _shopname = value; }
            get { return _shopname; }
        }
        /// <summary>
        /// 店铺状态(-1未开店,0未审核,1已审核,2已关闭)
        /// </summary>
        public int StoreStatus
        {
            set { _storestatus = value; }
            get { return _storestatus; }
        }
        /// <summary>
        /// 供应商分类
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 供应商等级
        /// </summary>
        public int Rank
        {
            set { _rank = value; }
            get { return _rank; }
        }
        /// <summary>
        /// 所属用户Id
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
        /// 电话
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string CellPhone
        {
            set { _cellphone = value; }
            get { return _cellphone; }
        }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string ContactMail
        {
            set { _contactmail = value; }
            get { return _contactmail; }
        }
        /// <summary>
        /// 供应商介绍
        /// </summary>
        public string Introduction
        {
            set { _introduction = value; }
            get { return _introduction; }
        }
        /// <summary>
        /// 注册资本
        /// </summary>
        public int? RegisteredCapital
        {
            set { _registeredcapital = value; }
            get { return _registeredcapital; }
        }
        /// <summary>
        /// 省/市
        /// </summary>
        public int? RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact
        {
            set { _contact = value; }
            get { return _contact; }
        }
        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime? EstablishedDate
        {
            set { _establisheddate = value; }
            get { return _establisheddate; }
        }
        /// <summary>
        /// 注册地
        /// </summary>
        public int? EstablishedCity
        {
            set { _establishedcity = value; }
            get { return _establishedcity; }
        }
        /// <summary>
        /// 标志
        /// </summary>
        public string LOGO
        {
            set { _logo = value; }
            get { return _logo; }
        }
 
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 主页
        /// </summary>
        public string HomePage
        {
            set { _homepage = value; }
            get { return _homepage; }
        }
        /// <summary>
        /// 法人
        /// </summary>
        public string ArtiPerson
        {
            set { _artiperson = value; }
            get { return _artiperson; }
        }
        /// <summary>
        /// 公司性质: 个体工商, 私营独资供应商, 国营供应商
        /// </summary>
        public int? CompanyType
        {
            set { _companytype = value; }
            get { return _companytype; }
        }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessLicense
        {
            set { _businesslicense = value; }
            get { return _businesslicense; }
        }
        /// <summary>
        /// 税务登记
        /// </summary>
        public string TaxNumber
        {
            set { _taxnumber = value; }
            get { return _taxnumber; }
        }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string AccountBank
        {
            set { _accountbank = value; }
            get { return _accountbank; }
        }
        /// <summary>
        /// 账号信息
        /// </summary>
        public string AccountInfo
        {
            set { _accountinfo = value; }
            get { return _accountinfo; }
        }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string ServicePhone
        {
            set { _servicephone = value; }
            get { return _servicephone; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// MSN
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// 0未审核  1正常  2冻结   3删除
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreatedUserId
        {
            set { _createduserid = value; }
            get { return _createduserid; }
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
        /// 更新用户
        /// </summary>
        public int? UpdatedUserId
        {
            set { _updateduserid = value; }
            get { return _updateduserid; }
        }
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpirationDate
        {
            set { _expirationdate = value; }
            get { return _expirationdate; }
        }
        /// <summary>
        /// 账户余额 暂未使用,目前使用Accounts_UsersExp表的Balance
        /// </summary>
        public decimal Balance
        {
            set { _balance = value; }
            get { return _balance; }
        }
        /// <summary>
        /// 用户是否认证
        /// </summary>
        public bool IsUserApprove
        {
            set { _isuserapprove = value; }
            get { return _isuserapprove; }
        }
        /// <summary>
        /// 供应商是否认证
        /// </summary>
        public bool IsSuppApprove
        {
            set { _issuppapprove = value; }
            get { return _issuppapprove; }
        }
        /// <summary>
        /// 描述相符评分 (平均值)
        /// </summary>
        public decimal ScoreDesc
        {
            set { _scoredesc = value; }
            get { return _scoredesc; }
        }
        /// <summary>
        /// 服务态度评分 (平均值)
        /// </summary>
        public decimal ScoreService
        {
            set { _scoreservice = value; }
            get { return _scoreservice; }
        }
        /// <summary>
        /// 发货速度评分 (平均值)
        /// </summary>
        public decimal ScoreSpeed
        {
            set { _scorespeed = value; }
            get { return _scorespeed; }
        }
        /// <summary>
        /// 推荐: 0未推荐 1已推荐
        /// </summary>
        public int Recomend
        {
            set { _recomend = value; }
            get { return _recomend; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 商品总数
        /// </summary>
        public int ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 图片总数
        /// </summary>
        public int PhotoCount
        {
            set { _photocount = value; }
            get { return _photocount; }
        }
        /// <summary>
        /// 当前模版Id
        /// </summary>
        public int ThemeId
        {
            set { _themeid = value; }
            get { return _themeid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 代理商Id
        /// </summary>
        public int AgentId
        {
            set { _agentid = value; }
            get { return _agentid; }
        }
        /// <summary>
        /// 首页显示商品个数
        /// </summary>
        public int IndexProdTop
        {
            set { _indexprodtop = value; }
            get { return _indexprodtop; }
        }
        /// <summary>
        /// 首页中间显示的内容
        /// </summary>
        public string IndexContent
        {
            set { _indexcontent = value; }
            get { return _indexcontent; }
        }
        /// <summary>
        /// 经度
        /// </summary>
	    public  decimal Longitude
	    {
            set { _longitude = value; }
            get { return _longitude; }
	    }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Latitude
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 供应商是否上线
        /// </summary>
        public bool IsOnLine
        {
            set { _isOnLine = value; }
            get { return _isOnLine; }
        }

        /// <summary>
        /// 商家标语
        /// </summary>
        public string Slogan
        {
            get { return _slogan; }
            set { _slogan = value; }
        }

        /// <summary>
        /// 商家折扣信息
        /// </summary>
        public string Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        /// <summary>
        ///店铺背景类型
        /// </summary>
        public bool BGType
        {
            get { return _bGType; }
            set { _bGType = value; }
        }

        /// <summary>
        /// 背景值（颜色值或图片路径）
        /// </summary>
        public string Background
        {
            get { return _background; }
            set { _background = value; }
        }
        #endregion Model

       
    }
}

