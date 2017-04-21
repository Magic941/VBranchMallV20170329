using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.IDAL.SysManage
{
    public interface IMultiLanguage
    {
        int Add(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang, string MultiLang_cValue);
        void Delete(int ID);
        bool Exists(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang);
        System.Data.DataSet GetLanguageList();
        string GetLanguageName(string Language_cCode);
        string GetDefaultLangCode();
        System.Data.DataSet GetList(int Top, string strWhere, string filedOrder);
        System.Data.DataSet GetValueListByLang(string MultiLang_cField, string MultiLang_cLang);
        System.Data.DataSet GetList(string strWhere);
        System.Data.DataSet GetLangListByValue(string MultiLang_cField, int MultiLang_iPKValue);
        string GetModel(int ID);
        string GetModel(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang);
        void Update(int ID, string MultiLang_cValue);
    }
}
