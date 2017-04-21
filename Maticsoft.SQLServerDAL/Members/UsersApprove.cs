/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：UsersApprove.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/10/25 15:36:34
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;//Please add references
using Maticsoft.IDAL.Members;

namespace Maticsoft.SQLServerDAL.Members
{
    /// <summary>
    /// 数据访问类:UsersApprove
    /// </summary>
    public partial class UsersApprove : IUsersApprove
    {
        public UsersApprove()
        { }

        #region Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ApproveID", "Accounts_UsersApprove");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ApproveID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Accounts_UsersApprove");
            strSql.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] parameters = {
					new SqlParameter("@ApproveID", SqlDbType.Int,4)
			};
            parameters[0].Value = ApproveID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.UsersApprove model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Accounts_UsersApprove(");
            strSql.Append("UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserID,@TrueName,@IDCardNum,@FrontView,@RearView,@DueDate,@Status,@ApproveUserID,@UserType,@CreatedDate,@ApproveDate)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@IDCardNum", SqlDbType.NVarChar,20),
					new SqlParameter("@FrontView", SqlDbType.NVarChar,500),
					new SqlParameter("@RearView", SqlDbType.NVarChar,500),
					new SqlParameter("@DueDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ApproveUserID", SqlDbType.Int,4),
					new SqlParameter("@UserType", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ApproveDate", SqlDbType.DateTime)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.TrueName;
            parameters[2].Value = model.IDCardNum;
            parameters[3].Value = model.FrontView;
            parameters[4].Value = model.RearView;
            parameters[5].Value = model.DueDate;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.ApproveUserID;
            parameters[8].Value = model.UserType;
            parameters[9].Value = model.CreatedDate;
            parameters[10].Value = model.ApproveDate;

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
        public bool Update(Maticsoft.Model.Members.UsersApprove model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_UsersApprove SET ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("TrueName=@TrueName,");
            strSql.Append("IDCardNum=@IDCardNum,");
            strSql.Append("FrontView=@FrontView,");
            strSql.Append("RearView=@RearView,");
            strSql.Append("DueDate=@DueDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("ApproveUserID=@ApproveUserID,");
            strSql.Append("UserType=@UserType,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("ApproveDate=@ApproveDate");
            strSql.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@IDCardNum", SqlDbType.NVarChar,20),
					new SqlParameter("@FrontView", SqlDbType.NVarChar,500),
					new SqlParameter("@RearView", SqlDbType.NVarChar,500),
					new SqlParameter("@DueDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ApproveUserID", SqlDbType.Int,4),
					new SqlParameter("@UserType", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ApproveDate", SqlDbType.DateTime),
					new SqlParameter("@ApproveID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.TrueName;
            parameters[2].Value = model.IDCardNum;
            parameters[3].Value = model.FrontView;
            parameters[4].Value = model.RearView;
            parameters[5].Value = model.DueDate;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.ApproveUserID;
            parameters[8].Value = model.UserType;
            parameters[9].Value = model.CreatedDate;
            parameters[10].Value = model.ApproveDate;
            parameters[11].Value = model.ApproveID;

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
        public bool Delete(int ApproveID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Accounts_UsersApprove ");
            strSql.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] parameters = {
					new SqlParameter("@ApproveID", SqlDbType.Int,4)
			};
            parameters[0].Value = ApproveID;

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
        public bool DeleteList(string ApproveIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Accounts_UsersApprove ");
            strSql.Append(" WHERE ApproveID in (" + ApproveIDlist + ")  ");
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
        public Maticsoft.Model.Members.UsersApprove GetModel(int ApproveID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate FROM Accounts_UsersApprove ");
            strSql.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] parameters = {
					new SqlParameter("@ApproveID", SqlDbType.Int,4)
			};
            parameters[0].Value = ApproveID;

            Maticsoft.Model.Members.UsersApprove model = new Maticsoft.Model.Members.UsersApprove();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ApproveID"] != null && ds.Tables[0].Rows[0]["ApproveID"].ToString() != "")
                {
                    model.ApproveID = int.Parse(ds.Tables[0].Rows[0]["ApproveID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TrueName"] != null && ds.Tables[0].Rows[0]["TrueName"].ToString() != "")
                {
                    model.TrueName = ds.Tables[0].Rows[0]["TrueName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IDCardNum"] != null && ds.Tables[0].Rows[0]["IDCardNum"].ToString() != "")
                {
                    model.IDCardNum = ds.Tables[0].Rows[0]["IDCardNum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FrontView"] != null && ds.Tables[0].Rows[0]["FrontView"].ToString() != "")
                {
                    model.FrontView = ds.Tables[0].Rows[0]["FrontView"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RearView"] != null && ds.Tables[0].Rows[0]["RearView"].ToString() != "")
                {
                    model.RearView = ds.Tables[0].Rows[0]["RearView"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DueDate"] != null && ds.Tables[0].Rows[0]["DueDate"].ToString() != "")
                {
                    model.DueDate = DateTime.Parse(ds.Tables[0].Rows[0]["DueDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"] != null && ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApproveUserID"] != null && ds.Tables[0].Rows[0]["ApproveUserID"].ToString() != "")
                {
                    model.ApproveUserID = int.Parse(ds.Tables[0].Rows[0]["ApproveUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApproveDate"] != null && ds.Tables[0].Rows[0]["ApproveDate"].ToString() != "")
                {
                    model.ApproveDate = DateTime.Parse(ds.Tables[0].Rows[0]["ApproveDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate ");
            strSql.Append(" FROM Accounts_UsersApprove ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate ");
            strSql.Append(" FROM Accounts_UsersApprove ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Accounts_UsersApprove ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
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
                strSql.Append("ORDER BY T." + orderby);
            }
            else
            {
                strSql.Append("ORDER BY T.ApproveID desc");
            }
            strSql.Append(")AS Row, T.*  FROM Accounts_UsersApprove T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion Method

        #region ExMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetApproveList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ApproveID, UA.UserID,UserName, UA.TrueName, IDCardNum, FrontView, RearView, DueDate, Status, ApproveUserID, UA.UserType, CreatedDate, ApproveDate  ");
            strSql.Append("FROM Accounts_UsersApprove UA  ");
            strSql.Append("LEFT JOIN Accounts_Users AU ON UA.UserID = AU.UserID ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(strWhere);
            }
            strSql.Append("ORDER BY UA.CreatedDate DESC ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 批量更新认证信息
        /// </summary>
        /// <param name="ids">待更新的ID</param>
        /// <param name="status">更新状态</param>
        /// <returns>是否更新成功</returns>
        public bool BatchUpdate(string ids, string status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_UsersApprove ");
            strSql.AppendFormat("SET Status={0} ,ApproveDate=GETDATE() ", status);
            strSql.AppendFormat("WHERE ApproveID IN ({0}) ", ids);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.UsersApprove GetModelByUserID(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate FROM Accounts_UsersApprove ");
            strSql.Append(" WHERE UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters[0].Value = UserID;

            Maticsoft.Model.Members.UsersApprove model = new Maticsoft.Model.Members.UsersApprove();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ApproveID"] != null && ds.Tables[0].Rows[0]["ApproveID"].ToString() != "")
                {
                    model.ApproveID = int.Parse(ds.Tables[0].Rows[0]["ApproveID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TrueName"] != null && ds.Tables[0].Rows[0]["TrueName"].ToString() != "")
                {
                    model.TrueName = ds.Tables[0].Rows[0]["TrueName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IDCardNum"] != null && ds.Tables[0].Rows[0]["IDCardNum"].ToString() != "")
                {
                    model.IDCardNum = ds.Tables[0].Rows[0]["IDCardNum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FrontView"] != null && ds.Tables[0].Rows[0]["FrontView"].ToString() != "")
                {
                    model.FrontView = ds.Tables[0].Rows[0]["FrontView"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RearView"] != null && ds.Tables[0].Rows[0]["RearView"].ToString() != "")
                {
                    model.RearView = ds.Tables[0].Rows[0]["RearView"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DueDate"] != null && ds.Tables[0].Rows[0]["DueDate"].ToString() != "")
                {
                    model.DueDate = DateTime.Parse(ds.Tables[0].Rows[0]["DueDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"] != null && ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApproveUserID"] != null && ds.Tables[0].Rows[0]["ApproveUserID"].ToString() != "")
                {
                    model.ApproveUserID = int.Parse(ds.Tables[0].Rows[0]["ApproveUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApproveDate"] != null && ds.Tables[0].Rows[0]["ApproveDate"].ToString() != "")
                {
                    model.ApproveDate = DateTime.Parse(ds.Tables[0].Rows[0]["ApproveDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByUserId(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Accounts_UsersApprove ");
            strSql.Append(" WHERE UserID=@UserID AND Status=2 ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;

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
        #endregion ExMethod
    }
}