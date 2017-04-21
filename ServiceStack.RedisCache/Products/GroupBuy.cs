using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.IDAL.Shop.PromoteSales;

namespace ServiceStack.RedisCache.Products
{
    public class GroupBuy
    {
        private readonly IGroupBuy dal = DAShopProSales.CreateGroupBuy();

        /// <summary>
        /// 得到所有团购，从缓存中
        /// </summary>
        public List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> GetAllGroupBuy()
        {
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> groupBuys = new List<Maticsoft.Model.Shop.PromoteSales.GroupBuy>();
            var redisClient = RedisManager.GetClient();
            var groupBuy = redisClient.GetTypedClient<Maticsoft.Model.Shop.PromoteSales.GroupBuy>();

            var pKeyList = groupBuy.GetAllKeys();
            foreach (var p in pKeyList)
            {
                Maticsoft.Model.Shop.PromoteSales.GroupBuy product = new Maticsoft.Model.Shop.PromoteSales.GroupBuy();
                product = groupBuy.GetValue(p);
                groupBuys.Add(product);
            }

            return groupBuys;
        }

        /// <summary>
        /// 保存所有团购，缓存
        /// </summary>
        public bool SaveAllGroupBuy()
        {
            List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> categoryInfos = new List<Maticsoft.Model.Shop.PromoteSales.GroupBuy>();
            var redisClient = RedisManager.GetClient();

            DataTable dt = dal.GetList("").Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Maticsoft.Model.Shop.PromoteSales.GroupBuy p = new Maticsoft.Model.Shop.PromoteSales.GroupBuy();
                p = dal.DataRowToModel(dt.Rows[i]);
                redisClient.Add("GroupBuyModel-" + p.GroupBuyId.ToString(), p);
            }
            return true;
        }

        /// <summary>
        /// 删除所有团购，从缓存中
        /// </summary>
        public bool DeleteAllGroupBuy()
        {
            var redisClient = RedisManager.GetClient();
            var groupBuy = redisClient.GetTypedClient<Maticsoft.Model.Shop.PromoteSales.GroupBuy>();
            var pKeyList = groupBuy.GetAllKeys();
            foreach (var p in pKeyList)
            {
                redisClient.Remove(p);
            }
            return true;
        }

        /// <summary>
        /// 更新一个对象实体，从缓存中
        /// </summary>
        public bool UpdateGroupBuy(Maticsoft.Model.Shop.PromoteSales.GroupBuy GroupBuyModle)
        {
            var redisClient = RedisManager.GetClient();
            redisClient.Set("GroupBuyModel-" + GroupBuyModle.GroupBuyId.ToString(), GroupBuyModle);
            return true;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GetModelByGroupBuyId(int GroupBuyId)
        {
            var redisClient = RedisManager.GetClient();
            var groupBuy = redisClient.GetTypedClient<Maticsoft.Model.Shop.PromoteSales.GroupBuy>();
            return groupBuy.GetValue("GroupBuyModel-" + GroupBuyId.ToString());
        }

        /// <summary>
        /// 删除一个对象实体，从缓存中
        /// </summary>
        public bool DeleteGroupBuy(Maticsoft.Model.Shop.PromoteSales.GroupBuy GroupBuyModle)
        {
            var redisClient = RedisManager.GetClient();
            redisClient.Remove("GroupBuyModel-" + GroupBuyModle.GroupBuyId.ToString());
            return true;
        }
    }
}
