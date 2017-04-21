/**  版本信息模板在安装目录下，可自行修改。
* TaskLog.cs
*
* 功 能： N/A
* 类 名： TaskLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/7 17:57:54   N/A    初版
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
	/// 数据访问类:TaskLog
	/// </summary>
	public partial class TaskLog:ITaskLog
	{
		public TaskLog()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TaskId", "WeChat_TaskLog"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TaskId,string UserName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WeChat_TaskLog");
			strSql.Append(" where TaskId=@TaskId and UserName=@UserName ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)			};
			parameters[0].Value = TaskId;
			parameters[1].Value = UserName;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.WeChat.Model.Push.TaskLog model)
		{
            string TableName = "WeChat_TaskLog_" + DateTime.Today.ToString("yyyyMMdd");
            CreateTab(TableName);
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+TableName+" (");
			strSql.Append("TaskId,UserName,CreatedTime)");
			strSql.Append(" values (");
			strSql.Append("@TaskId,@UserName,@CreatedTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime)};
			parameters[0].Value = model.TaskId;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.CreatedTime;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Push.TaskLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WeChat_TaskLog set ");
			strSql.Append("CreatedTime=@CreatedTime");
			strSql.Append(" where TaskId=@TaskId and UserName=@UserName ");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.CreatedTime;
			parameters[1].Value = model.TaskId;
			parameters[2].Value = model.UserName;

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
		public bool Delete(int TaskId,string UserName,string tableName)
		{
			
              if(tableName=="")
			{
				tableName="WeChat_TaskLog_"+DateTime.Today.ToString("yyyyMMdd");
			}
              if (TabExists(tableName))
              {
                  StringBuilder strSql = new StringBuilder();
                  strSql.Append("delete from  " + tableName);
                  strSql.Append(" where TaskId=@TaskId and UserName=@UserName ");
                  SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)			};
                  parameters[0].Value = TaskId;
                  parameters[1].Value = UserName;

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
              return false;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Maticsoft.WeChat.Model.Push.TaskLog GetModel(int TaskId, string UserName, string tableName)
		{
              if(tableName=="")
			{
				tableName="WeChat_TaskLog_"+DateTime.Today.ToString("yyyyMMdd");
			}
              if (TabExists(tableName))
              {
                  StringBuilder strSql = new StringBuilder();
                  strSql.Append("select  top 1 TaskId,UserName,CreatedTime from  " + tableName);
                  strSql.Append(" where TaskId=@TaskId and UserName=@UserName ");
                  SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)			};
                  parameters[0].Value = TaskId;
                  parameters[1].Value = UserName;

                  Maticsoft.WeChat.Model.Push.TaskLog model = new Maticsoft.WeChat.Model.Push.TaskLog();
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
              return null;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Push.TaskLog DataRowToModel(DataRow row)
		{
			Maticsoft.WeChat.Model.Push.TaskLog model=new Maticsoft.WeChat.Model.Push.TaskLog();
			if (row != null)
			{
				if(row["TaskId"]!=null && row["TaskId"].ToString()!="")
				{
					model.TaskId=int.Parse(row["TaskId"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["CreatedTime"]!=null && row["CreatedTime"].ToString()!="")
				{
					model.CreatedTime=DateTime.Parse(row["CreatedTime"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere,string tableName)
		{
              if(tableName=="")
			{
				tableName="WeChat_TaskLog_"+DateTime.Today.ToString("yyyyMMdd");
			}
              if (TabExists(tableName))
              {
                  StringBuilder strSql = new StringBuilder();
                  strSql.Append("select TaskId,UserName,CreatedTime ");
                  strSql.Append(" FROM  " + tableName);
                  if (strWhere.Trim() != "")
                  {
                      strSql.Append(" where " + strWhere);
                  }
                  return DbHelperSQL.Query(strSql.ToString());
              }
              return null;
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder,string tableName)
		{
			StringBuilder strSql=new StringBuilder();
            if(tableName=="")
			{
				tableName="WeChat_TaskLog_"+DateTime.Today.ToString("yyyyMMdd");
			}
            if (TabExists(tableName))
            {
                strSql.Append("select ");
                if (Top > 0)
                {
                    strSql.Append(" top " + Top.ToString());
                }
                strSql.Append(" TaskId,UserName,CreatedTime ");
                strSql.Append(" FROM  " + tableName);
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder);
                return DbHelperSQL.Query(strSql.ToString());
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere,string tableName)
		{
              if(tableName=="")
			{
				tableName="WeChat_TaskLog_"+DateTime.Today.ToString("yyyyMMdd");
			}
              if (TabExists(tableName))
              {
                  StringBuilder strSql = new StringBuilder();
                  strSql.Append("select count(1) FROM  " + tableName);
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
              return 0;
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,string tableName)
		{
              if(tableName=="")
			{
				tableName="WeChat_TaskLog_"+DateTime.Today.ToString("yyyyMMdd");
			}
              if (TabExists(tableName))
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
                      strSql.Append("order by T.UserName desc");
                  }
                  strSql.Append(")AS Row, T.*  from " + tableName + " T ");
                  if (!string.IsNullOrEmpty(strWhere.Trim()))
                  {
                      strSql.Append(" WHERE " + strWhere);
                  }
                  strSql.Append(" ) TT");
                  strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                  return DbHelperSQL.Query(strSql.ToString());
              }
              return null;
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
			parameters[0].Value = "WeChat_TaskLog";
			parameters[1].Value = "UserName";
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
        /// 表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public bool TabExists(string TableName)
        {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            object obj = DbHelperSQL.GetSingle(strsql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="TableName"></param>
        public void CreateTab(string TableName)
        {
            if (!TabExists(TableName))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("CREATE TABLE [" + TableName + "] (");
                strSql.Append(" [TaskId] [int] NOT NULL,");
                strSql.Append(" [UserName] [nvarchar](200) NOT NULL, ");
                strSql.Append(" [CreatedTime] [datetime] NOT NULL, ");
                strSql.Append(" CONSTRAINT [PK_" + TableName + "] PRIMARY KEY CLUSTERED  ( [TaskId] ASC, [UserName] ASC ");
                strSql.Append(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ");
                 strSql.Append(" ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)  ON [PRIMARY] ) ON [PRIMARY]");
                DbHelperSQL.ExecuteSql(strSql.ToString());
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(int taskId,string userName)
        {
            string TableName = "WeChat_TaskLog_" + DateTime.Today.ToString("yyyyMMdd");
            CreateTab(TableName);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TableName + " (");
            strSql.Append("TaskId,UserName,CreatedTime)");
            strSql.Append(" values (");
            strSql.Append("@TaskId,@UserName,@CreatedTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime)};
            parameters[0].Value = taskId;
            parameters[1].Value = userName;
            parameters[2].Value = DateTime.Now;

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

		#endregion  ExtensionMethod
	}
}

