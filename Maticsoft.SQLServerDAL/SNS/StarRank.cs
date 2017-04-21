/**
* StarRank.cs
*
* 功 能： N/A
* 类 名： StarRank
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:56   N/A    初版
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
	/// 数据访问类:StarRank
	/// </summary>       
	public partial class StarRank:IStarRank
	{
		public StarRank()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "SNS_StarRank");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_StarRank");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.StarRank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_StarRank(");
            strSql.Append("UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@UserGravatar,@TypeId,@NickName,@IsRecommend,@Sequence,@TimeUnit,@StartDate,@EndDate,@CreatedDate,@RankDate,@Status,@RankType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserGravatar", SqlDbType.NVarChar,200),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@TimeUnit", SqlDbType.Int,4),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@RankDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@RankType", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserGravatar;
            parameters[2].Value = model.TypeId;
            parameters[3].Value = model.NickName;
            parameters[4].Value = model.IsRecommend;
            parameters[5].Value = model.Sequence;
            parameters[6].Value = model.TimeUnit;
            parameters[7].Value = model.StartDate;
            parameters[8].Value = model.EndDate;
            parameters[9].Value = model.CreatedDate;
            parameters[10].Value = model.RankDate;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.RankType;

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
        public bool Update(Maticsoft.Model.SNS.StarRank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_StarRank set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserGravatar=@UserGravatar,");
            strSql.Append("TypeId=@TypeId,");
            strSql.Append("NickName=@NickName,");
            strSql.Append("IsRecommend=@IsRecommend,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("TimeUnit=@TimeUnit,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("RankDate=@RankDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("RankType=@RankType");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserGravatar", SqlDbType.NVarChar,200),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@TimeUnit", SqlDbType.Int,4),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@RankDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@RankType", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserGravatar;
            parameters[2].Value = model.TypeId;
            parameters[3].Value = model.NickName;
            parameters[4].Value = model.IsRecommend;
            parameters[5].Value = model.Sequence;
            parameters[6].Value = model.TimeUnit;
            parameters[7].Value = model.StartDate;
            parameters[8].Value = model.EndDate;
            parameters[9].Value = model.CreatedDate;
            parameters[10].Value = model.RankDate;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.RankType;
            parameters[13].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_StarRank ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_StarRank ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public Maticsoft.Model.SNS.StarRank GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType from SNS_StarRank ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.SNS.StarRank model = new Maticsoft.Model.SNS.StarRank();
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
        public Maticsoft.Model.SNS.StarRank DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.StarRank model = new Maticsoft.Model.SNS.StarRank();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserGravatar"] != null)
                {
                    model.UserGravatar = row["UserGravatar"].ToString();
                }
                if (row["TypeId"] != null && row["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if (row["NickName"] != null)
                {
                    model.NickName = row["NickName"].ToString();
                }
                if (row["IsRecommend"] != null && row["IsRecommend"].ToString() != "")
                {
                    if ((row["IsRecommend"].ToString() == "1") || (row["IsRecommend"].ToString().ToLower() == "true"))
                    {
                        model.IsRecommend = true;
                    }
                    else
                    {
                        model.IsRecommend = false;
                    }
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["TimeUnit"] != null && row["TimeUnit"].ToString() != "")
                {
                    model.TimeUnit = int.Parse(row["TimeUnit"].ToString());
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["RankDate"] != null && row["RankDate"].ToString() != "")
                {
                    model.RankDate = DateTime.Parse(row["RankDate"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["RankType"] != null && row["RankType"].ToString() != "")
                {
                    model.RankType = int.Parse(row["RankType"].ToString());
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
            strSql.Append("select ID,UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType ");
            strSql.Append(" FROM SNS_StarRank ");
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
            strSql.Append(" ID,UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType ");
            strSql.Append(" FROM SNS_StarRank ");
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
            strSql.Append("select count(1) FROM SNS_StarRank ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_StarRank T ");
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
            parameters[0].Value = "SNS_StarRank";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public bool UpdateStateList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_StarRank set ");
            strSql.Append("Status=@Status,");
            strSql.Append(" where ID in (" + IDlist + ")");
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
        /// 添加晒货达人排行
        /// </summary>
        /// <returns></returns>
        public bool AddShareProductRank()
        {
          //首先要删除数据
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SNS_StarRank ");
            strSql.Append(" where typeId=2  ");
            SqlParameter[] parameters = {
					};
         
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            //然后添加
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into SNS_StarRank(UserId,UserGravatar,TypeId,NickName,Status,RankType,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate) ");
            strSql2.Append(" select top 10 UserID,UserGravatar,TypeID,NickName,Status,'0','false',ROW_NUMBER() OVER( ORDER BY ID),'-1',GETDATE(),GETDATE(),GETDATE(),GETDATE() from   ");
            strSql2.Append(" (select top 10 *,PhotoCount=(select COUNT(1) from SNS_Photos where CreatedUserID=star.UserID and Type=0) from  SNS_Star star where Status=1 and TypeID=2 order by PhotoCount desc) temp  ");
            SqlParameter[] parameters2 = {
					};
         
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

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

        /// <summary>
        /// 添加明星达人排行
        /// </summary>
        /// <returns></returns>
        public bool AddHotStarRank()
        {
            //首先要删除数据
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SNS_StarRank ");
            strSql.Append(" where typeId=1  ");
            SqlParameter[] parameters = {
					};

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            //然后添加
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into SNS_StarRank(UserId,UserGravatar,TypeId,NickName,Status,RankType,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate) ");
            strSql2.Append(" select top 10 UserID,UserGravatar,TypeID,NickName,Status,'0','false',ROW_NUMBER() OVER( ORDER BY ID),'-1',GETDATE(),GETDATE(),GETDATE(),GETDATE() from  ");
            strSql2.Append(" (select top 10 *,FansCount=(select FansCount from Accounts_UsersExp where UserID=star.UserID) from  SNS_Star star where Status=1 and TypeID=1 order by FansCount desc) temp  ");
            SqlParameter[] parameters2 = {
					};

            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

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

        /// <summary>
        /// 添加搭配达人排行
        /// </summary>
        /// <returns></returns>
        public bool AddCollocationRank()
        {
            //首先要删除数据
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SNS_StarRank ");
            strSql.Append(" where typeId=3  ");//搭配达人类型
            SqlParameter[] parameters = {
					};

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            //然后添加
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into SNS_StarRank(UserId,UserGravatar,TypeId,NickName,Status,RankType,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate) ");
            strSql2.Append(" select top 10 UserID,UserGravatar,TypeID,NickName,Status,'0','false',ROW_NUMBER() OVER( ORDER BY ID),'-1',GETDATE(),GETDATE(),GETDATE(),GETDATE() from   ");
            strSql2.Append(" (select top 10 *,PhotoCount=(select COUNT(1) from SNS_Photos where CreatedUserID=star.UserID and Type=1) from  SNS_Star star where Status=1 and TypeID=3 order by PhotoCount desc) temp  ");
            SqlParameter[] parameters2 = {
					};

            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

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
