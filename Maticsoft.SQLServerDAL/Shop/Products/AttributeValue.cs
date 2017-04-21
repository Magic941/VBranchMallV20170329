/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：AttributeValues.cs
// 文件功能描述：
// 
// 创建标识： [Ben]              2012/06/11 20:36:22
// 修改标识： [Rock]            2012年6月14日 17:00:46
// 修改描述：新增 【AttributeValueManage】方法
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
    /// 数据访问类:AttributeValues
    /// </summary>
    public partial class AttributeValue : IAttributeValue
    {
        public AttributeValue()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ValueId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_AttributeValues");
            strSql.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] parameters = {
					new SqlParameter("@ValueId", SqlDbType.BigInt)
			};
            parameters[0].Value = ValueId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_AttributeValues(");
            strSql.Append("AttributeId,DisplaySequence,ValueStr,ImageUrl)");
            strSql.Append(" VALUES (");
            strSql.Append("@AttributeId,@DisplaySequence,@ValueStr,@ImageUrl)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@ValueStr", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,255)};
            parameters[0].Value = model.AttributeId;
            parameters[1].Value = model.DisplaySequence;
            parameters[2].Value = model.ValueStr;
            parameters[3].Value = model.ImageUrl;

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
        public bool Update(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_AttributeValues SET ");
            strSql.Append("AttributeId=@AttributeId,");
            strSql.Append("DisplaySequence=@DisplaySequence,");
            strSql.Append("ValueStr=@ValueStr,");
            strSql.Append("ImageUrl=@ImageUrl");
            strSql.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] parameters = {
					new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@ValueStr", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@ValueId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.AttributeId;
            parameters[1].Value = model.DisplaySequence;
            parameters[2].Value = model.ValueStr;
            parameters[3].Value = model.ImageUrl;
            parameters[4].Value = model.ValueId;

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
        public bool Delete(long ValueId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_AttributeValues ");
            strSql.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] parameters = {
					new SqlParameter("@ValueId", SqlDbType.BigInt)
			};
            parameters[0].Value = ValueId;

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
        public bool DeleteList(string ValueIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_AttributeValues ");
            strSql.Append(" WHERE ValueId in (" + ValueIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Products.AttributeValue GetModel(long ValueId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl FROM Shop_AttributeValues ");
            strSql.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] parameters = {
					new SqlParameter("@ValueId", SqlDbType.BigInt)
			};
            parameters[0].Value = ValueId;

            Maticsoft.Model.Shop.Products.AttributeValue model = new Maticsoft.Model.Shop.Products.AttributeValue();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ValueId"] != null && ds.Tables[0].Rows[0]["ValueId"].ToString() != "")
                {
                    model.ValueId = long.Parse(ds.Tables[0].Rows[0]["ValueId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AttributeId"] != null && ds.Tables[0].Rows[0]["AttributeId"].ToString() != "")
                {
                    model.AttributeId = long.Parse(ds.Tables[0].Rows[0]["AttributeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DisplaySequence"] != null && ds.Tables[0].Rows[0]["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(ds.Tables[0].Rows[0]["DisplaySequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ValueStr"] != null && ds.Tables[0].Rows[0]["ValueStr"].ToString() != "")
                {
                    model.ValueStr = ds.Tables[0].Rows[0]["ValueStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ImageUrl"] != null && ds.Tables[0].Rows[0]["ImageUrl"].ToString() != "")
                {
                    model.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
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
            strSql.Append("SELECT ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl ");
            strSql.Append(" FROM Shop_AttributeValues ");
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
            strSql.Append(" ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl ");
            strSql.Append(" FROM Shop_AttributeValues ");
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
            strSql.Append("SELECT COUNT(1) FROM Shop_AttributeValues ");
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
            strSql.Append(")AS Row, T.*  FROM Shop_AttributeValues T ");
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
            parameters[0].Value = "Shop_AttributeValues";
            parameters[1].Value = "ValueId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region NewMethod
        public bool AttributeValueManage(Model.Shop.Products.AttributeValue model, Model.Shop.Products.DataProviderAction Action)
        {
            int rows = 0;
            SqlParameter[] param = { 
                                   new SqlParameter("@Action",SqlDbType.Int),
                                   new SqlParameter("@ValueId",SqlDbType.BigInt),
                                   new SqlParameter("@AttributeId",SqlDbType.BigInt),
                                   new SqlParameter("@ValueStr",SqlDbType.NVarChar),
                                   new SqlParameter("@ImageUrl",SqlDbType.NVarChar)
                                   };
            param[0].Value = (int)Action;
            param[1].Value = model.ValueId;
            param[2].Value = model.AttributeId;
            param[3].Value = model.ValueStr;
            param[4].Value = model.ImageUrl;
            DbHelperSQL.RunProcedure("sp_Shop_AttributesValuesCreateEditDelete", param, out rows);
            if (rows > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByAttribute(long AttributeId)
        {
            return GetList(" AttributeId=" + AttributeId.ToString() + " ORDER BY DisplaySequence ");
        }

        public bool DeleteImage(long valueId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_AttributeValues SET ImageUrl=N''");
            strSql.Append("WHERE ValueId=@ValueId");
            SqlParameter[] param ={
                            new SqlParameter("@ValueId",SqlDbType.BigInt)
                            };
            param[0].Value = valueId;
            if (DbHelperSQL.ExecuteSql(strSql.ToString(),param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        public DataSet GetList(long? AttributeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl ");
            strSql.Append(" FROM Shop_AttributeValues ");
            if (AttributeId.HasValue)
            {
                strSql.Append("WHERE AttributeId=@AttributeId");
                SqlParameter[] param ={
                            new SqlParameter("@AttributeId",SqlDbType.BigInt)
                            };
                param[0].Value = AttributeId.Value;
                return DbHelperSQL.Query(strSql.ToString(),param);
            }
            else
            {
                return DbHelperSQL.Query(strSql.ToString());
            }
        }

        public DataSet GetAttributeValue(int ? cateID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM Shop_AttributeValues ");
            strSql.Append("WHERE AttributeId IN ( ");
            strSql.Append("SELECT DISTINCT AttributeId FROM Shop_ProductAttributes ");
            strSql.Append("WHERE ProductId IN(SELECT ProductId FROM Shop_Products ");
            if (cateID.HasValue)
            {
                strSql.AppendFormat("WHERE CategoryId ={0} ", cateID.Value);
            }
            strSql.Append(")) ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据商品listid和属性id获取商品属性值
        /// </summary>
        /// <param name="PordIDList">商品idList</param>
        ///  <param name="attrid">属性id</param>
        /// <returns></returns>
        public DataSet GetAttrValue(string PordIDList,int attrid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  SELECT  PA.ProductId  as ProductId,v.AttributeId,  V.ValueId ,V.ValueStr");
            strSql.Append(" FROM  Shop_ProductAttributes PA");
            strSql.Append("  LEFT JOIN Shop_AttributeValues V ON PA.ValueId = V.ValueId ");
            strSql.AppendFormat(" WHERE  PA.ProductId IN ( {0}) AND pa.AttributeId={1}  ORDER BY PA.ProductId DESC ", PordIDList, attrid);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_AttributeValues");
            if (!String.IsNullOrWhiteSpace(strWhere))
            {
                strSql.AppendFormat(" where {0} " ,strWhere);
            }
            return DbHelperSQL.Exists(strSql.ToString());
        }
    }
}

