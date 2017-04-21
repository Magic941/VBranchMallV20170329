using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace Maticsoft.SQLServerDAL.SysManage
{
    
    /// <summary>
    /// 用户日志的操作类
    /// </summary>
    public class UserLog : Maticsoft.IDAL.SysManage.IUserLog
    {
                
        /// <summary>
        /// 增加日志
        /// </summary>
        /// <param name="model"></param>
        public void LogUserAdd(Maticsoft.Model.SysManage.UserLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_UserLog(");
            strSql.Append("OPTime,Url,OPInfo,UserName,UserType,UserIP)");
            strSql.Append(" values (");
            strSql.Append("@OPTime,@Url,@OPInfo,@UserName,@UserType,@UserIP)");
            SqlParameter[] parameters = {
					new SqlParameter("@OPTime", SqlDbType.DateTime),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@OPInfo", SqlDbType.NVarChar),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserType", SqlDbType.Char,2),
                    new SqlParameter("@UserIP", SqlDbType.NVarChar,20)};
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = model.Url;
            parameters[2].Value = model.OPInfo;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.UserType;
            parameters[5].Value = model.UserIP;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
                    
        /// <summary>
        /// 根据查询条件获取日志列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回的数据集</returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[OPTime],[url],[OPInfo],[UserName],[UserType],[UserIp] ");
            strSql.Append(" FROM SA_UserLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + " Order By [OPTime] Desc ");
            }
            else
            {
                strSql.Append(" Order By [OPTime] Desc ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 删除一条日志记录
        /// </summary>
        /// <param name="iID">要删除的日志编号</param>
        public void LogUserDelete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SA_UserLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        public void LogUserDelete(string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SA_UserLog ");
            strSql.Append(" where ID in("+IdList+")");            
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除某一日期之前的数据
        /// </summary>
        /// <param name="dtDateBefore">日期</param>
        public void LogUserDelete(DateTime dtDateBefore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SA_UserLog ");
            strSql.Append(" where OPTime <= @OPTime");
            SqlParameter[] parameters = {
					new SqlParameter("@OPTime", SqlDbType.DateTime)
				};
            parameters[0].Value = dtDateBefore;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 删除某一日期之前的数据用存储过程
        /// </summary>
        /// <param name="dtDateBefore">日期</param>
        public void LogDelete(DateTime dtDateBefore)
        {
            SqlParameter[] parameters = { new SqlParameter("@OPTime", SqlDbType.DateTime), };

            parameters[0].Value = dtDateBefore;
            DbHelperSQL.RunProcedure("sp_LogUser_delete", parameters);

        }


        /// <summary>
        /// 得到要查询的数据的总数
        /// </summary>
        /// <param name="strTable">要查询的表</param>
        /// <param name="strWhere">查询的条件,如果没有条件填1=1</param>
        /// <returns>返回的记录总数</returns>
        public int GetCount( string strWhere)
        {
            string strCmd = "select count(*) from  SA_UserLog   where  " + strWhere;
            int iResult = Convert.ToInt32(DbHelperSQL.GetSingle(strCmd));
            return iResult;
        }

        
    }
   


}
