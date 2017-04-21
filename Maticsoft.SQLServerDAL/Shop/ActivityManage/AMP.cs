using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.ActivityManage;
using Maticsoft.DBUtility;

namespace Maticsoft.SQLServerDAL.Shop.ActivityManage
{
    public partial class AMP : IAMP
    {
        public AMP()
        { }

        #region
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AMPId", "Shop_ActivityManageProduct");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AMId, long SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ActivityManageProduct");
            strSql.Append(" where AMId=@AMId and SupplierId=@SupplierId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.BigInt,8)			
                                        };
            parameters[0].Value = AMId;
            parameters[1].Value = SupplierId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public bool ExistsSup(int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ActivityManageProduct ");
            strSql.Append(" where SupplierId = @SupplierId ");
            SqlParameter[] parameters ={
                                       new SqlParameter("@SupplierId",SqlDbType.Int,4)
                                       };
            parameters[0].Value = SupplierId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 是否存在该供应商的数据
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        public bool ExistsSups(int SupplierId, int AMId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ActivityManageProduct ");
            strSql.Append(" where SupplierId = @SupplierId and AMId=@AMId ");
            SqlParameter[] parameters ={
                                       new SqlParameter("@SupplierId",SqlDbType.Int,4),
                                       new SqlParameter("@AMId",SqlDbType.Int,4)
                                       };
            parameters[0].Value = SupplierId;
            parameters[1].Value = AMId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 是否存在该商品的数据
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public bool ExistsPro(int ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ActivityManageProduct ");
            strSql.Append(" where ProductId = @ProductId ");
            SqlParameter[] parameters ={
                                       new SqlParameter("@ProductId",SqlDbType.Int,4)
                                       };
            parameters[0].Value = ProductId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);

        }
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.ActivityManage.AMPModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ActivityManageProduct(");
            strSql.Append("AMId,ProductId,ProductName,SupplierId,GiftsProId,GiftsProName )");
            strSql.Append(" values (");
            strSql.Append("@AMId,@ProductId,@ProductName,@SupplierId,@GiftsProId,@GiftsProName )");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@GiftsProId", SqlDbType.Int,4),
                    new SqlParameter("@GiftsProName", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = model.AMId;
            parameters[1].Value = model.ProductId;
            parameters[2].Value = model.ProductName;
            parameters[3].Value = model.SupplierId;
            parameters[4].Value = model.GiftsProId;
            parameters[5].Value = model.GiftsProName;
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
        public bool Update(Maticsoft.Model.Shop.ActivityManage.AMPModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ActivityManageProduct set ");
            strSql.Append("ProductName=@ProductName");
            strSql.Append("ProductId=@ProductId");
            strSql.Append("SupplierId=@SupplierId");
            strSql.Append("GiftsProId=@GiftsProId");
            strSql.Append("GiftsProName=@GiftsProName");
            strSql.Append(" where AMId=@AMId  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@AMId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@SupplierId", SqlDbType.BigInt,8),
                    new SqlParameter("@GiftsProId", SqlDbType.BigInt,8),
                    new SqlParameter("@GiftsProName", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = model.ProductName;
            parameters[1].Value = model.AMId;
            parameters[2].Value = model.ProductId;
            parameters[3].Value = model.SupplierId;
            parameters[4].Value = model.GiftsProId;
            parameters[5].Value = model.GiftsProName;
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
        ///  根据供应商Id删除数据
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        public bool Delete(int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManageProduct ");
            strSql.Append(" where SupplierId=@SupplierId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.BigInt,8)			};
            parameters[0].Value = SupplierId;

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
        public bool Delete(int AMId, long SupplierId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManageProduct ");
            strSql.Append(" where AMId=@AMId and SupplierId=@SupplierId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.BigInt,8)			};
            parameters[0].Value = AMId;
            parameters[1].Value = SupplierId;

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
        /// 根据主商品删除一条数据
        /// </summary>
        public bool DeleteByProId(int ProductId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManageProduct ");
            strSql.Append(" where ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.Int,4)	};
            parameters[0].Value = ProductId;

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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AMId,ProductId,ProductName,SupplierId ");
            strSql.Append(" FROM Shop_ActivityManageProduct ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AMId,ProductId,ProductName,SupplierId,GiftsProId,GiftsProName ");
            strSql.Append(" FROM Shop_ActivityManageProduct ");

            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetLists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AMId,AMName,SupplierId,SupplierName,ProductId,ProductName,GiftsProId,GiftsProName ");
            strSql.Append(" FROM V_AmpSupplier ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetLists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AMId,AMName,SupplierId,SupplierName,ProductId,ProductName,GiftsProId,GiftsProName ");
            strSql.Append(" FROM V_AmpSupplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 查询出当前对应的活动商品
        /// </summary>
        /// <param name="AMId"></param>
        /// <returns></returns>
        public DataSet GetLists(int AMId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AMId,AMName,ProductId,ProductName,ProductCode,SupplierId,Name,GiftsProId,GiftsProName ");
            strSql.Append(" FROM V_AmpProducts ");
            if (AMId.ToString() != "")
            {
                strSql.Append(" where AMId = " + AMId);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetListAndPrice(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT DISTINCT a.AMPId,a.AMId,a.ProductId,a.ProductName,a.SupplierId,GiftsProId,GiftsProName, CONVERT(DECIMAL(12,2),b.LowestSalePrice)LowestSalePrice FROM dbo.Shop_ActivityManageProduct a 
INNER JOIN (SELECT * FROM dbo.Shop_Products) b 
ON a.ProductId = b.productid ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.ActivityManage.AMPModel GetModel(int AMId, long SupplierId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AMId,ProductId,ProductName,SupplierId,GiftsProId,GiftsProName from Shop_ActivityManageProduct ");
            strSql.Append(" where AMId=@AMId and SupplierId=@SupplierId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.BigInt,8)			};
            parameters[0].Value = AMId;
            parameters[1].Value = SupplierId;

            Maticsoft.Model.Shop.ActivityManage.AMPModel model = new Model.Shop.ActivityManage.AMPModel();
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

        public Maticsoft.Model.Shop.ActivityManage.AMPModel GetModelBySupplierId(int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AMId,SupplierId from Shop_ActivityManageProduct where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4)
					};
            parameters[0].Value = SupplierId;
            Maticsoft.Model.Shop.ActivityManage.AMPModel model = new Model.Shop.ActivityManage.AMPModel();
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
        public Maticsoft.Model.Shop.ActivityManage.AMPModel DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.ActivityManage.AMPModel model = new Model.Shop.ActivityManage.AMPModel();

            if (row["AMId"] != null && row["AMId"].ToString() != "")
            {
                model.AMId = int.Parse(row["AMId"].ToString());
            }
            if (row.Table.Columns.Contains("ProductId"))
            {
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = int.Parse(row["ProductId"].ToString());
                }
            }
            if (row.Table.Columns.Contains("ProductName"))
            {
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
            }
            if (row.Table.Columns.Contains("SupplierId"))
            {
                if (row["SupplierId"] != null)
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
            }
            if (row.Table.Columns.Contains("SupplierName"))
            {
                if (row["SupplierName"] != null)
                {
                    model.SupplierName = row["SupplierName"].ToString();
                }
            }
            if (row.Table.Columns.Contains("AMName"))
            {
                if (row["AMName"] != null)
                {
                    model.AMName = row["AMName"].ToString();
                }
            }
            if (row.Table.Columns.Contains("GiftsProId"))
            {
                if (row["GiftsProId"] != null)
                {
                    model.GiftsProId = int.Parse(row["GiftsProId"].ToString());
                }
            }
            if (row.Table.Columns.Contains("GiftsProName"))
            {
                if (row["GiftsProName"] != null)
                {
                    model.GiftsProName = row["GiftsProId"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.ActivityManage.AMPModel DataRow2ToModel(DataRow row)
        {
            Maticsoft.Model.Shop.ActivityManage.AMPModel model = new Model.Shop.ActivityManage.AMPModel();
            if (row != null)
            {
                if (row["AMId"] != null && row["AMId"].ToString() != "")
                {
                    model.AMId = int.Parse(row["AMId"].ToString());
                }
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = int.Parse(row["ProductId"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }

                if (row["AMName"] != null)
                {
                    model.AMName = row["AMName"].ToString();
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["GiftsProId"] != null)
                {
                    model.GiftsProId = int.Parse(row["GiftsProId"].ToString());
                }
                if (row["SupplierId"] != null)
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.SupplierName = (row["Name"].ToString());
                }
            }
            return model;
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
            strSql.Append(" AMId,ProductId,ProductName,SupplierId,GiftsProId,GiftsProName");
            strSql.Append(" FROM Shop_ActivityManageProduct ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }



        public DataSet GetListAndPrice(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(@" a.AMPId,a.AMId,a.ProductId,a.ProductName,a.SupplierId,CONVERT(DECIMAL(12,2),b.LowestSalePrice) LowestSalePrice FROM dbo.Shop_ActivityManageProduct a 
INNER JOIN (SELECT * FROM dbo.Shop_Products) b 
ON a.ProductId = b.productid  ");
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
            strSql.Append("select count(1) FROM Shop_ActivityManageProduct ");
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
                strSql.Append("order by T.ProductId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ActivityManageProduct T ");
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
            parameters[0].Value = "Shop_SalesRuleProduct";
            parameters[1].Value = "ProductId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        public bool DeleteByAMId(int AMId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ActivityManageProduct ");
            strSql.Append(" where AMId=@AMId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AMId", SqlDbType.Int,4)		};
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


        public DataSet GetAMProducts(int AMId, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PSM.ProductId ");
            strSql.Append("FROM Shop_ActivityManageProduct PSM ");
            strSql.Append("INNER JOIN (SELECT ProductId FROM  Shop_Products P ");
            strSql.Append("WHERE P.SaleStatus=1 ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append(")A ON PSM.ProductId = A.ProductId ");
            strSql.Append(" WHERE AMId=@AMId ");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@AMId", SqlDbType.Int, 4)
                };
            parameters[0].Value = AMId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }



        public DataTable GetActiveRuleByAMId(int AMId, decimal Price)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT TOP 1 a.AMId,a.AMType,a.AMStatus,AMDUnitValue,AMDRateValue,a.AMName FROM dbo.Shop_ActivityManage a INNER JOIN dbo.Shop_ActivityManageDetail b ON a.AMId = b.AMId ");
            stb.AppendFormat(" WHERE a.AMId={0} AND b.AMDUnitValue<={1} AND a.AMStatus=0 AND GETDATE() BETWEEN AMStartDate AND AMEndDate ORDER BY AMDUnitValue desc", AMId, Price);
            Maticsoft.Model.Shop.ActivityManage.AMPModel model = new Model.Shop.ActivityManage.AMPModel();
            DataSet ds = DbHelperSQL.Query(stb.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }


        //public DataSet GetAMProducts(int AMId, string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT ProductId FROM Shop_ActivityManageProduct a WHERE a.ProductId NOT IN(SELECT ProductId FROM  Shop_Products P WHERE P.SaleStatus=1 ");

        //    if (!string.IsNullOrWhiteSpace(strWhere))
        //    {
        //        strSql.Append(strWhere);
        //    }
        //    strSql.Append(" and AMId=@AMId ");
        //    SqlParameter[] parameters =
        //        {
        //            new SqlParameter("@AMId", SqlDbType.Int, 4)
        //        };
        //    parameters[0].Value = AMId;
        //    return DbHelperSQL.Query(strSql.ToString(), parameters);
        //}

        #endregion  ExtensionMethod
    }
}
