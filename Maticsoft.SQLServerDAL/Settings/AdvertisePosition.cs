/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：AdvertisePosition.cs
// 文件功能描述：
// 
// 创建标识： [孙鹏]  2012/05/31 13:22:19
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Settings;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Settings
{
	/// <summary>
	/// 数据访问类:AdvertisePosition
	/// </summary>
	public partial class AdvertisePosition:IAdvertisePosition
	{
		public AdvertisePosition()
		{}
		#region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AdvPositionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM AD_AdvertisePosition");
            strSql.Append(" WHERE AdvPositionId=@AdvPositionId");
            SqlParameter[] parameters = {
					new SqlParameter("@AdvPositionId", SqlDbType.Int,4)
			};
            parameters[0].Value = AdvPositionId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Settings.AdvertisePosition model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("INSERT INTO AD_AdvertisePosition(");
			strSql.Append("AdvPositionName,ShowType,RepeatColumns,Width,Height,AdvHtml,IsOne,TimeInterval,CreatedDate,CreatedUserID)");
			strSql.Append(" VALUES (");
			strSql.Append("@AdvPositionName,@ShowType,@RepeatColumns,@Width,@Height,@AdvHtml,@IsOne,@TimeInterval,@CreatedDate,@CreatedUserID)");
			strSql.Append(";SELECT @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvPositionName", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowType", SqlDbType.Int,4),
					new SqlParameter("@RepeatColumns", SqlDbType.Int,4),
					new SqlParameter("@Width", SqlDbType.Int,4),
					new SqlParameter("@Height", SqlDbType.Int,4),
					new SqlParameter("@AdvHtml", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsOne", SqlDbType.Bit,1),
					new SqlParameter("@TimeInterval", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4)};
			parameters[0].Value = model.AdvPositionName;
			parameters[1].Value = model.ShowType;
			parameters[2].Value = model.RepeatColumns;
			parameters[3].Value = model.Width;
			parameters[4].Value = model.Height;
			parameters[5].Value = model.AdvHtml;
			parameters[6].Value = model.IsOne;
			parameters[7].Value = model.TimeInterval;
			parameters[8].Value = model.CreatedDate;
			parameters[9].Value = model.CreatedUserID;

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
		public bool Update(Maticsoft.Model.Settings.AdvertisePosition model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE AD_AdvertisePosition SET ");
			strSql.Append("AdvPositionName=@AdvPositionName,");
			strSql.Append("ShowType=@ShowType,");
			strSql.Append("RepeatColumns=@RepeatColumns,");
			strSql.Append("Width=@Width,");
			strSql.Append("Height=@Height,");
			strSql.Append("AdvHtml=@AdvHtml,");
			strSql.Append("IsOne=@IsOne,");
			strSql.Append("TimeInterval=@TimeInterval,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserID=@CreatedUserID");
			strSql.Append(" WHERE AdvPositionId=@AdvPositionId");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvPositionName", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowType", SqlDbType.Int,4),
					new SqlParameter("@RepeatColumns", SqlDbType.Int,4),
					new SqlParameter("@Width", SqlDbType.Int,4),
					new SqlParameter("@Height", SqlDbType.Int,4),
					new SqlParameter("@AdvHtml", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsOne", SqlDbType.Bit,1),
					new SqlParameter("@TimeInterval", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@AdvPositionId", SqlDbType.Int,4)};
			parameters[0].Value = model.AdvPositionName;
			parameters[1].Value = model.ShowType;
			parameters[2].Value = model.RepeatColumns;
			parameters[3].Value = model.Width;
			parameters[4].Value = model.Height;
			parameters[5].Value = model.AdvHtml;
			parameters[6].Value = model.IsOne;
			parameters[7].Value = model.TimeInterval;
			parameters[8].Value = model.CreatedDate;
			parameters[9].Value = model.CreatedUserID;
			parameters[10].Value = model.AdvPositionId;

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
		public bool Delete(int AdvPositionId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM AD_AdvertisePosition ");
			strSql.Append(" WHERE AdvPositionId=@AdvPositionId");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvPositionId", SqlDbType.Int,4)
			};
			parameters[0].Value = AdvPositionId;

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
		public bool DeleteList(string AdvPositionIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM AD_AdvertisePosition ");
			strSql.Append(" WHERE AdvPositionId in ("+AdvPositionIdlist + ")  ");
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
		public Maticsoft.Model.Settings.AdvertisePosition GetModel(int AdvPositionId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT  TOP 1 * FROM AD_AdvertisePosition ");
			strSql.Append(" WHERE AdvPositionId=@AdvPositionId");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvPositionId", SqlDbType.Int,4)
			};
			parameters[0].Value = AdvPositionId;

			Maticsoft.Model.Settings.AdvertisePosition model=new Maticsoft.Model.Settings.AdvertisePosition();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["AdvPositionId"]!=null && ds.Tables[0].Rows[0]["AdvPositionId"].ToString()!="")
				{
					model.AdvPositionId=int.Parse(ds.Tables[0].Rows[0]["AdvPositionId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvPositionName"]!=null && ds.Tables[0].Rows[0]["AdvPositionName"].ToString()!="")
				{
					model.AdvPositionName=ds.Tables[0].Rows[0]["AdvPositionName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ShowType"]!=null && ds.Tables[0].Rows[0]["ShowType"].ToString()!="")
				{
					model.ShowType=int.Parse(ds.Tables[0].Rows[0]["ShowType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RepeatColumns"]!=null && ds.Tables[0].Rows[0]["RepeatColumns"].ToString()!="")
				{
					model.RepeatColumns=int.Parse(ds.Tables[0].Rows[0]["RepeatColumns"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Width"]!=null && ds.Tables[0].Rows[0]["Width"].ToString()!="")
				{
					model.Width=int.Parse(ds.Tables[0].Rows[0]["Width"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Height"]!=null && ds.Tables[0].Rows[0]["Height"].ToString()!="")
				{
					model.Height=int.Parse(ds.Tables[0].Rows[0]["Height"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvHtml"]!=null && ds.Tables[0].Rows[0]["AdvHtml"].ToString()!="")
				{
					model.AdvHtml=ds.Tables[0].Rows[0]["AdvHtml"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsOne"]!=null && ds.Tables[0].Rows[0]["IsOne"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsOne"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsOne"].ToString().ToLower()=="true"))
					{
						model.IsOne=true;
					}
					else
					{
						model.IsOne=false;
					}
				}
				if(ds.Tables[0].Rows[0]["TimeInterval"]!=null && ds.Tables[0].Rows[0]["TimeInterval"].ToString()!="")
				{
					model.TimeInterval=int.Parse(ds.Tables[0].Rows[0]["TimeInterval"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedDate"]!=null && ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedUserID"]!=null && ds.Tables[0].Rows[0]["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT AdvPositionId,AdvPositionName,ShowType,RepeatColumns,Width,Height,AdvHtml,IsOne,TimeInterval,CreatedDate,CreatedUserID ");
			strSql.Append(" FROM AD_AdvertisePosition ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT ");
			if(Top>0)
			{
				strSql.Append(" TOP "+Top.ToString());
			}
			strSql.Append(" AdvPositionId,AdvPositionName,ShowType,RepeatColumns,Width,Height,AdvHtml,IsOne,TimeInterval,CreatedDate,CreatedUserID ");
			strSql.Append(" FROM AD_AdvertisePosition ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			strSql.Append(" ORDER BY " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM AD_AdvertisePosition ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
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
			if (!string.IsNullOrWhiteSpace(orderby.Trim()))
			{
				strSql.Append("ORDER BY T." + orderby );
			}
			else
			{
				strSql.Append("ORDER BY T.AdvPositionId desc");
			}
			strSql.Append(")AS Row, T.*  FROM AD_AdvertisePosition T ");
			if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
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
			parameters[0].Value = "AD_AdvertisePosition";
			parameters[1].Value = "AdvPositionId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

