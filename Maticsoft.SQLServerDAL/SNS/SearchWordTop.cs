/**
* SearchWordTop.cs
*
* 功 能： N/A
* 类 名： SearchWordTop
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:45   N/A    初版
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
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:SearchWordTop
	/// </summary>
	public partial class SearchWordTop:ISearchWordTop
	{
		public SearchWordTop()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string HotWord)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_SearchWordTop");
            strSql.Append(" where HotWord=@HotWord");
			SqlParameter[] parameters = {
					new SqlParameter("@HotWord", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = HotWord;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.SearchWordTop model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_SearchWordTop(");
			strSql.Append("HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status)");
			strSql.Append(" values (");
			strSql.Append("@HotWord,@TimeUnit,@DateStart,@DateEnd,@SearchCount,@CreatedDate,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@HotWord", SqlDbType.NVarChar,100),
					new SqlParameter("@TimeUnit", SqlDbType.Int,4),
					new SqlParameter("@DateStart", SqlDbType.DateTime),
					new SqlParameter("@DateEnd", SqlDbType.DateTime),
					new SqlParameter("@SearchCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.HotWord;
			parameters[1].Value = model.TimeUnit;
			parameters[2].Value = model.DateStart;
			parameters[3].Value = model.DateEnd;
			parameters[4].Value = model.SearchCount;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.Status;

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
		public bool Update(Maticsoft.Model.SNS.SearchWordTop model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_SearchWordTop set ");
			strSql.Append("HotWord=@HotWord,");
			strSql.Append("TimeUnit=@TimeUnit,");
			strSql.Append("DateStart=@DateStart,");
			strSql.Append("DateEnd=@DateEnd,");
			strSql.Append("SearchCount=@SearchCount,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Status=@Status");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@HotWord", SqlDbType.NVarChar,100),
					new SqlParameter("@TimeUnit", SqlDbType.Int,4),
					new SqlParameter("@DateStart", SqlDbType.DateTime),
					new SqlParameter("@DateEnd", SqlDbType.DateTime),
					new SqlParameter("@SearchCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.HotWord;
			parameters[1].Value = model.TimeUnit;
			parameters[2].Value = model.DateStart;
			parameters[3].Value = model.DateEnd;
			parameters[4].Value = model.SearchCount;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.Status;
			parameters[7].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_SearchWordTop ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_SearchWordTop ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public Maticsoft.Model.SNS.SearchWordTop GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status from SNS_SearchWordTop ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.SNS.SearchWordTop model=new Maticsoft.Model.SNS.SearchWordTop();
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
		public Maticsoft.Model.SNS.SearchWordTop DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.SearchWordTop model=new Maticsoft.Model.SNS.SearchWordTop();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["HotWord"]!=null)
				{
					model.HotWord=row["HotWord"].ToString();
				}
				if(row["TimeUnit"]!=null && row["TimeUnit"].ToString()!="")
				{
					model.TimeUnit=int.Parse(row["TimeUnit"].ToString());
				}
				if(row["DateStart"]!=null && row["DateStart"].ToString()!="")
				{
					model.DateStart=DateTime.Parse(row["DateStart"].ToString());
				}
				if(row["DateEnd"]!=null && row["DateEnd"].ToString()!="")
				{
					model.DateEnd=DateTime.Parse(row["DateEnd"].ToString());
				}
				if(row["SearchCount"]!=null && row["SearchCount"].ToString()!="")
				{
					model.SearchCount=int.Parse(row["SearchCount"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
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
			strSql.Append("select ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status ");
			strSql.Append(" FROM SNS_SearchWordTop ");
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
			strSql.Append(" ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status ");
			strSql.Append(" FROM SNS_SearchWordTop ");
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
			strSql.Append("select count(1) FROM SNS_SearchWordTop ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_SearchWordTop T ");
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
			parameters[0].Value = "SNS_SearchWordTop";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status ,ROW_NUMBER() OVER( ORDER BY ID) AS Rank ");
            strSql.Append(" FROM SNS_SearchWordTop ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
		#endregion  ExtensionMethod
	}
}

