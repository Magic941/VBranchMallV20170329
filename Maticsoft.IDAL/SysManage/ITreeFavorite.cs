using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.IDAL.SysManage
{
    public interface ITreeFavorite
    {
        int Add(int UserID, int NodeID);
        void Delete(int ID);
        void Delete(int UserID, int NodeID);
        void DeleteByUser(int UserID);
        bool Exists(int UserID, int NodeID);
        System.Data.DataSet GetList(int Top, string strWhere, string filedOrder);
        System.Data.DataSet GetList(string strWhere);
        System.Data.DataSet GetMenuListByUser(int UserID);
        System.Data.DataSet GetNodeIDsByUser(int UserID);
        void UpDate(int OrderID, int UserID, int NodeID);
    }
}
