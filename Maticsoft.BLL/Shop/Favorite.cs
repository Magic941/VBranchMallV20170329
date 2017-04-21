/**
* Favorite.cs
*
* 功 能： N/A
* 类 名： Favorite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/22 15:32:12   N/A    初版
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
using System.Text;
using Maticsoft.Common;
using Maticsoft.Model.Shop;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop;
namespace Maticsoft.BLL.Shop
{
    /// <summary>
    /// Favorite
    /// </summary>
    public partial class Favorite
    {
        private readonly IFavorite dal = DAShop.CreateFavorite();
        public Favorite()
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
        public bool Exists(int FavoriteId)
        {
            return dal.Exists(FavoriteId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Favorite model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Favorite model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int FavoriteId)
        {

            return dal.Delete(FavoriteId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string FavoriteIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(FavoriteIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Favorite GetModel(int FavoriteId)
        {

            return dal.GetModel(FavoriteId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Favorite GetModelByCache(int FavoriteId)
        {

            string CacheKey = "FavoriteModel-" + FavoriteId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(FavoriteId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Favorite)objModel;
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
        public List<Maticsoft.Model.Shop.Favorite> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Favorite> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Favorite> modelList = new List<Maticsoft.Model.Shop.Favorite>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Favorite model;
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
        public DataSet GetListEX(int userid, string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            if (userid >0)
            {
                strWhere.AppendFormat("userid={0}", userid);
                if (!String.IsNullOrWhiteSpace(keyword))
                {
                    strWhere.AppendFormat("and Tags like '%{0}%' or Remark like '%{0}%'", keyword);
                }
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(keyword))
                {
                    strWhere.AppendFormat("Tags like '%{0}%' or Remark like '%{0}%'", keyword);
                }
                else
                {
                    return GetAllList();
                }
            }

            return dal.GetList(strWhere.ToString());
        }

        /// <summary>
        /// 分页获取收藏商品列表 
        /// </summary>
        public DataSet GetProductListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetProductListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取收藏商品列表 
        /// </summary>
        public List<Maticsoft.ViewModel.Shop.FavoProdModel> GetFavoriteProductListByPage(string strWhere, int startIndex, int endIndex)
        {   //FavoriteId,  CreatedDate ,  ProductId ,  ProductName,  SaleStatus    
            DataSet ds= dal.GetProductListByPage(strWhere, "CreatedDate desc ", startIndex, endIndex);
            DataTable dt = ds.Tables[0];
            List<Maticsoft.ViewModel.Shop.FavoProdModel> modelList = new List<Maticsoft.ViewModel.Shop.FavoProdModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.ViewModel.Shop.FavoProdModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    DataRow row = dt.Rows[n];
                    model = new Maticsoft.ViewModel.Shop.FavoProdModel();
                    if (row != null)
                    {
                        if (row["FavoriteId"] != null && row["FavoriteId"].ToString() != "")
                        {
                            model.FavoriteId = int.Parse(row["FavoriteId"].ToString());
                        }
                        if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                        {
                            model.ProductId = long.Parse(row["ProductId"].ToString());
                        }
                        if (row["ProductName"] != null )
                        {
                            model.ProductName =  row["ProductName"].ToString() ;
                        }
                        if (row["SaleStatus"] != null && row["SaleStatus"].ToString() != "")
                        {
                            model.SaleStatus =int.Parse(row["SaleStatus"].ToString());
                        }
                        if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                        {
                            model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                        }
                        if (row["ThumbnailUrl1"] != null )
                        {
                            model.ThumbnailUrl1 = row["ThumbnailUrl1"].ToString();
                        }
                        modelList.Add(model);
                    } 
                }
            }
            return modelList;
        }

        public bool Exists(long targetId,int userId,int type)
        {
            return dal.Exists(targetId, userId,type);
        }
        #endregion
    }
}

