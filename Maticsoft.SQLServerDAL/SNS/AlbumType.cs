/**
* AlbumType.cs
*
* 功 能： N/A
* 类 名： AlbumType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:40   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.SNS;
using System.Collections.Generic;

namespace Maticsoft.SQLServerDAL.SNS
{
    /// <summary>
    /// 数据访问类:AlbumType
    /// </summary>
    public partial class AlbumType : IAlbumType
    {
        public AlbumType()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在该名称
        /// </summary>
        public bool Exists(string TypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_AlbumType");
            strSql.Append(" where TypeName=@TypeName");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = TypeName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.AlbumType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_AlbumType(");
            strSql.Append("TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark)");
            strSql.Append(" values (");
            strSql.Append("@TypeName,@IsMenu,@MenuIsShow,@MenuSequence,@AlbumsCount,@Status,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsMenu", SqlDbType.Bit,1),
					new SqlParameter("@MenuIsShow", SqlDbType.Bit,1),
					new SqlParameter("@MenuSequence", SqlDbType.Int,4),
					new SqlParameter("@AlbumsCount", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.IsMenu;
            parameters[2].Value = model.MenuIsShow;
            parameters[3].Value = model.MenuSequence;
            parameters[4].Value = model.AlbumsCount;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.SNS.AlbumType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_AlbumType set ");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("IsMenu=@IsMenu,");
            strSql.Append("MenuIsShow=@MenuIsShow,");
            strSql.Append("MenuSequence=@MenuSequence,");
            strSql.Append("AlbumsCount=@AlbumsCount,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsMenu", SqlDbType.Bit,1),
					new SqlParameter("@MenuIsShow", SqlDbType.Bit,1),
					new SqlParameter("@MenuSequence", SqlDbType.Int,4),
					new SqlParameter("@AlbumsCount", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.IsMenu;
            parameters[2].Value = model.MenuIsShow;
            parameters[3].Value = model.MenuSequence;
            parameters[4].Value = model.AlbumsCount;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.ID;

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
            strSql.Append("delete from SNS_AlbumType ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_AlbumType ");
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
        public Maticsoft.Model.SNS.AlbumType GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark from SNS_AlbumType ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.SNS.AlbumType model = new Maticsoft.Model.SNS.AlbumType();
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
        public Maticsoft.Model.SNS.AlbumType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.AlbumType model = new Maticsoft.Model.SNS.AlbumType();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["TypeName"] != null && row["TypeName"].ToString() != "")
                {
                    model.TypeName = row["TypeName"].ToString();
                }
                if (row["IsMenu"] != null && row["IsMenu"].ToString() != "")
                {
                    if ((row["IsMenu"].ToString() == "1") || (row["IsMenu"].ToString().ToLower() == "true"))
                    {
                        model.IsMenu = true;
                    }
                    else
                    {
                        model.IsMenu = false;
                    }
                }
                if (row["MenuIsShow"] != null && row["MenuIsShow"].ToString() != "")
                {
                    if ((row["MenuIsShow"].ToString() == "1") || (row["MenuIsShow"].ToString().ToLower() == "true"))
                    {
                        model.MenuIsShow = true;
                    }
                    else
                    {
                        model.MenuIsShow = false;
                    }
                }
                if (row["MenuSequence"] != null && row["MenuSequence"].ToString() != "")
                {
                    model.MenuSequence = int.Parse(row["MenuSequence"].ToString());
                }
                if (row["AlbumsCount"] != null && row["AlbumsCount"].ToString() != "")
                {
                    model.AlbumsCount = int.Parse(row["AlbumsCount"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Remark"] != null && row["Remark"].ToString() != "")
                {
                    model.Remark = row["Remark"].ToString();
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
            strSql.Append("select ID,TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark ");
            strSql.Append(" FROM SNS_AlbumType ");
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
            strSql.Append(" ID,TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark ");
            strSql.Append(" FROM SNS_AlbumType ");
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
                strSql.Append(" order by ID desc");
            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SNS_AlbumType ");
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
            strSql.Append(")AS Row, T.*  from SNS_AlbumType T ");
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
            parameters[0].Value = "AlbumType";
            parameters[1].Value = "ID";
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsMenu(int IsMenu, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_AlbumType set ");
            strSql.AppendFormat(" IsMenu={0} ", IsMenu);
            strSql.AppendFormat(" where ID IN({0})", IdList);

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
        public bool UpdateMenuIsShow(int MenuIsShow, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_AlbumType set ");
            strSql.AppendFormat(" MenuIsShow={0} ", MenuIsShow);
            strSql.AppendFormat(" where ID IN({0})", IdList);

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
        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_AlbumType set ");
            strSql.AppendFormat(" Status={0} ", Status);
            strSql.AppendFormat(" where ID IN({0})", IdList);

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
        /// 获得数据列表
        /// </summary>
        public DataSet GetTypeList(int albumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SNS_AlbumType where ID in (select TypeID from  SNS_UserAlbumsType where AlbumsID=" + albumId + ") ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion ExtensionMethod
    }
}