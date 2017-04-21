using System.Data;

namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// 接口层Photo
	/// </summary>
	public interface IPhoto
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
		bool Exists(int PhotoID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Model.CMS.Photo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Model.CMS.Photo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int PhotoID);

	    bool DeleteList(string PhotoIDlist, out DataSet imageList);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.CMS.Photo GetModel(int PhotoID);
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

        #region Extension Method
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
	    /// <param name="strWhere">查询条件</param>
	    /// <param name="orderby">排序字段</param>
	    /// <param name="startIndex">开始索引</param>
	    /// <param name="endIndex">结束索引</param>
	    /// <returns></returns>
	    DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 获取总记录
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
	    int GetRecordCount(string strWhere);

	    /// <summary>
	    /// 批量修改图片所属相册
	    /// </summary>
	    /// <param name="AlbumID">相册ID</param>
	    /// <param name="newAlbumId">新相册ID</param>
	    /// <returns></returns>
	    bool UpdatePhotoAlbum(int AlbumID, int newAlbumId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="PhotoId"></param>
        /// <param name="ClassId"></param>
        /// <returns></returns>
	     DataSet GetListAroundPhotoId(int Top, int PhotoId, int ClassId);

         DataSet GetListToReGen(string strWhere);
	    #endregion

	    #endregion  成员方法
	} 
}
