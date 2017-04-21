using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Products.Lucene
{

    /// <summary>
    /// 商品字段索引信息
    /// </summary>
    public class ProductIndexField
    {
        /// <summary>
        /// 存储不索引
        /// </summary>
        public const string CURRENTPRICE = "price";

        /// <summary>
        /// 存储不索引
        /// </summary>
        public const string MARKETPRICE = "marketprice";

        /// <summary>
        /// 存储不索引
        /// </summary>
        public const string PRODUCT = "product";

        /// <summary>
        /// 存储不索引
        /// </summary>
        public const string BRANDID = "brandid";
       

        /// <summary>
        /// 存储不索引, 商吕所属分类编号，一个商品有多分类，我们对多分类多次存储，方便用编号进行过滤
        /// </summary>
        public const string CATEGORYID = "categoryid";
      

        /// <summary>
        /// 商品属于多分类，索引多次，检查时可以distinct一下
        /// </summary>      
        public const string CATEGORYNAME = "categoryname";
        /// <summary>
        /// 商品属于多分类，索引多次，检查时可以distinct一下
        /// </summary>      
        public const string CATEGORYPATH = "categorypath";

        
//--------------------------------------------------------------------------------
        /// <summary>
        /// 存储不索引 商铺编号,
        /// </summary>
        public const string SUPPLIERID = "supplierid";

        /// <summary>
        /// 存储不索引
        /// </summary>
        public const string SALEQTY = "saleqty";

        /// <summary>
        /// 存储不索引  上线时间
        /// </summary>
        public const string LAUNCHTIME = "launchtime";

       

        //-------------------------------------------------------
        public const string BRANDNAME = "brandname";
        /// <summary>
        /// 评论数量，存储不索引
        /// </summary>
        public const string COMMENTCOUNT = "commentcount";

        /// <summary>
        /// 好评得分
        /// </summary>
        public const string COMMENTSCORE = "commentscore";

        /// <summary>
        /// 商品标签  存储且索引
        /// </summary>
        public const string PRODUCTTAGS = "producttags";


        /// <summary>
        /// 商品分类标签
        /// </summary>
        public const string PRODUCTCATEGORYTAGS = "productcategorytags";

        /// <summary>
        /// 存储且索引、商品属性信息，按分隔符分隔
        /// </summary>
        public const string ATTRIBUTENAME = "attribute";

        /// <summary>
        /// 存储不索引，商品的系统编号
        /// </summary>
        public const string PRODUCTID = "productid";
        //--------------------------------------------------------------------
        public const string PRODUCTNAME = "productname";
        /// <summary>
        /// 商品型号，要检索
        /// </summary>
        public const string PRODUCTMODE = "productmode";

        /// <summary>
        ///商城副标题 
        /// </summary>
        public const string SUBHEAD = "subhead";

        /// <summary>
        /// 存储不索引 商铺名字
        /// </summary>
        public const string SUPPLIERNAME = "suppliername";

        /// <summary>
        /// 存储且索引 
        /// </summary>
        public const string PRODUCTCODE = "productcode";

        public const string KEYWORDS = "keywords";
        //public const string PRODUCTNAME = "productname";

        //public const string BRANDNAME = "brandname";
        /// <summary>
        /// 存储且索引
        /// </summary>
        public const string SEOKEYWORD = "seokeyword";
        //--------------------------
        public const string SEOTITLE = "seotitle";
        public const string SEODESCRIPTION = "seodescription";

        /// <summary>
        /// 索引但不存储，太大
        /// </summary>
        public const string PRODUCTDESCLONG = "productdesclong";

        /// <summary>
        /// 存储 不索引 供应商的分类id，商家有自已的分类
        /// </summary>
        public const string SUPPPARENTCATEGORYID = "suppparentcategoryid";
        
        /// <summary>
        /// 商家商品所在分类名称 存储且索引 分类名字
        /// </summary>
        public const string SUPPCATEGORYNAME = "suppcategoryname";

        /// <summary>
        /// 商品图片主图
        /// </summary>
        public const string IMAGEURL = "imageurl";


        // <summary>
        /// 商品图片缩略图
        /// </summary>
        public const string THUMBNAILURL = "thumbnailurl";

        // <summary>
        /// 商品所属所有的类目名称，方便检索
        /// </summary>
        public const string ALLCATEGORYNAMES = "AllCategoryNames";
    }
}
