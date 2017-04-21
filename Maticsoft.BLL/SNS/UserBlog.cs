/**
* UserBlog.cs
*
* 功 能： N/A
* 类 名： UserBlog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/3 12:08:16   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
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
    /// UserBlog
    /// </summary>
    public partial class UserBlog
    {
        private readonly IUserBlog dal = DASNS.CreateUserBlog();
        public UserBlog()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BlogID)
        {
            return dal.Exists(BlogID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.UserBlog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.UserBlog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int BlogID)
        {

            return dal.Delete(BlogID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string BlogIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(BlogIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.UserBlog GetModel(int BlogID)
        {

            return dal.GetModel(BlogID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.UserBlog GetModelByCache(int BlogID)
        {

            string CacheKey = "UserBlogModel-" + BlogID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(BlogID);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.UserBlog)objModel;
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
        public List<Maticsoft.Model.SNS.UserBlog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.UserBlog> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserBlog> modelList = new List<Maticsoft.Model.SNS.UserBlog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.UserBlog model;
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
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.UserBlog> GetMoreList(int userId, int blogId, int top)
        {
            string strWhere = " Status=1 ";
            if (userId > 0)
            {
                strWhere += " and UserID=" + userId;
            }
            if (blogId > 0)
            {

                strWhere += " and BlogID<> " + blogId;
            }
            DataSet ds = dal.GetList(top, strWhere, " CreatedDate desc");
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.UserBlog> GetHotBlogList(int top)
        {
            string strWhere = " Status=1";
            DataSet ds = dal.GetList(top, strWhere, " PvCount desc");
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 更新PV数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdatePvCount(int id)
        {
            return dal.UpdatePvCount(id);
        }
        /// <summary>
        /// 更新喜欢数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateFavCount(int id)
        {
            return dal.UpdateFavCount(id);
        }
        /// <summary>
        /// 获取PV数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetPvCount(int id)
        {
            return dal.GetPvCount(id);
        }

        /// <summary>
        /// 更新评论数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateCommentCount(int id)
        {
            return dal.UpdateCommentCount(id);
        }

        #region 分页得到数据
        public List<Maticsoft.Model.SNS.UserBlog> GetUserBlogPage(string strWhere, string orderby, int StartIndex, int EndIndex)
        {
            List<Maticsoft.Model.SNS.UserBlog> list = new List<Model.SNS.UserBlog>();

            list = DataTableToList(GetListByPage(strWhere, orderby, StartIndex, EndIndex).Tables[0]);
            return list;
        }
        #endregion

        /// <summary>
        /// 获取博客活跃用户
        /// </summary>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserBlog> GetActiveUser(int top)
        {
            string CacheKey = "GetActiveUser-" + top;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    DataSet ds = dal.GetActiveUser(top);
                    List<Maticsoft.Model.SNS.UserBlog> modelList = new List<Maticsoft.Model.SNS.UserBlog>();
                    int rowsCount = ds.Tables[0].Rows.Count;
                    if (rowsCount > 0)
                    {
                        Maticsoft.Model.SNS.UserBlog model;
                        for (int n = 0; n < rowsCount; n++)
                        {
                            model = new Model.SNS.UserBlog();
                            DataRow row = ds.Tables[0].Rows[n];
                            if (row != null)
                            {
                                if (row["UserID"] != null && row["UserID"].ToString() != "")
                                {
                                    model.UserID = int.Parse(row["UserID"].ToString());
                                }
                                if (row["UserName"] != null)
                                {
                                    model.UserName = row["UserName"].ToString();
                                }
                            }
                            if (model != null)
                            {
                                modelList.Add(model);
                            }
                        }
                    }
                    objModel = modelList;
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.UserBlog>)objModel;

        }

        /// <summary>
        /// 删除一条数据(级联删除)
        /// </summary>
        public bool DeleteEx(int BlogID)
        {
            return dal.DeleteEx(BlogID);
        }

        public List<Maticsoft.Model.SNS.UserBlog> GetRecBlogList(int top)
        {
            string strWhere = " Status=1 and Recomend=1";
            DataSet ds = dal.GetList(top, strWhere, " PvCount desc");
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateStatusList(string ids, int Status)
        {
            return dal.UpdateStatusList(ids, Status);
        }
        /// <summary>
        /// 推荐状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Rec"></param>
        /// <returns></returns>
        public bool UpdateRec(int id, int Rec)
        {
            return dal.UpdateRec(id, Rec);
        }
        /// <summary>
        /// 批量更新推荐状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Rec"></param>
        /// <returns></returns>
        public bool UpdateRecList(string ids, int Rec)
        {
            return dal.UpdateRecList(ids, Rec);
        }

        /// <summary>
        /// 删除一条数据(级联删除)
        /// </summary>
        public bool DeleteListEx(string  BlogIDs)
        {
            return dal.DeleteListEx(BlogIDs);
        }
        #endregion  ExtensionMethod
    }
}

