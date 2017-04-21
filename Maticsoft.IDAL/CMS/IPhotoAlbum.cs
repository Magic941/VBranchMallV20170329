using System.Data;
using Maticsoft.Model.CMS;

namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// 接口层PhotoAlbum
	/// </summary>
	public interface IPhotoAlbum
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
        /// <summary>
        /// 得到最大Sequence
        /// </summary>
        int GetMaxSequence();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int AlbumID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(PhotoAlbum model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(PhotoAlbum model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int AlbumID);
		bool DeleteList(string AlbumIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		PhotoAlbum GetModel(int AlbumID);
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

        #region Extension Method

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
	    int GetRecordCount(string strWhere);

	    /// <summary>
	    /// 分页获取数据
	    /// </summary>
	    /// <param name="strWhere">查询条件</param>
	    /// <param name="orderby">排序字段</param>
	    /// <param name="startIndex">开始索引</param>
	    /// <param name="endIndex">结束索引</param>
	    /// <returns></returns>
	    DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

	    #endregion
	} 
}
