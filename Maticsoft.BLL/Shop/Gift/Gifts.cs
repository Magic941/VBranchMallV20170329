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
	/// Gifts
	/// </summary>
	public partial class Gifts
	{
        private readonly IGifts dal =DAShopGifts.CreateGifts();
		public Gifts()
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
		public bool Exists(int GiftId)
		{
			return dal.Exists(GiftId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Gift.Gifts model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Gift.Gifts model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int GiftId)
		{
			
			return dal.Delete(GiftId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string GiftIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(GiftIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Gift.Gifts GetModel(int GiftId)
		{
			
			return dal.GetModel(GiftId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Gift.Gifts GetModelByCache(int GiftId)
		{
			
			string CacheKey = "GiftsModel-" + GiftId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GiftId);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Gift.Gifts)objModel;
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
		public List<Maticsoft.Model.Shop.Gift.Gifts> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Gift.Gifts> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Gift.Gifts> modelList = new List<Maticsoft.Model.Shop.Gift.Gifts>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Gift.Gifts model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Shop.Gift.Gifts();
					if(dt.Rows[n]["GiftId"]!=null && dt.Rows[n]["GiftId"].ToString()!="")
					{
						model.GiftId=int.Parse(dt.Rows[n]["GiftId"].ToString());
					}
					if(dt.Rows[n]["CategoryID"]!=null && dt.Rows[n]["CategoryID"].ToString()!="")
					{
						model.CategoryID=int.Parse(dt.Rows[n]["CategoryID"].ToString());
					}
					if(dt.Rows[n]["Name"]!=null && dt.Rows[n]["Name"].ToString()!="")
					{
					model.Name=dt.Rows[n]["Name"].ToString();
					}
					if(dt.Rows[n]["ShortDescription"]!=null && dt.Rows[n]["ShortDescription"].ToString()!="")
					{
					model.ShortDescription=dt.Rows[n]["ShortDescription"].ToString();
					}
					if(dt.Rows[n]["Unit"]!=null && dt.Rows[n]["Unit"].ToString()!="")
					{
					model.Unit=dt.Rows[n]["Unit"].ToString();
					}
					if(dt.Rows[n]["Weight"]!=null && dt.Rows[n]["Weight"].ToString()!="")
					{
						model.Weight=int.Parse(dt.Rows[n]["Weight"].ToString());
					}
					if(dt.Rows[n]["LongDescription"]!=null && dt.Rows[n]["LongDescription"].ToString()!="")
					{
					model.LongDescription=dt.Rows[n]["LongDescription"].ToString();
					}
					if(dt.Rows[n]["Title"]!=null && dt.Rows[n]["Title"].ToString()!="")
					{
					model.Title=dt.Rows[n]["Title"].ToString();
					}
					if(dt.Rows[n]["Meta_Description"]!=null && dt.Rows[n]["Meta_Description"].ToString()!="")
					{
					model.Meta_Description=dt.Rows[n]["Meta_Description"].ToString();
					}
					if(dt.Rows[n]["Meta_Keywords"]!=null && dt.Rows[n]["Meta_Keywords"].ToString()!="")
					{
					model.Meta_Keywords=dt.Rows[n]["Meta_Keywords"].ToString();
					}
					if(dt.Rows[n]["ThumbnailsUrl"]!=null && dt.Rows[n]["ThumbnailsUrl"].ToString()!="")
					{
					model.ThumbnailsUrl=dt.Rows[n]["ThumbnailsUrl"].ToString();
					}
					if(dt.Rows[n]["InFocusImageUrl"]!=null && dt.Rows[n]["InFocusImageUrl"].ToString()!="")
					{
					model.InFocusImageUrl=dt.Rows[n]["InFocusImageUrl"].ToString();
					}
					if(dt.Rows[n]["CostPrice"]!=null && dt.Rows[n]["CostPrice"].ToString()!="")
					{
						model.CostPrice=decimal.Parse(dt.Rows[n]["CostPrice"].ToString());
					}
					if(dt.Rows[n]["MarketPrice"]!=null && dt.Rows[n]["MarketPrice"].ToString()!="")
					{
						model.MarketPrice=decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
					}
					if(dt.Rows[n]["SalePrice"]!=null && dt.Rows[n]["SalePrice"].ToString()!="")
					{
						model.SalePrice=decimal.Parse(dt.Rows[n]["SalePrice"].ToString());
					}
					if(dt.Rows[n]["Stock"]!=null && dt.Rows[n]["Stock"].ToString()!="")
					{
						model.Stock=int.Parse(dt.Rows[n]["Stock"].ToString());
					}
					if(dt.Rows[n]["NeedPoint"]!=null && dt.Rows[n]["NeedPoint"].ToString()!="")
					{
						model.NeedPoint=int.Parse(dt.Rows[n]["NeedPoint"].ToString());
					}
					if(dt.Rows[n]["NeedGrade"]!=null && dt.Rows[n]["NeedGrade"].ToString()!="")
					{
						model.NeedGrade=int.Parse(dt.Rows[n]["NeedGrade"].ToString());
					}
					if(dt.Rows[n]["SaleCounts"]!=null && dt.Rows[n]["SaleCounts"].ToString()!="")
					{
						model.SaleCounts=int.Parse(dt.Rows[n]["SaleCounts"].ToString());
					}
					if(dt.Rows[n]["CreateDate"]!=null && dt.Rows[n]["CreateDate"].ToString()!="")
					{
						model.CreateDate=DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
					}
					if(dt.Rows[n]["Enabled"]!=null && dt.Rows[n]["Enabled"].ToString()!="")
					{
						if((dt.Rows[n]["Enabled"].ToString()=="1")||(dt.Rows[n]["Enabled"].ToString().ToLower()=="true"))
						{
						model.Enabled=true;
						}
						else
						{
							model.Enabled=false;
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
        public bool UpdateStock(int giftid, int stock)
        {
            return dal.UpdateStock(giftid, stock);
        }
        #endregion
    }
}

