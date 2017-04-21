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
    /// 数据访问类UserPoll。
    /// </summary>
    public class UserPoll : IUserPoll
    {
        #region 成员方法

        /// <summary>
        /// 用户投票 
        /// </summary>
        public void Add(Model.Poll.UserPoll model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            strSql1.Append("UserID,");
            strSql2.Append("" + model.UserID + ",");

            if (model.TopicID != null)
            {
                strSql1.Append("TopicID,");
                strSql2.Append("" + model.TopicID + ",");
            }
            if (model.OptionID != null)
            {
                strSql1.Append("OptionID,");
                strSql2.Append("" + model.OptionID + ",");
            }

            strSql1.Append("CreatTime,");
            strSql2.Append("'" + DateTime.Now.ToString() + "',");

            strSql1.Append("UserIP");
            strSql2.Append("'" + model.UserIP + "'");

            strSql.Append("insert into Poll_UserPoll(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("update Poll_Options set ");
            strSql3.Append("SubmitNum=SubmitNum+1");
            strSql3.Append(" where ID=" + model.OptionID);

            List<string> sqllist = new List<string>();
            sqllist.Add(strSql.ToString());
            sqllist.Add(strSql3.ToString());

            DbHelperSQL.ExecuteSqlTran(sqllist);
        }
        /// <summary>
        /// 用户投票 多选题的投票
        /// </summary>
        public bool Add2(Model.Poll.UserPoll model)
        {
            List<string> sqllist = new List<string>();
            if (model == null || String.IsNullOrWhiteSpace(model.OptionIDList))
                return false;
            string[] optionid = model.OptionIDList.Split(new char[] { ',' });
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Poll_UserPoll(UserID,TopicID,OptionID,CreatTime,UserIP) values ");
            foreach (var item in optionid)
            {
                strSql.AppendFormat(" ( {0},{1},{2},'{3}','{4}' ),", model.UserID, model.TopicID, item, DateTime.Now, model.UserIP);
            }
            string strsql2= strSql.ToString().TrimEnd(',');
            sqllist.Add(strsql2);
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("update Poll_Options set ");
            strSql3.Append("SubmitNum=SubmitNum+1");
            strSql3.Append(" where ID  in (" + model.OptionIDList + ")");
            sqllist.Add(strSql3.ToString());
            int rows= DbHelperSQL.ExecuteSqlTran(sqllist);
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
        /// 更新一条数据
        /// </summary>
        public void Update(Maticsoft.Model.Poll.UserPoll model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Poll_UserPoll set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("TopicID=@TopicID,");
            strSql.Append("OptionID=@OptionID,");
            strSql.Append("CreatTime=@CreatTime");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@OptionID", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.DateTime)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.TopicID;
            parameters[2].Value = model.OptionID;
            parameters[3].Value = model.CreatTime;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Poll_UserPoll ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
     

        /// <summary>
        /// 获取参与问卷的用户数
        /// </summary>
        public int GetUserByForm(int FormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(distinct userid) from Poll_UserPoll ");
            if (FormID > 0)
            {
                strSql.Append(" where TopicID in ( ");
                strSql.Append(" select ID from Poll_Topics where FormID=" + FormID);
                strSql.Append(" )");
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListInnerJoin(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Poll_UserPoll AS userpoll  INNER JOIN    Poll_Options  AS opt ON userpoll.TopicID = opt.TopicID AND userpoll.OptionID=opt.ID ");
            if (userid > 0)
            {
                strSql.AppendFormat("  AND  UserID ={0} ", userid);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion 成员方法
    }
}