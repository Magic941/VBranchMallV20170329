using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;
using System.Collections.Generic;
namespace Maticsoft.SQLServerDAL.Members
{
	/// <summary>
	/// 数据访问类:PointsLimit
	/// </summary>
	public partial class PointsLimit:IPointsLimit
	{
		public PointsLimit()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("LimitID", "Accounts_PointsLimit");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int LimitID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_PointsLimit");
            strSql.Append(" where LimitID=@LimitID");
            SqlParameter[] parameters = {
					new SqlParameter("@LimitID", SqlDbType.Int,4)
			};
            parameters[0].Value = LimitID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.PointsLimit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_PointsLimit(");
            strSql.Append("Name,Cycle,CycleUnit,MaxTimes,TargetId,TargetType)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Cycle,@CycleUnit,@MaxTimes,@TargetId,@TargetType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Cycle", SqlDbType.Int,4),
					new SqlParameter("@CycleUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@MaxTimes", SqlDbType.Int,4),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@TargetType", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Cycle;
            parameters[2].Value = model.CycleUnit;
            parameters[3].Value = model.MaxTimes;
            parameters[4].Value = model.TargetId;
            parameters[5].Value = model.TargetType;

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
        public bool Update(Maticsoft.Model.Members.PointsLimit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_PointsLimit set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Cycle=@Cycle,");
            strSql.Append("CycleUnit=@CycleUnit,");
            strSql.Append("MaxTimes=@MaxTimes,");
            strSql.Append("TargetId=@TargetId,");
            strSql.Append("TargetType=@TargetType");
            strSql.Append(" where LimitID=@LimitID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Cycle", SqlDbType.Int,4),
					new SqlParameter("@CycleUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@MaxTimes", SqlDbType.Int,4),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@TargetType", SqlDbType.Int,4),
					new SqlParameter("@LimitID", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Cycle;
            parameters[2].Value = model.CycleUnit;
            parameters[3].Value = model.MaxTimes;
            parameters[4].Value = model.TargetId;
            parameters[5].Value = model.TargetType;
            parameters[6].Value = model.LimitID;

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
        public bool Delete(int LimitID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_PointsLimit ");
            strSql.Append(" where LimitID=@LimitID");
            SqlParameter[] parameters = {
					new SqlParameter("@LimitID", SqlDbType.Int,4)
			};
            parameters[0].Value = LimitID;

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
        public bool DeleteList(string LimitIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_PointsLimit ");
            strSql.Append(" where LimitID in (" + LimitIDlist + ")  ");
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
        public Maticsoft.Model.Members.PointsLimit GetModel(int LimitID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LimitID,Name,Cycle,CycleUnit,MaxTimes,TargetId,TargetType from Accounts_PointsLimit ");
            strSql.Append(" where LimitID=@LimitID");
            SqlParameter[] parameters = {
					new SqlParameter("@LimitID", SqlDbType.Int,4)
			};
            parameters[0].Value = LimitID;

            Maticsoft.Model.Members.PointsLimit model = new Maticsoft.Model.Members.PointsLimit();
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
        public Maticsoft.Model.Members.PointsLimit DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.PointsLimit model = new Maticsoft.Model.Members.PointsLimit();
            if (row != null)
            {
                if (row["LimitID"] != null && row["LimitID"].ToString() != "")
                {
                    model.LimitID = int.Parse(row["LimitID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Cycle"] != null && row["Cycle"].ToString() != "")
                {
                    model.Cycle = int.Parse(row["Cycle"].ToString());
                }
                if (row["CycleUnit"] != null)
                {
                    model.CycleUnit = row["CycleUnit"].ToString();
                }
                if (row["MaxTimes"] != null && row["MaxTimes"].ToString() != "")
                {
                    model.MaxTimes = int.Parse(row["MaxTimes"].ToString());
                }
                if (row["TargetId"] != null && row["TargetId"].ToString() != "")
                {
                    model.TargetId = int.Parse(row["TargetId"].ToString());
                }
                if (row["TargetType"] != null && row["TargetType"].ToString() != "")
                {
                    model.TargetType = int.Parse(row["TargetType"].ToString());
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
            strSql.Append("select LimitID,Name,Cycle,CycleUnit,MaxTimes,TargetId,TargetType ");
            strSql.Append(" FROM Accounts_PointsLimit ");
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
            strSql.Append(" LimitID,Name,Cycle,CycleUnit,MaxTimes,TargetId,TargetType ");
            strSql.Append(" FROM Accounts_PointsLimit ");
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
            strSql.Append("select count(1) FROM Accounts_PointsLimit ");
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
                strSql.Append("order by T.LimitID desc");
            }
            strSql.Append(")AS Row, T.*  from Accounts_PointsLimit T ");
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
            parameters[0].Value = "Accounts_PointsLimit";
            parameters[1].Value = "LimitID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        #region 扩展方法
        /// <summary>
        /// 删除条件限制（更新引用该条件限制的规则）
        /// </summary>
        public bool DeleteEX(int PointsLimitID)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //删除条件限制
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_PointsLimit ");
            strSql.Append(" where LimitID=@LimitID");
            SqlParameter[] parameters = {
					new SqlParameter("@LimitID", SqlDbType.Int,4)
			};
            parameters[0].Value = PointsLimitID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            //更新引用的积分规则
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("UPDATE Accounts_PointsRule SET LimitID=-1 WHERE LimitID=@LimitID");
            SqlParameter[] parameters2 = {
					new SqlParameter("@LimitID", SqlDbType.Int,4)
                                        };
            parameters2[0].Value = PointsLimitID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistsLimit(int limitid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_PointsRule");
            strSql.Append(" where LimitID=@LimitID");
            SqlParameter[] parameters = {
					new SqlParameter("@LimitID", SqlDbType.Int,4)
			};
            parameters[0].Value = limitid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该限制名称
        /// </summary>
        public bool ExistsName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_PointsLimit");
            strSql.Append(" where Name=@Name");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,255)
			};
            parameters[0].Value = name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion
    }
}

