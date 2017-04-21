using Maticsoft.Json;
using Maticsoft.ViewModel.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppServices.Controllers
{
    public class ProSalesController : ApiController
    {
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";

        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";
        public const string SHOP_STATUS_ISNULL = "ISNULL";
        public const string TOTALCOUNT = "TOTALCOUNT";

        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierInfo = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
        private Maticsoft.BLL.Shop.Products.BrandInfo brandInfo = new Maticsoft.BLL.Shop.Products.BrandInfo();
        private Maticsoft.BLL.Shop.Products.CategoryInfo cateInfo = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.PromoteSales.GroupBuy groupBuy = new Maticsoft.BLL.Shop.PromoteSales.GroupBuy();
        private Maticsoft.BLL.Ms.Regions regionsBll = new Maticsoft.BLL.Ms.Regions();

        private AppServices.BLL.GroupBuy groupBuyExp = new BLL.GroupBuy();


        #region 返回商品秒杀列表 限时抢购

        [HttpGet]
        public JsonObject ShowCountDown(int cid, int? pageIndex = 1,int pageSize=32,int type=-1  )
        {
            ProductListModel model = new ProductListModel();

            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + pageSize - 1 : pageSize;
            int toalCount = productManage.GetProSalesCount();
            JsonObject json = new JsonObject();
            //if (cateList != null)
            //{
            //    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            //    json.Put(SHOP_KEY_DATA, cateList);
            //}
            //else
            //{
            //    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            //}
            return json;
        }

        #endregion
   

        #region 豪礼大放送

        /// <summary>
        /// 用户限购数量
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [HttpPost]
        public int GetBuyLimit(int productid, int UserID)
        {
            Maticsoft.BLL.Shop.PromoteSales.GroupBuy GB = new Maticsoft.BLL.Shop.PromoteSales.GroupBuy();
            return GB.GetGroupBuyLimit(UserID, productid);
        }
        #endregion

        #region 获取某天团购商品列表
        /// <summary>
        /// 获取团购商品列表
        /// </summary>
        /// <param name="PromotionType"> 抢购类型：0 团购 1 小时购 </param>
        /// <param name="StartDate">开始日期</param>
        /// <returns></returns>
         [HttpGet]
        public JsonObject GetgroupBuyList(int PromotionType, string StartDate)
        {
            JsonObject json = new JsonObject();
            try
            {
                if (string.IsNullOrEmpty(StartDate))
                {
                    StartDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                }

                List<AppServices.Models.GroupBuy> listmodel = groupBuyExp.GetModelListToday(string.Format(" PromotionType={0} and DATEDIFF(d,StartDate,{1})=0 ", PromotionType, StartDate));

                if (listmodel != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, listmodel);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                }
               
            }
            catch
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
            }
            return json;
        }
        #endregion

        #region 根据团购ID获取团购信息
        /// <summary>
        /// 获取团购信息
        /// </summary>
        /// <param name="GroupBuyId">团购ID</param>
        /// <returns></returns>
         [HttpGet]
        public JsonObject GetGroupBuyInfo(int GroupBuyId)
        {
            JsonObject json = new JsonObject();
            try
            {
                if (GroupBuyId < 1)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                    return json;
                }

                Maticsoft.Model.Shop.PromoteSales.GroupBuy GroupBuyInfo = groupBuy.GetModelByGroupID(GroupBuyId);

                if (GroupBuyInfo != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, GroupBuyInfo);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                }

            }
            catch
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
            }
            return json;
        }
        #endregion

        #region 根据商品ID获取团购信息
        /// <summary>
        /// 根据商品ID获取团购信息
        /// </summary>
        /// <param name="ProductId">商品ID</param>
        /// <returns></returns>
        [HttpGet]
        public JsonObject GetGroupBuyProductInfo(long ProductId)
        {
            JsonObject json = new JsonObject();
            try
            {
                string sqlwhere = "";
                if (ProductId < 1)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                    return json;
                }
               
                sqlwhere = string.Format(" ProductId={0} ", ProductId);
                

                AppServices.Models.GroupBuy GroupBuyInfo = groupBuyExp.GetProductGroupByInfo(sqlwhere);

                if (GroupBuyInfo != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, GroupBuyInfo);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                }

            }
            catch
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
            }
            return json;
        }
        #endregion




    }
}