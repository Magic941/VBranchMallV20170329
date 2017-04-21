/**  版本信息模板在安装目录下，可自行修改。
* Shop_PaymentNumber.cs
*
* 功 能： N/A
* 类 名： Shop_PaymentNumber
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/8/5 15:08:00   N/A    初版
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
using Maticsoft.IDAL;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.PaymentNumber;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.PaymentNumber
{
    /// <summary>
    /// 数据访问类:Shop_PaymentNumber
    /// </summary>
    public partial class Shop_PaymentNumber : IShop_PaymentNumber
    {
        public Shop_PaymentNumber()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "Shop_PaymentNumber");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_PaymentNumber");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_PaymentNumber(");
            strSql.Append("OrderId,OrderCode,ParentOrderId,SwiftNumber)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@OrderCode,@ParentOrderId,@SwiftNumber)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentOrderId", SqlDbType.BigInt,8),
					new SqlParameter("@SwiftNumber", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.ParentOrderId;
            parameters[3].Value = model.SwiftNumber;

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
        /// 返回一条insert语句
        /// </summary>
        /// <returns></returns>
        public string InsertStr(Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber model)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("insert into Shop_PaymentNumber(OrderId,OrderCode,ParentOrderId,SwiftNumber) values(");
            stb.Append("{0},'{1}',{2},{3})");
            return stb.ToString();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_PaymentNumber set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("ParentOrderId=@ParentOrderId,");
            strSql.Append("SwiftNumber=@SwiftNumber");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentOrderId", SqlDbType.BigInt,8),
					new SqlParameter("@SwiftNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.ParentOrderId;
            parameters[3].Value = model.SwiftNumber;
            parameters[4].Value = model.id;

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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_PaymentNumber ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_PaymentNumber ");
            strSql.Append(" where id in (" + idlist + ")  ");
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
        public Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,OrderId,OrderCode,ParentOrderId,SwiftNumber from Shop_PaymentNumber ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber model = new Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber();
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
        public Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber model = new Maticsoft.Model.Shop.PaymentNumber.Shop_PaymentNumber();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["ParentOrderId"] != null && row["ParentOrderId"].ToString() != "")
                {
                    model.ParentOrderId = long.Parse(row["ParentOrderId"].ToString());
                }
                if (row["SwiftNumber"] != null)
                {
                    model.SwiftNumber = row["SwiftNumber"].ToString();
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
            strSql.Append("select id,OrderId,OrderCode,ParentOrderId,SwiftNumber ");
            strSql.Append(" FROM Shop_PaymentNumber ");
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
            strSql.Append(" id,OrderId,OrderCode,ParentOrderId,SwiftNumber ");
            strSql.Append(" FROM Shop_PaymentNumber ");
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
            strSql.Append("select count(1) FROM Shop_PaymentNumber ");
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
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_PaymentNumber T ");
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
            parameters[0].Value = "Shop_PaymentNumber";
            parameters[1].Value = "id";
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

