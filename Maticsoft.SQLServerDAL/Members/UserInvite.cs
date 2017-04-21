/**
* UserInvite.cs
*
* 功 能： N/A
* 类 名： UserInvite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/9 13:49:11   N/A    初版
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
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Members
{
    /// <summary>
    /// 数据访问类:UserInvite
    /// </summary>
    public partial class UserInvite : IUserInvite
    {
        public UserInvite()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("InviteId", "Accounts_UserInvite");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int InviteId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_UserInvite");
            strSql.Append(" where InviteId=@InviteId");
            SqlParameter[] parameters = {
					new SqlParameter("@InviteId", SqlDbType.Int,4)
			};
            parameters[0].Value = InviteId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.UserInvite model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UserInvite(");
            strSql.Append("UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@UserNick,@InviteUserId,@InviteNick,@IsRebate,@IsNew,@CreatedDate,@Remark,@RebateDesc)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserNick", SqlDbType.NVarChar,200),
					new SqlParameter("@InviteUserId", SqlDbType.Int,4),
					new SqlParameter("@InviteNick", SqlDbType.NVarChar,200),
					new SqlParameter("@IsRebate", SqlDbType.Bit,1),
					new SqlParameter("@IsNew", SqlDbType.Bit,1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@RebateDesc", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserNick;
            parameters[2].Value = model.InviteUserId;
            parameters[3].Value = model.InviteNick;
            parameters[4].Value = model.IsRebate;
            parameters[5].Value = model.IsNew;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.RebateDesc;

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
        public bool Update(Maticsoft.Model.Members.UserInvite model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UserInvite set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserNick=@UserNick,");
            strSql.Append("InviteUserId=@InviteUserId,");
            strSql.Append("InviteNick=@InviteNick,");
            strSql.Append("IsRebate=@IsRebate,");
            strSql.Append("IsNew=@IsNew,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("RebateDesc=@RebateDesc");
            strSql.Append(" where InviteId=@InviteId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserNick", SqlDbType.NVarChar,200),
					new SqlParameter("@InviteUserId", SqlDbType.Int,4),
					new SqlParameter("@InviteNick", SqlDbType.NVarChar,200),
					new SqlParameter("@IsRebate", SqlDbType.Bit,1),
					new SqlParameter("@IsNew", SqlDbType.Bit,1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@RebateDesc", SqlDbType.NVarChar,200),
					new SqlParameter("@InviteId", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserNick;
            parameters[2].Value = model.InviteUserId;
            parameters[3].Value = model.InviteNick;
            parameters[4].Value = model.IsRebate;
            parameters[5].Value = model.IsNew;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.RebateDesc;
            parameters[9].Value = model.InviteId;

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
        public bool Delete(int InviteId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UserInvite ");
            strSql.Append(" where InviteId=@InviteId");
            SqlParameter[] parameters = {
					new SqlParameter("@InviteId", SqlDbType.Int,4)
			};
            parameters[0].Value = InviteId;

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
        public bool DeleteList(string InviteIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UserInvite ");
            strSql.Append(" where InviteId in (" + InviteIdlist + ")  ");
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
        public Maticsoft.Model.Members.UserInvite GetModel(int InviteId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 InviteId,UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc from Accounts_UserInvite ");
            strSql.Append(" where InviteId=@InviteId");
            SqlParameter[] parameters = {
					new SqlParameter("@InviteId", SqlDbType.Int,4)
			};
            parameters[0].Value = InviteId;

            Maticsoft.Model.Members.UserInvite model = new Maticsoft.Model.Members.UserInvite();
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
        public Maticsoft.Model.Members.UserInvite DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.UserInvite model = new Maticsoft.Model.Members.UserInvite();
            if (row != null)
            {
                if (row["InviteId"] != null && row["InviteId"].ToString() != "")
                {
                    model.InviteId = int.Parse(row["InviteId"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserNick"] != null)
                {
                    model.UserNick = row["UserNick"].ToString();
                }
                if (row["InviteUserId"] != null && row["InviteUserId"].ToString() != "")
                {
                    model.InviteUserId = int.Parse(row["InviteUserId"].ToString());
                }
                if (row["InviteNick"] != null)
                {
                    model.InviteNick = row["InviteNick"].ToString();
                }
                if (row["IsRebate"] != null && row["IsRebate"].ToString() != "")
                {
                    if ((row["IsRebate"].ToString() == "1") || (row["IsRebate"].ToString().ToLower() == "true"))
                    {
                        model.IsRebate = true;
                    }
                    else
                    {
                        model.IsRebate = false;
                    }
                }
                if (row["IsNew"] != null && row["IsNew"].ToString() != "")
                {
                    if ((row["IsNew"].ToString() == "1") || (row["IsNew"].ToString().ToLower() == "true"))
                    {
                        model.IsNew = true;
                    }
                    else
                    {
                        model.IsNew = false;
                    }
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["RebateDesc"] != null)
                {
                    model.RebateDesc = row["RebateDesc"].ToString();
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
            strSql.Append("select InviteId,UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc ");
            strSql.Append(" FROM Accounts_UserInvite ");
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
            strSql.Append(" InviteId,UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc ");
            strSql.Append(" FROM Accounts_UserInvite ");
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
            strSql.Append("select count(1) FROM Accounts_UserInvite ");
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
                strSql.Append("order by T.InviteId desc");
            }
            strSql.Append(")AS Row, T.*  from Accounts_UserInvite T ");
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
            parameters[0].Value = "Accounts_UserInvite";
            parameters[1].Value = "InviteId";
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

