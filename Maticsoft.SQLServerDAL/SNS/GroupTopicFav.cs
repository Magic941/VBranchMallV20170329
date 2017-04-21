/**
* GroupTopicFav.cs
*
* 功 能： N/A
* 类 名： GroupTopicFav
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:42   N/A    初版
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
	/// 数据访问类:GroupTopicFav
	/// </summary>
	public partial class GroupTopicFav:IGroupTopicFav
	{
		public GroupTopicFav()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否已经存在
		/// </summary>
        public bool Exists(int TopicID,int CreatedUserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_GroupTopicFav");
            strSql.Append(" where CreatedUserID=@CreatedUserID and TopicID=@TopicID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),					
					new SqlParameter("@TopicID", SqlDbType.Int,4)
			};
            parameters[0].Value = CreatedUserID;
            parameters[1].Value = TopicID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.GroupTopicFav model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_GroupTopicFav(");
			strSql.Append("CreatedUserID,CreatedDate,TopicID,Tags,Remark)");
			strSql.Append(" values (");
			strSql.Append("@CreatedUserID,@CreatedDate,@TopicID,@Tags,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.CreatedUserID;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.TopicID;
			parameters[3].Value = model.Tags;
			parameters[4].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.SNS.GroupTopicFav model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_GroupTopicFav set ");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("TopicID=@TopicID,");
			strSql.Append("Tags=@Tags,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.CreatedUserID;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.TopicID;
			parameters[3].Value = model.Tags;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.ID;

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
			strSql.Append("delete from SNS_GroupTopicFav ");
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
			strSql.Append("delete from SNS_GroupTopicFav ");
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
		public Maticsoft.Model.SNS.GroupTopicFav GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CreatedUserID,CreatedDate,TopicID,Tags,Remark from SNS_GroupTopicFav ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.SNS.GroupTopicFav model=new Maticsoft.Model.SNS.GroupTopicFav();
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
		public Maticsoft.Model.SNS.GroupTopicFav DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.GroupTopicFav model=new Maticsoft.Model.SNS.GroupTopicFav();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CreatedUserID"]!=null && row["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(row["CreatedUserID"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["TopicID"]!=null && row["TopicID"].ToString()!="")
				{
					model.TopicID=int.Parse(row["TopicID"].ToString());
				}
				if(row["Tags"]!=null)
				{
					model.Tags=row["Tags"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
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
			strSql.Append("select ID,CreatedUserID,CreatedDate,TopicID,Tags,Remark ");
			strSql.Append(" FROM SNS_GroupTopicFav ");
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
			strSql.Append(" ID,CreatedUserID,CreatedDate,TopicID,Tags,Remark ");
			strSql.Append(" FROM SNS_GroupTopicFav ");
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
			strSql.Append("select count(1) FROM SNS_GroupTopicFav ");
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
			strSql.Append(")AS Row, T.*  from SNS_GroupTopicFav T ");
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
			parameters[0].Value = "SNS_GroupTopicFav";
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
        public bool AddEx(Maticsoft.Model.SNS.GroupTopicFav model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            #region 加入收藏表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_GroupTopicFav(");
            strSql.Append("CreatedUserID,CreatedDate,TopicID,Tags,Remark)");
            strSql.Append(" values (");
            strSql.Append("@CreatedUserID,@CreatedDate,@TopicID,@Tags,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.CreatedUserID;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.TopicID;
            parameters[3].Value = model.Tags;
            parameters[4].Value = model.Remark;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            #endregion


            #region    用户收藏数量+1
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update Accounts_UsersExp set FavTopicCount=FavTopicCount-1");
            strSql1.Append(" where UserID=@UserID  ");

            SqlParameter[] parameters1 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters1[0].Value = model.CreatedUserID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);
            #endregion

            

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
        
        
        }
		#endregion  ExtensionMethod
	}
}

