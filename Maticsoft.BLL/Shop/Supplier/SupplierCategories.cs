/**
* SupplierCategories.cs
*
* 功 能： N/A
* 类 名： SupplierCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:48   Ben    初版
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
using System.Linq;
using System.Text;
using System.Web.Caching;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Supplier;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Supplier;
namespace Maticsoft.BLL.Shop.Supplier
{
	/// <summary>
	/// 供应商(店铺)分类
	/// </summary>
	public partial class SupplierCategories
	{
        private readonly ISupplierCategories dal = DAShopSupplier.CreateSupplierCategories();
		public SupplierCategories()
		{}
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
		public bool Exists(int CategoryId)
		{
			return dal.Exists(CategoryId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CategoryId)
		{
			
			return dal.Delete(CategoryId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CategoryIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(CategoryIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Supplier.SupplierCategories GetModel(int CategoryId)
		{
			
			return dal.GetModel(CategoryId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Supplier.SupplierCategories GetModelByCache(int CategoryId)
		{
			
			string CacheKey = "SupplierCategoriesModel-" + CategoryId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CategoryId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Supplier.SupplierCategories)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Supplier.SupplierCategories> modelList = new List<Maticsoft.Model.Shop.Supplier.SupplierCategories>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Supplier.SupplierCategories model;
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
        public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetAllModelList(string str)
        {
            DataSet ds = GetList(str);
           return DataTableToList(ds.Tables[0]);
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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
        /// 根据供应商ID获得分类数据列表
      /// </summary>
      /// <param name="supplierId">供应商ID</param>
      /// <returns></returns>
        public static List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetAllCateList(long supplierId)
        {
            string CacheKey = "GetSuppCateList-CateList-" + supplierId;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.BLL.Shop.Supplier.SupplierCategories categoryBll = new SupplierCategories();
                    DataSet ds = categoryBll.GetList(-1, " SupplierId= " + supplierId, " DisplaySequence");
                    objModel = categoryBll.DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Shop.Supplier.SupplierCategories>)objModel;
        }
        public bool CreateCategory(Model.Shop.Supplier.SupplierCategories model)
        {
            Maticsoft.Model.Shop.Supplier.SupplierCategories parentModel = null;
            if (model.ParentCategoryId.HasValue && model.ParentCategoryId.Value > 0)
            {
                parentModel = GetModel(model.ParentCategoryId.Value);
            }
            if (parentModel != null)
            {
                model.Depth = parentModel.Depth + 1;
            }
            else
            {
                model.Depth = 1;
            }
            model.DisplaySequence = GetMaxSeqByCid(model.ParentCategoryId.HasValue?model.ParentCategoryId.Value:0,model.SupplierId) + 1;

            model.Path = "";
            model.CategoryId = dal.Add(model);
            if (model.CategoryId > 0)
            {
                //更新父分类 是否含有子集

                if (parentModel != null)
                {
                    UpdateHasChild(model.ParentCategoryId.Value, model.SupplierId);
                    model.Path = parentModel.Path + "|" + model.CategoryId;
                }
                else
                {
                    model.Path = model.CategoryId.ToString();
                }
                return dal.UpdatePath(model);
            }
            return false;
        }
       
        //添加编辑分类时 更新分类的HasChildren 字段
        public bool UpdateHasChild(int cid, int SupplierId)
        {
            return dal.UpdateHasChild(cid,SupplierId);
        }

	    public bool UpdateHasChild(int cid, int SupplierId, bool Status)
	    {
	        return dal.UpdateHasChild(cid, SupplierId, Status);
	    }

	    public int GetMaxSeqByCid(int parentId, int SupplierId)
        {
            return dal.GetMaxSeqByCid(parentId, SupplierId);
        }
        public bool UpdateSeqByCid(int Seq, int Cid, int SupplierId)
	    {
	        return  dal.UpdateSeqByCid(Seq, Cid,SupplierId);
	    }
        public bool UpdatePath(Model.Shop.Supplier.SupplierCategories model)
        {
            return dal.UpdatePath(model);
        }
        public bool UpdateDepthAndPath(int Cid, int Depth, string Path, int SupplierId)
        {
            return dal.UpdateDepthAndPath(Cid, Depth, Path, SupplierId);
        }
        /// <summary>
        /// 同级下是否存在同名
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="name">名称</param>
        /// <param name="SupplierId">供应商id</param>
        /// <param name="categoryId">类别id</param>
        /// <returns></returns>
        public bool IsExisted(int parentId, string name, int SupplierId,int categoryId = 0)
        {
            return dal.IsExisted(parentId, name,SupplierId, categoryId);
        }
        public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetCategorysByParentId(int parentCategoryId,int SupplierId, int Top = -1)
        {
            //ADD Cache
            DataSet ds = GetList(Top, string.Format(" SupplierId={0} and  ParentCategoryId ={1} ", SupplierId, parentCategoryId), " DisplaySequence");
            return DataTableToList(ds.Tables[0]);
        }

	    /// <summary>
	    ///根据商品id获取分类信息
	    /// </summary>
	    /// <param name="productId"></param>
	    /// <returns></returns>
	    public Model.Shop.Supplier.SupplierCategories GetModelByProductId(long productId)
	    {
	        return dal.GetModelByProductId(productId);
	    }

	    /// <summary>
	    /// 判断该分类下是否有商品
	    /// </summary>
	    /// <param name="CategoryId"></param>
	    /// <returns></returns>
	    public bool IsExistsProd(int CategoryId)
	    {
	        return dal.IsExistsProd(CategoryId);
	    }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Model.Shop.Supplier.SupplierCategories model)
        {
            if (model == null)
            {
                return false;
            }
            bool re = dal.Delete(model.CategoryId);
            if (re)
            {
                if (model.ParentCategoryId.HasValue && model.ParentCategoryId.Value > 0) //本身是子分类
                {
                    //判断一下该父分类下是否还有子分类 如果没有 则更改父节点的hasValue字段
                    if (dal.GetRecordCount(" ParentCategoryId= " + model.ParentCategoryId.Value) <= 0)
                    {
                        UpdateHasChild(model.CategoryId, model.SupplierId, false);
                    }
                }
            }
            return re;
        }

        #region 获取店铺分类信息


        /// <summary>
        /// 获取商品所在店铺分类信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public  string ProductSuppCategories(long productId, int SupplierId)
        {
            StringBuilder strName = new StringBuilder();
            Model.Shop.Supplier.SupplierCategories suppCateModel = GetModelByProductId(productId);
            if (suppCateModel != null)
            {
                if (suppCateModel.ParentCategoryId.HasValue && suppCateModel.ParentCategoryId.Value > 0) //二级分类
                {
                    List<Model.Shop.Supplier.SupplierCategories> cateList = BLL.Shop.Supplier.SupplierCategories.GetAllCateList(SupplierId);
                    if (cateList != null && cateList.Count > 0)
                    {
                        Model.Shop.Supplier.SupplierCategories categoryInfo = cateList.FirstOrDefault(o => o.CategoryId == suppCateModel.ParentCategoryId);
                        strName.Append(categoryInfo != null ? categoryInfo.Name + " » " : "");
                    }
                }
                strName.Append(suppCateModel.Name);
            }
            return strName.ToString();
        }
        #endregion
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetModelList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetModelList(int SupplierId)
        {
            DataSet ds = dal.GetList(-1, string.Format(" ParentCategoryId=0 and SupplierId={0} ", SupplierId), " DisplaySequence ");
            return DataTableToList(ds.Tables[0]);
        }
        public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetModelList(int SupplierId,int categoryId)
        {
            DataSet ds = dal.GetList(-1, string.Format(" (CategoryId={1} or ParentCategoryId={1}) and SupplierId={0} ", SupplierId,categoryId), " DisplaySequence ");
            return DataTableToList(ds.Tables[0]);
        }
        public int GetCountBySupIdEx(int depth,int supplierId)
        {
            return dal.GetCountBySupIdEx(depth, supplierId);
        }

	    public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetListByPageEx(string strWhere, string orderby,
	                                                                                  int startIndex, int endIndex,
	                                                                                  int categoryId = 0)
	    {
	        DataSet ds;
	        if (categoryId == 0)
	        {
	            ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
	        }

	        else
	        {
	            StringBuilder sb = new StringBuilder();;
	            if (String.IsNullOrWhiteSpace(strWhere))
	            {
                    sb.Append(string.Format("CategoryId={0} or ParentCategoryId={0}", categoryId));
	            }
	            else
	            {
                    sb.Append(string.Format("And (CategoryId={0} or ParentCategoryId={0})", categoryId));
	            }
	         ds = dal.GetListByPage(sb.ToString(), orderby, startIndex, endIndex);
	      }
                return DataTableToList(ds.Tables[0]);
            }
	   

	    #endregion  ExtensionMethod
	}
}

