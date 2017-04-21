using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Gift;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Gift;
namespace Maticsoft.BLL.Shop.Gift
{
	/// <summary>
	/// GiftsCategory
	/// </summary>
	public partial class GiftsCategory
	{
        private readonly IGiftsCategory dal = DAShopGifts.CreateGiftsCategory();
		public GiftsCategory()
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
		public bool Exists(int CategoryID)
		{
			return dal.Exists(CategoryID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Gift.GiftsCategory model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Gift.GiftsCategory model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CategoryID)
		{
			
			return dal.Delete(CategoryID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CategoryIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(CategoryIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Gift.GiftsCategory GetModel(int CategoryID)
		{
			
			return dal.GetModel(CategoryID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Gift.GiftsCategory GetModelByCache(int CategoryID)
		{
			
			string CacheKey = "GiftsCategoryModel-" + CategoryID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CategoryID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Gift.GiftsCategory)objModel;
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
		public List<Maticsoft.Model.Shop.Gift.GiftsCategory> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Gift.GiftsCategory> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Gift.GiftsCategory> modelList = new List<Maticsoft.Model.Shop.Gift.GiftsCategory>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Gift.GiftsCategory model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Shop.Gift.GiftsCategory();
					if(dt.Rows[n]["CategoryID"]!=null && dt.Rows[n]["CategoryID"].ToString()!="")
					{
						model.CategoryID=int.Parse(dt.Rows[n]["CategoryID"].ToString());
					}
					if(dt.Rows[n]["ParentCategoryId"]!=null && dt.Rows[n]["ParentCategoryId"].ToString()!="")
					{
						model.ParentCategoryId=int.Parse(dt.Rows[n]["ParentCategoryId"].ToString());
					}
					if(dt.Rows[n]["Name"]!=null && dt.Rows[n]["Name"].ToString()!="")
					{
					model.Name=dt.Rows[n]["Name"].ToString();
					}
					if(dt.Rows[n]["Depth"]!=null && dt.Rows[n]["Depth"].ToString()!="")
					{
						model.Depth=int.Parse(dt.Rows[n]["Depth"].ToString());
					}
					if(dt.Rows[n]["Path"]!=null && dt.Rows[n]["Path"].ToString()!="")
					{
					model.Path=dt.Rows[n]["Path"].ToString();
					}
					if(dt.Rows[n]["DisplaySequence"]!=null && dt.Rows[n]["DisplaySequence"].ToString()!="")
					{
						model.DisplaySequence=int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
					}
					if(dt.Rows[n]["Description"]!=null && dt.Rows[n]["Description"].ToString()!="")
					{
					model.Description=dt.Rows[n]["Description"].ToString();
					}
					if(dt.Rows[n]["Theme"]!=null && dt.Rows[n]["Theme"].ToString()!="")
					{
					model.Theme=dt.Rows[n]["Theme"].ToString();
					}
					if(dt.Rows[n]["HasChildren"]!=null && dt.Rows[n]["HasChildren"].ToString()!="")
					{
						if((dt.Rows[n]["HasChildren"].ToString()=="1")||(dt.Rows[n]["HasChildren"].ToString().ToLower()=="true"))
						{
						model.HasChildren=true;
						}
						else
						{
							model.HasChildren=false;
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
        /// 判断分类下是否存在礼品
        /// </summary>
        public bool IsExistedGift(int categoryid)
        {
           Maticsoft.BLL.Shop.Gift.Gifts giftBll = new Gifts();
            int count= giftBll.GetRecordCount("CategoryID=" + categoryid);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Maticsoft.Model.Shop.Gift.GiftsCategory> GetCategorysByDepth(int depth)
        {
            //ADD Cache
            return GetModelList("Depth = " + depth);
        }

        public DataSet GetCategorysByParentId(int parentCategoryId)
        {
            //ADD Cache
            return GetList("ParentCategoryId = " + parentCategoryId);
        }
        /// <summary>
        /// 添加分类（更新树形结构）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            return dal.AddCategory(model);
        }
        /// <summary>
        /// 更新分类(更新树形结构)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            return dal.UpdateCategory(model);
        }
        /// <summary>
        /// 根据条件获取分类列表（是否排序）
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="IsOrder"></param>
        /// <returns></returns>
        public DataSet GetCategoryList(string strWhere)
        {
            return dal.GetCategoryList(strWhere);
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public bool DeleteCategory(int categoryId)
        {
            return dal.DeleteCategory(categoryId);
        }

        /// <summary>
        /// 对分类信息进行排序
        /// </summary>
        public bool SwapSequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex)
        {
            return dal.SwapSequence(CategoryId, zIndex);
        }
        #endregion
    }
}

