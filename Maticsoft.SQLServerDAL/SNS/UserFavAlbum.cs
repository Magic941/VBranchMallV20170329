/**
* UserFavAlbum.cs
*
* 功 能： N/A
* 类 名： UserFavAlbum
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:03   N/A    初版
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
using System.Collections.Generic;
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:UserFavAlbum
	/// </summary>
	public partial class UserFavAlbum:IUserFavAlbum
	{
		public UserFavAlbum()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AlbumID,int UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_UserFavAlbum");
			strSql.Append(" where AlbumID=@AlbumID and UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = AlbumID;
			parameters[1].Value = UserID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.UserFavAlbum model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_UserFavAlbum(");
			strSql.Append("AlbumID,UserID,Tags,CreatedDate)");
			strSql.Append(" values (");
			strSql.Append("@AlbumID,@UserID,@Tags,@CreatedDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
			parameters[0].Value = model.AlbumID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.Tags;
			parameters[3].Value = model.CreatedDate;

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
		public bool Update(Maticsoft.Model.SNS.UserFavAlbum model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_UserFavAlbum set ");
			strSql.Append("Tags=@Tags,");
			strSql.Append("CreatedDate=@CreatedDate");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = model.Tags;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.ID;
			parameters[3].Value = model.AlbumID;
			parameters[4].Value = model.UserID;

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
			strSql.Append("delete from SNS_UserFavAlbum ");
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int AlbumID,int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_UserFavAlbum ");
			strSql.Append(" where AlbumID=@AlbumID and UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = AlbumID;
			parameters[1].Value = UserID;

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
			strSql.Append("delete from SNS_UserFavAlbum ");
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
		public Maticsoft.Model.SNS.UserFavAlbum GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,AlbumID,UserID,Tags,CreatedDate from SNS_UserFavAlbum ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.SNS.UserFavAlbum model=new Maticsoft.Model.SNS.UserFavAlbum();
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
		public Maticsoft.Model.SNS.UserFavAlbum DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.UserFavAlbum model=new Maticsoft.Model.SNS.UserFavAlbum();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["AlbumID"]!=null && row["AlbumID"].ToString()!="")
				{
					model.AlbumID=int.Parse(row["AlbumID"].ToString());
				}
				if(row["UserID"]!=null && row["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(row["UserID"].ToString());
				}
				if(row["Tags"]!=null)
				{
					model.Tags=row["Tags"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
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
			strSql.Append("select ID,AlbumID,UserID,Tags,CreatedDate ");
			strSql.Append(" FROM SNS_UserFavAlbum ");
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
			strSql.Append(" ID,AlbumID,UserID,Tags,CreatedDate ");
			strSql.Append(" FROM SNS_UserFavAlbum ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append(" order by ID desc");
            }
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SNS_UserFavAlbum ");
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
			strSql.Append(")AS Row, T.*  from SNS_UserFavAlbum T ");
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
			parameters[0].Value = "SNS_UserFavAlbum";
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
        #region  收藏专辑

        public int FavAlbum(int AlbumId, int UserId)
        {
          
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserFavAlbum(");
            strSql.Append("AlbumID,UserID,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@AlbumID,@UserID,@CreatedDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = AlbumId;
            parameters[1].Value = UserId;
            parameters[2].Value = DateTime.Now;
          
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            StringBuilder strSql2;
            strSql2 = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update SNS_UserAlbums set ");
            strSql1.Append("FavouriteCount=FavouriteCount+1 ");
            strSql1.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters1 = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)};
            parameters1[0].Value = AlbumId;
            cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);
            return DbHelperSQL.ExecuteSqlTran(sqllist);
             

        } 
        #endregion

        #region 取消收藏专辑

        public int UnFavAlbum(int AlbumId, int UserId)
        {
            //转发的时候除过转发的内容以外，还应该直接转发动态的id和原始动态id，对他们的转发次数分别加1
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserFavAlbum ");
            strSql.Append(" where AlbumID=@AlbumID AND UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
                    new SqlParameter("@UserID",SqlDbType.Int,4)
			};
            parameters[0].Value = AlbumId;
            parameters[1].Value = UserId;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            StringBuilder strSql2;
            strSql2 = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update SNS_UserAlbums set ");
            strSql1.Append("FavouriteCount=FavouriteCount-1 ");
            strSql1.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters1 = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)};
            parameters[0].Value = AlbumId;
            cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);
          return   DbHelperSQL.ExecuteSqlTran(sqllist);
         
        }
        #endregion

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SELECT SNSFA.*,SNSUA.AlbumName,AU.NickName ");
            strSql.Append(" FROM SNS_UserFavAlbum SNSFA ");
            strSql.Append(" LEFT JOIN SNS_UserAlbums SNSUA ON SNSUA.AlbumID=SNSFA.AlbumID ");
            strSql.Append(" LEFT JOIN Accounts_Users AU ON AU.UserID=SNSFA.UserID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append(" order by ID desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

  
		#endregion  ExtensionMethod
	}
}

