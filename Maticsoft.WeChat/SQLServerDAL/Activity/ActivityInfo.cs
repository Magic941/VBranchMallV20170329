/**  版本信息模板在安装目录下，可自行修改。
* ActivityInfo.cs
*
* 功 能： N/A
* 类 名： ActivityInfo
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
	/// 数据访问类:ActivityInfo
	/// </summary>
	public partial class ActivityInfo:IActivityInfo
	{
		public ActivityInfo()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ActivityId", "WeChat_ActivityInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ActivityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_ActivityInfo");
            strSql.Append(" where ActivityId=@ActivityId");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4)
			};
            parameters[0].Value = ActivityId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Activity.ActivityInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_ActivityInfo(");
            strSql.Append("OpenId,PreName,Name,ImageUrl,StartDate,EndDate,CreatedDate,Summary,ActivityDesc,CreatedUserId,Type,Status,Probability,LimitType,UserTotal,EachCount,DayTotal,IsPwd,PwdLength,Remark,AwardType)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@PreName,@Name,@ImageUrl,@StartDate,@EndDate,@CreatedDate,@Summary,@ActivityDesc,@CreatedUserId,@Type,@Status,@Probability,@LimitType,@UserTotal,@EachCount,@DayTotal,@IsPwd,@PwdLength,@Remark,@AwardType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,50),
					new SqlParameter("@PreName", SqlDbType.NVarChar,200),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Summary", SqlDbType.NVarChar,300),
					new SqlParameter("@ActivityDesc", SqlDbType.NVarChar,-1),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Probability", SqlDbType.Decimal,9),
					new SqlParameter("@LimitType", SqlDbType.Int,4),
					new SqlParameter("@UserTotal", SqlDbType.Int,4),
					new SqlParameter("@EachCount", SqlDbType.Int,4),
					new SqlParameter("@DayTotal", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Bit,1),
					new SqlParameter("@PwdLength", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@AwardType", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.PreName;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.ImageUrl;
            parameters[4].Value = model.StartDate;
            parameters[5].Value = model.EndDate;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Summary;
            parameters[8].Value = model.ActivityDesc;
            parameters[9].Value = model.CreatedUserId;
            parameters[10].Value = model.Type;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.Probability;
            parameters[13].Value = model.LimitType;
            parameters[14].Value = model.UserTotal;
            parameters[15].Value = model.EachCount;
            parameters[16].Value = model.DayTotal;
            parameters[17].Value = model.IsPwd;
            parameters[18].Value = model.PwdLength;
            parameters[19].Value = model.Remark;
            parameters[20].Value = model.AwardType;

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
        public bool Update(Maticsoft.WeChat.Model.Activity.ActivityInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_ActivityInfo set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("PreName=@PreName,");
            strSql.Append("Name=@Name,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("ActivityDesc=@ActivityDesc,");
            strSql.Append("CreatedUserId=@CreatedUserId,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("Probability=@Probability,");
            strSql.Append("LimitType=@LimitType,");
            strSql.Append("UserTotal=@UserTotal,");
            strSql.Append("EachCount=@EachCount,");
            strSql.Append("DayTotal=@DayTotal,");
            strSql.Append("IsPwd=@IsPwd,");
            strSql.Append("PwdLength=@PwdLength,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AwardType=@AwardType");
            strSql.Append(" where ActivityId=@ActivityId");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,50),
					new SqlParameter("@PreName", SqlDbType.NVarChar,200),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Summary", SqlDbType.NVarChar,300),
					new SqlParameter("@ActivityDesc", SqlDbType.NVarChar,-1),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Probability", SqlDbType.Decimal,9),
					new SqlParameter("@LimitType", SqlDbType.Int,4),
					new SqlParameter("@UserTotal", SqlDbType.Int,4),
					new SqlParameter("@EachCount", SqlDbType.Int,4),
					new SqlParameter("@DayTotal", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Bit,1),
					new SqlParameter("@PwdLength", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@AwardType", SqlDbType.Int,4),
					new SqlParameter("@ActivityId", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.PreName;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.ImageUrl;
            parameters[4].Value = model.StartDate;
            parameters[5].Value = model.EndDate;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Summary;
            parameters[8].Value = model.ActivityDesc;
            parameters[9].Value = model.CreatedUserId;
            parameters[10].Value = model.Type;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.Probability;
            parameters[13].Value = model.LimitType;
            parameters[14].Value = model.UserTotal;
            parameters[15].Value = model.EachCount;
            parameters[16].Value = model.DayTotal;
            parameters[17].Value = model.IsPwd;
            parameters[18].Value = model.PwdLength;
            parameters[19].Value = model.Remark;
            parameters[20].Value = model.AwardType;
            parameters[21].Value = model.ActivityId;

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
        public bool Delete(int ActivityId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_ActivityInfo ");
            strSql.Append(" where ActivityId=@ActivityId");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4)
			};
            parameters[0].Value = ActivityId;

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
        public bool DeleteList(string ActivityIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_ActivityInfo ");
            strSql.Append(" where ActivityId in (" + ActivityIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Activity.ActivityInfo GetModel(int ActivityId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ActivityId,OpenId,PreName,Name,ImageUrl,StartDate,EndDate,CreatedDate,Summary,ActivityDesc,CreatedUserId,Type,Status,Probability,LimitType,UserTotal,EachCount,DayTotal,IsPwd,PwdLength,Remark,AwardType from WeChat_ActivityInfo ");
            strSql.Append(" where ActivityId=@ActivityId");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4)
			};
            parameters[0].Value = ActivityId;

            Maticsoft.WeChat.Model.Activity.ActivityInfo model = new Maticsoft.WeChat.Model.Activity.ActivityInfo();
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
        public Maticsoft.WeChat.Model.Activity.ActivityInfo DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Activity.ActivityInfo model = new Maticsoft.WeChat.Model.Activity.ActivityInfo();
            if (row != null)
            {
                if (row["ActivityId"] != null && row["ActivityId"].ToString() != "")
                {
                    model.ActivityId = int.Parse(row["ActivityId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["PreName"] != null)
                {
                    model.PreName = row["PreName"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Summary"] != null)
                {
                    model.Summary = row["Summary"].ToString();
                }
                if (row["ActivityDesc"] != null)
                {
                    model.ActivityDesc = row["ActivityDesc"].ToString();
                }
                if (row["CreatedUserId"] != null && row["CreatedUserId"].ToString() != "")
                {
                    model.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Probability"] != null && row["Probability"].ToString() != "")
                {
                    model.Probability = decimal.Parse(row["Probability"].ToString());
                }
                if (row["LimitType"] != null && row["LimitType"].ToString() != "")
                {
                    model.LimitType = int.Parse(row["LimitType"].ToString());
                }
                if (row["UserTotal"] != null && row["UserTotal"].ToString() != "")
                {
                    model.UserTotal = int.Parse(row["UserTotal"].ToString());
                }
                if (row["EachCount"] != null && row["EachCount"].ToString() != "")
                {
                    model.EachCount = int.Parse(row["EachCount"].ToString());
                }
                if (row["DayTotal"] != null && row["DayTotal"].ToString() != "")
                {
                    model.DayTotal = int.Parse(row["DayTotal"].ToString());
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
                if (row["PwdLength"] != null && row["PwdLength"].ToString() != "")
                {
                    model.PwdLength = int.Parse(row["PwdLength"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["AwardType"] != null && row["AwardType"].ToString() != "")
                {
                    model.AwardType = int.Parse(row["AwardType"].ToString());
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
            strSql.Append("select ActivityId,OpenId,PreName,Name,ImageUrl,StartDate,EndDate,CreatedDate,Summary,ActivityDesc,CreatedUserId,Type,Status,Probability,LimitType,UserTotal,EachCount,DayTotal,IsPwd,PwdLength,Remark,AwardType ");
            strSql.Append(" FROM WeChat_ActivityInfo ");
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
            strSql.Append(" ActivityId,OpenId,PreName,Name,ImageUrl,StartDate,EndDate,CreatedDate,Summary,ActivityDesc,CreatedUserId,Type,Status,Probability,LimitType,UserTotal,EachCount,DayTotal,IsPwd,PwdLength,Remark,AwardType ");
            strSql.Append(" FROM WeChat_ActivityInfo ");
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
            strSql.Append("select count(1) FROM WeChat_ActivityInfo ");
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
                strSql.Append("order by T.ActivityId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_ActivityInfo T ");
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
            parameters[0].Value = "WeChat_ActivityInfo";
            parameters[1].Value = "ActivityId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

		#region  ExtensionMethod
        public bool UpdateStatus(int activityId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_ActivityInfo set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where ActivityId=@ActivityId");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ActivityId", SqlDbType.Int,4)};
            parameters[0].Value = status;
            parameters[1].Value = activityId;

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
        /// 获取活动
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Maticsoft.WeChat.Model.Activity.ActivityInfo GetActivity(int activityId, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from WeChat_ActivityInfo ");
            strSql.Append(" where Status=1 and Type=@Type and EndDate>=GETDATE()");
            if (activityId > 0)
            {
                strSql.Append(" and ActivityId=@ActivityId ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
			};
            parameters[0].Value = activityId;
            parameters[1].Value = type;

            Maticsoft.WeChat.Model.Activity.ActivityInfo model = new Maticsoft.WeChat.Model.Activity.ActivityInfo();
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
		#endregion  ExtensionMethod
	}
}

