/**
* ShoppingCarts.cs
*
* 功 能： N/A
* 类 名： ShoppingCarts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 11:18:08   N/A    初版
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
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.DBUtility;
using Maticsoft.ShoppingCart.Model;

//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Products
{
	/// <summary>
	/// 数据访问类:ShoppingCarts
	/// </summary>
	public partial class ShoppingCarts:IShoppingCarts
	{
		public ShoppingCarts()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ItemId", "Shop_ShoppingCarts");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ItemId, int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ShoppingCarts");
            strSql.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4)			};
            parameters[0].Value = ItemId;
            parameters[1].Value = UserId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.ShoppingCartItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ShoppingCarts(");
            strSql.Append("ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName)");
            strSql.Append(" values (");
            strSql.Append("@ItemId,@UserId,@Target,@Quantity,@ItemType,@Name,@ThumbnailsUrl,@Description,@CostPrice,@SellPrice,@AdjustedPrice,@Attributes,@Weight,@Deduct,@Points,@ProductLineId,@SupplierId,@SupplierName)");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@SKU", SqlDbType.NVarChar,50),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@ItemType", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CostPrice", SqlDbType.Money,8),
					new SqlParameter("@SellPrice", SqlDbType.Money,8),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@Attributes", SqlDbType.Text),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@Deduct", SqlDbType.Money,8),
					new SqlParameter("@Points", SqlDbType.Int,4),
					new SqlParameter("@ProductLineId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ItemId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.SKU;
            parameters[3].Value = model.Quantity;
            parameters[4].Value = model.ItemType;
            parameters[5].Value = model.Name;
            parameters[6].Value = model.ThumbnailsUrl;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.CostPrice;
            parameters[9].Value = model.SellPrice;
            parameters[10].Value = model.AdjustedPrice;
            parameters[11].Value = model.Attributes;
            parameters[12].Value = model.Weight;
            parameters[13].Value = model.Deduct;
            parameters[14].Value = model.Points;
            parameters[15].Value = model.ProductLineId;
            parameters[16].Value = model.SupplierId;
            parameters[17].Value = model.SupplierName;

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
        public bool Update(Maticsoft.Model.Shop.Products.ShoppingCartItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ShoppingCarts set ");
            strSql.Append("Target=@Target,");
            strSql.Append("Quantity=@Quantity,");
            strSql.Append("ItemType=@ItemType,");
            strSql.Append("Name=@Name,");
            strSql.Append("ThumbnailsUrl=@ThumbnailsUrl,");
            strSql.Append("Description=@Description,");
            strSql.Append("CostPrice=@CostPrice,");
            strSql.Append("SellPrice=@SellPrice,");
            strSql.Append("AdjustedPrice=@AdjustedPrice,");
            strSql.Append("Attributes=@Attributes,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("Deduct=@Deduct,");
            strSql.Append("Points=@Points,");
            strSql.Append("ProductLineId=@ProductLineId,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("SupplierName=@SupplierName");
            strSql.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SKU", SqlDbType.NVarChar,50),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@ItemType", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CostPrice", SqlDbType.Money,8),
					new SqlParameter("@SellPrice", SqlDbType.Money,8),
					new SqlParameter("@AdjustedPrice", SqlDbType.Money,8),
					new SqlParameter("@Attributes", SqlDbType.Text),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@Deduct", SqlDbType.Money,8),
					new SqlParameter("@Points", SqlDbType.Int,4),
					new SqlParameter("@ProductLineId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4)};
            parameters[0].Value = model.SKU;
            parameters[1].Value = model.Quantity;
            parameters[2].Value = model.ItemType;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.ThumbnailsUrl;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.CostPrice;
            parameters[7].Value = model.SellPrice;
            parameters[8].Value = model.AdjustedPrice;
            parameters[9].Value = model.Attributes;
            parameters[10].Value = model.Weight;
            parameters[11].Value = model.Deduct;
            parameters[12].Value = model.Points;
            parameters[13].Value = model.ProductLineId;
            parameters[14].Value = model.SupplierId;
            parameters[15].Value = model.SupplierName;
            parameters[16].Value = model.ItemId;
            parameters[17].Value = model.UserId;

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
        public bool Delete(int ItemId, int UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ShoppingCarts ");
            strSql.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4)			};
            parameters[0].Value = ItemId;
            parameters[1].Value = UserId;

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
        public Maticsoft.Model.Shop.Products.ShoppingCartItem GetModel(int ItemId, int UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName from Shop_ShoppingCarts ");
            strSql.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4)			};
            parameters[0].Value = ItemId;
            parameters[1].Value = UserId;

            Maticsoft.Model.Shop.Products.ShoppingCartItem model = new Maticsoft.Model.Shop.Products.ShoppingCartItem();
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
        public Maticsoft.Model.Shop.Products.ShoppingCartItem DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.ShoppingCartItem model = new Maticsoft.Model.Shop.Products.ShoppingCartItem();
            if (row != null)
            {
                if (row["ItemId"] != null && row["ItemId"].ToString() != "")
                {
                    model.ItemId = int.Parse(row["ItemId"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["SKU"] != null)
                {
                    model.SKU = row["SKU"].ToString();
                }
                if (row["Quantity"] != null && row["Quantity"].ToString() != "")
                {
                    model.Quantity = int.Parse(row["Quantity"].ToString());
                }
                if (row["ItemType"] != null && row["ItemType"].ToString() != "")
                {
                    model.ItemType =(CartItemType) int.Parse(row["ItemType"].ToString());
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
                if (row["Attributes"] != null)
                {
                    model.Attributes = row["Attributes"].ToString();
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
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            strSql.Append(" FROM Shop_ShoppingCarts ");
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
            strSql.Append(" ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            strSql.Append(" FROM Shop_ShoppingCarts ");
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
            strSql.Append("select count(1) FROM Shop_ShoppingCarts ");
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
                strSql.Append("order by T.UserId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ShoppingCarts T ");
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
            parameters[0].Value = "Shop_ShoppingCarts";
            parameters[1].Value = "UserId";
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

