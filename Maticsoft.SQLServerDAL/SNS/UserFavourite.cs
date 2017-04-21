/**
* UserFavourite.cs
*
* 功 能： N/A
* 类 名： UserFavourite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:04   N/A    初版
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
	/// 数据访问类:UserFavourite
	/// </summary>
	public partial class UserFavourite:IUserFavourite
	{
		public UserFavourite()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int CreatedUserID, int Type, int TargetID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_UserFavourite");
            strSql.Append(" where CreatedUserID=@CreatedUserID and Type=@Type and TargetID=@TargetID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),                    
					new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@TargetID", SqlDbType.Int,4)
			};
            parameters[0].Value = CreatedUserID;
            parameters[1].Value = Type;
            parameters[2].Value = TargetID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.UserFavourite model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_UserFavourite(");
			strSql.Append("TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate)");
			strSql.Append(" values (");
			strSql.Append("@TargetID,@Type,@CreatedUserID,@CreatedNickName,@OwnerUserID,@OwnerNickName,@Description,@Tags,@CreatedDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@OwnerUserID", SqlDbType.Int,4),
					new SqlParameter("@OwnerNickName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
			parameters[0].Value = model.TargetID;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.CreatedUserID;
			parameters[3].Value = model.CreatedNickName;
			parameters[4].Value = model.OwnerUserID;
			parameters[5].Value = model.OwnerNickName;
			parameters[6].Value = model.Description;
			parameters[7].Value = model.Tags;
			parameters[8].Value = model.CreatedDate;

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
		public bool Update(Maticsoft.Model.SNS.UserFavourite model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_UserFavourite set ");
			strSql.Append("TargetID=@TargetID,");
			strSql.Append("Type=@Type,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CreatedNickName=@CreatedNickName,");
			strSql.Append("OwnerUserID=@OwnerUserID,");
			strSql.Append("OwnerNickName=@OwnerNickName,");
			strSql.Append("Description=@Description,");
			strSql.Append("Tags=@Tags,");
			strSql.Append("CreatedDate=@CreatedDate");
			strSql.Append(" where FavouriteID=@FavouriteID");
			SqlParameter[] parameters = {
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@OwnerUserID", SqlDbType.Int,4),
					new SqlParameter("@OwnerNickName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@FavouriteID", SqlDbType.Int,4)};
			parameters[0].Value = model.TargetID;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.CreatedUserID;
			parameters[3].Value = model.CreatedNickName;
			parameters[4].Value = model.OwnerUserID;
			parameters[5].Value = model.OwnerNickName;
			parameters[6].Value = model.Description;
			parameters[7].Value = model.Tags;
			parameters[8].Value = model.CreatedDate;
			parameters[9].Value = model.FavouriteID;

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
		public bool Delete(int FavouriteID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_UserFavourite ");
			strSql.Append(" where FavouriteID=@FavouriteID");
			SqlParameter[] parameters = {
					new SqlParameter("@FavouriteID", SqlDbType.Int,4)
			};
			parameters[0].Value = FavouriteID;

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
		public bool DeleteList(string FavouriteIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_UserFavourite ");
			strSql.Append(" where FavouriteID in ("+FavouriteIDlist + ")  ");
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
		public Maticsoft.Model.SNS.UserFavourite GetModel(int FavouriteID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 FavouriteID,TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate from SNS_UserFavourite ");
			strSql.Append(" where FavouriteID=@FavouriteID");
			SqlParameter[] parameters = {
					new SqlParameter("@FavouriteID", SqlDbType.Int,4)
			};
			parameters[0].Value = FavouriteID;

			Maticsoft.Model.SNS.UserFavourite model=new Maticsoft.Model.SNS.UserFavourite();
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
		public Maticsoft.Model.SNS.UserFavourite DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.UserFavourite model=new Maticsoft.Model.SNS.UserFavourite();
			if (row != null)
			{
				if(row["FavouriteID"]!=null && row["FavouriteID"].ToString()!="")
				{
					model.FavouriteID=int.Parse(row["FavouriteID"].ToString());
				}
				if(row["TargetID"]!=null && row["TargetID"].ToString()!="")
				{
					model.TargetID=int.Parse(row["TargetID"].ToString());
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["CreatedUserID"]!=null && row["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(row["CreatedUserID"].ToString());
				}
				if(row["CreatedNickName"]!=null)
				{
					model.CreatedNickName=row["CreatedNickName"].ToString();
				}
				if(row["OwnerUserID"]!=null && row["OwnerUserID"].ToString()!="")
				{
					model.OwnerUserID=int.Parse(row["OwnerUserID"].ToString());
				}
				if(row["OwnerNickName"]!=null)
				{
					model.OwnerNickName=row["OwnerNickName"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
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
			strSql.Append("select FavouriteID,TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate ");
			strSql.Append(" FROM SNS_UserFavourite ");
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
			strSql.Append(" FavouriteID,TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate ");
			strSql.Append(" FROM SNS_UserFavourite ");
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
			strSql.Append("select count(1) FROM SNS_UserFavourite ");
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
				strSql.Append("order by T.FavouriteID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_UserFavourite T ");
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
			parameters[0].Value = "SNS_UserFavourite";
			parameters[1].Value = "FavouriteID";
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
        /// 增加一条数据
        /// </summary>
        public bool AddEx(Maticsoft.Model.SNS.UserFavourite model,int TopicId, int ReplyId)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            #region 加入喜欢表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserFavourite(");
            strSql.Append("TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@TargetID,@Type,@CreatedUserID,@CreatedNickName,@OwnerUserID,@OwnerNickName,@Description,@Tags,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@OwnerUserID", SqlDbType.Int,4),
					new SqlParameter("@OwnerNickName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.TargetID;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.CreatedUserID;
            parameters[3].Value = model.CreatedNickName;
            parameters[4].Value = model.OwnerUserID;
            parameters[5].Value = model.OwnerNickName;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.Tags;
            parameters[8].Value = model.CreatedDate;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            #endregion

            #region 在相应的商品和图片中相应的喜欢数量增加1
            StringBuilder strSql2 = new StringBuilder();
            if (model.Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo)
            {
                strSql2.Append("Update  SNS_Photos ");
            }
            else
            {
                strSql2.Append("Update  SNS_Products ");
            }
            strSql2.Append(" Set FavouriteCount=FavouriteCount+1 ");
            if (model.Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo)
            {
                strSql2.Append(" where PhotoID=@TargetId");
            }
            else
            {
                strSql2.Append(" where ProductID=@TargetId");

            }
            SqlParameter[] parameters2 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)
		          };
            parameters2[0].Value = model.TargetID;
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd2);
            #endregion

            #region 给用户表中自己喜欢的数量加1
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("update Accounts_UsersExp Set FavouritesCount=FavouritesCount+1 ");
            strSql3.Append(" where UserID=@UserID");
            SqlParameter[] parameters3 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
		          };
            parameters3[0].Value = model.CreatedUserID;
            CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd3);
            #endregion

            #region 给用户表中被自己喜欢的用户的被喜欢数量加1
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Accounts_UsersExp Set FavoritedCount=FavoritedCount+1 ");
            strSql4.Append(" where UserID=@UserID");
            SqlParameter[] parameters4 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
		          };
            parameters4[0].Value = model.OwnerUserID;
            CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd4);
            #endregion

            #region   相应微博表 主题表 主题回复表中喜欢数量加1
            if (TopicId>0)
            {
                StringBuilder strSql6= new StringBuilder();
                strSql6.Append("update SNS_GroupTopics Set FavCount=FavCount+1 ");
                strSql6.Append(" where TopicID=@TopicID");
                SqlParameter[] parameters6 = {
					new SqlParameter("@TopicID", SqlDbType.Int,4)
                  
		          };
                parameters6[0].Value = TopicId;
                CommandInfo cmd6 = new CommandInfo(strSql6.ToString(), parameters6);
                sqllist.Add(cmd6);
            
            }
            else if (ReplyId > 0)
            {
                StringBuilder strSql7 = new StringBuilder();
                strSql7.Append("update SNS_GroupTopicReply Set FavCount=FavCount+1 ");
                strSql7.Append(" where ReplyID=@ReplyID");
                SqlParameter[] parameters7 = {
					new SqlParameter("@ReplyID", SqlDbType.Int,4)
                  
		          };
                parameters7[0].Value = ReplyId;
                CommandInfo cmd7 = new CommandInfo(strSql7.ToString(), parameters7);
                sqllist.Add(cmd7);
            }
            else
            {
                StringBuilder strSql5 = new StringBuilder();
                strSql5.Append("update SNS_Posts Set FavCount=FavCount+1 ");
                strSql5.Append(" where TargetId=@TargetId and Type=@Type");
                SqlParameter[] parameters5 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
		          };
                parameters5[0].Value = model.TargetID;
                parameters5[1].Value = model.Type + 1;
                CommandInfo cmd5 = new CommandInfo(strSql5.ToString(), parameters5);
                sqllist.Add(cmd5);
            }
            #endregion
            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
        }




        /// <summary>
        /// 获得喜欢的分页列表
        /// </summary>
        public DataSet GetFavListByPage(int UserId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.FavouriteID desc");
            }
            strSql.Append(")AS Row, T.*  from ");
            strSql.Append(" (select uad.FavouriteID,p.ProductID TargetID,p.ProductName TargetName,p.ShareDescription Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,p.Price,1 Type");
            strSql.AppendFormat(" from (select FavouriteID,TargetID from SNS_UserFavourite where CreatedUserID={0} and Type=1) uad ", UserId);
            strSql.Append(" inner join SNS_Products p on uad.TargetID=p.ProductID  ");
            strSql.Append(" union");
            strSql.Append(" select uad.FavouriteID,p.PhotoID TargetID,p.PhotoName TargetName,p.Description Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,0 price,0 Type ");
            strSql.AppendFormat(" from (select FavouriteID,TargetID from SNS_UserFavourite where CreatedUserID={0} and Type=0) uad ", UserId);
            strSql.Append(" inner join SNS_Photos p on uad.TargetID=p.PhotoID) T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);

            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 删除喜欢的动作
        /// </summary>
        /// <param name="TargetId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool DeleteEx(int UserId,int TargetId,int Type)
        {

            List<CommandInfo> sqllist = new List<CommandInfo>();
            #region 删除喜欢的数据
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from SNS_UserFavourite ");
            strSql1.Append(" where TargetID=@TargetID and Type=@Type");
            SqlParameter[] parameters1 = {
					new SqlParameter("@TargetID", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
			};
            parameters1[0].Value = TargetId;
            parameters1[1].Value = Type;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);
            #endregion

            #region  喜欢数量-1
            StringBuilder strSql = new StringBuilder();
            if (Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product)
            {
                strSql.Append("update SNS_Products set FavouriteCount=FavouriteCount-1");
                strSql.Append(" where ProductID=@TargetID ");
            }
            else
            {
                strSql.Append("update SNS_Photos set FavouriteCount=FavouriteCount-1");   
                strSql.Append(" where PhotoID=@TargetID  ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@TargetID", SqlDbType.Int,4)
			};
            parameters[0].Value = TargetId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            #endregion

            #region  用户喜欢数量-1
            StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("update Accounts_UsersExp set FavouritesCount=FavouritesCount-1");
                strSql3.Append(" where UserID=@UserID  ");

                SqlParameter[] parameters3 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters3[0].Value = UserId;
            CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd3);
            #endregion

            #region Post表中的喜欢-1
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("update SNS_Posts Set FavCount=FavCount-1 ");
            strSql5.Append(" where TargetId=@TargetId and Type=@Type");
            SqlParameter[] parameters5 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
		          };
            parameters5[0].Value = TargetId;
            parameters5[1].Value = Type + 1;
            CommandInfo cmd5 = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd5); 
            #endregion

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;

        }

		#endregion  ExtensionMethod
	}
}

