using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Members
{
    /// <summary>
    /// 数据访问类:UserBind
    /// </summary>
    public partial class UserBind : IUserBind
    {
        public UserBind()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("BindId", "Accounts_UserBind");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BindId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_UserBind");
            strSql.Append(" where BindId=@BindId");
            SqlParameter[] parameters = {
					new SqlParameter("@BindId", SqlDbType.Int,4)
			};
            parameters[0].Value = BindId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.UserBind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UserBind(");
            strSql.Append("UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@TokenAccess,@TokenExpireTime,@TokenRefresh,@MediaUserID,@MediaNickName,@MediaID,@iHome,@Comment,@GroupTopic,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@TokenAccess", SqlDbType.NVarChar,200),
					new SqlParameter("@TokenExpireTime", SqlDbType.DateTime),
					new SqlParameter("@TokenRefresh", SqlDbType.NVarChar,200),
					new SqlParameter("@MediaUserID", SqlDbType.NVarChar,1000),
					new SqlParameter("@MediaNickName", SqlDbType.NVarChar,200),
					new SqlParameter("@MediaID", SqlDbType.Int,4),
					new SqlParameter("@iHome", SqlDbType.Bit,1),
					new SqlParameter("@Comment", SqlDbType.Bit,1),
					new SqlParameter("@GroupTopic", SqlDbType.Bit,1),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.TokenAccess;
            parameters[2].Value = model.TokenExpireTime;
            parameters[3].Value = model.TokenRefresh;
            parameters[4].Value = model.MediaUserID;
            parameters[5].Value = model.MediaNickName;
            parameters[6].Value = model.MediaID;
            parameters[7].Value = model.iHome;
            parameters[8].Value = model.Comment;
            parameters[9].Value = model.GroupTopic;
            parameters[10].Value = model.Status;

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
        public bool Update(Maticsoft.Model.Members.UserBind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UserBind set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("TokenAccess=@TokenAccess,");
            strSql.Append("TokenExpireTime=@TokenExpireTime,");
            strSql.Append("TokenRefresh=@TokenRefresh,");
            strSql.Append("MediaUserID=@MediaUserID,");
            strSql.Append("MediaNickName=@MediaNickName,");
            strSql.Append("MediaID=@MediaID,");
            strSql.Append("iHome=@iHome,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("GroupTopic=@GroupTopic,");
            strSql.Append("Status=@Status");
            strSql.Append(" where BindId=@BindId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@TokenAccess", SqlDbType.NVarChar,200),
					new SqlParameter("@TokenExpireTime", SqlDbType.DateTime),
					new SqlParameter("@TokenRefresh", SqlDbType.NVarChar,200),
					new SqlParameter("@MediaUserID", SqlDbType.NVarChar,1000),
					new SqlParameter("@MediaNickName", SqlDbType.NVarChar,200),
					new SqlParameter("@MediaID", SqlDbType.Int,4),
					new SqlParameter("@iHome", SqlDbType.Bit,1),
					new SqlParameter("@Comment", SqlDbType.Bit,1),
					new SqlParameter("@GroupTopic", SqlDbType.Bit,1),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@BindId", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.TokenAccess;
            parameters[2].Value = model.TokenExpireTime;
            parameters[3].Value = model.TokenRefresh;
            parameters[4].Value = model.MediaUserID;
            parameters[5].Value = model.MediaNickName;
            parameters[6].Value = model.MediaID;
            parameters[7].Value = model.iHome;
            parameters[8].Value = model.Comment;
            parameters[9].Value = model.GroupTopic;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.BindId;

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
        public bool Delete(int BindId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UserBind ");
            strSql.Append(" where BindId=@BindId");
            SqlParameter[] parameters = {
					new SqlParameter("@BindId", SqlDbType.Int,4)
			};
            parameters[0].Value = BindId;

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
        public bool DeleteList(string BindIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UserBind ");
            strSql.Append(" where BindId in (" + BindIdlist + ")  ");
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
        public Maticsoft.Model.Members.UserBind GetModel(int BindId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BindId,UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status from Accounts_UserBind ");
            strSql.Append(" where BindId=@BindId");
            SqlParameter[] parameters = {
					new SqlParameter("@BindId", SqlDbType.Int,4)
			};
            parameters[0].Value = BindId;

            Maticsoft.Model.Members.UserBind model = new Maticsoft.Model.Members.UserBind();
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
        public Maticsoft.Model.Members.UserBind DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.UserBind model = new Maticsoft.Model.Members.UserBind();
            if (row != null)
            {
                if (row["BindId"] != null && row["BindId"].ToString() != "")
                {
                    model.BindId = int.Parse(row["BindId"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["TokenAccess"] != null)
                {
                    model.TokenAccess = row["TokenAccess"].ToString();
                }
                if (row["TokenExpireTime"] != null && row["TokenExpireTime"].ToString() != "")
                {
                    model.TokenExpireTime = DateTime.Parse(row["TokenExpireTime"].ToString());
                }
                if (row["TokenRefresh"] != null)
                {
                    model.TokenRefresh = row["TokenRefresh"].ToString();
                }
                if (row["MediaUserID"] != null)
                {
                    model.MediaUserID = row["MediaUserID"].ToString();
                }
                if (row["MediaNickName"] != null)
                {
                    model.MediaNickName = row["MediaNickName"].ToString();
                }
                if (row["MediaID"] != null && row["MediaID"].ToString() != "")
                {
                    model.MediaID = int.Parse(row["MediaID"].ToString());
                }
                if (row["iHome"] != null && row["iHome"].ToString() != "")
                {
                    if ((row["iHome"].ToString() == "1") || (row["iHome"].ToString().ToLower() == "true"))
                    {
                        model.iHome = true;
                    }
                    else
                    {
                        model.iHome = false;
                    }
                }
                if (row["Comment"] != null && row["Comment"].ToString() != "")
                {
                    if ((row["Comment"].ToString() == "1") || (row["Comment"].ToString().ToLower() == "true"))
                    {
                        model.Comment = true;
                    }
                    else
                    {
                        model.Comment = false;
                    }
                }
                if (row["GroupTopic"] != null && row["GroupTopic"].ToString() != "")
                {
                    if ((row["GroupTopic"].ToString() == "1") || (row["GroupTopic"].ToString().ToLower() == "true"))
                    {
                        model.GroupTopic = true;
                    }
                    else
                    {
                        model.GroupTopic = false;
                    }
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
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
            strSql.Append("select BindId,UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status ");
            strSql.Append(" FROM Accounts_UserBind ");
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
            strSql.Append(" BindId,UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status ");
            strSql.Append(" FROM Accounts_UserBind ");
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
            strSql.Append("select count(1) FROM Accounts_UserBind ");
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
                strSql.Append("order by T.BindId desc");
            }
            strSql.Append(")AS Row, T.*  from Accounts_UserBind T ");
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
            parameters[0].Value = "Accounts_UserBind";
            parameters[1].Value = "BindId";
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.UserBind GetModel(int userId, int MediaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Accounts_UserBind ");
            strSql.Append(" where userId=@UserId and MediaID=@MediaID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@MediaID", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;
            parameters[1].Value = MediaID;

            Maticsoft.Model.Members.UserBind model = new Maticsoft.Model.Members.UserBind();
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


        public bool Exists(int userId, string  MediaUserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_UserBind");
            strSql.Append(" where userId=@UserId and MediaUserID=@MediaUserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@MediaUserID", SqlDbType.NVarChar,1000)
			};
            parameters[0].Value = userId;
            parameters[1].Value = MediaUserID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateEx(Maticsoft.Model.Members.UserBind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UserBind set ");
            strSql.Append("TokenAccess=@TokenAccess,");
            strSql.Append("TokenExpireTime=@TokenExpireTime,");
            strSql.Append("TokenRefresh=@TokenRefresh,");
            strSql.Append("MediaNickName=@MediaNickName,");
            strSql.Append("MediaID=@MediaID,");
            strSql.Append("iHome=@iHome,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("GroupTopic=@GroupTopic,");
            strSql.Append("Status=@Status");
            strSql.Append(" where UserId=@UserId and MediaUserID=@MediaUserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@TokenAccess", SqlDbType.NVarChar,200),
					new SqlParameter("@TokenExpireTime", SqlDbType.DateTime),
					new SqlParameter("@TokenRefresh", SqlDbType.NVarChar,200),
					new SqlParameter("@MediaUserID", SqlDbType.NVarChar,1000),
					new SqlParameter("@MediaNickName", SqlDbType.NVarChar,200),
					new SqlParameter("@MediaID", SqlDbType.Int,4),
					new SqlParameter("@iHome", SqlDbType.Bit,1),
					new SqlParameter("@Comment", SqlDbType.Bit,1),
					new SqlParameter("@GroupTopic", SqlDbType.Bit,1),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@BindId", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.TokenAccess;
            parameters[2].Value = model.TokenExpireTime;
            parameters[3].Value = model.TokenRefresh;
            parameters[4].Value = model.MediaUserID;
            parameters[5].Value = model.MediaNickName;
            parameters[6].Value = model.MediaID;
            parameters[7].Value = model.iHome;
            parameters[8].Value = model.Comment;
            parameters[9].Value = model.GroupTopic;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.BindId;

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

