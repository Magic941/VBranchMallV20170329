/**
* Groups.cs
*
* 功 能： N/A
* 类 名： Groups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:42   N/A    初版
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
    /// 群组
    /// </summary>
    public partial class Groups
    {
        private readonly IGroups dal = DASNS.CreateGroups();
        public Groups()
        { }
        #region  BasicMethod



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GroupName)
        {
            return dal.Exists(GroupName);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists4Ignore(string GroupName, int groupId)
        {
            return dal.Exists(GroupName, groupId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Groups model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Groups model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupID)
        {

            return dal.Delete(GroupID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string GroupIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(GroupIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Groups GetModel(int GroupID)
        {

            return dal.GetModel(GroupID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Groups GetModelByCache(int GroupID)
        {

            string CacheKey = "GroupsModel-" + GroupID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GroupID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.Groups)objModel;
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
        public List<Maticsoft.Model.SNS.Groups> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Groups> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Groups> modelList = new List<Maticsoft.Model.SNS.Groups>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Groups model;
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
        public List<Model.SNS.Groups> GetTopList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Groups> GetUserJoinGroup(int UserId, int Top)
        {

            return GetModelList(" GroupID IN (SELECT TOP " + Top + " GroupID FROM SNS_GroupUsers WHERE UserID =" + UserId + ")");

        }

        #region 搜索小组
        /// <summary>
        /// 搜索相关的小组
        /// </summary>
        public List<Maticsoft.Model.SNS.Groups> GetGroupListByKeyWord(string q)
        {
            return GetModelList("GroupName like '%" + q + "%'");
        }
        /// <summary>
        /// 搜索小组分页
        /// </summary>
        public List<Maticsoft.Model.SNS.Groups> GetGroupListByKeyWord(int startIndex, int endIndex, string Sequence, string q, int rec = -1)
        {
            string strWhere = "";
            if (rec != -1)
            {
                strWhere = " IsRecommand=" + rec;
            }
            if (!String.IsNullOrWhiteSpace(q))
            {
                if (!String.IsNullOrWhiteSpace(strWhere))
                {
                    strWhere += " and ";
                }
                strWhere += " (GroupName like '%" + q + "%' or GroupDescription Like '%" + q + "%' or Tags like '%" + q +
                            "%')";
            }
            return DataTableToList(GetListByPage(strWhere, "", startIndex, endIndex).Tables[0]);
        }
        /// <summary>
        ///搜索小组相应的数量
        /// </summary>
        public int GetCountByKeyWord(string q, int rec = -1)
        {
            string strWhere = "";
            if (rec != -1)
            {
                strWhere = " IsRecommand=" + rec;
            }
            if (!String.IsNullOrWhiteSpace(q))
            {
                if (!String.IsNullOrWhiteSpace(strWhere))
                {
                    strWhere += " and ";
                }
                strWhere += " (GroupName like '%" + q + "%' or GroupDescription Like '%" + q + "%' or Tags like '%" + q +
                            "%')";
            }
            return GetRecordCount(strWhere);
        }

        /// <summary>
        /// 管理员推荐到首页的小组
        /// </summary>
        /// <param name="Top"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Groups> GetGroupListByRecommendType(int Top, Maticsoft.Model.SNS.EnumHelper.GroupRecommend Type)
        {
            return DataTableToList(GetListByPage("IsRecommand=" + (int)Type + "", "", 0, Top).Tables[0]);
        }
        /// <summary>
        /// 最活跃的小组（咦话题数为根据）
        /// </summary>
        /// <param name="Top"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Groups> GetHotGroupList(int Top)
        {
            return DataTableToList(GetListByPage("", "TopicCount Desc", 0, Top).Tables[0]);
        }
        #endregion

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="IdsStr">id集合</param>
        /// <param name="status">要更改的状态</param>
        /// <returns></returns>
        public bool UpdateStatusList(string IdsStr, Maticsoft.Model.SNS.EnumHelper.GroupStatus status)
        {
            return dal.UpdateStatusList(IdsStr, status);
        }
        /// <summary>
        /// 删除小组和小组下面的话题和回复
        /// </summary>
        public bool DeleteListEx(string GroupIDlist)
        {
            return dal.DeleteListEx(GroupIDlist);
        }

        /// <summary>
        /// 更新小组推荐的状态
        /// </summary>
        public bool UpdateRecommand(int GroupId, int Recommand)
        {
            return dal.UpdateRecommand(GroupId, Recommand);
        }

        #endregion  ExtensionMethod
    }
}

