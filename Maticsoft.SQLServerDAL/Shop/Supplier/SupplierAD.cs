/**  版本信息模板在安装目录下，可自行修改。
* SupplierAD.cs
*
* 功 能： N/A
* 类 名： SupplierAD
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/30 10:48:44   N/A    初版
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
using Maticsoft.IDAL.Shop;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Supplier;

//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
	/// <summary>
	/// 数据访问类:SupplierAD
	/// </summary>
	public partial class SupplierAD:ISupplierAD
	{
		public SupplierAD()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AdvertisementId", "Shop_SupplierAD"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AdvertisementId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_SupplierAD");
			strSql.Append(" where AdvertisementId=@AdvertisementId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvertisementId", SqlDbType.Int,4)			};
			parameters[0].Value = AdvertisementId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int  Add(Maticsoft.Model.Shop.Supplier.SupplierAD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_SupplierAD(");
			strSql.Append(" Name,PositionId,ContentType,FileUrl,AlternateText,NavigateUrl,AdvHtml,CreatedDate,CreatedUserID,Status,Sequence,SupplierId)");
			strSql.Append(" values (");
			strSql.Append(" @Name,@PositionId,@ContentType,@FileUrl,@AlternateText,@NavigateUrl,@AdvHtml,@CreatedDate,@CreatedUserID,@Status,@Sequence,@SupplierId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@PositionId", SqlDbType.Int,4),
					new SqlParameter("@ContentType", SqlDbType.Int,4),
					new SqlParameter("@FileUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@AlternateText", SqlDbType.NVarChar,200),
					new SqlParameter("@NavigateUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@AdvHtml", SqlDbType.NVarChar,-1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.PositionId;
			parameters[2].Value = model.ContentType;
			parameters[3].Value = model.FileUrl;
			parameters[4].Value = model.AlternateText;
			parameters[5].Value = model.NavigateUrl;
			parameters[6].Value = model.AdvHtml;
			parameters[7].Value = model.CreatedDate;
			parameters[8].Value = model.CreatedUserID;
			parameters[9].Value = model.Status;
			parameters[10].Value = model.Sequence;
			parameters[11].Value = model.SupplierId;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierAD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_SupplierAD set ");
			strSql.Append("Name=@Name,");
			strSql.Append("PositionId=@PositionId,");
			strSql.Append("ContentType=@ContentType,");
			strSql.Append("FileUrl=@FileUrl,");
			strSql.Append("AlternateText=@AlternateText,");
			strSql.Append("NavigateUrl=@NavigateUrl,");
			strSql.Append("AdvHtml=@AdvHtml,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("Status=@Status,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("SupplierId=@SupplierId");
			strSql.Append(" where AdvertisementId=@AdvertisementId ");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@PositionId", SqlDbType.Int,4),
					new SqlParameter("@ContentType", SqlDbType.Int,4),
					new SqlParameter("@FileUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@AlternateText", SqlDbType.NVarChar,200),
					new SqlParameter("@NavigateUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@AdvHtml", SqlDbType.NVarChar,-1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@AdvertisementId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.PositionId;
			parameters[2].Value = model.ContentType;
			parameters[3].Value = model.FileUrl;
			parameters[4].Value = model.AlternateText;
			parameters[5].Value = model.NavigateUrl;
			parameters[6].Value = model.AdvHtml;
			parameters[7].Value = model.CreatedDate;
			parameters[8].Value = model.CreatedUserID;
			parameters[9].Value = model.Status;
			parameters[10].Value = model.Sequence;
			parameters[11].Value = model.SupplierId;
			parameters[12].Value = model.AdvertisementId;

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
		public bool Delete(int AdvertisementId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierAD ");
			strSql.Append(" where AdvertisementId=@AdvertisementId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvertisementId", SqlDbType.Int,4)			};
			parameters[0].Value = AdvertisementId;

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
		public bool DeleteList(string AdvertisementIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierAD ");
			strSql.Append(" where AdvertisementId in ("+AdvertisementIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Supplier.SupplierAD GetModel(int AdvertisementId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AdvertisementId,Name,PositionId,ContentType,FileUrl,AlternateText,NavigateUrl,AdvHtml,CreatedDate,CreatedUserID,Status,Sequence,SupplierId from Shop_SupplierAD ");
			strSql.Append(" where AdvertisementId=@AdvertisementId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvertisementId", SqlDbType.Int,4)			};
			parameters[0].Value = AdvertisementId;

            Maticsoft.Model.Shop.Supplier.SupplierAD model = new Maticsoft.Model.Shop.Supplier.SupplierAD();
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
        public Maticsoft.Model.Shop.Supplier.SupplierAD DataRowToModel(DataRow row)
		{
            Maticsoft.Model.Shop.Supplier.SupplierAD model = new Maticsoft.Model.Shop.Supplier.SupplierAD();
			if (row != null)
			{
				if(row["AdvertisementId"]!=null && row["AdvertisementId"].ToString()!="")
				{
					model.AdvertisementId=int.Parse(row["AdvertisementId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["PositionId"]!=null && row["PositionId"].ToString()!="")
				{
					model.PositionId=int.Parse(row["PositionId"].ToString());
				}
				if(row["ContentType"]!=null && row["ContentType"].ToString()!="")
				{
					model.ContentType=int.Parse(row["ContentType"].ToString());
				}
				if(row["FileUrl"]!=null)
				{
					model.FileUrl=row["FileUrl"].ToString();
				}
				if(row["AlternateText"]!=null)
				{
					model.AlternateText=row["AlternateText"].ToString();
				}
				if(row["NavigateUrl"]!=null)
				{
					model.NavigateUrl=row["NavigateUrl"].ToString();
				}
				if(row["AdvHtml"]!=null)
				{
					model.AdvHtml=row["AdvHtml"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["CreatedUserID"]!=null && row["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(row["CreatedUserID"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
				}
				if(row["SupplierId"]!=null && row["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(row["SupplierId"].ToString());
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
			strSql.Append("select AdvertisementId,Name,PositionId,ContentType,FileUrl,AlternateText,NavigateUrl,AdvHtml,CreatedDate,CreatedUserID,Status,Sequence,SupplierId ");
			strSql.Append(" FROM Shop_SupplierAD ");
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
			strSql.Append(" AdvertisementId,Name,PositionId,ContentType,FileUrl,AlternateText,NavigateUrl,AdvHtml,CreatedDate,CreatedUserID,Status,Sequence,SupplierId ");
			strSql.Append(" FROM Shop_SupplierAD ");
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
			strSql.Append("select count(1) FROM Shop_SupplierAD ");
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
				strSql.Append("order by T.AdvertisementId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_SupplierAD T ");
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
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Shop_SupplierAD";
			parameters[1].Value = "AdvertisementId";
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

