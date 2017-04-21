using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.ActivityManage;
using Maticsoft.DBUtility;

namespace Maticsoft.SQLServerDAL.Shop.ActivityManage
{
    public partial class AMDetailDAL:AMDetailIDAL
    {
        public AMDetailDAL(){ }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AMDId", "Shop_ActivityManageDetail");
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AMDId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ActivityManageDetail");
            strSql.Append(" where AMDId=@AMDId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMDId", SqlDbType.Int,4)
			};
            parameters[0].Value = AMDId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.ActivityManage.AMDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ActivityManageDetail(");
            strSql.Append("AMId,AMDUnitValue,AMDRateValue,AMDType)");
            strSql.Append(" values (");
            strSql.Append("@AMId,@AMDUnitValue,@AMDRateValue,@AMDType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4),
					new SqlParameter("@AMDUnitValue", SqlDbType.Int,4),
					new SqlParameter("@AMDRateValue", SqlDbType.Int,4),
					new SqlParameter("@AMDType", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.AMId;
            parameters[1].Value = model.AMDUnitValue;
            parameters[2].Value = model.AMDRateValue;
            parameters[3].Value = model.AMDType;

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
        public bool Update(Maticsoft.Model.Shop.ActivityManage.AMDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ActivityManageDetail set ");
            strSql.Append("AMId=@AMId,");
            strSql.Append("AMDType=@AMDType,");
            strSql.Append("AMDUnitValue=@AMDUnitValue,");
            strSql.Append("AMDRateValue=@AMDRateValue");
            strSql.Append(" where AMDId=@AMDId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4),
					new SqlParameter("@ItemType", SqlDbType.NVarChar,50),
					new SqlParameter("@AMDUnitValue", SqlDbType.Decimal,6),
					new SqlParameter("@AMDRateValue", SqlDbType.Decimal,6),
					new SqlParameter("@AMDId", SqlDbType.Int,4)};
            parameters[0].Value = model.AMId;
            parameters[1].Value = model.AMDType;
            parameters[2].Value = model.AMDUnitValue;
            parameters[3].Value = model.AMDRateValue;
            parameters[4].Value = model.AMDId;

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
        public bool Delete(int AMDId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManageDetail ");
            strSql.Append(" where AMDId=@AMDId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMDId", SqlDbType.Int,4)
			};
            parameters[0].Value = AMDId;

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
        public bool DeleteList(string AMDIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManageDetail ");
            strSql.Append(" where AMDId in (" + AMDIdlist + ")  ");
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
        public Maticsoft.Model.Shop.ActivityManage.AMDetailModel GetModel(int AMDId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AMDId,AMId,AMDType ,AMDUnitValue,AMDRateValue from Shop_ActivityManageDetail ");
            strSql.Append(" where AMDId=@AMDId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMDId", SqlDbType.Int,4)
			};
            parameters[0].Value = AMDId;

            Maticsoft.Model.Shop.ActivityManage.AMModel model = new Model.Shop.ActivityManage.AMModel();
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
        public Maticsoft.Model.Shop.ActivityManage.AMDetailModel DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.ActivityManage.AMDetailModel model = new Model.Shop.ActivityManage.AMDetailModel();
            if (row != null)
            {
                if (row["AMDId"] != null && row["AMDId"].ToString() != "")
                {
                    model.AMDId = int.Parse(row["AMDId"].ToString());
                }
                if (row["AMId"] != null && row["AMId"].ToString() != "")
                {
                    model.AMId = int.Parse(row["AMId"].ToString());
                }
                if (row["AMDType"] != null && row["AMDType"].ToString() != "")
                {
                    model.AMDType = row["AMDType"].ToString();
                }
                if (row["AMDUnitValue"] != null && row["AMDUnitValue"].ToString() != "")
                {
                    model.AMDUnitValue = Convert.ToDecimal(row["AMDUnitValue"].ToString());
                }
                if (row["AMDRateValue"] != null && row["AMDRateValue"].ToString() != "")
                {
                    model.AMDRateValue = Convert.ToDecimal(row["AMDRateValue"].ToString());
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
            strSql.Append("select AMDId,AMId,AMDType ,AMDUnitValue,AMDRateValue ");
            strSql.Append(" FROM Shop_ActivityManageDetail ");
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
            strSql.Append(" AMDId,AMId,AMDType ,AMDUnitValue,AMDRateValue  ");
            strSql.Append(" FROM Shop_ActivityManageDetail ");
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
            strSql.Append("select count(1) FROM Shop_ActivityManageDetail ");
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
                strSql.Append("order by T.AMDId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ActivityManageDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }






        /// <summary>
        ///  根据 活动ID删除
        /// </summary>
        /// <param name="AMId"></param>
        /// <returns></returns>
        public bool DeleteByAMId(int AMId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManageDetail ");
            strSql.Append(" where AMId=@AMId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4)			};
            parameters[0].Value = AMId;

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
        #endregion
    }
}
