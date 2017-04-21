using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppServices.BLL
{
    public class ProductInfo
    {
        /// <summary>
        /// 商品推荐
        /// </summary>
        public static List<AppServices.Models.ProductInfo> ProductRecTableToList(List<Maticsoft.Model.Shop.Products.ProductInfo> ProductInfoLists)
        {
            List<AppServices.Models.ProductInfo> modelList = new List<AppServices.Models.ProductInfo>();
            AppServices.Models.ProductInfo model;
            foreach (Maticsoft.Model.Shop.Products.ProductInfo items in ProductInfoLists)
            {
                model = new AppServices.Models.ProductInfo();
                model.ProductId = items.ProductId;
                model.ProductName = items.ProductName;
                model.ProductCode = items.ProductCode;
                model.ThumbnailUrl1 = items.ThumbnailUrl1;
                model.ThumbnailUrl2 = items.ThumbnailUrl2;
                model.ShortDescription = items.ShortDescription;
                model.LowestSalePrice = items.LowestSalePrice;
                model.MarketPrice = items.MarketPrice;
                model.ImageUrl = items.ImageUrl;
                modelList.Add(model);
            }

            return modelList;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static AppServices.Models.ProductInfo ProductInfoToModel(Maticsoft.Model.Shop.Products.ProductInfo ProductInfoMode)
        {
            AppServices.Models.ProductInfo model = new AppServices.Models.ProductInfo();
            if (ProductInfoMode != null)
            {

                model.CategoryId = ProductInfoMode.CategoryId;


                model.TypeId = ProductInfoMode.TypeId;


                model.ProductId = ProductInfoMode.ProductId;


                model.BrandId = ProductInfoMode.BrandId;


                model.ProductName = ProductInfoMode.ProductName;


                model.Subhead = ProductInfoMode.Subhead;


                model.ProductCode = ProductInfoMode.ProductCode;


                model.SupplierId = ProductInfoMode.SupplierId;


                model.RegionId = ProductInfoMode.RegionId;


                model.ShortDescription = ProductInfoMode.ShortDescription;


                model.Unit = ProductInfoMode.Unit;

                model.Description = ProductInfoMode.Description;

                model.Meta_Title = ProductInfoMode.Meta_Title;


                model.Meta_Description = ProductInfoMode.Meta_Description;


                model.Meta_Keywords = ProductInfoMode.Meta_Keywords;


                model.SaleStatus = ProductInfoMode.SaleStatus;


                model.AddedDate = ProductInfoMode.AddedDate;


                model.VistiCounts = ProductInfoMode.VistiCounts;


                model.SaleCounts = ProductInfoMode.SaleCounts;


                model.DisplaySequence = ProductInfoMode.DisplaySequence;


                model.LineId = ProductInfoMode.LineId;


                model.MarketPrice = ProductInfoMode.MarketPrice;


                model.LowestSalePrice = ProductInfoMode.LowestSalePrice;


                model.PenetrationStatus = ProductInfoMode.PenetrationStatus;


                model.MainCategoryPath = ProductInfoMode.MainCategoryPath;


                model.ExtendCategoryPath = ProductInfoMode.ExtendCategoryPath;


                model.HasSKU = ProductInfoMode.HasSKU;


                model.Points = ProductInfoMode.Points;


                model.ImageUrl = ProductInfoMode.ImageUrl;


                model.ThumbnailUrl1 = ProductInfoMode.ThumbnailUrl1;


                model.ThumbnailUrl2 = ProductInfoMode.ThumbnailUrl2;


                model.ThumbnailUrl3 = ProductInfoMode.ThumbnailUrl3;


                model.ThumbnailUrl4 = ProductInfoMode.ThumbnailUrl4;


                model.ThumbnailUrl5 = ProductInfoMode.ThumbnailUrl5;



                model.MaxQuantity = ProductInfoMode.MaxQuantity;


                model.MinQuantity = ProductInfoMode.MinQuantity;


                model.Tags = ProductInfoMode.Tags;


                model.ImportPro = ProductInfoMode.ImportPro;

                model.Name = ProductInfoMode.Name;


                model.FalseSaleCount = ProductInfoMode.FalseSaleCount;


            }
            return model;
        }
    }
}