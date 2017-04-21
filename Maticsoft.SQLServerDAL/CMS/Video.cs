/**
* Video.cs
*
* 功 能： 
* 类 名： Video
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/22 16:28:49  蒋海滨    初版
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
using Maticsoft.IDAL.CMS;
using Maticsoft.DBUtility;
using Maticsoft.Common;
namespace Maticsoft.SQLServerDAL.CMS
{
	/// <summary>
	/// 数据访问类:Video
	/// </summary>
	public partial class Video:IVideo
	{
		public Video()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("VideoID", "CMS_Video"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int VideoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CMS_Video");
			strSql.Append(" where VideoID=@VideoID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoID", SqlDbType.Int,4)
			};
			parameters[0].Value = VideoID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.CMS.Video model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CMS_Video(");
            strSql.Append("Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount)");
			strSql.Append(" values (");
            strSql.Append("@Title,@Description,@AlbumID,@CreatedUserID,@CreatedDate,@LastUpdateUserID,@LastUpdateDate,@Sequence,@VideoClassID,@IsRecomend,@ImageUrl,@ThumbImageUrl,@NormalImageUrl,@TotalTime,@TotalComment,@TotalFav,@TotalUp,@Reference,@Tags,@VideoUrl,@UrlType,@VideoFormat,@Domain,@Grade,@Attachment,@Privacy,@State,@Remark,@PvCount)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUserID", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@VideoClassID", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Bit,1),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@TotalTime", SqlDbType.Int,4),
					new SqlParameter("@TotalComment", SqlDbType.Int,4),
					new SqlParameter("@TotalFav", SqlDbType.Int,4),
					new SqlParameter("@TotalUp", SqlDbType.Int,4),
					new SqlParameter("@Reference", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@VideoUrl", SqlDbType.NVarChar),
					new SqlParameter("@UrlType", SqlDbType.NVarChar),
					new SqlParameter("@VideoFormat", SqlDbType.NVarChar,50),
					new SqlParameter("@Domain", SqlDbType.NVarChar,50),
					new SqlParameter("@Grade", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,100),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
                    new SqlParameter("@PvCount", SqlDbType.Int,4)
                                        };
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.AlbumID;
			parameters[3].Value = model.CreatedUserID;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.LastUpdateUserID;
			parameters[6].Value = model.LastUpdateDate;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.VideoClassID;
			parameters[9].Value = model.IsRecomend;
			parameters[10].Value = model.ImageUrl;
			parameters[11].Value = model.ThumbImageUrl;
			parameters[12].Value = model.NormalImageUrl;
			parameters[13].Value = model.TotalTime;
			parameters[14].Value = model.TotalComment;
			parameters[15].Value = model.TotalFav;
			parameters[16].Value = model.TotalUp;
			parameters[17].Value = model.Reference;
			parameters[18].Value = model.Tags;
			parameters[19].Value = model.VideoUrl;
			parameters[20].Value = model.UrlType;
			parameters[21].Value = model.VideoFormat;
			parameters[22].Value = model.Domain;
			parameters[23].Value = model.Grade;
			parameters[24].Value = model.Attachment;
			parameters[25].Value = model.Privacy;
			parameters[26].Value = model.State;
			parameters[27].Value = model.Remark;
            parameters[28].Value = model.PvCount;
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
		public bool Update(Maticsoft.Model.CMS.Video model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CMS_Video set ");
			strSql.Append("Title=@Title,");
			strSql.Append("Description=@Description,");
			strSql.Append("AlbumID=@AlbumID,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("LastUpdateUserID=@LastUpdateUserID,");
			strSql.Append("LastUpdateDate=@LastUpdateDate,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("VideoClassID=@VideoClassID,");
			strSql.Append("IsRecomend=@IsRecomend,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("ThumbImageUrl=@ThumbImageUrl,");
			strSql.Append("NormalImageUrl=@NormalImageUrl,");
			strSql.Append("TotalTime=@TotalTime,");
			strSql.Append("TotalComment=@TotalComment,");
			strSql.Append("TotalFav=@TotalFav,");
			strSql.Append("TotalUp=@TotalUp,");
			strSql.Append("Reference=@Reference,");
			strSql.Append("Tags=@Tags,");
			strSql.Append("VideoUrl=@VideoUrl,");
			strSql.Append("UrlType=@UrlType,");
			strSql.Append("VideoFormat=@VideoFormat,");
			strSql.Append("Domain=@Domain,");
			strSql.Append("Grade=@Grade,");
			strSql.Append("Attachment=@Attachment,");
			strSql.Append("Privacy=@Privacy,");
			strSql.Append("State=@State,");
			strSql.Append("Remark=@Remark,");
            strSql.Append("PvCount=@PvCount");
			strSql.Append(" where VideoID=@VideoID");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUserID", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@VideoClassID", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Bit,1),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@TotalTime", SqlDbType.Int,4),
					new SqlParameter("@TotalComment", SqlDbType.Int,4),
					new SqlParameter("@TotalFav", SqlDbType.Int,4),
					new SqlParameter("@TotalUp", SqlDbType.Int,4),
					new SqlParameter("@Reference", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@VideoUrl", SqlDbType.NVarChar),
					new SqlParameter("@UrlType", SqlDbType.NVarChar),
					new SqlParameter("@VideoFormat", SqlDbType.NVarChar,50),
					new SqlParameter("@Domain", SqlDbType.NVarChar,50),
					new SqlParameter("@Grade", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,100),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
                    new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@VideoID", SqlDbType.Int,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.AlbumID;
			parameters[3].Value = model.CreatedUserID;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.LastUpdateUserID;
			parameters[6].Value = model.LastUpdateDate;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.VideoClassID;
			parameters[9].Value = model.IsRecomend;
			parameters[10].Value = model.ImageUrl;
			parameters[11].Value = model.ThumbImageUrl;
			parameters[12].Value = model.NormalImageUrl;
			parameters[13].Value = model.TotalTime;
			parameters[14].Value = model.TotalComment;
			parameters[15].Value = model.TotalFav;
			parameters[16].Value = model.TotalUp;
			parameters[17].Value = model.Reference;
			parameters[18].Value = model.Tags;
			parameters[19].Value = model.VideoUrl;
			parameters[20].Value = model.UrlType;
			parameters[21].Value = model.VideoFormat;
			parameters[22].Value = model.Domain;
			parameters[23].Value = model.Grade;
			parameters[24].Value = model.Attachment;
			parameters[25].Value = model.Privacy;
			parameters[26].Value = model.State;
			parameters[27].Value = model.Remark;
            parameters[28].Value = model.PvCount;
			parameters[29].Value = model.VideoID;

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
			strSql.Append("delete from CMS_Video ");
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
			strSql.Append("delete from CMS_Video ");
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
		public Maticsoft.Model.CMS.Video GetModel(int VideoID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 VideoID,Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount from CMS_Video ");
			strSql.Append(" where VideoID=@VideoID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoID", SqlDbType.Int,4)
			};
			parameters[0].Value = VideoID;

			Maticsoft.Model.CMS.Video model=new Maticsoft.Model.CMS.Video();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["VideoID"]!=null && ds.Tables[0].Rows[0]["VideoID"].ToString()!="")
				{
					model.VideoID=int.Parse(ds.Tables[0].Rows[0]["VideoID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Description"]!=null && ds.Tables[0].Rows[0]["Description"].ToString()!="")
				{
					model.Description=ds.Tables[0].Rows[0]["Description"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AlbumID"]!=null && ds.Tables[0].Rows[0]["AlbumID"].ToString()!="")
				{
					model.AlbumID=int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedUserID"]!=null && ds.Tables[0].Rows[0]["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedDate"]!=null && ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastUpdateUserID"]!=null && ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString()!="")
				{
					model.LastUpdateUserID=int.Parse(ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastUpdateDate"]!=null && ds.Tables[0].Rows[0]["LastUpdateDate"].ToString()!="")
				{
					model.LastUpdateDate=DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sequence"]!=null && ds.Tables[0].Rows[0]["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
				}
				if(ds.Tables[0].Rows[0]["VideoClassID"]!=null && ds.Tables[0].Rows[0]["VideoClassID"].ToString()!="")
				{
					model.VideoClassID=int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRecomend"]!=null && ds.Tables[0].Rows[0]["IsRecomend"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsRecomend"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower()=="true"))
					{
						model.IsRecomend=true;
					}
					else
					{
						model.IsRecomend=false;
					}
				}
				if(ds.Tables[0].Rows[0]["ImageUrl"]!=null && ds.Tables[0].Rows[0]["ImageUrl"].ToString()!="")
				{
					model.ImageUrl=ds.Tables[0].Rows[0]["ImageUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ThumbImageUrl"]!=null && ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString()!="")
				{
					model.ThumbImageUrl=ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["NormalImageUrl"]!=null && ds.Tables[0].Rows[0]["NormalImageUrl"].ToString()!="")
				{
					model.NormalImageUrl=ds.Tables[0].Rows[0]["NormalImageUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TotalTime"]!=null && ds.Tables[0].Rows[0]["TotalTime"].ToString()!="")
				{
					model.TotalTime=int.Parse(ds.Tables[0].Rows[0]["TotalTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalComment"]!=null && ds.Tables[0].Rows[0]["TotalComment"].ToString()!="")
				{
					model.TotalComment=int.Parse(ds.Tables[0].Rows[0]["TotalComment"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalFav"]!=null && ds.Tables[0].Rows[0]["TotalFav"].ToString()!="")
				{
					model.TotalFav=int.Parse(ds.Tables[0].Rows[0]["TotalFav"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalUp"]!=null && ds.Tables[0].Rows[0]["TotalUp"].ToString()!="")
				{
					model.TotalUp=int.Parse(ds.Tables[0].Rows[0]["TotalUp"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Reference"]!=null && ds.Tables[0].Rows[0]["Reference"].ToString()!="")
				{
					model.Reference=int.Parse(ds.Tables[0].Rows[0]["Reference"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tags"]!=null && ds.Tables[0].Rows[0]["Tags"].ToString()!="")
				{
					model.Tags=ds.Tables[0].Rows[0]["Tags"].ToString();
				}
				if(ds.Tables[0].Rows[0]["VideoUrl"]!=null && ds.Tables[0].Rows[0]["VideoUrl"].ToString()!="")
				{
					model.VideoUrl=ds.Tables[0].Rows[0]["VideoUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UrlType"]!=null && ds.Tables[0].Rows[0]["UrlType"].ToString()!="")
				{
                    model.UrlType = int.Parse(ds.Tables[0].Rows[0]["UrlType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["VideoFormat"]!=null && ds.Tables[0].Rows[0]["VideoFormat"].ToString()!="")
				{
					model.VideoFormat=ds.Tables[0].Rows[0]["VideoFormat"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Domain"]!=null && ds.Tables[0].Rows[0]["Domain"].ToString()!="")
				{
					model.Domain=ds.Tables[0].Rows[0]["Domain"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Grade"]!=null && ds.Tables[0].Rows[0]["Grade"].ToString()!="")
				{
					model.Grade=int.Parse(ds.Tables[0].Rows[0]["Grade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Attachment"]!=null && ds.Tables[0].Rows[0]["Attachment"].ToString()!="")
				{
					model.Attachment=ds.Tables[0].Rows[0]["Attachment"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Privacy"]!=null && ds.Tables[0].Rows[0]["Privacy"].ToString()!="")
				{
					model.Privacy=int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString());
				}
				if(ds.Tables[0].Rows[0]["State"]!=null && ds.Tables[0].Rows[0]["State"].ToString()!="")
				{
					model.State=int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
				{
					model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				}
                if (ds.Tables[0].Rows[0]["PvCount"] != null && ds.Tables[0].Rows[0]["PvCount"].ToString() != "")
                {
                    model.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
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
            strSql.Append("select VideoID,Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount ");
			strSql.Append(" FROM CMS_Video ");
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
            strSql.Append(" VideoID,Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount ");
			strSql.Append(" FROM CMS_Video ");
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
			strSql.Append("select count(1) FROM CMS_Video ");
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
			if (!string.IsNullOrWhiteSpace(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.VideoID desc");
			}
			strSql.Append(")AS Row, T.*  from CMS_Video T ");
			if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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
			parameters[0].Value = "CMS_Video";
			parameters[1].Value = "VideoID";
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM View_Video ");
            //strSql.Append(" SELECT *,Accounts_Users.UserName AS LastUpdateUserName, AlbumName,VideoClassName FROM CMS_Video CMSV ");
            //strSql.Append(" LEFT JOIN Accounts_Users ON Accounts_Users.UserID= CMSV.LastUpdateUserID ");
            //strSql.Append(" LEFT JOIN (SELECT * ,Accounts_Users.UserName AS CreatedUserName FROM CMS_Video CMSVS ");
            //strSql.Append(" LEFT JOIN  Accounts_Users ON Accounts_Users.UserID= CMSVS.CreatedUserID) CMSVS ON CMSVS.VideoID=CMSV.VideoID ");
            //strSql.Append(" LEFT JOIN CMS_VideoAlbum CMSVA ON CMSVA.AlbumID=CMSV.AlbumID ");
            //strSql.Append(" LEFT JOIN CMS_VideoClass CMSVC ON CMSVC.VideoClassID=CMSV.VideoClassID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                strSql.Append("order by " + orderby);
            }
            else
            {
                strSql.Append("order by VideoID desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Video GetModelEx(int VideoID)
        {

            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" SELECT Top 1 *,Accounts_Users.UserName AS LastUpdateUserName, AlbumName,VideoClassName FROM CMS_Video CMSV ");
            //strSql.Append(" LEFT JOIN Accounts_Users ON Accounts_Users.UserID= CMSV.LastUpdateUserID ");
            //strSql.Append(" LEFT JOIN (SELECT * ,Accounts_Users.UserName AS CreatedUserName FROM CMS_Video CMSVS ");
            //strSql.Append(" LEFT JOIN  Accounts_Users ON Accounts_Users.UserID= CMSVS.CreatedUserID) CMSVS ON CMSVS.VideoID=CMSV.VideoID ");
            //strSql.Append(" LEFT JOIN CMS_VideoAlbum CMSVA ON CMSVA.AlbumID=CMSV.AlbumID ");
            //strSql.Append(" LEFT JOIN CMS_VideoClass CMSVC ON CMSVC.VideoClassID=CMSV.VideoClassID ");
            //strSql.Append(" where CMSV.VideoID=@VideoID");
            strSql.Append(" SELECT Top 1 * FROM View_Video ");
            strSql.Append(" where VideoID=@VideoID");
            SqlParameter[] parameters = {
					new SqlParameter("@VideoID", SqlDbType.Int,4)
			};
            parameters[0].Value = VideoID;

            Maticsoft.Model.CMS.Video model = new Maticsoft.Model.CMS.Video();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["VideoID"] != null && ds.Tables[0].Rows[0]["VideoID"].ToString() != "")
                {
                    model.VideoID = int.Parse(ds.Tables[0].Rows[0]["VideoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AlbumID"] != null && ds.Tables[0].Rows[0]["AlbumID"].ToString() != "")
                {
                    model.AlbumID = int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedUserID"] != null && ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedUserName"] != null && ds.Tables[0].Rows[0]["CreatedUserName"].ToString() != "")
                {
                    model.CreatedUserName = ds.Tables[0].Rows[0]["CreatedUserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateUserID"] != null && ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString() != "")
                {
                    model.LastUpdateUserID = int.Parse(ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateUserName"] != null && ds.Tables[0].Rows[0]["LastUpdateUserName"].ToString() != "")
                {
                    model.LastUpdateUserName = ds.Tables[0].Rows[0]["LastUpdateUserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastUpdateDate"] != null && ds.Tables[0].Rows[0]["LastUpdateDate"].ToString() != "")
                {
                    model.LastUpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sequence"] != null && ds.Tables[0].Rows[0]["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VideoClassID"] != null && ds.Tables[0].Rows[0]["VideoClassID"].ToString() != "")
                {
                    model.VideoClassID = int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsRecomend"] != null && ds.Tables[0].Rows[0]["IsRecomend"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsRecomend"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower() == "true"))
                    {
                        model.IsRecomend = true;
                    }
                    else
                    {
                        model.IsRecomend = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ImageUrl"] != null && ds.Tables[0].Rows[0]["ImageUrl"].ToString() != "")
                {
                    model.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ThumbImageUrl"] != null && ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString() != "")
                {
                    model.ThumbImageUrl = ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NormalImageUrl"] != null && ds.Tables[0].Rows[0]["NormalImageUrl"].ToString() != "")
                {
                    model.NormalImageUrl = ds.Tables[0].Rows[0]["NormalImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TotalTime"] != null && ds.Tables[0].Rows[0]["TotalTime"].ToString() != "")
                {
                    model.TotalTime = int.Parse(ds.Tables[0].Rows[0]["TotalTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalComment"] != null && ds.Tables[0].Rows[0]["TotalComment"].ToString() != "")
                {
                    model.TotalComment = int.Parse(ds.Tables[0].Rows[0]["TotalComment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalFav"] != null && ds.Tables[0].Rows[0]["TotalFav"].ToString() != "")
                {
                    model.TotalFav = int.Parse(ds.Tables[0].Rows[0]["TotalFav"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalUp"] != null && ds.Tables[0].Rows[0]["TotalUp"].ToString() != "")
                {
                    model.TotalUp = int.Parse(ds.Tables[0].Rows[0]["TotalUp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Reference"] != null && ds.Tables[0].Rows[0]["Reference"].ToString() != "")
                {
                    model.Reference = int.Parse(ds.Tables[0].Rows[0]["Reference"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tags"] != null && ds.Tables[0].Rows[0]["Tags"].ToString() != "")
                {
                    model.Tags = ds.Tables[0].Rows[0]["Tags"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VideoUrl"] != null && ds.Tables[0].Rows[0]["VideoUrl"].ToString() != "")
                {
                    model.VideoUrl = ds.Tables[0].Rows[0]["VideoUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UrlType"] != null && ds.Tables[0].Rows[0]["UrlType"].ToString() != "")
                {
                    model.UrlType = int.Parse(ds.Tables[0].Rows[0]["UrlType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VideoFormat"] != null && ds.Tables[0].Rows[0]["VideoFormat"].ToString() != "")
                {
                    model.VideoFormat = ds.Tables[0].Rows[0]["VideoFormat"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Domain"] != null && ds.Tables[0].Rows[0]["Domain"].ToString() != "")
                {
                    model.Domain = ds.Tables[0].Rows[0]["Domain"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Grade"] != null && ds.Tables[0].Rows[0]["Grade"].ToString() != "")
                {
                    model.Grade = int.Parse(ds.Tables[0].Rows[0]["Grade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Attachment"] != null && ds.Tables[0].Rows[0]["Attachment"].ToString() != "")
                {
                    model.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Privacy"] != null && ds.Tables[0].Rows[0]["Privacy"].ToString() != "")
                {
                    model.Privacy = int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PvCount"] != null && ds.Tables[0].Rows[0]["PvCount"].ToString() != "")
                {
                    model.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        #region 批量处理
        /// <summary>
        /// 批量处理
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Video set " + strWhere);
            strSql.Append(" where VideoID in(" + IDlist + ")  ");
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

        /// <summary>
        /// 得到最大顺序
        /// </summary>
        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_Video");
        }
        #endregion
	}
}

