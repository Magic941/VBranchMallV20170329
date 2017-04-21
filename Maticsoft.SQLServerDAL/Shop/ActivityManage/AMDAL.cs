using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.ActivityManage;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Shop.ActivityManage
{
    public partial class AMDAL : AMIDAL
    {
        public AMDAL()
        { }
        #region

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AMId", "Shop_ActivityManage");
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AMId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ActivityManage");
            strSql.Append(" where AMId=@AMId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4)
			};
            parameters[0].Value = AMId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.ActivityManage.AMModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ActivityManage(");
            strSql.Append("AMType,AMName,AMLabel,AMStartDate,AMEndDate,AMUnit,AMFreeShipment,AMApplyStyles,AMStatus,AMUserId,AMCreateDate )");
            strSql.Append(" values (");
            strSql.Append("@AMType,@AMName,@AMLabel,@AMStartDate,@AMEndDate,@AMUnit,@AMFreeShipment,@AMApplyStyles,@AMStatus,@AMUserId,getdate() )");
            //strSql.Append("@AMType,@AMName,@AMLabel,@AMStartDate,@AMEndDate,@AMUnit,@AMFreeShipment,@AMApplyStyles,@AMStatus,@AMUserId,@AMCreateDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AMType", SqlDbType.Int,4),
					new SqlParameter("@AMName", SqlDbType.NVarChar,100),
					new SqlParameter("@AMLabel", SqlDbType.NVarChar,50),
					new SqlParameter("@AMStartDate", SqlDbType.DateTime),
					new SqlParameter("@AMEndDate", SqlDbType.DateTime),
					new SqlParameter("@AMUnit", SqlDbType.Int,4),
                    new SqlParameter("@AMFreeShipment", SqlDbType.Int,4),
                    new SqlParameter("@AMApplyStyles", SqlDbType.Int,4),
                    new SqlParameter("@AMStatus", SqlDbType.Int,4),
                    new SqlParameter("@AMUserId", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.AMType;
            parameters[1].Value = model.AMName;
            parameters[2].Value = model.AMLabel;
            parameters[3].Value = model.AMStartDate.ToString();
            parameters[4].Value = model.AMEndDate.ToString();
            parameters[5].Value = model.AMUnit;
            parameters[6].Value = model.AMFreeShipment;
            parameters[7].Value = model.AMApplyStyles;
            parameters[8].Value = model.AMStatus;
            parameters[9].Value = model.AMUserId;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AMId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManage ");
            strSql.Append(" where AMId=@AMId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4)
			};
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

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string AMIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManage ");
            strSql.Append(" where AMId in (" + AMIdlist + ")  ");
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
        public bool Update(Maticsoft.Model.Shop.ActivityManage.AMModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ActivityManage set ");
            strSql.Append("AMType=@AMType,");
            strSql.Append("AMName=@AMName,");
            strSql.Append("AMLabel=@AMLabel,");
            strSql.Append("AMStartDate=@AMStartDate,");
            strSql.Append("AMEndDate=@AMEndDate,");
            strSql.Append("AMUnit=@AMUnit,");
            strSql.Append("AMFreeShipment=@AMFreeShipment,");
            strSql.Append("AMApplyStyles=@AMApplyStyles,");
            strSql.Append("AMStatus=@AMStatus");
            strSql.Append(" where AMId=@AMId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMType",SqlDbType.Int,4 ),
					new SqlParameter("@AMName",SqlDbType.NVarChar,100),
					new SqlParameter("@AMLabel",SqlDbType.NVarChar,50),
					new SqlParameter("@AMStartDate",DateTime.Now),
					new SqlParameter("@AMEndDate", DateTime.Now),
					new SqlParameter("@AMUnit", SqlDbType.Int,4),
					new SqlParameter("@AMFreeShipment", SqlDbType.Int,4),
                    new SqlParameter("@AMApplyStyles", SqlDbType.Int,4),
                    new SqlParameter("@AMStatus", SqlDbType.Int,4),
                    new SqlParameter("@AMUserId", SqlDbType.NVarChar,50),
                    new SqlParameter("@AMId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.AMType;
            parameters[1].Value = model.AMName;
            parameters[2].Value = model.AMLabel;
            parameters[3].Value = model.AMStartDate.ToString();
            parameters[4].Value = model.AMEndDate.ToString();
            parameters[5].Value = model.AMUnit;
            parameters[6].Value = model.AMFreeShipment;
            parameters[7].Value = model.AMApplyStyles;
            parameters[8].Value = model.AMStatus;
            parameters[9].Value = model.AMUserId;
            parameters[10].Value = model.AMId;

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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.ActivityManage.AMModel GetModel(int AMId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AMId,AMType,AMName,AMLabel,AMStartDate,AMEndDate,AMUnit,AMFreeShipment,AMApplyStyles,AMStatus,AMUserId,AMCreateDate from Shop_ActivityManage ");
            strSql.Append(" where AMId=@AMId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4)
			};
            parameters[0].Value = AMId;

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
        /// 获取全场满减满折信息
        /// </summary>
        /// <param name="type">活动类型 0：商家 1：全场</param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.ActivityManage.AMModel GetAllActivity(int type)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat("SELECT * FROM dbo.Shop_ActivityManage WHERE AMApplyStyles={0} AND AMStatus=0",type);
            DataSet ds = DbHelperSQL.Query(stb.ToString());
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
        public Maticsoft.Model.Shop.ActivityManage.AMModel DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.ActivityManage.AMModel model = new Model.Shop.ActivityManage.AMModel();
            if (row != null)
            {
                if (row["AMId"] != null && row["AMId"].ToString() != "")
                {
                    model.AMId = int.Parse(row["AMId"].ToString());
                }
                if (row["AMType"] != null)
                {
                    model.AMType = int.Parse(row["AMType"].ToString());
                }
                if (row["AMName"] != null && row["AMName"].ToString() != "")
                {
                    model.AMName = row["AMName"].ToString();
                }
                if (row["AMLabel"] != null && row["AMLabel"].ToString() != "")
                {
                    model.AMLabel = row["AMLabel"].ToString();
                }
                if (row["AMStartDate"] != null && row["AMStartDate"].ToString() != "")
                {
                    model.AMStartDate = DateTime.Parse(row["AMStartDate"].ToString());
                }
                if (row["AMEndDate"] != null && row["AMEndDate"].ToString() != "")
                {
                    model.AMEndDate = DateTime.Parse(row["AMEndDate"].ToString());
                }
                if (row["AMUnit"] != null)
                {
                    model.AMUnit = int.Parse(row["AMUnit"].ToString());
                }
                if (row["AMFreeShipment"] != null)
                {
                    model.AMFreeShipment = int.Parse(row["AMFreeShipment"].ToString());
                }
                if (row["AMApplyStyles"] != null)
                {
                    model.AMApplyStyles = int.Parse(row["AMApplyStyles"].ToString());
                }
                if (row["AMStatus"] != null)
                {
                    model.AMStatus = int.Parse(row["AMStatus"].ToString());
                }

                if (row["AMUserId"] != null && row["AMUserId"].ToString() != "")
                {
                    model.AMUserId = row["AMUserId"].ToString();
                }
                if (row["AMCreateDate"] != null && row["AMCreateDate"].ToString() != "")
                {
                    model.AMUserId = row["AMCreateDate"].ToString();
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
            strSql.Append("select AMId,AMType,AMName,AMLabel,AMStartDate,AMEndDate,AMUnit,AMFreeShipment,AMApplyStyles,AMStatus,AMUserId,AMCreateDate ");
            strSql.Append(" FROM Shop_ActivityManage ");
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
            strSql.Append("AMId,AMType,AMName,AMLabel,AMStartDate,AMEndDate,AMUnit,AMFreeShipment,AMApplyStyles,AMStatus,AMUserId,AMCreateDate ");
            strSql.Append(" FROM Shop_ActivityManage ");
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
            strSql.Append("select count(1) FROM Shop_ActivityManage ");
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
                strSql.Append("order by T.AMId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ActivityManage T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool DeleteEx(int AMId)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //删除活动表数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Shop_ActivityManage ");
            strSql.Append(" where AMId=@AMId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4)};
            parameters[0].Value = AMId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //删除规则项表数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete Shop_ActivityManageDetail ");
            strSql2.Append(" where AMId=@AMId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@AMId", SqlDbType.Int,4)};
            parameters2[0].Value = AMId;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //如果该活动已经有添加商品，则把对应的商品进行删除
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete Shop_ActivityManageProduct ");
            strSql3.Append("where AMId=@AMId ");
            SqlParameter[] parameter3 = {
                    new SqlParameter("@AMId",SqlDbType.Int,4)};
            parameter3[0].Value = AMId;
            cmd = new CommandInfo(strSql3.ToString(), parameter3);
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


        public bool DeleteListEx(string AMIdlist)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //删除规则表数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManage ");
            strSql.Append(" where AMId in (" + AMIdlist + ") ");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4)};
            parameters[0].Value = 1;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //删除规则项表数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete Shop_ActivityManageDetail ");
            strSql2.Append(" where AMId in (" + AMIdlist + ") ");
            cmd = new CommandInfo(strSql2.ToString(), parameters);
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

        //public bool UpdStatusByDate(DateTime AMStartDate, DateTime AMEndDate)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("update Shop_ActivityManage set AMStatus=1 where AMEndDate<GetDate() AND AMStartDate>GetDate()");

        //    return true ;
        //}

        public bool UpdateStatus(int AMId, int AMStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ActivityManage set ");
            strSql.Append("AMStatus=@AMStatus");
            strSql.Append(" where AMId=@AMId");
            SqlParameter[] parameters = {
					new SqlParameter("@AMStatus", SqlDbType.Int,4),
					new SqlParameter("@AMId", SqlDbType.Int,4)};
            parameters[0].Value = AMStatus;
            parameters[1].Value = AMId;

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
