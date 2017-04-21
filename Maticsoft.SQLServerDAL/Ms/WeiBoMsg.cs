/**
* WeiBoMsg.cs
*
* 功 能： N/A
* 类 名： WeiBoMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/13 10:43:59   N/A    初版
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
using Maticsoft.IDAL.Ms;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Ms
{
	/// <summary>
	/// 数据访问类:WeiBoMsg
	/// </summary>
	public partial class WeiBoMsg:IWeiBoMsg
	{
		public WeiBoMsg()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("WeiBoId", "Ms_WeiBoMsg");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int WeiBoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_WeiBoMsg");
            strSql.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] parameters = {
					new SqlParameter("@WeiBoId", SqlDbType.Int,4)
			};
            parameters[0].Value = WeiBoId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ms_WeiBoMsg(");
            strSql.Append("WeiboMsg,ImageUrl,CreateDate,PublishDate)");
            strSql.Append(" values (");
            strSql.Append("@WeiboMsg,@ImageUrl,@CreateDate,@PublishDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WeiboMsg", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@PublishDate", SqlDbType.DateTime)};
            parameters[0].Value = model.WeiboMsg;
            parameters[1].Value = model.ImageUrl;
            parameters[2].Value = model.CreateDate;
            parameters[3].Value = model.PublishDate;

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
        public bool Update(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_WeiBoMsg set ");
            strSql.Append("WeiboMsg=@WeiboMsg,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("PublishDate=@PublishDate");
            strSql.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] parameters = {
					new SqlParameter("@WeiboMsg", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@WeiBoId", SqlDbType.Int,4)};
            parameters[0].Value = model.WeiboMsg;
            parameters[1].Value = model.ImageUrl;
            parameters[2].Value = model.CreateDate;
            parameters[3].Value = model.PublishDate;
            parameters[4].Value = model.WeiBoId;

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
        public bool Delete(int WeiBoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_WeiBoMsg ");
            strSql.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] parameters = {
					new SqlParameter("@WeiBoId", SqlDbType.Int,4)
			};
            parameters[0].Value = WeiBoId;

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
        public bool DeleteList(string WeiBoIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_WeiBoMsg ");
            strSql.Append(" where WeiBoId in (" + WeiBoIdlist + ")  ");
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
        public Maticsoft.Model.Ms.WeiBoMsg GetModel(int WeiBoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 WeiBoId,WeiboMsg,ImageUrl,CreateDate,PublishDate from Ms_WeiBoMsg ");
            strSql.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] parameters = {
					new SqlParameter("@WeiBoId", SqlDbType.Int,4)
			};
            parameters[0].Value = WeiBoId;

            Maticsoft.Model.Ms.WeiBoMsg model = new Maticsoft.Model.Ms.WeiBoMsg();
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
        public Maticsoft.Model.Ms.WeiBoMsg DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.WeiBoMsg model = new Maticsoft.Model.Ms.WeiBoMsg();
            if (row != null)
            {
                if (row["WeiBoId"] != null && row["WeiBoId"].ToString() != "")
                {
                    model.WeiBoId = int.Parse(row["WeiBoId"].ToString());
                }
                if (row["WeiboMsg"] != null)
                {
                    model.WeiboMsg = row["WeiboMsg"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["PublishDate"] != null && row["PublishDate"].ToString() != "")
                {
                    model.PublishDate = DateTime.Parse(row["PublishDate"].ToString());
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
            strSql.Append("select WeiBoId,WeiboMsg,ImageUrl,CreateDate,PublishDate ");
            strSql.Append(" FROM Ms_WeiBoMsg ");
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
            strSql.Append(" WeiBoId,WeiboMsg,ImageUrl,CreateDate,PublishDate ");
            strSql.Append(" FROM Ms_WeiBoMsg ");
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
            strSql.Append("select count(1) FROM Ms_WeiBoMsg ");
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
                strSql.Append("order by T.WeiBoId desc");
            }
            strSql.Append(")AS Row, T.*  from Ms_WeiBoMsg T ");
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
            parameters[0].Value = "Ms_WeiBoMsg";
            parameters[1].Value = "WeiBoId";
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

