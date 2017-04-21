using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
	/// <summary>
	/// 数据访问类:OrderRemark
	/// </summary>
	public partial class OrderRemark:IOrderRemark
	{
		public OrderRemark()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long RemarkId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrderRemark");
            strSql.Append(" where RemarkId=@RemarkId");
            SqlParameter[] parameters = {
					new SqlParameter("@RemarkId", SqlDbType.BigInt)
			};
            parameters[0].Value = RemarkId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderRemark model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrderRemark(");
            strSql.Append("OrderId,OrderCode,UserId,UserName,Remark,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@OrderCode,@UserId,@UserName,@Remark,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.UserId;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatedDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrderRemark model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderRemark set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatedDate=@CreatedDate");
            strSql.Append(" where RemarkId=@RemarkId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@RemarkId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.UserId;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatedDate;
            parameters[6].Value = model.RemarkId;

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
        public bool Delete(long RemarkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderRemark ");
            strSql.Append(" where RemarkId=@RemarkId");
            SqlParameter[] parameters = {
					new SqlParameter("@RemarkId", SqlDbType.BigInt)
			};
            parameters[0].Value = RemarkId;

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
        public bool DeleteList(string RemarkIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderRemark ");
            strSql.Append(" where RemarkId in (" + RemarkIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Order.OrderRemark GetModel(long RemarkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RemarkId,OrderId,OrderCode,UserId,UserName,Remark,CreatedDate from Shop_OrderRemark ");
            strSql.Append(" where RemarkId=@RemarkId");
            SqlParameter[] parameters = {
					new SqlParameter("@RemarkId", SqlDbType.BigInt)
			};
            parameters[0].Value = RemarkId;

            Maticsoft.Model.Shop.Order.OrderRemark model = new Maticsoft.Model.Shop.Order.OrderRemark();
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
        public Maticsoft.Model.Shop.Order.OrderRemark DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderRemark model = new Maticsoft.Model.Shop.Order.OrderRemark();
            if (row != null)
            {
                if (row["RemarkId"] != null && row["RemarkId"].ToString() != "")
                {
                    model.RemarkId = long.Parse(row["RemarkId"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
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
            strSql.Append("select RemarkId,OrderId,OrderCode,UserId,UserName,Remark,CreatedDate ");
            strSql.Append(" FROM Shop_OrderRemark ");
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
            strSql.Append(" RemarkId,OrderId,OrderCode,UserId,UserName,Remark,CreatedDate ");
            strSql.Append(" FROM Shop_OrderRemark ");
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
            strSql.Append("select count(1) FROM Shop_OrderRemark ");
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
                strSql.Append("order by T.RemarkId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrderRemark T ");
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
            parameters[0].Value = "Shop_OrderRemark";
            parameters[1].Value = "RemarkId";
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

