/**
* Categories.cs
*
* 功 能： N/A
* 类 名： Categories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:40   N/A    初版
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
    /// 接口层专辑类型
    /// </summary>
    public interface ICategories
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
        int Add(Maticsoft.Model.SNS.Categories model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.Categories model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int CategoryId);
        bool DeleteList(string CategoryIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.Categories GetModel(int CategoryId);
        Maticsoft.Model.SNS.Categories DataRowToModel(DataRow row);
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

        /// <summary>
        /// 根据根id，获得最底部的孩子节点
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>

        #endregion MethodEx

        #region 扩展方法

        /// <summary>
        /// 添加分类（）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddCategory(Maticsoft.Model.SNS.Categories model);

        /// <summary>
        /// 修改分类（）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCategory(Maticsoft.Model.SNS.Categories model);

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="IsOrder"></param>
        /// <returns></returns>
        DataSet GetCategoryList(string strWhere);

        /// <summary>
        /// 删除分类信息
        /// </summary>
        bool DeleteCategory(int categoryId);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        //  bool SwapSequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex);

        bool AddCategories(Model.SNS.Categories model);

        /// <summary>
        /// 对分类进行排序
        /// </summary>
        bool SwapCategorySequence(int CategoryId, Model.SNS.EnumHelper.SwapSequenceIndex zIndex);




        #endregion 扩展方法
    }
}