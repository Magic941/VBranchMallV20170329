/**
* ProductService.cs
*
* 功 能： Shop模块-产品相关 多表事务操作类
* 类 名： ProductService
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/22 10:46:33  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Maticsoft.Common;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// Shop模块-产品相关 多表事务操作类
    /// </summary>
    public class ProductService : IProductService
    {
        #region IProductService 成员

        #region 新增产品
        public bool AddProduct(Model.Shop.Products.ProductInfo productInfo, out long ProductId)
        {
            ProductId = 0;
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    object result;
                    try
                    {
                        //添加商品
                        result = DbHelperSQL.GetSingle4Trans(GenerateProductInfo(productInfo), transaction);
                        //获取新增的商品主键
                        productInfo.ProductId = Globals.SafeLong(result.ToString(), -1);

                        ProductId = productInfo.ProductId;

                        //添加产品分类
                        DbHelperSQL.ExecuteSqlTran4Indentity(SaveProductCategories(productInfo), transaction);

                        //添加属性
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateAttributeInfo(productInfo, transaction), transaction);

                        //添加SKU
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateSKUs(productInfo, transaction), transaction);

                        //添加相关商品
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateRelatedProduct(productInfo), transaction);

                        //添加图片
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateImages(productInfo), transaction);

                        //推荐商品
                        if (productInfo.isRec)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 0), transaction);
                        }
                        //最新商品
                        if (productInfo.isNow)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 3), transaction);
                        }
                        //最热商品
                        if (productInfo.isHot)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 1), transaction);
                        }
                        //特价商品
                        if (productInfo.isLowPrice)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 2), transaction);
                        }

                        //添加包装
                        DbHelperSQL.ExecuteSqlTran4Indentity(GeneratePackage(productInfo), transaction);
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region 修改产品
        public bool ModifyProduct(Model.Shop.Products.ProductInfo productInfo)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //删除原有信息
                        DeleteOldProductInfo(productInfo);

                        //更新商品基本信息
                        DbHelperSQL.GetSingle4Trans(UpdateProductInfo(productInfo), transaction);

                        //添加产品分类
                        DbHelperSQL.ExecuteSqlTran4Indentity(SaveProductCategories(productInfo), transaction);

                        //添加属性
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateAttributeInfo(productInfo, transaction), transaction);

                        //添加SKU
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateSKUs(productInfo, transaction), transaction);
 
                        //添加相关商品
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateRelatedProduct(productInfo), transaction);

                        //添加图片
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateImages(productInfo), transaction);

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #endregion IProductService 成员

          #region 新增供应商产品
        public bool AddSuppProduct(Model.Shop.Products.ProductInfo productInfo, out long ProductId)
        {
            ProductId = 0;
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    object result;
                    try
                    {
                        //添加商品
                        result = DbHelperSQL.GetSingle4Trans(GenerateProductInfo(productInfo), transaction);
                        //获取新增的商品主键
                        productInfo.ProductId = Globals.SafeLong(result.ToString(), -1);

                        ProductId = productInfo.ProductId;

                        //添加产品分类
                        DbHelperSQL.ExecuteSqlTran4Indentity(SaveProductCategories(productInfo), transaction);

                        //添加店铺产品分类
                        DbHelperSQL.ExecuteSqlTran4Indentity(SaveSuppProductCategories(productInfo), transaction);

                        //添加属性
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateAttributeInfo(productInfo, transaction), transaction);

                        //添加SKU
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateSKUs(productInfo, transaction), transaction);
 
                        //添加相关商品
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateRelatedProduct(productInfo), transaction);

                        //添加图片
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateImages(productInfo), transaction);

                        //推荐商品
                        if (productInfo.isRec)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 0), transaction);
                        }
                        //最新商品
                        if (productInfo.isNow)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 3), transaction);
                        }
                        //最热商品
                        if (productInfo.isHot)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 1), transaction);
                        }
                        //特价商品
                        if (productInfo.isLowPrice)
                        {
                            DbHelperSQL.GetSingle4Trans(GenerateProductStationModes(productInfo, 2), transaction);
                        }

                        //添加包装
                        DbHelperSQL.ExecuteSqlTran4Indentity(GeneratePackage(productInfo), transaction);
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region 修改供应商产品
        public bool ModifySuppProduct(Model.Shop.Products.ProductInfo productInfo)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //TODO：删除原有信息
                        DeleteOldProductInfo(productInfo);
                        DeleteOldSuppProductCate(productInfo);//删除原有分类

                        //TODO：更新商品基本信息
                        DbHelperSQL.GetSingle4Trans(UpdateProductInfo(productInfo), transaction);

                        //添加产品分类
                        DbHelperSQL.ExecuteSqlTran4Indentity(SaveProductCategories(productInfo), transaction);
                        //添加店铺产品分类

                        DbHelperSQL.ExecuteSqlTran4Indentity(SaveSuppProductCategories(productInfo), transaction);

                        //添加属性
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateAttributeInfo(productInfo, transaction), transaction);

                        //添加SKU
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateSKUs(productInfo, transaction), transaction);
 
                        //添加相关商品
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateRelatedProduct(productInfo), transaction);

                        //添加图片
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateImages(productInfo), transaction);

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

       

        #region 产品重新编辑保存前，删除产品相关联信息
        private void DeleteOldProductInfo(Model.Shop.Products.ProductInfo productInfo)
        {
            SqlParameter[] parameter = {
                                       new SqlParameter("@ProductId",SqlDbType.BigInt,8)
                                       };
            parameter[0].Value = productInfo.ProductId;

            DbHelperSQL.RunProcedure("sp_Shop_DeleteBeforeUpdate", parameter);
        }
        private void DeleteOldSuppProductCate(Model.Shop.Products.ProductInfo productInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SuppProductCategories ");
            strSql.Append(" where ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
            parameters[0].Value = productInfo.ProductId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        #endregion

        #region 产品信息

        private CommandInfo UpdateProductInfo(Model.Shop.Products.ProductInfo productInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_Products SET ");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("subHead=@SubHead,");
            strSql.Append("TypeId=@TypeId,");
            strSql.Append("BrandId=@BrandId,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("ShortDescription=@ShortDescription,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Description=@Description,");
            strSql.Append("Meta_Title=@Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords,");
            strSql.Append("SaleStatus=@SaleStatus,");
            strSql.Append("VistiCounts=@VistiCounts,");
            strSql.Append("SaleCounts=@SaleCounts,");
            strSql.Append("DisplaySequence=@DisplaySequence,");
            strSql.Append("LineId=@LineId,");
            strSql.Append("MarketPrice=@MarketPrice,");
            strSql.Append("LowestSalePrice=@LowestSalePrice,");
            strSql.Append("PenetrationStatus=@PenetrationStatus,");
            strSql.Append("MainCategoryPath=@MainCategoryPath,");
            strSql.Append("ExtendCategoryPath=@ExtendCategoryPath,");
            strSql.Append("HasSKU=@HasSKU,");
            strSql.Append("Points=@Points,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("ThumbnailUrl1=@ThumbnailUrl1,");
            strSql.Append("ThumbnailUrl2=@ThumbnailUrl2,");
            strSql.Append("ThumbnailUrl3=@ThumbnailUrl3,");
            strSql.Append("ThumbnailUrl4=@ThumbnailUrl4,");
            strSql.Append("ThumbnailUrl5=@ThumbnailUrl5,");
            strSql.Append("ThumbnailUrl6=@ThumbnailUrl6,");
            strSql.Append("ThumbnailUrl7=@ThumbnailUrl7,");
            strSql.Append("ThumbnailUrl8=@ThumbnailUrl8,");
            strSql.Append("MaxQuantity=@MaxQuantity,");
            strSql.Append("MinQuantity=@MinQuantity,");
            strSql.Append("Tags=@Tags,");
            strSql.Append("SeoUrl=@SeoUrl,");
            strSql.Append("SeoImageAlt=@SeoImageAlt,");
            strSql.Append("SeoImageTitle=@SeoImageTitle");
            strSql.Append(" WHERE ProductId=@ProductId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@TypeId", SqlDbType.Int,4),
                    new SqlParameter("@BrandId", SqlDbType.Int,4),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
                    new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@RegionId", SqlDbType.Int,4),
                    new SqlParameter("@ShortDescription", SqlDbType.NVarChar,2000),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.NText),
                    new SqlParameter("@Title", SqlDbType.NVarChar,100),
                    new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
                    new SqlParameter("@SaleStatus", SqlDbType.Int,4),
                    new SqlParameter("@VistiCounts", SqlDbType.Int,4),
                    new SqlParameter("@SaleCounts", SqlDbType.Int,4),
                    new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
                    new SqlParameter("@LineId", SqlDbType.Int,4),
                    new SqlParameter("@MarketPrice", SqlDbType.Money,8),
                    new SqlParameter("@LowestSalePrice", SqlDbType.Money,8),
                    new SqlParameter("@PenetrationStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@MainCategoryPath", SqlDbType.NVarChar,256),
                    new SqlParameter("@ExtendCategoryPath", SqlDbType.NVarChar,256),
                    new SqlParameter("@HasSKU", SqlDbType.Bit,1),
                    new SqlParameter("@Points", SqlDbType.Decimal,9),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar,255),
                    new SqlParameter("@MaxQuantity", SqlDbType.Int,4),
                    new SqlParameter("@MinQuantity", SqlDbType.Int,4),
                    new SqlParameter("@Tags", SqlDbType.NVarChar,50),
                    new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
                    new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
                    new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@SubHead", SqlDbType.NVarChar,200),};
            parameters[0].Value = productInfo.CategoryId;
            parameters[1].Value = productInfo.TypeId;
            parameters[2].Value = productInfo.BrandId;
            parameters[3].Value = productInfo.ProductName;
            parameters[4].Value = productInfo.ProductCode;
            parameters[5].Value = productInfo.SupplierId;
            parameters[6].Value = productInfo.RegionId;
            parameters[7].Value = productInfo.ShortDescription;
            parameters[8].Value = productInfo.Unit;
            parameters[9].Value = productInfo.Description;
            parameters[10].Value = productInfo.Meta_Title;
            parameters[11].Value = productInfo.Meta_Description;
            parameters[12].Value = productInfo.Meta_Keywords;
            parameters[13].Value = productInfo.SaleStatus;
            parameters[14].Value = productInfo.VistiCounts;
            parameters[15].Value = productInfo.SaleCounts;
            parameters[16].Value = productInfo.DisplaySequence;
            parameters[17].Value = productInfo.LineId;
            parameters[18].Value = productInfo.MarketPrice;
            parameters[19].Value = productInfo.LowestSalePrice;
            parameters[20].Value = productInfo.PenetrationStatus;
            parameters[21].Value = productInfo.MainCategoryPath;
            parameters[22].Value = productInfo.ExtendCategoryPath;
            parameters[23].Value = productInfo.HasSKU;
            parameters[24].Value = productInfo.Points;
            parameters[25].Value = productInfo.ImageUrl;
            parameters[26].Value = productInfo.ThumbnailUrl1;
            parameters[27].Value = productInfo.ThumbnailUrl2;
            parameters[28].Value = productInfo.ThumbnailUrl3;
            parameters[29].Value = productInfo.ThumbnailUrl4;
            parameters[30].Value = productInfo.ThumbnailUrl5;
            parameters[31].Value = productInfo.ThumbnailUrl6;
            parameters[32].Value = productInfo.ThumbnailUrl7;
            parameters[33].Value = productInfo.ThumbnailUrl8;
            parameters[34].Value = productInfo.MaxQuantity;
            parameters[35].Value = productInfo.MinQuantity;
            parameters[36].Value = productInfo.Tags;
            parameters[37].Value = productInfo.SeoUrl;
            parameters[38].Value = productInfo.SeoImageAlt;
            parameters[39].Value = productInfo.SeoImageTitle;
            parameters[40].Value = productInfo.ProductId;
            parameters[41].Value = productInfo.Subhead;
            return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateProductInfo(Model.Shop.Products.ProductInfo productInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_Products(");
            strSql.Append("CategoryId,TypeId,BrandId,ProductName,Subhead,ProductCode,SupplierId,RegionId,ShortDescription,Unit,");
            strSql.Append("Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,");
            strSql.Append("DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,");
            strSql.Append("ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,");
            strSql.Append("ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle)");
            strSql.Append(" VALUES (");
            strSql.Append("@CategoryId,@TypeId,@BrandId,@ProductName,@Subhead,@ProductCode,@SupplierId,@RegionId,");
            strSql.Append("@ShortDescription,@Unit,@Description,@Title,@Meta_Description,@Meta_Keywords,");
            strSql.Append("@SaleStatus,@AddedDate,@VistiCounts,@SaleCounts,@DisplaySequence,@LineId,@MarketPrice,");
            strSql.Append("@LowestSalePrice,@PenetrationStatus,@MainCategoryPath,@ExtendCategoryPath,@HasSKU,");
            strSql.Append("@Points,@ImageUrl,@ThumbnailUrl1,@ThumbnailUrl2,@ThumbnailUrl3,@ThumbnailUrl4,");
            strSql.Append("@ThumbnailUrl5,@ThumbnailUrl6,@ThumbnailUrl7,@ThumbnailUrl8,@MaxQuantity,@MinQuantity,@Tags,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters =
                            {
                                new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                                new SqlParameter("@TypeId", SqlDbType.Int, 4),
                                new SqlParameter("@BrandId", SqlDbType.Int, 4),
                                new SqlParameter("@ProductName", SqlDbType.NVarChar, 200),
                                new SqlParameter("@Subhead", SqlDbType.NVarChar, 200),
                                new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50),
                                new SqlParameter("@SupplierId", SqlDbType.Int, 4),
                                new SqlParameter("@RegionId", SqlDbType.Int, 4),
                                new SqlParameter("@ShortDescription", SqlDbType.NVarChar, 2000),
                                new SqlParameter("@Unit", SqlDbType.NVarChar, 50),
                                new SqlParameter("@Description", SqlDbType.NText),
                                new SqlParameter("@Title", SqlDbType.NVarChar, 100),
                                new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 1000),
                                new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 1000),
                                new SqlParameter("@SaleStatus", SqlDbType.Int, 4),
                                new SqlParameter("@AddedDate", SqlDbType.DateTime),
                                new SqlParameter("@VistiCounts", SqlDbType.Int, 4),
                                new SqlParameter("@SaleCounts", SqlDbType.Int, 4),
                                new SqlParameter("@DisplaySequence", SqlDbType.Int, 4),
                                new SqlParameter("@LineId", SqlDbType.Int, 4),
                                new SqlParameter("@MarketPrice", SqlDbType.Money, 8),
                                new SqlParameter("@LowestSalePrice", SqlDbType.Money, 8),
                                new SqlParameter("@PenetrationStatus", SqlDbType.SmallInt, 2),
                                new SqlParameter("@MainCategoryPath", SqlDbType.NVarChar, 256),
                                new SqlParameter("@ExtendCategoryPath", SqlDbType.NVarChar, 256),
                                new SqlParameter("@HasSKU", SqlDbType.Bit, 1),
                                new SqlParameter("@Points", SqlDbType.Decimal, 9),
                                new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 255),
                                new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 255),
                                new SqlParameter("@MaxQuantity", SqlDbType.Int, 4),
                                new SqlParameter("@MinQuantity", SqlDbType.Int, 4),
                                new SqlParameter("@Tags", SqlDbType.NVarChar,50),
                                new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
                                new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
                                new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300)};
            parameters[0].Value = productInfo.CategoryId;
            parameters[1].Value = productInfo.TypeId;
            parameters[2].Value = productInfo.BrandId;
            parameters[3].Value = productInfo.ProductName;
            parameters[4].Value = productInfo.Subhead;
            parameters[5].Value = productInfo.ProductCode;
            parameters[6].Value = productInfo.SupplierId;
            parameters[7].Value = productInfo.RegionId;
            parameters[8].Value = productInfo.ShortDescription;
            parameters[9].Value = productInfo.Unit;
            parameters[10].Value = productInfo.Description;
            parameters[11].Value = productInfo.Meta_Title;
            parameters[12].Value = productInfo.Meta_Description;
            parameters[13].Value = productInfo.Meta_Keywords;
            parameters[14].Value = productInfo.SaleStatus;
            parameters[15].Value = productInfo.AddedDate;
            parameters[16].Value = productInfo.VistiCounts;
            parameters[17].Value = productInfo.SaleCounts;
            parameters[18].Value = productInfo.DisplaySequence;
            parameters[19].Value = productInfo.LineId;
            parameters[20].Value = productInfo.MarketPrice;
            parameters[21].Value = productInfo.LowestSalePrice;
            parameters[22].Value = productInfo.PenetrationStatus;
            parameters[23].Value = productInfo.MainCategoryPath;
            parameters[24].Value = productInfo.ExtendCategoryPath;
            parameters[25].Value = productInfo.HasSKU;
            parameters[26].Value = productInfo.Points;
            parameters[27].Value = productInfo.ImageUrl;
            parameters[28].Value = productInfo.ThumbnailUrl1;
            parameters[29].Value = productInfo.ThumbnailUrl2;
            parameters[30].Value = productInfo.ThumbnailUrl3;
            parameters[31].Value = productInfo.ThumbnailUrl4;
            parameters[32].Value = productInfo.ThumbnailUrl5;
            parameters[33].Value = productInfo.ThumbnailUrl6;
            parameters[34].Value = productInfo.ThumbnailUrl7;
            parameters[35].Value = productInfo.ThumbnailUrl8;
            parameters[36].Value = productInfo.MaxQuantity;
            parameters[37].Value = productInfo.MinQuantity;
            parameters[38].Value = productInfo.Tags;
            parameters[39].Value = productInfo.SeoUrl;
            parameters[40].Value = productInfo.SeoImageAlt;
            parameters[41].Value = productInfo.SeoImageTitle;
            return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

        #endregion 产品信息

        #region 属性

        private List<CommandInfo> GenerateAttributeInfo(Model.Shop.Products.ProductInfo productInfo, SqlTransaction transaction)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Model.Shop.Products.AttributeInfo attributeInfo in productInfo.AttributeInfos)
            {
                switch (Globals.SafeEnum<ProductAttributeModel>(
                    attributeInfo.UsageMode.ToString(CultureInfo.InvariantCulture),
                    ProductAttributeModel.None))
                {
                    case ProductAttributeModel.One:
                        list.Add(GenerateAttribute4One(attributeInfo.AttributeValues[0], productInfo.ProductId));
                        break;

                    case ProductAttributeModel.Input:
                        list.Add(GenerateAttribute4Input(attributeInfo.AttributeValues[0], productInfo.ProductId, transaction));
                        break;

                    case ProductAttributeModel.Any:
                        foreach (Model.Shop.Products.AttributeValue attributeValue in attributeInfo.AttributeValues)
                        {
                            list.Add(GenerateAttribute4One(attributeValue, productInfo.ProductId));
                        }
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        private CommandInfo GenerateAttribute4Input(Model.Shop.Products.AttributeValue attributeValue, long productId, SqlTransaction transaction)
        {
            // Insert Input Value
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_AttributeValues(");
            strSql.Append("AttributeId,DisplaySequence,ValueStr,ImageUrl)");
            strSql.Append(" VALUES (");
            strSql.Append("@AttributeId,@DisplaySequence,@ValueStr,@ImageUrl)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
                                            new SqlParameter("@AttributeId", SqlDbType.BigInt, 8),
                                            new SqlParameter("@DisplaySequence", SqlDbType.Int, 4),
                                            new SqlParameter("@ValueStr", SqlDbType.NVarChar, 200),
                                            new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 255)
                                        };
            parameters[0].Value = attributeValue.AttributeId;
            parameters[1].Value = -1;
            parameters[2].Value = attributeValue.ValueStr;
            parameters[3].Value = attributeValue.ImageUrl;

            object obj = DbHelperSQL.GetSingle4Trans(new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows), transaction);
            attributeValue.ValueId = Globals.SafeInt(obj.ToString(), -1);

            return GenerateAttribute4One(attributeValue, productId);
        }

        private CommandInfo GenerateAttribute4One(Model.Shop.Products.AttributeValue attributeValue, long productId)
        {
            // Insert ValueId
            StringBuilder strSql;
            strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_ProductAttributes(");
            strSql.Append("ProductId,AttributeId,ValueId)");
            strSql.Append(" VALUES (");
            strSql.Append("@ProductId,@AttributeId,@ValueId)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                    new SqlParameter("@AttributeId", SqlDbType.BigInt, 8),
                    new SqlParameter("@ValueId", SqlDbType.Int, 4)};
            parameters[0].Value = productId;
            parameters[1].Value = attributeValue.AttributeId;
            parameters[2].Value = attributeValue.ValueId;
            return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

        #endregion 属性

        #region SKU

        private List<CommandInfo> GenerateSKUs(Model.Shop.Products.ProductInfo productInfo, SqlTransaction transaction)
        {
            Dictionary<long, long> specValues = new Dictionary<long, long>();   //Key:ValueId , Value:specId
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Model.Shop.Products.SKUInfo skuInfo in productInfo.SkuInfos)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("INSERT INTO Shop_SKUs(");
                strSql.Append("ProductId,SKU,Weight,Stock,AlertStock,CostPrice,CostPrice2,SalePrice,Upselling)");
                strSql.Append(" VALUES (");
                strSql.Append("@ProductId,@SKU,@Weight,@Stock,@AlertStock,@CostPrice,@CostPrice2,@SalePrice,@Upselling)");
                strSql.Append(";SELECT @RESULT = @@IDENTITY");
                SqlParameter[] parameters = {
                                                new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                                                new SqlParameter("@SKU", SqlDbType.NVarChar, 50),
                                                new SqlParameter("@Weight", SqlDbType.Int, 4),
                                                new SqlParameter("@Stock", SqlDbType.Int, 4),
                                                new SqlParameter("@AlertStock", SqlDbType.Int, 4),
                                                new SqlParameter("@CostPrice", SqlDbType.Money, 8),
                                                new SqlParameter("@CostPrice2", SqlDbType.Money, 8),
                                                new SqlParameter("@SalePrice", SqlDbType.Money, 8),
                                                new SqlParameter("@Upselling", SqlDbType.Bit, 1),
                                                DbHelperSQL.CreateOutParam("@RESULT", SqlDbType.BigInt, 8)//输出主键
                                            };
                parameters[0].Value = productInfo.ProductId;
                parameters[1].Value = skuInfo.SKU;
                parameters[2].Value = skuInfo.Weight;
                parameters[3].Value = skuInfo.Stock;
                parameters[4].Value = skuInfo.AlertStock;
                parameters[5].Value = skuInfo.CostPrice;
                parameters[6].Value = skuInfo.CostPrice2;
                parameters[7].Value = skuInfo.SalePrice;
                parameters[8].Value = skuInfo.Upselling;

                list.Add(new CommandInfo(strSql.ToString(),
                                         parameters, EffentNextType.ExcuteEffectRows));

                foreach (Model.Shop.Products.SKUItem skuItem in skuInfo.SkuItems)
                {
                    if (!specValues.ContainsKey(skuItem.ValueId))
                    {
                        object result = DbHelperSQL.GetSingle4Trans(GenerateSKUItems(skuItem, productInfo), transaction);
                        long specId = Globals.SafeLong(result.ToString(), -1);

                        specValues.Add(skuItem.ValueId, specId);
                    }

                    strSql = new StringBuilder();
                    strSql.Append("INSERT INTO Shop_SKURelation(");
                    strSql.Append("SkuId,SpecId,ProductId)");
                    strSql.Append(" VALUES (");
                    strSql.Append("@SkuId,@SpecId,@ProductId)");
                    parameters = new[]{
                            DbHelperSQL.CreateInputOutParam("@SkuId", SqlDbType.BigInt, 8, null), //输入主键
                            new SqlParameter("@SpecId", SqlDbType.BigInt,8),
                            new SqlParameter("@ProductId", SqlDbType.BigInt,8)
                        };
                    parameters[1].Value = specValues[skuItem.ValueId];
                    parameters[2].Value = productInfo.ProductId;

                    list.Add(new CommandInfo(strSql.ToString(),
                                         parameters, EffentNextType.ExcuteEffectRows));
                }
            }
            return list;
        }

        private CommandInfo CheckSkuItems(Model.Shop.Products.ProductInfo oldProductInfo, Model.Shop.Products.ProductInfo newProductInfo)
        {
            DataTable oldSKUItem = new DataTable(); //DB
            List<Model.Shop.Products.SKUItem> newSKUItem = new List<Model.Shop.Products.SKUItem>(); //页面

            foreach (DataRow row in oldSKUItem.Rows)
            {
                //NULL
                string imgURL = row["ImageUrl"].ToString();
                if (!newSKUItem.Exists(xx => xx.ImageUrl == imgURL))
                {
                    //DEL File 物理删除
                }
            }

            return null;
        }

        private CommandInfo GenerateSKUItems(Model.Shop.Products.SKUItem skuItem, Model.Shop.Products.ProductInfo productInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_SKUItems(");
            strSql.Append("AttributeId,ValueId,ImageUrl,ValueStr,ProductId)");
            strSql.Append(" VALUES (");
            strSql.Append("@AttributeId,@ValueId,@ImageUrl,@ValueStr,@ProductId)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = new[]{
                            new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
                            new SqlParameter("@ValueId", SqlDbType.BigInt,8),
                            new SqlParameter("@ImageUrl", SqlDbType.NVarChar),
                            new SqlParameter("@ValueStr", SqlDbType.NVarChar),
                            new SqlParameter("@ProductId", SqlDbType.BigInt,8)
                        };
            parameters[0].Value = skuItem.AttributeId;
            parameters[1].Value = skuItem.ValueId;
            parameters[2].Value = skuItem.ImageUrl;
            parameters[3].Value = skuItem.ValueStr;
            parameters[4].Value = productInfo.ProductId;
            return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

        #endregion SKU

        #region Package

        private List<CommandInfo> GeneratePackage(Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            if (productInfo.PackageId != null)
            {
                foreach (int PackageId in productInfo.PackageId)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into Shop_ProductPackage(");
                    strSql.Append("ProductId,PackageId)");
                    strSql.Append(" values (");
                    strSql.Append("@ProductId,@PackageId)");
                    SqlParameter[] parameters =
                        {
                            new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                            new SqlParameter("@PackageId", SqlDbType.Int, 4)
                        };
                    parameters[0].Value = productInfo.ProductId;
                    parameters[1].Value = PackageId;
                    list.Add(new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows));
                }
            }
            return list;
        }
        #endregion

        #region 图片

        private List<CommandInfo> GenerateImages(Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Model.Shop.Products.ProductImage productImage in productInfo.ProductImages)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("INSERT INTO Shop_ProductImages(");
                strSql.Append("ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8)");
                strSql.Append(" VALUES (");
                strSql.Append("@ProductId,@ImageUrl,@ThumbnailUrl1,@ThumbnailUrl2,@ThumbnailUrl3,@ThumbnailUrl4,@ThumbnailUrl5,@ThumbnailUrl6,@ThumbnailUrl7,@ThumbnailUrl8)");
                strSql.Append(";SELECT @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar,255),
                    new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar,255)};
                parameters[0].Value = productInfo.ProductId;    //产品主键
                parameters[1].Value = productImage.ImageUrl;
                parameters[2].Value = productImage.ThumbnailUrl1;
                parameters[3].Value = productImage.ThumbnailUrl2;
                parameters[4].Value = productImage.ThumbnailUrl3;
                parameters[5].Value = productImage.ThumbnailUrl4;
                parameters[6].Value = productImage.ThumbnailUrl5;
                parameters[7].Value = productImage.ThumbnailUrl6;
                parameters[8].Value = productImage.ThumbnailUrl7;
                parameters[9].Value = productImage.ThumbnailUrl8;

                list.Add(new CommandInfo(strSql.ToString(),
                                         parameters, EffentNextType.ExcuteEffectRows));
            }
            return list;
        }

        #endregion 图片

       
        //#region 添加配件  
        //private List<CommandInfo> GenerateAccessories(Model.Shop.Products.ProductInfo productInfo)
        //{
        //    List<CommandInfo> list = new List<CommandInfo>();
        //    foreach (Model.Shop.Products.ProductAccessorie productAccess in productInfo.ProductAccessories)
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("INSERT INTO Shop_AccessoriesValues (");
        //        strSql.Append(" ProductAccessoriesId ,ProductAccessoriesSKU)");
        //        strSql.Append(" VALUES  ((SELECT ProductId FROM Shop_SKUs WHERE SkuId=@ProductAccessoriesSKU),@ProductAccessoriesSKU)");
        //        strSql.Append(";SELECT @RESULT = @@IDENTITY");
        //        SqlParameter[] parameters ={
        //                                  new SqlParameter("@ProductAccessoriesSKU",SqlDbType.NVarChar),
        //                                        DbHelperSQL.CreateOutParam("@RESULT", SqlDbType.BigInt, 8)//输出主键
        //                                  };
        //        parameters[0].Value = productAccess.SkuId;

        //        list.Add(new CommandInfo(strSql.ToString(),
        //                                 parameters, EffentNextType.ExcuteEffectRows));

        //        strSql = new StringBuilder();
        //        strSql.Append("INSERT INTO Shop_ProductAccessories(");
        //        strSql.Append("ProductId ,AccessoriesValueId ,Name ,MaxQuantity ,MinQuantity ,DiscountType ,DiscountAmount)");
        //        strSql.Append(" VALUES (");
        //        strSql.Append("@ProductId ,@AccessoriesValueId ,@AccessoriesName ,@MaxQuantity ,@MinQuantity ,@DiscountType ,@DiscountAmount)");
        //        SqlParameter[] param ={
        //                             new SqlParameter("@ProductId",SqlDbType.BigInt,8),
        //                            DbHelperSQL.CreateInputOutParam("@AccessoriesValueId", SqlDbType.BigInt, 8, null), //输入主键
        //                             new SqlParameter("@Name",SqlDbType.NVarChar),
        //                             new SqlParameter("@MaxQuantity",SqlDbType.Int),
        //                             new SqlParameter("@MinQuantity",SqlDbType.Int),
        //                             new SqlParameter("@DiscountType",SqlDbType.Int),
        //                             new SqlParameter("@DiscountAmount",SqlDbType.Int)
        //                             };
        //        param[0].Value = productInfo.ProductId;
        //        param[2].Value = productAccess.Name;
        //        param[3].Value = productAccess.MaxQuantity;
        //        param[4].Value = productAccess.MinQuantity;
        //        param[5].Value = productAccess.DiscountType;
        //        param[6].Value = productAccess.DiscountAmount;
        //        list.Add(new CommandInfo(strSql.ToString(), param, EffentNextType.ExcuteEffectRows));
        //    }
        //    return list;
        //}
 
     //   #endregion 添加配件

        #region 相关商品

        private List<CommandInfo> GenerateRelatedProduct(Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            if (productInfo.RelatedProductId==null||productInfo.RelatedProductId.Length == 0) return list;
            foreach (string item in productInfo.RelatedProductId)
            {
                string[] relatedPid = item.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (Globals.SafeInt(relatedPid[1], 0) == 0)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO Shop_RelatedProducts(");
                    strSql.Append(" RelatedId, ProductId )");
                    strSql.Append("VALUES  (");
                    strSql.Append("@RelatedId,@ProductId)");
                    SqlParameter[] parameters = {
                                                        new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                                                        new SqlParameter("@RelatedId", SqlDbType.BigInt, 8)
                                                    };
                    parameters[0].Value = productInfo.ProductId;
                    parameters[1].Value = Globals.SafeLong(relatedPid[0], -1);

                    list.Add(new CommandInfo(strSql.ToString(),
                                             parameters, EffentNextType.ExcuteEffectRows));
                }
                else
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO Shop_RelatedProducts(");
                    strSql.Append(" RelatedId, ProductId )");
                    strSql.Append("VALUES  (");
                    strSql.Append("@RelatedId,@ProductId)");
                    SqlParameter[] parameters = {
                                                        new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                                                        new SqlParameter("@RelatedId", SqlDbType.BigInt, 8)
                                                    };
                    parameters[0].Value = productInfo.ProductId;
                    parameters[1].Value = Globals.SafeLong(relatedPid[0], -1);

                    list.Add(new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows));

                    StringBuilder strSqlRe = new StringBuilder();
                    strSqlRe.Append("INSERT INTO Shop_RelatedProducts(");
                    strSqlRe.Append(" RelatedId, ProductId )");
                    strSqlRe.Append("VALUES  (");
                    strSqlRe.Append("@RelatedId,@ProductId)");
                    SqlParameter[] para = {
                                                        new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                                                        new SqlParameter("@RelatedId", SqlDbType.BigInt, 8)
                                                    };
                    para[0].Value = Globals.SafeLong(relatedPid[0], -1);
                    para[1].Value = productInfo.ProductId;

                    list.Add(new CommandInfo(strSqlRe.ToString(), para, EffentNextType.ExcuteEffectRows));
                }
            }
            return list;
        }

        #endregion 相关商品

        #region 添加产品分类

        private List<CommandInfo> SaveProductCategories(Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (string productCategory in productInfo.Product_Categories)
            {
                if (!string.IsNullOrWhiteSpace(productCategory))
                {
                    string[] categoryArray = productCategory.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    int categoryId = Globals.SafeInt(categoryArray[0], 0);
                    list.Add(GeneratePaoductCategoriesOne(categoryId, productInfo.ProductId, categoryArray[1]));
                }
            }
            return list;
        }

        private CommandInfo GeneratePaoductCategoriesOne(int categoriesId, long productId, string path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_ProductCategories ( CategoryId, ProductId,CategoryPath ) ");
            strSql.Append(" VALUES (");
            strSql.Append("@CategoryId,@ProductId,@CategoryPath)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                    new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                    new SqlParameter("@CategoryPath", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = productId;
            parameters[1].Value = categoriesId;
            parameters[2].Value = path;
            return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

        #endregion 添加产品分类

        #region 添加店铺产品分类

        private List<CommandInfo> SaveSuppProductCategories(Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_SuppProductCategories ( CategoryId, ProductId,CategoryPath ) ");
            strSql.Append(" VALUES (");
            strSql.Append("@CategoryId,@ProductId,@CategoryPath)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                    new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                    new SqlParameter("@CategoryPath", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = productInfo.ProductId;
            parameters[1].Value = productInfo.SuppCategoryId;
            parameters[2].Value = productInfo.SuppCategoryPath;
            list.Add(new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows));
            return list;
        }
        private List<CommandInfo> UpdateSuppProductCategories(Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_SuppProductCategories set ");
            strSql.Append("CategoryPath=@CategoryPath ,");
            strSql.Append(" CategoryId=@CategoryId");
            strSql.Append(" where and ProductId=@ProductId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryPath", SqlDbType.NVarChar,4000),
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8)};
            parameters[0].Value = productInfo.SuppCategoryPath;
            parameters[1].Value = productInfo.SuppCategoryId;
            parameters[2].Value = productInfo.SuppCategoryPath;
            list.Add(new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows));
            return list;
        }

        #endregion 添加产品分类

        #region 产品对比
        public DataSet GetCompareProudctInfo(string ids)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductIDs", SqlDbType.NVarChar)
                    };
            parameters[0].Value = ids;
            return DbHelperSQL.RunProcedure("sp_Shop_CompareProduct", parameters, "ds");
        }

        public DataSet GetCompareProudctBasicInfo(string ids)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductIDs", SqlDbType.NVarChar)
                    };
            parameters[0].Value = ids;
            return DbHelperSQL.RunProcedure("sp_Shop_CompareProductBasicInfo", parameters, "ds");
        }
        #endregion

        #region 产品推荐
        private CommandInfo GenerateProductStationModes(Model.Shop.Products.ProductInfo productInfo, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE Shop_ProductStationModes WHERE ProductId = @ProductId AND [Type] = @Type; ");
            strSql.Append("INSERT INTO Shop_ProductStationModes(");
            strSql.Append("ProductId,DisplaySequence,Type)");
            strSql.Append(" VALUES (");
            strSql.Append("@ProductId,@DisplaySequence,@Type)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@ProductId", SqlDbType.Int, 4),
                    new SqlParameter("@DisplaySequence", SqlDbType.Int, 4),
                    new SqlParameter("@Type", SqlDbType.Int, 4)
                };
            parameters[0].Value = productInfo.ProductId;
            parameters[1].Value = productInfo.ProductId;
            parameters[2].Value = type;
            return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }
        private CommandInfo DelProductStationModes(Model.Shop.Products.ProductInfo productInfo, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE Shop_ProductStationModes WHERE ProductId = @ProductId AND [Type] = @Type; ");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@ProductId", SqlDbType.Int, 4),
                    new SqlParameter("@Type", SqlDbType.Int, 4)
                };
            parameters[0].Value = productInfo.ProductId;
            parameters[1].Value = type;
            return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }
        #endregion
    }
}