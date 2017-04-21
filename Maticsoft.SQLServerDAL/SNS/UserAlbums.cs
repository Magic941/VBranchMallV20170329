/**
* UserAlbums.cs
*
* 功 能： N/A
* 类 名： UserAlbums
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:01   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.SNS;

namespace Maticsoft.SQLServerDAL.SNS
{
    /// <summary>
    /// 数据访问类:UserAlbums
    /// </summary>
    public partial class UserAlbums : IUserAlbums
    {
        public UserAlbums()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在同名相册
        /// </summary>
        public bool Exists(int CreatedUserID, string AlbumName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_UserAlbums");
            strSql.Append(" where CreatedUserID=@CreatedUserID and AlbumName=@AlbumName");
            SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
                    new SqlParameter("@AlbumName", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = CreatedUserID;
            parameters[1].Value = AlbumName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.UserAlbums model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserAlbums(");
            strSql.Append("AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags)");
            strSql.Append(" values (");
            strSql.Append("@AlbumName,@Description,@CoverTargetID,@CoverPhotoUrl,@CoverTargetType,@Status,@CreatedUserID,@CreatedNickName,@PhotoCount,@PVCount,@FavouriteCount,@CreatedDate,@CommentsCount,@IsRecommend,@ChannelSequence,@Privacy,@Sequence,@LastUpdatedDate,@Tags)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@CoverTargetID", SqlDbType.Int,4),
					new SqlParameter("@CoverPhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CoverTargetType", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@PhotoCount", SqlDbType.Int,4),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CommentsCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@ChannelSequence", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.AlbumName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.CoverTargetID;
            parameters[3].Value = model.CoverPhotoUrl;
            parameters[4].Value = model.CoverTargetType;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.CreatedUserID;
            parameters[7].Value = model.CreatedNickName;
            parameters[8].Value = model.PhotoCount;
            parameters[9].Value = model.PVCount;
            parameters[10].Value = model.FavouriteCount;
            parameters[11].Value = model.CreatedDate;
            parameters[12].Value = model.CommentsCount;
            parameters[13].Value = model.IsRecommend;
            parameters[14].Value = model.ChannelSequence;
            parameters[15].Value = model.Privacy;
            parameters[16].Value = model.Sequence;
            parameters[17].Value = model.LastUpdatedDate;
            parameters[18].Value = model.Tags;

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
        public bool Update(Maticsoft.Model.SNS.UserAlbums model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbums set ");
            strSql.Append("AlbumName=@AlbumName,");
            strSql.Append("Description=@Description,");
            strSql.Append("CoverTargetID=@CoverTargetID,");
            strSql.Append("CoverPhotoUrl=@CoverPhotoUrl,");
            strSql.Append("CoverTargetType=@CoverTargetType,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("CreatedNickName=@CreatedNickName,");
            strSql.Append("PhotoCount=@PhotoCount,");
            strSql.Append("PVCount=@PVCount,");
            strSql.Append("FavouriteCount=@FavouriteCount,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CommentsCount=@CommentsCount,");
            strSql.Append("IsRecommend=@IsRecommend,");
            strSql.Append("ChannelSequence=@ChannelSequence,");
            strSql.Append("Privacy=@Privacy,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("LastUpdatedDate=@LastUpdatedDate,");
            strSql.Append("Tags=@Tags");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@CoverTargetID", SqlDbType.Int,4),
					new SqlParameter("@CoverPhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CoverTargetType", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@PhotoCount", SqlDbType.Int,4),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CommentsCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@ChannelSequence", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@AlbumID", SqlDbType.Int,4)};
            parameters[0].Value = model.AlbumName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.CoverTargetID;
            parameters[3].Value = model.CoverPhotoUrl;
            parameters[4].Value = model.CoverTargetType;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.CreatedUserID;
            parameters[7].Value = model.CreatedNickName;
            parameters[8].Value = model.PhotoCount;
            parameters[9].Value = model.PVCount;
            parameters[10].Value = model.FavouriteCount;
            parameters[11].Value = model.CreatedDate;
            parameters[12].Value = model.CommentsCount;
            parameters[13].Value = model.IsRecommend;
            parameters[14].Value = model.ChannelSequence;
            parameters[15].Value = model.Privacy;
            parameters[16].Value = model.Sequence;
            parameters[17].Value = model.LastUpdatedDate;
            parameters[18].Value = model.Tags;
            parameters[19].Value = model.AlbumID;

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
        public bool Delete(int AlbumID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserAlbums ");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters[0].Value = AlbumID;

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
        public bool DeleteList(string AlbumIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserAlbums ");
            strSql.Append(" where AlbumID in (" + AlbumIDlist + ")  ");
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
        public Maticsoft.Model.SNS.UserAlbums GetModel(int AlbumID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AlbumID,AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags from SNS_UserAlbums ");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters[0].Value = AlbumID;

            Maticsoft.Model.SNS.UserAlbums model = new Maticsoft.Model.SNS.UserAlbums();
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
        public Maticsoft.Model.SNS.UserAlbums DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserAlbums model = new Maticsoft.Model.SNS.UserAlbums();
            if (row != null)
            {
                if (row["AlbumID"] != null && row["AlbumID"].ToString() != "")
                {
                    model.AlbumID = int.Parse(row["AlbumID"].ToString());
                }
                if (row["AlbumName"] != null && row["AlbumName"].ToString() != "")
                {
                    model.AlbumName = row["AlbumName"].ToString();
                }
                if (row["Description"] != null && row["Description"].ToString() != "")
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["CoverTargetID"] != null && row["CoverTargetID"].ToString() != "")
                {
                    model.CoverTargetID = int.Parse(row["CoverTargetID"].ToString());
                }
                if (row["CoverPhotoUrl"] != null && row["CoverPhotoUrl"].ToString() != "")
                {
                    model.CoverPhotoUrl = row["CoverPhotoUrl"].ToString();
                }
                if (row["CoverTargetType"] != null && row["CoverTargetType"].ToString() != "")
                {
                    model.CoverTargetType = int.Parse(row["CoverTargetType"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null && row["CreatedNickName"].ToString() != "")
                {
                    model.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["PhotoCount"] != null && row["PhotoCount"].ToString() != "")
                {
                    model.PhotoCount = int.Parse(row["PhotoCount"].ToString());
                }
                if (row["PVCount"] != null && row["PVCount"].ToString() != "")
                {
                    model.PVCount = int.Parse(row["PVCount"].ToString());
                }
                if (row["FavouriteCount"] != null && row["FavouriteCount"].ToString() != "")
                {
                    model.FavouriteCount = int.Parse(row["FavouriteCount"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CommentsCount"] != null && row["CommentsCount"].ToString() != "")
                {
                    model.CommentsCount = int.Parse(row["CommentsCount"].ToString());
                }

                //if (row["IsRecommend"] != null && row["IsRecommend"].ToString() != "")
                //{
                //    if ((row["IsRecommend"].ToString() == "1") || (row["IsRecommend"].ToString().ToLower() == "true"))
                //    {
                //        model.IsRecommend = true;
                //    }
                //    else
                //    {
                //        model.IsRecommend = false;
                //    }
                //}

                if (row["IsRecommend"] != null && row["IsRecommend"].ToString() != "")
                {
                    model.IsRecommend = int.Parse(row["IsRecommend"].ToString());
                }

                if (row["ChannelSequence"] != null && row["ChannelSequence"].ToString() != "")
                {
                    model.ChannelSequence = int.Parse(row["ChannelSequence"].ToString());
                }
                if (row["Privacy"] != null && row["Privacy"].ToString() != "")
                {
                    model.Privacy = int.Parse(row["Privacy"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["LastUpdatedDate"] != null && row["LastUpdatedDate"].ToString() != "")
                {
                    model.LastUpdatedDate = DateTime.Parse(row["LastUpdatedDate"].ToString());
                }
                if (row["Tags"] != null && row["Tags"].ToString() != "")
                {
                    model.Tags = row["Tags"].ToString();
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
            strSql.Append("select AlbumID,AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags ");
            strSql.Append(" FROM SNS_UserAlbums ");
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
            strSql.Append(" AlbumID,AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags ");
            strSql.Append(" FROM SNS_UserAlbums ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append(" order by AlbumID desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_UserAlbums ");
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
                strSql.Append("order by T.AlbumID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_UserAlbums T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 用户收藏的专辑
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataSet GetUserFavAlbum(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ua.* FROM SNS_UserFavAlbum ufa RIGHT JOIN SNS_UserAlbums ua ON ufa.AlbumID=ua.AlbumID ");
            if (UserId > 0)
            {
                strSql.Append("WHERE ufa.UserID=" + UserId + "");
            }
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
            parameters[0].Value = "UserAlbums";
            parameters[1].Value = "AlbumID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion BasicMethod

        #region ExtensionMethod

        /// <summary>
        /// 根据类型获得首页推荐的专辑
        /// </summary>
        public DataSet GetListForIndex(int TypeID, int Top, string orderby,int RecommandType=-1)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * from SNS_UserAlbums ua ");
            if (TypeID > 0)
            {
                strSql.Append(" inner join SNS_UserAlbumsType uat on ua.AlbumID=uat.AlbumsID ");
                strSql.AppendFormat(" and uat.TypeID={0} ", TypeID);
            }
            if (TypeID > 0 && RecommandType > -1)
            {
                if (strSql.Length > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.AppendFormat("  ua.IsRecommend={0} ", RecommandType);

            }
            else if (TypeID <= 0 &&RecommandType > -1)
            {
                strSql.AppendFormat("where  ua.IsRecommend={0} ", RecommandType);
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append("order by ua." + orderby);
            }
            else
            {
                strSql.Append("order by ua.AlbumID desc");
            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据类型获得首页推荐的专辑（少于9个不能推荐） 此代码后期和上面的代码合并
        /// </summary>
        public DataSet GetListForIndexEx(int TypeID, int Top, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * from SNS_UserAlbums ua");
            if (TypeID > 0)
            {
                strSql.Append(" inner join SNS_UserAlbumsType uat on ua.AlbumID=uat.AlbumsID ");
                strSql.AppendFormat(" where uat.TypeID={0} and ua.PhotoCount>8", TypeID);
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append("order by ua." + orderby);
            }
            else
            {
                strSql.Append("order by ua.AlbumID desc");
            }

            return DbHelperSQL.Query(strSql.ToString());
        }


       


        /// <summary>
        /// 根据类型获得记录总数
        /// </summary>
        public int GetRecordCount(int TypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_UserAlbums ua");
            if (TypeID > 0)
            {
                strSql.Append(" inner join SNS_UserAlbumsType uat on ua.AlbumID=uat.AlbumsID ");
                strSql.AppendFormat(" and uat.TypeID={0} ", TypeID);
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
        /// 根据类型获得分页的专辑
        /// </summary>
        public DataSet GetListForPage(int TypeID, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.AlbumID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_UserAlbums T ");
            if (TypeID > 0)
            {
                strSql.Append(" inner join SNS_UserAlbumsType uat on T.AlbumID=uat.AlbumsID ");
                strSql.AppendFormat(" and uat.TypeID={0} ", TypeID);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据类型获得分页的专辑（专辑的数量条件是超过9个）
        /// </summary>
        public DataSet GetListForPageEx(int TypeID, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.AlbumID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_UserAlbums T ");

            strSql.Append(" left join SNS_UserAlbumsType uat on T.AlbumID=uat.AlbumsID ");

            strSql.Append(" where T.PhotoCount>8 and T.IsRecommend="+(int)Maticsoft.Model.SNS.EnumHelper.RecommendType.Home+"");

            if (TypeID > 0)
            {
                strSql.Append("  and uat.TypeID={0} ");
            }
            //   strSql.AppendFormat(" and uat.TypeID={0} and T.PhotoCount>8", TypeID);
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据用户专辑里面的一张图片获得相应的专辑的信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pid"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Maticsoft.Model.SNS.UserAlbums GetUserAlbum(int type, int pid, int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AlbumID from SNS_UserAlbumDetail ");
            strSql.Append(" where AlbumUserId=@AlbumUserId and TargetID=@TargetID and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumUserId", SqlDbType.Int,4),
                    new SqlParameter("@TargetID", SqlDbType.Int,4),
                    new SqlParameter("@Type",SqlDbType.Int,4)
			};
            parameters[0].Value = UserId;
            parameters[1].Value = pid;
            parameters[2].Value = type;
            Maticsoft.Model.SNS.UserAlbums model = new Maticsoft.Model.SNS.UserAlbums();
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return null;
            }
            else
            {
                return GetModel(Convert.ToInt32(obj));
            }
        }

        /// <summary>
        /// 只更新专辑的名字和描述
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEx(Maticsoft.Model.SNS.UserAlbums model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbums set ");
            strSql.Append("AlbumName=@AlbumName,");
            strSql.Append("Description=@Description ");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@AlbumID", SqlDbType.Int,4)};
            parameters[0].Value = model.AlbumName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.AlbumID;
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

        public bool DeleteEx(int AlbumID, int TypeId, int UserId)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            #region 删除专辑下面的数据

            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from SNS_UserAlbumsType ");
            strSql4.Append(" where AlbumsID=@AlbumsID");
            SqlParameter[] parameters4 = {
					new SqlParameter("@AlbumsID", SqlDbType.Int,4)
			};
            parameters4[0].Value = AlbumID;
            CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd4);

            #endregion 删除专辑下面的数据

            #region 删除专辑下面的数据

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from SNS_UserAlbumDetail ");
            strSql1.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters1 = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters1[0].Value = AlbumID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            #endregion 删除专辑下面的数据

            #region 删除专辑

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserAlbums ");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters[0].Value = AlbumID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            #endregion 删除专辑

            #region 专辑类型里面的数量相应的-1

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("Update  SNS_AlbumType set AlbumsCount=AlbumsCount-1 ");
            strSql2.Append(" where ID=@TypeId");
            SqlParameter[] parameters2 = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
            parameters2[0].Value = TypeId;
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd2);

            #endregion 专辑类型里面的数量相应的-1

            #region 用户表中相应的专辑的数量也-1

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("Update  Accounts_UsersExp set AblumsCount=AblumsCount-1 ");
            strSql3.Append(" where UserID=@UserID");
            SqlParameter[] parameters3 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters3[0].Value = UserId;
            CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd3);

            #endregion 用户表中相应的专辑的数量也-1

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
        }

        public int AddEx(Maticsoft.Model.SNS.UserAlbums model, int TypeId)
        {
            #region 专辑表中增加专辑

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserAlbums(");
            strSql.Append("AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags)");
            strSql.Append(" values (");
            strSql.Append("@AlbumName,@Description,@CoverTargetID,@CoverPhotoUrl,@CoverTargetType,@Status,@CreatedUserID,@CreatedNickName,@PhotoCount,@PVCount,@FavouriteCount,@CreatedDate,@CommentsCount,@IsRecommend,@ChannelSequence,@Privacy,@Sequence,@LastUpdatedDate,@Tags)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@CoverTargetID", SqlDbType.Int,4),
					new SqlParameter("@CoverPhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CoverTargetType", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@PhotoCount", SqlDbType.Int,4),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CommentsCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@ChannelSequence", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.AlbumName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.CoverTargetID;
            parameters[3].Value = model.CoverPhotoUrl;
            parameters[4].Value = model.CoverTargetType;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.CreatedUserID;
            parameters[7].Value = model.CreatedNickName;
            parameters[8].Value = model.PhotoCount;
            parameters[9].Value = model.PVCount;
            parameters[10].Value = model.FavouriteCount;
            parameters[11].Value = model.CreatedDate;
            parameters[12].Value = model.CommentsCount;
            parameters[13].Value = model.IsRecommend;
            parameters[14].Value = model.ChannelSequence;
            parameters[15].Value = model.Privacy;
            parameters[16].Value = model.Sequence;
            parameters[17].Value = model.LastUpdatedDate;
            parameters[18].Value = model.Tags;
            parameters[19].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            #endregion 专辑表中增加专辑

            #region 专辑类型里面的数量相应的+1

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("Update  SNS_AlbumType set AlbumsCount=AlbumsCount+1 ");
            strSql2.Append(" where ID=@TypeId");
            SqlParameter[] parameters2 = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
            parameters2[0].Value = TypeId;
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd2);

            #endregion 专辑类型里面的数量相应的+1

            #region 用户表中相应的专辑的数量也+1

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("Update  Accounts_UsersExp set AblumsCount=AblumsCount+1 ");
            strSql3.Append(" where UserID=@UserID");
            SqlParameter[] parameters3 = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
            parameters3[0].Value = model.CreatedUserID;
            CommandInfo cmd3 = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd3);

            #endregion 用户表中相应的专辑的数量也+1

            DbHelperSQL.ExecuteSqlTran(sqllist);
            return (int)parameters[19].Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool UpdatePhotoCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbums set ");
            strSql.Append("PhotoCount=(select COUNT(1) from SNS_UserAlbumDetail where AlbumID=SNS_UserAlbums.AlbumID)");
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

        public bool UpdatePvCount(int AlbumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbums set ");
            strSql.Append("PVCount=PvCount+1 Where AlbumID=" + AlbumId + "");
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbums set ");
            strSql.AppendFormat(" IsRecommend={0} ", IsRecommand);
            strSql.AppendFormat(" where AlbumID IN({0})", IdList);

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
        /// 根据专辑ID删除专辑信息
        /// </summary>
        /// <param name="albumId">专辑ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteAblumAction(int albumId)
        {
            int affectRows = 0;
            SqlParameter[] parameters = {
                    new SqlParameter("@AlbumID", SqlDbType.Int, 4)
                    };
            parameters[0].Value = albumId;
            return DbHelperSQL.RunProcedure("sp_SNS_AblumsDeleteAction", parameters, out affectRows)>0;
        }

        /// <summary>
        /// 更新专辑推荐状态
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <param name="Recommand">推荐状态</param>
        /// <returns>执行结果 True OR False</returns>
        public bool UpdateRecommand(int ablumId, Model.SNS.EnumHelper.RecommendType recommendType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbums set ");
            strSql.Append("IsRecommend=@IsRecommend");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
                    new SqlParameter("@IsRecommend", SqlDbType.Int),
                    new SqlParameter("@AlbumID", SqlDbType.Int,4)};
            parameters[0].Value = (int)recommendType;
            parameters[1].Value = ablumId;
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

        public bool UpdateCommentCount(int ablumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbums set ");
            strSql.Append("CommentsCount=CommentsCount+1");
            strSql.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters = {
 
                    new SqlParameter("@AlbumID", SqlDbType.Int,4)};
            parameters[0].Value = ablumId;
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
        #endregion ExtensionMethod
    }
}