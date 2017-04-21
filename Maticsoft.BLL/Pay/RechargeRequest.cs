/**  版本信息模板在安装目录下，可自行修改。
* RechargeRequest.cs
*
* 功 能： N/A
* 类 名： RechargeRequest
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/3 14:45:12   N/A    初版
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
using Maticsoft.Model.Pay;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Pay;
namespace Maticsoft.BLL.Pay
{
    /// <summary>
    /// RechargeRequest
    /// </summary>
    public partial class RechargeRequest
    {
        private readonly IRechargeRequest dal = DAPay.CreateRechargeRequest();
        public RechargeRequest()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Pay.RechargeRequest model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Pay.RechargeRequest model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long RechargeId)
        {

            return dal.Delete(RechargeId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string RechargeIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(RechargeIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Pay.RechargeRequest GetModel(long RechargeId)
        {

            return dal.GetModel(RechargeId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Pay.RechargeRequest GetModelByCache(long RechargeId)
        {

            string CacheKey = "RechargeRequestModel-" + RechargeId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RechargeId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Pay.RechargeRequest)objModel;
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
        public List<Maticsoft.Model.Pay.RechargeRequest> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Pay.RechargeRequest> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Pay.RechargeRequest> modelList = new List<Maticsoft.Model.Pay.RechargeRequest>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Pay.RechargeRequest model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(Model.Pay.RechargeRequest reModel)
        {
            if (reModel == null)
            {
                return false;
            }
            Members.UsersExp userBll = new Members.UsersExp();
            decimal balance = userBll.GetUserBalance(reModel.UserId);
            decimal rechargeRatio = SysManage.ConfigSystem.GetDecimalValueByCache("Shop_RechargeRatio");//比例
            decimal rechargeMoney = reModel.RechargeBlance;//充值金额
            if (rechargeRatio > decimal.MinusOne)//设置了比例值
            {
                rechargeMoney = reModel.RechargeBlance * rechargeRatio;//充值后得到的金额
            }
            return dal.UpdateStatus(reModel, balance, rechargeMoney);
        }

        /// <summary>
        /// 线下充值
        /// </summary>
        /// <param name="rechargeMoney">充值后得到的金额</param>
        public bool OfflineRecharge(int userId, decimal rechargeMoney)
        {
            Members.UsersExp userBll = new Members.UsersExp();
            decimal balance = userBll.GetUserBalance(userId);

            #region 线下充值不使用充值比例
            //decimal rechargeRatio = SysManage.ConfigSystem.GetDecimalValueByCache("Shop_RechargeRatio");//比例
            //if (rechargeRatio > decimal.MinusOne) //设置了比例值
            //{
            //    rechargeMoney = rechargeMoney * rechargeRatio;//充值后得到的金额
            //}
            #endregion

            return dal.OfflineRecharge(userId, balance, rechargeMoney, rechargeMoney > 0 ? "线下充值" : "线下消费");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.Pay.RechargeRequest> GetRechargeListByPage(string strWhere, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(strWhere, " RechargeId desc ", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表 与users表内联
        /// </summary>
        public DataSet GetListEx(string strWhere, string filedOrder)
        {
            return dal.GetListEx(strWhere, filedOrder);
        }

        public int GetTotalCount(string startTime, string endTime)
        {
            return dal.GetTotalcount(startTime, endTime);
        }
        public int GetTotalAmount(string startTime, string endTime)
        {
            return dal.GetTotalAmount(startTime, endTime);
        }
        #endregion  ExtensionMethod


    }
}

