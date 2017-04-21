/**
* PostsTopics.cs
*
* 功 能： N/A
* 类 名： PostsTopics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:48   N/A    初版
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
	/// 数据访问类:PostsTopics
	/// </summary>
	public partial class PostsTopics:IPostsTopics
	{
		public PostsTopics()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Title)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_PostsTopics");
			strSql.Append(" where Title=@Title ");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200)			
                                        };
			parameters[0].Value = Title;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SNS.PostsTopics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_PostsTopics(");
			strSql.Append("Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags)");
			strSql.Append(" values (");
			strSql.Append("@Title,@Description,@CreatedUserID,@CreatedNickName,@CreatedDate,@TopicsCount,@IsRecommend,@Sequence,@Tags)");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@TopicsCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.CreatedUserID;
			parameters[3].Value = model.CreatedNickName;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.TopicsCount;
			parameters[6].Value = model.IsRecommend;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.Tags;

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
		public bool Update(Maticsoft.Model.SNS.PostsTopics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_PostsTopics set ");
			strSql.Append("Description=@Description,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CreatedNickName=@CreatedNickName,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("TopicsCount=@TopicsCount,");
			strSql.Append("IsRecommend=@IsRecommend,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("Tags=@Tags");
			strSql.Append(" where Title=@Title ");
			SqlParameter[] parameters = {
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@TopicsCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@Title", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.Description;
			parameters[1].Value = model.CreatedUserID;
			parameters[2].Value = model.CreatedNickName;
			parameters[3].Value = model.CreatedDate;
			parameters[4].Value = model.TopicsCount;
			parameters[5].Value = model.IsRecommend;
			parameters[6].Value = model.Sequence;
			parameters[7].Value = model.Tags;
			parameters[8].Value = model.Title;

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
		public bool Delete(string Title)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_PostsTopics ");
			strSql.Append(" where Title=@Title ");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200)			};
			parameters[0].Value = Title;

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
		public bool DeleteList(string Titlelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_PostsTopics ");
			strSql.Append(" where Title in ("+Titlelist + ")  ");
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
		public Maticsoft.Model.SNS.PostsTopics GetModel(string Title)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags from SNS_PostsTopics ");
			strSql.Append(" where Title=@Title ");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200)			};
			parameters[0].Value = Title;

			Maticsoft.Model.SNS.PostsTopics model=new Maticsoft.Model.SNS.PostsTopics();
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
		public Maticsoft.Model.SNS.PostsTopics DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.PostsTopics model=new Maticsoft.Model.SNS.PostsTopics();
			if (row != null)
			{
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["CreatedUserID"]!=null && row["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(row["CreatedUserID"].ToString());
				}
				if(row["CreatedNickName"]!=null)
				{
					model.CreatedNickName=row["CreatedNickName"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["TopicsCount"]!=null && row["TopicsCount"].ToString()!="")
				{
					model.TopicsCount=int.Parse(row["TopicsCount"].ToString());
				}
				if(row["IsRecommend"]!=null && row["IsRecommend"].ToString()!="")
				{
					if((row["IsRecommend"].ToString()=="1")||(row["IsRecommend"].ToString().ToLower()=="true"))
					{
						model.IsRecommend=true;
					}
					else
					{
						model.IsRecommend=false;
					}
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
				}
				if(row["Tags"]!=null)
				{
					model.Tags=row["Tags"].ToString();
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
			strSql.Append("select Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags ");
			strSql.Append(" FROM SNS_PostsTopics ");
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
			strSql.Append(" Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags ");
			strSql.Append(" FROM SNS_PostsTopics ");
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
			strSql.Append("select count(1) FROM SNS_PostsTopics ");
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
				strSql.Append("order by T.Title desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_PostsTopics T ");
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
			parameters[0].Value = "SNS_PostsTopics";
			parameters[1].Value = "Title";
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

