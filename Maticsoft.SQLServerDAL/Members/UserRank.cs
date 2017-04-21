using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Members
{
	/// <summary>
	/// 数据访问类:UserRank
	/// </summary>
	public partial class UserRank:IUserRank
	{
		public UserRank()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("RankId", "Accounts_UserRank");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RankId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_UserRank");
            strSql.Append(" where RankId=@RankId");
            SqlParameter[] parameters = {
					new SqlParameter("@RankId", SqlDbType.Int,4)
			};
            parameters[0].Value = RankId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.UserRank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UserRank(");
            strSql.Append("Name,RankLevel,PointMax,PointMin,IsDefault,RankType,IsMemberCreated,Description,CreatorUserId,PriceType,PriceOperations,PriceValue)");
            strSql.Append(" values (");
            strSql.Append("@Name,@RankLevel,@PointMax,@PointMin,@IsDefault,@RankType,@IsMemberCreated,@Description,@CreatorUserId,@PriceType,@PriceOperations,@PriceValue)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@RankLevel", SqlDbType.Int,4),
					new SqlParameter("@PointMax", SqlDbType.Int,4),
					new SqlParameter("@PointMin", SqlDbType.Int,4),
					new SqlParameter("@IsDefault", SqlDbType.Bit,1),
					new SqlParameter("@RankType", SqlDbType.Int,4),
					new SqlParameter("@IsMemberCreated", SqlDbType.Bit,1),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatorUserId", SqlDbType.Int,4),
					new SqlParameter("@PriceType", SqlDbType.NVarChar,20),
					new SqlParameter("@PriceOperations", SqlDbType.NVarChar,10),
					new SqlParameter("@PriceValue", SqlDbType.Money,8)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.RankLevel;
            parameters[2].Value = model.PointMax;
            parameters[3].Value = model.PointMin;
            parameters[4].Value = model.IsDefault;
            parameters[5].Value = model.RankType;
            parameters[6].Value = model.IsMemberCreated;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.CreatorUserId;
            parameters[9].Value = model.PriceType;
            parameters[10].Value = model.PriceOperations;
            parameters[11].Value = model.PriceValue;

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
        public bool Update(Maticsoft.Model.Members.UserRank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UserRank set ");
            strSql.Append("Name=@Name,");
            strSql.Append("RankLevel=@RankLevel,");
            strSql.Append("PointMax=@PointMax,");
            strSql.Append("PointMin=@PointMin,");
            strSql.Append("IsDefault=@IsDefault,");
            strSql.Append("RankType=@RankType,");
            strSql.Append("IsMemberCreated=@IsMemberCreated,");
            strSql.Append("Description=@Description,");
            strSql.Append("CreatorUserId=@CreatorUserId,");
            strSql.Append("PriceType=@PriceType,");
            strSql.Append("PriceOperations=@PriceOperations,");
            strSql.Append("PriceValue=@PriceValue");
            strSql.Append(" where RankId=@RankId");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@RankLevel", SqlDbType.Int,4),
					new SqlParameter("@PointMax", SqlDbType.Int,4),
					new SqlParameter("@PointMin", SqlDbType.Int,4),
					new SqlParameter("@IsDefault", SqlDbType.Bit,1),
					new SqlParameter("@RankType", SqlDbType.Int,4),
					new SqlParameter("@IsMemberCreated", SqlDbType.Bit,1),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatorUserId", SqlDbType.Int,4),
					new SqlParameter("@PriceType", SqlDbType.NVarChar,20),
					new SqlParameter("@PriceOperations", SqlDbType.NVarChar,10),
					new SqlParameter("@PriceValue", SqlDbType.Money,8),
					new SqlParameter("@RankId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.RankLevel;
            parameters[2].Value = model.PointMax;
            parameters[3].Value = model.PointMin;
            parameters[4].Value = model.IsDefault;
            parameters[5].Value = model.RankType;
            parameters[6].Value = model.IsMemberCreated;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.CreatorUserId;
            parameters[9].Value = model.PriceType;
            parameters[10].Value = model.PriceOperations;
            parameters[11].Value = model.PriceValue;
            parameters[12].Value = model.RankId;

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
        public bool Delete(int RankId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UserRank ");
            strSql.Append(" where RankId=@RankId");
            SqlParameter[] parameters = {
					new SqlParameter("@RankId", SqlDbType.Int,4)
			};
            parameters[0].Value = RankId;

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
        public bool DeleteList(string RankIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Accounts_UserRank ");
            strSql.Append(" where RankId in (" + RankIdlist + ")  ");
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
        public Maticsoft.Model.Members.UserRank GetModel(int RankId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RankId,Name,RankLevel,PointMax,PointMin,IsDefault,RankType,IsMemberCreated,Description,CreatorUserId,PriceType,PriceOperations,PriceValue from Accounts_UserRank ");
            strSql.Append(" where RankId=@RankId");
            SqlParameter[] parameters = {
					new SqlParameter("@RankId", SqlDbType.Int,4)
			};
            parameters[0].Value = RankId;

            Maticsoft.Model.Members.UserRank model = new Maticsoft.Model.Members.UserRank();
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
        public Maticsoft.Model.Members.UserRank DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.UserRank model = new Maticsoft.Model.Members.UserRank();
            if (row != null)
            {
                if (row["RankId"] != null && row["RankId"].ToString() != "")
                {
                    model.RankId = int.Parse(row["RankId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["RankLevel"] != null && row["RankLevel"].ToString() != "")
                {
                    model.RankLevel = int.Parse(row["RankLevel"].ToString());
                }
                if (row["PointMax"] != null && row["PointMax"].ToString() != "")
                {
                    model.PointMax = int.Parse(row["PointMax"].ToString());
                }
                if (row["PointMin"] != null && row["PointMin"].ToString() != "")
                {
                    model.PointMin = int.Parse(row["PointMin"].ToString());
                }
                if (row["IsDefault"] != null && row["IsDefault"].ToString() != "")
                {
                    if ((row["IsDefault"].ToString() == "1") || (row["IsDefault"].ToString().ToLower() == "true"))
                    {
                        model.IsDefault = true;
                    }
                    else
                    {
                        model.IsDefault = false;
                    }
                }
                if (row["RankType"] != null && row["RankType"].ToString() != "")
                {
                    model.RankType = int.Parse(row["RankType"].ToString());
                }
                if (row["IsMemberCreated"] != null && row["IsMemberCreated"].ToString() != "")
                {
                    if ((row["IsMemberCreated"].ToString() == "1") || (row["IsMemberCreated"].ToString().ToLower() == "true"))
                    {
                        model.IsMemberCreated = true;
                    }
                    else
                    {
                        model.IsMemberCreated = false;
                    }
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["CreatorUserId"] != null && row["CreatorUserId"].ToString() != "")
                {
                    model.CreatorUserId = int.Parse(row["CreatorUserId"].ToString());
                }
                if (row["PriceType"] != null)
                {
                    model.PriceType = row["PriceType"].ToString();
                }
                if (row["PriceOperations"] != null)
                {
                    model.PriceOperations = row["PriceOperations"].ToString();
                }
                if (row["PriceValue"] != null && row["PriceValue"].ToString() != "")
                {
                    model.PriceValue = decimal.Parse(row["PriceValue"].ToString());
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
            strSql.Append("select RankId,Name,RankLevel,PointMax,PointMin,IsDefault,RankType,IsMemberCreated,Description,CreatorUserId,PriceType,PriceOperations,PriceValue ");
            strSql.Append(" FROM Accounts_UserRank ");
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
            strSql.Append(" RankId,Name,RankLevel,PointMax,PointMin,IsDefault,RankType,IsMemberCreated,Description,CreatorUserId,PriceType,PriceOperations,PriceValue ");
            strSql.Append(" FROM Accounts_UserRank ");
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
            strSql.Append("select count(1) FROM Accounts_UserRank ");
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
                strSql.Append("order by T.RankId desc");
            }
            strSql.Append(")AS Row, T.*  from Accounts_UserRank T ");
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
            parameters[0].Value = "Accounts_UserRank";
            parameters[1].Value = "RankId";
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
        /// 根据用户分数获取等级
        /// </summary>
        /// <param name="grades">用户分数</param>
        /// <returns></returns>
        public string GetUserLevel(int grades)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   top 1 name from Accounts_UserRank ");
            strSql.Append(" WHERE @Score BETWEEN PointMin AND PointMax");
            SqlParameter[] parameters = {
					new SqlParameter("@Score", SqlDbType.Int,4)
			};
            parameters[0].Value = grades;

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

       
		#endregion  ExtensionMethod
	}
}

