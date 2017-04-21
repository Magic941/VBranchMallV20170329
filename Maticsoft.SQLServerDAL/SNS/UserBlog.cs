/**
* UserBlog.cs
*
* 功 能： N/A
* 类 名： UserBlog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/3 12:08:16   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:UserBlog
	/// </summary>
	public partial class UserBlog:IUserBlog
	{
		public UserBlog()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("BlogID", "SNS_UserBlog");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BlogID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_UserBlog");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)
			};
            parameters[0].Value = BlogID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.UserBlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserBlog(");
            strSql.Append("Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Summary,@Description,@UserID,@UserName,@LinkUrl,@Status,@Keywords,@Recomend,@Attachment,@Remark,@PvCount,@TotalComment,@TotalFav,@TotalShare,@Meta_Title,@Meta_Description,@Meta_Keywords,@SeoUrl,@StaticUrl,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Summary", SqlDbType.NVarChar,300),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,50),
					new SqlParameter("@Recomend", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@TotalComment", SqlDbType.Int,4),
					new SqlParameter("@TotalFav", SqlDbType.Int,4),
					new SqlParameter("@TotalShare", SqlDbType.Int,4),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Summary;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.UserID;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.LinkUrl;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.Keywords;
            parameters[8].Value = model.Recomend;
            parameters[9].Value = model.Attachment;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.PvCount;
            parameters[12].Value = model.TotalComment;
            parameters[13].Value = model.TotalFav;
            parameters[14].Value = model.TotalShare;
            parameters[15].Value = model.Meta_Title;
            parameters[16].Value = model.Meta_Description;
            parameters[17].Value = model.Meta_Keywords;
            parameters[18].Value = model.SeoUrl;
            parameters[19].Value = model.StaticUrl;
            parameters[20].Value = model.CreatedDate;

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
        public bool Update(Maticsoft.Model.SNS.UserBlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserBlog set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Description=@Description,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("LinkUrl=@LinkUrl,");
            strSql.Append("Status=@Status,");
            strSql.Append("Keywords=@Keywords,");
            strSql.Append("Recomend=@Recomend,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("PvCount=@PvCount,");
            strSql.Append("TotalComment=@TotalComment,");
            strSql.Append("TotalFav=@TotalFav,");
            strSql.Append("TotalShare=@TotalShare,");
            strSql.Append("Meta_Title=@Meta_Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords,");
            strSql.Append("SeoUrl=@SeoUrl,");
            strSql.Append("StaticUrl=@StaticUrl,");
            strSql.Append("CreatedDate=@CreatedDate");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Summary", SqlDbType.NVarChar,300),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,50),
					new SqlParameter("@Recomend", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@TotalComment", SqlDbType.Int,4),
					new SqlParameter("@TotalFav", SqlDbType.Int,4),
					new SqlParameter("@TotalShare", SqlDbType.Int,4),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@BlogID", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Summary;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.UserID;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.LinkUrl;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.Keywords;
            parameters[8].Value = model.Recomend;
            parameters[9].Value = model.Attachment;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.PvCount;
            parameters[12].Value = model.TotalComment;
            parameters[13].Value = model.TotalFav;
            parameters[14].Value = model.TotalShare;
            parameters[15].Value = model.Meta_Title;
            parameters[16].Value = model.Meta_Description;
            parameters[17].Value = model.Meta_Keywords;
            parameters[18].Value = model.SeoUrl;
            parameters[19].Value = model.StaticUrl;
            parameters[20].Value = model.CreatedDate;
            parameters[21].Value = model.BlogID;

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
        public bool Delete(int BlogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserBlog ");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)
			};
            parameters[0].Value = BlogID;

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
        public bool DeleteList(string BlogIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserBlog ");
            strSql.Append(" where BlogID in (" + BlogIDlist + ")  ");
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
        public Maticsoft.Model.SNS.UserBlog GetModel(int BlogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BlogID,Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate from SNS_UserBlog ");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)
			};
            parameters[0].Value = BlogID;

            Maticsoft.Model.SNS.UserBlog model = new Maticsoft.Model.SNS.UserBlog();
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
        public Maticsoft.Model.SNS.UserBlog DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserBlog model = new Maticsoft.Model.SNS.UserBlog();
            if (row != null)
            {
                if (row["BlogID"] != null && row["BlogID"].ToString() != "")
                {
                    model.BlogID = int.Parse(row["BlogID"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Summary"] != null)
                {
                    model.Summary = row["Summary"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["LinkUrl"] != null)
                {
                    model.LinkUrl = row["LinkUrl"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Keywords"] != null)
                {
                    model.Keywords = row["Keywords"].ToString();
                }
                if (row["Recomend"] != null && row["Recomend"].ToString() != "")
                {
                    model.Recomend = int.Parse(row["Recomend"].ToString());
                }
                if (row["Attachment"] != null)
                {
                    model.Attachment = row["Attachment"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["PvCount"] != null && row["PvCount"].ToString() != "")
                {
                    model.PvCount = int.Parse(row["PvCount"].ToString());
                }
                if (row["TotalComment"] != null && row["TotalComment"].ToString() != "")
                {
                    model.TotalComment = int.Parse(row["TotalComment"].ToString());
                }
                if (row["TotalFav"] != null && row["TotalFav"].ToString() != "")
                {
                    model.TotalFav = int.Parse(row["TotalFav"].ToString());
                }
                if (row["TotalShare"] != null && row["TotalShare"].ToString() != "")
                {
                    model.TotalShare = int.Parse(row["TotalShare"].ToString());
                }
                if (row["Meta_Title"] != null)
                {
                    model.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    model.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    model.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["SeoUrl"] != null)
                {
                    model.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["StaticUrl"] != null)
                {
                    model.StaticUrl = row["StaticUrl"].ToString();
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
            strSql.Append("select BlogID,Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate ");
            strSql.Append(" FROM SNS_UserBlog ");
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
            strSql.Append(" BlogID,Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate ");
            strSql.Append(" FROM SNS_UserBlog ");
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
            strSql.Append("select count(1) FROM SNS_UserBlog ");
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
                strSql.Append("order by T.BlogID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_UserBlog T ");
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
            parameters[0].Value = "SNS_UserBlog";
            parameters[1].Value = "BlogID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

	    public bool UpdatePvCount(int id)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserBlog set ");
            strSql.Append("PvCount=PvCount+1 ");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)};
            parameters[0].Value = id;
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


        public bool UpdateFavCount(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserBlog set ");
            strSql.Append("TotalFav=TotalFav+1 ");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)};
            parameters[0].Value = id;
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


	    public int GetPvCount(int id)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PvCount from SNS_UserBlog ");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

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

        public bool UpdateCommentCount(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            #region 更新动作
           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserBlog set ");
            strSql.Append("TotalComment=TotalComment+1 ");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)};
            parameters[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

         
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("Update SNS_Posts Set CommentCount=CommentCount+1 where type=4 and  TargetId=@TargetId ");
            SqlParameter[] parameters6 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters6[0].Value = id;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);

            #endregion 更新动作

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }

	    public DataSet GetActiveUser(int top )
	    {
            StringBuilder strSql = new StringBuilder();
	        strSql.Append("SELECT ");
	        if (top > 0)
	        {
	            strSql.Append(" top " + top);
	        }
	 strSql.Append(" UserID,UserName FROM    SNS_UserBlog WHERE Status=1 GROUP BY UserID,UserName ORDER BY COUNT(UserID) DESC");
            return DbHelperSQL.Query(strSql.ToString());
	    }


	    public bool DeleteEx(int BlogID)
	    {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            #region 级联删除
            //删除长微博数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserBlog ");
            strSql.Append(" where BlogID=@BlogID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogID", SqlDbType.Int,4)};
            parameters[0].Value = BlogID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            //删除动态数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from SNS_Posts where type=4 and TargetId=@TargetId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters2[0].Value = BlogID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除评论数据
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("delete from SNS_Comments where type=4 and TargetId=@TargetId  ");
            SqlParameter[] parameters6 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters6[0].Value = BlogID;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);

            #endregion 

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
	    }

        public bool UpdateStatusList(string ids, int Status)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserBlog set ");
            strSql.Append("Status=@Status ");
            strSql.Append(" where BlogID in (" + ids + ")  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = Status;
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


        public bool UpdateRec(int id, int Rec)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserBlog set ");
            strSql.Append("Recomend=@Recomend ");
            strSql.Append(" where BlogID =@BlogID");
            SqlParameter[] parameters = {
                                            	new SqlParameter("@Recomend", SqlDbType.Int,4),
					new SqlParameter("@BlogID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = Rec;
            parameters[1].Value = id;
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

        public bool UpdateRecList(string ids, int Rec)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserBlog set ");
            strSql.Append("Recomend=@Recomend ");
            strSql.Append(" where BlogID in (" + ids + ")  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Recomend", SqlDbType.Int,4)};
            parameters[0].Value = Rec;
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

	    public bool DeleteListEx(string BlogIDs)
	    {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            #region 级联删除
            //删除长微博数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserBlog ");
            strSql.Append(" where BlogID in (" + BlogIDs + ") ");
       
            CommandInfo cmd = new CommandInfo(strSql.ToString(),null);
            sqllist.Add(cmd);
            //删除动态数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from SNS_Posts where type=4 and TargetId in (" + BlogIDs + ") ");
            cmd = new CommandInfo(strSql2.ToString(), null);
            sqllist.Add(cmd);

            //删除评论数据
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("delete from SNS_Comments where type=4 and TargetId in (" + BlogIDs + ") ");
            cmd = new CommandInfo(strSql6.ToString(), null);
            sqllist.Add(cmd);

            #endregion

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
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

