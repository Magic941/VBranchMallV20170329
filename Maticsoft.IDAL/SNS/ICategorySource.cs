using System;
using System.Data;
namespace Maticsoft.IDAL.SNS
{
	/// <summary>
	/// 接口层CategorySource
	/// </summary>
	public interface ICategorySource
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int SourceId,int CategoryId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.CategorySource model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.CategorySource model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int SourceId,int CategoryId);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.CategorySource GetModel(int SourceId,int CategoryId);
		Maticsoft.Model.SNS.CategorySource DataRowToModel(DataRow row);
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

		#endregion  MethodEx
        #region 扩展方法
        /// <summary>
        /// 添加分类（）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddCategory(Maticsoft.Model.SNS.CategorySource model);
        /// <summary>
        /// 修改分类（）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCategory(Maticsoft.Model.SNS.CategorySource model);
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
        /// <summary>
        /// 对应淘宝分类ID
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="SNSCateId"></param>
        /// <returns></returns>
        bool  UpdateSNSCate(int CategoryId, int SNSCateId,bool IsLoop);

        bool UpdateSNSCateList(string ids, int SNSCateId, bool IsLoop);

        bool IsUpdate(long CategoryId, string name, int SourceId, int ParentID);
        #endregion
	} 
}
