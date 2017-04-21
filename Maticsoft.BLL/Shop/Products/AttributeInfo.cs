/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Attributes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:22
// 修改标识： [Rock]         2012年6月14日 17:09:44
// 修改描述：新增 AttributeManage 方法
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// AttributeInfo
    /// </summary>
    public partial class AttributeInfo
    {
        private readonly IAttributeInfo dal = DAShopProducts.CreateAttributeInfo();
        public AttributeInfo()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AttributeId)
        {
            return dal.Exists(AttributeId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Products.AttributeInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.AttributeInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long AttributeId)
        {

            return dal.Delete(AttributeId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AttributeIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(AttributeIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.AttributeInfo GetModel(long AttributeId)
        {

            return dal.GetModel(AttributeId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.AttributeInfo GetModelByCache(long AttributeId)
        {

            string CacheKey = "AttributesModel-" + AttributeId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AttributeId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.AttributeInfo)objModel;
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
        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.AttributeInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.AttributeInfo> modelList = new List<Maticsoft.Model.Shop.Products.AttributeInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.AttributeInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.AttributeInfo();
                    if (dt.Rows[n]["AttributeId"] != null && dt.Rows[n]["AttributeId"].ToString() != "")
                    {
                        model.AttributeId = long.Parse(dt.Rows[n]["AttributeId"].ToString());
                    }
                    if (dt.Rows[n]["AttributeName"] != null && dt.Rows[n]["AttributeName"].ToString() != "")
                    {
                        model.AttributeName = dt.Rows[n]["AttributeName"].ToString();
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["UsageMode"] != null && dt.Rows[n]["UsageMode"].ToString() != "")
                    {
                        model.UsageMode = int.Parse(dt.Rows[n]["UsageMode"].ToString());
                    }
                    if (dt.Rows[n]["UseAttributeImage"] != null && dt.Rows[n]["UseAttributeImage"].ToString() != "")
                    {
                        if ((dt.Rows[n]["UseAttributeImage"].ToString() == "1") || (dt.Rows[n]["UseAttributeImage"].ToString().ToLower() == "true"))
                        {
                            model.UseAttributeImage = true;
                        }
                        else
                        {
                            model.UseAttributeImage = false;
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

        #endregion  Method

        #region NewMethod
        public bool AttributeManage(Model.Shop.Products.AttributeInfo model, Model.Shop.Products.DataProviderAction Action)
        {
            return dal.AttributeManage(model, Action);
        }

        public DataSet GetList(long? Typeid, Model.Shop.Products.SearchType searchType)
        {
            return dal.GetList(Typeid, searchType);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetModelList(Model.Shop.Products.SearchType searchType)
        {
            switch (searchType)
            {
                case SearchType.ExtAttribute:
                    return GetModelList("UsageMode <>3");
                case SearchType.Specification:
                    return GetModelList("UsageMode =3");
                default:
                    return null;
            }

        }

        public bool ChangeImageStatue(long AttributeId, Model.Shop.Products.ProductAttributeModel status)
        {
            return dal.ChangeImageStatue(AttributeId, status);
        }

        public List<Model.Shop.Products.AttributeInfo> GetAttributeInfoList(int? typeId, Model.Shop.Products.SearchType searchType)
        {
            return dal.GetAttributeInfoList(typeId, searchType);
        }
        /// <summary>
        /// 根据分类ID 获取属性列表  
        /// </summary>
        /// <param name="cateID"></param>
        /// <param name="isChild">是否包括子类</param>
        /// <returns></returns>

        public List<Model.Shop.Products.AttributeInfo> GetAttributeListByCateID(int cateID, bool isChild = false)
        {
            DataSet ds = dal.GetAttributesByCate(cateID, isChild);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
            //
        }

        public bool IsExistDefinedAttribute(int typeId, long? attId)
        {
            return dal.IsExistDefinedAttribute(typeId, attId);
        }

        public List<Model.Shop.Products.AttributeHelper> ProductAttributeInfo(long productId)
        {
            DataSet ds = dal.GetProductAttributes(productId);
            if (ds != null && ds.Tables.Count > 0)
            {
                return AttributeDataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }


        public List<Maticsoft.Model.Shop.Products.AttributeHelper> AttributeDataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.AttributeHelper> modelList = new List<Maticsoft.Model.Shop.Products.AttributeHelper>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.AttributeHelper model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.AttributeHelper();
                    if (dt.Rows[n]["ValueId"] != null && dt.Rows[n]["ValueId"].ToString() != "")
                    {
                        model.ValueId = int.Parse(dt.Rows[n]["ValueId"].ToString());
                    }
                    if (dt.Rows[n]["ValueStr"] != null && dt.Rows[n]["ValueStr"].ToString() != "")
                    {
                        model.ValueStr = dt.Rows[n]["ValueStr"].ToString();
                    }
                    if (dt.Rows[n]["AttributeId"] != null && dt.Rows[n]["AttributeId"].ToString() != "")
                    {
                        model.AttributeId = long.Parse(dt.Rows[n]["AttributeId"].ToString());
                    }
                    if (dt.Rows[n]["AttributeName"] != null && dt.Rows[n]["AttributeName"].ToString() != "")
                    {
                        model.AttributeName = dt.Rows[n]["AttributeName"].ToString();
                    }
                    if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["UsageMode"] != null && dt.Rows[n]["UsageMode"].ToString() != "")
                    {
                        model.UsageMode = int.Parse(dt.Rows[n]["UsageMode"].ToString());
                    }
                    if (dt.Rows[n]["UseAttributeImage"] != null && dt.Rows[n]["UseAttributeImage"].ToString() != "")
                    {
                        if ((dt.Rows[n]["UseAttributeImage"].ToString() == "1") || (dt.Rows[n]["UseAttributeImage"].ToString().ToLower() == "true"))
                        {
                            model.UseAttributeImage = true;
                        }
                        else
                        {
                            model.UseAttributeImage = false;
                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 判断该商品类型下是否存在同名的属性
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(int typeId, string name)
        {
            return dal.IsExistName(typeId, name);
        }

        public List<Model.Shop.Products.AttributeInfo> GetAttributeInfoListByProductId(long productId)
        {
            return dal.GetAttributeInfoListByProductId(productId);
        }
 
        /// <summary>
        /// 根据商品类型id获得数据列表(不包含SKU属性)
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetModelList(int typeid)
        {
            DataSet ds = dal.GetList(string.Format(" TypeId ={0} AND UsageMode!=3", typeid));
            return DataTableToList(ds.Tables[0]);
        }

        #endregion
    }
}


