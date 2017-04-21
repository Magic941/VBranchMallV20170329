using System;
using System.Data;
namespace Maticsoft.IDAL.Ms
{
	/// <summary>
	/// 接口层ThumbnailSize
	/// </summary>
	public interface IThumbnailSize
	{
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ThumName);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Maticsoft.Model.Ms.ThumbnailSize model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Ms.ThumbnailSize model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string ThumName);
        bool DeleteList(string ThumNamelist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Ms.ThumbnailSize GetModel(string ThumName);
        Maticsoft.Model.Ms.ThumbnailSize DataRowToModel(DataRow row);
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

	    #region  MethodEx

	    /// <summary>
	    /// 是否存在该记录
	    /// </summary>
	    /// <param name="ThumName">ThumName</param>
	    /// <param name="type">区域</param>
	    /// <param name="Theme">模版名称</param>
	    /// <returns></returns>
	    bool Exists(string ThumName, int type, string Theme);

	    #endregion  MethodEx
	} 
}
