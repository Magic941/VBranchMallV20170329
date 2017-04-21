/**  版本信息模板在安装目录下，可自行修改。
* PointsAction.cs
*
* 功 能： N/A
* 类 名： PointsAction
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/16 11:09:25   N/A    初版
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
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
using System.Linq;
namespace Maticsoft.BLL.Members
{
	/// <summary>
	/// PointsAction
	/// </summary>
	public partial class PointsAction
	{
		private readonly IPointsAction dal=DAMembers.CreatePointsAction();
		public PointsAction()
		{}

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
        public bool Exists(int ActionId)
        {
            return dal.Exists(ActionId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.PointsAction model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.PointsAction model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ActionId)
        {

            return dal.Delete(ActionId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ActionIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ActionIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.PointsAction GetModel(int ActionId)
        {

            return dal.GetModel(ActionId);
        }
       
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.PointsAction GetModelByCache(int ActionId)
        {

            string CacheKey = "PointsActionModel-" + ActionId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ActionId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.PointsAction)objModel;
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
        public List<Maticsoft.Model.Members.PointsAction> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.PointsAction> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.PointsAction> modelList = new List<Maticsoft.Model.Members.PointsAction>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.PointsAction model;
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
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.PointsAction> GetAllActionList()
        {
            DataSet ds = dal.GetList("");
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获取所有操作
        /// </summary>
        /// <returns></returns>
        public static List<Maticsoft.Model.Members.PointsAction> GetAllAction()
        {
            string CacheKey = "GetAllActionList";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.BLL.Members.PointsAction actionBll = new PointsAction();
                    objModel = actionBll.GetAllActionList();
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Members.PointsAction>)objModel;
        }
        /// <summary>
        /// 获取某一操作
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public static Maticsoft.Model.Members.PointsAction GetAction(int actionId)
        {
            List<Maticsoft.Model.Members.PointsAction> AllAction = GetAllAction();
            return AllAction.FirstOrDefault(c => c.ActionId == actionId);
        }

		#endregion  ExtensionMethod
	}
}

