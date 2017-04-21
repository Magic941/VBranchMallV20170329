/**
* Products.cs
*
* 功 能： N/A
* 类 名： Products
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:49   N/A    初版
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
    /// 接口层商品表
    /// </summary>
    public interface IProducts
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ProductID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        long Add(Maticsoft.Model.SNS.Products model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.Products model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long ProductID);
        bool DeleteList(string ProductIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.Products GetModel(long ProductID);
        Maticsoft.Model.SNS.Products DataRowToModel(DataRow row);
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
        #endregion  成员方法

        #region MethodEx

        DataSet GetProductByPage(string strWhere, string Order, int startIndex, int endIndex);

        bool UpdatePvCount(int ProductID);

        /// <summary>
        /// 删除一条数据（事务删除）
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        bool DeleteEX(int ProductID);

        /// <summary>
        /// 批量删除数据（事务删除）
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        bool DeleteListEX(string ProductIds);

        bool UpdateCateList(string ProductIds, int CateId);

        bool UpdateEX(int ProductId, int CateId);

        bool UpdateRecomendList(string ProductIds, int Recomend);

        bool UpdateRecomend(int ProductId, int Recomend);

        bool UpdateStatus(int ProductId, int Status);

        int GetRecordCountEx(string strWhere, int CateId);

        DataSet GetListEx(string strWhere, int CateId);

        DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex);

        bool UpdateRecommandState(int id, int State);

        /// <summary>
        /// 此人同样的商品是否发布了多遍
        /// </summary>
        bool Exsit(string ProductName, int Uid);
        /// <summary>
        /// 管理员批量插入数据去重
        /// </summary>
        /// <param name="originalID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool Exsit(long originalID, int type);
        /// <summary>
        /// 根据专辑ID获取该用户自定义上传的商品路径
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        DataSet UserUploadProductsImage(int ablumId);
        /// <summary>
        /// 删除商品ids
        /// </summary>
        DataSet DeleteListEx(string Ids, out int Result);
        /// <summary>
        /// 更新导购到淘宝的数量
        /// </summary>
        bool UpdateClickCount(int ProuductId);

        DataSet GetListToStatic(string strWhere);


        bool UpdateStaticUrl(int productId, string staticUrl);

        string GetProductUrl(long productId);

        DataSet GetProductUserIds(string ids);

        bool ExsitUrl(string ProductUrl);

        bool UpdateStatusList(string ids, int status);

        int ImportExcelData(int userid, int albumId, int categoryId, DataTable dt, int status, bool ReRepeat);

        bool ExsitUrl(string ProductUrl, int Uid);

        #endregion MethodEx
    }
}