using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Gift;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Shop.Gift
{
	/// <summary>
	/// 数据访问类:ExchangeDetail
	/// </summary>
	public partial class ExchangeDetail:IExchangeDetail
	{
		public ExchangeDetail()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DetailID", "Shop_ExchangeDetail");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DetailID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ExchangeDetail");
            strSql.Append(" where DetailID=@DetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.Int,4)
			};
            parameters[0].Value = DetailID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ExchangeDetail(");
            strSql.Append("Type,GiftID,UserID,OrderID,GiftName,Price,CouponCode,CostScore,Status,Description,CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@Type,@GiftID,@UserID,@OrderID,@GiftName,@Price,@CouponCode,@CostScore,@Status,@Description,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@GiftID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@GiftName", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.Money,8),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
					new SqlParameter("@CostScore", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Type;
            parameters[1].Value = model.GiftID;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.OrderID;
            parameters[4].Value = model.GiftName;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.CouponCode;
            parameters[7].Value = model.CostScore;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.CreatedDate;

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
        public bool Update(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ExchangeDetail set ");
            strSql.Append("Type=@Type,");
            strSql.Append("GiftID=@GiftID,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("OrderID=@OrderID,");
            strSql.Append("GiftName=@GiftName,");
            strSql.Append("Price=@Price,");
            strSql.Append("CouponCode=@CouponCode,");
            strSql.Append("CostScore=@CostScore,");
            strSql.Append("Status=@Status,");
            strSql.Append("Description=@Description,");
            strSql.Append("CreatedDate=@CreatedDate");
            strSql.Append(" where DetailID=@DetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@GiftID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@GiftName", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.Money,8),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
					new SqlParameter("@CostScore", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@DetailID", SqlDbType.Int,4)};
            parameters[0].Value = model.Type;
            parameters[1].Value = model.GiftID;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.OrderID;
            parameters[4].Value = model.GiftName;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.CouponCode;
            parameters[7].Value = model.CostScore;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.CreatedDate;
            parameters[11].Value = model.DetailID;

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
        public bool Delete(int DetailID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ExchangeDetail ");
            strSql.Append(" where DetailID=@DetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.Int,4)
			};
            parameters[0].Value = DetailID;

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
        public bool DeleteList(string DetailIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ExchangeDetail ");
            strSql.Append(" where DetailID in (" + DetailIDlist + ")  ");
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
        public Maticsoft.Model.Shop.Gift.ExchangeDetail GetModel(int DetailID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 DetailID,Type,GiftID,UserID,OrderID,GiftName,Price,CouponCode,CostScore,Status,Description,CreatedDate from Shop_ExchangeDetail ");
            strSql.Append(" where DetailID=@DetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.Int,4)
			};
            parameters[0].Value = DetailID;

            Maticsoft.Model.Shop.Gift.ExchangeDetail model = new Maticsoft.Model.Shop.Gift.ExchangeDetail();
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
        public Maticsoft.Model.Shop.Gift.ExchangeDetail DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Gift.ExchangeDetail model = new Maticsoft.Model.Shop.Gift.ExchangeDetail();
            if (row != null)
            {
                if (row["DetailID"] != null && row["DetailID"].ToString() != "")
                {
                    model.DetailID = int.Parse(row["DetailID"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["GiftID"] != null && row["GiftID"].ToString() != "")
                {
                    model.GiftID = int.Parse(row["GiftID"].ToString());
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["OrderID"] != null && row["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(row["OrderID"].ToString());
                }
                if (row["GiftName"] != null)
                {
                    model.GiftName = row["GiftName"].ToString();
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["CouponCode"] != null)
                {
                    model.CouponCode = row["CouponCode"].ToString();
                }
                if (row["CostScore"] != null && row["CostScore"].ToString() != "")
                {
                    model.CostScore = int.Parse(row["CostScore"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
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
            strSql.Append("select DetailID,Type,GiftID,UserID,OrderID,GiftName,Price,CouponCode,CostScore,Status,Description,CreatedDate ");
            strSql.Append(" FROM Shop_ExchangeDetail ");
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
            strSql.Append(" DetailID,Type,GiftID,UserID,OrderID,GiftName,Price,CouponCode,CostScore,Status,Description,CreatedDate ");
            strSql.Append(" FROM Shop_ExchangeDetail ");
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
            strSql.Append("select count(1) FROM Shop_ExchangeDetail ");
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
                strSql.Append("order by T.DetailID desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ExchangeDetail T ");
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
            parameters[0].Value = "Shop_ExchangeDetail";
            parameters[1].Value = "DetailID";
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
        /// 设置状态
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool SetStatus(int detailId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ExchangeDetail set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where DetailID=@DetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@DetailID", SqlDbType.Int,4)};
            parameters[0].Value = status;
            parameters[1].Value = detailId;

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
        /// 批量设置状态
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool SetStatusList(string  detailIds, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ExchangeDetail set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where DetailID in (" + detailIds + ")");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = status;

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

