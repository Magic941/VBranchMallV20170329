using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Members
{
	/// <summary>
	/// 数据访问类:SiteMessageLog
	/// </summary>
	public partial class SiteMessageLog:ISiteMessageLog
	{
		public SiteMessageLog()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "SA_SiteMessageLog"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SA_SiteMessageLog");
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
		public int Add(Maticsoft.Model.Members.SiteMessageLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SA_SiteMessageLog(");
			strSql.Append("MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName)");
			strSql.Append(" values (");
			strSql.Append("@MessageID,@MessageType,@MessageState,@ReceiverID,@Ext1,@Ext2,@ReceiverUserName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@MessageID", SqlDbType.Int,4),
					new SqlParameter("@MessageType", SqlDbType.NVarChar,50),
					new SqlParameter("@MessageState", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
					new SqlParameter("@Ext1", SqlDbType.NVarChar,300),
					new SqlParameter("@Ext2", SqlDbType.NVarChar,300),
					new SqlParameter("@ReceiverUserName", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.MessageID;
			parameters[1].Value = model.MessageType;
			parameters[2].Value = model.MessageState;
			parameters[3].Value = model.ReceiverID;
			parameters[4].Value = model.Ext1;
			parameters[5].Value = model.Ext2;
			parameters[6].Value = model.ReceiverUserName;

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
		public bool Update(Maticsoft.Model.Members.SiteMessageLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SA_SiteMessageLog set ");
			strSql.Append("MessageID=@MessageID,");
			strSql.Append("MessageType=@MessageType,");
			strSql.Append("MessageState=@MessageState,");
			strSql.Append("ReceiverID=@ReceiverID,");
			strSql.Append("Ext1=@Ext1,");
			strSql.Append("Ext2=@Ext2,");
			strSql.Append("ReceiverUserName=@ReceiverUserName");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@MessageID", SqlDbType.Int,4),
					new SqlParameter("@MessageType", SqlDbType.NVarChar,50),
					new SqlParameter("@MessageState", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
					new SqlParameter("@Ext1", SqlDbType.NVarChar,300),
					new SqlParameter("@Ext2", SqlDbType.NVarChar,300),
					new SqlParameter("@ReceiverUserName", SqlDbType.NVarChar,100),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.MessageID;
			parameters[1].Value = model.MessageType;
			parameters[2].Value = model.MessageState;
			parameters[3].Value = model.ReceiverID;
			parameters[4].Value = model.Ext1;
			parameters[5].Value = model.Ext2;
			parameters[6].Value = model.ReceiverUserName;
			parameters[7].Value = model.ID;

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
			strSql.Append("delete from SA_SiteMessageLog ");
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
			strSql.Append("delete from SA_SiteMessageLog ");
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
		public Maticsoft.Model.Members.SiteMessageLog GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName from SA_SiteMessageLog ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Members.SiteMessageLog model=new Maticsoft.Model.Members.SiteMessageLog();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MessageID"]!=null && ds.Tables[0].Rows[0]["MessageID"].ToString()!="")
				{
					model.MessageID=int.Parse(ds.Tables[0].Rows[0]["MessageID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MessageType"]!=null && ds.Tables[0].Rows[0]["MessageType"].ToString()!="")
				{
					model.MessageType=ds.Tables[0].Rows[0]["MessageType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MessageState"]!=null && ds.Tables[0].Rows[0]["MessageState"].ToString()!="")
				{
					model.MessageState=ds.Tables[0].Rows[0]["MessageState"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReceiverID"]!=null && ds.Tables[0].Rows[0]["ReceiverID"].ToString()!="")
				{
					model.ReceiverID=int.Parse(ds.Tables[0].Rows[0]["ReceiverID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ext1"]!=null && ds.Tables[0].Rows[0]["Ext1"].ToString()!="")
				{
					model.Ext1=ds.Tables[0].Rows[0]["Ext1"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Ext2"]!=null && ds.Tables[0].Rows[0]["Ext2"].ToString()!="")
				{
					model.Ext2=ds.Tables[0].Rows[0]["Ext2"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReceiverUserName"]!=null && ds.Tables[0].Rows[0]["ReceiverUserName"].ToString()!="")
				{
					model.ReceiverUserName=ds.Tables[0].Rows[0]["ReceiverUserName"].ToString();
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
			strSql.Append("select ID,MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName ");
			strSql.Append(" FROM SA_SiteMessageLog ");
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
			strSql.Append(" ID,MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName ");
			strSql.Append(" FROM SA_SiteMessageLog ");
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
			strSql.Append("select count(1) FROM SA_SiteMessageLog ");
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
			strSql.Append(")AS Row, T.*  from SA_SiteMessageLog T ");
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
			parameters[0].Value = "SA_SiteMessageLog";
			parameters[1].Value = "ID";
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

