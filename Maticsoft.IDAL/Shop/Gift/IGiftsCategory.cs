using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Gift
{
	/// <summary>
	/// 接口层GiftsCategory
	/// </summary>
	public interface IGiftsCategory
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int CategoryID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Gift.GiftsCategory model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Gift.GiftsCategory model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int CategoryID);
		bool DeleteList(string CategoryIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Gift.GiftsCategory GetModel(int CategoryID);
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
        #region 扩展方法
        /// <summary>
        /// 添加分类（）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model);
        /// <summary>
        /// 修改分类（）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model);
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
        bool SwapSequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex);
        #endregion
    } 
}
