/**  版本信息模板在安装目录下，可自行修改。
* CustomerMsg.cs
*
* 功 能： N/A
* 类 名： CustomerMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/21 20:52:18   N/A    初版
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
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:CustomerMsg
	/// </summary>
	public partial class CustomerMsg:ICustomerMsg
	{
		public CustomerMsg()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long MsgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_CustomerMsg");
            strSql.Append(" where MsgId=@MsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt)
			};
            parameters[0].Value = MsgId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.WeChat.Model.Core.CustomerMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_CustomerMsg(");
            strSql.Append("OpenId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@MsgType,@CreateTime,@Title,@Description,@MusicUrl,@HQMusicUrl,@ArticleCount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ArticleCount", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
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
        public bool Update(Maticsoft.WeChat.Model.Core.CustomerMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_CustomerMsg set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("MsgType=@MsgType,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Title=@Title,");
            strSql.Append("Description=@Description,");
            strSql.Append("MusicUrl=@MusicUrl,");
            strSql.Append("HQMusicUrl=@HQMusicUrl,");
            strSql.Append("ArticleCount=@ArticleCount");
            strSql.Append(" where MsgId=@MsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ArticleCount", SqlDbType.Int,4),
					new SqlParameter("@MsgId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.MusicUrl;
            parameters[6].Value = model.HQMusicUrl;
            parameters[7].Value = model.ArticleCount;
            parameters[8].Value = model.MsgId;

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
        public bool Delete(long MsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_CustomerMsg ");
            strSql.Append(" where MsgId=@MsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt)
			};
            parameters[0].Value = MsgId;

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
        public bool DeleteList(string MsgIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_CustomerMsg ");
            strSql.Append(" where MsgId in (" + MsgIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.CustomerMsg GetModel(long MsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MsgId,OpenId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount from WeChat_CustomerMsg ");
            strSql.Append(" where MsgId=@MsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt)
			};
            parameters[0].Value = MsgId;

            Maticsoft.WeChat.Model.Core.CustomerMsg model = new Maticsoft.WeChat.Model.Core.CustomerMsg();
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
        public Maticsoft.WeChat.Model.Core.CustomerMsg DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.CustomerMsg model = new Maticsoft.WeChat.Model.Core.CustomerMsg();
            if (row != null)
            {
                if (row["MsgId"] != null && row["MsgId"].ToString() != "")
                {
                    model.MsgId = long.Parse(row["MsgId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
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
            strSql.Append("select MsgId,OpenId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount ");
            strSql.Append(" FROM WeChat_CustomerMsg ");
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
            strSql.Append(" MsgId,OpenId,MsgType,CreateTime,Title,Description,MusicUrl,HQMusicUrl,ArticleCount ");
            strSql.Append(" FROM WeChat_CustomerMsg ");
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
            strSql.Append("select count(1) FROM WeChat_CustomerMsg ");
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
                strSql.Append("order by T.MsgId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_CustomerMsg T ");
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
            parameters[0].Value = "WeChat_CustomerMsg";
            parameters[1].Value = "MsgId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

        public bool DeleteListEx(string ids)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //删除项表
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete  WeChat_CustUserMsg  ");
            strSql1.Append(" where MsgId in (" + ids + ")  ");
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(),null);
            sqllist.Add(cmd1);

            //删除中间表
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete WeChat_CustomerMsg ");
            strSql2.Append(" where MsgId in (" + ids + ")  ");
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), null);
            sqllist.Add(cmd2);
            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
        }
		#endregion  ExtensionMethod
	}
}

