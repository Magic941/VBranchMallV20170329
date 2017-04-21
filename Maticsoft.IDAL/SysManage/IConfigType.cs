using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Maticsoft.IDAL.SysManage
{
    public interface IConfigType
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string TypeName);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(string TypeName);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(int KeyType, string TypeName);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int KeyType);
        bool DeleteList(string KeyTypelist);
              
       /// <summary>
        /// 得到一个对象实体
        /// </summary>
        string GetTypeName(int KeyType);

        DataSet GetList(string strWhere);

        #endregion  成员方法
        #region  MethodEx

        #endregion  MethodEx
    } 


}
