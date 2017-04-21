/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Products.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:27
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using Maticsoft.Model.Shop.PromoteSales;
using Maticsoft.Model.Shop.Products.Lucene;

namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// ProductInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProductInfo
    {
        public ProductInfo()
        { }
        #region Model
        private int _categoryid;


        private int _categoryid1;
        private int _categoryid2;
        private int _categoryid3;

        private int? _typeid;
        private long _productid;
        private int _brandid;

        private string _productname;
        private string _subhead;
        private string _productcode;
        private int _supplierid;
        private string _Name;
        private int? _regionid;
        private string _shortdescription;
        private string _unit;
        private string _description;
        private string _meta_title;
        private string _meta_description;
        private string _meta_keywords;
        private int _salestatus;
        private DateTime _addeddate;
        private int _visticounts = 0;
        private int _salecounts = 0;
        private int _displaysequence = 0;
        private int _lineid;
        private decimal? _marketprice;
        private decimal _lowestsaleprice;
        private int _penetrationstatus;
        private string _maincategorypath;
        private string _extendcategorypath;
        private bool _hassku;
        private decimal? _points;
        private string _imageurl;
        private string _thumbnailurl1;
        private string _thumbnailurl2;
        private string _thumbnailurl3;
        private string _thumbnailurl4;
        private string _thumbnailurl5;
        private string _thumbnailurl6;
        private string _thumbnailurl7;
        private string _thumbnailurl8;
        private int? _maxquantity;
        private int? _minquantity;
        private string _tags;
        private string _seourl;
        private string _seoimagealt;
        private string _seoimagetitle;
        private decimal? _rebatevalue;
        private decimal? _distributionprice;
        private int _ImportPro;

        //是否是进口商品 1:是 0:不是
        public int ImportPro
        {
            get { return _ImportPro; }
            set { _ImportPro = value; }
        }

        public int PromotionType { get; set; }

        private int _FalseSaleCount;

        /// <summary>
        /// 销售的基数（假销售量）
        /// </summary>
        public int FalseSaleCount
        {
            get { return _FalseSaleCount; }
            set { _FalseSaleCount = value; }
        }



        //秒杀描述
        private string _CountDownDescription;

        public string CountDownDescription
        {
            get { return _CountDownDescription; }
            set { _CountDownDescription = value; }
        }



        /// <summary>
        /// 分销价 add by zhongyu 2014/6/24
        /// </summary>
        public decimal? DistributionPrice
        {
            get { return _distributionprice; }
            set { _distributionprice = value; }
        }

        /// <summary>
        /// 商品索引类型
        /// </summary>
        public IndexType ProductIndexType
        {
            set;
            get;
        }

        /// <summary>
        /// 商品副标题
        /// </summary>
        public string Subhead
        {
            get { return _subhead; }
            set { _subhead = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName
        {
            get;
            set;
        }

        private string _staticurl;
        /// <summary>
        /// 目前在商品表该字段已无用，商品可能属于多个分类.全部被一级分类、二级分类、三级分类代替
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }

        public string CategoryName
        {
            get;
            set;
        }

        /// <summary>
        /// 分类路径
        /// </summary>
        public string CategoryPath
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }

        public int BrandId
        {
            set { _brandid = value; }
            get { return _brandid; }
        }

        public string BrandName
        {
            get;
            set;
        }

        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortDescription
        {
            set { _shortdescription = value; }
            get { return _shortdescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Title
        {
            set { _meta_title = value; }
            get { return _meta_title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Description
        {
            set { _meta_description = value; }
            get { return _meta_description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Keywords
        {
            set { _meta_keywords = value; }
            get { return _meta_keywords; }
        }
        /// <summary>
        ///-1:未审核 0:下架(仓库中)  1:上架 2:已删除
        /// </summary>
        public int SaleStatus
        {
            set { _salestatus = value; }
            get { return _salestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddedDate
        {
            set { _addeddate = value; }
            get { return _addeddate; }
        }
        /// <summary>
        /// 访问次数
        /// </summary>
        public int VistiCounts
        {
            set { _visticounts = value; }
            get { return _visticounts; }
        }
        /// <summary>
        /// 售出总数
        /// </summary>
        public int SaleCounts
        {
            set { _salecounts = value; }
            get { return _salecounts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DisplaySequence
        {
            set { _displaysequence = value; }
            get { return _displaysequence; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LineId
        {
            set { _lineid = value; }
            get { return _lineid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MarketPrice
        {
            set { _marketprice = value; }
            get { return _marketprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal LowestSalePrice
        {
            set { _lowestsaleprice = value; }
            get { return _lowestsaleprice; }
        }
        /// <summary>
        /// 铺货状态: 0未铺货 1已铺货
        /// </summary>
        public int PenetrationStatus
        {
            set { _penetrationstatus = value; }
            get { return _penetrationstatus; }
        }

        public string MainCategoryPath
        {
            set { _maincategorypath = value; }
            get { return _maincategorypath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtendCategoryPath
        {
            set { _extendcategorypath = value; }
            get { return _extendcategorypath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasSKU
        {
            set { _hassku = value; }
            get { return _hassku; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Points
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl1
        {
            set { _thumbnailurl1 = value; }
            get { return _thumbnailurl1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl2
        {
            set { _thumbnailurl2 = value; }
            get { return _thumbnailurl2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl3
        {
            set { _thumbnailurl3 = value; }
            get { return _thumbnailurl3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl4
        {
            set { _thumbnailurl4 = value; }
            get { return _thumbnailurl4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl5
        {
            set { _thumbnailurl5 = value; }
            get { return _thumbnailurl5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl6
        {
            set { _thumbnailurl6 = value; }
            get { return _thumbnailurl6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl7
        {
            set { _thumbnailurl7 = value; }
            get { return _thumbnailurl7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl8
        {
            set { _thumbnailurl8 = value; }
            get { return _thumbnailurl8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MaxQuantity
        {
            set { _maxquantity = value; }
            get { return _maxquantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MinQuantity
        {
            set { _minquantity = value; }
            get { return _minquantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }

        /// <summary>
        /// 所属分类的标签
        /// </summary>
        public string CTags
        {
            get;
            set;
        }

        /// <summary>
        /// SEO Url地址优化规则
        /// </summary>
        public string SeoUrl
        {
            set { _seourl = value; }
            get { return _seourl; }
        }
        /// <summary>
        /// SEO 图片Alt信息
        /// </summary>
        public string SeoImageAlt
        {
            set { _seoimagealt = value; }
            get { return _seoimagealt; }
        }
        /// <summary>
        /// SEO 图片Title信息
        /// </summary>
        public string SeoImageTitle
        {
            set { _seoimagetitle = value; }
            get { return _seoimagetitle; }
        }
        #endregion Model
        #region 扩展属性
        /// <summary>
        /// 
        /// </summary>
        public string StaticUrl
        {
            set { _staticurl = value; }
            get { return _staticurl; }
        }

        public decimal SalePrice
        {
            set;
            get;
        }




        #region 促销扩展属性
        /// <summary>
        /// 促销价格
        /// </summary>
        public decimal ProSalesPrice
        {
            set;
            get;
        }

        /// <summary>
        /// 促销ID
        /// </summary>
        public int CountDownId
        {
            set;
            get;
        }
        /// <summary>
        /// 促销结束时间
        /// </summary>
        public DateTime ProSalesEndDate
        {
            set;
            get;
        }


        #endregion
        #region 团购
        /// <summary>
        /// 团购ID
        /// </summary>
        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GroupBuy = new GroupBuy();

        #endregion

        #region 商家分类
        /// <summary>
        /// 分类id
        /// </summary>
        public int SuppCategoryId
        {
            set;
            get;
        }

        /// <summary>
        /// 分类Path
        /// </summary>
        public string SuppCategoryPath
        {
            set;
            get;
        }
        #endregion
        #region 商家扩展属性
        /// <summary>
        /// 商家商品分类的父分类Id
        /// </summary>
        public int SuppParentCategoryId
        {
            set;
            get;
        }
        /// <summary>
        /// 商家商品分类名称
        /// </summary>
        public string SuppCategoryName
        {
            set;
            get;
        }
        #endregion

        #region 扩展免邮相关属性
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int createrid { get; set; }
        public DateTime createdate { get; set; }
        #endregion

        /// <summary>
        /// 库存
        /// </summary>
        public long StockNum
        {
            set;
            get;
        }
        #endregion

        /// <summary>
        /// 所有类目名称
        /// </summary>
        public string ALLCategoryNames
        {
            set;
            get;
        }
    }


    /// <summary>
    /// 商品索引特定类
    /// </summary>
    public partial class ProductInfoForProductIndex
    {
        /// <summary>
        /// 商品索引类型
        /// </summary>
        public IndexType ProductIndexType
        {
            set;
            get;
        }

        /// <summary>
        /// 商品副标题
        /// </summary>
        public string Subhead
        {
            get;
            set;
        }


        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName
        {
            get;
            set;
        }

        private string _staticurl;
        /// <summary>
        /// 目前在商品表该字段已无用，商品可能属于多个分类.全部被一级分类、二级分类、三级分类代替
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }

        /// <summary>
        /// 分类路径
        /// </summary>
        public string CategoryPath
        {
            get;
            set;
        }

        /// <summary>
        /// 商品类型编号
        /// </summary>
        public int? TypeId
        {
            get;
            set;
        }

        public long ProductId
        {
            get;
            set;
        }

        public int BrandId
        {
            get;
            set;
        }

        public string BrandName
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCode
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RegionId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Title
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Description
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Keywords
        {
            get;
            set;
        }
        /// <summary>
        ///-1:未审核 0:下架(仓库中)  1:上架 2:已删除
        /// </summary>
        public int SaleStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddedDate
        {
            get;
            set;
        }
        /// <summary>
        /// 访问次数
        /// </summary>
        public int VistiCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 售出总数
        /// </summary>
        public int SaleCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int DisplaySequence
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int LineId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MarketPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal LowestSalePrice
        {
            get;
            set;
        }
        /// <summary>
        /// 铺货状态: 0未铺货 1已铺货
        /// </summary>
        public int PenetrationStatus
        {
            get;
            set;
        }

        public string MainCategoryPath
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtendCategoryPath
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasSKU
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Points
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl1
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? MaxQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MinQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tags
        {
            get;
            set;
        }
        /// <summary>
        /// SEO Url地址优化规则
        /// </summary>
        public string SeoUrl
        {
            get;
            set;
        }
        /// <summary>
        /// SEO 图片Alt信息
        /// </summary>
        public string SeoImageAlt
        {
            get;
            set;
        }
        /// <summary>
        /// SEO 图片Title信息
        /// </summary>
        public string SeoImageTitle
        {
            get;
            set;
        }

        public string StaticUrl
        {
            get;
            set;
        }

        public decimal SalePrice
        {
            set;
            get;
        }





        public decimal ProSalesPrice
        {
            set;
            get;
        }

        /// <summary>
        /// 促销ID
        /// </summary>
        public int CountDownId
        {
            set;
            get;
        }
        /// <summary>
        /// 促销结束时间
        /// </summary>
        public DateTime ProSalesEndDate
        {
            set;
            get;
        }





        #region 商家分类
        /// <summary>
        /// 分类id
        /// </summary>
        public int SuppCategoryId
        {
            set;
            get;
        }

        /// <summary>
        /// 分类Path
        /// </summary>
        public string SuppCategoryPath
        {
            set;
            get;
        }
        #endregion
        #region 商家扩展属性
        /// <summary>
        /// 商家商品分类的父分类Id
        /// </summary>
        public int SuppParentCategoryId
        {
            set;
            get;
        }
        /// <summary>
        /// 商家商品分类名称
        /// </summary>
        public string SuppCategoryName
        {
            set;
            get;
        }
        #endregion

        /// <summary>
        /// 库存
        /// </summary>
        public long StockNum
        {
            set;
            get;
        }

        /// <summary>
        /// 所有类目名称  一级类型目》二级类型目》三级类目
        /// </summary>
        public string ALLCategoryNames
        {
            set;
            get;
        }


    }
}
