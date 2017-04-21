/**
* Posts.cs
*
* 功 能： N/A
* 类 名： Posts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:47   N/A    初版
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
	/// 接口层动态表
	/// </summary>
	public interface IPosts
	{
		#region  成员方法
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.Posts model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.Posts model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int PostID);
		bool DeleteList(string PostIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.Posts GetModel(int PostID);
		Maticsoft.Model.SNS.Posts DataRowToModel(DataRow row);
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
        /// 更新被转发的数量
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        int UpdateForwardCount(string StrWhere);

       /// <summary>
       /// 增加一条转发的动态
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        int AddForwardPost(Maticsoft.Model.SNS.Posts model);
        /// <summary>
        /// 发布一条动态，包括3中类型
        /// </summary>
        Maticsoft.Model.SNS.Posts AddPost(Maticsoft.Model.SNS.Posts model, int AlbumId, long Pid, int PhotoCateId, Maticsoft.Model.SNS.Products PModel, int RecommandStateInt, string photoAdress, string mapLng, string mapLat, bool CreatePost);
        /// <summary>
        /// 添加长微博
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Desc"></param>
        /// <returns></returns>
        Maticsoft.Model.SNS.Posts AddBlogPost(Maticsoft.Model.SNS.Posts model, Maticsoft.Model.SNS.UserBlog blogModel, bool CreatePost);
 
        
        /// <summary>
        /// 动态状态置为删除
        /// </summary>
        bool UpdateToDel(int PostID);
         #region 删除单个评论信息
        /// <summary>
        /// 删除单个评论信息
        /// </summary>
        /// <param name="PostID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        bool DeleteEx(int PostID);


        /// <summary>
        /// 更新状态
        /// </summary>
        bool UpdateStatusList(string PostIds, int Status);
        #endregion
        /// <summary>
        /// 删除一般动态
        /// </summary>
        bool DeleteListByNormalPost(string PostIDs);


	    bool UpdateFavCount(int postId);

	    bool UpdateCommentCount(int postId);

	    DataSet GetPostUserIds(string ids);

	    Maticsoft.Model.SNS.Posts AddProductPost(Maticsoft.Model.SNS.Products PModel, int AblumId,bool CreatePost);

	    #endregion  MethodEx
	} 
}
