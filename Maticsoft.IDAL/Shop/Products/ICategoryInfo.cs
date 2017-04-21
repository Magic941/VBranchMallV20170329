/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ICategoryInfo.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:23
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Products
{
	/// <summary>
    /// 接口层CategoryInfo
	/// </summary>
    public interface ICategoryInfo
	{
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int CategoryId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Shop.Products.CategoryInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Products.CategoryInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int CategoryId);
        bool DeleteList(string CategoryIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Products.CategoryInfo GetModel(int CategoryId);
        Maticsoft.Model.Shop.Products.CategoryInfo DataRowToModel(DataRow row);
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

      //  bool CreateCategory(Model.Shop.Products.CategoryInfo model);

        /// <summary>
        /// 获取排序后的分类信息列表
        /// </summary>
        DataSet GetList(string strWhere, bool IsOrder);

        /// <summary>
        /// 对分类信息进行排序
        /// </summary>
        bool SwapCategorySequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex);

	    /// <summary>
	    /// 删除分类信息
	    /// </summary>
	    DataSet DeleteCategory(int categoryId, out int Result);

        /// <summary>
        /// 更新分类信息
        /// </summary>
        bool UpdateCategory(Model.Shop.Products.CategoryInfo model);

        /// <summary>
        /// 判断一个分类下是否存在商品
        /// </summary>
        bool IsExistedProduce(int category);

        /// <summary>
        /// 转移商品
        /// </summary>
        bool DisplaceCategory(int FromCategoryId, int ToCategoryId);
        /// <summary>
        /// 获取NamePath
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetNamePathByPath(string path);

        DataSet GetCategoryListByPath(string path);


	    DataSet GetNameByPid(long productId);

	    int GetMaxSeqByCid(int parentId);

	    int GetDepthByCid(int Cid);

	    bool UpdatePath(Model.Shop.Products.CategoryInfo model);

	    bool UpdateSeqByCid(int Seq,int Cid);

	    bool UpdateDepthAndPath(int Cid, int Depth, string Path);

	    bool UpdateHasChild(int cid,int hasChild);

        bool IsExisted(int parentId, string name, int categoryId);
        DataSet GetGroupCate();
	  
	} 
}
