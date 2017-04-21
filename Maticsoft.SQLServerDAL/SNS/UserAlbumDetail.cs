/**
* UserAlbumDetail.cs
*
* 功 能： N/A
* 类 名： UserAlbumDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:00   N/A    初版
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
using System.Collections.Generic;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SNS
{
    /// <summary>
    /// 数据访问类:UserAlbumDetail
    /// </summary>
    public partial class UserAlbumDetail : IUserAlbumDetail
    {
        public UserAlbumDetail()
        { }
        #region  BasicMethod



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AlbumID, int TargetID, int Type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_UserAlbumDetail");
            strSql.Append(" where AlbumID=@AlbumID and TargetID=@TargetID and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)			};
            parameters[0].Value = AlbumID;
            parameters[1].Value = TargetID;
            parameters[2].Value = Type;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserAlbumDetail(");
            strSql.Append("AlbumID,TargetID,Type,Description)");
            strSql.Append(" values (");
            strSql.Append("@AlbumID,@TargetID,@Type,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
                    new SqlParameter("@AlbumUserId", SqlDbType.Int,4),     };
            parameters[0].Value = model.AlbumID;
            parameters[1].Value = model.TargetID;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.AlbumUserId;

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
        public bool Update(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_UserAlbumDetail set ");
            strSql.Append("Description=@Description");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = model.Description;
            parameters[1].Value = model.ID;
            parameters[2].Value = model.AlbumID;
            parameters[3].Value = model.TargetID;
            parameters[4].Value = model.Type;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserAlbumDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool Delete(int AlbumID, int TargetID, int Type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserAlbumDetail ");
            strSql.Append(" where AlbumID=@AlbumID and TargetID=@TargetID and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)			};
            parameters[0].Value = AlbumID;
            parameters[1].Value = TargetID;
            parameters[2].Value = Type;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_UserAlbumDetail ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public Maticsoft.Model.SNS.UserAlbumDetail GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,AlbumID,TargetID,Type,Description from SNS_UserAlbumDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.SNS.UserAlbumDetail model = new Maticsoft.Model.SNS.UserAlbumDetail();
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
        public Maticsoft.Model.SNS.UserAlbumDetail DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserAlbumDetail model = new Maticsoft.Model.SNS.UserAlbumDetail();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["AlbumID"] != null && row["AlbumID"].ToString() != "")
                {
                    model.AlbumID = int.Parse(row["AlbumID"].ToString());
                }
                if (row["TargetID"] != null && row["TargetID"].ToString() != "")
                {
                    model.TargetID = int.Parse(row["TargetID"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
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
            strSql.Append("select ID,AlbumID,TargetID,Type,Description ");
            strSql.Append(" FROM SNS_UserAlbumDetail ");
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
            strSql.Append(" ID,AlbumID,TargetID,Type,Description ");
            strSql.Append(" FROM SNS_UserAlbumDetail ");
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
            strSql.Append("select count(1) FROM SNS_UserAlbumDetail ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_UserAlbumDetail T ");
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
            parameters[0].Value = "SNS_UserAlbumDetail";
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
        /// 获得专辑的推荐产品照片
        /// </summary>
        public List<string> GetThumbImageByAlbum(int AlbumID,int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT TOP 9 A.ThumbImageUrl FROM    ( ");
            switch (type)
            {
                case 0:
                    strSql.AppendFormat(
                        " SELECT TOP 9 p.ThumbImageUrl ,  p.PhotoID ID  FROM      SNS_UserAlbumDetail d ,SNS_Photos p WHERE     d.Type = 0 AND d.AlbumID = {0} AND d.TargetID = p.PhotoID",
                        AlbumID);
                    break;
                case 1:
                    strSql.AppendFormat(
                     " SELECT TOP 9 p.ThumbImageUrl , p.ProductID ID FROM      SNS_UserAlbumDetail d ,SNS_Products p WHERE     d.Type = 1 AND d.AlbumID = {0} AND d.TargetID = p.ProductID",
                     AlbumID);
                    break;

                default:
                    strSql.AppendFormat(
                  " SELECT TOP 9 p.ThumbImageUrl ,  p.PhotoID ID  FROM      SNS_UserAlbumDetail d ,SNS_Photos p WHERE     d.Type = 0 AND d.AlbumID = {0} AND d.TargetID = p.PhotoID",
                  AlbumID);
                    strSql.Append(" UNION ");
                    strSql.AppendFormat(
              " SELECT TOP 9 p.ThumbImageUrl , p.ProductID ID FROM      SNS_UserAlbumDetail d ,SNS_Products p WHERE     d.Type = 1 AND d.AlbumID = {0} AND d.TargetID = p.ProductID",
              AlbumID);
                    break;

            }
            strSql.Append("        ) A ORDER BY A.ID DESC  ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            List<string> imglist = new List<string>();
            if (ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr != null)
                    {
                        //专辑的产品图片 300x300 替换为 60x60 的以减少流量损耗, BEN ADD 2012-11-26
                        imglist.Add(dr["ThumbImageUrl"].ToString().Replace("300x300", "60x60"));
                    }
                }
            }
            return imglist;
        }


        /// <summary>
        /// 获得专辑下的图片记录总数
        /// </summary>
        public int GetRecordCount4AlbumImgByAlbumID(int albumID, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) FROM SNS_UserAlbumDetail  where  AlbumID={0}", albumID);
            if (type != -1)
            {
                strSql.AppendFormat(" and type={0}", type);
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
        /// 获得专辑下的图片数据列表
        /// </summary>
        public DataSet GetAlbumImgListByPage(int albumID, string orderby, int startIndex, int endIndex, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from ");
            //GetAlbumImgListByPage 结果集中的Type字段 并非来自于Photo表, 而是在Sql文中增加的固定字段 0:数据来自Photo表 1:数据来自产品表
            switch (type)
            {
                case  0:
                                strSql.Append(" (select uad.ID,p.PhotoID TargetID,p.PhotoName TargetName,p.Description Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,0 price,0 Type ");
            strSql.AppendFormat("  FROM      SNS_UserAlbumDetail uad   INNER JOIN SNS_Photos p ON uad.TargetID = p.PhotoID AND uad.Type = 0  AND uad.AlbumID = {0}",albumID);
                    break;
                case  1:
                                   strSql.Append(" (select uad.ID,p.ProductID TargetID,p.ProductName TargetName,p.ShareDescription Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,p.Price,1 Type");
                      strSql.AppendFormat(" FROM   SNS_UserAlbumDetail uad  INNER JOIN SNS_Products p ON uad.TargetID = p.ProductID AND uad.Type = 1 AND uad.AlbumID = {0}", albumID);
                    break;
                default:
                      strSql.Append(" (select uad.ID,p.ProductID TargetID,p.ProductName TargetName,p.ShareDescription Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,p.Price,1 Type");
                      strSql.AppendFormat(" FROM   SNS_UserAlbumDetail uad  INNER JOIN SNS_Products p ON uad.TargetID = p.ProductID AND uad.Type = 1 AND uad.AlbumID = {0}", albumID);
            strSql.Append(" union");
            strSql.Append(" select uad.ID,p.PhotoID TargetID,p.PhotoName TargetName,p.Description Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,0 price,0 Type ");
            strSql.AppendFormat("  FROM      SNS_UserAlbumDetail uad   INNER JOIN SNS_Photos p ON uad.TargetID = p.PhotoID AND uad.Type = 0  AND uad.AlbumID = {0}",albumID);
          
                    break;
            }
            strSql.Append("  )T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);

            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool AddEx(Maticsoft.Model.SNS.UserAlbumDetail model)
        {

            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_UserAlbumDetail(");
            strSql.Append("AlbumID,TargetID,Type,Description,AlbumUserId)");
            strSql.Append(" values (");
            strSql.Append("@AlbumID,@TargetID,@Type,@Description,@AlbumUserId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4),
					new SqlParameter("@TargetID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
                    new SqlParameter("@AlbumUserId", SqlDbType.Int,4),     };
            parameters[0].Value = model.AlbumID;
            parameters[1].Value = model.TargetID;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.AlbumUserId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update SNS_UserAlbums set ");
            strSql1.Append("PhotoCount=PhotoCount+1 ");
            strSql1.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameters1 = { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameters1[0].Value = model.AlbumID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;

        }



        /// <summary>
        /// 删除专辑中具体图片的信息的动作
        /// </summary>
        /// <param name="TargetId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool DeleteEx(int AlbumID, int TargetId, int Type)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            #region 删除专辑中的具体图片
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from SNS_UserAlbumDetail ");
            strSql1.Append(" where TargetID=@TargetID and Type=@Type and AlbumID=@AlbumID");
            SqlParameter[] parameters1 = {
					new SqlParameter("@TargetID", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters1[0].Value = TargetId;
            parameters1[1].Value = Type;
            parameters1[2].Value = AlbumID;
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd1);
            #endregion

            #region  专辑图片数量-1
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update SNS_UserAlbums set PhotoCount=PhotoCount-1");
            strSql.Append(" where AlbumID=@AlbumID ");


            SqlParameter[] parameters = {
					new SqlParameter("@AlbumID", SqlDbType.Int,4)
			};
            parameters[0].Value = AlbumID;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            #endregion

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;

        }
        #endregion  ExtensionMethod
    }
}

