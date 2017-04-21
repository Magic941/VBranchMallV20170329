/**
* Menu.cs
*
* 功 能： N/A
* 类 名： Menu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/17 12:25:28   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:Menu
	/// </summary>
	public partial class Menu:IMenu
	{
		public Menu()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("MenuId", "WeChat_Menu");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int MenuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_Menu");
            strSql.Append(" where MenuId=@MenuId");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuId", SqlDbType.Int,4)
			};
            parameters[0].Value = MenuId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_Menu(");
            strSql.Append("OpenId,ParentId,Name,Type,Sequence,MenuKey,MenuUrl,Status,CreateDate,Remark,HasChildren)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@ParentId,@Name,@Type,@Sequence,@MenuKey,@MenuUrl,@Status,@CreateDate,@Remark,@HasChildren)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@MenuKey", SqlDbType.NVarChar,50),
					new SqlParameter("@MenuUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Sequence;
            parameters[5].Value = model.MenuKey;
            parameters[6].Value = model.MenuUrl;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.CreateDate;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.HasChildren;

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
        public bool Update(Maticsoft.WeChat.Model.Core.Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_Menu set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("Name=@Name,");
            strSql.Append("Type=@Type,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("MenuKey=@MenuKey,");
            strSql.Append("MenuUrl=@MenuUrl,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("HasChildren=@HasChildren");
            strSql.Append(" where MenuId=@MenuId");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@MenuKey", SqlDbType.NVarChar,50),
					new SqlParameter("@MenuUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@MenuId", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Sequence;
            parameters[5].Value = model.MenuKey;
            parameters[6].Value = model.MenuUrl;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.CreateDate;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.HasChildren;
            parameters[11].Value = model.MenuId;

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
        public bool Delete(int MenuId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_Menu ");
            strSql.Append(" where MenuId=@MenuId");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuId", SqlDbType.Int,4)
			};
            parameters[0].Value = MenuId;

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
        public bool DeleteList(string MenuIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_Menu ");
            strSql.Append(" where MenuId in (" + MenuIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.Menu GetModel(int MenuId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MenuId,OpenId,ParentId,Name,Type,Sequence,MenuKey,MenuUrl,Status,CreateDate,Remark,HasChildren from WeChat_Menu ");
            strSql.Append(" where MenuId=@MenuId");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuId", SqlDbType.Int,4)
			};
            parameters[0].Value = MenuId;

            Maticsoft.WeChat.Model.Core.Menu model = new Maticsoft.WeChat.Model.Core.Menu();
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
        public Maticsoft.WeChat.Model.Core.Menu DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.Menu model = new Maticsoft.WeChat.Model.Core.Menu();
            if (row != null)
            {
                if (row["MenuId"] != null && row["MenuId"].ToString() != "")
                {
                    model.MenuId = int.Parse(row["MenuId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["ParentId"] != null && row["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(row["ParentId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = row["Type"].ToString();
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["MenuKey"] != null)
                {
                    model.MenuKey = row["MenuKey"].ToString();
                }
                if (row["MenuUrl"] != null)
                {
                    model.MenuUrl = row["MenuUrl"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["HasChildren"] != null && row["HasChildren"].ToString() != "")
                {
                    if ((row["HasChildren"].ToString() == "1") || (row["HasChildren"].ToString().ToLower() == "true"))
                    {
                        model.HasChildren = true;
                    }
                    else
                    {
                        model.HasChildren = false;
                    }
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
            strSql.Append("select MenuId,OpenId,ParentId,Name,Type,Sequence,MenuKey,MenuUrl,Status,CreateDate,Remark,HasChildren ");
            strSql.Append(" FROM WeChat_Menu ");
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
            strSql.Append(" MenuId,OpenId,ParentId,Name,Type,Sequence,MenuKey,MenuUrl,Status,CreateDate,Remark,HasChildren ");
            strSql.Append(" FROM WeChat_Menu ");
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
            strSql.Append("select count(1) FROM WeChat_Menu ");
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
                strSql.Append("order by T.MenuId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_Menu T ");
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
            parameters[0].Value = "WeChat_Menu";
            parameters[1].Value = "MenuId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

	    public bool UpdateSeq(int seq, int menuId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_Menu set ");
            strSql.Append("Sequence=@Sequence");
            strSql.Append(" where MenuId=@MenuId");
            SqlParameter[] parameters = {
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@MenuId", SqlDbType.Int,4)};
            parameters[0].Value = seq;
            parameters[1].Value = menuId;

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

        public int GetSequence(string openId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT MAX(Sequence) FROM WeChat_Menu where OpenId=@OpenId");
            SqlParameter[] parameters = {
						new SqlParameter("@OpenId", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = openId;
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

        public bool AddEx(Maticsoft.WeChat.Model.Core.Menu model)
	    {

            // 事件添加
            List<CommandInfo> sqllist = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_Menu(");
            strSql.Append("OpenId,ParentId,Name,Type,Sequence,MenuKey,MenuUrl,Status,CreateDate,Remark,HasChildren)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@ParentId,@Name,@Type,@Sequence,@MenuKey,@MenuUrl,@Status,@CreateDate,@Remark,@HasChildren)");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@MenuKey", SqlDbType.NVarChar,50),
					new SqlParameter("@MenuUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Sequence;
            parameters[5].Value = model.MenuKey;
            parameters[6].Value = model.MenuUrl;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.CreateDate;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.HasChildren;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //更新父级
            if (model.ParentId > 0)
            {
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("update WeChat_Menu set ");
                strSql1.Append("HasChildren=@HasChildren");
                strSql1.Append(" where MenuId=@MenuId");
                SqlParameter[] parameters1 = {
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@MenuId", SqlDbType.Int,4)};
                parameters1[0].Value = true;
                parameters1[1].Value = model.ParentId;
                CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
                sqllist.Add(cmd1);
            }
            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
      
	    }

	    public bool DeleteEx(int menuId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_Menu ");
            strSql.Append(" where MenuId=@MenuId or ParentId=@MenuId");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuId", SqlDbType.Int,4)
			};
            parameters[0].Value = menuId;

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

      public  Maticsoft.WeChat.Model.Core.Menu GetMenu(string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from WeChat_Menu ");
            strSql.Append(" where MenuKey=@MenuKey and Type='click'");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuKey", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = key;

            Maticsoft.WeChat.Model.Core.Menu model = new Maticsoft.WeChat.Model.Core.Menu();
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
	    #endregion  ExtensionMethod
	}
}

