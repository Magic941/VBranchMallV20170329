/**
* Inquiry.cs
*
* 功 能： N/A
* 类 名： Inquiry
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/4 19:23:28   N/A    初版
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
using Maticsoft.IDAL.Shop.Inquiry;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Inquiry
{
	/// <summary>
	/// 数据访问类:Inquiry
	/// </summary>
	public partial class InquiryInfo:IInquiryInfo
	{
		public InquiryInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InquiryId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_Inquiry");
			strSql.Append(" where InquiryId=@InquiryId");
			SqlParameter[] parameters = {
					new SqlParameter("@InquiryId", SqlDbType.BigInt)
			};
			parameters[0].Value = InquiryId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Maticsoft.Model.Shop.Inquiry.InquiryInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_Inquiry(");
			strSql.Append("ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark)");
			strSql.Append(" values (");
			strSql.Append("@ParentId,@UserId,@UserName,@Email,@CellPhone,@Telephone,@RegionId,@Company,@Address,@QQ,@Status,@LeaveMsg,@ReplyMsg,@MarketPrice,@Amount,@CreatedDate,@UpdatedDate,@UpdatedUserId,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentId", SqlDbType.BigInt,8),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@CellPhone", SqlDbType.NVarChar,100),
					new SqlParameter("@Telephone", SqlDbType.NVarChar,100),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Company", SqlDbType.NVarChar,200),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@QQ", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@LeaveMsg", SqlDbType.Text),
					new SqlParameter("@ReplyMsg", SqlDbType.Text),
					new SqlParameter("@MarketPrice", SqlDbType.Money,8),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatedUserId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.ParentId;
			parameters[1].Value = model.UserId;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.Email;
			parameters[4].Value = model.CellPhone;
			parameters[5].Value = model.Telephone;
			parameters[6].Value = model.RegionId;
			parameters[7].Value = model.Company;
			parameters[8].Value = model.Address;
			parameters[9].Value = model.QQ;
			parameters[10].Value = model.Status;
			parameters[11].Value = model.LeaveMsg;
			parameters[12].Value = model.ReplyMsg;
			parameters[13].Value = model.MarketPrice;
			parameters[14].Value = model.Amount;
			parameters[15].Value = model.CreatedDate;
			parameters[16].Value = model.UpdatedDate;
			parameters[17].Value = model.UpdatedUserId;
			parameters[18].Value = model.Remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.Shop.Inquiry.InquiryInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_Inquiry set ");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("UserId=@UserId,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("Email=@Email,");
			strSql.Append("CellPhone=@CellPhone,");
			strSql.Append("Telephone=@Telephone,");
			strSql.Append("RegionId=@RegionId,");
			strSql.Append("Company=@Company,");
			strSql.Append("Address=@Address,");
			strSql.Append("QQ=@QQ,");
			strSql.Append("Status=@Status,");
			strSql.Append("LeaveMsg=@LeaveMsg,");
			strSql.Append("ReplyMsg=@ReplyMsg,");
			strSql.Append("MarketPrice=@MarketPrice,");
			strSql.Append("Amount=@Amount,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("UpdatedDate=@UpdatedDate,");
			strSql.Append("UpdatedUserId=@UpdatedUserId,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where InquiryId=@InquiryId");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentId", SqlDbType.BigInt,8),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@CellPhone", SqlDbType.NVarChar,100),
					new SqlParameter("@Telephone", SqlDbType.NVarChar,100),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Company", SqlDbType.NVarChar,200),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@QQ", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@LeaveMsg", SqlDbType.Text),
					new SqlParameter("@ReplyMsg", SqlDbType.Text),
					new SqlParameter("@MarketPrice", SqlDbType.Money,8),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatedUserId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@InquiryId", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ParentId;
			parameters[1].Value = model.UserId;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.Email;
			parameters[4].Value = model.CellPhone;
			parameters[5].Value = model.Telephone;
			parameters[6].Value = model.RegionId;
			parameters[7].Value = model.Company;
			parameters[8].Value = model.Address;
			parameters[9].Value = model.QQ;
			parameters[10].Value = model.Status;
			parameters[11].Value = model.LeaveMsg;
			parameters[12].Value = model.ReplyMsg;
			parameters[13].Value = model.MarketPrice;
			parameters[14].Value = model.Amount;
			parameters[15].Value = model.CreatedDate;
			parameters[16].Value = model.UpdatedDate;
			parameters[17].Value = model.UpdatedUserId;
			parameters[18].Value = model.Remark;
			parameters[19].Value = model.InquiryId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(long InquiryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_Inquiry ");
			strSql.Append(" where InquiryId=@InquiryId");
			SqlParameter[] parameters = {
					new SqlParameter("@InquiryId", SqlDbType.BigInt)
			};
			parameters[0].Value = InquiryId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string InquiryIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_Inquiry ");
			strSql.Append(" where InquiryId in ("+InquiryIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.Shop.Inquiry.InquiryInfo GetModel(long InquiryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 InquiryId,ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark from Shop_Inquiry ");
			strSql.Append(" where InquiryId=@InquiryId");
			SqlParameter[] parameters = {
					new SqlParameter("@InquiryId", SqlDbType.BigInt)
			};
			parameters[0].Value = InquiryId;

			Maticsoft.Model.Shop.Inquiry.InquiryInfo model=new Maticsoft.Model.Shop.Inquiry.InquiryInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public Maticsoft.Model.Shop.Inquiry.InquiryInfo DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Inquiry.InquiryInfo model=new Maticsoft.Model.Shop.Inquiry.InquiryInfo();
			if (row != null)
			{
				if(row["InquiryId"]!=null && row["InquiryId"].ToString()!="")
				{
					model.InquiryId=long.Parse(row["InquiryId"].ToString());
				}
				if(row["ParentId"]!=null && row["ParentId"].ToString()!="")
				{
					model.ParentId=long.Parse(row["ParentId"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["Email"]!=null)
				{
					model.Email=row["Email"].ToString();
				}
				if(row["CellPhone"]!=null)
				{
					model.CellPhone=row["CellPhone"].ToString();
				}
				if(row["Telephone"]!=null)
				{
					model.Telephone=row["Telephone"].ToString();
				}
				if(row["RegionId"]!=null && row["RegionId"].ToString()!="")
				{
					model.RegionId=int.Parse(row["RegionId"].ToString());
				}
				if(row["Company"]!=null)
				{
					model.Company=row["Company"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["QQ"]!=null)
				{
					model.QQ=row["QQ"].ToString();
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["LeaveMsg"]!=null)
				{
					model.LeaveMsg=row["LeaveMsg"].ToString();
				}
				if(row["ReplyMsg"]!=null)
				{
					model.ReplyMsg=row["ReplyMsg"].ToString();
				}
				if(row["MarketPrice"]!=null && row["MarketPrice"].ToString()!="")
				{
					model.MarketPrice=decimal.Parse(row["MarketPrice"].ToString());
				}
				if(row["Amount"]!=null && row["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(row["Amount"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["UpdatedDate"]!=null && row["UpdatedDate"].ToString()!="")
				{
					model.UpdatedDate=DateTime.Parse(row["UpdatedDate"].ToString());
				}
				if(row["UpdatedUserId"]!=null && row["UpdatedUserId"].ToString()!="")
				{
					model.UpdatedUserId=int.Parse(row["UpdatedUserId"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select InquiryId,ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark ");
			strSql.Append(" FROM Shop_Inquiry ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" InquiryId,ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark ");
			strSql.Append(" FROM Shop_Inquiry ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Shop_Inquiry ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.InquiryId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_Inquiry T ");
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
			parameters[0].Value = "Shop_Inquiry";
			parameters[1].Value = "InquiryId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

	    public bool DeleteEx(long InquiryId)
	    {
            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Inquiry ");
            strSql.Append(" where InquiryId=@InquiryId");
            SqlParameter[] parameters = {
						new SqlParameter("@InquiryId", SqlDbType.BigInt)
			};
            parameters[0].Value = InquiryId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from Shop_InquiryItem ");
            strSql1.Append(" where InquiryId=@InquiryId  ");
            SqlParameter[] parameters1 = {
							new SqlParameter("@InquiryId", SqlDbType.BigInt)
                                         };
            parameters1[0].Value = InquiryId;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
	    }

	    #endregion  ExtensionMethod
	}
}

