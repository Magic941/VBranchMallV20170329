using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Team;
using Maticsoft.Model.Team;

namespace Maticsoft.BLL.Team
{
    public class SalesInfo
    {
        private readonly ISalesInfo dal = DATeam.CreateSalesInfo();

        #region 龙虎榜
        /// <summary>
        /// 获取最新批次的龙虎榜（粉丝或业绩排行，根据传入参数决定）
        /// </summary>
        /// <param name="BillboardType">排行榜类型  1=周粉丝榜  2=月粉丝榜  3=周收入榜  4=月收入榜</param>
        /// <returns></returns>
        public DataTable GetBillboard(int BillboardType)
        {
            return dal.GetBillboard(BillboardType);
        }
        #endregion
    }
}
