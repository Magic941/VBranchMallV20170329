using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;

namespace ServiceStack.RedisCache.Products
{
    public class ProductImage
    {
        private readonly IProductImage dal = DAShopProducts.CreateProductImage();

        /// <summary>
        /// 得到所有商品图片信息，从缓存中
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductImage> GetAllProductImage()
        {
            List<Maticsoft.Model.Shop.Products.ProductImage> productImages = new List<Maticsoft.Model.Shop.Products.ProductImage>();
            var redisClient = RedisManager.GetClient();
            var productImage = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductImage>();

            var pKeyList = productImage.GetAllKeys();
            foreach (var p in pKeyList)
            {
                Maticsoft.Model.Shop.Products.ProductImage product = new Maticsoft.Model.Shop.Products.ProductImage();
                product = productImage.GetValue(p);
                productImages.Add(product);
            }

            return productImages;
        }

        /// <summary>
        /// 保存所有产品图片信息，缓存
        /// </summary>
        public bool SaveAllProductImage()
        {
            //List<Maticsoft.Model.Shop.Products.ProductImage> productImages = new List<Maticsoft.Model.Shop.Products.ProductImage>();
            var redisClient = RedisManager.GetClient();

            DataTable dt = dal.GetList("").Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Maticsoft.Model.Shop.Products.ProductImage p = new Maticsoft.Model.Shop.Products.ProductImage();
                if (dt.Rows[0]["ProductImageId"] != null && dt.Rows[0]["ProductImageId"].ToString() != "")
                {
                    p.ProductImageId = int.Parse(dt.Rows[0]["ProductImageId"].ToString());
                }
                if (dt.Rows[0]["ProductId"] != null && dt.Rows[0]["ProductId"].ToString() != "")
                {
                    p.ProductId = long.Parse(dt.Rows[0]["ProductId"].ToString());
                }
                if (dt.Rows[0]["ImageUrl"] != null && dt.Rows[0]["ImageUrl"].ToString() != "")
                {
                    p.ImageUrl = dt.Rows[0]["ImageUrl"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl1"] != null && dt.Rows[0]["ThumbnailUrl1"].ToString() != "")
                {
                    p.ThumbnailUrl1 = dt.Rows[0]["ThumbnailUrl1"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl2"] != null && dt.Rows[0]["ThumbnailUrl2"].ToString() != "")
                {
                    p.ThumbnailUrl2 = dt.Rows[0]["ThumbnailUrl2"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl3"] != null && dt.Rows[0]["ThumbnailUrl3"].ToString() != "")
                {
                    p.ThumbnailUrl3 = dt.Rows[0]["ThumbnailUrl3"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl4"] != null && dt.Rows[0]["ThumbnailUrl4"].ToString() != "")
                {
                    p.ThumbnailUrl4 = dt.Rows[0]["ThumbnailUrl4"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl5"] != null && dt.Rows[0]["ThumbnailUrl5"].ToString() != "")
                {
                    p.ThumbnailUrl5 = dt.Rows[0]["ThumbnailUrl5"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl6"] != null && dt.Rows[0]["ThumbnailUrl6"].ToString() != "")
                {
                    p.ThumbnailUrl6 = dt.Rows[0]["ThumbnailUrl6"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl7"] != null && dt.Rows[0]["ThumbnailUrl7"].ToString() != "")
                {
                    p.ThumbnailUrl7 = dt.Rows[0]["ThumbnailUrl7"].ToString();
                }
                if (dt.Rows[0]["ThumbnailUrl8"] != null && dt.Rows[0]["ThumbnailUrl8"].ToString() != "")
                {
                    p.ThumbnailUrl8 = dt.Rows[0]["ThumbnailUrl8"].ToString();
                }
                //p = dal.DataRowToModel(dt.Rows[i]);
                redisClient.Add("ProductImagesModel-" + p.ProductImageId.ToString(), p);
            }
            return true;
        }

        /// <summary>
        /// 删除所有分类，从缓存中
        /// </summary>
        public bool DeleteAllProductImage()
        {
            var redisClient = RedisManager.GetClient();
            var productImage = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductImage>();
            var pKeyList = productImage.GetAllKeys();
            foreach (var p in pKeyList)
            {
                redisClient.Remove(p);
            }
            return true;
        }

        /// <summary>
        /// 更新一个对象实体，从缓存中
        /// </summary>
        public bool UpdateProductImage(Maticsoft.Model.Shop.Products.ProductImage ProductImageModle)
        {
            var redisClient = RedisManager.GetClient();
            redisClient.Set("ProductImagesModel-" + ProductImageModle.ProductImageId.ToString(), ProductImageModle);
            return true;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductImage GetModelByProductImageId(int ProductImageId)
        {
            var redisClient = RedisManager.GetClient();
            var productImage = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductImage>();
            return productImage.GetValue("ProductImagesModel-" + ProductImageId.ToString());
        }

        /// <summary>
        /// 删除一个对象实体，从缓存中
        /// </summary>
        public bool DeleteGroupBuy(Maticsoft.Model.Shop.Products.ProductImage ProductImageModle)
        {
            var redisClient = RedisManager.GetClient();
            redisClient.Remove("ProductImagesModel-" + ProductImageModle.ProductImageId.ToString());
            return true;
        }
    }
}
