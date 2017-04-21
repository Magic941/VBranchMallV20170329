/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SEORelation.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/10/15 10:50:22
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Settings;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Settings
{
	/// <summary>
	/// 数据访问类:SEORelation
	/// </summary>
	public partial class SEORelation:ISEORelation
	{
		public SEORelation()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("RelationID", "Ms_SEORelation"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int RelationID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Ms_SEORelation");
			strSql.Append(" WHERE RelationID=@RelationID");
			SqlParameter[] parameters = {
					new SqlParameter("@RelationID", SqlDbType.Int,4)
			};
			parameters[0].Value = RelationID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Settings.SEORelation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("INSERT INTO Ms_SEORelation(");
			strSql.Append("KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive)");
			strSql.Append(" VALUES (");
			strSql.Append("@KeyName,@LinkURL,@IsCMS,@IsShop,@IsSNS,@IsComment,@CreatedDate,@IsActive)");
			strSql.Append(";SELECT @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkURL", SqlDbType.NVarChar,500),
					new SqlParameter("@IsCMS", SqlDbType.Bit,1),
					new SqlParameter("@IsShop", SqlDbType.Bit,1),
					new SqlParameter("@IsSNS", SqlDbType.Bit,1),
					new SqlParameter("@IsComment", SqlDbType.Bit,1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.Bit,1)};
			parameters[0].Value = model.KeyName;
			parameters[1].Value = model.LinkURL;
			parameters[2].Value = model.IsCMS;
			parameters[3].Value = model.IsShop;
			parameters[4].Value = model.IsSNS;
			parameters[5].Value = model.IsComment;
			parameters[6].Value = model.CreatedDate;
			parameters[7].Value = model.IsActive;

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
		public bool Update(Maticsoft.Model.Settings.SEORelation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE Ms_SEORelation SET ");
			strSql.Append("KeyName=@KeyName,");
			strSql.Append("LinkURL=@LinkURL,");
			strSql.Append("IsCMS=@IsCMS,");
			strSql.Append("IsShop=@IsShop,");
			strSql.Append("IsSNS=@IsSNS,");
			strSql.Append("IsComment=@IsComment,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("IsActive=@IsActive");
			strSql.Append(" WHERE RelationID=@RelationID");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkURL", SqlDbType.NVarChar,500),
					new SqlParameter("@IsCMS", SqlDbType.Bit,1),
					new SqlParameter("@IsShop", SqlDbType.Bit,1),
					new SqlParameter("@IsSNS", SqlDbType.Bit,1),
					new SqlParameter("@IsComment", SqlDbType.Bit,1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.Bit,1),
					new SqlParameter("@RelationID", SqlDbType.Int,4)};
			parameters[0].Value = model.KeyName;
			parameters[1].Value = model.LinkURL;
			parameters[2].Value = model.IsCMS;
			parameters[3].Value = model.IsShop;
			parameters[4].Value = model.IsSNS;
			parameters[5].Value = model.IsComment;
			parameters[6].Value = model.CreatedDate;
			parameters[7].Value = model.IsActive;
			parameters[8].Value = model.RelationID;

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
		public bool Delete(int RelationID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM Ms_SEORelation ");
			strSql.Append(" WHERE RelationID=@RelationID");
			SqlParameter[] parameters = {
					new SqlParameter("@RelationID", SqlDbType.Int,4)
			};
			parameters[0].Value = RelationID;

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
		public bool DeleteList(string RelationIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM Ms_SEORelation ");
			strSql.Append(" WHERE RelationID in ("+RelationIDlist + ")  ");
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
		public Maticsoft.Model.Settings.SEORelation GetModel(int RelationID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT  TOP 1 RelationID,KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive FROM Ms_SEORelation ");
			strSql.Append(" WHERE RelationID=@RelationID");
			SqlParameter[] parameters = {
					new SqlParameter("@RelationID", SqlDbType.Int,4)
			};
			parameters[0].Value = RelationID;

			Maticsoft.Model.Settings.SEORelation model=new Maticsoft.Model.Settings.SEORelation();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RelationID"]!=null && ds.Tables[0].Rows[0]["RelationID"].ToString()!="")
				{
					model.RelationID=int.Parse(ds.Tables[0].Rows[0]["RelationID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["KeyName"]!=null && ds.Tables[0].Rows[0]["KeyName"].ToString()!="")
				{
					model.KeyName=ds.Tables[0].Rows[0]["KeyName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LinkURL"]!=null && ds.Tables[0].Rows[0]["LinkURL"].ToString()!="")
				{
					model.LinkURL=ds.Tables[0].Rows[0]["LinkURL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsCMS"]!=null && ds.Tables[0].Rows[0]["IsCMS"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsCMS"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsCMS"].ToString().ToLower()=="true"))
					{
						model.IsCMS=true;
					}
					else
					{
						model.IsCMS=false;
					}
				}
				if(ds.Tables[0].Rows[0]["IsShop"]!=null && ds.Tables[0].Rows[0]["IsShop"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsShop"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsShop"].ToString().ToLower()=="true"))
					{
						model.IsShop=true;
					}
					else
					{
						model.IsShop=false;
					}
				}
				if(ds.Tables[0].Rows[0]["IsSNS"]!=null && ds.Tables[0].Rows[0]["IsSNS"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsSNS"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsSNS"].ToString().ToLower()=="true"))
					{
						model.IsSNS=true;
					}
					else
					{
						model.IsSNS=false;
					}
				}
				if(ds.Tables[0].Rows[0]["IsComment"]!=null && ds.Tables[0].Rows[0]["IsComment"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsComment"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsComment"].ToString().ToLower()=="true"))
					{
						model.IsComment=true;
					}
					else
					{
						model.IsComment=false;
					}
				}
				if(ds.Tables[0].Rows[0]["CreatedDate"]!=null && ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsActive"]!=null && ds.Tables[0].Rows[0]["IsActive"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsActive"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsActive"].ToString().ToLower()=="true"))
					{
						model.IsActive=true;
					}
					else
					{
						model.IsActive=false;
					}
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
			strSql.Append("SELECT RelationID,KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive ");
			strSql.Append(" FROM Ms_SEORelation ");
			if(!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT ");
			if(Top>0)
			{
				strSql.Append(" TOP "+Top.ToString());
			}
			strSql.Append(" RelationID,KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive ");
			strSql.Append(" FROM Ms_SEORelation ");
			if(!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			strSql.Append(" ORDER BY " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Ms_SEORelation ");
			if(!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
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
				strSql.Append("ORDER BY T." + orderby );
			}
			else
			{
				strSql.Append("ORDER BY T.RelationID desc");
			}
			strSql.Append(")AS Row, T.*  FROM Ms_SEORelation T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
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
			parameters[0].Value = "Ms_SEORelation";
			parameters[1].Value = "RelationID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

	    public bool Exists(string name)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Ms_SEORelation");
            strSql.Append(" WHERE KeyName=@KeyName");
            SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
	    }
	}
}

