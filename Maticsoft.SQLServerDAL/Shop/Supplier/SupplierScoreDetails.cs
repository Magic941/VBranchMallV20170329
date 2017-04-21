/**
* SupplierScoreDetails.cs
*
* 功 能： N/A
* 类 名： SupplierScoreDetails
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:51   Ben    初版
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
using Maticsoft.IDAL.Shop.Supplier;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
	/// <summary>
	/// 数据访问类:SupplierScoreDetails
	/// </summary>
	public partial class SupplierScoreDetails:ISupplierScoreDetails
	{
		public SupplierScoreDetails()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Shop_SupplierScoreDetails");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_SupplierScoreDetails");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierScoreDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_SupplierScoreDetails(");
            strSql.Append("Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark,SupplierId)");
            strSql.Append(" values (");
            strSql.Append("@Score,@ScoreType,@CreatedDate,@CreatedUserId,@Status,@Remark,@SupplierId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Score", SqlDbType.Money,8),
					new SqlParameter("@ScoreType", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@SupplierId", SqlDbType.Int,4)};
            parameters[0].Value = model.Score;
            parameters[1].Value = model.ScoreType;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.CreatedUserId;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.SupplierId;

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
        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierScoreDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_SupplierScoreDetails set ");
            strSql.Append("Score=@Score,");
            strSql.Append("ScoreType=@ScoreType,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CreatedUserId=@CreatedUserId,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("SupplierId=@SupplierId");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Score", SqlDbType.Money,8),
					new SqlParameter("@ScoreType", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Score;
            parameters[1].Value = model.ScoreType;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.CreatedUserId;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.SupplierId;
            parameters[7].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SupplierScoreDetails ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SupplierScoreDetails ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public Maticsoft.Model.Shop.Supplier.SupplierScoreDetails GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark,SupplierId from Shop_SupplierScoreDetails ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.Shop.Supplier.SupplierScoreDetails model = new Maticsoft.Model.Shop.Supplier.SupplierScoreDetails();
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
        public Maticsoft.Model.Shop.Supplier.SupplierScoreDetails DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierScoreDetails model = new Maticsoft.Model.Shop.Supplier.SupplierScoreDetails();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Score"] != null && row["Score"].ToString() != "")
                {
                    model.Score = decimal.Parse(row["Score"].ToString());
                }
                if (row["ScoreType"] != null && row["ScoreType"].ToString() != "")
                {
                    model.ScoreType = int.Parse(row["ScoreType"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CreatedUserId"] != null && row["CreatedUserId"].ToString() != "")
                {
                    model.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
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
            strSql.Append("select ID,Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark,SupplierId ");
            strSql.Append(" FROM Shop_SupplierScoreDetails ");
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
            strSql.Append(" ID,Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark,SupplierId ");
            strSql.Append(" FROM Shop_SupplierScoreDetails ");
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
            strSql.Append("select count(1) FROM Shop_SupplierScoreDetails ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_SupplierScoreDetails T ");
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
            parameters[0].Value = "Shop_SupplierScoreDetails";
            parameters[1].Value = "ID";
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

