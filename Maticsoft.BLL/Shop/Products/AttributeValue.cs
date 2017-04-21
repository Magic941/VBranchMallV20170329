/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：AttributeValues.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:22
// 修改标识： [Rock]      2012年6月14日 17:04:37
// 修改描述： 新增 AttributeValueManage方法
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// AttributeValue
    /// </summary>
    public partial class AttributeValue
    {
        private readonly IAttributeValue dal = DAShopProducts.CreateAttributeValue();
        public AttributeValue()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ValueId)
        {
            return dal.Exists(ValueId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ValueId)
        {

            return dal.Delete(ValueId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ValueIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ValueIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.AttributeValue GetModel(long ValueId)
        {

            return dal.GetModel(ValueId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.AttributeValue GetModelByCache(long ValueId)
        {

            string CacheKey = "AttributeValuesModel-" + ValueId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ValueId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.AttributeValue)objModel;
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
        public List<Maticsoft.Model.Shop.Products.AttributeValue> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.AttributeValue> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.AttributeValue> modelList = new List<Maticsoft.Model.Shop.Products.AttributeValue>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.AttributeValue model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.AttributeValue();
                    if (dt.Rows[n]["ValueId"] != null && dt.Rows[n]["ValueId"].ToString() != "")
                    {
                        model.ValueId = long.Parse(dt.Rows[n]["ValueId"].ToString());
                    }
                    if (dt.Rows[n]["AttributeId"] != null && dt.Rows[n]["AttributeId"].ToString() != "")
                    {
                        model.AttributeId = long.Parse(dt.Rows[n]["AttributeId"].ToString());
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["ValueStr"] != null && dt.Rows[n]["ValueStr"].ToString() != "")
                    {
                        model.ValueStr = dt.Rows[n]["ValueStr"].ToString();
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
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
        public bool AttributeValueManage(Model.Shop.Products.AttributeValue model, Model.Shop.Products.DataProviderAction Action)
        {
            return dal.AttributeValueManage(model, Action);
        }

        public DataSet GetListByAttribute(long AttributeId)
        {
            return dal.GetListByAttribute(AttributeId);
        }

        public bool DeleteImage(long valueId)
        {
            return dal.DeleteImage(valueId);
        }

        //        public DataSet GetList(long? AttributeId)
        //{

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.AttributeValue> GetModelList(long? AttributeId)
        {
            DataSet ds = dal.GetList(AttributeId);
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeValue> GetModelListByCateID(int? cateId)
        {
            DataSet ds = dal.GetAttributeValue(cateId);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据商品listid和属性id获取商品属性值
        /// </summary>
        /// <param name="pordIDList">商品idList</param>
        ///  <param name="attrid">属性id</param>
        /// <returns></returns>
        public DataSet GetAttrValue(string pordIDList, int attrid)
        {
            return dal.GetAttrValue(pordIDList, attrid);
        }
     
        /// <summary>
        /// 根据商品id批量获取商品属性值
        /// </summary>
        /// <param name="PordIDList">商品idList</param>
        /// <returns></returns>
        public List<ViewModel.Shop.ProdCompareModel> GetTableToList(DataTable dt)
        {
            List<ViewModel.Shop.ProdCompareModel> modelList = new List<ViewModel.Shop.ProdCompareModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ViewModel.Shop.ProdCompareModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ViewModel.Shop.ProdCompareModel();
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["ValueId"] != null && dt.Rows[n]["ValueId"].ToString() != "")
                    {
                        model.ValueId = int.Parse(dt.Rows[n]["ValueId"].ToString());
                    }
                    if (dt.Rows[n]["AttributeId"] != null && dt.Rows[n]["AttributeId"].ToString() != "")
                    {
                        model.AttributeId = long.Parse(dt.Rows[n]["AttributeId"].ToString());
                    }
                    if (dt.Rows[n]["ValueStr"] != null && dt.Rows[n]["ValueStr"].ToString() != "")
                    {
                        model.ValueStr = dt.Rows[n]["ValueStr"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string strWhere)
        {
            return dal.Exists(strWhere);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AttributeId, string ValueStr, long ValueId=0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" AttributeId={0} and ValueStr ='{1}'", AttributeId, ValueStr);
            if (ValueId > 0)
            {
                strSql.AppendFormat("  and ValueId!={0}", ValueId);
            }
            return dal.Exists(strSql.ToString());
        }
        #endregion
    }
}

