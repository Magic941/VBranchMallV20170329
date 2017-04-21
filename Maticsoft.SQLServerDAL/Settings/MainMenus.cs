/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。 
//
// 文件名：WebMenuConfig.cs
// 文件功能描述：网站菜单接口
// 
// 创建标识：
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
using Maticsoft.Common;
using System.Collections.Generic;

namespace Maticsoft.SQLServerDAL.Settings
{
    /// <summary>
    /// 数据访问类:WebMenuConfig
    /// </summary>
    public partial class MainMenus : IMainMenus
    {
        public MainMenus()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("MenuID", "SA_WebMenuConfig");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int MenuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SA_WebMenuConfig");
            strSql.Append(" where MenuID=@MenuID");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuID", SqlDbType.Int,4)
			};
            parameters[0].Value = MenuID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Settings.MainMenus model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_WebMenuConfig(");
            strSql.Append("MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme)");
            strSql.Append(" values (");
            strSql.Append("@MenuName,@NavURL,@MenuTitle,@MenuType,@Target,@IsUsed,@Sequence,@Visible,@NavArea,@URLType,@NavTheme)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuName", SqlDbType.NVarChar,50),
					new SqlParameter("@NavURL", SqlDbType.NVarChar,-1),
					new SqlParameter("@MenuTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@MenuType", SqlDbType.Int,4),
					new SqlParameter("@Target", SqlDbType.Int,4),
					new SqlParameter("@IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Visible", SqlDbType.Int,4),
					new SqlParameter("@NavArea", SqlDbType.Int,4),
					new SqlParameter("@URLType", SqlDbType.Int,4),
					new SqlParameter("@NavTheme", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.MenuName;
            parameters[1].Value = model.NavURL;
            parameters[2].Value = model.MenuTitle;
            parameters[3].Value = model.MenuType;
            parameters[4].Value = model.Target;
            parameters[5].Value = model.IsUsed;
            parameters[6].Value = model.Sequence;
            parameters[7].Value = model.Visible;
            parameters[8].Value = model.NavArea;
            parameters[9].Value = model.URLType;
            parameters[10].Value = model.NavTheme;

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
        public bool Update(Maticsoft.Model.Settings.MainMenus model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_WebMenuConfig set ");
            strSql.Append("MenuName=@MenuName,");
            strSql.Append("NavURL=@NavURL,");
            strSql.Append("MenuTitle=@MenuTitle,");
            strSql.Append("MenuType=@MenuType,");
            strSql.Append("Target=@Target,");
            strSql.Append("IsUsed=@IsUsed,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("Visible=@Visible,");
            strSql.Append("NavArea=@NavArea,");
            strSql.Append("URLType=@URLType,");
            strSql.Append("NavTheme=@NavTheme");
            strSql.Append(" where MenuID=@MenuID");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuName", SqlDbType.NVarChar,50),
					new SqlParameter("@NavURL", SqlDbType.NVarChar,-1),
					new SqlParameter("@MenuTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@MenuType", SqlDbType.Int,4),
					new SqlParameter("@Target", SqlDbType.Int,4),
					new SqlParameter("@IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Visible", SqlDbType.Int,4),
					new SqlParameter("@NavArea", SqlDbType.Int,4),
					new SqlParameter("@URLType", SqlDbType.Int,4),
					new SqlParameter("@NavTheme", SqlDbType.NVarChar,100),
					new SqlParameter("@MenuID", SqlDbType.Int,4)};
            parameters[0].Value = model.MenuName;
            parameters[1].Value = model.NavURL;
            parameters[2].Value = model.MenuTitle;
            parameters[3].Value = model.MenuType;
            parameters[4].Value = model.Target;
            parameters[5].Value = model.IsUsed;
            parameters[6].Value = model.Sequence;
            parameters[7].Value = model.Visible;
            parameters[8].Value = model.NavArea;
            parameters[9].Value = model.URLType;
            parameters[10].Value = model.NavTheme;
            parameters[11].Value = model.MenuID;

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
        public bool Delete(int MenuID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_WebMenuConfig ");
            strSql.Append(" where MenuID=@MenuID");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuID", SqlDbType.Int,4)
			};
            parameters[0].Value = MenuID;

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
        public bool DeleteList(string MenuIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_WebMenuConfig ");
            strSql.Append(" where MenuID in (" + MenuIDlist + ")  ");
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
        public Maticsoft.Model.Settings.MainMenus GetModel(int MenuID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MenuID,MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme from SA_WebMenuConfig ");
            strSql.Append(" where MenuID=@MenuID");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuID", SqlDbType.Int,4)
			};
            parameters[0].Value = MenuID;

            Maticsoft.Model.Settings.MainMenus model = new Maticsoft.Model.Settings.MainMenus();
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
        public Maticsoft.Model.Settings.MainMenus DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Settings.MainMenus model = new Maticsoft.Model.Settings.MainMenus();
            if (row != null)
            {
                if (row["MenuID"] != null && row["MenuID"].ToString() != "")
                {
                    model.MenuID = int.Parse(row["MenuID"].ToString());
                }
                if (row["MenuName"] != null)
                {
                    model.MenuName = row["MenuName"].ToString();
                }
                if (row["NavURL"] != null)
                {
                    model.NavURL = row["NavURL"].ToString();
                }
                if (row["MenuTitle"] != null)
                {
                    model.MenuTitle = row["MenuTitle"].ToString();
                }
                if (row["MenuType"] != null && row["MenuType"].ToString() != "")
                {
                    model.MenuType = int.Parse(row["MenuType"].ToString());
                }
                if (row["Target"] != null && row["Target"].ToString() != "")
                {
                    model.Target = int.Parse(row["Target"].ToString());
                }
                if (row["IsUsed"] != null && row["IsUsed"].ToString() != "")
                {
                    if ((row["IsUsed"].ToString() == "1") || (row["IsUsed"].ToString().ToLower() == "true"))
                    {
                        model.IsUsed = true;
                    }
                    else
                    {
                        model.IsUsed = false;
                    }
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["Visible"] != null && row["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(row["Visible"].ToString());
                }
                if (row["NavArea"] != null && row["NavArea"].ToString() != "")
                {
                    model.NavArea = int.Parse(row["NavArea"].ToString());
                }
                if (row["URLType"] != null && row["URLType"].ToString() != "")
                {
                    model.URLType = int.Parse(row["URLType"].ToString());
                }
                if (row["NavTheme"] != null)
                {
                    model.NavTheme = row["NavTheme"].ToString();
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
            strSql.Append("select MenuID,MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme ");
            strSql.Append(" FROM SA_WebMenuConfig ");
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
            strSql.Append(" MenuID,MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme ");
            strSql.Append(" FROM SA_WebMenuConfig ");
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
            strSql.Append("select count(1) FROM SA_WebMenuConfig ");
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
                strSql.Append("order by T.MenuID desc");
            }
            strSql.Append(")AS Row, T.*  from SA_WebMenuConfig T ");
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
            parameters[0].Value = "SA_WebMenuConfig";
            parameters[1].Value = "MenuID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
    }
}

