/**
* Comments.cs
*
* 功 能： N/A
* 类 名： Comments
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.SNS;

namespace Maticsoft.SQLServerDAL.SNS
{
    /// <summary>
    /// 数据访问类:Comments
    /// </summary>
    public partial class Comments : IComments
    {
        public Comments()
        { }

        #region BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Comments model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Comments(");
            strSql.Append("Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@Type,@TargetId,@ParentID,@CreatedUserID,@CreatedNickName,@HasReferUser,@Description,@IsRead,@Status,@ReplyCount,@UserIP,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@HasReferUser", SqlDbType.Bit,1),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Type;
            parameters[1].Value = model.TargetId;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.CreatedNickName;
            parameters[5].Value = model.HasReferUser;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.IsRead;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.ReplyCount;
            parameters[10].Value = model.UserIP;
            parameters[11].Value = model.CreatedDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(Maticsoft.Model.SNS.Comments model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Comments set ");
            strSql.Append("Type=@Type,");
            strSql.Append("TargetId=@TargetId,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("CreatedNickName=@CreatedNickName,");
            strSql.Append("HasReferUser=@HasReferUser,");
            strSql.Append("Description=@Description,");
            strSql.Append("IsRead=@IsRead,");
            strSql.Append("Status=@Status,");
            strSql.Append("ReplyCount=@ReplyCount,");
            strSql.Append("UserIP=@UserIP,");
            strSql.Append("CreatedDate=@CreatedDate");
            strSql.Append(" where CommentID=@CommentID");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@HasReferUser", SqlDbType.Bit,1),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CommentID", SqlDbType.Int,4)};
            parameters[0].Value = model.Type;
            parameters[1].Value = model.TargetId;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.CreatedNickName;
            parameters[5].Value = model.HasReferUser;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.IsRead;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.ReplyCount;
            parameters[10].Value = model.UserIP;
            parameters[11].Value = model.CreatedDate;
            parameters[12].Value = model.CommentID;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CommentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Comments ");
            strSql.Append(" where CommentID=@CommentID");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)
			};
            parameters[0].Value = CommentID;

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

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string CommentIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Comments ");
            strSql.Append(" where CommentID in (" + CommentIDlist + ")  ");
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Comments GetModel(int CommentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CommentID,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate from SNS_Comments ");
            strSql.Append(" where CommentID=@CommentID");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)
			};
            parameters[0].Value = CommentID;

            Maticsoft.Model.SNS.Comments model = new Maticsoft.Model.SNS.Comments();
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Comments DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Comments model = new Maticsoft.Model.SNS.Comments();
            if (row != null)
            {
                if (row["CommentID"] != null && row["CommentID"].ToString() != "")
                {
                    model.CommentID = int.Parse(row["CommentID"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["TargetId"] != null && row["TargetId"].ToString() != "")
                {
                    model.TargetId = int.Parse(row["TargetId"].ToString());
                }
                if (row["ParentID"] != null && row["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    model.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["HasReferUser"] != null && row["HasReferUser"].ToString() != "")
                {
                    if ((row["HasReferUser"].ToString() == "1") || (row["HasReferUser"].ToString().ToLower() == "true"))
                    {
                        model.HasReferUser = true;
                    }
                    else
                    {
                        model.HasReferUser = false;
                    }
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["IsRead"] != null && row["IsRead"].ToString() != "")
                {
                    if ((row["IsRead"].ToString() == "1") || (row["IsRead"].ToString().ToLower() == "true"))
                    {
                        model.IsRead = true;
                    }
                    else
                    {
                        model.IsRead = false;
                    }
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["ReplyCount"] != null && row["ReplyCount"].ToString() != "")
                {
                    model.ReplyCount = int.Parse(row["ReplyCount"].ToString());
                }
                if (row["UserIP"] != null)
                {
                    model.UserIP = row["UserIP"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommentID,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate ");
            strSql.Append(" FROM SNS_Comments ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" CommentID,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate ");
            strSql.Append(" FROM SNS_Comments ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_Comments ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.CommentID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Comments T ");
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
            parameters[0].Value = "SNS_Comments";
            parameters[1].Value = "CommentID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion BasicMethod

        #region ExtensionMethod

        /// <summary>
        /// 增加一条新的评论，给对应的type的评论数量相应的加1；
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddEx(Maticsoft.Model.SNS.Comments model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Comments(");
            strSql.Append("Status,ReplyCount,UserIP,CreatedDate,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead)");
            strSql.Append(" values (");
            strSql.Append("@Status,@ReplyCount,@UserIP,@CreatedDate,@Type,@TargetId,@ParentID,@CreatedUserID,@CreatedNickName,@HasReferUser,@Description,@IsRead)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@HasReferUser", SqlDbType.Bit,1),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.Status;
            parameters[1].Value = model.ReplyCount;
            parameters[2].Value = model.UserIP;
            parameters[3].Value = model.CreatedDate;
            parameters[4].Value = model.Type;
            parameters[5].Value = model.TargetId;
            parameters[6].Value = model.ParentID;
            parameters[7].Value = model.CreatedUserID;
            parameters[8].Value = model.CreatedNickName;
            parameters[9].Value = model.HasReferUser;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.IsRead;
            parameters[12].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            #region 如果评论的对象是图片类型或者商品的类型，则对图片或者商品的评论总数相应的加1，同时在动态中发表此图片的动态的评论数也要相应的加1

            if (model.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo || model.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product)
            {
                #region 图片或商品的评论加1

                StringBuilder strSql2 = new StringBuilder();
                if (model.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo)
                {
                    strSql2.Append("Update  SNS_Photos ");
                }
                else
                {
                    strSql2.Append("Update  SNS_Products ");
                }
                strSql2.Append(" Set CommentCount=CommentCount+1 ");
                if (model.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo)
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
                parameters2[0].Value = model.TargetId;
                CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd2);

                #endregion 图片或商品的评论加1

                #region 对应的动态加1

                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("Update  SNS_Posts ");
                strSql3.Append(" Set CommentCount=CommentCount+1 ");
                strSql3.Append(" where TargetId=@TargetId and Type=@Type");

                SqlParameter[] parameters3 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4),
                    	new SqlParameter("@Type", SqlDbType.Int,4),
		          };
                parameters3[0].Value = model.TargetId;
                parameters3[1].Value = model.Type;
                CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
                sqllist.Add(cmd3);

                #endregion 对应的动态加1
            }

            #endregion 如果评论的对象是图片类型或者商品的类型，则对图片或者商品的评论总数相应的加1，同时在动态中发表此图片的动态的评论数也要相应的加1

            #region 如果评论的对象是一般动态的类型，则对应的动态动态的评论数加1

            else
            {
                #region 对应的动态加1

                StringBuilder strSql4 = new StringBuilder();
                strSql4.Append("Update  SNS_Posts ");
                strSql4.Append(" Set CommentCount=CommentCount+1 ");
                strSql4.Append(" where PostID=@TargetId and Type=@Type");
                SqlParameter[] parameters4 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4),
                    	new SqlParameter("@Type", SqlDbType.Int,4),
		          };
                parameters4[0].Value = model.TargetId;
                parameters4[1].Value = model.Type;
                CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), parameters4);
                sqllist.Add(cmd4);

                #endregion 对应的动态加1
            }

            #endregion 如果评论的对象是一般动态的类型，则对应的动态动态的评论数加1

            ///执行相应的事务返回相应的增加成功评论的id
            DbHelperSQL.ExecuteSqlTran(sqllist);
            return (int)parameters[12].Value;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TargetId, int Type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Comments ");
            strSql.Append(" where TargetId=@TargetId  AND Type=@Type");
            SqlParameter[] parameters = {
                    new SqlParameter("@TargetId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
			};
            parameters[0].Value = TargetId;
            parameters[1].Value = Type;

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

        /// <summary>
        /// 专辑评论信息
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <returns></returns>
        public DataSet AblumComment(int ablumId,string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM( ");
            strSql.Append("SELECT C.CommentID,C.CreatedNickName,C.CreatedDate,c.Description,C.UserIP ,C.Type,C.TargetId,PP.ThumbImageUrl,AlbumID FROM SNS_Comments C, ");
            strSql.Append("(SELECT P.PhotoID, U.CreatedUserID,P.ThumbImageUrl,U.AlbumID FROM SNS_Photos P , ");
            strSql.Append("(SELECT TargetID FROM SNS_UserAlbumDetail WHERE AlbumID = @AlbumID AND Type = 0 ) UAD , ");
            strSql.Append("(SELECT CreatedUserID,AlbumID FROM SNS_UserAlbums WHERE AlbumID = @AlbumID ) U ");
            strSql.Append("WHERE P.PhotoID = UAD.TargetID AND P.CreatedUserID = U.CreatedUserID)PP ");
            strSql.Append("WHERE C.TargetId=PP.PhotoID  AND C.Type=3");
            strSql.Append("UNION ALL ");
            strSql.Append("SELECT C.CommentID,C.CreatedNickName,C.CreatedDate,c.Description,C.UserIP,C.Type,C.TargetId,PP.ThumbImageUrl,AlbumID FROM SNS_Comments C, ");
            strSql.Append("(SELECT P.ProductID , U.CreatedUserID,P.ThumbImageUrl,U.AlbumID FROM SNS_Products P , ");
            strSql.Append("(SELECT TargetID FROM SNS_UserAlbumDetail WHERE AlbumID = @AlbumID AND Type = 1 ) UAD , ");
            strSql.Append("(SELECT CreatedUserID,AlbumID FROM SNS_UserAlbums WHERE AlbumID =@AlbumID ) U ");
            strSql.Append("WHERE P.ProductID = UAD.TargetID AND P.CreateUserID = U.CreatedUserID)PP ");
            strSql.Append("WHERE C.TargetId=PP.ProductID AND C.Type=3)A ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.AppendFormat("WHERE A.CreatedNickName LIKE '%{0}%'",strWhere);
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters[0].Value = ablumId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专辑评论
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <param name="commentId">评论ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteComment(int ablumId,int commentId)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            #region 删除评论

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM SNS_Comments  ");
            strSql.Append(" WHERE CommentID=@CommentID");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)
			};
            parameters[0].Value = commentId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            #endregion 

            #region 专辑表中对应的评论数-1

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("UPDATE SNS_UserAlbums SET CommentsCount=(CASE WHEN CommentsCount > 0 THEN CommentsCount-1 ELSE 0 END ) ");
            strSql1.Append(" WHERE AlbumID=@AlbumID ");
            SqlParameter[] parameters1 = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters1[0].Value = ablumId;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            #endregion 

            
           return DbHelperSQL.ExecuteSqlTran(sqllist)>0;
        }

        public bool DeleteListEx(string CommentIDlist)
        { 
         int result;
       SqlParameter[] parameters = {
                    new SqlParameter("@TargetIds ", SqlDbType.NVarChar),
                    DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)};
               parameters[0].Value = CommentIDlist;
             DbHelperSQL.RunProcedure("sp_SNS_CommentDeleteAction", parameters, out result);
             if (result > 0)
             {
                 return true;
             }
             return false;
        }
        #endregion ExtensionMethod
    }
}