/**
* HotWords.cs
*
* 功 能： N/A
* 类 名： HotWords
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:53   N/A    初版
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
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 搜索推荐关键字
	/// </summary>
	public partial class HotWords:IHotWords
	{
		public HotWords()
		{}
		#region  BasicMethod

	

		/// <summary>
		/// 是否存在
		/// </summary>
        public bool Exists(string KeyWord)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_HotWords");
            strSql.Append(" where KeyWord=@KeyWord");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyWord", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = KeyWord;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.HotWords model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_HotWords(");
			strSql.Append("KeyWord,CreatedDate,IsRecommend,Sequence,Status)");
			strSql.Append(" values (");
			strSql.Append("@KeyWord,@CreatedDate,@IsRecommend,@Sequence,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyWord", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.KeyWord;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.IsRecommend;
			parameters[3].Value = model.Sequence;
			parameters[4].Value = model.Status;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.SNS.HotWords model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_HotWordsset ");
			strSql.Append("KeyWord=@KeyWord,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("IsRecommend=@IsRecommend,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("Status=@Status");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@KeyWord", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.KeyWord;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.IsRecommend;
			parameters[3].Value = model.Sequence;
			parameters[4].Value = model.Status;
			parameters[5].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_HotWords");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_HotWords");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.SNS.HotWords GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,KeyWord,CreatedDate,IsRecommend,Sequence,Status from SNS_HotWords");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.SNS.HotWords model=new Maticsoft.Model.SNS.HotWords();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public Maticsoft.Model.SNS.HotWords DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.HotWords model=new Maticsoft.Model.SNS.HotWords();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["KeyWord"]!=null)
				{
					model.KeyWord=row["KeyWord"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["IsRecommend"]!=null && row["IsRecommend"].ToString()!="")
				{
					if((row["IsRecommend"].ToString()=="1")||(row["IsRecommend"].ToString().ToLower()=="true"))
					{
						model.IsRecommend=true;
					}
					else
					{
						model.IsRecommend=false;
					}
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,KeyWord,CreatedDate,IsRecommend,Sequence,Status ");
			strSql.Append(" FROM SNS_HotWords");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,KeyWord,CreatedDate,IsRecommend,Sequence,Status ");
			strSql.Append(" FROM SNS_HotWords");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            else
            {

            } strSql.Append(" order by ID DESC");
			
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SNS_HotWords");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_HotWordsT ");
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
			parameters[0].Value = "SNS_HotWords";
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
        #region 得到最大Sequence
        /// <summary>
        /// 得到最大Sequence
        /// </summary>
        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "SNS_HotWords");
        }
        #endregion
		
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SNS_HotWords");
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

