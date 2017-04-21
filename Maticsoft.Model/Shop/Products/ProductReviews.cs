/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductReviews.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/08/27 14:50:43
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// 商品评论表
	/// </summary>
	[Serializable]
	public partial class ProductReviews
	{
		public ProductReviews()
		{}
        #region Model
        private int _reviewid;
        private long _productid;
        private int _userid;
        private string _reviewtext;
        private string _username;
        private string _useremail;
        private DateTime _createddate;
        private int _parentid;
        private int _status;
        private long _orderid = 0;
        private string _sku;
        private string _attribute;
        private string _imagespath;
        private string _imagesnames;
        /// <summary>
        /// 评论ID
        /// </summary>
        public int ReviewId
        {
            set { _reviewid = value; }
            get { return _reviewid; }
        }
        /// <summary>
        /// 评论商品
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 评论人
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string ReviewText
        {
            set { _reviewtext = value; }
            get { return _reviewtext; }
        }
        /// <summary>
        /// 评论人姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 评论人邮箱
        /// </summary>
        public string UserEmail
        {
            set { _useremail = value; }
            get { return _useremail; }
        }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 评论状态 0：未审核 1：审核通过 2：审核失败
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 订单的ID
        /// </summary>
        public long OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// SKU
        /// </summary>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }
        /// <summary>
        /// 属性
        /// </summary>
        public string Attribute
        {
            set { _attribute = value; }
            get { return _attribute; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagesPath
        {
            set { _imagespath = value; }
            get { return _imagespath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImagesNames
        {
            set { _imagesnames = value; }
            get { return _imagesnames; }
        }
        #endregion Model

	}
}

