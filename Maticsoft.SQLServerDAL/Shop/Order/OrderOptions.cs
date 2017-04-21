using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
	/// <summary>
	/// 数据访问类:OrderOptions
	/// </summary>
	public partial class OrderOptions:IOrderOptions
	{
		public OrderOptions()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("LookupListId", "Shop_OrderOptions");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int LookupListId, int LookupItemId, long OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrderOptions");
            strSql.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4),
					new SqlParameter("@LookupItemId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)			};
            parameters[0].Value = LookupListId;
            parameters[1].Value = LookupItemId;
            parameters[2].Value = OrderId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Order.OrderOptions model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrderOptions(");
            strSql.Append("LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription)");
            strSql.Append(" values (");
            strSql.Append("@LookupListId,@LookupItemId,@OrderId,@OrderCode,@ListDescription,@ItemDescription,@AdjustedPrice,@CustomerTitle,@CustomerDescription)");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4),
					new SqlParameter("@LookupItemId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ListDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@ItemDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@CustomerTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerDescription", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.LookupListId;
            parameters[1].Value = model.LookupItemId;
            parameters[2].Value = model.OrderId;
            parameters[3].Value = model.OrderCode;
            parameters[4].Value = model.ListDescription;
            parameters[5].Value = model.ItemDescription;
            parameters[6].Value = model.AdjustedPrice;
            parameters[7].Value = model.CustomerTitle;
            parameters[8].Value = model.CustomerDescription;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrderOptions model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderOptions set ");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("ListDescription=@ListDescription,");
            strSql.Append("ItemDescription=@ItemDescription,");
            strSql.Append("AdjustedPrice=@AdjustedPrice,");
            strSql.Append("CustomerTitle=@CustomerTitle,");
            strSql.Append("CustomerDescription=@CustomerDescription");
            strSql.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ListDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@ItemDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@CustomerTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@LookupListId", SqlDbType.Int,4),
					new SqlParameter("@LookupItemId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.OrderCode;
            parameters[1].Value = model.ListDescription;
            parameters[2].Value = model.ItemDescription;
            parameters[3].Value = model.AdjustedPrice;
            parameters[4].Value = model.CustomerTitle;
            parameters[5].Value = model.CustomerDescription;
            parameters[6].Value = model.LookupListId;
            parameters[7].Value = model.LookupItemId;
            parameters[8].Value = model.OrderId;

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
        public bool Delete(int LookupListId, int LookupItemId, long OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderOptions ");
            strSql.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4),
					new SqlParameter("@LookupItemId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)			};
            parameters[0].Value = LookupListId;
            parameters[1].Value = LookupItemId;
            parameters[2].Value = OrderId;

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
        public Maticsoft.Model.Shop.Order.OrderOptions GetModel(int LookupListId, int LookupItemId, long OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription from Shop_OrderOptions ");
            strSql.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4),
					new SqlParameter("@LookupItemId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)			};
            parameters[0].Value = LookupListId;
            parameters[1].Value = LookupItemId;
            parameters[2].Value = OrderId;

            Maticsoft.Model.Shop.Order.OrderOptions model = new Maticsoft.Model.Shop.Order.OrderOptions();
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
        public Maticsoft.Model.Shop.Order.OrderOptions DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderOptions model = new Maticsoft.Model.Shop.Order.OrderOptions();
            if (row != null)
            {
                if (row["LookupListId"] != null && row["LookupListId"].ToString() != "")
                {
                    model.LookupListId = int.Parse(row["LookupListId"].ToString());
                }
                if (row["LookupItemId"] != null && row["LookupItemId"].ToString() != "")
                {
                    model.LookupItemId = int.Parse(row["LookupItemId"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["ListDescription"] != null)
                {
                    model.ListDescription = row["ListDescription"].ToString();
                }
                if (row["ItemDescription"] != null)
                {
                    model.ItemDescription = row["ItemDescription"].ToString();
                }
                if (row["AdjustedPrice"] != null && row["AdjustedPrice"].ToString() != "")
                {
                    model.AdjustedPrice = decimal.Parse(row["AdjustedPrice"].ToString());
                }
                if (row["CustomerTitle"] != null)
                {
                    model.CustomerTitle = row["CustomerTitle"].ToString();
                }
                if (row["CustomerDescription"] != null)
                {
                    model.CustomerDescription = row["CustomerDescription"].ToString();
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
            strSql.Append("select LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription ");
            strSql.Append(" FROM Shop_OrderOptions ");
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
            strSql.Append(" LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription ");
            strSql.Append(" FROM Shop_OrderOptions ");
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
            strSql.Append("select count(1) FROM Shop_OrderOptions ");
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
                strSql.Append("order by T.OrderId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrderOptions T ");
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
            parameters[0].Value = "Shop_OrderOptions";
            parameters[1].Value = "OrderId";
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

