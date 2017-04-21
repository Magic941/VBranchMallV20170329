using Maticsoft.BLL.Shop.Products;
using Maticsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace AppServices.Controllers
{
    public class ProductsController : ApiController
    {
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";

        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";
        public const string SHOP_STATUS_ISNULL = "ISNULL";
        public const string TOTALCOUNT = "TOTALCOUNT";

        Maticsoft.BLL.Shop.Products.ProductImage imageManage = new Maticsoft.BLL.Shop.Products.ProductImage();


        protected Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected Maticsoft.BLL.Shop.Products.CategoryInfo categoryManage = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        protected Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();
        protected Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();
        protected Maticsoft.BLL.Shop.Products.BrandInfo brandBll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        protected Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierBll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
        protected Maticsoft.BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
        protected Maticsoft.BLL.Shop.Products.ProductConsults conBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        protected readonly Maticsoft.BLL.Shop.Products.AttributeInfo attributeManage = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        protected int _basePageSize = 30;
        protected int _waterfallSize = 32;
        protected int _waterfallDataCount = 1;

        #region 获取单个商品的资料信息（包括SKU，ProductInfo，ProductImages 图片）
        /// <summary>
        /// 返回商品的资料信息（包括SKU，ProductInfo，ProductImages 图片）
        /// </summary>
        /// <param name="ProductId">商品ID</param>
        /// <returns>返回 Maticsoft.Model.Shop.Products.ProductInfo（SKU，ProductInfo，ProductImages ） </returns>
        [HttpGet]
        public JsonObject ProductInfo(long ProductId)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Shop.Products.ProductInfo info = new Maticsoft.Model.Shop.Products.ProductInfo();

            info = productManage.GetModel(ProductId);
           
            if (info != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, AppServices.BLL.ProductInfo.ProductInfoToModel(info));
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
            }
            return json;
        }

        #endregion

        #region 获取产品图片

        [HttpGet]
        public JsonObject ProductImages(long ProductId)
        {
            JsonObject json = new JsonObject();
            BLL.ProductImage productimagebll = new BLL.ProductImage();
            List<AppServices.Models.ProductImage> ProductImageLists = productimagebll.GetModelList(ProductId);
           json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
           json.Put(SHOP_KEY_DATA, ProductImageLists);
           
            return json;
        }

     
        #endregion

        #region 获取产品Skus

        [HttpGet]
        public JsonObject ProductSkus(long ProductId)
        {
            JsonObject json = new JsonObject();
            BLL.SKUInfo SKUInfoBll = new BLL.SKUInfo();
            List<AppServices.Models.SKUInfo> SKUInfoLists = SKUInfoBll.GetProductSkuInfo(ProductId);
            if (SKUInfoLists != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, SKUInfoLists);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
            }

            return json;
        }


        #endregion

        #region 获取产品Shop_SKUItems组合(Shop_SKURelation ,Shop_Attributes,Shop_AttributeValues 这三张表关联起来)

        [HttpGet]
        public JsonObject ProductSKUItems(long ProductId)
        {
            JsonObject json = new JsonObject();
            BLL.SKUItem SKUItemBll = new BLL.SKUItem();
            List<AppServices.Models.SKUItem> SKUItemLists = SKUItemBll.GetSKUItemsByProductId( ProductId);
            if (SKUItemLists != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, SKUItemLists);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
            }

            return json;
        }


        #endregion

        #region 商品SKU规格选择
        /// <summary>
        /// 商品SKU规格选择，如果商品开启多一个返回值：SKUDATA
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>返回商品SKU 和Sku列表转化为json </returns>
        [HttpGet]
        public JsonObject OptionSKU(long productId)
        {
            JsonObject json = new JsonObject();
            if (productId < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                return json;
            }
            Maticsoft.ViewModel.Shop.ProductSKUModel productSKUModel = skuBll.GetProductSKUInfoByProductId(productId);
            //NO SKU ERROR
            if (productSKUModel == null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                return json;
            }
            //NO SKU ERROR
            if (productSKUModel.ListSKUInfos == null || productSKUModel.ListSKUInfos.Count < 1 ||
                productSKUModel.ListSKUItems == null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                return json;
            }

            //木有开启SKU的情况
            if (productSKUModel.ListSKUItems.Count == 0)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, productSKUModel);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, productSKUModel);
                json.Put("SKUDATA", SKUInfoToJson(productSKUModel.ListSKUInfos).ToString());
            }

            return json;
        }
        protected JsonObject SKUInfoToJson(List<Maticsoft.Model.Shop.Products.SKUInfo> list)
        {
            if (list == null || list.Count < 1) return null;
            JsonObject json = new JsonObject();

            JsonObject jsonSKU = new JsonObject();
            long[] key;
            int index;
            foreach (Maticsoft.Model.Shop.Products.SKUInfo item in list)
            {
                if (item.SkuItems == null || item.SkuItems.Count < 1) continue;

                //无库存SKU不提供给页面
                //是否开启警戒库存判断
                bool IsOpenAlertStock = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock");
                if (IsOpenAlertStock && item.Stock <= item.AlertStock)
                {
                    continue;
                }
                if (item.Stock < 1)
                    continue;

                //组合SKU 的 ValueId
                key = new long[item.SkuItems.Count];
                index = 0;
                item.SkuItems.ForEach(xx => key[index++] = xx.ValueId);
                jsonSKU.Accumulate(string.Join(",", key), new
                {
                    sku = item.SKU,
                    count = item.Stock,
                    price = item.SalePrice
                });
            }

            //获取最小/最大价格
            list.Sort((x, y) => x.SalePrice.CompareTo(y.SalePrice));
            json.Put("Default", new
            {
                minPrice = list[0].SalePrice,
                maxPrice = list[list.Count - 1].SalePrice
            });
            json.Put("SKUDATA", jsonSKU);
            return json;
        }


        #endregion

        #region 商品扩展属性
        /// <summary>
        /// 返回商品的扩展属性
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <returns>返回商品扩展属性（List<Maticsoft.Model.Shop.Products.AttributeInfo>）</returns>
        [HttpGet]
        public JsonObject OptionAttr(long productId)
        {
            JsonObject json = new JsonObject();
            if (productId < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                return json;
            }
            List<Maticsoft.Model.Shop.Products.AttributeInfo> model = attributeManage.GetAttributeInfoListByProductId(productId);
            if (model != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, model);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion

        #region 返回商品分类列表
        /// <summary>
        /// 返回商品分类列表
        /// </summary>
        /// <returns>返回所有商品分类List（List<Maticsoft.Model.Shop.Products.CategoryInfo> ）</returns>
        [HttpGet]
        public JsonObject CategoryInfoList()
        {
            List<AppServices.Models.CategoryInfo> cateList = BLL.CategoryInfo.GetAllCateList();
            JsonObject json = new JsonObject();
            if (cateList != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, cateList);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion

        #region 返回商品分类商品列表分页(cid=分类ID, brandid=品牌ID,attrvalues= 属性ID,mod=排序方法，price=价格范围100-200 ，pageIndex 页码，pageSize 分页大小)
        /// <summary>
        /// 返回商品分类商品列表分页(cid=分类ID, brandid=品牌ID,attrvalues= 属性ID,mod=排序方法，price=价格范围100-200 ，pageIndex 页码，pageSize 分页大小，importpro 是否进口商品 -1 表示所有 0 非进口 1进口)
        /// </summary>
        /// <param name="cid">分类ID</param>
        /// <param name="brandid">品牌ID</param>
        /// <param name="attrvalues">属性</param>
        /// <param name="mod">排序方法</param>
        /// <param name="price">价格范围</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="importpro"> importpro 是否进口商品 -1 表示所有 0 非进口 1进口</param>
        /// <returns></returns>
        [HttpGet]
        public JsonObject ProductInfoList(int cid = 0, int brandid = 0, string attrvalues = "0", string mod = "default", string price = "",
                                  int? pageIndex = 1, int pageSize = 32, int importpro=-1)
        {
            JsonObject json = new JsonObject();

            if (pageSize <= 0)
            {
                pageSize = _basePageSize; //默认值
            }
            else
            {
                _basePageSize = pageSize;
            }
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = productManage.GetProductsCountEx(cid, brandid, attrvalues, price);
            //瀑布流Index
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = productManage.GetProductsListEx(cid, brandid, attrvalues, price, mod, startIndex, endIndex);
          
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(TOTALCOUNT, toalCount);
            json.Put(SHOP_KEY_DATA, AppServices.BLL.ProductInfo.ProductRecTableToList(list));
            return json;

        }

        #endregion

        #region 获取随机商品
        /// <summary>
        /// 随机商品()
        /// </summary>
        /// <param name="top">top 多少</param>
        /// <returns>返回List<Maticsoft.Model.Shop.Products.ProductInfo> </returns>
        [HttpGet]
        public JsonObject ProductRan(int top = 10)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productManage.GetProductRanList(top);
            JsonObject json = new JsonObject();
            if (productList != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, AppServices.BLL.ProductInfo.ProductRecTableToList(productList));
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion

        #region 关联商品
        /// <summary>
        /// 获取商品的关联商品
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <param name="top">top 取前多少个</param>
        /// <returns>返回相关商品列表List<Maticsoft.Model.Shop.Products.ProductInfo> </returns>
        [HttpGet]
        public virtual JsonObject ProductRelation(long productid, int top = 12)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productManage.RelatedProductsList(productid, top);
            JsonObject json = new JsonObject();
            if (productList != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, AppServices.BLL.ProductInfo.ProductRecTableToList(productList));
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion
        #region 获取图片服务器地址

        /// <summary>
        /// 获取图片服务器地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonObject GetFtpPicServerUrl()
        {
            JsonObject json = new JsonObject();
            try
            {
                var picServerUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, picServerUrl);
            }
            catch
            {
                json.Put(SHOP_KEY_STATUS,SHOP_STATUS_ERROR);
            }
            return json;
        }
       
        #endregion

        #region 销售记录
        /// <summary>
        /// 获取商品的销售记录
        /// </summary>
        /// <param name="productid">商品ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>返回商品的销售记录列表和总条数</returns>
        [HttpGet]
        public JsonObject SaleRecord(long productid, int pageIndex = 1, int pageSize = 15)
        {
            JsonObject json = new JsonObject();
            try
            {
                int _pageSize = pageSize;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;
                int toalCount = 0;

                //获取总条数
             
                toalCount = itemBll.GetSaleRecordCount(productid);
                if (toalCount < 1)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                    return json;
                }
                List<Maticsoft.ViewModel.Shop.SaleRecord> saleRecords = itemBll.GetSaleRecordByPage(productid, "", startIndex, endIndex);
                if (saleRecords != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, saleRecords);
                    json.Put(TOTALCOUNT, toalCount);
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

        #region 返回分类品牌列表品牌
        /// <summary>
        /// 获取分类的品牌
        /// </summary>
        /// <param name="Cid">商品分类</param>
        /// <param name="top">默认-1 所有</param>
        /// <returns>返回分类下所有品牌</returns>
        [HttpGet]
        public JsonObject BrandList(int Cid = 0, int top = -1)
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> brandInfos = brandBll.GetBrandsByCateId(Cid, true, top);
            JsonObject json = new JsonObject();
            try
            {
                if (brandInfos != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, brandInfos);
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

        #region 获取品牌信息
        /// <summary>
        /// 获取商品品牌信息
        /// </summary>
        /// <param name="brandid">品牌ID</param>
        /// <returns>返回 （Maticsoft.Model.Shop.Products.BrandInfo）</returns>
        [HttpGet]
        public JsonObject BrandInfo(int brandid)
        {
            Maticsoft.Model.Shop.Products.BrandInfo brandInfos = brandBll.GetModel(brandid);
            JsonObject json = new JsonObject();
            try
            {
                if (brandInfos != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, brandInfos);
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

        #region 获取供应商信息
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="SupplierId">供应商ID</param>
        /// <returns>返回（Maticsoft.Model.Shop.Supplier.SupplierInfo）</returns>
        [HttpGet]
        public JsonObject SupplierInfo(int SupplierId)
        {
            Maticsoft.Model.Shop.Supplier.SupplierInfo brandInfos = supplierBll.GetModel(SupplierId);
            JsonObject json = new JsonObject();
            try
            {
                if (brandInfos != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, brandInfos);
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

        #region 获取单个商品评论列表
        /// <summary>
        /// 获取单个商品评论列表
        /// </summary>
        /// <param name="ProductId">商品ProductId</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pagesize">页大小</param>
        /// <returns>返回评论列表（List<Maticsoft.Model.Shop.Products.ProductReviews>） 和总行数 </returns>
        [HttpGet]
        public JsonObject ProductComments(long ProductId, int pageIndex = 1, int pagesize = 15)
        {
            JsonObject json = new JsonObject();
            try
            {
                int _pageSize = pagesize;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;
                int totalCount = 0;

                //获取总条数
                totalCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + ProductId);
                if (totalCount < 1)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                    return json;
                }

                List<Maticsoft.Model.Shop.Products.ProductReviews> productReviewses = reviewsBll.GetReviewsByPage(ProductId, " CreatedDate desc", startIndex, endIndex);


                if (productReviewses != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, productReviewses);
                    json.Put(TOTALCOUNT, totalCount);
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

        #region 热卖推荐 Maticsoft.Model.Shop.Products.ProductRecType Type=按分类  0：推荐 1热卖 2特价 3：最新,4:首页推荐,6：分享商品
        /// <summary>
        /// 热卖推荐商品
        /// </summary>
        /// <param name="Type">推荐类型 0：推荐 1热卖 2特价 3：最新,4:首页推荐,6：分享商品 </param>
        /// <param name="Cid">分类ID</param>
        /// <param name="Top"></param>
        /// <returns>返回商品推荐列表（ List<Maticsoft.Model.Shop.Products.ProductInfo>）</returns>
        [HttpGet]
        public JsonObject HotCommand(Maticsoft.Model.Shop.Products.ProductRecType Type = Maticsoft.Model.Shop.Products.ProductRecType.Recommend, int Cid = 0, int Top = 5)
        {
            JsonObject json = new JsonObject();
            try
            {
                List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productManage.GetProductRecList(Type, Cid, Top);
                if (productList != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    List<AppServices.Models.ProductInfo> ProductInfoApilist = AppServices.BLL.ProductInfo.ProductRecTableToList(productList);
                    json.Put(SHOP_KEY_DATA, ProductInfoApilist);
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

      

        #region 商品咨询
        /// <summary>
        /// 返回商品咨询列表
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>返回商品咨询列表（List<Maticsoft.Model.Shop.Products.ProductConsults>）和总条数 </returns>
        [HttpGet]
        public JsonObject ProductConsult(int ProductId, int pageIndex = 1, int pageSize = 15)
        {

            int _pageSize = pageSize;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int totalCount = 0;

            //获取总条数
            totalCount = conBll.GetRecordCount("Status=1 and ProductId=" + ProductId);

            List<Maticsoft.Model.Shop.Products.ProductConsults> productConsults = conBll.GetConsultationsByPage(ProductId, " CreatedDate desc", startIndex, endIndex);

            JsonObject json = new JsonObject();
            if (productConsults != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, productConsults);
                json.Put(TOTALCOUNT, totalCount);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion

        #region 列表属性（获取商品的分类的属性(如：颜色，尺码)）
        /// <summary>
        /// 该分类下的属性（获取商品的分类的属性(如：颜色，尺码)）
        /// </summary>
        /// <param name="cid">商品分类ID</param>
        /// <param name="Top"></param>
        /// <returns>返回获取商品的分类的属性(如：颜色，尺码)（List<Maticsoft.Model.Shop.Products.AttributeInfo>）</returns>
        [HttpGet]
        public virtual JsonObject AttrList(int cid, int Top = -1)
        {
            Maticsoft.BLL.Shop.Products.AttributeInfo attributeBll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
            List<Maticsoft.Model.Shop.Products.AttributeInfo> lists = attributeBll.GetAttributeListByCateID(cid, true);
            JsonObject json = new JsonObject();
            if (lists != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, lists);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion


        #region 该分类下的属性对应的值

        /// <summary>
        /// 该分类下的属性对应的值
        /// </summary>
        /// <param name="AttrId">属性ID</param>
        /// <param name="Top"></param>
        /// <returns>返回该分类下的属性对应的值（List<Maticsoft.Model.Shop.Products.AttributeValue> ）</returns>
        [HttpGet]
        public virtual JsonObject AttrValues(int AttrId, int Top = -1)
        {
            Maticsoft.BLL.Shop.Products.AttributeValue valueBll = new Maticsoft.BLL.Shop.Products.AttributeValue();
            List<Maticsoft.Model.Shop.Products.AttributeValue> lists = valueBll.GetModelList(" AttributeId=" + AttrId);
            JsonObject json = new JsonObject();
            if (lists != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, lists);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }

        #endregion


        #region  根据商品ID 获取批发优惠规则以及规则项  Maticsoft.Model.Shop.Sales.SalesRule Maticsoft.Model.Shop.Sales.SalesItem
        /// <summary>
        ///  根据商品ID 获取批发优惠规则以及规则项  Maticsoft.Model.Shop.Sales.SalesRule Maticsoft.Model.Shop.Sales.SalesItem
        /// </summary>
        /// <param name="ProductId">产品ID</param>
        /// <param name="userid">用户ID根据用户级别优惠</param>
        /// <returns>返回批发优惠规则以及规则项  Maticsoft.Model.Shop.Sales.SalesRule Maticsoft.Model.Shop.Sales.SalesItem </returns>
        [HttpGet]
        public JsonObject WholeSale(long ProductId, int userid = -1)
        {
            Maticsoft.ViewModel.Shop.SalesModel salesModel = new Maticsoft.ViewModel.Shop.SalesModel();

            Maticsoft.BLL.Shop.Sales.SalesRuleProduct ruleBll = new Maticsoft.BLL.Shop.Sales.SalesRuleProduct();
            salesModel = ruleBll.GetSalesRuleByCache(ProductId, userid);

            JsonObject json = new JsonObject();
            if (salesModel != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, salesModel);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion



        #region 商品对比
        /// <summary>
        /// 商品对比
        /// </summary>
        /// <param name="prodidlist">商品ID 字符串 多个用‘_’分割 返回商品的AttributeInfo 对比信息</param>
        /// <returns></returns>
        [HttpGet]
        public JsonObject Compare(string prodidlist = "")
        {
            JsonObject json = new JsonObject();

            if (!String.IsNullOrWhiteSpace(prodidlist))
            {
                string[] ids = prodidlist.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length <= 0)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                    return json;
                }
                long[] convertedItems = new long[ids.Length];
                for (int i = 0; i < ids.Length; i++)
                {
                    convertedItems[i] = Maticsoft.Common.Globals.SafeLong(ids[i], 0);
                    if (convertedItems[i] <= 0)
                    {
                        json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                        return json;
                    }
                }
                Dictionary<string, Maticsoft.Model.Shop.Products.AttributeInfo> data = productManage.GetProdValueList(convertedItems);
                if (data != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                }
                return json;
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            return json;
        }
        #endregion

        #region 商品组合优惠和配件信息
        /// <summary>
        /// 组合列表
        /// </summary>
        /// <param name="productId">组合id</param>
        /// <param name="type">type 1 配件  2组合优惠</param>
        /// <returns>返回 商品组合优惠和配件信息ProductAccessorieInfo= Maticsoft.ViewModel.Shop.ProductAccessorie（商品的SkuInfo=List<Model.Shop.Products.SKUInfo>） </returns>
        [HttpGet]
        public JsonObject PromotionCombo(long productId, int type)
        {
            JsonObject json = new JsonObject();
            List<Maticsoft.ViewModel.Shop.ProductAccessorie> listModel = new List<Maticsoft.ViewModel.Shop.ProductAccessorie>();
            List<Maticsoft.Model.Shop.Products.ProductAccessorie> list = new Maticsoft.BLL.Shop.Products.ProductAccessorie().GetModelList(productId, type);
            if (list == null || list.Count <= 0)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            List<Maticsoft.Model.Shop.Products.SKUInfo> skulist;
     
            Maticsoft.ViewModel.Shop.ProductAccessorie model;
            foreach (var item in list)
            {
                skulist = skuBll.GetSKUListByAcceId(item.AccessoriesId, 0); //SKU列表
                if (skulist != null && skulist.Count >= 2)//每组商品要保证最少有两条数据
                {
                   

                    model = new Maticsoft.ViewModel.Shop.ProductAccessorie();
                    model.ProductAccessorieInfo = item;
                    model.SkuInfo = skulist;
                    listModel.Add(model);
                }
            }
            
            if (listModel != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, listModel);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion





    }

}
