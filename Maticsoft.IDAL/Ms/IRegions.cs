using System;
using System.Data;
namespace Maticsoft.IDAL.Ms
{
	/// <summary>
	/// 接口层Regions
	/// </summary>
	public interface IRegions
	{
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int RegionId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Ms.Regions model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Ms.Regions model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int RegionId);
        bool DeleteList(string RegionIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
        #region 新增的成员方法
        /// <summary>
        /// 获取省份数据
        /// </summary>
        /// <returns></returns>
        DataSet GetProvinces();
        /// <summary>
        /// 获取城市数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        DataSet GetCitys(int parentID);
        /// <summary>
        /// 获取父级regionID
        /// </summary>
        /// <param name="regionID"></param>
        /// <returns></returns>
        DataTable GetParentID(int regionID);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Ms.Regions GetModel(int RegionId);

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        DataSet GetPrivoces();

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        DataSet GetPrivoceName();

         /// <summary>
        /// 根据省份获取城市
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        DataSet GetRegionName(string parentID);

        DataSet GetDistrictByParentId(int iParentId);

        DataSet GetAllCityList();

        string GetPath(int regid);

        /// <summary>
        /// 根据任何地区ID，获取地区完整名称
        /// </summary>        
        System.Collections.Generic.List<string> GetRegionNameByRID(int RID);


        int GetRegPath(int? regid);

        DataSet GetParentIDs(int regID, out int Count);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        Maticsoft.Model.Ms.Regions DataRowToModel(DataRow row);

	    /// <summary>
	    /// 更新多条数据的AreaID
	    /// </summary>
        bool UpdateAreaID(string regionlist, int AreaId);

        string GetRegionIDsByAreaId(int areaid);

	    #endregion  新增的成员方法
        DataSet GetSamePathArea(int regionId);
	    /// <summary>
	    /// 获取父ID
	    /// </summary>
	    /// <param name="regionId">当前Id</param>
	    /// <returns></returns>
        int GetCurrentParentId(int regionId);

	    bool IsParentRegion(int regionId);
	} 
}
