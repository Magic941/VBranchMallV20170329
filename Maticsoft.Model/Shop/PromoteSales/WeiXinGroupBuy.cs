/**
* GroupBuy.cs
*
* 功 能： N/A
* 类 名： GroupBuy
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/14 15:51:55   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.PromoteSales
{
    /// <summary>
    /// GroupBuy:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class WeiXinGroupBuy
    {
        public WeiXinGroupBuy()
        { }
        #region Model
        private int _groupbuyid;
        private long _productid;
        private int _sequence;
        private string _subhead;
        private decimal _fineprice;
        private DateTime _startdate;
        private DateTime _enddate;
        private int _maxcount;
        private int _groupcount;
        private int _buycount;
        private decimal _price;
        private int _status;
        private string _description;
        private int _regionid;
        private string _productname;
        private string _productcategory;
        private string _groupbuyimage;
        private int _categoryId;
        private string _categoryPath;
        private int _GroupBase;
        private int _GoodsTypeID;
        private int _FloorID;
        private int _IsIndex;
        private int _leastbuynum; //最低起订量
        
        private int _GoodsActiveType;
        /// <summary>
        /// 商品活动分类
        /// </summary>
        public int GoodsActiveType
        {
            get { return _GoodsActiveType; }
            set { _GoodsActiveType = value; }
        }

        /// <summary>
        /// 商品类型
        /// </summary>
        public int GoodsTypeID
        {
            get { return _GoodsTypeID; }
            set { _GoodsTypeID = value; }
        }
        /// <summary>
        /// 楼层
        /// </summary>
        public int FloorID
        {
            get { return _FloorID; }
            set { _FloorID = value; }
        }
        /// <summary>
        /// 是否是首页推荐
        /// </summary>
        public int IsIndex
        {
            get { return _IsIndex; }
            set { _IsIndex = value; }
        }

        public int PromotionLimitQu { get; set; }

        public int PromotionType { get; set; }

        //商品市场价
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 团购主键ID
        /// </summary>
        public int GroupBuyId
        {
            set { _groupbuyid = value; }
            get { return _groupbuyid; }
        }
        /// <summary>
        /// 商品副标题
        /// </summary>
        public string Subhead
        {
            get { return _subhead; }
            set { _subhead = value; }
        }
        public int RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 团购商品ID
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 违约金
        /// </summary>
        public decimal FinePrice
        {
            set { _fineprice = value; }
            get { return _fineprice; }
        }
        /// <summary>
        /// 活动开始时间
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
        /// 限购数量
        /// </summary>
        public int MaxCount
        {
            set { _maxcount = value; }
            get { return _maxcount; }
        }
        /// <summary>
        /// 团购满足数量
        /// </summary>
        public int GroupCount
        {
            set { _groupcount = value; }
            get { return _groupcount; }
        }
        /// <summary>
        /// 已经购买数量
        /// </summary>
        public int BuyCount
        {
            set { _buycount = value; }
            get { return _buycount; }
        }
        /// <summary>
        /// 团购价格
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 活动状态 0：未审核 1 ：审核通过  2：活动结束
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 活动说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }

        /// <summary>
        /// 商品分类
        /// </summary>
        public string ProductCategory
        {
            set { _productcategory = value; }
            get { return _productcategory; }
        }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string GroupBuyImage
        {
            set { _groupbuyimage = value; }
            get { return _groupbuyimage; }
        }
        public int CategoryId
        {
            set { _categoryId = value; }
            get { return _categoryId; }
        }
        public string CategoryPath
        {
            set { _categoryPath = value; }
            get { return _categoryPath; }
        }
        /// <summary>
        /// 团购基数
        /// </summary>
        public int GroupBase
        {
            set { _GroupBase = value; }
            get { return _GroupBase; }
        }
        /// <summary>
        /// 最低起订量
        /// </summary>
        public int LeastbuyNum 
        {
            set { _leastbuynum = value; }
            get { return _leastbuynum; }
        }


        #endregion Model

    }
}

