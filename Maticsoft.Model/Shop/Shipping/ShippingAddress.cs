/**
* ShippingAddress.cs
*
* 功 能： N/A
* 类 名： ShippingAddress
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 10:24:44   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Shipping
{
	/// <summary>
	/// ShippingAddress:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShippingAddress
	{
		public ShippingAddress()
		{}
        #region Model
        private int _shippingid;
        private int _regionid;
        private int _userid;
        private string _shipname;
        private string _address;
        private string _zipcode;
        private string _emailaddress;
        private string _telphone;
        private string _celphone;
        private bool _isDefault;

        
        /// <summary>
        /// 
        /// </summary>
        public int ShippingId
        {
            set { _shippingid = value; }
            get { return _shippingid; }
        }
        /// <summary>
        /// 区域ID
        /// </summary>
        public int RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 用户 ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string ShipName
        {
            set { _shipname = value; }
            get { return _shipname; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Zipcode
        {
            set { _zipcode = value; }
            get { return _zipcode; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmailAddress
        {
            set { _emailaddress = value; }
            get { return _emailaddress; }
        }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string CelPhone
        {
            set { _celphone = value; }
            get { return _celphone; }
        }
        #endregion Model

        /// <summary>
        /// 区域完整名
        /// </summary>
	    public string RegionFullName { get; set; }

        /// <summary>
        /// 是否为默认收货地址
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }
	}
}

