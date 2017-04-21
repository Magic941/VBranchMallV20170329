using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//请先添加引用
using Maticsoft.IDAL.Poll;
namespace Maticsoft.SQLServerDAL.Poll
{
	/// <summary>
	/// 数据访问类Options。
	/// </summary>
	public class Options:IOptions
	{
		
		#region  成员方法

		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int TopicID, string Name)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Poll_Options");
            strSql.Append(" where TopicID=@TopicID and Name=@Name ");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicID", SqlDbType.Int,4),
                                        new SqlParameter("@Name", SqlDbType.NVarChar)};
            parameters[0].Value = TopicID;
            parameters[1].Value = Name;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Poll.Options model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Poll_Options(");
			strSql.Append("Name,TopicID,isChecked,SubmitNum)");
			strSql.Append(" values (");
			strSql.Append("@Name,@TopicID,@isChecked,@SubmitNum)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,150),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@isChecked", SqlDbType.SmallInt,2),
					new SqlParameter("@SubmitNum", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.TopicID;
			parameters[2].Value = model.isChecked;
			parameters[3].Value = model.SubmitNum;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public void Update(Maticsoft.Model.Poll.Options model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Poll_Options set ");
			strSql.Append("Name=@Name,");
			strSql.Append("TopicID=@TopicID,");
			strSql.Append("isChecked=@isChecked,");
			strSql.Append("SubmitNum=@SubmitNum");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,150),
					new SqlParameter("@TopicID", SqlDbType.Int,4),
					new SqlParameter("@isChecked", SqlDbType.SmallInt,2),
					new SqlParameter("@SubmitNum", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.TopicID;
			parameters[3].Value = model.isChecked;
			parameters[4].Value = model.SubmitNum;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Poll_Options ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ClassIDlist"></param>
        /// <returns></returns>
        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Poll_Options ");
            strSql.Append(" where ID in (" + ClassIDlist + ")  ");
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
		public Maticsoft.Model.Poll.Options GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Name,TopicID,isChecked,SubmitNum from Poll_Options ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			Maticsoft.Model.Poll.Options model=new Maticsoft.Model.Poll.Options();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				if(ds.Tables[0].Rows[0]["TopicID"].ToString()!="")
				{
					model.TopicID=int.Parse(ds.Tables[0].Rows[0]["TopicID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isChecked"].ToString()!="")
				{
					model.isChecked=int.Parse(ds.Tables[0].Rows[0]["isChecked"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubmitNum"].ToString()!="")
				{
					model.SubmitNum=int.Parse(ds.Tables[0].Rows[0]["SubmitNum"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,Name,TopicID,isChecked,SubmitNum ");
			strSql.Append(" FROM Poll_Options ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 得到问卷投票统计
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        public DataSet GetCountList(int FormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Name,SubmitNum,TopicID from Poll_Options ");
            strSql.Append(" where TopicID in  (");
            strSql.Append(" select ID from Poll_Topics where FormID=" + FormID);
            strSql.Append(")");
            strSql.Append("order by ID");
            
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到问卷投票统计
        /// </summary>
        /// <param name="strwhere"></param>
        /// <returns></returns>
        public DataSet GetCountList(string strwhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Name,SubmitNum,TopicID from Poll_Options ");
            strSql.Append(" where TopicID in  (");
            strSql.Append(" select ID from Poll_Topics ");
            if (strwhere.Length > 0)
                strSql.AppendFormat(" where {0} ",strwhere);
            strSql.Append(")");
            strSql.Append("order by ID");
            return DbHelperSQL.Query(strSql.ToString());
        }

		#endregion  成员方法
	}
}

