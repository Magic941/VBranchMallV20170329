using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Accounts.Bus;
using System.Data;

namespace Maticsoft.IDAL.Team
{
    public interface ITeamB2CComm
    {
        /// <summary>
        /// 新增用户，弥补原有用户无法新增身份照的缺点
        /// </summary>
        /// <param name="Model">用户实体</param>
        /// <param name="CardId">身份证号</param>
        /// <returns></returns>
        int AddUser(User Model, string CardId);

        /// <summary>
        ///查询用户
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        DataTable GetUserList(string where);
    }
}
