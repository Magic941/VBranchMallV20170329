/**
* Class1.cs
*
* 功 能： [N/A]
* 类 名： Class1
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/3/1 19:31:47  Ben    初版
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

    internal interface IPermission
    {
        /// <summary>
        /// 创建一个权限
        /// </summary>
        int Create(int categoryID, string description);

        /// <summary>
        /// 更新权限信息
        /// </summary>
        bool Update(int PermissionID, string description);

        void UpdateCategory(string PermissionIDlist, int CategoryID);

        /// <summary>
        /// 删除权限
        /// </summary>        
        bool Delete(int id);

        /// <summary>
        /// 根据权限ID获取权限信息
        /// </summary>
        DataSet Retrieve(int permissionId);

        /// <summary>
        /// 获取权限列表
        /// </summary>        
        DataSet GetPermissionList();

        /// <summary>
        /// 获取指定角色的权限列表
        /// </summary>        
        DataSet GetPermissionList(int roleId);

        /// <summary>
        /// 获取指定角色没有的权限列表
        /// </summary>        
        DataSet GetNoPermissionList(int roleId);
    }
}
