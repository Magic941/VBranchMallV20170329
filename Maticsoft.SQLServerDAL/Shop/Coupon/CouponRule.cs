/**
* CouponRule.cs
*
* 功 能： N/A
* 类 名： CouponRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:21:01   N/A    初版
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
	/// 数据访问类:CouponRule
	/// </summary>
	public partial class CouponRule:ICouponRule
	{
		public CouponRule()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("RuleId", "Shop_CouponRule");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RuleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CouponRule");
            strSql.Append(" where RuleId=@RuleId");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
            parameters[0].Value = RuleId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Coupon.CouponRule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_CouponRule(");
            strSql.Append("CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId,Type,CpLength,PwdLength,UseType,AutoState)");
            strSql.Append(" values (");
            strSql.Append("@CategoryId,@ClassId,@SupplierId,@Name,@PreName,@ImageUrl,@CouponPrice,@LimitPrice,@CouponDesc,@SendCount,@NeedPoint,@IsPwd,@IsReuse,@Status,@Recommend,@StartDate,@EndDate,@CreateDate,@CreateUserId,@Type,@CpLength,@PwdLength,@UseType,@AutoState)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@PreName", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@CouponPrice", SqlDbType.Money,8),
					new SqlParameter("@LimitPrice", SqlDbType.Money,8),
					new SqlParameter("@CouponDesc", SqlDbType.NVarChar,-1),
					new SqlParameter("@SendCount", SqlDbType.Int,4),
					new SqlParameter("@NeedPoint", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Int,4),
					new SqlParameter("@IsReuse", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Recommend", SqlDbType.Int,4),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUserId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CpLength", SqlDbType.Int,4),
					new SqlParameter("@PwdLength", SqlDbType.Int,4),
                    new SqlParameter("@UseType",SqlDbType.SmallInt),
                    new SqlParameter("@AutoState",SqlDbType.SmallInt)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.SupplierId;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.PreName;
            parameters[5].Value = model.ImageUrl;
            parameters[6].Value = model.CouponPrice;
            parameters[7].Value = model.LimitPrice;
            parameters[8].Value = model.CouponDesc;
            parameters[9].Value = model.SendCount;
            parameters[10].Value = model.NeedPoint;
            parameters[11].Value = model.IsPwd;
            parameters[12].Value = model.IsReuse;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.Recommend;
            parameters[15].Value = model.StartDate;
            parameters[16].Value = model.EndDate;
            parameters[17].Value = model.CreateDate;
            parameters[18].Value = model.CreateUserId;
            parameters[19].Value = model.Type;
            parameters[20].Value = model.CpLength;
            parameters[21].Value = model.PwdLength;
            parameters[22].Value = model.UseType;
            parameters[23].Value = model.AutoState;

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
        public bool Update(Maticsoft.Model.Shop.Coupon.CouponRule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponRule set ");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("Name=@Name,");
            strSql.Append("PreName=@PreName,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("CouponPrice=@CouponPrice,");
            strSql.Append("LimitPrice=@LimitPrice,");
            strSql.Append("CouponDesc=@CouponDesc,");
            strSql.Append("SendCount=@SendCount,");
            strSql.Append("NeedPoint=@NeedPoint,");
            strSql.Append("IsPwd=@IsPwd,");
            strSql.Append("IsReuse=@IsReuse,");
            strSql.Append("Status=@Status,");
            strSql.Append("Recommend=@Recommend,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CreateUserId=@CreateUserId,");
            strSql.Append("Type=@Type,");
            strSql.Append("CpLength=@CpLength,");
            strSql.Append("PwdLength=@PwdLength");
            strSql.Append(" where RuleId=@RuleId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@PreName", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@CouponPrice", SqlDbType.Money,8),
					new SqlParameter("@LimitPrice", SqlDbType.Money,8),
					new SqlParameter("@CouponDesc", SqlDbType.NVarChar,-1),
					new SqlParameter("@SendCount", SqlDbType.Int,4),
					new SqlParameter("@NeedPoint", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Int,4),
					new SqlParameter("@IsReuse", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Recommend", SqlDbType.Int,4),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUserId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CpLength", SqlDbType.Int,4),
					new SqlParameter("@PwdLength", SqlDbType.Int,4),
					new SqlParameter("@RuleId", SqlDbType.Int,4)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.SupplierId;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.PreName;
            parameters[5].Value = model.ImageUrl;
            parameters[6].Value = model.CouponPrice;
            parameters[7].Value = model.LimitPrice;
            parameters[8].Value = model.CouponDesc;
            parameters[9].Value = model.SendCount;
            parameters[10].Value = model.NeedPoint;
            parameters[11].Value = model.IsPwd;
            parameters[12].Value = model.IsReuse;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.Recommend;
            parameters[15].Value = model.StartDate;
            parameters[16].Value = model.EndDate;
            parameters[17].Value = model.CreateDate;
            parameters[18].Value = model.CreateUserId;
            parameters[19].Value = model.Type;
            parameters[20].Value = model.CpLength;
            parameters[21].Value = model.PwdLength;
            parameters[22].Value = model.RuleId;

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
        public bool Delete(int RuleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CouponRule ");
            strSql.Append(" where RuleId=@RuleId");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
            parameters[0].Value = RuleId;

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
        public bool DeleteList(string RuleIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CouponRule ");
            strSql.Append(" where RuleId in (" + RuleIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Coupon.CouponRule GetModel(int RuleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RuleId,CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId,Type,CpLength,PwdLength,UseType,AutoState from Shop_CouponRule ");
            strSql.Append(" where RuleId=@RuleId");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
            parameters[0].Value = RuleId;

            Maticsoft.Model.Shop.Coupon.CouponRule model = new Maticsoft.Model.Shop.Coupon.CouponRule();
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
        public Maticsoft.Model.Shop.Coupon.CouponRule DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponRule model = new Maticsoft.Model.Shop.Coupon.CouponRule();
            if (row != null)
            {
                if (row["RuleId"] != null && row["RuleId"].ToString() != "")
                {
                    model.RuleId = int.Parse(row["RuleId"].ToString());
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
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["PreName"] != null)
                {
                    model.PreName = row["PreName"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["CouponPrice"] != null && row["CouponPrice"].ToString() != "")
                {
                    model.CouponPrice = decimal.Parse(row["CouponPrice"].ToString());
                }
                if (row["LimitPrice"] != null && row["LimitPrice"].ToString() != "")
                {
                    model.LimitPrice = decimal.Parse(row["LimitPrice"].ToString());
                }
                if (row["CouponDesc"] != null)
                {
                    model.CouponDesc = row["CouponDesc"].ToString();
                }
                if (row["SendCount"] != null && row["SendCount"].ToString() != "")
                {
                    model.SendCount = int.Parse(row["SendCount"].ToString());
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
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Recommend"] != null && row["Recommend"].ToString() != "")
                {
                    model.Recommend = int.Parse(row["Recommend"].ToString());
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["CreateUserId"] != null && row["CreateUserId"].ToString() != "")
                {
                    model.CreateUserId = int.Parse(row["CreateUserId"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["CpLength"] != null && row["CpLength"].ToString() != "")
                {
                    model.CpLength = int.Parse(row["CpLength"].ToString());
                }
                if (row["PwdLength"] != null && row["PwdLength"].ToString() != "")
                {
                    model.PwdLength = int.Parse(row["PwdLength"].ToString());
                }

                if (row["UseType"] != null && row["UseType"].ToString() != "")
                {
                    model.UseType = Int16.Parse(row["UseType"].ToString());
                }
                if (row["AutoState"] != null && row["AutoState"].ToString() != "")
                {
                    model.AutoState = Int16.Parse(row["AutoState"].ToString());
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
            strSql.Append("select RuleId,CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId,Type,CpLength,PwdLength,UseType,AutoState ");
            strSql.Append(" FROM Shop_CouponRule ");
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
            strSql.Append(" RuleId,CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId,Type,CpLength,PwdLength,UseType,AutoState ");
            strSql.Append(" FROM Shop_CouponRule ");
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
            strSql.Append("select count(1) FROM Shop_CouponRule ");
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
                strSql.Append("order by T.RuleId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_CouponRule T ");
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
            parameters[0].Value = "Shop_CouponRule";
            parameters[1].Value = "RuleId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

		#region  ExtensionMethod
        /// <summary>
        ///级联删除数据
        /// </summary>
        public bool DeleteEx(int RuleId)
        {
            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CouponRule ");
            strSql.Append(" where RuleId=@RuleId");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
            parameters[0].Value = RuleId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from Shop_CouponInfo ");
            strSql1.Append(" where RuleId=@RuleId  ");
            SqlParameter[] parameters1 = {
						new SqlParameter("@RuleId", SqlDbType.Int,4)
                                         };
            parameters1[0].Value = RuleId;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            StringBuilder strSql2= new StringBuilder();
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


	    public bool GenCoupon(Maticsoft.Model.Shop.Coupon.CouponInfo infoModel)
	    {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //添加优惠券
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into Shop_CouponInfo(");
            strSql3.Append("CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate)");
            strSql3.Append(" values (");
            strSql3.Append("@CouponCode,@CategoryId,@ClassId,@SupplierId,@RuleId,@CouponName,@CouponPwd,@UserId,@UserEmail,@Status,@CouponPrice,@LimitPrice,@NeedPoint,@IsPwd,@IsReuse,@StartDate,@EndDate,@GenerateTime,@UsedDate)");
            SqlParameter[] parameters3 = {
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
            parameters3[0].Value = infoModel.CouponCode;
            parameters3[1].Value = infoModel.CategoryId;
            parameters3[2].Value = infoModel.ClassId;
            parameters3[3].Value = infoModel.SupplierId;
            parameters3[4].Value = infoModel.RuleId;
            parameters3[5].Value = infoModel.CouponName;
            parameters3[6].Value = infoModel.CouponPwd;
            parameters3[7].Value = infoModel.UserId;
            parameters3[8].Value = infoModel.UserEmail;
            parameters3[9].Value = infoModel.Status;
            parameters3[10].Value = infoModel.CouponPrice;
            parameters3[11].Value = infoModel.LimitPrice;
            parameters3[12].Value = infoModel.NeedPoint;
            parameters3[13].Value = infoModel.IsPwd;
            parameters3[14].Value = infoModel.IsReuse;
            parameters3[15].Value = infoModel.StartDate;
            parameters3[16].Value = infoModel.EndDate;
            parameters3[17].Value = infoModel.GenerateTime;
            parameters3[18].Value = infoModel.UsedDate;
            CommandInfo cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //添加兑换记录
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("insert into Shop_ExchangeDetail(");
            strSql4.Append("Type,GiftID,UserID,OrderID,GiftName,Price,CouponCode,CostScore,Status,Description,CreatedDate)");
            strSql4.Append(" values (");
            strSql4.Append("@Type,@GiftID,@UserID,@OrderID,@GiftName,@Price,@CouponCode,@CostScore,@Status,@Description,@CreatedDate)");
            strSql4.Append(";select @@IDENTITY");
            SqlParameter[] parameters4 = {
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
            parameters4[0].Value =1;
            parameters4[1].Value = 0;
            parameters4[2].Value = infoModel.UserId;
            parameters4[3].Value = 0;
            parameters4[4].Value = "";
            parameters4[5].Value = infoModel.CouponPrice;
            parameters4[6].Value = infoModel.CouponCode;
            parameters4[7].Value = infoModel.NeedPoint;
            parameters4[8].Value = 1;
            parameters4[9].Value = "积分兑换优惠券";
            parameters4[10].Value = infoModel.GenerateTime;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            //添加积分明细
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_PointsDetail(");
            strSql.Append("RuleId,UserID,Score,ExtData,CurrentPoints,Description,CreatedDate,Type)");
            strSql.Append(" values (");
            strSql.Append("@RuleId,@UserID,@Score,@ExtData,0,@Description,@CreatedDate,@Type)");
            SqlParameter[] parameters = {
                    new SqlParameter("@RuleId", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Score", SqlDbType.Int,4),
                    new SqlParameter("@ExtData", SqlDbType.NVarChar),
                    new SqlParameter("@Description", SqlDbType.NVarChar),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
            new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = -1;
            parameters[1].Value = infoModel.UserId;
            parameters[2].Value = infoModel.NeedPoint;
            parameters[3].Value = "";
            parameters[4].Value = "兑换优惠券";
            parameters[5].Value = infoModel.GenerateTime;
            parameters[6].Value = 1;
             cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //更新个人积分数
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update Accounts_UsersExp set ");
        
                strSql2.Append("Points=Points-@Points");
        
            strSql2.Append(" where UserID=@UserID ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@Points", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4)
                                        };
            parameters2[0].Value = infoModel.NeedPoint;
            parameters2[1].Value = infoModel.UserId;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
	    }


       public int  ImportExcelData(Maticsoft.Model.Shop.Coupon.CouponRule ruleModel, bool IsDate, bool IsPrice, bool IsLimitPrice, DataTable dt)
        {
            string connectionString = PubConstant.GetConnectionString("ConnectionString");
            int count = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    int rowsCount = dt.Rows.Count;
                    Maticsoft.Model.Shop.Coupon.CouponInfo model;

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into Shop_CouponRule(");
                    strSql.Append("CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId,Type,CpLength,PwdLength)");
                    strSql.Append(" values (");
                    strSql.Append("@CategoryId,@ClassId,@SupplierId,@Name,@PreName,@ImageUrl,@CouponPrice,@LimitPrice,@CouponDesc,@SendCount,@NeedPoint,@IsPwd,@IsReuse,@Status,@Recommend,@StartDate,@EndDate,@CreateDate,@CreateUserId,@Type,@CpLength,@PwdLength)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@PreName", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@CouponPrice", SqlDbType.Money,8),
					new SqlParameter("@LimitPrice", SqlDbType.Money,8),
					new SqlParameter("@CouponDesc", SqlDbType.NVarChar,-1),
					new SqlParameter("@SendCount", SqlDbType.Int,4),
					new SqlParameter("@NeedPoint", SqlDbType.Int,4),
					new SqlParameter("@IsPwd", SqlDbType.Int,4),
					new SqlParameter("@IsReuse", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Recommend", SqlDbType.Int,4),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUserId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@CpLength", SqlDbType.Int,4),
					new SqlParameter("@PwdLength", SqlDbType.Int,4)};
                    parameters[0].Value = ruleModel.CategoryId;
                    parameters[1].Value = ruleModel.ClassId;
                    parameters[2].Value = ruleModel.SupplierId;
                    parameters[3].Value = ruleModel.Name;
                    parameters[4].Value = ruleModel.PreName;
                    parameters[5].Value = ruleModel.ImageUrl;
                    parameters[6].Value = ruleModel.CouponPrice;
                    parameters[7].Value = ruleModel.LimitPrice;
                    parameters[8].Value = ruleModel.CouponDesc;
                    parameters[9].Value = ruleModel.SendCount;
                    parameters[10].Value = ruleModel.NeedPoint;
                    parameters[11].Value = ruleModel.IsPwd;
                    parameters[12].Value = ruleModel.IsReuse;
                    parameters[13].Value = ruleModel.Status;
                    parameters[14].Value = ruleModel.Recommend;
                    parameters[15].Value = ruleModel.StartDate;
                    parameters[16].Value = ruleModel.EndDate;
                    parameters[17].Value = ruleModel.CreateDate;
                    parameters[18].Value = ruleModel.CreateUserId;
                    parameters[19].Value = ruleModel.Type;
                    parameters[20].Value = ruleModel.CpLength;
                    parameters[21].Value = ruleModel.PwdLength;

                    object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                    int ruleId = Common.Globals.SafeInt(obj, 0);
                    if (ruleId == 0)
                    {
                        return 0;
                    }

                    StringBuilder infostrSql = new StringBuilder();
                    infostrSql.Append("insert into Shop_CouponInfo(");
                    infostrSql.Append("CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate)");
                    infostrSql.Append(" values (");
                    infostrSql.Append("@CouponCode,@CategoryId,@ClassId,@SupplierId,@RuleId,@CouponName,@CouponPwd,@UserId,@UserEmail,@Status,@CouponPrice,@LimitPrice,@NeedPoint,@IsPwd,@IsReuse,@StartDate,@EndDate,@GenerateTime,@UsedDate)");
                    SqlParameter[] infoparameters = {
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

                    cmd.Connection = conn;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        #region 插入优惠券数据
                        model = new Maticsoft.Model.Shop.Coupon.CouponInfo();
                        if (dt.Rows[n]["优惠券卡号"] != null && dt.Rows[n]["优惠券卡号"].ToString() != "")
                        {
                            model.CouponCode = dt.Rows[n]["优惠券卡号"].ToString();
                        }
                        if (dt.Rows[n]["面值"] != null && dt.Rows[n]["面值"].ToString() != "")
                        {
                            model.CouponPrice =Common.Globals.SafeDecimal( dt.Rows[n]["面值"].ToString(),0);
                        }
                        if (dt.Rows[n]["最低消费金额"] != null && dt.Rows[n]["最低消费金额"].ToString() != "")
                        {
                            model.LimitPrice = Common.Globals.SafeDecimal(dt.Rows[n]["最低消费金额"].ToString(), 0);
                        }
                        if (dt.Rows[n]["开始时间"] != null && dt.Rows[n]["开始时间"].ToString() != "")
                        {
                            model.StartDate =Common.Globals.SafeDateTime(dt.Rows[n]["开始时间"].ToString(),DateTime.Now);
                        }
                        if (dt.Rows[n]["结束时间"] != null && dt.Rows[n]["结束时间"].ToString() != "")
                        {
                            model.EndDate = Common.Globals.SafeDateTime(dt.Rows[n]["结束时间"].ToString(), DateTime.Now);
                        }

                        if (String.IsNullOrWhiteSpace(model.CouponCode))
                        {
                            continue;
                        }
                        model.StartDate = IsDate ? ruleModel.StartDate:model.StartDate ;
                        model.EndDate = IsDate ? ruleModel.EndDate : model.EndDate;
                        model.LimitPrice = IsLimitPrice ? ruleModel.LimitPrice : model.LimitPrice;
                        model.CouponPrice = IsPrice ? ruleModel.CouponPrice : model.CouponPrice;
                        #region 判断重复
                            StringBuilder exsitSql = new StringBuilder();
                            exsitSql.Append("select count(1) from Shop_CouponInfo");
                            exsitSql.Append(" where CouponCode=@CouponCode  ");
                            SqlParameter[] exsitpars = {
					        new SqlParameter("@CouponCode", SqlDbType.NVarChar)};
                            exsitpars[0].Value = model.CouponCode;
                            cmd.CommandText = exsitSql.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(exsitpars);
                            object codeobj = cmd.ExecuteScalar();

                            if (Common.Globals.SafeInt(codeobj.ToString(), 0) > 0)
                            {
                                continue;
                            }

                        #endregion
                        model.Status = ruleModel.Status;
                        model.CategoryId = ruleModel.CategoryId;
                        model.ClassId = ruleModel.ClassId;
                        model.CouponName = ruleModel.Name;
                        model.CouponPwd = "";
                        model.GenerateTime = DateTime.Now;
                        model.IsPwd = ruleModel.IsPwd;
                        model.IsReuse = ruleModel.IsReuse;
                        model.NeedPoint = 0;
                        model.RuleId = ruleId;
                        model.SupplierId = ruleModel.SupplierId;
                        model.UserId = 0;

                        infoparameters[0].Value = model.CouponCode;
                        infoparameters[1].Value = model.CategoryId;
                        infoparameters[2].Value = model.ClassId;
                        infoparameters[3].Value = model.SupplierId;
                        infoparameters[4].Value = model.RuleId;
                        infoparameters[5].Value = model.CouponName;
                        infoparameters[6].Value = model.CouponPwd;
                        infoparameters[7].Value = model.UserId;
                        infoparameters[8].Value = model.UserEmail;
                        infoparameters[9].Value = model.Status;
                        infoparameters[10].Value = model.CouponPrice;
                        infoparameters[11].Value = model.LimitPrice;
                        infoparameters[12].Value = model.NeedPoint;
                        infoparameters[13].Value = model.IsPwd;
                        infoparameters[14].Value = model.IsReuse;
                        infoparameters[15].Value = model.StartDate;
                        infoparameters[16].Value = model.EndDate;
                        infoparameters[17].Value = model.GenerateTime;
                        infoparameters[18].Value = model.UsedDate;

                        cmd.CommandText = infostrSql.ToString();
                        cmd.Parameters.Clear();
                        foreach (SqlParameter parameter in infoparameters)
                        {
                            if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                                (parameter.Value == null))
                            {
                                parameter.Value = DBNull.Value;
                            }
                            cmd.Parameters.Add(parameter);
                        }
                        cmd.ExecuteNonQuery();
                        #endregion
                        count++;
                        
                    }
                    #region 更新活动Count
                    StringBuilder strSql1 = new StringBuilder();
                    strSql1.Append("update Shop_CouponRule set ");
                    strSql1.Append("SendCount=" + count);
                    strSql1.Append(" where RuleId=@RuleId");
                    SqlParameter[] parameters1 = { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
                    parameters1[0].Value = ruleModel.RuleId;
                    cmd.CommandText = strSql1.ToString();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parameters1);
                    cmd.ExecuteNonQuery();
                    #endregion
                }
            }
            return count;
        }

	    #endregion  ExtensionMethod
	}
}

