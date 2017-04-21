using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Ms;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Ms
{
	/// <summary>
	/// 数据访问类:EmailTemplet
	/// </summary>
	public partial class EmailTemplet:IEmailTemplet
	{
		public EmailTemplet()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TempletId", "Ms_EmailTemplet");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TempletId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_EmailTemplet");
            strSql.Append(" where TempletId=@TempletId");
            SqlParameter[] parameters = {
					new SqlParameter("@TempletId", SqlDbType.Int,4)
			};
            parameters[0].Value = TempletId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Ms.EmailTemplet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ms_EmailTemplet(");
            strSql.Append("EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody)");
            strSql.Append(" values (");
            strSql.Append("@EmailType,@EmailPriority,@TagDescription,@EmailDescription,@EmailSubject,@EmailBody)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@EmailType", SqlDbType.NVarChar,100),
					new SqlParameter("@EmailPriority", SqlDbType.Int,4),
					new SqlParameter("@TagDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@EmailDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@EmailSubject", SqlDbType.NVarChar,1024),
					new SqlParameter("@EmailBody", SqlDbType.NText)};
            parameters[0].Value = model.EmailType;
            parameters[1].Value = model.EmailPriority;
            parameters[2].Value = model.TagDescription;
            parameters[3].Value = model.EmailDescription;
            parameters[4].Value = model.EmailSubject;
            parameters[5].Value = model.EmailBody;

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
        public bool Update(Maticsoft.Model.Ms.EmailTemplet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_EmailTemplet set ");
            strSql.Append("EmailType=@EmailType,");
            strSql.Append("EmailPriority=@EmailPriority,");
            strSql.Append("TagDescription=@TagDescription,");
            strSql.Append("EmailDescription=@EmailDescription,");
            strSql.Append("EmailSubject=@EmailSubject,");
            strSql.Append("EmailBody=@EmailBody");
            strSql.Append(" where TempletId=@TempletId");
            SqlParameter[] parameters = {
					new SqlParameter("@EmailType", SqlDbType.NVarChar,100),
					new SqlParameter("@EmailPriority", SqlDbType.Int,4),
					new SqlParameter("@TagDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@EmailDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@EmailSubject", SqlDbType.NVarChar,1024),
					new SqlParameter("@EmailBody", SqlDbType.NText),
					new SqlParameter("@TempletId", SqlDbType.Int,4)};
            parameters[0].Value = model.EmailType;
            parameters[1].Value = model.EmailPriority;
            parameters[2].Value = model.TagDescription;
            parameters[3].Value = model.EmailDescription;
            parameters[4].Value = model.EmailSubject;
            parameters[5].Value = model.EmailBody;
            parameters[6].Value = model.TempletId;

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
        public bool Delete(int TempletId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_EmailTemplet ");
            strSql.Append(" where TempletId=@TempletId");
            SqlParameter[] parameters = {
					new SqlParameter("@TempletId", SqlDbType.Int,4)
			};
            parameters[0].Value = TempletId;

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
        public bool DeleteList(string TempletIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_EmailTemplet ");
            strSql.Append(" where TempletId in (" + TempletIdlist + ")  ");
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
        public Maticsoft.Model.Ms.EmailTemplet GetModel(int TempletId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TempletId,EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody from Ms_EmailTemplet ");
            strSql.Append(" where TempletId=@TempletId");
            SqlParameter[] parameters = {
					new SqlParameter("@TempletId", SqlDbType.Int,4)
			};
            parameters[0].Value = TempletId;

            Maticsoft.Model.Ms.EmailTemplet model = new Maticsoft.Model.Ms.EmailTemplet();
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
        public Maticsoft.Model.Ms.EmailTemplet DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.EmailTemplet model = new Maticsoft.Model.Ms.EmailTemplet();
            if (row != null)
            {
                if (row["TempletId"] != null && row["TempletId"].ToString() != "")
                {
                    model.TempletId = int.Parse(row["TempletId"].ToString());
                }
                if (row["EmailType"] != null)
                {
                    model.EmailType = row["EmailType"].ToString();
                }
                if (row["EmailPriority"] != null && row["EmailPriority"].ToString() != "")
                {
                    model.EmailPriority = int.Parse(row["EmailPriority"].ToString());
                }
                if (row["TagDescription"] != null)
                {
                    model.TagDescription = row["TagDescription"].ToString();
                }
                if (row["EmailDescription"] != null)
                {
                    model.EmailDescription = row["EmailDescription"].ToString();
                }
                if (row["EmailSubject"] != null)
                {
                    model.EmailSubject = row["EmailSubject"].ToString();
                }
                if (row["EmailBody"] != null)
                {
                    model.EmailBody = row["EmailBody"].ToString();
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
            strSql.Append("select TempletId,EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody ");
            strSql.Append(" FROM Ms_EmailTemplet ");
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
            strSql.Append(" TempletId,EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody ");
            strSql.Append(" FROM Ms_EmailTemplet ");
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
            strSql.Append("select count(1) FROM Ms_EmailTemplet ");
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
                strSql.Append("order by T.TempletId desc");
            }
            strSql.Append(")AS Row, T.*  from Ms_EmailTemplet T ");
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
            parameters[0].Value = "Ms_EmailTemplet";
            parameters[1].Value = "TempletId";
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

