/**
* MsgItem.cs
*
* 功 能： N/A
* 类 名： MsgItem
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:09   N/A    初版
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
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:MsgItem
	/// </summary>
	public partial class MsgItem:IMsgItem
	{
		public MsgItem()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ItemId", "WeChat_MsgItem");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ItemId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_MsgItem");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.MsgItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_MsgItem(");
            strSql.Append("OpenId,Title,PicUrl,Url,Description)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@Title,@PicUrl,@Url,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,50),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.PicUrl;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.Description;

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
        public bool Update(Maticsoft.WeChat.Model.Core.MsgItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_MsgItem set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("Title=@Title,");
            strSql.Append("PicUrl=@PicUrl,");
            strSql.Append("Url=@Url,");
            strSql.Append("Description=@Description");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,50),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@PicUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@ItemId", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.PicUrl;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.ItemId;

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
        public bool Delete(int ItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_MsgItem ");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemId;

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
        public bool DeleteList(string ItemIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_MsgItem ");
            strSql.Append(" where ItemId in (" + ItemIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.MsgItem GetModel(int ItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemId,OpenId,Title,PicUrl,Url,Description from WeChat_MsgItem ");
            strSql.Append(" where ItemId=@ItemId");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemId", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemId;

            Maticsoft.WeChat.Model.Core.MsgItem model = new Maticsoft.WeChat.Model.Core.MsgItem();
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
        public Maticsoft.WeChat.Model.Core.MsgItem DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.MsgItem model = new Maticsoft.WeChat.Model.Core.MsgItem();
            if (row != null)
            {
                if (row["ItemId"] != null && row["ItemId"].ToString() != "")
                {
                    model.ItemId = int.Parse(row["ItemId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
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
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemId,OpenId,Title,PicUrl,Url,Description ");
            strSql.Append(" FROM WeChat_MsgItem ");
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
            strSql.Append(" ItemId,OpenId,Title,PicUrl,Url,Description ");
            strSql.Append(" FROM WeChat_MsgItem ");
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
            strSql.Append("select count(1) FROM WeChat_MsgItem ");
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
                strSql.Append("order by T.ItemId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_MsgItem T ");
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
            parameters[0].Value = "WeChat_MsgItem";
            parameters[1].Value = "ItemId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod
        public DataSet GetItemList(int msgId, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select *  from WeChat_MsgItem M where EXISTS   ");
            strSql.Append(" (select * from WeChat_PostMsgItem P where P.PostMsgId=@PostMsgId and P.Type=@Type  and P.ItemId=M.ItemId) ");
            SqlParameter[] parameters = {
					new SqlParameter("@PostMsgId", SqlDbType.NVarChar,4),
					new SqlParameter("@Type", SqlDbType.NVarChar,4)
        };
            parameters[0].Value = msgId;
            parameters[1].Value = type;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
		#endregion  ExtensionMethod
	}
}

