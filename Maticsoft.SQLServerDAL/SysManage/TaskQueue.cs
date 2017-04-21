using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.SysManage;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.SysManage
{
    /// <summary>
    /// 数据访问类:TaskQueue
    /// </summary>
    public partial class TaskQueue : ITaskQueue
    {
        public TaskQueue()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "SA_TaskQueue");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID, int Type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SA_TaskQueue");
            strSql.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)			};
            parameters[0].Value = ID;
            parameters[1].Value = Type;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.SysManage.TaskQueue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_TaskQueue(");
            strSql.Append("ID,Type,TaskId,Status,RunDate)");
            strSql.Append(" values (");
            strSql.Append("@ID,@Type,@TaskId,@Status,@RunDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@RunDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.TaskId;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.RunDate;

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
        public bool Update(Maticsoft.Model.SysManage.TaskQueue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_TaskQueue set ");
            strSql.Append("TaskId=@TaskId,");
            strSql.Append("Status=@Status,");
            strSql.Append("RunDate=@RunDate");
            strSql.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@RunDate", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = model.TaskId;
            parameters[1].Value = model.Status;
            parameters[2].Value = model.RunDate;
            parameters[3].Value = model.ID;
            parameters[4].Value = model.Type;

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
        public bool Delete(int ID, int Type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_TaskQueue ");
            strSql.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)			};
            parameters[0].Value = ID;
            parameters[1].Value = Type;

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
        public Maticsoft.Model.SysManage.TaskQueue GetModel(int ID, int Type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Type,TaskId,Status,RunDate from SA_TaskQueue ");
            strSql.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)			};
            parameters[0].Value = ID;
            parameters[1].Value = Type;

            Maticsoft.Model.SysManage.TaskQueue model = new Maticsoft.Model.SysManage.TaskQueue();
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
        public Maticsoft.Model.SysManage.TaskQueue DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SysManage.TaskQueue model = new Maticsoft.Model.SysManage.TaskQueue();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["TaskId"] != null && row["TaskId"].ToString() != "")
                {
                    model.TaskId = int.Parse(row["TaskId"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["RunDate"] != null && row["RunDate"].ToString() != "")
                {
                    model.RunDate = DateTime.Parse(row["RunDate"].ToString());
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
            strSql.Append("select ID,Type,TaskId,Status,RunDate ");
            strSql.Append(" FROM SA_TaskQueue ");
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
            strSql.Append(" ID,Type,TaskId,Status,RunDate ");
            strSql.Append(" FROM SA_TaskQueue ");
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
            strSql.Append("select count(1) FROM SA_TaskQueue ");
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
                strSql.Append("order by T.Type desc");
            }
            strSql.Append(")AS Row, T.*  from SA_TaskQueue T ");
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
            parameters[0].Value = "SA_TaskQueue";
            parameters[1].Value = "Type";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public bool DeleteArticle()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_TaskQueue where type=0");
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

        public DataSet GetContinueTask(int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Type,TaskId,Status,RunDate ");
            strSql.Append(" FROM SA_TaskQueue ");
            strSql.Append(" where  type="+type+"  and Status=0 order by ID");
            return DbHelperSQL.Query(strSql.ToString());
        }

       public Maticsoft.Model.SysManage.TaskQueue GetLastModel(int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Type,TaskId,Status,RunDate from SA_TaskQueue ");
            strSql.Append(" where type=" + type + " and Status=1 order by ID desc ");

            Maticsoft.Model.SysManage.TaskQueue model = new Maticsoft.Model.SysManage.TaskQueue();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
        /// 删除指定类型任务列表 0： 表示文章，1：表示商品
        /// </summary>
        /// <returns></returns>
       public bool DeleteTask(int Type)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("delete from SA_TaskQueue where type=@Type");
           SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4)			};
           parameters[0].Value = Type;
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

