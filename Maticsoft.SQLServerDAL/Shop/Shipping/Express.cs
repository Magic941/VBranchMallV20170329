using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Shipping;

namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    public class Express:IExpress
    {
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * FROM Shop_Express  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" Order by  " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Shipping.Shop_Express model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Express set ");
            strSql.Append("FromAddress=@FromAddress,");
            strSql.Append("ToAddress=@ToAddress,");
            strSql.Append("EType=@EType,");
            strSql.Append("EName=@EName,");
            strSql.Append("ExpressContent=@ExpressContent,");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("State=@State,");
            strSql.Append("ResultV2=@ResultV2,");
            strSql.Append("IsCheck=@IsCheck,");
            strSql.Append("Salt=@Salt,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("UseSign=@UseSign");
            strSql.Append(" where ExpressCode=@ExpressCode");
            SqlParameter[] parameters = {
					new SqlParameter("@FromAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@ToAddress", SqlDbType.NVarChar,50),
                    new SqlParameter("@EType", SqlDbType.NVarChar,50),
                    new SqlParameter("@EName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ExpressContent", SqlDbType.NVarChar),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@State", SqlDbType.NVarChar,2),
                    new SqlParameter("@ResultV2", SqlDbType.NVarChar,2),
                    new SqlParameter("@IsCheck", SqlDbType.NVarChar,2),
                    new SqlParameter("@Salt", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@UseSign", SqlDbType.NVarChar,2),
                    new SqlParameter("@ExpressCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.FromAddress;
            parameters[1].Value = model.ToAddress;
            parameters[2].Value = model.EType;
            parameters[3].Value = model.EName;
            parameters[4].Value = model.ExpressContent;
            parameters[5].Value = model.OrderCode;
            parameters[6].Value = model.State;
            parameters[7].Value = model.ResultV2;
            parameters[8].Value = model.IsCheck;
            parameters[9].Value = model.Salt;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.AddTime;
            parameters[12].Value = model.UseSign;
            parameters[13].Value = model.ExpressCode;

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
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Shipping.Shop_Express model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Express(");
            strSql.Append("ExpressCode,FromAddress,ToAddress,EType,EName,ExpressContent,OrderCode,State,ResultV2,IsCheck,Salt,UpdateTime,AddTime,UseSign)");
            strSql.Append(" values (");
            strSql.Append("@ExpressCode,@FromAddress,@ToAddress,@EType,@EName,@ExpressContent,@OrderCode,@State,@ResultV2,@IsCheck,@Salt,@UpdateTime,@AddTime,@UseSign)");
            SqlParameter[] parameters = {
					new SqlParameter("@FromAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@ToAddress", SqlDbType.NVarChar,50),
                    new SqlParameter("@EType", SqlDbType.NVarChar,50),
                    new SqlParameter("@EName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ExpressContent", SqlDbType.NVarChar),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@State", SqlDbType.NVarChar,2),
                    new SqlParameter("@ResultV2", SqlDbType.NVarChar,2),
                    new SqlParameter("@IsCheck", SqlDbType.NVarChar,2),
                    new SqlParameter("@Salt", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@UseSign", SqlDbType.NVarChar,2),
                    new SqlParameter("@ExpressCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.FromAddress;
            parameters[1].Value = model.ToAddress;
            parameters[2].Value = model.EType;
            parameters[3].Value = model.EName;
            parameters[4].Value = model.ExpressContent;
            parameters[5].Value = model.OrderCode;
            parameters[6].Value = model.State;
            parameters[7].Value = model.ResultV2;
            parameters[8].Value = model.IsCheck;
            parameters[9].Value = model.Salt;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.AddTime;
            parameters[12].Value = model.UseSign;
            parameters[13].Value = model.ExpressCode;

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
        /// 获取对象列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="filedOrder">排序</param>
        /// <param name="type">转换类型，0：用于前台显示，1：用于后台数据处理</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Shipping.Shop_Express> GetListModel(string strWhere,string filedOrder,int type)
        {
            return DataTableToList(GetList(strWhere, filedOrder).Tables[0], type);
        }

        /// <summary>
        /// Table转化为对象列表
        /// </summary>
        /// <param name="table">表对象</param>
        /// <param name="type">转换类型，0：用于前台显示，1：用于后台数据处理</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Shipping.Shop_Express> DataTableToList(DataTable table, int type)
        {
            List<Maticsoft.Model.Shop.Shipping.Shop_Express> list = new List<Model.Shop.Shipping.Shop_Express>();
            foreach (DataRow row in table.Rows)
            {
                if (type == 0)
                {
                    if (row["ExpressContent"] != null && !string.IsNullOrWhiteSpace(row["ExpressContent"].ToString()))
                    {
                        Maticsoft.Model.Shop.Shipping.Shop_Express model = new Model.Shop.Shipping.Shop_Express();
                        model.ExpressCode = row["ExpressCode"].ToString();
                        model.FromAddress = row["FromAddress"].ToString();
                        model.ToAddress = row["ToAddress"].ToString();
                        model.EType = row["EType"].ToString();
                        model.EName = row["EName"].ToString();
                        model.ExpressContent = row["ExpressContent"].ToString();
                        model.OrderCode = row["OrderCode"].ToString();
                        model.State = row["State"].ToString();
                        model.ResultV2 = row["ResultV2"].ToString();
                        model.IsCheck = row["IsCheck"].ToString();
                        model.Salt = row["Salt"].ToString();
                        model.UpdateTime = Convert.ToDateTime(row["UpdateTime"].ToString());
                        model.AddTime = Convert.ToDateTime(row["AddTime"].ToString());
                        model.UseSign = row["UseSign"].ToString();
                        list.Add(model);
                    }
                }
                else
                {
                    Maticsoft.Model.Shop.Shipping.Shop_Express model = new Model.Shop.Shipping.Shop_Express();
                    model.ExpressCode = row["ExpressCode"].ToString();
                    model.FromAddress = row["FromAddress"].ToString();
                    model.ToAddress = row["ToAddress"].ToString();
                    model.EType = row["EType"].ToString();
                    model.EName = row["EName"].ToString();
                    model.ExpressContent = row["ExpressContent"].ToString();
                    model.OrderCode = row["OrderCode"].ToString();
                    model.State = row["State"].ToString();
                    model.ResultV2 = row["ResultV2"].ToString();
                    model.IsCheck = row["IsCheck"].ToString();
                    model.Salt = row["Salt"].ToString();
                    model.UpdateTime = Convert.ToDateTime(row["UpdateTime"].ToString());
                    model.AddTime = Convert.ToDateTime(row["AddTime"].ToString());
                    model.UseSign = row["UseSign"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
