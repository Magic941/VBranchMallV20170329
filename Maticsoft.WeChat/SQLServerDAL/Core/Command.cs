/**
* Command.cs
*
* 功 能： N/A
* 类 名： Command
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:22   N/A    初版
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
	/// 数据访问类:Command
	/// </summary>
	public partial class Command:ICommand
	{
		public Command()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("CommandId", "WeChat_Command");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommandId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_Command");
            strSql.Append(" where CommandId=@CommandId");
            SqlParameter[] parameters = {
					new SqlParameter("@CommandId", SqlDbType.Int,4)
			};
            parameters[0].Value = CommandId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.Command model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_Command(");
            strSql.Append("OpenId,ActionId,TargetId,Name,Sequence,Status,ParseType,ParseLength,ParseChar,Remark)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@ActionId,@TargetId,@Name,@Sequence,@Status,@ParseType,@ParseLength,@ParseChar,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@ActionId", SqlDbType.Int,4),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ParseType", SqlDbType.Int,4),
					new SqlParameter("@ParseLength", SqlDbType.Int,4),
					new SqlParameter("@ParseChar", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.ActionId;
            parameters[2].Value = model.TargetId;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.Sequence;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.ParseType;
            parameters[7].Value = model.ParseLength;
            parameters[8].Value = model.ParseChar;
            parameters[9].Value = model.Remark;

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
        public bool Update(Maticsoft.WeChat.Model.Core.Command model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_Command set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("ActionId=@ActionId,");
            strSql.Append("TargetId=@TargetId,");
            strSql.Append("Name=@Name,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("Status=@Status,");
            strSql.Append("ParseType=@ParseType,");
            strSql.Append("ParseLength=@ParseLength,");
            strSql.Append("ParseChar=@ParseChar,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where CommandId=@CommandId");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@ActionId", SqlDbType.Int,4),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ParseType", SqlDbType.Int,4),
					new SqlParameter("@ParseLength", SqlDbType.Int,4),
					new SqlParameter("@ParseChar", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@CommandId", SqlDbType.Int,4)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.ActionId;
            parameters[2].Value = model.TargetId;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.Sequence;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.ParseType;
            parameters[7].Value = model.ParseLength;
            parameters[8].Value = model.ParseChar;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.CommandId;

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
        public bool Delete(int CommandId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_Command ");
            strSql.Append(" where CommandId=@CommandId");
            SqlParameter[] parameters = {
					new SqlParameter("@CommandId", SqlDbType.Int,4)
			};
            parameters[0].Value = CommandId;

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
        public bool DeleteList(string CommandIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_Command ");
            strSql.Append(" where CommandId in (" + CommandIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.Command GetModel(int CommandId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CommandId,OpenId,ActionId,TargetId,Name,Sequence,Status,ParseType,ParseLength,ParseChar,Remark from WeChat_Command ");
            strSql.Append(" where CommandId=@CommandId");
            SqlParameter[] parameters = {
					new SqlParameter("@CommandId", SqlDbType.Int,4)
			};
            parameters[0].Value = CommandId;

            Maticsoft.WeChat.Model.Core.Command model = new Maticsoft.WeChat.Model.Core.Command();
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
        public Maticsoft.WeChat.Model.Core.Command DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.Command model = new Maticsoft.WeChat.Model.Core.Command();
            if (row != null)
            {
                if (row["CommandId"] != null && row["CommandId"].ToString() != "")
                {
                    model.CommandId = int.Parse(row["CommandId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["ActionId"] != null && row["ActionId"].ToString() != "")
                {
                    model.ActionId = int.Parse(row["ActionId"].ToString());
                }
                if (row["TargetId"] != null && row["TargetId"].ToString() != "")
                {
                    model.TargetId = int.Parse(row["TargetId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["ParseType"] != null && row["ParseType"].ToString() != "")
                {
                    model.ParseType = int.Parse(row["ParseType"].ToString());
                }
                if (row["ParseLength"] != null && row["ParseLength"].ToString() != "")
                {
                    model.ParseLength = int.Parse(row["ParseLength"].ToString());
                }
                if (row["ParseChar"] != null)
                {
                    model.ParseChar = row["ParseChar"].ToString();
                }
                if (row["Remark"] != null)
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
            strSql.Append("select CommandId,OpenId,ActionId,TargetId,Name,Sequence,Status,ParseType,ParseLength,ParseChar,Remark ");
            strSql.Append(" FROM WeChat_Command ");
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
            strSql.Append(" CommandId,OpenId,ActionId,TargetId,Name,Sequence,Status,ParseType,ParseLength,ParseChar,Remark ");
            strSql.Append(" FROM WeChat_Command ");
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
            strSql.Append("select count(1) FROM WeChat_Command ");
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
                strSql.Append("order by T.CommandId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_Command T ");
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
            parameters[0].Value = "WeChat_Command";
            parameters[1].Value = "CommandId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod
        public int  GetSequence(string openId)
        {
               StringBuilder strSql = new StringBuilder();
               strSql.Append(" SELECT MAX(Sequence) FROM WeChat_Command where OpenId=@OpenId");
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
		#endregion  ExtensionMethod
	}
}

