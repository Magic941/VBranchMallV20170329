/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductConsultationsType.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/24 17:43:18
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
    public partial class ProductConsultsType
    {
        private readonly IProductConsultsType dal = DAShopProducts.CreateProductConsultsType();

        public ProductConsultsType()
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
        public bool Exists(int TypeId)
        {
            return dal.Exists(TypeId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TypeId)
        {
            return dal.Delete(TypeId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TypeIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(TypeIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductConsultsType GetModel(int TypeId)
        {
            return dal.GetModel(TypeId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductConsultsType GetModelByCache(int TypeId)
        {
            string CacheKey = "ProductConsultationsTypeModel-" + TypeId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(TypeId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductConsultsType)objModel;
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
        public List<Maticsoft.Model.Shop.Products.ProductConsultsType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductConsultsType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductConsultsType> modelList = new List<Maticsoft.Model.Shop.Products.ProductConsultsType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductConsultsType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductConsultsType();
                    if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["TypeName"] != null && dt.Rows[n]["TypeName"].ToString() != "")
                    {
                        model.TypeName = dt.Rows[n]["TypeName"].ToString();
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
    }
}