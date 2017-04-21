/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：FilterWords.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/24 11:00:36
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Settings;

namespace Maticsoft.SQLServerDAL.Settings
{
    /// <summary>
    /// 数据访问类:FilterWords
    /// </summary>
    public partial class FilterWords : IFilterWords
    {
        public FilterWords()
        { }

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("FilterId", "SA_FilterWords");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FilterId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SA_FilterWords");
            strSql.Append(" where FilterId=@FilterId");
            SqlParameter[] parameters = {
					new SqlParameter("@FilterId", SqlDbType.Int,4)
			};
            parameters[0].Value = FilterId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Settings.FilterWords model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_FilterWords(");
            strSql.Append("WordPattern,ActionType,RepalceWord)");
            strSql.Append(" values (");
            strSql.Append("@WordPattern,@ActionType,@RepalceWord)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WordPattern", SqlDbType.NVarChar,100),
					new SqlParameter("@ActionType", SqlDbType.Int,4),
					new SqlParameter("@RepalceWord", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.WordPattern;
            parameters[1].Value = model.ActionType;
            parameters[2].Value = model.RepalceWord;

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
        public bool Update(Maticsoft.Model.Settings.FilterWords model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_FilterWords set ");
            strSql.Append("WordPattern=@WordPattern,");
            strSql.Append("ActionType=@ActionType,");
            strSql.Append("RepalceWord=@RepalceWord");
            strSql.Append(" where FilterId=@FilterId");
            SqlParameter[] parameters = {
					new SqlParameter("@WordPattern", SqlDbType.NVarChar,100),
					new SqlParameter("@ActionType", SqlDbType.Int,4),
					new SqlParameter("@RepalceWord", SqlDbType.NVarChar,100),
					new SqlParameter("@FilterId", SqlDbType.Int,4)};
            parameters[0].Value = model.WordPattern;
            parameters[1].Value = model.ActionType;
            parameters[2].Value = model.RepalceWord;
            parameters[3].Value = model.FilterId;

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
        public bool Delete(int FilterId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_FilterWords ");
            strSql.Append(" where FilterId=@FilterId");
            SqlParameter[] parameters = {
					new SqlParameter("@FilterId", SqlDbType.Int,4)
			};
            parameters[0].Value = FilterId;

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
        public bool DeleteList(string FilterIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_FilterWords ");
            strSql.Append(" where FilterId in (" + FilterIdlist + ")  ");
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
        public Maticsoft.Model.Settings.FilterWords GetModel(int FilterId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 FilterId,WordPattern,ActionType,RepalceWord from SA_FilterWords ");
            strSql.Append(" where FilterId=@FilterId");
            SqlParameter[] parameters = {
					new SqlParameter("@FilterId", SqlDbType.Int,4)
			};
            parameters[0].Value = FilterId;

            Maticsoft.Model.Settings.FilterWords model = new Maticsoft.Model.Settings.FilterWords();
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
        public Maticsoft.Model.Settings.FilterWords DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Settings.FilterWords model = new Maticsoft.Model.Settings.FilterWords();
            if (row != null)
            {
                if (row["FilterId"] != null && row["FilterId"].ToString() != "")
                {
                    model.FilterId = int.Parse(row["FilterId"].ToString());
                }
                if (row["WordPattern"] != null)
                {
                    model.WordPattern = row["WordPattern"].ToString();
                }
                if (row["ActionType"] != null && row["ActionType"].ToString() != "")
                {
                    model.ActionType = int.Parse(row["ActionType"].ToString());
                }
                if (row["RepalceWord"] != null)
                {
                    model.RepalceWord = row["RepalceWord"].ToString();
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
            strSql.Append("select FilterId,WordPattern,ActionType,RepalceWord ");
            strSql.Append(" FROM SA_FilterWords ");
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
            strSql.Append(" FilterId,WordPattern,ActionType,RepalceWord ");
            strSql.Append(" FROM SA_FilterWords ");
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
            strSql.Append("select count(1) FROM SA_FilterWords ");
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
                strSql.Append("order by T.FilterId desc");
            }
            strSql.Append(")AS Row, T.*  from SA_FilterWords T ");
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
            parameters[0].Value = "SA_FilterWords";
            parameters[1].Value = "FilterId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        #region 扩展方法

        public Model.Settings.FilterWords GetByWordPattern(string wordPattern)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 FilterId,WordPattern,ActionType,RepalceWord from SA_FilterWords ");
            strSql.Append(" where WordPattern='@WordPattern'");
            SqlParameter[] parameters = {
					new SqlParameter("@WordPattern", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = wordPattern;

            Maticsoft.Model.Settings.FilterWords model = new Maticsoft.Model.Settings.FilterWords();
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

        public bool UpdateActionType(string ids, int type,string replace)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_FilterWords set ");
            strSql.Append("ActionType=@ActionType,RepalceWord=@RepalceWord");
            strSql.Append(" where FilterId in (" + ids + ")  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActionType", SqlDbType.Int,4),
                    	new SqlParameter("@RepalceWord", SqlDbType.NVarChar,100)
                                        };
            parameters[0].Value = type;
            parameters[1].Value = replace;

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

        public bool Exists(string word)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SA_FilterWords");
            strSql.Append(" where WordPattern=@WordPattern");
            SqlParameter[] parameters = {
					new SqlParameter("@WordPattern", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = word;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        #endregion 
    }
}