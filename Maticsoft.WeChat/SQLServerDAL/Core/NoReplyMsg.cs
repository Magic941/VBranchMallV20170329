/**  版本信息模板在安装目录下，可自行修改。
* NoReplyMsg.cs
*
* 功 能： N/A
* 类 名： NoReplyMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/23 17:18:18   N/A    初版
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
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:NoReplyMsg
	/// </summary>
	public partial class NoReplyMsg:INoReplyMsg
	{
		public NoReplyMsg()
		{}

		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long MsgId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WeChat_NoReplyMsg");
			strSql.Append(" where MsgId=@MsgId");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt)
			};
			parameters[0].Value = MsgId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Maticsoft.WeChat.Model.Core.NoReplyMsg model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WeChat_NoReplyMsg(");
			strSql.Append("OpenId,UserName,MsgType,CreateTime,Description,Location_X,Location_Y,Scale,PicUrl,Title,Url,Event,EventKey,Status)");
			strSql.Append(" values (");
			strSql.Append("@OpenId,@UserName,@MsgType,@CreateTime,@Description,@Location_X,@Location_Y,@Scale,@PicUrl,@Title,@Url,@Event,@EventKey,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@Location_X", SqlDbType.NVarChar,50),
					new SqlParameter("@Location_Y", SqlDbType.NVarChar,50),
					new SqlParameter("@Scale", SqlDbType.Int,4),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Event", SqlDbType.NVarChar,50),
					new SqlParameter("@EventKey", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.OpenId;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.MsgType;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.Description;
			parameters[5].Value = model.Location_X;
			parameters[6].Value = model.Location_Y;
			parameters[7].Value = model.Scale;
			parameters[8].Value = model.PicUrl;
			parameters[9].Value = model.Title;
			parameters[10].Value = model.Url;
			parameters[11].Value = model.Event;
			parameters[12].Value = model.EventKey;
			parameters[13].Value = model.Status;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.WeChat.Model.Core.NoReplyMsg model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WeChat_NoReplyMsg set ");
			strSql.Append("OpenId=@OpenId,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("MsgType=@MsgType,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("Description=@Description,");
			strSql.Append("Location_X=@Location_X,");
			strSql.Append("Location_Y=@Location_Y,");
			strSql.Append("Scale=@Scale,");
			strSql.Append("PicUrl=@PicUrl,");
			strSql.Append("Title=@Title,");
			strSql.Append("Url=@Url,");
			strSql.Append("Event=@Event,");
			strSql.Append("EventKey=@EventKey,");
			strSql.Append("Status=@Status");
			strSql.Append(" where MsgId=@MsgId");
			SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@Location_X", SqlDbType.NVarChar,50),
					new SqlParameter("@Location_Y", SqlDbType.NVarChar,50),
					new SqlParameter("@Scale", SqlDbType.Int,4),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Event", SqlDbType.NVarChar,50),
					new SqlParameter("@EventKey", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@MsgId", SqlDbType.BigInt,8)};
			parameters[0].Value = model.OpenId;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.MsgType;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.Description;
			parameters[5].Value = model.Location_X;
			parameters[6].Value = model.Location_Y;
			parameters[7].Value = model.Scale;
			parameters[8].Value = model.PicUrl;
			parameters[9].Value = model.Title;
			parameters[10].Value = model.Url;
			parameters[11].Value = model.Event;
			parameters[12].Value = model.EventKey;
			parameters[13].Value = model.Status;
			parameters[14].Value = model.MsgId;

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
		public bool Delete(long MsgId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WeChat_NoReplyMsg ");
			strSql.Append(" where MsgId=@MsgId");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt)
			};
			parameters[0].Value = MsgId;

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
		public bool DeleteList(string MsgIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WeChat_NoReplyMsg ");
			strSql.Append(" where MsgId in ("+MsgIdlist + ")  ");
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
		public Maticsoft.WeChat.Model.Core.NoReplyMsg GetModel(long MsgId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MsgId,OpenId,UserName,MsgType,CreateTime,Description,Location_X,Location_Y,Scale,PicUrl,Title,Url,Event,EventKey,Status from WeChat_NoReplyMsg ");
			strSql.Append(" where MsgId=@MsgId");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.BigInt)
			};
			parameters[0].Value = MsgId;

			Maticsoft.WeChat.Model.Core.NoReplyMsg model=new Maticsoft.WeChat.Model.Core.NoReplyMsg();
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
		public Maticsoft.WeChat.Model.Core.NoReplyMsg DataRowToModel(DataRow row)
		{
			Maticsoft.WeChat.Model.Core.NoReplyMsg model=new Maticsoft.WeChat.Model.Core.NoReplyMsg();
			if (row != null)
			{
				if(row["MsgId"]!=null && row["MsgId"].ToString()!="")
				{
					model.MsgId=long.Parse(row["MsgId"].ToString());
				}
				if(row["OpenId"]!=null)
				{
					model.OpenId=row["OpenId"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["MsgType"]!=null)
				{
					model.MsgType=row["MsgType"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["Location_X"]!=null)
				{
					model.Location_X=row["Location_X"].ToString();
				}
				if(row["Location_Y"]!=null)
				{
					model.Location_Y=row["Location_Y"].ToString();
				}
				if(row["Scale"]!=null && row["Scale"].ToString()!="")
				{
					model.Scale=int.Parse(row["Scale"].ToString());
				}
				if(row["PicUrl"]!=null)
				{
					model.PicUrl=row["PicUrl"].ToString();
				}
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["Url"]!=null)
				{
					model.Url=row["Url"].ToString();
				}
				if(row["Event"]!=null)
				{
					model.Event=row["Event"].ToString();
				}
				if(row["EventKey"]!=null)
				{
					model.EventKey=row["EventKey"].ToString();
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
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
			strSql.Append("select MsgId,OpenId,UserName,MsgType,CreateTime,Description,Location_X,Location_Y,Scale,PicUrl,Title,Url,Event,EventKey,Status ");
			strSql.Append(" FROM WeChat_NoReplyMsg ");
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
			strSql.Append(" MsgId,OpenId,UserName,MsgType,CreateTime,Description,Location_X,Location_Y,Scale,PicUrl,Title,Url,Event,EventKey,Status ");
			strSql.Append(" FROM WeChat_NoReplyMsg ");
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
			strSql.Append("select count(1) FROM WeChat_NoReplyMsg ");
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
				strSql.Append("order by T.MsgId desc");
			}
			strSql.Append(")AS Row, T.*  from WeChat_NoReplyMsg T ");
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
			parameters[0].Value = "WeChat_NoReplyMsg";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(int msgId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_NoReplyMsg set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where MsgId=@MsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@MsgId", SqlDbType.BigInt,8)};
            parameters[0].Value = status;
            parameters[1].Value = msgId;

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

        public long AddMsg(Maticsoft.WeChat.Model.Core.RequestMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_NoReplyMsg(");
            strSql.Append("OpenId,UserName,MsgType,CreateTime,Description,Location_X,Location_Y,Scale,PicUrl,Title,Url,Event,EventKey,Status)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@UserName,@MsgType,@CreateTime,@Description,@Location_X,@Location_Y,@Scale,@PicUrl,@Title,@Url,@Event,@EventKey,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@Location_X", SqlDbType.NVarChar,50),
					new SqlParameter("@Location_Y", SqlDbType.NVarChar,50),
					new SqlParameter("@Scale", SqlDbType.Int,4),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Event", SqlDbType.NVarChar,50),
					new SqlParameter("@EventKey", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.MsgType;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Location_X;
            parameters[6].Value = model.Location_Y;
            parameters[7].Value = model.Scale;
            parameters[8].Value = model.PicUrl;
            parameters[9].Value = model.Title;
            parameters[10].Value = model.Url;
            parameters[11].Value = model.Event;
            parameters[12].Value = model.EventKey;
            parameters[13].Value = 0;

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
		#endregion  ExtensionMethod
	}
}

