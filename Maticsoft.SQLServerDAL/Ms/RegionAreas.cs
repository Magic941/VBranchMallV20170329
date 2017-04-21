/**
* Area.cs
*
* 功 能： N/A
* 类 名： Area
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 18:28:15   N/A    初版
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
using Maticsoft.IDAL.Ms;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Ms
{
	/// <summary>
	/// 数据访问类:Area
	/// </summary>
	public partial class RegionAreas:IRegionAreas
	{
		public RegionAreas()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AreaId", "Ms_RegionAreas");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AreaId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_RegionAreas");
            strSql.Append(" where AreaId=@AreaId");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4)
			};
            parameters[0].Value = AreaId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Ms.RegionAreas model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ms_RegionAreas(");
            strSql.Append("Name)");
            strSql.Append(" values (");
            strSql.Append("@Name)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Name;

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
        public bool Update(Maticsoft.Model.Ms.RegionAreas model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_RegionAreas set ");
            strSql.Append("Name=@Name");
            strSql.Append(" where AreaId=@AreaId");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@AreaId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.AreaId;

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
        public bool Delete(int AreaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_RegionAreas ");
            strSql.Append(" where AreaId=@AreaId");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4)
			};
            parameters[0].Value = AreaId;

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
        public bool DeleteList(string AreaIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_RegionAreas ");
            strSql.Append(" where AreaId in (" + AreaIdlist + ")  ");
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
        public Maticsoft.Model.Ms.RegionAreas GetModel(int AreaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AreaId,Name from Ms_RegionAreas ");
            strSql.Append(" where AreaId=@AreaId");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4)
			};
            parameters[0].Value = AreaId;

            Maticsoft.Model.Ms.RegionAreas model = new Maticsoft.Model.Ms.RegionAreas();
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
        public Maticsoft.Model.Ms.RegionAreas DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.RegionAreas model = new Maticsoft.Model.Ms.RegionAreas();
            if (row != null)
            {
                if (row["AreaId"] != null && row["AreaId"].ToString() != "")
                {
                    model.AreaId = int.Parse(row["AreaId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
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
            strSql.Append("select AreaId,Name ");
            strSql.Append(" FROM Ms_RegionAreas ");
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
            strSql.Append(" AreaId,Name ");
            strSql.Append(" FROM Ms_RegionAreas ");
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
            strSql.Append("select count(1) FROM Ms_RegionAreas ");
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
                strSql.Append("order by T.AreaId desc");
            }
            strSql.Append(")AS Row, T.*  from Ms_RegionAreas T ");
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
            parameters[0].Value = "Ms_RegionAreas";
            parameters[1].Value = "AreaId";
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

