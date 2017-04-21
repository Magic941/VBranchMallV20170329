/**
* Groups.cs
*
* 功 能： N/A
* 类 名： Groups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:42   N/A    初版
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
    /// 数据访问类:Groups
    /// </summary>
    public partial class Groups : IGroups
    {
        public Groups()
        { }
        #region  BasicMethod



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GroupName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Groups");
            strSql.Append(" where GroupName=@GroupName");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupName", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = GroupName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GroupName, int groupId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SNS_Groups");
            strSql.Append(" where GroupName=@GroupName and groupId <>@GroupId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
                    new SqlParameter("@GroupId", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupName;
            parameters[1].Value = groupId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SNS_Groups(");
            strSql.Append("GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags)");
            strSql.Append(" values (");
            strSql.Append("@GroupName,@GroupDescription,@GroupUserCount,@CreatedUserId,@CreatedNickName,@CreatedDate,@GroupLogo,@GroupLogoThumb,@GroupBackground,@ApplyGroupReason,@IsRecommand,@TopicCount,@TopicReplyCount,@Status,@Sequence,@Privacy,@Tags)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
                    new SqlParameter("@GroupDescription", SqlDbType.NVarChar),
                    new SqlParameter("@GroupUserCount", SqlDbType.Int,4),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@GroupLogo", SqlDbType.NVarChar,200),
                    new SqlParameter("@GroupLogoThumb", SqlDbType.NVarChar,200),
                    new SqlParameter("@GroupBackground", SqlDbType.NVarChar,200),
                    new SqlParameter("@ApplyGroupReason", SqlDbType.NVarChar),
                    new SqlParameter("@IsRecommand", SqlDbType.Int,4),
                    new SqlParameter("@TopicCount", SqlDbType.Int,4),
                    new SqlParameter("@TopicReplyCount", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Sequence", SqlDbType.Int,4),
                    new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
                    new SqlParameter("@Tags", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.GroupName;
            parameters[1].Value = model.GroupDescription;
            parameters[2].Value = model.GroupUserCount;
            parameters[3].Value = model.CreatedUserId;
            parameters[4].Value = model.CreatedNickName;
            parameters[5].Value = model.CreatedDate;
            parameters[6].Value = model.GroupLogo;
            parameters[7].Value = model.GroupLogoThumb;
            parameters[8].Value = model.GroupBackground;
            parameters[9].Value = model.ApplyGroupReason;
            parameters[10].Value = model.IsRecommand;
            parameters[11].Value = model.TopicCount;
            parameters[12].Value = model.TopicReplyCount;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.Sequence;
            parameters[15].Value = model.Privacy;
            parameters[16].Value = model.Tags;

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
        public bool Update(Maticsoft.Model.SNS.Groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Groups set ");
            strSql.Append("GroupName=@GroupName,");
            strSql.Append("GroupDescription=@GroupDescription,");
            strSql.Append("GroupUserCount=@GroupUserCount,");
            strSql.Append("CreatedUserId=@CreatedUserId,");
            strSql.Append("CreatedNickName=@CreatedNickName,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("GroupLogo=@GroupLogo,");
            strSql.Append("GroupLogoThumb=@GroupLogoThumb,");
            strSql.Append("GroupBackground=@GroupBackground,");
            strSql.Append("ApplyGroupReason=@ApplyGroupReason,");
            strSql.Append("IsRecommand=@IsRecommand,");
            strSql.Append("TopicCount=@TopicCount,");
            strSql.Append("TopicReplyCount=@TopicReplyCount,");
            strSql.Append("Status=@Status,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("Privacy=@Privacy,");
            strSql.Append("Tags=@Tags");
            strSql.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
                    new SqlParameter("@GroupDescription", SqlDbType.NVarChar),
                    new SqlParameter("@GroupUserCount", SqlDbType.Int,4),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@CreatedNickName", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@GroupLogo", SqlDbType.NVarChar,200),
                    new SqlParameter("@GroupLogoThumb", SqlDbType.NVarChar,200),
                    new SqlParameter("@GroupBackground", SqlDbType.NVarChar,200),
                    new SqlParameter("@ApplyGroupReason", SqlDbType.NVarChar),
                    new SqlParameter("@IsRecommand", SqlDbType.Int,4),
                    new SqlParameter("@TopicCount", SqlDbType.Int,4),
                    new SqlParameter("@TopicReplyCount", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Sequence", SqlDbType.Int,4),
                    new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
                    new SqlParameter("@Tags", SqlDbType.NVarChar,100),
                    new SqlParameter("@GroupID", SqlDbType.Int,4)};
            parameters[0].Value = model.GroupName;
            parameters[1].Value = model.GroupDescription;
            parameters[2].Value = model.GroupUserCount;
            parameters[3].Value = model.CreatedUserId;
            parameters[4].Value = model.CreatedNickName;
            parameters[5].Value = model.CreatedDate;
            parameters[6].Value = model.GroupLogo;
            parameters[7].Value = model.GroupLogoThumb;
            parameters[8].Value = model.GroupBackground;
            parameters[9].Value = model.ApplyGroupReason;
            parameters[10].Value = model.IsRecommand;
            parameters[11].Value = model.TopicCount;
            parameters[12].Value = model.TopicReplyCount;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.Sequence;
            parameters[15].Value = model.Privacy;
            parameters[16].Value = model.Tags;
            parameters[17].Value = model.GroupID;

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
        public bool Delete(int GroupID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Groups ");
            strSql.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupID", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupID;

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
        public bool DeleteList(string GroupIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Groups ");
            strSql.Append(" where GroupID in (" + GroupIDlist + ")  ");
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
        public Maticsoft.Model.SNS.Groups GetModel(int GroupID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GroupID,GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags from SNS_Groups ");
            strSql.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupID", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupID;

            Maticsoft.Model.SNS.Groups model = new Maticsoft.Model.SNS.Groups();
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
        public Maticsoft.Model.SNS.Groups DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Groups model = new Maticsoft.Model.SNS.Groups();
            if (row != null)
            {
                if (row["GroupID"] != null && row["GroupID"].ToString() != "")
                {
                    model.GroupID = int.Parse(row["GroupID"].ToString());
                }
                if (row["GroupName"] != null)
                {
                    model.GroupName = row["GroupName"].ToString();
                }
                if (row["GroupDescription"] != null)
                {
                    model.GroupDescription = row["GroupDescription"].ToString();
                }
                if (row["GroupUserCount"] != null && row["GroupUserCount"].ToString() != "")
                {
                    model.GroupUserCount = int.Parse(row["GroupUserCount"].ToString());
                }
                if (row["CreatedUserId"] != null && row["CreatedUserId"].ToString() != "")
                {
                    model.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    model.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["GroupLogo"] != null)
                {
                    model.GroupLogo = row["GroupLogo"].ToString();
                }
                if (row["GroupLogoThumb"] != null)
                {
                    model.GroupLogoThumb = row["GroupLogoThumb"].ToString();
                }
                if (row["GroupBackground"] != null)
                {
                    model.GroupBackground = row["GroupBackground"].ToString();
                }
                if (row["ApplyGroupReason"] != null)
                {
                    model.ApplyGroupReason = row["ApplyGroupReason"].ToString();
                }
                if (row["IsRecommand"] != null && row["IsRecommand"].ToString() != "")
                {
                    model.IsRecommand = int.Parse(row["IsRecommand"].ToString());
                }
                if (row["TopicCount"] != null && row["TopicCount"].ToString() != "")
                {
                    model.TopicCount = int.Parse(row["TopicCount"].ToString());
                }
                if (row["TopicReplyCount"] != null && row["TopicReplyCount"].ToString() != "")
                {
                    model.TopicReplyCount = int.Parse(row["TopicReplyCount"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["Privacy"] != null && row["Privacy"].ToString() != "")
                {
                    model.Privacy = int.Parse(row["Privacy"].ToString());
                }
                if (row["Tags"] != null)
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
            strSql.Append("select GroupID,GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags ");
            strSql.Append(" FROM SNS_Groups ");
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
            strSql.Append(" GroupID,GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags ");
            strSql.Append(" FROM SNS_Groups ");
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
            strSql.Append("select count(1) FROM SNS_Groups ");
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
                strSql.Append("order by T.GroupID desc");
            }
            strSql.Append(")AS Row, T.*  from SNS_Groups T ");
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
            parameters[0].Value = "SNS_Groups";
            parameters[1].Value = "GroupID";
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
        /// 更新小组的状态
        /// </summary>
        /// <param name="IdsStr">id的集合</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool UpdateStatusList(string IdsStr, Maticsoft.Model.SNS.EnumHelper.GroupStatus status)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Groups set Status="+(int)status+" ");
            strSql.Append(" where GroupID in (" + IdsStr + ")  ");
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
        /// 删除小组和小组下面的话题以及怀特的回复
        /// </summary>
        public  bool DeleteListEx(string GroupIDlist)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            StringBuilder strSql0 = new StringBuilder();
            strSql0.Append("delete from SNS_GroupTopicFav ");
            strSql0.Append(" where TopicID in (select TopicID from SNS_GroupTopics where GroupID in (" + GroupIDlist + "))  ");
            CommandInfo cmd0 = new CommandInfo(strSql0.ToString(), null);
            sqllist.Add(cmd0);

 
            StringBuilder strSql2 = new StringBuilder();
           strSql2.Append("delete from SNS_GroupTopicReply ");
            strSql2.Append(" where GroupID in (" + GroupIDlist + ")  ");
            CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), null);
            sqllist.Add(cmd2);
            #region 删除相应的话题和话题的回复
            StringBuilder strSql1 = new StringBuilder();
           strSql1.Append("delete from SNS_GroupTopics ");
            strSql1.Append(" where GroupID in (" + GroupIDlist + ")  ");
            CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), null);
            sqllist.Add(cmd1);


            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from SNS_GroupUsers ");
            strSql4.Append(" where GroupID in (" + GroupIDlist + ")  ");
            CommandInfo cmd4 = new CommandInfo(strSql4.ToString(), null);
            sqllist.Add(cmd4);



         
            #endregion
            #region 删除小组
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_Groups ");
            strSql.Append(" where GroupID in (" + GroupIDlist + ")  ");
          
            CommandInfo cmd = new CommandInfo(strSql.ToString(), null);
            sqllist.Add(cmd);
            #endregion
            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;

        
        }

        public bool UpdateRecommand(int GroupId, int Recommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SNS_Groups set ");
            strSql.Append("IsRecommand=@IsRecommand");
            strSql.Append(" where GroupID=@GroupID");
            SqlParameter[] parameters = {
    
                    new SqlParameter("@IsRecommand", SqlDbType.Int,4),
             
                    new SqlParameter("@GroupID", SqlDbType.Int,4)};

            parameters[0].Value = Recommand;
            parameters[1].Value = GroupId;
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

