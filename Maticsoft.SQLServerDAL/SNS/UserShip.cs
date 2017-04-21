/**
* UserShip.cs
*
* 功 能： N/A
* 类 名： UserShip
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:05   N/A    初版
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
    /// 数据访问类:UserShip
    /// </summary>
    public partial class UserShip : IUserShip
    {
        public UserShip()
        { }

        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ActiveUserID, int PassiveUserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_UserShip");
            strSql.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4)			};
            parameters[0].Value = ActiveUserID;
            parameters[1].Value = PassiveUserID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.SNS.UserShip model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserShip(");
            strSql.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
            strSql.Append(" values (");
            strSql.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
            SqlParameter[] parameters = {
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Bit,1)};
            parameters[0].Value = model.ActiveUserID;
            parameters[1].Value = model.PassiveUserID;
            parameters[2].Value = model.State;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.CategoryID;
            parameters[5].Value = model.CreatedDate;
            parameters[6].Value = model.IsRead;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.UserShip model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserShip set ");
            strSql.Append("State=@State,");
            strSql.Append("Type=@Type,");
            strSql.Append("CategoryID=@CategoryID,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("IsRead=@IsRead");
            strSql.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4)};
            parameters[0].Value = model.State;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.CategoryID;
            parameters[3].Value = model.CreatedDate;
            parameters[4].Value = model.IsRead;
            parameters[5].Value = model.ActiveUserID;
            parameters[6].Value = model.PassiveUserID;

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
        public bool Delete(int ActiveUserID, int PassiveUserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserShip ");
            strSql.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4)			};
            parameters[0].Value = ActiveUserID;
            parameters[1].Value = PassiveUserID;

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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.UserShip GetModel(int ActiveUserID, int PassiveUserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead from SNS_UserShip ");
            strSql.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4)			};
            parameters[0].Value = ActiveUserID;
            parameters[1].Value = PassiveUserID;

            Maticsoft.Model.SNS.UserShip model = new Maticsoft.Model.SNS.UserShip();
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
        public Maticsoft.Model.SNS.UserShip DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserShip model = new Maticsoft.Model.SNS.UserShip();
            if (row != null)
            {
                if (row["ActiveUserID"] != null && row["ActiveUserID"].ToString() != "")
                {
                    model.ActiveUserID = int.Parse(row["ActiveUserID"].ToString());
                }
                if (row["PassiveUserID"] != null && row["PassiveUserID"].ToString() != "")
                {
                    model.PassiveUserID = int.Parse(row["PassiveUserID"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["CategoryID"] != null && row["CategoryID"].ToString() != "")
                {
                    model.CategoryID = int.Parse(row["CategoryID"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
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
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead ");
            strSql.Append(" FROM SNS_UserShip ");
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
            strSql.Append(" ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead ");
            strSql.Append(" FROM SNS_UserShip ");
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
            strSql.Append("select count(1) FROM SNS_UserShip ");
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
                strSql.Append("order by T.PassiveUserID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_UserShip T ");
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
            parameters[0].Value = "SNS_UserShip";
            parameters[1].Value = "PassiveUserID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.UserShip> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserShip> modelList = new List<Maticsoft.Model.SNS.UserShip>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.UserShip model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        #endregion  BasicMethod

        #region  MethodEx
        /// <summary>
        /// 关注某人，事务处理
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="FellowUserId"></param>
        /// <returns></returns>
        public bool FellowUser(Maticsoft.Model.SNS.UserShip model)
        {
            #region 被关注者的粉丝增加1
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update Accounts_UsersExp set ");
            strSql1.Append("FansCount=FansCount+1,");
            strSql1.Append(" where UserID=@UserID");
            SqlParameter[] parameters1 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters1[0].Value = model.PassiveUserID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1); 
            #endregion

            #region 自己的关注数量增加1
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update Accounts_UsersExp set ");
            strSql2.Append("FellowCount=FellowCount+1,");
            strSql2.Append(" where UserID=@UserID");
            SqlParameter[] parameters2 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters2[0].Value = model.ActiveUserID;
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters1);
            sqllist.Add(cmd2); 
            #endregion

            //如果是对方也关注的情况下增加一条数据同时，给对方的类型为互相关组
            if (Exists(model.PassiveUserID, model.ActiveUserID))
            {
                model.Type = (int)Maticsoft.Model.SNS.EnumHelper.FansType.EachOher;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into SNS_UserShip(");
                strSql.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
                strSql.Append(" values (");
                strSql.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
                SqlParameter[] parameters = {
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Bit,1)};
                parameters[0].Value = model.ActiveUserID;
                parameters[1].Value = model.PassiveUserID;
                parameters[2].Value = 1;
                parameters[3].Value = model.Type;
                parameters[4].Value = model.CategoryID;
                parameters[5].Value = model.CreatedDate;
                parameters[6].Value = model.IsRead;
                CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
                sqllist.Add(cmd);
                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("update SNS_UserShip set ");
                strSql3.Append("Type=@Type,");
                strSql3.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                SqlParameter[] parameters3 = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4)};
                parameters3[0].Value = model.Type;
                parameters3[1].Value = model.PassiveUserID;
                parameters3[2].Value = model.ActiveUserID;
                CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
                sqllist.Add(cmd3);
                return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
            }
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("insert into SNS_UserShip(");
            strSql4.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
            strSql4.Append(" values (");
            strSql4.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
            SqlParameter[] parameters4 = {
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Bit,1)};
            parameters4[0].Value = model.ActiveUserID;
            parameters4[1].Value = model.PassiveUserID;
            parameters4[2].Value = model.State;
            parameters4[3].Value = model.Type;
            parameters4[4].Value = model.CategoryID;
            parameters4[5].Value = model.CreatedDate;
            parameters4[6].Value = model.IsRead;
            CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd4);
            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;

        }
        /// <summary>
        /// 取消关注某人的情况
        /// </summary>
        /// <param name="UserID">取消者</param>
        /// <param name="UnfellowUserID">被取消者</param>
        /// <returns></returns>
        public bool UnFellowUser(int UserID, int UnfellowUserID)
        {
            //如果是对方也关注了自己，则在取消的情况下，取消对方互相关注的状态
            if (Exists(UnfellowUserID, UserID))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from SNS_UserShip ");
                strSql.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4)			};
                parameters[0].Value = UserID;
                parameters[1].Value = UnfellowUserID;
                List<CommandInfo> sqllist = new List<CommandInfo>();
                CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
                sqllist.Add(cmd);
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("update SNS_UserShip set ");
                strSql1.Append("Type=@Type,");
                strSql1.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                SqlParameter[] parameters1 = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
					new SqlParameter("@PassiveUserID", SqlDbType.Int,4)};
                parameters1[0].Value = (int)Maticsoft.Model.SNS.EnumHelper.FansType.None;
                parameters1[1].Value = UnfellowUserID;
                parameters1[2].Value = UserID;
                CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
                sqllist.Add(cmd1);
                return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
            }
            return Delete(UserID, UnfellowUserID);

        }

        public List<Maticsoft.Model.SNS.UserShip> DataTableToListEx(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserShip> modelList = new List<Maticsoft.Model.SNS.UserShip>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.UserShip model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModelEx(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.UserShip DataRowToModelEx(DataRow row)
        {
            Maticsoft.Model.SNS.UserShip model = new Maticsoft.Model.SNS.UserShip();
            if (row != null)
            {
                if (row["ActiveUserID"] != null && row["ActiveUserID"].ToString() != "")
                {
                    model.ActiveUserID = int.Parse(row["ActiveUserID"].ToString());
                }
                if (row["PassiveUserID"] != null && row["PassiveUserID"].ToString() != "")
                {
                    model.PassiveUserID = int.Parse(row["PassiveUserID"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["CategoryID"] != null && row["CategoryID"].ToString() != "")
                {
                    model.CategoryID = int.Parse(row["CategoryID"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
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
                if (row["NickName"] != null && row["NickName"].ToString() != "")
                {
                    model.NickName = row["NickName"].ToString();
                }
                if (row["FansCount"] != null && row["FansCount"].ToString() != "")
                {
                    model.FansCount = int.Parse(row["FansCount"].ToString());
                }
                if (row["Singature"] != null && row["Singature"].ToString() != "")
                {
                    model.Singature = row["Singature"].ToString();
                }
                //if (row["IsBothway"] != null && row["IsBothway"].ToString() != "")
                //{
                //    model.IsBothway = true;
                //}
                //else
                //{
                //    model.IsBothway = false;
                //}
               
            }
            return model;
        }

        #region 分页获取用户所有粉丝的数据列表
        /// <summary>
        /// 分页获取用户所有粉丝的数据列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByFansPage(int userid, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *,NickName,FansCount,Singature FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.PassiveUserID desc");
            }
            strSql.Append(")AS Row, T.*  FROM  SNS_UserShip T");
            strSql.AppendFormat(" WHERE T.PassiveUserID={0}", userid);
            strSql.Append(" ) TT");
            strSql.Append(" inner JOIN Accounts_Users AU ON AU.UserID=ActiveUserID ");
            strSql.Append(" inner JOIN Accounts_UsersExp AUE ON AUE.UserID=ActiveUserID ");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 分页获取用户关注的所有用户数据列表
        /// <summary>
        /// 分页获取用户关注的所有用户数据列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByFellowsPage(int userid, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *,NickName,FansCount,Singature FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.PassiveUserID desc");
            }
            strSql.Append(" )AS Row, T.* FROM  SNS_UserShip T ");
            strSql.AppendFormat(" WHERE T.ActiveUserID={0}", userid);
            strSql.Append(" ) TT");
            strSql.Append(" LEFT JOIN Accounts_Users AU ON AU.UserID=PassiveUserID ");
            strSql.Append(" LEFT JOIN Accounts_UsersExp AUE ON AUE.UserID=PassiveUserID ");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 添加关注
        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="ActiveUserID"></param>
        /// <param name="PassiveUserID"></param>
        /// <returns></returns>
        public bool AddAttention(int ActiveUserID, int PassiveUserID)
        {
            //判断用户是否被关注用户的粉丝，即已关注了该用户。
            if (Exists(ActiveUserID, PassiveUserID))
            {
                return true;
            }
            else
            {
                List<CommandInfo> sqllist = new List<CommandInfo>();
                int Type = (int)Maticsoft.Model.SNS.EnumHelper.FansType.Normal;

                //判断被关注用户是否是用户的粉丝，即被关注者关注了用户。
                if (Exists(PassiveUserID, ActiveUserID))
                {
                    Type = (int)Maticsoft.Model.SNS.EnumHelper.FansType.EachOher;

                    #region 若被关注用户是用户的粉丝，则更新被关注用户和用户的用户关系类型为：互相关注。
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update SNS_UserShip set ");
                    strSql.Append("Type=@Type");
                    strSql.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                    SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
                    new SqlParameter("@PassiveUserID", SqlDbType.Int,4)};
                    parameters[0].Value = Type;
                    parameters[1].Value = PassiveUserID;
                    parameters[2].Value = ActiveUserID;
                    CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
                    sqllist.Add(cmd);
                    #endregion
                }

                #region 新增一条数据
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("insert into SNS_UserShip(");
                strSql1.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
                strSql1.Append(" values (");
                strSql1.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
                SqlParameter[] parameters1 = {
                    new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
                    new SqlParameter("@PassiveUserID", SqlDbType.Int,4),
                    new SqlParameter("@State", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@CategoryID", SqlDbType.Int,4),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@IsRead", SqlDbType.Bit,1)};
                parameters1[0].Value = ActiveUserID;
                parameters1[1].Value = PassiveUserID;
                parameters1[2].Value = 1;//状态：1暂时默认同意
                parameters1[3].Value = Type;
                parameters1[4].Value = 0;//分类：0暂无分类
                parameters1[5].Value = DateTime.Now;
                parameters1[6].Value = false;
                CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
                sqllist.Add(cmd1);
                #endregion

                #region 用户的关注数+1
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append(" update Accounts_UsersExp set ");
                strSql2.Append(" FellowCount=FellowCount+1 ");
                strSql2.Append(" where UserID=@UserID");
                SqlParameter[] parameters2 = {
                    new SqlParameter("@UserID", SqlDbType.Int,4)};
                parameters2[0].Value = ActiveUserID;
                CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd2);
                #endregion

                #region 被关注的用户粉丝数+1
                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("update Accounts_UsersExp set ");
                strSql3.Append("FansCount=FansCount+1");
                strSql3.Append(" where UserID=@UserID");
                SqlParameter[] parameters3 = {
                    new SqlParameter("@UserID", SqlDbType.Int,4)};
                parameters3[0].Value = PassiveUserID;
                CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
                sqllist.Add(cmd3);
                #endregion

                return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
            }
        }
        #endregion

        #region 取消关注
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="ActiveUserID"></param>
        /// <param name="PassiveUserID"></param>
        /// <returns></returns>
        public bool CancelAttention(int ActiveUserID, int PassiveUserID)
        {
            //判断用户是否被关注用户的粉丝，即已关注了该用户。
            if (Exists(ActiveUserID, PassiveUserID))
            {
                List<CommandInfo> sqllist = new List<CommandInfo>();
                //判断被关注用户是否是用户的粉丝，即被关注者关注了用户。
                if (Exists(PassiveUserID, ActiveUserID))
                {
                    int Type = (int)Maticsoft.Model.SNS.EnumHelper.FansType.Normal;

                    #region 若被关注用户是用户的粉丝，则更新被关注用户和用户的用户关系类型为：互相关注。
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update SNS_UserShip set ");
                    strSql.Append("Type=@Type");
                    strSql.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                    SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
                    new SqlParameter("@PassiveUserID", SqlDbType.Int,4)};
                    parameters[0].Value = Type;
                    parameters[1].Value = PassiveUserID;
                    parameters[2].Value = ActiveUserID;
                    CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
                    sqllist.Add(cmd);
                    #endregion
                }

                #region 删除一条数据
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("DELETE FROM  SNS_UserShip ");
                strSql1.Append(" where ActiveUserID=@ActiveUserID AND PassiveUserID=@PassiveUserID");
                SqlParameter[] parameters1 = {
                    new SqlParameter("@ActiveUserID", SqlDbType.Int,4),
                    new SqlParameter("@PassiveUserID", SqlDbType.Int,4)};
                parameters1[0].Value = ActiveUserID;
                parameters1[1].Value = PassiveUserID;
                CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
                sqllist.Add(cmd1);
                #endregion

                #region 用户的关注数-1
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append(" update Accounts_UsersExp set ");
                strSql2.Append(" FellowCount=FellowCount-1 ");
                strSql2.Append(" where UserID=@UserID");
                SqlParameter[] parameters2 = {
                    new SqlParameter("@UserID", SqlDbType.Int,4)};
                parameters2[0].Value = ActiveUserID;
                CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd2);
                #endregion

                #region 被关注的用户粉丝数-1
                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("update Accounts_UsersExp set ");
                strSql3.Append("FansCount=FansCount-1");
                strSql3.Append(" where UserID=@UserID");
                SqlParameter[] parameters3 = {
                    new SqlParameter("@UserID", SqlDbType.Int,4)};
                parameters3[0].Value = PassiveUserID;
                CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
                sqllist.Add(cmd3);
                #endregion

                return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #endregion MethodEx
    }
}

