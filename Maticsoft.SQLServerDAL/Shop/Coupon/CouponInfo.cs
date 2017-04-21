/**
* CouponInfo.cs
*
* 功 能： N/A
* 类 名： CouponInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:20:59   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Coupon;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Coupon
{
    /// <summary>
    /// 数据访问类:CouponInfo
    /// </summary>
    public partial class CouponInfo : ICouponInfo
    {
        public CouponInfo()
        { }

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CouponCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CouponInfo");
            strSql.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)			};
            parameters[0].Value = CouponCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_CouponInfo(");
            strSql.Append("CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate,UseType,AutoState)");
            strSql.Append(" values (");
            strSql.Append("@CouponCode,@CategoryId,@ClassId,@SupplierId,@RuleId,@CouponName,@CouponPwd,@UserId,@UserEmail,@Status,@CouponPrice,@LimitPrice,@NeedPoint,@IsPwd,@IsReuse,@StartDate,@EndDate,@GenerateTime,@UsedDate,@UseType,@AutoState)");
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
					new SqlParameter("@UsedDate", SqlDbType.DateTime),
                    new SqlParameter("@UseType",SqlDbType.SmallInt),
                    new SqlParameter("@AutoState",SqlDbType.SmallInt)};
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
            parameters[19].Value = model.UseType;
            parameters[20].Value = model.AutoState;

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
        public bool Update(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
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
            strSql.Append("delete from Shop_CouponInfo ");
            strSql.Append(" where CouponCode='@CouponCode' ");
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
            strSql.Append("delete from Shop_CouponInfo ");
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
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetModel(string CouponCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate from Shop_CouponInfo ");
            strSql.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)			};
            parameters[0].Value = CouponCode;

            Maticsoft.Model.Shop.Coupon.CouponInfo model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
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
        public Maticsoft.Model.Shop.Coupon.CouponInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponInfo model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetModelFromDataRow(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponInfo model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
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
            strSql.Append("select CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate,CardNo,Batch ");
            strSql.Append(" FROM Shop_CouponInfo ");
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
            strSql.Append(" CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate,CardNo,Batch ");
            strSql.Append(" FROM Shop_CouponInfo ");
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
            strSql.Append("select count(1) FROM Shop_CouponInfo ");
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
            strSql.Append(")AS Row, T.*  from Shop_CouponInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetListByPage(string strwhere, string orderby, int startIndex, int endIndex, out int total)
        {
            total = GetRecordCount(strwhere);
            var x = GetListByPage(strwhere, orderby, startIndex, endIndex);
            return x;
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
            parameters[0].Value = "Shop_CouponInfo";
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

        public bool AddHistory(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();
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
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            //删除指令
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from Shop_CouponInfo ");
            strSql1.Append(" where CouponCode=@CouponCode  ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
                                         };
            parameters2[0].Value = model.CouponCode;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters2);
            sqllist.Add(cmd1);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;

        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, bool IsExpired)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  * from Shop_CouponInfo");
            strSql.Append(" where CouponCode=@CouponCode ");
            if (!IsExpired)
            {
                strSql.AppendFormat(" and  EndDate>='{0}'", DateTime.Now);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)			};
            parameters[0].Value = CouponCode;

            Maticsoft.Model.Shop.Coupon.CouponInfo model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return GetModelFromDataRow(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, string pwd, bool IsExpired)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from Shop_CouponInfo");
            strSql.Append(" where CouponCode=@CouponCode  and CouponPwd=@CouponPwd");
            if (!IsExpired)
            {
                strSql.AppendFormat(" and  EndDate>='{0}'", DateTime.Now);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
                                        new SqlParameter("@CouponPwd", SqlDbType.NVarChar,200)};
            parameters[0].Value = CouponCode;
            parameters[1].Value = pwd;

            Maticsoft.Model.Shop.Coupon.CouponInfo model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
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
        /// 分配给用户优惠券
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool UpdateUser(string couponCode, int userId, string userEmail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserEmail=@UserEmail,");
            strSql.Append("Status=@Status");
            strSql.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)};
            parameters[0].Value = userId;
            parameters[1].Value = userEmail;
            parameters[2].Value = 1;
            parameters[3].Value = couponCode;

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
        /// 分配给用户优惠券(根据优惠券规则)
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool UpdateUser(int ruleId, int userId, string userEmail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserEmail=@UserEmail,");
            strSql.Append("Status=@Status");
            strSql.Append(" where CouponCode=(SELECT TOP 1 CouponCode FROM Shop_CouponInfo WHERE ruleId=@ruleId AND Status=0 )  ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ruleId", SqlDbType.Int,4)};
            parameters[0].Value = userId;
            parameters[1].Value = userEmail;
            parameters[2].Value = 1;
            parameters[3].Value = ruleId;

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
        /// 使用优惠券
        /// </summary>
        /// <param name="couponCode"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool UseCoupon(string couponCode, int userId, string userEmail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserEmail=@UserEmail,");
            strSql.Append("Status=@Status,");
            strSql.Append("UsedDate=@UsedDate");
            strSql.Append(" where CouponCode=@CouponCode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
                    	new SqlParameter("@UsedDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = userId;
            parameters[1].Value = userEmail;
            parameters[2].Value = 2;
            parameters[3].Value = couponCode;
            parameters[4].Value = DateTime.Now;

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

        public bool UseCoupon(string couponCode, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("Status=@Status,");
            strSql.Append("UsedDate=@UsedDate");
            strSql.Append(" where CouponCode=@CouponCode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
                    	new SqlParameter("@UsedDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = userId;
            parameters[1].Value = 2;
            parameters[2].Value = couponCode;
            parameters[3].Value = DateTime.Now;

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

        public bool UseCoupon(string couponCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
            strSql.Append("Status=@Status,");
            strSql.Append("UsedDate=@UsedDate");
            strSql.Append(" where CouponCode=@CouponCode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
                    	new SqlParameter("@UsedDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = 2;
            parameters[1].Value = couponCode;
            parameters[2].Value = DateTime.Now;

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


        public bool DeleteEx(int RuleId)
        {
            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from Shop_CouponInfo ");
            strSql1.Append(" where RuleId=@RuleId  ");
            SqlParameter[] parameters1 = {
						new SqlParameter("@RuleId", SqlDbType.Int,4)
                                         };
            parameters1[0].Value = RuleId;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from Shop_CouponHistory ");
            strSql2.Append(" where RuleId=@RuleId  ");
            SqlParameter[] parameters2 = {
						new SqlParameter("@RuleId", SqlDbType.Int,4)
                                         };
            parameters2[0].Value = RuleId;
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd2);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
        }
        /// <summary>
        /// 随机获取各个状态的优惠券
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetActCoupon(int ruleId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Shop_CouponInfo ");
            strSql.Append(" where Status=@Status ");
            if (ruleId > 0)
            {
                strSql.Append(" and  RuleId=@RuleId ");
            }
            strSql.Append("  order By NewID()");

            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4)	,
		            new SqlParameter("@RuleId", SqlDbType.Int,4)		
                                        };
            parameters[0].Value = status;
            parameters[1].Value = ruleId;
            Maticsoft.Model.Shop.Coupon.CouponInfo model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
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
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByUser(int userId, int ruleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CouponInfo");
            strSql.Append(" where UserId=@UserId ");
            if (ruleId > 0)
            {
                strSql.Append(" and  RuleId= " + ruleId);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)			};
            parameters[0].Value = userId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistsByUser(string email, int ruleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CouponInfo");
            strSql.Append(" where UserEmail=@UserEmail ");
            if (ruleId > 0)
            {
                strSql.Append(" and  RuleId=" + ruleId);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,200)			
                                    };
            parameters[0].Value = email;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool UpdateStatusList(string ids, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
            if (status == 0)
            {
                strSql.Append("UserId=0,");
                strSql.Append("UserEmail='',");
            }
            strSql.Append("Status=@Status");
            strSql.Append(" where CouponCode in (" + ids + ")  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
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

        public DataSet GetCouponList(int userId, int status, bool IsExpired)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select    a.*,b.Name  from Shop_CouponInfo a INNER JOIN dbo.Shop_CouponClass b ON a.ClassId = b.ClassId");
            //strSql.Append(" where a.UserId=@UserId  and a.Status=@Status");

            strSql.Append("select    *  from Shop_CouponInfo");
            strSql.Append(" where UserId=@UserId  and Status=@Status");
            if (!IsExpired)
            {
                strSql.AppendFormat(" and  EndDate>='{0}'", DateTime.Now);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.NVarChar,200),
                                        new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
            parameters[0].Value = userId.ToString();
            parameters[1].Value = status;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #region 微信优惠劵
        public DataSet GetCouponList(int userId, int status,int isweixin, bool IsExpired)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select    a.*,b.Name  from Shop_CouponInfo a INNER JOIN dbo.Shop_CouponClass b ON a.ClassId = b.ClassId");
            //strSql.Append(" where a.UserId=@UserId  and a.Status=@Status");

            strSql.Append("select    *  from Shop_CouponInfo");
            strSql.Append(" where UserId=@UserId  and Status=@Status and UseType=@UseType ");
            if (!IsExpired)
            {
                strSql.AppendFormat(" and  EndDate>='{0}'", DateTime.Now);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.NVarChar,200),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@UseType", SqlDbType.SmallInt)
                                        };
            parameters[0].Value = userId.ToString();
            parameters[1].Value = status;
            parameters[2].Value = isweixin;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion

        public bool IsEffect(string coupon)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1)  from Shop_CouponInfo");
            strSql.Append(" where CouponCode=@CouponCode   ");
            strSql.AppendFormat(" and  EndDate>='{0}'", DateTime.Now);
            SqlParameter[] parameters = {
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = coupon;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return false;
            }
            else
            {
                return Convert.ToInt32(obj) > 0;
            }
        }


        public int GetRuleId(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Top 1 RuleId FROM Shop_CouponInfo A  WHERE NOT EXISTS(SELECT * FROM Shop_CouponInfo  B WHERE B.UserId=@UserId and B.RuleId=A.RuleId)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.NVarChar,200)};
            parameters[0].Value = userId.ToString();
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Common.Globals.SafeInt(obj, 0);
            }
        }
        public int GetRuleId(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Top 1 RuleId FROM Shop_CouponInfo A  WHERE NOT EXISTS(SELECT * FROM Shop_CouponInfo  B WHERE B.UserEmail=@UserEmail and B.RuleId=A.RuleId)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,200)
                                      
                                        };
            parameters[0].Value = UserName;
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
        /// 
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Coupon.CouponInfo GetAwardCode(int activityId, bool IsExpired)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Top 1 *  FROM  Shop_CouponInfo C");
            strSql.Append(" where C.Status=0 and EXISTS(SELECT TargetId  FROM WeChat_ActivityAward A WHERE ActivityId=@ActivityId AND A.TargetId= c.RuleId)");
            if (!IsExpired)
            {
                strSql.AppendFormat("  and  C.EndDate>='{0}'", DateTime.Now);
            }
            strSql.AppendFormat(" order  By NewID()");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = activityId;
            Maticsoft.Model.Shop.Coupon.CouponInfo model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
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


        public bool BindCoupon(string Code, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponInfo set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserEmail='' ,");
            strSql.Append("Status=1");
            strSql.Append(" where CouponCode=@CouponCode and  Status=@Status ");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@CouponCode", SqlDbType.NVarChar,200),
                    new SqlParameter("@UserId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = 0;
            parameters[1].Value = Code;
            parameters[2].Value = userId;

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


        #endregion  ExtensionMethod
    }
}

