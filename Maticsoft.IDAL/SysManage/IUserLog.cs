using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.IDAL.SysManage
{
    public interface IUserLog
    {
        System.Data.DataSet GetList(string strWhere);
        void LogDelete(DateTime dtDateBefore);
        void LogUserAdd(Maticsoft.Model.SysManage.UserLog model);
        void LogUserDelete(DateTime dtDateBefore);
        void LogUserDelete(int ID);
        void LogUserDelete(string IdList);

        int GetCount(string strWhere);
    }
}
