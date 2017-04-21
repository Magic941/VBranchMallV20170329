/**
* CouponHistory.cs
*
* 功 能： N/A
* 类 名： CouponHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:20:57   N/A    初版
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
	/// CouponHistory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CouponHistory
	{
		public CouponHistory()
		{}
        #region Model
        private string _couponcode;
        private int _categoryid;
        private int _classid;
        private int _supplierid;
        private int _ruleid;
        private string _couponname;
        private string _couponpwd;
        private int _userid;
        private string _useremail;
        private int _status;
        private decimal _couponprice;
        private decimal _limitprice;
        private int _needpoint;
        private int _ispwd;
        private int _isreuse;
        private DateTime _startdate;
        private DateTime _enddate;
        private DateTime _generatetime;
        private DateTime? _useddate;
        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string CouponCode
        {
            set { _couponcode = value; }
            get { return _couponcode; }
        }
        /// <summary>
        /// 商品分类ID
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
        /// 优惠券ID
        /// </summary>
        public int RuleId
        {
            set { _ruleid = value; }
            get { return _ruleid; }
        }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName
        {
            set { _couponname = value; }
            get { return _couponname; }
        }
        /// <summary>
        /// 优惠券 密码
        /// </summary>
        public string CouponPwd
        {
            set { _couponpwd = value; }
            get { return _couponpwd; }
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
        /// 
        /// </summary>
        public string UserEmail
        {
            set { _useremail = value; }
            get { return _useremail; }
        }
        /// <summary>
        /// 状态 0：表示未分配 1：表示已分配 2：表示已使用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
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
        public DateTime GenerateTime
        {
            set { _generatetime = value; }
            get { return _generatetime; }
        }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? UsedDate
        {
            set { _useddate = value; }
            get { return _useddate; }
        }
        #endregion Model

	}
}

