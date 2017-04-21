/**
* Star.cs
*
* 功 能： N/A
* 类 名： Star
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:55   N/A    初版
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
using System.Collections.Generic;

namespace Maticsoft.SQLServerDAL.SNS
{
    /// <summary>
    /// 数据访问类:Star
    /// </summary>
    public partial class Star : IStar
    {
        public Star()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在重复记录
        /// </summary>
        public bool Exists(int UserID, int TypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Star");
            strSql.Append(" where UserID=@UserID and TypeID=@TypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@TypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = UserID;
            parameters[1].Value = TypeID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Star model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Star(");
            strSql.Append("UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@TypeID,@NickName,@UserGravatar,@ApplyReason,@CreatedDate,@ExpiredDate,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserGravatar", SqlDbType.NVarChar,200),
					new SqlParameter("@ApplyReason", SqlDbType.NVarChar),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ExpiredDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.TypeID;
            parameters[2].Value = model.NickName;
            parameters[3].Value = model.UserGravatar;
            parameters[4].Value = model.ApplyReason;
            parameters[5].Value = model.CreatedDate;
            parameters[6].Value = model.ExpiredDate;
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
        public bool Update(Maticsoft.Model.SNS.Star model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Star set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("NickName=@NickName,");
            strSql.Append("UserGravatar=@UserGravatar,");
            strSql.Append("ApplyReason=@ApplyReason,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("ExpiredDate=@ExpiredDate,");
            strSql.Append("Status=@Status");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserGravatar", SqlDbType.NVarChar,200),
					new SqlParameter("@ApplyReason", SqlDbType.NVarChar),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ExpiredDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.TypeID;
            parameters[2].Value = model.NickName;
            parameters[3].Value = model.UserGravatar;
            parameters[4].Value = model.ApplyReason;
            parameters[5].Value = model.CreatedDate;
            parameters[6].Value = model.ExpiredDate;
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
            strSql.Append("delete from SNS_Star ");
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
            strSql.Append("delete from SNS_Star ");
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
        public Maticsoft.Model.SNS.Star GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status from SNS_Star ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.SNS.Star model = new Maticsoft.Model.SNS.Star();
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
        public Maticsoft.Model.SNS.Star DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Star model = new Maticsoft.Model.SNS.Star();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["TypeID"] != null && row["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if (row["NickName"] != null)
                {
                    model.NickName = row["NickName"].ToString();
                }
                if (row["UserGravatar"] != null)
                {
                    model.UserGravatar = row["UserGravatar"].ToString();
                }
                if (row["ApplyReason"] != null)
                {
                    model.ApplyReason = row["ApplyReason"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["ExpiredDate"] != null && row["ExpiredDate"].ToString() != "")
                {
                    model.ExpiredDate = DateTime.Parse(row["ExpiredDate"].ToString());
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
            strSql.Append("select ID,UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status ");
            strSql.Append(" FROM SNS_Star ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY CreatedDate DESC");
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
            strSql.Append(" ID,UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status ");
            strSql.Append(" FROM SNS_Star ");
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
            strSql.Append("select count(1) FROM SNS_Star ");
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
            strSql.Append(")AS Row, T.*  from SNS_Star T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 批量删除数据,发回原数据
        /// </summary>
        public bool DeleteList(string IDlist,out DataSet ds)
        {
            ds = GetList("ID in (" + IDlist + ") ");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Star ");
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
            parameters[0].Value = "SNS_Star";
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

        public bool UpdateStateList(string IDlist, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Star set ");
            strSql.AppendFormat("Status={0} ", status);
            strSql.AppendFormat(" where ID in ({0})", IDlist);

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
        /// 是否是达人
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public bool IsStar(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM SNS_Star ");
            strSql.AppendFormat("WHERE UserID={0} AND Status =1 ", userId);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return false;
            }
            else
            {
                return Convert.ToInt32(obj) > 0;
            }
        }

        /// <summary>
        /// 查询用户申请的达人信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataSet StarName(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ST.TypeName FROM SNS_Star S ");
            strSql.Append("LEFT JOIN SNS_StarType ST ON S.TypeID = ST.TypeID ");
            strSql.AppendFormat("WHERE UserID={0} AND S.Status=1 ",userId);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 是否存在重复记录
        /// </summary>
        public bool IsExists(int UserID, int TypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Star");
            strSql.Append(" where UserID=@UserID and TypeID=@TypeID and Status=1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@TypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = UserID;
            parameters[1].Value = TypeID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion ExtensionMethod
    }
}