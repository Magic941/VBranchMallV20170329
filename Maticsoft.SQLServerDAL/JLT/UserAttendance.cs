/**
* UserAttendance.cs
*
* 功 能： N/A
* 类 名： UserAttendance
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/20 16:07:41   N/A    初版
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
using Maticsoft.IDAL.JLT;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.JLT
{
	/// <summary>
	/// 数据访问类:UserAttendance
	/// </summary>
	public partial class UserAttendance:IUserAttendance
	{
		public UserAttendance()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "JLT_UserAttendance"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from JLT_UserAttendance");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.JLT.UserAttendance model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into JLT_UserAttendance(");
			strSql.Append("EnterpriseID,UserID,UserName,TrueName,Latitude,Longitude,Address,Kilometers,TypeID,CreatedDate,AttendanceDate,Description,ImagePath,Score,Status,ReviewedUserID,ReviewedDescription,ReviewedDate,ReviewedStatus,Remark)");
			strSql.Append(" values (");
			strSql.Append("@EnterpriseID,@UserID,@UserName,@TrueName,@Latitude,@Longitude,@Address,@Kilometers,@TypeID,@CreatedDate,@AttendanceDate,@Description,@ImagePath,@Score,@Status,@ReviewedUserID,@ReviewedDescription,@ReviewedDate,@ReviewedStatus,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Latitude", SqlDbType.NVarChar,50),
					new SqlParameter("@Longitude", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Kilometers", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@AttendanceDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ImagePath", SqlDbType.NVarChar,500),
					new SqlParameter("@Score", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ReviewedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReviewedDescription", SqlDbType.NVarChar,300),
					new SqlParameter("@ReviewedDate", SqlDbType.DateTime),
					new SqlParameter("@ReviewedStatus", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.EnterpriseID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.TrueName;
			parameters[4].Value = model.Latitude;
			parameters[5].Value = model.Longitude;
			parameters[6].Value = model.Address;
			parameters[7].Value = model.Kilometers;
			parameters[8].Value = model.TypeID;
			parameters[9].Value = model.CreatedDate;
			parameters[10].Value = model.AttendanceDate;
			parameters[11].Value = model.Description;
			parameters[12].Value = model.ImagePath;
			parameters[13].Value = model.Score;
			parameters[14].Value = model.Status;
			parameters[15].Value = model.ReviewedUserID;
			parameters[16].Value = model.ReviewedDescription;
			parameters[17].Value = model.ReviewedDate;
			parameters[18].Value = model.ReviewedStatus;
			parameters[19].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.JLT.UserAttendance model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update JLT_UserAttendance set ");
			strSql.Append("EnterpriseID=@EnterpriseID,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("TrueName=@TrueName,");
			strSql.Append("Latitude=@Latitude,");
			strSql.Append("Longitude=@Longitude,");
			strSql.Append("Address=@Address,");
			strSql.Append("Kilometers=@Kilometers,");
			strSql.Append("TypeID=@TypeID,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("AttendanceDate=@AttendanceDate,");
			strSql.Append("Description=@Description,");
			strSql.Append("ImagePath=@ImagePath,");
			strSql.Append("Score=@Score,");
			strSql.Append("Status=@Status,");
			strSql.Append("ReviewedUserID=@ReviewedUserID,");
			strSql.Append("ReviewedDescription=@ReviewedDescription,");
			strSql.Append("ReviewedDate=@ReviewedDate,");
			strSql.Append("ReviewedStatus=@ReviewedStatus,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@Latitude", SqlDbType.NVarChar,50),
					new SqlParameter("@Longitude", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Kilometers", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@AttendanceDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ImagePath", SqlDbType.NVarChar,500),
					new SqlParameter("@Score", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ReviewedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReviewedDescription", SqlDbType.NVarChar,300),
					new SqlParameter("@ReviewedDate", SqlDbType.DateTime),
					new SqlParameter("@ReviewedStatus", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.EnterpriseID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.TrueName;
			parameters[4].Value = model.Latitude;
			parameters[5].Value = model.Longitude;
			parameters[6].Value = model.Address;
			parameters[7].Value = model.Kilometers;
			parameters[8].Value = model.TypeID;
			parameters[9].Value = model.CreatedDate;
			parameters[10].Value = model.AttendanceDate;
			parameters[11].Value = model.Description;
			parameters[12].Value = model.ImagePath;
			parameters[13].Value = model.Score;
			parameters[14].Value = model.Status;
			parameters[15].Value = model.ReviewedUserID;
			parameters[16].Value = model.ReviewedDescription;
			parameters[17].Value = model.ReviewedDate;
			parameters[18].Value = model.ReviewedStatus;
			parameters[19].Value = model.Remark;
			parameters[20].Value = model.ID;

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
			strSql.Append("delete from JLT_UserAttendance ");
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
			strSql.Append("delete from JLT_UserAttendance ");
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
		public Maticsoft.Model.JLT.UserAttendance GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,EnterpriseID,UserID,UserName,TrueName,Latitude,Longitude,Address,Kilometers,TypeID,CreatedDate,AttendanceDate,Description,ImagePath,Score,Status,ReviewedUserID,ReviewedDescription,ReviewedDate,ReviewedStatus,Remark from JLT_UserAttendance ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.JLT.UserAttendance model=new Maticsoft.Model.JLT.UserAttendance();
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
		public Maticsoft.Model.JLT.UserAttendance DataRowToModel(DataRow row)
		{
			Maticsoft.Model.JLT.UserAttendance model=new Maticsoft.Model.JLT.UserAttendance();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["EnterpriseID"]!=null && row["EnterpriseID"].ToString()!="")
				{
					model.EnterpriseID=int.Parse(row["EnterpriseID"].ToString());
				}
				if(row["UserID"]!=null && row["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(row["UserID"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["TrueName"]!=null)
				{
					model.TrueName=row["TrueName"].ToString();
				}
				if(row["Latitude"]!=null)
				{
					model.Latitude=row["Latitude"].ToString();
				}
				if(row["Longitude"]!=null)
				{
					model.Longitude=row["Longitude"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["Kilometers"]!=null && row["Kilometers"].ToString()!="")
				{
					model.Kilometers=int.Parse(row["Kilometers"].ToString());
				}
				if(row["TypeID"]!=null && row["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(row["TypeID"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["AttendanceDate"]!=null && row["AttendanceDate"].ToString()!="")
				{
					model.AttendanceDate=DateTime.Parse(row["AttendanceDate"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["ImagePath"]!=null)
				{
					model.ImagePath=row["ImagePath"].ToString();
				}
				if(row["Score"]!=null && row["Score"].ToString()!="")
				{
					model.Score=int.Parse(row["Score"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["ReviewedUserID"]!=null && row["ReviewedUserID"].ToString()!="")
				{
					model.ReviewedUserID=int.Parse(row["ReviewedUserID"].ToString());
				}
				if(row["ReviewedDescription"]!=null)
				{
					model.ReviewedDescription=row["ReviewedDescription"].ToString();
				}
				if(row["ReviewedDate"]!=null && row["ReviewedDate"].ToString()!="")
				{
					model.ReviewedDate=DateTime.Parse(row["ReviewedDate"].ToString());
				}
				if(row["ReviewedStatus"]!=null && row["ReviewedStatus"].ToString()!="")
				{
					model.ReviewedStatus=int.Parse(row["ReviewedStatus"].ToString());
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
			strSql.Append("select ID,EnterpriseID,UserID,UserName,TrueName,Latitude,Longitude,Address,Kilometers,TypeID,CreatedDate,AttendanceDate,Description,ImagePath,Score,Status,ReviewedUserID,ReviewedDescription,ReviewedDate,ReviewedStatus,Remark ");
			strSql.Append(" FROM JLT_UserAttendance ");
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
			strSql.Append(" ID,EnterpriseID,UserID,UserName,TrueName,Latitude,Longitude,Address,Kilometers,TypeID,CreatedDate,AttendanceDate,Description,ImagePath,Score,Status,ReviewedUserID,ReviewedDescription,ReviewedDate,ReviewedStatus,Remark ");
			strSql.Append(" FROM JLT_UserAttendance ");
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
			strSql.Append("select count(1) FROM JLT_UserAttendance ");
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
			strSql.Append(")AS Row, T.*  from JLT_UserAttendance T ");
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
			parameters[0].Value = "JLT_UserAttendance";
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
        /// 批量处理
        /// </summary>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JLT_UserAttendance set " + strWhere);
            strSql.Append(" where ID in(" + IDlist + ")  ");
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
        /// 统计考勤
        /// </summary>
        /// <param name="strWhere">条件</param>
        public DataSet Statistics(string strWhere)
        {
            return DbHelperSQL.RunProcedure("sp_UserAttendance_Statistics",
              new IDataParameter[]
                  {
                      DbHelperSQL.CreateInParam("@SqlWhere", SqlDbType.NVarChar, 3000, strWhere)
                  }, "StatisticsTable");
        }

        /// <summary>
        /// 获取用户考勤数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        public DataSet GetCollectAttendance(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
SELECT  T.UserID
      , T.UserName
      , T.AttendanceDate
      , STUFF(( SELECT  '　' + RTRIM(CONVERT(CHAR(8), CreatedDate, 114))
                FROM    JLT_UserAttendance
                WHERE   UserID = T.UserID
                        AND AttendanceDate = T.AttendanceDate
              FOR
                XML PATH('')
              ), 1, 1, '') AS CreatedDate
FROM    ( SELECT DISTINCT
                    UserID
                  , UserName
                  , AttendanceDate
          FROM      JLT_UserAttendance");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) T");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  ExtensionMethod
    }
}

