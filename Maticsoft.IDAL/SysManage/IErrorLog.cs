using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.IDAL.SysManage
{
    public interface IErrorLog
    {
        int Add(Maticsoft.Model.SysManage.ErrorLog model);
        void Delete(int ID);
        void Delete(string IDList);
        /// <summary>
        /// 删除某一日期之前的数据
        /// </summary>
        /// <param name="dtDateBefore">日期</param>
        void DeleteByDate(DateTime dtDateBefore);
        System.Data.DataSet GetList(int Top, string strWhere, string filedOrder);
        System.Data.DataSet GetList(string strWhere);
        Maticsoft.Model.SysManage.ErrorLog GetModel(int ID);
        void Update(Maticsoft.Model.SysManage.ErrorLog model);
    }
}
