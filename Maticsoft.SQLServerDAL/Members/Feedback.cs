using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Members
{
	/// <summary>
	/// 数据访问类:Feedback
	/// </summary>
	public partial class Feedback:IFeedback
	{
		public Feedback()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("FeedbackId", "SA_Feedback"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int FeedbackId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SA_Feedback");
			strSql.Append(" where FeedbackId=@FeedbackId");
			SqlParameter[] parameters = {
					new SqlParameter("@FeedbackId", SqlDbType.Int,4)
			};
			parameters[0].Value = FeedbackId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Members.Feedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SA_Feedback(");
			strSql.Append("TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData)");
			strSql.Append(" values (");
			strSql.Append("@TypeId,@Description,@UserName,@UserSex,@UserEmail,@Phone,@TelPhone,@UserCompany,@UserIP,@IsSolved,@CreatedDate,@Result,@Status,@Remark,@ExtData)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@UserSex", SqlDbType.Char,10),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,100),
					new SqlParameter("@Phone", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,100),
					new SqlParameter("@UserCompany", SqlDbType.NVarChar,200),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,20),
					new SqlParameter("@IsSolved", SqlDbType.Bit,1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Result", SqlDbType.Text),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@ExtData", SqlDbType.Text)};
			parameters[0].Value = model.TypeId;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.UserSex;
			parameters[4].Value = model.UserEmail;
			parameters[5].Value = model.Phone;
			parameters[6].Value = model.TelPhone;
			parameters[7].Value = model.UserCompany;
			parameters[8].Value = model.UserIP;
			parameters[9].Value = model.IsSolved;
			parameters[10].Value = model.CreatedDate;
			parameters[11].Value = model.Result;
			parameters[12].Value = model.Status;
			parameters[13].Value = model.Remark;
			parameters[14].Value = model.ExtData;

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
		public bool Update(Maticsoft.Model.Members.Feedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SA_Feedback set ");
			strSql.Append("TypeId=@TypeId,");
			strSql.Append("Description=@Description,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserSex=@UserSex,");
			strSql.Append("UserEmail=@UserEmail,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("TelPhone=@TelPhone,");
			strSql.Append("UserCompany=@UserCompany,");
			strSql.Append("UserIP=@UserIP,");
			strSql.Append("IsSolved=@IsSolved,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Result=@Result,");
			strSql.Append("Status=@Status,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("ExtData=@ExtData");
			strSql.Append(" where FeedbackId=@FeedbackId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@UserSex", SqlDbType.Char,10),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,100),
					new SqlParameter("@Phone", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,100),
					new SqlParameter("@UserCompany", SqlDbType.NVarChar,200),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,20),
					new SqlParameter("@IsSolved", SqlDbType.Bit,1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Result", SqlDbType.Text),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@ExtData", SqlDbType.Text),
					new SqlParameter("@FeedbackId", SqlDbType.Int,4)};
			parameters[0].Value = model.TypeId;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.UserSex;
			parameters[4].Value = model.UserEmail;
			parameters[5].Value = model.Phone;
			parameters[6].Value = model.TelPhone;
			parameters[7].Value = model.UserCompany;
			parameters[8].Value = model.UserIP;
			parameters[9].Value = model.IsSolved;
			parameters[10].Value = model.CreatedDate;
			parameters[11].Value = model.Result;
			parameters[12].Value = model.Status;
			parameters[13].Value = model.Remark;
			parameters[14].Value = model.ExtData;
			parameters[15].Value = model.FeedbackId;

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
		public bool Delete(int FeedbackId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SA_Feedback ");
			strSql.Append(" where FeedbackId=@FeedbackId");
			SqlParameter[] parameters = {
					new SqlParameter("@FeedbackId", SqlDbType.Int,4)
			};
			parameters[0].Value = FeedbackId;

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
		public bool DeleteList(string FeedbackIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SA_Feedback ");
			strSql.Append(" where FeedbackId in ("+FeedbackIdlist + ")  ");
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
		public Maticsoft.Model.Members.Feedback GetModel(int FeedbackId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 FeedbackId,TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData from SA_Feedback ");
			strSql.Append(" where FeedbackId=@FeedbackId");
			SqlParameter[] parameters = {
					new SqlParameter("@FeedbackId", SqlDbType.Int,4)
			};
			parameters[0].Value = FeedbackId;

			Maticsoft.Model.Members.Feedback model=new Maticsoft.Model.Members.Feedback();
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
		public Maticsoft.Model.Members.Feedback DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Members.Feedback model=new Maticsoft.Model.Members.Feedback();
			if (row != null)
			{
				if(row["FeedbackId"]!=null && row["FeedbackId"].ToString()!="")
				{
					model.FeedbackId=int.Parse(row["FeedbackId"].ToString());
				}
				if(row["TypeId"]!=null && row["TypeId"].ToString()!="")
				{
					model.TypeId=int.Parse(row["TypeId"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["UserSex"]!=null)
				{
					model.UserSex=row["UserSex"].ToString();
				}
				if(row["UserEmail"]!=null)
				{
					model.UserEmail=row["UserEmail"].ToString();
				}
				if(row["Phone"]!=null)
				{
					model.Phone=row["Phone"].ToString();
				}
				if(row["TelPhone"]!=null)
				{
					model.TelPhone=row["TelPhone"].ToString();
				}
				if(row["UserCompany"]!=null)
				{
					model.UserCompany=row["UserCompany"].ToString();
				}
				if(row["UserIP"]!=null)
				{
					model.UserIP=row["UserIP"].ToString();
				}
				if(row["IsSolved"]!=null && row["IsSolved"].ToString()!="")
				{
					if((row["IsSolved"].ToString()=="1")||(row["IsSolved"].ToString().ToLower()=="true"))
					{
						model.IsSolved=true;
					}
					else
					{
						model.IsSolved=false;
					}
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Result"]!=null)
				{
					model.Result=row["Result"].ToString();
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["ExtData"]!=null)
				{
					model.ExtData=row["ExtData"].ToString();
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
			strSql.Append("select FeedbackId,TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData ");
			strSql.Append(" FROM SA_Feedback ");
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
			strSql.Append(" FeedbackId,TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData ");
			strSql.Append(" FROM SA_Feedback ");
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
			strSql.Append("select count(1) FROM SA_Feedback ");
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
				strSql.Append("order by T.FeedbackId desc");
			}
			strSql.Append(")AS Row, T.*  from SA_Feedback T ");
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
			parameters[0].Value = "SA_Feedback";
			parameters[1].Value = "FeedbackId";
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

