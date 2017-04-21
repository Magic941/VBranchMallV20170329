/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SKUItems.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:32
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
namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:SKUItem
    /// </summary>
    public partial class SKUItem : ISKUItem
    {
        public SKUItem()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SkuId, long AttributeId, long ValueId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_SKUItems");
            strSql.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt,8),
                    new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
                    new SqlParameter("@ValueId", SqlDbType.BigInt,8)			};
            parameters[0].Value = SkuId;
            parameters[1].Value = AttributeId;
            parameters[2].Value = ValueId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.SKUItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_SKUItems(");
            strSql.Append("SkuId,AttributeId,ValueId)");
            strSql.Append(" VALUES (");
            strSql.Append("@SkuId,@AttributeId,@ValueId)");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt,8),
                    new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
                    new SqlParameter("@ValueId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.SkuId;
            parameters[1].Value = model.AttributeId;
            parameters[2].Value = model.ValueId;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.SKUItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_SKUItems SET ");
            strSql.Append("SkuId=@SkuId,");
            strSql.Append("AttributeId=@AttributeId,");
            strSql.Append("ValueId=@ValueId");
            strSql.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt,8),
                    new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
                    new SqlParameter("@ValueId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.SkuId;
            parameters[1].Value = model.AttributeId;
            parameters[2].Value = model.ValueId;

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
        public bool Delete(long SkuId, long AttributeId, long ValueId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_SKUItems ");
            strSql.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt,8),
                    new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
                    new SqlParameter("@ValueId", SqlDbType.BigInt,8)			};
            parameters[0].Value = SkuId;
            parameters[1].Value = AttributeId;
            parameters[2].Value = ValueId;

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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.SKUItem GetModel(long SkuId, long AttributeId, long ValueId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 SkuId,AttributeId,ValueId FROM Shop_SKUItems ");
            strSql.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SkuId", SqlDbType.BigInt,8),
                    new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
                    new SqlParameter("@ValueId", SqlDbType.BigInt,8)			};
            parameters[0].Value = SkuId;
            parameters[1].Value = AttributeId;
            parameters[2].Value = ValueId;

            Maticsoft.Model.Shop.Products.SKUItem model = new Maticsoft.Model.Shop.Products.SKUItem();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SkuId"] != null && ds.Tables[0].Rows[0]["SkuId"].ToString() != "")
                {
                    model.SkuId = long.Parse(ds.Tables[0].Rows[0]["SkuId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AttributeId"] != null && ds.Tables[0].Rows[0]["AttributeId"].ToString() != "")
                {
                    model.AttributeId = long.Parse(ds.Tables[0].Rows[0]["AttributeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ValueId"] != null && ds.Tables[0].Rows[0]["ValueId"].ToString() != "")
                {
                    model.ValueId = long.Parse(ds.Tables[0].Rows[0]["ValueId"].ToString());
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
            strSql.Append("SELECT SkuId,AttributeId,ValueId ");
            strSql.Append(" FROM Shop_SKUItems ");
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
            strSql.Append(" SkuId,AttributeId,ValueId ");
            strSql.Append(" FROM Shop_SKUItems ");
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
            strSql.Append("SELECT COUNT(1) FROM Shop_SKUItems ");
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
                strSql.Append("ORDER BY T.ValueId desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_SKUItems T ");
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
            parameters[0].Value = "Shop_SKUItems";
            parameters[1].Value = "ValueId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        public DataSet GetSKUItem4AttrValByProductId(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT  SR.SkuId
      , SI.SpecId
      , SI.AttributeId
      , SI.ValueId
      , SI.ImageUrl
      , SI.ValueStr
      , SI.ProductId
      
      , AB.AttributeName
      , AB.DisplaySequence AS AB_DisplaySequence
      , AB.UsageMode
      , AB.UseAttributeImage
      , AB.UserDefinedPic
      
      , AV.DisplaySequence AS AV_DisplaySequence
      , AV.ValueStr AS AV_ValueStr
      , AV.ImageUrl AS AV_ImageUrl
FROM    Shop_SKUItems SI
        LEFT JOIN Shop_SKURelation SR ON SI.SpecId = SR.SpecId
        LEFT JOIN Shop_Attributes AB ON AB.AttributeId = SI.AttributeId
        LEFT JOIN Shop_AttributeValues AV ON SI.ValueId = AV.ValueId
WHERE   SI.ProductId = @ProductId
ORDER BY AB_DisplaySequence,SI.AttributeId,AV_DisplaySequence
");
            SqlParameter[] parameter = { 
                                           new SqlParameter("@ProductId",SqlDbType.BigInt,8)
                                       };
            parameter[0].Value = productId;
            return DbHelperSQL.Query(strSql.ToString(), parameter);
        }

        public DataSet GetSKUItem4AttrValBySkuId(long skuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT  SR.SkuId
      , SI.SpecId
      , SI.AttributeId
      , SI.ValueId
      , SI.ImageUrl
      , SI.ValueStr
      , SI.ProductId
      
      , AB.AttributeName
      , AB.DisplaySequence AS AB_DisplaySequence
      , AB.UsageMode
      , AB.UseAttributeImage
      , AB.UserDefinedPic
      
      , AV.DisplaySequence AS AV_DisplaySequence
      , AV.ValueStr AS AV_ValueStr
      , AV.ImageUrl AS AV_ImageUrl
FROM    Shop_SKUItems SI
        LEFT JOIN Shop_SKURelation SR ON SI.SpecId = SR.SpecId
        LEFT JOIN Shop_Attributes AB ON AB.AttributeId = SI.AttributeId
        LEFT JOIN Shop_AttributeValues AV ON SI.ValueId = AV.ValueId
WHERE   SR.SkuId = @SkuId
ORDER BY AB_DisplaySequence,SI.AttributeId,AV_DisplaySequence
");
            SqlParameter[] parameter = { 
                                           new SqlParameter("@SkuId",SqlDbType.BigInt,8)
                                       };
            parameter[0].Value = skuId;
            return DbHelperSQL.Query(strSql.ToString(), parameter);
        }

        public bool Exists(long? SkuId, long? AttributeId, long? ValueId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_SKUItems");
            strSql.Append(" WHERE 1=1 ");
            if (SkuId.HasValue)
            {
                strSql.Append(" AND SkuId=" + SkuId.Value);
            }
            if (AttributeId.HasValue)
            {
                strSql.Append(" AND AttributeId=" + AttributeId.Value);
            }
            if (ValueId.HasValue)
            {
                strSql.Append(" AND ValueId=" + ValueId.Value);
            }
            return DbHelperSQL.Exists(strSql.ToString());
        }

        public DataSet AttributeValuesInfo(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.*,B.UserDefinedPic  ");
            strSql.Append("FROM Shop_SKUItems A ");
            strSql.Append("LEFT JOIN Shop_Attributes  B ON A.AttributeId = B.AttributeId ");
            strSql.Append("WHERE ProductId=@ProductId ");
            SqlParameter[] parameter = { 
                                           new SqlParameter("@ProductId",SqlDbType.BigInt,8)
                                       };
            parameter[0].Value = productId;
            return DbHelperSQL.Query(strSql.ToString(), parameter);
        }
    }
}

