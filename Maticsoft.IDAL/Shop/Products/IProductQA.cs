using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Products
{
	/// <summary>
	/// 接口层ProductQA
	/// </summary>
	public interface IProductQA
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int QAId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Products.ProductQA model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.ProductQA model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int QAId);
		bool DeleteList(string QAIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.ProductQA GetModel(int QAId);
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
        bool SetStatus(string ids, int status);
        #endregion
    } 
}
