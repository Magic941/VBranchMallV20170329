/**
* CouponRule.cs
*
* 功 能： N/A
* 类 名： CouponRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:21:01   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Coupon
{
	/// <summary>
	/// CouponRule:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CouponRule
	{
		public CouponRule()
		{}
        #region Model
        private int _ruleid;
        private int _categoryid;
        private int _classid;
        private int _supplierid;
        private string _name;
        private string _prename;
        private string _imageurl;
        private decimal _couponprice;
        private decimal _limitprice;
        private string _coupondesc;
        private int _sendcount;
        private int _needpoint;
        private int _ispwd;
        private int _isreuse;
        private int _status;
        private int _recommend;
        private DateTime _startdate;
        private DateTime _enddate;
        private DateTime _createdate;
        private int _createuserid;
        private int _type = 0;
        private int _cplength;
        private int _pwdlength;
        private Int16 _useType = 0;
        private Int16 _autoState = 0;

        /// <summary>
        /// 
        /// </summary>
        public int RuleId
        {
            set { _ruleid = value; }
            get { return _ruleid; }
        }
        /// <summary>
        /// 商品分类
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 分类Id
        /// </summary>
        public int ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 优惠券 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 生成优惠券前缀
        /// </summary>
        public string PreName
        {
            set { _prename = value; }
            get { return _prename; }
        }
        /// <summary>
        /// 优惠券图片
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public decimal CouponPrice
        {
            set { _couponprice = value; }
            get { return _couponprice; }
        }
        /// <summary>
        /// 最低消费金额
        /// </summary>
        public decimal LimitPrice
        {
            set { _limitprice = value; }
            get { return _limitprice; }
        }
        /// <summary>
        /// 优惠券描述
        /// </summary>
        public string CouponDesc
        {
            set { _coupondesc = value; }
            get { return _coupondesc; }
        }
        /// <summary>
        /// 生成数量
        /// </summary>
        public int SendCount
        {
            set { _sendcount = value; }
            get { return _sendcount; }
        }
        /// <summary>
        /// 兑换所需积分
        /// </summary>
        public int NeedPoint
        {
            set { _needpoint = value; }
            get { return _needpoint; }
        }
        /// <summary>
        /// 是否需要密码 0：不需要  1：需要
        /// </summary>
        public int IsPwd
        {
            set { _ispwd = value; }
            get { return _ispwd; }
        }
        /// <summary>
        /// 是否重用  0： 否 1：是
        /// </summary>
        public int IsReuse
        {
            set { _isreuse = value; }
            get { return _isreuse; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 推荐状态  
        /// </summary>
        public int Recommend
        {
            set { _recommend = value; }
            get { return _recommend; }
        }
        /// <summary>
        /// 开始时间  
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束时间 
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CreateUserId
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 优惠券类型 0：普通优惠券  1：积分兑换优惠券
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 生成优惠券长度 （不包括前缀）
        /// </summary>
        public int CpLength
        {
            set { _cplength = value; }
            get { return _cplength; }
        }
        /// <summary>
        /// 优惠券密码长度
        /// </summary>
        public int PwdLength
        {
            set { _pwdlength = value; }
            get { return _pwdlength; }
        }

        /// <summary>
        /// 优惠券使用类型，0：B2C商城，1：微信商城
        /// </summary>
        public Int16 UseType
        {
            get { return _useType; }
            set { _useType = value; }
        }

        /// <summary>
        /// 优惠券发放类型：0：手动发放，1：自动发放
        /// </summary>
        public Int16 AutoState
        {
            get { return _autoState; }
            set { _autoState = value; }
        }
        #endregion Model

	}
}

