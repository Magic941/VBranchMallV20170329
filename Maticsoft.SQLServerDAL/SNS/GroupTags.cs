/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：GroupTags.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/10/25 11:53:32
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
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:GroupTags
	/// </summary>
	public partial class GroupTags:IGroupTags
	{
		public GroupTags()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TagID", "SNS_GroupTags"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TagID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM SNS_GroupTags");
			strSql.Append(" WHERE TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
			parameters[0].Value = TagID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.GroupTags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("INSERT INTO SNS_GroupTags(");
			strSql.Append("TagName,IsRecommand,Status)");
			strSql.Append(" VALUES (");
			strSql.Append("@TagName,@IsRecommand,@Status)");
			strSql.Append(";SELECT @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRecommand", SqlDbType.SmallInt,2),
					new SqlParameter("@Status", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.TagName;
			parameters[1].Value = model.IsRecommand;
			parameters[2].Value = model.Status;

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
		public bool Update(Maticsoft.Model.SNS.GroupTags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE SNS_GroupTags SET ");
			strSql.Append("TagName=@TagName,");
			strSql.Append("IsRecommand=@IsRecommand,");
			strSql.Append("Status=@Status");
			strSql.Append(" WHERE TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRecommand", SqlDbType.SmallInt,2),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@TagID", SqlDbType.Int,4)};
			parameters[0].Value = model.TagName;
			parameters[1].Value = model.IsRecommand;
			parameters[2].Value = model.Status;
			parameters[3].Value = model.TagID;

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
		public bool Delete(int TagID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM SNS_GroupTags ");
			strSql.Append(" WHERE TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
			parameters[0].Value = TagID;

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
		public bool DeleteList(string TagIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM SNS_GroupTags ");
			strSql.Append(" WHERE TagID in ("+TagIDlist + ")  ");
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
		public Maticsoft.Model.SNS.GroupTags GetModel(int TagID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT  TOP 1 TagID,TagName,IsRecommand,Status FROM SNS_GroupTags ");
			strSql.Append(" WHERE TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
			parameters[0].Value = TagID;

			Maticsoft.Model.SNS.GroupTags model=new Maticsoft.Model.SNS.GroupTags();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TagID"]!=null && ds.Tables[0].Rows[0]["TagID"].ToString()!="")
				{
					model.TagID=int.Parse(ds.Tables[0].Rows[0]["TagID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TagName"]!=null && ds.Tables[0].Rows[0]["TagName"].ToString()!="")
				{
					model.TagName=ds.Tables[0].Rows[0]["TagName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsRecommand"]!=null && ds.Tables[0].Rows[0]["IsRecommand"].ToString()!="")
				{
					model.IsRecommand=int.Parse(ds.Tables[0].Rows[0]["IsRecommand"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Status"]!=null && ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT TagID,TagName,IsRecommand,Status ");
			strSql.Append(" FROM SNS_GroupTags ");
			if(!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT ");
			if(Top>0)
			{
				strSql.Append(" TOP "+Top.ToString());
			}
			strSql.Append(" TagID,TagName,IsRecommand,Status ");
			strSql.Append(" FROM SNS_GroupTags ");
			if(!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
            if (!string.IsNullOrEmpty(filedOrder.Trim()))
            {
                strSql.Append(" ORDER BY " + filedOrder);
            }
            else
            {
                strSql.Append(" ORDER BY TagID DESC");
            }
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM SNS_GroupTags ");
			if(!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
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
				strSql.Append("ORDER BY T." + orderby );
			}
			else
			{
				strSql.Append("ORDER BY T.TagID desc");
			}
			strSql.Append(")AS Row, T.*  FROM SNS_GroupTags T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
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
			parameters[0].Value = "SNS_GroupTags";
			parameters[1].Value = "TagID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/
		#endregion  Method

        #region MethodEx
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string TagName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SNS_GroupTags");
            strSql.Append(" WHERE TagName=@TagName");
            SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = TagName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TagID, string TagName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SNS_GroupTags");
            strSql.Append(" WHERE TagID<>@TagID AND TagName=@TagName");
            SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4),
                    new SqlParameter("@TagName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = TagID;
            parameters[1].Value = TagName;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupTags set ");
            strSql.AppendFormat(" IsRecommand={0} ", IsRecommand);
            strSql.AppendFormat(" where TagID IN({0})", IdList);

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupTags set ");
            strSql.AppendFormat(" Status={0} ", Status);
            strSql.AppendFormat(" where TagID IN({0})", IdList);

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
        #endregion

	}
}

