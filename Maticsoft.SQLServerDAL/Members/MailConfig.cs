using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL
{
    /// <summary>
    /// 用户邮箱配置
    /// </summary>
    public class MailConfig : Maticsoft.IDAL.IMailConfig
    {
        public MailConfig()
        { }

        #region  

        /// <summary>
        /// 
        /// </summary>
        public bool Exists(int UserID, string Mailaddress)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_MailConfig");
            strSql.Append(" where UserID=@UserID and  Mailaddress=@Mailaddress");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Mailaddress", SqlDbType.NVarChar,100)};
            parameters[0].Value = UserID;
            parameters[1].Value = Mailaddress;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 
        /// </summary>
        public int Add(Maticsoft.Model.MailConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_MailConfig(");
            strSql.Append("UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Mailaddress,@Username,@Password,@SMTPServer,@SMTPPort,@SMTPSSL,@POPServer,@POPPort,@POPSSL)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Mailaddress", SqlDbType.NVarChar,100),
					new SqlParameter("@Username", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@SMTPServer", SqlDbType.NVarChar,50),
					new SqlParameter("@SMTPPort", SqlDbType.Int,4),
					new SqlParameter("@SMTPSSL", SqlDbType.SmallInt,2),
					new SqlParameter("@POPServer", SqlDbType.NVarChar,50),
					new SqlParameter("@POPPort", SqlDbType.Int,4),
					new SqlParameter("@POPSSL", SqlDbType.SmallInt,2)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Mailaddress;
            parameters[2].Value = model.Username;
            parameters[3].Value = model.Password;
            parameters[4].Value = model.SMTPServer;
            parameters[5].Value = model.SMTPPort;
            parameters[6].Value = model.SMTPSSL ? 1 : 0;
            parameters[7].Value = model.POPServer;
            parameters[8].Value = model.POPPort;
            parameters[9].Value = model.POPSSL ? 1 : 0;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 
        /// </summary>
        public void Update(Maticsoft.Model.MailConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_MailConfig set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Mailaddress=@Mailaddress,");
            strSql.Append("Username=@Username,");
            strSql.Append("Password=@Password,");
            strSql.Append("SMTPServer=@SMTPServer,");
            strSql.Append("SMTPPort=@SMTPPort,");
            strSql.Append("SMTPSSL=@SMTPSSL,");
            strSql.Append("POPServer=@POPServer,");
            strSql.Append("POPPort=@POPPort,");
            strSql.Append("POPSSL=@POPSSL");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Mailaddress", SqlDbType.NVarChar,100),
					new SqlParameter("@Username", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@SMTPServer", SqlDbType.NVarChar,50),
					new SqlParameter("@SMTPPort", SqlDbType.Int,4),
					new SqlParameter("@SMTPSSL", SqlDbType.SmallInt,2),
					new SqlParameter("@POPServer", SqlDbType.NVarChar,50),
					new SqlParameter("@POPPort", SqlDbType.Int,4),
					new SqlParameter("@POPSSL", SqlDbType.SmallInt,2)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Mailaddress;
            parameters[3].Value = model.Username;
            parameters[4].Value = model.Password;
            parameters[5].Value = model.SMTPServer;
            parameters[6].Value = model.SMTPPort;
            parameters[7].Value = model.SMTPSSL ? 1 : 0;
            parameters[8].Value = model.POPServer;
            parameters[9].Value = model.POPPort;
            parameters[10].Value = model.POPSSL ? 1 : 0;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_MailConfig ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 
        /// </summary>
        public Maticsoft.Model.MailConfig GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL from Accounts_MailConfig ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            Maticsoft.Model.MailConfig model = new Maticsoft.Model.MailConfig();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Mailaddress = ds.Tables[0].Rows[0]["Mailaddress"].ToString();
                model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.SMTPServer = ds.Tables[0].Rows[0]["SMTPServer"].ToString();
                if (ds.Tables[0].Rows[0]["SMTPPort"].ToString() != "")
                {
                    model.SMTPPort = int.Parse(ds.Tables[0].Rows[0]["SMTPPort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SMTPSSL"].ToString() != "")
                {
                    model.SMTPSSL = ds.Tables[0].Rows[0]["SMTPSSL"].ToString() == "1" ? true : false;
                }
                model.POPServer = ds.Tables[0].Rows[0]["POPServer"].ToString();
                if (ds.Tables[0].Rows[0]["POPPort"].ToString() != "")
                {
                    model.POPPort = int.Parse(ds.Tables[0].Rows[0]["POPPort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["POPSSL"].ToString() != "")
                {
                    model.POPSSL = ds.Tables[0].Rows[0]["POPSSL"].ToString()== "1" ? true : false;
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Maticsoft.Model.MailConfig GetModel()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL from Accounts_MailConfig ");

            Maticsoft.Model.MailConfig model = new Maticsoft.Model.MailConfig();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Mailaddress = ds.Tables[0].Rows[0]["Mailaddress"].ToString();
                model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.SMTPServer = ds.Tables[0].Rows[0]["SMTPServer"].ToString();
                if (ds.Tables[0].Rows[0]["SMTPPort"].ToString() != "")
                {
                    model.SMTPPort = int.Parse(ds.Tables[0].Rows[0]["SMTPPort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SMTPSSL"].ToString() != "")
                {
                    model.SMTPSSL = ds.Tables[0].Rows[0]["SMTPSSL"].ToString() == "1" ? true : false;
                }
                model.POPServer = ds.Tables[0].Rows[0]["POPServer"].ToString();
                if (ds.Tables[0].Rows[0]["POPPort"].ToString() != "")
                {
                    model.POPPort = int.Parse(ds.Tables[0].Rows[0]["POPPort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["POPSSL"].ToString() != "")
                {
                    model.POPSSL = ds.Tables[0].Rows[0]["POPSSL"].ToString() == "1" ? true : false;
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        public Maticsoft.Model.MailConfig GetModel(int? userId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL from Accounts_MailConfig ");
            if (userId.HasValue)
            {
                strSql.AppendFormat(" WHERE UserID={0}",userId.Value);
            }

            Maticsoft.Model.MailConfig model = new Maticsoft.Model.MailConfig();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Mailaddress = ds.Tables[0].Rows[0]["Mailaddress"].ToString();
                model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.SMTPServer = ds.Tables[0].Rows[0]["SMTPServer"].ToString();
                if (ds.Tables[0].Rows[0]["SMTPPort"].ToString() != "")
                {
                    model.SMTPPort = int.Parse(ds.Tables[0].Rows[0]["SMTPPort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SMTPSSL"].ToString() != "")
                {
                    model.SMTPSSL = ds.Tables[0].Rows[0]["SMTPSSL"].ToString() == "1" ? true : false;
                }
                model.POPServer = ds.Tables[0].Rows[0]["POPServer"].ToString();
                if (ds.Tables[0].Rows[0]["POPPort"].ToString() != "")
                {
                    model.POPPort = int.Parse(ds.Tables[0].Rows[0]["POPPort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["POPSSL"].ToString() != "")
                {
                    model.POPSSL = ds.Tables[0].Rows[0]["POPSSL"].ToString() == "1" ? true : false;
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL ");
            strSql.Append(" FROM Accounts_MailConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL ");
            strSql.Append(" FROM Accounts_MailConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

       

        #endregion  成员方法
    }
}
