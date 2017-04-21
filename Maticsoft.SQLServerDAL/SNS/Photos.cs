/**
* Photos.cs
*
* 功 能： N/A
* 类 名： Photos
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:47   N/A    初版
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
    /// 数据访问类:Photos
    /// </summary>
    public partial class Photos : IPhotos
    {
        public Photos()
        { }

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PhotoID", "SNS_Photos");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PhotoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Photos");
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
        public int Add(Maticsoft.Model.SNS.Photos model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Photos(");
            strSql.Append("PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress)");
            strSql.Append(" values (");
            strSql.Append("@PhotoName,@PhotoUrl,@Type,@Description,@Status,@CreatedUserID,@CreatedNickName,@CreatedDate,@CategoryId,@PVCount,@ThumbImageUrl,@NormalImageUrl,@Sequence,@IsRecomend,@TopCommentsId,@ForwardedCount,@CommentCount,@FavouriteCount,@OwnerPhotoId,@Tags,@StaticUrl,@MapLng,@MapLat,@PhotoAddress)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoName", SqlDbType.NVarChar,200),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@TopCommentsId", SqlDbType.NVarChar,100),
					new SqlParameter("@ForwardedCount", SqlDbType.Int,4),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@OwnerPhotoId", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@MapLng", SqlDbType.NVarChar,200),
					new SqlParameter("@MapLat", SqlDbType.NVarChar,200),
					new SqlParameter("@PhotoAddress", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.PhotoName;
            parameters[1].Value = model.PhotoUrl;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.CreatedUserID;
            parameters[6].Value = model.CreatedNickName;
            parameters[7].Value = model.CreatedDate;
            parameters[8].Value = model.CategoryId;
            parameters[9].Value = model.PVCount;
            parameters[10].Value = model.ThumbImageUrl;
            parameters[11].Value = model.NormalImageUrl;
            parameters[12].Value = model.Sequence;
            parameters[13].Value = model.IsRecomend;
            parameters[14].Value = model.TopCommentsId;
            parameters[15].Value = model.ForwardedCount;
            parameters[16].Value = model.CommentCount;
            parameters[17].Value = model.FavouriteCount;
            parameters[18].Value = model.OwnerPhotoId;
            parameters[19].Value = model.Tags;
            parameters[20].Value = model.StaticUrl;
            parameters[21].Value = model.MapLng;
            parameters[22].Value = model.MapLat;
            parameters[23].Value = model.PhotoAddress;

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
        public bool Update(Maticsoft.Model.SNS.Photos model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Photos set ");
            strSql.Append("PhotoName=@PhotoName,");
            strSql.Append("PhotoUrl=@PhotoUrl,");
            strSql.Append("Type=@Type,");
            strSql.Append("Description=@Description,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("CreatedNickName=@CreatedNickName,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("PVCount=@PVCount,");
            strSql.Append("ThumbImageUrl=@ThumbImageUrl,");
            strSql.Append("NormalImageUrl=@NormalImageUrl,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("IsRecomend=@IsRecomend,");
            strSql.Append("TopCommentsId=@TopCommentsId,");
            strSql.Append("ForwardedCount=@ForwardedCount,");
            strSql.Append("CommentCount=@CommentCount,");
            strSql.Append("FavouriteCount=@FavouriteCount,");
            strSql.Append("OwnerPhotoId=@OwnerPhotoId,");
            strSql.Append("Tags=@Tags,");
            strSql.Append("StaticUrl=@StaticUrl,");
            strSql.Append("MapLng=@MapLng,");
            strSql.Append("MapLat=@MapLat,");
            strSql.Append("PhotoAddress=@PhotoAddress");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoName", SqlDbType.NVarChar,200),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@TopCommentsId", SqlDbType.NVarChar,100),
					new SqlParameter("@ForwardedCount", SqlDbType.Int,4),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@OwnerPhotoId", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,100),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@MapLng", SqlDbType.NVarChar,200),
					new SqlParameter("@MapLat", SqlDbType.NVarChar,200),
					new SqlParameter("@PhotoAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@PhotoID", SqlDbType.Int,4)};
            parameters[0].Value = model.PhotoName;
            parameters[1].Value = model.PhotoUrl;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.CreatedUserID;
            parameters[6].Value = model.CreatedNickName;
            parameters[7].Value = model.CreatedDate;
            parameters[8].Value = model.CategoryId;
            parameters[9].Value = model.PVCount;
            parameters[10].Value = model.ThumbImageUrl;
            parameters[11].Value = model.NormalImageUrl;
            parameters[12].Value = model.Sequence;
            parameters[13].Value = model.IsRecomend;
            parameters[14].Value = model.TopCommentsId;
            parameters[15].Value = model.ForwardedCount;
            parameters[16].Value = model.CommentCount;
            parameters[17].Value = model.FavouriteCount;
            parameters[18].Value = model.OwnerPhotoId;
            parameters[19].Value = model.Tags;
            parameters[20].Value = model.StaticUrl;
            parameters[21].Value = model.MapLng;
            parameters[22].Value = model.MapLat;
            parameters[23].Value = model.PhotoAddress;
            parameters[24].Value = model.PhotoID;

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
        public bool Delete(int PhotoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Photos ");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)
			};
            parameters[0].Value = PhotoID;

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
        public bool DeleteList(string PhotoIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Photos ");
            strSql.Append(" where PhotoID in (" + PhotoIDlist + ")  ");
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
        public Maticsoft.Model.SNS.Photos GetModel(int PhotoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PhotoID,PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress from SNS_Photos ");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)
			};
            parameters[0].Value = PhotoID;

            Maticsoft.Model.SNS.Photos model = new Maticsoft.Model.SNS.Photos();
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
        public Maticsoft.Model.SNS.Photos DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Photos model = new Maticsoft.Model.SNS.Photos();
            if (row != null)
            {
                if (row["PhotoID"] != null && row["PhotoID"].ToString() != "")
                {
                    model.PhotoID = int.Parse(row["PhotoID"].ToString());
                }
                if (row["PhotoName"] != null)
                {
                    model.PhotoName = row["PhotoName"].ToString();
                }
                if (row["PhotoUrl"] != null)
                {
                    model.PhotoUrl = row["PhotoUrl"].ToString();
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    model.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["PVCount"] != null && row["PVCount"].ToString() != "")
                {
                    model.PVCount = int.Parse(row["PVCount"].ToString());
                }
                if (row["ThumbImageUrl"] != null)
                {
                    model.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if (row["NormalImageUrl"] != null)
                {
                    model.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["IsRecomend"] != null && row["IsRecomend"].ToString() != "")
                {
                    model.IsRecomend = int.Parse(row["IsRecomend"].ToString());
                }
                if (row["TopCommentsId"] != null)
                {
                    model.TopCommentsId = row["TopCommentsId"].ToString();
                }
                if (row["ForwardedCount"] != null && row["ForwardedCount"].ToString() != "")
                {
                    model.ForwardedCount = int.Parse(row["ForwardedCount"].ToString());
                }
                if (row["CommentCount"] != null && row["CommentCount"].ToString() != "")
                {
                    model.CommentCount = int.Parse(row["CommentCount"].ToString());
                }
                if (row["FavouriteCount"] != null && row["FavouriteCount"].ToString() != "")
                {
                    model.FavouriteCount = int.Parse(row["FavouriteCount"].ToString());
                }
                if (row["OwnerPhotoId"] != null && row["OwnerPhotoId"].ToString() != "")
                {
                    model.OwnerPhotoId = int.Parse(row["OwnerPhotoId"].ToString());
                }
                if (row["Tags"] != null)
                {
                    model.Tags = row["Tags"].ToString();
                }
                if (row["StaticUrl"] != null)
                {
                    model.StaticUrl = row["StaticUrl"].ToString();
                }
                if (row["MapLng"] != null)
                {
                    model.MapLng = row["MapLng"].ToString();
                }
                if (row["MapLat"] != null)
                {
                    model.MapLat = row["MapLat"].ToString();
                }
                if (row["PhotoAddress"] != null)
                {
                    model.PhotoAddress = row["PhotoAddress"].ToString();
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
            strSql.Append("select PhotoID,PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress ");
            strSql.Append(" FROM SNS_Photos ");
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
            strSql.Append(" PhotoID,PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress ");
            strSql.Append(" FROM SNS_Photos ");
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
            strSql.Append("select count(1) FROM SNS_Photos ");
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
                strSql.Append("order by T.PhotoID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Photos T ");
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
            parameters[0].Value = "SNS_Photos";
            parameters[1].Value = "PhotoID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        #region ExtensionMethod

        public bool UpdatePvCount(int PhotoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Photos set ");
            strSql.Append("PVCount=PVCount+1");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)};
            parameters[0].Value = PhotoID;
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
        /// 事务删除一条数据
        /// </summary>
        /// <param name="PhotoID"></param>
        /// <returns></returns>
        public bool DeleteEX(int PhotoID)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //删除用户收藏数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SNS_UserFavourite ");
            strSql.Append(" where type=0 and TargetID=@TargetId ");
            SqlParameter[] parameters = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters[0].Value = PhotoID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            #region 更新动作

            //更新自己的分享数据和商品数量
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("Update Accounts_UsersExp set  ShareCount=ShareCount-1");
            strSql6.Append(" where UserID=( Select CreatedUserID  from SNS_Photos where PhotoID=@PhotoID) ");
            SqlParameter[] parameters6 = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)};
            parameters6[0].Value = PhotoID;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);

            //更新别人的喜欢数据（性能考虑 单独执行更新）
            //StringBuilder strSql7 = new StringBuilder();
            //strSql7.Append("Update Accounts_UsersExp set  FavouritesCount=FavouritesCount-1 ");
            //strSql7.Append("  where UserID=( Select CreatedUserID  from SNS_UserFavourite where type=1 and TargetID=@TargetId)");
            //SqlParameter[] parameters7 = {
            //        new SqlParameter("@TargetId", SqlDbType.Int,4)};
            //parameters7[0].Value = PhotoID;
            //cmd = new CommandInfo(strSql7.ToString(), parameters7);
            //sqllist.Add(cmd);

            //更新用户专辑
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            strSql8.Append("  where AlbumID=( Select AlbumID  from SNS_UserAlbumDetail where type=0 and TargetID=@TargetId)");
            SqlParameter[] parameters8 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters8[0].Value = PhotoID;
            cmd = new CommandInfo(strSql8.ToString(), parameters8);
            sqllist.Add(cmd);

            #endregion 更新动作

            #region 删除动作

            //删除动态数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete SNS_Posts ");
            strSql2.Append(" where Type=1 and TargetId=@TargetId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters2[0].Value = PhotoID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除评论数据
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete SNS_Comments ");
            strSql3.Append(" where type=1 and TargetID=@TargetId ");
            SqlParameter[] parameters3 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters3[0].Value = PhotoID;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //删除专辑该图片数据
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete SNS_UserAlbumDetail ");
            strSql5.Append(" where type=0 and TargetID=@TargetId ");
            SqlParameter[] parameters5 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters5[0].Value = PhotoID;
            cmd = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd);

            //删除图片数据
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete SNS_Photos ");
            strSql4.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters4 = {
					new SqlParameter("@PhotoID", SqlDbType.Int,4)
			};
            parameters4[0].Value = PhotoID;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            #endregion 删除动作

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetZuiInList(int CategoryId, int Top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT p.CreatedNickName NickName,p.PhotoID PhotoId,ue.AblumsCount AlbumsCount,ue.FansCount FansCount,p.ThumbImageUrl PhotoUrl,p.CreatedUserID UserId");
            if (Top > 0)
            {
                strSql.Append("  FROM ( SELECT TOP " + Top + " * FROM SNS_Photos ");
            }
            strSql.Append("WHERE CategoryId=" + CategoryId + " AND IsRecomend=2 ) AS p INNER JOIN Accounts_UsersExp ue ON ue.UserID=p.CreatedUserID");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 批量删除数据（事务删除）
        /// </summary>
        /// <param name="PhotoID"></param>
        /// <returns></returns>
        public bool DeleteListEX(string PhotoIDs)
        {
            int count = PhotoIDs.Split(',').Length;
            List<CommandInfo> sqllist = new List<CommandInfo>();

            #region 更新动作

            //更新自己的分享数据和商品数
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("Update Accounts_UsersExp set  ShareCount=ShareCount-" + count);
            strSql6.Append(" where UserID in ( Select CreatedUserID  from SNS_Photos where PhotoID in (" + PhotoIDs + ")) ");
            SqlParameter[] parameters = { };
            CommandInfo cmd = new CommandInfo(strSql6.ToString(), parameters);
            sqllist.Add(cmd);

            //更新别人的喜欢数据（性能考虑，需要单独更新）
            //StringBuilder strSql7 = new StringBuilder();
            //strSql7.Append("Update Accounts_UsersExp set  FavouritesCount=FavouritesCount-1 ");
            //strSql7.Append("  where UserID in ( Select CreatedUserID  from SNS_UserFavourite where type=1 and TargetID in (" + PhotoIDs + "))");
            //cmd = new CommandInfo(strSql7.ToString(), parameters);
            //sqllist.Add(cmd);

            //更新用户专辑
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            strSql8.Append("  where AlbumID in ( Select AlbumID  from SNS_UserAlbumDetail where type=0 and TargetID in (" + PhotoIDs + "))");
            cmd = new CommandInfo(strSql8.ToString(), parameters);
            sqllist.Add(cmd);

            #endregion 更新动作

            #region 删除动作

            //删除商品数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SNS_Photos ");
            strSql.Append(" where PhotoID in (" + PhotoIDs + ")");
            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //删除动态数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete SNS_Posts ");
            strSql2.Append(" where Type=1 and TargetId in (" + PhotoIDs + ") ");
            cmd = new CommandInfo(strSql2.ToString(), parameters);
            sqllist.Add(cmd);

            //删除评论数据
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete SNS_Comments ");
            strSql3.Append(" where type=1 and TargetID in (" + PhotoIDs + ") ");
            cmd = new CommandInfo(strSql3.ToString(), parameters);
            sqllist.Add(cmd);

            //删除用户收藏数据
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete SNS_UserFavourite ");
            strSql4.Append(" where type=0 and TargetID in (" + PhotoIDs + ") ");
            cmd = new CommandInfo(strSql4.ToString(), parameters);
            sqllist.Add(cmd);

            //删除专辑该商品数据
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete SNS_UserAlbumDetail ");
            strSql5.Append(" where type=0 and TargetID in (" + PhotoIDs + ") ");
            cmd = new CommandInfo(strSql5.ToString(), parameters);
            sqllist.Add(cmd);

            #endregion 删除动作

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCateList(string PhotoIDs, int CateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Photos set ");
            strSql.Append("CategoryID=@CategoryID");
            strSql.Append(" where PhotoID in (" + PhotoIDs + ")");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
					};
            parameters[0].Value = CateId;

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

        public bool UpdateRecomend(int PhotoID, int Recomend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Photos set ");
            strSql.Append("IsRecomend=@Recomend,Status=1");
            strSql.Append(" where PhotoID =@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@Recomend", SqlDbType.Int,4),
						new SqlParameter("@PhotoID", SqlDbType.Int,4)
					};
            parameters[0].Value = Recomend;
            parameters[1].Value = PhotoID;
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

        public bool UpdateStatus(int PhotoID, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Photos set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where PhotoID =@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
						new SqlParameter("@PhotoID", SqlDbType.Int,4)
					};
            parameters[0].Value = Status;
            parameters[1].Value = PhotoID;
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

        public bool UpdateRecommandState(int id, int Recomend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");

            strSql.Append("IsRecomend=@IsRecomend,Status=1");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4)};
            parameters[0].Value = Recomend;
            parameters[1].Value = id;
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
        /// 根据专辑ID获取该用户自定义上传的照片路径
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <returns>结果集</returns>
        public DataSet UserUploadPhoto(int ablumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT P.*  ");
            strSql.Append("FROM  SNS_Photos P , ");
            strSql.Append("( SELECT TargetID ");
            strSql.Append("FROM SNS_UserAlbumDetail ");
            strSql.Append("WHERE AlbumID = @AlbumID ");
            strSql.Append("AND Type = 0 ");
            strSql.Append(") UAD , ");
            strSql.Append("( SELECT CreatedUserID ");
            strSql.Append("FROM SNS_UserAlbums ");
            strSql.Append("WHERE AlbumID = @AlbumID ");
            strSql.Append(") U ");
            strSql.Append("WHERE P.PhotoID = UAD.TargetID ");
            strSql.Append("AND P.CreatedUserID = U.CreatedUserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)};
            parameters[0].Value = ablumId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataSet DeleteListEx(string Ids, out int Result)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TargetIds ", SqlDbType.NVarChar),
					DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)};
            parameters[0].Value = 1;
            parameters[1].Value = Ids;
            DataSet ds = DbHelperSQL.RunProcedure("sp_SNS_ImageDeleteAction", parameters, "tb", out Result);
            if (Result == 1)
            {
                return ds;
            }
            return null;
        }

        public DataSet GetListEx(string strWhere, int CateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SNS_Photos  where");

            if (CateId == 0)
            {
                strSql.Append("  CategoryID >0");
            }
            if (CateId > 0)
            {
                strSql.Append("  CategoryID in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=" + CateId + " or Path like '" + CateId + "|%'))");
            }
            if (CateId == -1)
            {
                strSql.Append("  CategoryID <=0");
            }
    
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and ");
                strSql.Append(strWhere);
            }

            strSql.Append(" order by CreatedDate desc");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCountEx(string strWhere, int CateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_Photos where");
            if (CateId == 0)
            {
                strSql.Append("  CategoryID >0");
            }
            if (CateId > 0)
            {
                strSql.Append("  CategoryID in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=" + CateId + " or Path like '" + CateId + "|%'))");
            }
            if (CateId == -1)
            {
                strSql.Append("  CategoryID<=0 " );
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and ");

                strSql.Append(strWhere);
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
        public DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.PhotoID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Photos T  where");
            if (CateId == 0)
            {
                strSql.Append("  CategoryID >0");
            }
            if (CateId > 0)
            {
                strSql.Append("  CategoryID in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=" + CateId + " or Path like '" + CateId + "|%'))");
            }
            if (CateId == -1)
            {
                strSql.Append("  CategoryID <=0" );
            }
            if (strWhere.Length > 1)
            {
                strSql.Append(" and ");
                strSql.Append(strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());



            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT * FROM ( ");
            //strSql.Append(" SELECT ROW_NUMBER() OVER (");
            //if (!string.IsNullOrEmpty(orderby.Trim()))
            //{
            //    strSql.Append("order by T." + orderby);
            //}
            //else
            //{
            //    strSql.Append("order by T.ProductID desc");
            //}
            //strSql.Append(")AS Row, T.*  from SNS_Products T  ");
            //if (CateId > 0 || strWhere.Length > 1)
            //{
            //    strSql.Append(" where ");
            //}
            //if (CateId > 0)
            //{
            //    strSql.Append("  CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=" + CateId + " or Path like '" + CateId + "|%'))");
            //}
            //if (strWhere.Length > 1)
            //{
            //    if (CateId > 0)
            //    {
            //        strSql.Append(" and ");
            //    }
            //    strSql.Append(strWhere);
            //}
            //strSql.Append(" ) TT");
            //strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            //return DbHelperSQL.Query(strSql.ToString());
        }

        //批量推荐到首页
        public bool UpdateRecomendList(string PhotoIds, int Recomend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Photos set ");
            strSql.Append("IsRecomend=@Recomend,Status=1");
            strSql.Append(" where PhotoID in (" + PhotoIds + ")");
            SqlParameter[] parameters = {
					new SqlParameter("@Recomend", SqlDbType.Int,4)
					};
            parameters[0].Value = Recomend;
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
        /// 获取需要重新生成缩略图的数据
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListToReGen(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select PhotoID from SNS_Photos  ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append("WHERE  " + strWhere);
            }

            //  strSql.Append("ORDER BY AddedDate DESC ");
            return DbHelperSQL.Query((strSql.ToString()));
        }

        /// <summary>
        /// 更新静态页面地址
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="staticUrl"></param>
        /// <returns></returns>
        public bool UpdateStaticUrl(int photoId, string staticUrl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Photos set ");
            strSql.Append("StaticUrl=@StaticUrl");
            strSql.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameters = {
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@PhotoID", SqlDbType.Int,4)};
            parameters[0].Value = staticUrl;
            parameters[1].Value = photoId;

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
        /// 得到上一个PhotoId
        /// </summary>
        public int GetPrevID(int photoID, int albumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(TargetID) from SNS_UserAlbumDetail   where  Type=0  ");
            if (albumId == -1)
            {
                strSql.Append(" and AlbumID=(select  top 1 AlbumID from SNS_UserAlbumDetail where  Type=0 and TargetID=@TargetID and AlbumUserId=(select CreatedUserID from SNS_Photos where PhotoID=@TargetID) order by ID) ");
            }
            else
            {
                strSql.Append(" AND  AlbumID=" + albumId);
            }
            strSql.Append(" AND TargetID<@TargetID");
            SqlParameter[] parameters = {
					new SqlParameter("@TargetID", SqlDbType.Int,4)
			};
            parameters[0].Value = photoID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 得到下一个PhotoId
        /// </summary>
        public int GetNextID(int photoID, int albumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MIN(TargetID) from SNS_UserAlbumDetail   where  Type=0  ");
            if (albumId == -1)
            {
                strSql.Append(" and AlbumID=(select  top 1 AlbumID from SNS_UserAlbumDetail where  Type=0 and TargetID=@TargetID and AlbumUserId=(select CreatedUserID from SNS_Photos where PhotoID=@TargetID) order by ID) ");
            }
            else
            {
                strSql.Append(" AND  AlbumID=" + albumId);
            }
            strSql.Append(" AND TargetID>@TargetID");
            SqlParameter[] parameters = {
					new SqlParameter("@TargetID", SqlDbType.Int,4)
			};
            parameters[0].Value = photoID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        public int GetCountEx(int type, int categoryId, string address)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM  SNS_Photos SP  where   SP.Status=1");
            if (type > 0)
            {
                strSql.Append(" and  SP.Type=" + type);
            }
            if (categoryId > 0)
            {
                strSql.Append("  AND SP.CategoryId in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=" + categoryId + " or Path like '" + categoryId + "|%'))");
            }
            if (!String.IsNullOrWhiteSpace(address))
            {
                strSql.Append(" and SP.PhotoAddress like '%" + address + "%'");
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


        public DataSet GetListByPageEx(int type, int categoryId, string address, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.PhotoID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Photos T ");


            strSql.Append(" where  T.Status=1");
            if (type > 0)
            {
                strSql.Append(" and  T.Type=" + type);
            }
            if (categoryId > 0)
            {
                strSql.Append("  AND T.CategoryId in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=" + categoryId + " or Path like '" + categoryId + "|%'))");
            }
            if (!String.IsNullOrWhiteSpace(address))
            {
                strSql.Append(" and T.PhotoAddress like '%" + address + "%'");
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetPhotoUserIds(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select CreatedUserID from SNS_Photos  where PhotoID IN (" + ids + ") ");
            //  strSql.Append("ORDER BY AddedDate DESC ");
            return DbHelperSQL.Query((strSql.ToString()));
        }

        #endregion ExtensionMethod
    }
}