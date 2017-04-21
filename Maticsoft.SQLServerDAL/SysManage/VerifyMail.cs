using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.SysManage;
namespace Maticsoft.SQLServerDAL.SysManage
{
	/// <summary>
	/// 数据访问类:VerifyMail
	/// </summary>
    public partial class VerifyMail : IVerifyMail
	{
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string KeyValue)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Accounts_VerifyMail");
			strSql.Append(" where KeyValue=@KeyValue ");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyValue", SqlDbType.NVarChar,50)			};
			parameters[0].Value = KeyValue;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SysManage.VerifyMail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Accounts_VerifyMail(");
			strSql.Append("UserName,KeyValue,CreatedDate,Status,ValidityType)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@KeyValue,@CreatedDate,@Status,@ValidityType)");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@KeyValue", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ValidityType", SqlDbType.Int,4)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.KeyValue;
			parameters[2].Value = model.CreatedDate;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.ValidityType;

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
		public bool Update(Maticsoft.Model.SysManage.VerifyMail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Accounts_VerifyMail set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Status=@Status,");
			strSql.Append("ValidityType=@ValidityType");
			strSql.Append(" where KeyValue=@KeyValue ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ValidityType", SqlDbType.Int,4),
					new SqlParameter("@KeyValue", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.Status;
			parameters[3].Value = model.ValidityType;
			parameters[4].Value = model.KeyValue;

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
		public bool Delete(string KeyValue)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Accounts_VerifyMail ");
			strSql.Append(" where KeyValue=@KeyValue ");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyValue", SqlDbType.NVarChar,50)			};
			parameters[0].Value = KeyValue;

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
		public bool DeleteList(string KeyValuelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Accounts_VerifyMail ");
			strSql.Append(" where KeyValue in ("+KeyValuelist + ")  ");
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
		public Maticsoft.Model.SysManage.VerifyMail GetModel(string KeyValue)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserName,KeyValue,CreatedDate,Status,ValidityType from Accounts_VerifyMail ");
			strSql.Append(" where KeyValue=@KeyValue ");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyValue", SqlDbType.NVarChar,50)			};
			parameters[0].Value = KeyValue;

			Maticsoft.Model.SysManage.VerifyMail model=new Maticsoft.Model.SysManage.VerifyMail();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserName"]!=null && ds.Tables[0].Rows[0]["UserName"].ToString()!="")
				{
					model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["KeyValue"]!=null && ds.Tables[0].Rows[0]["KeyValue"].ToString()!="")
				{
					model.KeyValue=ds.Tables[0].Rows[0]["KeyValue"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreatedDate"]!=null && ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Status"]!=null && ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ValidityType"]!=null && ds.Tables[0].Rows[0]["ValidityType"].ToString()!="")
				{
					model.ValidityType=int.Parse(ds.Tables[0].Rows[0]["ValidityType"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserName,KeyValue,CreatedDate,Status,ValidityType ");
			strSql.Append(" FROM Accounts_VerifyMail ");
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
			strSql.Append(" UserName,KeyValue,CreatedDate,Status,ValidityType ");
			strSql.Append(" FROM Accounts_VerifyMail ");
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
			strSql.Append("select count(1) FROM Accounts_VerifyMail ");
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
			if (!string.IsNullOrWhiteSpace(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.KeyValue desc");
			}
			strSql.Append(")AS Row, T.*  from Accounts_VerifyMail T ");
			if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

	

		#endregion  Method
	}
}

