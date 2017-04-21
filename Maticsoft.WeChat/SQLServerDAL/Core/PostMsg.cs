/**
* PostMsg.cs
*
* 功 能： N/A
* 类 名： PostMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:13   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;

namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:PostMsg
	/// </summary>
	public partial class PostMsg:IPostMsg
	{
		public PostMsg()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long PostMsgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_PostMsg");
            strSql.Append(" where PostMsgId=@PostMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@PostMsgId", SqlDbType.BigInt)
			};
            parameters[0].Value = PostMsgId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.WeChat.Model.Core.PostMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_PostMsg(");
            strSql.Append("RuleId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount)");
            strSql.Append(" values (");
            strSql.Append("@RuleId,@MsgType,@CreateTime,@Title,@Description,@MusicUrl,@HQMusicUrl,@ArticleCount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ArticleCount", SqlDbType.Int,4)};
            parameters[0].Value = model.RuleId;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.MusicUrl;
            parameters[6].Value = model.HQMusicUrl;
            parameters[7].Value = model.ArticleCount;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.PostMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_PostMsg set ");
            strSql.Append("RuleId=@RuleId,");
            strSql.Append("MsgType=@MsgType,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Title=@Title,");
            strSql.Append("Description=@Description,");
            strSql.Append("MusicUrl=@MusicUrl,");
            strSql.Append("HQMusicUrl=@HQMusicUrl,");
            strSql.Append("ArticleCount=@ArticleCount");
            strSql.Append(" where PostMsgId=@PostMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ArticleCount", SqlDbType.Int,4),
					new SqlParameter("@PostMsgId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.RuleId;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.MusicUrl;
            parameters[6].Value = model.HQMusicUrl;
            parameters[7].Value = model.ArticleCount;
            parameters[8].Value = model.PostMsgId;

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
        public bool Delete(long PostMsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_PostMsg ");
            strSql.Append(" where PostMsgId=@PostMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@PostMsgId", SqlDbType.BigInt)
			};
            parameters[0].Value = PostMsgId;

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
        public bool DeleteList(string PostMsgIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_PostMsg ");
            strSql.Append(" where PostMsgId in (" + PostMsgIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.PostMsg GetModel(long PostMsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PostMsgId,RuleId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount from WeChat_PostMsg ");
            strSql.Append(" where PostMsgId=@PostMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@PostMsgId", SqlDbType.BigInt)
			};
            parameters[0].Value = PostMsgId;

            Maticsoft.WeChat.Model.Core.PostMsg model = new Maticsoft.WeChat.Model.Core.PostMsg();
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
        public Maticsoft.WeChat.Model.Core.PostMsg DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.PostMsg model = new Maticsoft.WeChat.Model.Core.PostMsg();
            if (row != null)
            {
                if (row["PostMsgId"] != null && row["PostMsgId"].ToString() != "")
                {
                    model.PostMsgId = long.Parse(row["PostMsgId"].ToString());
                }
                if (row["RuleId"] != null && row["RuleId"].ToString() != "")
                {
                    model.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if (row["MsgType"] != null)
                {
                    model.MsgType = row["MsgType"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
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
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PostMsgId,RuleId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount ");
            strSql.Append(" FROM WeChat_PostMsg ");
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
            strSql.Append(" PostMsgId,RuleId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount ");
            strSql.Append(" FROM WeChat_PostMsg ");
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
            strSql.Append("select count(1) FROM WeChat_PostMsg ");
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
                strSql.Append("order by T.PostMsgId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_PostMsg T ");
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
            parameters[0].Value = "WeChat_PostMsg";
            parameters[1].Value = "PostMsgId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

	    public Maticsoft.WeChat.Model.Core.PostMsg GetRanMsg(int ruleId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1");
            strSql.Append(" *  from WeChat_PostMsg where RuleId=@RuleId order By NewID()  ");
            SqlParameter[] parameters = {
							new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
            parameters[0].Value = ruleId;
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

	    #endregion  ExtensionMethod
	}
}

