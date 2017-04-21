/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductConsultations.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/24 17:49:44
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
    /// 商品咨询类别表
    /// </summary>
    public partial class ProductConsults
    {
        private readonly IProductConsults dal = DAShopProducts.CreateProductConsults();

        public ProductConsults()
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
        public bool Exists(int ConsultationId)
        {
            return dal.Exists(ConsultationId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.ProductConsults model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductConsults model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ConsultationId)
        {

            return dal.Delete(ConsultationId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ConsultationIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ConsultationIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductConsults GetModel(int ConsultationId)
        {

            return dal.GetModel(ConsultationId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductConsults GetModelByCache(int ConsultationId)
        {

            string CacheKey = "ProductConsultsModel-" + ConsultationId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ConsultationId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductConsults)objModel;
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
        public List<Maticsoft.Model.Shop.Products.ProductConsults> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductConsults> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductConsults> modelList = new List<Maticsoft.Model.Shop.Products.ProductConsults>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductConsults model;
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
        public List<Maticsoft.Model.Shop.Products.ProductConsults> GetConsultationsByPage(long productId, string orderBy,
                                                                               int startIndex,
                                                                               int endIndex)
        {
            DataSet ds = dal.GetListByPage("Status=1 and ProductId=" + productId, orderBy, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 批量更新审核状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool UpdateStatusList(string ids,int status)
        {
            return dal.UpdateStatusList(ids, status);
        }

        #endregion
    }
}