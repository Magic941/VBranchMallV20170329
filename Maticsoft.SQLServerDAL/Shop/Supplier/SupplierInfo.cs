/**
* SupplierInfo.cs
*
* 功 能： N/A
* 类 名： SupplierInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:50   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Supplier;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    /// <summary>
    /// 数据访问类:SupplierInfo
    /// </summary>
    public partial class SupplierInfo : ISupplierInfo
    {
        public SupplierInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SupplierId", "Shop_Suppliers");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Suppliers");
            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4)
			};
            parameters[0].Value = SupplierId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Suppliers(");
            strSql.Append("Name,ShopName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId,IndexProdTop,IndexContent,Latitude,Longitude,Slogan,Discount,IsOnLine)");
            strSql.Append(" values (");
            strSql.Append("@Name,@ShopName,@StoreStatus,@CategoryId,@Rank,@UserId,@UserName,@TelPhone,@CellPhone,@ContactMail,@Introduction,@RegisteredCapital,@RegionId,@Address,@Contact,@EstablishedDate,@EstablishedCity,@LOGO,@Fax,@PostCode,@HomePage,@ArtiPerson,@CompanyType,@BusinessLicense,@TaxNumber,@AccountBank,@AccountInfo,@ServicePhone,@QQ,@MSN,@Status,@CreatedDate,@CreatedUserId,@UpdatedDate,@UpdatedUserId,@ExpirationDate,@Balance,@IsUserApprove,@IsSuppApprove,@ScoreDesc,@ScoreService,@ScoreSpeed,@Recomend,@Sequence,@ProductCount,@PhotoCount,@ThemeId,@Remark,@AgentId,@IndexProdTop,@IndexContent,@Latitude,@Longitude,@Slogan,@Discount,@IsOnLine)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@ShopName", SqlDbType.NVarChar,100),
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
					new SqlParameter("@IsSuppApprove", SqlDbType.Bit,1),
					new SqlParameter("@ScoreDesc", SqlDbType.Money,8),
					new SqlParameter("@ScoreService", SqlDbType.Money,8),
					new SqlParameter("@ScoreSpeed", SqlDbType.Money,8),
					new SqlParameter("@Recomend", SqlDbType.SmallInt,2),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Int,4),
					new SqlParameter("@PhotoCount", SqlDbType.Int,4),
					new SqlParameter("@ThemeId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@AgentId", SqlDbType.Int,4),
					new SqlParameter("@IndexProdTop", SqlDbType.Int,4),
					new SqlParameter("@IndexContent", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Latitude", SqlDbType.Decimal,9),
                    new SqlParameter("@Longitude", SqlDbType.Decimal,9),
                    new SqlParameter("@Slogan",SqlDbType.NVarChar,400),
                    new SqlParameter("@Discount",SqlDbType.NVarChar,400),
                    new SqlParameter("@IsOnLine",SqlDbType.Bit,2)
                                        };
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ShopName;
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
            parameters[38].Value = model.IsSuppApprove;
            parameters[39].Value = model.ScoreDesc;
            parameters[40].Value = model.ScoreService;
            parameters[41].Value = model.ScoreSpeed;
            parameters[42].Value = model.Recomend;
            parameters[43].Value = model.Sequence;
            parameters[44].Value = model.ProductCount;
            parameters[45].Value = model.PhotoCount;
            parameters[46].Value = model.ThemeId;
            parameters[47].Value = model.Remark;
            parameters[48].Value = model.AgentId;
            parameters[49].Value = model.IndexProdTop;
            parameters[50].Value = model.IndexContent;
            parameters[51].Value = model.Latitude;
            parameters[52].Value = model.Longitude;
            parameters[53].Value = model.Slogan;
            parameters[54].Value = model.Discount;
            parameters[55].Value = model.IsOnLine;

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
        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Suppliers set ");
            strSql.Append("Name=@Name,");
            strSql.Append("ShopName=@ShopName,");
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
            strSql.Append("IsSuppApprove=@IsSuppApprove,");
            strSql.Append("ScoreDesc=@ScoreDesc,");
            strSql.Append("ScoreService=@ScoreService,");
            strSql.Append("ScoreSpeed=@ScoreSpeed,");
            strSql.Append("Recomend=@Recomend,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("ProductCount=@ProductCount,");
            strSql.Append("PhotoCount=@PhotoCount,");
            strSql.Append("ThemeId=@ThemeId,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AgentId=@AgentId,");
            strSql.Append("IndexProdTop=@IndexProdTop,");
            strSql.Append("IndexContent=@IndexContent,");
            strSql.Append("Latitude=@Latitude,");
            strSql.Append("Longitude=@Longitude,");
            strSql.Append("Slogan=@Slogan,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("BGType=@BGType,");
            strSql.Append("Background=@Background");

            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@ShopName", SqlDbType.NVarChar,100),
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
					new SqlParameter("@IsSuppApprove", SqlDbType.Bit,1),
					new SqlParameter("@ScoreDesc", SqlDbType.Money,8),
					new SqlParameter("@ScoreService", SqlDbType.Money,8),
					new SqlParameter("@ScoreSpeed", SqlDbType.Money,8),
					new SqlParameter("@Recomend", SqlDbType.SmallInt,2),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Int,4),
					new SqlParameter("@PhotoCount", SqlDbType.Int,4),
					new SqlParameter("@ThemeId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@AgentId", SqlDbType.Int,4),
					new SqlParameter("@IndexProdTop", SqlDbType.Int,4),
					new SqlParameter("@IndexContent", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Latitude", SqlDbType.Decimal,9),
                    new SqlParameter("@Longitude", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@Slogan", SqlDbType.NVarChar,400),
                    new SqlParameter("@Discount", SqlDbType.NVarChar,400),
                    new SqlParameter("@BGType", SqlDbType.Bit,1),
                    new SqlParameter("@Background", SqlDbType.NVarChar,400)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ShopName;
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
            parameters[38].Value = model.IsSuppApprove;
            parameters[39].Value = model.ScoreDesc;
            parameters[40].Value = model.ScoreService;
            parameters[41].Value = model.ScoreSpeed;
            parameters[42].Value = model.Recomend;
            parameters[43].Value = model.Sequence;
            parameters[44].Value = model.ProductCount;
            parameters[45].Value = model.PhotoCount;
            parameters[46].Value = model.ThemeId;
            parameters[47].Value = model.Remark;
            parameters[48].Value = model.AgentId;
            parameters[49].Value = model.IndexProdTop;
            parameters[50].Value = model.IndexContent;

            parameters[51].Value = model.Latitude;
            parameters[52].Value = model.Longitude;

            parameters[53].Value = model.SupplierId;
            parameters[54].Value = model.Slogan;
            parameters[55].Value = model.Discount;
            parameters[56].Value = model.BGType;
            parameters[57].Value = model.Background;

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
        public bool Delete(int SupplierId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Suppliers ");
            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4)
			};
            parameters[0].Value = SupplierId;

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
        public bool DeleteList(string SupplierIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Suppliers ");
            strSql.Append(" where SupplierId in (" + SupplierIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModel(int SupplierId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupplierId,Name,ShopName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId,IndexProdTop,IndexContent,Latitude,Longitude,Slogan,Discount,Slogan,Discount,BGType,Background from Shop_Suppliers ");
            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4)
			};
            parameters[0].Value = SupplierId;

            Maticsoft.Model.Shop.Supplier.SupplierInfo model = new Maticsoft.Model.Shop.Supplier.SupplierInfo();
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
        public Maticsoft.Model.Shop.Supplier.SupplierInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierInfo model = new Maticsoft.Model.Shop.Supplier.SupplierInfo();
            if (row != null)
            {
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["ShopName"] != null)
                {
                    model.ShopName = row["ShopName"].ToString();
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
                if (row["IsSuppApprove"] != null && row["IsSuppApprove"].ToString() != "")
                {
                    if ((row["IsSuppApprove"].ToString() == "1") || (row["IsSuppApprove"].ToString().ToLower() == "true"))
                    {
                        model.IsSuppApprove = true;
                    }
                    else
                    {
                        model.IsSuppApprove = false;
                    }
                }
                if (row["ScoreDesc"] != null && row["ScoreDesc"].ToString() != "")
                {
                    model.ScoreDesc = decimal.Parse(row["ScoreDesc"].ToString());
                }
                if (row["ScoreService"] != null && row["ScoreService"].ToString() != "")
                {
                    model.ScoreService = decimal.Parse(row["ScoreService"].ToString());
                }
                if (row["ScoreSpeed"] != null && row["ScoreSpeed"].ToString() != "")
                {
                    model.ScoreSpeed = decimal.Parse(row["ScoreSpeed"].ToString());
                }
                if (row["Recomend"] != null && row["Recomend"].ToString() != "")
                {
                    model.Recomend = int.Parse(row["Recomend"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["ProductCount"] != null && row["ProductCount"].ToString() != "")
                {
                    model.ProductCount = int.Parse(row["ProductCount"].ToString());
                }
                if (row["PhotoCount"] != null && row["PhotoCount"].ToString() != "")
                {
                    model.PhotoCount = int.Parse(row["PhotoCount"].ToString());
                }
                if (row["ThemeId"] != null && row["ThemeId"].ToString() != "")
                {
                    model.ThemeId = int.Parse(row["ThemeId"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["AgentId"] != null && row["AgentId"].ToString() != "")
                {
                    model.AgentId = int.Parse(row["AgentId"].ToString());
                }
                if (row["IndexProdTop"] != null && row["IndexProdTop"].ToString() != "")
                {
                    model.IndexProdTop = int.Parse(row["IndexProdTop"].ToString());
                }
                if (row["IndexContent"] != null)
                {
                    model.IndexContent = row["IndexContent"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(row["Latitude"].ToString()))
                {
                    model.Latitude = decimal.Parse(row["Latitude"].ToString());
                }
                if (row["Longitude"] != null && row["Longitude"].ToString()!="")
                {
                    model.Longitude = decimal.Parse(row["Longitude"].ToString());
                }
                if (row["Slogan"] != null && row["Slogan"].ToString() != "")
                {
                    model.Slogan = row["Slogan"].ToString();
                }
                if (row["Discount"] != null && row["Discount"].ToString() != "")
                {
                    model.Discount = row["Discount"].ToString();
                }
                if (row["BGType"] != null && row["BGType"].ToString() != "")
                {
                    model.BGType = Convert.ToBoolean(row["BGType"]);
                }
                if (row["Background"] != null && row["Background"].ToString() != "")
                {
                    model.Background = row["Background"].ToString();
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
            strSql.Append("select SupplierId,Name,ShopName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId,IndexProdTop,IndexContent,Latitude,Longitude,IsOnLine ,Slogan,Discount,BGType,Background");
            strSql.Append(" FROM Shop_Suppliers ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append("  order by Sequence  ");
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
            strSql.Append(" SupplierId,Name,ShopName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId,IndexProdTop,IndexContent ,Latitude,Longitude,Slogan,Discount,BGType,Background");
            strSql.Append(" FROM Shop_Suppliers ");
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
            strSql.Append("select count(1) FROM Shop_Suppliers ");
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
                strSql.Append("order by T.SupplierId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Suppliers T ");
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
            parameters[0].Value = "Shop_Suppliers";
            parameters[1].Value = "SupplierId";
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByUserId(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupplierId,Name,ShopName,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId,StoreStatus,Latitude,Longitude,Slogan,Discount,BGType,Background,IndexProdTop,IndexContent from Shop_Suppliers ");
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
        /// 供应商名称是否已存在
        /// </summary>
        public bool Exists(string Name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Suppliers");
            strSql.Append(" where Name=@Name");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = Name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Suppliers");
            strSql.Append(" where ShopName=@ShopName");
            SqlParameter[] parameters = {
					new SqlParameter("@ShopName", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = Name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name, int SupplierID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Suppliers");
            strSql.Append(" where ShopName=@ShopName AND SupplierID<>@SupplierID");
            SqlParameter[] parameters = {
					new SqlParameter("@ShopName", SqlDbType.NVarChar,100),
                     new SqlParameter("@SupplierID", SqlDbType.Int,4)
			};
            parameters[0].Value = Name;
            parameters[1].Value = SupplierID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 供应商名称是否已存在
        /// </summary>
        public bool Exists(string Name, int SupplierID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Suppliers");
            strSql.Append(" where Name=@Name AND SupplierID<>@SupplierID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@SupplierID", SqlDbType.Int,4)
			};
            parameters[0].Value = Name;
            parameters[1].Value = SupplierID;

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
            strSql.Append("update Shop_Suppliers set " + strWhere);
            strSql.Append(" where SupplierId in(" + IDlist + ")  ");
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

        public DataSet GetStatisticsSupply(int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
                @"
--剩余总量/额
SELECT 1 AS [Type], SUM(S.Stock) ToalQuantity
      , SUM(S.SalePrice * S.Stock) ToalPrice
FROM    Shop_SKUs S
WHERE   EXISTS ( SELECT *, P.MarketPrice
                 FROM   Shop_Products P
                 WHERE  S.ProductId = P.ProductId
                        AND P.SupplierId = {0} )
UNION ALL      
--已售量/额         
SELECT  2 AS [Type], SUM(I.Quantity) ToalQuantity
      , SUM(I.SellPrice * I.Quantity) ToalPrice
FROM    Shop_OrderItems I, Shop_Orders O
WHERE I.OrderId = O.OrderId AND O.SupplierId = {0} AND O.OrderStatus = 2 AND O.OrderType = 1
", supplierId);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetStatisticsSales(int supplierId, int year)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                @"
--销量/业绩走势图
SELECT  A.[Month] AS Mon
      , CASE WHEN B.ToalQuantity IS NULL THEN 0
             ELSE B.ToalQuantity
        END AS ToalQuantity
      , CASE WHEN B.ToalPrice IS NULL THEN 0.00
             ELSE B.ToalPrice
        END AS ToalPrice
FROM    ( SELECT    *
          FROM      GET_GeneratedMonthEx()
        ) A
        LEFT JOIN ( SELECT  MONTH(O.CreatedDate) Mon
                          , SUM(I.Quantity) ToalQuantity
                          , SUM(I.SellPrice) ToalPrice
                    FROM    Shop_OrderItems I
                          , Shop_Orders O
                    WHERE   I.OrderId = O.OrderId ");
            if (supplierId > 0)
            {
                strSql.AppendFormat("  AND O.SupplierId = {0}", supplierId);
            }
            strSql.AppendFormat(@" AND O.OrderStatus = 2 AND O.OrderType = 1
                            AND YEAR(O.CreatedDate) = '{0}'
                    GROUP BY MONTH(O.CreatedDate)
                  ) B ON A.[Month] = B.Mon 
", year);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 关闭店铺 
        /// </summary>
        public bool CloseShop(int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Suppliers set ");
            strSql.Append("StoreStatus=2");
            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4)};
            parameters[0].Value = SupplierId;
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
       /// <param name="ShopName"></param>
       /// <returns></returns>
        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByShopName(string ShopName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupplierId,Name,ShopName,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId,IndexProdTop,IndexContent,StoreStatus,Latitude,Longitude,Slogan,Discount,BGType,Background  from Shop_Suppliers ");
            strSql.Append(" where ShopName=@ShopName and Status=1 and StoreStatus=1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@ShopName", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = ShopName;
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
       /// 推荐
       /// </summary>
       /// <param name="SupplierId"></param>
       /// <param name="Rec"></param>
       /// <returns></returns>
        public bool SetRec(int SupplierId, int Rec)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update Shop_Suppliers set Recomend= {0} " , Rec);
            strSql.AppendFormat(" where SupplierId ={0} ", SupplierId);
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
        /// 删除一条数据 事物删除该商家的所以关联数据
       /// </summary>
       /// <param name="SupplierId"></param>
       /// <returns></returns>
        public bool DeleteEx(int SupplierId)
        {
         
           List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from  Shop_SuppProductStatModes WHERE SupplierId=@SupplierId ");
            SqlParameter[] parameters1 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters1[0].Value = SupplierId;
            CommandInfo cmd = new CommandInfo(strSql1.ToString(),parameters1);
            sqllist.Add(cmd);

            //StringBuilder strSql2 = new StringBuilder();
            //strSql2.Append("delete from   Shop_SuppProductCategories WHERE  EXISTS(SELECT ProductId FROM   Shop_Products p WHERE  SupplierId=@SupplierId and  Shop_SuppProductCategories.ProductId=p.ProductId) ");
            //SqlParameter[] parameters2 = {
            //       new SqlParameter("@SupplierId", SqlDbType.Int,4)
            //};
            //parameters2[0].Value = SupplierId;
            //cmd = new CommandInfo(strSql2.ToString(), parameters2 );
            //sqllist.Add(cmd);
 
            //StringBuilder strSql3 = new StringBuilder();
            //strSql3.Append(" delete from   Shop_Products WHERE SupplierId=@SupplierId ");
            //SqlParameter[] parameters3 = {
            //       new SqlParameter("@SupplierId", SqlDbType.Int,4)
            //};
            //parameters3[0].Value = SupplierId;
            //cmd = new CommandInfo(strSql3.ToString(), parameters3 );
            //sqllist.Add(cmd);

            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append(" delete from   Shop_SupplierCategories WHERE SupplierId=@SupplierId  ");
            SqlParameter[] parameters4 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters4[0].Value = SupplierId;
            cmd = new CommandInfo(strSql4.ToString(), parameters4 );
            sqllist.Add(cmd);

            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append(" delete from   Shop_SupplierAD WHERE SupplierId=@SupplierId  ");
            SqlParameter[] parameters5 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters5[0].Value = SupplierId;
            cmd = new CommandInfo(strSql5.ToString(), parameters5 );
            sqllist.Add(cmd);

             StringBuilder strSql6 = new StringBuilder();
             strSql6.Append(" delete from   Shop_SupplierConfig WHERE SupplierId=@SupplierId  ");
            SqlParameter[] parameters6 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters6[0].Value = SupplierId;
            cmd = new CommandInfo(strSql6.ToString(), parameters6 );
            sqllist.Add(cmd);

            StringBuilder strSql7 = new StringBuilder();
            strSql7.Append(" delete from   Shop_SupplierMenus WHERE SupplierId=@SupplierId  ");
            SqlParameter[] parameters7 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters7[0].Value = SupplierId;
            cmd = new CommandInfo(strSql7.ToString(), parameters7 );
            sqllist.Add(cmd);

            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append(" delete from   Shop_SupplierScoreDetails WHERE SupplierId=@SupplierId  ");
            SqlParameter[] parameters8 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters8[0].Value = SupplierId;
            cmd = new CommandInfo(strSql8.ToString(), parameters8 );
            sqllist.Add(cmd);

            StringBuilder strSql9 = new StringBuilder();
            strSql9.Append(" delete from   Shop_Suppliers  WHERE SupplierId=@SupplierId  ");
            SqlParameter[] parameters9 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters9[0].Value = SupplierId;
            cmd = new CommandInfo(strSql9.ToString(), parameters9, EffentNextType.ExcuteEffectRows);
            sqllist.Add(cmd);

         
            StringBuilder strSql10 = new StringBuilder();
            strSql10.Append("  UPDATE  Accounts_Users  SET UserType='UU' WHERE DepartmentID=@SupplierId  ");
            SqlParameter[] parameters10 = {
                   new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters10[0].Value = SupplierId;
            cmd = new CommandInfo(strSql10.ToString(), parameters10, EffentNextType.ExcuteEffectRows);
            sqllist.Add(cmd);

            if(ExistsTable("Shop_SuppDistProduct_" + SupplierId))
            {
                 StringBuilder strSql11 = new StringBuilder();
                 strSql11.AppendFormat("  DROP TABLE  Shop_SuppDistProduct_{0}", SupplierId);
                    SqlParameter[] parameters11 = {
                 };
                 cmd = new CommandInfo(strSql11.ToString(), parameters11, EffentNextType.ExcuteEffectRows);
                sqllist.Add(cmd);
                
            }

            if (ExistsTable("Shop_SuppDistSKU_" + SupplierId))
            {
                StringBuilder strSql12 = new StringBuilder();
                strSql12.AppendFormat("  DROP TABLE  Shop_SuppDistSKU_{0}", SupplierId);
                SqlParameter[] parameters12 = {
                 };
                cmd = new CommandInfo(strSql12.ToString(), parameters12, EffentNextType.ExcuteEffectRows);
                sqllist.Add(cmd);
            }

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断表是否存在  返回true存在，false不存在
        /// </summary>
        /// <param name="table">要判断的表的名称</param>
        /// <returns></returns>
        public bool ExistsTable(string table)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select 1 from sysobjects where name='{0}'", table);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据经纬度获取商家集合
        /// </summary>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <param name="range">范围</param>
        /// <returns></returns>
        public DataSet GetSupplierByPosition(double latitudeLow, double longitudeLow, double latitudeHigh, double longitudeHigh, double range)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupplierId,Name,ShopName,StoreStatus,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId,IndexProdTop,IndexContent,Latitude,Longitude ");
            strSql.Append(" FROM Shop_Suppliers ");
            strSql.AppendFormat(string.Format("where (  Latitude between {0} And {1}", latitudeLow, latitudeHigh));
            strSql.Append(" And ");
            strSql.AppendFormat(string.Format("  Longitude between {0} And {1} )", longitudeLow, longitudeHigh));
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod


        public bool Update(Model.Shop.Supplier.SupplierInfo model, int SupplierId, List<int> idList)
        {
            List<CommandInfo> listCommand = new List<CommandInfo>();

            #region Update SupplierInfo
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Suppliers set ");
            strSql.Append("Name=@Name,");
            strSql.Append("ShopName=@ShopName,");
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
            strSql.Append("IsSuppApprove=@IsSuppApprove,");
            strSql.Append("ScoreDesc=@ScoreDesc,");
            strSql.Append("ScoreService=@ScoreService,");
            strSql.Append("ScoreSpeed=@ScoreSpeed,");
            strSql.Append("Recomend=@Recomend,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("ProductCount=@ProductCount,");
            strSql.Append("PhotoCount=@PhotoCount,");
            strSql.Append("ThemeId=@ThemeId,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AgentId=@AgentId,");
            strSql.Append("IndexProdTop=@IndexProdTop,");
            strSql.Append("IndexContent=@IndexContent,");
            strSql.Append("Latitude=@Latitude,");
            strSql.Append("Longitude=@Longitude");

            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@ShopName", SqlDbType.NVarChar,100),
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
					new SqlParameter("@IsSuppApprove", SqlDbType.Bit,1),
					new SqlParameter("@ScoreDesc", SqlDbType.Money,8),
					new SqlParameter("@ScoreService", SqlDbType.Money,8),
					new SqlParameter("@ScoreSpeed", SqlDbType.Money,8),
					new SqlParameter("@Recomend", SqlDbType.SmallInt,2),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Int,4),
					new SqlParameter("@PhotoCount", SqlDbType.Int,4),
					new SqlParameter("@ThemeId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@AgentId", SqlDbType.Int,4),
					new SqlParameter("@IndexProdTop", SqlDbType.Int,4),
					new SqlParameter("@IndexContent", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Latitude", SqlDbType.Decimal,9),
                    new SqlParameter("@Longitude", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ShopName;
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
            parameters[38].Value = model.IsSuppApprove;
            parameters[39].Value = model.ScoreDesc;
            parameters[40].Value = model.ScoreService;
            parameters[41].Value = model.ScoreSpeed;
            parameters[42].Value = model.Recomend;
            parameters[43].Value = model.Sequence;
            parameters[44].Value = model.ProductCount;
            parameters[45].Value = model.PhotoCount;
            parameters[46].Value = model.ThemeId;
            parameters[47].Value = model.Remark;
            parameters[48].Value = model.AgentId;
            parameters[49].Value = model.IndexProdTop;
            parameters[50].Value = model.IndexContent;

            parameters[51].Value = model.Latitude;
            parameters[52].Value = model.Longitude;

            parameters[53].Value = model.SupplierId; 
            #endregion
            listCommand.Add(new CommandInfo(strSql.ToString(), parameters,EffentNextType.ExcuteEffectRows));

            #region delete Data
            string deleteSql = "delete    from Shop_SupplierBrands where SupplierId=@SupplierId";
            SqlParameter[] parameter = { new SqlParameter("@SupplierId", SqlDbType.Int) };
            parameter[0].Value = SupplierId;
            listCommand.Add(new CommandInfo(deleteSql, parameter, EffentNextType.None)); 
            #endregion

            #region Add Data
            StringBuilder insertSb = new StringBuilder();
            foreach (var id in idList)
            {
                insertSb.Append(
                    string.Format("insert into  Shop_SupplierBrands(SupplierId,BrandId) values({1},{0}) ;", id, SupplierId));
            }
            listCommand.Add(new CommandInfo(insertSb.ToString(), new SqlParameter[0], EffentNextType.ExcuteEffectRows)); 
            #endregion
           return DbHelperSQL.ExecuteSqlTran(listCommand) > 0;
        }


        public bool Add(int supplierId, List<int> idList)
        {
            StringBuilder insertSb = new StringBuilder();
            foreach (var id in idList)
            {
                insertSb.Append(
                    string.Format("insert into  Shop_SupplierBrands(SupplierId,BrandId) values({1},{0}) ;", id, supplierId));
            }
            return DbHelperSQL.ExecuteSql(insertSb.ToString())>0;
        }

    }

}

