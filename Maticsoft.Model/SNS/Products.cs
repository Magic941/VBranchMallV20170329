/**
* Products.cs
*
* 功 能： N/A
* 类 名： Products
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:49   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 商品
	/// </summary>
	[Serializable]
	public partial class Products
	{
		public Products()
		{}
        #region Model
        private long _productid;
        private string _productname;
        private string _productdescription;
        private decimal? _price;
        private int? _productsourceid;
        private int? _categoryid;
        private string _producturl;
        private int _favouritecount = 0;
        private int? _groupbuycount = 0;
        private int _createuserid;
        private string _creatednickname;
        private string _thumbimageurl;
        private string _normalimageurl;
        private int _pvcount = 0;
        private int _isrecomend = 0;
        private int _status;
        private int _sequence = 0;
        private string _topcommentsid;
        private int _commentcount = 0;
        private int? _forwardedcount = 0;
        private string _sharedescription;
        private int _skipcount = 0;
        private int? _ownerproductid;
        private string _tags;
        private DateTime _createddate;
        private string _color;
        private long? _originalid;
        private int? _sourcetype;
        private string _staticurl;
        /// <summary>
        /// 产品的类型
        /// </summary>
        public long ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 产品的名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 产品的描述
        /// </summary>
        public string ProductDescription
        {
            set { _productdescription = value; }
            get { return _productdescription; }
        }
        /// <summary>
        /// 产品的价格
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 商品来源id(淘宝的,还是京东的)
        /// </summary>
        public int? ProductSourceID
        {
            set { _productsourceid = value; }
            get { return _productsourceid; }
        }
        /// <summary>
        /// 产品所属的分类
        /// </summary>
        public int? CategoryID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 产品所指向的链接
        /// </summary>
        public string ProductUrl
        {
            set { _producturl = value; }
            get { return _producturl; }
        }
        /// <summary>
        /// 被别人喜欢的数量
        /// </summary>
        public int FavouriteCount
        {
            set { _favouritecount = value; }
            get { return _favouritecount; }
        }
        /// <summary>
        /// 申请团购的数量(预留字段)
        /// </summary>
        public int? GroupBuyCount
        {
            set { _groupbuycount = value; }
            get { return _groupbuycount; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatedNickName
        {
            set { _creatednickname = value; }
            get { return _creatednickname; }
        }
        /// <summary>
        /// 缩略小图
        /// </summary>
        public string ThumbImageUrl
        {
            set { _thumbimageurl = value; }
            get { return _thumbimageurl; }
        }
        /// <summary>
        /// 缩略正常图
        /// </summary>
        public string NormalImageUrl
        {
            set { _normalimageurl = value; }
            get { return _normalimageurl; }
        }
        /// <summary>
        /// 浏览数量
        /// </summary>
        public int PVCount
        {
            set { _pvcount = value; }
            get { return _pvcount; }
        }
        /// <summary>
        /// 是否推荐（预留字段）1 推荐到首页
        /// </summary>
        public int IsRecomend
        {
            set { _isrecomend = value; }
            get { return _isrecomend; }
        }
        /// <summary>
        /// 状态 0:未审核 1：已审核  2：审核未通过 3：分类未明确 4：分类已明确
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 前n个对此商品评论的id
        /// </summary>
        public string TopCommentsId
        {
            set { _topcommentsid = value; }
            get { return _topcommentsid; }
        }
        /// <summary>
        /// 评论的数量
        /// </summary>
        public int CommentCount
        {
            set { _commentcount = value; }
            get { return _commentcount; }
        }
        /// <summary>
        /// 被转发的数量
        /// </summary>
        public int? ForwardedCount
        {
            set { _forwardedcount = value; }
            get { return _forwardedcount; }
        }
        /// <summary>
        /// 分享时分享者的说明
        /// </summary>
        public string ShareDescription
        {
            set { _sharedescription = value; }
            get { return _sharedescription; }
        }
        /// <summary>
        /// 跳转到商品内容网站的次数如taobao(后加的)
        /// </summary>
        public int SkipCount
        {
            set { _skipcount = value; }
            get { return _skipcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OwnerProductId
        {
            set { _ownerproductid = value; }
            get { return _ownerproductid; }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        /// <summary>
        /// 上传的日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Color
        {
            set { _color = value; }
            get { return _color; }
        }
        /// <summary>
        /// 原始商品ID （淘宝商品ID或者是京东商品ID）
        /// </summary>
        public long? OriginalID
        {
            set { _originalid = value; }
            get { return _originalid; }
        }
        /// <summary>
        /// 0:表示用户采集  1：表示管理员批量采集
        /// </summary>
        public int? SourceType
        {
            set { _sourcetype = value; }
            get { return _sourcetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StaticUrl
        {
            set { _staticurl = value; }
            get { return _staticurl; }
        }
        #endregion Model

	}
}

