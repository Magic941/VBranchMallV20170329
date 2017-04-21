/**
* GroupUsers.cs
*
* 功 能： N/A
* 类 名： GroupUsers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:44   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 小组人员
    /// </summary>
    public partial class GroupUsers
    {
        private readonly IGroupUsers dal = DASNS.CreateGroupUsers();
        public GroupUsers()
        { }
        #region  BasicMethod



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GroupID, int UserID)
        {
            return dal.Exists(GroupID, UserID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.SNS.GroupUsers model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.GroupUsers model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupID, int UserID)
        {

            return dal.Delete(GroupID, UserID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.GroupUsers GetModel(int GroupID, int UserID)
        {

            return dal.GetModel(GroupID, UserID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.GroupUsers GetModelByCache(int GroupID, int UserID)
        {

            string CacheKey = "GroupUsersModel-" + GroupID + UserID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GroupID, UserID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.GroupUsers)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.GroupUsers> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.GroupUsers> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GroupUsers> modelList = new List<Maticsoft.Model.SNS.GroupUsers>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.GroupUsers model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 更新用户状态
        /// </summary>
        public bool UpdateStatus(int GroupID, int UserID, int Status)
        {
            return dal.UpdateStatus(GroupID, UserID, Status);
        }
        /// <summary>
        /// 更新用户的权限
        /// </summary>
        public bool UpdateRole(int GroupID, int UserID, int Role)
        {
            return dal.UpdateRole(GroupID, UserID, Role);
        }


        public List<Maticsoft.Model.SNS.GroupUsers> GetNewUserListByGroup(int GroupId, int Top)
        {
            return DataTableToList(GetListByPage("GroupID=" + GroupId + "", "JoinTime Desc", 0, Top).Tables[0]);

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddEx(Maticsoft.Model.SNS.GroupUsers model)
        {
            return dal.AddEx(model);

        }



        /// <summary>
        /// 获取管理员用户
        /// </summary>
        /// <param name="GroupId">小组ID</param>
        public List<Maticsoft.Model.SNS.GroupUsers> GetAdminUserList(int GroupId)
        {
            return DataTableToList(GetList(-1, "GroupID=" + GroupId + "AND Role in (2,1)", "Role desc,JoinTime desc").Tables[0]);
        }
        /// <summary>
        /// 分页获取非管理员用户
        /// </summary>
        /// <param name="GroupId">小组ID</param>
        public List<Maticsoft.Model.SNS.GroupUsers> GetUserList(int GroupId, int startIndex, int endIndex)
        {
            return DataTableToList(GetListByPage("GroupID=" + GroupId + "AND Role = 0", "Role desc,JoinTime desc", startIndex, endIndex).Tables[0]);
        }

        public bool UpdateRecommand(int GroupID, int UserID, int Recommand)
        {
            return dal.UpdateRecommand(GroupID,UserID,Recommand);
        }

        /// <summary>
        /// 删除小组成员
        /// </summary>
        /// <param name="GroupId">小组id</param>
        /// <param name="UserID">成员id</param>
        /// <returns></returns>
        public bool DeleteEx(int GroupId,int UserID)
        { 
            
         return dal.DeleteEx( GroupId, UserID);
         
        }  
        /// <summary>
        /// 批量删除小组成员
        /// </summary>
        /// <param name="GroupId">小组ids</param>
        /// <param name="UserID">成员id</param>
        /// <returns></returns>
        public bool DeleteEx(int GroupId,string UserIDs)
        { 
            
         return dal.DeleteEx( GroupId, UserIDs);
         
        }

        /// <summary>
        /// 根据帖子对用户禁言
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateStatusByTopicIds(string Ids,int Status)
        {

            return dal.UpdateStatusByTopicIds(Ids,Status);
        
        }

        /// <summary>
        /// 根据帖子回复对用户禁言
        /// </summary>
        public bool UpdateStatusByTopicReplyIds(string Ids, int Status)
        {
            return dal.UpdateStatusByTopicReplyIds(Ids, Status);
        }
        #endregion
    }
    }



