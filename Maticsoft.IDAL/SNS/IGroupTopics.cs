/**
* GroupTopics.cs
*
* 功 能： N/A
* 类 名： GroupTopics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:43   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.SNS
{
	/// <summary>
	/// 接口层小组话题表
	/// </summary>
	public interface IGroupTopics
	{
		#region  成员方法
        bool Exists(int TopicID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.GroupTopics model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.GroupTopics model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int TopicID);
		bool DeleteList(string TopicIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.GroupTopics GetModel(int TopicID);
		Maticsoft.Model.SNS.GroupTopics DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx
         /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteEx(int TopicID);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetListEx(int Top, string strWhere, string filedOrder);
        /// <summary>
        /// 增加一个帖子
        /// </summary>
        int AddEx(Maticsoft.Model.SNS.GroupTopics Tmodel, Maticsoft.Model.SNS.Products PModel);

        /// <summary>
        /// 批量审核
        /// </summary>
        bool UpdateStatusList(string IdsStr, Maticsoft.Model.SNS.EnumHelper.TopicStatus status);


        /// <summary>
        /// 更新推荐状态
        /// </summary>
        bool UpdateRecommand(int TopicId, int Recommand);

	    bool UpdateAdminRecommand(int TopicId, bool IsAdmin);
        /// <summary>
        /// 更新pvcount
        /// </summary>
        bool UpdatePVCount(int TopicId);

           /// <summary>
        /// 删除数据返回imageurl
        /// </summary>
         bool DeleteEx(int TopicID, out string ImageUrl);
		#endregion  MethodEx
	} 
}
