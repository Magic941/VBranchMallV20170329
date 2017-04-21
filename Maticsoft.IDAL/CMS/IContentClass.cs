using System;
using System.Data;
using System.Collections.Generic;
namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// 接口层ContentClass
	/// </summary>
	public interface IContentClass
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ClassID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.CMS.ContentClass model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.CMS.ContentClass model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ClassID);
        bool DeleteList(string ClassIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.CMS.ContentClass GetModel(int ClassID);
        Maticsoft.Model.CMS.ContentClass DataRowToModel(DataRow row);
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

        #region 批量审核
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere); 
        #endregion

        #region 获取树集合
        /// <summary>
        /// 获取树集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetTreeList(string strWhere); 
        #endregion

        #region 删除分类信息
        /// <summary>
        /// 删除分类信息
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool DeleteCategory(int categoryId); 
        #endregion

        #region 对类别进行排序
        /// <summary>
        /// 对类别进行排序
        /// </summary>
        /// <param name="VideoClassId">类别ID</param>
        /// <param name="zIndex">排序方式</param>
        /// <returns></returns>
        int SwapCategorySequence(int ContentClassId, Maticsoft.Common.Video.SwapSequenceIndex zIndex);
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListByView(string strWhere);
        #endregion

        #region 获得前几行数据
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetListByView(int Top, string strWhere, string filedOrder);
        #endregion
        bool AddExt(Maticsoft.Model.CMS.ContentClass model);

        string GetNamePathByPath(string path);
        #endregion
    } 
}
