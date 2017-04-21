
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;
using Maticsoft.Model.Shop.Order;
namespace Maticsoft.SQLServerDAL.Members
{
    /// <summary>
    /// 数据访问类:Users
    /// </summary>
    public partial class Users : IUsers
    {
        public Users()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserID", "Accounts_Users");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Users");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters[0].Value = UserID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_Users(");
            strSql.Append("UserName,Password,TrueName,Sex,Phone,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_iCreator,User_dateCreate,User_dateValid,User_dateExpire,User_iApprover,User_dateApprove,User_iApproveState,User_cLang)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@Password,@TrueName,@Sex,@Phone,@Email,@EmployeeID,@DepartmentID,@Activity,@UserType,@Style,@User_iCreator,@User_dateCreate,@User_dateValid,@User_dateExpire,@User_iApprover,@User_dateApprove,@User_iApproveState,@User_cLang)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.Binary,20),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.Char,10),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@EmployeeID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentID", SqlDbType.NVarChar,15),
					new SqlParameter("@Activity", SqlDbType.Bit,1),
					new SqlParameter("@UserType", SqlDbType.Char,2),
					new SqlParameter("@Style", SqlDbType.Int,4),
					new SqlParameter("@User_iCreator", SqlDbType.Int,4),
					new SqlParameter("@User_dateCreate", SqlDbType.DateTime),
					new SqlParameter("@User_dateValid", SqlDbType.DateTime),
					new SqlParameter("@User_dateExpire", SqlDbType.DateTime),
					new SqlParameter("@User_iApprover", SqlDbType.Int,4),
					new SqlParameter("@User_dateApprove", SqlDbType.DateTime),
					new SqlParameter("@User_iApproveState", SqlDbType.Int,4),
					new SqlParameter("@User_cLang", SqlDbType.NVarChar,10)};
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.TrueName;
            parameters[3].Value = model.Sex;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.EmployeeID;
            parameters[7].Value = model.DepartmentID;
            parameters[8].Value = model.Activity;
            parameters[9].Value = model.UserType;
            parameters[10].Value = model.Style;
            parameters[11].Value = model.User_iCreator;
            parameters[12].Value = model.User_dateCreate;
            parameters[13].Value = model.User_dateValid;
            parameters[14].Value = model.User_dateExpire;
            parameters[15].Value = model.User_iApprover;
            parameters[16].Value = model.User_dateApprove;
            parameters[17].Value = model.User_iApproveState;
            parameters[18].Value = model.User_cLang;

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
        public bool Update(Maticsoft.Model.Members.Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Users set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Password=@Password,");
            strSql.Append("TrueName=@TrueName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Email=@Email,");
            strSql.Append("EmployeeID=@EmployeeID,");
            strSql.Append("DepartmentID=@DepartmentID,");
            strSql.Append("Activity=@Activity,");
            strSql.Append("UserType=@UserType,");
            strSql.Append("Style=@Style,");
            strSql.Append("User_iCreator=@User_iCreator,");
            strSql.Append("User_dateCreate=@User_dateCreate,");
            strSql.Append("User_dateValid=@User_dateValid,");
            strSql.Append("User_dateExpire=@User_dateExpire,");
            strSql.Append("User_iApprover=@User_iApprover,");
            strSql.Append("User_dateApprove=@User_dateApprove,");
            strSql.Append("User_iApproveState=@User_iApproveState,");
            strSql.Append("User_cLang=@User_cLang");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.Binary,20),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.Char,10),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@EmployeeID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentID", SqlDbType.NVarChar,15),
					new SqlParameter("@Activity", SqlDbType.Bit,1),
					new SqlParameter("@UserType", SqlDbType.Char,2),
					new SqlParameter("@Style", SqlDbType.Int,4),
					new SqlParameter("@User_iCreator", SqlDbType.Int,4),
					new SqlParameter("@User_dateCreate", SqlDbType.DateTime),
					new SqlParameter("@User_dateValid", SqlDbType.DateTime),
					new SqlParameter("@User_dateExpire", SqlDbType.DateTime),
					new SqlParameter("@User_iApprover", SqlDbType.Int,4),
					new SqlParameter("@User_dateApprove", SqlDbType.DateTime),
					new SqlParameter("@User_iApproveState", SqlDbType.Int,4),
					new SqlParameter("@User_cLang", SqlDbType.NVarChar,10),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.TrueName;
            parameters[3].Value = model.Sex;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.EmployeeID;
            parameters[7].Value = model.DepartmentID;
            parameters[8].Value = model.Activity;
            parameters[9].Value = model.UserType;
            parameters[10].Value = model.Style;
            parameters[11].Value = model.User_iCreator;
            parameters[12].Value = model.User_dateCreate;
            parameters[13].Value = model.User_dateValid;
            parameters[14].Value = model.User_dateExpire;
            parameters[15].Value = model.User_iApprover;
            parameters[16].Value = model.User_dateApprove;
            parameters[17].Value = model.User_iApproveState;
            parameters[18].Value = model.User_cLang;
            parameters[19].Value = model.UserID;

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
        /// 申请好邻服务店，更新users表 +++ 欧阳 +++
        /// </summary>
        public bool UpdateApplyAgentHao(Maticsoft.Model.Members.Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Users set ");

            strSql.Append("TrueName=@TrueName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Email=@Email,");
            strSql.Append("CardId = @CardId");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
					
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.Char,10),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
                    new SqlParameter("@CardId", SqlDbType.NVarChar,18),
					new SqlParameter("@UserID", SqlDbType.Int,4)};

            parameters[0].Value = model.TrueName;
            parameters[1].Value = model.Sex;
            parameters[2].Value = model.Phone;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.CardId;
            parameters[5].Value = model.UserID;
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
            strSql.Append("delete from Accounts_Users ");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
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
            strSql.Append("delete from Accounts_Users ");
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
        public Maticsoft.Model.Members.Users GetModel(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,UserName,NickName,Password,TrueName,Sex,Phone,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_iCreator,User_dateCreate,User_dateValid,User_dateExpire,User_iApprover,User_dateApprove,User_iApproveState,User_cLang,CardId from Accounts_Users ");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters[0].Value = UserID;

            Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Password"] != null && ds.Tables[0].Rows[0]["Password"].ToString() != "")
                {
                    model.Password = (byte[])ds.Tables[0].Rows[0]["Password"];
                }
             
                model.TrueName = ds.Tables[0].Rows[0]["TrueName"].ToString();
                
                if (ds.Tables[0].Rows[0]["NickName"] != null && ds.Tables[0].Rows[0]["NickName"].ToString() != "")
                {
                    model.NickName = ds.Tables[0].Rows[0]["NickName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sex"] != null && ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Phone"] != null && ds.Tables[0].Rows[0]["Phone"].ToString() != "")
                {
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmployeeID"] != null && ds.Tables[0].Rows[0]["EmployeeID"].ToString() != "")
                {
                    model.EmployeeID = int.Parse(ds.Tables[0].Rows[0]["EmployeeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DepartmentID"] != null && ds.Tables[0].Rows[0]["DepartmentID"].ToString() != "")
                {
                    model.DepartmentID = ds.Tables[0].Rows[0]["DepartmentID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Activity"] != null && ds.Tables[0].Rows[0]["Activity"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Activity"].ToString() == "1") || (ds.Tables[0].Rows[0]["Activity"].ToString().ToLower() == "true"))
                    {
                        model.Activity = true;
                    }
                    else
                    {
                        model.Activity = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Style"] != null && ds.Tables[0].Rows[0]["Style"].ToString() != "")
                {
                    model.Style = int.Parse(ds.Tables[0].Rows[0]["Style"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_iCreator"] != null && ds.Tables[0].Rows[0]["User_iCreator"].ToString() != "")
                {
                    model.User_iCreator = int.Parse(ds.Tables[0].Rows[0]["User_iCreator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateCreate"] != null && ds.Tables[0].Rows[0]["User_dateCreate"].ToString() != "")
                {
                    model.User_dateCreate = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateCreate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateValid"] != null && ds.Tables[0].Rows[0]["User_dateValid"].ToString() != "")
                {
                    model.User_dateValid = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateValid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateExpire"] != null && ds.Tables[0].Rows[0]["User_dateExpire"].ToString() != "")
                {
                    model.User_dateExpire = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateExpire"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_iApprover"] != null && ds.Tables[0].Rows[0]["User_iApprover"].ToString() != "")
                {
                    model.User_iApprover = int.Parse(ds.Tables[0].Rows[0]["User_iApprover"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateApprove"] != null && ds.Tables[0].Rows[0]["User_dateApprove"].ToString() != "")
                {
                    model.User_dateApprove = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateApprove"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_iApproveState"] != null && ds.Tables[0].Rows[0]["User_iApproveState"].ToString() != "")
                {
                    model.User_iApproveState = int.Parse(ds.Tables[0].Rows[0]["User_iApproveState"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_cLang"] != null && ds.Tables[0].Rows[0]["User_cLang"].ToString() != "")
                {
                    model.User_cLang = ds.Tables[0].Rows[0]["User_cLang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CardId"] != null && ds.Tables[0].Rows[0]["CardId"].ToString() != "")
                {
                    model.CardId = ds.Tables[0].Rows[0]["CardId"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Accounts_Users ");
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
            strSql.Append(" UserID,UserName,Password,TrueName,Sex,Phone,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_iCreator,User_dateCreate,User_dateValid,User_dateExpire,User_iApprover,User_dateApprove,User_iApproveState,User_cLang ");
            strSql.Append(" FROM Accounts_Users ");
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
            strSql.Append("select count(1) FROM Accounts_Users ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.UserID desc");
            }
            strSql.Append(")AS Row, T.*  from Accounts_Users T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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
            parameters[0].Value = "Accounts_Users";
            parameters[1].Value = "UserID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region MethodEx
        /// <summary>
        /// 根据DepartmentID删除一条数据
        /// </summary>
        public bool DeleteByDepartmentID(int DepartmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_Users ");
            strSql.Append(" where DepartmentID=@DepartmentID");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentID", SqlDbType.Int,4)
			};
            parameters[0].Value = DepartmentID;

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
        /// 根据DepartmentID批量删除数据
        /// </summary>
        public bool DeleteListByDepartmentID(string DepartmentIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_Users ");
            strSql.Append(" where DepartmentID in (" + DepartmentIDlist + ")  ");
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
        /// 判断电话是否一件存在
        /// </summary>
        public bool ExistByPhone(string Phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Accounts_Users ");
            strSql.Append(" where UserName=@Phone ");
            SqlParameter[] parameters = {
					new SqlParameter("@Phone", SqlDbType.NVarChar)

			};
            parameters[0].Value = Phone;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 根据用户邮箱判断是否存在该记录
        /// </summary>
        public bool ExistsByEmail(string Email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from Accounts_Users ");
            strSql.Append(" where Email=@Email");
            SqlParameter[] parameters = {
					new SqlParameter("@Email", SqlDbType.NVarChar)
			};
            parameters[0].Value = Email;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;


            }
        }

        /// <summary>
        ///根据用户输入的昵称是否存在
        /// </summary>
        public bool ExistsNickName(string nickname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Users");
            strSql.Append(" where NickName=@NickName");
            SqlParameter[] parameters = {
                    new SqlParameter("@NickName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = nickname;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion

        /// <summary>
        ///根据用户ID判断昵称是否已被其他用户使用
        /// </summary>
        public bool ExistsNickName(int userid, string nickname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Users");
            strSql.Append(" where UserID<>@UserID AND NickName=@NickName");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@NickName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = userid;
            parameters[1].Value = nickname;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public DataSet GetList(string type, string keyWord)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Accounts_Users ");
            StringBuilder strWhere = new StringBuilder();
            strSql.Append(" WHERE Activity=1 ");
            if (!string.IsNullOrWhiteSpace(type))
            {
                strSql.Append(" AND UserType=" + type);
            }
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                strSql.AppendFormat(" AND UserName LIKE '%{0}%' ", keyWord);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        //联合查询用户表和用户附件表(普通用户)
        public DataSet GetListEX(string keyWord = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID");
            strSql.Append(" WHERE UserType='UU'");
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                strSql.AppendFormat(" AND UserName LIKE '%{0}%' ", keyWord);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        //联合查询用户表和用户附件表
        public DataSet GetListEXByType(string type, string keyWord = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID");
            StringBuilder strWhere = new StringBuilder();

            if (!string.IsNullOrEmpty(type))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" AND ");
                }
                strWhere.Append("  UserType='" + type + "'");
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" AND ");
                }
                strWhere.AppendFormat("  UserName LIKE '%{0}%' ", keyWord);
            }
            strSql.Append(" WHERE   " + strWhere.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }



        //联合查询用户表和用户附件表
        public DataSet GetSearchList(string type, string StrWhere = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID");
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
            strSql.Append(" WHERE   " + strWhere2.ToString());
            strSql.Append(" order by  User_dateCreate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetUserIdByNickName(string NickName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID FROM Accounts_Users ");
            if (NickName.Trim() != "")
            {
                strSql.Append(" where NickName=@NickName");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@NickName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = NickName;
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

        public int GetUserIdByUserName(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID FROM Accounts_Users ");
            if (UserName.Trim() != "")
            {
                strSql.Append(" where UserName=@UserName");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = UserName;
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

        public int GetUserIdByUserEmail(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID FROM Accounts_Users ");
            if (userName.Trim() != "")
            {
                strSql.Append(" where userName=@userName");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = userName;
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


        public string GetUserName(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName FROM Accounts_Users ");
            strSql.Append(" where UserId=" + UserId);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        public string GetUserByID(int? UserId)
        {
            if (UserId != null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Phone FROM Accounts_Users ");
                strSql.Append(" where UserId=" + UserId);
                object obj = DbHelperSQL.GetSingle(strSql.ToString());
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 一键更新用户的粉丝数和关注数
        /// </summary>
        /// <returns></returns>
        public bool UpdateFansAndFellowCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_UsersExp SET FansCount=(SELECT COUNT(1) FROM SNS_UserShip us WHERE Accounts_UsersExp.UserID=us.PassiveUserID),FellowCount=(SELECT COUNT(1) FROM SNS_UserShip us WHERE Accounts_UsersExp.UserID=us.ActiveUserID)");
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
        /// 对用户进行批量冻结和解冻
        /// </summary>
        /// <param name="Ids">用户的id集合</param>
        /// <param name="ActiveType">冻结或冻结</param>
        /// <returns></returns>
        public bool UpdateActiveStatus(string Ids, int ActiveType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_Users SET Activity=" + ActiveType + " Where UserID in(" + Ids + ")");
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

        public Maticsoft.Model.Members.Users GetUserIdByDepartmentID(string DepartmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID FROM Accounts_Users ");
            strSql.Append(" where DepartmentID=@DepartmentID");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentID", SqlDbType.NVarChar,15),
			};
            parameters[0].Value = DepartmentID;

            Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        public int GetDefaultUserId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MIN(UserID) from Accounts_Users   where  Activity=1 and UserType='UU'");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }



        public string GetNickName(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select NickName FROM Accounts_Users ");
            strSql.Append(" where UserId=" + UserId);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }


        public bool DeleteEx(int userId)
        {

            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_Users ");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from Accounts_UsersExp ");
            strSql1.Append(" where UserID=@UserID  ");
            SqlParameter[] parameters1 = {
						new SqlParameter("@UserID", SqlDbType.Int,4)
                                         };
            parameters1[0].Value = userId;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;


        }

        public Maticsoft.Model.Members.Users GetModel(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,UserName,NickName,Password,TrueName,Sex,Phone,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_iCreator,User_dateCreate,User_dateValid,User_dateExpire,User_iApprover,User_dateApprove,User_iApproveState,User_cLang,Oid,CardId from Accounts_Users ");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = userName;

            Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Password"] != null && ds.Tables[0].Rows[0]["Password"].ToString() != "")
                {
                    model.Password = (byte[])ds.Tables[0].Rows[0]["Password"];
                }
                if (ds.Tables[0].Rows[0]["TrueName"] != null && ds.Tables[0].Rows[0]["TrueName"].ToString() != "")
                {
                    model.TrueName = ds.Tables[0].Rows[0]["TrueName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NickName"] != null && ds.Tables[0].Rows[0]["NickName"].ToString() != "")
                {
                    model.NickName = ds.Tables[0].Rows[0]["NickName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sex"] != null && ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Phone"] != null && ds.Tables[0].Rows[0]["Phone"].ToString() != "")
                {
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmployeeID"] != null && ds.Tables[0].Rows[0]["EmployeeID"].ToString() != "")
                {
                    model.EmployeeID = int.Parse(ds.Tables[0].Rows[0]["EmployeeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DepartmentID"] != null && ds.Tables[0].Rows[0]["DepartmentID"].ToString() != "")
                {
                    model.DepartmentID = ds.Tables[0].Rows[0]["DepartmentID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Activity"] != null && ds.Tables[0].Rows[0]["Activity"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Activity"].ToString() == "1") || (ds.Tables[0].Rows[0]["Activity"].ToString().ToLower() == "true"))
                    {
                        model.Activity = true;
                    }
                    else
                    {
                        model.Activity = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Style"] != null && ds.Tables[0].Rows[0]["Style"].ToString() != "")
                {
                    model.Style = int.Parse(ds.Tables[0].Rows[0]["Style"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_iCreator"] != null && ds.Tables[0].Rows[0]["User_iCreator"].ToString() != "")
                {
                    model.User_iCreator = int.Parse(ds.Tables[0].Rows[0]["User_iCreator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateCreate"] != null && ds.Tables[0].Rows[0]["User_dateCreate"].ToString() != "")
                {
                    model.User_dateCreate = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateCreate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateValid"] != null && ds.Tables[0].Rows[0]["User_dateValid"].ToString() != "")
                {
                    model.User_dateValid = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateValid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateExpire"] != null && ds.Tables[0].Rows[0]["User_dateExpire"].ToString() != "")
                {
                    model.User_dateExpire = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateExpire"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_iApprover"] != null && ds.Tables[0].Rows[0]["User_iApprover"].ToString() != "")
                {
                    model.User_iApprover = int.Parse(ds.Tables[0].Rows[0]["User_iApprover"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_dateApprove"] != null && ds.Tables[0].Rows[0]["User_dateApprove"].ToString() != "")
                {
                    model.User_dateApprove = DateTime.Parse(ds.Tables[0].Rows[0]["User_dateApprove"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_iApproveState"] != null && ds.Tables[0].Rows[0]["User_iApproveState"].ToString() != "")
                {
                    model.User_iApproveState = int.Parse(ds.Tables[0].Rows[0]["User_iApproveState"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_cLang"] != null && ds.Tables[0].Rows[0]["User_cLang"].ToString() != "")
                {
                    model.User_cLang = ds.Tables[0].Rows[0]["User_cLang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CardId"] != null && ds.Tables[0].Rows[0]["CardId"].ToString() != "")
                {
                    model.CardId = ds.Tables[0].Rows[0]["CardId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Oid"] != null && ds.Tables[0].Rows[0]["Oid"].ToString() != "")
                {
                    model.OId = int.Parse(ds.Tables[0].Rows[0]["Oid"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public DataSet GetUserCount(StatisticMode mode, DateTime startDate, DateTime endDate)
        {

            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
           @"
--用户统计走势图
SELECT  A.GeneratedDate AS GeneratedDate
        ,UserID as Users
FROM    ( SELECT    *
          FROM      {0}(@StartDate, @EndDate)
        ) A
        LEFT JOIN ( SELECT  CONVERT(varchar({1}) , U.User_dateCreate, 112 ) GeneratedDate
                         ,count(UserID) UserID
                    FROM    Accounts_Users U ", method, subLength);

            strSql.AppendFormat(@" 
                          where U.User_dateCreate BETWEEN @StartDate AND @EndDate 
                    GROUP BY CONVERT(varchar({0}) , U.User_dateCreate, 112 )
                  ) B 
ON CONVERT(varchar({0}) , A.GeneratedDate, 112 ) = CONVERT(varchar({0}) , B.GeneratedDate, 112 ) 
", subLength);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 得到推荐人
        /// </summary>
        /// <returns></returns>
        public Maticsoft.Model.Members.Users RecommendUserName(int RecommendUserID)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT * FROM dbo.Accounts_Users");
            strsql.Append(" WHERE UserID=@UserID");
            SqlParameter[] parameters = { 
                          new SqlParameter("@UserID",SqlDbType.BigInt)
                                         };
            parameters[0].Value = RecommendUserID;

            Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
            DataSet ds = DbHelperSQL.Query(strsql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TrueName"] != null && ds.Tables[0].Rows[0]["TrueName"].ToString() != "")
                {
                    model.TrueName = ds.Tables[0].Rows[0]["TrueName"].ToString();
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }


    }
}


