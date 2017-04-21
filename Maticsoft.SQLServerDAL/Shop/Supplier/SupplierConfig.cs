/**
* SupplierConfig.cs
*
* 功 能： N/A
* 类 名： SupplierConfig
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:48   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Supplier;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
	/// <summary>
	/// 数据访问类:SupplierConfig
	/// </summary>
	public partial class SupplierConfig:ISupplierConfig
	{
		public SupplierConfig()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "Shop_SupplierConfig"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_SupplierConfig");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_SupplierConfig(");
			strSql.Append("KeyName,Value,KeyType,Description,SupplierId)");
			strSql.Append(" values (");
			strSql.Append("@KeyName,@Value,@KeyType,@Description,@SupplierId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.NVarChar,50),
					new SqlParameter("@Value", SqlDbType.NVarChar,-1),
					new SqlParameter("@KeyType", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@SupplierId", SqlDbType.Int,4)};
			parameters[0].Value = model.KeyName;
			parameters[1].Value = model.Value;
			parameters[2].Value = model.KeyType;
			parameters[3].Value = model.Description;
			parameters[4].Value = model.SupplierId;

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
		public bool Update(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_SupplierConfig set ");
			strSql.Append("KeyName=@KeyName,");
			strSql.Append("Value=@Value,");
			strSql.Append("KeyType=@KeyType,");
			strSql.Append("Description=@Description,");
			strSql.Append("SupplierId=@SupplierId");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.NVarChar,50),
					new SqlParameter("@Value", SqlDbType.NVarChar,-1),
					new SqlParameter("@KeyType", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.KeyName;
			parameters[1].Value = model.Value;
			parameters[2].Value = model.KeyType;
			parameters[3].Value = model.Description;
			parameters[4].Value = model.SupplierId;
			parameters[5].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierConfig ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierConfig ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public Maticsoft.Model.Shop.Supplier.SupplierConfig GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,KeyName,Value,KeyType,Description,SupplierId from Shop_SupplierConfig ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Shop.Supplier.SupplierConfig model=new Maticsoft.Model.Shop.Supplier.SupplierConfig();
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
		public Maticsoft.Model.Shop.Supplier.SupplierConfig DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Supplier.SupplierConfig model=new Maticsoft.Model.Shop.Supplier.SupplierConfig();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["KeyName"]!=null)
				{
					model.KeyName=row["KeyName"].ToString();
				}
				if(row["Value"]!=null)
				{
					model.Value=row["Value"].ToString();
				}
				if(row["KeyType"]!=null && row["KeyType"].ToString()!="")
				{
					model.KeyType=int.Parse(row["KeyType"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["SupplierId"]!=null && row["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(row["SupplierId"].ToString());
				}
			}
			return model;
		}
        public Maticsoft.Model.Shop.Supplier.SupplierConfig GetModel(string strWhere)
        {
            DataSet ds = GetList(strWhere);
           return DataRowToModel(ds.Tables[0].Rows[0]);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,KeyName,Value,KeyType,Description,SupplierId ");
			strSql.Append(" FROM Shop_SupplierConfig ");
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
			strSql.Append(" ID,KeyName,Value,KeyType,Description,SupplierId ");
			strSql.Append(" FROM Shop_SupplierConfig ");
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
			strSql.Append("select count(1) FROM Shop_SupplierConfig ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_SupplierConfig T ");
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
			parameters[0].Value = "Shop_SupplierConfig";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod

		#region  ExtensionMethod
      /// <summary>
      /// 根据供应商id和参数名称获取参数值 
      /// </summary>
        /// <param name="suppId">供应商id</param>
      /// <param name="keyName">参数名称</param>
      /// <returns></returns>
        public  string GetValue(int suppId, string keyName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT Value  FROM Shop_SupplierConfig ");
            strSql.AppendFormat(" WHERE  SupplierId={0} AND KeyName='{1}' ", suppId, keyName);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

       public bool UpdateEx(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_SupplierConfig set ");
            strSql.Append("KeyName=@KeyName,");
            strSql.Append("Value=@Value,");
            strSql.Append("KeyType=@KeyType,");
            strSql.Append("Description=@Description,");
            strSql.Append("SupplierId=@SupplierId");
            strSql.Append(" where KeyName=@KeyName and SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.NVarChar,50),
					new SqlParameter("@Value", SqlDbType.NVarChar,-1),
					new SqlParameter("@KeyType", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@SupplierId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.KeyName;
            parameters[1].Value = model.Value;
            parameters[2].Value = model.KeyType;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.SupplierId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       public bool Exists(string key, int sipId)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select count(1) from Shop_SupplierConfig");
           strSql.Append(" where SupplierId=@SupplierId");
           strSql.Append("  And   KeyName=@KeyName");
           SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@KeyName", SqlDbType.NVarChar,50)
			};
           parameters[0].Value = sipId;
           parameters[1].Value = key;
           return DbHelperSQL.Exists(strSql.ToString(), parameters);
       }
		#endregion  ExtensionMethod
	}
}

