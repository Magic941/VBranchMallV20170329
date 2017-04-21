/**  版本信息模板在安装目录下，可自行修改。
* ActivityAward.cs
*
* 功 能： N/A
* 类 名： ActivityAward
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:15   N/A    初版
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
	/// 数据访问类:ActivityAward
	/// </summary>
	public partial class ActivityAward:IActivityAward
	{
		public ActivityAward()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AwardId", "WeChat_ActivityAward");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AwardId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_ActivityAward");
            strSql.Append(" where AwardId=@AwardId");
            SqlParameter[] parameters = {
					new SqlParameter("@AwardId", SqlDbType.Int,4)
			};
            parameters[0].Value = AwardId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Activity.ActivityAward model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_ActivityAward(");
            strSql.Append("ActivityId,AwardName,GiftName,ImageUrl,ThumbImage,Count,AwardDesc,Remark,TargetId)");
            strSql.Append(" values (");
            strSql.Append("@ActivityId,@AwardName,@GiftName,@ImageUrl,@ThumbImage,@Count,@AwardDesc,@Remark,@TargetId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
					new SqlParameter("@AwardName", SqlDbType.NVarChar,200),
					new SqlParameter("@GiftName", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbImage", SqlDbType.NVarChar,200),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@AwardDesc", SqlDbType.NVarChar,-1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters[0].Value = model.ActivityId;
            parameters[1].Value = model.AwardName;
            parameters[2].Value = model.GiftName;
            parameters[3].Value = model.ImageUrl;
            parameters[4].Value = model.ThumbImage;
            parameters[5].Value = model.Count;
            parameters[6].Value = model.AwardDesc;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.TargetId;

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
        public bool Update(Maticsoft.WeChat.Model.Activity.ActivityAward model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_ActivityAward set ");
            strSql.Append("ActivityId=@ActivityId,");
            strSql.Append("AwardName=@AwardName,");
            strSql.Append("GiftName=@GiftName,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("ThumbImage=@ThumbImage,");
            strSql.Append("Count=@Count,");
            strSql.Append("AwardDesc=@AwardDesc,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("TargetId=@TargetId");
            strSql.Append(" where AwardId=@AwardId");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
					new SqlParameter("@AwardName", SqlDbType.NVarChar,200),
					new SqlParameter("@GiftName", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbImage", SqlDbType.NVarChar,200),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@AwardDesc", SqlDbType.NVarChar,-1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@AwardId", SqlDbType.Int,4)};
            parameters[0].Value = model.ActivityId;
            parameters[1].Value = model.AwardName;
            parameters[2].Value = model.GiftName;
            parameters[3].Value = model.ImageUrl;
            parameters[4].Value = model.ThumbImage;
            parameters[5].Value = model.Count;
            parameters[6].Value = model.AwardDesc;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.TargetId;
            parameters[9].Value = model.AwardId;

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
        public bool Delete(int AwardId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_ActivityAward ");
            strSql.Append(" where AwardId=@AwardId");
            SqlParameter[] parameters = {
					new SqlParameter("@AwardId", SqlDbType.Int,4)
			};
            parameters[0].Value = AwardId;

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
        public bool DeleteList(string AwardIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_ActivityAward ");
            strSql.Append(" where AwardId in (" + AwardIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Activity.ActivityAward GetModel(int AwardId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AwardId,ActivityId,AwardName,GiftName,ImageUrl,ThumbImage,Count,AwardDesc,Remark,TargetId from WeChat_ActivityAward ");
            strSql.Append(" where AwardId=@AwardId");
            SqlParameter[] parameters = {
					new SqlParameter("@AwardId", SqlDbType.Int,4)
			};
            parameters[0].Value = AwardId;

            Maticsoft.WeChat.Model.Activity.ActivityAward model = new Maticsoft.WeChat.Model.Activity.ActivityAward();
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
        public Maticsoft.WeChat.Model.Activity.ActivityAward DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Activity.ActivityAward model = new Maticsoft.WeChat.Model.Activity.ActivityAward();
            if (row != null)
            {
                if (row["AwardId"] != null && row["AwardId"].ToString() != "")
                {
                    model.AwardId = int.Parse(row["AwardId"].ToString());
                }
                if (row["ActivityId"] != null && row["ActivityId"].ToString() != "")
                {
                    model.ActivityId = int.Parse(row["ActivityId"].ToString());
                }
                if (row["AwardName"] != null)
                {
                    model.AwardName = row["AwardName"].ToString();
                }
                if (row["GiftName"] != null)
                {
                    model.GiftName = row["GiftName"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["ThumbImage"] != null)
                {
                    model.ThumbImage = row["ThumbImage"].ToString();
                }
                if (row["Count"] != null && row["Count"].ToString() != "")
                {
                    model.Count = int.Parse(row["Count"].ToString());
                }
                if (row["AwardDesc"] != null)
                {
                    model.AwardDesc = row["AwardDesc"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["TargetId"] != null && row["TargetId"].ToString() != "")
                {
                    model.TargetId = int.Parse(row["TargetId"].ToString());
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
            strSql.Append("select AwardId,ActivityId,AwardName,GiftName,ImageUrl,ThumbImage,Count,AwardDesc,Remark,TargetId ");
            strSql.Append(" FROM WeChat_ActivityAward ");
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
            strSql.Append(" AwardId,ActivityId,AwardName,GiftName,ImageUrl,ThumbImage,Count,AwardDesc,Remark,TargetId ");
            strSql.Append(" FROM WeChat_ActivityAward ");
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
            strSql.Append("select count(1) FROM WeChat_ActivityAward ");
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
                strSql.Append("order by T.AwardId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_ActivityAward T ");
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
            parameters[0].Value = "WeChat_ActivityAward";
            parameters[1].Value = "AwardId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod
        public Maticsoft.WeChat.Model.Activity.ActivityAward GetAwardInfo(int targetId, int ActivityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from WeChat_ActivityAward ");
            strSql.Append(" where TargetId=@TargetId and ActivityId=@ActivityId");
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TargetId", SqlDbType.Int, 4),
				new SqlParameter("@ActivityId", SqlDbType.Int, 4)
			};
            parameters[0].Value = targetId;
            parameters[1].Value = ActivityId;
            Maticsoft.WeChat.Model.Activity.ActivityAward model = new Maticsoft.WeChat.Model.Activity.ActivityAward();
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

