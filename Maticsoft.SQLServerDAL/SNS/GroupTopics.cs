/**
* GroupTopics.cs
*
* 功 能： N/A
* 类 名： GroupTopics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:43   N/A    初版
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
	/// 数据访问类:GroupTopics
	/// </summary>
	public partial class GroupTopics:IGroupTopics
	{
		public GroupTopics()
		{}
		#region  BasicMethod


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TopicID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_GroupTopics");
            strSql.Append(" where TopicID=@TopicID");
            SqlParameter[] parameters = {
					new SqlParameter("@TopicID", SqlDbType.Int,50)
			};
            parameters[0].Value = TopicID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.GroupTopics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_GroupTopics(");
			strSql.Append("CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags)");
			strSql.Append(" values (");
			strSql.Append("@CreatedUserID,@CreatedNickName,@GroupID,@GroupName,@Title,@Description,@IsRecomend,@Sequence,@ReplyCount,@PvCount,@DingCount,@Status,@IsTop,@IsActive,@IsAdminRecommend,@ChannelSequence,@HasReferUsers,@PostExUrl,@ImageUrl,@VideoUrl,@AudioUrl,@ProductName,@Price,@ProductLinkUrl,@Type,@TargetID,@FavCount,@CreatedDate,@LastReplyUserId,@LastReplyNickName,@LastPostTime,@Tags)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@DingCount", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IsActive", SqlDbType.Bit,1),
					new SqlParameter("@IsAdminRecommend", SqlDbType.Bit,1),
					new SqlParameter("@ChannelSequence", SqlDbType.Int,4),
					new SqlParameter("@HasReferUsers", SqlDbType.Bit,1),
					new SqlParameter("@PostExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@VideoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@AudioUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@FavCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastReplyUserId", SqlDbType.Int,4),
					new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@LastPostTime", SqlDbType.DateTime),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.CreatedUserID;
			parameters[1].Value = model.CreatedNickName;
			parameters[2].Value = model.GroupID;
			parameters[3].Value = model.GroupName;
			parameters[4].Value = model.Title;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.IsRecomend;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.ReplyCount;
			parameters[9].Value = model.PvCount;
			parameters[10].Value = model.DingCount;
			parameters[11].Value = model.Status;
			parameters[12].Value = model.IsTop;
			parameters[13].Value = model.IsActive;
			parameters[14].Value = model.IsAdminRecommend;
			parameters[15].Value = model.ChannelSequence;
			parameters[16].Value = model.HasReferUsers;
			parameters[17].Value = model.PostExUrl;
			parameters[18].Value = model.ImageUrl;
			parameters[19].Value = model.VideoUrl;
			parameters[20].Value = model.AudioUrl;
			parameters[21].Value = model.ProductName;
			parameters[22].Value = model.Price;
			parameters[23].Value = model.ProductLinkUrl;
			parameters[24].Value = model.Type;
			parameters[25].Value = model.TargetID;
			parameters[26].Value = model.FavCount;
			parameters[27].Value = model.CreatedDate;
			parameters[28].Value = model.LastReplyUserId;
			parameters[29].Value = model.LastReplyNickName;
			parameters[30].Value = model.LastPostTime;
			parameters[31].Value = model.Tags;

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
		public bool Update(Maticsoft.Model.SNS.GroupTopics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_GroupTopics set ");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CreatedNickName=@CreatedNickName,");
			strSql.Append("GroupID=@GroupID,");
			strSql.Append("GroupName=@GroupName,");
			strSql.Append("Title=@Title,");
			strSql.Append("Description=@Description,");
			strSql.Append("IsRecomend=@IsRecomend,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("ReplyCount=@ReplyCount,");
			strSql.Append("PvCount=@PvCount,");
			strSql.Append("DingCount=@DingCount,");
			strSql.Append("Status=@Status,");
			strSql.Append("IsTop=@IsTop,");
			strSql.Append("IsActive=@IsActive,");
			strSql.Append("IsAdminRecommend=@IsAdminRecommend,");
			strSql.Append("ChannelSequence=@ChannelSequence,");
			strSql.Append("HasReferUsers=@HasReferUsers,");
			strSql.Append("PostExUrl=@PostExUrl,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("VideoUrl=@VideoUrl,");
			strSql.Append("AudioUrl=@AudioUrl,");
			strSql.Append("ProductName=@ProductName,");
			strSql.Append("Price=@Price,");
			strSql.Append("ProductLinkUrl=@ProductLinkUrl,");
			strSql.Append("Type=@Type,");
			strSql.Append("TargetID=@TargetID,");
			strSql.Append("FavCount=@FavCount,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("LastReplyUserId=@LastReplyUserId,");
			strSql.Append("LastReplyNickName=@LastReplyNickName,");
			strSql.Append("LastPostTime=@LastPostTime,");
			strSql.Append("Tags=@Tags");
			strSql.Append(" where TopicID=@TopicID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@DingCount", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IsActive", SqlDbType.Bit,1),
					new SqlParameter("@IsAdminRecommend", SqlDbType.Bit,1),
					new SqlParameter("@ChannelSequence", SqlDbType.Int,4),
					new SqlParameter("@HasReferUsers", SqlDbType.Bit,1),
					new SqlParameter("@PostExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@VideoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@AudioUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@FavCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastReplyUserId", SqlDbType.Int,4),
					new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@LastPostTime", SqlDbType.DateTime),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@TopicID", SqlDbType.Int,4)};
			parameters[0].Value = model.CreatedUserID;
			parameters[1].Value = model.CreatedNickName;
			parameters[2].Value = model.GroupID;
			parameters[3].Value = model.GroupName;
			parameters[4].Value = model.Title;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.IsRecomend;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.ReplyCount;
			parameters[9].Value = model.PvCount;
			parameters[10].Value = model.DingCount;
			parameters[11].Value = model.Status;
			parameters[12].Value = model.IsTop;
			parameters[13].Value = model.IsActive;
			parameters[14].Value = model.IsAdminRecommend;
			parameters[15].Value = model.ChannelSequence;
			parameters[16].Value = model.HasReferUsers;
			parameters[17].Value = model.PostExUrl;
			parameters[18].Value = model.ImageUrl;
			parameters[19].Value = model.VideoUrl;
			parameters[20].Value = model.AudioUrl;
			parameters[21].Value = model.ProductName;
			parameters[22].Value = model.Price;
			parameters[23].Value = model.ProductLinkUrl;
			parameters[24].Value = model.Type;
			parameters[25].Value = model.TargetID;
			parameters[26].Value = model.FavCount;
			parameters[27].Value = model.CreatedDate;
			parameters[28].Value = model.LastReplyUserId;
			parameters[29].Value = model.LastReplyNickName;
			parameters[30].Value = model.LastPostTime;
			parameters[31].Value = model.Tags;
			parameters[32].Value = model.TopicID;

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
		public bool Delete(int TopicID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_GroupTopics ");
			strSql.Append(" where TopicID=@TopicID");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)
			};
			parameters[0].Value = TopicID;

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
		public bool DeleteList(string TopicIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_GroupTopics ");
			strSql.Append(" where TopicID in ("+TopicIDlist + ")  ");
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
		public Maticsoft.Model.SNS.GroupTopics GetModel(int TopicID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TopicID,CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags from SNS_GroupTopics ");
			strSql.Append(" where TopicID=@TopicID");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)
			};
			parameters[0].Value = TopicID;

			Maticsoft.Model.SNS.GroupTopics model=new Maticsoft.Model.SNS.GroupTopics();
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
		public Maticsoft.Model.SNS.GroupTopics DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.GroupTopics model=new Maticsoft.Model.SNS.GroupTopics();
			if (row != null)
			{
				if(row["TopicID"]!=null && row["TopicID"].ToString()!="")
				{
					model.TopicID=int.Parse(row["TopicID"].ToString());
				}
				if(row["CreatedUserID"]!=null && row["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(row["CreatedUserID"].ToString());
				}
				if(row["CreatedNickName"]!=null)
				{
					model.CreatedNickName=row["CreatedNickName"].ToString();
				}
				if(row["GroupID"]!=null && row["GroupID"].ToString()!="")
				{
					model.GroupID=int.Parse(row["GroupID"].ToString());
				}
				if(row["GroupName"]!=null)
				{
					model.GroupName=row["GroupName"].ToString();
				}
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["IsRecomend"]!=null && row["IsRecomend"].ToString()!="")
				{
					model.IsRecomend=int.Parse(row["IsRecomend"].ToString());
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
				}
				if(row["ReplyCount"]!=null && row["ReplyCount"].ToString()!="")
				{
					model.ReplyCount=int.Parse(row["ReplyCount"].ToString());
				}
				if(row["PvCount"]!=null && row["PvCount"].ToString()!="")
				{
					model.PvCount=int.Parse(row["PvCount"].ToString());
				}
				if(row["DingCount"]!=null && row["DingCount"].ToString()!="")
				{
					model.DingCount=int.Parse(row["DingCount"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["IsTop"]!=null && row["IsTop"].ToString()!="")
				{
					model.IsTop=int.Parse(row["IsTop"].ToString());
				}
				if(row["IsActive"]!=null && row["IsActive"].ToString()!="")
				{
					if((row["IsActive"].ToString()=="1")||(row["IsActive"].ToString().ToLower()=="true"))
					{
						model.IsActive=true;
					}
					else
					{
						model.IsActive=false;
					}
				}
				if(row["IsAdminRecommend"]!=null && row["IsAdminRecommend"].ToString()!="")
				{
					if((row["IsAdminRecommend"].ToString()=="1")||(row["IsAdminRecommend"].ToString().ToLower()=="true"))
					{
						model.IsAdminRecommend=true;
					}
					else
					{
						model.IsAdminRecommend=false;
					}
				}
				if(row["ChannelSequence"]!=null && row["ChannelSequence"].ToString()!="")
				{
					model.ChannelSequence=int.Parse(row["ChannelSequence"].ToString());
				}
				if(row["HasReferUsers"]!=null && row["HasReferUsers"].ToString()!="")
				{
					if((row["HasReferUsers"].ToString()=="1")||(row["HasReferUsers"].ToString().ToLower()=="true"))
					{
						model.HasReferUsers=true;
					}
					else
					{
						model.HasReferUsers=false;
					}
				}
				if(row["PostExUrl"]!=null)
				{
					model.PostExUrl=row["PostExUrl"].ToString();
				}
				if(row["ImageUrl"]!=null)
				{
					model.ImageUrl=row["ImageUrl"].ToString();
				}
				if(row["VideoUrl"]!=null)
				{
					model.VideoUrl=row["VideoUrl"].ToString();
				}
				if(row["AudioUrl"]!=null)
				{
					model.AudioUrl=row["AudioUrl"].ToString();
				}
				if(row["ProductName"]!=null)
				{
					model.ProductName=row["ProductName"].ToString();
				}
				if(row["Price"]!=null && row["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(row["Price"].ToString());
				}
				if(row["ProductLinkUrl"]!=null)
				{
					model.ProductLinkUrl=row["ProductLinkUrl"].ToString();
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["TargetID"]!=null && row["TargetID"].ToString()!="")
				{
					model.TargetID=int.Parse(row["TargetID"].ToString());
				}
				if(row["FavCount"]!=null && row["FavCount"].ToString()!="")
				{
					model.FavCount=int.Parse(row["FavCount"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["LastReplyUserId"]!=null && row["LastReplyUserId"].ToString()!="")
				{
					model.LastReplyUserId=int.Parse(row["LastReplyUserId"].ToString());
				}
				if(row["LastReplyNickName"]!=null)
				{
					model.LastReplyNickName=row["LastReplyNickName"].ToString();
				}
				if(row["LastPostTime"]!=null && row["LastPostTime"].ToString()!="")
				{
					model.LastPostTime=DateTime.Parse(row["LastPostTime"].ToString());
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
			strSql.Append("select TopicID,CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags ");
			strSql.Append(" FROM SNS_GroupTopics ");
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
			strSql.Append(" TopicID,CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags ");
			strSql.Append(" FROM SNS_GroupTopics ");
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
			strSql.Append("select count(1) FROM SNS_GroupTopics ");
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
				strSql.Append("order by T.TopicID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_GroupTopics T ");
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
			parameters[0].Value = "SNS_GroupTopics";
			parameters[1].Value = "TopicID";
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
        /// 删除一条数据
        /// </summary>
        public bool DeleteEx(int TopicID)
        {
            //得到对应的小组id

            Maticsoft.Model.SNS.GroupTopics model = GetModel(TopicID);
            if (null == model)
            {
                return true;
            }
            return DeleteData(model);
         
        }

        public bool DeleteEx(int TopicID, out string ImageUrl)
         {
        //得到对应的小组id

           Maticsoft.Model.SNS.GroupTopics model = GetModel(TopicID);
           if (null == model)
           {
               ImageUrl = "";
              return true;
           }
           ImageUrl = model.ImageUrl;
           return DeleteData(model);
        }

        public bool DeleteData(Maticsoft.Model.SNS.GroupTopics model)
        {
            int GroupID = model.GroupID;
            int CreatedUserId = model.CreatedUserID;

            List<CommandInfo> sqllist = new List<CommandInfo>();

            //根据GroupID更新SNS_Groups的TopicCount：TopicCount-1
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update SNS_Groups set ");
            strSql1.Append("TopicCount=TopicCount-1");
            strSql1.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters1 = {
					new SqlParameter("@GroupID", SqlDbType.Int,4)};
            parameters1[0].Value = model.GroupID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            //根据UserID更新Accounts_UsersExp的TopicCount：TopicCount-1
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update Accounts_UsersExp  set TopicCount=TopicCount-1 where UserID=@UserID");
            SqlParameter[] parameters2 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters2[0].Value = CreatedUserId;
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd2);

            //根据TopicID删除SNS_GroupTopicReply的相关数据
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from SNS_GroupTopicReply ");
            strSql3.Append(" where TopicID=@TopicID");
            SqlParameter[] parameters3 = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)
			};
            parameters3[0].Value =model.TopicID;
            CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd3);

            //得到所有SNS_GroupTopicFav收藏的CreatedUserID
            //根据UserID更新Accounts_UsersExp的FavTopicCount:FavTopicCount-1。
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Accounts_UsersExp  set FavTopicCount=FavTopicCount-1 where UserID in(select CreatedUserID from SNS_GroupTopicFav where TopicID=@TopicID) ");
            SqlParameter[] parameters4 = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)
			};
            parameters4[0].Value = model.TopicID;
            CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd4);

            //根据TopicID删除SNS_GroupTopicFav的相关数据
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from SNS_GroupTopicFav ");
            strSql5.Append(" where TopicID=@TopicID");
            SqlParameter[] parameters5 = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)
			};
            parameters5[0].Value = model.TopicID;
            CommandInfo cmd5 = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd5);

            //删除数据
            StringBuilder strSql0 = new StringBuilder();
            strSql0.Append("delete from SNS_GroupTopics ");
            strSql0.Append(" where TopicID=@TopicID");
            SqlParameter[] parameters0 = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)
			};
            parameters0[0].Value = model.TopicID;
            CommandInfo cmd0 = new CommandInfo(strSql0.ToString(), parameters0);
            sqllist.Add(cmd0);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
        
        }

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
            strSql.Append(" * FROM SNS_GroupTopics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 增加小组主题

        public int AddEx(Maticsoft.Model.SNS.GroupTopics Tmodel, Maticsoft.Model.SNS.Products PModel)
        {

            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {


                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        object TargetID = "0";
                        object TopicId = "0";
                        Tmodel.Type = (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal;
                        if ((PModel != null && PModel.ProductID > 0) || !string.IsNullOrEmpty(Tmodel.ImageUrl))
                        {
                            TargetID = DbHelperSQL.GetSingle4Trans(GenerateImageInfo(Tmodel, PModel), transaction);
                            Tmodel.TargetID = Common.Globals.SafeInt(TargetID != null ? TargetID.ToString() : "", 0);
                            Tmodel.Type = PModel.ProductID > 0 ? (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product : (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo;
                        }
                        TopicId = DbHelperSQL.GetSingle4Trans(GenerateTopicInfo(Tmodel), transaction);
                        DbHelperSQL.GetSingle4Trans(GenerateUpdateUserEx(Tmodel.CreatedUserID, Tmodel.Type), transaction);
                        DbHelperSQL.GetSingle4Trans(GenerateUpdateGroupCount(Tmodel), transaction);
                        transaction.Commit();
                        return Common.Globals.SafeInt(TopicId != null ? TopicId.ToString() : "", 0);
                    }
                    catch (Exception)
                    {

                        transaction.Rollback();
                        return 0;
                    }
                }
            }

        } 
        #endregion

        #region 更新小组主题的个数

        public CommandInfo GenerateUpdateGroupCount(Maticsoft.Model.SNS.GroupTopics Tmodel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Groups set TopicCount=TopicCount+1 WHERE GroupID=@GroupID ");
            SqlParameter[] parameters = { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            parameters[0].Value = Tmodel.GroupID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);
            return cmd;

        } 
        #endregion

        #region 增加主题
        public CommandInfo GenerateTopicInfo(Maticsoft.Model.SNS.GroupTopics Tmodel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_GroupTopics(");
            strSql.Append("CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags)");
            strSql.Append(" values (");
            strSql.Append("@CreatedUserID,@CreatedNickName,@GroupID,@GroupName,@Title,@Description,@IsRecomend,@Sequence,@ReplyCount,@PvCount,@DingCount,@Status,@IsTop,@IsActive,@IsAdminRecommend,@ChannelSequence,@HasReferUsers,@PostExUrl,@ImageUrl,@VideoUrl,@AudioUrl,@ProductName,@Price,@ProductLinkUrl,@Type,@TargetID,@FavCount,@CreatedDate,@LastReplyUserId,@LastReplyNickName,@LastPostTime,@Tags)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@DingCount", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IsActive", SqlDbType.Bit,1),
					new SqlParameter("@IsAdminRecommend", SqlDbType.Bit,1),
					new SqlParameter("@ChannelSequence", SqlDbType.Int,4),
					new SqlParameter("@HasReferUsers", SqlDbType.Bit,1),
					new SqlParameter("@PostExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@VideoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@AudioUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@FavCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastReplyUserId", SqlDbType.Int,4),
					new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@LastPostTime", SqlDbType.DateTime),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100)};
            parameters[0].Value = Tmodel.CreatedUserID;
            parameters[1].Value = Tmodel.CreatedNickName;
            parameters[2].Value = Tmodel.GroupID;
            parameters[3].Value = Tmodel.GroupName;
            parameters[4].Value = Tmodel.Title;
            parameters[5].Value = Tmodel.Description;
            parameters[6].Value = Tmodel.IsRecomend;
            parameters[7].Value = Tmodel.Sequence;
            parameters[8].Value = Tmodel.ReplyCount;
            parameters[9].Value = Tmodel.PvCount;
            parameters[10].Value = Tmodel.DingCount;
            parameters[11].Value = Tmodel.Status;
            parameters[12].Value = Tmodel.IsTop;
            parameters[13].Value = Tmodel.IsActive;
            parameters[14].Value = Tmodel.IsAdminRecommend;
            parameters[15].Value = Tmodel.ChannelSequence;
            parameters[16].Value = Tmodel.HasReferUsers;
            parameters[17].Value = Tmodel.PostExUrl;
            parameters[18].Value = Tmodel.ImageUrl;
            parameters[19].Value = Tmodel.VideoUrl;
            parameters[20].Value = Tmodel.AudioUrl;
            parameters[21].Value = Tmodel.ProductName;
            parameters[22].Value = Tmodel.Price;
            parameters[23].Value = Tmodel.ProductLinkUrl;
            parameters[24].Value = Tmodel.Type;
            parameters[25].Value = Tmodel.TargetID;
            parameters[26].Value = Tmodel.FavCount;
            parameters[27].Value = Tmodel.CreatedDate;
            parameters[28].Value = Tmodel.LastReplyUserId;
            parameters[29].Value = Tmodel.LastReplyNickName;
            parameters[30].Value = Tmodel.LastPostTime;
            parameters[31].Value = Tmodel.Tags;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);
            return cmd;
        }
        #endregion

        #region 用户扩展表中相应的数量增加1
        public CommandInfo GenerateUpdateUserEx(int UserId, int type)
        {
            CommandInfo cmd = new CommandInfo();
            ///第一种情况，如果上传的图片，除过小组的主题加1外，用户分享的数量也加1
            if ((int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo == type)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Accounts_UsersExp set ShareCount=ShareCount+1,TopicCount=TopicCount+1 WHERE UserID=@UserID ");
                SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameters[0].Value = UserId;
                cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);
            }
            ///第二种情况，如果上传的是商品，除过小组的主题加1外，用户分享和商品的数量也加1
            else if ((int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo == type)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Accounts_UsersExp set ShareCount=ShareCount+1,ProductsCount=ProductsCount+1,TopicCount=TopicCount+1 WHERE UserID=@UserID ");
                SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameters[0].Value = UserId;
                cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);
            }
            ///第二种情况，小组的主题加1
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Accounts_UsersExp set TopicCount=TopicCount+1 WHERE UserID=@UserID ");
                SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameters[0].Value = UserId;
                cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);

            }

            return cmd;
        } 
        #endregion

        #region 商品或图片表中相应的的插入数据
        private CommandInfo GenerateImageInfo(Maticsoft.Model.SNS.GroupTopics Tmodel, Maticsoft.Model.SNS.Products PModel)
        {
            #region 如果是增加商品，同时更新用户分享和商品的数量
            if (PModel != null && PModel.ProductID > 0)
            {
                StringBuilder strSql0 = new StringBuilder();
                strSql0.Append("insert into SNS_Products(");
                strSql0.Append("ProductName,Price,ProductSourceID,CategoryID,ProductUrl,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,Status,ShareDescription,CreatedDate,Tags,IsRecomend)");
                strSql0.Append(" values (");
                strSql0.Append("@ProductName,@Price,@ProductSourceID,@CategoryID,@ProductUrl,@CreateUserID,@CreatedNickName,@ThumbImageUrl,@NormalImageUrl,@Status,@ShareDescription,@CreatedDate,@Tags,@IsRecomend)");
                strSql0.Append(";select @@IDENTITY");
                SqlParameter[] parameters0 = {
                        new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
                        new SqlParameter("@Price", SqlDbType.Decimal,9),
                        new SqlParameter("@ProductSourceID", SqlDbType.Int,4),
                        new SqlParameter("@CategoryID", SqlDbType.Int,4),
                        new SqlParameter("@ProductUrl", SqlDbType.NVarChar,500),
                        new SqlParameter("@CreateUserID", SqlDbType.Int,4),
                        new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
                        new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
                        new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
                        new SqlParameter("@Status", SqlDbType.Int,4),
                        new SqlParameter("@ShareDescription", SqlDbType.NVarChar,200),
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                        new SqlParameter("@Tags",SqlDbType.NVarChar,400),
                        new SqlParameter("@IsRecomend",SqlDbType.NVarChar,400)
                      };
                parameters0[0].Value = PModel.ProductName;
                parameters0[1].Value = PModel.Price;
                parameters0[2].Value = PModel.ProductSourceID;
                parameters0[3].Value = PModel.CategoryID;
                parameters0[4].Value = PModel.ProductUrl;
                parameters0[5].Value = PModel.CreateUserID;
                parameters0[6].Value = PModel.CreatedNickName;
                parameters0[7].Value = PModel.ThumbImageUrl;
                parameters0[8].Value = PModel.NormalImageUrl;
                parameters0[9].Value = PModel.Status;
                parameters0[10].Value = PModel.ShareDescription;
                parameters0[11].Value = PModel.CreatedDate;
                parameters0[12].Value = PModel.Tags;
                parameters0[13].Value = 0;
                Tmodel.ImageUrl = PModel.ThumbImageUrl;
                Tmodel.Price = PModel.Price;
                Tmodel.ProductLinkUrl = PModel.ProductUrl;
                Tmodel.ProductName = PModel.ProductName;
                CommandInfo cmd1 = new CommandInfo(strSql0.ToString(), parameters0);
                return new CommandInfo(strSql0.ToString(),
                                  parameters0, EffentNextType.ExcuteEffectRows);
            }
            #endregion
            #region  如果是图片想想要的图片表插入数据
            else if (!string.IsNullOrEmpty(Tmodel.ImageUrl))
            {

                StringBuilder strSql5 = new StringBuilder();
                strSql5.Append("insert into SNS_Photos(");
                strSql5.Append(" PhotoUrl,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,Type,ThumbImageUrl,NormalImageUrl)");
                strSql5.Append(" values (");
                strSql5.Append(" @PhotoUrl,@Description,@Status,@CreatedUserID,@CreatedNickName,@CreatedDate,@Type,@ThumbImageUrl,@NormalImageUrl)");
                strSql5.Append(";select @@IDENTITY");
                SqlParameter[] parameters5 = {
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200)};
                string[] imagesurl = string.IsNullOrEmpty(Tmodel.ImageUrl) ? null : Tmodel.ImageUrl.Split('|');
                if (imagesurl != null && imagesurl.Length >= 2)
                {
                    parameters5[0].Value = imagesurl[0];
                    parameters5[7].Value = imagesurl[1];
                    Tmodel.ImageUrl = imagesurl[1];
                }
                else
                {
                    parameters5[0].Value = "";
                    parameters5[7].Value = "";
                    parameters5[8].Value = "";
                }
                parameters5[1].Value = "";
                ///设置默认的状态
                string Status = new SysManage.ConfigSystem().GetValue("SNS_PhotoDefaultStatus");
                if (!string.IsNullOrEmpty(Status))
                {
                    parameters5[2].Value = Common.Globals.SafeInt(Status, 1);
                }
                else
                {
                    parameters5[2].Value = (int)Model.SNS.EnumHelper.PhotoStatus.CategoryUnDefined;
                }
                parameters5[3].Value = Tmodel.CreatedUserID;
                parameters5[4].Value = Tmodel.CreatedNickName;
                parameters5[5].Value = Tmodel.CreatedDate;
                parameters5[6].Value = (int)Maticsoft.Model.SNS.EnumHelper.PhotoType.Group; 
                
                CommandInfo cmd4 = new CommandInfo(strSql5.ToString(), parameters5, EffentNextType.ExcuteEffectRows);
                return cmd4;
            }
            return null;
            #endregion



        } 
        #endregion

        #region 更新状态

        /// <summary>
        /// 更新话题的状态
        /// </summary>
        /// <param name="IdsStr">id的集合</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool UpdateStatusList(string IdsStr, Maticsoft.Model.SNS.EnumHelper.TopicStatus status)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupTopics set Status=" + (int)status + " ");
            strSql.Append(" where TopicID in (" + IdsStr + ")  ");
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
        /// 更新主题的状态（包括推荐到小组首页和小组频道首页）
        /// </summary>
        /// <param name="TopicId"></param>
        /// <param name="Recommand"></param>
        /// <param name="IsAdmin"></param>
        /// <returns></returns>
        public bool UpdateRecommand(int TopicId, int Recommand)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append("Update SNS_GroupTopics set  ");
                strSql.Append("IsRecomend=" + Recommand );
            strSql.Append("where TopicID=" + TopicId + "");
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

        public bool UpdateAdminRecommand(int TopicId,  bool IsAdmin)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update SNS_GroupTopics set  ");
            if (IsAdmin)
            {
                strSql.Append(" IsAdminRecommend=1 ");
            }
            else
            {
                strSql.Append("IsAdminRecommend=0 ");

            }
            strSql.Append("where TopicID=" + TopicId + "");
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

        public bool UpdatePVCount(int TopicID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("Update SNS_GroupTopics set PvCount=PvCount+1 ");
            strSql.Append("where TopicID=" + TopicID);
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
		#endregion  ExtensionMethod
	}
}

