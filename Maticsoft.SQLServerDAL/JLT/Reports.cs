/**
* Reports.cs
*
* 功 能： N/A
* 类 名： Reports
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/24 15:53:42   N/A    初版
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
using Maticsoft.IDAL.JLT;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.JLT
{
    /// <summary>
    /// 数据访问类:Reports
    /// </summary>
    public partial class Reports : IReports
    {
        public Reports()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "JLT_Reports");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from JLT_Reports");
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
        public int Add(Maticsoft.Model.JLT.Reports model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into JLT_Reports(");
            strSql.Append("EnterpriseID,UserId,Title,Content,Type,Status,FileNames,FileDataPath,CreatedDate,ReportDate,Remark)");
            strSql.Append(" values (");
            strSql.Append("@EnterpriseID,@UserId,@Title,@Content,@Type,@Status,@FileNames,@FileDataPath,@CreatedDate,@ReportDate,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@Content", SqlDbType.Text),
                    new SqlParameter("@Type", SqlDbType.SmallInt,2),
                    new SqlParameter("@Status", SqlDbType.SmallInt,2),
                    new SqlParameter("@FileNames", SqlDbType.NVarChar,500),
                    new SqlParameter("@FileDataPath", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@ReportDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.EnterpriseID;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.Type;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.FileNames;
            parameters[7].Value = model.FileDataPath;
            parameters[8].Value = model.CreatedDate;
            parameters[9].Value = model.ReportDate;
            parameters[10].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.JLT.Reports model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_Reports set ");
            strSql.Append("EnterpriseID=@EnterpriseID,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("Title=@Title,");
            strSql.Append("Content=@Content,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("FileNames=@FileNames,");
            strSql.Append("FileDataPath=@FileDataPath,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("ReportDate=@ReportDate,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@Content", SqlDbType.Text),
                    new SqlParameter("@Type", SqlDbType.SmallInt,2),
                    new SqlParameter("@Status", SqlDbType.SmallInt,2),
                    new SqlParameter("@FileNames", SqlDbType.NVarChar,500),
                    new SqlParameter("@FileDataPath", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@ReportDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.EnterpriseID;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.Type;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.FileNames;
            parameters[7].Value = model.FileDataPath;
            parameters[8].Value = model.CreatedDate;
            parameters[9].Value = model.ReportDate;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.ID;

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
            strSql.Append("delete from JLT_Reports ");
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
            strSql.Append("delete from JLT_Reports ");
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
        public Maticsoft.Model.JLT.Reports GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,EnterpriseID,UserId,Title,Content,Type,Status,FileNames,FileDataPath,CreatedDate,ReportDate,Remark from JLT_Reports ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            Maticsoft.Model.JLT.Reports model = new Maticsoft.Model.JLT.Reports();
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
        public Maticsoft.Model.JLT.Reports DataRowToModel(DataRow row)
        {
            Maticsoft.Model.JLT.Reports model = new Maticsoft.Model.JLT.Reports();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["EnterpriseID"] != null && row["EnterpriseID"].ToString() != "")
                {
                    model.EnterpriseID = int.Parse(row["EnterpriseID"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Content"] != null)
                {
                    model.Content = row["Content"].ToString();
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["FileNames"] != null)
                {
                    model.FileNames = row["FileNames"].ToString();
                }
                if (row["FileDataPath"] != null)
                {
                    model.FileDataPath = row["FileDataPath"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["ReportDate"] != null && row["ReportDate"].ToString() != "")
                {
                    model.ReportDate = DateTime.Parse(row["ReportDate"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
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
            strSql.Append("select ID,EnterpriseID,UserId,Title,Content,Type,Status,FileNames,FileDataPath,CreatedDate,ReportDate,Remark ");
            strSql.Append(" FROM JLT_Reports ");
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
            strSql.Append(" ID,EnterpriseID,UserId,Title,Content,Type,Status,FileNames,FileDataPath,CreatedDate,ReportDate,Remark ");
            strSql.Append(" FROM JLT_Reports ");
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
            strSql.Append("select count(1) FROM JLT_Reports ");
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
            strSql.Append(")AS Row, T.*  from JLT_Reports T ");
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
            parameters[0].Value = "JLT_Reports";
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,RP.UserId,Content,Type,Status,FileDataPath,CreatedDate,ReportDate,Remark,AU.UserName,FileNames ");
            strSql.Append(" FROM JLT_Reports RP LEFT JOIN Accounts_Users AU ON RP.UserId = AU.UserId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool Update(int id, string fileNames, string fileDataPath)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_Reports set ");
            strSql.Append("FileNames=@FileNames,");
            strSql.Append("FileDataPath=@FileDataPath");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileNames", SqlDbType.NVarChar,500),
                    new SqlParameter("@FileDataPath", SqlDbType.NVarChar,2000),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = fileNames;
            parameters[1].Value = fileDataPath;
            parameters[2].Value = id;

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

