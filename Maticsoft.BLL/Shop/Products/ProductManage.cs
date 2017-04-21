/**
* ProductManage.cs
*
* 功 能： Shop模块-产品相关 多表事务业务类
* 类 名： ProductManage
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/22 11:14:41  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Collections.Generic;
using System.Data;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;

using ServiceStack.RedisCache;

namespace Maticsoft.BLL.Shop.Products
{
    public class ProductManage
    {
        private static readonly IProductService service = DAShopProducts.CreateProductService();

        public static bool AddProduct(Model.Shop.Products.ProductInfo productInfo, out long ProductId)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + productInfo.ProductId.ToString();
                if (RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey) != null)
                {

                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, productInfo);
                }
            }

            return service.AddProduct(productInfo,out ProductId);
        }

        public static bool ModifyProduct(Model.Shop.Products.ProductInfo productInfo)
        {
             if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + productInfo.ProductId.ToString();
                if (RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey) != null)
                {

                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, productInfo);
                }
            }

            return service.ModifyProduct(productInfo);
        }
       public static bool AddSuppProduct(Model.Shop.Products.ProductInfo productInfo, out long ProductId)
       {
           return service.AddSuppProduct(productInfo, out ProductId);
       }

        public static bool ModifySuppProduct(Model.Shop.Products.ProductInfo productInfo)
        {
            return service.ModifySuppProduct(productInfo);
        }

        #region 对比信息获取
        /// <summary>
        /// 获取对比结果
        /// </summary>
        public static List<Model.Shop.Products.ProductCompareServer> GetCompareProudctInfo(string ids)
        {
            DataSet ds = service.GetCompareProudctInfo(ids);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取对比结果基本信息 图片价格
        /// </summary>
        public static List<Model.Shop.Products.ProductCompareServer> GetCompareProudctBasicInfo(string ids)
        {
            DataSet ds = service.GetCompareProudctBasicInfo(ids);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        private static List<Model.Shop.Products.ProductCompareServer> DataTableToList(DataTable dt)
        {
            List<Model.Shop.Products.ProductCompareServer> list = null;
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                list = new List<Model.Shop.Products.ProductCompareServer>();

                Model.Shop.Products.ProductCompareServer model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Shop.Products.ProductCompareServer();
                    if (dt.Rows[n]["AttName"] != null && dt.Rows[n]["AttName"].ToString() != "")
                    {
                        model.AttrName = dt.Rows[n]["AttName"].ToString();
                    }
                    if (dt.Rows[n]["Product1"] != null && dt.Rows[n]["Product1"].ToString() != "")
                    {
                        model.Product1 = dt.Rows[n]["Product1"].ToString();
                    }
                    if (dt.Rows[n]["Product2"] != null && dt.Rows[n]["Product2"].ToString() != "")
                    {
                        model.Product2 = dt.Rows[n]["Product2"].ToString();
                    }
                    if (dt.Rows[n]["Product3"] != null && dt.Rows[n]["Product3"].ToString() != "")
                    {
                        model.Product3 = dt.Rows[n]["Product3"].ToString();
                    }
                    if (dt.Rows[n]["Product4"] != null && dt.Rows[n]["Product4"].ToString() != "")
                    {
                        model.Product4 = dt.Rows[n]["Product4"].ToString();
                    }
                    list.Add(model);
                }
            }
            return list;
        } 
        #endregion

        #region 修改产品数据库索引
        /// <summary>
        /// 修改产品数据库索引 1.在商品新增上架时 调用Add方法 2.在商品修改上架时  3.调用 Mod方法在商品下架时，  调用 Del  

        /// </summary>
        /// <param name="oldstatus"></param>
        /// <param name="newSaleStatus"></param>
        /// <param name="ProductId"></param>
        public static void InsertOrUpdateIndex(int oldstatus, int newSaleStatus, long ProductId)
        {
            if (newSaleStatus == (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale) //上架
            {

                if (oldstatus == (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale)
                {
                    Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Mod(ProductId);
                }
                else
                {
                    Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Add(ProductId);
                }
            }
            if (newSaleStatus == (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock) //下架
            {
                if (oldstatus == (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale)
                {
                    Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Del(ProductId);
                }
            }
            if (newSaleStatus == (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.Deleted) //删除
            {
                if (oldstatus == (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.OnSale)
                {
                    Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Del(ProductId);
                }
            }
        }
        #endregion
    }
}