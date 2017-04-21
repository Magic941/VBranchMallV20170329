/**
* ToDoInfo.cs
*
* 功 能： N/A
* 类 名： ToDoInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/28 19:02:44   N/A    初版
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
using Maticsoft.Model.JLT;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.JLT;
namespace Maticsoft.BLL.JLT
{
    /// <summary>
    /// ToDoInfo
    /// </summary>
    public partial class ToDoInfo
    {
        private readonly IToDoInfo dal = DAJLT.CreateToDoInfo();
        public ToDoInfo()
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
        public int Add(Maticsoft.Model.JLT.ToDoInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.JLT.ToDoInfo model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(int id,string fileNames,string fileDataPath)
        {
            return dal.Update(id, fileNames, fileDataPath);
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
        public Maticsoft.Model.JLT.ToDoInfo GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.JLT.ToDoInfo GetModelByCache(int ID)
        {

            string CacheKey = "ToDoInfoModel-" + ID;
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
            return (Maticsoft.Model.JLT.ToDoInfo)objModel;
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
        public List<Maticsoft.Model.JLT.ToDoInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.JLT.ToDoInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.JLT.ToDoInfo> modelList = new List<Maticsoft.Model.JLT.ToDoInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.JLT.ToDoInfo model;
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
        public List<Maticsoft.Model.JLT.ToDoInfo> GetModelListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
        /// 批复待办信息
        /// </summary>
        public bool ReplyToDoInfo(string ids, string setUpdate)
        {
            return dal.ReplyToDoInfo(ids, setUpdate);
        }

        /// <summary>
        /// 分页获取数据列表
        /// 监理通定制功能, 对应接口:获取待办列表
        /// </summary>
        public DataSet GetListByPage4API(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 分页获取数据列表
        /// 监理通定制功能, 对应接口:获取待办列表 - 此接口返回数据为主待办
        /// </summary>
        public List<Maticsoft.Model.JLT.ToDoInfo> GetListByPageEx4API(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPageEx4API(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.JLT.ToDoInfo GetModelEx(int ID)
        {
            DataSet ds = dal.GetListByPageEx4API("T.ID = " + ID, "", 1, 1);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return dal.DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        #endregion  ExtensionMethod
    }
}

