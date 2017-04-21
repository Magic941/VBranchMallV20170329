using System.Data;
using Maticsoft.Model.CMS;

namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// 接口层PhotoClass
	/// </summary>
	public interface IPhotoClass
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
        /// <summary>
		/// 得到最大ID
		/// </summary>
        int GetMaxSequence();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ClassID);

	    /// <summary>
	    /// 是否存在该记录
	    /// </summary>
	    bool ExistsByClassName(string ClassName);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		void Add(PhotoClass model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(PhotoClass model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ClassID);
		bool DeleteList(string ClassIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		PhotoClass GetModel(int ClassID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	} 
}
