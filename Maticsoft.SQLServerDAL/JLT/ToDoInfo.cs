/**
* ToDoInfo.cs
*
* 功 能： N/A
* 类 名： ToDoInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/24 16:24:58   N/A    初版
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
    /// 数据访问类:ToDoInfo
    /// </summary>
    public partial class ToDoInfo : IToDoInfo
    {
        public ToDoInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "JLT_ToDoInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from JLT_ToDoInfo");
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
        public int Add(Maticsoft.Model.JLT.ToDoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into JLT_ToDoInfo(");
            strSql.Append("EnterpriseID,UserId,UserName,Title,Content,ToUserId,ToType,Status,ParentId,CreatedUserId,CreatedDate,ToDoDate,ReviewedUserID,ReviewedContent,ReviewedDate,FileNames,FileDataPath,Remark)");
            strSql.Append(" values (");
            strSql.Append("@EnterpriseID,@UserId,@UserName,@Title,@Content,@ToUserId,@ToType,@Status,@ParentId,@CreatedUserId,@CreatedDate,@ToDoDate,@ReviewedUserID,@ReviewedContent,@ReviewedDate,@FileNames,@FileDataPath,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@Content", SqlDbType.Text),
                    new SqlParameter("@ToUserId", SqlDbType.NVarChar,500),
                    new SqlParameter("@ToType", SqlDbType.SmallInt,2),
                    new SqlParameter("@Status", SqlDbType.SmallInt,2),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDoDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@ReviewedUserID", SqlDbType.Int,4),
                    new SqlParameter("@ReviewedContent", SqlDbType.NVarChar,300),
                    new SqlParameter("@ReviewedDate", SqlDbType.DateTime),
                    new SqlParameter("@FileNames", SqlDbType.NVarChar,500),
                    new SqlParameter("@FileDataPath", SqlDbType.NVarChar,2000),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.EnterpriseID;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.ToUserId;
            parameters[6].Value = model.ToType;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.ParentId;
            parameters[9].Value = model.CreatedUserId;
            parameters[10].Value = model.CreatedDate;
            parameters[11].Value = model.ToDoDate;
            parameters[12].Value = model.ReviewedUserID;
            parameters[13].Value = model.ReviewedContent;
            parameters[14].Value = model.ReviewedDate;
            parameters[15].Value = model.FileNames;
            parameters[16].Value = model.FileDataPath;
            parameters[17].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.JLT.ToDoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_ToDoInfo set ");
            strSql.Append("EnterpriseID=@EnterpriseID,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Title=@Title,");
            strSql.Append("Content=@Content,");
            strSql.Append("ToUserId=@ToUserId,");
            strSql.Append("ToType=@ToType,");
            strSql.Append("Status=@Status,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("CreatedUserId=@CreatedUserId,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("ToDoDate=@ToDoDate,");
            strSql.Append("ReviewedUserID=@ReviewedUserID,");
            strSql.Append("ReviewedContent=@ReviewedContent,");
            strSql.Append("ReviewedDate=@ReviewedDate,");
            strSql.Append("FileNames=@FileNames,");
            strSql.Append("FileDataPath=@FileDataPath,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@Content", SqlDbType.Text),
                    new SqlParameter("@ToUserId", SqlDbType.NVarChar,500),
                    new SqlParameter("@ToType", SqlDbType.SmallInt,2),
                    new SqlParameter("@Status", SqlDbType.SmallInt,2),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDoDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@ReviewedUserID", SqlDbType.Int,4),
                    new SqlParameter("@ReviewedContent", SqlDbType.NVarChar,300),
                    new SqlParameter("@ReviewedDate", SqlDbType.DateTime),
                    new SqlParameter("@FileNames", SqlDbType.NVarChar,500),
                    new SqlParameter("@FileDataPath", SqlDbType.NVarChar,2000),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.EnterpriseID;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.ToUserId;
            parameters[6].Value = model.ToType;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.ParentId;
            parameters[9].Value = model.CreatedUserId;
            parameters[10].Value = model.CreatedDate;
            parameters[11].Value = model.ToDoDate;
            parameters[12].Value = model.ReviewedUserID;
            parameters[13].Value = model.ReviewedContent;
            parameters[14].Value = model.ReviewedDate;
            parameters[15].Value = model.FileNames;
            parameters[16].Value = model.FileDataPath;
            parameters[17].Value = model.Remark;
            parameters[18].Value = model.ID;

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
            strSql.Append("delete from JLT_ToDoInfo ");
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
            strSql.Append("delete from JLT_ToDoInfo ");
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
        public Maticsoft.Model.JLT.ToDoInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,EnterpriseID,UserId,UserName,Title,Content,ToUserId,ToType,Status,ParentId,CreatedUserId,CreatedDate,ToDoDate,ReviewedUserID,ReviewedContent,ReviewedDate,FileNames,FileDataPath,Remark from JLT_ToDoInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            Maticsoft.Model.JLT.ToDoInfo model = new Maticsoft.Model.JLT.ToDoInfo();
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
        public Maticsoft.Model.JLT.ToDoInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.JLT.ToDoInfo model = new Maticsoft.Model.JLT.ToDoInfo();
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
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Content"] != null)
                {
                    model.Content = row["Content"].ToString();
                }
                if (row["ToUserId"] != null)
                {
                    model.ToUserId = row["ToUserId"].ToString();
                }
                if (row["ToType"] != null && row["ToType"].ToString() != "")
                {
                    model.ToType = int.Parse(row["ToType"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["ParentId"] != null && row["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(row["ParentId"].ToString());
                }
                if (row["CreatedUserId"] != null && row["CreatedUserId"].ToString() != "")
                {
                    model.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["ToDoDate"] != null && row["ToDoDate"].ToString() != "")
                {
                    model.ToDoDate = DateTime.Parse(row["ToDoDate"].ToString());
                }
                if (row["ReviewedUserID"] != null && row["ReviewedUserID"].ToString() != "")
                {
                    model.ReviewedUserID = int.Parse(row["ReviewedUserID"].ToString());
                }
                if (row["ReviewedContent"] != null)
                {
                    model.ReviewedContent = row["ReviewedContent"].ToString();
                }
                if (row["ReviewedDate"] != null && row["ReviewedDate"].ToString() != "")
                {
                    model.ReviewedDate = DateTime.Parse(row["ReviewedDate"].ToString());
                }
                if (row["FileNames"] != null)
                {
                    model.FileNames = row["FileNames"].ToString();
                }
                if (row["FileDataPath"] != null)
                {
                    model.FileDataPath = row["FileDataPath"].ToString();
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
            strSql.Append("select ID,EnterpriseID,UserId,UserName,Title,Content,ToUserId,ToType,Status,ParentId,CreatedUserId,CreatedDate,ToDoDate,ReviewedUserID,ReviewedContent,ReviewedDate,FileNames,FileDataPath,Remark ");
            strSql.Append(" FROM JLT_ToDoInfo ");
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
            strSql.Append(" ID,EnterpriseID,UserId,UserName,Title,Content,ToUserId,ToType,Status,ParentId,CreatedUserId,CreatedDate,ToDoDate,ReviewedUserID,ReviewedContent,ReviewedDate,FileNames,FileDataPath,Remark ");
            strSql.Append(" FROM JLT_ToDoInfo ");
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
            strSql.Append("select count(1) FROM JLT_ToDoInfo ");
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
            strSql.Append(")AS Row, T.*  from JLT_ToDoInfo T ");
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
            parameters[0].Value = "JLT_ToDoInfo";
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
        /// 批量处理
        /// </summary>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_ToDoInfo set " + strWhere);
            strSql.Append(" where ID in(" + IDlist + ")  ");
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
        /// 批复待办信息
        /// </summary>
        public bool ReplyToDoInfo(string ids, string setUpdate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_ToDoInfo set ");
            strSql.Append(setUpdate);
            strSql.AppendFormat(" where ID IN ({0})", ids);
            return (DbHelperSQL.ExecuteSql(strSql.ToString()) > 0);
        }

        /// <summary>
        /// 分页获取数据列表
        /// 监理通定制功能, 对应接口:获取待办列表
        /// </summary>
        public DataSet GetListByPage4API(string strWhere, string orderby, int startIndex, int endIndex)
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
            //TODO: Sql性能优化
            strSql.Append(@")AS Row, 
      P.DoneCount
      , C.TotalCount
      , T.ID
      , T.EnterpriseID
      , T.UserId
      , T.UserName
      , T.Title
      , T.Content
      , T.ToUserId
      , T.ToType
      , T.Status
      , T.ParentId
      , T.CreatedUserId
      , T.CreatedDate
      , T.ToDoDate
      , T.ReviewedUserID
      , T.ReviewedContent
      , T.ReviewedDate
      , ISNULL(T.FileNames, F.FileNames) FileNames
      , ISNULL(T.FileDataPath, F.FileDataPath) FileDataPath
      , T.Remark
FROM    JLT_ToDoInfo T
        LEFT JOIN ( SELECT  COUNT(ParentId) DoneCount
                          , ParentId
                    FROM    JLT_ToDoInfo
                    WHERE   ParentId IS NOT NULL
                            AND Status <> 0
                    GROUP BY ParentId
                  ) P ON T.ID = P.ParentId
        LEFT JOIN ( SELECT  COUNT(ParentId) TotalCount
                          , ParentId
                    FROM    JLT_ToDoInfo
                    WHERE   ParentId IS NOT NULL
                    GROUP BY ParentId
                  ) C ON T.ID = C.ParentId
        LEFT JOIN ( SELECT  FileNames
                          , FileDataPath
                          , ID
                    FROM    JLT_ToDoInfo
                    WHERE   ParentId IS NULL
                  ) F ON T.ParentId = F.ID ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// 监理通定制功能, 对应接口:获取待办列表
        /// </summary>
        public DataSet GetListByPageEx4API(string strWhere, string orderby, int startIndex, int endIndex)
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
            //TODO: Sql性能优化 
            //TODO: DATALEANG Content 字段处理
            strSql.Append(@")AS Row, 
       T.ID
      , T.EnterpriseID
      , T.UserId
      , T.UserName
      , T.Title
      , F.Content
      , T.ToUserId
      , T.ToType
      , T.Status
      , T.ParentId
      , T.CreatedUserId
      , T.CreatedDate
      , T.ToDoDate
      , T.ReviewedUserID
      , T.ReviewedContent
      , T.ReviewedDate
      , F.FileNames
      , F.FileDataPath
      , T.Remark
FROM    JLT_ToDoInfo T
        LEFT JOIN ( SELECT  Content ,FileNames
                          , FileDataPath
                          , ID
                    FROM    JLT_ToDoInfo
                    WHERE   ParentId IS NULL
                  ) F ON T.ParentId = F.ID OR T.ID = F.ID");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod


        public bool Update(int id, string fileNames, string fileDataPath)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_ToDoInfo set ");
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
    }
}

