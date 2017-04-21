using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL
{
    /// <summary>
    /// DAL公共类
    /// </summary>
    public class DALCommon
    {

        /// <summary>
        /// 设置数据表字段有效和无效
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="FieldName">bvalid的字段名</param>
        /// <param name="bvalid">1为有效，0为无效</param>
        /// <param name="FielddateExpire">设置无效的时间字段</param>
        /// <param name="WhereField">条件字段</param>
        /// <param name="WhereFieldValueList">批量条件字符串，如: 1,2,3,4 或 '1','2','3'</param>
        public static void SetValid(string TableName, string FieldName, int bvalid,string FielddateExpire,string WhereField, string WhereFieldValueList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", TableName);
            strSql.AppendFormat(" {0}={1}", FieldName, bvalid);
            if (FielddateExpire.Length > 0)
            {
                strSql.AppendFormat(" ,{0}='{1}'", FielddateExpire, DateTime.Now);
            }
            strSql.AppendFormat(" where {0} in ({1}) ", WhereField, WhereFieldValueList);            
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
       
    }
}
