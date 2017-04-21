using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class  DAShopAMDetail: DataAccessBase
    {
        /// <summary>
        /// 创建AM数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.ActivityManage.IAMDetail CreateAMD()
        {
            string ClassNamespace = AssemblyPath + ".Shop.ActivityManage.AMDetail";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.ActivityManage.IAMDetail)objType;
        }
    }
}
