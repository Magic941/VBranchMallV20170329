/**
* Comments.cs
*
* 功 能： N/A
* 类 名： Comments
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Data;

namespace Maticsoft.IDAL.SNS
{
    /// <summary>
    /// 接口层评论表
    /// </summary>
    public interface IComments
    {
        #region 成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.SNS.Comments model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.Comments model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int CommentID);

        bool DeleteList(string CommentIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.Comments GetModel(int CommentID);

        Maticsoft.Model.SNS.Comments DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

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

        #endregion 成员方法

        #region MethodEx

        /// <summary>
        ///  /// 增加一条新的评论，给对应的type的评论数量相应的加1；
        /// </summary>

        int AddEx(Maticsoft.Model.SNS.Comments ComModel);

        /// <summary>
        /// 专辑评论信息
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        DataSet AblumComment(int ablumId, string strWhere);


                /// <summary>
        /// 删除专辑评论
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <param name="commentId">评论ID</param>
        /// <returns>是否删除成功</returns>
        bool DeleteComment(int ablumId, int commentId);

        /// <summary>
        /// 删除评论
        /// </summary>
        bool DeleteListEx(string CommentIDlist);
        #endregion MethodEx
    }
}