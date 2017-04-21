/**
* Tags.cs
*
* 功 能： N/A
* 类 名： Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:58   N/A    初版
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
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:Tags
	/// </summary>
	public partial class Tags:ITags
	{
		public Tags()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TagID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Tags");
            strSql.Append(" where TagID=@TagID");
            SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
            parameters[0].Value = TagID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TypeId, string TagName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Tags");
            strSql.Append(" where TypeId=@TypeId and TagName=@TagName");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4),
                    new SqlParameter("@TagName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = TypeId;
            parameters[1].Value = TagName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Tags model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Tags(");
            strSql.Append("TagName,TypeId,IsRecommand,Status)");
            strSql.Append(" values (");
            strSql.Append("@TagName,@TypeId,@IsRecommand,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@IsRecommand", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.TagName;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.IsRecommand;
            parameters[3].Value = model.Status;

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
        public bool Update(Maticsoft.Model.SNS.Tags model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Tags set ");
            strSql.Append("TagName=@TagName,");
            strSql.Append("TypeId=@TypeId,");
            strSql.Append("IsRecommand=@IsRecommand,");
            strSql.Append("Status=@Status");
            strSql.Append(" where TagID=@TagID");
            SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@IsRecommand", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@TagID", SqlDbType.Int,4)};
            parameters[0].Value = model.TagName;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.IsRecommand;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.TagID;

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
        public bool Delete(int TagID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Tags ");
            strSql.Append(" where TagID=@TagID");
            SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
            parameters[0].Value = TagID;

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
        public bool DeleteList(string TagIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Tags ");
            strSql.Append(" where TagID in (" + TagIDlist + ")  ");
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
        public Maticsoft.Model.SNS.Tags GetModel(int TagID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TagID,TagName,TypeId,IsRecommand,Status from SNS_Tags ");
            strSql.Append(" where TagID=@TagID");
            SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
            parameters[0].Value = TagID;

            Maticsoft.Model.SNS.Tags model = new Maticsoft.Model.SNS.Tags();
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
        public Maticsoft.Model.SNS.Tags DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Tags model = new Maticsoft.Model.SNS.Tags();
            if (row != null)
            {
                if (row["TagID"] != null && row["TagID"].ToString() != "")
                {
                    model.TagID = int.Parse(row["TagID"].ToString());
                }
                if (row["TagName"] != null && row["TagName"].ToString() != "")
                {
                    model.TagName = row["TagName"].ToString();
                }
                if (row["TypeId"] != null && row["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if (row["IsRecommand"] != null && row["IsRecommand"].ToString() != "")
                {
                    model.IsRecommand = int.Parse(row["IsRecommand"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
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
            strSql.Append("select TagID,TagName,TypeId,IsRecommand,Status ");
            strSql.Append(" FROM SNS_Tags ");
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
            strSql.Append(" TagID,TagName,TypeId,IsRecommand,Status ");
            strSql.Append(" FROM SNS_Tags ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append(" order by TagID desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_Tags ");
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
                strSql.Append("order by T.TagID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Tags T ");
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
            parameters[0].Value = "SNS_Tags";
            parameters[1].Value = "TagID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
		#region  ExtensionMethod
        public DataSet GetHotTags(int top)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   ");
            if (top > 10)
            {
                strSql.Append("top " + top + "  * from  SNS_Tags order by newid()");
            }
            else
            {
                strSql.Append("top 10  * from  SNS_Tags order by newid()");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SNST.*,SNSTT.TypeName ");
            strSql.Append("  FROM SNS_Tags SNST LEFT JOIN SNS_TagType SNSTT ON SNSTT.ID=SNST.TypeId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append(" order by TagID desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Tags set ");
            strSql.AppendFormat(" IsRecommand={0} ", IsRecommand);
            strSql.AppendFormat(" where TagID IN({0})", IdList);

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Tags set ");
            strSql.AppendFormat(" Status={0} ", Status);
            strSql.AppendFormat(" where TagID IN({0})", IdList);

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

