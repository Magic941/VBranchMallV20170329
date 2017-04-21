/**
* IVideoClass.cs
*
* 功 能： 
* 类 名： IVideoClass
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
using Maticsoft.Common.Video;
namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// 接口层视频分类表
	/// </summary>
	public interface IVideoClass
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int VideoClassID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.CMS.VideoClass model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.CMS.VideoClass model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int VideoClassID);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="VideoClassIDlist"></param>
        /// <returns></returns>
		bool DeleteList(string VideoClassIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.CMS.VideoClass GetModel(int VideoClassID);
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

        #region 扩展的成员方法
        /// <summary>
        /// 级联删除分类及子类
        /// </summary>
        int DeleteEx(int VideoClassID);
          /// <summary>
        /// 增加一条数据
        /// </summary>
        int AddEx(Maticsoft.Model.CMS.VideoClass model);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListEx(string strWhere, string orderby);
        /// <summary>
        /// 根据ParentID得到一个对象实体
        /// </summary>
        Maticsoft.Model.CMS.VideoClass GetModelByParentID(int ParentID);
         /// <summary>
        /// 得到最大顺序
        /// </summary>
        int GetMaxSequence();
        /// <summary>
        /// 对类别进行排序
        /// </summary>
        /// <param name="VideoClassId">类别ID</param>
        /// <param name="zIndex">排序方式</param>
        /// <returns></returns>
        int SwapCategorySequence(int VideoClassId, SwapSequenceIndex zIndex);
        #endregion
	} 
}
