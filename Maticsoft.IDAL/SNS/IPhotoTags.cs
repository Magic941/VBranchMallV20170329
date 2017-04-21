using System;
using System.Data;
namespace Maticsoft.IDAL.SNS
{
	/// <summary>
	/// 接口层PhotoTags
	/// </summary>
	public interface IPhotoTags
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int TagID);
        /// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(int TagID, string TagName); 
        /// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string TagName);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.PhotoTags model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.PhotoTags model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int TagID);
		bool DeleteList(string TagIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.PhotoTags GetModel(int TagID);
		Maticsoft.Model.SNS.PhotoTags DataRowToModel(DataRow row);
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
        /// <summary>
        /// 更新状态
        /// </summary>
        bool UpdateStatus(int Status, string IdList);

        DataSet GetHotTags(int top);
		#endregion  成员方法
	} 
}
