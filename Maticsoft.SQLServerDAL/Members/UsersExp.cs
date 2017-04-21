using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Members;

namespace Maticsoft.SQLServerDAL.Members
{
    /// <summary>
    /// 用户扩展类
    /// </summary>
    public partial class UsersExp : IUsersExp
    {
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserID", "Accounts_UsersExp");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_UsersExp");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
            parameters[0].Value = UserID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Members.UsersExpModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UsersExp(");
            strSql.Append("UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount,UserCardCode,UserCardType,RecommendUserID)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Gravatar,@Singature,@TelPhone,@QQ,@MSN,@HomePage,@Birthday,@BirthdayVisible,@BirthdayIndexVisible,@Constellation,@ConstellationVisible,@ConstellationIndexVisible,@NativePlace,@NativePlaceVisible,@NativePlaceIndexVisible,@RegionId,@Address,@AddressVisible,@AddressIndexVisible,@BodilyForm,@BodilyFormVisible,@BodilyFormIndexVisible,@BloodType,@BloodTypeVisible,@BloodTypeIndexVisible,@Marriaged,@MarriagedVisible,@MarriagedIndexVisible,@PersonalStatus,@PersonalStatusVisible,@PersonalStatusIndexVisible,@Grade,@Balance,@Points,@TopicCount,@ReplyTopicCount,@FavTopicCount,@PvCount,@FansCount,@FellowCount,@AblumsCount,@FavouritesCount,@FavoritedCount,@ShareCount,@ProductsCount,@PersonalDomain,@LastAccessTime,@LastAccessIP,@LastPostTime,@LastLoginTime,@Remark,@IsUserDPI,@PayAccount,@UserCardCode,@UserCardType,@RecommendUserID)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Gravatar", SqlDbType.NVarChar,200),
					new SqlParameter("@Singature", SqlDbType.NVarChar,200),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@MSN", SqlDbType.NVarChar,50),
					new SqlParameter("@HomePage", SqlDbType.NVarChar,50),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@BirthdayVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BirthdayIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Constellation", SqlDbType.NVarChar,50),
					new SqlParameter("@ConstellationVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@ConstellationIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@NativePlace", SqlDbType.NVarChar,300),
					new SqlParameter("@NativePlaceVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@NativePlaceIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@AddressVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@AddressIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@BodilyForm", SqlDbType.NVarChar,10),
					new SqlParameter("@BodilyFormVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BodilyFormIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@BloodType", SqlDbType.NVarChar,10),
					new SqlParameter("@BloodTypeVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BloodTypeIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Marriaged", SqlDbType.NVarChar,10),
					new SqlParameter("@MarriagedVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@MarriagedIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@PersonalStatus", SqlDbType.NVarChar,10),
					new SqlParameter("@PersonalStatusVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@PersonalStatusIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Grade", SqlDbType.Int,4),
					new SqlParameter("@Balance", SqlDbType.Money,8),
					new SqlParameter("@Points", SqlDbType.Int,4),
					new SqlParameter("@TopicCount", SqlDbType.Int,4),
					new SqlParameter("@ReplyTopicCount", SqlDbType.Int,4),
					new SqlParameter("@FavTopicCount", SqlDbType.Int,4),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@FansCount", SqlDbType.Int,4),
					new SqlParameter("@FellowCount", SqlDbType.Int,4),
					new SqlParameter("@AblumsCount", SqlDbType.Int,4),
					new SqlParameter("@FavouritesCount", SqlDbType.Int,4),
					new SqlParameter("@FavoritedCount", SqlDbType.Int,4),
					new SqlParameter("@ShareCount", SqlDbType.Int,4),
					new SqlParameter("@ProductsCount", SqlDbType.Int,4),
					new SqlParameter("@PersonalDomain", SqlDbType.NVarChar,50),
					new SqlParameter("@LastAccessTime", SqlDbType.DateTime),
					new SqlParameter("@LastAccessIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LastPostTime", SqlDbType.DateTime),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@IsUserDPI", SqlDbType.Bit,1),
					new SqlParameter("@PayAccount", SqlDbType.NVarChar,200),
					new SqlParameter("@UserCardCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserCardType", SqlDbType.SmallInt,2),
                    new SqlParameter("@RecommendUserID", SqlDbType.Int,4)
                    
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Gravatar;
            parameters[2].Value = model.Singature;
            parameters[3].Value = model.TelPhone;
            parameters[4].Value = model.QQ;
            parameters[5].Value = model.MSN;
            parameters[6].Value = model.HomePage;
            parameters[7].Value = model.Birthday;
            parameters[8].Value = model.BirthdayVisible;
            parameters[9].Value = model.BirthdayIndexVisible;
            parameters[10].Value = model.Constellation;
            parameters[11].Value = model.ConstellationVisible;
            parameters[12].Value = model.ConstellationIndexVisible;
            parameters[13].Value = model.NativePlace;
            parameters[14].Value = model.NativePlaceVisible;
            parameters[15].Value = model.NativePlaceIndexVisible;
            parameters[16].Value = model.RegionId;
            parameters[17].Value = model.Address;
            parameters[18].Value = model.AddressVisible;
            parameters[19].Value = model.AddressIndexVisible;
            parameters[20].Value = model.BodilyForm;
            parameters[21].Value = model.BodilyFormVisible;
            parameters[22].Value = model.BodilyFormIndexVisible;
            parameters[23].Value = model.BloodType;
            parameters[24].Value = model.BloodTypeVisible;
            parameters[25].Value = model.BloodTypeIndexVisible;
            parameters[26].Value = model.Marriaged;
            parameters[27].Value = model.MarriagedVisible;
            parameters[28].Value = model.MarriagedIndexVisible;
            parameters[29].Value = model.PersonalStatus;
            parameters[30].Value = model.PersonalStatusVisible;
            parameters[31].Value = model.PersonalStatusIndexVisible;
            parameters[32].Value = model.Grade;
            parameters[33].Value = model.Balance;
            parameters[34].Value = model.Points;
            parameters[35].Value = model.TopicCount;
            parameters[36].Value = model.ReplyTopicCount;
            parameters[37].Value = model.FavTopicCount;
            parameters[38].Value = model.PvCount;
            parameters[39].Value = model.FansCount;
            parameters[40].Value = model.FellowCount;
            parameters[41].Value = model.AblumsCount;
            parameters[42].Value = model.FavouritesCount;
            parameters[43].Value = model.FavoritedCount;
            parameters[44].Value = model.ShareCount;
            parameters[45].Value = model.ProductsCount;
            parameters[46].Value = model.PersonalDomain;
            parameters[47].Value = model.LastAccessTime;
            parameters[48].Value = model.LastAccessIP;
            parameters[49].Value = model.LastPostTime;
            parameters[50].Value = model.LastLoginTime;
            parameters[51].Value = model.Remark;
            parameters[52].Value = model.IsUserDPI;
            parameters[53].Value = model.PayAccount;
            parameters[54].Value = model.UserCardCode;
            parameters[55].Value = model.UserCardType;
            parameters[56].Value = model.RecommendUserID;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.UsersExpModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UsersExp set ");
            strSql.Append("Gravatar=@Gravatar,");
            strSql.Append("Singature=@Singature,");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("MSN=@MSN,");
            strSql.Append("HomePage=@HomePage,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("BirthdayVisible=@BirthdayVisible,");
            strSql.Append("BirthdayIndexVisible=@BirthdayIndexVisible,");
            strSql.Append("Constellation=@Constellation,");
            strSql.Append("ConstellationVisible=@ConstellationVisible,");
            strSql.Append("ConstellationIndexVisible=@ConstellationIndexVisible,");
            strSql.Append("NativePlace=@NativePlace,");
            strSql.Append("NativePlaceVisible=@NativePlaceVisible,");
            strSql.Append("NativePlaceIndexVisible=@NativePlaceIndexVisible,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("Address=@Address,");
            strSql.Append("AddressVisible=@AddressVisible,");
            strSql.Append("AddressIndexVisible=@AddressIndexVisible,");
            strSql.Append("BodilyForm=@BodilyForm,");
            strSql.Append("BodilyFormVisible=@BodilyFormVisible,");
            strSql.Append("BodilyFormIndexVisible=@BodilyFormIndexVisible,");
            strSql.Append("BloodType=@BloodType,");
            strSql.Append("BloodTypeVisible=@BloodTypeVisible,");
            strSql.Append("BloodTypeIndexVisible=@BloodTypeIndexVisible,");
            strSql.Append("Marriaged=@Marriaged,");
            strSql.Append("MarriagedVisible=@MarriagedVisible,");
            strSql.Append("MarriagedIndexVisible=@MarriagedIndexVisible,");
            strSql.Append("PersonalStatus=@PersonalStatus,");
            strSql.Append("PersonalStatusVisible=@PersonalStatusVisible,");
            strSql.Append("PersonalStatusIndexVisible=@PersonalStatusIndexVisible,");
            strSql.Append("Grade=@Grade,");
            strSql.Append("Balance=@Balance,");
            strSql.Append("Points=@Points,");
            strSql.Append("TopicCount=@TopicCount,");
            strSql.Append("ReplyTopicCount=@ReplyTopicCount,");
            strSql.Append("FavTopicCount=@FavTopicCount,");
            strSql.Append("PvCount=@PvCount,");
            strSql.Append("FansCount=@FansCount,");
            strSql.Append("FellowCount=@FellowCount,");
            strSql.Append("AblumsCount=@AblumsCount,");
            strSql.Append("FavouritesCount=@FavouritesCount,");
            strSql.Append("FavoritedCount=@FavoritedCount,");
            strSql.Append("ShareCount=@ShareCount,");
            strSql.Append("ProductsCount=@ProductsCount,");
            strSql.Append("PersonalDomain=@PersonalDomain,");
            strSql.Append("LastAccessTime=@LastAccessTime,");
            strSql.Append("LastAccessIP=@LastAccessIP,");
            strSql.Append("LastPostTime=@LastPostTime,");
            strSql.Append("LastLoginTime=@LastLoginTime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("IsUserDPI=@IsUserDPI,");
            strSql.Append("PayAccount=@PayAccount,");
            strSql.Append("UserCardCode=@UserCardCode,");
            strSql.Append("UserCardType=@UserCardType,");
            strSql.Append("UserOldType=@UserOldType");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Gravatar", SqlDbType.NVarChar,200),
					new SqlParameter("@Singature", SqlDbType.NVarChar,200),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@MSN", SqlDbType.NVarChar,50),
					new SqlParameter("@HomePage", SqlDbType.NVarChar,50),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@BirthdayVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BirthdayIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Constellation", SqlDbType.NVarChar,50),
					new SqlParameter("@ConstellationVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@ConstellationIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@NativePlace", SqlDbType.NVarChar,300),
					new SqlParameter("@NativePlaceVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@NativePlaceIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@AddressVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@AddressIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@BodilyForm", SqlDbType.NVarChar,10),
					new SqlParameter("@BodilyFormVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BodilyFormIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@BloodType", SqlDbType.NVarChar,10),
					new SqlParameter("@BloodTypeVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BloodTypeIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Marriaged", SqlDbType.NVarChar,10),
					new SqlParameter("@MarriagedVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@MarriagedIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@PersonalStatus", SqlDbType.NVarChar,10),
					new SqlParameter("@PersonalStatusVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@PersonalStatusIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Grade", SqlDbType.Int,4),
					new SqlParameter("@Balance", SqlDbType.Money,8),
					new SqlParameter("@Points", SqlDbType.Int,4),
					new SqlParameter("@TopicCount", SqlDbType.Int,4),
					new SqlParameter("@ReplyTopicCount", SqlDbType.Int,4),
					new SqlParameter("@FavTopicCount", SqlDbType.Int,4),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@FansCount", SqlDbType.Int,4),
					new SqlParameter("@FellowCount", SqlDbType.Int,4),
					new SqlParameter("@AblumsCount", SqlDbType.Int,4),
					new SqlParameter("@FavouritesCount", SqlDbType.Int,4),
					new SqlParameter("@FavoritedCount", SqlDbType.Int,4),
					new SqlParameter("@ShareCount", SqlDbType.Int,4),
					new SqlParameter("@ProductsCount", SqlDbType.Int,4),
					new SqlParameter("@PersonalDomain", SqlDbType.NVarChar,50),
					new SqlParameter("@LastAccessTime", SqlDbType.DateTime),
					new SqlParameter("@LastAccessIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LastPostTime", SqlDbType.DateTime),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@IsUserDPI", SqlDbType.Bit,1),
					new SqlParameter("@PayAccount", SqlDbType.NVarChar,200),
					new SqlParameter("@UserCardCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserCardType", SqlDbType.SmallInt,2),
                    new SqlParameter("@UserOldType",SqlDbType.Int,8),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = model.Gravatar;
            parameters[1].Value = model.Singature;
            parameters[2].Value = model.TelPhone;
            parameters[3].Value = model.QQ;
            parameters[4].Value = model.MSN;
            parameters[5].Value = model.HomePage;
            parameters[6].Value = model.Birthday;
            parameters[7].Value = model.BirthdayVisible;
            parameters[8].Value = model.BirthdayIndexVisible;
            parameters[9].Value = model.Constellation;
            parameters[10].Value = model.ConstellationVisible;
            parameters[11].Value = model.ConstellationIndexVisible;
            parameters[12].Value = model.NativePlace;
            parameters[13].Value = model.NativePlaceVisible;
            parameters[14].Value = model.NativePlaceIndexVisible;
            parameters[15].Value = model.RegionId;
            parameters[16].Value = model.Address;
            parameters[17].Value = model.AddressVisible;
            parameters[18].Value = model.AddressIndexVisible;
            parameters[19].Value = model.BodilyForm;
            parameters[20].Value = model.BodilyFormVisible;
            parameters[21].Value = model.BodilyFormIndexVisible;
            parameters[22].Value = model.BloodType;
            parameters[23].Value = model.BloodTypeVisible;
            parameters[24].Value = model.BloodTypeIndexVisible;
            parameters[25].Value = model.Marriaged;
            parameters[26].Value = model.MarriagedVisible;
            parameters[27].Value = model.MarriagedIndexVisible;
            parameters[28].Value = model.PersonalStatus;
            parameters[29].Value = model.PersonalStatusVisible;
            parameters[30].Value = model.PersonalStatusIndexVisible;
            parameters[31].Value = model.Grade;
            parameters[32].Value = model.Balance;
            parameters[33].Value = model.Points;
            parameters[34].Value = model.TopicCount;
            parameters[35].Value = model.ReplyTopicCount;
            parameters[36].Value = model.FavTopicCount;
            parameters[37].Value = model.PvCount;
            parameters[38].Value = model.FansCount;
            parameters[39].Value = model.FellowCount;
            parameters[40].Value = model.AblumsCount;
            parameters[41].Value = model.FavouritesCount;
            parameters[42].Value = model.FavoritedCount;
            parameters[43].Value = model.ShareCount;
            parameters[44].Value = model.ProductsCount;
            parameters[45].Value = model.PersonalDomain;
            parameters[46].Value = model.LastAccessTime;
            parameters[47].Value = model.LastAccessIP;
            parameters[48].Value = model.LastPostTime;
            parameters[49].Value = model.LastLoginTime;
            parameters[50].Value = model.Remark;
            parameters[51].Value = model.IsUserDPI;
            parameters[52].Value = model.PayAccount;
            parameters[53].Value = model.UserCardCode;
            parameters[54].Value = model.UserCardType;
            parameters[55].Value = model.UserOldType;
            parameters[56].Value = model.UserID;

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
        /// 更新一条数据 申请好代
        /// </summary>
        public bool UpdateApplyAgent(Maticsoft.Model.Members.UsersExpModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UsersExp set ");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("Address=@Address,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UserAppType=@UserAppType,");
            strSql.Append("UserStatus=@UserStatus,");
            strSql.Append("IsAppUserType=@IsAppUserType");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@UserAppType", SqlDbType.Int,4),
					new SqlParameter("@UserStatus", SqlDbType.SmallInt),
					new SqlParameter("@IsAppUserType", SqlDbType.SmallInt),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = model.TelPhone;
            parameters[1].Value = model.RegionId;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.UserAppType;
            parameters[5].Value = model.UserStatus;
            parameters[6].Value = model.IsAppUserType;
            parameters[7].Value = model.UserID;


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
        public bool Delete(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UsersExp ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
            parameters[0].Value = UserID;

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
        public bool DeleteList(string UserIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UsersExp ");
            strSql.Append(" where UserID in (" + UserIDlist + ")  ");
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
        public Maticsoft.Model.Members.UsersExpModel GetModel(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount,UserCardCode,UserCardType,UserAppType, UserStatus,UserOldType,IsAppUserType,Probation,SalesNo,RecommendUserID from Accounts_UsersExp ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
            parameters[0].Value = UserID;

            Maticsoft.Model.Members.UsersExpModel model = new Maticsoft.Model.Members.UsersExpModel();
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
        public Maticsoft.Model.Members.UsersExpModel DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.UsersExpModel model = new Maticsoft.Model.Members.UsersExpModel();
            if (row != null)
            {
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["Gravatar"] != null)
                {
                    model.Gravatar = row["Gravatar"].ToString();
                }
                if (row["Singature"] != null)
                {
                    model.Singature = row["Singature"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    model.TelPhone = row["TelPhone"].ToString();
                }
                if (row["QQ"] != null)
                {
                    model.QQ = row["QQ"].ToString();
                }
                if (row["MSN"] != null)
                {
                    model.MSN = row["MSN"].ToString();
                }
                if (row["HomePage"] != null)
                {
                    model.HomePage = row["HomePage"].ToString();
                }
                if (row["Birthday"] != null && row["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(row["Birthday"].ToString());
                }
                if (row["BirthdayVisible"] != null && row["BirthdayVisible"].ToString() != "")
                {
                    model.BirthdayVisible = int.Parse(row["BirthdayVisible"].ToString());
                }
                if (row["BirthdayIndexVisible"] != null && row["BirthdayIndexVisible"].ToString() != "")
                {
                    if ((row["BirthdayIndexVisible"].ToString() == "1") || (row["BirthdayIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.BirthdayIndexVisible = true;
                    }
                    else
                    {
                        model.BirthdayIndexVisible = false;
                    }
                }
                if (row["Constellation"] != null)
                {
                    model.Constellation = row["Constellation"].ToString();
                }
                if (row["ConstellationVisible"] != null && row["ConstellationVisible"].ToString() != "")
                {
                    model.ConstellationVisible = int.Parse(row["ConstellationVisible"].ToString());
                }
                if (row["ConstellationIndexVisible"] != null && row["ConstellationIndexVisible"].ToString() != "")
                {
                    if ((row["ConstellationIndexVisible"].ToString() == "1") || (row["ConstellationIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.ConstellationIndexVisible = true;
                    }
                    else
                    {
                        model.ConstellationIndexVisible = false;
                    }
                }
                if (row["NativePlace"] != null)
                {
                    model.NativePlace = row["NativePlace"].ToString();
                }
                if (row["NativePlaceVisible"] != null && row["NativePlaceVisible"].ToString() != "")
                {
                    model.NativePlaceVisible = int.Parse(row["NativePlaceVisible"].ToString());
                }
                if (row["NativePlaceIndexVisible"] != null && row["NativePlaceIndexVisible"].ToString() != "")
                {
                    if ((row["NativePlaceIndexVisible"].ToString() == "1") || (row["NativePlaceIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.NativePlaceIndexVisible = true;
                    }
                    else
                    {
                        model.NativePlaceIndexVisible = false;
                    }
                }
                if (row["RegionId"] != null && row["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["AddressVisible"] != null && row["AddressVisible"].ToString() != "")
                {
                    model.AddressVisible = int.Parse(row["AddressVisible"].ToString());
                }
                if (row["AddressIndexVisible"] != null && row["AddressIndexVisible"].ToString() != "")
                {
                    if ((row["AddressIndexVisible"].ToString() == "1") || (row["AddressIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.AddressIndexVisible = true;
                    }
                    else
                    {
                        model.AddressIndexVisible = false;
                    }
                }
                if (row["BodilyForm"] != null)
                {
                    model.BodilyForm = row["BodilyForm"].ToString();
                }
                if (row["BodilyFormVisible"] != null && row["BodilyFormVisible"].ToString() != "")
                {
                    model.BodilyFormVisible = int.Parse(row["BodilyFormVisible"].ToString());
                }
                if (row["BodilyFormIndexVisible"] != null && row["BodilyFormIndexVisible"].ToString() != "")
                {
                    if ((row["BodilyFormIndexVisible"].ToString() == "1") || (row["BodilyFormIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.BodilyFormIndexVisible = true;
                    }
                    else
                    {
                        model.BodilyFormIndexVisible = false;
                    }
                }
                if (row["BloodType"] != null)
                {
                    model.BloodType = row["BloodType"].ToString();
                }
                if (row["BloodTypeVisible"] != null && row["BloodTypeVisible"].ToString() != "")
                {
                    model.BloodTypeVisible = int.Parse(row["BloodTypeVisible"].ToString());
                }
                if (row["BloodTypeIndexVisible"] != null && row["BloodTypeIndexVisible"].ToString() != "")
                {
                    if ((row["BloodTypeIndexVisible"].ToString() == "1") || (row["BloodTypeIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.BloodTypeIndexVisible = true;
                    }
                    else
                    {
                        model.BloodTypeIndexVisible = false;
                    }
                }
                if (row["Marriaged"] != null)
                {
                    model.Marriaged = row["Marriaged"].ToString();
                }
                if (row["MarriagedVisible"] != null && row["MarriagedVisible"].ToString() != "")
                {
                    model.MarriagedVisible = int.Parse(row["MarriagedVisible"].ToString());
                }
                if (row["MarriagedIndexVisible"] != null && row["MarriagedIndexVisible"].ToString() != "")
                {
                    if ((row["MarriagedIndexVisible"].ToString() == "1") || (row["MarriagedIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.MarriagedIndexVisible = true;
                    }
                    else
                    {
                        model.MarriagedIndexVisible = false;
                    }
                }
                if (row["PersonalStatus"] != null)
                {
                    model.PersonalStatus = row["PersonalStatus"].ToString();
                }
                if (row["PersonalStatusVisible"] != null && row["PersonalStatusVisible"].ToString() != "")
                {
                    model.PersonalStatusVisible = int.Parse(row["PersonalStatusVisible"].ToString());
                }
                if (row["PersonalStatusIndexVisible"] != null && row["PersonalStatusIndexVisible"].ToString() != "")
                {
                    if ((row["PersonalStatusIndexVisible"].ToString() == "1") || (row["PersonalStatusIndexVisible"].ToString().ToLower() == "true"))
                    {
                        model.PersonalStatusIndexVisible = true;
                    }
                    else
                    {
                        model.PersonalStatusIndexVisible = false;
                    }
                }
                if (row["Grade"] != null && row["Grade"].ToString() != "")
                {
                    model.Grade = int.Parse(row["Grade"].ToString());
                }
                if (row["Balance"] != null && row["Balance"].ToString() != "")
                {
                    model.Balance = decimal.Parse(row["Balance"].ToString());
                }
                if (row["Points"] != null && row["Points"].ToString() != "")
                {
                    model.Points = int.Parse(row["Points"].ToString());
                }
                if (row["TopicCount"] != null && row["TopicCount"].ToString() != "")
                {
                    model.TopicCount = int.Parse(row["TopicCount"].ToString());
                }
                if (row["ReplyTopicCount"] != null && row["ReplyTopicCount"].ToString() != "")
                {
                    model.ReplyTopicCount = int.Parse(row["ReplyTopicCount"].ToString());
                }
                if (row["FavTopicCount"] != null && row["FavTopicCount"].ToString() != "")
                {
                    model.FavTopicCount = int.Parse(row["FavTopicCount"].ToString());
                }
                if (row["PvCount"] != null && row["PvCount"].ToString() != "")
                {
                    model.PvCount = int.Parse(row["PvCount"].ToString());
                }
                if (row["FansCount"] != null && row["FansCount"].ToString() != "")
                {
                    model.FansCount = int.Parse(row["FansCount"].ToString());
                }
                if (row["FellowCount"] != null && row["FellowCount"].ToString() != "")
                {
                    model.FellowCount = int.Parse(row["FellowCount"].ToString());
                }
                if (row["AblumsCount"] != null && row["AblumsCount"].ToString() != "")
                {
                    model.AblumsCount = int.Parse(row["AblumsCount"].ToString());
                }
                if (row["FavouritesCount"] != null && row["FavouritesCount"].ToString() != "")
                {
                    model.FavouritesCount = int.Parse(row["FavouritesCount"].ToString());
                }
                if (row["FavoritedCount"] != null && row["FavoritedCount"].ToString() != "")
                {
                    model.FavoritedCount = int.Parse(row["FavoritedCount"].ToString());
                }
                if (row["ShareCount"] != null && row["ShareCount"].ToString() != "")
                {
                    model.ShareCount = int.Parse(row["ShareCount"].ToString());
                }
                if (row["ProductsCount"] != null && row["ProductsCount"].ToString() != "")
                {
                    model.ProductsCount = int.Parse(row["ProductsCount"].ToString());
                }
                if (row["PersonalDomain"] != null)
                {
                    model.PersonalDomain = row["PersonalDomain"].ToString();
                }
                if (row["LastAccessTime"] != null && row["LastAccessTime"].ToString() != "")
                {
                    model.LastAccessTime = DateTime.Parse(row["LastAccessTime"].ToString());
                }
                if (row["LastAccessIP"] != null)
                {
                    model.LastAccessIP = row["LastAccessIP"].ToString();
                }
                if (row["LastPostTime"] != null && row["LastPostTime"].ToString() != "")
                {
                    model.LastPostTime = DateTime.Parse(row["LastPostTime"].ToString());
                }
                if (row["LastLoginTime"] != null && row["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(row["LastLoginTime"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["IsUserDPI"] != null && row["IsUserDPI"].ToString() != "")
                {
                    if ((row["IsUserDPI"].ToString() == "1") || (row["IsUserDPI"].ToString().ToLower() == "true"))
                    {
                        model.IsUserDPI = true;
                    }
                    else
                    {
                        model.IsUserDPI = false;
                    }
                }
                if (row["PayAccount"] != null)
                {
                    model.PayAccount = row["PayAccount"].ToString();
                }
                if (row["UserCardCode"] != null)
                {
                    model.UserCardCode = row["UserCardCode"].ToString();
                }
                if (row["UserCardType"] != null && row["UserCardType"].ToString() != "")
                {
                    model.UserCardType = int.Parse(row["UserCardType"].ToString());
                }
                ///扩展字段
                if (row["UserAppType"] != null && row["UserAppType"].ToString() != "")
                {
                    model.UserAppType = int.Parse(row["UserAppType"].ToString());
                }
                if (row["UserOldType"] != null && row["UserOldType"].ToString() != "")
                {
                    model.UserOldType = int.Parse(row["UserOldType"].ToString());
                }
                if (row["UserStatus"] != null && row["UserStatus"].ToString() != "")
                {
                    model.UserStatus = int.Parse(row["UserStatus"].ToString());
                }
                if (row["IsAppUserType"] != null && row["IsAppUserType"].ToString() != "")
                {
                    model.IsAppUserType = row["IsAppUserType"].ToString() == "1" ? true : false;
                }
                if (row["Probation"] != null && row["Probation"].ToString() != "")
                {
                    model.Probation = int.Parse(row["Probation"].ToString());
                }
                if (row["SalesNo"] != null && row["SalesNo"].ToString() != "")
                {
                    model.SalesNo = row["SalesNo"].ToString();
                }
                if (row["RecommendUserID"] != null && row["RecommendUserID"].ToString() != "")
                {
                    model.RecommendUserID = int.Parse(row["RecommendUserID"].ToString());
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
            strSql.Append("select UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount,UserCardCode,UserCardType,UserAppType, UserStatus,UserOldType,IsAppUserType,Probation");
            strSql.Append(" FROM Accounts_UsersExp ");
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
            strSql.Append(" UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount,UserCardCode,UserCardType,UserAppType, UserStatus,UserOldType,IsAppUserType,Probation");
            strSql.Append(" FROM Accounts_UsersExp ");
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
            strSql.Append("select count(1) FROM Accounts_UsersExp ");
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
                strSql.Append("order by T.UserID desc");
            }
            strSql.Append(")AS Row, T.*  from Accounts_UsersExp T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  BasicMethod

        #region 扩展方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(int userID)
        {
            int rowsAffected = 0;
            SqlParameter[] param = {
                                   new SqlParameter("@UserID",SqlDbType.Int)
                                   };
            param[0].Value = userID;
            DbHelperSQL.RunProcedure("sp_Accounts_CreateUserExp", param, out rowsAffected);
            if (rowsAffected > 0)
            {
                return true;
            } return false;
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetUserList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Accounts_Users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool UpdateFavouritesCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_UsersExp SET ");
            strSql.Append("FavouritesCount=( select COUNT(1) from SNS_UserFavourite where CreatedUserID=Accounts_UsersExp.UserID)");
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

        public bool UpdateProductCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_UsersExp SET ");
            strSql.Append("ProductsCount=(select COUNT(1) from SNS_Products where CreateUserID=Accounts_UsersExp.UserID)");
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

        public bool UpdateShareCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_UsersExp SET ");
            strSql.Append("ShareCount=ProductsCount+(select COUNT(1) from SNS_Photos where CreatedUserID=Accounts_UsersExp.UserID)");
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

        public bool UpdateAblumsCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_UsersExp SET ");
            strSql.Append("AblumsCount=(select COUNT(1) from SNS_UserAlbums where CreatedUserID=Accounts_UsersExp.UserID)");
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

        public int GetUserCountByKeyWord(string NickName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM  Accounts_Users au inner JOIN Accounts_UsersExp uea ON au.UserID=uea.UserID  ");
            if (!string.IsNullOrEmpty(NickName))
            {
                strSql.Append("AND NickName LIKE '%" + NickName + "%'");
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
        public DataSet GetUserListByKeyWord(string NickName, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("ORDER BY T." + orderby);
            }
            else
            {
                strSql.Append("ORDER BY T.UserID desc");
            }
            strSql.Append(")AS Row, T.*  FROM (SELECT uea.*,au.NickName FROM Accounts_Users au inner JOIN Accounts_UsersExp uea ON au.UserID=uea.UserID  ");
            if (!string.IsNullOrEmpty(NickName))
            {
                strSql.Append(" AND NickName LIKE @NickName");
            }
            strSql.Append(" ) T) TT");
            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);

            SqlParameter[] parameters = {
                    new SqlParameter("@NickName", SqlDbType.NVarChar)};
            parameters[0].Value = "%" + NickName + "%";
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否通过实名认证
        /// </summary>
        public bool UpdateIsDPI(string userIds, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("UPDATE UE SET IsUserDPI={0} FROM Accounts_UsersExp UE, ", status);
            strSql.AppendFormat("(SELECT UserID FROM Accounts_UsersApprove WHERE ApproveID IN ({0}))AP ", userIds);
            strSql.Append("WHERE UE.UserID=AP.UserID ");

            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        public bool UpdatePhoneAndPay(int userId, string account, string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UsersExp set ");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("PayAccount=@PayAccount");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@PayAccount", SqlDbType.NVarChar,200),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = phone;
            parameters[1].Value = account;
            parameters[2].Value = userId;

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

        public int GetUserRankId(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  Grade FROM Accounts_UsersExp WHERE UserId=@UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserId;
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
        /// 获取用户余额
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public decimal GetUserBalance(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  Balance FROM Accounts_UsersExp WHERE UserId=@UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        /// <summary>
        /// 获得指定用户ID的全部下属用户
        /// </summary>
        public DataSet GetAllEmpByUserId(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
WITH    CTEGetChild
          AS ( SELECT   *
               FROM     Accounts_Users
               WHERE    EmployeeID = {0}
               UNION ALL
               ( SELECT a.*
                 FROM   Accounts_Users AS a
                        INNER JOIN CTEGetChild AS b ON a.EmployeeID = b.UserID
               )
             )
    SELECT  *
    FROM    CTEGetChild ORDER BY EmployeeID, UserID
", userId);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据 (用户表和邀请表)事物执行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="inviteID">邀请者UserID</param>
        /// <param name="inviteNick">邀请者昵称</param>
        /// <param name="pointScore">影响积分</param>
        /// <returns></returns>
        public bool AddEx(Model.Members.UsersExpModel model, int inviteID, string inviteNick, int pointScore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UsersExp(");
            strSql.Append("UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Gravatar,@Singature,@TelPhone,@QQ,@MSN,@HomePage,@Birthday,@BirthdayVisible,@BirthdayIndexVisible,@Constellation,@ConstellationVisible,@ConstellationIndexVisible,@NativePlace,@NativePlaceVisible,@NativePlaceIndexVisible,@RegionId,@Address,@AddressVisible,@AddressIndexVisible,@BodilyForm,@BodilyFormVisible,@BodilyFormIndexVisible,@BloodType,@BloodTypeVisible,@BloodTypeIndexVisible,@Marriaged,@MarriagedVisible,@MarriagedIndexVisible,@PersonalStatus,@PersonalStatusVisible,@PersonalStatusIndexVisible,@Grade,@Balance,@Points,@TopicCount,@ReplyTopicCount,@FavTopicCount,@PvCount,@FansCount,@FellowCount,@AblumsCount,@FavouritesCount,@FavoritedCount,@ShareCount,@ProductsCount,@PersonalDomain,@LastAccessTime,@LastAccessIP,@LastPostTime,@LastLoginTime,@Remark,@IsUserDPI,@PayAccount)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Gravatar", SqlDbType.NVarChar,200),
					new SqlParameter("@Singature", SqlDbType.NVarChar,200),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,30),
					new SqlParameter("@MSN", SqlDbType.NVarChar,30),
					new SqlParameter("@HomePage", SqlDbType.NVarChar,50),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@BirthdayVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BirthdayIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Constellation", SqlDbType.NVarChar,10),
					new SqlParameter("@ConstellationVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@ConstellationIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@NativePlace", SqlDbType.NVarChar,300),
					new SqlParameter("@NativePlaceVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@NativePlaceIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@AddressVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@AddressIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@BodilyForm", SqlDbType.NVarChar,10),
					new SqlParameter("@BodilyFormVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BodilyFormIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@BloodType", SqlDbType.NVarChar,10),
					new SqlParameter("@BloodTypeVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@BloodTypeIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Marriaged", SqlDbType.NVarChar,10),
					new SqlParameter("@MarriagedVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@MarriagedIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@PersonalStatus", SqlDbType.NVarChar,10),
					new SqlParameter("@PersonalStatusVisible", SqlDbType.SmallInt,2),
					new SqlParameter("@PersonalStatusIndexVisible", SqlDbType.Bit,1),
					new SqlParameter("@Grade", SqlDbType.Int,4),
					new SqlParameter("@Balance", SqlDbType.Money,8),
					new SqlParameter("@Points", SqlDbType.Int,4),
					new SqlParameter("@TopicCount", SqlDbType.Int,4),
					new SqlParameter("@ReplyTopicCount", SqlDbType.Int,4),
					new SqlParameter("@FavTopicCount", SqlDbType.Int,4),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@FansCount", SqlDbType.Int,4),
					new SqlParameter("@FellowCount", SqlDbType.Int,4),
					new SqlParameter("@AblumsCount", SqlDbType.Int,4),
					new SqlParameter("@FavouritesCount", SqlDbType.Int,4),
					new SqlParameter("@FavoritedCount", SqlDbType.Int,4),
					new SqlParameter("@ShareCount", SqlDbType.Int,4),
					new SqlParameter("@ProductsCount", SqlDbType.Int,4),
					new SqlParameter("@PersonalDomain", SqlDbType.NVarChar,50),
					new SqlParameter("@LastAccessTime", SqlDbType.DateTime),
					new SqlParameter("@LastAccessIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LastPostTime", SqlDbType.DateTime),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@IsUserDPI", SqlDbType.Bit,1),
					new SqlParameter("@PayAccount", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Gravatar;
            parameters[2].Value = model.Singature;
            parameters[3].Value = model.TelPhone;
            parameters[4].Value = model.QQ;
            parameters[5].Value = model.MSN;
            parameters[6].Value = model.HomePage;
            parameters[7].Value = model.Birthday;
            parameters[8].Value = model.BirthdayVisible;
            parameters[9].Value = model.BirthdayIndexVisible;
            parameters[10].Value = model.Constellation;
            parameters[11].Value = model.ConstellationVisible;
            parameters[12].Value = model.ConstellationIndexVisible;
            parameters[13].Value = model.NativePlace;
            parameters[14].Value = model.NativePlaceVisible;
            parameters[15].Value = model.NativePlaceIndexVisible;
            parameters[16].Value = model.RegionId;
            parameters[17].Value = model.Address;
            parameters[18].Value = model.AddressVisible;
            parameters[19].Value = model.AddressIndexVisible;
            parameters[20].Value = model.BodilyForm;
            parameters[21].Value = model.BodilyFormVisible;
            parameters[22].Value = model.BodilyFormIndexVisible;
            parameters[23].Value = model.BloodType;
            parameters[24].Value = model.BloodTypeVisible;
            parameters[25].Value = model.BloodTypeIndexVisible;
            parameters[26].Value = model.Marriaged;
            parameters[27].Value = model.MarriagedVisible;
            parameters[28].Value = model.MarriagedIndexVisible;
            parameters[29].Value = model.PersonalStatus;
            parameters[30].Value = model.PersonalStatusVisible;
            parameters[31].Value = model.PersonalStatusIndexVisible;
            parameters[32].Value = model.Grade;
            parameters[33].Value = model.Balance;
            parameters[34].Value = model.Points;
            parameters[35].Value = model.TopicCount;
            parameters[36].Value = model.ReplyTopicCount;
            parameters[37].Value = model.FavTopicCount;
            parameters[38].Value = model.PvCount;
            parameters[39].Value = model.FansCount;
            parameters[40].Value = model.FellowCount;
            parameters[41].Value = model.AblumsCount;
            parameters[42].Value = model.FavouritesCount;
            parameters[43].Value = model.FavoritedCount;
            parameters[44].Value = model.ShareCount;
            parameters[45].Value = model.ProductsCount;
            parameters[46].Value = model.PersonalDomain;
            parameters[47].Value = model.LastAccessTime;
            parameters[48].Value = model.LastAccessIP;
            parameters[49].Value = model.LastPostTime;
            parameters[50].Value = model.LastLoginTime;
            parameters[51].Value = model.Remark;
            parameters[52].Value = model.IsUserDPI;
            parameters[53].Value = model.PayAccount;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into Accounts_UserInvite(");
            strSql2.Append("UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,RebateDesc)");
            strSql2.Append(" values (");
            strSql2.Append("@UserId,@UserNick,@InviteUserId,@InviteNick,@IsRebate,@IsNew,@CreatedDate,@RebateDesc)");
            strSql2.Append(";select @@IDENTITY");
            SqlParameter[] parameters2 = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserNick", SqlDbType.NVarChar,200),
					new SqlParameter("@InviteUserId", SqlDbType.Int,4),
					new SqlParameter("@InviteNick", SqlDbType.NVarChar,200),
					new SqlParameter("@IsRebate", SqlDbType.Bit,1),
					new SqlParameter("@IsNew", SqlDbType.Bit,1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@RebateDesc", SqlDbType.NVarChar,200)};
            parameters2[0].Value = model.UserID;
            parameters2[1].Value = model.NickName;
            parameters2[2].Value = inviteID;
            parameters2[3].Value = inviteNick;
            parameters2[4].Value = true;
            parameters2[5].Value = true;
            parameters2[6].Value = DateTime.Now;
            parameters2[7].Value = "邀请用户+" + pointScore + "积分";
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
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
        /// 用户中心好代管理(李永琴)
        /// </summary>
        /// <returns></returns>
        public DataSet Select_UsersEXP(string type, string StrWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  *,ISNULL(b.UserStatus,0) as UserStatus,ISNULL(b.UserAppType,0) as UserAppType,ISNULL(b.UserOldType,0) as UserOldType, ISNULL(b.Probation,0) as Probation from Accounts_Users as a inner join Accounts_UsersExp as b");
            strSql.Append(" on a.UserID=b.UserID");
            strSql.Append(" WHERE b.IsAppUserType=1 ");

            StringBuilder strWhere2 = new StringBuilder();

            if (!string.IsNullOrEmpty(type))
            {
                if (!String.IsNullOrWhiteSpace(strWhere2.ToString()))
                {
                    strWhere2.Append(" AND ");
                }
                strWhere2.Append("  UserType='" + type + "'");
            }
            if (!string.IsNullOrEmpty(StrWhere))
            {
                if (!String.IsNullOrWhiteSpace(strWhere2.ToString()))
                {
                    strWhere2.Append(" AND ");
                }
                strWhere2.Append(StrWhere);
            }
            strSql.Append(" and " + strWhere2.ToString());
            strSql.Append(" order by  User_dateCreate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到申请状态
        /// </summary>
        /// <returns></returns>
        public long GetUserAppType(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ISNULL(Accounts_UsersExp.UserAppType,0) from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID ");
            strSql.Append("WHERE Accounts_usersEXP.IsAppUserType=1 AND  Accounts_UsersExp.UserID=@UserID");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.BigInt,8)
            };
            parameters[0].Value = UserID;
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
        /// 获取审核状态
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public long GetUserStatus(string type, int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere2 = new StringBuilder();
            strSql.Append("select ISNULL(Accounts_UsersExp.UserStatus,0) from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID ");
            strSql.Append("WHERE Accounts_usersEXP.IsAppUserType=1 AND  Accounts_UsersExp.UserID=@UserID");

            if (!string.IsNullOrEmpty(type))
            {
                if (!String.IsNullOrWhiteSpace(strWhere2.ToString()))
                {
                    strWhere2.Append(" AND ");
                }
                strWhere2.Append("  UserType='" + type + "'");
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.BigInt,8)
            };

            parameters[0].Value = UserID;
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
        /// 审核修改状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGoodUserStatus(Maticsoft.Model.Members.UsersExpModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UsersExp set ");
            strSql.Append("UserStatus=@UserStatus,");
            strSql.Append("UserAppType=@UserAppType,");
            strSql.Append("UserOldType=@UserOldType");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@UserStatus",SqlDbType.Int,4),
                        new SqlParameter("@UserAppType",SqlDbType.Int,4),
                        new SqlParameter("@UserOldType",SqlDbType.Int,8),
                        new SqlParameter("@UserID",SqlDbType.BigInt,8)
                                        };

            parameters[0].Value = model.UserStatus;
            parameters[1].Value = model.UserAppType;
            parameters[2].Value = model.UserOldType;
            parameters[3].Value = model.UserID;
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
        /// 修改试用状态
        /// </summary>
        /// <returns></returns>
        public bool UpdateGoodUsersProbation(Maticsoft.Model.Members.UsersExpModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UsersExp set ");
            strSql.Append("Probation=@Probation");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@Probation",SqlDbType.Int,4),
                        new SqlParameter("@UserID",SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = model.Probation;
            parameters[1].Value = model.UserID;
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
        /// 修改UserOldType为分销店=3或服务店=4 
        /// </summary>
        /// <returns></returns>
        public bool UpdateOldType(int UserOldType,int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UsersExp set ");
            strSql.Append("UserOldType=@UserOldType");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@UserOldType",SqlDbType.Int,4),
                        new SqlParameter("@UserID",SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = UserOldType;
            parameters[1].Value = UserID;
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

        #endregion 扩展方法
    }
}