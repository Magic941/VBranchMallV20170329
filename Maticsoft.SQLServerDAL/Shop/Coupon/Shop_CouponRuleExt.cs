using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using Maticsoft.IDAL;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Coupon;//Please add references
using Maticsoft.Model.Shop.Coupon;
namespace Maticsoft.SQLServerDAL.Shop.Coupon
{
    /// <summary>
    /// 数据访问类:Shop_CouponRuleExt
    /// </summary>
    public partial class Shop_CouponRuleExt : IShop_CouponRuleExt
    {
        public Shop_CouponRuleExt()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CouponRuleExt");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_CouponRuleExt(");
            strSql.Append("batchID,ClassID,CouponCount)");
            strSql.Append(" values (");
            strSql.Append("@batchID,@ClassID,@CouponCount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@batchID", SqlDbType.VarChar,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@CouponCount", SqlDbType.Int,4)};
            parameters[0].Value = model.batchID;
            parameters[1].Value = model.ClassID;
            parameters[2].Value = model.CouponCount;

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
        public bool Update(Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponRuleExt set ");
            strSql.Append("batchID=@batchID,");
            strSql.Append("ClassID=@ClassID,");
            strSql.Append("CouponCount=@CouponCount");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@batchID", SqlDbType.VarChar,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@CouponCount", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.batchID;
            parameters[1].Value = model.ClassID;
            parameters[2].Value = model.CouponCount;
            parameters[3].Value = model.Id;

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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CouponRuleExt ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
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
            strSql.Append("delete from Shop_CouponRuleExt ");
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
        public Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,batchID,ClassID,CouponCount from Shop_CouponRuleExt ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model = new Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt();
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
        public Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt model = new Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["batchID"] != null)
                {
                    model.batchID = row["batchID"].ToString();
                }
                if (row["ClassID"] != null && row["ClassID"].ToString() != "")
                {
                    model.ClassID = int.Parse(row["ClassID"].ToString());
                }
                if (row["CouponCount"] != null && row["CouponCount"].ToString() != "")
                {
                    model.CouponCount = int.Parse(row["CouponCount"].ToString());
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
            strSql.Append("select Id,batchID,ClassID,CouponCount ");
            strSql.Append(" FROM Shop_CouponRuleExt ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,a.batchID,a.ClassID,a.CouponCount,b.Name ");
            strSql.Append(" FROM Shop_CouponRuleExt a,Shop_CouponClass b ");
            strSql.Append(" WHERE a.ClassID=b.ClassId");
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
            strSql.Append(" Id,batchID,ClassID,CouponCount ");
            strSql.Append(" FROM Shop_CouponRuleExt ");
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
            strSql.Append("select count(1) FROM Shop_CouponRuleExt ");
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
            strSql.Append(")AS Row, T.*  from Shop_CouponRuleExt T ");
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
            parameters[0].Value = "Shop_CouponRuleExt";
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
        /// <summary>
        /// 根据卡批次号获取优惠券规则
        /// </summary>
        /// <param name="BatchID">卡批次号</param>
        /// <returns></returns>
        public DataSet GetListByBatchID(string BatchID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,batchID,ClassID,CouponCount ");
            strSql.Append(" FROM Shop_CouponRuleExt ");
            strSql.Append(" where batchid=@BatchID");
            SqlParameter[] parameters = {
                  new SqlParameter("@BatchID", SqlDbType.VarChar,4)               
                                     };
            parameters[0].Value = BatchID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        //根据规则和优惠券类型发送优惠券
        public bool SetUserCoupon(int userID, int count, int classID)
        {
            int rowsAffected;

            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE  dbo.Shop_CouponInfo ");
            sql.Append(" SET     UserId = @USERID ,Status = 1 ");
            sql.Append("WHERE   CouponCode IN ( SELECT TOP " + count.ToString());
            sql.Append(" CouponCode FROM dbo.Shop_CouponInfo  WHERE   UserId = 0 ");
            sql.Append(" AND ClassId = @ClassID AND GETDATE() > StartDate  AND GETDATE() < EndDate )");


            SqlParameter[] parameters = {
                                        new SqlParameter("@USERID",SqlDbType.Int),
                                        new SqlParameter("@ClassID",SqlDbType.Int),};
            parameters[0].Value = userID;
            parameters[1].Value = classID;

            rowsAffected = DbHelperSQL.ExecuteSql(sql.ToString(), parameters);




            // DbHelperSQL.RunProcedure("SP_SetUser4Coupon", parameters, out rowsAffected);
            return (rowsAffected == count);
        }





        //根据规则和优惠券类型发送优惠券
        public bool SetManualCoupon(int userID, int count, int classID, string cardNo, string batch)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                                        new SqlParameter("@TOP",SqlDbType.Int),
                                        new SqlParameter("@USERID",SqlDbType.Int),
                                        new SqlParameter("@ClassID",SqlDbType.Int),
                                        new SqlParameter("@CardNo",SqlDbType.VarChar,18),
                                        new SqlParameter("@Batch",SqlDbType.VarChar,8),};
            parameters[0].Value = count;
            parameters[1].Value = userID;
            parameters[2].Value = classID;
            parameters[3].Value = cardNo;
            parameters[4].Value = batch;

            DbHelperSQL.RunProcedure("SP_SetManualCoupon", parameters, out rowsAffected);
            return (rowsAffected == count);
        }

        #endregion  ExtensionMethod
    }
}

