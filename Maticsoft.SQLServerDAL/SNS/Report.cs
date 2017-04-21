/**
* Report.cs
*
* 功 能： N/A
* 类 名： Report
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.SNS;

namespace Maticsoft.SQLServerDAL.SNS
{
    /// <summary>
    /// 数据访问类:Report
    /// </summary>
    public partial class Report : IReport
    {
        public Report()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Report");
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
        public int Add(Maticsoft.Model.SNS.Report model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Report(");
            strSql.Append("ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status)");
            strSql.Append(" values (");
            strSql.Append("@ReportTypeID,@TargetType,@TargetID,@CreatedUserID,@CreatedNickName,@Description,@CreatedDate,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReportTypeID", SqlDbType.Int,4),
					new SqlParameter("@TargetType", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.ReportTypeID;
            parameters[1].Value = model.TargetType;
            parameters[2].Value = model.TargetID;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.CreatedNickName;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Status;

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
        public bool Update(Maticsoft.Model.SNS.Report model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Report set ");
            strSql.Append("ReportTypeID=@ReportTypeID,");
            strSql.Append("TargetType=@TargetType,");
            strSql.Append("TargetID=@TargetID,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("CreatedNickName=@CreatedNickName,");
            strSql.Append("Description=@Description,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Status=@Status");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ReportTypeID", SqlDbType.Int,4),
					new SqlParameter("@TargetType", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ReportTypeID;
            parameters[1].Value = model.TargetType;
            parameters[2].Value = model.TargetID;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.CreatedNickName;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.ID;

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
            strSql.Append("delete from SNS_Report ");
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
            strSql.Append("delete from SNS_Report ");
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
        public Maticsoft.Model.SNS.Report GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status from SNS_Report ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.SNS.Report model = new Maticsoft.Model.SNS.Report();
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
        public Maticsoft.Model.SNS.Report DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Report model = new Maticsoft.Model.SNS.Report();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ReportTypeID"] != null && row["ReportTypeID"].ToString() != "")
                {
                    model.ReportTypeID = int.Parse(row["ReportTypeID"].ToString());
                }
                if (row["TargetType"] != null && row["TargetType"].ToString() != "")
                {
                    model.TargetType = int.Parse(row["TargetType"].ToString());
                }
                if (row["TargetID"] != null && row["TargetID"].ToString() != "")
                {
                    model.TargetID = int.Parse(row["TargetID"].ToString());
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    model.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
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
            strSql.Append("select ID,ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status ");
            strSql.Append(" FROM SNS_Report ");
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
            strSql.Append(" ID,ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status ");
            strSql.Append(" FROM SNS_Report ");
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
            strSql.Append("select count(1) FROM SNS_Report ");
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
            strSql.Append(")AS Row, T.*  from SNS_Report T ");
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
            parameters[0].Value = "SNS_Report";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion BasicMethod

        #region ExtensionMethod

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" SNSR.*,SNSRT.* ");
            strSql.Append(" FROM SNS_Report SNSR LEFT JOIN SNS_ReportType SNSRT ON SNSRT.ID=SNSR.ReportTypeID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" ORDER BY " + filedOrder);
            }
            else
            {
                strSql.Append(" ORDER BY SNSR.ID DESC");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新举报内容状态
        /// </summary>
        public bool UpdateReportStatus(int status,int reportId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SNS_Report ");
            strSql.Append("SET Status=@Status ");
            strSql.Append("WHERE ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@ID", SqlDbType.Int)
                    };
            parameters[0].Value = status;
            parameters[1].Value = reportId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 批量更新举报内容状态
        /// </summary>
        public bool UpdateReportStatus(int status,string reportIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SNS_Report ");
            strSql.Append("SET Status="+status+" ");
            strSql.Append("WHERE ID in("+reportIds+") ");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }
        #endregion ExtensionMethod
    }
}