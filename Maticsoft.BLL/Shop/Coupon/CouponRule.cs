/**
* CouponRule.cs
*
* 功 能： N/A
* 类 名： CouponRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:21:01   N/A    初版
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
using Maticsoft.Common;
using Maticsoft.Model.Shop.Coupon;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Coupon;
namespace Maticsoft.BLL.Shop.Coupon
{
	/// <summary>
	/// CouponRule
	/// </summary>
	public partial class CouponRule
	{
        private readonly ICouponRule dal = DAShopCoupon.CreateCouponRule();
		public CouponRule()
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
		public bool Exists(int RuleId)
		{
			return dal.Exists(RuleId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Coupon.CouponRule model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Coupon.CouponRule model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int RuleId)
		{
			
			return dal.Delete(RuleId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string RuleIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(RuleIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Coupon.CouponRule GetModel(int RuleId)
		{
			
			return dal.GetModel(RuleId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Coupon.CouponRule GetModelByCache(int RuleId)
		{
			
			string CacheKey = "CouponRuleModel-" + RuleId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RuleId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Coupon.CouponRule)objModel;
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
		public List<Maticsoft.Model.Shop.Coupon.CouponRule> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Coupon.CouponRule> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Coupon.CouponRule> modelList = new List<Maticsoft.Model.Shop.Coupon.CouponRule>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Coupon.CouponRule model;
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
	    public bool AddEx(Maticsoft.Model.Shop.Coupon.CouponRule model)
	    {
	        int ruleId = Add(model);
	        if (ruleId > 0 && model.Type == 1)
	        {
                return true;
	        }

	        if (ruleId > 0&&model.SendCount>0)
	        {
                //批量生成优惠券
                Maticsoft.BLL.Shop.Coupon.CouponInfo couponBll=new CouponInfo();
	            Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = new Model.Shop.Coupon.CouponInfo();
	            infoModel.CategoryId = model.CategoryId;
	            infoModel.ClassId = model.ClassId;
	            infoModel.RuleId = ruleId;
	            infoModel.CouponName = model.Name;
	            infoModel.CouponPrice = model.CouponPrice;
                Random rnd = new Random();
	            infoModel.EndDate = model.EndDate;
	            infoModel.StartDate = model.StartDate;
	            infoModel.Status = 0;
	            infoModel.GenerateTime = DateTime.Now;
	            infoModel.IsPwd = model.IsPwd;
	            infoModel.IsReuse = model.IsReuse;
	            infoModel.LimitPrice = model.LimitPrice;
	            infoModel.SupplierId = model.SupplierId;
	            infoModel.NeedPoint = model.NeedPoint;
                infoModel.UseType = model.UseType;
                infoModel.AutoState = model.AutoState;
                infoModel.CouponPwd = "";
                for (int i = 0; i < model.SendCount; i++)
                {
                    string code = i.ToString().PadLeft(6, '0');
                    infoModel.CouponCode = model.PreName + DateTime.Now.ToString("yyMMddHHmmss") + code;
                    couponBll.Add(infoModel);
                }
	            return true;
	        }
	        return false;
	    }

        public bool UpdateEx(Maticsoft.Model.Shop.Coupon.CouponRule model,int sendCount)
        {
            model.SendCount = model.SendCount + sendCount;
            bool IsSuccess = Update(model);
            if (IsSuccess && model.Type == 1)
            {
                return true;
            }

            if (IsSuccess && sendCount > 0)
            {
                List<string> codeList = new List<string>();
                //批量生成优惠券
                Maticsoft.BLL.Shop.Coupon.CouponInfo couponBll = new CouponInfo();
                Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = new Model.Shop.Coupon.CouponInfo();
                infoModel.CategoryId = model.CategoryId;
                infoModel.ClassId = model.ClassId;
                infoModel.RuleId = model.RuleId;
                infoModel.CouponName = model.Name;
                infoModel.CouponPrice = model.CouponPrice;
                Random rnd = new Random();
                infoModel.EndDate = model.EndDate;
                infoModel.StartDate = model.StartDate;
                infoModel.Status = 0;
                infoModel.GenerateTime = DateTime.Now;
                infoModel.IsPwd = model.IsPwd;
                infoModel.IsReuse = model.IsReuse;
                infoModel.LimitPrice = model.LimitPrice;
                infoModel.SupplierId = model.SupplierId;
                infoModel.NeedPoint = model.NeedPoint;
                int maxValue = 10;
                for (int j = 1; j < model.CpLength - 4; j++)
                {
                    maxValue = maxValue * 10;
                }
                int pwdValue = 10;
                for (int k = 1; k < model.PwdLength; k++)
                {
                    pwdValue = pwdValue * 10;
                }
                for (int i = 0; i < sendCount; i++)
                {
                    int rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                    infoModel.CouponCode = model.PreName + DateTime.Now.ToString("MMdd") +
                                           rand.ToString();
                    infoModel.CouponPwd = infoModel.IsPwd == 1 ? rnd.Next(pwdValue / 10, pwdValue - 1).ToString() : "";
                    while (codeList.Contains(infoModel.CouponCode))
                    {
                        rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                        infoModel.CouponCode = model.PreName + DateTime.Now.ToString("MMdd") + rand.ToString();
                    }
                    codeList.Add(infoModel.CouponCode);
                    couponBll.Add(infoModel);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 级联删除
        /// </summary>
        public bool DeleteEx(int RuleId)
        {
            return dal.DeleteEx(RuleId);
        }
        /// <summary>
        /// 生成积分兑换券
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
	    public bool GenCoupon(Maticsoft.Model.Shop.Coupon.CouponRule model,int userId)
	    {
              Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = new Model.Shop.Coupon.CouponInfo();
            Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll=new CouponInfo();
	            infoModel.CategoryId = model.CategoryId;
	            infoModel.ClassId = model.ClassId;
	            infoModel.RuleId = model.RuleId;
	            infoModel.CouponName = model.Name;
	            infoModel.CouponPrice = model.CouponPrice;
                Random rnd = new Random();
	            infoModel.EndDate = model.EndDate;
	            infoModel.StartDate = model.StartDate;
	            infoModel.Status = 0;
	            infoModel.GenerateTime = DateTime.Now;
	            infoModel.IsPwd = model.IsPwd;
	            infoModel.IsReuse = model.IsReuse;
	            infoModel.LimitPrice = model.LimitPrice;
	            infoModel.SupplierId = model.SupplierId;
	            infoModel.NeedPoint = model.NeedPoint;
            infoModel.UserId = userId;
            infoModel.Status = 1;
                int maxValue = 10;
                for (int j = 1; j < model.CpLength - 4; j++)
                {
                    maxValue = maxValue * 10;
                }
                int pwdValue = 10;
                for (int k = 1; k < model.PwdLength; k++)
                {
                    pwdValue = pwdValue * 10;
                }
                    int rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                    infoModel.CouponCode = model.PreName +userId+ DateTime.Now.ToString("MMdd") +
                                           rand.ToString();
                    infoModel.CouponPwd =infoModel.IsPwd==1? rnd.Next(pwdValue/10, pwdValue - 1).ToString():"";
                    while (infoBll.Exists(infoModel.CouponPwd))
                    {
                        rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                         infoModel.CouponCode = model.PreName +userId+ DateTime.Now.ToString("MMdd") +
                                           rand.ToString();
                    }


            return dal.GenCoupon(infoModel);
	    }


        public int ImportExcelData(Maticsoft.Model.Shop.Coupon.CouponRule ruleModel, bool IsDate, bool IsPrice, bool IsLimitPrice, DataTable dt)
        {
            int count = 0;
            if (dt.Rows.Count > 0)
            {
                count = dal.ImportExcelData(ruleModel, IsDate, IsPrice, IsLimitPrice, dt);
            }
            return count;
        }

	    #endregion  ExtensionMethod
	}
}

