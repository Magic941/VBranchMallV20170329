/**
* IVideo.cs
*
* 功 能： 
* 类 名： IVideo
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/22 16:28:49  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// 接口层视频信息表
	/// </summary>
	public interface IVideo
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int VideoID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.CMS.Video model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.CMS.Video model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int VideoID);
		bool DeleteList(string VideoIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.CMS.Video GetModel(int VideoID);
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

        #region MethodEx

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListEx(string strWhere, string orderby);
         /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.CMS.Video GetModelEx(int VideoID);
        #region 批量处理
        /// <summary>
        /// 批量处理
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere);
        #endregion
        /// <summary>
        /// 得到最大顺序
        /// </summary>
        int GetMaxSequence();
        #endregion
	} 
}
