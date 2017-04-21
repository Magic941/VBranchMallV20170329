/**  版本信息模板在安装目录下，可自行修改。
* TaskMsg.cs
*
* 功 能： N/A
* 类 名： TaskMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/7 17:58:09   N/A    初版
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
using Maticsoft.WeChat.IDAL.Push;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Push
{
	/// <summary>
	/// 数据访问类:TaskMsg
	/// </summary>
	public partial class TaskMsg:ITaskMsg
	{
		public TaskMsg()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TaskId", "WeChat_TaskMsg");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TaskId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_TaskMsg");
            strSql.Append(" where TaskId=@TaskId");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4)
			};
            parameters[0].Value = TaskId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Push.TaskMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_TaskMsg(");
            strSql.Append("OpenId,GroupId,UserName,MsgType,CreatedDate,Title,Description,MediaId,VoiceUrl,MusicUrl,HQMusicUrl,ArticleCount,PublishDate)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@GroupId,@UserName,@MsgType,@CreatedDate,@Title,@Description,@MediaId,@VoiceUrl,@MusicUrl,@HQMusicUrl,@ArticleCount,@PublishDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@GroupId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MediaId", SqlDbType.NVarChar,300),
					new SqlParameter("@VoiceUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ArticleCount", SqlDbType.Int,4),
					new SqlParameter("@PublishDate", SqlDbType.DateTime)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.GroupId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.MsgType;
            parameters[4].Value = model.CreatedDate;
            parameters[5].Value = model.Title;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.MediaId;
            parameters[8].Value = model.VoiceUrl;
            parameters[9].Value = model.MusicUrl;
            parameters[10].Value = model.HQMusicUrl;
            parameters[11].Value = model.ArticleCount;
            parameters[12].Value = model.PublishDate;

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
        public bool Update(Maticsoft.WeChat.Model.Push.TaskMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_TaskMsg set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("GroupId=@GroupId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("MsgType=@MsgType,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Title=@Title,");
            strSql.Append("Description=@Description,");
            strSql.Append("MediaId=@MediaId,");
            strSql.Append("VoiceUrl=@VoiceUrl,");
            strSql.Append("MusicUrl=@MusicUrl,");
            strSql.Append("HQMusicUrl=@HQMusicUrl,");
            strSql.Append("ArticleCount=@ArticleCount,");
            strSql.Append("PublishDate=@PublishDate");
            strSql.Append(" where TaskId=@TaskId");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@GroupId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MediaId", SqlDbType.NVarChar,300),
					new SqlParameter("@VoiceUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ArticleCount", SqlDbType.Int,4),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@TaskId", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.GroupId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.MsgType;
            parameters[4].Value = model.CreatedDate;
            parameters[5].Value = model.Title;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.MediaId;
            parameters[8].Value = model.VoiceUrl;
            parameters[9].Value = model.MusicUrl;
            parameters[10].Value = model.HQMusicUrl;
            parameters[11].Value = model.ArticleCount;
            parameters[12].Value = model.PublishDate;
            parameters[13].Value = model.TaskId;

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
        public bool Delete(int TaskId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_TaskMsg ");
            strSql.Append(" where TaskId=@TaskId");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4)
			};
            parameters[0].Value = TaskId;

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
        public bool DeleteList(string TaskIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_TaskMsg ");
            strSql.Append(" where TaskId in (" + TaskIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Push.TaskMsg GetModel(int TaskId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TaskId,OpenId,GroupId,UserName,MsgType,CreatedDate,Title,Description,MediaId,VoiceUrl,MusicUrl,HQMusicUrl,ArticleCount,PublishDate from WeChat_TaskMsg ");
            strSql.Append(" where TaskId=@TaskId");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4)
			};
            parameters[0].Value = TaskId;

            Maticsoft.WeChat.Model.Push.TaskMsg model = new Maticsoft.WeChat.Model.Push.TaskMsg();
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
        public Maticsoft.WeChat.Model.Push.TaskMsg DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Push.TaskMsg model = new Maticsoft.WeChat.Model.Push.TaskMsg();
            if (row != null)
            {
                if (row["TaskId"] != null && row["TaskId"].ToString() != "")
                {
                    model.TaskId = int.Parse(row["TaskId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["GroupId"] != null && row["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(row["GroupId"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["MsgType"] != null)
                {
                    model.MsgType = row["MsgType"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["MediaId"] != null)
                {
                    model.MediaId = row["MediaId"].ToString();
                }
                if (row["VoiceUrl"] != null)
                {
                    model.VoiceUrl = row["VoiceUrl"].ToString();
                }
                if (row["MusicUrl"] != null)
                {
                    model.MusicUrl = row["MusicUrl"].ToString();
                }
                if (row["HQMusicUrl"] != null)
                {
                    model.HQMusicUrl = row["HQMusicUrl"].ToString();
                }
                if (row["ArticleCount"] != null && row["ArticleCount"].ToString() != "")
                {
                    model.ArticleCount = int.Parse(row["ArticleCount"].ToString());
                }
                if (row["PublishDate"] != null && row["PublishDate"].ToString() != "")
                {
                    model.PublishDate = DateTime.Parse(row["PublishDate"].ToString());
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
            strSql.Append("select TaskId,OpenId,GroupId,UserName,MsgType,CreatedDate,Title,Description,MediaId,VoiceUrl,MusicUrl,HQMusicUrl,ArticleCount,PublishDate ");
            strSql.Append(" FROM WeChat_TaskMsg ");
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
            strSql.Append(" TaskId,OpenId,GroupId,UserName,MsgType,CreatedDate,Title,Description,MediaId,VoiceUrl,MusicUrl,HQMusicUrl,ArticleCount,PublishDate ");
            strSql.Append(" FROM WeChat_TaskMsg ");
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
            strSql.Append("select count(1) FROM WeChat_TaskMsg ");
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
                strSql.Append("order by T.TaskId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_TaskMsg T ");
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
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "WeChat_TaskMsg";
            parameters[1].Value = "TaskId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

         public DataSet GetMsgList(string openid, string datetime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM WeChat_TaskMsg ");
            strSql.AppendFormat("  where OpenId='{0}' and PublishDate BETWEEN '{1}' AND GETDATE()", openid, datetime);
            return DbHelperSQL.Query(strSql.ToString());    
        }
		#endregion  ExtensionMethod
	}
}

