using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.IDAL
{
    public interface IMailConfig
    {
        int Add(Maticsoft.Model.MailConfig model);
        void Delete(int ID);
        bool Exists(int UserID, string Mailaddress);
        System.Data.DataSet GetList(int Top, string strWhere, string filedOrder);
        System.Data.DataSet GetList(string strWhere);
        Maticsoft.Model.MailConfig GetModel(int ID);
        Maticsoft.Model.MailConfig GetModel();
        Maticsoft.Model.MailConfig GetModel(int? userId);
        void Update(Maticsoft.Model.MailConfig model);
    }
}
