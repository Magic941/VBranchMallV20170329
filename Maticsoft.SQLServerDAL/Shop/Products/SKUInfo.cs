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
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.DBUtility;
using System.Collections.Generic;
namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:SKUInfo
    /// </summary>
    public partial class SKUInfo : ISKUInfo
    {
        public SKUInfo()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SkuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_SKUs");
            strSql.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt)
            };
            parameters[0].Value = SkuId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_SKUs(");
            strSql.Append("ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling)");
            strSql.Append(" VALUES (");
            strSql.Append("@ProductId,@SKU,@Weight,@Stock,@AlertStock,@CostPrice,@SalePrice,@Upselling)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@SKU", SqlDbType.NVarChar,50),
                    new SqlParameter("@Weight", SqlDbType.Int,4),
                    new SqlParameter("@Stock", SqlDbType.Int,4),
                    new SqlParameter("@AlertStock", SqlDbType.Int,4),
                    new SqlParameter("@CostPrice", SqlDbType.Money,8),
                    new SqlParameter("@SalePrice", SqlDbType.Money,8),
                    new SqlParameter("@Upselling", SqlDbType.Bit,1)};
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.SKU;
            parameters[2].Value = model.Weight;
            parameters[3].Value = model.Stock;
            parameters[4].Value = model.AlertStock;
            parameters[5].Value = model.CostPrice;
            parameters[6].Value = model.SalePrice;
            parameters[7].Value = model.Upselling;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_SKUs SET ");
            strSql.Append("ProductId=@ProductId,");
            strSql.Append("SKU=@SKU,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("Stock=@Stock,");
            strSql.Append("AlertStock=@AlertStock,");
            strSql.Append("CostPrice=@CostPrice,");
            strSql.Append("SalePrice=@SalePrice,");
            strSql.Append("Upselling=@Upselling");
            strSql.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@SKU", SqlDbType.NVarChar,50),
                    new SqlParameter("@Weight", SqlDbType.Int,4),
                    new SqlParameter("@Stock", SqlDbType.Int,4),
                    new SqlParameter("@AlertStock", SqlDbType.Int,4),
                    new SqlParameter("@CostPrice", SqlDbType.Money,8),
                    new SqlParameter("@SalePrice", SqlDbType.Money,8),
                    new SqlParameter("@Upselling", SqlDbType.Bit,1),
                    new SqlParameter("@SkuId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.SKU;
            parameters[2].Value = model.Weight;
            parameters[3].Value = model.Stock;
            parameters[4].Value = model.AlertStock;
            parameters[5].Value = model.CostPrice;
            parameters[6].Value = model.SalePrice;
            parameters[7].Value = model.Upselling;
            parameters[8].Value = model.SkuId;

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
        public bool Delete(long SkuId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_SKUs ");
            strSql.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt)
            };
            parameters[0].Value = SkuId;

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
        public bool DeleteList(string SkuIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_SKUs ");
            strSql.Append(" WHERE SkuId in (" + SkuIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Products.SKUInfo GetModel(long SkuId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling FROM Shop_SKUs ");
            strSql.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt)
            };
            parameters[0].Value = SkuId;

            Maticsoft.Model.Shop.Products.SKUInfo model = new Maticsoft.Model.Shop.Products.SKUInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SkuId"] != null && ds.Tables[0].Rows[0]["SkuId"].ToString() != "")
                {
                    model.SkuId = long.Parse(ds.Tables[0].Rows[0]["SkuId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"] != null && ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SKU"] != null && ds.Tables[0].Rows[0]["SKU"].ToString() != "")
                {
                    model.SKU = ds.Tables[0].Rows[0]["SKU"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    model.Weight = int.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Stock"] != null && ds.Tables[0].Rows[0]["Stock"].ToString() != "")
                {
                    model.Stock = int.Parse(ds.Tables[0].Rows[0]["Stock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlertStock"] != null && ds.Tables[0].Rows[0]["AlertStock"].ToString() != "")
                {
                    model.AlertStock = int.Parse(ds.Tables[0].Rows[0]["AlertStock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostPrice"] != null && ds.Tables[0].Rows[0]["CostPrice"].ToString() != "")
                {
                    model.CostPrice = decimal.Parse(ds.Tables[0].Rows[0]["CostPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SalePrice"] != null && ds.Tables[0].Rows[0]["SalePrice"].ToString() != "")
                {
                    model.SalePrice = decimal.Parse(ds.Tables[0].Rows[0]["SalePrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Upselling"] != null && ds.Tables[0].Rows[0]["Upselling"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Upselling"].ToString() == "1") || (ds.Tables[0].Rows[0]["Upselling"].ToString().ToLower() == "true"))
                    {
                        model.Upselling = true;
                    }
                    else
                    {
                        model.Upselling = false;
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling ");
            strSql.Append(" FROM Shop_SKUs ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling ");
            strSql.Append(" FROM Shop_SKUs ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_SKUs ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                strSql.Append("ORDER BY T." + orderby);
            }
            else
            {
                strSql.Append("ORDER BY T.SkuId desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_SKUs T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
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
            parameters[0].Value = "Shop_SKUs";
            parameters[1].Value = "SkuId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region NewMethod
        /// <summary>
        /// 分页获取SKU胶塑列表
        /// </summary>
        public DataSet GetSKUListByPage(string strWhere, string orderby, int startIndex, int endIndex, out int dataCount,long productId)
        {
            //            StringBuilder strSql = new StringBuilder();
            //            strSql.Append("SELECT * FROM ( ");
            //            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            //            if (!string.IsNullOrWhiteSpace(orderby))
            //            {
            //                strSql.Append("ORDER BY T." + orderby);
            //            }
            //            else
            //            {
            //                strSql.Append("ORDER BY T.SkuId desc");
            //            }
            //            strSql.Append(")AS Row, T.*  FROM " +
            //                          @"(SELECT SP.* ,
            //                                    SI.AttributeId ,
            //                                    SI.ValueId ,
            //                                    AV.ValueStr
            //                            FROM    ( SELECT    S.* ,
            //                                                P.ProductName
            //                                        FROM      Shop_SKUs S
            //                                                LEFT JOIN Shop_Products P ON S.ProductId = P.ProductId");
            //            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            //            {
            //                strSql.Append(" WHERE " + strWhere);
            //            }
            //            strSql.Append(@" ) SP ,
            //                                    Shop_SKUItems SI ,
            //                                    Shop_AttributeValues AV
            //                            WHERE   SP.SkuId = SI.SkuId AND AV.AttributeId = SI.AttributeId
            //                            AND AV.ValueId = SI.ValueId) T ");
            //            strSql.Append(" ) TT");
            //            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            //            return DbHelperSQL.Query(strSql.ToString());
            //if (!string.IsNullOrWhiteSpace(strWhere))
            //{
            //    strWhere = strWhere.Insert(0, " AND ");
            //}
            SqlParameter[] parameters = {
                                            new SqlParameter("@SqlWhere", SqlDbType.NVarChar, 4000),
                                            new SqlParameter("@OrderBy", SqlDbType.NVarChar, 1000),
                                            new SqlParameter("@StartIndex", SqlDbType.Int, 4),
                                            new SqlParameter("@EndIndex", SqlDbType.Int, 4),
                                            new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                                            DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)
                                        };
            parameters[0].Value = strWhere;
            parameters[1].Value = orderby;
            parameters[2].Value = startIndex;
            parameters[3].Value = endIndex;
            parameters[4].Value = productId;
            return DbHelperSQL.RunProcedure("sp_Shop_ProductSkuInfo_Get", parameters, "ProductSkuInfo", out dataCount);
        }


        public DataSet PrductsSkuInfo(long prductId)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT A.*,B.SpecId,AttributeId,B.ValueId,ValueStr  ");
            //strSql.Append("FROM Shop_SKUs A ");
            //strSql.Append("LEFT JOIN (SELECT C.SpecId,AttributeId,c.ValueId,ValueStr,SkuId FROM Shop_SKUItems C ");
            //strSql.Append("LEFT  JOIN Shop_SKURelation D ON  C.SpecId = D.SpecId)B ON A.SkuId = B.SkuId ");
            strSql.Append("SELECT * FROM  Shop_SKUs ");
            strSql.Append("WHERE ProductId=@ProducId ");
            SqlParameter[] parameters = { 
                                        new SqlParameter("@ProducId",SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = prductId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <remarks>prductId 编辑时使用, 排除自己</remarks>
        public bool Exists(string skuCode, long prductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_SKUs");
            strSql.Append(" WHERE SKU=@SkuCode");
            if (prductId > 0)
            {
                strSql.Append(" AND ProductId<>" + prductId);
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuCode", SqlDbType.NVarChar)
            };
            parameters[0].Value = skuCode;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public int GetStockById(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(Stock)Stock FROM Shop_SKUs ");
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
                return Convert.ToInt32(obj);
            }
        }

        public int GetStockBySKU(string SKU,bool IsOpenAS)
        {

            StringBuilder strSql = new StringBuilder();
            if (IsOpenAS)
            {
                strSql.Append("SELECT SUM(Stock-AlertStock)AlertStock FROM Shop_SKUs ");
            }
            else
            {
                strSql.Append("SELECT SUM(Stock)Stock FROM Shop_SKUs ");
            }
         
            strSql.Append("WHERE SKU=@SKU ");

            SqlParameter[] parameters = {
					new SqlParameter("@SKU", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = SKU;
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.SKUInfo GetModelBySKU(string sku)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,CostPrice2,SalePrice,Upselling FROM Shop_SKUs ");
            strSql.Append(" WHERE SKU=@SKU");
            SqlParameter[] parameters = {
                    new SqlParameter("@SKU", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = sku;

            Maticsoft.Model.Shop.Products.SKUInfo model = new Maticsoft.Model.Shop.Products.SKUInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SkuId"] != null && ds.Tables[0].Rows[0]["SkuId"].ToString() != "")
                {
                    model.SkuId = long.Parse(ds.Tables[0].Rows[0]["SkuId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"] != null && ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SKU"] != null && ds.Tables[0].Rows[0]["SKU"].ToString() != "")
                {
                    model.SKU = ds.Tables[0].Rows[0]["SKU"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    model.Weight = int.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Stock"] != null && ds.Tables[0].Rows[0]["Stock"].ToString() != "")
                {
                    model.Stock = int.Parse(ds.Tables[0].Rows[0]["Stock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlertStock"] != null && ds.Tables[0].Rows[0]["AlertStock"].ToString() != "")
                {
                    model.AlertStock = int.Parse(ds.Tables[0].Rows[0]["AlertStock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostPrice"] != null && ds.Tables[0].Rows[0]["CostPrice"].ToString() != "")
                {
                    model.CostPrice = decimal.Parse(ds.Tables[0].Rows[0]["CostPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostPrice2"] != null && ds.Tables[0].Rows[0]["CostPrice2"].ToString() != "")
                {
                    model.CostPrice2 = decimal.Parse(ds.Tables[0].Rows[0]["CostPrice2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SalePrice"] != null && ds.Tables[0].Rows[0]["SalePrice"].ToString() != "")
                {
                    model.SalePrice = decimal.Parse(ds.Tables[0].Rows[0]["SalePrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Upselling"] != null && ds.Tables[0].Rows[0]["Upselling"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Upselling"].ToString() == "1") || (ds.Tables[0].Rows[0]["Upselling"].ToString().ToLower() == "true"))
                    {
                        model.Upselling = true;
                    }
                    else
                    {
                        model.Upselling = false;
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
        }


       
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <remarks>添加组合商品时，判断这个sku是否是自己的</remarks>
        public bool ExistsEx(string SKU, long prductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_SKUs");
            strSql.Append(" WHERE SKU=@SKU");
            strSql.Append(" AND ProductId=" + prductId);
            SqlParameter[] parameters = {
                    new SqlParameter("@SKU", SqlDbType.NVarChar)
            };
            parameters[0].Value = SKU;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListInnerJoinProd(string strWhere)
        {
            //SELECT s.*,p.ProductName AS  Shop_Products,p.ThumbnailUrl1 AS ThumbnailUrl1   FROM Shop_SKUs s INNER JOIN     Shop_Products p ON  s.ProductId=p.ProductId

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT s.*,p.ProductName AS  ProductName ,p.ThumbnailUrl1 AS ThumbnailUrl1   ");
            strSql.Append(" FROM Shop_SKUs s");
            strSql.Append(" INNER JOIN  Shop_Products p ON  s.ProductId=p.ProductId ");   
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取SKU数据列表
        /// </summary>
        public DataSet GetSKUList(string strWhere,int AccessoriesId ,  string orderby , long productId)
        { 
            SqlParameter[] parameters = {
                                            new SqlParameter("@SqlWhere", SqlDbType.NVarChar, 4000),
                                            new SqlParameter("@OrderBy", SqlDbType.NVarChar, 1000), 
                                            new SqlParameter("@AccessoriesId", SqlDbType.Int), 
                                            new SqlParameter("@ProductId", SqlDbType.BigInt, 8)
                                        };
            parameters[0].Value = strWhere;
            parameters[1].Value = orderby;
            parameters[2].Value = AccessoriesId;
            parameters[3].Value = productId;
            return DbHelperSQL.RunProcedure("sp_Shop_ProductSkuInfo_NotPage_GetProdAcce", parameters, "ProductSkuInfoGetProdAcce");
        }

        /// <summary>
        /// 根据商品分类 获取SKU
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public DataSet GetSKUListByCid(int Cid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * ");
            strSql.Append(" FROM Shop_SKUs K ");
            strSql.Append(" WHERE   EXISTS ( SELECT ProductId FROM   Shop_Products P WHERE  P.SaleStatus = 1  ");
            //查询分类
            if (Cid > 0)
            {
                strSql.AppendFormat(
                    " AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =P.ProductId  ");
                strSql.AppendFormat(
              "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
              Cid);
                strSql.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            strSql.Append("   AND P.ProductId = K.ProductId ) ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 根据商品id获取该商品的sku数
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int skuCount(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT COUNT(*)  FROM  Shop_SKUs ");
            strSql.Append(" where ProductId =@ProductId ");
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
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 通过传入商品名称，查找ProductCode再找到对应的库存
        /// </summary>
        /// <param name="SKU"></param>
        /// <param name="IsOpenAS"></param>
        /// <returns></returns>
        public int GetStockByProductName(string ProductName)
        {

            StringBuilder strSql = new StringBuilder();
            if (ProductName != null)
            {
                strSql.Append(@"SELECT stock FROM 
(SELECT ProductCode,ProductName,ProductId FROM dbo.Shop_Products  WHERE ProductName  = @ProductName )AS p
INNER JOIN  (SELECT ProductId,SKU,Stock FROM dbo.Shop_SKUs) s 
ON p.ProductCode = s.SKU");

                SqlParameter[] parameters = {
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200)
			    };
                parameters[0].Value = ProductName;
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
            return 0;
        }

        #endregion


        public DataSet ProductsSkuInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM  Shop_SKUs ");
            //strSql.Append("WHERE @Where ");
            if (string.IsNullOrEmpty(strWhere.Trim()))
            {
                strWhere = " 1=1";
            }
            //SqlParameter[] parameters = { new SqlParameter("@Where", SqlDbType.NVarChar) };
            //parameters[0].Value = strWhere;
            strSql.Append("WHERE " + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

