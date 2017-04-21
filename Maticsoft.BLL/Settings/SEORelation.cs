/**
* SEORelation.cs
*
* 功 能：
* 类 名： SEORelation
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/15 10:50:22   Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Settings;

namespace Maticsoft.BLL.Settings
{
    /// <summary>
    /// SEO关联管理
    /// </summary>
    public partial class SEORelation
    {
        private readonly ISEORelation dal = DASettings.CreateSEORelation();

        public SEORelation()
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
        public bool Exists(int RelationID)
        {
            return dal.Exists(RelationID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Settings.SEORelation model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Settings.SEORelation model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RelationID)
        {
            return dal.Delete(RelationID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string RelationIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(RelationIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Settings.SEORelation GetModel(int RelationID)
        {
            return dal.GetModel(RelationID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Settings.SEORelation GetModelByCache(int RelationID)
        {
            string CacheKey = "SEORelationModel-" + RelationID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RelationID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Settings.SEORelation)objModel;
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
        public List<Maticsoft.Model.Settings.SEORelation> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Settings.SEORelation> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.SEORelation> modelList = new List<Maticsoft.Model.Settings.SEORelation>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Settings.SEORelation model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Settings.SEORelation();
                    if (dt.Rows[n]["RelationID"] != null && dt.Rows[n]["RelationID"].ToString() != "")
                    {
                        model.RelationID = int.Parse(dt.Rows[n]["RelationID"].ToString());
                    }
                    if (dt.Rows[n]["KeyName"] != null && dt.Rows[n]["KeyName"].ToString() != "")
                    {
                        model.KeyName = dt.Rows[n]["KeyName"].ToString();
                    }
                    if (dt.Rows[n]["LinkURL"] != null && dt.Rows[n]["LinkURL"].ToString() != "")
                    {
                        model.LinkURL = dt.Rows[n]["LinkURL"].ToString();
                    }
                    if (dt.Rows[n]["IsCMS"] != null && dt.Rows[n]["IsCMS"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsCMS"].ToString() == "1") || (dt.Rows[n]["IsCMS"].ToString().ToLower() == "true"))
                        {
                            model.IsCMS = true;
                        }
                        else
                        {
                            model.IsCMS = false;
                        }
                    }
                    if (dt.Rows[n]["IsShop"] != null && dt.Rows[n]["IsShop"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsShop"].ToString() == "1") || (dt.Rows[n]["IsShop"].ToString().ToLower() == "true"))
                        {
                            model.IsShop = true;
                        }
                        else
                        {
                            model.IsShop = false;
                        }
                    }
                    if (dt.Rows[n]["IsSNS"] != null && dt.Rows[n]["IsSNS"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsSNS"].ToString() == "1") || (dt.Rows[n]["IsSNS"].ToString().ToLower() == "true"))
                        {
                            model.IsSNS = true;
                        }
                        else
                        {
                            model.IsSNS = false;
                        }
                    }
                    if (dt.Rows[n]["IsComment"] != null && dt.Rows[n]["IsComment"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsComment"].ToString() == "1") || (dt.Rows[n]["IsComment"].ToString().ToLower() == "true"))
                        {
                            model.IsComment = true;
                        }
                        else
                        {
                            model.IsComment = false;
                        }
                    }
                    if (dt.Rows[n]["CreatedDate"] != null && dt.Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
                    }
                    if (dt.Rows[n]["IsActive"] != null && dt.Rows[n]["IsActive"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsActive"].ToString() == "1") || (dt.Rows[n]["IsActive"].ToString().ToLower() == "true"))
                        {
                            model.IsActive = true;
                        }
                        else
                        {
                            model.IsActive = false;
                        }
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string  name)
        {
            return dal.Exists(name);
        }
    }
}