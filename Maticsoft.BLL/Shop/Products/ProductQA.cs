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
	/// ProductQA
	/// </summary>
	public partial class ProductQA
	{
		private readonly IProductQA dal = DAShopProducts.CreateProductQA();
		public ProductQA()
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
		public bool Exists(int QAId)
		{
			return dal.Exists(QAId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Products.ProductQA model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Products.ProductQA model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int QAId)
		{
			
			return dal.Delete(QAId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string QAIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(QAIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Products.ProductQA GetModel(int QAId)
		{
			
			return dal.GetModel(QAId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Products.ProductQA GetModelByCache(int QAId)
		{
			
			string CacheKey = "ProductQAModel-" + QAId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(QAId);
					if (objModel != null)
					{
						int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Products.ProductQA)objModel;
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
		public List<Maticsoft.Model.Shop.Products.ProductQA> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Products.ProductQA> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Products.ProductQA> modelList = new List<Maticsoft.Model.Shop.Products.ProductQA>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Products.ProductQA model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Shop.Products.ProductQA();
					if(dt.Rows[n]["QAId"]!=null && dt.Rows[n]["QAId"].ToString()!="")
					{
						model.QAId=int.Parse(dt.Rows[n]["QAId"].ToString());
					}
					if(dt.Rows[n]["ParentId"]!=null && dt.Rows[n]["ParentId"].ToString()!="")
					{
						model.ParentId=int.Parse(dt.Rows[n]["ParentId"].ToString());
					}
					if(dt.Rows[n]["ProductId"]!=null && dt.Rows[n]["ProductId"].ToString()!="")
					{
						model.ProductId=int.Parse(dt.Rows[n]["ProductId"].ToString());
					}
					if(dt.Rows[n]["UserId"]!=null && dt.Rows[n]["UserId"].ToString()!="")
					{
						model.UserId=int.Parse(dt.Rows[n]["UserId"].ToString());
					}
					if(dt.Rows[n]["UserName"]!=null && dt.Rows[n]["UserName"].ToString()!="")
					{
					model.UserName=dt.Rows[n]["UserName"].ToString();
					}
					if(dt.Rows[n]["Question"]!=null && dt.Rows[n]["Question"].ToString()!="")
					{
					model.Question=dt.Rows[n]["Question"].ToString();
					}
					if(dt.Rows[n]["State"]!=null && dt.Rows[n]["State"].ToString()!="")
					{
						model.State=int.Parse(dt.Rows[n]["State"].ToString());
					}
					if(dt.Rows[n]["CreatedDate"]!=null && dt.Rows[n]["CreatedDate"].ToString()!="")
					{
						model.CreatedDate=DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
					}
					if(dt.Rows[n]["ReplyContent"]!=null && dt.Rows[n]["ReplyContent"].ToString()!="")
					{
					model.ReplyContent=dt.Rows[n]["ReplyContent"].ToString();
					}
					if(dt.Rows[n]["ReplyDate"]!=null && dt.Rows[n]["ReplyDate"].ToString()!="")
					{
						model.ReplyDate=DateTime.Parse(dt.Rows[n]["ReplyDate"].ToString());
					}
					if(dt.Rows[n]["ReplyUserId"]!=null && dt.Rows[n]["ReplyUserId"].ToString()!="")
					{
						model.ReplyUserId=int.Parse(dt.Rows[n]["ReplyUserId"].ToString());
					}
					if(dt.Rows[n]["ReplyUserName"]!=null && dt.Rows[n]["ReplyUserName"].ToString()!="")
					{
					model.ReplyUserName=dt.Rows[n]["ReplyUserName"].ToString();
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
		#region 扩展方法
		/// <summary>
		/// 根据状态值获取列表(只获取问题)
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public DataSet GetListEX(int status)
		{
			if (status != -1)
			{
				return GetList("(parentid is null or parentid<0) and State=" + status);
			}
			else
			{
				return GetList("parentid is null or parentid<0");
			}
		}

		public bool SetStatus(string ids, int status)
		{
			return dal.SetStatus(ids, status);
		}
		/// <summary>
		/// 根据状态获取回复
		/// </summary>
		/// <param name="ParentId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public DataSet GetReplyList(int ParentId, int status)
		{
			if (status != -1)
			{
				return GetList("parentid=" + ParentId + " and State=" + status);
			}
			else
			{
				return GetList("parentid="+ParentId);
			}
		}


		public List<Maticsoft.Model.Shop.Products.ProductQA> GetProductQAsByPage(long productId, string orderBy,
																				 int startIndex,
																				 int endIndex)
		{
			DataSet ds = dal.GetListByPage("State=1 and ProductId=" + productId, orderBy, startIndex, endIndex);
			return DataTableToList(ds.Tables[0]);
		}

		#endregion
	}
}

