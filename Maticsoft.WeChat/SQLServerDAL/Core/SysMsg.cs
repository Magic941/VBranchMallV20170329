/**
* SysMsg.cs
*
* 功 能： N/A
* 类 名： SysMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/2 14:55:58   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:SysMsg
	/// </summary>
	public partial class SysMsg:ISysMsg
	{
		public SysMsg()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SysMsgId", "WeChat_SysMsg");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SysMsgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_SysMsg");
            strSql.Append(" where SysMsgId=@SysMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@SysMsgId", SqlDbType.Int,4)
			};
            parameters[0].Value = SysMsgId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.SysMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_SysMsg(");
            strSql.Append("OpenId,MsgType,SysType,CreateTime,Title,PicUrl,Url,Description,MusicUrl,HQMusicUrl)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@MsgType,@SysType,@CreateTime,@Title,@PicUrl,@Url,@Description,@MusicUrl,@HQMusicUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@SysType", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.SysType;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.PicUrl;
            parameters[6].Value = model.Url;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.MusicUrl;
            parameters[9].Value = model.HQMusicUrl;

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
        public bool Update(Maticsoft.WeChat.Model.Core.SysMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_SysMsg set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("MsgType=@MsgType,");
            strSql.Append("SysType=@SysType,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Title=@Title,");
            strSql.Append("PicUrl=@PicUrl,");
            strSql.Append("Url=@Url,");
            strSql.Append("Description=@Description,");
            strSql.Append("MusicUrl=@MusicUrl,");
            strSql.Append("HQMusicUrl=@HQMusicUrl");
            strSql.Append(" where SysMsgId=@SysMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@SysType", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@MusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@HQMusicUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SysMsgId", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.SysType;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.PicUrl;
            parameters[6].Value = model.Url;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.MusicUrl;
            parameters[9].Value = model.HQMusicUrl;
            parameters[10].Value = model.SysMsgId;

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
        public bool Delete(int SysMsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_SysMsg ");
            strSql.Append(" where SysMsgId=@SysMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@SysMsgId", SqlDbType.Int,4)
			};
            parameters[0].Value = SysMsgId;

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
        public bool DeleteList(string SysMsgIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_SysMsg ");
            strSql.Append(" where SysMsgId in (" + SysMsgIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.SysMsg GetModel(int SysMsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SysMsgId,OpenId,MsgType,SysType,CreateTime,Title,PicUrl,Url,Description,MusicUrl,HQMusicUrl from WeChat_SysMsg ");
            strSql.Append(" where SysMsgId=@SysMsgId");
            SqlParameter[] parameters = {
					new SqlParameter("@SysMsgId", SqlDbType.Int,4)
			};
            parameters[0].Value = SysMsgId;

            Maticsoft.WeChat.Model.Core.SysMsg model = new Maticsoft.WeChat.Model.Core.SysMsg();
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
        public Maticsoft.WeChat.Model.Core.SysMsg DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.SysMsg model = new Maticsoft.WeChat.Model.Core.SysMsg();
            if (row != null)
            {
                if (row["SysMsgId"] != null && row["SysMsgId"].ToString() != "")
                {
                    model.SysMsgId = int.Parse(row["SysMsgId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["MsgType"] != null)
                {
                    model.MsgType = row["MsgType"].ToString();
                }
                if (row["SysType"] != null && row["SysType"].ToString() != "")
                {
                    model.SysType = int.Parse(row["SysType"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["PicUrl"] != null)
                {
                    model.PicUrl = row["PicUrl"].ToString();
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["MusicUrl"] != null)
                {
                    model.MusicUrl = row["MusicUrl"].ToString();
                }
                if (row["HQMusicUrl"] != null)
                {
                    model.HQMusicUrl = row["HQMusicUrl"].ToString();
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
            strSql.Append("select SysMsgId,OpenId,MsgType,SysType,CreateTime,Title,PicUrl,Url,Description,MusicUrl,HQMusicUrl ");
            strSql.Append(" FROM WeChat_SysMsg ");
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
            strSql.Append(" SysMsgId,OpenId,MsgType,SysType,CreateTime,Title,PicUrl,Url,Description,MusicUrl,HQMusicUrl ");
            strSql.Append(" FROM WeChat_SysMsg ");
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
            strSql.Append("select count(1) FROM WeChat_SysMsg ");
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
                strSql.Append("order by T.SysMsgId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_SysMsg T ");
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
            parameters[0].Value = "WeChat_SysMsg";
            parameters[1].Value = "SysMsgId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

        public DataSet GetSysMsgs(int type, string openId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   * from WeChat_SysMsg ");
            strSql.Append(" where SysType=@SysType  and OpenId=@OpenId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SysType", SqlDbType.Int,4),
                    new SqlParameter("@OpenId", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = type;
            parameters[1].Value = openId;

            Maticsoft.WeChat.Model.Core.SysMsg model = new Maticsoft.WeChat.Model.Core.SysMsg();
            return DbHelperSQL.Query(strSql.ToString(), parameters);
          
	    }

        public bool AddEx(Maticsoft.WeChat.Model.Core.SysMsg msg)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int sysId = Common.Globals.SafeInt(DbHelperSQL.GetSingle4Trans(AddMsgInfo(msg), transaction), 0);
                        if (msg.MsgType == "news" && msg.MsgItems != null && msg.MsgItems.Count > 0)
                        {
                            foreach (var item in msg.MsgItems)
                            {
                             int itemId=Common.Globals.SafeInt( DbHelperSQL.GetSingle4Trans(AddMsgItem(item), transaction),0);
                             DbHelperSQL.GetSingle4Trans(AddSysItem(sysId, itemId, 2), transaction);
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }


       
        private CommandInfo AddMsgInfo(Maticsoft.WeChat.Model.Core.SysMsg model)
        {
                       StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_SysMsg(");
            strSql.Append("OpenId,MsgType,CreateTime,Title,Description,SysType)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@MsgType,@CreateTime,@Title,@Description,@SysType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@SysType", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.SysType;
                    return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

         private CommandInfo AddMsgItem(Maticsoft.WeChat.Model.Core.MsgItem model)
        {
                     StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_MsgItem(");
            strSql.Append("Title,PicUrl,Url,Description)");
            strSql.Append(" values (");
            strSql.Append("@Title,@PicUrl,@Url,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.PicUrl;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.Description;
         
                    return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

         private CommandInfo AddSysItem(int systId,int itemId,int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_PostMsgItem(");
            strSql.Append("PostMsgId,ItemId,Type)");
            strSql.Append(" values (");
            strSql.Append("@PostMsgId,@ItemId,@Type)");
            SqlParameter[] parameters = {
					new SqlParameter("@PostMsgId", SqlDbType.Int,4),
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = systId;
            parameters[1].Value = itemId;
            parameters[2].Value = type;
                    return new CommandInfo(strSql.ToString(),
                                    parameters, EffentNextType.ExcuteEffectRows);
        }

            //级联删除
         public bool DeleteEx(int msgId,int type)
         {
             List<CommandInfo> sqllist = new List<CommandInfo>();
             StringBuilder strSql = new StringBuilder();
             if (type == 0)
             {
                 strSql.Append("Delete WeChat_PostMsg  Where PostMsgId =@PostMsgId");
                 SqlParameter[] parameters = {
					new SqlParameter("@PostMsgId", SqlDbType.Int,4)
                                         };
                 parameters[0].Value = msgId;
                 CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
                 sqllist.Add(cmd);
             }
             if (type == 1)
             {
                 strSql.Append("Delete WeChat_CustomerMsg  Where MsgId =@MsgId");
                 SqlParameter[] parameters = {
					new SqlParameter("@MsgId", SqlDbType.Int,4)};
                 parameters[0].Value = msgId;
                 CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
                 sqllist.Add(cmd);
             }
             if (type == 2)
             {
                 strSql.Append("Delete WeChat_SysMsg  Where SysMsgId =@SysMsgId");
                 SqlParameter[] parameters = {
					new SqlParameter("@SysMsgId", SqlDbType.Int,4)};
                 parameters[0].Value = msgId;
                 CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
                 sqllist.Add(cmd);
             }

             //删除项表
             StringBuilder strSql1 = new StringBuilder();
             strSql1.Append("delete  WeChat_MsgItem  where EXISTS");
             strSql1.Append(" (select * from WeChat_PostMsgItem P where P.PostMsgId=@SysMsgId and P.Type=@Type  and P.ItemId=WeChat_MsgItem.ItemId) ");
             SqlParameter[] parameters1 = {
					new SqlParameter("@SysMsgId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
                                         };
             parameters1[0].Value = msgId;
             parameters1[0].Value = type;
             CommandInfo cmd1 = new CommandInfo(strSql1.ToString(), parameters1);
             sqllist.Add(cmd1);

             //删除中间表
             StringBuilder strSql2 = new StringBuilder();
             strSql2.Append("delete WeChat_PostMsgItem where PostMsgId=@SysMsgId and Type=@Type");
             SqlParameter[] parameters2 = {
					new SqlParameter("@SysMsgId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
                                         };
             parameters2[0].Value = msgId;
             parameters2[0].Value = type;
             CommandInfo cmd2 = new CommandInfo(strSql2.ToString(), parameters2);
             sqllist.Add(cmd2);
             return DbHelperSQL.ExecuteSqlTran(sqllist) > 0 ? true : false;
         }


         public bool DeleteEx(string msgType, int type,string openId)
         {
             StringBuilder strSql = new StringBuilder();
             strSql.Append("delete from WeChat_SysMsg ");
             if (msgType == "text")
             {
                 strSql.AppendFormat(" where openId='{0}' and SysType={1} ", openId, type);
             }
             if (msgType == "news")
             {
                 strSql.AppendFormat(" where openId='{0}' and SysType={1} and MsgType<>'news'", openId, type);
             }
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
        
	    #endregion  ExtensionMethod
	}
}

