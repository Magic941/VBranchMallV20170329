using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SysManage;
namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 配置参数类别
    /// </summary>
    public partial class ConfigType
    {
        private readonly IConfigType dal = DASysManage.CreateConfigType();
       

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string TypeName)
        {
            return dal.Exists(TypeName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(string TypeName)
        {
            return dal.Add(TypeName);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(int KeyType, string TypeName)
        {
            return dal.Update(KeyType, TypeName);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int KeyType)
        {

            return dal.Delete(KeyType);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string KeyTypelist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(KeyTypelist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetTypeName(int KeyType)
        {

            return dal.GetTypeName(KeyType);
        }

       
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
       
   
   

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }


}
