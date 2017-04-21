/**  版本信息模板在安装目录下，可自行修改。
* GoodsType.cs
*
* 功 能： N/A
* 类 名： GoodsType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/25 10:14:27   N/A    初版
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
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:GoodsType
    /// </summary>
    public partial class GoodsType : IGoodsType
    {
        public GoodsType()
        { }
        #region  BasicMethod

        /// <summary>
        /// 获取最大的Sort
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort()
        {
            return DbHelperSQL.GetMaxID("Sort", "TB_GoodsType");
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByTypeName(string TypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TB_GoodsType");
            strSql.Append(" where GoodTypeName=@TypeName ");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,200)};
            parameters[0].Value = TypeName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.GoodsType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TB_GoodsType(");
            strSql.Append("GoodTypeName,Sort,PID,Path,IshasClass,EntryPicPath,BannerPicPath,BgColor)");
            strSql.Append(" values (");
            strSql.Append("@GoodTypeName,@Sort,@PID,@Path,@IshasClass,@EntryPicPath,@BannerPicPath,@BgColor)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@PID", SqlDbType.Int,4),
                    new SqlParameter("@Path", SqlDbType.NVarChar,100),
                    new SqlParameter("@IshasClass", SqlDbType.Int,4),
                    new SqlParameter("@EntryPicPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@BannerPicPath", SqlDbType.NVarChar,2000),
                    new SqlParameter("@BgColor", SqlDbType.NVarChar,10)
                                        };
            parameters[0].Value = model.GoodTypeName;
            parameters[1].Value = model.Sort;
            parameters[2].Value = model.PID;
            parameters[3].Value = model.Path;
            parameters[4].Value = model.IshasClass;
            parameters[5].Value = model.EntryPicPath;
            parameters[6].Value = model.BannerPicPath;
            parameters[7].Value = model.BgColor;
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
        public bool Update(Maticsoft.Model.Shop.Products.GoodsType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_GoodsType set ");
            strSql.Append("GoodTypeName=@GoodTypeName,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("PID=@PID,");
            strSql.Append("IshasClass=@IshasClass,");
            strSql.Append("Path=@Path,");
            strSql.Append("EntryPicPath=@EntryPicPath,");
            strSql.Append("BannerPicPath=@BannerPicPath,");
            strSql.Append("BgColor=@BgColor");
            strSql.Append(" where GoodTypeID=@GoodTypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@PID", SqlDbType.Int,4),
                    new SqlParameter("@IshasClass", SqlDbType.Int,4),
                    new SqlParameter("@Path", SqlDbType.NVarChar,100),
                    new SqlParameter("@EntryPicPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@BannerPicPath", SqlDbType.NVarChar,2000),
                    new SqlParameter("@BgColor", SqlDbType.NVarChar,10),
                    new SqlParameter("@GoodTypeID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.GoodTypeName;
            parameters[1].Value = model.Sort;
            parameters[2].Value = model.PID;
            parameters[3].Value = model.IshasClass;
            parameters[4].Value = model.Path;
            parameters[5].Value = model.EntryPicPath;
            parameters[6].Value = model.BannerPicPath;
            parameters[7].Value = model.BgColor;
            parameters[8].Value = model.GoodTypeID;

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
        public bool Delete(int GoodTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_GoodsType ");
            strSql.Append(" where GoodTypeID=@GoodTypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodTypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = GoodTypeID;

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
        public bool DeleteList(string GoodTypeIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_GoodsType ");
            strSql.Append(" where GoodTypeID in (" + GoodTypeIDlist + ")  ");
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
        public Maticsoft.Model.Shop.Products.GoodsType GetModel(int GoodTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GoodTypeID,GoodTypeName,Sort,PID, IshasClass, Path, EntryPicPath, BannerPicPath, BgColor from TB_GoodsType ");
            strSql.Append(" where GoodTypeID=@GoodTypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodTypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = GoodTypeID;

            Maticsoft.Model.Shop.Products.GoodsType model = new Maticsoft.Model.Shop.Products.GoodsType();
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
        public Model.Shop.Products.GoodsType GetModelBuyPID(int PID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GoodTypeID,GoodTypeName,Sort,PID,IshasClass,Path,EntryPicPath,BannerPicPath,BgColor from TB_GoodsType ");
            strSql.Append(" where GoodTypeID=@PID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PID", SqlDbType.Int,4)};
            parameters[0].Value = PID;

            Model.Shop.Products.GoodsType goodsModel = new Model.Shop.Products.GoodsType();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["GoodTypeID"] != null && ds.Tables[0].Rows[0]["GoodTypeID"].ToString() != "")
                {
                    goodsModel.GoodTypeID = int.Parse(ds.Tables[0].Rows[0]["GoodTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GoodTypeName"] != null && ds.Tables[0].Rows[0]["GoodTypeName"].ToString() != "")
                {
                    goodsModel.GoodTypeName = ds.Tables[0].Rows[0]["GoodTypeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PID"] != null && ds.Tables[0].Rows[0]["PID"].ToString() != "")
                {
                    goodsModel.PID = int.Parse(ds.Tables[0].Rows[0]["PID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"] != null && ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    goodsModel.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Path"] != null && ds.Tables[0].Rows[0]["Path"].ToString() != "")
                {
                    goodsModel.Path = ds.Tables[0].Rows[0]["Path"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IshasClass"] != null && ds.Tables[0].Rows[0]["IshasClass"].ToString() != "")
                {
                    goodsModel.IshasClass = int.Parse(ds.Tables[0].Rows[0]["IshasClass"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EntryPicPath"] != null && ds.Tables[0].Rows[0]["EntryPicPath"].ToString() != "")
                {
                    goodsModel.EntryPicPath = ds.Tables[0].Rows[0]["EntryPicPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BannerPicPath"] != null && ds.Tables[0].Rows[0]["BannerPicPath"].ToString() != "")
                {
                    goodsModel.BannerPicPath = ds.Tables[0].Rows[0]["BannerPicPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BgColor"] != null && ds.Tables[0].Rows[0]["BgColor"].ToString() != "")
                {
                    goodsModel.BgColor = ds.Tables[0].Rows[0]["BgColor"].ToString();
                }
                return goodsModel;
            }
            return null;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.GoodsType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.GoodsType model = new Maticsoft.Model.Shop.Products.GoodsType();
            if (row != null)
            {
                if (row["GoodTypeID"] != null && row["GoodTypeID"].ToString() != "")
                {
                    model.GoodTypeID = int.Parse(row["GoodTypeID"].ToString());
                }
                if (row["GoodTypeName"] != null)
                {
                    model.GoodTypeName = row["GoodTypeName"].ToString();
                }
                if (row["Sort"] != null && row["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(row["Sort"].ToString());
                }
                if (row["PID"] != null && row["PID"].ToString() != "")
                {
                    model.PID = int.Parse(row["PID"].ToString());
                }
                if (row["IshasClass"] != null && row["IshasClass"].ToString() != "")
                {
                    model.IshasClass = int.Parse(row["IshasClass"].ToString());
                }
                if (row["Path"] != null && row["Path"].ToString() != "")
                {
                    model.Path = row["Path"].ToString();
                }
                if (row.Table.Columns.Contains("EntryPicPath"))
                {
                    model.EntryPicPath = null == row["EntryPicPath"] ? string.Empty : row["EntryPicPath"].ToString().Trim();
                }
                if (row.Table.Columns.Contains("BannerPicPath"))
                {
                    model.BannerPicPath = null == row["BannerPicPath"] ? string.Empty : row["BannerPicPath"].ToString().Trim();
                }
                if (row.Table.Columns.Contains("BgColor"))
                {
                    model.BgColor = null == row["BgColor"] ? string.Empty : row["BgColor"].ToString().Trim();
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
            strSql.Append("select GoodTypeID,GoodTypeName,Sort,PID, IshasClass, Path, EntryPicPath, BannerPicPath, BgColor ");
            strSql.Append(" FROM TB_GoodsType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sort asc");
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
            strSql.Append(" GoodTypeID,GoodTypeName,Sort,PID, IshasClass, Path, EntryPicPath, BannerPicPath, BgColor ");
            strSql.Append(" FROM TB_GoodsType ");
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
            strSql.Append("select count(1) FROM TB_GoodsType ");
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
                strSql.Append("order by T.GoodTypeID desc");
            }
            strSql.Append(")AS Row, T.*  from TB_GoodsType T ");
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
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "TB_GoodsType";
            parameters[1].Value = "GoodTypeID";
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
        /// 获得活动分类列表数据列表
        /// </summary>
        public DataSet GetGoodsActiveTypeList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[Name],[StatusType],[Status] ");
            strSql.Append(" FROM TB_GoodsActiveType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sort asc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  ExtensionMethod
    }
}

