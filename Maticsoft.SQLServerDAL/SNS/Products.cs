/**
* Products.cs
*
* 功 能： N/A
* 类 名： Products
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:49   N/A    初版
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
    /// 数据访问类:Products
    /// </summary>
    public partial class Products : IProducts
    {
        public Products()
        { }

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ProductID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Products");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.BigInt)
			};
            parameters[0].Value = ProductID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.SNS.Products model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Products(");
            strSql.Append("ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl)");
            strSql.Append(" values (");
            strSql.Append("@ProductName,@ProductDescription,@Price,@ProductSourceID,@CategoryID,@ProductUrl,@FavouriteCount,@GroupBuyCount,@CreateUserID,@CreatedNickName,@ThumbImageUrl,@NormalImageUrl,@PVCount,@IsRecomend,@Status,@Sequence,@TopCommentsId,@CommentCount,@ForwardedCount,@ShareDescription,@SkipCount,@OwnerProductId,@Tags,@CreatedDate,@Color,@OriginalID,@SourceType,@StaticUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductSourceID", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@GroupBuyCount", SqlDbType.Int,4),
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@TopCommentsId", SqlDbType.NVarChar,100),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@ForwardedCount", SqlDbType.Int,4),
					new SqlParameter("@ShareDescription", SqlDbType.NVarChar,200),
					new SqlParameter("@SkipCount", SqlDbType.Int,4),
					new SqlParameter("@OwnerProductId", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,400),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Color", SqlDbType.NVarChar,100),
					new SqlParameter("@OriginalID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceType", SqlDbType.Int,4),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.ProductName;
            parameters[1].Value = model.ProductDescription;
            parameters[2].Value = model.Price;
            parameters[3].Value = model.ProductSourceID;
            parameters[4].Value = model.CategoryID;
            parameters[5].Value = model.ProductUrl;
            parameters[6].Value = model.FavouriteCount;
            parameters[7].Value = model.GroupBuyCount;
            parameters[8].Value = model.CreateUserID;
            parameters[9].Value = model.CreatedNickName;
            parameters[10].Value = model.ThumbImageUrl;
            parameters[11].Value = model.NormalImageUrl;
            parameters[12].Value = model.PVCount;
            parameters[13].Value = model.IsRecomend;
            parameters[14].Value = model.Status;
            parameters[15].Value = model.Sequence;
            parameters[16].Value = model.TopCommentsId;
            parameters[17].Value = model.CommentCount;
            parameters[18].Value = model.ForwardedCount;
            parameters[19].Value = model.ShareDescription;
            parameters[20].Value = model.SkipCount;
            parameters[21].Value = model.OwnerProductId;
            parameters[22].Value = model.Tags;
            parameters[23].Value = model.CreatedDate;
            parameters[24].Value = model.Color;
            parameters[25].Value = model.OriginalID;
            parameters[26].Value = model.SourceType;
            parameters[27].Value = model.StaticUrl;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(Maticsoft.Model.SNS.Products model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ProductDescription=@ProductDescription,");
            strSql.Append("Price=@Price,");
            strSql.Append("ProductSourceID=@ProductSourceID,");
            strSql.Append("CategoryID=@CategoryID,");
            strSql.Append("ProductUrl=@ProductUrl,");
            strSql.Append("FavouriteCount=@FavouriteCount,");
            strSql.Append("GroupBuyCount=@GroupBuyCount,");
            strSql.Append("CreateUserID=@CreateUserID,");
            strSql.Append("CreatedNickName=@CreatedNickName,");
            strSql.Append("ThumbImageUrl=@ThumbImageUrl,");
            strSql.Append("NormalImageUrl=@NormalImageUrl,");
            strSql.Append("PVCount=@PVCount,");
            strSql.Append("IsRecomend=@IsRecomend,");
            strSql.Append("Status=@Status,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("TopCommentsId=@TopCommentsId,");
            strSql.Append("CommentCount=@CommentCount,");
            strSql.Append("ForwardedCount=@ForwardedCount,");
            strSql.Append("ShareDescription=@ShareDescription,");
            strSql.Append("SkipCount=@SkipCount,");
            strSql.Append("OwnerProductId=@OwnerProductId,");
            strSql.Append("Tags=@Tags,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Color=@Color,");
            strSql.Append("OriginalID=@OriginalID,");
            strSql.Append("SourceType=@SourceType,");
            strSql.Append("StaticUrl=@StaticUrl");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductSourceID", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@GroupBuyCount", SqlDbType.Int,4),
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@TopCommentsId", SqlDbType.NVarChar,100),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@ForwardedCount", SqlDbType.Int,4),
					new SqlParameter("@ShareDescription", SqlDbType.NVarChar,200),
					new SqlParameter("@SkipCount", SqlDbType.Int,4),
					new SqlParameter("@OwnerProductId", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,400),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Color", SqlDbType.NVarChar,100),
					new SqlParameter("@OriginalID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceType", SqlDbType.Int,4),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ProductID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ProductName;
            parameters[1].Value = model.ProductDescription;
            parameters[2].Value = model.Price;
            parameters[3].Value = model.ProductSourceID;
            parameters[4].Value = model.CategoryID;
            parameters[5].Value = model.ProductUrl;
            parameters[6].Value = model.FavouriteCount;
            parameters[7].Value = model.GroupBuyCount;
            parameters[8].Value = model.CreateUserID;
            parameters[9].Value = model.CreatedNickName;
            parameters[10].Value = model.ThumbImageUrl;
            parameters[11].Value = model.NormalImageUrl;
            parameters[12].Value = model.PVCount;
            parameters[13].Value = model.IsRecomend;
            parameters[14].Value = model.Status;
            parameters[15].Value = model.Sequence;
            parameters[16].Value = model.TopCommentsId;
            parameters[17].Value = model.CommentCount;
            parameters[18].Value = model.ForwardedCount;
            parameters[19].Value = model.ShareDescription;
            parameters[20].Value = model.SkipCount;
            parameters[21].Value = model.OwnerProductId;
            parameters[22].Value = model.Tags;
            parameters[23].Value = model.CreatedDate;
            parameters[24].Value = model.Color;
            parameters[25].Value = model.OriginalID;
            parameters[26].Value = model.SourceType;
            parameters[27].Value = model.StaticUrl;
            parameters[28].Value = model.ProductID;

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
        public bool Delete(long ProductID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Products ");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.BigInt)
			};
            parameters[0].Value = ProductID;

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
        public bool DeleteList(string ProductIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Products ");
            strSql.Append(" where ProductID in (" + ProductIDlist + ")  ");
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
        public Maticsoft.Model.SNS.Products GetModel(long ProductID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl from SNS_Products ");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.BigInt)
			};
            parameters[0].Value = ProductID;

            Maticsoft.Model.SNS.Products model = new Maticsoft.Model.SNS.Products();
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
        public Maticsoft.Model.SNS.Products DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Products model = new Maticsoft.Model.SNS.Products();
            if (row != null)
            {
                if (row["ProductID"] != null && row["ProductID"].ToString() != "")
                {
                    model.ProductID = long.Parse(row["ProductID"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["ProductDescription"] != null)
                {
                    model.ProductDescription = row["ProductDescription"].ToString();
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["ProductSourceID"] != null && row["ProductSourceID"].ToString() != "")
                {
                    model.ProductSourceID = int.Parse(row["ProductSourceID"].ToString());
                }
                if (row["CategoryID"] != null && row["CategoryID"].ToString() != "")
                {
                    model.CategoryID = int.Parse(row["CategoryID"].ToString());
                }
                if (row["ProductUrl"] != null)
                {
                    model.ProductUrl = row["ProductUrl"].ToString();
                }
                if (row["FavouriteCount"] != null && row["FavouriteCount"].ToString() != "")
                {
                    model.FavouriteCount = int.Parse(row["FavouriteCount"].ToString());
                }
                if (row["GroupBuyCount"] != null && row["GroupBuyCount"].ToString() != "")
                {
                    model.GroupBuyCount = int.Parse(row["GroupBuyCount"].ToString());
                }
                if (row["CreateUserID"] != null && row["CreateUserID"].ToString() != "")
                {
                    model.CreateUserID = int.Parse(row["CreateUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    model.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["ThumbImageUrl"] != null)
                {
                    model.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if (row["NormalImageUrl"] != null)
                {
                    model.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if (row["PVCount"] != null && row["PVCount"].ToString() != "")
                {
                    model.PVCount = int.Parse(row["PVCount"].ToString());
                }
                if (row["IsRecomend"] != null && row["IsRecomend"].ToString() != "")
                {
                    model.IsRecomend = int.Parse(row["IsRecomend"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["TopCommentsId"] != null)
                {
                    model.TopCommentsId = row["TopCommentsId"].ToString();
                }
                if (row["CommentCount"] != null && row["CommentCount"].ToString() != "")
                {
                    model.CommentCount = int.Parse(row["CommentCount"].ToString());
                }
                if (row["ForwardedCount"] != null && row["ForwardedCount"].ToString() != "")
                {
                    model.ForwardedCount = int.Parse(row["ForwardedCount"].ToString());
                }
                if (row["ShareDescription"] != null)
                {
                    model.ShareDescription = row["ShareDescription"].ToString();
                }
                if (row["SkipCount"] != null && row["SkipCount"].ToString() != "")
                {
                    model.SkipCount = int.Parse(row["SkipCount"].ToString());
                }
                if (row["OwnerProductId"] != null && row["OwnerProductId"].ToString() != "")
                {
                    model.OwnerProductId = int.Parse(row["OwnerProductId"].ToString());
                }
                if (row["Tags"] != null)
                {
                    model.Tags = row["Tags"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Color"] != null)
                {
                    model.Color = row["Color"].ToString();
                }
                if (row["OriginalID"] != null && row["OriginalID"].ToString() != "")
                {
                    model.OriginalID = long.Parse(row["OriginalID"].ToString());
                }
                if (row["SourceType"] != null && row["SourceType"].ToString() != "")
                {
                    model.SourceType = int.Parse(row["SourceType"].ToString());
                }
                if (row["StaticUrl"] != null)
                {
                    model.StaticUrl = row["StaticUrl"].ToString();
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
            strSql.Append("select ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl ");
            strSql.Append(" FROM SNS_Products ");
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
            strSql.Append(" ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl ");
            strSql.Append(" FROM SNS_Products ");
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
            strSql.Append("select count(1) FROM SNS_Products ");
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
                strSql.Append("order by T.ProductID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Products T ");
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
            parameters[0].Value = "SNS_Products";
            parameters[1].Value = "ProductID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        #region ExtensionMethod

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetProductByPage(string strWhere, string Order, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(Order.Trim()))
            {
                strSql.Append(Order);
            }
            else
            {
                strSql.Append("order by T.ProductID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Products T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool UpdatePvCount(int ProductID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("PVCount=PVCount+1");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.Int,4)};
            parameters[0].Value = ProductID;
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
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public bool DeleteEX(int ProductID)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //删除用户收藏数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SNS_UserFavourite ");
            strSql.Append(" where type=1 and TargetID=@TargetId ");
            SqlParameter[] parameters = {
					new SqlParameter("@TargetId",SqlDbType.Int,4)};
            parameters[0].Value = ProductID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            #region 更新动作

            //更新自己的分享数据和商品数量
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("Update Accounts_UsersExp set  ShareCount=ShareCount-1,ProductsCount=ProductsCount-1");
            strSql6.Append(" where UserID=( Select CreateUserID  from SNS_Products where ProductID=@ProductID) ");
            SqlParameter[] parameters6 = {
					new SqlParameter("@ProductID", SqlDbType.Int,4)};
            parameters6[0].Value = ProductID;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);

            //更新别人的喜欢数据（性能考虑 单独执行更新）
            //StringBuilder strSql7 = new StringBuilder();
            //strSql7.Append("Update Accounts_UsersExp set  FavouritesCount=FavouritesCount-1 ");
            //strSql7.Append("  where UserID=( Select CreatedUserID  from SNS_UserFavourite where type=1 and TargetID=@TargetId)");
            //SqlParameter[] parameters7 = {
            //        new SqlParameter("@TargetId", SqlDbType.Int,4)};
            //parameters7[0].Value = ProductID;
            //cmd = new CommandInfo(strSql7.ToString(), parameters7);
            //sqllist.Add(cmd);

            //更新用户专辑
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            strSql8.Append("  where AlbumID=( Select AlbumID  from SNS_UserAlbumDetail where type=1 and TargetID=@TargetId)");
            SqlParameter[] parameters8 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters8[0].Value = ProductID;
            cmd = new CommandInfo(strSql8.ToString(), parameters8);
            sqllist.Add(cmd);

            #endregion 更新动作

            #region 删除动作

            //删除动态数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete SNS_Posts ");
            strSql2.Append(" where Type=2 and TargetId=@TargetId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters2[0].Value = ProductID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除评论数据
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete SNS_Comments ");
            strSql3.Append(" where type=2 and TargetID=@TargetId ");
            SqlParameter[] parameters3 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters3[0].Value = ProductID;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //删除专辑该商品数据
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete SNS_UserAlbumDetail ");
            strSql5.Append(" where type=1 and TargetID=@TargetId ");
            SqlParameter[] parameters5 = {
					new SqlParameter("@TargetId", SqlDbType.Int,4)};
            parameters5[0].Value = ProductID;
            cmd = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd);

            //删除商品数据
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete SNS_Products ");
            strSql4.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters4 = {
					new SqlParameter("@ProductID", SqlDbType.Int,4)
			};
            parameters4[0].Value = ProductID;
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

        /// <summary>
        /// 批量删除数据（事务删除）
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public bool DeleteListEX(string ProductIds)
        {
            int count = ProductIds.Split(',').Length;
            List<CommandInfo> sqllist = new List<CommandInfo>();

            #region 更新动作

            //更新自己的分享数据和商品数
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("Update Accounts_UsersExp set  ShareCount=ShareCount-" + count + " ,ProductsCount=ProductsCount-" + count);
            strSql6.Append(" where UserID in ( Select CreateUserID  from SNS_Products where ProductID in (" + ProductIds + ")) ");
            SqlParameter[] parameters = { };
            CommandInfo cmd = new CommandInfo(strSql6.ToString(), parameters);
            sqllist.Add(cmd);

            //更新别人的喜欢数据（性能考虑，需要单独更新）
            //StringBuilder strSql7 = new StringBuilder();
            //strSql7.Append("Update Accounts_UsersExp set  FavouritesCount=FavouritesCount-1 ");
            //strSql7.Append("  where UserID in ( Select CreatedUserID  from SNS_UserFavourite where type=1 and TargetID in (" + ProductIds + "))");
            //cmd = new CommandInfo(strSql7.ToString(), parameters);
            //sqllist.Add(cmd);

            //更新用户专辑
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            strSql8.Append("  where AlbumID in ( Select AlbumID  from SNS_UserAlbumDetail where type=1 and TargetID in (" + ProductIds + "))");
            cmd = new CommandInfo(strSql8.ToString(), parameters);
            sqllist.Add(cmd);

            #endregion 更新动作

            #region 删除动作

            //删除商品数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SNS_Products ");
            strSql.Append(" where ProductID in (" + ProductIds + ")");
            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //删除动态数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete SNS_Posts ");
            strSql2.Append(" where Type=2 and TargetId in (" + ProductIds + ") ");
            cmd = new CommandInfo(strSql2.ToString(), parameters);
            sqllist.Add(cmd);

            //删除评论数据
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete SNS_Comments ");
            strSql3.Append(" where type=2 and TargetID in (" + ProductIds + ") ");
            cmd = new CommandInfo(strSql3.ToString(), parameters);
            sqllist.Add(cmd);

            //删除用户收藏数据
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete SNS_UserFavourite ");
            strSql4.Append(" where type=1 and TargetID in (" + ProductIds + ") ");
            cmd = new CommandInfo(strSql4.ToString(), parameters);
            sqllist.Add(cmd);

            //删除专辑该商品数据
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete SNS_UserAlbumDetail ");
            strSql5.Append(" where type=1 and TargetID in (" + ProductIds + ") ");
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

        public bool UpdateCateList(string ProductIds, int CateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("CategoryID=@CategoryID");
            strSql.Append(" where ProductID in (" + ProductIds + ")");
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

        public bool UpdateEX(int ProductId, int CateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("CategoryID=@CategoryID");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
                    	new SqlParameter("@ProductID", SqlDbType.Int,4)
					};
            parameters[0].Value = CateId;
            parameters[1].Value = ProductId;

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

        //批量推荐到首页
        public bool UpdateRecomendList(string ProductIds, int Recomend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("IsRecomend=@Recomend");
            strSql.Append(" where ProductID in (" + ProductIds + ")");
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

        public bool UpdateRecomend(int ProductId, int Recomend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("IsRecomend=@Recomend");
            strSql.Append(" where ProductID =@ProductId");
            SqlParameter[] parameters = {
					new SqlParameter("@Recomend", SqlDbType.Int,4),
                    	new SqlParameter("@ProductId", SqlDbType.Int,4)
					};
            parameters[0].Value = Recomend;
            parameters[1].Value = ProductId;
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

        public bool UpdateStatus(int ProductId, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where ProductID =@ProductId");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
                    	new SqlParameter("@ProductId", SqlDbType.Int,4)
					};
            parameters[0].Value = Status;
            parameters[1].Value = ProductId;
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

        public DataSet GetListEx(string strWhere, int CateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate ");
            strSql.Append(" FROM SNS_Products where");
            if (CateId > 0)
            {
                strSql.Append("   CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=" + CateId + " or Path like '" + CateId + "|%'))");
            }
            if (strWhere.Trim() != "")
            {
                if (CateId > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY CreatedDate DESC");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCountEx(string strWhere, int CateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_Products ");
            if (CateId > 0 || strWhere.Length > 1)
            {
                strSql.Append(" where ");
            }
            if (CateId > 0)
            {
                //
                strSql.Append("  CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=" + CateId + " or Path like  (select path from SNS_Categories where CategoryID=" + CateId + ")+'|%'))");
            }
            if (strWhere.Trim() != "")
            {
                if (CateId > 0)
                {
                    strSql.Append(" and ");
                }
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
                strSql.AppendFormat("order by T.{0} DESC", orderby);
            }
            else
            {
                strSql.Append("order by T.ProductID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Products T  ");
            if (CateId > 0 || strWhere.Length > 1)
            {
                strSql.Append(" where ");
            }
            if (CateId > 0)
            {
                // strSql.Append("  CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=" + CateId + " or Path like '" + CateId + "|%'))");
                //更正如下
                strSql.Append("  CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=" + CateId + " or Path like  (select path from SNS_Categories where CategoryID=" + CateId + ")+'|%'))");
            }
            if (strWhere.Length > 1)
            {
                if (CateId > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.Append(strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool UpdateRecommandState(int id, int State)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");

            strSql.Append("IsRecomend=@IsRecomend,");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4)};
            parameters[0].Value = State;
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
        /// 此人同样的商品是否发布了多遍
        /// </summary>
        public bool Exsit(string ProductName, int Uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Products");
            strSql.Append(" where ProductName=@ProductName and CreateUserID=@CreateUserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductName", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUserID", SqlDbType.Int,4)
			};
            parameters[0].Value = ProductName;
            parameters[1].Value = Uid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        ///  管理员批量插入数据去重
        /// </summary>
        public bool Exsit(long originalID, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Products");
            strSql.Append(" where originalID=@OriginalID and ProductSourceID=@ProductSourceID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OriginalID", SqlDbType.BigInt),
                    new SqlParameter("@ProductSourceID", SqlDbType.Int,4)
			};
            parameters[0].Value = originalID;
            parameters[1].Value = type;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据专辑ID获取该用户自定义上传的商品路径
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <returns>结果集</returns>
        public DataSet UserUploadProductsImage(int ablumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT P.* FROM SNS_Products P , ");
            strSql.Append("( SELECT TargetID ");
            strSql.Append("FROM SNS_UserAlbumDetail ");
            strSql.Append("WHERE AlbumID = @AlbumID ");
            strSql.Append("AND Type = 1 ) UAD , ");
            strSql.Append("( SELECT CreatedUserID ");
            strSql.Append("FROM SNS_UserAlbums ");
            strSql.Append("WHERE AlbumID = @AlbumID) U ");
            strSql.Append("WHERE P.ProductID = UAD.TargetID ");
            strSql.Append("AND P.CreateUserID = U.CreatedUserID ");
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
            parameters[0].Value = 2;
            parameters[1].Value = Ids;
            DataSet ds = DbHelperSQL.RunProcedure("sp_SNS_ImageDeleteAction", parameters, "tb", out Result);
            if (Result == 1)
            {
                return ds;
            }
            return null;
        }
        public bool UpdateClickCount(int ProuductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");

            strSql.Append("SkipCount=SkipCount+1 ");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.Int,4)};
            parameters[0].Value = ProuductId;
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
        /// 获取需要静态化的商品数据
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListToStatic(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ProductId from SNS_Products  ");
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
        public bool UpdateStaticUrl(int productId, string staticUrl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("StaticUrl=@StaticUrl");
            strSql.Append(" where ProductId=@ProductId");
            SqlParameter[] parameters = {
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)};
            parameters[0].Value = staticUrl;
            parameters[1].Value = productId;

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


        public string GetProductUrl(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProductUrl from SNS_Products ");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.BigInt)
			};
            parameters[0].Value = productId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }

        }

        public DataSet GetProductUserIds(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select CreateUserID from SNS_Products  where ProductID IN (" + ids + ") ");
            //  strSql.Append("ORDER BY AddedDate DESC ");
            return DbHelperSQL.Query((strSql.ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ExsitUrl(string ProductUrl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Products");
            strSql.Append(" where ProductUrl=@ProductUrl  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar)
			};
            parameters[0].Value = ProductUrl;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExsitUrl(string ProductUrl, int Uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Products");
            strSql.Append(" where ProductUrl=@ProductUrl and CreateUserID=@CreateUserID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUserID", SqlDbType.Int)
			};
            parameters[0].Value = ProductUrl;
            parameters[1].Value = Uid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        public bool UpdateStatusList(string ids, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Products set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where ProductID in (" + ids + ")  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4)
					};
            parameters[0].Value = status;
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
        /// 导入Excel数据
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="albumId"></param>
        /// <param name="categoryId"></param>
        /// <param name="dt"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int ImportExcelData(int userid, int albumId, int categoryId, DataTable dt, int status, bool ReRepeat)
        {
            string connectionString = PubConstant.GetConnectionString("ConnectionString");
            int count = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    int rowsCount = dt.Rows.Count;
                    Maticsoft.Model.SNS.Products model;
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into SNS_Products(");
                    strSql.Append("ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl)");
                    strSql.Append(" select ");
                    strSql.Append("@ProductName,@ProductDescription,@Price,@ProductSourceID,@CategoryID,@ProductUrl,@FavouriteCount,@GroupBuyCount,@CreateUserID, NickName ,@ThumbImageUrl,@NormalImageUrl,@PVCount,@IsRecomend,@Status,@Sequence,@TopCommentsId,@CommentCount,@ForwardedCount,@ShareDescription,@SkipCount,@OwnerProductId,@Tags,@CreatedDate,@Color,@OriginalID,@SourceType,@StaticUrl from Accounts_Users where UserID=@CreateUserID");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@ProductName", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductSourceID", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@ProductUrl", SqlDbType.NVarChar),
					new SqlParameter("@FavouriteCount", SqlDbType.Int,4),
					new SqlParameter("@GroupBuyCount", SqlDbType.Int,4),
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@PVCount", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@TopCommentsId", SqlDbType.NVarChar,100),
					new SqlParameter("@CommentCount", SqlDbType.Int,4),
					new SqlParameter("@ForwardedCount", SqlDbType.Int,4),
					new SqlParameter("@ShareDescription", SqlDbType.NVarChar,200),
					new SqlParameter("@SkipCount", SqlDbType.Int,4),
					new SqlParameter("@OwnerProductId", SqlDbType.Int,4),
					new SqlParameter("@Tags", SqlDbType.NVarChar,400),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Color", SqlDbType.NVarChar,100),
					new SqlParameter("@OriginalID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceType", SqlDbType.Int,4),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,300)};
                   

                    StringBuilder albumSql = new StringBuilder();
                    albumSql.Append("insert into SNS_UserAlbumDetail(");
                    albumSql.Append("AlbumID,TargetID,Type,Description,AlbumUserId)");
                    albumSql.Append(" values (");
                    albumSql.Append("@AlbumID,@TargetID,@Type,@Description,@AlbumUserId)");
                    SqlParameter[] albumparas = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
                    new SqlParameter("@AlbumUserId", SqlDbType.Int,4),     };

                    cmd.Connection = conn;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        #region 插入商品数据
                        model = new Maticsoft.Model.SNS.Products();
                        if (dt.Rows[n]["主图片"] != null && dt.Rows[n]["主图片"].ToString() != "")
                        {
                            model.NormalImageUrl = dt.Rows[n]["主图片"].ToString();
                            model.ThumbImageUrl = dt.Rows[n]["主图片"].ToString();
                        }
                        if (dt.Rows[n]["宝贝标题"] != null && dt.Rows[n]["宝贝标题"].ToString() != "")
                        {
                            model.ProductName = dt.Rows[n]["宝贝标题"].ToString();
                        }
                        if (dt.Rows[n]["商品单价"] != null && dt.Rows[n]["商品单价"].ToString() != "")
                        {
                            model.Price = Common.Globals.SafeDecimal(dt.Rows[n]["商品单价"].ToString(), -1);
                        }
                        if (dt.Rows[n]["单品链接"] != null && dt.Rows[n]["单品链接"].ToString() != "")
                        {
                            model.ProductUrl = dt.Rows[n]["单品链接"].ToString();
                        }
                        #region 判断重复

                        if (ReRepeat)
                        {
                            StringBuilder exsitSql = new StringBuilder();
                            exsitSql.Append("select count(1) from SNS_Products");
                            exsitSql.Append(" where ProductUrl=@ProductUrl1  ");
                            SqlParameter[] exsitpars = {
					        new SqlParameter("@ProductUrl1", SqlDbType.NVarChar)};
                            exsitpars[0].Value = model.ProductUrl;
                            cmd.CommandText = exsitSql.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(exsitpars);
                            object obj = cmd.ExecuteScalar();
                           
                            if (Common.Globals.SafeInt(obj.ToString(), 0)>0)
                            {
                                continue;
                            }
                          
                        }
                        #endregion
                        model.Status = status;
                        model.CreateUserID = userid;
                        model.CategoryID = categoryId;
                        model.SourceType = 1;
                        model.CreatedDate = DateTime.Now;
                        parameters[0].Value = model.ProductName;
                        parameters[1].Value = model.ProductDescription;
                        parameters[2].Value = model.Price;
                        parameters[3].Value = model.ProductSourceID;
                        parameters[4].Value = model.CategoryID;
                        parameters[5].Value = model.ProductUrl;
                        parameters[6].Value = model.FavouriteCount;
                        parameters[7].Value = model.GroupBuyCount;
                        parameters[8].Value = model.CreateUserID;
                        parameters[9].Value = model.ThumbImageUrl;
                        parameters[10].Value = model.NormalImageUrl;
                        parameters[11].Value = model.PVCount;
                        parameters[12].Value = model.IsRecomend;
                        parameters[13].Value = model.Status;
                        parameters[14].Value = model.Sequence;
                        parameters[15].Value = model.TopCommentsId;
                        parameters[16].Value = model.CommentCount;
                        parameters[17].Value = model.ForwardedCount;
                        parameters[18].Value = model.ShareDescription;
                        parameters[19].Value = model.SkipCount;
                        parameters[20].Value = model.OwnerProductId;
                        parameters[21].Value = model.Tags;
                        parameters[22].Value = model.CreatedDate;
                        parameters[23].Value = model.Color;
                        parameters[24].Value = model.OriginalID;
                        parameters[25].Value = model.SourceType;
                        parameters[26].Value = model.StaticUrl;

                        cmd.CommandText = strSql.ToString();
                        cmd.Parameters.Clear();
                        foreach (SqlParameter parameter in parameters)
                        {
                            if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                                (parameter.Value == null))
                            {
                                parameter.Value = DBNull.Value;
                            }
                            cmd.Parameters.Add(parameter);
                        }
                        long productId = Common.Globals.SafeLong(cmd.ExecuteScalar().ToString(),0);
                        #endregion

                        #region 插入用户专辑数据
                        if (productId > 0)
                        {
                            albumparas[0].Value = albumId;
                            albumparas[1].Value = productId;
                            albumparas[2].Value = 1;
                            albumparas[3].Value = "";
                            albumparas[4].Value = userid;
                            cmd.CommandText = albumSql.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(albumparas);
                            cmd.ExecuteNonQuery();
                            count++;
                        }
                        #endregion
                    }
                    #region 更新专辑Count
                    StringBuilder strSql1 = new StringBuilder();
                    strSql1.Append("update SNS_UserAlbums set ");
                    strSql1.Append("PhotoCount=PhotoCount+ " + count);
                    strSql1.Append(" where AlbumID=@AlbumID1");
                    SqlParameter[] parameters1 = { new SqlParameter("@AlbumID1", SqlDbType.Int, 4) };
                    parameters1[0].Value = albumId;
                    cmd.CommandText = strSql1.ToString();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parameters1);
                    cmd.ExecuteNonQuery();
                    #endregion
                }
            }
            return count;
        }

        #endregion ExtensionMethod
    }
}