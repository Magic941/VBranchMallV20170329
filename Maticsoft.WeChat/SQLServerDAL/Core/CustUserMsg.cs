/**  版本信息模板在安装目录下，可自行修改。
* CustUserMsg.cs
*
* 功 能： N/A
* 类 名： CustUserMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/22 11:50:22   N/A    初版
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
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:CustUserMsg
	/// </summary>
	public partial class CustUserMsg:ICustUserMsg
	{
		public CustUserMsg()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long MsgId,string UserName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WeChat_CustUserMsg");
			strSql.Append(" where MsgId=@MsgId and UserName=@UserName ");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)			};
			parameters[0].Value = MsgId;
			parameters[1].Value = UserName;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.WeChat.Model.Core.CustUserMsg model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WeChat_CustUserMsg(");
			strSql.Append("MsgId,UserName)");
			strSql.Append(" values (");
			strSql.Append("@MsgId,@UserName)");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.MsgId;
			parameters[1].Value = model.UserName;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.CustUserMsg model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WeChat_CustUserMsg set ");
#warning 系统发现缺少更新的字段，请手工确认如此更新是否正确！ 
			strSql.Append("MsgId=@MsgId,");
			strSql.Append("UserName=@UserName");
			strSql.Append(" where MsgId=@MsgId and UserName=@UserName ");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.MsgId;
			parameters[1].Value = model.UserName;

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
		public bool Delete(long MsgId,string UserName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WeChat_CustUserMsg ");
			strSql.Append(" where MsgId=@MsgId and UserName=@UserName ");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)			};
			parameters[0].Value = MsgId;
			parameters[1].Value = UserName;

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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.CustUserMsg GetModel(long MsgId,string UserName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MsgId,UserName from WeChat_CustUserMsg ");
			strSql.Append(" where MsgId=@MsgId and UserName=@UserName ");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)			};
			parameters[0].Value = MsgId;
			parameters[1].Value = UserName;

			Maticsoft.WeChat.Model.Core.CustUserMsg model=new Maticsoft.WeChat.Model.Core.CustUserMsg();
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
		public Maticsoft.WeChat.Model.Core.CustUserMsg DataRowToModel(DataRow row)
		{
			Maticsoft.WeChat.Model.Core.CustUserMsg model=new Maticsoft.WeChat.Model.Core.CustUserMsg();
			if (row != null)
			{
				if(row["MsgId"]!=null && row["MsgId"].ToString()!="")
				{
					model.MsgId=long.Parse(row["MsgId"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
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
			strSql.Append("select MsgId,UserName ");
			strSql.Append(" FROM WeChat_CustUserMsg ");
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
			strSql.Append(" MsgId,UserName ");
			strSql.Append(" FROM WeChat_CustUserMsg ");
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
			strSql.Append("select count(1) FROM WeChat_CustUserMsg ");
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
				strSql.Append("order by T.UserName desc");
			}
			strSql.Append(")AS Row, T.*  from WeChat_CustUserMsg T ");
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
			parameters[0].Value = "WeChat_CustUserMsg";
			parameters[1].Value = "UserName";
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

