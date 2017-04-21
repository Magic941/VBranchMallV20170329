/**
* Categories.cs
*
* 功 能： N/A
* 类 名： Categories
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
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;
using System.Collections.Generic;
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:Categories
	/// </summary>
	public partial class Categories:ICategories
	{
		public Categories()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("CategoryId", "SNS_Categories");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CategoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Categories");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Categories model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Categories(");
            strSql.Append("Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Description,@ParentID,@Path,@Depth,@Sequence,@HasChildren,@IsMenu,@Type,@MenuIsShow,@MenuSequence,@FontColor,@CreatedUserID,@CreatedDate,@Status,@SeoUrl,@Meta_Title,@Meta_Description,@Meta_Keywords)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,50),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@IsMenu", SqlDbType.Bit,1),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@MenuIsShow", SqlDbType.Bit,1),
					new SqlParameter("@MenuSequence", SqlDbType.Int,4),
					new SqlParameter("@FontColor", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.Path;
            parameters[4].Value = model.Depth;
            parameters[5].Value = model.Sequence;
            parameters[6].Value = model.HasChildren;
            parameters[7].Value = model.IsMenu;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.MenuIsShow;
            parameters[10].Value = model.MenuSequence;
            parameters[11].Value = model.FontColor;
            parameters[12].Value = model.CreatedUserID;
            parameters[13].Value = model.CreatedDate;
            parameters[14].Value = model.Status;
            parameters[15].Value = model.SeoUrl;
            parameters[16].Value = model.Meta_Title;
            parameters[17].Value = model.Meta_Description;
            parameters[18].Value = model.Meta_Keywords;

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
        public bool Update(Maticsoft.Model.SNS.Categories model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Categories set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Description=@Description,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("Path=@Path,");
            strSql.Append("Depth=@Depth,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("HasChildren=@HasChildren,");
            strSql.Append("IsMenu=@IsMenu,");
            strSql.Append("Type=@Type,");
            strSql.Append("MenuIsShow=@MenuIsShow,");
            strSql.Append("MenuSequence=@MenuSequence,");
            strSql.Append("FontColor=@FontColor,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("SeoUrl=@SeoUrl,");
            strSql.Append("Meta_Title=@Meta_Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,50),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@IsMenu", SqlDbType.Bit,1),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@MenuIsShow", SqlDbType.Bit,1),
					new SqlParameter("@MenuSequence", SqlDbType.Int,4),
					new SqlParameter("@FontColor", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.Path;
            parameters[4].Value = model.Depth;
            parameters[5].Value = model.Sequence;
            parameters[6].Value = model.HasChildren;
            parameters[7].Value = model.IsMenu;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.MenuIsShow;
            parameters[10].Value = model.MenuSequence;
            parameters[11].Value = model.FontColor;
            parameters[12].Value = model.CreatedUserID;
            parameters[13].Value = model.CreatedDate;
            parameters[14].Value = model.Status;
            parameters[15].Value = model.SeoUrl;
            parameters[16].Value = model.Meta_Title;
            parameters[17].Value = model.Meta_Description;
            parameters[18].Value = model.Meta_Keywords;
            parameters[19].Value = model.CategoryId;

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
        public bool Delete(int CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Categories ");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryId;

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
        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Categories ");
            strSql.Append(" where CategoryId in (" + CategoryIdlist + ")  ");
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
        public Maticsoft.Model.SNS.Categories GetModel(int CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords from SNS_Categories ");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryId;

            Maticsoft.Model.SNS.Categories model = new Maticsoft.Model.SNS.Categories();
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
        public Maticsoft.Model.SNS.Categories DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Categories model = new Maticsoft.Model.SNS.Categories();
            if (row != null)
            {
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["ParentID"] != null && row["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["Path"] != null)
                {
                    model.Path = row["Path"].ToString();
                }
                if (row["Depth"] != null && row["Depth"].ToString() != "")
                {
                    model.Depth = int.Parse(row["Depth"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
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
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
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
                if (row["FontColor"] != null)
                {
                    model.FontColor = row["FontColor"].ToString();
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["SeoUrl"] != null)
                {
                    model.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["Meta_Title"] != null)
                {
                    model.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    model.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    model.Meta_Keywords = row["Meta_Keywords"].ToString();
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
            strSql.Append("select CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords ");
            strSql.Append(" FROM SNS_Categories ");
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
            strSql.Append(" CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords ");
            strSql.Append(" FROM SNS_Categories ");
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
            strSql.Append("select count(1) FROM SNS_Categories ");
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
                strSql.Append("order by T.CategoryId desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Categories T ");
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
            parameters[0].Value = "SNS_Categories";
            parameters[1].Value = "CategoryId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

   
		#endregion  ExtensionMethod

        #region 扩展方法
        public bool UpdatePathAndDepth(int id, int parentid)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo();
            if (parentid == 0)
            {
                //更新自己
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SNS_Categories set ");
                strSql.Append("Depth=@Depth,");
                strSql.Append("Path=@Path,");
                strSql.Append("HasChildren='false'");
                strSql.Append(" where CategoryID=@CategoryID");
                SqlParameter[] parameters = {
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
                parameters[0].Value = 1;
                parameters[1].Value = id;
                parameters[2].Value = id;
                cmd = new CommandInfo(strSql.ToString(), parameters);
                sqllist.Add(cmd);
            }
            else
            {
                //更新自己
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SNS_Categories set ");
                strSql.Append("Depth=(select SNS_Categories.depth from SNS_Categories where CategoryID=@ParentID)+1,");
                strSql.Append("Path=(select SNS_Categories.Path from SNS_Categories where CategoryID=@ParentID)+@Path,");
                strSql.Append("HasChildren='true'");
                strSql.Append(" where CategoryID=@CategoryID");
                SqlParameter[] parameters = {
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
                parameters[0].Value = "|" + id;
                parameters[1].Value = parentid;
                parameters[2].Value = id;
                cmd = new CommandInfo(strSql.ToString(), parameters);
                sqllist.Add(cmd);


            }
            //更新子类
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("UPDATE SNS_Categories set");
            strSql2.Append(" Depth=(select SNS_Categories.depth from SNS_Categories where CategoryID=@CategoryID)+1,");
            strSql2.Append(" Path=(select SNS_Categories.Path from SNS_Categories where CategoryID=@CategoryID)+@Path ");
            strSql2.Append("where ParentID=@CategoryID");
            SqlParameter[] parameters2 = {
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
            parameters2[0].Value = "|" + id;
            parameters2[1].Value = parentid;
            parameters2[2].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

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
        /// 添加分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCategory(Maticsoft.Model.SNS.Categories model)
        {
            int categoryid = Add(model);
            if (categoryid > 0)
            {
                return UpdatePathAndDepth(categoryid, model.ParentID);
            }
            return false;
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCategory(Maticsoft.Model.SNS.Categories model)
        {
            if (Update(model))
            {
                return UpdatePathAndDepth(model.CategoryId, model.ParentID);
            }
            return false;
        }

        /// <summary>
        /// 获得数据列表(是否排序)
        /// </summary>
        public DataSet GetCategoryList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SNS_Categories ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere );
            }
            strSql.Append("  ORDER BY  Sequence ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public bool DeleteCategory(int CategoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Categories ");
            strSql.Append(" where path like (select SNS_Categories.Path from SNS_Categories where CategoryId=@CategoryId)+'%'");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryID;
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

        public bool AddCategories(Model.SNS.Categories model)
        {
            int resultRows = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@HasChildren", SqlDbType.Bit),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@MenuSequence", SqlDbType.Int,4),
					new SqlParameter("@MenuIsShow", SqlDbType.Bit),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@IsMenu", SqlDbType.Bit),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@FontColor", SqlDbType.NVarChar),
                   	new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.HasChildren;
            parameters[3].Value = model.ParentID;
            parameters[4].Value = model.MenuSequence;
            parameters[5].Value =model.MenuIsShow;
            parameters[6].Value = model.CreatedUserID;
            parameters[7].Value = model.IsMenu;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.FontColor;
            parameters[11].Value = model.SeoUrl;
            parameters[12].Value = model.Meta_Title;
            parameters[13].Value = model.Meta_Description;
            parameters[14].Value = model.Meta_Keywords;


            return DbHelperSQL.RunProcedure("sp_SNS_Category_Create", parameters, out resultRows) > 0;
            
        }


        /// <summary>
        /// 对分类进行排序
        /// </summary>
        public bool SwapCategorySequence(int CategoryId, Model.SNS.EnumHelper.SwapSequenceIndex zIndex)
        {
            int AffectedRows = 0;
            SqlParameter[] parameter = { 
                                       new SqlParameter("@CategoryId",SqlDbType.Int),
                                       new SqlParameter("@ZIndex",SqlDbType.Int)
                                       };
            parameter[0].Value = CategoryId;
            parameter[1].Value = (int)zIndex;

            DbHelperSQL.RunProcedure("sp_SNS_Category_SwapSequence", parameter, out AffectedRows);
            return AffectedRows > 0;
        }



	    #endregion
	}
}

