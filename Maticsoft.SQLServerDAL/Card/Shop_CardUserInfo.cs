using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Maticsoft.DBUtility;
using Maticsoft.IDAL;
using System.Data.SqlClient;
using System.Data;

namespace Maticsoft.SQLServerDAL.Card
{
    /// <summary>
    /// 数据访问类:Shop_CardUserInfo
    /// </summary>
    public partial class Shop_CardUserInfo : IShop_CardUserInfo
    {
        public Shop_CardUserInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Shop_CardUserInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CardUserInfo");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop_CardUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_CardUserInfo(");
            strSql.Append("UserId,CardTypeNo,CardTypeName,TrueName,Name,Sex,CardNo,Password,CardId,Email,Moble,Tel,Address,CodeNo,BirthDay,IsMarry,Job,BookNo,InsureActiveDate,OutDate,BackPerson,BakPersonMoble,BakPersonNo,UserInfoStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,InsureNo,UserInfoOutNo,InsurePerson,InsureDate,ActiveDate,IsMainCard,CardSysId,NameOne,NameOneCardId,RelationshipOne,NameTwo,NameTwoCardId,RelationshipTwo,UserName)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@CardTypeNo,@CardTypeName,@TrueName,@Name,@Sex,@CardNo,@Password,@CardId,@Email,@Moble,@Tel,@Address,@CodeNo,@BirthDay,@IsMarry,@Job,@BookNo,@InsureActiveDate,@OutDate,@BackPerson,@BakPersonMoble,@BakPersonNo,@UserInfoStatus,@CREATEDATE,@CREATER,@MODIFYDATE,@MODIFYER,@InsureNo,@UserInfoOutNo,@InsurePerson,@InsureDate,@ActiveDate,@IsMainCard,@CardSysId,@NameOne,@NameOneCardId,@RelationshipOne,@NameTwo,@NameTwoCardId,@RelationshipTwo,@UserName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.NVarChar,100),
					new SqlParameter("@CardTypeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CardTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NChar,10),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@CardId", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@Moble", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@CodeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@BirthDay", SqlDbType.DateTime),
					new SqlParameter("@IsMarry", SqlDbType.Bit,1),
					new SqlParameter("@Job", SqlDbType.NVarChar,30),
					new SqlParameter("@BookNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsureActiveDate", SqlDbType.DateTime),
					new SqlParameter("@OutDate", SqlDbType.DateTime),
					new SqlParameter("@BackPerson", SqlDbType.NChar,10),
					new SqlParameter("@BakPersonMoble", SqlDbType.NVarChar,20),
					new SqlParameter("@BakPersonNo", SqlDbType.NVarChar,20),
					new SqlParameter("@UserInfoStatus", SqlDbType.Int,4),
					new SqlParameter("@CREATEDATE", SqlDbType.DateTime),
					new SqlParameter("@CREATER", SqlDbType.NVarChar,50),
					new SqlParameter("@MODIFYDATE", SqlDbType.DateTime),
					new SqlParameter("@MODIFYER", SqlDbType.NVarChar,50),
					new SqlParameter("@InsureNo", SqlDbType.NVarChar,30),
					new SqlParameter("@UserInfoOutNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurePerson", SqlDbType.NVarChar,20),
					new SqlParameter("@InsureDate", SqlDbType.DateTime),
					new SqlParameter("@ActiveDate", SqlDbType.DateTime),
					new SqlParameter("@IsMainCard", SqlDbType.Bit,1),
					new SqlParameter("@CardSysId", SqlDbType.Int,4),
					new SqlParameter("@NameOne", SqlDbType.NVarChar,50),
					new SqlParameter("@NameOneCardId", SqlDbType.NVarChar,50),
					new SqlParameter("@RelationshipOne", SqlDbType.NVarChar,20),
					new SqlParameter("@NameTwo", SqlDbType.NVarChar,50),
					new SqlParameter("@NameTwoCardId", SqlDbType.NVarChar,50),
					new SqlParameter("@RelationshipTwo", SqlDbType.NVarChar,20),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.CardTypeNo;
            parameters[2].Value = model.CardTypeName;
            parameters[3].Value = model.TrueName;
            parameters[4].Value = model.Name;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.CardNo;
            parameters[7].Value = model.Password;
            parameters[8].Value = model.CardId;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.Moble;
            parameters[11].Value = model.Tel;
            parameters[12].Value = model.Address;
            parameters[13].Value = model.CodeNo;
            parameters[14].Value = model.BirthDay;
            parameters[15].Value = model.IsMarry;
            parameters[16].Value = model.Job;
            parameters[17].Value = model.BookNo;
            parameters[18].Value = model.InsureActiveDate;
            parameters[19].Value = model.OutDate;
            parameters[20].Value = model.BackPerson;
            parameters[21].Value = model.BakPersonMoble;
            parameters[22].Value = model.BakPersonNo;
            parameters[23].Value = model.UserInfoStatus;
            parameters[24].Value = model.CREATEDATE;
            parameters[25].Value = model.CREATER;
            parameters[26].Value = model.MODIFYDATE;
            parameters[27].Value = model.MODIFYER;
            parameters[28].Value = model.InsureNo;
            parameters[29].Value = model.UserInfoOutNo;
            parameters[30].Value = model.InsurePerson;
            parameters[31].Value = model.InsureDate;
            parameters[32].Value = model.ActiveDate;
            parameters[33].Value = model.IsMainCard;
            parameters[34].Value = model.CardSysId;
            parameters[35].Value = model.NameOne;
            parameters[36].Value = model.NameOneCardId;
            parameters[37].Value = model.RelationshipOne;
            parameters[38].Value = model.NameTwo;
            parameters[39].Value = model.NameTwoCardId;
            parameters[40].Value = model.RelationshipTwo;
            parameters[41].Value = model.UserName;

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
        public bool Update(Maticsoft.Model.Shop_CardUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CardUserInfo set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CardTypeNo=@CardTypeNo,");
            strSql.Append("CardTypeName=@CardTypeName,");
            strSql.Append("TrueName=@TrueName,");
            strSql.Append("Name=@Name,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("Password=@Password,");
            strSql.Append("CardId=@CardId,");
            strSql.Append("Email=@Email,");
            strSql.Append("Moble=@Moble,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Address=@Address,");
            strSql.Append("CodeNo=@CodeNo,");
            strSql.Append("BirthDay=@BirthDay,");
            strSql.Append("IsMarry=@IsMarry,");
            strSql.Append("Job=@Job,");
            strSql.Append("BookNo=@BookNo,");
            strSql.Append("InsureActiveDate=@InsureActiveDate,");
            strSql.Append("OutDate=@OutDate,");
            strSql.Append("BackPerson=@BackPerson,");
            strSql.Append("BakPersonMoble=@BakPersonMoble,");
            strSql.Append("BakPersonNo=@BakPersonNo,");
            strSql.Append("UserInfoStatus=@UserInfoStatus,");
            strSql.Append("CREATEDATE=@CREATEDATE,");
            strSql.Append("CREATER=@CREATER,");
            strSql.Append("MODIFYDATE=@MODIFYDATE,");
            strSql.Append("MODIFYER=@MODIFYER,");
            strSql.Append("InsureNo=@InsureNo,");
            strSql.Append("UserInfoOutNo=@UserInfoOutNo,");
            strSql.Append("InsurePerson=@InsurePerson,");
            strSql.Append("InsureDate=@InsureDate,");
            strSql.Append("ActiveDate=@ActiveDate,");
            strSql.Append("IsMainCard=@IsMainCard,");
            strSql.Append("CardSysId=@CardSysId,");
            strSql.Append("NameOne=@NameOne,");
            strSql.Append("NameOneCardId=@NameOneCardId,");
            strSql.Append("RelationshipOne=@RelationshipOne,");
            strSql.Append("NameTwo=@NameTwo,");
            strSql.Append("NameTwoCardId=@NameTwoCardId,");
            strSql.Append("RelationshipTwo=@RelationshipTwo");
            strSql.Append("UserName=@UserName");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.NVarChar,100),
					new SqlParameter("@CardTypeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CardTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.Int),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@CardId", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@Moble", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@CodeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@BirthDay", SqlDbType.DateTime),
					new SqlParameter("@IsMarry", SqlDbType.Bit,1),
					new SqlParameter("@Job", SqlDbType.NVarChar,30),
					new SqlParameter("@BookNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsureActiveDate", SqlDbType.DateTime),
					new SqlParameter("@OutDate", SqlDbType.DateTime),
					new SqlParameter("@BackPerson", SqlDbType.NChar,10),
					new SqlParameter("@BakPersonMoble", SqlDbType.NVarChar,20),
					new SqlParameter("@BakPersonNo", SqlDbType.NVarChar,20),
					new SqlParameter("@UserInfoStatus", SqlDbType.Int,4),
					new SqlParameter("@CREATEDATE", SqlDbType.DateTime),
					new SqlParameter("@CREATER", SqlDbType.NVarChar,50),
					new SqlParameter("@MODIFYDATE", SqlDbType.DateTime),
					new SqlParameter("@MODIFYER", SqlDbType.NVarChar,50),
					new SqlParameter("@InsureNo", SqlDbType.NVarChar,30),
					new SqlParameter("@UserInfoOutNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurePerson", SqlDbType.NVarChar,20),
					new SqlParameter("@InsureDate", SqlDbType.DateTime),
					new SqlParameter("@ActiveDate", SqlDbType.DateTime),
					new SqlParameter("@IsMainCard", SqlDbType.Bit,1),
					new SqlParameter("@CardSysId", SqlDbType.Int,4),
					new SqlParameter("@NameOne", SqlDbType.NVarChar,50),
					new SqlParameter("@NameOneCardId", SqlDbType.NVarChar,50),
					new SqlParameter("@RelationshipOne", SqlDbType.NVarChar,20),
					new SqlParameter("@NameTwo", SqlDbType.NVarChar,50),
					new SqlParameter("@NameTwoCardId", SqlDbType.NVarChar,50),
					new SqlParameter("@RelationshipTwo", SqlDbType.NVarChar,20),
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.CardTypeNo;
            parameters[2].Value = model.CardTypeName;
            parameters[3].Value = model.TrueName;
            parameters[4].Value = model.Name;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.CardNo;
            parameters[7].Value = model.Password;
            parameters[8].Value = model.CardId;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.Moble;
            parameters[11].Value = model.Tel;
            parameters[12].Value = model.Address;
            parameters[13].Value = model.CodeNo;
            parameters[14].Value = model.BirthDay;
            parameters[15].Value = model.IsMarry;
            parameters[16].Value = model.Job;
            parameters[17].Value = model.BookNo;
            parameters[18].Value = model.InsureActiveDate;
            parameters[19].Value = model.OutDate;
            parameters[20].Value = model.BackPerson;
            parameters[21].Value = model.BakPersonMoble;
            parameters[22].Value = model.BakPersonNo;
            parameters[23].Value = model.UserInfoStatus;
            parameters[24].Value = model.CREATEDATE;
            parameters[25].Value = model.CREATER;
            parameters[26].Value = model.MODIFYDATE;
            parameters[27].Value = model.MODIFYER;
            parameters[28].Value = model.InsureNo;
            parameters[29].Value = model.UserInfoOutNo;
            parameters[30].Value = model.InsurePerson;
            parameters[31].Value = model.InsureDate;
            parameters[32].Value = model.ActiveDate;
          
            if (model.IsMainCard == "1")
            {
                parameters[33].Value = true;
            }
            else
            {
                parameters[33].Value = false;
            }
            parameters[34].Value = model.CardSysId;
            parameters[35].Value = model.NameOne;
            parameters[36].Value = model.NameOneCardId;
            parameters[37].Value = model.RelationshipOne;
            parameters[38].Value = model.NameTwo;
            parameters[39].Value = model.NameTwoCardId;
            parameters[40].Value = model.RelationshipTwo;
            parameters[41].Value = model.Id;
            parameters[42].Value = model.UserName;

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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CardUserInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CardUserInfo ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public Maticsoft.Model.Shop_CardUserInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserId,CardTypeNo,CardTypeName,TrueName,Name,Sex,CardNo,Password,CardId,Email,Moble,Tel,Address,CodeNo,BirthDay,IsMarry,Job,BookNo,InsureActiveDate,OutDate,BackPerson,BakPersonMoble,BakPersonNo,UserInfoStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,InsureNo,UserInfoOutNo,InsurePerson,InsureDate,ActiveDate,IsMainCard,CardSysId,NameOne,NameOneCardId,RelationshipOne,NameTwo,NameTwoCardId,RelationshipTwo,UserName from Shop_CardUserInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            Maticsoft.Model.Shop_CardUserInfo model = new Maticsoft.Model.Shop_CardUserInfo();
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

        public Maticsoft.Model.Shop_CardUserInfo GetActicedCardUser(string cardId)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT DISTINCT top 1  a.UserId,b.User_dateCreate FROM dbo.Shop_CardUserInfo a LEFT JOIN dbo.Accounts_Users b ON a.UserId = b.UserName  ");
            stb.AppendFormat(" WHERE a.CardId='{0}'", cardId);
            stb.Append(" ORDER BY b.User_dateCreate DESC");
            Maticsoft.Model.Shop_CardUserInfo model = new Maticsoft.Model.Shop_CardUserInfo();

            DataSet ds = DbHelperSQL.Query(stb.ToString());
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
        public Maticsoft.Model.Shop_CardUserInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop_CardUserInfo model = new Maticsoft.Model.Shop_CardUserInfo();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["UserId"] != null)
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["CardTypeNo"] != null)
                {
                    model.CardTypeNo = row["CardTypeNo"].ToString();
                }
                if (row["CardTypeName"] != null)
                {
                    model.CardTypeName = row["CardTypeName"].ToString();
                }
                if (row["TrueName"] != null)
                {
                    model.TrueName = row["TrueName"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Sex"] != null)
                {
                    model.Sex = Convert.ToInt16(row["Sex"].ToString());
                }
                if (row["CardNo"] != null)
                {
                    model.CardNo = row["CardNo"].ToString();
                }
                if (row["Password"] != null)
                {
                    model.Password = row["Password"].ToString();
                }
                if (row["CardId"] != null)
                {
                    model.CardId = row["CardId"].ToString();
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["Moble"] != null)
                {
                    model.Moble = row["Moble"].ToString();
                }
                if (row["Tel"] != null)
                {
                    model.Tel = row["Tel"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["CodeNo"] != null)
                {
                    model.CodeNo = row["CodeNo"].ToString();
                }
                if (row["BirthDay"] != null && row["BirthDay"].ToString() != "")
                {
                    model.BirthDay = DateTime.Parse(row["BirthDay"].ToString());
                }
                if (row["IsMarry"] != null && row["IsMarry"].ToString() != "")
                {
                    if ((row["IsMarry"].ToString() == "1") || (row["IsMarry"].ToString().ToLower() == "true"))
                    {
                        model.IsMarry = true;
                    }
                    else
                    {
                        model.IsMarry = false;
                    }
                }
                if (row["Job"] != null)
                {
                    model.Job = row["Job"].ToString();
                }
                if (row["BookNo"] != null)
                {
                    model.BookNo = row["BookNo"].ToString();
                }
                if (row["InsureActiveDate"] != null && row["InsureActiveDate"].ToString() != "")
                {
                    model.InsureActiveDate = DateTime.Parse(row["InsureActiveDate"].ToString());
                }
                if (row["OutDate"] != null && row["OutDate"].ToString() != "")
                {
                    model.OutDate = DateTime.Parse(row["OutDate"].ToString());
                }
                if (row["BackPerson"] != null)
                {
                    model.BackPerson = row["BackPerson"].ToString();
                }
                if (row["BakPersonMoble"] != null)
                {
                    model.BakPersonMoble = row["BakPersonMoble"].ToString();
                }
                if (row["BakPersonNo"] != null)
                {
                    model.BakPersonNo = row["BakPersonNo"].ToString();
                }
                if (row["UserInfoStatus"] != null && row["UserInfoStatus"].ToString() != "")
                {
                    model.UserInfoStatus = int.Parse(row["UserInfoStatus"].ToString());
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
                }
                if (row["CREATER"] != null)
                {
                    model.CREATER = row["CREATER"].ToString();
                }
                if (row["MODIFYDATE"] != null && row["MODIFYDATE"].ToString() != "")
                {
                    model.MODIFYDATE = DateTime.Parse(row["MODIFYDATE"].ToString());
                }
                if (row["MODIFYER"] != null)
                {
                    model.MODIFYER = row["MODIFYER"].ToString();
                }
                if (row["InsureNo"] != null)
                {
                    model.InsureNo = row["InsureNo"].ToString();
                }
                if (row["UserInfoOutNo"] != null)
                {
                    model.UserInfoOutNo = row["UserInfoOutNo"].ToString();
                }
                if (row["InsurePerson"] != null)
                {
                    model.InsurePerson = row["InsurePerson"].ToString();
                }
                if (row["InsureDate"] != null && row["InsureDate"].ToString() != "")
                {
                    model.InsureDate = DateTime.Parse(row["InsureDate"].ToString());
                }
                if (row["ActiveDate"] != null && row["ActiveDate"].ToString() != "")
                {
                    model.ActiveDate = DateTime.Parse(row["ActiveDate"].ToString());
                }
                if ((row["IsMainCard"].ToString() == "1") || (row["IsMainCard"].ToString().ToLower() == "true"))
                {
                    model.IsMainCard = "1";
                }
                else
                {
                    model.IsMainCard = "0";
                }
                if (row["CardSysId"] != null && row["CardSysId"].ToString() != "")
                {
                    model.CardSysId = int.Parse(row["CardSysId"].ToString());
                }
                if (row["NameOne"] != null)
                {
                    model.NameOne = row["NameOne"].ToString();
                }
                if (row["NameOneCardId"] != null)
                {
                    model.NameOneCardId = row["NameOneCardId"].ToString();
                }
                if (row["RelationshipOne"] != null)
                {
                    model.RelationshipOne = row["RelationshipOne"].ToString();
                }
                if (row["NameTwo"] != null)
                {
                    model.NameTwo = row["NameTwo"].ToString();
                }
                if (row["NameTwoCardId"] != null)
                {
                    model.NameTwoCardId = row["NameTwoCardId"].ToString();
                }
                if (row["RelationshipTwo"] != null)
                {
                    model.RelationshipTwo = row["RelationshipTwo"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
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
            strSql.Append("select Id,UserId,CardTypeNo,CardTypeName,TrueName,Name,Sex,CardNo,Password,CardId,Email,Moble,Tel,Address,CodeNo,BirthDay,IsMarry,Job,BookNo,InsureActiveDate,OutDate,BackPerson,BakPersonMoble,BakPersonNo,UserInfoStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,InsureNo,UserInfoOutNo,InsurePerson,InsureDate,ActiveDate,IsMainCard,CardSysId,NameOne,NameOneCardId,RelationshipOne,NameTwo,NameTwoCardId,RelationshipTwo,UserName ");
            strSql.Append(" FROM Shop_CardUserInfo ");
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
            strSql.Append(" Id,UserId,CardTypeNo,CardTypeName,TrueName,Name,Sex,CardNo,Password,CardId,Email,Moble,Tel,Address,CodeNo,BirthDay,IsMarry,Job,BookNo,InsureActiveDate,OutDate,BackPerson,BakPersonMoble,BakPersonNo,UserInfoStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,InsureNo,UserInfoOutNo,InsurePerson,InsureDate,ActiveDate,IsMainCard,CardSysId,NameOne,NameOneCardId,RelationshipOne,NameTwo,NameTwoCardId,RelationshipTwo,UserName ");
            strSql.Append(" FROM Shop_CardUserInfo ");
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
            strSql.Append("select count(1) FROM Shop_CardUserInfo ");
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
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_CardUserInfo T ");
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
            parameters[0].Value = "Shop_CardUserInfo";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetModelList(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,CardTypeNo,CardTypeName,TrueName,Name,Sex,CardNo,CardId,Email,Moble,Tel,Address,CodeNo,BirthDay,IsMarry,Job,BookNo,InsureActiveDate,OutDate,BackPerson,BakPersonMoble,BakPersonNo,UserInfoStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,InsureNo,UserInfoOutNo,InsurePerson,InsureDate,ActiveDate,IsMainCard,CardSysId,Password ,NameOne,NameOneCardId,RelationShipOne,NameTwo,NameTwoCardId,RelationShipTwo,UserName");
            strSql.Append(" FROM Shop_CardUserInfo ");
            if (!string.IsNullOrEmpty(username))
            {
                strSql.Append(" where UserName= " + username);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod


        /// <summary>
        /// 获取默认卡号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetDefaultCardNo(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 CardNo  ");
            strSql.Append("FROM Shop_CardUserInfo");
            strSql.Append(" where UserId=@username");
            strSql.Append(" order by CREATEDATE");
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.NVarChar,40)
			};
            parameters[0].Value = username;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取默认卡的系统编号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetDefaultCardsysID(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT CardSysId  ");
            strSql.Append("FROM Shop_CardUserInfo");
            strSql.Append(" where UserId=@username");
            strSql.Append(" AND IsMainCard=1");
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.NVarChar,40)
			};
            parameters[0].Value = username;
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
        /// 根据卡号来创建新用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="carduserinfo"></param>
        /// <returns></returns>
        public bool AddUser4Card(Maticsoft.Accounts.Bus.User user, Maticsoft.Model.Shop_CardUserInfo carduserinfo)
        {
            return true;
        }



        /// <summary>
        /// 根据用户名和卡号设置默认卡
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool SetDefaultCard(string username, string cardNo)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {

                        DbHelperSQL.ExecuteSqlTran4Indentity(GenDefaultCard(username, cardNo), transaction);


                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 这个逻辑暂时无用,第一张卡就是默认卡
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        private List<CommandInfo> GenDefaultCard(string username, string cardNo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            //先吧会员卡状态全部更新为非默认
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_CardUserInfo SET ");
            strSql.Append("IsMainCard =0 ");
            strSql.Append("where UserId=@username");
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.NVarChar,40)
										 };
            parameters[0].Value = username;
            list.Add(new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows));

            //设置默认卡
            StringBuilder Sql = new StringBuilder();
            Sql.Append("UPDATE Shop_CardUserInfo SET ");
            Sql.Append("IsMainCard =1 ");
            Sql.Append("where cardNo =@cardNo");
            SqlParameter[] pars ={
					 new SqlParameter("@cardNo",SqlDbType.NVarChar,36)
								 };
            pars[0].Value = cardNo;
            list.Add(new CommandInfo(Sql.ToString(), pars, EffentNextType.ExcuteEffectRows));

            return list;
        }

        public bool UpdateUserName(string oldusername,string newusername)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_CardUserInfo SET ");
            strSql.Append("UserId=@newusername ");
            strSql.Append(" Where UserId=@oldusername");
            SqlParameter[] parameters = {
					new SqlParameter("@newusername", SqlDbType.NVarChar,40),
                    new SqlParameter("@oldusername",SqlDbType.NVarChar,40)
										 };
            parameters[0].Value = newusername;
            parameters[1].Value = oldusername;

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

    }
}
