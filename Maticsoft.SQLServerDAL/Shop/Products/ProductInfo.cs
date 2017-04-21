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
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:Products
    /// </summary>
    public partial class ProductInfo : IProductInfo
    {
        public ProductInfo()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Products");
            strSql.Append(" where ProductId=@ProductId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt)
            };
            parameters[0].Value = ProductId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Products(");
            strSql.Append("CategoryId,TypeId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle)");
            strSql.Append(" values (");
            strSql.Append("@CategoryId,@TypeId,@BrandId,@ProductName,@ProductCode,@SupplierId,@RegionId,@ShortDescription,@Unit,@Description,@Meta_Title,@Meta_Description,@Meta_Keywords,@SaleStatus,@AddedDate,@VistiCounts,@SaleCounts,@DisplaySequence,@LineId,@MarketPrice,@LowestSalePrice,@PenetrationStatus,@MainCategoryPath,@ExtendCategoryPath,@HasSKU,@Points,@ImageUrl,@ThumbnailUrl1,@ThumbnailUrl2,@ThumbnailUrl3,@ThumbnailUrl4,@ThumbnailUrl5,@ThumbnailUrl6,@ThumbnailUrl7,@ThumbnailUrl8,@MaxQuantity,@MinQuantity,@Tags,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            strSql.Append(";select @@IDENTITY");
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
                    new SqlParameter("@Meta_Title", SqlDbType.NVarChar,100),
                    new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
                    new SqlParameter("@SaleStatus", SqlDbType.Int,4),
                    new SqlParameter("@AddedDate", SqlDbType.DateTime),
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
                    new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.BrandId;
            parameters[3].Value = model.ProductName;
            parameters[4].Value = model.ProductCode;
            parameters[5].Value = model.SupplierId;
            parameters[6].Value = model.RegionId;
            parameters[7].Value = model.ShortDescription;
            parameters[8].Value = model.Unit;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.Meta_Title;
            parameters[11].Value = model.Meta_Description;
            parameters[12].Value = model.Meta_Keywords;
            parameters[13].Value = model.SaleStatus;
            parameters[14].Value = model.AddedDate;
            parameters[15].Value = model.VistiCounts;
            parameters[16].Value = model.SaleCounts;
            parameters[17].Value = model.DisplaySequence;
            parameters[18].Value = model.LineId;
            parameters[19].Value = model.MarketPrice;
            parameters[20].Value = model.LowestSalePrice;
            parameters[21].Value = model.PenetrationStatus;
            parameters[22].Value = model.MainCategoryPath;
            parameters[23].Value = model.ExtendCategoryPath;
            parameters[24].Value = model.HasSKU;
            parameters[25].Value = model.Points;
            parameters[26].Value = model.ImageUrl;
            parameters[27].Value = model.ThumbnailUrl1;
            parameters[28].Value = model.ThumbnailUrl2;
            parameters[29].Value = model.ThumbnailUrl3;
            parameters[30].Value = model.ThumbnailUrl4;
            parameters[31].Value = model.ThumbnailUrl5;
            parameters[32].Value = model.ThumbnailUrl6;
            parameters[33].Value = model.ThumbnailUrl7;
            parameters[34].Value = model.ThumbnailUrl8;
            parameters[35].Value = model.MaxQuantity;
            parameters[36].Value = model.MinQuantity;
            parameters[37].Value = model.Tags;
            parameters[38].Value = model.SeoUrl;
            parameters[39].Value = model.SeoImageAlt;
            parameters[40].Value = model.SeoImageTitle;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// 新增商品运费
        /// </summary>
        /// <param name="ProductCode">商品编号</param>
        /// <param name="SKU">商品规格编号</param>
        /// <param name="Freight">运费</param>
        /// <param name="ModeId">配送方式编号</param>
        /// <param name="Editor">操作员</param>
        /// <returns></returns>

        public bool AddFreight(string ProductCode, string SKU, decimal Freight, int ModeId, string Editor)
        {
            string str_Sql = " INSERT INTO [Shop_ProductsFreight]([ProductCode],[SKU],[Freight],[ModeId],[Eidtor]) VALUES('{0}','{1}',{2},{3},'{4}') ";
            str_Sql = string.Format(str_Sql, ProductCode, SKU, Freight, ModeId, Editor);
            int rows = DbHelperSQL.ExecuteSql(str_Sql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除商品运费
        /// </summary>
        /// <param name="ProductCode">商品编号</param>
        /// <param name="SKU">商品规格编号</param>
        /// <returns></returns>
        public bool DeleteFreight(string ProductCode, string SKU)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ProductsFreight where ProductCode='" + ProductCode + "' ");

            if (!string.IsNullOrWhiteSpace(SKU))
            {
                strSql.Append(" and SKU='" + SKU + "' ");
            }

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询运费
        /// </summary>
        /// <param name="ProductCode">商品编号</param>
        /// <param name="SKU">商品规格编号</param>
        /// <returns></returns>
        public DataSet GetFreightList(string ProductCode, string SKU)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Shop_ProductsFreight where ProductCode='" + ProductCode + "' ");
            if (!string.IsNullOrWhiteSpace(SKU))
            {
                strSql.Append(" and SKU='" + SKU + "' ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询分页商品运费
        /// </summary>
        /// <param name="Where">查询条件（SKU，Freight，ModeId,Eidtor,AddTime,UpdateTime筛选条件需要用x.表示，其他商品字段需要用y.表示）</param>
        /// <param name="StartIndex">开始行</param>
        /// <param name="EndIndex">结束行</param>
        /// <returns></returns>
        public DataSet GetFreightListByPage(string Where, Int64? StartIndex, Int64? EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  * from(select row_number() over(order by T.ProductCode) as rowId,* from (select x.SKU,x.Freight,x.ModeId,x.Eidtor,x.AddTime,x.UpdateTime as xUpdateTime,y.* from Shop_ProductsFreight x left join Shop_Products y on x.ProductCode = y.ProductCode ");
            if (!string.IsNullOrWhiteSpace(Where))
            {
                strSql.Append(" where 1=1 " + Where);
            }
            strSql.Append(" ) T)TT ");
            if (StartIndex != null && EndIndex != null)
            {
                strSql.Append(" where TT.rowId between " + StartIndex + " and " + EndIndex);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新商品运费
        /// </summary>
        /// <param name="ProductCode">商品编号</param>
        /// <param name="SKU">商品规格编号</param>
        /// <param name="Freight">运费</param>
        /// <param name="ModeId">配送方式编号</param>
        /// <param name="Editor">操作员</param>
        /// <returns></returns>
        public bool UpdateFreight(string ProductCode, string SKU, decimal Freight, int ModeId, string Editor)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE [Shop_ProductsFreight] SET [Freight] = " + Freight + ",[ModeId] = " + ModeId + " ,[UpdateTime]='" + DateTime.Now + "',[Eidtor]='" + Editor + "' WHERE ProductCode='" + ProductCode + "' ");

            if (!string.IsNullOrWhiteSpace(SKU))
            {
                strSql.Append(" and SKU='" + SKU + "' ");
            }

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetProductListByPage(string Where, Int64? StartIndex, Int64? EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select * from ( select ROW_NUMBER() over (order by ProductId) Row,  * from (select * from dbo.Shop_Products  where 1=1 ");
            if (!string.IsNullOrWhiteSpace(Where))
            {
                strSql.Append(Where);
            }
            strSql.Append(" ) T  ) TT where 1=1 ");
            if (StartIndex != null && EndIndex != null)
            {
                strSql.Append(" and TT.Row between " + StartIndex + " and " + EndIndex);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set ");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("TypeId=@TypeId,");
            strSql.Append("BrandId=@BrandId,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("ShortDescription=@ShortDescription,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Description=@Description,");
            strSql.Append("Meta_Title=@Meta_Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords,");
            strSql.Append("SaleStatus=@SaleStatus,");
            strSql.Append("AddedDate=@AddedDate,");
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
            strSql.Append("Subhead=@Subhead");
            strSql.Append(" where ProductId=@ProductId");
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
                    new SqlParameter("@Meta_Title", SqlDbType.NVarChar,100),
                    new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
                    new SqlParameter("@SaleStatus", SqlDbType.Int,4),
                    new SqlParameter("@AddedDate", SqlDbType.DateTime),
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
                    new SqlParameter("@Subhead",SqlDbType.NVarChar,200)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.BrandId;
            parameters[3].Value = model.ProductName;
            parameters[4].Value = model.ProductCode;
            parameters[5].Value = model.SupplierId;
            parameters[6].Value = model.RegionId;
            parameters[7].Value = model.ShortDescription;
            parameters[8].Value = model.Unit;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.Meta_Title;
            parameters[11].Value = model.Meta_Description;
            parameters[12].Value = model.Meta_Keywords;
            parameters[13].Value = model.SaleStatus;
            parameters[14].Value = model.AddedDate;
            parameters[15].Value = model.VistiCounts;
            parameters[16].Value = model.SaleCounts;
            parameters[17].Value = model.DisplaySequence;
            parameters[18].Value = model.LineId;
            parameters[19].Value = model.MarketPrice;
            parameters[20].Value = model.LowestSalePrice;
            parameters[21].Value = model.PenetrationStatus;
            parameters[22].Value = model.MainCategoryPath;
            parameters[23].Value = model.ExtendCategoryPath;
            parameters[24].Value = model.HasSKU;
            parameters[25].Value = model.Points;
            parameters[26].Value = model.ImageUrl;
            parameters[27].Value = model.ThumbnailUrl1;
            parameters[28].Value = model.ThumbnailUrl2;
            parameters[29].Value = model.ThumbnailUrl3;
            parameters[30].Value = model.ThumbnailUrl4;
            parameters[31].Value = model.ThumbnailUrl5;
            parameters[32].Value = model.ThumbnailUrl6;
            parameters[33].Value = model.ThumbnailUrl7;
            parameters[34].Value = model.ThumbnailUrl8;
            parameters[35].Value = model.MaxQuantity;
            parameters[36].Value = model.MinQuantity;
            parameters[37].Value = model.Tags;
            parameters[38].Value = model.SeoUrl;
            parameters[39].Value = model.SeoImageAlt;
            parameters[40].Value = model.SeoImageTitle;
            parameters[41].Value = model.ProductId;
            parameters[42].Value = model.Subhead;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Products ");
            strSql.Append(" where ProductId=@ProductId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt)
            };
            parameters[0].Value = ProductId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string ProductIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Products ");
            strSql.Append(" where ProductId in (" + ProductIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductInfo GetModel(long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 a.CategoryId,TypeId,ProductId,BrandId,ProductName,Subhead,ProductCode,a.SupplierId,b.Name ,
                            a.RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,
                            DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,Subhead,FalseSaleCount,
                            ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle,b.Slogan,b.Discount,a.FalseSaleCount,a.ImportPro 
                            from Shop_Products a left JOIN dbo.Shop_Suppliers b ON a.SupplierId = b.SupplierId  ");
            strSql.Append(" where ProductId=@ProductId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt)
            };
            parameters[0].Value = ProductId;

            Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.Model.Shop.Products.ProductInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.Model.Shop.Products.ProductInfo();
            if (row != null)
            {
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["TypeId"] != null && row["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["BrandId"] != null && row["BrandId"].ToString() != "")
                {
                    model.BrandId = int.Parse(row["BrandId"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["Subhead"] != null)
                {
                    model.Subhead = row["Subhead"].ToString();
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["RegionId"] != null && row["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["ShortDescription"] != null)
                {
                    model.ShortDescription = row["ShortDescription"].ToString();
                }
                if (row["Unit"] != null)
                {
                    model.Unit = row["Unit"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["Meta_Title"] != null)
                {
                    model.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    model.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    model.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["SaleStatus"] != null && row["SaleStatus"].ToString() != "")
                {
                    model.SaleStatus = int.Parse(row["SaleStatus"].ToString());
                }
                if (row["AddedDate"] != null && row["AddedDate"].ToString() != "")
                {
                    model.AddedDate = DateTime.Parse(row["AddedDate"].ToString());
                }
                if (row["VistiCounts"] != null && row["VistiCounts"].ToString() != "")
                {
                    model.VistiCounts = int.Parse(row["VistiCounts"].ToString());
                }
                if (row["SaleCounts"] != null && row["SaleCounts"].ToString() != "")
                {
                    model.SaleCounts = int.Parse(row["SaleCounts"].ToString());
                }
                if (row["DisplaySequence"] != null && row["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["LineId"] != null && row["LineId"].ToString() != "")
                {
                    model.LineId = int.Parse(row["LineId"].ToString());
                }
                if (row["MarketPrice"] != null && row["MarketPrice"].ToString() != "")
                {
                    model.MarketPrice = decimal.Parse(row["MarketPrice"].ToString());
                }
                if (row["LowestSalePrice"] != null && row["LowestSalePrice"].ToString() != "")
                {
                    model.LowestSalePrice = decimal.Parse(row["LowestSalePrice"].ToString());
                }
                if (row["PenetrationStatus"] != null && row["PenetrationStatus"].ToString() != "")
                {
                    model.PenetrationStatus = int.Parse(row["PenetrationStatus"].ToString());
                }
                if (row["MainCategoryPath"] != null)
                {
                    model.MainCategoryPath = row["MainCategoryPath"].ToString();
                }
                if (row["ExtendCategoryPath"] != null)
                {
                    model.ExtendCategoryPath = row["ExtendCategoryPath"].ToString();
                }
                if (row["HasSKU"] != null && row["HasSKU"].ToString() != "")
                {
                    if ((row["HasSKU"].ToString() == "1") || (row["HasSKU"].ToString().ToLower() == "true"))
                    {
                        model.HasSKU = true;
                    }
                    else
                    {
                        model.HasSKU = false;
                    }
                }
                if (row["Points"] != null && row["Points"].ToString() != "")
                {
                    model.Points = decimal.Parse(row["Points"].ToString());
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["ThumbnailUrl1"] != null)
                {
                    model.ThumbnailUrl1 = row["ThumbnailUrl1"].ToString();
                }
                if (row["ThumbnailUrl2"] != null)
                {
                    model.ThumbnailUrl2 = row["ThumbnailUrl2"].ToString();
                }
                if (row["ThumbnailUrl3"] != null)
                {
                    model.ThumbnailUrl3 = row["ThumbnailUrl3"].ToString();
                }
                if (row["ThumbnailUrl4"] != null)
                {
                    model.ThumbnailUrl4 = row["ThumbnailUrl4"].ToString();
                }
                if (row["ThumbnailUrl5"] != null)
                {
                    model.ThumbnailUrl5 = row["ThumbnailUrl5"].ToString();
                }
                if (row["ThumbnailUrl6"] != null)
                {
                    model.ThumbnailUrl6 = row["ThumbnailUrl6"].ToString();
                }
                if (row["ThumbnailUrl7"] != null)
                {
                    model.ThumbnailUrl7 = row["ThumbnailUrl7"].ToString();
                }
                if (row["ThumbnailUrl8"] != null)
                {
                    model.ThumbnailUrl8 = row["ThumbnailUrl8"].ToString();
                }
                if (row["MaxQuantity"] != null && row["MaxQuantity"].ToString() != "")
                {
                    model.MaxQuantity = int.Parse(row["MaxQuantity"].ToString());
                }
                if (row["MinQuantity"] != null && row["MinQuantity"].ToString() != "")
                {
                    model.MinQuantity = int.Parse(row["MinQuantity"].ToString());
                }
                if (row["Tags"] != null)
                {
                    model.Tags = row["Tags"].ToString();
                }
                if (row["SeoUrl"] != null)
                {
                    model.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["SeoImageAlt"] != null)
                {
                    model.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if (row["SeoImageTitle"] != null)
                {
                    model.SeoImageTitle = row["SeoImageTitle"].ToString();
                }

                if (row["ImportPro"] != null && row["ImportPro"].ToString() != "")
                {
                    model.ImportPro = int.Parse(row["ImportPro"].ToString());
                }

                //if (row["Rebatevalue"]!=null)
                //{
                //    model.Rebatevalue = decimal.Parse(row["Rebatevalue"].ToString());
                //}
                //if (row["DistributionPrice"] != null)
                //{
                //    model.DistributionPrice = decimal.Parse(row["DistributionPrice"].ToString());
                //}
                if (row.Table.Columns.Contains("Name"))
                {
                    if (row["Name"] != null)
                    {
                        model.Name = row["Name"].ToString();
                    }
                }
                model.FalseSaleCount = 0;
                if (row.Table.Columns.Contains("FalseSaleCount"))
                {
                    if (row["FalseSaleCount"] != null && !string.IsNullOrWhiteSpace(row["FalseSaleCount"].ToString().Trim()))
                    {
                        model.FalseSaleCount = int.Parse(row["FalseSaleCount"].ToString());
                    }
                }

            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Subhead, CategoryId,TypeId,ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle,FalseSaleCount,ImportPro ");
            strSql.Append(" FROM Shop_Products ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取符合条件的上架商品列表，并用SKU第一条的成本价替换掉商品的最低价
        /// </summary>
        public DataSet GetList2(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Subhead, CategoryId,TypeId,x.ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,y.CostPrice LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle,FalseSaleCount from Shop_Products x left join (select * from (select ROW_NUMBER() over(partition by ProductId order by SkuId) Row, * from Shop_SKUs) S where S.Row=1) y on x.ProductId = y.ProductId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 专业为商品多分类准备的，索引制作中使用
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListALL(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Subhead, TypeId,sp.ProductId,sp.BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,sp.Description,sp.Meta_Title,sp.Meta_Description,sp.Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,sp.DisplaySequence,LineId,MarketPrice,vp.SalePrice as LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,sp.ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,sp.SeoUrl,sp.SeoImageAlt,sp.SeoImageTitle,vsc.CategoryName,vsc.CategoryId,vsc.Path as CategoryPath ,sb.BrandName,vsc.AllCategoryNames,vsc.CTags,FalseSaleCount ");
            strSql.Append(" FROM Shop_Products  sp");
            strSql.Append(" LEFT JOIN  Shop_ProductCategories  spc ON sp.ProductId=spc.ProductId");
            strSql.Append(" LEFT JOIN  V_ProductCategories  vsc ON vsc.CategoryId=spc.CategoryId");
            strSql.Append(" LEFT JOIN Shop_Brands sb on sp.BrandId=sb.BrandId ");
            strSql.Append(" LEFT JOIN V_ProductLowestSalePrice vp on vp.ProductId=sp.ProductId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" CategoryId,TypeId,ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle,FalseSaleCount ");
            strSql.Append(" FROM Shop_Products ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Shop_Products ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, int Floor = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT  {0} FROM ( ", Floor == 0 ? "*" : "Top 5 *");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            if (Floor != 0)
            {
                strSql.AppendFormat(")AS Row, T.*,p.Floor,p.Sort  from Shop_Products T INNER JOIN dbo.Shop_ProductStationModes p ON t.ProductId=p.ProductId and p.Floor={0}", Floor);
            }
            else
            {
                strSql.Append(")AS Row, T.*  from Shop_Products T ");
                // strSql.Append(")AS Row, T.*,F.Quantity,F.createdate,F.createrid,F.StartDate,F.EndDate  from Shop_Products T RIGHT JOIN dbo.Shop_freefreight F ON T.ProductId=F.ProductId ");
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NVarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.NVarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.NVarChar,1000),
                    };
            parameters[0].Value = "Shop_Products";
            parameters[1].Value = "ProductId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion BasicMethod

        #region NewMethod

        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strSetValue"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strSetValue)
        {
            if (string.IsNullOrWhiteSpace(IDlist))
            {
                return false;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set " + strSetValue);
            strSql.Append(" where ProductId in(" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //public bool UpdateListImportPro(string IDlist, string strSetValue) 
        //{ 

        //}


        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strSetValue"></param>
        /// <returns></returns>
        public bool UpdatetimeList(string IDlist, string strSetValue)
        {
            if (string.IsNullOrWhiteSpace(IDlist))
            {
                return false;
            }
            StringBuilder strsql1 = new StringBuilder();
            strsql1.Append("select ProductId from Shop_WeiXinGroupBuy where GroupBuyId in(" + IDlist + ") ");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set " + strSetValue);
            strSql.Append(" where ProductId in(" + strsql1 + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByCategoryIdSaleStatus(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT CategoryId,TypeId,ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity ");
            strSql.Append(" FROM Shop_Products ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY AddedDate DESC  ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 商品导出数据列表
        /// </summary>
        public DataSet GetListByExport(int SaleStatus, string ProductName, int CategoryId, string SKU, int BrandId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT P.*,S.SKU,P.Subhead, FROM Shop_SKUs S LEFT JOIN Shop_Products P on P.ProductId=S.ProductId  ");
            strSql.Append(" WHERE ");

            strSql.Append(" SaleStatus =" + SaleStatus);
            if (!string.IsNullOrWhiteSpace(ProductName.Trim()))
            {
                strSql.AppendFormat(" and ProductName like '%{0}%' ", ProductName);
            }
            strSql.Append(" and CategoryId =" + CategoryId);
            if (!string.IsNullOrWhiteSpace(SKU.Trim()))
            {
                strSql.AppendFormat(" and SKU like '%{0}%' ", SKU);
            }
            strSql.Append(BrandId == -1 ? string.Empty : " and BrandId =" + BrandId);

            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion NewMethod

        public DataSet SearchProducts(int cateId, Model.Shop.Products.ProductSearch model)
        {
            SqlParameter[] parameters = {
                                        new SqlParameter("@CategoryId",SqlDbType.Int),
                                        new SqlParameter("@BrandId",SqlDbType.Int),
                                        new SqlParameter("@ValueStr1",SqlDbType.Int),
                                        new SqlParameter("@ValueStr2",SqlDbType.Int),
                                        new SqlParameter("@ValueStr3",SqlDbType.Int),
                                        new SqlParameter("@ValueStr4",SqlDbType.Int),
                                        new SqlParameter("@ValueStr5",SqlDbType.Int),
                                        new SqlParameter("@ValueStr6",SqlDbType.Int)
                                        };
            parameters[0].Value = cateId;
            parameters[1].Value = model.Parameter1;
            parameters[2].Value = model.Parameter2;
            parameters[3].Value = model.Parameter3;
            parameters[4].Value = model.Parameter4;
            parameters[5].Value = model.Parameter5;
            parameters[6].Value = model.Parameter6;
            parameters[7].Value = model.Parameter7;
            return DbHelperSQL.RunProcedure("sp_SearchProducts", parameters, "ds");
        }

        public DataSet GetProductListByCategoryId(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex,
                                                  out int dataCount)
        {
            StringBuilder sqlBase = new StringBuilder(" from Shop_Products P ");
            sqlBase.Append(" WHERE EXISTS ( SELECT 1 FROM Shop_ProductCategories PC ");
            sqlBase.Append(" WHERE P.ProductId = PC.ProductId ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                sqlBase.Append(strWhere);
            }
            sqlBase.Append(" ) ");

            object obj = DbHelperSQL.GetSingle("select count(1) " + sqlBase);
            dataCount = obj == null ? 0 : Convert.ToInt32(obj);

            if (dataCount == 0) return null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                strSql.Append("order by " + orderBy);
            }
            else
            {
                strSql.Append("order by P.ProductId desc");
            }
            strSql.Append(")AS Row, P.* ");
            strSql.Append(sqlBase);
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetProductListByCategoryIdEx(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex,
                                          out int dataCount)
        {
            StringBuilder sqlBase = new StringBuilder(" from Shop_Products P JOIN Shop_SKUs SKU  ON  P.ProductId=SKU.ProductId");
            sqlBase.Append(" WHERE EXISTS ( SELECT 1 FROM Shop_ProductCategories PC ");
            sqlBase.Append(" WHERE P.ProductId = PC.ProductId ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                sqlBase.Append(strWhere);
            }
            sqlBase.Append(" ) ");

            object obj = DbHelperSQL.GetSingle("select count(1) " + sqlBase);
            dataCount = obj == null ? 0 : Convert.ToInt32(obj);

            if (dataCount == 0) return null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                strSql.Append("order by " + orderBy);
            }
            else
            {
                strSql.Append("order by P.ProductId desc");
            }
            strSql.Append(")AS Row, P.* ,SKU.SalePrice");
            strSql.Append(sqlBase);
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetProductListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId)
        {
            SqlParameter[] parameters = {
                                        new SqlParameter("@SqlWhere",SqlDbType.NVarChar),
                                        new SqlParameter("@OrderBy",SqlDbType.NVarChar),
                                        new SqlParameter("@StartIndex",SqlDbType.Int),
                                        new SqlParameter("@EndIndex",SqlDbType.Int),
                                        new SqlParameter("@ProductId",SqlDbType.BigInt),
                                            DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)
                                        };
            parameters[0].Value = strWhere;
            parameters[1].Value = orderBy;
            parameters[2].Value = startIndex;
            parameters[3].Value = endIndex;
            parameters[4].Value = productId;
            return DbHelperSQL.RunProcedure("sp_Shop_ProductInfo_Get", parameters, "ds", out dataCount);
        }

        /// <summary>
        /// 商品推荐列表信息
        /// </summary>
        public DataSet GetProductCommendListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId, int modeType)
        {
            SqlParameter[] parameters = {
                                        new SqlParameter("@SqlWhere",SqlDbType.NVarChar),
                                        new SqlParameter("@OrderBy",SqlDbType.NVarChar),
                                        new SqlParameter("@StartIndex",SqlDbType.Int),
                                        new SqlParameter("@EndIndex",SqlDbType.Int),
                                        new SqlParameter("@ProductId",SqlDbType.BigInt),
                                        new SqlParameter("@ModeType",SqlDbType.Int),
                                            DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)
                                        };
            parameters[0].Value = strWhere;
            parameters[1].Value = orderBy;
            parameters[2].Value = startIndex;
            parameters[3].Value = endIndex;
            parameters[4].Value = productId;
            parameters[5].Value = modeType;
            return DbHelperSQL.RunProcedure("sp_Shop_ProductStationModesInfo", parameters, "ds", out dataCount);
        }

        public DataSet GetProductListInfo(string strProductIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * FROM     ");
            strSql.Append("Shop_Products  ");
            strSql.Append("WHERE   SaleStatus = 1  ");
            strSql.AppendFormat("AND ProductId  IN ({0}) ", strProductIds);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public string GetProductName(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ProductName  ");
            strSql.Append("FROM Shop_Products ");
            strSql.AppendFormat("WHERE ProductId={0} ", productId);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());

            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBrands(int BrandId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM Shop_Products");
            strSql.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] parameters = {
                    new SqlParameter("@BrandId", SqlDbType.BigInt)
            };
            parameters[0].Value = BrandId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到表的结构
        /// </summary>
        /// <returns></returns>
        public DataSet GetTableSchema()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from( ");
            strSql.Append("SELECT  * FROM     ");
            strSql.Append("INFORMATION_SCHEMA.COLUMNS ");
            strSql.Append("WHERE   TABLE_Name ='Shop_Products' ");
            strSql.Append("  ) as t");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetTableSchemaEx()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  	u.name + '.' + t.name AS [table], ");
            strSql.Append("            td.value AS [table_desc], ");
            strSql.Append("    		c.name AS [column], ");
            strSql.Append("    		cd.value AS [column_desc] ");
            strSql.Append("FROM    	sysobjects t ");
            strSql.Append("INNER JOIN  sysusers u ");
            strSql.Append("    ON		u.uid = t.uid AND t.name='Shop_Products' ");
            strSql.Append("LEFT OUTER JOIN sys.extended_properties td ");
            strSql.Append("    ON		td.major_id = t.id ");
            strSql.Append("    AND 	td.minor_id = 0 ");
            strSql.Append("    AND		td.name = 'MS_Description'  ");
            strSql.Append("INNER JOIN  syscolumns c ");
            strSql.Append("    ON		c.id = t.id ");
            strSql.Append("LEFT OUTER JOIN sys.extended_properties cd ");
            strSql.Append("    ON		cd.major_id = c.id ");
            strSql.Append("    AND		cd.minor_id = c.colid ");
            strSql.Append("    AND		cd.name = 'MS_Description'  ");
            strSql.Append("WHERE t.type = 'u' ");
            strSql.Append("ORDER BY    t.name, c.colorder     ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetTableHead()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT TOP 0 [CategoryId] as 类别ID,
      ,[TypeId] as 类型ID
      ,[ProductId] as 商品ID
      ,[BrandId] as 品牌Id
      ,[ProductName] as 名称
      ,[ProductCode] as 编码
      ,[SupplierId] as 供应商Id
      ,[RegionId] as 地区Id
      ,[ShortDescription] as 介绍
      ,[Unit] as 单位
      ,[Description] as 描述
      ,[Meta_Title] as SEO_Title
      ,[Meta_Description]  as SEO_Description
      ,[Meta_Keywords]  as SEO_KeyWord
      ,[SaleStatus]  as 状态
      ,[AddedDate]  as 添加日期
      ,[VistiCounts]  as 访问次数
      ,[SaleCounts]  as 售出总数
      ,[Stock]  as 商品库存 
      ,[DisplaySequence]  as 显示顺序
      ,[LineId]  as 生产线
      ,[MarketPrice]  as 市场价
      ,[LowestSalePrice]  as 最低价
      ,[PenetrationStatus]  as 铺货状态
      ,[MainCategoryPath]  as 分类路径
      ,[ExtendCategoryPath]  as 扩展路径
      ,[HasSKU]  as 是否有SKU
      ,[Points]  as 积分
      ,[ImageUrl]  as  图片路径
      ,[ThumbnailUrl1]  as  图片路径1
      ,[ThumbnailUrl2]  as 图片路径2
      ,[ThumbnailUrl3]  as 图片路径3
      ,[ThumbnailUrl4]  as 图片路径4
      ,[ThumbnailUrl5]  as 图片路径5
      ,[ThumbnailUrl6]  as 图片路径6
      ,[ThumbnailUrl7]  as 图片路径7
      ,[ThumbnailUrl8]  as 图片路径8
      ,[MaxQuantity]  as 最大购买量
      ,[MinQuantity]  as 最小购买量
      ,[Tags]  as 标签
      ,[SeoUrl]  as  Url地址优化规则
      ,[SeoImageAlt]  as 图片Alt信息
      ,[SeoImageTitle]  as 图片Title信息
  FROM Shop_Products
");
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string Ids, string DataField)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + DataField + " ");
            strSql.Append(" FROM Shop_Products ");
            if (!string.IsNullOrWhiteSpace(Ids.Trim()))
            {
                strSql.Append(" WHERE ProductId in(" + Ids + ")");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetProductInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT P.CategoryId, TypeId, P.ProductId, BrandId, ProductName, ProductCode, SupplierId, RegionId, ShortDescription, Unit,  Meta_Title, Meta_Description, Meta_Keywords, SaleStatus, AddedDate, VistiCounts, SaleCounts, P.DisplaySequence, LineId, MarketPrice, LowestSalePrice, PenetrationStatus, MainCategoryPath, ExtendCategoryPath, HasSKU, Points, ImageUrl, ThumbnailUrl1, ThumbnailUrl2, ThumbnailUrl3, ThumbnailUrl4, ThumbnailUrl5, ThumbnailUrl6, ThumbnailUrl7, ThumbnailUrl8, MaxQuantity, MinQuantity, Tags, SeoUrl, SeoImageAlt, SeoImageTitle,FalseSaleCount ");
            strSql.Append("FROM Shop_Products P ");
            strSql.Append("LEFT JOIN (SELECT * FROM Shop_ProductCategories )PC ON P.ProductId = PC.ProductId ");
            strSql.Append("LEFT JOIN Shop_SKUs SKU ON PC.ProductId = SKU.ProductId ");
            strSql.Append("LEFT JOIN Shop_ProductStationModes PSM ON SKU.ProductId = PSM.ProductId ");
            strSql.Append("WHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append("ORDER BY AddedDate DESC ");
            return DbHelperSQL.Query((strSql.ToString()));
        }

        public DataSet DeleteProducts(string Ids, out int Result)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductIds ", SqlDbType.NVarChar),
                    DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)};
            parameters[0].Value = Ids;
            DataSet ds = DbHelperSQL.RunProcedure("sp_Shop_DeleteProducts", parameters, "tb", out Result);
            if (Result == 1)
            {
                return ds;
            }
            return null;
        }

        /// <summary>
        /// 获得回收站数据
        /// </summary>
        public DataSet GetRecycleList(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * FROM     ");
            strSql.Append("Shop_Products  ");
            strSql.Append("WHERE   SaleStatus =2  ");

            if (!string.IsNullOrWhiteSpace(where))
            {
                strSql.Append(" and " + where);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 还原所有回收站商品
        /// </summary>
        /// <returns></returns>
        public bool RevertAll()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products    ");
            strSql.Append("set SaleStatus=0 ");
            strSql.Append("WHERE   SaleStatus =2  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateStatus(long productId, int SaleStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set  SaleStatus=@SaleStatus");
            strSql.Append(" where ProductId =@ProductId ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt),
                    new SqlParameter("@SaleStatus", SqlDbType.Int,4)
            };
            parameters[0].Value = productId;
            parameters[1].Value = SaleStatus;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        #region 未分类商品重新设置分类
        /// <summary>
        /// 未分类商品重新设置分类 
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool ChangeProductsCategory(string productIds, int categoryId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductIds ", SqlDbType.NVarChar),
                    new SqlParameter("@CategoryId ", SqlDbType.Int),
                    DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)
                                        };
            parameters[0].Value = productIds;
            parameters[1].Value = categoryId;
            int rows = 0;
            DbHelperSQL.RunProcedure("sp_Shop_ChangeProductsCategory", parameters, "ds", out rows);
            return rows > 0;
        }
        #endregion


        public bool UpdateProductName(long productId, string strSetValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set  ProductName=@ProductName");
            strSql.Append(" where ProductId =@ProductId ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar)
            };
            parameters[0].Value = productId;
            parameters[1].Value = strSetValue;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public long StockNum(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(cast(Stock AS bigint)) Stock FROM Shop_SKUs ");
            strSql.Append("WHERE ProductId=@ProductId ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt)
            };
            parameters[0].Value = productId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }


        public bool UpdateMarketPrice(long productId, decimal price)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set  MarketPrice=@MarketPrice");
            strSql.Append(" where ProductId =@ProductId ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt),
                    new SqlParameter("@MarketPrice", SqlDbType.Money,8)
            };
            parameters[0].Value = productId;
            parameters[1].Value = price;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateLowestSalePrice(long productId, decimal price)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set  LowestSalePrice=@LowestSalePrice");
            strSql.Append(" where ProductId =@ProductId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt),
                    new SqlParameter("@LowestSalePrice", SqlDbType.Money,8)
            };
            parameters[0].Value = productId;
            parameters[1].Value = price;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);
            sqllist.Add(cmd);
            if (new SKUInfo().skuCount(productId) == 1)
            {
                //未开启sku和只有一个sku 的商品最低价同步到sku数据中
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append(" UPDATE  Shop_SKUs SET SalePrice=@SalePrice ");
                strSql2.Append(" where ProductId =@ProductId ");
                SqlParameter[] parameters2 = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt),
                    new SqlParameter("@SalePrice", SqlDbType.Money,8)
                };
                parameters2[0].Value = productId;
                parameters2[1].Value = price;
                cmd = new CommandInfo(strSql2.ToString(), parameters2, EffentNextType.ExcuteEffectRows);
                sqllist.Add(cmd);
            }
            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获取推荐产品信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetProductRecList(ProductRecType type, int categoryId, int top)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  ");
            if (top > 0)
            {
                strSql.AppendFormat(" TOP {0} ", top);
            }
            strSql.Append(" PSM.Sort,P.ProductId,P.ShortDescription,P.ProductName,p.ThumbnailUrl1,p.ThumbnailUrl2,P.ProductCode ,P.LowestSalePrice,p.MarketPrice,FalseSaleCount FROM    Shop_Products P ");
            if (categoryId > 0)
            {
                strSql.Append(" INNER JOIN  ");
                strSql.Append("(SELECT DISTINCT ProductId FROM Shop_ProductCategories ");
                strSql.Append("WHERE CategoryPath LIKE (SELECT Path FROM Shop_Categories ");
                strSql.AppendFormat("WHERE CategoryId={0})+'%') C ON P.ProductId = C.ProductId ", categoryId);
            }
            strSql.Append(" INNER JOIN Shop_ProductStationModes PSM ON P.ProductId = PSM.ProductId ");
            strSql.Append(" WHERE PSM.Type=@Type And P.SaleStatus=1 ");
            strSql.Append(" ORDER BY PSM.Sort DESC ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = (int)type;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取推荐产品信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetProductRecList2(int type, int categoryId, int top)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  ");
            if (top > 0)
            {
                strSql.AppendFormat(" TOP {0} ", top);
            }
            strSql.Append("PSM.Sort,P.ProductId,P.ShortDescription,P.ProductName,p.ThumbnailUrl1,p.ThumbnailUrl2,P.ProductCode ,P.LowestSalePrice,p.MarketPrice FROM    Shop_Products P ");
            if (categoryId > 0)
            {
                strSql.Append(" INNER JOIN  ");
                strSql.Append("(SELECT DISTINCT ProductId FROM Shop_ProductCategories ");
                strSql.Append("WHERE CategoryPath LIKE (SELECT Path FROM Shop_Categories ");
                strSql.AppendFormat("WHERE CategoryId={0})+'%') C ON P.ProductId = C.ProductId ", categoryId);
            }
            strSql.Append(" INNER JOIN Shop_ProductStationModes PSM ON P.ProductId = PSM.ProductId ");
            strSql.Append(" WHERE PSM.Type=@Type And P.SaleStatus=1 ");
            strSql.Append(" ORDER BY PSM.Sort asc ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = (int)type;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取推荐产品信息(不考虑分类)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetProductRecListWithOutCatg(ProductRecType type, int Floor, int top)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  ");
            if (top > 0)
            {
                strSql.AppendFormat(" TOP {0} ", top);
            }
            strSql.Append(" P.ProductId,P.ShortDescription,P.ProductName,p.ThumbnailUrl1,p.ThumbnailUrl2,P.ProductCode ,P.LowestSalePrice,FalseSaleCount,P.MarketPrice FROM    Shop_Products P ");
            strSql.Append(" INNER JOIN Shop_ProductStationModes PSM ON P.ProductId = PSM.ProductId ");
            //strSql.Append(" WHERE PSM.Type=@Type And P.SaleStatus=1 ");
            strSql.Append(" WHERE P.SaleStatus=1 ");
            if (Floor > 0)
            {
                strSql.AppendFormat(" and PSM.Floor={0}", Floor);
            }
            strSql.Append(" ORDER BY PSM.Sort ASC ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = (int)type;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取推荐产品信息(不考虑分类  李永琴)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetProductRecListWithOutCatgB(ProductRecType type, int Floor, int top)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  ");
            if (top > 0)
            {
                strSql.AppendFormat(" TOP {0} ", top);
            }
            strSql.Append(" PSM.GroupBuyId as ProductId,P.ShortDescription,P.ProductName, (case when PSM.GroupBuyImage='' then P.ThumbnailUrl1 else PSM.GroupBuyImage end)   as ImageUrl, p.ThumbnailUrl1,p.ThumbnailUrl2,P.ProductCode,FalseSaleCount ,");
            strSql.Append(" (CASE WHEN PSM.Price=0 THEN P.LowestSalePrice ELSE PSM.Price END ) AS LowestSalePrice,PSM.GroupBuyId ");
            strSql.Append("FROM  Shop_Products P  INNER JOIN Shop_WeiXinGroupBuy PSM ON P.ProductId = PSM.ProductId ");
            strSql.Append(" WHERE PSM.IsIndex=1 And P.SaleStatus=1 ");
            if (Floor > 0)
            {
                strSql.AppendFormat(" and PSM.floorID={0}", Floor);
            }
            strSql.Append(" ORDER BY PSM.Sequence ASC ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = (int)type;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public int GetProductRecCount(ProductRecType type, int categoryId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  ");
            strSql.Append(" count(*) FROM    Shop_Products P ");
            if (categoryId > 0)
            {
                strSql.Append(" INNER JOIN  ");
                strSql.Append("(SELECT DISTINCT ProductId FROM Shop_ProductCategories ");
                strSql.Append("WHERE CategoryPath LIKE (SELECT Path FROM Shop_Categories ");
                strSql.AppendFormat("WHERE CategoryId={0})+'%') C ON P.ProductId = C.ProductId ", categoryId);
            }
            strSql.Append(" INNER JOIN Shop_ProductStationModes PSM ON P.ProductId = PSM.ProductId ");
            strSql.Append(" WHERE PSM.Type=@Type And P.SaleStatus=1 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = (int)type;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 摇摇
        /// </summary>
        /// <param name="type"></param>
        /// <param name="categoryId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public DataSet GetProductRanListByRec(ProductRecType type, int categoryId, int top)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  ");
            if (top > 0)
            {
                strSql.AppendFormat(" TOP {0} ", top);
            }
            strSql.Append(" P.ProductId,P.ShortDescription,P.ProductName,p.ThumbnailUrl1,p.ThumbnailUrl2,P.ProductCode ,P.LowestSalePrice , P.MarketPrice,FalseSaleCount FROM    Shop_Products P ");
            if (categoryId > 0)
            {
                strSql.Append(" INNER JOIN  ");
                strSql.Append("(SELECT DISTINCT ProductId FROM Shop_ProductCategories ");
                strSql.Append("WHERE CategoryPath LIKE (SELECT Path FROM Shop_Categories ");
                strSql.AppendFormat("WHERE CategoryId={0})+'%') C ON P.ProductId = C.ProductId ", categoryId);
            }
            strSql.Append(" INNER JOIN Shop_ProductStationModes PSM ON P.ProductId = PSM.ProductId ");
            strSql.Append(" WHERE PSM.Type=@Type And P.SaleStatus=1 ");
            strSql.Append(" ORDER BY NewID(), PSM.StationId DESC,P.DisplaySequence ASC ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = (int)type;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取随机产品
        /// </summary>
        /// <param name="type"></param>
        /// <param name="categoryId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public DataSet GetProductRanList(int top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  ");
            if (top > 0)
            {
                strSql.AppendFormat(" TOP {0} ", top);
            }
            strSql.Append(" P.*,sku.SalePrice From Shop_Products P JOIN Shop_SKUs sku  ON p.ProductId=sku.ProductId  where SaleStatus=1 order By NewID()  ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet RelatedProductSource(long productId, int top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (top > 0)
            {
                strSql.AppendFormat(" TOP {0} ", top);
            }
            strSql.Append("P.* FROM Shop_Products P ");
            strSql.Append("INNER JOIN (SELECT RelatedId FROM Shop_RelatedProducts ");
            strSql.Append("WHERE ProductId=@ProductId)RP ON P.ProductId = RP.RelatedId AND p.SaleStatus=1  ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.Int,8)};
            parameters[0].Value = productId;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            ///如果没有相关的产品，则从同类商品中提取相关的商品
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("SELECT  P.* ");
            strSql1.Append("FROM    Shop_Products P ");
            strSql1.Append("WHERE P.SaleStatus=1 and  ProductId IN ( SELECT  ");
            if (top > 0)
            {
                strSql1.Append(" TOP " + top);
            }
            else
            {
                strSql1.Append(" TOP 3 ");
            }
            strSql1.Append("  ProductId ");
            strSql1.Append("  FROM     Shop_ProductCategories ");
            strSql1.Append("  WHERE    CategoryId IN ( SELECT  CategoryId ");
            strSql1.Append("  FROM    Shop_ProductCategories ");
            strSql1.Append("  WHERE   ProductId = " + productId + " )  AND ProductId NOT IN ( " + productId + ") ) ");
            return DbHelperSQL.Query(strSql1.ToString());
        }

        /// <summary>
        /// 根据条件获取商品
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetProductsListEx(int Cid, int BrandId, string attrValues, string priceRange,
                                          string mod, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by " + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.AppendFormat(" WHERE   SaleStatus = 1 ");
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(
                    " AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat(
              "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
              Cid);
                strSql.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            //循环属性
            if (!String.IsNullOrWhiteSpace(attrValues))
            {
                var attrValue_arry = attrValues.Split('-');
                foreach (var attr in attrValue_arry)
                {
                    int valueId = Common.Globals.SafeInt(attr, 0);
                    if (valueId > 0)
                    {
                        strSql.AppendFormat(
                            "  AND EXISTS ( SELECT * FROM   Shop_ProductAttributes WHERE  ProductId = T.ProductId AND ValueId = {0} )",
                            valueId);
                    }
                }
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取商品数量
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public int GetProductsCountEx(int Cid, int BrandId, string attrValues, string priceRange)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1) from Shop_Products T ");
            strSql.AppendFormat(" WHERE   SaleStatus = 1 ");
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(
                    " AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat(
              "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
              Cid);
                strSql.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            //循环属性
            if (!String.IsNullOrWhiteSpace(attrValues))
            {
                var attrValue_arry = attrValues.Split('-');
                foreach (var attr in attrValue_arry)
                {
                    int valueId = Common.Globals.SafeInt(attr, 0);
                    if (valueId > 0)
                    {
                        strSql.AppendFormat(
                            "  AND EXISTS ( SELECT * FROM   Shop_ProductAttributes WHERE  ProductId = T.ProductId AND ValueId = {0} )",
                            valueId);
                    }
                }
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND LowestSalePrice <= {0} ", price_arr[1]);
                }
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 根据条件获取分享商品列表
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetProductsListExShare(int Cid, int BrandId, string attrValues, string priceRange,
                                          string mod, int type, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by " + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (type > 0)
            {
                strSql.AppendFormat("   AND aa.[Type] = {0}", type);
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", Cid);
            }
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND T.BrandId = {0}", BrandId);
            }

            ////循环属性
            //if (!String.IsNullOrWhiteSpace(attrValues))
            //{
            //    var attrValue_arry = attrValues.Split('-');
            //    foreach (var attr in attrValue_arry)
            //    {
            //        int valueId = Common.Globals.SafeInt(attr, 0);
            //        if (valueId > 0)
            //        {
            //            strSql.AppendFormat(
            //                "  AND EXISTS ( SELECT * FROM   Shop_ProductAttributes WHERE  ProductId = T.ProductId AND ValueId = {0} )",
            //                valueId);
            //        }
            //    }
            //}

            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取分享商品数量
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public int GetProductsCountExShare(int Cid, int BrandId, string attrValues, string priceRange, int type)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1)  from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (type > 0)
            {
                strSql.AppendFormat("   AND aa.[Type] = {0}", type);
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", Cid);
            }
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND T.BrandId = {0}", BrandId);
            }

            ////循环属性
            //if (!String.IsNullOrWhiteSpace(attrValues))
            //{
            //    var attrValue_arry = attrValues.Split('-');
            //    foreach (var attr in attrValue_arry)
            //    {
            //        int valueId = Common.Globals.SafeInt(attr, 0);
            //        if (valueId > 0)
            //        {
            //            strSql.AppendFormat(
            //                "  AND EXISTS ( SELECT * FROM   Shop_ProductAttributes WHERE  ProductId = T.ProductId AND ValueId = {0} )",
            //                valueId);
            //        }
            //    }
            //}

            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        #region 李永琴
        public int GetProductsCountExShareB(int Cid, int BrandId, string attrValues, string priceRange, int type, string path)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1)  from Shop_Products T inner join Shop_WeiXinGroupBuy aa on aa.ProductId=T.ProductId ");
            strSql.Append(" INNER JOIN dbo.TB_GoodsType AS cc ON aa.GoodsTypeID=cc.GoodTypeID");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (type > 0)
            {
                strSql.AppendFormat("   AND aa.Status=1");
            }
            //查询分类
            //if (Cid > 0)
            //{
            //    strSql.AppendFormat("   AND aa.GoodsTypeID = {0}", Cid);
            //}
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND T.BrandId = {0}", BrandId);
            }
            if (path != "" || path != "")
            {
                strSql.AppendFormat(" AND cc.Path like '{0}%'", path);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int GetProductsCountExShareC(int Gtype, int GoodtypeId, string attrValues, string priceRange, int type, string path)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1)  from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ");

            strSql.AppendFormat(" WHERE  t.SaleStatus = 1 ");
            //分享首页
            if (Gtype > 0)
            {
                strSql.AppendFormat("   AND aa.type= {0}", Gtype);
            }
            //品牌查询
            if (GoodtypeId > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", GoodtypeId);
            }
            if (path != "" || path != "")
            {
                // strSql.AppendFormat(" AND cc.Path like '{0}%'", path);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public DataSet GetProductsListExShareB(int Cid, int BrandId, string attrValues, string priceRange,
                                         string mod, int type, int startIndex, int endIndex, string path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by " + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row,");
            strSql.Append("t.CategoryId, t.TypeId, aa.GroupBuyId AS ProductId, t.BrandId, t.ProductName,");
            strSql.Append(" t.ProductCode, t.SupplierId, t.RegionId, t.ShortDescription, t.Unit, ");
            strSql.Append(" t.Description, t.Meta_Title, t.Meta_Description, t.Meta_Keywords, t.SaleStatus, ");
            strSql.Append("t.AddedDate, t.VistiCounts, t.SaleCounts, t.Stock, t.DisplaySequence, t.LineId, ");
            strSql.Append(" t.MarketPrice,  (CASE WHEN aa.Price=0 THEN t.LowestSalePrice ELSE aa.Price END ) AS  LowestSalePrice, t.PenetrationStatus, t.MainCategoryPath, t.ExtendCategoryPath, ");
            strSql.Append("t.HasSKU, t.Points, t.ImageUrl,T.ThumbnailUrl1 AS ThumbnailUrl1 , t.ThumbnailUrl2, t.ThumbnailUrl3, t.ThumbnailUrl4, ");
            strSql.Append("t.ThumbnailUrl5, t.ThumbnailUrl6, t.ThumbnailUrl7, t.ThumbnailUrl8, t.MaxQuantity, t.MinQuantity, t.Tags,");
            strSql.Append(" t.SeoUrl, t.SeoImageAlt, t.SeoImageTitle, t.subHead, t.ISTB, t.Recommend, t.UpdateTime");
            strSql.Append(" from Shop_Products T inner join Shop_WeiXinGroupBuy aa on aa.ProductId=T.ProductId ");
            strSql.Append(" INNER JOIN dbo.TB_GoodsType cc ON cc.GoodTypeID=aa.GoodsTypeID ");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (type > 0)
            {
                strSql.AppendFormat("  AND aa.Status=1");
            }
            //查询分类
            //if (Cid > 0)
            //{
            //    strSql.AppendFormat("   AND aa.GoodsTypeID = {0}", Cid);
            //}
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND T.BrandId = {0}", BrandId);
            }
            if (path != "" || path != "")
            {
                strSql.AppendFormat(" AND cc.Path like'{0}%'", path);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetProductsListExShareC(int Gtype, int GoodtypeId, string attrValues, string priceRange,
                                         string mod, int type, int startIndex, int endIndex, string path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by " + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row,");
            strSql.Append("t.CategoryId, t.TypeId, t.ProductId, t.BrandId, t.ProductName,");
            strSql.Append(" t.ProductCode, t.SupplierId, t.RegionId, t.ShortDescription, t.Unit, ");
            strSql.Append(" t.Description, t.Meta_Title, t.Meta_Description, t.Meta_Keywords, t.SaleStatus, ");
            strSql.Append("t.AddedDate, t.VistiCounts, t.SaleCounts, t.Stock, t.DisplaySequence, t.LineId, ");
            strSql.Append(" t.MarketPrice,  t.LowestSalePrice, t.PenetrationStatus, t.MainCategoryPath, t.ExtendCategoryPath, ");
            strSql.Append("t.HasSKU, t.Points, t.ImageUrl,T.ThumbnailUrl1 AS ThumbnailUrl1 , t.ThumbnailUrl2, t.ThumbnailUrl3, t.ThumbnailUrl4, ");
            strSql.Append("t.ThumbnailUrl5, t.ThumbnailUrl6, t.ThumbnailUrl7, t.ThumbnailUrl8, t.MaxQuantity, t.MinQuantity, t.Tags,");
            strSql.Append(" t.SeoUrl, t.SeoImageAlt, t.SeoImageTitle, t.subHead, t.ISTB, t.Recommend, t.UpdateTime");
            strSql.Append(" from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ");
            strSql.Append("");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (Gtype > 0)
            {
                strSql.AppendFormat("  AND aa.type={0}", Gtype);
            }
            //品牌查询
            if (GoodtypeId > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", GoodtypeId);
            }
            if (path != "" || path != "")
            {
                // strSql.AppendFormat(" AND cc.Path like'{0}%'", path);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }





        #endregion

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
        public int GetProductsCountExActiveB(int Cid, int BrandId, string attrValues, string priceRange, int type)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1)  from Shop_Products T inner join Shop_WeiXinGroupBuy aa on aa.ProductId=T.ProductId ");
            strSql.Append(" INNER JOIN dbo.[TB_GoodsActiveType] AS cc ON aa.GoodsActiveType=cc.ID");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (type > 0)
            {
                strSql.AppendFormat("   AND aa.Status=1");
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodsActiveType = {0}", Cid);
            }
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND T.BrandId = {0}", BrandId);
            }

            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public DataSet GetProductsListExActiveB(int Cid, int BrandId, string attrValues, string priceRange,
                                         string mod, int type, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by " + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row,");
            strSql.Append("t.CategoryId, t.TypeId, aa.GroupBuyId AS ProductId, t.BrandId, t.ProductName,");
            strSql.Append(" t.ProductCode, t.SupplierId, t.RegionId, t.ShortDescription, t.Unit, ");
            strSql.Append(" t.Description, t.Meta_Title, t.Meta_Description, t.Meta_Keywords, t.SaleStatus, ");
            strSql.Append("t.AddedDate, t.VistiCounts, t.SaleCounts, t.Stock, t.DisplaySequence, t.LineId, ");
            strSql.Append(" t.MarketPrice,  (CASE WHEN aa.Price=0 THEN t.LowestSalePrice ELSE aa.Price END ) AS  LowestSalePrice, t.PenetrationStatus, t.MainCategoryPath, t.ExtendCategoryPath, ");
            strSql.Append("t.HasSKU, t.Points, t.ImageUrl,T.ThumbnailUrl1 AS ThumbnailUrl1 , t.ThumbnailUrl2, t.ThumbnailUrl3, t.ThumbnailUrl4, ");
            strSql.Append("t.ThumbnailUrl5, t.ThumbnailUrl6, t.ThumbnailUrl7, t.ThumbnailUrl8, t.MaxQuantity, t.MinQuantity, t.Tags,");
            strSql.Append(" t.SeoUrl, t.SeoImageAlt, t.SeoImageTitle, t.subHead, t.ISTB, t.Recommend, t.UpdateTime,FalseSaleCount");
            strSql.Append(" from Shop_Products T inner join Shop_WeiXinGroupBuy aa on aa.ProductId=T.ProductId ");
            strSql.Append(" INNER JOIN dbo.TB_GoodsActiveType cc ON cc.ID=aa.GoodsActiveType ");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (type > 0)
            {
                strSql.AppendFormat("  AND aa.Status=1");
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodsActiveType = {0}", Cid);
            }
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND T.BrandId = {0}", BrandId);
            }

            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion



        public int MaxSequence()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MAX(DisplaySequence) AS DisplaySequence FROM Shop_Products");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 根据类别地址 得到该类别下最大顺序值  兼容一个商品属于多个分类的情况 
        /// </summary>
        /// <param name="CategoryPath"></param>
        /// <returns></returns>
        public int MaxSequence(string CategoryPath)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  MAX(DisplaySequence)  AS DisplaySequenceDisplaySequence FROM Shop_ProductCategories  AS prodcate  ");
            strSql.Append(" LEFT JOIN   Shop_Products  AS  prod ON prodcate.ProductId=prod.ProductId  ");
            if (!string.IsNullOrWhiteSpace(CategoryPath))
            {
                strSql.Append(" WHERE  prodcate.CategoryPath in ( @CategoryPath )"); //因为一个商品可能属于多个分类，所以使用in
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryPath", SqlDbType.NVarChar)};
            parameters[0].Value = CategoryPath;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        #region 搜索商品数据
        /// <summary>
        /// 根据条件获取商品
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetSearchListEx(int Cid, int BrandId, string keyword, string priceRange,
                                          string mod, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by T." + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.AppendFormat(" WHERE   SaleStatus = 1 ");
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(
                    " AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat(
              "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
              Cid);
                strSql.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (ProductName like '%{0}%' or ShortDescription like '%{0}%') ", keyword);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取商品数量
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public int GetSearchCountEx(int Cid, int BrandId, string keyword, string priceRange)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1) from Shop_Products T ");
            strSql.AppendFormat(" WHERE   SaleStatus = 1 ");
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(
                    " AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat(
              "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
              Cid);
                strSql.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (ProductName like '%{0}%' or ShortDescription like '%{0}%' )", keyword);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND LowestSalePrice <= {0} ", price_arr[1]);
                }
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int GetProductNoRecCount(int categoryId, string pName, int modeType, int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Shop_Products ");

            strSql.Append(" WHERE  SaleStatus = 1  ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = Shop_Products.ProductId ) ",
                    categoryId);
            }
            strSql.Append(" AND NOT EXISTS ( SELECT *  FROM   Shop_ProductStationModes ");
            strSql.AppendFormat("   WHERE  Type = {0} AND ProductId = Shop_Products.ProductId ) ", modeType);
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND SupplierId ={0} ", supplierId);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int GetGiftListCount(int categoryId, string pName, int supplierId, DateTime StartDate, DateTime EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by T.ProductId desc");
            strSql.Append(")AS Row, G.GroupBuyId AS ProductId,G.ProductName,G.FinePrice AS LowestSalePrice,T.ThumbnailUrl1  from Shop_Products T ");
            strSql.Append(" RIGHT JOIN dbo.Shop_GroupBuy G ON T.ProductId = G.ProductId");
            strSql.Append(" WHERE  T.SaleStatus = 1 AND G.PromotionType = 1 ");
            if (StartDate != DateTime.MinValue && EndDate == DateTime.MinValue)
            {
                strSql.AppendFormat(" AND G.StartDate>'{0}' ", StartDate);
            }
            if (StartDate == DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND G.EndDate<'{0}' ", EndDate);
            }
            if (StartDate != DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND G.EndDate BETWEEN '{0}' AND '{1}' ", StartDate, EndDate);
            }
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ",
                    categoryId);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND G.ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND T.SupplierId ={0} ", supplierId);
            }
            strSql.Append(" ) TT");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int GetProductNoSetFreeFreightCount(int categoryId, string pName, int supplierId, string pCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Shop_Products WHERE  SaleStatus = 1   ");

            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = Shop_Products.ProductId ) ",
                    categoryId);
            }
            strSql.Append(" AND NOT EXISTS(SELECT * FROM dbo.Shop_freefreight WHERE FreeType=2 AND dbo.Shop_Products.ProductId=dbo.Shop_freefreight.ProductId) ");
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductCode = '{0}' ", pCode);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND SupplierId ={0} ", supplierId);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public DataSet GetProductNoRecList(int categoryId, int supplierId, string pName, int modeType, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by T.ProductId desc");
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.Append(" WHERE  SaleStatus = 1  ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ",
                    categoryId);
            }
            strSql.Append(" AND NOT EXISTS ( SELECT *  FROM   Shop_ProductStationModes ");
            strSql.AppendFormat("   WHERE  Type = {0} AND ProductId = T.ProductId ) ", modeType);
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND SupplierId ={0} ", supplierId);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetProductNoGroupBuyList(int categoryId, int supplierId, string pName, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by T.ProductId desc");
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.Append(" WHERE  SaleStatus = 1  ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ",
                    categoryId);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND SupplierId ={0} ", supplierId);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetProductNoGroupBuyCount(int categoryId, string pName, int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM Shop_Products T");

            strSql.Append(" WHERE  T.SaleStatus = 1  ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ",
                    categoryId);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND T.ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND T.SupplierId ={0} ", supplierId);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public DataSet GetGiftList(int categoryId, int supplierId, string pName, int startIndex, int endIndex, DateTime StartDate, DateTime EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by T.ProductId desc");
            strSql.Append(")AS Row, G.GroupBuyId AS ProductId,G.ProductName,G.FinePrice AS LowestSalePrice,T.ThumbnailUrl1  from Shop_Products T ");
            strSql.Append(" RIGHT JOIN dbo.Shop_GroupBuy G ON T.ProductId = G.ProductId");
            strSql.Append(" WHERE  T.SaleStatus = 1 AND G.PromotionType = 1 ");
            if (StartDate != DateTime.MinValue && EndDate == DateTime.MinValue)
            {
                strSql.AppendFormat(" AND G.StartDate>'{0}' ", StartDate);
            }
            if (StartDate == DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND G.EndDate<'{0}' ", EndDate);
            }
            if (StartDate != DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND G.EndDate BETWEEN '{0}' AND '{1}' ", StartDate, EndDate);
            }

            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ",
                    categoryId);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND G.ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND T.SupplierId ={0} ", supplierId);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
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
        public DataSet GetGiftDistinctList(int categoryId, int supplierId, string pName, int startIndex, int endIndex, DateTime StartDate, DateTime EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from (select ROW_NUMBER() over(order by x.GroupBuyId) as RowID, x.GroupBuyId xGroupBuyId,x.ProductId xProductId,x.Sequence xSequence,x.FinePrice xFinePrice,x.StartDate xStartDate,x.EndDate xEndDate,x.MaxCount xMaxCount,x.GroupCount xGroupCount,x.BuyCount xBuyCount,x.Price xPrice,x.Status xStatus,x.Description xDescription,x.RegionId xRegionId,x.ProductName xProductName,x.ProductCategory xProductCategory,x.GroupBuyImage xGroupBuyImage,x.CategoryId xCategoryId,x.CategoryPath xCategoryPath,x.PromotionLimitQu xPromotionLimitQu,x.GroupBase xGroupBase,x.PromotionType xPromotionType,y.* from (select ROW_NUMBER() over(partition by ProductId order by GroupBuyId) RowID,* from Shop_GroupBuy where PromotionType=1) x left join Shop_Products y on x.ProductId = y.ProductId ");
            strSql.Append(" where x.RowID=1 and y.SaleStatus=1 ");
            if (StartDate != DateTime.MinValue && EndDate == DateTime.MinValue)
            {
                strSql.AppendFormat(" AND x.StartDate>'{0}' ", StartDate);
            }
            if (StartDate == DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND x.EndDate<'{0}' ", EndDate);
            }
            if (StartDate != DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND x.EndDate BETWEEN '{0}' AND '{1}' ", StartDate, EndDate);
            }

            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = x.ProductId ) ",
                    categoryId);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND x.ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND y.SupplierId ={0} ", supplierId);
            }
            strSql.AppendFormat(" ) T where T.RowID between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from (select ROW_NUMBER() over(order by x.GroupBuyId) as RowID, x.GroupBuyId xGroupBuyId,x.ProductId xProductId,x.Sequence xSequence,x.FinePrice xFinePrice,x.StartDate xStartDate,x.EndDate xEndDate,x.MaxCount xMaxCount,x.GroupCount xGroupCount,x.BuyCount xBuyCount,x.Price xPrice,x.Status xStatus,x.Description xDescription,x.RegionId xRegionId,x.ProductName xProductName,x.ProductCategory xProductCategory,x.GroupBuyImage xGroupBuyImage,x.CategoryId xCategoryId,x.CategoryPath xCategoryPath,x.PromotionLimitQu xPromotionLimitQu,x.GroupBase xGroupBase,x.PromotionType xPromotionType,y.*,z.SkuId zSkuId,z.SKU zSKU,z.Weight zWeight,z.Stock zStock,z.AlertStock zAlertStock,z.CostPrice zCostPrice,z.SalePrice zSalePrice,z.Upselling zUpselling from (select ROW_NUMBER() over(partition by ProductId order by GroupBuyId) RowID,* from Shop_GroupBuy where PromotionType=1) x left join Shop_Products y on x.ProductId = y.ProductId left join (select * from (select ROW_NUMBER() over(partition by ProductId order by SkuId) Row, * from Shop_SKUs) S where S.Row=1) z on x.ProductId = z.ProductId where x.RowID=1 and y.SaleStatus=1 ");
            if (StartDate != DateTime.MinValue && EndDate == DateTime.MinValue)
            {
                strSql.AppendFormat(" AND x.StartDate>'{0}' ", StartDate);
            }
            if (StartDate == DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND x.EndDate<'{0}' ", EndDate);
            }
            if (StartDate != DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                strSql.AppendFormat(" AND x.EndDate BETWEEN '{0}' AND '{1}' ", StartDate, EndDate);
            }

            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = x.ProductId ) ",
                    categoryId);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND x.ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" y.SupplierId ={0} ", supplierId);
            }
            strSql.Append(" ) T ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows.Count);
        }

        public DataSet GetSelectedProducts(string groupbuyids)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT T.ProductId,T.ProductName,T.ThumbnailUrl1,G.Price FROM dbo.Shop_GroupBuy G LEFT JOIN dbo.Shop_Products T ON G.ProductId = T.ProductId ");
            stb.AppendFormat("  WHERE G.GroupBuyId IN ({0})", groupbuyids.Trim(','));
            return DbHelperSQL.Query(stb.ToString());
        }

        /// <summary>
        /// 获取未设置免邮的商品列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        /// <param name="pName"></param>
        /// <param name="pCode"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetProductNoSetFreeFreightList(int categoryId, int supplierId, string pName, string pCode, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by T.ProductId desc");
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.Append(" WHERE  SaleStatus = 1  ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ",
                    categoryId);
            }
            strSql.Append(" AND NOT EXISTS ( SELECT * FROM dbo.Shop_freefreight WHERE FreeType=2 AND T.ProductId=dbo.Shop_freefreight.ProductId ) ");
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(" AND Supplier ={0} ", supplierId);
            }
            if (!string.IsNullOrEmpty(pCode))
            {
                strSql.AppendFormat(" and ProductCode = '{0}'", pCode);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取免邮商品列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetFreeFreightProductList(int FreeType, int supplierId, string pName, string pCode, int categoryId)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT a.ProductId FROM dbo.Shop_freefreight a LEFT	 JOIN dbo.Shop_Products b ON a.productid=b.ProductId ");
            stb.AppendFormat(" where a.FreeType={0}", FreeType);

            if (categoryId > 0)
            {
                stb.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                stb.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ",
                    categoryId);
            }
            stb.Append(" AND EXISTS ( SELECT *  FROM      Shop_Products WHERE  SaleStatus = 1   )");
            if (!String.IsNullOrWhiteSpace(pName))
            {
                stb.AppendFormat(" and b.productname like '%{0}%'", pName);
            }
            if (!String.IsNullOrWhiteSpace(pCode))
            {
                stb.AppendFormat(" and b.productcode like '%{0}%'", pCode);
            }
            if (supplierId > 0)
            {
                stb.AppendFormat(" and b.SupplierId={0}", supplierId);
            }
            return DbHelperSQL.Query(stb.ToString());
        }


        #endregion

        #region 搜索分享商品数据
        /// <summary>
        /// 搜索分享商品数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetSearchListExShare(int Cid, int BrandId, string keyword, string priceRange,
                                          string mod, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by " + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }

            strSql.Append(")AS Row, T.*  from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 AND aa.[Type] =6 ");

            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", Cid);
            }


            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }

            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (T.ProductName like '%{0}%' or T.ShortDescription like '%{0}%') ", keyword);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获取商品数量
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="BrandId"></param>
        /// <param name="attrValues"></param>
        /// <param name="priceRange"></param>
        /// <param name="mod"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public int GetSearchCountExShare(int Cid, int BrandId, string keyword, string priceRange)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1)  from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ");

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 AND aa.[Type] =6 ");

            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", Cid);
            }


            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }

            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (T.ProductName like '%{0}%' or T.ShortDescription like '%{0}%') ", keyword);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 搜索分享商品数据(李永琴)
        /// </summary>
        /// <returns></returns>
        public DataSet GetSearchListExShareB(int Cid, int BrandId, string keyword, string priceRange,
                                          string mod, int startIndex, int endIndex, string path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                strSql.Append("order by " + mod);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }

            //strSql.Append(")AS Row, T.*  from Shop_Products T inner join Shop_WeiXinGroupBuy aa on aa.ProductId=T.ProductId ");

            strSql.Append(")AS Row,");
            strSql.Append("t.CategoryId, t.TypeId, aa.GroupBuyId AS ProductId, t.BrandId, t.ProductName,");
            strSql.Append(" t.ProductCode, t.SupplierId, t.RegionId, t.ShortDescription, t.Unit, ");
            strSql.Append(" t.Description, t.Meta_Title, t.Meta_Description, t.Meta_Keywords, t.SaleStatus, ");
            strSql.Append("t.AddedDate, t.VistiCounts, t.SaleCounts, t.Stock, t.DisplaySequence, t.LineId, ");
            strSql.Append(" t.MarketPrice,  (CASE WHEN aa.Price=0 THEN t.LowestSalePrice ELSE aa.Price END ) AS  LowestSalePrice, t.PenetrationStatus, t.MainCategoryPath, t.ExtendCategoryPath, ");
            strSql.Append("t.HasSKU, t.Points, t.ImageUrl,T.ThumbnailUrl1  AS ThumbnailUrl1 , t.ThumbnailUrl2, t.ThumbnailUrl3, t.ThumbnailUrl4, ");
            strSql.Append("t.ThumbnailUrl5, t.ThumbnailUrl6, t.ThumbnailUrl7, t.ThumbnailUrl8, t.MaxQuantity, t.MinQuantity, t.Tags,");
            strSql.Append(" t.SeoUrl, t.SeoImageAlt, t.SeoImageTitle, t.subHead, t.ISTB, t.Recommend, t.UpdateTime");
            strSql.Append(" from Shop_Products T inner join Shop_WeiXinGroupBuy aa on aa.ProductId=T.ProductId ");
            strSql.Append("INNER JOIN dbo.TB_GoodsType cc ON cc.GoodTypeID=aa.GoodsTypeID  ");
            strSql.AppendFormat(" WHERE   t.SaleStatus = 1  AND aa.Status=1 ");

            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", Cid);
            }

            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }

            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (T.ProductName like '%{0}%' or T.ShortDescription like '%{0}%') ", keyword);
            }
            if (path != "" || path != "")
            {
                strSql.AppendFormat(" AND cc.Path like'{0}%'", path);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }

            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetSearchCountExShareB(int Cid, int BrandId, string keyWord, string priceRange, string path)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT  count(1)  from Shop_Products T inner join Shop_WeiXinGroupBuy aa on aa.ProductId=T.ProductId ");
            strSql.Append("INNER JOIN dbo.TB_GoodsType cc ON cc.GoodTypeID=aa.GoodsTypeID  ");
            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 AND aa.Status=1");

            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", Cid);
            }
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyWord))
            {
                strSql.AppendFormat(" AND (T.ProductName like '%{0}%' or T.ShortDescription like '%{0}%') ", keyWord);
            }
            if (path != "" || path != "")
            {
                strSql.AppendFormat(" AND cc.Path like'{0}%'", path);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND T.LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND T.LowestSalePrice <= {0} ", price_arr[1]);
                }
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        #endregion

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string productCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Products");
            strSql.Append(" where ProductCode=@ProductCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductCode", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = productCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        public DataSet GetProductsByCid(int Cid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");

            strSql.Append("order by T.ProductId desc");
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.AppendFormat(" WHERE   SaleStatus = 1 ");

            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(
                    " AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat(
              "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
              Cid);
                strSql.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }

            strSql.Append(" ) TT");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region  限时抢购

        public int GetProSalesCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  COUNT(1) FROM    Shop_CountDown D  ");
            strSql.AppendFormat("  WHERE   Status = 1 AND EndDate>=GETDATE() ");
            strSql.AppendFormat("   AND EXISTS ( SELECT ProductId FROM   Shop_Products P WHERE  SaleStatus = 1 AND D.ProductId = P.ProductId ) ");

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public DataSet GetProSalesList(int startIndex, int endIndex, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by D.Sequence Desc ");
            strSql.Append(")AS Row, D.CountDownId,D.Price AS ProSalesPrice,D.EndDate AS ProSalesEndDate ,P.* FROM  Shop_CountDown D INNER JOIN ");
            strSql.Append(" Shop_Products P ON P.ProductId = d.ProductId LEFT JOIN (SELECT ProductId,SUM(Stock) Stock FROM dbo.Shop_SKUs GROUP BY ProductId) as S ON d.ProductId = s.ProductId");
            strSql.Append("  WHERE   Status = 1 AND EndDate>=GETDATE()  AND SaleStatus=1 ");
            if (type == 1)//库存大于0
            {
                strSql.AppendFormat(" AND S.Stock>={0} ", 1);
            }
            else if (type == 2)
            {
                strSql.AppendFormat(" AND S.Stock<={0} ", 0);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #region 会员体验区查询
        public DataSet GetProSalesList(int startIndex, int endIndex, int type, int CategoryId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int),
                    new SqlParameter("@ISStock", SqlDbType.Int),
                    new SqlParameter("@startIndex", SqlDbType.Int),
                    new SqlParameter("@endIndex", SqlDbType.Int)
                                        };
            parameters[0].Value = CategoryId;
            parameters[1].Value = type;
            parameters[2].Value = startIndex;
            parameters[3].Value = endIndex;
            return DbHelperSQL.RunProcedure("SP_Shop_CountDown_Select", parameters, "Shop_CountDown_Select");
        }
        #endregion

        public DataSet GetProSalesList(int cid, int regionId, int startIndex, int endIndex, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            if (cid == 0)
            {
                if (regionId == 0)//
                {

                    strSql.Append("SELECT * FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    strSql.Append("order by D.Sequence Desc ");
                    strSql.Append(")AS Row, D.GroupBuyId,D.Sequence,D.FinePrice,D.StartDate,D.EndDate,D.MaxCount,D.GroupCount,D.BuyCount,D.Price,D.Status,D.Description AS BuyDesc,P.* FROM  Shop_GroupBuy D ,Shop_Products P ");
                    strSql.Append("  WHERE   Status = 1 AND EndDate>=GETDATE()   AND StartDate<=GETDATE()");
                    strSql.Append(" AND P.ProductId=D.ProductId AND SaleStatus=1  ");
                    strSql.Append(" ) TT");
                    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                    strSql.Append("order by TT." + orderby);

                }
                else
                {
                    //StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT * FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    strSql.Append("order by D.Sequence Desc ");
                    strSql.Append(")AS Row, D.GroupBuyId,D.Sequence,D.FinePrice,D.StartDate,D.EndDate,D.MaxCount,D.GroupCount,D.BuyCount,D.Price,D.Status,D.Description AS BuyDesc,P.* FROM  Shop_GroupBuy D ,Shop_Products P ");
                    strSql.Append("  WHERE   Status = 1 AND EndDate>=GETDATE()   AND StartDate<=GETDATE()");
                    strSql.Append(" AND P.ProductId=D.ProductId AND SaleStatus=1  And D.RegionId=" + regionId);
                    strSql.Append(" ) TT");
                    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                    strSql.Append("order by TT." + orderby);
                }
            }
            else
            {
                if (regionId == 0)//
                {
                    // StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT * FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    strSql.Append("order by D.Sequence Desc ");
                    strSql.Append(")AS Row, D.GroupBuyId,D.Sequence,D.FinePrice,D.StartDate,D.EndDate,D.MaxCount,D.GroupCount,D.BuyCount,D.Price,D.Status,D.Description AS BuyDesc,P.* FROM  Shop_GroupBuy D ,Shop_Products P,Shop_ProductCategories C ");
                    strSql.Append("  WHERE   Status = 1 AND EndDate>=GETDATE()   AND StartDate<=GETDATE()");
                    strSql.Append(" AND P.ProductId=D.ProductId AND SaleStatus=1  AND P.ProductId=C.ProductId And C.CategoryId=" + cid);
                    strSql.Append(" ) TT");
                    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                    strSql.Append("order by TT." + orderby);
                }
                else
                {
                    // StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT * FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    strSql.Append("order by D.Sequence Desc ");
                    strSql.Append(")AS Row, D.GroupBuyId,D.Sequence,D.FinePrice,D.StartDate,D.EndDate,D.MaxCount,D.GroupCount,D.BuyCount,D.Price,D.Status,D.Description AS BuyDesc,P.* FROM  Shop_GroupBuy D ,Shop_Products P,Shop_ProductCategories C ");
                    strSql.Append("  WHERE   Status = 1 AND EndDate>=GETDATE()   AND StartDate<=GETDATE()");
                    strSql.Append(" AND P.ProductId=D.ProductId AND SaleStatus=1  AND P.ProductId=C.ProductId And C.CategoryId=" + cid);
                    strSql.Append(" AND D.RegionId=" + regionId);
                    strSql.Append(" ) TT");
                    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                    strSql.Append("order by TT." + orderby);
                }
            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetProSaleModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 D.CountDownId,D.Price AS ProSalesPrice,D.EndDate AS ProSalesEndDate,d.Description as CountDownDescription  ,P.* FROM Shop_CountDown D ,Shop_Products P  ");
            strSql.Append("  WHERE   Status = 1 and CountDownId=@CountDownId ");
            strSql.Append(" AND P.ProductId=d.ProductId AND SaleStatus=1  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CountDownId", SqlDbType.Int)
            };
            parameters[0].Value = id;
            Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.Model.Shop.Products.ProductInfo();
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取团购数据 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetGroupBuyList(int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by D.Sequence Desc ");
            strSql.Append(")AS Row, D.GroupBuyId,D.Sequence,D.FinePrice,D.StartDate,D.EndDate,D.MaxCount,D.GroupCount,D.BuyCount,D.Price,D.Status,D.Description AS BuyDesc,P.* FROM  Shop_GroupBuy D ,Shop_Products P ");
            strSql.Append("  WHERE   Status = 1 AND EndDate>=GETDATE()   AND StartDate<=GETDATE()");
            strSql.Append(" AND P.ProductId=D.ProductId AND SaleStatus=1  ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取团购Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetGroupBuyModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 D.GroupBuyImage,D.GroupBuyId,D.Sequence,D.FinePrice,D.StartDate,D.EndDate,D.MaxCount,D.GroupCount,D.BuyCount,D.Price,D.Status,D.Description AS BuyDesc,D.PromotionType,P.* FROM Shop_GroupBuy D ,Shop_Products P  ");
            strSql.Append("  WHERE   Status = 1 and GroupBuyId=@GroupBuyId ");
            strSql.Append(" AND P.ProductId=d.ProductId AND SaleStatus=1  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupBuyId", SqlDbType.Int)
            };
            parameters[0].Value = id;
            //Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.Model.Shop.Products.ProductInfo();
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public int GetGroupBuyCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  COUNT(1) FROM    Shop_GroupBuy D  ");
            strSql.AppendFormat("  WHERE   Status = 1 AND EndDate>=GETDATE()  AND StartDate<=GETDATE()");
            strSql.AppendFormat("   AND EXISTS ( SELECT ProductId FROM   Shop_Products P WHERE  SaleStatus = 1 AND D.ProductId = P.ProductId ) ");

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int GetGroupBuyCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  COUNT(1) FROM    Shop_GroupBuy D  ");
            strSql.AppendFormat("  WHERE   Status = 1 AND EndDate>=GETDATE()  AND StartDate<=GETDATE()");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.AppendFormat("And {0}", strWhere);
            }
            strSql.AppendFormat("   AND EXISTS ( SELECT ProductId FROM   Shop_Products P WHERE  SaleStatus = 1 AND D.ProductId = P.ProductId ) ");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public int GetProductStatus(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SaleStatus  ");
            strSql.Append("FROM Shop_Products ");
            strSql.AppendFormat("WHERE ProductId={0} ", productId);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            return Common.Globals.SafeInt(obj, -1);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  count(1) from Shop_Products T ");
            strSql.AppendFormat(" WHERE  SaleStatus = 1 and SupplierId={0} ", supplierId);
            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (ProductName like '%{0}%' or ShortDescription like '%{0}%') ", keyword);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND LowestSalePrice <= {0} ", price_arr[1]);
                }
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_SuppProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_SupplierCategories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                strSql.AppendFormat(" OR Shop_SuppProductCategories.CategoryId = {0}))", Cid);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.AppendFormat(" WHERE   SaleStatus = 1  and SupplierId={0} ", supplierId);
            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (ProductName like '%{0}%' or ShortDescription like '%{0}%') ", keyword);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND LowestSalePrice <= {0} ", price_arr[1]);
                }
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_SuppProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_SupplierCategories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                strSql.AppendFormat(" OR Shop_SuppProductCategories.CategoryId = {0}))", Cid);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据条件获取供应商商品
        /// </summary>
        /// <param name="top"></param>
        /// <param name="Cid"></param>
        /// <param name="supplierId"></param>
        /// <param name="orderby"></param>
        /// <param name="keyword"></param>
        /// <param name="priceRange"></param>
        /// <returns></returns>
        public DataSet GetSuppProductsList(int top, int Cid, int supplierId, string orderby, string keyword, string priceRange)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select   ");
            if (top > 0)
            {
                strSql.AppendFormat(" Top  {0}  ", top);
            }
            strSql.Append("   * from Shop_Products T ");
            strSql.AppendFormat(" WHERE   SaleStatus = 1  and SupplierId={0} ", supplierId);
            //关键字搜索
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND (ProductName like '%{0}%' or ShortDescription like '%{0}%') ", keyword);
            }
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_SuppProductCategories WHERE  ProductId =T.ProductId  ");
                strSql.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_SupplierCategories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                strSql.AppendFormat(" OR Shop_SuppProductCategories.CategoryId = {0}))", Cid);
            }
            //价格区间
            if (!String.IsNullOrWhiteSpace(priceRange))
            {
                var price_arr = priceRange.Split('-');
                decimal startPrice = Common.Globals.SafeInt(price_arr[0], 0);
                strSql.AppendFormat("   AND LowestSalePrice >= {0} ", startPrice);
                if (price_arr.Length > 1 && Common.Globals.SafeInt(price_arr[1], 0) > 0)
                {
                    strSql.AppendFormat("   AND LowestSalePrice <= {0} ", price_arr[1]);
                }
            }
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ProductId desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool UpdateThumbnail(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set ");
            strSql.Append("ThumbnailUrl1=@ThumbnailUrl1 ");
            strSql.Append(" where ProductId=@ProductId AND ImageUrl=@ImageUrl");
            SqlParameter[] parameters = {
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar,255),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ImageUrl;
            parameters[1].Value = model.ThumbnailUrl1;
            parameters[2].Value = model.ProductId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetListToReGen(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ProductId from Shop_Products  ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append("WHERE  " + strWhere);
            }
            //  strSql.Append("ORDER BY AddedDate DESC ");
            return DbHelperSQL.Query((strSql.ToString()));
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetProdRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DISTINCT  count(1) ");
            strSql.Append("FROM Shop_Products P ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 商品数据分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetProdListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT  DISTINCT  ROW_NUMBER() OVER ( ");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by P." + orderby);
            }
            else
            {
                strSql.Append("order by P.ProductId desc");
            }
            strSql.Append(" )AS Row, ");
            strSql.Append(" P.CategoryId, TypeId, P.ProductId, supplierid, BrandId, ProductName, ProductCode, SaleStatus, AddedDate, VistiCounts, SaleCounts, MarketPrice, LowestSalePrice, PenetrationStatus, MainCategoryPath, ExtendCategoryPath,  ImageUrl, ThumbnailUrl1,FalseSaleCount ");
            strSql.Append("FROM Shop_Products P ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query((strSql.ToString()));
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select P.* From  Shop_SuppProductStatModes S, Shop_Products P ");
            strSql.AppendFormat(" WHERE S.SupplierId={0} AND S.ProductId = P.ProductId  and   SaleStatus = 1 ", supplierId);
            strSql.AppendFormat(" and S.Type = {0} ", type);
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                strSql.AppendFormat(" ORDER BY {0} ", orderby);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 获取商品类别下的列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetProList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ShopCategories.CategoryId,ShopCategories.ProductId,ShopCategories.CategoryPath,ShopProduct.CategoryId,ShopProduct.TypeId,
            ShopProduct.ProductId, ShopProduct.BrandId, ShopProduct.ProductName, ShopProduct.ProductCode, ShopProduct.SupplierId, ShopProduct.RegionId, 
            ShopProduct.ShortDescription, ShopProduct.Unit, ShopProduct.[Description], ShopProduct.Meta_Title, ShopProduct.Meta_Description, ShopProduct.Meta_Keywords,
            ShopProduct.SaleStatus, ShopProduct.AddedDate, ShopProduct.VistiCounts, ShopProduct.SaleCounts, ShopProduct.Stock, ShopProduct.DisplaySequence, ShopProduct.LineId, 
            ShopProduct.MarketPrice, ShopProduct.LowestSalePrice, ShopProduct.PenetrationStatus, ShopProduct.MainCategoryPath, ShopProduct.ExtendCategoryPath, ShopProduct.HasSKU, 
            ShopProduct.Points, ShopProduct.ImageUrl, ShopProduct.ThumbnailUrl1, ShopProduct.ThumbnailUrl2, ShopProduct.ThumbnailUrl3, ShopProduct.ThumbnailUrl4, 
            ShopProduct.ThumbnailUrl5, ShopProduct.ThumbnailUrl6, ShopProduct.ThumbnailUrl7, ShopProduct.ThumbnailUrl8, ShopProduct.MaxQuantity, ShopProduct.MinQuantity, FalseSaleCount,
            ShopProduct.Tags, ShopProduct.SeoUrl, ShopProduct.SeoImageAlt, ShopProduct.SeoImageTitle,ShopProduct.Subhead ");
            strSql.Append(" from    Shop_ProductCategories as ShopCategories inner join    Shop_Products as ShopProduct ");
            strSql.Append(" on ShopProduct.ProductId = ShopCategories.ProductId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public int GetCount(int cid, int regionId)
        {
            if (cid == 0)//没有分类
            {
                if (regionId == 0)//没有地区
                {
                    return GetGroupBuyCount();
                }
                else//没有分类有地区
                {
                    string strWhere = string.Format("RegionId={0}", regionId);
                    return GetGroupBuyCount(strWhere);
                }
            }
            else
            {
                if (regionId == 0)//有分类没有地区
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT  COUNT(1) FROM    [Shop_ProductCategories] p, [Shop_GroupBuy] g ");
                    strSql.AppendFormat("  WHERE p.ProductId=g.ProductId  And   g.Status = 1 AND g.EndDate>=GETDATE()  AND g.StartDate<=GETDATE()");
                    strSql.AppendFormat("   AND EXISTS ( SELECT ProductId FROM   Shop_Products P WHERE  SaleStatus = 1 AND g.ProductId = P.ProductId ) ");
                    object obj = DbHelperSQL.GetSingle(strSql.ToString());
                    if (obj == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(obj);
                    }

                }
                else//既有分类也有地区
                {
                    string strWhere = string.Format("RegionId={0}", regionId);
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT  COUNT(1) FROM    [Shop_ProductCategories] p, [Shop_GroupBuy] g ");
                    strSql.AppendFormat("  WHERE p.ProductId=g.ProductId  And   g.Status = 1 AND g.EndDate>=GETDATE()  AND g.StartDate<=GETDATE()");
                    if (!string.IsNullOrWhiteSpace(strWhere))
                    {
                        strSql.AppendFormat("And {0}", strWhere);
                    }
                    strSql.AppendFormat("   AND EXISTS ( SELECT ProductId FROM   Shop_Products P WHERE  SaleStatus = 1 AND g.ProductId = P.ProductId ) ");
                    object obj = DbHelperSQL.GetSingle(strSql.ToString());
                    if (obj == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(obj);
                    }
                }
            }
        }


        /// <summary>
        /// 根据商家id获得是否存在该记录
        /// </summary>
        public bool Exists(int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Products");
            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupplierId", SqlDbType.Int)
            };
            parameters[0].Value = supplierId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #region 获取会员体验区上方商品类型
        public DataSet GetShopCountDownCategories()
        {
            SqlParameter[] parameters = { };
            return DbHelperSQL.RunProcedure("SP_Shop_CountDown_Categories", parameters, "GetShopCountDownCategories");

        }
        #endregion

        #region 获取会员体验产品top
        public DataSet GetShopCountDownProductTop(long ProductId)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt
                        )
            };
            parameters[0].Value = ProductId;
            return DbHelperSQL.RunProcedure("SP_Shop_CountDown_SelectTop", parameters, "GetCountDown_SelectTop");

        }

        #endregion

        /// <summary>
        /// 更新商品Tag标签
        /// </summary>
        /// <param name="categoryId">类型</param>
        /// <returns></returns>
        public int ChangeProductsTag(int categoryId, string Tags, int updatetype)
        {
            StringBuilder strSql = new StringBuilder();
            if (updatetype == 0)
            {
                strSql.Append("UPDATE Shop_Products SET Tags=@Tags FROM Shop_Products aa ,Shop_ProductCategories bb  WHERE aa.ProductId=bb.ProductId AND bb.CategoryId=@CategoryId");
            }
            else
            {
                strSql.Append("UPDATE Shop_Products SET Tags=(CASE WHEN Tags IS NULL OR Tags='' THEN '' ELSE Tags+',' END)+@Tags FROM Shop_Products aa ,Shop_ProductCategories bb  WHERE aa.ProductId=bb.ProductId AND bb.CategoryId=@CategoryId");
            }

            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int),
                    new SqlParameter("@Tags", SqlDbType.NVarChar,200)
            };
            parameters[0].Value = categoryId;
            parameters[1].Value = Tags;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 产品ProductIDList 'a,b,c' 0 是覆盖，1是追加
        /// </summary>
        /// <param name="ProductIDList"></param>
        /// <returns></returns>
        public int ChangeProductsTag(string ProductIDList, string Tags, int updatetype)
        {
            StringBuilder strSql = new StringBuilder();
            if (updatetype == 0)
            {
                strSql.Append("update Shop_Products set Tags='" + Tags + "'");
                strSql.Append(" where ProductId in (" + ProductIDList + ")");
            }
            else
            {
                strSql.Append("update Shop_Products set Tags=(CASE WHEN Tags IS NULL OR Tags='' THEN '' ELSE Tags+',' END)+ '" + Tags + "'");
                strSql.Append(" where ProductId in (" + ProductIDList + ")");
            }

            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 返回一个类型中所有产品
        /// </summary>
        /// <param name="categoryId">产品类型</param>
        /// <param name="SaleStatus">是否上架（0表示下架 1 表示下架 ）</param>
        /// <returns></returns>
        public DataSet GetProductsTagCategories(int categoryId, int SaleStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT aa.ProductId,bb.CategoryId,aa.SaleStatus FROM Shop_Products aa ,Shop_ProductCategories bb  WHERE aa.ProductId=bb.ProductId AND bb.CategoryId=@CategoryId and SaleStatus=@SaleStatus ");

            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int),
                    new SqlParameter("@SaleStatus", SqlDbType.Int)
            };
            parameters[0].Value = categoryId;
            parameters[1].Value = SaleStatus;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 商品推荐  0删除商品   1添加商品
        /// </summary>
        /// <returns></returns>
        public bool UpdateRecommend(int ProductId, int Recommend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Products set ");
            strSql.Append("Recommend=@Recommend,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ProductId=@ProductId");
            SqlParameter[] parameters = {
                    new SqlParameter("@Recommend", SqlDbType.Int,4),
                    new SqlParameter("@ProductId",SqlDbType.BigInt,8),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime),
                                          };
            parameters[0].Value = Recommend;
            parameters[1].Value = ProductId;
            parameters[2].Value = DateTime.Now;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 分页获取数据列表(李永琴2014-11-07 16:51添加此方法)
        /// </summary>
        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex, int Floor = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT pp.Sort AS DisplaySequence ,pp.StationId AS SaleStatus, {0} FROM ( ", Floor == 0 ? "*" : "Top 5 *");
            strSql.Append(" SELECT  T.*  from Shop_Products T where t.SaleStatus=1 ");
            strSql.Append(" ) TT INNER JOIN (select ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by " + orderby);
            }
            else
            {
                strSql.Append("order by StationId desc");
            }
            strSql.Append(")AS Row, productid , Sort ,StationId,[Type],[Floor],GoodTypeID from dbo.Shop_ProductStationModes ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (Floor != 0)
            {
                strSql.Append(" and Floor=" + Floor);
            }
            strSql.Append(") AS pp ON tt.ProductId=pp.ProductId");
            strSql.AppendFormat(" WHERE pp.Row between {0} and {1} ", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 获取活动商品
        public DataSet GetProductsActiveList(int GoodTypeID, int BrandId, int Floor, int type, int top,string mod="")
        {
            StringBuilder strSql = new StringBuilder();
       
            if (top > 0)
            {
                strSql.Append( string.Format("  SELECT TOP {0} T.* from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ",top));
            }
            else
            {
                strSql.Append("SELECT  T.* from Shop_Products T inner join Shop_ProductStationModes aa on aa.ProductId=T.ProductId ");
            }

            strSql.AppendFormat(" WHERE   t.SaleStatus = 1 ");
            //分享首页
            if (type > 0)
            {
                strSql.AppendFormat("   AND aa.[Type] = {0}", type);
            }
            //查询分类
            if (GoodTypeID > 0)
            {
                strSql.AppendFormat("   AND aa.GoodTypeID = {0}", GoodTypeID);
            }
            //品牌查询
            if (BrandId > 0)
            {
                strSql.AppendFormat("   AND T.BrandId = {0}", BrandId);
            }
            if (Floor > 0)
            {
                strSql.AppendFormat("   AND T.Floor = {0}", Floor);
            }
            if (string.IsNullOrEmpty(mod))
            {
                strSql.Append(" order by Sort  ");
            }
            else
            {
                strSql.Append(string.Format(" order by {0} ",mod)) ;
            }

            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

    }
}