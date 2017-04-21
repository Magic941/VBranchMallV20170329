/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IProducts.cs
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
using System.Data;
using System.Collections.Generic;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.IDAL.Shop.Products
{
    /// <summary>
    /// 接口层ProductInfo
    /// </summary>
    public interface IProductInfo
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ProductId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        long Add(Maticsoft.Model.Shop.Products.ProductInfo model);

        /// <summary>
        /// 新增商品运费
        /// </summary>
        bool AddFreight(string ProductCode, string SKU, decimal Freight, int ModeId, string Editor);

        /// <summary>
        /// 删除商品运费
        /// </summary>
        bool DeleteFreight(string ProductCode, string SKU);

        /// <summary>
        /// 查询商品运费
        /// </summary>
        DataSet GetFreightList(string ProductCode, string SKU);

        /// <summary>
        /// 查询分页商品运费
        /// </summary>
        DataSet GetFreightListByPage(string Where, Int64? StartIndex, Int64? EndIndex);

        /// <summary>
        /// 更新商品运费
        /// </summary>
        bool UpdateFreight(string ProductCode, string SKU, decimal Freight, int ModeId, string Editor);

        /// <summary>
        /// 查询分页商品列表
        /// </summary>
        DataSet GetProductListByPage(string Where, Int64? StartIndex, Int64? EndIndex);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Products.ProductInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long ProductId);
        bool DeleteList(string ProductIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Products.ProductInfo GetModel(long ProductId);
        Maticsoft.Model.Shop.Products.ProductInfo DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获取符合条件的上架商品列表，并用SKU第一条的成本价替换掉商品的最低价
        /// </summary>
        DataSet GetList2(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, int Floor = 0);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法

        #region 新增的成员方法
        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strSetValue"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strSetValue);
        bool UpdatetimeList(string IDlist, string strSetValue);

        bool UpdateProductName(long productId, string strSetValue);

        DataSet GetListByCategoryIdSaleStatus(string strWhere);

        DataSet GetListByExport(int SaleStatus, string ProductName, int CategoryId, string SKU, int BrandId);

        DataSet SearchProducts(int cateId, Model.Shop.Products.ProductSearch model);
        DataSet GetProductListByCategoryId(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount);


        DataSet GetProductListByCategoryIdEx(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount);

        DataSet GetProductListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId);
        /// <summary>
        /// 商品推荐列表信息
        /// </summary>
        DataSet GetProductCommendListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId, int modeType);

        DataSet GetProductListInfo(string strProductIds);

        string GetProductName(long productId);

        bool ExistsBrands(int BrandId);
        /// <summary>
        /// 得到商品表的结构
        /// </summary>
        DataSet GetTableSchema();
        DataSet GetTableSchemaEx();
        /// <summary>
        /// 根据需要的字段获得相应的数据
        /// </summary>
        DataSet GetList(string strWhere, string DataField);

        /// <summary>
        /// 获取商品所有相关的信息，如分类、供应商、品牌
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetListALL(string strWhere);

        #endregion

        DataSet GetProductInfo(string strWhere);

        DataSet DeleteProducts(string Ids, out int Result);
        DataSet GetSelectedProducts(string groupbuyids);

        DataSet GetRecycleList(string strWhere);
        /// <summary>
        /// 还原所有商品
        /// </summary>
        /// <returns></returns>
        bool RevertAll();
        /// <summary>
        /// 更新商品状态
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="SaleStatus"></param>
        /// <returns></returns>
        bool UpdateStatus(long productId, int SaleStatus);

        bool ChangeProductsCategory(string productIds, int categoryId);

        long StockNum(long productId);

        bool UpdateMarketPrice(long productId, decimal price);

        bool UpdateLowestSalePrice(long productId, decimal price);

        DataSet GetProductRecList(ProductRecType type, int categoryId, int top);

        DataSet GetProductRecList2(int type, int categoryId, int top);

        DataSet GetProductRecListWithOutCatg(ProductRecType type, int Floor, int top);

        /// <summary>
        /// 获取推荐产品信息(不考虑分类  李永琴)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        DataSet GetProductRecListWithOutCatgB(ProductRecType type, int Floor, int top);

        int GetProductRecCount(ProductRecType type, int categoryId);
        DataSet GetProductRanList(int top);

        DataSet GetProductNoGroupBuyList(int categoryId, int supplierId, string pName, int startIndex, int endIndex);
        int GetProductNoGroupBuyCount(int categoryId, string pName, int supplierId);

        DataSet RelatedProductSource(long productId, int top);

        DataSet GetProductsListEx(int Cid, int BrandId, string attrValues, string priceRange,
                                  string mod, int startIndex, int endIndex);

        int GetProductsCountEx(int Cid, int BrandId, string attrValues, string priceRange);

        /// <summary>
        /// 获取分享商品列表
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="type"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        DataSet GetProductsListExShare(int Cid, int BrandId, string attrValues, string priceRange,
                                string mod, int type, int startIndex, int endIndex);
        /// <summary>
        /// 获取分享商品列表总数
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetProductsCountExShare(int Cid, int BrandId, string attrValues, string priceRange, int type);


        #region 李永琴
        int GetProductsCountExShareB(int Cid, int BrandId, string attrValues, string priceRange, int type,string path);

        int GetProductsCountExShareC(int Gtype, int GoodtypeId, string attrValues, string priceRange, int type, string path);

        DataSet GetProductsListExShareB(int Cid, int BrandId, string attrValues, string priceRange,
                                                                string mod, int type, int startIndex, int endIndex,string path);

        DataSet GetProductsListExShareC(int Gtype, int GoodtypeId, string attrValues, string priceRange,
                                                                string mod, int type, int startIndex, int endIndex, string path);
        
        
        #endregion

        int MaxSequence();

        /// <summary>
        /// 根据类别地址 得到该类别下最大顺序值
        /// </summary>
        /// <param name="CategoryPath"></param>
        /// <returns></returns>
        int MaxSequence(string CategoryPath);

        int GetSearchCountEx(int Cid, int BrandId, string keyWord, string priceRange);

        DataSet GetSearchListEx(int Cid, int BrandId, string keyWord, string priceRange,
                                 string mod, int startIndex, int endIndex);

        int GetSearchCountExShare(int Cid, int BrandId, string keyWord, string priceRange);

        DataSet GetSearchListExShare(int Cid, int BrandId, string keyWord, string priceRange,
                                 string mod, int startIndex, int endIndex);


        /// <summary>
        /// 搜索分享商品数据(李永琴)
        /// </summary>
        /// <returns></returns>
        DataSet GetSearchListExShareB(int Cid, int BrandId, string keyword, string priceRange,
                                          string mod, int startIndex, int endIndex,string path);

        int GetSearchCountExShareB(int Cid, int BrandId, string keyWord, string priceRange,string path);

        int GetProductNoRecCount(int categoryId, string pName, int modeType, int supplierId);
        int GetProductNoSetFreeFreightCount(int categoryId, string pName, int supplierId, string pCode);
        DataSet GetProductNoRecList(int categoryId, int supplierId, string pName, int modeType, int startIdex, int endIndex);
        DataSet GetGiftList(int categoryId, int supplierId, string pName, int startIndex, int endIndex, DateTime StartDate, DateTime EndDate);
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
        DataSet GetGiftDistinctList(int categoryId, int supplierId, string pName, int startIndex, int endIndex, DateTime StartDate, DateTime EndDate);
        /// <summary>
        /// 获取消除重复行的促销商品数量
        /// </summary>
        /// <param name="categoryId">类路径</param>
        /// <param name="supplierId">供应商编号</param>
        /// <param name="pName">商品名称</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        int GetGiftDistinctListCount(int categoryId, int supplierId, string pName, DateTime StartDate, DateTime EndDate);
        int GetGiftListCount(int categoryId, string pName, int supplierId, DateTime StartDate, DateTime EndDate);
        DataSet GetProductNoSetFreeFreightList(int categoryId, int supplierId, string pName, string pCode, int startIndex, int endIndex);
        DataSet GetFreeFreightProductList(int FreeType, int supplierId, string pName, string pCode, int categoryId);

        bool Exists(string productCode);

        DataSet GetProductsByCid(int cid);

        int GetProSalesCount();

        int GetGroupBuyCount();

        DataSet GetProSalesList(int startIndex, int endIndex, int type);

        DataSet GetProSaleModel(int id);

        DataSet GetGroupBuyList(int startIndex, int endIndex);

        DataSet GetGroupBuyModel(int id);

        int GetProductStatus(long productId);

        /// <summary>
        /// 获取供应商商品数量
        /// </summary>
        /// <param name="Cid">分类</param>
        /// <param name="supplierId">供应商ID</param>
        /// <param name="keyword">关键词</param>
        /// <param name="priceRange">价格区间</param>
        /// <returns></returns>
        int GetSuppProductsCount(int Cid, int supplierId, string keyword, string priceRange);

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
        DataSet GetSuppProductsList(int Cid, int supplierId, string keyword, string priceRange, string orderby, int startIndex, int endIndex);

        DataSet GetSuppProductsList(int top, int Cid, int supplierId, string orderby, string keyword, string priceRange);

        bool UpdateThumbnail(Maticsoft.Model.Shop.Products.ProductInfo model);

        DataSet GetListToReGen(string strWhere);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetProdRecordCount(string strWhere);

        /// <summary>
        /// 商品数据分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        DataSet GetProdListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 获取商家推荐的商品列表
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="strType"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        DataSet GetSuppRecList(int supplierId, int strType, string orderby);

        DataSet GetProList(string strWhere);
        int GetCount(int cid, int regionId);
        DataSet GetProSalesList(int cid, int regionId, int startIndex, int endIndex, string orderby = "");
        DataSet GetTableHead();
        DataSet GetProductRanListByRec(ProductRecType type, int categoryId, int top);

        /// <summary>
        /// 根据商家id获得是否存在该记录
        /// </summary>
        bool Exists(int supplierId);

        /// <summary>
        ///  获取会员体验区上方商品类型
        /// </summary>
        /// <returns></returns>
        DataSet GetShopCountDownCategories();
        /// <summary>
        /// 会员体验区查询
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="type"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        DataSet GetProSalesList(int startIndex, int endIndex, int type, int CategoryId);

        /// <summary>
        /// 获取会员体验产品top
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        DataSet GetShopCountDownProductTop(long ProductId);
        /// <summary>
        /// 更新商品Tag标签
        /// </summary>
        /// <param name="categoryId">类型</param>
        /// <returns></returns>
        int ChangeProductsTag(int categoryId, string Tags, int updatetype);
        /// <summary>
        /// 产品ProductIDList 'a,b,c'
        /// </summary>
        /// <param name="ProductIDList"></param>
        /// <returns></returns>
        int ChangeProductsTag(string ProductIDList, string Tags, int updatetype);
        /// <summary>
        /// 返回一个类型中所有产品
        /// </summary>
        /// <param name="categoryId">产品类型</param>
        /// <param name="SaleStatus">是否上架（0表示下架 1 表示下架 ）</param>
        /// <returns></returns>
        DataSet GetProductsTagCategories(int categoryId, int SaleStatus);

        /// <summary>
        /// 商品推荐  0删除商品   1添加商品
        /// </summary>
        /// <returns></returns>
        bool UpdateRecommend(int ProductId, int Recommend);



        /// <summary>
        /// 分页获取数据列表(李永琴)
        /// </summary>
        DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex, int Floor = 0);

        #region 于菊新
        /// <summary>
        /// 商品活动分类列表
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        int GetProductsCountExActiveB(int Cid, int BrandId, string attrValues, string priceRange, int type);

        DataSet GetProductsListExActiveB(int Cid, int BrandId, string attrValues, string priceRange,
                                        string mod, int type, int startIndex, int endIndex);
        #endregion

        /// <summary>
        /// 获取活动商品
        /// </summary>
        /// <param name="GoodTypeID"></param>
        /// <param name="BrandId"></param>
        /// <param name="Floor"></param>
        /// <param name="type"></param>
        /// <param name="top"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        DataSet GetProductsActiveList(int GoodTypeID, int BrandId, int Floor, int type, int top, string mod = "");
        


    }
}
