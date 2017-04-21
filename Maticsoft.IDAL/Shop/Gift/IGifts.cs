using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Gift
{
	/// <summary>
	/// 接口层Gifts
	/// </summary>
	public interface IGifts
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int GiftId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Gift.Gifts model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Gift.Gifts model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int GiftId);
		bool DeleteList(string GiftIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Gift.Gifts GetModel(int GiftId);
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
        bool UpdateStock(int giftid, int stock);
        #endregion
    } 
}
