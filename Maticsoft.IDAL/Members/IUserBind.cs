using System;
using System.Data;
namespace Maticsoft.IDAL.Members
{
	/// <summary>
	/// 接口层UserBind
	/// </summary>
	public interface IUserBind
	{
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int BindId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Members.UserBind model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Members.UserBind model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int BindId);
        bool DeleteList(string BindIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Members.UserBind GetModel(int BindId);
        Maticsoft.Model.Members.UserBind DataRowToModel(DataRow row);
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
        Maticsoft.Model.Members.UserBind GetModel(int userId, int type);

	    bool Exists(int userId, string MediaUserID);

	  bool  UpdateEx(Maticsoft.Model.Members.UserBind model);

	    #endregion  MethodEx
	} 
}
