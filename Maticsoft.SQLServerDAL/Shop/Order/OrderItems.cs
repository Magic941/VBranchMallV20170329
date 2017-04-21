using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
	/// <summary>
	/// 数据访问类:OrderItem
	/// </summary>
	public partial class OrderItems:IOrderItems
	{
		public OrderItems()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ItemId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrderItems");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.BigInt)
			};
            parameters[0].Value = ItemId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrderItems(");
            strSql.Append("OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName,Returnqty,Returnstatus,ReturnOrderType)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@OrderCode,@ProductId,@ProductCode,@SKU,@Name,@ThumbnailsUrl,@Description,@Quantity,@ShipmentQuantity,@CostPrice,@SellPrice,@AdjustedPrice,@Attribute,@Remark,@Weight,@Deduct,@Points,@ProductLineId,@SupplierId,@SupplierName,@ReturnQty,@ReturnStatus,@ReturnOrderType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SKU", SqlDbType.NVarChar,200),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@ShipmentQuantity", SqlDbType.Int,4),
					new SqlParameter("@CostPrice", SqlDbType.Money,8),
					new SqlParameter("@SellPrice", SqlDbType.Money,8),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@Attribute", SqlDbType.Text),
					new SqlParameter("@Remark", SqlDbType.Text),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@Deduct", SqlDbType.Money,8),
					new SqlParameter("@Points", SqlDbType.Int,4),
					new SqlParameter("@ProductLineId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnQty", SqlDbType.Int,4),
                    new SqlParameter("@ReturnStatus", SqlDbType.Int,4),
                    new SqlParameter("@ReturnOrderType", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.ProductId;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.SKU;
            parameters[5].Value = model.Name;
            parameters[6].Value = model.ThumbnailsUrl;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.Quantity;
            parameters[9].Value = model.ShipmentQuantity;
            parameters[10].Value = model.CostPrice;
            parameters[11].Value = model.SellPrice;
            parameters[12].Value = model.AdjustedPrice;
            parameters[13].Value = model.Attribute;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.Weight;
            parameters[16].Value = model.Deduct;
            parameters[17].Value = model.Points;
            parameters[18].Value = model.ProductLineId;
            parameters[19].Value = model.SupplierId;
            parameters[20].Value = model.SupplierName;
            parameters[21].Value = model.ReturnQty;
            parameters[22].Value = model.ReturnStatus;
            parameters[23].Value = model.ReturnOrderType;

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
        public bool Update(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderItems set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("ProductId=@ProductId,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("SKU=@SKU,");
            strSql.Append("Name=@Name,");
            strSql.Append("ThumbnailsUrl=@ThumbnailsUrl,");
            strSql.Append("Description=@Description,");
            strSql.Append("Quantity=@Quantity,");
            strSql.Append("ShipmentQuantity=@ShipmentQuantity,");
            strSql.Append("CostPrice=@CostPrice,");
            strSql.Append("SellPrice=@SellPrice,");
            strSql.Append("AdjustedPrice=@AdjustedPrice,");
            strSql.Append("Attribute=@Attribute,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("Deduct=@Deduct,");
            strSql.Append("Points=@Points,");
            strSql.Append("ProductLineId=@ProductLineId,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("Returnqty=@Returnqty,");
            strSql.Append("Returnstatus=@Returnstatus,");
            strSql.Append("ReturnOrderType=@ReturnOrderType");
            strSql.Append("ReturnQty=@ReturnQty");
            strSql.Append("ReturnStatus=@ReturnStatus");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SKU", SqlDbType.NVarChar,200),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@ShipmentQuantity", SqlDbType.Int,4),
					new SqlParameter("@CostPrice", SqlDbType.Money,8),
					new SqlParameter("@SellPrice", SqlDbType.Money,8),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@Attribute", SqlDbType.Text),
					new SqlParameter("@Remark", SqlDbType.Text),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@Deduct", SqlDbType.Money,8),
					new SqlParameter("@Points", SqlDbType.Int,4),
					new SqlParameter("@ProductLineId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnQty", SqlDbType.Int),
                    new SqlParameter("@ReturnStatus", SqlDbType.Int),
                    new SqlParameter("@ReturnOrderType", SqlDbType.Int,4),
                    new SqlParameter("@ItemId", SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.ProductId;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.SKU;
            parameters[5].Value = model.Name;
            parameters[6].Value = model.ThumbnailsUrl;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.Quantity;
            parameters[9].Value = model.ShipmentQuantity;
            parameters[10].Value = model.CostPrice;
            parameters[11].Value = model.SellPrice;
            parameters[12].Value = model.AdjustedPrice;
            parameters[13].Value = model.Attribute;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.Weight;
            parameters[16].Value = model.Deduct;
            parameters[17].Value = model.Points;
            parameters[18].Value = model.ProductLineId;
            parameters[19].Value = model.SupplierId;
            parameters[20].Value = model.SupplierName;
            parameters[21].Value = model.ReturnQty;
            parameters[22].Value = model.ReturnStatus;
            parameters[23].Value = model.ReturnOrderType;
            parameters[24].Value = model.ItemId;

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


        public bool UpdateStatus(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Shop_OrderItems set");
            strSql.Append(" ReturnQty = @ReturnQty,");
            strSql.Append(" ReturnStatus = @ReturnStatus, ");
            strSql.Append("ReturnOrderType = @ReturnOrderType ");
            strSql.Append(" Where ItemId = @ItemId");
            SqlParameter[] parameters = {
                                        new SqlParameter("@ReturnQty",SqlDbType.Int),
                                        new SqlParameter("@ReturnStatus",SqlDbType.Int),
                                        new SqlParameter("@ReturnOrderType",SqlDbType.Int),
                                        new SqlParameter("@ItemId",SqlDbType.BigInt)
                                        };
            parameters[0].Value = model.ReturnQty;
            parameters[1].Value = model.ReturnStatus;
            parameters[2].Value = model.ReturnOrderType;
            parameters[3].Value = model.ItemId;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows >0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改 Shop_OrderItems原本数量减去退货数量
        /// </summary>
        /// <param name="model"></param>
        public bool updateOrader2(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderItems set ");
            strSql.Append("ReturnQty=@ReturnQty");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ReturnQty",SqlDbType.Int,8),
                    new SqlParameter("@OrderCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@ItemId", SqlDbType.BigInt,8)};

            parameters[0].Value = model.ReturnQty;
            parameters[1].Value = model.ItemId;

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
        public bool Delete(long ItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderItems ");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.BigInt)
			};
            parameters[0].Value = ItemId;

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
        public bool DeleteList(string ItemIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderItems ");
            strSql.Append(" where ItemId in (" + ItemIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Order.OrderItems GetModel(long ItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemId,OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName,ActiveID,ActiveType,ReturnQty,Returnstatus,ReturnOrderType from Shop_OrderItems ");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.BigInt)
			};
            parameters[0].Value = ItemId;

            Maticsoft.Model.Shop.Order.OrderItems model = new Maticsoft.Model.Shop.Order.OrderItems();
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
        public Maticsoft.Model.Shop.Order.OrderItems DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderItems model = new Maticsoft.Model.Shop.Order.OrderItems();
            if (row != null)
            {
                if (row["ItemId"] != null && row["ItemId"].ToString() != "")
                {
                    model.ItemId = long.Parse(row["ItemId"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["SKU"] != null)
                {
                    model.SKU = row["SKU"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["ThumbnailsUrl"] != null)
                {
                    model.ThumbnailsUrl = row["ThumbnailsUrl"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["Quantity"] != null && row["Quantity"].ToString() != "")
                {
                    model.Quantity = int.Parse(row["Quantity"].ToString());
                }
                if (row["ShipmentQuantity"] != null && row["ShipmentQuantity"].ToString() != "")
                {
                    model.ShipmentQuantity = int.Parse(row["ShipmentQuantity"].ToString());
                }
                if (row["CostPrice"] != null && row["CostPrice"].ToString() != "")
                {
                    model.CostPrice = decimal.Parse(row["CostPrice"].ToString());
                }
                if (row["SellPrice"] != null && row["SellPrice"].ToString() != "")
                {
                    model.SellPrice = decimal.Parse(row["SellPrice"].ToString());
                }
                if (row["AdjustedPrice"] != null && row["AdjustedPrice"].ToString() != "")
                {
                    model.AdjustedPrice = decimal.Parse(row["AdjustedPrice"].ToString());
                }
                if (row["Attribute"] != null)
                {
                    model.Attribute = row["Attribute"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["Weight"] != null && row["Weight"].ToString() != "")
                {
                    model.Weight = int.Parse(row["Weight"].ToString());
                }
                if (row["Deduct"] != null && row["Deduct"].ToString() != "")
                {
                    model.Deduct = decimal.Parse(row["Deduct"].ToString());
                }
                if (row["Points"] != null && row["Points"].ToString() != "")
                {
                    model.Points = int.Parse(row["Points"].ToString());
                }
                if (row["ProductLineId"] != null && row["ProductLineId"].ToString() != "")
                {
                    model.ProductLineId = int.Parse(row["ProductLineId"].ToString());
                }
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["SupplierName"] != null)
                {
                    model.SupplierName = row["SupplierName"].ToString();
                }

                if (row["ActiveID"] != null && row["ActiveID"].ToString() != "")
                {
                    model.ActiveID =int.Parse( row["ActiveID"].ToString());
                }
                if (row["ActiveType"] != null && row["ActiveType"].ToString() != "")
                {
                    model.ActiveType = int.Parse(row["ActiveType"].ToString());
                }
                if (row.Table.Columns.Contains("ReturnStatus"))
                {
                    int returnStatus = 0;
                    int.TryParse(row["ReturnStatus"].ToString(), out returnStatus);
                    model.ReturnStatus = returnStatus;
                }
                if (row.Table.Columns.Contains("ReturnQty"))
                {
                    int returnQty = 0;
                    int.TryParse(row["ReturnQty"].ToString(), out returnQty);
                    model.ReturnQty = returnQty;
                }
                if (row.Table.Columns.Contains("ReturnOrderType"))
                {
                    int returnOrderType = 0;
                    int.TryParse(row["ReturnOrderType"].ToString(), out returnOrderType);
                    model.ReturnOrderType = returnOrderType;
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
            strSql.Append("select ItemId,OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName,ActiveID, ActiveType,ReturnQty,Returnstatus,ReturnOrderType ");
            strSql.Append(" FROM Shop_OrderItems ");
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
            strSql.Append(" ItemId,OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName,ReturnQty,Returnstatus,ReturnOrderType");
            strSql.Append(" FROM Shop_OrderItems ");
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
            strSql.Append("select count(1) FROM Shop_OrderItems ");
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
                strSql.Append("order by T.ItemId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrderItems T ");
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
            parameters[0].Value = "Shop_OrderItems";
            parameters[1].Value = "ItemId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

	    public  DataSet GetSaleRecordByPage(long  productId ,string  orderby , int startIndex ,int endIndex )
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
                strSql.Append("order by T.ItemId desc");
            }
            strSql.Append(")AS Row,  T.SellPrice , T.ShipmentQuantity , p.BuyerName ,p.CreatedDate  from Shop_OrderItems T ");
            strSql.AppendFormat(" JOIN Shop_Orders p ON T.ProductId = {0} AND p.OrderStatus = 2  and  T.OrderId=p.OrderId", productId);
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
	    }


	    public int GetSaleRecordCount(long productId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT COUNT(1) FROM Shop_OrderItems tt JOIN    Shop_Orders p");
	        strSql.AppendFormat(" ON tt.ProductId={0} AND p.OrderStatus=2 AND tt.OrderId=p.OrderId", productId);
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
        #region 查询商品的SKU对应的商品属性和值
        /// <summary>
        /// 查询商品的SKU对应的商品属性和值
        /// </summary>
        /// <param name="SKU"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public DataSet GetProductSkuItemAttributeValues(string SKU, long ProductId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@SKU", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ProductId", SqlDbType.BigInt)
                    };
            parameters[0].Value = SKU;
            parameters[1].Value = ProductId;
            return DbHelperSQL.RunProcedure("SP_Product_Sku_item_AttributeValues", parameters, "ProductSkuItemAttributeValues");
        }
        #endregion

        #region 退货更新
        /// <summary>
        /// 退货数量
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <param name="itemReturnStatus"></param>
        /// <param name="returnQty"></param>
        /// <returns></returns>
        public bool UpdateOrderRereturnStatus(long orderItemId, Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus itemReturnStatus, int returnQty, Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType returnGoodsType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderItems set ");
            strSql.Append("ReturnStatus=@ReturnStatus,");
            strSql.Append("ReturnQty=ISNULL(ReturnQty,0)+@ReturnQty,");
            strSql.Append("ReturnOrderType=@ReturnOrderType");
            strSql.Append(" where ItemId=@ItemId ");

            SqlParameter[] parameters = {				 
                 new SqlParameter("@ReturnStatus", SqlDbType.Int,4),
                 new SqlParameter("@ReturnQty", SqlDbType.Int,4),
                 new SqlParameter("@ReturnOrderType", SqlDbType.Int,4),
                 new SqlParameter("@ItemId", SqlDbType.BigInt,8)};
            parameters[0].Value = (int)itemReturnStatus;
            parameters[1].Value = returnQty;
            parameters[2].Value = (int)returnGoodsType;
            parameters[3].Value = orderItemId;
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

        public DataSet GetCommission(decimal DErate, decimal CPrate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
                @"
--设计师 总量/额
SELECT  1 AS [Type], SUM(I.Quantity) ToalQuantity
      , SUM(I.SellPrice * I.Quantity) * {0} ToalPrice
FROM    Shop_OrderItems I, Shop_Orders O,Accounts_Users U,Shop_Products P
WHERE I.OrderId = O.OrderId
  AND P.ProductId=I.ProductId AND P.CreateUserID=U.UserID AND U.UserType = 'DE'
AND O.OrderStatus = 2 AND O.OrderType = 1
UNION ALL      
--CP 总量/额         
SELECT  2 AS [Type], SUM(I.Quantity) ToalQuantity
      , SUM(I.SellPrice * I.Quantity) * {1} ToalPrice
FROM    Shop_OrderItems I, Shop_Orders O,Accounts_Users U,Shop_Products P
WHERE I.OrderId = O.OrderId
 AND P.ProductId=I.ProductId AND P.CreateUserID=U.UserID AND U.UserType = 'CP'
AND O.OrderStatus = 2 AND O.OrderType = 1

", DErate, CPrate);
            return DbHelperSQL.Query(strSql.ToString());
        }

	    #endregion  ExtensionMethod
	}
}

