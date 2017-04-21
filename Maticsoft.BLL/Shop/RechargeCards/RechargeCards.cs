/**  版本信息模板在安装目录下，可自行修改。
* RechargeCards.cs
*
* 功 能： N/A
* 类 名： RechargeCards
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/17 14:27:59   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop;
namespace Maticsoft.BLL.Shop.RechargeCards
{
	/// <summary>
	/// RechargeCards
	/// </summary>
	public partial class RechargeCards
	{
        private readonly IRechargeCards dal = DAShopRechargeCards.CreateRechargeCards();
		public RechargeCards()
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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.RechargeCards model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.RechargeCards model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.RechargeCards GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.RechargeCards GetModelByCache(int ID)
		{
			
			string CacheKey = "RechargeCardsModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.RechargeCards)objModel;
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
		public List<Maticsoft.Model.Shop.RechargeCards> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.RechargeCards> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.RechargeCards> modelList = new List<Maticsoft.Model.Shop.RechargeCards>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.RechargeCards model;
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
		public bool AddEx(Maticsoft.Model.Shop.RechargeCards model, int numberLength, int pwdLength,int Count,string preName)
		{
		    Maticsoft.BLL.Shop.RechargeCards.RechargeCards bllRecharge = new RechargeCards();
            List<string> list=new List<string>();
		    Random random = new Random();
		    if (Count > 0)
		    {
		        //批量生成充值卡
		        int maxValue = 10;
                for (int i = 1; i < numberLength - 4; i++)
                {
                    maxValue =maxValue* 10;
                }
		        int pwdValue = 10;
                for (int i = 1; i < pwdLength; i++)
                {
                    pwdValue = pwdValue*10;
                }
                for (int i = 0; i < Count; i++)
                {
                    int rand = random.Next(maxValue/10 + 1, maxValue - 1);
                    model.Number = preName + DateTime.Now.ToString("MMdd") + rand.ToString();
                    model.Password = random.Next(pwdValue/10 + 1, pwdValue - 1).ToString();
                    while (list.Contains(model.Number))
                    {
                        rand = random.Next(maxValue/10 + 1, maxValue - 1);
                        model.Number = preName + DateTime.Now.ToString("MMdd") + rand.ToString();
                    }
                    list.Add(model.Number);
                    bllRecharge.Add(model);
                }
		        return true;
		    }
            return false;
		}

        public Maticsoft.Model.Shop.RechargeCards GetModelByNumber(string number)
        {
            return dal.ExitEx(number);
        }
		#endregion  ExtensionMethod
	}
}

