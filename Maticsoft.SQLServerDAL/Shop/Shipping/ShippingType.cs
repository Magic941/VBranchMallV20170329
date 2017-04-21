/**
* ShippingType.cs
*
* 功 能： N/A
* 类 名： ShippingType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 10:24:45   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Shipping;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
	/// <summary>
	/// 数据访问类:ShippingType
	/// </summary>
	public partial class ShippingType:IShippingType
	{
		public ShippingType()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ModeId", "Shop_ShippingType"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ModeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_ShippingType");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ModeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ModeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Shipping.ShippingType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_ShippingType(");
			strSql.Append("Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn)");
			strSql.Append(" values (");
			strSql.Append("@Name,@Weight,@AddWeight,@Price,@AddPrice,@Description,@DisplaySequence,@ExpressCompanyName,@ExpressCompanyEn)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@AddWeight", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Money,8),
					new SqlParameter("@AddPrice", SqlDbType.Money,8),
					new SqlParameter("@Description", SqlDbType.NVarChar,4000),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar,500),
					new SqlParameter("@ExpressCompanyEn", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Weight;
			parameters[2].Value = model.AddWeight;
			parameters[3].Value = model.Price;
			parameters[4].Value = model.AddPrice;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.DisplaySequence;
			parameters[7].Value = model.ExpressCompanyName;
			parameters[8].Value = model.ExpressCompanyEn;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Shipping.ShippingType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_ShippingType set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Weight=@Weight,");
			strSql.Append("AddWeight=@AddWeight,");
			strSql.Append("Price=@Price,");
			strSql.Append("AddPrice=@AddPrice,");
			strSql.Append("Description=@Description,");
			strSql.Append("DisplaySequence=@DisplaySequence,");
			strSql.Append("ExpressCompanyName=@ExpressCompanyName,");
			strSql.Append("ExpressCompanyEn=@ExpressCompanyEn");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@AddWeight", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Money,8),
					new SqlParameter("@AddPrice", SqlDbType.Money,8),
					new SqlParameter("@Description", SqlDbType.NVarChar,4000),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar,500),
					new SqlParameter("@ExpressCompanyEn", SqlDbType.NVarChar,500),
					new SqlParameter("@ModeId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Weight;
			parameters[2].Value = model.AddWeight;
			parameters[3].Value = model.Price;
			parameters[4].Value = model.AddPrice;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.DisplaySequence;
			parameters[7].Value = model.ExpressCompanyName;
			parameters[8].Value = model.ExpressCompanyEn;
			parameters[9].Value = model.ModeId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_ShippingType ");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ModeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ModeId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string ModeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_ShippingType ");
			strSql.Append(" where ModeId in ("+ModeIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Shipping.ShippingType GetModel(int ModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ModeId,Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn from Shop_ShippingType ");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ModeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ModeId;

			Maticsoft.Model.Shop.Shipping.ShippingType model=new Maticsoft.Model.Shop.Shipping.ShippingType();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Shipping.ShippingType DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Shipping.ShippingType model=new Maticsoft.Model.Shop.Shipping.ShippingType();
			if (row != null)
			{
				if(row["ModeId"]!=null && row["ModeId"].ToString()!="")
				{
					model.ModeId=int.Parse(row["ModeId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Weight"]!=null && row["Weight"].ToString()!="")
				{
					model.Weight=int.Parse(row["Weight"].ToString());
				}
				if(row["AddWeight"]!=null && row["AddWeight"].ToString()!="")
				{
					model.AddWeight=int.Parse(row["AddWeight"].ToString());
				}
				if(row["Price"]!=null && row["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(row["Price"].ToString());
				}
				if(row["AddPrice"]!=null && row["AddPrice"].ToString()!="")
				{
					model.AddPrice=decimal.Parse(row["AddPrice"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["DisplaySequence"]!=null && row["DisplaySequence"].ToString()!="")
				{
					model.DisplaySequence=int.Parse(row["DisplaySequence"].ToString());
				}
				if(row["ExpressCompanyName"]!=null)
				{
					model.ExpressCompanyName=row["ExpressCompanyName"].ToString();
				}
				if(row["ExpressCompanyEn"]!=null)
				{
					model.ExpressCompanyEn=row["ExpressCompanyEn"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ModeId,Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn ");
			strSql.Append(" FROM Shop_ShippingType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ModeId,Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn ");
			strSql.Append(" FROM Shop_ShippingType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Shop_ShippingType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ModeId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_ShippingType T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.NVarChar, 255),
					new SqlParameter("@fldName", SqlDbType.NVarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.NVarChar,1000),
					};
			parameters[0].Value = "Shop_ShippingType";
			parameters[1].Value = "ModeId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

