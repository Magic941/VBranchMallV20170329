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
    public class ProductCategories
    {
        private readonly IProductCategories dal = DAShopProducts.CreateProductCategories();

        /// <summary>
        /// 得到所有商品分类信息，从缓存中
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductCategories> GetAllProductCategories()
        {
            List<Maticsoft.Model.Shop.Products.ProductCategories> productCategories = new List<Maticsoft.Model.Shop.Products.ProductCategories>();
            var redisClient = RedisManager.GetClient();
            var productCategorie = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductCategories>();

            var pKeyList = productCategorie.GetAllKeys();
            foreach (var p in pKeyList)
            {
                Maticsoft.Model.Shop.Products.ProductCategories product = new Maticsoft.Model.Shop.Products.ProductCategories();
                product = productCategorie.GetValue(p);
                productCategories.Add(product);
            }

            return productCategories;
        }

        /// <summary>
        /// 保存所有分类，缓存
        /// </summary>
        public bool SaveAllCategories()
        {
            List<Maticsoft.Model.Shop.Products.ProductCategories> categoryInfos = new List<Maticsoft.Model.Shop.Products.ProductCategories>();
            var redisClient = RedisManager.GetClient();

            DataTable dt = dal.GetList("").Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Maticsoft.Model.Shop.Products.ProductCategories p = new Maticsoft.Model.Shop.Products.ProductCategories();
                if (dt.Rows[0]["CategoryId"] != null && dt.Rows[0]["CategoryId"].ToString() != "")
                {
                    p.CategoryId = int.Parse(dt.Rows[0]["CategoryId"].ToString());
                }
                if (dt.Rows[0]["ProductId"] != null && dt.Rows[0]["ProductId"].ToString() != "")
                {
                    p.ProductId = long.Parse(dt.Rows[0]["ProductId"].ToString());
                }
                if (dt.Rows[0]["CategoryPath"] != null && dt.Rows[0]["CategoryPath"].ToString() != "")
                {
                    p.CategoryPath = dt.Rows[0]["CategoryPath"].ToString();
                }
                //p = dal.DataRowToModel(dt.Rows[i]);
                redisClient.Add("ProductCategoriesModel-" + p.CategoryId.ToString(), p);
            }
            return true;
        }

        /// <summary>
        /// 删除所有类别关联，从缓存中
        /// </summary>
        public bool DeleteAllCategories()
        {
            var redisClient = RedisManager.GetClient();
            var categories = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductCategories>();
            var pKeyList = categories.GetAllKeys();
            foreach (var p in pKeyList)
            {
                redisClient.Remove(p);
            }
            return true;
        }

        /// <summary>
        /// 更新一个对象实体，从缓存中
        /// </summary>
        public bool UpdateCategories(Maticsoft.Model.Shop.Products.ProductCategories CategoriesModle)
        {
            var redisClient = RedisManager.GetClient();
            redisClient.Set("ProductCategoriesModel-" + CategoriesModle.CategoryId.ToString(), CategoriesModle);
            return true;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductCategories GetModelByCategoryId(long CategoryId)
        {
            var redisClient = RedisManager.GetClient();
            var categories = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.ProductCategories>();
            return categories.GetValue("ProductCategoriesModel-" + CategoryId.ToString());
        }

        /// <summary>
        /// 删除一个对象实体，从缓存中
        /// </summary>
        public bool DeleteCategories(Maticsoft.Model.Shop.Products.ProductCategories CategoriesModle)
        {
            var redisClient = RedisManager.GetClient();
            redisClient.Remove("ProductCategoriesModel-" + CategoriesModle.CategoryId.ToString());
            return true;
        }
    }
}
