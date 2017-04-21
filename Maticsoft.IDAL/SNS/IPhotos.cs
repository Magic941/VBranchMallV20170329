/**
* Photos.cs
*
* 功 能： N/A
* 类 名： Photos
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

using System.Data;
using System.Collections.Generic;

namespace Maticsoft.IDAL.SNS
{
    /// <summary>
    /// 接口层照片表
    /// </summary>
    public interface IPhotos
    {
        #region 成员方法
        bool Exists(int PhotoID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.SNS.Photos model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.Photos model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int PhotoID);

        bool DeleteList(string PhotoIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.Photos GetModel(int PhotoID);

        Maticsoft.Model.SNS.Photos DataRowToModel(DataRow row);

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
        /// <summary>
        /// 删除图片ids
        /// </summary>
        DataSet DeleteListEx(string Ids, out int Result);
        #endregion 成员方法

        #region MethodEx

        bool UpdatePvCount(int ProductID);

        /// <summary>
        /// 删除一条数据（事务删除）
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        bool DeleteEX(int PhotoID);

        /// <summary>
        /// 批量删除数据（事务删除）
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        bool DeleteListEX(string PhotoIds);

        bool UpdateCateList(string PhotoIds, int CateId);

        DataSet GetZuiInList(int Type, int Top);

        bool UpdateRecomend(int PhotoID, int Recomend);

        bool UpdateStatus(int PhotoID, int Status);

        bool UpdateRecommandState(int id, int State);

        
        /// <summary>
        /// 根据专辑ID获取该用户自定义上传的照片路径
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        DataSet UserUploadPhoto(int ablumId);

        DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex);

        DataSet GetListEx(string strWhere, int CateId);

        int GetRecordCountEx(string strWhere, int CateId);

        bool UpdateRecomendList(string PhotoIds, int Recomend);

        DataSet GetListToReGen(string strWhere);

        bool UpdateStaticUrl(int photoId, string staticUrl);

        int GetPrevID(int photoId, int albumId);

        int GetNextID(int photoId, int albumId);

        int GetCountEx(int type, int categoryId, string address);

        DataSet GetListByPageEx(int type, int categoryId, string address, string orderby, int startIndex, int endIndex);

        DataSet GetPhotoUserIds(string ids);

        #endregion MethodEx
    }
}