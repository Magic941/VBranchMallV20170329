/**
* GroupBuy.cs
*
* 功 能： N/A
* 类 名： GroupBuy
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/14 15:51:55   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
namespace Maticsoft.IDAL.Shop.PromoteSales
{
	/// <summary>
	/// 接口层GroupBuy
	/// </summary>
	public interface IGroupBuy
	{
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int GroupBuyId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Shop.PromoteSales.GroupBuy model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.PromoteSales.GroupBuy model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int GroupBuyId);
        bool DeleteList(string GroupBuyIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.PromoteSales.GroupBuy GetModel(int GroupBuyId);
        Maticsoft.Model.Shop.PromoteSales.GroupBuy DataRowToModel(DataRow row);
        Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory DataRow2Model(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获取今天开始的数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetListToday(string strWhere);
        
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法

	    #region  MethodEx
        int MaxSequence();
        
        bool IsExists(long ProductId);
        bool UpdateStatus(string ids, int status);
        int EditStatus();
	    bool UpdateBuyCount(int buyId, int count);
        DataSet GetListByPage(string strWhere, int cid, int regionId, string orderby, int startIndex, int endIndex, int promotionType);//分页获取数据
	    int GetCount(string strWhere, int regionId,bool IsForMember);//得到总数量
        int GetCountHaoLi(string curDate);//得到总数量, 主要是日期做为查询条件
        int Insert2GroupBuy(List<string> GroupBuyIdList, DateTime StartDate, DateTime EndDate);
        int BulkInsert2ShopGroup(List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> GroupBuyList);
	    DataSet GetCategory(string strWhere);//为了取分类数据
        DataSet GetGroupbyCategory(string strWhere);//为了取团购分类数据
        DataSet GetGroupBuyHot();
        Maticsoft.Model.Shop.PromoteSales.GroupBuy GetPromotionLimitQu(int productId, int PromotionType = 1);
        bool UpdateGroupBuyCountByProductID(long productid, int qty);
        DataSet GetGroupBuyLimt4Hour();
        DataSet GetGroupbuyHistory(int groupbuyid);
        DataSet GetGroupBuyListByPage(int groupbuyid, int startIndex, int endIndex);

	    #endregion  MethodEx
	} 
}
