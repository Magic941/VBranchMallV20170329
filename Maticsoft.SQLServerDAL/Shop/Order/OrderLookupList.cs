using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
	/// <summary>
	/// 数据访问类:OrderLookupList
	/// </summary>
	public partial class OrderLookupList:IOrderLookupList
	{
		public OrderLookupList()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("LookupListId", "Shop_OrderLookupList");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int LookupListId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrderLookupList");
            strSql.Append(" where LookupListId=@LookupListId");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4)
			};
            parameters[0].Value = LookupListId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Order.OrderLookupList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrderLookupList(");
            strSql.Append("Name,SelectMode,Description)");
            strSql.Append(" values (");
            strSql.Append("@Name,@SelectMode,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@SelectMode", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.SelectMode;
            parameters[2].Value = model.Description;

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
        public bool Update(Maticsoft.Model.Shop.Order.OrderLookupList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderLookupList set ");
            strSql.Append("Name=@Name,");
            strSql.Append("SelectMode=@SelectMode,");
            strSql.Append("Description=@Description");
            strSql.Append(" where LookupListId=@LookupListId");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@SelectMode", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@LookupListId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.SelectMode;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.LookupListId;

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
        public bool Delete(int LookupListId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderLookupList ");
            strSql.Append(" where LookupListId=@LookupListId");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4)
			};
            parameters[0].Value = LookupListId;

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
        public bool DeleteList(string LookupListIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderLookupList ");
            strSql.Append(" where LookupListId in (" + LookupListIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Order.OrderLookupList GetModel(int LookupListId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LookupListId,Name,SelectMode,Description from Shop_OrderLookupList ");
            strSql.Append(" where LookupListId=@LookupListId");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4)
			};
            parameters[0].Value = LookupListId;

            Maticsoft.Model.Shop.Order.OrderLookupList model = new Maticsoft.Model.Shop.Order.OrderLookupList();
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
        public Maticsoft.Model.Shop.Order.OrderLookupList DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderLookupList model = new Maticsoft.Model.Shop.Order.OrderLookupList();
            if (row != null)
            {
                if (row["LookupListId"] != null && row["LookupListId"].ToString() != "")
                {
                    model.LookupListId = int.Parse(row["LookupListId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["SelectMode"] != null && row["SelectMode"].ToString() != "")
                {
                    model.SelectMode = int.Parse(row["SelectMode"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
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
            strSql.Append("select LookupListId,Name,SelectMode,Description ");
            strSql.Append(" FROM Shop_OrderLookupList ");
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
            strSql.Append(" LookupListId,Name,SelectMode,Description ");
            strSql.Append(" FROM Shop_OrderLookupList ");
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
            strSql.Append("select count(1) FROM Shop_OrderLookupList ");
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
                strSql.Append("order by T.LookupListId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrderLookupList T ");
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
            parameters[0].Value = "Shop_OrderLookupList";
            parameters[1].Value = "LookupListId";
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

