using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Package;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Package
{
	/// <summary>
	/// 数据访问类:ProductPackage
	/// </summary>
	public partial class ProductPackage:IProductPackage
	{
		public ProductPackage()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("PackageId", "Shop_ProductPackage"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ProductId,int PackageId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_ProductPackage");
			strSql.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@PackageId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductId;
			parameters[1].Value = PackageId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Package.ProductPackage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_ProductPackage(");
			strSql.Append("ProductId,PackageId)");
			strSql.Append(" values (");
			strSql.Append("@ProductId,@PackageId)");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@PackageId", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductId;
			parameters[1].Value = model.PackageId;

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
		public bool Update(Maticsoft.Model.Shop.Package.ProductPackage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_ProductPackage set ");
			strSql.Append("ProductId=@ProductId,");
			strSql.Append("PackageId=@PackageId");
			strSql.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@PackageId", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductId;
			parameters[1].Value = model.PackageId;

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
		public bool Delete(long ProductId,int PackageId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_ProductPackage ");
			strSql.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@PackageId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductId;
			parameters[1].Value = PackageId;

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
		public Maticsoft.Model.Shop.Package.ProductPackage GetModel(long ProductId,int PackageId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ProductId,PackageId from Shop_ProductPackage ");
			strSql.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@PackageId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductId;
			parameters[1].Value = PackageId;

			Maticsoft.Model.Shop.Package.ProductPackage model=new Maticsoft.Model.Shop.Package.ProductPackage();
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
		public Maticsoft.Model.Shop.Package.ProductPackage DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Package.ProductPackage model=new Maticsoft.Model.Shop.Package.ProductPackage();
			if (row != null)
			{
				if(row["ProductId"]!=null && row["ProductId"].ToString()!="")
				{
					model.ProductId=long.Parse(row["ProductId"].ToString());
				}
				if(row["PackageId"]!=null && row["PackageId"].ToString()!="")
				{
					model.PackageId=int.Parse(row["PackageId"].ToString());
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
			strSql.Append("select ProductId,PackageId ");
			strSql.Append(" FROM Shop_ProductPackage ");
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
			strSql.Append(" ProductId,PackageId ");
			strSql.Append(" FROM Shop_ProductPackage ");
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
			strSql.Append("select count(1) FROM Shop_ProductPackage ");
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
				strSql.Append("order by T.PackageId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_ProductPackage T ");
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
			parameters[0].Value = "Shop_ProductPackage";
			parameters[1].Value = "PackageId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

