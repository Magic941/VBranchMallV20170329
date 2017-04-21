/**  版本信息模板在安装目录下，可自行修改。
* SceneDetail.cs
*
* 功 能： N/A
* 类 名： SceneDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/20 12:32:25   N/A    初版
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
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Core
{
	/// <summary>
	/// 数据访问类:SceneDetail
	/// </summary>
	public partial class SceneDetail:ISceneDetail
	{
		public SceneDetail()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DetailId", "WeChat_SceneDetail");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WeChat_SceneDetail");
            strSql.Append(" where DetailId=@DetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.Int,4)
			};
            parameters[0].Value = DetailId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.SceneDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeChat_SceneDetail(");
            strSql.Append("SceneId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@SceneId,@OpenId,@UserName,@NickName,@Sex,@City,@Province,@Country,@Language,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SceneId", SqlDbType.Int,4),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@NickName", SqlDbType.NVarChar,200),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@City", SqlDbType.NVarChar,200),
					new SqlParameter("@Province", SqlDbType.NVarChar,200),
					new SqlParameter("@Country", SqlDbType.NVarChar,200),
					new SqlParameter("@Language", SqlDbType.NVarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.SceneId;
            parameters[1].Value = model.OpenId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.NickName;
            parameters[4].Value = model.Sex;
            parameters[5].Value = model.City;
            parameters[6].Value = model.Province;
            parameters[7].Value = model.Country;
            parameters[8].Value = model.Language;
            parameters[9].Value = model.CreateTime;

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
        public bool Update(Maticsoft.WeChat.Model.Core.SceneDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeChat_SceneDetail set ");
            strSql.Append("SceneId=@SceneId,");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("NickName=@NickName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("City=@City,");
            strSql.Append("Province=@Province,");
            strSql.Append("Country=@Country,");
            strSql.Append("Language=@Language,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where DetailId=@DetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@SceneId", SqlDbType.Int,4),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@NickName", SqlDbType.NVarChar,200),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@City", SqlDbType.NVarChar,200),
					new SqlParameter("@Province", SqlDbType.NVarChar,200),
					new SqlParameter("@Country", SqlDbType.NVarChar,200),
					new SqlParameter("@Language", SqlDbType.NVarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@DetailId", SqlDbType.Int,4)};
            parameters[0].Value = model.SceneId;
            parameters[1].Value = model.OpenId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.NickName;
            parameters[4].Value = model.Sex;
            parameters[5].Value = model.City;
            parameters[6].Value = model.Province;
            parameters[7].Value = model.Country;
            parameters[8].Value = model.Language;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.DetailId;

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
        public bool Delete(int DetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_SceneDetail ");
            strSql.Append(" where DetailId=@DetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.Int,4)
			};
            parameters[0].Value = DetailId;

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
        public bool DeleteList(string DetailIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeChat_SceneDetail ");
            strSql.Append(" where DetailId in (" + DetailIdlist + ")  ");
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
        public Maticsoft.WeChat.Model.Core.SceneDetail GetModel(int DetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 DetailId,SceneId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,CreateTime from WeChat_SceneDetail ");
            strSql.Append(" where DetailId=@DetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.Int,4)
			};
            parameters[0].Value = DetailId;

            Maticsoft.WeChat.Model.Core.SceneDetail model = new Maticsoft.WeChat.Model.Core.SceneDetail();
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
        public Maticsoft.WeChat.Model.Core.SceneDetail DataRowToModel(DataRow row)
        {
            Maticsoft.WeChat.Model.Core.SceneDetail model = new Maticsoft.WeChat.Model.Core.SceneDetail();
            if (row != null)
            {
                if (row["DetailId"] != null && row["DetailId"].ToString() != "")
                {
                    model.DetailId = int.Parse(row["DetailId"].ToString());
                }
                if (row["SceneId"] != null && row["SceneId"].ToString() != "")
                {
                    model.SceneId = int.Parse(row["SceneId"].ToString());
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["NickName"] != null)
                {
                    model.NickName = row["NickName"].ToString();
                }
                if (row["Sex"] != null && row["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(row["Sex"].ToString());
                }
                if (row["City"] != null)
                {
                    model.City = row["City"].ToString();
                }
                if (row["Province"] != null)
                {
                    model.Province = row["Province"].ToString();
                }
                if (row["Country"] != null)
                {
                    model.Country = row["Country"].ToString();
                }
                if (row["Language"] != null)
                {
                    model.Language = row["Language"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
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
            strSql.Append("select DetailId,SceneId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,CreateTime ");
            strSql.Append(" FROM WeChat_SceneDetail ");
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
            strSql.Append(" DetailId,SceneId,OpenId,UserName,NickName,Sex,City,Province,Country,Language,CreateTime ");
            strSql.Append(" FROM WeChat_SceneDetail ");
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
            strSql.Append("select count(1) FROM WeChat_SceneDetail ");
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
                strSql.Append("order by T.DetailId desc");
            }
            strSql.Append(")AS Row, T.*  from WeChat_SceneDetail T ");
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
            parameters[0].Value = "WeChat_SceneDetail";
            parameters[1].Value = "DetailId";
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
	}
}

