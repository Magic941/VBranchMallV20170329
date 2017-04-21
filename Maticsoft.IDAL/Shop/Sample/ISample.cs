using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Sample
{
	/// <summary>
	/// 接口层Sample
	/// </summary>
	public interface ISample
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int SampleId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Sample.Sample model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Sample.Sample model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int SampleId);
		bool DeleteList(string SampleIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Sample.Sample GetModel(int SampleId);
		Maticsoft.Model.Shop.Sample.Sample DataRowToModel(DataRow row);
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
	} 
}
