using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//请先添加引用
using Maticsoft.IDAL.Poll;
namespace Maticsoft.SQLServerDAL.Poll
{
	/// <summary>
	/// 数据访问类Users。
	/// </summary>
	public class PollUsers:IPollUsers
	{
		

		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserID", "Poll_Users"); 
		}

        public int GetUserCount()
        {
            string strsql = "select count(1) from Poll_Users" ;
            object obj = DbHelperSQL.GetSingle(strsql);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Poll_Users");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = UserID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Poll.PollUsers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Poll_Users(");
			strSql.Append("UserName,Password,TrueName,Age,Sex,Phone,Email,UserType)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@Password,@TrueName,@Age,@Sex,@Phone,@Email,@UserType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.Binary,50),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.NVarChar,5),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@UserType", SqlDbType.Char,2)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.Password;
			parameters[2].Value = model.TrueName;
			parameters[3].Value = model.Age;
			parameters[4].Value = model.Sex;
			parameters[5].Value = model.Phone;
			parameters[6].Value = model.Email;
			parameters[7].Value = model.UserType;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Poll.PollUsers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Poll_Users set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("Password=@Password,");
			strSql.Append("TrueName=@TrueName,");
			strSql.Append("Age=@Age,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("Email=@Email,");
			strSql.Append("UserType=@UserType");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.Binary,50),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.NVarChar,5),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@UserType", SqlDbType.Char,2)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.Password;
			parameters[3].Value = model.TrueName;
			parameters[4].Value = model.Age;
			parameters[5].Value = model.Sex;
			parameters[6].Value = model.Phone;
			parameters[7].Value = model.Email;
			parameters[8].Value = model.UserType;

            int n = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (n == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
		}

		/// <summary>
		/// 删除用户及所有数据
		/// </summary>
		public void Delete(int UserID)
		{			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Poll_Users ");
            strSql.Append(" where UserID=" + UserID);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update Poll_Options set SubmitNum=SubmitNum-1  ");
            strSql2.Append(" where ID in (select OptionID from Poll_UserPoll where UserID=" + UserID + ")");
            
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from Poll_UserPoll ");
            strSql3.Append(" where UserID=" + UserID);

            List<string> sqllist = new List<string>();            
            sqllist.Add(strSql2.ToString());
            sqllist.Add(strSql3.ToString());
            sqllist.Add(strSql.ToString());

            DbHelperSQL.ExecuteSqlTran(sqllist);
            
		}

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ClassIDlist"></param>
        /// <returns></returns>
        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Poll_Users ");
            strSql.Append(" where UserID in (" + ClassIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.Poll.PollUsers GetModel(int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserID,UserName,Password,TrueName,Age,Sex,Phone,Email,UserType from Poll_Users ");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = UserID;

            Maticsoft.Model.Poll.PollUsers model = new Maticsoft.Model.Poll.PollUsers();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				if(ds.Tables[0].Rows[0]["Password"].ToString()!="")
				{
					model.Password=(byte[])ds.Tables[0].Rows[0]["Password"];
				}
				model.TrueName=ds.Tables[0].Rows[0]["TrueName"].ToString();
				if(ds.Tables[0].Rows[0]["Age"].ToString()!="")
				{
					model.Age=int.Parse(ds.Tables[0].Rows[0]["Age"].ToString());
				}
				model.Sex=ds.Tables[0].Rows[0]["Sex"].ToString();
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				model.UserType=ds.Tables[0].Rows[0]["UserType"].ToString();
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
			strSql.Append("select UserID,UserName,Password,TrueName,Age,Sex,Phone,Email,UserType ");
			strSql.Append(" FROM Poll_Users ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by UserID desc ");
			return DbHelperSQL.Query(strSql.ToString());
		}

		

		#endregion  成员方法


       
    }
}

