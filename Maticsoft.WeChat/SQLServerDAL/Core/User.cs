/**
* User.cs
*
* 功 能： N/A
* 类 名： User
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:22   N/A    初版
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
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:User
	/// </summary>
	public partial class User:IUser
	{
		public User()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "WeChat_User");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_User");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_User(");
            strSql.Append("UserId,GroupId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,Headimgurl,CreateTime,Status,CancelTime,Remark,LastMsgTime)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@GroupId,@OpenId,@UserName,@NickName,@Sex,@City,@Province,@Country,@Language,@Headimgurl,@CreateTime,@Status,@CancelTime,@Remark,@LastMsgTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@GroupId", SqlDbType.Int,4),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@NickName", SqlDbType.NVarChar,200),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@City", SqlDbType.NVarChar,200),
					new SqlParameter("@Province", SqlDbType.NVarChar,200),
					new SqlParameter("@Country", SqlDbType.NVarChar,200),
					new SqlParameter("@Language", SqlDbType.NVarChar,200),
					new SqlParameter("@Headimgurl", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CancelTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@LastMsgTime", SqlDbType.DateTime)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.GroupId;
            parameters[2].Value = model.OpenId;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.NickName;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.City;
            parameters[7].Value = model.Province;
            parameters[8].Value = model.Country;
            parameters[9].Value = model.Language;
            parameters[10].Value = model.Headimgurl;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.CancelTime;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.LastMsgTime;

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
        public bool Update(Maticsoft.WeChat.Model.Core.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("GroupId=@GroupId,");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("NickName=@NickName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("City=@City,");
            strSql.Append("Province=@Province,");
            strSql.Append("Country=@Country,");
            strSql.Append("Language=@Language,");
            strSql.Append("Headimgurl=@Headimgurl,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("CancelTime=@CancelTime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("LastMsgTime=@LastMsgTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@GroupId", SqlDbType.Int,4),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@NickName", SqlDbType.NVarChar,200),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@City", SqlDbType.NVarChar,200),
					new SqlParameter("@Province", SqlDbType.NVarChar,200),
					new SqlParameter("@Country", SqlDbType.NVarChar,200),
					new SqlParameter("@Language", SqlDbType.NVarChar,200),
					new SqlParameter("@Headimgurl", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CancelTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@LastMsgTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.GroupId;
            parameters[2].Value = model.OpenId;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.NickName;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.City;
            parameters[7].Value = model.Province;
            parameters[8].Value = model.Country;
            parameters[9].Value = model.Language;
            parameters[10].Value = model.Headimgurl;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.CancelTime;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.LastMsgTime;
            parameters[16].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_User ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_User ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.User GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserId,GroupId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,Headimgurl,CreateTime,Status,CancelTime,Remark,LastMsgTime from WeChat_User ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.WeChat.Model.Core.User model = new Maticsoft.WeChat.Model.Core.User();
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
        public Maticsoft.WeChat.Model.Core.User DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.User model = new Maticsoft.WeChat.Model.Core.User();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["GroupId"] != null && row["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(row["GroupId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["NickName"] != null)
                {
                    model.NickName = row["NickName"].ToString();
                }
                if (row["Sex"] != null && row["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(row["Sex"].ToString());
                }
                if (row["City"] != null)
                {
                    model.City = row["City"].ToString();
                }
                if (row["Province"] != null)
                {
                    model.Province = row["Province"].ToString();
                }
                if (row["Country"] != null)
                {
                    model.Country = row["Country"].ToString();
                }
                if (row["Language"] != null)
                {
                    model.Language = row["Language"].ToString();
                }
                if (row["Headimgurl"] != null)
                {
                    model.Headimgurl = row["Headimgurl"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["CancelTime"] != null && row["CancelTime"].ToString() != "")
                {
                    model.CancelTime = DateTime.Parse(row["CancelTime"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["LastMsgTime"] != null && row["LastMsgTime"].ToString() != "")
                {
                    model.LastMsgTime = DateTime.Parse(row["LastMsgTime"].ToString());
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
            strSql.Append("select ID,UserId,GroupId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,Headimgurl,CreateTime,Status,CancelTime,Remark,LastMsgTime ");
            strSql.Append(" FROM WeChat_User ");
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
            strSql.Append(" ID,UserId,GroupId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,Headimgurl,CreateTime,Status,CancelTime,Remark,LastMsgTime ");
            strSql.Append(" FROM WeChat_User ");
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
            strSql.Append("select count(1) FROM WeChat_User ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_User T ");
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
            parameters[0].Value = "WeChat_User";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

	    public bool DeleteUser(string OpenId, string userName)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_User ");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = OpenId;
            parameters[1].Value = userName;

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

        public bool Exists(string OpenId, string userName)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_User");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = OpenId;
            parameters[1].Value = userName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
	    }


        public bool UpdateEx(Maticsoft.WeChat.Model.Core.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set ");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Status=@Status");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.Status;

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

        public bool CancelUser(string OpenId, string userName)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set ");
            strSql.Append("Status=0,");
            strSql.Append("CancelTime=@CancelTime");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@CancelTime", SqlDbType.DateTime)
                                        };
            parameters[0].Value = OpenId;
            parameters[1].Value = userName;
            parameters[2].Value = DateTime.Now;

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

	    public bool UpdateGroup(int groupId, string ids)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set ");
            strSql.Append("GroupId=@GroupId");
            strSql.Append(" where ID in (" + ids + ")  ");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupId", SqlDbType.Int,4)
        };
            parameters[0].Value = groupId;
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

        public string GetNickName(string userName, string OpenId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 NickName from WeChat_User ");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
                                            new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = OpenId;
            parameters[1].Value = userName;

            Maticsoft.WeChat.Model.Core.User model = new Maticsoft.WeChat.Model.Core.User();
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
	    }

        public bool UpdateNick(string userName,string OpenId, string nickName)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set  ");
            strSql.Append(" NickName=@NickName");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
                                           
					new SqlParameter("@NickName", SqlDbType.NVarChar,200),
                     new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = nickName;
            parameters[1].Value = OpenId;
            parameters[2].Value = userName;

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

        public bool UpdateNick(int  Id, string nickName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set  ");
            strSql.Append(" NickName=@NickName");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@NickName", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = nickName;
            parameters[1].Value = Id;

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

	    public Maticsoft.WeChat.Model.Core.User GetUser(string OpenId, string userName)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from WeChat_User ");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = OpenId;
            parameters[1].Value = userName;

            Maticsoft.WeChat.Model.Core.User model = new Maticsoft.WeChat.Model.Core.User();
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


        public bool UpdateMsgTime(string openId, string userName, DateTime date)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set  ");
            strSql.Append(" LastMsgTime=@LastMsgTime");
            strSql.Append(" where OpenId=@OpenId and UserName=@UserName");
            SqlParameter[] parameters = {
                    new SqlParameter("@LastMsgTime", SqlDbType.DateTime),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = date;
            parameters[1].Value = openId;
            parameters[2].Value = userName;

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


        public bool IsCanSend(string user,int hours=48)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_User");
            strSql.Append(" where UserName=@UserName and LastMsgTime>=dateadd(hh,-@hours,getdate())");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
                    	new SqlParameter("@hours", SqlDbType.Int)
			};
            parameters[0].Value = user;
            parameters[1].Value = hours;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public string GetNickName(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 NickName from WeChat_User ");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = userName;

            Maticsoft.WeChat.Model.Core.User model = new Maticsoft.WeChat.Model.Core.User();
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }


        public bool UpdateUser(Maticsoft.WeChat.Model.Core.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_User set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("City=@City,");
            strSql.Append("Province=@Province,");
            strSql.Append("Country=@Country,");
            strSql.Append("Language=@Language,");
            strSql.Append("NickName=@NickName,");
            strSql.Append("Headimgurl=@Headimgurl");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
                                            	new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@City", SqlDbType.NVarChar,200),
					new SqlParameter("@Province", SqlDbType.NVarChar,200),
					new SqlParameter("@Country", SqlDbType.NVarChar,200),
					new SqlParameter("@Language", SqlDbType.NVarChar,200),
					new SqlParameter("@Headimgurl", SqlDbType.NVarChar,500),
                    	new SqlParameter("@NickName", SqlDbType.NVarChar,200),
                                        	new SqlParameter("@UserName", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Sex;
            parameters[2].Value = model.City;
            parameters[3].Value = model.Province;
            parameters[4].Value = model.Country;
            parameters[5].Value = model.Language;
            parameters[6].Value = model.Headimgurl;
            parameters[7].Value = model.NickName;
            parameters[8].Value = model.UserName;
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

        public DataSet GetDayCount(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select CONVERT(varchar(12) , CreateTime, 111 )  AS Date  , COUNT(1)  AS DayCount   ");
            stringBuilder.Append("FROM WeChat_User  where Status=1 ");
            if (strWhere.Trim() != "")
            {
                stringBuilder.Append("  and " + strWhere);
            }
            stringBuilder.Append(" GROUP BY CONVERT(varchar(12) , CreateTime, 111 )  ORDER BY Date ");
            return DbHelperSQL.Query(stringBuilder.ToString());
        }
        public DataSet GetCancelCount(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select CONVERT(varchar(12) , CancelTime, 111 )  AS Date  , COUNT(1)  AS DayCount   ");
            stringBuilder.Append("FROM WeChat_User  where Status=0 ");
            if (strWhere.Trim() != "")
            {
                stringBuilder.Append("  and " + strWhere);
            }
            stringBuilder.Append(" GROUP BY CONVERT(varchar(12) , CancelTime, 111 )  ORDER BY Date ");
            return DbHelperSQL.Query(stringBuilder.ToString());
        }
	    #endregion  ExtensionMethod
	}
}

