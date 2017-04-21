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
    public class CategoryInfo
    {
        private readonly ICategoryInfo dal = DAShopProducts.CreateCategoryInfo();

        /// <summary>
        /// 得到所有分类，从缓存中
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetAllCategory()
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categorys = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            var redisClient = RedisManager.GetClient();
            var category = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.CategoryInfo>();

            var pKeyList = category.GetAllKeys();
            foreach (var p in pKeyList)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo product = new Maticsoft.Model.Shop.Products.CategoryInfo();
                product = category.GetValue(p);
                categorys.Add(product);
            }

            return categorys;
        }

        /// <summary>
        /// 保存所有分类，缓存
        /// </summary>
        public bool SaveAllCategory()
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            var redisClient = RedisManager.GetClient();
            //var categoryInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.CategoryInfo>();

            DataTable dt = dal.GetList("").Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo p = new Maticsoft.Model.Shop.Products.CategoryInfo();
                p = dal.DataRowToModel(dt.Rows[i]);
                redisClient.Add("CategoryInfoModel-" + p.CategoryId.ToString(), p);
            }
            return true;
        }

        /// <summary>
        /// 删除所有分类，从缓存中
        /// </summary>
        public bool DeleteAllCategory()
        {
            var redisClient = RedisManager.GetClient();
            var categoryInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.CategoryInfo>();
            var pKeyList = categoryInfo.GetAllKeys();
            foreach (var p in pKeyList)
            {
                redisClient.Remove(p);
            }
            return true;
        }

        /// <summary>
        /// 更新一个对象实体，从缓存中
        /// </summary>
        public bool UpdateCategory(Maticsoft.Model.Shop.Products.CategoryInfo CategoryModle)
        {
            var redisClient = RedisManager.GetClient();
            var categoryInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.CategoryInfo>();
            redisClient.Set("CategoryInfoModel-" + CategoryModle.CategoryId.ToString(), CategoryModle);
            return true;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.CategoryInfo GetModelByCategoryId(int CategoryId)
        {
            var redisClient = RedisManager.GetClient();
            var categoryInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.CategoryInfo>();
            return categoryInfo.GetValue("CategoryInfoModel-" + CategoryId.ToString());
        }

        /// <summary>
        /// 删除一个对象实体，从缓存中
        /// </summary>
        public bool DeleteCategory(Maticsoft.Model.Shop.Products.CategoryInfo CategoryModle)
        {
            var redisClient = RedisManager.GetClient();
            var categoryInfo = redisClient.GetTypedClient<Maticsoft.Model.Shop.Products.CategoryInfo>();
            redisClient.Remove("CategoryInfoModel-" + CategoryModle.CategoryId.ToString());
            return true;
        }
    }
}
