/**
* ShippingAddress.cs
*
* 功 能： N/A
* 类 名： ShippingAddress
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 10:24:44   N/A    初版
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
using Maticsoft.IDAL.Shop.Shipping;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
	/// <summary>
	/// 数据访问类:ShippingAddress
	/// </summary>
	public partial class ShippingAddress:IShippingAddress
	{
		public ShippingAddress()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ShippingId", "Shop_ShippingAddress");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ShippingId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ShippingAddress");
            strSql.Append(" where ShippingId=@ShippingId");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingId", SqlDbType.Int,4)
			};
            parameters[0].Value = ShippingId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ShippingAddress(");
            strSql.Append("RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone)");
            strSql.Append(" values (");
            strSql.Append("@RegionId,@UserId,@ShipName,@Address,@Zipcode,@EmailAddress,@TelPhone,@CelPhone)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ShipName", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@Zipcode", SqlDbType.NVarChar,20),
					new SqlParameter("@EmailAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@CelPhone", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.RegionId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ShipName;
            parameters[3].Value = model.Address;
            parameters[4].Value = model.Zipcode;
            parameters[5].Value = model.EmailAddress;
            parameters[6].Value = model.TelPhone;
            parameters[7].Value = model.CelPhone;

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
        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ShippingAddress set ");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("ShipName=@ShipName,");
            strSql.Append("Address=@Address,");
            strSql.Append("Zipcode=@Zipcode,");
            strSql.Append("EmailAddress=@EmailAddress,");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("CelPhone=@CelPhone,");
            strSql.Append("IsDefault=@IsDefault");
            strSql.Append(" where ShippingId=@ShippingId");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ShipName", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,300),
					new SqlParameter("@Zipcode", SqlDbType.NVarChar,20),
					new SqlParameter("@EmailAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@CelPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsDefault",SqlDbType.Bit),
					new SqlParameter("@ShippingId", SqlDbType.Int,4)};
            parameters[0].Value = model.RegionId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ShipName;
            parameters[3].Value = model.Address;
            parameters[4].Value = model.Zipcode;
            parameters[5].Value = model.EmailAddress;
            parameters[6].Value = model.TelPhone;
            parameters[7].Value = model.CelPhone;
            parameters[8].Value = model.IsDefault;
            parameters[9].Value = model.ShippingId;

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
        public bool Delete(int ShippingId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ShippingAddress ");
            strSql.Append(" where ShippingId=@ShippingId");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingId", SqlDbType.Int,4)
			};
            parameters[0].Value = ShippingId;

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
        public bool DeleteList(string ShippingIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ShippingAddress ");
            strSql.Append(" where ShippingId in (" + ShippingIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Shipping.ShippingAddress GetModel(int ShippingId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ShippingId,RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone,IsDefault from Shop_ShippingAddress ");
            strSql.Append(" where ShippingId=@ShippingId");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingId", SqlDbType.Int,4)
			};
            parameters[0].Value = ShippingId;

            Maticsoft.Model.Shop.Shipping.ShippingAddress model = new Maticsoft.Model.Shop.Shipping.ShippingAddress();
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
        public Maticsoft.Model.Shop.Shipping.ShippingAddress DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = new Maticsoft.Model.Shop.Shipping.ShippingAddress();
            if (row != null)
            {
                if (row["ShippingId"] != null && row["ShippingId"].ToString() != "")
                {
                    model.ShippingId = int.Parse(row["ShippingId"].ToString());
                }
                if (row["RegionId"] != null && row["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["ShipName"] != null)
                {
                    model.ShipName = row["ShipName"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Zipcode"] != null)
                {
                    model.Zipcode = row["Zipcode"].ToString();
                }
                if (row["EmailAddress"] != null)
                {
                    model.EmailAddress = row["EmailAddress"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    model.TelPhone = row["TelPhone"].ToString();
                }
                if (row["CelPhone"] != null)
                {
                    model.CelPhone = row["CelPhone"].ToString();
                }
                if (row["IsDefault"] != null)
                {
                    model.IsDefault = Convert.ToBoolean(row["IsDefault"]);
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
            strSql.Append("select ShippingId,RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone,IsDefault ");
            strSql.Append(" FROM Shop_ShippingAddress ");
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
            strSql.Append(" ShippingId,RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone,IsDefault ");
            strSql.Append(" FROM Shop_ShippingAddress ");
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
            strSql.Append("select count(1) FROM Shop_ShippingAddress ");
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
                strSql.Append("order by T.ShippingId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ShippingAddress T ");
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
            parameters[0].Value = "Shop_ShippingAddress";
            parameters[1].Value = "ShippingId";
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

