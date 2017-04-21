/**
* Tags.cs
*
* 功 能： N/A
* 类 名： Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年12月14日 10:13:34   Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;//Please add references
using Maticsoft.IDAL.Shop.Tags;

namespace Maticsoft.SQLServerDAL.Shop.Tags
{
    /// <summary>
    /// 数据访问类:Tags
    /// </summary>
    public partial class Tags : ITags
    {
        public Tags()
        { }

        #region Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TagID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_Tags");
            strSql.Append(" WHERE TagID=@TagID");
            SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
            parameters[0].Value = TagID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Tags.Tags model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_Tags(");
            strSql.Append("TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords)");
            strSql.Append(" VALUES (");
            strSql.Append("@TagCategoryId,@TagName,@IsRecommand,@Status,@Meta_Title,@Meta_Description,@Meta_Keywords)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TagCategoryId", SqlDbType.Int,4),
					new SqlParameter("@TagName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRecommand", SqlDbType.Bit,1),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.TagCategoryId;
            parameters[1].Value = model.TagName;
            parameters[2].Value = model.IsRecommand;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Meta_Title;
            parameters[5].Value = model.Meta_Description;
            parameters[6].Value = model.Meta_Keywords;

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
        public bool Update(Maticsoft.Model.Shop.Tags.Tags model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_Tags SET ");
            strSql.Append("TagCategoryId=@TagCategoryId,");
            strSql.Append("TagName=@TagName,");
            strSql.Append("IsRecommand=@IsRecommand,");
            strSql.Append("Status=@Status,");
            strSql.Append("Meta_Title=@Meta_Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords");
            strSql.Append(" WHERE TagID=@TagID");
            SqlParameter[] parameters = {
					new SqlParameter("@TagCategoryId", SqlDbType.Int,4),
					new SqlParameter("@TagName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRecommand", SqlDbType.Bit,1),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@TagID", SqlDbType.Int,4)};
            parameters[0].Value = model.TagCategoryId;
            parameters[1].Value = model.TagName;
            parameters[2].Value = model.IsRecommand;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Meta_Title;
            parameters[5].Value = model.Meta_Description;
            parameters[6].Value = model.Meta_Keywords;
            parameters[7].Value = model.TagID;

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
            strSql.Append("DELETE FROM Shop_Tags ");
            strSql.Append(" WHERE TagID=@TagID");
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
            strSql.Append("DELETE FROM Shop_Tags ");
            strSql.Append(" WHERE TagID in (" + TagIDlist + ")  ");
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
        public Maticsoft.Model.Shop.Tags.Tags GetModel(int TagID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 TagID,TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords FROM Shop_Tags ");
            strSql.Append(" WHERE TagID=@TagID");
            SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
            parameters[0].Value = TagID;

            Maticsoft.Model.Shop.Tags.Tags model = new Maticsoft.Model.Shop.Tags.Tags();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TagID"] != null && ds.Tables[0].Rows[0]["TagID"].ToString() != "")
                {
                    model.TagID = int.Parse(ds.Tables[0].Rows[0]["TagID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TagCategoryId"] != null && ds.Tables[0].Rows[0]["TagCategoryId"].ToString() != "")
                {
                    model.TagCategoryId = int.Parse(ds.Tables[0].Rows[0]["TagCategoryId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TagName"] != null && ds.Tables[0].Rows[0]["TagName"].ToString() != "")
                {
                    model.TagName = ds.Tables[0].Rows[0]["TagName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsRecommand"] != null && ds.Tables[0].Rows[0]["IsRecommand"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsRecommand"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecommand"].ToString().ToLower() == "true"))
                    {
                        model.IsRecommand = true;
                    }
                    else
                    {
                        model.IsRecommand = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Status"] != null && ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Meta_Title"] != null && ds.Tables[0].Rows[0]["Meta_Title"].ToString() != "")
                {
                    model.Meta_Title = ds.Tables[0].Rows[0]["Meta_Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Meta_Description"] != null && ds.Tables[0].Rows[0]["Meta_Description"].ToString() != "")
                {
                    model.Meta_Description = ds.Tables[0].Rows[0]["Meta_Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Meta_Keywords"] != null && ds.Tables[0].Rows[0]["Meta_Keywords"].ToString() != "")
                {
                    model.Meta_Keywords = ds.Tables[0].Rows[0]["Meta_Keywords"].ToString();
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
            strSql.Append("SELECT TagID,TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords ");
            strSql.Append(" FROM Shop_Tags ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" TagID,TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords ");
            strSql.Append(" FROM Shop_Tags ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_Tags ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
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
                strSql.Append("ORDER BY T." + orderby);
            }
            else
            {
                strSql.Append("ORDER BY T.TagID desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_Tags T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion Method

        #region ExtensionMethod

         /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(string IsRecommand, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Tags set ");
            strSql.AppendFormat(" IsRecommand={0} ", "'" + IsRecommand + "'");
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
            strSql.Append("update Shop_Tags set ");
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

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" TagT.*,TagTT.CategoryName ");
            strSql.Append(" from Shop_Tags TagT Left join Shop_TagCategories TagTT on TagTT.ID=TagT.TagCategoryId ");
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

        #endregion
    }
}