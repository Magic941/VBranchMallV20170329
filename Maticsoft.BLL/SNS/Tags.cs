/**
* Tags.cs
*
* 功 能： N/A
* 类 名： Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:58   N/A    初版
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
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using System.Linq;
using System.Text.RegularExpressions;
namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 商品标签
    /// </summary>
    public partial class Tags
    {
        private readonly ITags dal = DASNS.CreateTags();
        public Tags()
        { }
        #region  BasicMethod



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TypeId, string TagName)
        {
            return dal.Exists(TypeId, TagName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Tags model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Tags model)
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
        public Maticsoft.Model.SNS.Tags GetModel(int TagID)
        {

            return dal.GetModel(TagID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Tags GetModelByCache(int TagID)
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
            return (Maticsoft.Model.SNS.Tags)objModel;
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
        public List<Maticsoft.Model.SNS.Tags> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Tags> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Tags> modelList = new List<Maticsoft.Model.SNS.Tags>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Tags model;
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
        /// 根据类型获取正常状态的标签数据集
        /// </summary>
        public List<Maticsoft.Model.SNS.Tags> GetList(int TypeId)
        {
            return DataTableToList(dal.GetList("Status=1 and TypeId=" + TypeId).Tables[0]);
        }
        public List<Maticsoft.Model.SNS.Tags> GetHotTags(int top)
        {
            return DataTableToList(dal.GetHotTags(top).Tables[0]);
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public string GetTagUnionStrByCache()
        {

            string CacheKey = "TagUnionStr";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    List<Maticsoft.Model.SNS.Tags> list = GetModelList("");
                    var Array = (from item in list select item.TagName).ToArray();
                    objModel = string.Join("|", Array);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (objModel != null ? objModel.ToString() : "");
        }
        /// <summary>
        /// 进行分类提取标签
        /// </summary>
        /// <param name="Des"></param>
        /// <returns></returns>
        public string GetTagStr(string Des)
        {
            string Result = "";
            MatchCollection matches = Regex.Matches(Des, GetTagUnionStrByCache());
            foreach (Match item in matches)
            {
                Result = item.Value + "|";

            }
            return string.IsNullOrEmpty(Result) ? "" : Result.TrimEnd('|');
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetSearchList(string Keywords)
        {
            string strWhere = "";
            if (Keywords.Length > 0)
            {
                strWhere = string.Format(" TagName like '%{0}%' ", Keywords);
            }
            return dal.GetListEx(0, strWhere, "");
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(string strWhere)
        {
            return dal.GetListEx(0, strWhere, "");
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(int IsRecommand, string IdList)
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
        #endregion  ExtensionMethod
    }
}

