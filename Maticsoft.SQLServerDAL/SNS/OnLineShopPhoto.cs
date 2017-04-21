/**
* OnLineShopPhoto.cs
*
* 功 能： N/A
* 类 名： OnLineShopPhoto
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:46   N/A    初版
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
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:OnLineShopPhoto
	/// </summary>
	public partial class OnLineShopPhoto:IOnLineShopPhoto
	{
		public OnLineShopPhoto()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int PhotoID,int ProductID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_OnLineShopPhoto");
			strSql.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4)			};
			parameters[0].Value = PhotoID;
			parameters[1].Value = ProductID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SNS.OnLineShopPhoto model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_OnLineShopPhoto(");
			strSql.Append("PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status)");
			strSql.Append(" values (");
			strSql.Append("@PhotoID,@ProductID,@CreatedUserId,@CreatedNickName,@CreatedDate,@Status)");
			SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.PhotoID;
			parameters[1].Value = model.ProductID;
			parameters[2].Value = model.CreatedUserId;
			parameters[3].Value = model.CreatedNickName;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.Status;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.OnLineShopPhoto model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_OnLineShopPhoto set ");
			strSql.Append("CreatedUserId=@CreatedUserId,");
			strSql.Append("CreatedNickName=@CreatedNickName,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Status=@Status");
			strSql.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@PhotoID", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4)};
			parameters[0].Value = model.CreatedUserId;
			parameters[1].Value = model.CreatedNickName;
			parameters[2].Value = model.CreatedDate;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.PhotoID;
			parameters[5].Value = model.ProductID;

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
		public bool Delete(int PhotoID,int ProductID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_OnLineShopPhoto ");
			strSql.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4)			};
			parameters[0].Value = PhotoID;
			parameters[1].Value = ProductID;

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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.OnLineShopPhoto GetModel(int PhotoID,int ProductID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status from SNS_OnLineShopPhoto ");
			strSql.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4)			};
			parameters[0].Value = PhotoID;
			parameters[1].Value = ProductID;

			Maticsoft.Model.SNS.OnLineShopPhoto model=new Maticsoft.Model.SNS.OnLineShopPhoto();
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
		public Maticsoft.Model.SNS.OnLineShopPhoto DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.OnLineShopPhoto model=new Maticsoft.Model.SNS.OnLineShopPhoto();
			if (row != null)
			{
				if(row["PhotoID"]!=null && row["PhotoID"].ToString()!="")
				{
					model.PhotoID=int.Parse(row["PhotoID"].ToString());
				}
				if(row["ProductID"]!=null && row["ProductID"].ToString()!="")
				{
					model.ProductID=int.Parse(row["ProductID"].ToString());
				}
				if(row["CreatedUserId"]!=null && row["CreatedUserId"].ToString()!="")
				{
					model.CreatedUserId=int.Parse(row["CreatedUserId"].ToString());
				}
				if(row["CreatedNickName"]!=null)
				{
					model.CreatedNickName=row["CreatedNickName"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
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
			strSql.Append("select PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status ");
			strSql.Append(" FROM SNS_OnLineShopPhoto ");
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
			strSql.Append(" PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status ");
			strSql.Append(" FROM SNS_OnLineShopPhoto ");
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
			strSql.Append("select count(1) FROM SNS_OnLineShopPhoto ");
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
				strSql.Append("order by T.ProductID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_OnLineShopPhoto T ");
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
			parameters[0].Value = "SNS_OnLineShopPhoto";
			parameters[1].Value = "ProductID";
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

