/**  版本信息模板在安装目录下，可自行修改。
* Shop_CardUserInfo.cs
*
* 功 能： N/A
* 类 名： Shop_CardUserInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/6/28 16:15:20   lcy   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL
{
	/// <summary>
	/// 接口层Shop_CardUserInfo
	/// </summary>
	public interface IShop_CardUserInfo
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int Id);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop_CardUserInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop_CardUserInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int Id);
		bool DeleteList(string Idlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop_CardUserInfo GetModel(int Id);
		Maticsoft.Model.Shop_CardUserInfo DataRowToModel(DataRow row);
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
        DataSet GetModelList(string username);
        string GetDefaultCardNo(string username);
        int GetDefaultCardsysID(string username);
        Maticsoft.Model.Shop_CardUserInfo GetActicedCardUser(string cardId);


       /// 根据用户名和卡号设置默认卡
        bool SetDefaultCard(string username, string cardNo);
        //根据好邻卡添加用户
        bool AddUser4Card(Maticsoft.Accounts.Bus.User user, Maticsoft.Model.Shop_CardUserInfo carduserinfo);

        bool UpdateUserName(string oldusername, string newusername);

		#endregion  MethodEx
	} 
}
