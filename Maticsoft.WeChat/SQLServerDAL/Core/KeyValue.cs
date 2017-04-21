/**
* KeyValue.cs
*
* 功 能： N/A
* 类 名： KeyValue
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:26   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:KeyValue
	/// </summary>
	public partial class KeyValue:IKeyValue
	{
		public KeyValue()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ValueId", "WeChat_KeyValue"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ValueId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WeChat_KeyValue");
			strSql.Append(" where ValueId=@ValueId");
			SqlParameter[] parameters = {
					new SqlParameter("@ValueId", SqlDbType.Int,4)
			};
			parameters[0].Value = ValueId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.WeChat.Model.Core.KeyValue model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WeChat_KeyValue(");
			strSql.Append("RuleId,Value,MatchType)");
			strSql.Append(" values (");
			strSql.Append("@RuleId,@Value,@MatchType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@Value", SqlDbType.NVarChar,-1),
					new SqlParameter("@MatchType", SqlDbType.Int,4)};
			parameters[0].Value = model.RuleId;
			parameters[1].Value = model.Value;
			parameters[2].Value = model.MatchType;

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
		public bool Update(Maticsoft.WeChat.Model.Core.KeyValue model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WeChat_KeyValue set ");
			strSql.Append("RuleId=@RuleId,");
			strSql.Append("Value=@Value,");
			strSql.Append("MatchType=@MatchType");
			strSql.Append(" where ValueId=@ValueId");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@Value", SqlDbType.NVarChar,-1),
					new SqlParameter("@MatchType", SqlDbType.Int,4),
					new SqlParameter("@ValueId", SqlDbType.Int,4)};
			parameters[0].Value = model.RuleId;
			parameters[1].Value = model.Value;
			parameters[2].Value = model.MatchType;
			parameters[3].Value = model.ValueId;

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
		public bool Delete(int ValueId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WeChat_KeyValue ");
			strSql.Append(" where ValueId=@ValueId");
			SqlParameter[] parameters = {
					new SqlParameter("@ValueId", SqlDbType.Int,4)
			};
			parameters[0].Value = ValueId;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string ValueIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WeChat_KeyValue ");
			strSql.Append(" where ValueId in ("+ValueIdlist + ")  ");
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
		public Maticsoft.WeChat.Model.Core.KeyValue GetModel(int ValueId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ValueId,RuleId,Value,MatchType from WeChat_KeyValue ");
			strSql.Append(" where ValueId=@ValueId");
			SqlParameter[] parameters = {
					new SqlParameter("@ValueId", SqlDbType.Int,4)
			};
			parameters[0].Value = ValueId;

			Maticsoft.WeChat.Model.Core.KeyValue model=new Maticsoft.WeChat.Model.Core.KeyValue();
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
		public Maticsoft.WeChat.Model.Core.KeyValue DataRowToModel(DataRow row)
		{
			Maticsoft.WeChat.Model.Core.KeyValue model=new Maticsoft.WeChat.Model.Core.KeyValue();
			if (row != null)
			{
				if(row["ValueId"]!=null && row["ValueId"].ToString()!="")
				{
					model.ValueId=int.Parse(row["ValueId"].ToString());
				}
				if(row["RuleId"]!=null && row["RuleId"].ToString()!="")
				{
					model.RuleId=int.Parse(row["RuleId"].ToString());
				}
				if(row["Value"]!=null)
				{
					model.Value=row["Value"].ToString();
				}
				if(row["MatchType"]!=null && row["MatchType"].ToString()!="")
				{
					model.MatchType=int.Parse(row["MatchType"].ToString());
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
			strSql.Append("select ValueId,RuleId,Value,MatchType ");
			strSql.Append(" FROM WeChat_KeyValue ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
			strSql.Append(" ValueId,RuleId,Value,MatchType ");
			strSql.Append(" FROM WeChat_KeyValue ");
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
			strSql.Append("select count(1) FROM WeChat_KeyValue ");
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
				strSql.Append("order by T.ValueId desc");
			}
			strSql.Append(")AS Row, T.*  from WeChat_KeyValue T ");
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
			parameters[0].Value = "WeChat_KeyValue";
			parameters[1].Value = "ValueId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

        public bool UpdateType(int valueId, int type)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_KeyValue set ");
            strSql.Append("MatchType=@MatchType");
            strSql.Append(" where ValueId=@ValueId");
            SqlParameter[] parameters = {
					new SqlParameter("@MatchType", SqlDbType.Int,4),
					new SqlParameter("@ValueId", SqlDbType.Int,4)};
            parameters[0].Value = type;
            parameters[1].Value = valueId;

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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string  value,string openId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_KeyValue");
            strSql.Append(" where Value=@Value");
            SqlParameter[] parameters = {
						new SqlParameter("@Value", SqlDbType.NVarChar,-1),
                        new SqlParameter("@OpenId", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = value;
            parameters[1].Value = openId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public DataSet GetValueList(string openId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("  select *  from WeChat_KeyValue V join WeChat_KeyRule R  on V.RuleId=R.RuleId and R.OpenId=@OpenId ");
            SqlParameter[] parameters = {
						new SqlParameter("@OpenId", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = openId;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
	    #endregion  ExtensionMethod
	}
}

