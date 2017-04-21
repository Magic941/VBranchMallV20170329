using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
    /// <summary>
    /// 数据访问类:OrderReturnGoodsItem
    /// </summary>
    public partial class OrderReturnGoodsItem : IOrderReturnGoodsItem
    {
        public OrderReturnGoodsItem()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrderReturnGoodsItem");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrderReturnGoodsItem(");
            strSql.Append("ReturnId,ProductName,ProductCode,SKU,ProductId,ProductAttachmentId,ProductAttachmentName,Costprice,SellPrice,AdjustedPrice,Quantity,OrderItemId,OrderId,Attribute,Supplierid,Suppliername,OrderCode,OrderAmounts,TimeOut,CreateTime,UserID,ReturnOrderCode)");
            strSql.Append(" values (");
            strSql.Append("@ReturnId,@ProductName,@ProductCode,@SKU,@ProductId,@ProductAttachmentId,@ProductAttachmentName,@Costprice,@SellPrice,@AdjustedPrice,@Quantity,@OrderItemId,@OrderId,@Attribute,@Supplierid,@Suppliername,@OrderCode,@OrderAmounts,@TimeOut,@CreateTime,@UserID,@ReturnOrderCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductName", SqlDbType.VarChar,200),
					new SqlParameter("@ProductCode", SqlDbType.VarChar,100),
					new SqlParameter("@SKU", SqlDbType.VarChar,100),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductAttachmentId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductAttachmentName", SqlDbType.VarChar,500),
					new SqlParameter("@Costprice", SqlDbType.Money,8),
					new SqlParameter("@SellPrice", SqlDbType.Money,8),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@OrderItemId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@Attribute", SqlDbType.VarChar,500),
					new SqlParameter("@Supplierid", SqlDbType.BigInt,8),
					new SqlParameter("@Suppliername", SqlDbType.VarChar,100),
					new SqlParameter("@OrderCode", SqlDbType.VarChar,100),
					new SqlParameter("@OrderAmounts", SqlDbType.Money,8),
					new SqlParameter("@TimeOut", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UserID", SqlDbType.BigInt,8),
                    new SqlParameter("@ReturnOrderCode",SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.ReturnId;
            parameters[1].Value = model.ProductName;
            parameters[2].Value = model.ProductCode;
            parameters[3].Value = model.SKU;
            parameters[4].Value = model.ProductId;
            parameters[5].Value = model.ProductAttachmentId;
            parameters[6].Value = model.ProductAttachmentName;
            parameters[7].Value = model.Costprice;
            parameters[8].Value = model.SellPrice;
            parameters[9].Value = model.AdjustedPrice;
            parameters[10].Value = model.Quantity;
            parameters[11].Value = model.OrderItemId;
            parameters[12].Value = model.OrderId;
            parameters[13].Value = model.Attribute;
            parameters[14].Value = model.Supplierid;
            parameters[15].Value = model.Suppliername;
            parameters[16].Value = model.OrderCode;
            parameters[17].Value = model.OrderAmounts;
            parameters[18].Value = model.TimeOut;
            parameters[19].Value = model.CreateTime;
            parameters[20].Value = model.UserID;
            parameters[21].Value = model.ReturnOrderCode;

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
        public bool Update(Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoodsItem set ");
            strSql.Append("ReturnId=@ReturnId,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("SKU=@SKU,");
            strSql.Append("ProductId=@ProductId,");
            strSql.Append("ProductAttachmentId=@ProductAttachmentId,");
            strSql.Append("ProductAttachmentName=@ProductAttachmentName,");
            strSql.Append("Costprice=@Costprice,");
            strSql.Append("SellPrice=@SellPrice,");
            strSql.Append("AdjustedPrice=@AdjustedPrice,");
            strSql.Append("Quantity=@Quantity,");
            strSql.Append("OrderItemId=@OrderItemId,");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("Attribute=@Attribute,");
            strSql.Append("Supplierid=@Supplierid,");
            strSql.Append("Suppliername=@Suppliername,");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("OrderAmounts=@OrderAmounts,");
            strSql.Append("TimeOut=@TimeOut,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UserID=@UserID");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductName", SqlDbType.VarChar,200),
					new SqlParameter("@ProductCode", SqlDbType.VarChar,100),
					new SqlParameter("@SKU", SqlDbType.VarChar,100),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductAttachmentId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductAttachmentName", SqlDbType.VarChar,500),
					new SqlParameter("@Costprice", SqlDbType.Money,8),
					new SqlParameter("@SellPrice", SqlDbType.Money,8),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@OrderItemId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@Attribute", SqlDbType.VarChar,500),
					new SqlParameter("@Supplierid", SqlDbType.BigInt,8),
					new SqlParameter("@Suppliername", SqlDbType.VarChar,100),
					new SqlParameter("@OrderCode", SqlDbType.VarChar,100),
					new SqlParameter("@OrderAmounts", SqlDbType.Money,8),
					new SqlParameter("@TimeOut", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UserID", SqlDbType.BigInt,8),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ReturnId;
            parameters[1].Value = model.ProductName;
            parameters[2].Value = model.ProductCode;
            parameters[3].Value = model.SKU;
            parameters[4].Value = model.ProductId;
            parameters[5].Value = model.ProductAttachmentId;
            parameters[6].Value = model.ProductAttachmentName;
            parameters[7].Value = model.Costprice;
            parameters[8].Value = model.SellPrice;
            parameters[9].Value = model.AdjustedPrice;
            parameters[10].Value = model.Quantity;
            parameters[11].Value = model.OrderItemId;
            parameters[12].Value = model.OrderId;
            parameters[13].Value = model.Attribute;
            parameters[14].Value = model.Supplierid;
            parameters[15].Value = model.Suppliername;
            parameters[16].Value = model.OrderCode;
            parameters[17].Value = model.OrderAmounts;
            parameters[18].Value = model.TimeOut;
            parameters[19].Value = model.CreateTime;
            parameters[20].Value = model.UserID;
            parameters[21].Value = model.Id;

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
        public bool Delete(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderReturnGoodsItem ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
			};
            parameters[0].Value = Id;

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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderReturnGoodsItem ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public Maticsoft.Model.Shop.Order.OrderReturnGoodsItem GetModel(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,ReturnId,ProductName,ProductCode,SKU,ProductId,ProductAttachmentId,ProductAttachmentName,Costprice,SellPrice,AdjustedPrice,Quantity,OrderItemId,OrderId,Attribute,Supplierid,Suppliername,OrderCode,OrderAmounts,TimeOut,CreateTime,UserID,ReturnOrderCode from Shop_OrderReturnGoodsItem ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
			};
            parameters[0].Value = Id;

            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model = new Maticsoft.Model.Shop.Order.OrderReturnGoodsItem();
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
        /// 得到一个对象实体(李永琴新增)
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderReturnGoodsItem GetReturnGoodItemModel(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,ReturnId,ProductName,ProductCode,SKU,ProductId,ProductAttachmentId,ProductAttachmentName,Costprice,SellPrice,AdjustedPrice,Quantity,OrderItemId,OrderId,Attribute,Supplierid,Suppliername,OrderCode,OrderAmounts,TimeOut,CreateTime,UserID,ReturnOrderCode from Shop_OrderReturnGoodsItem ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
			};
            parameters[0].Value = Id;

            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model = new Maticsoft.Model.Shop.Order.OrderReturnGoodsItem();
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
        public Maticsoft.Model.Shop.Order.OrderReturnGoodsItem DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem model = new Maticsoft.Model.Shop.Order.OrderReturnGoodsItem();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = long.Parse(row["Id"].ToString());
                }
                if (row["ReturnId"] != null && row["ReturnId"].ToString() != "")
                {
                    model.ReturnId = long.Parse(row["ReturnId"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["SKU"] != null)
                {
                    model.SKU = row["SKU"].ToString();
                }
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["ProductAttachmentId"] != null && row["ProductAttachmentId"].ToString() != "")
                {
                    model.ProductAttachmentId = long.Parse(row["ProductAttachmentId"].ToString());
                }
                if (row["ProductAttachmentName"] != null)
                {
                    model.ProductAttachmentName = row["ProductAttachmentName"].ToString();
                }
                if (row["Costprice"] != null && row["Costprice"].ToString() != "")
                {
                    model.Costprice = decimal.Parse(row["Costprice"].ToString());
                }
                if (row["SellPrice"] != null && row["SellPrice"].ToString() != "")
                {
                    model.SellPrice = decimal.Parse(row["SellPrice"].ToString());
                }
                if (row["AdjustedPrice"] != null && row["AdjustedPrice"].ToString() != "")
                {
                    model.AdjustedPrice = decimal.Parse(row["AdjustedPrice"].ToString());
                }
                if (row["Quantity"] != null && row["Quantity"].ToString() != "")
                {
                    model.Quantity = int.Parse(row["Quantity"].ToString());
                }
                if (row["OrderItemId"] != null && row["OrderItemId"].ToString() != "")
                {
                    model.OrderItemId = long.Parse(row["OrderItemId"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["Attribute"] != null)
                {
                    model.Attribute = row["Attribute"].ToString();
                }
                if (row["Supplierid"] != null && row["Supplierid"].ToString() != "")
                {
                    model.Supplierid = long.Parse(row["Supplierid"].ToString());
                }
                if (row["Suppliername"] != null)
                {
                    model.Suppliername = row["Suppliername"].ToString();
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["OrderAmounts"] != null && row["OrderAmounts"].ToString() != "")
                {
                    model.OrderAmounts = decimal.Parse(row["OrderAmounts"].ToString());
                }
                if (row["TimeOut"] != null && row["TimeOut"].ToString() != "")
                {
                    model.TimeOut = DateTime.Parse(row["TimeOut"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = long.Parse(row["UserID"].ToString());
                }
                if (row["ReturnOrderCode"] != null && row["ReturnOrderCode"].ToString() != "")
                {
                    model.ReturnOrderCode = row["ReturnOrderCode"].ToString();
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
            strSql.Append("select Id,ReturnId,ProductName,ProductCode,SKU,ProductId,ProductAttachmentId,ProductAttachmentName,Costprice,SellPrice,AdjustedPrice,Quantity,OrderItemId,OrderId,Attribute,Supplierid,Suppliername,OrderCode,OrderAmounts,TimeOut,CreateTime,UserID,ReturnOrderCode ");
            strSql.Append(" FROM Shop_OrderReturnGoodsItem ");
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
            strSql.Append("a.OrderId, a.UserId, a.ReturnAmounts, a.ReturnTime, a.ReturnReason, a.ReturnDescription, a.ReturnAddress, a.ReturnPostCode, a.ReturnTelphone, a.ReturnContacts, a.Attachment, a.ApproveStatus, a.ApprovePeason, a.ApproveTime, a.ApproveRemark, a.AccountStatus, a.AccountTime, a.AccountPeason, a.IsDeleted, a.Information, a.ExpressNO, a.AmountActual, a.ReturnRemark, a.ReturnOrderCode, a.OrderCode, a.SupplierId, a.SupplierName, a.CouponCode, a.CouponName, a.CouponAmount, a.CouponValueType, a.CouponValue, a.ReturnGoodsType, a.ReturnCoupon, a.ActualSalesTotal, a.AmountAdjusted, a.Amount, a.ServiceType, a.ReturnType, a.PickRegionId, a.PickRegion, a.PickAddress, a.PickZipCode, a.PickName, a.PickTelPhone, a.PickCellPhone, a.PickEmail, a.ReturnTrueName, a.ReturnBankName, a.ReturnCard, a.ReturnCardType, a.ContactName, a.ContactPhone, a.Status, a.RefundStatus, a.LogisticStatus, a.CustomerReview, a.RefuseReason, a.Refuseremark, a.Repairremark, a.Adjustableremark,b.Id, b.ReturnId, b.ProductName, b.ProductCode, b.SKU, b.ProductId, b.ProductAttachmentId, b.ProductAttachmentName, b.Costprice, b.SellPrice, b.AdjustedPrice, b.Quantity, b.OrderItemId, b.OrderId, b.Attribute, b.Supplierid, b.Suppliername, b.OrderCode, b.OrderAmounts, b.TimeOut, b.CreateTime, b.UserID, b.ReturnOrderCode FROM dbo.Shop_OrderReturnGoods AS a");
            strSql.Append(" LEFT JOIN dbo.Shop_OrderReturnGoodsItem AS b");
            strSql.Append(" ON a.id=b.ReturnId");
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
            strSql.Append("select count(1) FROM Shop_OrderReturnGoodsItem ");
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
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrderReturnGoodsItem T ");
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
            parameters[0].Value = "Shop_OrderReturnGoodsItem";
            parameters[1].Value = "Id";
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

