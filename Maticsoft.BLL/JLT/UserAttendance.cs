/**
* UserAttendance.cs
*
* 功 能： N/A
* 类 名： UserAttendance
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/20 16:07:41   N/A    初版
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
using System.Text;
using Maticsoft.Common;
using Maticsoft.Model.JLT;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.JLT;
namespace Maticsoft.BLL.JLT
{
    /// <summary>
    /// 考勤信息表
    /// </summary>
    public partial class UserAttendance
    {
        private readonly IUserAttendance dal = DAJLT.CreateUserAttendance();
        public UserAttendance()
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
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.JLT.UserAttendance model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.JLT.UserAttendance model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.JLT.UserAttendance GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.JLT.UserAttendance GetModelByCache(int ID)
        {

            string CacheKey = "UserAttendanceModel-" + ID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.JLT.UserAttendance)objModel;
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
        public List<Maticsoft.Model.JLT.UserAttendance> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.JLT.UserAttendance> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.JLT.UserAttendance> modelList = new List<Maticsoft.Model.JLT.UserAttendance>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.JLT.UserAttendance model;
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
        public List<Maticsoft.Model.JLT.UserAttendance> GetModelListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 批量处理
        /// </summary>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }

        /// <summary>
        /// 统计考勤
        /// </summary>
        /// <param name="currentUser">当前登录用户</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="reviewedStatus">批复状态</param>
        public DataSet Statistics(Maticsoft.Accounts.Bus.User currentUser, DateTime startDate, DateTime endDate, int reviewedStatus)
        {
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            if (currentUser.UserType == "UU")
            {
                strWhere.Append(" AND  UA.UserID in( SELECT UserID FROM Accounts_Users WHERE EmployeeID=" + currentUser.UserID + " or UserID=" + currentUser.UserID + ") ");
            }
            else if (currentUser.UserType == "EE")
            {
                strWhere.Append(" AND  UA.UserID in( SELECT UserID FROM Accounts_Users WHERE DepartmentID='" + currentUser.DepartmentID + "') ");
            }
            if (startDate > DateTime.MinValue && endDate > DateTime.MinValue)
            {
                strWhere.AppendFormat(" AND UA.AttendanceDate BETWEEN '{0}' AND '{1}'",
                    startDate.ToShortDateString(), endDate.ToShortDateString());
            }
            if (reviewedStatus > -1)
            {
                strWhere.AppendFormat(" AND UA.ReviewedStatus = {0}",
                    reviewedStatus);
            }
            return dal.Statistics(strWhere.ToString());
        }

        /// <summary>
        /// 获取用户考勤数据
        /// </summary>
        /// <param name="currentUser">当前登录用户</param>
        /// <param name="userName">用户名</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="reviewedStatus">批复状态</param>
        public DataSet GetCollectAttendance(Maticsoft.Accounts.Bus.User currentUser, string userName, DateTime startDate, DateTime endDate, int reviewedStatus)
        {
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            if (currentUser.UserType == "UU")
            {
                strWhere.Append(" AND UserID in( SELECT UserID FROM Accounts_Users WHERE EmployeeID=" + currentUser.UserID + " or UserID=" + currentUser.UserID + ") ");
            }
            else if (currentUser.UserType == "EE")
            {
                strWhere.Append(" AND UserID in( SELECT UserID FROM Accounts_Users WHERE DepartmentID='" + currentUser.DepartmentID + "') ");
            }
            if (!string.IsNullOrWhiteSpace(userName))
            {
                strWhere.AppendFormat(" AND UserName = '{0}'",
                    userName);
            }
            if (startDate > DateTime.MinValue && endDate > DateTime.MinValue)
            {
                strWhere.AppendFormat(" AND AttendanceDate BETWEEN '{0}' AND '{1}'",
                    startDate.ToShortDateString(), endDate.ToShortDateString());
            }
            if (reviewedStatus > -1)
            {
                strWhere.AppendFormat(" AND ReviewedStatus = {0}",
                    reviewedStatus);
            }
            return dal.GetCollectAttendance(strWhere.ToString());
        }

        #endregion  ExtensionMethod
    }
}

