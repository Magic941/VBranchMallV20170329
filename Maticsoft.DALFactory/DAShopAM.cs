using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class DAShopAM : DataAccessBase
    {
        /// <summary>
        /// 创建AM数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.ActivityManage.IAM CreateAM()
        {
            string ClassNamespace = AssemblyPath + ".Shop.ActivityManage.AM";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.ActivityManage.IAM)objType;
        }
    }
}
