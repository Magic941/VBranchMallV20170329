using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.IDAL.Team;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace Maticsoft.SQLServerDAL.Team
{
    public class SalesInfo : ISalesInfo
    {
        #region 龙虎榜
        /// <summary>
        /// 获取最新批次的龙虎榜（粉丝或业绩排行，根据传入参数决定）
        /// </summary>
        /// <param name="BillboardType">排行榜类型  1=周粉丝榜  2=月粉丝榜  3=周收入榜  4=月收入榜</param>
        /// <returns></returns>
        public DataTable GetBillboard(int BillboardType)
        {
            string str_Sql = " select * from TeamSYS.dbo.SalesPersonBillboard where BatchNo in(select top 1 BatchNo from TeamSYS.dbo.SalesPersonBillboard where BillboardType=" + BillboardType + " Order by CreatedDate Desc)  order by Num asc ";
            return DbHelperSQL.Query(str_Sql).Tables[0];
        }
        #endregion
    }
}
