/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SKUMemberPrice.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:33
// 修改标识：
// 修改描述：
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
	/// SKUMemberPrice
	/// </summary>
	public partial class SKUMemberPrice
	{
        private readonly ISKUMemberPrice dal = DAShopProducts.CreateSKUMemberPrice();
		public SKUMemberPrice()
		{}
		#region  Method

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
		public bool Exists(long SkuId,int GradeId)
		{
			return dal.Exists(SkuId,GradeId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Products.SKUMemberPrice model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Products.SKUMemberPrice model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long SkuId,int GradeId)
		{
			
			return dal.Delete(SkuId,GradeId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Products.SKUMemberPrice GetModel(long SkuId,int GradeId)
		{
			
			return dal.GetModel(SkuId,GradeId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Products.SKUMemberPrice GetModelByCache(long SkuId,int GradeId)
		{
			
			string CacheKey = "SKUMemberPriceModel-" + SkuId+GradeId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SkuId,GradeId);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Products.SKUMemberPrice)objModel;
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
		public List<Maticsoft.Model.Shop.Products.SKUMemberPrice> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Products.SKUMemberPrice> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Products.SKUMemberPrice> modelList = new List<Maticsoft.Model.Shop.Products.SKUMemberPrice>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Products.SKUMemberPrice model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Shop.Products.SKUMemberPrice();
					if(dt.Rows[n]["SkuId"]!=null && dt.Rows[n]["SkuId"].ToString()!="")
					{
						model.SkuId=long.Parse(dt.Rows[n]["SkuId"].ToString());
					}
					if(dt.Rows[n]["GradeId"]!=null && dt.Rows[n]["GradeId"].ToString()!="")
					{
						model.GradeId=int.Parse(dt.Rows[n]["GradeId"].ToString());
					}
					if(dt.Rows[n]["MemberSalePrice"]!=null && dt.Rows[n]["MemberSalePrice"].ToString()!="")
					{
						model.MemberSalePrice=decimal.Parse(dt.Rows[n]["MemberSalePrice"].ToString());
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

