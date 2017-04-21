using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// PicAd
	/// </summary>
	public partial class PicAd
	{
        private readonly Maticsoft.SQLServerDAL.SNS.PicAd dal = new Maticsoft.SQLServerDAL.SNS.PicAd();
		public PicAd()
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
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

        public DataSet GetList()
        {
            return dal.GetList();
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.PicAd model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.PicAd model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(Idlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.PicAd GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.PicAd GetModelByCache(int Id)
		{
			
			string CacheKey = "PicAdModel-" + Id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						 int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.PicAd)objModel;
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
		public List<Maticsoft.Model.SNS.PicAd> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.PicAd> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.PicAd> modelList = new List<Maticsoft.Model.SNS.PicAd>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.PicAd model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.SNS.PicAd();
					if(dt.Rows[n]["Id"]!=null && dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["Name"]!=null && dt.Rows[n]["Name"].ToString()!="")
					{
					model.Name=dt.Rows[n]["Name"].ToString();
					}
					if(dt.Rows[n]["Src"]!=null && dt.Rows[n]["Src"].ToString()!="")
					{
					model.Src=dt.Rows[n]["Src"].ToString();
					}
					if(dt.Rows[n]["Href"]!=null && dt.Rows[n]["Href"].ToString()!="")
					{
					model.Href=dt.Rows[n]["Href"].ToString();
					}
					if(dt.Rows[n]["Title"]!=null && dt.Rows[n]["Title"].ToString()!="")
					{
					model.Title=dt.Rows[n]["Title"].ToString();
					}
					if(dt.Rows[n]["Alt"]!=null && dt.Rows[n]["Alt"].ToString()!="")
					{
					model.Alt=dt.Rows[n]["Alt"].ToString();
					}
					if(dt.Rows[n]["IsShow"]!=null && dt.Rows[n]["IsShow"].ToString()!="")
					{
						if((dt.Rows[n]["IsShow"].ToString()=="1")||(dt.Rows[n]["IsShow"].ToString().ToLower()=="true"))
						{
						model.IsShow=true;
						}
						else
						{
							model.IsShow=false;
						}
					}
					if(dt.Rows[n]["Orders"]!=null && dt.Rows[n]["Orders"].ToString()!="")
					{
						model.Orders=int.Parse(dt.Rows[n]["Orders"].ToString());
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

