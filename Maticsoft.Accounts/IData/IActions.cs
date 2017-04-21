/**
* Class1.cs
*
* 功 能： [N/A]
* 类 名： Class1
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/3/1 19:28:49  Ben    初版
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
    internal interface IActions
    {
        /// <summary>
        /// Whether Exists
        /// </summary>
        bool Exists(string Description);

        /// <summary>
        /// Add a record
        /// </summary>
        int Add(string Description);

        /// <summary>
        /// Add a record,include perimission
        /// </summary>
        int Add(string Description, int PermissionID);

        /// <summary>
        /// Update a record
        /// </summary>
        void Update(int ActionID, string Description);

        /// <summary>
        /// Update a record, include permission
        /// </summary>
        void Update(int ActionID, string Description, int PermissionID);

        /// <summary>
        /// Delete a record
        /// </summary>
        void Delete(int ActionID);

        /// <summary>
        /// Get Description
        /// </summary>
        string GetDescription(int ActionID);

        /// <summary>
        /// Query data list
        /// </summary>
        DataSet GetList(string strWhere);

        void AddPermission(int ActionID, int PermissionID);

        /// <summary>
        /// 批量增加权限设置
        /// </summary>
        /// <param name="ActionIDs"></param>
        /// <param name="PermissionID"></param>
        void AddPermission(string ActionIDs, int PermissionID);

        void ClearPermissions(int ActionID);
    }
}
