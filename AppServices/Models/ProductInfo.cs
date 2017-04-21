using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppServices.Models
{
  
    public  class ProductInfo
    {
        public ProductInfo()
        { }
        #region Model
       

        //是否是进口商品 1:是 0:不是
        public int ImportPro { get; set; }
      

        public int PromotionType { get; set; }

     

        /// <summary>
        /// 销售的基数（假销售量）
        /// </summary>
        public int FalseSaleCount
        { get; set; }



        //秒杀描述
      

        public string CountDownDescription{ get; set; }



        /// <summary>
        /// 分销价 add by zhongyu 2014/6/24
        /// </summary>
        public decimal? DistributionPrice { get; set; }

     

        /// <summary>
        /// 商品副标题
        /// </summary>
        public string Subhead { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

       
        /// <summary>
        /// 目前在商品表该字段已无用，商品可能属于多个分类.全部被一级分类、二级分类、三级分类代替
        /// </summary>
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        /// <summary>
        /// 分类路径
        /// </summary>
        public string CategoryPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ProductId { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? RegionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Keywords { get; set; }
        /// <summary>
        ///-1:未审核 0:下架(仓库中)  1:上架 2:已删除
        /// </summary>
        public int SaleStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddedDate { get; set; }
        /// <summary>
        /// 访问次数
        /// </summary>
        public int VistiCounts { get; set; }
        /// <summary>
        /// 售出总数
        /// </summary>
        public int SaleCounts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DisplaySequence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LineId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MarketPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal LowestSalePrice { get; set; }
        /// <summary>
        /// 铺货状态: 0未铺货 1已铺货
        /// </summary>
        public int PenetrationStatus { get; set; }

        public string MainCategoryPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ExtendCategoryPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HasSKU { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Points { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl6 { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public int? MaxQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MinQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 所属分类的标签
        /// </summary>
        public string CTags
        {
            get;
            set;
        }

     
        #endregion Model
       
    }
}