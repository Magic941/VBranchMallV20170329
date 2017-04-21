/**  版本信息模板在安装目录下，可自行修改。
* Shippers.cs
*
* 功 能： N/A
* 类 名： Shippers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/5 10:13:21   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop;

//Please add references
namespace Maticsoft.SQLServerDAL.Shop
{
	/// <summary>
	/// 数据访问类:Shippers
	/// </summary>
	public partial class Shippers:IShippers
	{
		public Shippers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ShipperId", "Shop_Shippers"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ShipperId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_Shippers");
			strSql.Append(" where ShipperId=@ShipperId");
			SqlParameter[] parameters = {
					new SqlParameter("@ShipperId", SqlDbType.Int,4)
			};
			parameters[0].Value = ShipperId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Maticsoft.Model.Shop.Shippers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_Shippers(");
			strSql.Append("IsDefault,ShipperTag,ShipperName,RegionId,Address,CellPhone,TelPhone,Zipcode,Remark)");
			strSql.Append(" values (");
			strSql.Append("@IsDefault,@ShipperTag,@ShipperName,@RegionId,@Address,@CellPhone,@TelPhone,@Zipcode,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@IsDefault", SqlDbType.Bit,1),
					new SqlParameter("@ShipperTag", SqlDbType.NVarChar,100),
					new SqlParameter("@ShipperName", SqlDbType.NVarChar,100),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@CellPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@Zipcode", SqlDbType.NVarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300)};
			parameters[0].Value = model.IsDefault;
			parameters[1].Value = model.ShipperTag;
			parameters[2].Value = model.ShipperName;
			parameters[3].Value = model.RegionId;
			parameters[4].Value = model.Address;
			parameters[5].Value = model.CellPhone;
			parameters[6].Value = model.TelPhone;
			parameters[7].Value = model.Zipcode;
			parameters[8].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.Shop.Shippers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_Shippers set ");
			strSql.Append("IsDefault=@IsDefault,");
			strSql.Append("ShipperTag=@ShipperTag,");
			strSql.Append("ShipperName=@ShipperName,");
			strSql.Append("RegionId=@RegionId,");
			strSql.Append("Address=@Address,");
			strSql.Append("CellPhone=@CellPhone,");
			strSql.Append("TelPhone=@TelPhone,");
			strSql.Append("Zipcode=@Zipcode,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ShipperId=@ShipperId");
			SqlParameter[] parameters = {
					new SqlParameter("@IsDefault", SqlDbType.Bit,1),
					new SqlParameter("@ShipperTag", SqlDbType.NVarChar,100),
					new SqlParameter("@ShipperName", SqlDbType.NVarChar,100),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@CellPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@Zipcode", SqlDbType.NVarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@ShipperId", SqlDbType.Int,4)};
			parameters[0].Value = model.IsDefault;
			parameters[1].Value = model.ShipperTag;
			parameters[2].Value = model.ShipperName;
			parameters[3].Value = model.RegionId;
			parameters[4].Value = model.Address;
			parameters[5].Value = model.CellPhone;
			parameters[6].Value = model.TelPhone;
			parameters[7].Value = model.Zipcode;
			parameters[8].Value = model.Remark;
			parameters[9].Value = model.ShipperId;

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
		public bool Delete(int ShipperId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_Shippers ");
			strSql.Append(" where ShipperId=@ShipperId");
			SqlParameter[] parameters = {
					new SqlParameter("@ShipperId", SqlDbType.Int,4)
			};
			parameters[0].Value = ShipperId;

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
		public bool DeleteList(string ShipperIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_Shippers ");
			strSql.Append(" where ShipperId in ("+ShipperIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Shippers GetModel(int ShipperId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ShipperId,IsDefault,ShipperTag,ShipperName,RegionId,Address,CellPhone,TelPhone,Zipcode,Remark from Shop_Shippers ");
			strSql.Append(" where ShipperId=@ShipperId");
			SqlParameter[] parameters = {
					new SqlParameter("@ShipperId", SqlDbType.Int,4)
			};
			parameters[0].Value = ShipperId;

            Maticsoft.Model.Shop.Shippers model = new Maticsoft.Model.Shop.Shippers();
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
        public Maticsoft.Model.Shop.Shippers DataRowToModel(DataRow row)
		{
            Maticsoft.Model.Shop.Shippers model = new Maticsoft.Model.Shop.Shippers();
			if (row != null)
			{
				if(row["ShipperId"]!=null && row["ShipperId"].ToString()!="")
				{
					model.ShipperId=int.Parse(row["ShipperId"].ToString());
				}
				if(row["IsDefault"]!=null && row["IsDefault"].ToString()!="")
				{
					if((row["IsDefault"].ToString()=="1")||(row["IsDefault"].ToString().ToLower()=="true"))
					{
						model.IsDefault=true;
					}
					else
					{
						model.IsDefault=false;
					}
				}
				if(row["ShipperTag"]!=null)
				{
					model.ShipperTag=row["ShipperTag"].ToString();
				}
				if(row["ShipperName"]!=null)
				{
					model.ShipperName=row["ShipperName"].ToString();
				}
				if(row["RegionId"]!=null && row["RegionId"].ToString()!="")
				{
					model.RegionId=int.Parse(row["RegionId"].ToString());
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["CellPhone"]!=null)
				{
					model.CellPhone=row["CellPhone"].ToString();
				}
				if(row["TelPhone"]!=null)
				{
					model.TelPhone=row["TelPhone"].ToString();
				}
				if(row["Zipcode"]!=null)
				{
					model.Zipcode=row["Zipcode"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
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
			strSql.Append("select ShipperId,IsDefault,ShipperTag,ShipperName,RegionId,Address,CellPhone,TelPhone,Zipcode,Remark ");
			strSql.Append(" FROM Shop_Shippers ");
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
			strSql.Append(" ShipperId,IsDefault,ShipperTag,ShipperName,RegionId,Address,CellPhone,TelPhone,Zipcode,Remark ");
			strSql.Append(" FROM Shop_Shippers ");
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
			strSql.Append("select count(1) FROM Shop_Shippers ");
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
				strSql.Append("order by T.ShipperId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_Shippers T ");
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
			parameters[0].Value = "Shop_Shippers";
			parameters[1].Value = "ShipperId";
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
        /// 是否存在该记录
        /// </summary>
        /// <param name="shipperTag">发货人标签</param>
        /// <param name="shipperId">id</param>
        /// <returns></returns>
        public bool Exists(string shipperTag, int shipperId = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Shippers");
            strSql.Append(" where ShipperTag=@ShipperTag");
            if (shipperId > 0)
            {
                strSql.AppendFormat(" and  ShipperId<>{0}", shipperId);
            }
            SqlParameter[] parameters = {
				new SqlParameter("@ShipperTag", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = shipperTag;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 设置或取消默认值
        /// </summary>
        /// <param name="shipperId"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public bool UpdateIsDefault(int shipperId,bool isDefault)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Shop_Shippers set ");
            strSql.Append(" IsDefault=@IsDefault "); 
            strSql.Append(" where ShipperId=@ShipperId");
            SqlParameter[] parameters = {
                    new SqlParameter("@IsDefault", SqlDbType.Bit,1),
					new SqlParameter("@ShipperId", SqlDbType.Int,4)};
            parameters[0].Value = isDefault;
            parameters[1].Value = shipperId;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            if (isDefault)//设置默认值时将其他的都改为否
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append(" update Shop_Shippers set ");
                strSql2.Append(" IsDefault=0 ");
                strSql2.Append(" where ShipperId<>@ShipperId ");
                SqlParameter[] parameters2 = {
					new SqlParameter("@ShipperId", SqlDbType.Int,4)};
                parameters2[0].Value = shipperId;
                cmd = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd);
            }
            int rows= DbHelperSQL.ExecuteSqlTran(sqllist); 
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		#endregion  ExtensionMethod
	}
}

