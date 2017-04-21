using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.CMS;
using Maticsoft.DBUtility;


namespace Maticsoft.SQLServerDAL.CMS
{
	/// <summary>
	/// 数据访问类:PhotoAlbum
	/// </summary>
	public class PhotoAlbum:IPhotoAlbum
	{
		public PhotoAlbum()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AlbumID", "CMS_PhotoAlbum"); 
		}

        /// <summary>
        /// 得到最大Sequence
        /// </summary>
        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_PhotoAlbum");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AlbumID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_PhotoAlbum");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
};
            parameters[0].Value = AlbumID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.CMS.PhotoAlbum model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CMS_PhotoAlbum(");
			strSql.Append("AlbumName,Description,CoverPhoto,State,CreatedUserID,CreatedDate,PVCount,Sequence,Privacy,LastUpdatedDate)");
			strSql.Append(" values (");
			strSql.Append("@AlbumName,@Description,@CoverPhoto,@State,@CreatedUserID,@CreatedDate,@PVCount,@Sequence,@Privacy,@LastUpdatedDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumName", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CoverPhoto", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
					new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime)};
			parameters[0].Value = model.AlbumName;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.CoverPhoto;
			parameters[3].Value = model.State;
			parameters[4].Value = model.CreatedUserID;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.PVCount;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.Privacy;
			parameters[9].Value = model.LastUpdatedDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			return obj == null ? 0 : Convert.ToInt32(obj);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.CMS.PhotoAlbum model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CMS_PhotoAlbum set ");
			strSql.Append("AlbumName=@AlbumName,");
			strSql.Append("Description=@Description,");
			strSql.Append("CoverPhoto=@CoverPhoto,");
			strSql.Append("State=@State,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("PVCount=@PVCount,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("Privacy=@Privacy,");
			strSql.Append("LastUpdatedDate=@LastUpdatedDate");
			strSql.Append(" where AlbumID=@AlbumID");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumName", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CoverPhoto", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
					new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@AlbumID", SqlDbType.Int,4)};
			parameters[0].Value = model.AlbumName;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.CoverPhoto;
			parameters[3].Value = model.State;
			parameters[4].Value = model.CreatedUserID;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.PVCount;
			parameters[7].Value = model.Sequence;
			parameters[8].Value = model.Privacy;
			parameters[9].Value = model.LastUpdatedDate;
			parameters[10].Value = model.AlbumID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows > 0;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int AlbumID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CMS_PhotoAlbum ");
			strSql.Append(" where AlbumID=@AlbumID");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
};
			parameters[0].Value = AlbumID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows > 0;
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string AlbumIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CMS_PhotoAlbum ");
			strSql.Append(" where AlbumID in ("+AlbumIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.CMS.PhotoAlbum GetModel(int AlbumID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AlbumID,AlbumName,Description,CoverPhoto,State,CreatedUserID,CreatedDate,PVCount,Sequence,Privacy,LastUpdatedDate from CMS_PhotoAlbum ");
			strSql.Append(" where AlbumID=@AlbumID");
			SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
};
			parameters[0].Value = AlbumID;

			Model.CMS.PhotoAlbum model=new Model.CMS.PhotoAlbum();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["AlbumID"]!=null && ds.Tables[0].Rows[0]["AlbumID"].ToString()!="")
				{
					model.AlbumID=int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AlbumName"]!=null && ds.Tables[0].Rows[0]["AlbumName"].ToString()!="")
				{
					model.AlbumName=ds.Tables[0].Rows[0]["AlbumName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Description"]!=null && ds.Tables[0].Rows[0]["Description"].ToString()!="")
				{
					model.Description=ds.Tables[0].Rows[0]["Description"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CoverPhoto"]!=null && ds.Tables[0].Rows[0]["CoverPhoto"].ToString()!="")
				{
					model.CoverPhoto=int.Parse(ds.Tables[0].Rows[0]["CoverPhoto"].ToString());
				}
				if(ds.Tables[0].Rows[0]["State"]!=null && ds.Tables[0].Rows[0]["State"].ToString()!="")
				{
					model.State=int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedUserID"]!=null && ds.Tables[0].Rows[0]["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedDate"]!=null && ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PVCount"]!=null && ds.Tables[0].Rows[0]["PVCount"].ToString()!="")
				{
					model.PVCount=int.Parse(ds.Tables[0].Rows[0]["PVCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sequence"]!=null && ds.Tables[0].Rows[0]["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Privacy"]!=null && ds.Tables[0].Rows[0]["Privacy"].ToString()!="")
				{
					model.Privacy=int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastUpdatedDate"]!=null && ds.Tables[0].Rows[0]["LastUpdatedDate"].ToString()!="")
				{
					model.LastUpdatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdatedDate"].ToString());
				}
				return model;
			}
		    return null;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("SELECT PL.*,p.ThumbImageUrl FROM CMS_PhotoAlbum PL LEFT JOIN CMS_Photo P ON p.PhotoID = PL.CoverPhoto");
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
            strSql.Append(" * FROM CMS_PhotoAlbum ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[0].Value = "CMS_PhotoAlbum";
			parameters[1].Value = "AlbumID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region Extension Method
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CMS_PhotoAlbum ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.AlbumID ASC");
            }
            strSql.Append(")AS Row, T.*,P.ThumbImageUrl from CMS_PhotoAlbum T LEFT JOIN CMS_Photo P ON p.PhotoID = T.CoverPhoto");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
	}
}

