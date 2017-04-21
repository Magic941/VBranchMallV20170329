/**
* Class1.cs
*
* 功 能： [N/A]
* 类 名： Class1
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/3/1 19:35:08  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Maticsoft.Accounts.IData
{
    internal interface IUser
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        int Create(string userName,
                                   byte[] password,
                                   string nickName,
                                   string trueName,
                                   string sex,
                                   string phone,
                                   string email,
                                   int employeeID,
                                   string departmentID,
                                   bool activity,
                                   string userType,
                                   int style,
                                   int User_iCreator,
                                   DateTime User_dateValid,
                                   string User_cLang
            );
        /// <summary>
        /// 创建用户
        /// </summary>
        int Create(string userName,
                                   byte[] password,
                                   string nickName,
                                   string trueName,
                                   string sex,
                                   string phone,
                                   string email,
                                   int employeeID,
                                   string departmentID,
                                   bool activity,
                                   string userType,
                                   int style,
                                   int User_iCreator,
                                   DateTime User_dateCreate,
                                   DateTime User_dateValid,
                                   string User_cLang
            );

        /// <summary>
        /// 根据UserID查询用户详细信息
        /// </summary>
        DataRow Retrieve(int userID);

        /// <summary>
        /// 根据UserName查询用户详细信息
        /// </summary>
        DataRow Retrieve(string userName);

        /// <summary>
        /// 根据NickName查询用户详细信息
        /// </summary>
        DataRow RetrieveByNickName(string nickName);

        /// <summary>
        /// 用户名是否已经存在
        /// </summary>
        [Obsolete]
        bool HasUser(string userName);

        /// <summary>
        /// 用户名是否已经存在
        /// </summary>
        bool HasUserByUserName(string userName);

        /// <summary>
        /// 邮箱是否已经存在
        /// </summary>
        bool HasUserByEmail(string email);

        /// <summary>
        /// 昵称是否已经存在
        /// </summary>
        bool HasUserByNickName(string nickName);

        /// <summary>
        /// 手机是否已经存在
        /// </summary>
        bool HasUserByPhone(string phone);

        /// <summary>
        /// 手机是否已经存在
        /// </summary>
        bool HasUserByPhone(string phone, string userType);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        bool Update(int userID,
                                    string userName,
                                    byte[] password,
                                    string nickName,
                                    string trueName,
                                    string sex,
                                    string phone,
                                    string email,
                                    int employeeID,
                                    string departmentID,
                                    bool activity,
                                    string userType,
                                    int style
            );

        /// <summary>
        /// 设置部门和员工编号
        /// </summary>
        bool Update(int UserID, int EmployeeID, string DepartmentID);

        /// <summary>
        /// 设置审核
        /// </summary>
        bool Update(int UserID, int User_iApprover, int User_iApproveState);

        /// <summary>
        /// 设置用户密码
        /// </summary>
        bool SetPassword(string UserName, byte[] encPassword);

        /// <summary>
        /// 删除用户
        /// </summary>
        bool Delete(int userID);

        /// <summary>
        /// 验证用户登录信息
        /// 用户名登录
        /// </summary>
        int ValidateLogin(string userName, byte[] encPassword);

        /// <summary>
        /// 验证用户登录信息
        /// 邮箱登录
        /// </summary>
        int ValidateLogin4Email(string email, byte[] encPassword);

        /// <summary>
        /// 测试用户密码
        /// </summary>
        int TestPassword(int userID, byte[] encPassword);

        /// <summary>
        /// 根据关键字查询用户
        /// </summary>
        DataSet GetUserList(string key);

        /// <summary>
        /// 根据用户类型和关键字查询用户信息
        /// </summary>
        DataSet GetUsersByType(string UserType, string Key);

        /// <summary>
        /// 根据部门和关键字查询用户信息
        /// </summary>
        DataSet GetUsersByDepart(string DepartmentID, string Key);

        /// <summary>
        /// 根据用户类型，部门，关键字查询用户
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="DepartmentID"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        DataSet GetUserList(string UserType, string DepartmentID, string Key);

        /// <summary>
        /// 根据员工编号，获取员工编号的用户信息
        /// </summary>        
        /// <param name="EmployeeID">员工编号</param>        
        /// <returns></returns>
        DataSet GetUsersByEmp(int EmployeeID);

        /// <summary>
        /// 获取某角色下的所有用户
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        DataSet GetUsersByRole(int RoleID);

        /// <summary>
        /// 获取用户的角色信息
        /// </summary>
        [Obsolete]
        ArrayList GetUserRoles(int userID);

        /// <summary>
        /// 获取用户的角色信息
        /// </summary>
        Dictionary<int, string> GetUserRoles4KeyValues(int userID);

        /// <summary>
        /// 获取用户有效的权限列表数据集
        /// </summary>
        DataSet GetEffectivePermissionLists(int userID);

        /// <summary>
        /// 获取用户有效的权限名称列表
        /// </summary>
        ArrayList GetEffectivePermissionList(int userID);

        /// <summary>
        /// 获取用户有效的权限ID列表
        /// </summary>
        ArrayList GetEffectivePermissionListID(int userID);

        /// <summary>
        /// 为用户增加角色
        /// </summary>
        bool AddRole(int userId, int roleId);

        /// <summary>
        /// 从用户移除角色
        /// </summary>
        bool RemoveRole(int userId, int roleId);

        /// <summary>
        /// 要分配是否存在该记录
        /// </summary>
        bool AssignRoleExists(int UserID, int RoleID);

        /// <summary>
        /// 增加一条关联数据
        /// </summary>
        void AddAssignRole(int UserID, int RoleID);

        /// <summary>
        /// 删除一条关联数据
        /// </summary>
        void DeleteAssignRole(int UserID, int RoleID);

        /// <summary>
        /// 获取用户分配的角色列表
        /// </summary>
        DataSet GetAssignRolesByUser(int UserID);

        /// <summary>
        /// 获取用户的未分配的角色列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        DataSet GetNoAssignRolesByUser(int UserID);

        /// <summary>
        /// 更新用户状态
        /// </summary>
        bool UpdateActivity(int userId, bool activity);
    }

}
