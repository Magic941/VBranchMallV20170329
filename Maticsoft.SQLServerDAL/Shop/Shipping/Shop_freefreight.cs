/**  版本信息模板在安装目录下，可自行修改。
* Shop_freefreight.cs
*
* 功 能： N/A
* 类 名： Shop_freefreight
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/8/13 11:01:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Shipping;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    /// <summary>
	/// 数据访问类:Shop_freefreight
	/// </summary>
	public partial class Shop_freefreight:IShop_freefreight
	{
		public Shop_freefreight()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "Shop_freefreight"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_freefreight");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		public bool ExistsRegion(int regionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_freefreight");
            strSql.Append(" where regionid=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = regionId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Shipping.Shop_freefreight model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_freefreight(");
			strSql.Append("RegionId,createdate,createrid,totalmoney,StartDate,EndDate,ProductId,Quantity,FreeType)");
			strSql.Append(" values (");
			strSql.Append("@RegionId,@createdate,@createrid,@totalmoney,@StartDate,@EndDate,@ProductId,@Quantity,@FreeType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@createrid", SqlDbType.Int,4),
					new SqlParameter("@totalmoney", SqlDbType.Decimal,9),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@FreeType", SqlDbType.Int,4)};
			parameters[0].Value = model.RegionId;
			parameters[1].Value = model.createdate;
			parameters[2].Value = model.createrid;
			parameters[3].Value = model.totalmoney;
			parameters[4].Value = model.StartDate;
			parameters[5].Value = model.EndDate;
			parameters[6].Value = model.ProductId;
			parameters[7].Value = model.Quantity;
			parameters[8].Value = model.FreeType;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Shipping.Shop_freefreight model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_freefreight set ");
			strSql.Append("RegionId=@RegionId,");
			strSql.Append("createdate=@createdate,");
			strSql.Append("createrid=@createrid,");
			strSql.Append("totalmoney=@totalmoney,");
			strSql.Append("StartDate=@StartDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("ProductId=@ProductId,");
			strSql.Append("Quantity=@Quantity,");
			strSql.Append("FreeType=@FreeType");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@createrid", SqlDbType.Int,4),
					new SqlParameter("@totalmoney", SqlDbType.Decimal,9),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@FreeType", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.RegionId;
			parameters[1].Value = model.createdate;
			parameters[2].Value = model.createrid;
			parameters[3].Value = model.totalmoney;
			parameters[4].Value = model.StartDate;
			parameters[5].Value = model.EndDate;
			parameters[6].Value = model.ProductId;
			parameters[7].Value = model.Quantity;
			parameters[8].Value = model.FreeType;
			parameters[9].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_freefreight ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public bool Delete(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_freefreight ");
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.AppendFormat(" and {0}", strWhere);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_freefreight ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.Shop.Shipping.Shop_freefreight GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,RegionId,createdate,createrid,totalmoney,StartDate,EndDate,ProductId,Quantity,FreeType from Shop_freefreight ");
			strSql.Append(" where regionid=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.Shop.Shipping.Shop_freefreight model=new Maticsoft.Model.Shop.Shipping.Shop_freefreight();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public Maticsoft.Model.Shop.Shipping.Shop_freefreight DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Shipping.Shop_freefreight model=new Maticsoft.Model.Shop.Shipping.Shop_freefreight();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["RegionId"]!=null && row["RegionId"].ToString()!="")
				{
					model.RegionId=int.Parse(row["RegionId"].ToString());
				}
				if(row["createdate"]!=null && row["createdate"].ToString()!="")
				{
					model.createdate=DateTime.Parse(row["createdate"].ToString());
				}
				if(row["createrid"]!=null && row["createrid"].ToString()!="")
				{
					model.createrid=int.Parse(row["createrid"].ToString());
				}
				if(row["totalmoney"]!=null && row["totalmoney"].ToString()!="")
				{
					model.totalmoney=decimal.Parse(row["totalmoney"].ToString());
				}
				if(row["StartDate"]!=null && row["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(row["StartDate"].ToString());
				}
				if(row["EndDate"]!=null && row["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(row["EndDate"].ToString());
				}
				if(row["ProductId"]!=null && row["ProductId"].ToString()!="")
				{
					model.ProductId=long.Parse(row["ProductId"].ToString());
				}
				if(row["Quantity"]!=null && row["Quantity"].ToString()!="")
				{
					model.Quantity=int.Parse(row["Quantity"].ToString());
				}
				if(row["FreeType"]!=null && row["FreeType"].ToString()!="")
				{
					model.FreeType=int.Parse(row["FreeType"].ToString());
				}
                if (row["RegionId"] != null && row["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row.Table.Columns.Contains("RegionName"))
                {
                    if (row["RegionName"] != null && row["RegionName"].ToString() != "")
                    {
                        model.RegionName = row["RegionName"].ToString();
                    }
                }
                if (row.Table.Columns.Contains("username"))
                {
                    if (row["username"] != null && row["username"].ToString() != "")
                    {
                        model.username = row["username"].ToString();
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,RegionId,createdate,createrid,totalmoney,StartDate,EndDate,ProductId,Quantity,FreeType ");
			strSql.Append(" FROM Shop_freefreight ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT a.*,c.username, CASE WHEN  b.RegionName is NULL THEN '全国' ELSE RegionName END AS RegionName FROM Shop_freefreight a LEFT JOIN dbo.Ms_Regions b ON a.regionid=b.RegionId ");
            strSql.Append("INNER JOIN accounts_users c ON c.UserID=a.createrid AND a.FreeType=1 ");
            return DbHelperSQL.Query(strSql.ToString());
        }
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,RegionId,createdate,createrid,totalmoney,StartDate,EndDate,ProductId,Quantity,FreeType ");
			strSql.Append(" FROM Shop_freefreight ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Shop_freefreight ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_freefreight T ");
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
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Shop_freefreight";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

