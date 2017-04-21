/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Tags.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/12/14 10:05:40
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Tags;

namespace Maticsoft.BLL.Shop.Tags
{
    /// <summary>
    /// Tags
    /// </summary>
    public partial class Tags
    {
        private readonly ITags dal = DAShopProducts.CreateTags();

        public Tags()
        { }

        #region Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TagID)
        {
            return dal.Exists(TagID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Tags.Tags model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Tags.Tags model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TagID)
        {
            return dal.Delete(TagID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TagIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(TagIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Tags.Tags GetModel(int TagID)
        {
            return dal.GetModel(TagID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Tags.Tags GetModelByCache(int TagID)
        {
            string CacheKey = "TagsModel-" + TagID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(TagID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Tags.Tags)objModel;
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
        public List<Maticsoft.Model.Shop.Tags.Tags> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Tags.Tags> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Tags.Tags> modelList = new List<Maticsoft.Model.Shop.Tags.Tags>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Tags.Tags model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Tags.Tags();
                    if (dt.Rows[n]["TagID"] != null && dt.Rows[n]["TagID"].ToString() != "")
                    {
                        model.TagID = int.Parse(dt.Rows[n]["TagID"].ToString());
                    }
                    if (dt.Rows[n]["TagCategoryId"] != null && dt.Rows[n]["TagCategoryId"].ToString() != "")
                    {
                        model.TagCategoryId = int.Parse(dt.Rows[n]["TagCategoryId"].ToString());
                    }
                    if (dt.Rows[n]["TagName"] != null && dt.Rows[n]["TagName"].ToString() != "")
                    {
                        model.TagName = dt.Rows[n]["TagName"].ToString();
                    }
                    if (dt.Rows[n]["IsRecommand"] != null && dt.Rows[n]["IsRecommand"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsRecommand"].ToString() == "1") || (dt.Rows[n]["IsRecommand"].ToString().ToLower() == "true"))
                        {
                            model.IsRecommand = true;
                        }
                        else
                        {
                            model.IsRecommand = false;
                        }
                    }
                    if (dt.Rows[n]["Status"] != null && dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["Meta_Title"] != null && dt.Rows[n]["Meta_Title"].ToString() != "")
                    {
                        model.Meta_Title = dt.Rows[n]["Meta_Title"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Description"] != null && dt.Rows[n]["Meta_Description"].ToString() != "")
                    {
                        model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Keywords"] != null && dt.Rows[n]["Meta_Keywords"].ToString() != "")
                    {
                        model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
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

        #region ExtensionMethod

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(string IsRecommand, string IdList)
        {
            return dal.UpdateIsRecommand(IsRecommand, IdList);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateStatus(int Status, string IdList)
        {
            return dal.UpdateStatus(Status, IdList);
        }
        ///<summary>
        /// 获取数据列表
        ///</summary>
        public DataSet GetListEx(string strWhere)
        {
            return dal.GetListEx(0, strWhere, "");
        }
        #endregion
        
    }
}