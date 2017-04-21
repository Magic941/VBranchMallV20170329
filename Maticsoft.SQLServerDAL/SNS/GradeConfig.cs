/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：GradeConfig.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/11/12 14:54:12
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
using Maticsoft.DBUtility;
using Maticsoft.IDAL.SNS;//Please add references

namespace Maticsoft.SQLServerDAL.SNS
{
    /// <summary>
    /// 数据访问类:GradeConfig
    /// </summary>
    public partial class GradeConfig : IGradeConfig
    {
        public GradeConfig()
        { }

        #region Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GradeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SNS_GradeConfig");
            strSql.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] parameters = {
					new SqlParameter("@GradeID", SqlDbType.Int,4)
			};
            parameters[0].Value = GradeID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.GradeConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SNS_GradeConfig(");
            strSql.Append("GradeName,MinRange,MaxRange,Remark)");
            strSql.Append(" VALUES (");
            strSql.Append("@GradeName,@MinRange,@MaxRange,@Remark)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GradeName", SqlDbType.NVarChar,50),
					new SqlParameter("@MinRange", SqlDbType.Int,4),
					new SqlParameter("@MaxRange", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.GradeName;
            parameters[1].Value = model.MinRange;
            parameters[2].Value = model.MaxRange;
            parameters[3].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.SNS.GradeConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SNS_GradeConfig SET ");
            strSql.Append("GradeName=@GradeName,");
            strSql.Append("MinRange=@MinRange,");
            strSql.Append("MaxRange=@MaxRange,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] parameters = {
					new SqlParameter("@GradeName", SqlDbType.NVarChar,50),
					new SqlParameter("@MinRange", SqlDbType.Int,4),
					new SqlParameter("@MaxRange", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@GradeID", SqlDbType.Int,4)};
            parameters[0].Value = model.GradeName;
            parameters[1].Value = model.MinRange;
            parameters[2].Value = model.MaxRange;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.GradeID;

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
        public bool Delete(int GradeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM SNS_GradeConfig ");
            strSql.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] parameters = {
					new SqlParameter("@GradeID", SqlDbType.Int,4)
			};
            parameters[0].Value = GradeID;

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
        public bool DeleteList(string GradeIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM SNS_GradeConfig ");
            strSql.Append(" WHERE GradeID in (" + GradeIDlist + ")  ");
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
        public Maticsoft.Model.SNS.GradeConfig GetModel(int GradeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 GradeID,GradeName,MinRange,MaxRange,Remark FROM SNS_GradeConfig ");
            strSql.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] parameters = {
					new SqlParameter("@GradeID", SqlDbType.Int,4)
			};
            parameters[0].Value = GradeID;

            Maticsoft.Model.SNS.GradeConfig model = new Maticsoft.Model.SNS.GradeConfig();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["GradeID"] != null && ds.Tables[0].Rows[0]["GradeID"].ToString() != "")
                {
                    model.GradeID = int.Parse(ds.Tables[0].Rows[0]["GradeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GradeName"] != null && ds.Tables[0].Rows[0]["GradeName"].ToString() != "")
                {
                    model.GradeName = ds.Tables[0].Rows[0]["GradeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinRange"] != null && ds.Tables[0].Rows[0]["MinRange"].ToString() != "")
                {
                    model.MinRange = int.Parse(ds.Tables[0].Rows[0]["MinRange"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxRange"] != null && ds.Tables[0].Rows[0]["MaxRange"].ToString() != "")
                {
                    model.MaxRange = int.Parse(ds.Tables[0].Rows[0]["MaxRange"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
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
            strSql.Append("SELECT GradeID,GradeName,MinRange,MaxRange,Remark ");
            strSql.Append(" FROM SNS_GradeConfig ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY MinRange ASC");
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
            strSql.Append(" GradeID,GradeName,MinRange,MaxRange,Remark ");
            strSql.Append(" FROM SNS_GradeConfig ");
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
            strSql.Append("SELECT COUNT(1) FROM SNS_GradeConfig ");
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

        #endregion Method

        #region ExtMethod

        /// <summary>
        /// 根据用户分数获取等级
        /// </summary>
        /// <param name="grades">用户分数</param>
        /// <returns></returns>
        public Maticsoft.Model.SNS.GradeConfig GetUserLevel(int? grades)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1.* FROM SNS_GradeConfig   ");
            strSql.Append("WHERE @Score BETWEEN MinRange AND MaxRange ");

            SqlParameter[] parameters = {
					new SqlParameter("@Score", SqlDbType.Int,4)
			};
            parameters[0].Value = grades;

            Maticsoft.Model.SNS.GradeConfig model = new Maticsoft.Model.SNS.GradeConfig();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["GradeID"] != null && ds.Tables[0].Rows[0]["GradeID"].ToString() != "")
                {
                    model.GradeID = int.Parse(ds.Tables[0].Rows[0]["GradeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GradeName"] != null && ds.Tables[0].Rows[0]["GradeName"].ToString() != "")
                {
                    model.GradeName = ds.Tables[0].Rows[0]["GradeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinRange"] != null && ds.Tables[0].Rows[0]["MinRange"].ToString() != "")
                {
                    model.MinRange = int.Parse(ds.Tables[0].Rows[0]["MinRange"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxRange"] != null && ds.Tables[0].Rows[0]["MaxRange"].ToString() != "")
                {
                    model.MaxRange = int.Parse(ds.Tables[0].Rows[0]["MaxRange"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion ExtMethod
    }
}