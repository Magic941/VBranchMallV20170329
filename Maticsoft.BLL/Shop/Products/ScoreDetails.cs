/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ScoreDetails.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/27 14:50:44
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
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.Common;
namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// 评分记录表
    /// </summary>
    public partial class ScoreDetails
    {
        private readonly IScoreDetails dal = DAShopProducts.CreateScoreDetails();

        public ScoreDetails()
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
        public bool Exists(int ScoreId)
        {
            return dal.Exists(ScoreId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ScoreId)
        {
            return dal.Delete(ScoreId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ScoreIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ScoreIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ScoreDetails GetModel(int ScoreId)
        {
            return dal.GetModel(ScoreId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ScoreDetails GetModelByCache(int ScoreId)
        {
            string CacheKey = "ScoreDetailsModel-" + ScoreId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ScoreId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ScoreDetails)objModel;
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
        public List<Maticsoft.Model.Shop.Products.ScoreDetails> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ScoreDetails> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ScoreDetails> modelList = new List<Maticsoft.Model.Shop.Products.ScoreDetails>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ScoreDetails model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ScoreDetails();
                    if (dt.Rows[n]["ScoreId"] != null && dt.Rows[n]["ScoreId"].ToString() != "")
                    {
                        model.ScoreId = int.Parse(dt.Rows[n]["ScoreId"].ToString());
                    }
                    if (dt.Rows[n]["ReviewId"] != null && dt.Rows[n]["ReviewId"].ToString() != "")
                    {
                        model.ReviewId = int.Parse(dt.Rows[n]["ReviewId"].ToString());
                    }
                    if (dt.Rows[n]["UserId"] != null && dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["Score"] != null && dt.Rows[n]["Score"].ToString() != "")
                    {
                        model.Score = int.Parse(dt.Rows[n]["Score"].ToString());
                    }
                    if (dt.Rows[n]["CreatedDate"] != null && dt.Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
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

        public int GetScore(int? ReviewId)
        {
            return dal.GetScore(ReviewId);
        }
    }
}