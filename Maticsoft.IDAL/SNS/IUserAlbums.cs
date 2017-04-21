/**
* UserAlbums.cs
*
* 功 能： N/A
* 类 名： UserAlbums
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:01   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Data;
using System.Collections.Generic;

namespace Maticsoft.IDAL.SNS
{
    /// <summary>
    /// 接口层用户专辑表
    /// </summary>
    public interface IUserAlbums
    {
        #region 成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int CreatedUserID, string AlbumName);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.SNS.UserAlbums model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.UserAlbums model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int AlbumID);

        bool DeleteList(string AlbumIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.UserAlbums GetModel(int AlbumID);

        Maticsoft.Model.SNS.UserAlbums DataRowToModel(DataRow row);

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
        /// 根据类型获得首页推荐的专辑
        /// </summary>
        DataSet GetListForIndex(int TypeID, int Top, string orderby,int RecommandType=-1);

        /// <summary>
        /// 根据类型获得首页推荐的专辑（少于9个不能推荐）此代码后期和上面的代码合并
        /// </summary>
        DataSet GetListForIndexEx(int TypeID, int Top, string orderby);

        /// <summary>
        /// 根据类型获得分页的专辑
        /// </summary>
        DataSet GetListForPage(int TypeID, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 根据类型获得分页的专辑（专辑的数量条件是超过9个）
        /// </summary>
        DataSet GetListForPageEx(int TypeID, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 根据类型获得记录总数
        /// </summary>
        int GetRecordCount(int TypeID);

        /// 根据用户专辑里面的一张图片获得相应的专辑的信息s
        Maticsoft.Model.SNS.UserAlbums GetUserAlbum(int type, int pid, int UserId);

        /// <summary>
        /// 更新部分数据
        /// </summary>
        bool UpdateEx(Maticsoft.Model.SNS.UserAlbums model);

        /// <summary>
        /// 删除专辑
        /// </summary>
        bool DeleteEx(int AlbumID, int TypeId, int UserId);

        /// <summary>
        /// 增加专辑
        /// </summary>
        int AddEx(Maticsoft.Model.SNS.UserAlbums model, int TypeId);

        /// <summary>
        /// 用户收藏的专辑
        /// </summary>
        DataSet GetUserFavAlbum(int UserId);

        bool UpdatePhotoCount();

        /// <summary>
        /// 更改pvcount
        /// </summary>
        /// <returns></returns>
        bool UpdatePvCount(int AlbumId);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool UpdateIsRecommand(int IsRecommand, string IdList);

        /// <summary>
        /// 根据专辑ID删除专辑信息
        /// </summary>
        /// <param name="albumId">专辑ID</param>
        bool DeleteAblumAction(int albumId);

        /// <summary>
        /// 更新专辑推荐状态
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <param name="Recommand">推荐状态</param>
        bool UpdateRecommand(int ablumId, Model.SNS.EnumHelper.RecommendType recommendType);


        /// <summary>
        /// 更新评论数量
        /// </summary>
        /// <param name="ablumId"></param>
        /// <returns></returns>
        bool UpdateCommentCount(int ablumId);

      
        #endregion MethodEx
    }
}