/**
* Class1.cs
*
* 功 能： [N/A]
* 类 名： Class1
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/3/1 19:32:29  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Data;

namespace Maticsoft.Accounts.IData
{
    internal interface IPermissionCategory
    {
        /// <summary>
        /// 创建权限类别
        /// </summary>        
        int Create(string description);

        /// <summary>
        /// 该类别下是否存在权限记录
        /// </summary>
        bool ExistsPerm(int CategoryID);

        /// <summary>
        /// 删除权限类别
        /// </summary>        
        bool Delete(int CategoryID);

        /// <summary>
        /// 获取权限类别信息
        /// </summary>        
        DataRow Retrieve(int categoryId);

        /// <summary>
        /// 获取指定类别下的权限列表
        /// </summary>        
        DataSet GetPermissionsInCategory(int categoryId);

        /// <summary>
        /// 获取权限类别的列表
        /// </summary>        
        DataSet GetCategoryList();
    }

}
