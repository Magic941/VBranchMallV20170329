/**
* Agents.cs
*
* 功 能： N/A
* 类 名： Agents
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/28 18:17:12   Ben    初版
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
using Maticsoft.IDAL.Ms.Agent;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Ms.Agent
{
    /// <summary>
    /// 数据访问类:Agents
    /// </summary>
    public partial class Agents : IAgents
    {
        public Agents()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AgentId", "Ms_Agents");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AgentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_Agents");
            strSql.Append(" where AgentId=@AgentId");
            SqlParameter[] parameters = {
                    new SqlParameter("@AgentId", SqlDbType.Int,4)
            };
            parameters[0].Value = AgentId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Ms.Agent.AgentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ms_Agents(");
            strSql.Append("Name,StoreName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsAgenApprove,Recomend,Sequence,ThemeName,ParentId,Remark)");
            strSql.Append(" values (");
            strSql.Append("@Name,@StoreName,@StoreStatus,@CategoryId,@Rank,@UserId,@UserName,@TelPhone,@CellPhone,@ContactMail,@Introduction,@RegisteredCapital,@RegionId,@Address,@Contact,@EstablishedDate,@EstablishedCity,@LOGO,@Fax,@PostCode,@HomePage,@ArtiPerson,@CompanyType,@BusinessLicense,@TaxNumber,@AccountBank,@AccountInfo,@ServicePhone,@QQ,@MSN,@Status,@CreatedDate,@CreatedUserId,@UpdatedDate,@UpdatedUserId,@ExpirationDate,@Balance,@IsUserApprove,@IsAgenApprove,@Recomend,@Sequence,@ThemeName,@ParentId,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@StoreName", SqlDbType.NVarChar,100),
                    new SqlParameter("@StoreStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@Rank", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@CellPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@ContactMail", SqlDbType.NVarChar,50),
                    new SqlParameter("@Introduction", SqlDbType.NVarChar,-1),
                    new SqlParameter("@RegisteredCapital", SqlDbType.Int,4),
                    new SqlParameter("@RegionId", SqlDbType.Int,4),
                    new SqlParameter("@Address", SqlDbType.NVarChar,500),
                    new SqlParameter("@Contact", SqlDbType.NVarChar,50),
                    new SqlParameter("@EstablishedDate", SqlDbType.DateTime),
                    new SqlParameter("@EstablishedCity", SqlDbType.Int,4),
                    new SqlParameter("@LOGO", SqlDbType.NVarChar,300),
                    new SqlParameter("@Fax", SqlDbType.NVarChar,30),
                    new SqlParameter("@PostCode", SqlDbType.NVarChar,10),
                    new SqlParameter("@HomePage", SqlDbType.NVarChar,50),
                    new SqlParameter("@ArtiPerson", SqlDbType.NVarChar,50),
                    new SqlParameter("@CompanyType", SqlDbType.SmallInt,2),
                    new SqlParameter("@BusinessLicense", SqlDbType.NVarChar,300),
                    new SqlParameter("@TaxNumber", SqlDbType.NVarChar,300),
                    new SqlParameter("@AccountBank", SqlDbType.NVarChar,300),
                    new SqlParameter("@AccountInfo", SqlDbType.NVarChar,300),
                    new SqlParameter("@ServicePhone", SqlDbType.NVarChar,300),
                    new SqlParameter("@QQ", SqlDbType.NVarChar,30),
                    new SqlParameter("@MSN", SqlDbType.NVarChar,30),
                    new SqlParameter("@Status", SqlDbType.SmallInt,2),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
                    new SqlParameter("@UpdatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@ExpirationDate", SqlDbType.DateTime),
                    new SqlParameter("@Balance", SqlDbType.Money,8),
                    new SqlParameter("@IsUserApprove", SqlDbType.Bit,1),
                    new SqlParameter("@IsAgenApprove", SqlDbType.Bit,1),
                    new SqlParameter("@Recomend", SqlDbType.SmallInt,2),
                    new SqlParameter("@Sequence", SqlDbType.Int,4),
                    new SqlParameter("@ThemeName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.StoreName;
            parameters[2].Value = model.StoreStatus;
            parameters[3].Value = model.CategoryId;
            parameters[4].Value = model.Rank;
            parameters[5].Value = model.UserId;
            parameters[6].Value = model.UserName;
            parameters[7].Value = model.TelPhone;
            parameters[8].Value = model.CellPhone;
            parameters[9].Value = model.ContactMail;
            parameters[10].Value = model.Introduction;
            parameters[11].Value = model.RegisteredCapital;
            parameters[12].Value = model.RegionId;
            parameters[13].Value = model.Address;
            parameters[14].Value = model.Contact;
            parameters[15].Value = model.EstablishedDate;
            parameters[16].Value = model.EstablishedCity;
            parameters[17].Value = model.LOGO;
            parameters[18].Value = model.Fax;
            parameters[19].Value = model.PostCode;
            parameters[20].Value = model.HomePage;
            parameters[21].Value = model.ArtiPerson;
            parameters[22].Value = model.CompanyType;
            parameters[23].Value = model.BusinessLicense;
            parameters[24].Value = model.TaxNumber;
            parameters[25].Value = model.AccountBank;
            parameters[26].Value = model.AccountInfo;
            parameters[27].Value = model.ServicePhone;
            parameters[28].Value = model.QQ;
            parameters[29].Value = model.MSN;
            parameters[30].Value = model.Status;
            parameters[31].Value = model.CreatedDate;
            parameters[32].Value = model.CreatedUserId;
            parameters[33].Value = model.UpdatedDate;
            parameters[34].Value = model.UpdatedUserId;
            parameters[35].Value = model.ExpirationDate;
            parameters[36].Value = model.Balance;
            parameters[37].Value = model.IsUserApprove;
            parameters[38].Value = model.IsAgenApprove;
            parameters[39].Value = model.Recomend;
            parameters[40].Value = model.Sequence;
            parameters[41].Value = model.ThemeName;
            parameters[42].Value = model.ParentId;
            parameters[43].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.Ms.Agent.AgentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_Agents set ");
            strSql.Append("Name=@Name,");
            strSql.Append("StoreName=@StoreName,");
            strSql.Append("StoreStatus=@StoreStatus,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("Rank=@Rank,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("CellPhone=@CellPhone,");
            strSql.Append("ContactMail=@ContactMail,");
            strSql.Append("Introduction=@Introduction,");
            strSql.Append("RegisteredCapital=@RegisteredCapital,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("Address=@Address,");
            strSql.Append("Contact=@Contact,");
            strSql.Append("EstablishedDate=@EstablishedDate,");
            strSql.Append("EstablishedCity=@EstablishedCity,");
            strSql.Append("LOGO=@LOGO,");
            strSql.Append("Fax=@Fax,");
            strSql.Append("PostCode=@PostCode,");
            strSql.Append("HomePage=@HomePage,");
            strSql.Append("ArtiPerson=@ArtiPerson,");
            strSql.Append("CompanyType=@CompanyType,");
            strSql.Append("BusinessLicense=@BusinessLicense,");
            strSql.Append("TaxNumber=@TaxNumber,");
            strSql.Append("AccountBank=@AccountBank,");
            strSql.Append("AccountInfo=@AccountInfo,");
            strSql.Append("ServicePhone=@ServicePhone,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("MSN=@MSN,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CreatedUserId=@CreatedUserId,");
            strSql.Append("UpdatedDate=@UpdatedDate,");
            strSql.Append("UpdatedUserId=@UpdatedUserId,");
            strSql.Append("ExpirationDate=@ExpirationDate,");
            strSql.Append("Balance=@Balance,");
            strSql.Append("IsUserApprove=@IsUserApprove,");
            strSql.Append("IsAgenApprove=@IsAgenApprove,");
            strSql.Append("Recomend=@Recomend,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("ThemeName=@ThemeName,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where AgentId=@AgentId");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@StoreName", SqlDbType.NVarChar,100),
                    new SqlParameter("@StoreStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@Rank", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@CellPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@ContactMail", SqlDbType.NVarChar,50),
                    new SqlParameter("@Introduction", SqlDbType.NVarChar,-1),
                    new SqlParameter("@RegisteredCapital", SqlDbType.Int,4),
                    new SqlParameter("@RegionId", SqlDbType.Int,4),
                    new SqlParameter("@Address", SqlDbType.NVarChar,500),
                    new SqlParameter("@Contact", SqlDbType.NVarChar,50),
                    new SqlParameter("@EstablishedDate", SqlDbType.DateTime),
                    new SqlParameter("@EstablishedCity", SqlDbType.Int,4),
                    new SqlParameter("@LOGO", SqlDbType.NVarChar,300),
                    new SqlParameter("@Fax", SqlDbType.NVarChar,30),
                    new SqlParameter("@PostCode", SqlDbType.NVarChar,10),
                    new SqlParameter("@HomePage", SqlDbType.NVarChar,50),
                    new SqlParameter("@ArtiPerson", SqlDbType.NVarChar,50),
                    new SqlParameter("@CompanyType", SqlDbType.SmallInt,2),
                    new SqlParameter("@BusinessLicense", SqlDbType.NVarChar,300),
                    new SqlParameter("@TaxNumber", SqlDbType.NVarChar,300),
                    new SqlParameter("@AccountBank", SqlDbType.NVarChar,300),
                    new SqlParameter("@AccountInfo", SqlDbType.NVarChar,300),
                    new SqlParameter("@ServicePhone", SqlDbType.NVarChar,300),
                    new SqlParameter("@QQ", SqlDbType.NVarChar,30),
                    new SqlParameter("@MSN", SqlDbType.NVarChar,30),
                    new SqlParameter("@Status", SqlDbType.SmallInt,2),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
                    new SqlParameter("@UpdatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@ExpirationDate", SqlDbType.DateTime),
                    new SqlParameter("@Balance", SqlDbType.Money,8),
                    new SqlParameter("@IsUserApprove", SqlDbType.Bit,1),
                    new SqlParameter("@IsAgenApprove", SqlDbType.Bit,1),
                    new SqlParameter("@Recomend", SqlDbType.SmallInt,2),
                    new SqlParameter("@Sequence", SqlDbType.Int,4),
                    new SqlParameter("@ThemeName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
                    new SqlParameter("@AgentId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.StoreName;
            parameters[2].Value = model.StoreStatus;
            parameters[3].Value = model.CategoryId;
            parameters[4].Value = model.Rank;
            parameters[5].Value = model.UserId;
            parameters[6].Value = model.UserName;
            parameters[7].Value = model.TelPhone;
            parameters[8].Value = model.CellPhone;
            parameters[9].Value = model.ContactMail;
            parameters[10].Value = model.Introduction;
            parameters[11].Value = model.RegisteredCapital;
            parameters[12].Value = model.RegionId;
            parameters[13].Value = model.Address;
            parameters[14].Value = model.Contact;
            parameters[15].Value = model.EstablishedDate;
            parameters[16].Value = model.EstablishedCity;
            parameters[17].Value = model.LOGO;
            parameters[18].Value = model.Fax;
            parameters[19].Value = model.PostCode;
            parameters[20].Value = model.HomePage;
            parameters[21].Value = model.ArtiPerson;
            parameters[22].Value = model.CompanyType;
            parameters[23].Value = model.BusinessLicense;
            parameters[24].Value = model.TaxNumber;
            parameters[25].Value = model.AccountBank;
            parameters[26].Value = model.AccountInfo;
            parameters[27].Value = model.ServicePhone;
            parameters[28].Value = model.QQ;
            parameters[29].Value = model.MSN;
            parameters[30].Value = model.Status;
            parameters[31].Value = model.CreatedDate;
            parameters[32].Value = model.CreatedUserId;
            parameters[33].Value = model.UpdatedDate;
            parameters[34].Value = model.UpdatedUserId;
            parameters[35].Value = model.ExpirationDate;
            parameters[36].Value = model.Balance;
            parameters[37].Value = model.IsUserApprove;
            parameters[38].Value = model.IsAgenApprove;
            parameters[39].Value = model.Recomend;
            parameters[40].Value = model.Sequence;
            parameters[41].Value = model.ThemeName;
            parameters[42].Value = model.ParentId;
            parameters[43].Value = model.Remark;
            parameters[44].Value = model.AgentId;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AgentId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_Agents ");
            strSql.Append(" where AgentId=@AgentId");
            SqlParameter[] parameters = {
                    new SqlParameter("@AgentId", SqlDbType.Int,4)
            };
            parameters[0].Value = AgentId;

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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string AgentIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_Agents ");
            strSql.Append(" where AgentId in (" + AgentIdlist + ")  ");
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
        public Maticsoft.Model.Ms.Agent.AgentInfo GetModel(int AgentId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AgentId,Name,StoreName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsAgenApprove,Recomend,Sequence,ThemeName,ParentId,Remark from Ms_Agents ");
            strSql.Append(" where AgentId=@AgentId");
            SqlParameter[] parameters = {
                    new SqlParameter("@AgentId", SqlDbType.Int,4)
            };
            parameters[0].Value = AgentId;

            Maticsoft.Model.Ms.Agent.AgentInfo model = new Maticsoft.Model.Ms.Agent.AgentInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public Maticsoft.Model.Ms.Agent.AgentInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.Agent.AgentInfo model = new Maticsoft.Model.Ms.Agent.AgentInfo();
            if (row != null)
            {
                if (row["AgentId"] != null && row["AgentId"].ToString() != "")
                {
                    model.AgentId = int.Parse(row["AgentId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["StoreName"] != null)
                {
                    model.StoreName = row["StoreName"].ToString();
                }
                if (row["StoreStatus"] != null && row["StoreStatus"].ToString() != "")
                {
                    model.StoreStatus = int.Parse(row["StoreStatus"].ToString());
                }
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["Rank"] != null && row["Rank"].ToString() != "")
                {
                    model.Rank = int.Parse(row["Rank"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    model.TelPhone = row["TelPhone"].ToString();
                }
                if (row["CellPhone"] != null)
                {
                    model.CellPhone = row["CellPhone"].ToString();
                }
                if (row["ContactMail"] != null)
                {
                    model.ContactMail = row["ContactMail"].ToString();
                }
                if (row["Introduction"] != null)
                {
                    model.Introduction = row["Introduction"].ToString();
                }
                if (row["RegisteredCapital"] != null && row["RegisteredCapital"].ToString() != "")
                {
                    model.RegisteredCapital = int.Parse(row["RegisteredCapital"].ToString());
                }
                if (row["RegionId"] != null && row["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Contact"] != null)
                {
                    model.Contact = row["Contact"].ToString();
                }
                if (row["EstablishedDate"] != null && row["EstablishedDate"].ToString() != "")
                {
                    model.EstablishedDate = DateTime.Parse(row["EstablishedDate"].ToString());
                }
                if (row["EstablishedCity"] != null && row["EstablishedCity"].ToString() != "")
                {
                    model.EstablishedCity = int.Parse(row["EstablishedCity"].ToString());
                }
                if (row["LOGO"] != null)
                {
                    model.LOGO = row["LOGO"].ToString();
                }
                if (row["Fax"] != null)
                {
                    model.Fax = row["Fax"].ToString();
                }
                if (row["PostCode"] != null)
                {
                    model.PostCode = row["PostCode"].ToString();
                }
                if (row["HomePage"] != null)
                {
                    model.HomePage = row["HomePage"].ToString();
                }
                if (row["ArtiPerson"] != null)
                {
                    model.ArtiPerson = row["ArtiPerson"].ToString();
                }
                if (row["CompanyType"] != null && row["CompanyType"].ToString() != "")
                {
                    model.CompanyType = int.Parse(row["CompanyType"].ToString());
                }
                if (row["BusinessLicense"] != null)
                {
                    model.BusinessLicense = row["BusinessLicense"].ToString();
                }
                if (row["TaxNumber"] != null)
                {
                    model.TaxNumber = row["TaxNumber"].ToString();
                }
                if (row["AccountBank"] != null)
                {
                    model.AccountBank = row["AccountBank"].ToString();
                }
                if (row["AccountInfo"] != null)
                {
                    model.AccountInfo = row["AccountInfo"].ToString();
                }
                if (row["ServicePhone"] != null)
                {
                    model.ServicePhone = row["ServicePhone"].ToString();
                }
                if (row["QQ"] != null)
                {
                    model.QQ = row["QQ"].ToString();
                }
                if (row["MSN"] != null)
                {
                    model.MSN = row["MSN"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CreatedUserId"] != null && row["CreatedUserId"].ToString() != "")
                {
                    model.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["UpdatedDate"] != null && row["UpdatedDate"].ToString() != "")
                {
                    model.UpdatedDate = DateTime.Parse(row["UpdatedDate"].ToString());
                }
                if (row["UpdatedUserId"] != null && row["UpdatedUserId"].ToString() != "")
                {
                    model.UpdatedUserId = int.Parse(row["UpdatedUserId"].ToString());
                }
                if (row["ExpirationDate"] != null && row["ExpirationDate"].ToString() != "")
                {
                    model.ExpirationDate = DateTime.Parse(row["ExpirationDate"].ToString());
                }
                if (row["Balance"] != null && row["Balance"].ToString() != "")
                {
                    model.Balance = decimal.Parse(row["Balance"].ToString());
                }
                if (row["IsUserApprove"] != null && row["IsUserApprove"].ToString() != "")
                {
                    if ((row["IsUserApprove"].ToString() == "1") || (row["IsUserApprove"].ToString().ToLower() == "true"))
                    {
                        model.IsUserApprove = true;
                    }
                    else
                    {
                        model.IsUserApprove = false;
                    }
                }
                if (row["IsAgenApprove"] != null && row["IsAgenApprove"].ToString() != "")
                {
                    if ((row["IsAgenApprove"].ToString() == "1") || (row["IsAgenApprove"].ToString().ToLower() == "true"))
                    {
                        model.IsAgenApprove = true;
                    }
                    else
                    {
                        model.IsAgenApprove = false;
                    }
                }
                if (row["Recomend"] != null && row["Recomend"].ToString() != "")
                {
                    model.Recomend = int.Parse(row["Recomend"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["ThemeName"] != null)
                {
                    model.ThemeName = row["ThemeName"].ToString();
                }
                if (row["ParentId"] != null && row["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(row["ParentId"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AgentId,Name,StoreName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsAgenApprove,Recomend,Sequence,ThemeName,ParentId,Remark ");
            strSql.Append(" FROM Ms_Agents ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" AgentId,Name,StoreName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsAgenApprove,Recomend,Sequence,ThemeName,ParentId,Remark ");
            strSql.Append(" FROM Ms_Agents ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Ms_Agents ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.AgentId desc");
            }
            strSql.Append(")AS Row, T.*  from Ms_Agents T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Ms.Agent.AgentInfo GetModelByUserId(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Ms_Agents ");
            strSql.Append(" where UserId=@UserId");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.Int,4)
            };
            parameters[0].Value = UserId;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_Agents");
            strSql.Append(" where StoreName=@StoreName");
            SqlParameter[] parameters = {
                    new SqlParameter("@StoreName", SqlDbType.NVarChar,100)
            };
            parameters[0].Value = Name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name, int AgentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_Agents");
            strSql.Append(" where StoreName=@StoreName AND AgentID<>@AgentID");
            SqlParameter[] parameters = {
                    new SqlParameter("@StoreName", SqlDbType.NVarChar,100),
                     new SqlParameter("@AgentID", SqlDbType.Int,4)
            };
            parameters[0].Value = Name;
            parameters[1].Value = AgentID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_Agents set " + strWhere);
            strSql.Append(" where AgentID in(" + IDlist + ")  ");
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

//        public DataSet GetStatisticsSupply(int supplierId)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.AppendFormat(
//                @"
//--剩余总量/额
//SELECT 1 AS [Type], SUM(S.Stock) ToalQuantity
//      , SUM(S.SalePrice * S.Stock) ToalPrice
//FROM    Shop_SKUs S
//WHERE   EXISTS ( SELECT *, P.MarketPrice
//                 FROM   Shop_Products P
//                 WHERE  S.ProductId = P.ProductId
//                        AND P.SupplierId = {0} )
//UNION ALL      
//--已售量/额         
//SELECT  2 AS [Type], SUM(I.Quantity) ToalQuantity
//      , SUM(I.SellPrice * I.Quantity) ToalPrice
//FROM    Shop_OrderItems I, Shop_Orders O
//WHERE I.OrderId = O.OrderId AND O.SupplierId = {0} AND O.OrderStatus = 2 AND O.OrderType = 1
//", supplierId);
//            return DbHelperSQL.Query(strSql.ToString());
//        }

//        public DataSet GetStatisticsSales(int supplierId, int year)
//        {

//            StringBuilder strSql = new StringBuilder();
//            strSql.Append(
//                @"
//--销量/业绩走势图
//SELECT  A.[Month] AS Mon
//      , CASE WHEN B.ToalQuantity IS NULL THEN 0
//             ELSE B.ToalQuantity
//        END AS ToalQuantity
//      , CASE WHEN B.ToalPrice IS NULL THEN 0.00
//             ELSE B.ToalPrice
//        END AS ToalPrice
//FROM    ( SELECT    *
//          FROM      GET_GeneratedMonthEx()
//        ) A
//        LEFT JOIN ( SELECT  MONTH(O.CreatedDate) Mon
//                          , SUM(I.Quantity) ToalQuantity
//                          , SUM(I.SellPrice) ToalPrice
//                    FROM    Shop_OrderItems I
//                          , Shop_Orders O
//                    WHERE   I.OrderId = O.OrderId ");
//            if (supplierId > 0)
//            {
//                strSql.AppendFormat("  AND O.SupplierId = {0}", supplierId);
//            }
//            strSql.AppendFormat(@" AND O.OrderStatus = 2 AND O.OrderType = 1
//                            AND YEAR(O.CreatedDate) = '{0}'
//                    GROUP BY MONTH(O.CreatedDate)
//                  ) B ON A.[Month] = B.Mon 
//", year);
//            return DbHelperSQL.Query(strSql.ToString());
//        }
        /// <summary>
        /// 关闭店铺 
        /// </summary>
        public bool CloseShop(int AgentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_Agents set ");
            strSql.Append("StoreStatus=2");
            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
                    new SqlParameter("@AgentID", SqlDbType.Int,4)};
            parameters[0].Value = AgentID;
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

        /// <summary>
        /// 根据店铺名称得到店铺model
        /// </summary>
        /// <param name="StoreName"></param>
        /// <returns></returns>
        public Maticsoft.Model.Ms.Agent.AgentInfo GetModelByShopName(string StoreName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Ms_Agents ");
            strSql.Append(" where StoreName=@StoreName and Status=1");
            SqlParameter[] parameters = {
                    new SqlParameter("@StoreName", SqlDbType.NVarChar,100)
            };
            parameters[0].Value = StoreName;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 代理商名称是否已存在
        /// </summary>
        public bool Exists(string name, int id = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_Agents");
            strSql.Append(" where Name=@Name ");
            if (id > 0)
            {
                strSql.AppendFormat(" AND AgentId<>{0}", id);
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100)
            };
            parameters[0].Value = name;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion  ExtensionMethod
    }
}

