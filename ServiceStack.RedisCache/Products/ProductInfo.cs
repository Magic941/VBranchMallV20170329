using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.Model.Shop.Products;

namespace ServiceStack.RedisCache.Products
{
    public class ProductInfo
    {
        private readonly IProductInfo dal = DAShopProducts.CreateProductInfo();

        /// <summary>
        /// 得到所有商品，从缓存中
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetAllProduct()
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productInfos = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            var redisClient = RedisManager.GetClient();
            var productInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductInfo>();

            var pKeyList = productInfo.GetAllKeys();
            foreach (var p in pKeyList)
            {
                Maticsoft.Model.Shop.Products.ProductInfo product = new Maticsoft.Model.Shop.Products.ProductInfo();
                product = productInfo.GetValue(p);
                productInfos.Add(product);
            }

            return productInfos;
        }

        /// <summary>
        /// 保存所有商品，从缓存中
        /// </summary>
        public bool SaveAllProduct()
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productInfos = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            var redisClient = RedisManager.GetClient();
            var productInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductInfo>();            

            DataTable dt = dal.GetList("").Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Maticsoft.Model.Shop.Products.ProductInfo p = new Maticsoft.Model.Shop.Products.ProductInfo();
                p = dal.DataRowToModel(dt.Rows[i]);
                redisClient.Add("ProductsModel-" + p.ProductId.ToString(), p);
            }
            return true;
        }

        /// <summary>
        /// 删除所有，从缓存中
        /// </summary>
        public bool DeleteAllProduct()
        {
            var redisClient = RedisManager.GetClient();
            var productInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductInfo>();
            var pKeyList = productInfo.GetAllKeys();
            foreach (var p in pKeyList)
            {
                redisClient.Remove(p);
            }
            return true;
        }

        public bool UpdateProduct(Maticsoft.Model.Shop.Products.ProductInfo ProductModle)
        {
            var redisClient = RedisManager.GetClient();
            var productInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductInfo>();
            redisClient.Set("ProductsModel-" + ProductModle.ProductId.ToString(), ProductModle);
            return true;
        }

        public Maticsoft.Model.Shop.Products.ProductInfo GetProductByProductId(long ProductId)
        {
            var redisClient = RedisManager.GetClient();
            var productInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductInfo>();
            return productInfo.GetValue("ProductsModel-" + ProductId.ToString());
        }

        public bool DeleteProduct(Maticsoft.Model.Shop.Products.ProductInfo ProductModle)
        {
            var redisClient = RedisManager.GetClient();
            var productInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductInfo>();
            redisClient.Remove("ProductsModel-" + ProductModle.ProductId.ToString());
            return true;
        }
    }
}
