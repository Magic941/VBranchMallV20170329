using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SysManage
{

    public class TreeFavorite : Maticsoft.IDAL.SysManage.ITreeFavorite
    {

        public TreeFavorite()
        { }


        #region  Method

        /// <summary>
        /// Whether there is Exists
        /// </summary>
        public bool Exists(int UserID, int NodeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SA_TreeFavorite");
            strSql.Append(" where UserID=@UserID and  NodeID=@NodeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@NodeID", SqlDbType.Int,4)
					};
            parameters[0].Value = UserID;
            parameters[1].Value = NodeID;
           
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// Add a record
        /// </summary>
        public int Add(int UserID, int NodeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_TreeFavorite(");
            strSql.Append("UserID,NodeID,CreatDate)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@NodeID,@CreatDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@NodeID", SqlDbType.Int,4),
					new SqlParameter("@CreatDate", SqlDbType.DateTime)};
            parameters[0].Value = UserID;
            parameters[1].Value = NodeID;
            parameters[2].Value = DateTime.Now;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        ///<summary>
        /// 更新一个OrderID
        /// </summary>
        public void UpDate(int OrderID, int UserID , int NodeID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("UPDATE SA_TreeFavorite SET OrderID = @OrderID where UserID =@UserID and NodeID=@NodeID");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@NodeID",SqlDbType.Int,4)};
            parameters[0].Value = OrderID;
            parameters[1].Value = UserID;
            parameters[2].Value = NodeID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        
        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_TreeFavorite ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void DeleteByUser(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_TreeFavorite ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)					
					};
            parameters[0].Value = UserID;            
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int UserID, int NodeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_TreeFavorite ");
            strSql.Append(" where UserID=@UserID and  NodeID=@NodeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@NodeID", SqlDbType.Int,4)
					};
            parameters[0].Value = UserID;
            parameters[1].Value = NodeID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
           
        /// <summary>
        /// Query data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserID,NodeID,CreatDate ");
            strSql.Append(" FROM SA_TreeFavorite ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetNodeIDsByUser(int UserID)
        {
            StringBuilder strSql = new StringBuilder();          
            strSql.Append("select NodeID ");
            strSql.Append(" FROM SA_TreeFavorite ");
            if (UserID > 0)
            {
                strSql.Append(" where UserID=" + UserID);
            }
            
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetMenuListByUser(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SA_TreeFavorite.*,SA_Tree.TreeText,SA_Tree.Url from SA_TreeFavorite  ");
            strSql.Append(" left join SA_Tree on SA_TreeFavorite.NodeID=SA_Tree.NodeID ");            
            if (UserID>0)
            {
                strSql.Append(" where UserID=" + UserID);
            }           
            return DbHelperSQL.Query(strSql.ToString());
        }
        

        /// <summary>
        /// Query top lines of data 
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,UserID,NodeID,CreatDate ");
            strSql.Append(" FROM SA_TreeFavorite ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

       

        #endregion  Method
    }

}
