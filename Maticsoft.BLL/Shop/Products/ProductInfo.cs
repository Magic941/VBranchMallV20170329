/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Products.cs
// 文件功能描述：
//
// 创建标识： [Ben]  2012/06/11 20:36:27
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.Model.Shop.Products;
using Maticsoft.TaoBao;
using Maticsoft.TaoBao.Request;
using Maticsoft.TaoBao.Response;
using System.Linq;

using ServiceStack.RedisCache;

namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// ProductInfo1
    /// </summary>
    public partial class ProductInfo
    {
        private readonly IProductInfo dal = DAShopProducts.CreateProductInfo();
        //ServiceStack.RedisCache.Products.ProductInfo productCache = new ServiceStack.RedisCache.Products.ProductInfo();

        public ProductInfo()
        { }

        #region Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ProductId)
        {
            return dal.Exists(ProductId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 新增商品运费
        /// </summary>
        public bool AddFreight(string ProductCode, string SKU, decimal Freight, int ModeId, string Editor)
        {
            return dal.AddFreight(ProductCode, SKU, Freight, ModeId, Editor);
        }

        /// <summary>
        /// 删除商品运费
        /// </summary>
        public bool DeleteFreight(string ProductCode, string SKU)
        {
            return dal.DeleteFreight(ProductCode, SKU);
        }

        /// <summary>
        /// 查询商品运费
        /// </summary>
        public DataSet GetFreightList(string ProductCode, string SKU)
        {
            return dal.GetFreightList(ProductCode, SKU);
        }

        /// <summary>
        /// 查询分页商品运费
        /// </summary>
        /// <param name="Where">查询条件（SKU，Freight，ModeId筛选条件需要用x.表示，其他商品字段需要用y.表示）</param>
        /// <param name="StartIndex">开始行</param>
        /// <param name="EndIndex">结束行</param>
        /// <returns></returns>
        public DataSet GetFreightListByPage(string Where, Int64? StartIndex, Int64? EndIndex)
        {
            return dal.GetFreightListByPage(Where, StartIndex, EndIndex);
        }

        /// <summary>
        /// 更新商品运费
        /// </summary>
        public bool UpdateFreight(string ProductCode, string SKU, decimal Freight, int ModeId, string Editor)
        {
            return dal.UpdateFreight(ProductCode, SKU, Freight, ModeId, Editor);
        }

        /// <summary>
        /// 查询分页商品列表
        /// </summary>
        public DataSet GetProductListByPage(string Where, Int64? StartIndex, Int64? EndIndex)
        {
            return dal.GetProductListByPage(Where, StartIndex, EndIndex);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + model.ProductId.ToString();
                if (RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey) != null)
                {

                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, model);
                }
            }

            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductId)
        {
            return dal.Delete(ProductId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ProductIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ProductIdlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductInfo GetModel(long ProductId)
        {
            Maticsoft.Model.Shop.Products.ProductInfo product = null;
            string CacheKey = "ProductsModel-" + ProductId.ToString();
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                product = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey);
            }

            if (product == null)
            {
                product = dal.GetModel(ProductId);
                if (Maticsoft.BLL.DataCacheType.CacheType == 1)
                {
                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, product);
                }
            }

            return product;
        }
        public Maticsoft.Model.Shop.Products.ProductInfo GetModelNew(long ProductId)
        {
            return dal.GetModel(ProductId);

        }



        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductInfo GetModelByCache(long ProductId)
        {
            object objModel = null;
            string CacheKey = "ProductsModel-" + ProductId;

            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                Maticsoft.Model.Shop.Products.ProductInfo product = new Maticsoft.Model.Shop.Products.ProductInfo();
                product = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey);
                objModel = product;
            }

            if (Maticsoft.BLL.DataCacheType.CacheType == 0)
            {
                objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            }
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ProductId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        if (Maticsoft.BLL.DataCacheType.CacheType == 1)
                        {
                            RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, (Maticsoft.Model.Shop.Products.ProductInfo)objModel);
                        }
                        if (Maticsoft.BLL.DataCacheType.CacheType == 0)
                        {
                            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                        }
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductInfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获取商品及所有的分类,准备创建索引等使用
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListALL(string strWhere)
        {
            return dal.GetListALL(strWhere);
        }


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetModelList2(string strWhere)
        {
            DataSet ds = dal.GetList2(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if (dt.Columns.Contains("CategoryId"))
                    {
                        if (dt.Rows[n]["CategoryId"] != null && dt.Rows[n]["CategoryId"].ToString() != "")
                        {
                            model.CategoryId = int.Parse(dt.Rows[n]["CategoryId"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("TypeId"))
                    {
                        if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                        {
                            model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("ProductId"))
                    {
                        if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                        {
                            model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("BrandId"))
                    {
                        if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                        {
                            model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("ProductName"))
                    {
                        if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                        {
                            model.ProductName = dt.Rows[n]["ProductName"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("Subhead"))
                    {
                        if (dt.Rows[n]["Subhead"] != null && dt.Rows[n]["Subhead"].ToString() != "")
                        {
                            model.Subhead = dt.Rows[n]["Subhead"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ProductCode"))
                    {
                        if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                        {
                            model.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("SupplierId"))
                    {
                        if (dt.Rows[n]["SupplierId"] != null && dt.Rows[n]["SupplierId"].ToString() != "")
                        {
                            model.SupplierId = int.Parse(dt.Rows[n]["SupplierId"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("RegionId"))
                    {
                        if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                        {
                            model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("ShortDescription"))
                    {
                        if (dt.Rows[n]["ShortDescription"] != null && dt.Rows[n]["ShortDescription"].ToString() != "")
                        {
                            model.ShortDescription = dt.Rows[n]["ShortDescription"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("Unit"))
                    {
                        if (dt.Rows[n]["Unit"] != null && dt.Rows[n]["Unit"].ToString() != "")
                        {
                            model.Unit = dt.Rows[n]["Unit"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("Description"))
                    {
                        if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                        {
                            model.Description = dt.Rows[n]["Description"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("Meta_Title"))
                    {
                        if (dt.Rows[n]["Meta_Title"] != null && dt.Rows[n]["Meta_Title"].ToString() != "")
                        {
                            model.Meta_Title = dt.Rows[n]["Meta_Title"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("Meta_Description"))
                    {
                        if (dt.Rows[n]["Meta_Description"] != null && dt.Rows[n]["Meta_Description"].ToString() != "")
                        {
                            model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("Meta_Keywords"))
                    {
                        if (dt.Rows[n]["Meta_Keywords"] != null && dt.Rows[n]["Meta_Keywords"].ToString() != "")
                        {
                            model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("SaleStatus"))
                    {
                        if (dt.Rows[n]["SaleStatus"] != null && dt.Rows[n]["SaleStatus"].ToString() != "")
                        {
                            model.SaleStatus = int.Parse(dt.Rows[n]["SaleStatus"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("AddedDate"))
                    {
                        if (dt.Rows[n]["AddedDate"] != null && dt.Rows[n]["AddedDate"].ToString() != "")
                        {
                            model.AddedDate = DateTime.Parse(dt.Rows[n]["AddedDate"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("VistiCounts"))
                    {
                        if (dt.Rows[n]["VistiCounts"] != null && dt.Rows[n]["VistiCounts"].ToString() != "")
                        {
                            model.VistiCounts = int.Parse(dt.Rows[n]["VistiCounts"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("SaleCounts"))
                    {
                        if (dt.Rows[n]["SaleCounts"] != null && dt.Rows[n]["SaleCounts"].ToString() != "")
                        {
                            model.SaleCounts = int.Parse(dt.Rows[n]["SaleCounts"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("FalseSaleCount"))
                    {
                        if (dt.Rows[n]["FalseSaleCount"] != null && dt.Rows[n]["FalseSaleCount"].ToString() != "")
                        {
                            model.FalseSaleCount = int.Parse(dt.Rows[n]["FalseSaleCount"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("DisplaySequence"))
                    {
                        if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                        {
                            model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("LineId"))
                    {
                        if (dt.Rows[n]["LineId"] != null && dt.Rows[n]["LineId"].ToString() != "")
                        {
                            model.LineId = int.Parse(dt.Rows[n]["LineId"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("MarketPrice"))
                    {
                        if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                        {
                            model.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("LowestSalePrice"))
                    {
                        if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                        {
                            model.LowestSalePrice = decimal.Parse(dt.Rows[n]["LowestSalePrice"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("PenetrationStatus"))
                    {
                        if (dt.Rows[n]["PenetrationStatus"] != null && dt.Rows[n]["PenetrationStatus"].ToString() != "")
                        {
                            model.PenetrationStatus = int.Parse(dt.Rows[n]["PenetrationStatus"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("MainCategoryPath"))
                    {
                        if (dt.Rows[n]["MainCategoryPath"] != null && dt.Rows[n]["MainCategoryPath"].ToString() != "")
                        {
                            model.MainCategoryPath = dt.Rows[n]["MainCategoryPath"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ExtendCategoryPath"))
                    {
                        if (dt.Rows[n]["ExtendCategoryPath"] != null && dt.Rows[n]["ExtendCategoryPath"].ToString() != "")
                        {
                            model.ExtendCategoryPath = dt.Rows[n]["ExtendCategoryPath"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("HasSKU"))
                    {
                        if (dt.Rows[n]["HasSKU"] != null && dt.Rows[n]["HasSKU"].ToString() != "")
                        {
                            if ((dt.Rows[n]["HasSKU"].ToString() == "1") || (dt.Rows[n]["HasSKU"].ToString().ToLower() == "true"))
                            {
                                model.HasSKU = true;
                            }
                            else
                            {
                                model.HasSKU = false;
                            }
                        }
                    }
                    if (dt.Columns.Contains("Points"))
                    {
                        if (dt.Rows[n]["Points"] != null && dt.Rows[n]["Points"].ToString() != "")
                        {
                            model.Points = decimal.Parse(dt.Rows[n]["Points"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("ImageUrl"))
                    {
                        if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                        {
                            model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl1"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                        {
                            model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl2"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl2"] != null && dt.Rows[n]["ThumbnailUrl2"].ToString() != "")
                        {
                            model.ThumbnailUrl2 = dt.Rows[n]["ThumbnailUrl2"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl3"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl3"] != null && dt.Rows[n]["ThumbnailUrl3"].ToString() != "")
                        {
                            model.ThumbnailUrl3 = dt.Rows[n]["ThumbnailUrl3"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl4"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl4"] != null && dt.Rows[n]["ThumbnailUrl4"].ToString() != "")
                        {
                            model.ThumbnailUrl4 = dt.Rows[n]["ThumbnailUrl4"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl5"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl5"] != null && dt.Rows[n]["ThumbnailUrl5"].ToString() != "")
                        {
                            model.ThumbnailUrl5 = dt.Rows[n]["ThumbnailUrl5"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl6"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl6"] != null && dt.Rows[n]["ThumbnailUrl6"].ToString() != "")
                        {
                            model.ThumbnailUrl6 = dt.Rows[n]["ThumbnailUrl6"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl7"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl7"] != null && dt.Rows[n]["ThumbnailUrl7"].ToString() != "")
                        {
                            model.ThumbnailUrl7 = dt.Rows[n]["ThumbnailUrl7"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("ThumbnailUrl8"))
                    {
                        if (dt.Rows[n]["ThumbnailUrl8"] != null && dt.Rows[n]["ThumbnailUrl8"].ToString() != "")
                        {
                            model.ThumbnailUrl8 = dt.Rows[n]["ThumbnailUrl8"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("MaxQuantity"))
                    {
                        if (dt.Rows[n]["MaxQuantity"] != null && dt.Rows[n]["MaxQuantity"].ToString() != "")
                        {
                            model.MaxQuantity = int.Parse(dt.Rows[n]["MaxQuantity"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("MinQuantity"))
                    {
                        if (dt.Rows[n]["MinQuantity"] != null && dt.Rows[n]["MinQuantity"].ToString() != "")
                        {
                            model.MinQuantity = int.Parse(dt.Rows[n]["MinQuantity"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("Tags"))
                    {
                        if (dt.Rows[n]["Tags"] != null && dt.Rows[n]["Tags"].ToString() != "")
                        {
                            model.Tags = dt.Rows[n]["Tags"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("SeoUrl"))
                    {
                        if (dt.Rows[n]["SeoUrl"] != null && dt.Rows[n]["SeoUrl"].ToString() != "")
                        {
                            model.SeoUrl = dt.Rows[n]["SeoUrl"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("SeoImageAlt"))
                    {
                        if (dt.Rows[n]["SeoImageAlt"] != null && dt.Rows[n]["SeoImageAlt"].ToString() != "")
                        {
                            model.SeoImageAlt = dt.Rows[n]["SeoImageAlt"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("SeoImageTitle"))
                    {
                        if (dt.Rows[n]["SeoImageTitle"] != null && dt.Rows[n]["SeoImageTitle"].ToString() != "")
                        {
                            model.SeoImageTitle = dt.Rows[n]["SeoImageTitle"].ToString();
                        }
                    }
                    if (dt.Columns.Contains("Quantity"))
                    {
                        if (dt.Columns.Contains("Quantity"))
                        {
                            if (dt.Rows[n]["Quantity"] != null && dt.Rows[n]["Quantity"].ToString() != "")
                            {
                                model.Quantity = int.Parse(dt.Rows[n]["Quantity"].ToString());
                            }
                        }
                    }
                    if (dt.Columns.Contains("StartDate"))
                    {
                        if (dt.Rows[n]["StartDate"] != null && dt.Rows[n]["StartDate"].ToString() != "")
                        {
                            model.StartDate = DateTime.Parse(dt.Rows[n]["StartDate"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("EndDate"))
                    {
                        if (dt.Rows[n]["EndDate"] != null && dt.Rows[n]["EndDate"].ToString() != "")
                        {
                            model.EndDate = DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("createdate"))
                    {
                        if (dt.Rows[n]["createdate"] != null && dt.Rows[n]["createdate"].ToString() != "")
                        {
                            model.createdate = DateTime.Parse(dt.Rows[n]["createdate"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("createrid"))
                    {
                        if (dt.Rows[n]["createrid"] != null && dt.Rows[n]["createrid"].ToString() != "")
                        {
                            model.createrid = int.Parse(dt.Rows[n]["createrid"].ToString());
                        }
                    }
                    if (dt.Columns.Contains("ImportPro"))
                    {
                        if (dt.Rows[n]["ImportPro"] != null && dt.Rows[n]["ImportPro"].ToString() != "")
                        {
                            model.ImportPro = int.Parse(dt.Rows[n]["ImportPro"].ToString());
                        }
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, int Floor = 0)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex, Floor);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion Method

        #region NewMethod

        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strSetValue"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, Maticsoft.Model.Shop.Products.ProductSaleStatus saleStatus)
        {
            string strWhere = string.Format(" SaleStatus ={0}", (int)saleStatus);
            string[] ids = IDlist.Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                UpdateStatus(Convert.ToInt32(ids[i]), (int)saleStatus);
            }
            return dal.UpdateList(IDlist, strWhere);
        }


        public bool UpdateProductName(long productId, string productName)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + productId.ToString();
                Maticsoft.Model.Shop.Products.ProductInfo product = new Model.Shop.Products.ProductInfo();
                product = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey);
                if (product != null)
                {
                    product.ProductName = productName;
                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, product);
                }
            }

            return dal.UpdateProductName(productId, productName);
        }

        public DataSet GetListByCategoryIdSaleStatus(Model.Shop.Products.ProductInfo model)
        {
            if (model == null) return null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" WHERE CategoryId={0}", model.CategoryId);
            strWhere.AppendFormat(" AND SaleStatus <>{0}", model.SaleStatus);
            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE '%{0}%'", model.ProductName);
            }
            return dal.GetListByCategoryIdSaleStatus(strWhere.ToString());
        }

        /// <summary>
        /// 商品导出数据列表
        /// </summary>
        public DataSet GetListByExport(int SaleStatus, string ProductName, int CategoryId, string SKU, int BrandId)
        {
            return dal.GetListByExport(SaleStatus, ProductName, CategoryId, SKU, BrandId);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> SearchProducts(int cateId, Model.Shop.Products.ProductSearch model)
        {
            DataSet ds = dal.SearchProducts(cateId, model);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ProductDataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProductDataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductInfo();
                    //单一分类不存在
                    if (dt.Rows[n]["CategoryId"] != null && dt.Rows[n]["CategoryId"].ToString() != "")
                    {
                        model.CategoryId = int.Parse(dt.Rows[n]["CategoryId"].ToString());
                    }
                    if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                    {
                        model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                    {
                        model.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                    }
                    if (dt.Rows[n]["SupplierId"] != null && dt.Rows[n]["SupplierId"].ToString() != "")
                    {
                        model.SupplierId = int.Parse(dt.Rows[n]["SupplierId"].ToString());
                    }
                    if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    if (dt.Rows[n]["ShortDescription"] != null && dt.Rows[n]["ShortDescription"].ToString() != "")
                    {
                        model.ShortDescription = dt.Rows[n]["ShortDescription"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Title"] != null && dt.Rows[n]["Meta_Title"].ToString() != "")
                    {
                        model.Meta_Title = dt.Rows[n]["Meta_Title"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Description"] != null && dt.Rows[n]["Meta_Description"].ToString() != "")
                    {
                        model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Keywords"] != null && dt.Rows[n]["Meta_Keywords"].ToString() != "")
                    {
                        model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
                    }
                    if (dt.Rows[n]["SaleStatus"] != null && dt.Rows[n]["SaleStatus"].ToString() != "")
                    {
                        model.SaleStatus = int.Parse(dt.Rows[n]["SaleStatus"].ToString());
                    }
                    if (dt.Rows[n]["VistiCounts"] != null && dt.Rows[n]["VistiCounts"].ToString() != "")
                    {
                        model.VistiCounts = int.Parse(dt.Rows[n]["VistiCounts"].ToString());
                    }
                    if (dt.Rows[n]["SaleCounts"] != null && dt.Rows[n]["SaleCounts"].ToString() != "")
                    {
                        model.SaleCounts = int.Parse(dt.Rows[n]["SaleCounts"].ToString());
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                    {
                        model.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                    }
                    if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                    {
                        model.LowestSalePrice = decimal.Parse(dt.Rows[n]["LowestSalePrice"].ToString());
                    }
                    if (dt.Rows[n]["PenetrationStatus"] != null && dt.Rows[n]["PenetrationStatus"].ToString() != "")
                    {
                        model.PenetrationStatus = int.Parse(dt.Rows[n]["PenetrationStatus"].ToString());
                    }
                    if (dt.Rows[n]["MainCategoryPath"] != null && dt.Rows[n]["MainCategoryPath"].ToString() != "")
                    {
                        model.MainCategoryPath = dt.Rows[n]["MainCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["ExtendCategoryPath"] != null && dt.Rows[n]["ExtendCategoryPath"].ToString() != "")
                    {
                        model.ExtendCategoryPath = dt.Rows[n]["ExtendCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["Points"] != null && dt.Rows[n]["Points"].ToString() != "")
                    {
                        model.Points = decimal.Parse(dt.Rows[n]["Points"].ToString());
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["MaxQuantity"] != null && dt.Rows[n]["MaxQuantity"].ToString() != "")
                    {
                        model.MaxQuantity = int.Parse(dt.Rows[n]["MaxQuantity"].ToString());
                    }
                    if (dt.Rows[n]["MinQuantity"] != null && dt.Rows[n]["MinQuantity"].ToString() != "")
                    {
                        model.MinQuantity = int.Parse(dt.Rows[n]["MinQuantity"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 商品资料所有相关信息的集合，品牌、商家、分类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProductDataTableToListAll(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductInfo();
                    //单一分类不存在
                    if (dt.Rows[n]["CategoryId"] != null && dt.Rows[n]["CategoryId"].ToString() != "")
                    {
                        model.CategoryId = int.Parse(dt.Rows[n]["CategoryId"].ToString());
                    }
                    if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                    {
                        model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["Subhead"] != null && dt.Rows[n]["Subhead"].ToString() != "")
                    {
                        model.Subhead = dt.Rows[n]["Subhead"].ToString();
                    }
                    if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                    {
                        model.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                    }
                    if (dt.Rows[n]["SupplierId"] != null && dt.Rows[n]["SupplierId"].ToString() != "")
                    {
                        model.SupplierId = int.Parse(dt.Rows[n]["SupplierId"].ToString());
                    }
                    if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    if (dt.Rows[n]["ShortDescription"] != null && dt.Rows[n]["ShortDescription"].ToString() != "")
                    {
                        model.ShortDescription = dt.Rows[n]["ShortDescription"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Title"] != null && dt.Rows[n]["Meta_Title"].ToString() != "")
                    {
                        model.Meta_Title = dt.Rows[n]["Meta_Title"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Description"] != null && dt.Rows[n]["Meta_Description"].ToString() != "")
                    {
                        model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Keywords"] != null && dt.Rows[n]["Meta_Keywords"].ToString() != "")
                    {
                        model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
                    }
                    if (dt.Rows[n]["SaleStatus"] != null && dt.Rows[n]["SaleStatus"].ToString() != "")
                    {
                        model.SaleStatus = int.Parse(dt.Rows[n]["SaleStatus"].ToString());
                    }
                    if (dt.Rows[n]["VistiCounts"] != null && dt.Rows[n]["VistiCounts"].ToString() != "")
                    {
                        model.VistiCounts = int.Parse(dt.Rows[n]["VistiCounts"].ToString());
                    }
                    if (dt.Rows[n]["SaleCounts"] != null && dt.Rows[n]["SaleCounts"].ToString() != "")
                    {
                        model.SaleCounts = int.Parse(dt.Rows[n]["SaleCounts"].ToString());
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                    {
                        model.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                    }
                    if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                    {
                        model.LowestSalePrice = decimal.Parse(dt.Rows[n]["LowestSalePrice"].ToString());
                    }
                    if (dt.Rows[n]["PenetrationStatus"] != null && dt.Rows[n]["PenetrationStatus"].ToString() != "")
                    {
                        model.PenetrationStatus = int.Parse(dt.Rows[n]["PenetrationStatus"].ToString());
                    }
                    if (dt.Rows[n]["MainCategoryPath"] != null && dt.Rows[n]["MainCategoryPath"].ToString() != "")
                    {
                        model.MainCategoryPath = dt.Rows[n]["MainCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["ExtendCategoryPath"] != null && dt.Rows[n]["ExtendCategoryPath"].ToString() != "")
                    {
                        model.ExtendCategoryPath = dt.Rows[n]["ExtendCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["Points"] != null && dt.Rows[n]["Points"].ToString() != "")
                    {
                        model.Points = decimal.Parse(dt.Rows[n]["Points"].ToString());
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["MaxQuantity"] != null && dt.Rows[n]["MaxQuantity"].ToString() != "")
                    {
                        model.MaxQuantity = int.Parse(dt.Rows[n]["MaxQuantity"].ToString());
                    }
                    if (dt.Rows[n]["MinQuantity"] != null && dt.Rows[n]["MinQuantity"].ToString() != "")
                    {
                        model.MinQuantity = int.Parse(dt.Rows[n]["MinQuantity"].ToString());
                    }

                    //补充做索引的映射 wusg 20140726
                    if (dt.Rows[n]["CategoryName"] != null && dt.Rows[n]["CategoryName"].ToString() != "")
                    {
                        model.CategoryName = dt.Rows[n]["CategoryName"].ToString();
                    }

                    if (dt.Rows[n]["CategoryPath"] != null && dt.Rows[n]["CategoryPath"].ToString() != "")
                    {
                        model.CategoryPath = dt.Rows[n]["CategoryPath"].ToString();
                    }

                    if (dt.Rows[n]["BrandName"] != null && dt.Rows[n]["BrandName"].ToString() != "")
                    {
                        model.BrandName = dt.Rows[n]["BrandName"].ToString();
                    }

                    if (dt.Rows[n]["AddedDate"] != null && dt.Rows[n]["AddedDate"].ToString() != "")
                    {
                        model.AddedDate = DateTime.Parse(dt.Rows[n]["AddedDate"].ToString());
                    }

                    if (dt.Rows[n]["AllCategoryNames"] != null && dt.Rows[n]["AllCategoryNames"].ToString() != "")
                    {
                        model.ALLCategoryNames = dt.Rows[n]["AllCategoryNames"].ToString();
                    }
                    //被谁更新掉了，特加上1
                    if (dt.Rows[n]["Tags"] != null && dt.Rows[n]["Tags"].ToString() != "")
                    {
                        model.Tags = dt.Rows[n]["Tags"].ToString();
                    }

                    //商品分类标签
                    if (dt.Rows[n]["CTags"] != null && dt.Rows[n]["CTags"].ToString() != "")
                    {
                        model.CTags = dt.Rows[n]["CTags"].ToString();
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }


        #endregion NewMethod

        public List<Model.Shop.Products.ProductInfo> GetProductsList(string selectedPids, string pName, string categoryId, int startIdex, int endIndex, out int dataCount, long productId)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE CategoryPath LIKE '{0}%' ) ", categoryId);
            }

            //if (selectedPids != null && selectedPids.Length > 0)
            //{
            //    strWhere.AppendFormat(" AND ProductId NOT IN ({0})", selectedPids);
            //}

            if (!string.IsNullOrWhiteSpace(selectedPids))
            {
                strWhere.AppendFormat(" AND ProductId NOT IN ({0})", selectedPids);
            }

            DataSet ds = dal.GetProductListInfo(strWhere.ToString(), " ORDER BY SaleCounts DESC ", startIdex, endIndex, out dataCount, productId);

            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 商品推荐信息列表
        /// </summary>
        public List<Model.Shop.Products.ProductInfo> GetProductNoRecList(int categoryId, int supplierId, string pName, int modeType, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetProductNoRecList(categoryId, supplierId, pName, modeType, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public List<Model.Shop.Products.ProductInfo> GetProductNoSetFreeFreightList(int categoryId, int supplierId, string pName, string pCode, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetProductNoSetFreeFreightList(categoryId, supplierId, pName, pCode, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public List<Model.Shop.Products.ProductInfo> GetGiftList(int categoryId, int supplierId, string pName, int startIndex, int endIndex, DateTime StartDate, DateTime EndDate)
        {
            DataSet ds = dal.GetGiftList(categoryId, supplierId, pName, startIndex, endIndex, StartDate, EndDate);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获取消除重复行的促销商品信息
        /// </summary>
        /// <param name="categoryId">类路径</param>
        /// <param name="supplierId">供应商编号</param>
        /// <param name="pName">商品名称</param>
        /// <param name="startIndex">开始行</param>
        /// <param name="endIndex">结束行</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetGiftDistinctList(int categoryId, int supplierId, string pName, int startIndex, int endIndex, DateTime StartDate, DateTime EndDate)
        {
            DataSet ds = dal.GetGiftDistinctList(categoryId, supplierId, pName, startIndex, endIndex, StartDate, EndDate);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获取消除重复行的促销商品数量
        /// </summary>
        /// <param name="categoryId">类路径</param>
        /// <param name="supplierId">供应商编号</param>
        /// <param name="pName">商品名称</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        public int GetGiftDistinctListCount(int categoryId, int supplierId, string pName, DateTime StartDate, DateTime EndDate)
        {
            return dal.GetGiftDistinctListCount(categoryId, supplierId, pName, StartDate, EndDate);
        }

        public int GetGiftListCount(int categoryId, string pName, int supplierId, DateTime StartDate, DateTime EndDate)
        {
            return dal.GetGiftListCount(categoryId, pName, supplierId, StartDate, EndDate);
        }
        public List<Model.Shop.Products.ProductInfo> GetSelectedProducts(string groupbuyids)
        {
            DataSet ds = dal.GetSelectedProducts(groupbuyids);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 商品推荐信息Count
        /// </summary>
        public int GetProductNoRecCount(int categoryId, string pName, int modeType, int supplierId = 0)
        {
            return dal.GetProductNoRecCount(categoryId, pName, modeType, supplierId);
        }

        public List<Model.Shop.Products.ProductInfo> GetProductNoGroupBuyList(int categoryId, int supplierId, string pName, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetProductNoGroupBuyList(categoryId, supplierId, pName, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }
        public int GetProductNoGroupBuyCount(int categoryId, string pName, int supplierId)
        {
            return dal.GetProductNoGroupBuyCount(categoryId, pName, supplierId);
        }

        /// <summary>
        /// 未设置免邮的单品Count
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pName"></param>
        /// <param name="supplierId"></param>
        /// <param name="pCode"></param>
        /// <returns></returns>
        public int GetProductNoSetFreeFreightCount(int categoryId, string pName, int supplierId, string pCode)
        {
            return dal.GetProductNoSetFreeFreightCount(categoryId, pName, supplierId, pCode);
        }

        public List<Model.Shop.Products.ProductInfo> GetCommendProductsList(string[] selectedPids, string pName, string categoryId, int startIdex, int endIndex, out int dataCount, long productId, int? commendType)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat(" AND CategoryId ={0}", categoryId);
            }

            if (selectedPids != null && selectedPids.Length > 0)
            {
                strWhere.AppendFormat(" AND ProductId NOT IN ({0})", selectedPids);
            }
            if (commendType.HasValue)
            {
                if (commendType.Value == 0)
                {
                    strWhere.AppendFormat(" AND ProductId NOT IN ({0})", selectedPids);
                }
            }

            DataSet ds = dal.GetProductListInfo(strWhere.ToString(), " ORDER BY SaleCounts DESC ", startIdex, endIndex, out dataCount, productId);

            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public List<Model.Shop.Products.ProductInfo> GetProductsList(int? categoryId, string mod, int startIndex, int endIndex, out int dataCount)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" AND P.SaleStatus=1");
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                strWhere.AppendFormat(" AND PC.CategoryPath LIKE '{0}%' ", categoryId);
            }
            switch (mod)
            {
                case "rec":
                    mod = " P.DisplaySequence DESC ";
                    break;
                case "hot":
                    mod = " P.SaleCounts DESC ";
                    break;
                case "new":
                default:
                    mod = null;
                    break;
            }
            DataSet ds = dal.GetProductListByCategoryId(categoryId, strWhere.ToString(), mod, startIndex, endIndex, out dataCount);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public DataSet GetFreeFreightProductList(int FreeType, int supplierId, string pName, string pCode, int categoryId)
        {
            return dal.GetFreeFreightProductList(FreeType, supplierId, pName, pCode, categoryId);
        }

        public List<Model.Shop.Products.ProductInfo> GetProductsListEx(int? parentCategoryId, int? subCategoryId,
            string mod, int startIndex, int endIndex, out int dataCount)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" AND P.SaleStatus=1");
            if (parentCategoryId.HasValue && parentCategoryId.Value > 0)
            {
                strWhere.AppendFormat(" AND PC.CategoryPath LIKE '{0}%' ", parentCategoryId);
            }
            else if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                strWhere.AppendFormat(" AND PC.CategoryId = {0} ", subCategoryId);
            }

            switch (mod)
            {
                case "rec":
                    mod = " P.DisplaySequence DESC ";
                    break;
                case "hot":
                    mod = " P.SaleCounts DESC ";
                    break;
                case "new":
                default:
                    mod = null;
                    break;
            }
            DataSet ds = dal.GetProductListByCategoryIdEx(null, strWhere.ToString(), mod, startIndex, endIndex, out dataCount);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return ProductAndSKUToList(ds.Tables[0]);
        }


        public List<Model.Shop.Products.ProductInfo> GetProductsList(string[] selectedSkus, int startIndex, int endIndex, out int dataCount, long productId)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                dataCount = 0;
                return null;
            }
            StringBuilder strWhere = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("  AND  ProductId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }

            DataSet ds = dal.GetProductListInfo(strWhere.ToString(), " ORDER BY SaleCounts DESC ", startIndex, endIndex, out dataCount, productId);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public List<Model.Shop.Products.ProductInfo> GetProductRecListByPage(string[] selectedSkus,
                                                                       int startIndex, int endIndex, int Floor = 0)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                return null;
            }
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SaleStatus=1");
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("  AND  T.ProductId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }
            DataSet ds = GetListByPage(strWhere.ToString(), " SaleCounts DESC", startIndex, endIndex, Floor);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetProductRecListCount(string[] selectedSkus)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                return 0;
            }
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SaleStatus=1");
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("  AND  ProductId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }
            return GetRecordCount(strWhere.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetModelList(long productId)
        {
            DataSet ds = dal.GetList(string.Format(" ProductId={0}", productId));
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            return null;
        }

        /// <summary>
        /// 获取商品名 by tzh
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public string GetProductName(long productId)
        {
            return dal.GetProductName(productId);
        }

        public bool ExistsBrands(int BrandId)
        {
            return dal.ExistsBrands(BrandId);
        }

        public DataSet GetTableSchema()
        {
            return dal.GetTableSchema();
        }
        public DataSet GetTableSchemaEx()
        {

            return dal.GetTableSchemaEx();
        }
        #region 商品的id集合和商品的名称获取数据的相关操作

        /// <summary>
        /// 根据需要的字段获得相应的数据
        /// </summary>
        public DataSet GetList(string strWhere, string DataField)
        {
            return dal.GetList(strWhere, DataField);
        }

        /// <summary>
        /// 根据商品的id集合和商品的名称获取数据
        /// </summary>
        public DataSet GetListEx(string InIds, string productName, string OutIds, int CategoryId = 0)
        {
            return dal.GetList(GetListExSql(InIds, productName, OutIds, CategoryId));
        }

        /// <summary>
        /// 根据商品的id集合和商品的名称获取数据的条数
        /// </summary>
        public int GetRecordCountEx(string InIds, string productName, string OutIds, int CategoryId = 0)
        {
            return dal.GetRecordCount(GetListExSql(InIds, productName, OutIds, CategoryId));
        }

        public string GetListExSql(string InIds, string productName, string OutIds, int CategoryId = 0)
        {
            StringBuilder sbSql = new StringBuilder();
            if (!string.IsNullOrEmpty(InIds))
            {
                sbSql.Append(" ProductId in (" + InIds.TrimStart(',').TrimEnd(',') + ")");
            }
            if (!string.IsNullOrEmpty(OutIds))
            {
                if (sbSql.Length > 0)
                {
                    sbSql.Append(" and ");
                }
                sbSql.Append(" ProductId not in (" + OutIds.TrimStart(',').TrimEnd(',') + ")");
            }
            if (!string.IsNullOrEmpty(productName))
            {
                if (sbSql.Length > 0)
                {
                    sbSql.Append(" and ");
                }
                sbSql.Append(" ProductName like '%" + productName + "%'");
            }
            if (CategoryId > 0)
            {
                if (sbSql.Length > 0)
                {
                    sbSql.Append(" and ");
                }
                sbSql.Append(" CategoryId =" + CategoryId + "");
            }
            return sbSql.ToString();
        }

        #endregion 商品的id集合和商品的名称获取数据的相关操作

        public DataSet GetProductInfo(Model.Shop.Products.ProductInfo model, bool showAlert = false)
        {
            StringBuilder strWhere = new StringBuilder();
            if (model == null) return dal.GetProductInfo(null);

            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE '%{0}%' ", model.ProductName);
            }
            if (model.CategoryId > 0)
            {
                strWhere.AppendFormat("AND PC.CategoryPath LIKE '{0}%' ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.SearchProductCategories))
            {
                strWhere.AppendFormat(" AND PC.CategoryId IN ( {0} ) ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductCode))
            {
                strWhere.AppendFormat("AND ProductCode = '{0}' ", model.ProductCode);
            }
            if (model.SupplierId != 0)
            {
                strWhere.AppendFormat("AND P.SupplierId = {0} ", model.SupplierId);
            }
            if (model.SuppCategoryId > 0)
            {
                strWhere.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_SuppProductCategories WHERE  ProductId =P.ProductId  ");
                strWhere.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_SupplierCategories WHERE CategoryId = {0}  ) + '|%' ", model.SuppCategoryId);
                strWhere.AppendFormat(" OR Shop_SuppProductCategories.CategoryId = {0}))", model.SuppCategoryId);
            }
            if (showAlert)
            {
                strWhere.AppendFormat("  AND  SKU.Stock<=SKU.AlertStock  ");
            }
            strWhere.AppendFormat(" AND P.SaleStatus= {0}  ", model.SaleStatus);
            return dal.GetProductInfo(strWhere.ToString());
        }

        /// <summary>
        /// 满足SKU查询
        /// </summary>
        /// <param name="model">商品Model</param>
        /// <param name="SKU">SKU</param>
        /// <param name="showAlert"></param>
        /// <returns></returns>
        public DataSet GetProductInfo(Model.Shop.Products.ProductInfo model, string SKU, bool showAlert = false)
        {
            StringBuilder strWhere = new StringBuilder();
            if (model == null) return dal.GetProductInfo(null);

            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE '%{0}%' ", model.ProductName);
            }
            if (model.CategoryId > 0)
            {
                strWhere.AppendFormat("AND PC.CategoryPath LIKE '{0}%' ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.SearchProductCategories))
            {
                strWhere.AppendFormat(" AND PC.CategoryId IN ( {0} ) ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductCode))
            {
                strWhere.AppendFormat("AND ProductCode = '{0}' ", model.ProductCode);
            }
            if (model.SupplierId != 0)
            {
                strWhere.AppendFormat("AND P.SupplierId = {0} ", model.SupplierId);
            }

            if (!string.IsNullOrWhiteSpace(SKU))
            {
                strWhere.AppendFormat("AND P.ProductId in(select ProductId from dbo.Shop_SKUs where SKU like '%{0}%') ", SKU);
            }

            if (model.SuppCategoryId > 0)
            {
                strWhere.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_SuppProductCategories WHERE  ProductId =P.ProductId  ");
                strWhere.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_SupplierCategories WHERE CategoryId = {0}  ) + '|%' ", model.SuppCategoryId);
                strWhere.AppendFormat(" OR Shop_SuppProductCategories.CategoryId = {0}))", model.SuppCategoryId);
            }
            if (showAlert)
            {
                strWhere.AppendFormat("  AND  SKU.Stock<=SKU.AlertStock  ");
            }
            strWhere.AppendFormat(" AND P.SaleStatus= {0}  ", model.SaleStatus);
            return dal.GetProductInfo(strWhere.ToString());
        }


        public DataSet DeleteProducts(string Ids, out int Result)
        {
            return dal.DeleteProducts(Ids, out Result);
        }

        public bool ChangeProductsCategory(string productIds, int categoryId)
        {
            return dal.ChangeProductsCategory(productIds, categoryId);
        }
        /// <summary>
        /// 获取回收站数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecycleList(string strWhere)
        {
            return dal.GetRecycleList(strWhere);
        }

        /// <summary>
        /// 还原所有商品
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strSetValue"></param>
        /// <returns></returns>
        public bool RevertAll()
        {
            return dal.RevertAll();
        }

        /// <summary>
        /// 更新商品状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strSetValue"></param>
        /// <returns></returns>
        public bool UpdateStatus(long productId, int SaleStatus)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + productId.ToString();
                Maticsoft.Model.Shop.Products.ProductInfo product = new Model.Shop.Products.ProductInfo();
                product = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey);
                if (product != null)
                {
                    product.SaleStatus = SaleStatus;
                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, product);
                }
            }

            return dal.UpdateStatus(productId, SaleStatus);
        }

        public long StockNum(long productId)
        {
            return dal.StockNum(productId);
        }

        //public bool UpdateStockNum(long productId, int )
        //{
        //    return dal.UpdateStockNum(productId, productName);
        //}

        public bool UpdateMarketPrice(long productId, decimal price)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + productId.ToString();
                Maticsoft.Model.Shop.Products.ProductInfo product = new Model.Shop.Products.ProductInfo();
                product = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey);
                if (product != null)
                {
                    product.MarketPrice = price;
                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, product);
                }
            }

            return dal.UpdateMarketPrice(productId, price);
        }
        public bool UpdateLowestSalePrice(long productId, decimal price)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + productId.ToString();
                Maticsoft.Model.Shop.Products.ProductInfo product = new Model.Shop.Products.ProductInfo();
                product = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey);
                if (product != null)
                {
                    product.SalePrice = price;
                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, product);
                }
            }

            return dal.UpdateLowestSalePrice(productId, price);
        }

        /// <summary>
        /// 获取商品推荐信息 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductRecList(ProductRecType type, int categoryId = 0, int top = -1)
        {
            DataSet ds = dal.GetProductRecList(type, categoryId, top);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                List<Model.Shop.Products.ProductInfo> listProduct = ProductRecTableToList(ds.Tables[0]);
                Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
                foreach (var productInfo in listProduct)
                {
                    //获取分类名称
                    productInfo.CategoryName = cateBll.GetNameByPid(productInfo.ProductId);
                }
                return listProduct;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取推荐产品信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductRecList2(int type, int categoryId, int top)
        {
            DataSet ds = dal.GetProductRecList2(type, categoryId, top);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Model.Shop.Products.ProductInfo> listProduct = ProductRecTableToList(ds.Tables[0]);
                Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
                foreach (var productInfo in listProduct)
                {
                    //获取分类名称
                    productInfo.CategoryName = cateBll.GetNameByPid(productInfo.ProductId);
                }
                return listProduct;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取商品推荐信息 (不考虑分类)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductRecListWithOutCatg(ProductRecType type, int Floor, int top = -1)
        {
            DataSet ds = dal.GetProductRecListWithOutCatg(type, Floor, top);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                List<Model.Shop.Products.ProductInfo> listProduct = ProductRecTableToList(ds.Tables[0]);
                Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
                foreach (var productInfo in listProduct)
                {
                    //获取分类名称
                    productInfo.CategoryName = cateBll.GetNameByPid(productInfo.ProductId);
                }
                return listProduct;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取推荐产品信息(不考虑分类  李永琴)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductRecListWithOutCatgB(ProductRecType type, int Floor, int top)
        {
            //DataSet ds = dal.GetProductRecListWithOutCatgB(type, Floor, top);

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{

            //    List<Model.Shop.Products.ProductInfo> listProduct = ProductRecTableToList(ds.Tables[0]);
            //    Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
            //    foreach (var productInfo in listProduct)
            //    {
            //        //获取分类名称
            //        productInfo.CategoryName = cateBll.GetNameByPid(productInfo.ProductId);
            //    }
            //    return listProduct;
            //}
            //else
            //{
            //    return null;
            //}

            string CacheKey = "Index_XianShi";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    DataSet ds = dal.GetProductRecListWithOutCatgB(type, Floor, top);
                    objModel = DataTableToList(ds.Tables[0]);

                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("Cache_HorsTime");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }

                    //if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    List<Model.Shop.Products.ProductInfo> listProduct = ProductRecTableToList(ds.Tables[0]);
                    //    Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
                    //    foreach (var productInfo in listProduct)
                    //    {
                    //        //获取分类名称
                    //        productInfo.CategoryName = cateBll.GetNameByPid(productInfo.ProductId);
                    //    }
                    //    return listProduct;
                    //}
                    //else
                    //{
                    //    return null;
                    //}
                }
                catch { }
            }
            return (List<Model.Shop.Products.ProductInfo>)objModel;

        }



        public int GetProductRecCount(ProductRecType type, int categoryId)
        {
            return dal.GetProductRecCount(type, categoryId);
        }
        public List<Model.Shop.Products.ProductInfo> GetProductRanListByRec(ProductRecType type, int categoryId = 0, int top = -1)
        {
            DataSet ds = dal.GetProductRanListByRec(type, categoryId, top);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                List<Model.Shop.Products.ProductInfo> listProduct = ProductRecTableToList(ds.Tables[0]);
                Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
                foreach (var productInfo in listProduct)
                {
                    //获取分类名称
                    productInfo.CategoryName = cateBll.GetNameByPid(productInfo.ProductId);
                }
                return listProduct;
            }
            else
            {
                return null;
            }
        }

        public List<Model.Shop.Products.ProductInfo> GetProductRanList(int top = -1)
        {
            DataSet ds = dal.GetProductRanList(top);
            return ProductAndSKUToList(ds.Tables[0]);

        }


        /// <summary>
        /// 获取产品所关联的产品信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> RelatedProductsList(long productId, int top = -1)
        {
            DataSet ds = dal.RelatedProductSource(productId, top);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 商品推荐
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProductRecTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                bool HasMarketPrice = dt.Columns.Contains("MarketPrice");
                Maticsoft.Model.Shop.Products.ProductInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                    {
                        model.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl2"] != null && dt.Rows[n]["ThumbnailUrl2"].ToString() != "")
                    {
                        model.ThumbnailUrl2 = dt.Rows[n]["ThumbnailUrl2"].ToString();
                    }
                    if (dt.Rows[n]["ShortDescription"] != null && dt.Rows[n]["ShortDescription"].ToString() != "")
                    {
                        model.ShortDescription = dt.Rows[n]["ShortDescription"].ToString();
                    }

                    if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                    {
                        model.LowestSalePrice = Common.Globals.SafeDecimal(dt.Rows[n]["LowestSalePrice"].ToString(), 0);
                    }
                    model.MarketPrice = 0;
                    if (HasMarketPrice)
                    {
                        if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                        {
                            model.MarketPrice = Common.Globals.SafeDecimal(dt.Rows[n]["MarketPrice"].ToString(), 0);
                        }
                    }
                   
                    if (dt.Columns.Contains("ImageUrl"))
                    {
                        if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                        {
                            model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                        }
                    }


                    //if (dt.Rows[n]["Weight"] != null && dt.Rows[n]["Weight"].ToString() != "")
                    //{
                    //    model.Weight = Common.Globals.SafeDecimal(dt.Rows[n]["Weight"].ToString(), 0);
                    //}
                    //if (dt.Rows[n]["SalePrice"] != null && dt.Rows[n]["SalePrice"].ToString() != "")
                    //{
                    //    model.SalePrice = Common.Globals.SafeDecimal(dt.Rows[n]["SalePrice"].ToString(), 0);
                    //}
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        public int MaxSequence()
        {
            return dal.MaxSequence();
        }

        /// <summary>
        /// 产品信息和SKU信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProductAndSKUToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if (dt.Rows[n]["CategoryId"] != null && dt.Rows[n]["CategoryId"].ToString() != "")
                    {
                        model.CategoryId = int.Parse(dt.Rows[n]["CategoryId"].ToString());
                    }
                    if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                    {
                        model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                    {
                        model.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                    }
                    if (dt.Rows[n]["SupplierId"] != null && dt.Rows[n]["SupplierId"].ToString() != "")
                    {
                        model.SupplierId = int.Parse(dt.Rows[n]["SupplierId"].ToString());
                    }
                    if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    if (dt.Rows[n]["ShortDescription"] != null && dt.Rows[n]["ShortDescription"].ToString() != "")
                    {
                        model.ShortDescription = dt.Rows[n]["ShortDescription"].ToString();
                    }
                    if (dt.Rows[n]["Unit"] != null && dt.Rows[n]["Unit"].ToString() != "")
                    {
                        model.Unit = dt.Rows[n]["Unit"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Title"] != null && dt.Rows[n]["Meta_Title"].ToString() != "")
                    {
                        model.Meta_Title = dt.Rows[n]["Meta_Title"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Description"] != null && dt.Rows[n]["Meta_Description"].ToString() != "")
                    {
                        model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Keywords"] != null && dt.Rows[n]["Meta_Keywords"].ToString() != "")
                    {
                        model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
                    }
                    if (dt.Rows[n]["SaleStatus"] != null && dt.Rows[n]["SaleStatus"].ToString() != "")
                    {
                        model.SaleStatus = int.Parse(dt.Rows[n]["SaleStatus"].ToString());
                    }
                    if (dt.Rows[n]["AddedDate"] != null && dt.Rows[n]["AddedDate"].ToString() != "")
                    {
                        model.AddedDate = DateTime.Parse(dt.Rows[n]["AddedDate"].ToString());
                    }
                    if (dt.Rows[n]["VistiCounts"] != null && dt.Rows[n]["VistiCounts"].ToString() != "")
                    {
                        model.VistiCounts = int.Parse(dt.Rows[n]["VistiCounts"].ToString());
                    }
                    if (dt.Rows[n]["SaleCounts"] != null && dt.Rows[n]["SaleCounts"].ToString() != "")
                    {
                        model.SaleCounts = int.Parse(dt.Rows[n]["SaleCounts"].ToString());
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["LineId"] != null && dt.Rows[n]["LineId"].ToString() != "")
                    {
                        model.LineId = int.Parse(dt.Rows[n]["LineId"].ToString());
                    }
                    if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                    {
                        model.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                    }
                    if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                    {
                        model.LowestSalePrice = decimal.Parse(dt.Rows[n]["LowestSalePrice"].ToString());
                    }
                    if (dt.Rows[n]["PenetrationStatus"] != null && dt.Rows[n]["PenetrationStatus"].ToString() != "")
                    {
                        model.PenetrationStatus = int.Parse(dt.Rows[n]["PenetrationStatus"].ToString());
                    }
                    if (dt.Rows[n]["MainCategoryPath"] != null && dt.Rows[n]["MainCategoryPath"].ToString() != "")
                    {
                        model.MainCategoryPath = dt.Rows[n]["MainCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["ExtendCategoryPath"] != null && dt.Rows[n]["ExtendCategoryPath"].ToString() != "")
                    {
                        model.ExtendCategoryPath = dt.Rows[n]["ExtendCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["HasSKU"] != null && dt.Rows[n]["HasSKU"].ToString() != "")
                    {
                        if ((dt.Rows[n]["HasSKU"].ToString() == "1") || (dt.Rows[n]["HasSKU"].ToString().ToLower() == "true"))
                        {
                            model.HasSKU = true;
                        }
                        else
                        {
                            model.HasSKU = false;
                        }
                    }
                    if (dt.Rows[n]["Points"] != null && dt.Rows[n]["Points"].ToString() != "")
                    {
                        model.Points = decimal.Parse(dt.Rows[n]["Points"].ToString());
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl2"] != null && dt.Rows[n]["ThumbnailUrl2"].ToString() != "")
                    {
                        model.ThumbnailUrl2 = dt.Rows[n]["ThumbnailUrl2"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl3"] != null && dt.Rows[n]["ThumbnailUrl3"].ToString() != "")
                    {
                        model.ThumbnailUrl3 = dt.Rows[n]["ThumbnailUrl3"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl4"] != null && dt.Rows[n]["ThumbnailUrl4"].ToString() != "")
                    {
                        model.ThumbnailUrl4 = dt.Rows[n]["ThumbnailUrl4"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl5"] != null && dt.Rows[n]["ThumbnailUrl5"].ToString() != "")
                    {
                        model.ThumbnailUrl5 = dt.Rows[n]["ThumbnailUrl5"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl6"] != null && dt.Rows[n]["ThumbnailUrl6"].ToString() != "")
                    {
                        model.ThumbnailUrl6 = dt.Rows[n]["ThumbnailUrl6"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl7"] != null && dt.Rows[n]["ThumbnailUrl7"].ToString() != "")
                    {
                        model.ThumbnailUrl7 = dt.Rows[n]["ThumbnailUrl7"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl8"] != null && dt.Rows[n]["ThumbnailUrl8"].ToString() != "")
                    {
                        model.ThumbnailUrl8 = dt.Rows[n]["ThumbnailUrl8"].ToString();
                    }
                    if (dt.Rows[n]["MaxQuantity"] != null && dt.Rows[n]["MaxQuantity"].ToString() != "")
                    {
                        model.MaxQuantity = int.Parse(dt.Rows[n]["MaxQuantity"].ToString());
                    }
                    if (dt.Rows[n]["MinQuantity"] != null && dt.Rows[n]["MinQuantity"].ToString() != "")
                    {
                        model.MinQuantity = int.Parse(dt.Rows[n]["MinQuantity"].ToString());
                    }
                    if (dt.Rows[n]["Tags"] != null && dt.Rows[n]["Tags"].ToString() != "")
                    {
                        model.Tags = dt.Rows[n]["Tags"].ToString();
                    }
                    if (dt.Rows[n]["SeoUrl"] != null && dt.Rows[n]["SeoUrl"].ToString() != "")
                    {
                        model.SeoUrl = dt.Rows[n]["SeoUrl"].ToString();
                    }
                    if (dt.Rows[n]["SeoImageAlt"] != null && dt.Rows[n]["SeoImageAlt"].ToString() != "")
                    {
                        model.SeoImageAlt = dt.Rows[n]["SeoImageAlt"].ToString();
                    }
                    if (dt.Rows[n]["SeoImageTitle"] != null && dt.Rows[n]["SeoImageTitle"].ToString() != "")
                    {
                        model.SeoImageTitle = dt.Rows[n]["SeoImageTitle"].ToString();
                    }
                    if (dt.Rows[n]["SalePrice"] != null && dt.Rows[n]["SalePrice"].ToString() != "")
                    {
                        model.SalePrice = decimal.Parse(dt.Rows[n]["SalePrice"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获取对应批发规则的商品
        /// </summary>
        /// <param name="selectedSkus"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetRuleProductList(string[] selectedSkus, int startIndex, int endIndex)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                return null;
            }
            StringBuilder strWhere = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("   ProductId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }
            DataSet ds = GetListByPage(strWhere.ToString(), " SaleCounts DESC", startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获取包含批发规则的商品数
        /// </summary>
        /// <param name="selectedSkus"></param>
        /// <returns></returns>
        public int GetRuleProductCount(string[] selectedSkus)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                return 0;
            }
            StringBuilder strWhere = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("   ProductId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }
            return GetRecordCount(strWhere.ToString());
        }

        /// <summary>
        /// 获取不包含批发规则的商品
        /// </summary>
        /// <param name="selectedPids"></param>
        /// <param name="pName"></param>
        /// <param name="categoryId"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetNoRuleProductList(string pName, string categoryId, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SaleStatus=1");
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE' %{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}) ) ", categoryId);
            }

            strWhere.Append(" AND  NOT EXISTS(SELECT *  FROM Shop_SalesRuleProduct WHERE ProductId=T.ProductId) ");

            DataSet ds = GetListByPage(strWhere.ToString(), "  SaleCounts DESC ", startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        ///  获取不包含批发规则的商品数
        /// </summary>
        /// <param name="selectedPids"></param>
        /// <param name="pName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int GetNoRuleProductCount(string pName, string categoryId)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SaleStatus=1");
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}) ) ", categoryId);
            }

            strWhere.Append(" AND  NOT EXISTS(SELECT *  FROM Shop_SalesRuleProduct WHERE ProductId=Shop_Products.ProductId) ");
            return GetRecordCount(strWhere.ToString());
        }



        /// <summary>
        /// 获取对应活动规则的商品
        /// </summary>
        /// <param name="selectedSkus"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetAMProductList(string[] selectedSkus, int startIndex, int endIndex)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                return null;
            }
            StringBuilder strWhere = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("   ProductId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }
            DataSet ds = GetListByPage(strWhere.ToString(), " SaleCounts DESC", startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获取包含活动规则的商品数
        /// </summary>
        /// <param name="selectedSkus"></param>
        /// <returns></returns>
        public int GetAMProductCount(string[] selectedSkus)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                return 0;
            }
            StringBuilder strWhere = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("   ProductId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }
            return GetRecordCount(strWhere.ToString());
        }

        /// <summary>
        /// 获取不包含活动规则的商品
        /// </summary>
        /// <param name="selectedPids"></param>
        /// <param name="pName"></param>
        /// <param name="categoryId"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetNoAMProductList(string pName, string pCode, string categoryId, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SaleStatus=1");
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE' %{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(pCode))
            {
                strWhere.AppendFormat(" AND ProductCode LIKE '%{0}%'", pCode);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}) ) ", categoryId);
            }

            strWhere.Append(" AND  NOT EXISTS(SELECT *  FROM Shop_ActivityManageProduct WHERE ProductId=T.ProductId) ");

            DataSet ds = GetListByPage(strWhere.ToString(), "  SaleCounts DESC ", startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        ///  获取不包含活动规则的商品数
        /// </summary>
        /// <param name="selectedPids"></param>
        /// <param name="pName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int GetNoAMProductCount(string pName, string categoryId)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SaleStatus=1");
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}) ) ", categoryId);
            }

            strWhere.Append(" AND  NOT EXISTS(SELECT *  FROM Shop_ActivityManageProduct WHERE ProductId=Shop_Products.ProductId) ");
            return GetRecordCount(strWhere.ToString());
        }


        public int GetProductsCountEx(int Cid, int BrandId, string attrValues, string priceRange)
        {
            return dal.GetProductsCountEx(Cid, BrandId, attrValues, priceRange);
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="Cid">商品分类</param>
        /// <param name="BrandId">品牌</param>
        /// <param name="attrValues">属性值</param>
        /// <param name="priceRange">价格区间</param>
        /// <param name="mod">排序方式</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductsListEx(int Cid, int BrandId, string attrValues, string priceRange,
    string mod, int startIndex, int endIndex)
        {

            switch (mod)
            {
                case "default":
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
                case "hot":
                    mod = " T.SaleCounts DESC ";
                    break;
                case "new":
                    mod = "T.AddedDate desc ";
                    break;
                case "price":
                    mod = "T.LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "T.LowestSalePrice  desc";
                    break;
                default:
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
            }
            DataSet ds = dal.GetProductsListEx(Cid, BrandId, attrValues, priceRange, mod, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 根据条件获取分页数据分享商品
        /// </summary>
        /// <param name="Cid">商品分类</param>
        /// <param name="BrandId">品牌</param>
        /// <param name="attrValues">属性值</param>
        /// <param name="priceRange">价格区间</param>
        /// <param name="mod">排序方式</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductsListExShare(int Cid, int BrandId, string attrValues, string priceRange,
    string mod, int type, int startIndex, int endIndex)
        {

            switch (mod)
            {
                case "default":
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
                case "hot":
                    mod = " T.SaleCounts DESC ";
                    break;
                case "new":
                    mod = "T.AddedDate desc ";
                    break;
                case "price":
                    mod = "T.LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "T.LowestSalePrice  desc";
                    break;
                default:
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
            }
            DataSet ds = dal.GetProductsListExShare(Cid, BrandId, attrValues, priceRange, mod, type, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public int GetProductsCountExShare(int Cid, int BrandId, string attrValues, string priceRange, int type)
        {
            return dal.GetProductsCountExShare(Cid, BrandId, attrValues, priceRange, type);
        }

        #region 李永琴
        public int GetProductsCountExShareB(int Cid, int BrandId, string attrValues, string priceRange, int type, string path)
        {
            return dal.GetProductsCountExShareB(Cid, BrandId, attrValues, priceRange, type, path);
        }

        public int GetProductsCountExShareC(int Gtype, int GoodtypeId, string attrValues, string priceRange, int type, string path)
        {
            return dal.GetProductsCountExShareC(Gtype, GoodtypeId, attrValues, priceRange, type, path);
        }


        public List<Model.Shop.Products.ProductInfo> GetProductsListExShareB(int Cid, int BrandId, string attrValues, string priceRange,
         string mod, int type, int startIndex, int endIndex, string path)
        {

            switch (mod)
            {
                case "default":
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
                case "hot":
                    mod = " T.SaleCounts DESC ";
                    break;
                case "new":
                    mod = "T.AddedDate desc ";
                    break;
                case "price":
                    mod = "T.LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "T.LowestSalePrice  desc";
                    break;
                default:
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
            }
            DataSet ds = dal.GetProductsListExShareB(Cid, BrandId, attrValues, priceRange, mod, type, startIndex, endIndex, path);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public List<Model.Shop.Products.ProductInfo> GetProductsListExShareC(int Gtype, int GoodtypeId, string attrValues, string priceRange,
        string mod, int type, int startIndex, int endIndex, string path)
        {
            switch (mod)
            {
                case "default":
                    mod = " ISNULL(aa.Sort, 0) ASC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
                case "hot":
                    mod = " T.SaleCounts DESC ";
                    break;
                case "new":
                    mod = "T.AddedDate desc ";
                    break;
                case "price":
                    mod = "T.LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "T.LowestSalePrice  desc";
                    break;
                default:
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
            }
            DataSet ds = dal.GetProductsListExShareC(Gtype, GoodtypeId, attrValues, priceRange, mod, type, startIndex, endIndex, path);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        #endregion

        #region yujuxin
        /// <summary>
        /// 活动分类商品总数
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetProductsCountExActiveB(int Cid, int BrandId, string attrValues, string priceRange, int type)
        {
            return dal.GetProductsCountExActiveB(Cid, BrandId, attrValues, priceRange, type);
        }

        /// <summary>
        /// 活动分类商品列表
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="type"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductsListExActiveB(int Cid, int BrandId, string attrValues, string priceRange,
         string mod, int type, int startIndex, int endIndex)
        {

            switch (mod)
            {
                case "default":
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
                case "hot":
                    mod = " T.SaleCounts DESC ";
                    break;
                case "new":
                    mod = "T.AddedDate desc ";
                    break;
                case "price":
                    mod = "T.LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "T.LowestSalePrice  desc";
                    break;
                default:
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
            }
            DataSet ds = dal.GetProductsListExActiveB(Cid, BrandId, attrValues, priceRange, mod, type, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }



        #endregion


        #region  商品搜索数据查询
        public int GetSearchCountEx(int Cid, int BrandId, string keyWord, string priceRange)
        {
            return dal.GetSearchCountEx(Cid, BrandId, keyWord, priceRange);
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetSearchListEx(int Cid, int BrandId, string keyWord, string priceRange,
    string mod, int startIndex, int endIndex)
        {

            switch (mod)
            {

                case "default":
                    mod = " DisplaySequence DESC ";
                    break;
                case "hot":
                    mod = " SaleCounts DESC ";
                    break;
                case "new":
                    mod = "AddedDate desc ";
                    break;
                case "price":
                    mod = "LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "LowestSalePrice  desc";
                    break;
                default:
                    mod = null;
                    break;
            }
            DataSet ds = dal.GetSearchListEx(Cid, BrandId, keyWord, priceRange, mod, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }
        #endregion

        #region  分享商品搜索数据查询
        public int GetSearchCountExShare(int Cid, int BrandId, string keyWord, string priceRange)
        {
            return dal.GetSearchCountExShare(Cid, BrandId, keyWord, priceRange);
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetSearchListExShare(int Cid, int BrandId, string keyWord, string priceRange,
                                                                    string mod, int startIndex, int endIndex)
        {
            switch (mod)
            {

                case "default":
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
                case "hot":
                    mod = " T.SaleCounts DESC ";
                    break;
                case "new":
                    mod = "T.AddedDate desc ";
                    break;
                case "price":
                    mod = "T.LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "T.LowestSalePrice  desc";
                    break;
                default:
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
            }
            DataSet ds = dal.GetSearchListExShare(Cid, BrandId, keyWord, priceRange, mod, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        #region  李永琴
        /// <summary>
        /// 搜索分享商品数据(李永琴)
        /// </summary>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetSearchListExShareB(int Cid, int BrandId, string keyword, string priceRange,
                                          string mod, int startIndex, int endIndex, string path)
        {
            switch (mod)
            {

                case "default":
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
                case "hot":
                    mod = " T.SaleCounts DESC ";
                    break;
                case "new":
                    mod = "T.AddedDate desc ";
                    break;
                case "price":
                    mod = "T.LowestSalePrice ";
                    break;
                case "pricedesc":
                    mod = "T.LowestSalePrice  desc";
                    break;
                default:
                    mod = " ISNULL(T.Recommend,0) DESC ,ISNULL(t.UpdateTime,AddedDate) DESC";
                    break;
            }
            DataSet ds = dal.GetSearchListExShareB(Cid, BrandId, keyword, priceRange, mod, startIndex, endIndex, path);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        public int GetSearchCountExShareB(int Cid, int BrandId, string keyWord, string priceRange, string path)
        {
            return dal.GetSearchCountExShareB(Cid, BrandId, keyWord, priceRange, path);
        }
        #endregion


        #endregion
        /// <summary>
        /// 根据类别地址 得到该类别下最大顺序值
        /// </summary>
        /// <param name="CategoryPath"></param>
        /// <returns></returns>
        public int MaxSequence(string CategoryPath)
        {
            return dal.MaxSequence(CategoryPath);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetKeyWordList(int top, string keyWord)
        {
            string CacheKey = "GetKeyWordList-" + top + keyWord;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("  SaleStatus = 1  ");
                    strSql.AppendFormat("  and  ProductName like '%{0}%' or ShortDescription like '%{0}%'  ", keyWord);
                    DataSet ds = dal.GetList(top, strSql.ToString(), "  NewID()");
                    objModel = DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Shop.Products.ProductInfo>)objModel;
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string productCode)
        {
            return dal.Exists(productCode);
        }

        /// <summary>
        /// 获取授权用户的店铺商品列表
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="keyword"></param>
        /// <param name="page_no"></param>
        /// <param name="page_size"></param>
        /// <param name="vertical_market"></param>
        /// <param name="market_id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<TaoBao.Domain.Item> GetTaoListByUser(string sessionKey, int cid, string keyword, int page_no = 1, int page_size = 40, string hasDiscount = "", string hasShowcase = "")
        {
            List<TaoBao.Domain.Item> TaoDataList = new List<TaoBao.Domain.Item>();
            ITopClient client = BLL.Shop.TaoBaoConfig.GetTopClient();
            ItemsOnsaleGetRequest req = new ItemsOnsaleGetRequest();
            if (cid > 0)
            {
                req.Cid = cid;
            }

            req.PageSize = page_size;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                req.Q = keyword;
            }
            if (!String.IsNullOrWhiteSpace(hasDiscount))
            {
                req.HasDiscount = Common.Globals.SafeBool(hasDiscount, false);
            }

            if (!String.IsNullOrWhiteSpace(hasShowcase))
            {
                req.HasShowcase = Common.Globals.SafeBool(hasShowcase, false);
            }
            req.Fields = "num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_invoice,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id";
            for (int i = 1; i <= page_no; i++)
            {
                req.PageNo = i;
                ItemsOnsaleGetResponse response = client.Execute(req, sessionKey);
                if (response.Items.Count > 0)
                {
                    //获取商品评论  这个可以采用批量方式获取
                    string ids = String.Join(",", response.Items.Select(c => c.NumIid));
                    List<TaoBao.Domain.Item> itemList = GetTaoListByIds(sessionKey, ids);
                    TaoDataList.AddRange(itemList);
                }
            }
            return TaoDataList;
        }

        public List<TaoBao.Domain.Item> GetTaoListByIds(string sessionKey, string ids)
        {
            List<TaoBao.Domain.Item> TaoDataList = new List<TaoBao.Domain.Item>();
            ITopClient client = BLL.Shop.TaoBaoConfig.GetTopClient();
            ItemsListGetRequest req = new ItemsListGetRequest();
            req.Fields = "num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_invoice,has_showcase,modified,delist_time,postage_id,seller_cids,desc";
            req.NumIids = ids;
            ItemsListGetResponse response = client.Execute(req, sessionKey);
            if (response.Items.Count > 0)
            {
                TaoDataList.AddRange(response.Items);
            }
            return TaoDataList;
        }

        /// <summary>
        /// 根据分类ID获取商品列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsByCid(int cid)
        {
            DataSet ds = dal.GetProductsByCid(cid);
            return DataTableToList(ds.Tables[0]);
        }

        #region 商品促销-限时抢购
        /// <summary>
        /// 获取秒杀Count
        /// </summary>
        /// <returns></returns>
        public int GetProSalesCount()
        {
            return dal.GetProSalesCount();
        }
        /// <summary>
        /// 获取秒杀Count
        /// </summary>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProSalesList(int startIndex, int endIndex, int type)
        {
            DataSet ds = dal.GetProSalesList(startIndex, endIndex, type);
            return ProSalesToList(ds.Tables[0]);
        }
        #region 会员体验区查询
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProSalesList(int startIndex, int endIndex, int type, int CategoryId)
        {
            DataSet ds = dal.GetProSalesList(startIndex, endIndex, type, CategoryId);
            return ProSalesToList(ds.Tables[0]);
        }
        #endregion

        /// <summary>
        /// 获取促销商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ProductInfo GetProSaleModel(int id)
        {
            DataSet ds = dal.GetProSaleModel(id);
            return ProSalesToList(ds.Tables[0]).Count > 0 ? ProSalesToList(ds.Tables[0])[0] : null;
        }

        /// <summary>
        /// 促销商品Model化
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProSalesToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductInfo();

                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                    {
                        model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                    {
                        model.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                    }

                    if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    if (dt.Rows[n]["SaleStatus"] != null && dt.Rows[n]["SaleStatus"].ToString() != "")
                    {
                        model.SaleStatus = int.Parse(dt.Rows[n]["SaleStatus"].ToString());
                    }
                    if (dt.Rows[n]["AddedDate"] != null && dt.Rows[n]["AddedDate"].ToString() != "")
                    {
                        model.AddedDate = DateTime.Parse(dt.Rows[n]["AddedDate"].ToString());
                    }

                    if (dt.Rows[n]["SaleCounts"] != null && dt.Rows[n]["SaleCounts"].ToString() != "")
                    {
                        model.SaleCounts = int.Parse(dt.Rows[n]["SaleCounts"].ToString());
                    }
                    if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                    {
                        model.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                    }
                    if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                    {
                        model.LowestSalePrice = decimal.Parse(dt.Rows[n]["LowestSalePrice"].ToString());
                    }
                    if (dt.Rows[n]["Points"] != null && dt.Rows[n]["Points"].ToString() != "")
                    {
                        model.Points = decimal.Parse(dt.Rows[n]["Points"].ToString());
                    }
                    if (dt.Rows[n]["Description"] != null)
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["Tags"] != null && dt.Rows[n]["Tags"].ToString() != "")
                    {
                        model.Tags = dt.Rows[n]["Tags"].ToString();
                    }
                    if (dt.Rows[n]["SeoUrl"] != null && dt.Rows[n]["SeoUrl"].ToString() != "")
                    {
                        model.SeoUrl = dt.Rows[n]["SeoUrl"].ToString();
                    }
                    //促销价格
                    if (dt.Rows[n]["ProSalesPrice"] != null && dt.Rows[n]["ProSalesPrice"].ToString() != "")
                    {
                        model.ProSalesPrice = decimal.Parse(dt.Rows[n]["ProSalesPrice"].ToString());
                    }
                    //结束时间
                    if (dt.Rows[n]["ProSalesEndDate"] != null && dt.Rows[n]["ProSalesEndDate"].ToString() != "")
                    {
                        model.ProSalesEndDate = DateTime.Parse(dt.Rows[n]["ProSalesEndDate"].ToString());
                    }
                    if (dt.Rows[n]["CountDownId"] != null && dt.Rows[n]["CountDownId"].ToString() != "")
                    {
                        model.CountDownId = int.Parse(dt.Rows[n]["CountDownId"].ToString());
                    }
                    if (dt.Columns.Contains("CountDownDescription"))
                    {
                        if (dt.Rows[n]["CountDownDescription"] != null && dt.Rows[n]["CountDownDescription"].ToString() != "")
                        {
                            model.CountDownDescription = dt.Rows[n]["CountDownDescription"].ToString();
                        }
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

        #region 团购商品方法
        /// <summary>
        /// 获取团购数据
        /// </summary>
        /// <returns></returns>
        public int GetGroupBuyCount()
        {
            return dal.GetGroupBuyCount();
        }
        /// <summary>
        /// 获取团购数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetGroupBuyList(int startIndex, int endIndex)
        {
            DataSet ds = dal.GetGroupBuyList(startIndex, endIndex);
            return GroupBuyToList(ds.Tables[0]);
        }
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetGroupBuyList(int cid, int regionId, int startIndex,
                                                                               int endIndex, string orderby)
        {
            switch (orderby)
            {
                case "default":
                    orderby = " DisplaySequence DESC ";
                    break;
                case "hot":
                    orderby = " SaleCounts DESC ";
                    break;
                case "new":
                    orderby = "AddedDate desc ";
                    break;
                case "price":
                    orderby = "LowestSalePrice ";
                    break;
                default:
                    orderby = "ProductId desc";
                    break;
            }
            DataSet ds = dal.GetProSalesList(cid, regionId, startIndex, endIndex, orderby);
            return GroupBuyToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获取团购Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ProductInfo GetGroupBuyModel(int id)
        {
            DataSet ds = dal.GetGroupBuyModel(id);
            return GroupBuyToList(ds.Tables[0]).Count > 0 ? GroupBuyToList(ds.Tables[0])[0] : null;
        }
        /// <summary>
        /// 团购商品Model化
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GroupBuyToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductInfo();

                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                    {
                        model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                    {
                        model.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                    }

                    if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    if (dt.Rows[n]["SaleStatus"] != null && dt.Rows[n]["SaleStatus"].ToString() != "")
                    {
                        model.SaleStatus = int.Parse(dt.Rows[n]["SaleStatus"].ToString());
                    }
                    if (dt.Rows[n]["AddedDate"] != null && dt.Rows[n]["AddedDate"].ToString() != "")
                    {
                        model.AddedDate = DateTime.Parse(dt.Rows[n]["AddedDate"].ToString());
                    }

                    if (dt.Rows[n]["SaleCounts"] != null && dt.Rows[n]["SaleCounts"].ToString() != "")
                    {
                        model.SaleCounts = int.Parse(dt.Rows[n]["SaleCounts"].ToString());
                    }
                    if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                    {
                        model.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                    }
                    if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                    {
                        model.LowestSalePrice = decimal.Parse(dt.Rows[n]["LowestSalePrice"].ToString());
                    }
                    if (dt.Rows[n]["Points"] != null && dt.Rows[n]["Points"].ToString() != "")
                    {
                        model.Points = decimal.Parse(dt.Rows[n]["Points"].ToString());
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null)
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["Tags"] != null && dt.Rows[n]["Tags"].ToString() != "")
                    {
                        model.Tags = dt.Rows[n]["Tags"].ToString();
                    }
                    if (dt.Rows[n]["SeoUrl"] != null && dt.Rows[n]["SeoUrl"].ToString() != "")
                    {
                        model.SeoUrl = dt.Rows[n]["SeoUrl"].ToString();
                    }

                    if (dt.Columns.Contains("PromotionType"))
                    {
                        if (dt.Rows[n]["PromotionType"] != null && dt.Rows[n]["PromotionType"].ToString() != "")
                        {
                            model.PromotionType = int.Parse(dt.Rows[n]["PromotionType"].ToString());
                        }
                    }

                    #region 团购部分Model

                    if (dt.Rows[n]["GroupBuyId"] != null && dt.Rows[n]["GroupBuyId"].ToString() != "")
                    {
                        model.GroupBuy.GroupBuyId = int.Parse(dt.Rows[n]["GroupBuyId"].ToString());
                    }
                    if (dt.Rows[n]["Sequence"] != null && dt.Rows[n]["Sequence"].ToString() != "")
                    {
                        model.GroupBuy.Sequence = int.Parse(dt.Rows[n]["Sequence"].ToString());
                    }
                    if (dt.Rows[n]["FinePrice"] != null && dt.Rows[n]["FinePrice"].ToString() != "")
                    {
                        model.GroupBuy.FinePrice = decimal.Parse(dt.Rows[n]["FinePrice"].ToString());
                    }
                    if (dt.Rows[n]["StartDate"] != null && dt.Rows[n]["StartDate"].ToString() != "")
                    {
                        model.GroupBuy.StartDate = DateTime.Parse(dt.Rows[n]["StartDate"].ToString());
                    }
                    if (dt.Rows[n]["EndDate"] != null && dt.Rows[n]["EndDate"].ToString() != "")
                    {
                        model.GroupBuy.EndDate = DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
                    }
                    if (dt.Rows[n]["MaxCount"] != null && dt.Rows[n]["MaxCount"].ToString() != "")
                    {
                        model.GroupBuy.MaxCount = int.Parse(dt.Rows[n]["MaxCount"].ToString());
                    }
                    if (dt.Rows[n]["GroupCount"] != null && dt.Rows[n]["GroupCount"].ToString() != "")
                    {
                        model.GroupBuy.GroupCount = int.Parse(dt.Rows[n]["GroupCount"].ToString());
                    }
                    if (dt.Rows[n]["BuyCount"] != null && dt.Rows[n]["BuyCount"].ToString() != "")
                    {
                        model.GroupBuy.BuyCount = int.Parse(dt.Rows[n]["BuyCount"].ToString());
                    }
                    if (dt.Rows[n]["Price"] != null && dt.Rows[n]["Price"].ToString() != "")
                    {
                        model.GroupBuy.Price = decimal.Parse(dt.Rows[n]["Price"].ToString());
                    }
                    if (dt.Rows[n]["Status"] != null && dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.GroupBuy.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["BuyDesc"] != null)
                    {
                        model.GroupBuy.Description = dt.Rows[n]["BuyDesc"].ToString();
                    }
                    if (dt.Rows[n]["GroupBuyImage"] != null)
                    {
                        model.GroupBuy.GroupBuyImage = dt.Rows[n]["GroupBuyImage"].ToString();
                    }
                    #endregion
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        public int GetProductStatus(long productId)
        {
            return dal.GetProductStatus(productId);
        }
        #endregion

        /// <summary>
        /// 获取供应商商品数量
        /// </summary>
        /// <param name="Cid">分类</param>
        /// <param name="supplierId">供应商ID</param>
        /// <param name="keyword">关键词</param>
        /// <param name="priceRange">价格区间</param>
        /// <returns></returns>
        public int GetSuppProductsCount(int Cid, int supplierId, string keyword, string priceRange)
        {
            return dal.GetSuppProductsCount(Cid, supplierId, keyword, priceRange);
        }

        /// <summary>
        /// 根据条件获取供应商商品
        /// </summary>
        /// <param name="Cid">类别ID</param>
        /// <param name="supplierId">供应商id</param>
        /// <param name="keyword">关键词</param>
        /// <param name="priceRange">价格区间</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetSuppProductsList(int Cid, int supplierId, string keyword, string priceRange, string orderby, int startIndex, int endIndex)
        {
            return dal.GetSuppProductsList(Cid, supplierId, keyword, priceRange, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="Cid">类别ID</param>
        /// <param name="supplierId">供应商id</param>
        /// <param name="keyword">关键词</param>
        /// <param name="priceRange">价格区间</param>
        /// <param name="mod">排序</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetSuppProductsListEx(int Cid, int supplierId, string keyword, string priceRange, string mod, int startIndex, int endIndex)
        {
            switch (mod)
            {
                case "hot":
                    mod = " SaleCounts DESC ";
                    break;
                case "new":
                    mod = "AddedDate desc ";
                    break;
                case "pricedesc":
                    mod = "LowestSalePrice  desc";
                    break;
                default:
                    mod = null;
                    break;
            }
            DataSet ds = dal.GetSuppProductsList(Cid, supplierId, keyword, priceRange, mod, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="top"></param>
        /// <param name="Cid"></param>
        /// <param name="supplierId"></param>
        /// <param name="mod"></param>
        /// <param name="keyword"></param>
        /// <param name="priceRange"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetSuppProductsList(int top, int Cid, int supplierId, string mod, string keyword, string priceRange)
        {
            switch (mod)
            {
                case "hot":
                    mod = " SaleCounts DESC ";
                    break;
                case "new":
                    mod = "AddedDate desc ";
                    break;
                case "pricedesc":
                    mod = "LowestSalePrice  desc";
                    break;
                default:
                    mod = null;
                    break;
            }
            DataSet ds = dal.GetSuppProductsList(top, Cid, supplierId, mod, keyword, priceRange);
            if (DataSetTools.DataSetIsNull(ds)) return null;
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateThumbnail(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "ProductsModel-" + model.ProductId.ToString();
                if (RedisBase.Item_Get<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey) != null)
                {

                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.ProductInfo>(CacheKey, model);
                }
            }

            return dal.UpdateThumbnail(model);
        }


        /// <summary>
        /// 获取需要静态化的商品数据(或者图片重新生成)
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<int> GetListToReGen(string strWhere)
        {
            DataSet ds = dal.GetListToReGen(strWhere);
            List<int> PhotoIdList = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["ProductID"] != null && ds.Tables[0].Rows[n]["ProductId"].ToString() != "")
                    {
                        PhotoIdList.Add(int.Parse(ds.Tables[0].Rows[n]["ProductId"].ToString()));
                    }
                }
            }
            return PhotoIdList;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Model.Shop.Products.ProductInfo> GetListByPage(Model.Shop.Products.ProductInfo model, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE '%{0}%' ", model.ProductName);
            }
            if (model.CategoryId > 0)
            {
                strWhere.AppendFormat("AND PC.CategoryPath LIKE '{0}%' ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.SearchProductCategories))
            {
                strWhere.AppendFormat(" AND PC.CategoryId IN ( {0} ) ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductCode))
            {
                strWhere.AppendFormat("AND SKU.SKU LIKE '%{0}%' ", model.ProductCode);
            }
            if (model.SupplierId > 0)
            {
                strWhere.AppendFormat("AND P.SupplierId = {0} ", model.SupplierId);
            }
            if (model.SuppCategoryId > 0)
            {
                strWhere.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_SuppProductCategories WHERE  ProductId =P.ProductId  ");
                strWhere.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_SupplierCategories WHERE CategoryId = {0}  ) + '|%' ", model.SuppCategoryId);
                strWhere.AppendFormat(" OR Shop_SuppProductCategories.CategoryId = {0}))", model.SuppCategoryId);
            }
            strWhere.AppendFormat(" AND P.SaleStatus= {0}  ", model.SaleStatus);
            return null; //dal.GetListByPage(strWhere.ToString(), orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetProdRecordCount(string strWhere)
        {
            return dal.GetProdRecordCount(strWhere);
        }

        /// <summary>
        /// 商品数据分页列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProdListByPage(Model.Shop.Products.ProductInfo model, int startIndex, int endIndex, out int toalCount)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" P.SaleStatus= {0}  ", model.SaleStatus);
            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE '%{0}%' ", InjectionFilter.SqlFilter(model.ProductName));
            }
            if (model.CategoryId > 0)
            {
                strWhere.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =P.ProductId  ");
                strWhere.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", model.CategoryId);
                strWhere.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductCode))
            {
                strWhere.AppendFormat(" AND ProductCode  LIKE '%{0}%' ", InjectionFilter.SqlFilter(model.ProductCode));
            }
            if (!string.IsNullOrWhiteSpace(model.ProductCode))
            {
                strWhere.AppendFormat(" AND EXISTS(SELECT * FROM  Shop_SKUs WHERE ProductId=p.ProductId and SKU  LIKE '%{0}%' )", InjectionFilter.SqlFilter(model.ShortDescription));
            }
            if (model.SupplierId > 0)
            {
                strWhere.AppendFormat("AND P.SupplierId = {0} ", model.SupplierId);
            }
            if (model.SuppCategoryId > 0)
            {
                strWhere.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_SuppProductCategories WHERE  ProductId =P.ProductId  ");
                strWhere.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_SupplierCategories WHERE CategoryId = {0}  ) + '|%' ", model.SuppCategoryId);
                strWhere.AppendFormat(" OR Shop_SuppProductCategories.CategoryId = {0}))", model.SuppCategoryId);
            }
            toalCount = dal.GetProdRecordCount(strWhere.ToString());
            DataSet ds = dal.GetProdListByPage(strWhere.ToString(), "", startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds)) return null;
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            DataTable dt = ds.Tables[0];
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Supplier.SupplierCategories suppcateBll = new Supplier.SupplierCategories();
                Maticsoft.Model.Shop.Products.ProductInfo prodmodel;
                for (int n = 0; n < rowsCount; n++)
                {
                    prodmodel = new Model.Shop.Products.ProductInfo();
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        prodmodel.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        prodmodel.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["ProductCode"] != null && dt.Rows[n]["ProductCode"].ToString() != "")
                    {
                        prodmodel.ProductCode = dt.Rows[n]["ProductCode"].ToString();
                    }
                    if (dt.Rows[n]["AddedDate"] != null && dt.Rows[n]["AddedDate"].ToString() != "")
                    {
                        prodmodel.AddedDate = DateTime.Parse(dt.Rows[n]["AddedDate"].ToString());
                    }
                    if (dt.Rows[n]["VistiCounts"] != null && dt.Rows[n]["VistiCounts"].ToString() != "")
                    {
                        prodmodel.VistiCounts = int.Parse(dt.Rows[n]["VistiCounts"].ToString());
                    }
                    if (dt.Rows[n]["SaleCounts"] != null && dt.Rows[n]["SaleCounts"].ToString() != "")
                    {
                        prodmodel.SaleCounts = int.Parse(dt.Rows[n]["SaleCounts"].ToString());
                    }
                    if (dt.Rows[n]["SaleStatus"] != null && dt.Rows[n]["SaleStatus"].ToString() != "")
                    {
                        prodmodel.SaleStatus = int.Parse(dt.Rows[n]["SaleStatus"].ToString());
                    }
                    if (dt.Rows[n]["MarketPrice"] != null && dt.Rows[n]["MarketPrice"].ToString() != "")
                    {
                        prodmodel.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                    }
                    if (dt.Rows[n]["LowestSalePrice"] != null && dt.Rows[n]["LowestSalePrice"].ToString() != "")
                    {
                        prodmodel.LowestSalePrice = decimal.Parse(dt.Rows[n]["LowestSalePrice"].ToString());
                    }
                    if (dt.Rows[n]["PenetrationStatus"] != null && dt.Rows[n]["PenetrationStatus"].ToString() != "")
                    {
                        prodmodel.PenetrationStatus = int.Parse(dt.Rows[n]["PenetrationStatus"].ToString());
                    }
                    if (dt.Rows[n]["MainCategoryPath"] != null && dt.Rows[n]["MainCategoryPath"].ToString() != "")
                    {
                        prodmodel.MainCategoryPath = dt.Rows[n]["MainCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["ExtendCategoryPath"] != null && dt.Rows[n]["ExtendCategoryPath"].ToString() != "")
                    {
                        prodmodel.ExtendCategoryPath = dt.Rows[n]["ExtendCategoryPath"].ToString();
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        prodmodel.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["supplierid"] != null && dt.Rows[n]["supplierid"].ToString() != "")
                    {
                        prodmodel.SupplierId = Convert.ToInt32(dt.Rows[n]["supplierid"].ToString());
                    }

                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        prodmodel.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    else
                    {
                        prodmodel.ThumbnailUrl1 = "/Upload/Shop/Images/ProductThumbs/none.jpg";
                    }
                    prodmodel.CategoryName = ProductCategories(prodmodel.ProductId);
                    prodmodel.SuppCategoryName = suppcateBll.ProductSuppCategories(prodmodel.ProductId, model.SupplierId);
                    prodmodel.StockNum = StockNum(prodmodel.ProductId);
                    modelList.Add(prodmodel);
                }
            }
            return modelList;
        }

        CategoryInfo manage = new CategoryInfo();
        /// <summary>
        /// 获取商品所在商城分类信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private string ProductCategories(long productId)
        {
            List<Model.Shop.Products.ProductCategories> list = new ProductCategories().GetModelList(productId);
            StringBuilder strName = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (Model.Shop.Products.ProductCategories productCategoriese in list)
                {
                    strName.Append(manage.GetFullNameByCache(productCategoriese.CategoryId));
                    strName.Append("</br>");
                }
            }
            return strName.ToString();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Shop.Products.ProductInfo> GetModelListByIdList(string idlist)
        {
            if (!String.IsNullOrWhiteSpace(idlist))
            {
                DataSet ds = dal.GetList(0, string.Format(" ProductId in ( {0} ) ", idlist), " ProductId desc ");
                return DataTableToList(ds.Tables[0]);
            }
            return null;
        }

        /// <summary>
        /// 获取商家推荐的商品列表
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="type"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet GetSuppRecList(int supplierId, int type, string orderby)
        {
            return dal.GetSuppRecList(supplierId, type, orderby);
        }
        public List<Model.Shop.Products.ProductInfo> GetSuppRecList(int supplierId, int type)
        {
            DataSet ds = dal.GetSuppRecList(supplierId, type, "   s.StationId  DESC ");
            return DataTableToList(ds.Tables[0]);
        }

        //public List<Maticsoft.Model.Shop.Products.ProductInfo> GetModelList(string strWhere)
        //{
        //    DataSet ds = dal.GetList(strWhere);
        //    return DataTableToList(ds.Tables[0]);
        //}
        public DataSet GetProList(string strWhere)
        {
            return dal.GetProList(strWhere);
        }
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProModelList(string strWhere)
        {
            DataSet ds = dal.GetProList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        public string GetProductUrl(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            if (model == null)
            {
                return "";
            }
            int rule = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("Shop_Static_NameRule");
            if (rule == 0)
            {
                return model.ProductId.ToString();
            }
            if (rule == 1)
            {
                return Common.PinyinHelper.GetPinyin(model.ProductName) + "_" + model.ProductId;
            }
            if (rule == 2)
            {
                return String.IsNullOrWhiteSpace(model.SeoUrl) ? model.ProductId.ToString() : model.SeoUrl + "_" + model.ProductId;
            }
            return model.ProductId.ToString();
        }
        public int GetGroupBuyCount(int cid, int regionid)
        {
            return dal.GetCount(cid, regionid);
        }

        /// <summary>
        /// 商品对比
        /// </summary>
        /// <param name="pids"></param>
        /// <returns></returns>
        public Dictionary<string, Model.Shop.Products.AttributeInfo> GetProdValueList(long[] pids)
        {
            AttributeInfo attributeManage = new Maticsoft.BLL.Shop.Products.AttributeInfo();
            Dictionary<string, Model.Shop.Products.AttributeInfo> dic = new Dictionary<string, Model.Shop.Products.AttributeInfo>();
            Model.Shop.Products.ProductInfo pInfo = new Model.Shop.Products.ProductInfo();
            List<Model.Shop.Products.AttributeInfo> pAttrList = new List<Model.Shop.Products.AttributeInfo>();//商品属性
            //Dictionary<long, Model.Shop.Products.AttributeInfo> data = new Dictionary<long, Model.Shop.Products.AttributeInfo>();  
            //for (int i = 0; i < pids.Length; i++)
            //{
            //    data.Add(pids[i],null); 
            //}
            int index = 0;
            //foreach (KeyValuePair<long, Model.Shop.Products.AttributeInfo> kvp in  data)
            //{
            foreach (long kvp in pids)
            {
                pInfo = GetModelByCache(kvp);//根据商品id 读取单个商品基本信息// 
                pAttrList = attributeManage.GetAttributeInfoListByProductId(pids[index]);//读取单个商品的所有属性
                if (dic.ContainsKey("商品图片"))
                {
                    dic["商品图片"].AttributeValues.Add(new Model.Shop.Products.AttributeValue
                    {
                        ValueStr = pInfo.ProductName,
                        ImageUrl = pInfo.ThumbnailUrl1,
                        ValueId = pInfo.ProductId
                    });
                }
                else
                {
                    dic.Add("商品图片", new Model.Shop.Products.AttributeInfo
                    {
                        AttributeName = "商品图片",
                        AttributeValues = new List<Model.Shop.Products.AttributeValue>()
                                {
                                    new Model.Shop.Products.AttributeValue
                                        {
                                            ValueStr = pInfo.ProductName,
                                            ImageUrl = pInfo.ThumbnailUrl1,
                                            ValueId=pInfo.ProductId
                                        }
                                }
                    });
                }
                if (dic.ContainsKey("价格"))
                {
                    dic["价格"].AttributeValues.Add(new Model.Shop.Products.AttributeValue
                    {
                        ValueStr = pInfo.LowestSalePrice.ToString("F"),
                    });
                }
                else
                {
                    dic.Add("价格", new Model.Shop.Products.AttributeInfo
                    {
                        AttributeName = "价格",
                        AttributeValues = new List<Model.Shop.Products.AttributeValue>()
                                {
                                    new Model.Shop.Products.AttributeValue
                                        {
                                            ValueStr = pInfo.LowestSalePrice.ToString("F"),
                                        }
                                }
                    });
                }
                foreach (Model.Shop.Products.AttributeInfo attr in pAttrList)
                {
                    string value = string.Empty;
                    List<string> valueList = new List<string>();
                    for (int k = 0; k < pids.Length; k++)
                    {
                        valueList.Add(string.Empty);
                    }
                    attr.AttributeValues.ForEach(val => value += val.ValueStr + ",");
                    if (!dic.ContainsKey(attr.AttributeName))
                    {
                        dic.Add(attr.AttributeName, new Model.Shop.Products.AttributeInfo
                        {
                            AttributeName = attr.AttributeName,
                            ValueStr = valueList
                        });
                    }
                    dic[attr.AttributeName].ValueStr[index] = value.TrimEnd(',');
                }
                index++;
            }
            return dic;
        }
        public DataSet GetTableHead()
        {
            return dal.GetTableHead();
        }

        /// <summary>
        /// 根据商家id获得是否存在该记录
        /// </summary>
        public bool Exists(int supplierId)
        {
            return dal.Exists(supplierId);
        }
        #region 获取会员体验区上方商品类型
        public DataSet GetShopCountDownCategories()
        {
            return dal.GetShopCountDownCategories();
        }
        #endregion

        #region 获取会员体验产品top
        public DataSet GetShopCountDownProductTop(long ProductId)
        {
            return dal.GetShopCountDownProductTop(ProductId);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新商品Tag标签
        /// </summary>
        /// <param name="categoryId">类型</param>
        /// <returns></returns>
        public int ChangeProductsTag(int categoryId, string Tags, int updatetype)
        {
            return dal.ChangeProductsTag(categoryId, Tags, updatetype);
        }
        /// <summary>
        /// 产品ProductIDList 'a,b,c'
        /// </summary>
        /// <param name="ProductIDList"></param>
        /// <returns></returns>
        public int ChangeProductsTag(string ProductIDList, string Tags, int updatetype)
        {
            return dal.ChangeProductsTag(ProductIDList, Tags, updatetype);
        }
        /// <summary>
        /// 返回一个类型中所有产品
        /// </summary>
        /// <param name="categoryId">产品类型</param>
        /// <param name="SaleStatus">是否上架（0表示下架 1 表示下架 ）</param>
        /// <returns></returns>
        public DataSet GetProductsTagCategories(int categoryId, int SaleStatus)
        {
            return dal.GetProductsTagCategories(categoryId, SaleStatus);
        }
        #endregion

        /// <summary>
        /// 分页获取数据列表 (李永琴2014-11-07 16:51添加此方法)
        /// </summary>
        /// <param name="selectedSkus"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="Floor"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductInfo> GetProductRecListByPageNew(string[] selectedSkus,
                                                                       int startIndex, int endIndex, int Floor = 0)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                return null;
            }
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            if (selectedSkus.Length > 0)
            {
                strWhere.Append("  AND  StationId IN (");
                strWhere.Append(string.Join(",", selectedSkus));
                strWhere.Append(") ");
            }
            DataSet ds = GetListByPageNew(strWhere.ToString(), " sort DESC", startIndex, endIndex, Floor);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 分页获取数据列表(李永琴2014-11-07 16:51添加此方法)
        /// </summary>
        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex, int Floor = 0)
        {
            return dal.GetListByPageNew(strWhere, orderby, startIndex, endIndex, Floor);
        }


        /// <summary>
        /// 商品推荐  0删除商品   1添加商品
        /// </summary>
        /// <returns></returns>
        public bool UpdateRecommend(int ProductId, int Recommend)
        {
            return dal.UpdateRecommend(ProductId, Recommend);
        }

        /// <summary>
        /// 批量处理刷新时间
        /// </summary>
        /// <returns></returns>
        public bool UpdateListDate(string IDlist, string Updatetime)
        {
            string strWhere = string.Format("UpdateTime='{0}'", Updatetime);
            return dal.UpdateList(IDlist, strWhere);
        }

        public bool UpdateListDate2(string IDlist, string Updatetime)
        {
            string strWhere = string.Format("UpdateTime='{0}'", Updatetime);
            return dal.UpdatetimeList(IDlist, strWhere);
        }
        /// <summary>
        /// 批量进口商品状态
        /// </summary>
        /// <returns></returns>
        public bool UpdateListImportPro(string IDlist, int ImportPro)
        {
            string strWhere = string.Format("ImportPro='{0}'", ImportPro);
            return dal.UpdateList(IDlist, strWhere);
        }

        #region 获取活动商品
        /// <summary>
        /// 获取活动商品
        /// </summary>
        /// <param name="GoodTypeID">商品类型</param>
        /// <param name="BrandId">品牌ID</param>
        /// <param name="Floor">楼层</param>
        /// <param name="type">类型（推荐）</param>
        /// <param name="top"></param>
        /// <param name="mod">排序</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsActiveList(int GoodTypeID, int BrandId = 0, int Floor = 0, int type = 0, int top = 0, string mod = "")
        {
            DataSet ds =dal.GetProductsActiveList(GoodTypeID, BrandId, Floor, type, top, mod);
            return DataTableToList(ds.Tables[0]);
            
        }

        #endregion

    }
}