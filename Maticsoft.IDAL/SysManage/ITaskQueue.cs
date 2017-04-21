using System;
using System.Data;
namespace Maticsoft.IDAL.SysManage
{
	/// <summary>
	/// 接口层TaskQueue
	/// </summary>
	public interface ITaskQueue
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ID,int Type);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.Model.SysManage.TaskQueue model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SysManage.TaskQueue model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID,int Type);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SysManage.TaskQueue GetModel(int ID,int Type);
		Maticsoft.Model.SysManage.TaskQueue DataRowToModel(DataRow row);
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
        bool DeleteArticle();

        DataSet GetContinueTask(int type);

        Maticsoft.Model.SysManage.TaskQueue GetLastModel(int type);

        bool DeleteTask(int Type);
		#endregion  MethodEx
	} 
}
