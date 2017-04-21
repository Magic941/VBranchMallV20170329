/**
* Class1.cs
*
* 功 能： [N/A]
* 类 名： Class1
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/3/1 19:33:20  Ben    初版
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
    internal interface IRole
    {
        /// <summary>
        /// 是否存在该角色
        /// </summary>
        bool RoleExists(string Description);

        /// <summary>
        /// 增加角色
        /// </summary>       
        int Create(string description);

        /// <summary>
        /// 更新角色信息
        /// </summary>
        bool Update(int roleId, string description);

        /// <summary>
        /// 删除角色
        /// </summary>
        bool Delete(int roleId);

        /// <summary>
        /// 根据角色ID获取角色的信息
        /// </summary>
        DataRow Retrieve(int roleId);

        /// <summary>
        /// 为角色增加权限
        /// </summary>
        void AddPermission(int roleId, int permissionId);

        /// <summary>
        /// 从角色移除权限
        /// </summary>
        void RemovePermission(int roleId, int permissionId);

        /// <summary>
        /// 清空角色的权限
        /// </summary>
        void ClearPermissions(int roleId);

        /// <summary>
        /// 获取所有角色的列表
        /// </summary>
        DataSet GetRoleList();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetRoleList(string idlist);
    }
}
