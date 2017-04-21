/**
* GroupUsers.cs
*
* 功 能： N/A
* 类 名： GroupUsers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:44   N/A    初版
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
	/// 数据访问类:GroupUsers
	/// </summary>
	public partial class GroupUsers:IGroupUsers
	{
		public GroupUsers()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int GroupID,int UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_GroupUsers");
			strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = GroupID;
			parameters[1].Value = UserID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SNS.GroupUsers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_GroupUsers(");
			strSql.Append("GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status)");
			strSql.Append(" values (");
			strSql.Append("@GroupID,@UserID,@NickName,@JoinTime,@Role,@ApplyReason,@IsRecommend,@Sequence,@Status)");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),
					new SqlParameter("@Role", SqlDbType.Int,4),
					new SqlParameter("@ApplyReason", SqlDbType.NVarChar),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.GroupID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.NickName;
			parameters[3].Value = model.JoinTime;
			parameters[4].Value = model.Role;
			parameters[5].Value = model.ApplyReason;
			parameters[6].Value = model.IsRecommend;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.Status;

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
		public bool Update(Maticsoft.Model.SNS.GroupUsers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_GroupUsers set ");
			strSql.Append("NickName=@NickName,");
			strSql.Append("JoinTime=@JoinTime,");
			strSql.Append("Role=@Role,");
			strSql.Append("ApplyReason=@ApplyReason,");
			strSql.Append("IsRecommend=@IsRecommend,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("Status=@Status");
			strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),
					new SqlParameter("@Role", SqlDbType.Int,4),
					new SqlParameter("@ApplyReason", SqlDbType.NVarChar),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = model.NickName;
			parameters[1].Value = model.JoinTime;
			parameters[2].Value = model.Role;
			parameters[3].Value = model.ApplyReason;
			parameters[4].Value = model.IsRecommend;
			parameters[5].Value = model.Sequence;
			parameters[6].Value = model.Status;
			parameters[7].Value = model.GroupID;
			parameters[8].Value = model.UserID;

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
		public bool Delete(int GroupID,int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_GroupUsers ");
			strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = GroupID;
			parameters[1].Value = UserID;

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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.GroupUsers GetModel(int GroupID,int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status from SNS_GroupUsers ");
			strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = GroupID;
			parameters[1].Value = UserID;

			Maticsoft.Model.SNS.GroupUsers model=new Maticsoft.Model.SNS.GroupUsers();
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
		public Maticsoft.Model.SNS.GroupUsers DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.GroupUsers model=new Maticsoft.Model.SNS.GroupUsers();
			if (row != null)
			{
				if(row["GroupID"]!=null && row["GroupID"].ToString()!="")
				{
					model.GroupID=int.Parse(row["GroupID"].ToString());
				}
				if(row["UserID"]!=null && row["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(row["UserID"].ToString());
				}
				if(row["NickName"]!=null)
				{
					model.NickName=row["NickName"].ToString();
				}
				if(row["JoinTime"]!=null && row["JoinTime"].ToString()!="")
				{
					model.JoinTime=DateTime.Parse(row["JoinTime"].ToString());
				}
				if(row["Role"]!=null && row["Role"].ToString()!="")
				{
					model.Role=int.Parse(row["Role"].ToString());
				}
				if(row["ApplyReason"]!=null)
				{
					model.ApplyReason=row["ApplyReason"].ToString();
				}
				if(row["IsRecommend"]!=null && row["IsRecommend"].ToString()!="")
				{
					if((row["IsRecommend"].ToString()=="1")||(row["IsRecommend"].ToString().ToLower()=="true"))
					{
						model.IsRecommend=true;
					}
					else
					{
						model.IsRecommend=false;
					}
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
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
			strSql.Append("select GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status ");
			strSql.Append(" FROM SNS_GroupUsers ");
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
			strSql.Append(" GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status ");
			strSql.Append(" FROM SNS_GroupUsers ");
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
			strSql.Append("select count(1) FROM SNS_GroupUsers ");
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
				strSql.Append("order by T.UserID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_GroupUsers T ");
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
			parameters[0].Value = "SNS_GroupUsers";
			parameters[1].Value = "UserID";
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
        /// 更新用户状态
        /// </summary>
        public bool UpdateStatus(int GroupID, int UserID, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupUsers set ");                                  
            strSql.Append(" Status=@Status");
            strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] parameters = {					
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)};            
            parameters[0].Value = Status;
            parameters[1].Value = GroupID;
            parameters[2].Value = UserID;
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

        public bool AddEx(Maticsoft.Model.SNS.GroupUsers model)
        {
               #region 加入小组表
		  StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_GroupUsers(");
            strSql.Append("GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status)");
            strSql.Append(" values (");
            strSql.Append("@GroupID,@UserID,@NickName,@JoinTime,@Role,@ApplyReason,@IsRecommend,@Sequence,@Status)");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),
					new SqlParameter("@Role", SqlDbType.Int,4),
					new SqlParameter("@ApplyReason", SqlDbType.NVarChar),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.GroupID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.NickName;
            parameters[3].Value = model.JoinTime;
            parameters[4].Value = model.Role;
            parameters[5].Value = model.ApplyReason;
            parameters[6].Value = model.IsRecommend;
            parameters[7].Value = model.Sequence;
            parameters[8].Value = model.Status;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);


             
	#endregion

           
              #region 对应小组的人员表中的数量增加一
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("Update  SNS_Groups ");
            strSql1.Append(" Set GroupUserCount=GroupUserCount+1 ");
            strSql1.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters1 = {
				new SqlParameter("@GroupID", SqlDbType.Int,4)
                   
		        };
            parameters1[0].Value = model.GroupID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);
            #endregion
          
         
          return  DbHelperSQL.ExecuteSqlTran(sqllist)>0?true:false;
        }

        /// <summary>
        /// 更新推荐状态
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="UserID"></param>
        /// <param name="Recommand"></param>
        /// <returns></returns>
        public bool UpdateRecommand(int GroupID, int UserID,int Recommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupUsers set ");
            strSql.Append("IsRecommend=@IsRecommend");
            strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = Recommand==1?true:false;
            parameters[1].Value =GroupID;
            parameters[2].Value = UserID;
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

        public bool DeleteEx(int GroupID, int UserID)
        {

            #region 退出小组
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_GroupUsers ");
            strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
            parameters[0].Value = GroupID;
            parameters[1].Value = UserID;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            #endregion
            #region 对应小组的人员表中的数量增减一
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("Update  SNS_Groups ");
            strSql1.Append(" Set GroupUserCount=GroupUserCount-1 ");
            strSql1.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters1 = {
				new SqlParameter("@GroupID", SqlDbType.Int,4)
                   
		        };
            parameters1[0].Value =GroupID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);
            #endregion
            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
        
        
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool DeleteEx(int GroupID, string UserIDs)
        {

            #region 退出小组
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_GroupUsers ");
            strSql.Append(" where GroupID="+GroupID+" and UserID in ("+UserIDs+") ");
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(),null);
            sqllist.Add(cmd);
            #endregion
            #region 对应小组的人员表中的数量增减一
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("Update  SNS_Groups ");
            strSql1.Append(" Set GroupUserCount=GroupUserCount-1 ");
            strSql1.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters1 = {
				new SqlParameter("@GroupID", SqlDbType.Int,4)
                   
		        };
            parameters1[0].Value = GroupID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);
            #endregion
            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;


        }

        public bool UpdateRole(int GroupID, int UserID, int Role)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_GroupUsers set ");
            strSql.Append("Role=@Role");
            strSql.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Role", SqlDbType.Bit,1),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = Role;
            parameters[1].Value = GroupID;
            parameters[2].Value = UserID;
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
        /// 根据帖子对用户禁言
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateStatusByTopicIds(string Ids, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE SNS_GroupUsers SET  Status ="+Status+" FROM  SNS_GroupUsers");
            strSql.Append(" INNER JOIN SNS_GroupTopics ON SNS_GroupUsers.GroupID = SNS_GroupTopics.GroupID AND SNS_GroupTopics.CreatedUserID=SNS_GroupUsers.UserID WHERE (SNS_GroupTopics.TopicID IN("+Ids+"))");
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
        /// 根据帖子回复对用户禁言
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateStatusByTopicReplyIds(string Ids, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE SNS_GroupUsers SET  Status =" + Status + " FROM  SNS_GroupUsers");
            strSql.Append(" INNER JOIN SNS_GroupTopicReply ON SNS_GroupUsers.GroupID = SNS_GroupTopicReply.GroupID AND SNS_GroupTopicReply.ReplyUserID=SNS_GroupUsers.UserID WHERE (SNS_GroupTopicReply.ReplyID IN(" + Ids + "))");
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

