/**
* UserAlbumsType.cs
*
* 功 能： N/A
* 类 名： UserAlbumsType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:02   N/A    初版
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
	/// 数据访问类:UserAlbumsType
	/// </summary>
	public partial class UserAlbumsType:IUserAlbumsType
	{
		public UserAlbumsType()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AlbumsID,int TypeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_UserAlbumsType");
			strSql.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumsID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)			};
			parameters[0].Value = AlbumsID;
			parameters[1].Value = TypeID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SNS.UserAlbumsType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_UserAlbumsType(");
			strSql.Append("AlbumsID,TypeID,AlbumsUserID)");
			strSql.Append(" values (");
			strSql.Append("@AlbumsID,@TypeID,@AlbumsUserID)");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumsID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@AlbumsUserID", SqlDbType.Int,4)};
			parameters[0].Value = model.AlbumsID;
			parameters[1].Value = model.TypeID;
			parameters[2].Value = model.AlbumsUserID;

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
		public bool Update(Maticsoft.Model.SNS.UserAlbumsType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_UserAlbumsType set ");
			strSql.Append("AlbumsUserID=@AlbumsUserID");
			strSql.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumsUserID", SqlDbType.Int,4),
					new SqlParameter("@AlbumsID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)};
			parameters[0].Value = model.AlbumsUserID;
			parameters[1].Value = model.AlbumsID;
			parameters[2].Value = model.TypeID;

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
		public bool Delete(int AlbumsID,int TypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_UserAlbumsType ");
			strSql.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumsID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)			};
			parameters[0].Value = AlbumsID;
			parameters[1].Value = TypeID;

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
		public Maticsoft.Model.SNS.UserAlbumsType GetModel(int AlbumsID,int TypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AlbumsID,TypeID,AlbumsUserID from SNS_UserAlbumsType ");
			strSql.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumsID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)			};
			parameters[0].Value = AlbumsID;
			parameters[1].Value = TypeID;

			Maticsoft.Model.SNS.UserAlbumsType model=new Maticsoft.Model.SNS.UserAlbumsType();
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
		public Maticsoft.Model.SNS.UserAlbumsType DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.UserAlbumsType model=new Maticsoft.Model.SNS.UserAlbumsType();
			if (row != null)
			{
				if(row["AlbumsID"]!=null && row["AlbumsID"].ToString()!="")
				{
					model.AlbumsID=int.Parse(row["AlbumsID"].ToString());
				}
				if(row["TypeID"]!=null && row["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(row["TypeID"].ToString());
				}
				if(row["AlbumsUserID"]!=null && row["AlbumsUserID"].ToString()!="")
				{
					model.AlbumsUserID=int.Parse(row["AlbumsUserID"].ToString());
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
			strSql.Append("select AlbumsID,TypeID,AlbumsUserID ");
			strSql.Append(" FROM SNS_UserAlbumsType ");
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
			strSql.Append(" AlbumsID,TypeID,AlbumsUserID ");
			strSql.Append(" FROM SNS_UserAlbumsType ");
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
			strSql.Append("select count(1) FROM SNS_UserAlbumsType ");
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
				strSql.Append("order by T.TypeID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_UserAlbumsType T ");
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
			parameters[0].Value = "SNS_UserAlbumsType";
			parameters[1].Value = "TypeID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

	    public Maticsoft.Model.SNS.UserAlbumsType GetModelByUserId(int AlbumsID, int UserId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AlbumsID,TypeID,AlbumsUserID from SNS_UserAlbumsType ");
            strSql.Append(" where AlbumsID=@AlbumsID and AlbumsUserID=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumsID", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4)			};
            parameters[0].Value = AlbumsID;
            parameters[1].Value = UserId;

            Maticsoft.Model.SNS.UserAlbumsType model = new Maticsoft.Model.SNS.UserAlbumsType();
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateType(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbumsType set ");
            strSql.Append("TypeID=@TypeID");
            strSql.Append(" where AlbumsID=@AlbumsID and AlbumsUserID=@AlbumsUserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumsUserID", SqlDbType.Int,4),
					new SqlParameter("@AlbumsID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)};
            parameters[0].Value = model.AlbumsUserID;
            parameters[1].Value = model.AlbumsID;
            parameters[2].Value = model.TypeID;

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
	    #endregion  ExtensionMethod
	}
}

