/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：UsersApprove.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/10/25 15:36:34
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
using Maticsoft.Common;

namespace Maticsoft.BLL.Members
{
    /// <summary>
    /// 用户实名认证
    /// </summary>
    public partial class UsersApprove
    {
        private readonly IUsersApprove dal = DAMembers.CreateUsersApprove();

        public UsersApprove()
        { }

        #region Method

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
        public bool Exists(int ApproveID)
        {
            return dal.Exists(ApproveID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.UsersApprove model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.UsersApprove model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ApproveID)
        {
            return dal.Delete(ApproveID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ApproveIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ApproveIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.UsersApprove GetModel(int ApproveID)
        {
            return dal.GetModel(ApproveID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.UsersApprove GetModelByCache(int ApproveID)
        {
            string CacheKey = "UsersApproveModel-" + ApproveID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ApproveID);
                    if (objModel != null)
                    {
                      int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.UsersApprove)objModel;
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
        public List<Maticsoft.Model.Members.UsersApprove> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.UsersApprove> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UsersApprove> modelList = new List<Maticsoft.Model.Members.UsersApprove>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.UsersApprove model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Members.UsersApprove();
                    if (dt.Rows[n]["ApproveID"] != null && dt.Rows[n]["ApproveID"].ToString() != "")
                    {
                        model.ApproveID = int.Parse(dt.Rows[n]["ApproveID"].ToString());
                    }
                    if (dt.Rows[n]["UserID"] != null && dt.Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    }
                    if (dt.Rows[n]["TrueName"] != null && dt.Rows[n]["TrueName"].ToString() != "")
                    {
                        model.TrueName = dt.Rows[n]["TrueName"].ToString();
                    }
                    if (dt.Rows[n]["IDCardNum"] != null && dt.Rows[n]["IDCardNum"].ToString() != "")
                    {
                        model.IDCardNum = dt.Rows[n]["IDCardNum"].ToString();
                    }
                    if (dt.Rows[n]["FrontView"] != null && dt.Rows[n]["FrontView"].ToString() != "")
                    {
                        model.FrontView = dt.Rows[n]["FrontView"].ToString();
                    }
                    if (dt.Rows[n]["RearView"] != null && dt.Rows[n]["RearView"].ToString() != "")
                    {
                        model.RearView = dt.Rows[n]["RearView"].ToString();
                    }
                    if (dt.Rows[n]["DueDate"] != null && dt.Rows[n]["DueDate"].ToString() != "")
                    {
                        model.DueDate = DateTime.Parse(dt.Rows[n]["DueDate"].ToString());
                    }
                    if (dt.Rows[n]["Status"] != null && dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["ApproveUserID"] != null && dt.Rows[n]["ApproveUserID"].ToString() != "")
                    {
                        model.ApproveUserID = int.Parse(dt.Rows[n]["ApproveUserID"].ToString());
                    }
                    if (dt.Rows[n]["UserType"] != null && dt.Rows[n]["UserType"].ToString() != "")
                    {
                        model.UserType = int.Parse(dt.Rows[n]["UserType"].ToString());
                    }
                    if (dt.Rows[n]["CreatedDate"] != null && dt.Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
                    }
                    if (dt.Rows[n]["ApproveDate"] != null && dt.Rows[n]["ApproveDate"].ToString() != "")
                    {
                        model.ApproveDate = DateTime.Parse(dt.Rows[n]["ApproveDate"].ToString());
                    }
                    modelList.Add(model);
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

        #endregion Method

        #region ExMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetApproveList(string status, string trueName)
        {
            System.Text.StringBuilder strWhere = new System.Text.StringBuilder();
            if (!string.IsNullOrWhiteSpace(status) && !string.IsNullOrWhiteSpace(trueName))
            {
                strWhere.Append(" WHERE ");
                strWhere.AppendFormat(" Status ={0} AND UA.TrueName  like '%{1}%'", status, trueName);
            }
            else if (!string.IsNullOrWhiteSpace(status))
            {
                strWhere.Append(" WHERE ");
                strWhere.AppendFormat(" Status ={0} ", status);
            }
            else if (!string.IsNullOrWhiteSpace(trueName))
            {
                strWhere.Append(" WHERE ");
                strWhere.AppendFormat(" UA.TrueName  like '%{0}%'", trueName);
            }
            else
            {
            }
            return dal.GetApproveList(strWhere.ToString());
        }

        /// <summary>
        /// 批量更新认证信息
        /// </summary>
        /// <param name="ids">待更新的ID</param>
        /// <param name="status">更新状态</param>
        /// <returns>是否更新成功</returns>
        public bool BatchUpdate(string ids, string status)
        {
            return dal.BatchUpdate(ids, status);
        }

        public Maticsoft.Model.Members.UsersApprove GetModelByUserID(int UserID)
        {
            return dal.GetModelByUserID(UserID);
        }

        public bool DeleteByUserId(int userId)
        {
            return dal.DeleteByUserId(userId);
        }
        #endregion ExMethod
    }
}