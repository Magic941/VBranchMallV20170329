/**  版本信息模板在安装目录下，可自行修改。
* PointsAction.cs
*
* 功 能： N/A
* 类 名： PointsAction
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/16 11:09:25   N/A    初版
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
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Members
{
	/// <summary>
	/// 数据访问类:PointsAction
	/// </summary>
	public partial class PointsAction:IPointsAction
	{
		public PointsAction()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ActionId", "Accounts_PointsAction");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ActionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_PointsAction");
            strSql.Append(" where ActionId=@ActionId");
            SqlParameter[] parameters = {
					new SqlParameter("@ActionId", SqlDbType.Int,4)
			};
            parameters[0].Value = ActionId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.PointsAction model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_PointsAction(");
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
        public bool Update(Maticsoft.Model.Members.PointsAction model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_PointsAction set ");
            strSql.Append("Name=@Name");
            strSql.Append(" where ActionId=@ActionId");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@ActionId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ActionId;

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
        public bool Delete(int ActionId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_PointsAction ");
            strSql.Append(" where ActionId=@ActionId");
            SqlParameter[] parameters = {
					new SqlParameter("@ActionId", SqlDbType.Int,4)
			};
            parameters[0].Value = ActionId;

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
        public bool DeleteList(string ActionIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_PointsAction ");
            strSql.Append(" where ActionId in (" + ActionIdlist + ")  ");
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
        public Maticsoft.Model.Members.PointsAction GetModel(int ActionId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ActionId,Name from Accounts_PointsAction ");
            strSql.Append(" where ActionId=@ActionId");
            SqlParameter[] parameters = {
					new SqlParameter("@ActionId", SqlDbType.Int,4)
			};
            parameters[0].Value = ActionId;

            Maticsoft.Model.Members.PointsAction model = new Maticsoft.Model.Members.PointsAction();
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
        public Maticsoft.Model.Members.PointsAction DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.PointsAction model = new Maticsoft.Model.Members.PointsAction();
            if (row != null)
            {
                if (row["ActionId"] != null && row["ActionId"].ToString() != "")
                {
                    model.ActionId = int.Parse(row["ActionId"].ToString());
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
            strSql.Append("select ActionId,Name ");
            strSql.Append(" FROM Accounts_PointsAction ");
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
            strSql.Append(" ActionId,Name ");
            strSql.Append(" FROM Accounts_PointsAction ");
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
            strSql.Append("select count(1) FROM Accounts_PointsAction ");
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
                strSql.Append("order by T.ActionId desc");
            }
            strSql.Append(")AS Row, T.*  from Accounts_PointsAction T ");
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
            parameters[0].Value = "Accounts_PointsAction";
            parameters[1].Value = "ActionId";
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

