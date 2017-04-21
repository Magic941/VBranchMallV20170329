/**  版本信息模板在安装目录下，可自行修改。
* ActivityCode.cs
*
* 功 能： N/A
* 类 名： ActivityCode
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:16   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.WeChat.IDAL.Activity;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Activity
{
	/// <summary>
	/// 数据访问类:ActivityCode
	/// </summary>
	public partial class ActivityCode:IActivityCode
	{
		public ActivityCode()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CodeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_ActivityCode");
            strSql.Append(" where CodeName=@CodeName ");
            SqlParameter[] parameters = {
					new SqlParameter("@CodeName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = CodeName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.WeChat.Model.Activity.ActivityCode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_ActivityCode(");
            strSql.Append("CodeName,ActivityId,AwardId,AwardName,ActivityName,ActivityPwd,UserId,UserName,Phone,Status,IsPwd,StartDate,EndDate,GenerateDate,UsedDate,Remark)");
            strSql.Append(" values (");
            strSql.Append("@CodeName,@ActivityId,@AwardId,@AwardName,@ActivityName,@ActivityPwd,@UserId,@UserName,@Phone,@Status,@IsPwd,@StartDate,@EndDate,@GenerateDate,@UsedDate,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@CodeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
					new SqlParameter("@AwardId", SqlDbType.Int,4),
					new SqlParameter("@AwardName", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivityName", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivityPwd", SqlDbType.NVarChar,200),
					new SqlParameter("@UserId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Bit,1),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@GenerateDate", SqlDbType.DateTime),
					new SqlParameter("@UsedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.CodeName;
            parameters[1].Value = model.ActivityId;
            parameters[2].Value = model.AwardId;
            parameters[3].Value = model.AwardName;
            parameters[4].Value = model.ActivityName;
            parameters[5].Value = model.ActivityPwd;
            parameters[6].Value = model.UserId;
            parameters[7].Value = model.UserName;
            parameters[8].Value = model.Phone;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.IsPwd;
            parameters[11].Value = model.StartDate;
            parameters[12].Value = model.EndDate;
            parameters[13].Value = model.GenerateDate;
            parameters[14].Value = model.UsedDate;
            parameters[15].Value = model.Remark;

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
        public bool Update(Maticsoft.WeChat.Model.Activity.ActivityCode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_ActivityCode set ");
            strSql.Append("ActivityId=@ActivityId,");
            strSql.Append("AwardId=@AwardId,");
            strSql.Append("AwardName=@AwardName,");
            strSql.Append("ActivityName=@ActivityName,");
            strSql.Append("ActivityPwd=@ActivityPwd,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Status=@Status,");
            strSql.Append("IsPwd=@IsPwd,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("GenerateDate=@GenerateDate,");
            strSql.Append("UsedDate=@UsedDate,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where CodeName=@CodeName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
					new SqlParameter("@AwardId", SqlDbType.Int,4),
					new SqlParameter("@AwardName", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivityName", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivityPwd", SqlDbType.NVarChar,200),
					new SqlParameter("@UserId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Bit,1),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@GenerateDate", SqlDbType.DateTime),
					new SqlParameter("@UsedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@CodeName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ActivityId;
            parameters[1].Value = model.AwardId;
            parameters[2].Value = model.AwardName;
            parameters[3].Value = model.ActivityName;
            parameters[4].Value = model.ActivityPwd;
            parameters[5].Value = model.UserId;
            parameters[6].Value = model.UserName;
            parameters[7].Value = model.Phone;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.IsPwd;
            parameters[10].Value = model.StartDate;
            parameters[11].Value = model.EndDate;
            parameters[12].Value = model.GenerateDate;
            parameters[13].Value = model.UsedDate;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.CodeName;

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
        public bool Delete(string CodeName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_ActivityCode ");
            strSql.Append(" where CodeName=@CodeName ");
            SqlParameter[] parameters = {
					new SqlParameter("@CodeName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = CodeName;

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
        public bool DeleteList(string CodeNamelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_ActivityCode ");
            strSql.Append(" where CodeName in (" + CodeNamelist + ")  ");
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
        public Maticsoft.WeChat.Model.Activity.ActivityCode GetModel(string CodeName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CodeName,ActivityId,AwardId,AwardName,ActivityName,ActivityPwd,UserId,UserName,Phone,Status,IsPwd,StartDate,EndDate,GenerateDate,UsedDate,Remark from WeChat_ActivityCode ");
            strSql.Append(" where CodeName=@CodeName ");
            SqlParameter[] parameters = {
					new SqlParameter("@CodeName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = CodeName;

            Maticsoft.WeChat.Model.Activity.ActivityCode model = new Maticsoft.WeChat.Model.Activity.ActivityCode();
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
        public Maticsoft.WeChat.Model.Activity.ActivityCode DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Activity.ActivityCode model = new Maticsoft.WeChat.Model.Activity.ActivityCode();
            if (row != null)
            {
                if (row["CodeName"] != null)
                {
                    model.CodeName = row["CodeName"].ToString();
                }
                if (row["ActivityId"] != null && row["ActivityId"].ToString() != "")
                {
                    model.ActivityId = int.Parse(row["ActivityId"].ToString());
                }
                if (row["AwardId"] != null && row["AwardId"].ToString() != "")
                {
                    model.AwardId = int.Parse(row["AwardId"].ToString());
                }
                if (row["AwardName"] != null)
                {
                    model.AwardName = row["AwardName"].ToString();
                }
                if (row["ActivityName"] != null)
                {
                    model.ActivityName = row["ActivityName"].ToString();
                }
                if (row["ActivityPwd"] != null)
                {
                    model.ActivityPwd = row["ActivityPwd"].ToString();
                }
                if (row["UserId"] != null)
                {
                    model.UserId = row["UserId"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["Phone"] != null)
                {
                    model.Phone = row["Phone"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["IsPwd"] != null && row["IsPwd"].ToString() != "")
                {
                    if ((row["IsPwd"].ToString() == "1") || (row["IsPwd"].ToString().ToLower() == "true"))
                    {
                        model.IsPwd = true;
                    }
                    else
                    {
                        model.IsPwd = false;
                    }
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["GenerateDate"] != null && row["GenerateDate"].ToString() != "")
                {
                    model.GenerateDate = DateTime.Parse(row["GenerateDate"].ToString());
                }
                if (row["UsedDate"] != null && row["UsedDate"].ToString() != "")
                {
                    model.UsedDate = DateTime.Parse(row["UsedDate"].ToString());
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
            strSql.Append("select CodeName,ActivityId,AwardId,AwardName,ActivityName,ActivityPwd,UserId,UserName,Phone,Status,IsPwd,StartDate,EndDate,GenerateDate,UsedDate,Remark ");
            strSql.Append(" FROM WeChat_ActivityCode ");
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
            strSql.Append(" CodeName,ActivityId,AwardId,AwardName,ActivityName,ActivityPwd,UserId,UserName,Phone,Status,IsPwd,StartDate,EndDate,GenerateDate,UsedDate,Remark ");
            strSql.Append(" FROM WeChat_ActivityCode ");
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
            strSql.Append("select count(1) FROM WeChat_ActivityCode ");
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
                strSql.Append("order by T.CodeName desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_ActivityCode T ");
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
            parameters[0].Value = "WeChat_ActivityCode";
            parameters[1].Value = "CodeName";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

		#region  ExtensionMethod
       public Maticsoft.WeChat.Model.Activity.ActivityCode GetRandCode(int activityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  *  from WeChat_ActivityCode ");
            strSql.Append(" where ActivityId=@ActivityId and Status=0 order  By NewID() ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int)			};
            parameters[0].Value = activityId;
            Maticsoft.WeChat.Model.Activity.ActivityCode model = new Maticsoft.WeChat.Model.Activity.ActivityCode();
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

       public bool UpdateUser(string CodeName, string userId, string userName, int status, string phone, string remark)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("update WeChat_ActivityCode set ");
           strSql.Append("UserId=@UserId,UserName=@UserName, Status=@Status,");
           strSql.Append("Phone=@Phone,Remark=@Remark");
           strSql.Append(" where CodeName=@CodeName  ");
           SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,200),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CodeName", SqlDbType.NVarChar,200)
					};
           parameters[0].Value = userId;
           parameters[1].Value = userName;
           parameters[2].Value = phone;
           parameters[3].Value = status;
           parameters[4].Value = remark;
           parameters[5].Value = CodeName;

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
       public bool UpdateStatusList(string ids, int status)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("update WeChat_ActivityCode set ");
           strSql.Append("Status=" + status);
           strSql.Append(" where CodeName in (" + ids + ")   ");
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
		#endregion  ExtensionMethod
	}
}

