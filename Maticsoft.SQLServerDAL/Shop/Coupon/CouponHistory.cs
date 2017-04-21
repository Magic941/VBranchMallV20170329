/**
* CouponHistory.cs
*
* 功 能： N/A
* 类 名： CouponHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:20:57   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Coupon;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Coupon
{
	/// <summary>
	/// 数据访问类:CouponHistory
	/// </summary>
	public partial class CouponHistory:ICouponHistory
	{
		public CouponHistory()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CouponCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CouponHistory");
            strSql.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)			};
            parameters[0].Value = CouponCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Coupon.CouponHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_CouponHistory(");
            strSql.Append("CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate)");
            strSql.Append(" values (");
            strSql.Append("@CouponCode,@CategoryId,@ClassId,@SupplierId,@RuleId,@CouponName,@CouponPwd,@UserId,@UserEmail,@Status,@CouponPrice,@LimitPrice,@NeedPoint,@IsPwd,@IsReuse,@StartDate,@EndDate,@GenerateTime,@UsedDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@CouponName", SqlDbType.NVarChar,200),
					new SqlParameter("@CouponPwd", SqlDbType.NVarChar,200),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CouponPrice", SqlDbType.Money,8),
					new SqlParameter("@LimitPrice", SqlDbType.Money,8),
					new SqlParameter("@NeedPoint", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Int,4),
					new SqlParameter("@IsReuse", SqlDbType.Int,4),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@GenerateTime", SqlDbType.DateTime),
					new SqlParameter("@UsedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.CouponCode;
            parameters[1].Value = model.CategoryId;
            parameters[2].Value = model.ClassId;
            parameters[3].Value = model.SupplierId;
            parameters[4].Value = model.RuleId;
            parameters[5].Value = model.CouponName;
            parameters[6].Value = model.CouponPwd;
            parameters[7].Value = model.UserId;
            parameters[8].Value = model.UserEmail;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.CouponPrice;
            parameters[11].Value = model.LimitPrice;
            parameters[12].Value = model.NeedPoint;
            parameters[13].Value = model.IsPwd;
            parameters[14].Value = model.IsReuse;
            parameters[15].Value = model.StartDate;
            parameters[16].Value = model.EndDate;
            parameters[17].Value = model.GenerateTime;
            parameters[18].Value = model.UsedDate;

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
        public bool Update(Maticsoft.Model.Shop.Coupon.CouponHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponHistory set ");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("RuleId=@RuleId,");
            strSql.Append("CouponName=@CouponName,");
            strSql.Append("CouponPwd=@CouponPwd,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserEmail=@UserEmail,");
            strSql.Append("Status=@Status,");
            strSql.Append("CouponPrice=@CouponPrice,");
            strSql.Append("LimitPrice=@LimitPrice,");
            strSql.Append("NeedPoint=@NeedPoint,");
            strSql.Append("IsPwd=@IsPwd,");
            strSql.Append("IsReuse=@IsReuse,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("GenerateTime=@GenerateTime,");
            strSql.Append("UsedDate=@UsedDate");
            strSql.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@CouponName", SqlDbType.NVarChar,200),
					new SqlParameter("@CouponPwd", SqlDbType.NVarChar,200),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CouponPrice", SqlDbType.Money,8),
					new SqlParameter("@LimitPrice", SqlDbType.Money,8),
					new SqlParameter("@NeedPoint", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Int,4),
					new SqlParameter("@IsReuse", SqlDbType.Int,4),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@GenerateTime", SqlDbType.DateTime),
					new SqlParameter("@UsedDate", SqlDbType.DateTime),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.SupplierId;
            parameters[3].Value = model.RuleId;
            parameters[4].Value = model.CouponName;
            parameters[5].Value = model.CouponPwd;
            parameters[6].Value = model.UserId;
            parameters[7].Value = model.UserEmail;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.CouponPrice;
            parameters[10].Value = model.LimitPrice;
            parameters[11].Value = model.NeedPoint;
            parameters[12].Value = model.IsPwd;
            parameters[13].Value = model.IsReuse;
            parameters[14].Value = model.StartDate;
            parameters[15].Value = model.EndDate;
            parameters[16].Value = model.GenerateTime;
            parameters[17].Value = model.UsedDate;
            parameters[18].Value = model.CouponCode;

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
        public bool Delete(string CouponCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CouponHistory ");
            strSql.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)			};
            parameters[0].Value = CouponCode;

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
        public bool DeleteList(string CouponCodelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CouponHistory ");
            strSql.Append(" where CouponCode in (" + CouponCodelist + ")  ");
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
        public Maticsoft.Model.Shop.Coupon.CouponHistory GetModel(string CouponCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate from Shop_CouponHistory ");
            strSql.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)			};
            parameters[0].Value = CouponCode;

            Maticsoft.Model.Shop.Coupon.CouponHistory model = new Maticsoft.Model.Shop.Coupon.CouponHistory();
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
        public Maticsoft.Model.Shop.Coupon.CouponHistory DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponHistory model = new Maticsoft.Model.Shop.Coupon.CouponHistory();
            if (row != null)
            {
                if (row["CouponCode"] != null)
                {
                    model.CouponCode = row["CouponCode"].ToString();
                }
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["ClassId"] != null && row["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["RuleId"] != null && row["RuleId"].ToString() != "")
                {
                    model.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if (row["CouponName"] != null)
                {
                    model.CouponName = row["CouponName"].ToString();
                }
                if (row["CouponPwd"] != null)
                {
                    model.CouponPwd = row["CouponPwd"].ToString();
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserEmail"] != null)
                {
                    model.UserEmail = row["UserEmail"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["CouponPrice"] != null && row["CouponPrice"].ToString() != "")
                {
                    model.CouponPrice = decimal.Parse(row["CouponPrice"].ToString());
                }
                if (row["LimitPrice"] != null && row["LimitPrice"].ToString() != "")
                {
                    model.LimitPrice = decimal.Parse(row["LimitPrice"].ToString());
                }
                if (row["NeedPoint"] != null && row["NeedPoint"].ToString() != "")
                {
                    model.NeedPoint = int.Parse(row["NeedPoint"].ToString());
                }
                if (row["IsPwd"] != null && row["IsPwd"].ToString() != "")
                {
                    model.IsPwd = int.Parse(row["IsPwd"].ToString());
                }
                if (row["IsReuse"] != null && row["IsReuse"].ToString() != "")
                {
                    model.IsReuse = int.Parse(row["IsReuse"].ToString());
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["GenerateTime"] != null && row["GenerateTime"].ToString() != "")
                {
                    model.GenerateTime = DateTime.Parse(row["GenerateTime"].ToString());
                }
                if (row["UsedDate"] != null && row["UsedDate"].ToString() != "")
                {
                    model.UsedDate = DateTime.Parse(row["UsedDate"].ToString());
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
            strSql.Append("select CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate ");
            strSql.Append(" FROM Shop_CouponHistory ");
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
            strSql.Append(" CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate ");
            strSql.Append(" FROM Shop_CouponHistory ");
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
            strSql.Append("select count(1) FROM Shop_CouponHistory ");
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
                strSql.Append("order by T.CouponCode desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_CouponHistory T ");
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
            parameters[0].Value = "Shop_CouponHistory";
            parameters[1].Value = "CouponCode";
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

