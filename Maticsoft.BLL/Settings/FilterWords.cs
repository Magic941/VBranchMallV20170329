/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：FilterWords.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/24 11:00:36
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Settings;

namespace Maticsoft.BLL.Settings
{
    /// <summary>
    /// FilterWords
    /// </summary>
    public partial class FilterWords
    {
        private readonly IFilterWords dal = DASettings.CreateFilterWords();

        public FilterWords()
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
        public bool Exists(int FilterId)
        {
            return dal.Exists(FilterId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Settings.FilterWords model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Settings.FilterWords model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int FilterId)
        {

            return dal.Delete(FilterId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string FilterIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(FilterIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Settings.FilterWords GetModel(int FilterId)
        {

            return dal.GetModel(FilterId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Settings.FilterWords GetModelByCache(int FilterId)
        {

            string CacheKey = "FilterWordsModel-" + FilterId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(FilterId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Settings.FilterWords)objModel;
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
        public List<Maticsoft.Model.Settings.FilterWords> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Settings.FilterWords> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.FilterWords> modelList = new List<Maticsoft.Model.Settings.FilterWords>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Settings.FilterWords model;
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

        #region 扩展方法

        /// <summary>
        /// 根据敏感词获取实体
        /// </summary>
        public Model.Settings.FilterWords GetByWordPattern(string wordPattern)
        {
            return dal.GetByWordPattern(wordPattern);
        }

        /// <summary>
        /// 判断是否含有禁用词
        /// </summary>
        public static bool ContainsDisWords(string str)
        {
            string CacheKey = "ContainsDisWords-" + str;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = false;
                    Maticsoft.BLL.Settings.FilterWords bll = new FilterWords();
                    List<Model.Settings.FilterWords> list = bll.GetModelList("ActionType=0");
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (str.Contains(item.WordPattern.Trim()))
                            {
                                objModel = true;
                                break;
                            }
                        }
                    }
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                    Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                }
                catch { }
            }
            return (bool)objModel;

        }
        /// <summary>
        /// 判断是否含有审核词
        /// </summary>
        public static bool ContainsModWords(string str)
        {
            string CacheKey = "ContainsModWords-" + str;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = false;
                    Maticsoft.BLL.Settings.FilterWords bll = new FilterWords();
                    List<Model.Settings.FilterWords> list = bll.GetModelList("ActionType=1");
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (str.Contains(item.WordPattern.Trim()))
                            {
                                objModel = true;
                                break;
                            }
                        }
                    }
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                    Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                }
                catch { }
            }
            return (bool)objModel;

        }

        /// <summary>
        /// 替换词词
        /// </summary>
        /// <param name="contains">是否已替换</param>
        public static string ReplaceWords(string str, out bool contains)
        {
            contains = false;
            Maticsoft.BLL.Settings.FilterWords bll = new FilterWords();
            string result = str;
            List<Model.Settings.FilterWords> list = bll.GetModelList("ActionType=2");
            if (list != null && list.Count > 0)
            {
                foreach (var word in list)
                {
                    if (!contains)
                    {
                        contains = result.Contains(word.WordPattern);
                    }
                    result = result.Replace(word.WordPattern, word.RepalceWord);
                }
            }
            return result;
        }

        /// <summary>
        /// 替换词词
        /// </summary>
        public static string ReplaceWords(string str)
        {
            Maticsoft.BLL.Settings.FilterWords bll = new FilterWords();
            string result = str;
            List<Model.Settings.FilterWords> list = bll.GetModelList("ActionType=2");
            if (list != null && list.Count > 0)
            {
                foreach (var word in list)
                {
                    result = result.Replace(word.WordPattern, word.RepalceWord);
                }
            }
            return result;
        }


        /// <summary>
        /// 清除敏感词缓存
        /// </summary>
        public void ClearCache()
        {
            HttpRuntime.Cache.Remove("ForbidWordRegEx");
        }

        public bool UpdateActionType(string ids, int type, string replace)
        {
            return dal.UpdateActionType(ids, type, replace);
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string word)
        {
            return dal.Exists(word);
        }

        #endregion 扩展方法
    }
}