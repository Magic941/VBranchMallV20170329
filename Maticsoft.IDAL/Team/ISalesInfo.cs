using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Maticsoft.Model.Team;

namespace Maticsoft.IDAL.Team
{
    public interface ISalesInfo
    {
        #region 龙虎榜
        /// <summary>
        /// 获取最新批次的龙虎榜（粉丝或业绩排行，根据传入参数决定）
        /// </summary>
       /// <param name="BillboardType">排行榜类型  1=周粉丝榜  2=月粉丝榜  3=周收入榜  4=月收入榜</param>
        /// <returns></returns>
       DataTable GetBillboard(int BillboardType);
        #endregion
    }
}
