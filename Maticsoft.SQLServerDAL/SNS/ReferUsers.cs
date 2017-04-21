/**
* ReferUsers.cs
*
* 功 能： N/A
* 类 名： ReferUsers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:51   N/A    初版
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
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:ReferUsers
	/// </summary>
	public partial class ReferUsers:IReferUsers
	{
		public ReferUsers()
		{}
		#region  BasicMethod

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.ReferUsers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_ReferUsers(");
			strSql.Append("TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead)");
			strSql.Append(" values (");
			strSql.Append("@TagetID,@Type,@ReferUserID,@ReferNickName,@CreatedDate,@IsRead)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TagetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ReferUserID", SqlDbType.Int,4),
					new SqlParameter("@ReferNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Bit,1)};
			parameters[0].Value = model.TagetID;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.ReferUserID;
			parameters[3].Value = model.ReferNickName;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.IsRead;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.SNS.ReferUsers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_ReferUsers set ");
			strSql.Append("TagetID=@TagetID,");
			strSql.Append("Type=@Type,");
			strSql.Append("ReferUserID=@ReferUserID,");
			strSql.Append("ReferNickName=@ReferNickName,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("IsRead=@IsRead");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ReferUserID", SqlDbType.Int,4),
					new SqlParameter("@ReferNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.TagetID;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.ReferUserID;
			parameters[3].Value = model.ReferNickName;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.IsRead;
			parameters[6].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_ReferUsers ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_ReferUsers ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public Maticsoft.Model.SNS.ReferUsers GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead from SNS_ReferUsers ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.SNS.ReferUsers model=new Maticsoft.Model.SNS.ReferUsers();
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
		public Maticsoft.Model.SNS.ReferUsers DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.ReferUsers model=new Maticsoft.Model.SNS.ReferUsers();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["TagetID"]!=null && row["TagetID"].ToString()!="")
				{
					model.TagetID=int.Parse(row["TagetID"].ToString());
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["ReferUserID"]!=null && row["ReferUserID"].ToString()!="")
				{
					model.ReferUserID=int.Parse(row["ReferUserID"].ToString());
				}
				if(row["ReferNickName"]!=null)
				{
					model.ReferNickName=row["ReferNickName"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["IsRead"]!=null && row["IsRead"].ToString()!="")
				{
					if((row["IsRead"].ToString()=="1")||(row["IsRead"].ToString().ToLower()=="true"))
					{
						model.IsRead=true;
					}
					else
					{
						model.IsRead=false;
					}
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
			strSql.Append("select ID,TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead ");
			strSql.Append(" FROM SNS_ReferUsers ");
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
			strSql.Append(" ID,TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead ");
			strSql.Append(" FROM SNS_ReferUsers ");
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
			strSql.Append("select count(1) FROM SNS_ReferUsers ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_ReferUsers T ");
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
			parameters[0].Value = "SNS_ReferUsers";
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
        /// <summary>
        /// 更新状态为已经知道
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool UpdateReferStateToRead(int UserID, int Type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_ReferUsers set ");
            strSql.Append("IsRead='True'");
            strSql.Append(" where ReferUserID="+UserID+" and Type="+Type+"");
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
        public int GetReferNotReadCountByType(int UserId,int Type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_ReferUsers ");
            if (UserId>0)
            {
                strSql.Append("where ReferUserID="+UserId+" and Type="+Type+" and IsRead='False'");
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
		#endregion  ExtensionMethod
	}
}

