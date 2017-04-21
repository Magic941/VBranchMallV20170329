/**
* Comment.cs
*
* 功 能： N/A
* 类 名： Comment
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/30 18:33:35   N/A    初版
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
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.CMS;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.CMS
{
    /// <summary>
    /// 数据访问类:Comment
    /// </summary>
    public partial class Comment : IComment
    {
        public Comment()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "CMS_Comment");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_Comment");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_Comment(");
            strSql.Append("ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName)");
            strSql.Append(" values (");
            strSql.Append("@ContentId,@Description,@CreatedDate,@CreatedUserID,@ReplyCount,@ParentID,@TypeID,@State,@IsRead,@CreatedNickName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.Bit,1),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.ContentId;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.ReplyCount;
            parameters[5].Value = model.ParentID;
            parameters[6].Value = model.TypeID;
            parameters[7].Value = model.State;
            parameters[8].Value = model.IsRead;
            parameters[9].Value = model.CreatedNickName;

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
        public bool Update(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Comment set ");
            strSql.Append("ContentId=@ContentId,");
            strSql.Append("Description=@Description,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("ReplyCount=@ReplyCount,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("State=@State,");
            strSql.Append("IsRead=@IsRead,");
            strSql.Append("CreatedNickName=@CreatedNickName");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.Bit,1),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ContentId;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.ReplyCount;
            parameters[5].Value = model.ParentID;
            parameters[6].Value = model.TypeID;
            parameters[7].Value = model.State;
            parameters[8].Value = model.IsRead;
            parameters[9].Value = model.CreatedNickName;
            parameters[10].Value = model.ID;

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
            strSql.Append("delete from CMS_Comment ");
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
            strSql.Append("delete from CMS_Comment ");
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
        public Maticsoft.Model.CMS.Comment GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName from CMS_Comment ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.CMS.Comment model = new Maticsoft.Model.CMS.Comment();
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
        public Maticsoft.Model.CMS.Comment DataRowToModel(DataRow row)
        {
            Maticsoft.Model.CMS.Comment model = new Maticsoft.Model.CMS.Comment();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ContentId"] != null && row["ContentId"].ToString() != "")
                {
                    model.ContentId = int.Parse(row["ContentId"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["ReplyCount"] != null && row["ReplyCount"].ToString() != "")
                {
                    model.ReplyCount = int.Parse(row["ReplyCount"].ToString());
                }
                if (row["ParentID"] != null && row["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["TypeID"] != null && row["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    if ((row["State"].ToString() == "1") || (row["State"].ToString().ToLower() == "true"))
                    {
                        model.State = true;
                    }
                    else
                    {
                        model.State = false;
                    }
                }
                if (row["IsRead"] != null && row["IsRead"].ToString() != "")
                {
                    if ((row["IsRead"].ToString() == "1") || (row["IsRead"].ToString().ToLower() == "true"))
                    {
                        model.IsRead = true;
                    }
                    else
                    {
                        model.IsRead = false;
                    }
                }
                if (row["CreatedNickName"] != null)
                {
                    model.CreatedNickName = row["CreatedNickName"].ToString();
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
            strSql.Append("select ID,ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName ");
            strSql.Append(" FROM CMS_Comment ");
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
            strSql.Append(" ID,ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName ");
            strSql.Append(" FROM CMS_Comment ");
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
            strSql.Append("select count(1) FROM CMS_Comment ");
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
            strSql.Append(")AS Row, T.*  from CMS_Comment T ");
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
            parameters[0].Value = "CMS_Comment";
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
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" *,CMS_Content.Title ");
            strSql.Append(" FROM CMS_Comment LEFT JOIN CMS_Content ON CMS_Comment.ContentId = CMS_Content.ContentID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int AddEx(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_Comment(");
            strSql.Append("ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName)");
            strSql.Append(" values (");
            strSql.Append("@ContentId,@Description,@CreatedDate,@CreatedUserID,@ReplyCount,@ParentID,@TypeID,@State,@IsRead,@CreatedNickName)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.Bit,1),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,200),
                     new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.ContentId;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.ReplyCount;
            parameters[5].Value = model.ParentID;
            parameters[6].Value = model.TypeID;
            parameters[7].Value = model.State;
            parameters[8].Value = model.IsRead;
            parameters[9].Value = model.CreatedNickName;
            parameters[10].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("Update  CMS_Content ");
            strSql4.Append(" Set TotalComment=TotalComment+1 ");
            strSql4.Append(" where ContentID=@ContentID ");
            SqlParameter[] parameters4 = {
                    	new SqlParameter("@ContentID", SqlDbType.Int,4),
		          };
            parameters4[0].Value = model.ContentId;
            CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd4);
            DbHelperSQL.ExecuteSqlTran(sqllist);
            return (int)parameters[10].Value;
        }

        public int  AddTran(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_Comment(");
            strSql.Append("ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName)");
            strSql.Append(" values (");
            strSql.Append("@ContentId,@Description,@CreatedDate,@CreatedUserID,@ReplyCount,@ParentID,@TypeID,@State,@IsRead,@CreatedNickName)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.Bit,1),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,200),
                     new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.ContentId;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.ReplyCount;
            parameters[5].Value = model.ParentID;
            parameters[6].Value = model.TypeID;
            parameters[7].Value = model.State;
            parameters[8].Value = model.IsRead;
            parameters[9].Value = model.CreatedNickName;
            parameters[10].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            switch (model.TypeID)
            {
                case 2:
                    StringBuilder strSql2 = new StringBuilder();
                    strSql2.Append("Update  CMS_Video ");
                    strSql2.Append(" Set TotalComment=TotalComment+1 ");
                    strSql2.Append(" where VideoID=@VideoID ");
                    SqlParameter[] parameters2 = {
                    	new SqlParameter("@VideoID", SqlDbType.Int,4),
		          };
                    parameters2[0].Value = model.ContentId;
                    CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd2);
                    break;
                case 3:
                    StringBuilder strSql4 = new StringBuilder();
                    strSql4.Append("Update  CMS_Content ");
                    strSql4.Append(" Set TotalComment=TotalComment+1 ");
                    strSql4.Append(" where ContentID=@ContentID ");
                    SqlParameter[] parameters4 = {
                    	new SqlParameter("@ContentID", SqlDbType.Int,4),
		          };
                    parameters4[0].Value = model.ContentId;
                    CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), parameters4);
                    sqllist.Add(cmd4);
                    break;
                default:
                    break;
            }
            DbHelperSQL.ExecuteSqlTran(sqllist);
            return (int)parameters[10].Value;
       
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="IDlist">id列表</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Comment set ");
            strSql.Append("State=@State");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            SqlParameter[] parameters = {
					new SqlParameter("@State", SqlDbType.Bit,1)};
            parameters[0].Value = state;
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
        #endregion  ExtensionMethod
    }
}

