/**
* ExpressTemplates.cs
*
* 功 能： N/A
* 类 名： ExpressTemplates
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/18 19:00:30   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.DALFactory;

namespace Maticsoft.BLL.Shop.Sales
{
    /// <summary>
    /// ExpressTemplates
    /// </summary>
    public partial class ExpressTemplate
    {
        private readonly IDAL.Shop.Sales.IExpressTemplate dal = DAShopSales.CreateExpressTemplate();

        public ExpressTemplate()
        {
        }

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
        public bool Exists(int ExpressId)
        {
            return dal.Exists(ExpressId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Shop.Sales.ExpressTemplate model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Shop.Sales.ExpressTemplate model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ExpressId)
        {

            return dal.Delete(ExpressId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ExpressIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ExpressIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Shop.Sales.ExpressTemplate GetModel(int ExpressId)
        {

            return dal.GetModel(ExpressId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.Shop.Sales.ExpressTemplate GetModelByCache(int ExpressId)
        {

            string CacheKey = "ExpressTemplatesModel-" + ExpressId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ExpressId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                            TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Model.Shop.Sales.ExpressTemplate)objModel;
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
        public List<Model.Shop.Sales.ExpressTemplate> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Shop.Sales.ExpressTemplate> DataTableToList(DataTable dt)
        {
            List<Model.Shop.Sales.ExpressTemplate> modelList =
                new List<Model.Shop.Sales.ExpressTemplate>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Shop.Sales.ExpressTemplate model;
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

        public bool UpdateExpressName(int expressId, string expressName)
        {
            return dal.UpdateExpressName(expressId, expressName);
        }
        #endregion  ExtensionMethod

    }
}

