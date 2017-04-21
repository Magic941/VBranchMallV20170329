using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:PhotoTags
	/// </summary>
	public partial class PhotoTags:IPhotoTags
	{
		public PhotoTags()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TagID", "SNS_PhotoTags"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TagID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_PhotoTags");
			strSql.Append(" where TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
			parameters[0].Value = TagID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string TagName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SNS_PhotoTags");
            strSql.Append(" WHERE TagName=@TagName");
            SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = TagName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TagID, string TagName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SNS_PhotoTags");
            strSql.Append(" WHERE TagID<>@TagID AND TagName=@TagName");
            SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4),
                    new SqlParameter("@TagName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = TagID;
            parameters[1].Value = TagName;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.PhotoTags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_PhotoTags(");
			strSql.Append("TagName,IsRecommand,Status,CreatedDate,Remark)");
			strSql.Append(" values (");
			strSql.Append("@TagName,@IsRecommand,@Status,@CreatedDate,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,100),
					new SqlParameter("@IsRecommand", SqlDbType.SmallInt,2),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.TagName;
			parameters[1].Value = model.IsRecommand;
			parameters[2].Value = model.Status;
			parameters[3].Value = model.CreatedDate;
			parameters[4].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.SNS.PhotoTags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_PhotoTags set ");
			strSql.Append("TagName=@TagName,");
			strSql.Append("IsRecommand=@IsRecommand,");
			strSql.Append("Status=@Status,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagName", SqlDbType.NVarChar,100),
					new SqlParameter("@IsRecommand", SqlDbType.SmallInt,2),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@TagID", SqlDbType.Int,4)};
			parameters[0].Value = model.TagName;
			parameters[1].Value = model.IsRecommand;
			parameters[2].Value = model.Status;
			parameters[3].Value = model.CreatedDate;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.TagID;

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
		public bool Delete(int TagID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_PhotoTags ");
			strSql.Append(" where TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
			parameters[0].Value = TagID;

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
		public bool DeleteList(string TagIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_PhotoTags ");
			strSql.Append(" where TagID in ("+TagIDlist + ")  ");
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
		public Maticsoft.Model.SNS.PhotoTags GetModel(int TagID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TagID,TagName,IsRecommand,Status,CreatedDate,Remark from SNS_PhotoTags ");
			strSql.Append(" where TagID=@TagID");
			SqlParameter[] parameters = {
					new SqlParameter("@TagID", SqlDbType.Int,4)
			};
			parameters[0].Value = TagID;

			Maticsoft.Model.SNS.PhotoTags model=new Maticsoft.Model.SNS.PhotoTags();
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
		public Maticsoft.Model.SNS.PhotoTags DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.PhotoTags model=new Maticsoft.Model.SNS.PhotoTags();
			if (row != null)
			{
				if(row["TagID"]!=null && row["TagID"].ToString()!="")
				{
					model.TagID=int.Parse(row["TagID"].ToString());
				}
				if(row["TagName"]!=null && row["TagName"].ToString()!="")
				{
					model.TagName=row["TagName"].ToString();
				}
				if(row["IsRecommand"]!=null && row["IsRecommand"].ToString()!="")
				{
					model.IsRecommand=int.Parse(row["IsRecommand"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Remark"]!=null && row["Remark"].ToString()!="")
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
			strSql.Append("select TagID,TagName,IsRecommand,Status,CreatedDate,Remark ");
			strSql.Append(" FROM SNS_PhotoTags ");
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
			strSql.Append(" TagID,TagName,IsRecommand,Status,CreatedDate,Remark ");
			strSql.Append(" FROM SNS_PhotoTags ");
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
			strSql.Append("select count(1) FROM SNS_PhotoTags ");
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
				strSql.Append("order by T.TagID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_PhotoTags T ");
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
			parameters[0].Value = "SNS_PhotoTags";
			parameters[1].Value = "TagID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region MethodEx
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_PhotoTags set ");
            strSql.AppendFormat(" Status={0} ", Status);
            strSql.AppendFormat(" where TagID IN({0})", IdList);

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

        public DataSet GetHotTags(int top)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   ");
            if (top > 10)
            {
                strSql.Append("top " + top + "  * from  SNS_PhotoTags order by newid()");
            }
            else
            {
                strSql.Append("top 10  * from  SNS_Tags order by newid()");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
	}
}

