/**
* AttendanceType.cs
*
* 功 能： N/A
* 类 名： AttendanceType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/27 22:58:29   N/A    初版
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
    /// 数据访问类:AttendanceType
    /// </summary>
    public partial class AttendanceType : IAttendanceType
    {
        public AttendanceType()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TypeID", "JLT_AttendanceType");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from JLT_AttendanceType");
            strSql.Append(" where TypeID=@TypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = TypeID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.JLT.AttendanceType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into JLT_AttendanceType(");
            strSql.Append("TypeName,CreatedDate,Status,Sequence,Remark)");
            strSql.Append(" values (");
            strSql.Append("@TypeName,@CreatedDate,@Status,@Sequence,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Sequence;
            parameters[4].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.JLT.AttendanceType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_AttendanceType set ");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where TypeID=@TypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@TypeID", SqlDbType.Int,4)};
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Sequence;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.TypeID;

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
        public bool Delete(int TypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from JLT_AttendanceType ");
            strSql.Append(" where TypeID=@TypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = TypeID;

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
        public bool DeleteList(string TypeIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from JLT_AttendanceType ");
            strSql.Append(" where TypeID in (" + TypeIDlist + ")  ");
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
        public Maticsoft.Model.JLT.AttendanceType GetModel(int TypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TypeID,TypeName,CreatedDate,Status,Sequence,Remark from JLT_AttendanceType ");
            strSql.Append(" where TypeID=@TypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = TypeID;

            Maticsoft.Model.JLT.AttendanceType model = new Maticsoft.Model.JLT.AttendanceType();
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
        public Maticsoft.Model.JLT.AttendanceType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.JLT.AttendanceType model = new Maticsoft.Model.JLT.AttendanceType();
            if (row != null)
            {
                if (row["TypeID"] != null && row["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if (row["TypeName"] != null)
                {
                    model.TypeName = row["TypeName"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
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
            strSql.Append("select TypeID,TypeName,CreatedDate,Status,Sequence,Remark ");
            strSql.Append(" FROM JLT_AttendanceType ");
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
            strSql.Append(" TypeID,TypeName,CreatedDate,Status,Sequence,Remark ");
            strSql.Append(" FROM JLT_AttendanceType ");
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
            strSql.Append("select count(1) FROM JLT_AttendanceType ");
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
                strSql.Append("order by T.TypeID desc");
            }
            strSql.Append(")AS Row, T.*  from JLT_AttendanceType T ");
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
            parameters[0].Value = "JLT_AttendanceType";
            parameters[1].Value = "TypeID";
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
        /// 批量处理
        /// </summary>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_AttendanceType set " + strWhere);
            strSql.Append(" where TypeID in(" + IDlist + ")  ");
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM JLT_AttendanceType ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                strSql.Append("order by " + orderBy);
            }
            else
            {
                strSql.Append("order by TypeID desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  ExtensionMethod
    }
}

