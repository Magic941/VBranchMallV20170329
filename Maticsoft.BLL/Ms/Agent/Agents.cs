/**
* Agents.cs
*
* 功 能： N/A
* 类 名： Agents
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/28 18:17:12   Ben    初版
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
using Maticsoft.Model.Ms.Agent;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Ms.Agent;
namespace Maticsoft.BLL.Ms.Agent
{
    /// <summary>
    /// 代理商
    /// </summary>
    public partial class Agents
    {
        private readonly IAgents dal = DAMsAgent.CreateAgents();
        public Agents()
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
        public bool Exists(int AgentId)
        {
            return dal.Exists(AgentId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Ms.Agent.AgentInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Ms.Agent.AgentInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AgentId)
        {

            return dal.Delete(AgentId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AgentIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(AgentIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Ms.Agent.AgentInfo GetModel(int AgentId)
        {

            return dal.GetModel(AgentId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Ms.Agent.AgentInfo GetModelByCache(int AgentId)
        {

            string CacheKey = "AgentsModel-" + AgentId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AgentId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Ms.Agent.AgentInfo)objModel;
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
        public List<Maticsoft.Model.Ms.Agent.AgentInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Ms.Agent.AgentInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.Agent.AgentInfo> modelList = new List<Maticsoft.Model.Ms.Agent.AgentInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Ms.Agent.AgentInfo model;
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Ms.Agent.AgentInfo GetModelByUserId(int userId)
        {
            return dal.GetModelByUserId(userId);
        }
        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name)
        {
            return dal.ExistsShopName(Name);
        }

        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name, int SupplierID)
        {
            return dal.ExistsShopName(Name, SupplierID);
        }
        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }

        public DataSet GetEnteName(string name, int iCount)
        {
            string strWhere = "Name like '" + name + "%' AND Status=1 ";
            return dal.GetList(iCount, strWhere, "Name");
        }

        public List<Maticsoft.Model.Ms.Agent.AgentInfo> GetModelBySupplierName(string name)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                strWhere = "Name = '" + name + "'";
            }
            return GetModelList(strWhere);
        }

        //public DataSet GetStatisticsSupply(int supplierId)
        //{
        //    return dal.GetStatisticsSupply(supplierId);
        //}

        //public DataSet GetStatisticsSales(int supplierId, int year)
        //{
        //    return dal.GetStatisticsSales(supplierId, year);
        //}

        /// <summary>
        /// 关闭店铺 
        /// </summary>
        public bool CloseShop(int SupplierId)
        {
            return dal.CloseShop(SupplierId);
        }

        /// <summary>
        /// 根据店铺名称得到店铺model
        /// </summary>
        /// <param name="ShopName"></param>
        /// <returns></returns>
        public Maticsoft.Model.Ms.Agent.AgentInfo GetModelByShopName(string ShopName)
        {
            if (!string.IsNullOrWhiteSpace(ShopName))
            {
                return dal.GetModelByShopName(ShopName);
            }
            return null;
        }

        /// <summary>
        /// 代理商名称是否已存在
        /// </summary>
        public bool Exists(string name, int id = 0)
        {
            return dal.Exists(name, id);
        }
        #endregion  ExtensionMethod
    }
}

