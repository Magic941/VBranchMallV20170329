/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SKUs.cs
// 文件功能描述：
//
// 创建标识： [Ben]  2012/06/11 20:36:34
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;

namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// SKUInfo
    /// </summary>
    public partial class SKUInfo
    {
        private readonly ISKUInfo dal = DAShopProducts.CreateSKUInfo();

        public SKUInfo()
        { }

        #region Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SkuId)
        {
            return dal.Exists(SkuId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long SkuId)
        {
            return dal.Delete(SkuId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string SkuIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(SkuIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.SKUInfo GetModel(long SkuId)
        {
            return dal.GetModel(SkuId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.SKUInfo GetModelByCache(long SkuId)
        {
            string CacheKey = "SKUsModel-" + SkuId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SkuId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.SKUInfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        public List<Maticsoft.Model.Shop.Products.SKUInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.SKUInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUInfo> modelList = new List<Maticsoft.Model.Shop.Products.SKUInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.SKUInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.SKUInfo();
                    if (dt.Rows[n]["SkuId"] != null && dt.Rows[n]["SkuId"].ToString() != "")
                    {
                        model.SkuId = long.Parse(dt.Rows[n]["SkuId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["SKU"] != null && dt.Rows[n]["SKU"].ToString() != "")
                    {
                        model.SKU = dt.Rows[n]["SKU"].ToString();
                    }
                    if (dt.Rows[n]["Weight"] != null && dt.Rows[n]["Weight"].ToString() != "")
                    {
                        model.Weight = int.Parse(dt.Rows[n]["Weight"].ToString());
                    }
                    if (dt.Rows[n]["Stock"] != null && dt.Rows[n]["Stock"].ToString() != "")
                    {
                        model.Stock = int.Parse(dt.Rows[n]["Stock"].ToString());
                    }
                    if (dt.Rows[n]["AlertStock"] != null && dt.Rows[n]["AlertStock"].ToString() != "")
                    {
                        model.AlertStock = int.Parse(dt.Rows[n]["AlertStock"].ToString());
                    }
                    if (dt.Rows[n]["CostPrice"] != null && dt.Rows[n]["CostPrice"].ToString() != "")
                    {
                        model.CostPrice = decimal.Parse(dt.Rows[n]["CostPrice"].ToString());
                    }
                    if (dt.Rows[n]["SalePrice"] != null && dt.Rows[n]["SalePrice"].ToString() != "")
                    {
                        model.SalePrice = decimal.Parse(dt.Rows[n]["SalePrice"].ToString());
                    }
                    if (dt.Rows[n]["Upselling"] != null && dt.Rows[n]["Upselling"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Upselling"].ToString() == "1") || (dt.Rows[n]["Upselling"].ToString().ToLower() == "true"))
                        {
                            model.Upselling = true;
                        }
                        else
                        {
                            model.Upselling = false;
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

#if false    //方案1 临时解决方案
        public Dictionary<long, string> GetSKU4AttrVal(string sku, string productName, string categoryId, int startIndex, int endIndex, out int dataCount)
        {
            Dictionary<long, string> skuAttrValKV = new Dictionary<long, string>();
            StringBuilder where = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sku))
            {
                where.Append("SKU = '");
                where.Append(Common.InjectionFilter.SqlFilter(sku));
                where.Append("'");
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                if (where.Length > 0)
                {
                    where.Append(" AND ");
                }
                where.Append(" MainCategoryPath LIKE '");
                where.Append(Common.InjectionFilter.SqlFilter(categoryId.Trim()));
                where.Append("%'");
            }
            if (!string.IsNullOrWhiteSpace(productName))
            {
                if (where.Length > 0)
                {
                    where.Append(" AND ");
                }
                where.Append(" ProductName LIKE '");
                where.Append(Common.InjectionFilter.SqlFilter(productName.Trim()));
                where.Append("%'");
            }
            DataSet dataSet = dal.GetSKUListByPage(where.ToString(), null, startIndex, endIndex, out dataCount);
            if (Common.DataSetTools.DataSetIsNull(dataSet)) return null;
            long tmpSkuId;
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                tmpSkuId = dataRow.Field<long>("SkuId");
                if (!skuAttrValKV.ContainsKey(tmpSkuId))
                {
                    skuAttrValKV.Add(tmpSkuId, dataRow.Field<string>("ProductName"));
                    skuAttrValKV[tmpSkuId] += "&nbsp;" + dataRow.Field<string>("ValueStr");
                }
                else
                {
                    skuAttrValKV[tmpSkuId] += "、" + dataRow.Field<string>("ValueStr");
                }
            }
            return skuAttrValKV;
        }
#else   //方案2 由于前台未实现暂不使用

        public List<Model.Shop.Products.SKUInfo> GetSKU4AttrVal(string productName, string categoryId,
            string[] selectedSkus, int startIndex, int endIndex, out int dataCount, long productId)
        {
            return GetSKU4AttrVal(null, selectedSkus, productName, categoryId, startIndex, endIndex, out dataCount, productId);
        }

        public List<Model.Shop.Products.SKUInfo> GetSKU4AttrVal(string sku, string[] selectedSkus,
            string productName, string categoryId, int startIndex, int endIndex, out int dataCount, long productId)
        {
            StringBuilder where = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sku))
            {
                where.Append(" and SKU = '");
                where.Append(InjectionFilter.SqlFilter(sku));
                where.Append("'");
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                where.Append("  AND  P.ProductId IN(  SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE CategoryPath  LIKE '");
                where.Append(InjectionFilter.SqlFilter(categoryId.Trim()));
                where.Append("%')");
            }
            if (!string.IsNullOrWhiteSpace(productName))
            {
                where.Append("  AND ProductName LIKE '");
                where.Append(InjectionFilter.SqlFilter(productName.Trim()));
                where.Append("%'");
            }
            if (selectedSkus != null && selectedSkus.Length > 0)
            {
                where.Append("  AND SkuId NOT IN (");
                where.Append(string.Join(",", selectedSkus));
                where.Append(") ");
            }

            return GetSKUInfosData(where.ToString(), null, startIndex, endIndex, out dataCount, productId);
        }

        public List<Model.Shop.Products.SKUInfo> GetSKU4AttrVal(string[] selectedSkus, int startIndex, int endIndex, out int dataCount, long productId)
        {
            if (selectedSkus == null || selectedSkus.Length < 1)
            {
                dataCount = 0;
                return null;
            }
            StringBuilder where = new StringBuilder();
            where.Append("  AND SkuId IN (");
            where.Append(string.Join(",", selectedSkus));
            where.Append(") ");

            return GetSKUInfosData(where.ToString(), null, startIndex, endIndex, out dataCount, productId);
        }

        private List<Model.Shop.Products.SKUInfo> GetSKUInfosData(string where, string orderby, int startIndex, int endIndex, out int dataCount, long productId)
        {
            DataSet dataSet = dal.GetSKUListByPage(where, orderby, startIndex, endIndex, out dataCount, productId);
            List<Model.Shop.Products.SKUInfo> list = GetSkuModelList(dataSet);
            if (list==null || list.Count<1) return null;
            List<Model.Shop.Products.SKUInfo> listPage = new List<Model.Shop.Products.SKUInfo>();
            dataCount = list.Count;
            if (dataCount <= startIndex - 1)
            {
                return list;
            }
            for (int i = startIndex - 1; i < endIndex && i < list.Count; i++)
            {
                listPage.Add(list[i]);
            }

            return listPage;
        }

#endif

        public List<Model.Shop.Products.SKUInfo> GetProductSkuInfo(long productId)
        {
            DataSet ds = dal.PrductsSkuInfo(productId);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ProductSkuDataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        public List<Model.Shop.Products.SKUInfo> GetProductSkuInfo(string strWhere)
        {
            DataSet ds = dal.ProductsSkuInfo(strWhere);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ProductSkuDataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        #region ProductSkuDataTableToList
        public List<Maticsoft.Model.Shop.Products.SKUInfo> ProductSkuDataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUInfo> modelList = new List<Maticsoft.Model.Shop.Products.SKUInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.SKUInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.SKUInfo();
                    if (dt.Rows[n]["SkuId"] != null && dt.Rows[n]["SkuId"].ToString() != "")
                    {
                        model.SkuId = long.Parse(dt.Rows[n]["SkuId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["SKU"] != null && dt.Rows[n]["SKU"].ToString() != "")
                    {
                        model.SKU = dt.Rows[n]["SKU"].ToString();
                    }
                    if (dt.Rows[n]["Weight"] != null && dt.Rows[n]["Weight"].ToString() != "")
                    {
                        model.Weight = int.Parse(dt.Rows[n]["Weight"].ToString());
                    }
                    if (dt.Rows[n]["Stock"] != null && dt.Rows[n]["Stock"].ToString() != "")
                    {
                        model.Stock = int.Parse(dt.Rows[n]["Stock"].ToString());
                    }
                    if (dt.Rows[n]["AlertStock"] != null && dt.Rows[n]["AlertStock"].ToString() != "")
                    {
                        model.AlertStock = int.Parse(dt.Rows[n]["AlertStock"].ToString());
                    }
                    if (dt.Rows[n]["CostPrice"] != null && dt.Rows[n]["CostPrice"].ToString() != "")
                    {
                        model.CostPrice = decimal.Parse(dt.Rows[n]["CostPrice"].ToString());
                    }
                    if (dt.Rows[n]["CostPrice2"] != null && dt.Rows[n]["CostPrice2"].ToString() != "")
                    {
                        model.CostPrice2 = decimal.Parse(dt.Rows[n]["CostPrice2"].ToString());
                    }
                    if (dt.Rows[n]["SalePrice"] != null && dt.Rows[n]["SalePrice"].ToString() != "")
                    {
                        model.SalePrice = decimal.Parse(dt.Rows[n]["SalePrice"].ToString());
                    }
                    if (dt.Rows[n]["Upselling"] != null && dt.Rows[n]["Upselling"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Upselling"].ToString() == "1") || (dt.Rows[n]["Upselling"].ToString().ToLower() == "true"))
                        {
                            model.Upselling = true;
                        }
                        else
                        {
                            model.Upselling = false;
                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

        public  bool Exists(string skuCode, long prductId = 0)
        {
            return dal.Exists(skuCode, prductId);
        }

        public int GetStockById(long productId)
        {
            return dal.GetStockById(productId);
        }

        public int GetStockBySKU(string SKU)
        {
            bool IsOpenAS=  Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock");
            return dal.GetStockBySKU(SKU, IsOpenAS);
        }

        #region GetSKUInfoByProductId
        public ViewModel.Shop.ProductSKUModel GetProductSKUInfoByProductId(long productId)
        {
            ViewModel.Shop.ProductSKUModel productSKUModel = new ViewModel.Shop.ProductSKUModel();

            //获取SKU信息
            productSKUModel.ListSKUInfos = GetProductSkuInfo(productId);
            if (productSKUModel.ListSKUInfos == null || productSKUModel.ListSKUInfos.Count < 1) return productSKUModel;

            //获取SKUItem信息
            productSKUModel.ListSKUItems = GetSKUItemsByProductId(productId);
            if (productSKUModel.ListSKUItems == null || productSKUModel.ListSKUItems.Count < 1) return productSKUModel;

            //将SKUItem数据合并到SKUInfo中
            productSKUModel.ListSKUInfos.ForEach(skuInfo =>
            {
                if (!skuInfo.Upselling) return;
                foreach (Model.Shop.Products.SKUItem item in productSKUModel.ListSKUItems)
                {
                    if (item.SkuId != skuInfo.SkuId) continue;

                    if (skuInfo.SkuItems == null)
                        skuInfo.SkuItems = new List<Model.Shop.Products.SKUItem>();

                    //合并SKUItem / Attribute / AttributeValue
                    //DONE: 处理属性值优先问题 BEN 20130627
                    MergeSKUItem4AV(item);

                    //DONE: 解析为 属性 > 值 数据结构
                    productSKUModel.ListAttrSKUItems.Add(
                        new Model.Shop.Products.AttributeInfo
                        {
                            AttributeId = item.AttributeId,
                            AttributeName = item.AttributeName,
                            DisplaySequence = item.AB_DisplaySequence,
                            UsageMode = item.UsageMode,
                            UseAttributeImage = item.UseAttributeImage,
                            UserDefinedPic = item.UserDefinedPic
                        }, item);

                    skuInfo.SkuItems.Add(item);
                }
            });

            return productSKUModel;
        }


        private void MergeSKUItem4AV(Model.Shop.Products.SKUItem skuItem)
        {
            if (skuItem == null) return;

            //优先SKU值处理, 如SKU未设置, 以AttrValue为主
            if (string.IsNullOrWhiteSpace(skuItem.ValueStr) && !string.IsNullOrWhiteSpace(skuItem.AV_ValueStr))
                skuItem.ValueStr = skuItem.AV_ValueStr;
            if (string.IsNullOrWhiteSpace(skuItem.ImageUrl) && !string.IsNullOrWhiteSpace(skuItem.AV_ImageUrl))
                skuItem.ImageUrl = skuItem.AV_ImageUrl;
        }

        private List<Model.Shop.Products.SKUItem> GetSKUItemsByProductId(long productId)
        {
            SKUItem skuItemManage = new SKUItem();
            List<Model.Shop.Products.SKUItem> list = skuItemManage.GetSKUItemsByProductId(productId);
            return list;
            //BUG: 自动去重BUG, 未修复
            //if (list == null || list.Count < 1) return null;
            //return new SortedSet<Model.Shop.Products.SKUItem>(list,
            //    Maticsoft.Common.ComparerFactroy<Model.Shop.Products.SKUItem>.Create(
            //        x => x.AB_DisplaySequence, x => x.AttributeId, x => x.AV_DisplaySequence));
        }

        #endregion

        public List<Model.Shop.Products.SKUItem> GetSKUItemsBySkuId(long skuId)
        {
            SKUItem skuItemManage = new SKUItem();
            List<Model.Shop.Products.SKUItem> list = skuItemManage.GetSKUItemsBySkuId(skuId);
            //DONE: 处理属性值优先问题 BEN 20130630
            list.ForEach(MergeSKUItem4AV);
            return list;
        }

        public List<Model.Shop.Products.SKUItem> GetSKUItemsBySku(string  sku)
        {
            SKUItem skuItemManage = new SKUItem();
            Maticsoft.Model.Shop.Products.SKUInfo infoModel = GetModelBySKU(sku);
            if (infoModel == null)
            {
                return null;
            }
            List<Model.Shop.Products.SKUItem> list = skuItemManage.GetSKUItemsBySkuId(infoModel.SkuId);
            //DONE: 处理属性值优先问题 BEN 20130630
            list.ForEach(MergeSKUItem4AV);
            return list;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.SKUInfo GetModelBySKU(string sku)
        {
            return dal.GetModelBySKU(sku);
        }

        
        /// <summary>
        /// 根据组合id分页获取组合商品sku数据列表  (没有添加到组合商品中的数据)
        /// </summary>
        /// <param name="acceid"></param>
        /// <param name="productName"></param>
        /// <param name="categoryId"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="dataCount"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.SKUInfo> GetNotAcceSKUListByPage(int acceid, string productName, string categoryId, int startIndex, int endIndex, out int dataCount, long productId)
        {
            StringBuilder where = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                where.Append("  AND P.ProductId IN(  SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE CategoryPath  LIKE '");
                where.Append(InjectionFilter.SqlFilter(categoryId.Trim()));
                where.Append("%')");
            }
            if (!string.IsNullOrWhiteSpace(productName))
            { 
                where.Append("  AND ProductName LIKE '");
                where.Append(InjectionFilter.SqlFilter(productName.Trim()));
                where.Append("%'");
            }
            where.AppendFormat(" AND  S.SKU NOT in ( SELECT SKU  FROM  Shop_AccessoriesValues  WHERE  AccessoriesId={0} ) ", acceid);
            return GetSKUInfosData(where.ToString(), null, startIndex, endIndex, out dataCount, productId);
        }
        /// <summary>
        /// 根据组合id分页获取组合商品sku数据列表  (已经添加到组合商品中的数据)
        /// </summary>
        /// <param name="acceid"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.SKUInfo> GetSKUListByPageAndAcceId(int acceid, int startIndex, int endIndex, out int dataCount)
        {
            StringBuilder where = new StringBuilder();
            where.AppendFormat(" INNER JOIN  Shop_AccessoriesValues AS accValues ON  S.SKU = accValues.SKU  and accValues.AccessoriesId={0} ", acceid);
            return GetSKUInfosData(where.ToString(), null, startIndex, endIndex, out dataCount, 0);
        }
        /// <summary>
        /// 根据商品id获取sku数据列表
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Model.Shop.Products.SKUInfo> GetSKUListByProdId(long productId, int startIndex, int endIndex, out int dataCount)
        {
            StringBuilder where = new StringBuilder();
            where.AppendFormat(" AND P.ProductId={0} ", productId);
            return GetSKUInfosData(where.ToString(), null, startIndex, endIndex, out dataCount, 0);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <remarks>添加组合商品时，判断这个sku是否是自己的</remarks>
        public bool ExistsEx(string SKU, long prductId)
        {
            return dal.ExistsEx(SKU, prductId);
        }

        public DataSet GetListInnerJoinProd(string strWhere)
        {
            return dal.GetListInnerJoinProd(strWhere);
        }

        public List<Model.Shop.Products.SKUInfo> GetListInnerJoinProd(long productId)
        {
            DataSet ds = dal.GetListInnerJoinProd(" s.ProductId= " + productId);
            if (!(ds != null && ds.Tables.Count > 0))
            {
                return null;
            }
            List<Model.Shop.Products.SKUInfo> modelList = new List<Model.Shop.Products.SKUInfo>();
            DataTable dt = ds.Tables[0];
            int rowsCount = dt.Rows.Count; 
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.SKUInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.SKUInfo();
                    if (dt.Rows[n]["SkuId"] != null && dt.Rows[n]["SkuId"].ToString() != "")
                    {
                        model.SkuId = long.Parse(dt.Rows[n]["SkuId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["SKU"] != null && dt.Rows[n]["SKU"].ToString() != "")
                    {
                        model.SKU = dt.Rows[n]["SKU"].ToString();
                    }
                    if (dt.Rows[n]["Weight"] != null && dt.Rows[n]["Weight"].ToString() != "")
                    {
                        model.Weight = int.Parse(dt.Rows[n]["Weight"].ToString());
                    }
                    if (dt.Rows[n]["Stock"] != null && dt.Rows[n]["Stock"].ToString() != "")
                    {
                        model.Stock = int.Parse(dt.Rows[n]["Stock"].ToString());
                    }
                    if (dt.Rows[n]["AlertStock"] != null && dt.Rows[n]["AlertStock"].ToString() != "")
                    {
                        model.AlertStock = int.Parse(dt.Rows[n]["AlertStock"].ToString());
                    }
                    if (dt.Rows[n]["CostPrice"] != null && dt.Rows[n]["CostPrice"].ToString() != "")
                    {
                        model.CostPrice = decimal.Parse(dt.Rows[n]["CostPrice"].ToString());
                    }
                    if (dt.Rows[n]["SalePrice"] != null && dt.Rows[n]["SalePrice"].ToString() != "")
                    {
                        model.SalePrice = decimal.Parse(dt.Rows[n]["SalePrice"].ToString());
                    }
                    if (dt.Rows[n]["Upselling"] != null && dt.Rows[n]["Upselling"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Upselling"].ToString() == "1") || (dt.Rows[n]["Upselling"].ToString().ToLower() == "true"))
                        {
                            model.Upselling = true;
                        }
                        else
                        {
                            model.Upselling = false;
                        }
                    }
                    if (dt.Rows[n]["ProductName"] != null && dt.Rows[n]["ProductName"].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n]["ProductName"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ProductThumbnailUrl= dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    //if (dt.Rows[n]["SKU"] != null && dt.Rows[n]["SKU"].ToString() != "")
                    //{
                    //    model.SKU = dt.Rows[n]["SKU"].ToString();
                    //}
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获取SKU数据列表
        /// </summary>
        public List<Model.Shop.Products.SKUInfo> GetSKUList(string strWhere, int AccessoriesId, string orderby, long productId)
        {
            DataSet dataSet = dal.GetSKUList(strWhere,AccessoriesId, orderby, productId);
            return GetSkuModelList(dataSet);
        }

        public List<Model.Shop.Products.SKUInfo> GetSkuModelList(DataSet ds)
        {
            if (DataSetTools.DataSetIsNull(ds)) return null;
            List<Model.Shop.Products.SKUInfo> list = new List<Model.Shop.Products.SKUInfo>();
            long tmpSkuId;
            Model.Shop.Products.SKUInfo tmpSkuInfo;

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if (dataRow["SkuId"] == DBNull.Value) continue;

                tmpSkuId = dataRow.Field<long>("SkuId");
                tmpSkuInfo = list.Find(info => info.SkuId == tmpSkuId);

                if (tmpSkuInfo == null)
                {
                    tmpSkuInfo = new Model.Shop.Products.SKUInfo();
                    tmpSkuInfo.SkuId = tmpSkuId;
                    tmpSkuInfo.ProductId = dataRow.Field<long>("ProductId");
                    tmpSkuInfo.SKU = dataRow.Field<string>("SKU");
                    tmpSkuInfo.Weight = dataRow.Field<int?>("Weight");
                    tmpSkuInfo.Stock = dataRow.Field<int>("Stock");
                    tmpSkuInfo.AlertStock = dataRow.Field<int>("AlertStock");
                    tmpSkuInfo.CostPrice = dataRow.Field<decimal?>("CostPrice");
                    tmpSkuInfo.SalePrice = dataRow.Field<decimal>("SalePrice");
                    tmpSkuInfo.Upselling = dataRow.Field<bool>("Upselling");
                    tmpSkuInfo.ProductName = dataRow.Field<string>("ProductName");
                    tmpSkuInfo.ProductImageUrl = dataRow.Field<string>("ImageUrl");
                    tmpSkuInfo.ProductThumbnailUrl = dataRow.Field<string>("ThumbnailUrl1");
                    tmpSkuInfo.MarketPrice = dataRow.Field<decimal?>("MarketPrice");
                    if (dataRow.Table.Columns.Contains("SupplierId"))
                    {
                        tmpSkuInfo.SupplierId = dataRow.Field<int>("SupplierId");
                    }
                    list.Add(tmpSkuInfo);
                }
                if (dataRow["AttributeId"] == DBNull.Value) continue;
                tmpSkuInfo.SkuItems.Add(new Model.Shop.Products.SKUItem
                {
                    AttributeId = dataRow.Field<long>("AttributeId"),
                    ValueId = dataRow.Field<long>("ValueId"),
                    ValueStr = dataRow.Field<string>("ValueStr"),
                    ImageUrl = dataRow.Field<string>("ImageUrl")
                });
            }
            return list;
        }

        /// <summary>
        /// 根据组合id获取组合商品sku数据列表  (已经添加到组合商品中的数据)
        /// </summary>
        /// <param name="acceid">组合id</param>
        /// <param name="productid">需要排除掉的商品</param>
        /// <returns></returns>
        public List<Model.Shop.Products.SKUInfo> GetSKUListByAcceId(int acceid,long productid)
        {
           // StringBuilder where = new StringBuilder();
           // where.AppendFormat( " INNER JOIN  Shop_AccessoriesValues AS accValues ON  S.SKU = accValues.SKU  and accValues.AccessoriesId={0} ", acceid);
            return GetSKUList("", acceid,"",productid);
        }
        #endregion NewMethod
    }
}