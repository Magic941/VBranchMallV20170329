using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;//请先添加引用
using Maticsoft.IDAL.Poll;

namespace Maticsoft.SQLServerDAL.Poll
{
    /// <summary>
    /// 数据访问类Forms。
    /// </summary>
    public class Forms : IForms
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("FormID", "Poll_Forms");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Poll_Forms");
            strSql.Append(" where FormID=@FormID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.Int,4)};
            parameters[0].Value = FormID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Poll.Forms model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Poll_Forms(");
            strSql.Append("Name,IsActive,Description)");
            strSql.Append(" values (");
            strSql.Append("@Name,@IsActive,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@IsActive", SqlDbType.Bit),
					new SqlParameter("@Description", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.IsActive;
            parameters[2].Value = model.Description;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Maticsoft.Model.Poll.Forms model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Poll_Forms set ");
            strSql.Append("Name=@Name,");
            strSql.Append("IsActive=@IsActive,");
            strSql.Append("Description=@Description");
            strSql.Append(" where FormID=@FormID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@IsActive", SqlDbType.Bit),
					new SqlParameter("@Description", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.IsActive;
            parameters[3].Value = model.Description;

          return  DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int FormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Poll_Forms ");
            strSql.Append(" where FormID= " + FormID.ToString());

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from Poll_Topics where FormID= " + FormID.ToString());

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from Poll_UserPoll ");
            strSql2.Append(" where TopicID in (select ID from Poll_Topics where FormID=" + FormID.ToString() + ") ");

            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from Poll_Options ");
            strSql4.Append(" where TopicID in (select ID from Poll_Topics where FormID=" + FormID.ToString() + ") ");

            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from Poll_Reply ");
            strSql5.Append(" where TopicID in (select ID from Poll_Topics where FormID=" + FormID.ToString() + ") ");

            List<string> sqllist = new List<string>();

            sqllist.Add(strSql2.ToString());
            sqllist.Add(strSql4.ToString());
            sqllist.Add(strSql5.ToString());
            sqllist.Add(strSql3.ToString());
            sqllist.Add(strSql.ToString());

            DbHelperSQL.ExecuteSqlTran(sqllist);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ClassIDlist"></param>
        /// <returns></returns>
        public bool DeleteList(string FormIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Poll_Forms ");
            strSql.Append(" where FormID in (" + FormIDlist + ")  ");

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from Poll_Topics where FormID in (" + FormIDlist + ")  ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from Poll_UserPoll ");
            strSql2.Append(" where TopicID in (select ID from Poll_Topics where FormID in(" + FormIDlist + ")) ");

            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from Poll_Options ");
            strSql4.Append(" where TopicID in (select ID from Poll_Topics where FormID in(" + FormIDlist + ")) ");

            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from Poll_Reply ");
            strSql5.Append(" where TopicID in (select ID from Poll_Topics where FormID in(" + FormIDlist + ")) ");

            List<string> sqllist = new List<string>();

            sqllist.Add(strSql2.ToString());
            sqllist.Add(strSql4.ToString());
            sqllist.Add(strSql5.ToString());
            sqllist.Add(strSql3.ToString());
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
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
        public Maticsoft.Model.Poll.Forms GetModel(int FormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Poll_Forms ");
            strSql.Append(" where FormID=@FormID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.Int,4)};
            parameters[0].Value = FormID;

            Maticsoft.Model.Poll.Forms model = new Maticsoft.Model.Poll.Forms();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["FormID"].ToString() != "")
                {
                    model.FormID = int.Parse(ds.Tables[0].Rows[0]["FormID"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();

                if (ds.Tables[0].Rows[0]["IsActive"] != null && ds.Tables[0].Rows[0]["IsActive"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsActive"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActive"].ToString().ToLower() == "true"))
                    {
                        model.IsActive = true;
                    }
                    else
                    {
                        model.IsActive = false;
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Poll_Forms ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion 成员方法
    }
}