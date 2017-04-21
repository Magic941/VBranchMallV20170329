using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
	/// <summary>
	/// 数据访问类:OrderLookupItems
	/// </summary>
	public partial class OrderLookupItems:IOrderLookupItems
	{
		public OrderLookupItems()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("LookupItemId", "Shop_OrderLookupItems");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int LookupItemId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrderLookupItems");
            strSql.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupItemId", SqlDbType.Int,4)
			};
            parameters[0].Value = LookupItemId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Order.OrderLookupItems model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrderLookupItems(");
            strSql.Append("LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark)");
            strSql.Append(" values (");
            strSql.Append("@LookupListId,@Name,@IsInputRequired,@InputTitle,@AppendMoney,@CalculateMode,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@IsInputRequired", SqlDbType.Bit,1),
					new SqlParameter("@InputTitle", SqlDbType.NVarChar,20),
					new SqlParameter("@AppendMoney", SqlDbType.Money,8),
					new SqlParameter("@CalculateMode", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.LookupListId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.IsInputRequired;
            parameters[3].Value = model.InputTitle;
            parameters[4].Value = model.AppendMoney;
            parameters[5].Value = model.CalculateMode;
            parameters[6].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.Shop.Order.OrderLookupItems model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderLookupItems set ");
            strSql.Append("LookupListId=@LookupListId,");
            strSql.Append("Name=@Name,");
            strSql.Append("IsInputRequired=@IsInputRequired,");
            strSql.Append("InputTitle=@InputTitle,");
            strSql.Append("AppendMoney=@AppendMoney,");
            strSql.Append("CalculateMode=@CalculateMode,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupListId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@IsInputRequired", SqlDbType.Bit,1),
					new SqlParameter("@InputTitle", SqlDbType.NVarChar,20),
					new SqlParameter("@AppendMoney", SqlDbType.Money,8),
					new SqlParameter("@CalculateMode", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@LookupItemId", SqlDbType.Int,4)};
            parameters[0].Value = model.LookupListId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.IsInputRequired;
            parameters[3].Value = model.InputTitle;
            parameters[4].Value = model.AppendMoney;
            parameters[5].Value = model.CalculateMode;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.LookupItemId;

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
        public bool Delete(int LookupItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderLookupItems ");
            strSql.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupItemId", SqlDbType.Int,4)
			};
            parameters[0].Value = LookupItemId;

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
        public bool DeleteList(string LookupItemIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderLookupItems ");
            strSql.Append(" where LookupItemId in (" + LookupItemIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Order.OrderLookupItems GetModel(int LookupItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LookupItemId,LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark from Shop_OrderLookupItems ");
            strSql.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@LookupItemId", SqlDbType.Int,4)
			};
            parameters[0].Value = LookupItemId;

            Maticsoft.Model.Shop.Order.OrderLookupItems model = new Maticsoft.Model.Shop.Order.OrderLookupItems();
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
        public Maticsoft.Model.Shop.Order.OrderLookupItems DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderLookupItems model = new Maticsoft.Model.Shop.Order.OrderLookupItems();
            if (row != null)
            {
                if (row["LookupItemId"] != null && row["LookupItemId"].ToString() != "")
                {
                    model.LookupItemId = int.Parse(row["LookupItemId"].ToString());
                }
                if (row["LookupListId"] != null && row["LookupListId"].ToString() != "")
                {
                    model.LookupListId = int.Parse(row["LookupListId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["IsInputRequired"] != null && row["IsInputRequired"].ToString() != "")
                {
                    if ((row["IsInputRequired"].ToString() == "1") || (row["IsInputRequired"].ToString().ToLower() == "true"))
                    {
                        model.IsInputRequired = true;
                    }
                    else
                    {
                        model.IsInputRequired = false;
                    }
                }
                if (row["InputTitle"] != null)
                {
                    model.InputTitle = row["InputTitle"].ToString();
                }
                if (row["AppendMoney"] != null && row["AppendMoney"].ToString() != "")
                {
                    model.AppendMoney = decimal.Parse(row["AppendMoney"].ToString());
                }
                if (row["CalculateMode"] != null && row["CalculateMode"].ToString() != "")
                {
                    model.CalculateMode = int.Parse(row["CalculateMode"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
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
            strSql.Append("select LookupItemId,LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark ");
            strSql.Append(" FROM Shop_OrderLookupItems ");
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
            strSql.Append(" LookupItemId,LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark ");
            strSql.Append(" FROM Shop_OrderLookupItems ");
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
            strSql.Append("select count(1) FROM Shop_OrderLookupItems ");
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
                strSql.Append("order by T.LookupItemId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrderLookupItems T ");
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
            parameters[0].Value = "Shop_OrderLookupItems";
            parameters[1].Value = "LookupItemId";
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

