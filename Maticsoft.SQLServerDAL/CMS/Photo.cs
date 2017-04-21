using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.CMS;

namespace Maticsoft.SQLServerDAL.CMS
{
    /// <summary>
    /// 数据访问类:Photo
    /// </summary>
    public partial class Photo : IPhoto
    {
        public Photo()
        { }

        #region Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PhotoID", "CMS_Photo");
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_Photo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PhotoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_Photo");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)
};
            parameters[0].Value = PhotoID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.CMS.Photo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_Photo(");
            strSql.Append("PhotoName,ImageUrl,Description,AlbumID,State,CreatedUserID,CreatedDate,PVCount,ClassID,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,CommentCount,Tags,FavouriteCount)");
            strSql.Append(" values (");
            strSql.Append("@PhotoName,@ImageUrl,@Description,@AlbumID,@State,@CreatedUserID,@CreatedDate,@PVCount,@ClassID,@ThumbImageUrl,@NormalImageUrl,@Sequence,@IsRecomend,@CommentCount,@Tags,@FavouriteCount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoName", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Bit,1),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,200)
                    ,new SqlParameter("@FavouriteCount", SqlDbType.Int,4)};
            parameters[0].Value = model.PhotoName;
            parameters[1].Value = model.ImageUrl;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.AlbumID;
            parameters[4].Value = model.State;
            parameters[5].Value = model.CreatedUserID;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.PVCount;
            parameters[8].Value = model.ClassID;
            parameters[9].Value = model.ThumbImageUrl;
            parameters[10].Value = model.NormalImageUrl;
            parameters[11].Value = model.Sequence;
            parameters[12].Value = model.IsRecomend;
            parameters[13].Value = model.CommentCount;
            parameters[14].Value = model.Tags;
            parameters[15].Value = model.FavouriteCount;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.CMS.Photo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Photo set ");
            strSql.Append("PhotoName=@PhotoName,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("Description=@Description,");
            strSql.Append("AlbumID=@AlbumID,");
            strSql.Append("State=@State,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("PVCount=@PVCount,");
            strSql.Append("ClassID=@ClassID,");
            strSql.Append("ThumbImageUrl=@ThumbImageUrl,");
            strSql.Append("NormalImageUrl=@NormalImageUrl,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("IsRecomend=@IsRecomend,");
            strSql.Append("CommentCount=@CommentCount,");
            strSql.Append("Tags=@Tags,");
            strSql.Append("FavouriteCount=@FavouriteCount ");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoName", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Bit,1),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,200),
					new SqlParameter("@PhotoID", SqlDbType.Int,4),
                    new SqlParameter("@FavouriteCount", SqlDbType.Int,4)};
            parameters[0].Value = model.PhotoName;
            parameters[1].Value = model.ImageUrl;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.AlbumID;
            parameters[4].Value = model.State;
            parameters[5].Value = model.CreatedUserID;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.PVCount;
            parameters[8].Value = model.ClassID;
            parameters[9].Value = model.ThumbImageUrl;
            parameters[10].Value = model.NormalImageUrl;
            parameters[11].Value = model.Sequence;
            parameters[12].Value = model.IsRecomend;
            parameters[13].Value = model.CommentCount;
            parameters[14].Value = model.Tags;
            parameters[15].Value = model.PhotoID;
            parameters[16].Value = model.FavouriteCount;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PhotoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_Photo ");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)
};
            parameters[0].Value = PhotoID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PhotoIDlist, out DataSet imageList)
        {
            StringBuilder strImg = new StringBuilder();
            strImg.Append(" SELECT ImageUrl ,ThumbImageUrl,NormalImageUrl FROM CMS_Photo");
            strImg.AppendFormat(" WHERE PhotoID IN ({0})  ", PhotoIDlist);
            imageList = DbHelperSQL.Query(strImg.ToString());

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_Photo ");
            strSql.Append(" where PhotoID in (" + PhotoIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.CMS.Photo GetModel(int PhotoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CMSP.*,AU.UserName AS CreatedUserName from CMS_Photo CMSP ");
            strSql.Append(" LEFT JOIN Accounts_Users AU ON CMSP.CreatedUserID=AU.UserID ");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)
};
            parameters[0].Value = PhotoID;

            Model.CMS.Photo model = new Model.CMS.Photo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PhotoID"] != null && ds.Tables[0].Rows[0]["PhotoID"].ToString() != "")
                {
                    model.PhotoID = int.Parse(ds.Tables[0].Rows[0]["PhotoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PhotoName"] != null && ds.Tables[0].Rows[0]["PhotoName"].ToString() != "")
                {
                    model.PhotoName = ds.Tables[0].Rows[0]["PhotoName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ImageUrl"] != null && ds.Tables[0].Rows[0]["ImageUrl"].ToString() != "")
                {
                    model.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AlbumID"] != null && ds.Tables[0].Rows[0]["AlbumID"].ToString() != "")
                {
                    model.AlbumID = int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedUserID"] != null && ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PVCount"] != null && ds.Tables[0].Rows[0]["PVCount"].ToString() != "")
                {
                    model.PVCount = int.Parse(ds.Tables[0].Rows[0]["PVCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassID"] != null && ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
                {
                    model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ThumbImageUrl"] != null && ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString() != "")
                {
                    model.ThumbImageUrl = ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NormalImageUrl"] != null && ds.Tables[0].Rows[0]["NormalImageUrl"].ToString() != "")
                {
                    model.NormalImageUrl = ds.Tables[0].Rows[0]["NormalImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sequence"] != null && ds.Tables[0].Rows[0]["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsRecomend"] != null && ds.Tables[0].Rows[0]["IsRecomend"].ToString() != "")
                {
                    model.IsRecomend = (ds.Tables[0].Rows[0]["IsRecomend"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower() == "true");
                }
                if (ds.Tables[0].Rows[0]["CommentCount"] != null && ds.Tables[0].Rows[0]["CommentCount"].ToString() != "")
                {
                    model.CommentCount = int.Parse(ds.Tables[0].Rows[0]["CommentCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tags"] != null && ds.Tables[0].Rows[0]["Tags"].ToString() != "")
                {
                    model.Tags = ds.Tables[0].Rows[0]["Tags"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedUserName"] != null && ds.Tables[0].Rows[0]["CreatedUserName"].ToString() != "")
                {
                    model.CreatedUserName = ds.Tables[0].Rows[0]["CreatedUserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FavouriteCount"] != null && ds.Tables[0].Rows[0]["FavouriteCount"].ToString() != "")
                {
                    model.FavouriteCount = int.Parse(ds.Tables[0].Rows[0]["FavouriteCount"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select T.*,PA.CoverPhoto FROM CMS_Photo  T LEFT JOIN CMS_PhotoAlbum PA ON T.AlbumID = PA.AlbumID");
            strSql.Append(" ");
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
                strSql.Append(" top " + Top);
            }
            strSql.Append(" *  FROM CMS_Photo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "CMS_Photo";
            parameters[1].Value = "PhotoID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion Method

        #region Extension Method

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CMS_Photo T");
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
                strSql.Append("order by T.PhotoID desc");
            }
            strSql.Append(")AS Row, T.*,PA.CoverPhoto  from CMS_Photo T LEFT JOIN CMS_PhotoAlbum PA ON T.AlbumID = PA.AlbumID");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 批量修改图片所属相册
        /// </summary>
        /// <param name="AlbumID">相册ID</param>
        /// <param name="newAlbumId">新相册ID</param>
        /// <returns></returns>
        public bool UpdatePhotoAlbum(int AlbumID, int newAlbumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update CMS_Photo set AlbumID = @newAlbumId where AlbumID = @AlbumID");
            SqlParameter[] parameters = {
                                            new SqlParameter("@newAlbumId", SqlDbType.Int),
                                            new SqlParameter("@AlbumID", SqlDbType.Int)
                                        };
            parameters[0].Value = newAlbumId;
            parameters[1].Value = AlbumID;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }
        public DataSet GetListAroundPhotoId(int Top,int PhotoId,int ClassId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * ");
            strSql.Append("FROM    CMS_Photo ");
            strSql.Append("WHERE   PhotoID IN ( SELECT PhotoID ");
            strSql.Append("                     FROM   ( SELECT TOP "+Top+" ");
            strSql.Append("                                        PhotoID , ");
            strSql.Append("                                        ABS(PhotoID - "+PhotoId+") AS seq ");
            strSql.Append("                              FROM      ( SELECT    * ");
            strSql.Append("                                          FROM      CMS_Photo ");
            strSql.Append("                                          WHERE     ClassID = "+ClassId+" ");
            strSql.Append("                                        ) temp ");
            strSql.Append("                              ORDER BY  seq ");
            strSql.Append("                            ) temp1 ) ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获取需要重新生成缩略图的数据
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListToReGen(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select PhotoID from CMS_Photo  ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append("WHERE  " + strWhere);
            }

            //  strSql.Append("ORDER BY AddedDate DESC ");
            return DbHelperSQL.Query((strSql.ToString()));
        }
        #endregion Extension Method
    }
}