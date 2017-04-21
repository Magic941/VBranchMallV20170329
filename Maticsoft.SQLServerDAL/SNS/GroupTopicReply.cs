/**
* GroupTopicReply.cs
*
* 功 能： N/A
* 类 名： GroupTopicReply
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
	/// 数据访问类:GroupTopicReplyAddEx
	/// </summary>
	public partial class GroupTopicReply:IGroupTopicReply
	{
		public GroupTopicReply()
		{}
		#region  BasicMethod
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.GroupTopicReply model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_GroupTopicReply(");
			strSql.Append("GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate)");
			strSql.Append(" values (");
			strSql.Append("@GroupID,@ReplyType,@ReplyNickName,@ReplyUserID,@OriginalID,@OrginalDes,@OrginalUserID,@OrgianlNickName,@TopicID,@Description,@HasReferUsers,@PhotoUrl,@TargetId,@Type,@ProductUrl,@ProductName,@ReplyExUrl,@ProductLinkUrl,@FavCount,@Price,@UserIP,@Status,@CreatedDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@ReplyType", SqlDbType.Int,4),
					new SqlParameter("@ReplyNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReplyUserID", SqlDbType.Int,4),
					new SqlParameter("@OriginalID", SqlDbType.Int,4),
					new SqlParameter("@OrginalDes", SqlDbType.NVarChar),
					new SqlParameter("@OrginalUserID", SqlDbType.Int,4),
					new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@HasReferUsers", SqlDbType.Bit,1),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@FavCount", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
			parameters[0].Value = model.GroupID;
			parameters[1].Value = model.ReplyType;
			parameters[2].Value = model.ReplyNickName;
			parameters[3].Value = model.ReplyUserID;
			parameters[4].Value = model.OriginalID;
			parameters[5].Value = model.OrginalDes;
			parameters[6].Value = model.OrginalUserID;
			parameters[7].Value = model.OrgianlNickName;
			parameters[8].Value = model.TopicID;
			parameters[9].Value = model.Description;
			parameters[10].Value = model.HasReferUsers;
			parameters[11].Value = model.PhotoUrl;
			parameters[12].Value = model.TargetId;
			parameters[13].Value = model.Type;
			parameters[14].Value = model.ProductUrl;
			parameters[15].Value = model.ProductName;
			parameters[16].Value = model.ReplyExUrl;
			parameters[17].Value = model.ProductLinkUrl;
			parameters[18].Value = model.FavCount;
			parameters[19].Value = model.Price;
			parameters[20].Value = model.UserIP;
			parameters[21].Value = model.Status;
			parameters[22].Value = model.CreatedDate;

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
		public bool Update(Maticsoft.Model.SNS.GroupTopicReply model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_GroupTopicReply set ");
			strSql.Append("GroupID=@GroupID,");
			strSql.Append("ReplyType=@ReplyType,");
			strSql.Append("ReplyNickName=@ReplyNickName,");
			strSql.Append("ReplyUserID=@ReplyUserID,");
			strSql.Append("OriginalID=@OriginalID,");
			strSql.Append("OrginalDes=@OrginalDes,");
			strSql.Append("OrginalUserID=@OrginalUserID,");
			strSql.Append("OrgianlNickName=@OrgianlNickName,");
			strSql.Append("TopicID=@TopicID,");
			strSql.Append("Description=@Description,");
			strSql.Append("HasReferUsers=@HasReferUsers,");
			strSql.Append("PhotoUrl=@PhotoUrl,");
			strSql.Append("TargetId=@TargetId,");
			strSql.Append("Type=@Type,");
			strSql.Append("ProductUrl=@ProductUrl,");
			strSql.Append("ProductName=@ProductName,");
			strSql.Append("ReplyExUrl=@ReplyExUrl,");
			strSql.Append("ProductLinkUrl=@ProductLinkUrl,");
			strSql.Append("FavCount=@FavCount,");
			strSql.Append("Price=@Price,");
			strSql.Append("UserIP=@UserIP,");
			strSql.Append("Status=@Status,");
			strSql.Append("CreatedDate=@CreatedDate");
			strSql.Append(" where ReplyID=@ReplyID");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@ReplyType", SqlDbType.Int,4),
					new SqlParameter("@ReplyNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReplyUserID", SqlDbType.Int,4),
					new SqlParameter("@OriginalID", SqlDbType.Int,4),
					new SqlParameter("@OrginalDes", SqlDbType.NVarChar),
					new SqlParameter("@OrginalUserID", SqlDbType.Int,4),
					new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@HasReferUsers", SqlDbType.Bit,1),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@FavCount", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ReplyID", SqlDbType.Int,4)};
			parameters[0].Value = model.GroupID;
			parameters[1].Value = model.ReplyType;
			parameters[2].Value = model.ReplyNickName;
			parameters[3].Value = model.ReplyUserID;
			parameters[4].Value = model.OriginalID;
			parameters[5].Value = model.OrginalDes;
			parameters[6].Value = model.OrginalUserID;
			parameters[7].Value = model.OrgianlNickName;
			parameters[8].Value = model.TopicID;
			parameters[9].Value = model.Description;
			parameters[10].Value = model.HasReferUsers;
			parameters[11].Value = model.PhotoUrl;
			parameters[12].Value = model.TargetId;
			parameters[13].Value = model.Type;
			parameters[14].Value = model.ProductUrl;
			parameters[15].Value = model.ProductName;
			parameters[16].Value = model.ReplyExUrl;
			parameters[17].Value = model.ProductLinkUrl;
			parameters[18].Value = model.FavCount;
			parameters[19].Value = model.Price;
			parameters[20].Value = model.UserIP;
			parameters[21].Value = model.Status;
			parameters[22].Value = model.CreatedDate;
			parameters[23].Value = model.ReplyID;

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
		public bool Delete(int ReplyID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_GroupTopicReply ");
			strSql.Append(" where ReplyID=@ReplyID");
			SqlParameter[] parameters = {
					new SqlParameter("@ReplyID", SqlDbType.Int,4)
			};
			parameters[0].Value = ReplyID;

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
		public bool DeleteList(string ReplyIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_GroupTopicReply ");
			strSql.Append(" where ReplyID in ("+ReplyIDlist + ")  ");
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
		public Maticsoft.Model.SNS.GroupTopicReply GetModel(int ReplyID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ReplyID,GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate from SNS_GroupTopicReply ");
			strSql.Append(" where ReplyID=@ReplyID");
			SqlParameter[] parameters = {
					new SqlParameter("@ReplyID", SqlDbType.Int,4)
			};
			parameters[0].Value = ReplyID;

			Maticsoft.Model.SNS.GroupTopicReply model=new Maticsoft.Model.SNS.GroupTopicReply();
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
		public Maticsoft.Model.SNS.GroupTopicReply DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.GroupTopicReply model=new Maticsoft.Model.SNS.GroupTopicReply();
			if (row != null)
			{
				if(row["ReplyID"]!=null && row["ReplyID"].ToString()!="")
				{
					model.ReplyID=int.Parse(row["ReplyID"].ToString());
				}
				if(row["GroupID"]!=null && row["GroupID"].ToString()!="")
				{
					model.GroupID=int.Parse(row["GroupID"].ToString());
				}
				if(row["ReplyType"]!=null && row["ReplyType"].ToString()!="")
				{
					model.ReplyType=int.Parse(row["ReplyType"].ToString());
				}
				if(row["ReplyNickName"]!=null)
				{
					model.ReplyNickName=row["ReplyNickName"].ToString();
				}
				if(row["ReplyUserID"]!=null && row["ReplyUserID"].ToString()!="")
				{
					model.ReplyUserID=int.Parse(row["ReplyUserID"].ToString());
				}
				if(row["OriginalID"]!=null && row["OriginalID"].ToString()!="")
				{
					model.OriginalID=int.Parse(row["OriginalID"].ToString());
				}
				if(row["OrginalDes"]!=null)
				{
					model.OrginalDes=row["OrginalDes"].ToString();
				}
				if(row["OrginalUserID"]!=null && row["OrginalUserID"].ToString()!="")
				{
					model.OrginalUserID=int.Parse(row["OrginalUserID"].ToString());
				}
				if(row["OrgianlNickName"]!=null)
				{
					model.OrgianlNickName=row["OrgianlNickName"].ToString();
				}
				if(row["TopicID"]!=null && row["TopicID"].ToString()!="")
				{
					model.TopicID=int.Parse(row["TopicID"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
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
				if(row["PhotoUrl"]!=null)
				{
					model.PhotoUrl=row["PhotoUrl"].ToString();
				}
				if(row["TargetId"]!=null && row["TargetId"].ToString()!="")
				{
					model.TargetId=int.Parse(row["TargetId"].ToString());
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["ProductUrl"]!=null)
				{
					model.ProductUrl=row["ProductUrl"].ToString();
				}
				if(row["ProductName"]!=null)
				{
					model.ProductName=row["ProductName"].ToString();
				}
				if(row["ReplyExUrl"]!=null)
				{
					model.ReplyExUrl=row["ReplyExUrl"].ToString();
				}
				if(row["ProductLinkUrl"]!=null)
				{
					model.ProductLinkUrl=row["ProductLinkUrl"].ToString();
				}
				if(row["FavCount"]!=null && row["FavCount"].ToString()!="")
				{
					model.FavCount=int.Parse(row["FavCount"].ToString());
				}
				if(row["Price"]!=null && row["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(row["Price"].ToString());
				}
				if(row["UserIP"]!=null)
				{
					model.UserIP=row["UserIP"].ToString();
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
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
			strSql.Append("select ReplyID,GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate ");
			strSql.Append(" FROM SNS_GroupTopicReply ");
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
			strSql.Append(" ReplyID,GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate ");
			strSql.Append(" FROM SNS_GroupTopicReply ");
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
			strSql.Append("select count(1) FROM SNS_GroupTopicReply ");
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
				strSql.Append("order by T.ReplyID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_GroupTopicReply T ");
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
			parameters[0].Value = "SNS_GroupTopicReply";
			parameters[1].Value = "ReplyID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

        #region 事务增加主题回复

        public int AddEx(Maticsoft.Model.SNS.GroupTopicReply Tmodel, Maticsoft.Model.SNS.Products PModel)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        object TargetID = "0";
                        object ReplyId = "0";
                        Tmodel.Type = (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal;
                        if ((PModel != null && PModel.ProductID > 0) || !string.IsNullOrEmpty(Tmodel.PhotoUrl))
                        {
                            TargetID = DbHelperSQL.GetSingle4Trans(GenerateImageInfo(Tmodel, PModel), transaction);
                            Tmodel.TargetId = Common.Globals.SafeInt(TargetID != null ? TargetID.ToString() : "", 0);
                            Tmodel.Type = PModel.ProductID > 0 ? (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product : (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo;
                            DbHelperSQL.GetSingle4Trans(GenerateUpdateUserEx(Tmodel.ReplyUserID, Tmodel.Type.Value), transaction);
                        }
                        ReplyId = DbHelperSQL.GetSingle4Trans(GenerateTopicReplyInfo(Tmodel), transaction);
                        DbHelperSQL.GetSingle4Trans(GenerateUpdateGroupEx(Tmodel.GroupID), transaction);
                        DbHelperSQL.GetSingle4Trans(GenerateUpdateTopicEx(Tmodel), transaction);
                        transaction.Commit();
                        return Common.Globals.SafeInt(ReplyId != null ? ReplyId.ToString() : "", 0);
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

        #region 增加回复主题
        public CommandInfo GenerateTopicReplyInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_GroupTopicReply(");
            strSql.Append("GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@GroupID,@ReplyType,@ReplyNickName,@ReplyUserID,@OriginalID,@OrginalDes,@OrginalUserID,@OrgianlNickName,@TopicID,@Description,@HasReferUsers,@PhotoUrl,@TargetId,@Type,@ProductUrl,@ProductName,@ReplyExUrl,@ProductLinkUrl,@FavCount,@Price,@UserIP,@Status,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@ReplyType", SqlDbType.Int,4),
					new SqlParameter("@ReplyNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReplyUserID", SqlDbType.Int,4),
					new SqlParameter("@OriginalID", SqlDbType.Int,4),
					new SqlParameter("@OrginalDes", SqlDbType.NVarChar),
					new SqlParameter("@OrginalUserID", SqlDbType.Int,4),
					new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@HasReferUsers", SqlDbType.Bit,1),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@FavCount", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.GroupID;
            parameters[1].Value = model.ReplyType;
            parameters[2].Value = model.ReplyNickName;
            parameters[3].Value = model.ReplyUserID;
            parameters[4].Value = 0;
            parameters[5].Value = model.OrginalDes;
            parameters[6].Value = model.OrginalUserID;
            parameters[7].Value = model.OrgianlNickName;
            parameters[8].Value = model.TopicID;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.HasReferUsers;
            parameters[11].Value = model.PhotoUrl;
            parameters[12].Value = model.TargetId;
            parameters[13].Value = model.Type;
            parameters[14].Value = model.ProductUrl;
            parameters[15].Value = model.ProductName;
            parameters[16].Value = model.ReplyExUrl;
            parameters[17].Value = model.ProductLinkUrl;
            parameters[18].Value =0;
            parameters[19].Value = model.Price;
            parameters[20].Value = model.UserIP;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.CreatedDate;
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
            else 
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Accounts_UsersExp set ShareCount=ShareCount+1,ProductsCount=ProductsCount+1,TopicCount=TopicCount+1 WHERE UserID=@UserID ");
                SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameters[0].Value = UserId;
                cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);
            }
            return cmd;
        }
        #endregion

        #region 主题表中相应的回复+1
        public CommandInfo GenerateUpdateTopicEx(Maticsoft.Model.SNS.GroupTopicReply TopicReply)
        {
            CommandInfo cmd = new CommandInfo();
            ///第一种情况，如果上传的图片，除过小组的主题加1外，用户分享的数量也加1
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupTopics set ReplyCount=ReplyCount+1,LastReplyNickName=@LastReplyNickName ,LastReplyUserId=@LastReplyUserId,LastPostTime=@LastPostTime WHERE TopicID=@TopicID ");
            SqlParameter[] parameters = { new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar, 200), new SqlParameter("@LastReplyUserId", SqlDbType.Int, 4), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            parameters[0].Value = TopicReply.ReplyNickName;
            parameters[1].Value = TopicReply.ReplyUserID;
            parameters[2].Value =DateTime.Now;  
            parameters[3].Value = TopicReply.TopicID;
            cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);

           

            return cmd;
        }
        #endregion

        #region 小组表中相应的回复+1
        public CommandInfo GenerateUpdateGroupEx(int GroupID)
        {
            CommandInfo cmd = new CommandInfo();
            ///第一种情况，如果上传的图片，除过小组的主题加1外，用户分享的数量也加1
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Groups set TopicReplyCount=TopicReplyCount+1 WHERE GroupID=@GroupID ");
            SqlParameter[] parameters = { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            parameters[0].Value = GroupID;
            cmd = new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows);
            return cmd;
        }
        #endregion

        #region 商品或图片表中相应的的插入数据
        private CommandInfo GenerateImageInfo(Maticsoft.Model.SNS.GroupTopicReply Tmodel, Maticsoft.Model.SNS.Products PModel)
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
                        new SqlParameter("@ProductUrl", SqlDbType.NVarChar,200),
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
                Tmodel.PhotoUrl = PModel.ThumbImageUrl;
                Tmodel.ProductName = PModel.ProductName;
                Tmodel.ProductLinkUrl = PModel.ProductUrl;
                Tmodel.Price = PModel.Price;
                CommandInfo cmd1 = new CommandInfo(strSql0.ToString(), parameters0);
                return new CommandInfo(strSql0.ToString(),
                                  parameters0, EffentNextType.ExcuteEffectRows);
            }
            #endregion
            #region  如果是图片想想要的图片表插入数据
            else if (!string.IsNullOrEmpty(Tmodel.PhotoUrl))
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
                string[] imagesurl = string.IsNullOrEmpty(Tmodel.PhotoUrl) ? null : Tmodel.PhotoUrl.Split('|');
                if (imagesurl != null && imagesurl.Length >= 2)
                {
                    parameters5[0].Value = imagesurl[0];
                    parameters5[7].Value = imagesurl[1];
                    Tmodel.PhotoUrl = imagesurl[0];
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
                parameters5[3].Value = Tmodel.ReplyUserID;
                parameters5[4].Value = Tmodel.ReplyNickName;
                parameters5[5].Value = Tmodel.CreatedDate;
                parameters5[6].Value = (int)Maticsoft.Model.SNS.EnumHelper.PhotoType.Group;

                CommandInfo cmd4 = new CommandInfo(strSql5.ToString(), parameters5, EffentNextType.ExcuteEffectRows);
                return cmd4;
            }
            return null;
            #endregion



        }
        #endregion

        #region 转发
        public int ForwardReply(Maticsoft.Model.SNS.GroupTopicReply TModel)
        {

            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        object TargetID = "0";
                        object ReplyId = "0";
                        //Tmodel.Type = (int)Maticsoft.Model.SNS.EnumHelper.ImageType.None;
                        //if ((PModel != null && PModel.ProductID > 0) || !string.IsNullOrEmpty(Tmodel.PhotoUrl))
                        //{
                        //    TargetID = DbHelperSQL.GetSingle4Trans(GenerateImageInfo(Tmodel, PModel), transaction);
                        //    Tmodel.TargetId = Common.Globals.SafeInt(TargetID != null ? TargetID.ToString() : "", 0);
                        //    Tmodel.Type = PModel.ProductID > 0 ? (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product : (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo;
                        //    DbHelperSQL.GetSingle4Trans(GenerateUpdateUserEx(Tmodel.ReplyUserID, Tmodel.Type.Value), transaction);
                        //}
                        ReplyId = DbHelperSQL.GetSingle4Trans(GenerateForwardReplyInfo(TModel), transaction);
                        DbHelperSQL.GetSingle4Trans(GenerateUpdateGroupInfo(TModel), transaction);
                        DbHelperSQL.GetSingle4Trans(GenerateUpdateUserExInfo(TModel), transaction);
                        DbHelperSQL.GetSingle4Trans(GenerateUpdateTopicInfo(TModel), transaction);
                        transaction.Commit();
                        return Common.Globals.SafeInt(ReplyId != null ? ReplyId.ToString() : "", 0);
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
        private CommandInfo GenerateForwardReplyInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_GroupTopicReply(");
            strSql.Append("GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@GroupID,@ReplyType,@ReplyNickName,@ReplyUserID,@OriginalID,@OrginalDes,@OrginalUserID,@OrgianlNickName,@TopicID,@Description,@HasReferUsers,@PhotoUrl,@TargetId,@Type,@ProductUrl,@ProductName,@ReplyExUrl,@ProductLinkUrl,@FavCount,@Price,@UserIP,@Status,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@ReplyType", SqlDbType.Int,4),
					new SqlParameter("@ReplyNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReplyUserID", SqlDbType.Int,4),
					new SqlParameter("@OriginalID", SqlDbType.Int,4),
					new SqlParameter("@OrginalDes", SqlDbType.NVarChar),
					new SqlParameter("@OrginalUserID", SqlDbType.Int,4),
					new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@HasReferUsers", SqlDbType.Bit,1),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@FavCount", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.GroupID;
            parameters[1].Value = model.ReplyType;
            parameters[2].Value = model.ReplyNickName;
            parameters[3].Value = model.ReplyUserID;
            parameters[4].Value = model.OriginalID;
            parameters[5].Value = model.OrginalDes;
            parameters[6].Value = model.OrginalUserID;
            parameters[7].Value = model.OrgianlNickName;
            parameters[8].Value = model.TopicID;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.HasReferUsers;
            parameters[11].Value = model.PhotoUrl;
            parameters[12].Value = model.TargetId;
            parameters[13].Value = model.Type;
            parameters[14].Value = model.ProductUrl;
            parameters[15].Value = model.ProductName;
            parameters[16].Value = model.ReplyExUrl;
            parameters[17].Value = model.ProductLinkUrl;
            parameters[18].Value = model.FavCount;
            parameters[19].Value = model.Price;
            parameters[20].Value = model.UserIP;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.CreatedDate;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            return new CommandInfo(strSql.ToString(),
                              parameters, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateUpdateGroupInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Groups set TopicCount=TopicCount+1,TopicReplyCount=TopicReplyCount+1 ");

            strSql.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters = {
                        new SqlParameter("@GroupID", SqlDbType.Int,4)};
            parameters[0].Value = model.GroupID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            return new CommandInfo(strSql.ToString(),
                              parameters, EffentNextType.ExcuteEffectRows);
        }


        private CommandInfo GenerateUpdateTopicInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupTopics set ReplyCount=ReplyCount+1");

            strSql.Append(" where TopicID=@TopicID");
            SqlParameter[] parameters = {
                        new SqlParameter("@TopicID", SqlDbType.Int,4)};
            parameters[0].Value = model.TopicID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            return new CommandInfo(strSql.ToString(),
                              parameters, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateUpdateUserExInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UsersExp set TopicCount=TopicCount+1 ");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
                        new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = model.ReplyUserID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            return new CommandInfo(strSql.ToString(),
                              parameters, EffentNextType.ExcuteEffectRows);
        }
        #region 获取数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM  (SELECT tp.*,gp.GroupName,gp.Title FROM SNS_GroupTopicReply tp LEFT JOIN SNS_GroupTopics gp ON tp.TopicID=gp.TopicID) temp");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        } 
        #endregion


        #region 删除ids集合所对应的数据
        /// <summary>
        /// 删除所对应的数据列表
        /// </summary>
        public bool DeleteListEx(string ReplyIDlist)
        {
            string[] ReplyIDs = ReplyIDlist.Split(',');
            if (ReplyIDs.Length > 0)
            {
                foreach (string i in ReplyIDs)
                {
                    int TopicId = Common.Globals.SafeInt(i, 0);
                    if (!DeleteEx(TopicId))
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        
        #endregion


        #region 删除一条数据
        public bool DeleteEx(int ReplyId)
        {
            Maticsoft.Model.SNS.GroupTopicReply model = GetModel(ReplyId);
            if (model== null)
            {
                return false;
            }
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //根据GroupID更新SNS_Groups的TopicCount：TopicCount-1
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update SNS_Groups set ");
            strSql1.Append("TopicReplyCount=TopicReplyCount-1");
            strSql1.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters1 = {
					new SqlParameter("@GroupID", SqlDbType.Int,4)};
            parameters1[0].Value = model.GroupID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            //根据TopicID更新SNS_Groups的TopicCount：TopicCount-1
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update SNS_GroupTopics set ");
            strSql2.Append("ReplyCount=ReplyCount-1");
            strSql2.Append(" where TopicID=@TopicID");
            SqlParameter[] parameters2 = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)};
            parameters2[0].Value = model.TopicID;
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd2);
            //根据ReplyID删除SNS_GroupTopicReply的相关数据
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from SNS_GroupTopicReply ");
            strSql5.Append(" where ReplyID=@ReplyID");
            SqlParameter[] parameters5 = {
					new SqlParameter("@ReplyID", SqlDbType.Int,4)
			};
            parameters5[0].Value = model.ReplyID;
            CommandInfo cmd5 = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd5);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupTopicReply set ");
            strSql.Append("OrginalDes=@OrginalDes,");
            strSql.Append("OrginalUserID=@OrginalUserID,");
            strSql.Append("OrgianlNickName=@OrgianlNickName,");
            strSql.Append("PhotoUrl=@PhotoUrl,");
            strSql.Append("TargetId=@TargetId,");
            strSql.Append("ProductUrl=@ProductUrl,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ReplyExUrl=@ReplyExUrl,");
            strSql.Append("ProductLinkUrl=@ProductLinkUrl");
            strSql.Append(" where OriginalID=@OriginalID");
            SqlParameter[] parameters = {
					new SqlParameter("@OriginalID", SqlDbType.Int,4),
					new SqlParameter("@OrginalDes", SqlDbType.NVarChar),
					new SqlParameter("@OrginalUserID", SqlDbType.Int,4),
					new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.ReplyID;
            parameters[1].Value = "";
            parameters[2].Value = 0;
            parameters[3].Value ="";
            parameters[4].Value = "";
            parameters[5].Value =0;
            parameters[6].Value = "";
            parameters[7].Value = "";
            parameters[8].Value ="";
            parameters[9].Value ="";
            CommandInfo cmd6 = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd6);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
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
            strSql.Append("update SNS_GroupTopicReply set Status=" + (int)status + " ");
            strSql.Append(" where ReplyID in (" + IdsStr + ")  ");
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
		#endregion  ExtensionMethod
	}
}

