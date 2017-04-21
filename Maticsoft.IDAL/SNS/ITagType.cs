/**
* TagType.cs
*
* 功 能： N/A
* 类 名： TagType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:59   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.SNS
{
	/// <summary>
	/// 接口层标签的类型 如 材质
	/// </summary>
	public interface ITagType
	{
		#region  成员方法
		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string TypeName);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.TagType model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.TagType model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.TagType GetModel(int ID);
		Maticsoft.Model.SNS.TagType DataRowToModel(DataRow row);
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
        DataSet GetAllListEX();
        bool RelationSNSCate(int tagTypeId, int SNSCategoryId);
		#endregion  MethodEx
	} 
}
