/**
* Videos.cs
*
* 功 能： N/A
* 类 名： Videos
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:08   N/A    初版
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
	/// 数据访问类:Videos
	/// </summary>
	public partial class Videos:IVideos
	{
		public Videos()
		{}
		#region  BasicMethod

		

		


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.Videos model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_Videos(");
			strSql.Append("VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags)");
			strSql.Append(" values (");
			strSql.Append("@VideoName,@VideoUrl,@Type,@Description,@Status,@CreatedUserID,@CreatedNickName,@CreatedDate,@CategoryId,@PVCount,@ThumbImageUrl,@NormalImageUrl,@Sequence,@IsRecomend,@ForwardedCount,@CommentCount,@FavouriteCount,@OwnerVideoId,@Tags)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoName", SqlDbType.NVarChar,200),
					new SqlParameter("@VideoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@ForwardedCount", SqlDbType.Int,4),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@OwnerVideoId", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.VideoName;
			parameters[1].Value = model.VideoUrl;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.Description;
			parameters[4].Value = model.Status;
			parameters[5].Value = model.CreatedUserID;
			parameters[6].Value = model.CreatedNickName;
			parameters[7].Value = model.CreatedDate;
			parameters[8].Value = model.CategoryId;
			parameters[9].Value = model.PVCount;
			parameters[10].Value = model.ThumbImageUrl;
			parameters[11].Value = model.NormalImageUrl;
			parameters[12].Value = model.Sequence;
			parameters[13].Value = model.IsRecomend;
			parameters[14].Value = model.ForwardedCount;
			parameters[15].Value = model.CommentCount;
			parameters[16].Value = model.FavouriteCount;
			parameters[17].Value = model.OwnerVideoId;
			parameters[18].Value = model.Tags;

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
		public bool Update(Maticsoft.Model.SNS.Videos model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_Videos set ");
			strSql.Append("VideoName=@VideoName,");
			strSql.Append("VideoUrl=@VideoUrl,");
			strSql.Append("Type=@Type,");
			strSql.Append("Description=@Description,");
			strSql.Append("Status=@Status,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CreatedNickName=@CreatedNickName,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CategoryId=@CategoryId,");
			strSql.Append("PVCount=@PVCount,");
			strSql.Append("ThumbImageUrl=@ThumbImageUrl,");
			strSql.Append("NormalImageUrl=@NormalImageUrl,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("IsRecomend=@IsRecomend,");
			strSql.Append("ForwardedCount=@ForwardedCount,");
			strSql.Append("CommentCount=@CommentCount,");
			strSql.Append("FavouriteCount=@FavouriteCount,");
			strSql.Append("OwnerVideoId=@OwnerVideoId,");
			strSql.Append("Tags=@Tags");
			strSql.Append(" where VideoID=@VideoID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoName", SqlDbType.NVarChar,200),
					new SqlParameter("@VideoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@ForwardedCount", SqlDbType.Int,4),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@OwnerVideoId", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@VideoID", SqlDbType.Int,4)};
			parameters[0].Value = model.VideoName;
			parameters[1].Value = model.VideoUrl;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.Description;
			parameters[4].Value = model.Status;
			parameters[5].Value = model.CreatedUserID;
			parameters[6].Value = model.CreatedNickName;
			parameters[7].Value = model.CreatedDate;
			parameters[8].Value = model.CategoryId;
			parameters[9].Value = model.PVCount;
			parameters[10].Value = model.ThumbImageUrl;
			parameters[11].Value = model.NormalImageUrl;
			parameters[12].Value = model.Sequence;
			parameters[13].Value = model.IsRecomend;
			parameters[14].Value = model.ForwardedCount;
			parameters[15].Value = model.CommentCount;
			parameters[16].Value = model.FavouriteCount;
			parameters[17].Value = model.OwnerVideoId;
			parameters[18].Value = model.Tags;
			parameters[19].Value = model.VideoID;

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
		public bool Delete(int VideoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_Videos ");
			strSql.Append(" where VideoID=@VideoID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoID", SqlDbType.Int,4)
			};
			parameters[0].Value = VideoID;

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
		public bool DeleteList(string VideoIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_Videos ");
			strSql.Append(" where VideoID in ("+VideoIDlist + ")  ");
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
		public Maticsoft.Model.SNS.Videos GetModel(int VideoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 VideoID,VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags from SNS_Videos ");
			strSql.Append(" where VideoID=@VideoID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoID", SqlDbType.Int,4)
			};
			parameters[0].Value = VideoID;

			Maticsoft.Model.SNS.Videos model=new Maticsoft.Model.SNS.Videos();
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
		public Maticsoft.Model.SNS.Videos DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.Videos model=new Maticsoft.Model.SNS.Videos();
			if (row != null)
			{
				if(row["VideoID"]!=null && row["VideoID"].ToString()!="")
				{
					model.VideoID=int.Parse(row["VideoID"].ToString());
				}
				if(row["VideoName"]!=null)
				{
					model.VideoName=row["VideoName"].ToString();
				}
				if(row["VideoUrl"]!=null)
				{
					model.VideoUrl=row["VideoUrl"].ToString();
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
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
				if(row["CategoryId"]!=null && row["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(row["CategoryId"].ToString());
				}
				if(row["PVCount"]!=null && row["PVCount"].ToString()!="")
				{
					model.PVCount=int.Parse(row["PVCount"].ToString());
				}
				if(row["ThumbImageUrl"]!=null)
				{
					model.ThumbImageUrl=row["ThumbImageUrl"].ToString();
				}
				if(row["NormalImageUrl"]!=null)
				{
					model.NormalImageUrl=row["NormalImageUrl"].ToString();
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
				}
				if(row["IsRecomend"]!=null && row["IsRecomend"].ToString()!="")
				{
					model.IsRecomend=int.Parse(row["IsRecomend"].ToString());
				}
				if(row["ForwardedCount"]!=null && row["ForwardedCount"].ToString()!="")
				{
					model.ForwardedCount=int.Parse(row["ForwardedCount"].ToString());
				}
				if(row["CommentCount"]!=null && row["CommentCount"].ToString()!="")
				{
					model.CommentCount=int.Parse(row["CommentCount"].ToString());
				}
				if(row["FavouriteCount"]!=null && row["FavouriteCount"].ToString()!="")
				{
					model.FavouriteCount=int.Parse(row["FavouriteCount"].ToString());
				}
				if(row["OwnerVideoId"]!=null && row["OwnerVideoId"].ToString()!="")
				{
					model.OwnerVideoId=int.Parse(row["OwnerVideoId"].ToString());
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
			strSql.Append("select VideoID,VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags ");
			strSql.Append(" FROM SNS_Videos ");
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
			strSql.Append(" VideoID,VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags ");
			strSql.Append(" FROM SNS_Videos ");
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
			strSql.Append("select count(1) FROM SNS_Videos ");
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
				strSql.Append("order by T.VideoID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_Videos T ");
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
			parameters[0].Value = "SNS_Videos";
			parameters[1].Value = "VideoID";
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

