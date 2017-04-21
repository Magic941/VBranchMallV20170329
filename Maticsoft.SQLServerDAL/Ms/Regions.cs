using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Ms;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Ms
{
	/// <summary>
	/// 数据访问类:Regions
	/// </summary>
	public partial class Regions:IRegions
	{
		public Regions()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("RegionId", "Ms_Regions");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RegionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_Regions");
            strSql.Append(" where RegionId=@RegionId");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionId", SqlDbType.Int,4)
			};
            parameters[0].Value = RegionId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Ms.Regions model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ms_Regions(");
            strSql.Append("AreaId,ParentId,RegionName,Spell,SpellShort,DisplaySequence,Path,Depth)");
            strSql.Append(" values (");
            strSql.Append("@AreaId,@ParentId,@RegionName,@Spell,@SpellShort,@DisplaySequence,@Path,@Depth)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@RegionName", SqlDbType.NVarChar,100),
					new SqlParameter("@Spell", SqlDbType.NVarChar,50),
					new SqlParameter("@SpellShort", SqlDbType.NVarChar,50),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,4000),
					new SqlParameter("@Depth", SqlDbType.Int,4)};
            parameters[0].Value = model.AreaId;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.RegionName;
            parameters[3].Value = model.Spell;
            parameters[4].Value = model.SpellShort;
            parameters[5].Value = model.DisplaySequence;
            parameters[6].Value = model.Path;
            parameters[7].Value = model.Depth;

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
        public bool Update(Maticsoft.Model.Ms.Regions model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_Regions set ");
            strSql.Append("AreaId=@AreaId,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("RegionName=@RegionName,");
            strSql.Append("Spell=@Spell,");
            strSql.Append("SpellShort=@SpellShort,");
            strSql.Append("DisplaySequence=@DisplaySequence,");
            strSql.Append("Path=@Path,");
            strSql.Append("Depth=@Depth");
            strSql.Append(" where RegionId=@RegionId");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@RegionName", SqlDbType.NVarChar,100),
					new SqlParameter("@Spell", SqlDbType.NVarChar,50),
					new SqlParameter("@SpellShort", SqlDbType.NVarChar,50),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,4000),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@RegionId", SqlDbType.Int,4)};
            parameters[0].Value = model.AreaId;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.RegionName;
            parameters[3].Value = model.Spell;
            parameters[4].Value = model.SpellShort;
            parameters[5].Value = model.DisplaySequence;
            parameters[6].Value = model.Path;
            parameters[7].Value = model.Depth;
            parameters[8].Value = model.RegionId;

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
        public bool Delete(int RegionId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_Regions ");
            strSql.Append(" where RegionId=@RegionId");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionId", SqlDbType.Int,4)
			};
            parameters[0].Value = RegionId;

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
        public bool DeleteList(string RegionIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_Regions ");
            strSql.Append(" where RegionId in (" + RegionIdlist + ")  ");
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
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Ms_Regions ");
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
                strSql.Append("order by T.RegionId desc");
            }
            strSql.Append(")AS Row, T.*  from Ms_Regions T ");
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
            parameters[0].Value = "Ms_Regions";
            parameters[1].Value = "RegionId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region NewMethod
        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetProvinces()
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT TR.RegionId,RegionName FROM Ms_Regions TR ");
            str.Append("WHERE AreaId BETWEEN 1 AND 10 ");
            return DbHelperSQL.Query(str.ToString());
        }
        /// <summary>
        /// 根据省份获取城市
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public DataSet GetCitys(int parentID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT RegionId,RegionName  ");
            str.Append("FROM Ms_Regions  ");
            str.Append("WHERE ParentId= " + parentID);
            return DbHelperSQL.Query(str.ToString());
        }

        /// <summary>
        /// 获取读取父Id
        /// </summary>
        /// <param name="regionID"></param>
        /// <returns></returns>
        public DataTable GetParentID(int regionID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT ParentId  ");
            str.Append("FROM Ms_Regions  ");
            str.Append("WHERE RegionId= " + regionID);
            return DbHelperSQL.Query(str.ToString()).Tables[0];
        }
        public int GetCurrentParentId(int regionId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT ParentId  ");
            str.Append("FROM Ms_Regions  ");
            str.Append("WHERE RegionId= " + regionId);
            object obj = DbHelperSQL.GetSingle(str.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Ms.Regions DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.Regions model = new Maticsoft.Model.Ms.Regions();
            if (row != null)
            {
                if (row["AreaId"] != null && row["AreaId"].ToString() != "")
                {
                    model.AreaId = int.Parse(row["AreaId"].ToString());
                }
                if (row["RegionId"] != null && row["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["ParentId"] != null && row["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(row["ParentId"].ToString());
                }
                if (row["RegionName"] != null)
                {
                    model.RegionName = row["RegionName"].ToString();
                }
                if (row["Spell"] != null)
                {
                    model.Spell = row["Spell"].ToString();
                }
                if (row["SpellShort"] != null)
                {
                    model.SpellShort = row["SpellShort"].ToString();
                }
                if (row["DisplaySequence"] != null && row["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["Path"] != null)
                {
                    model.Path = row["Path"].ToString();
                }
                if (row["Depth"] != null && row["Depth"].ToString() != "")
                {
                    model.Depth = int.Parse(row["Depth"].ToString());
                }
            }
            return model;
        }



        public Model.Ms.Regions GetModel(int RegionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from MS_Regions ");
            strSql.Append(" where RegionId=@RegionId");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionId", SqlDbType.Int,4)
			};
            parameters[0].Value = RegionId;

            Maticsoft.Model.Ms.Regions model = new Maticsoft.Model.Ms.Regions();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AreaId"] != null && ds.Tables[0].Rows[0]["AreaId"].ToString() != "")
                {
                    model.AreaId = int.Parse(ds.Tables[0].Rows[0]["AreaId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RegionId"] != null && ds.Tables[0].Rows[0]["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(ds.Tables[0].Rows[0]["RegionId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RegionName"] != null && ds.Tables[0].Rows[0]["RegionName"].ToString() != "")
                {
                    model.RegionName = ds.Tables[0].Rows[0]["RegionName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Spell"] != null && ds.Tables[0].Rows[0]["Spell"].ToString() != "")
                {
                    model.Spell = ds.Tables[0].Rows[0]["Spell"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpellShort"] != null && ds.Tables[0].Rows[0]["SpellShort"].ToString() != "")
                {
                    model.SpellShort = ds.Tables[0].Rows[0]["SpellShort"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DisplaySequence"] != null && ds.Tables[0].Rows[0]["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(ds.Tables[0].Rows[0]["DisplaySequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Path"] != null && ds.Tables[0].Rows[0]["Path"].ToString() != "")
                {
                    model.Path = ds.Tables[0].Rows[0]["Path"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Depth"] != null && ds.Tables[0].Rows[0]["Depth"].ToString() != "")
                {
                    model.Depth = int.Parse(ds.Tables[0].Rows[0]["Depth"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public DataSet GetPrivoces()
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT * FROM Ms_Regions  ");
            str.Append("WHERE Depth=1 ");
            return DbHelperSQL.Query(str.ToString());
        }

        public DataSet GetPrivoceName()
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT TR.RegionId,RegionName FROM Ms_Regions TR ");
            str.Append("WHERE AreaId BETWEEN 1 AND 10 ");
            return DbHelperSQL.Query(str.ToString());
        }

        public DataSet GetRegionName(string parentID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT RegionId,RegionName ");
            str.Append("FROM Ms_Regions  ");
            str.Append("WHERE ParentId= " + parentID);
            return DbHelperSQL.Query(str.ToString());
        }

        public DataSet GetDistrictByParentId(int iParentId)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT *  ");
            str.Append("FROM Ms_Regions  ");
            str.Append("WHERE ParentId= " + iParentId);
            return DbHelperSQL.Query(str.ToString());
        }

        public DataSet GetAllCityList()
        {
            string strSql = "SELECT * FROM MS_Regions where Depth=2";
            return DbHelperSQL.Query(strSql);
        }

        public string GetPath(int regid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Path FROM MS_Regions ");
            strSql.Append("WHERE RegionId= " + regid);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "0.";
            }
        }

        public System.Collections.Generic.List<string> GetRegionNameByRID(int RID)
        {
            string path = GetPath(RID) + RID.ToString();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM MS_Regions ");
            strSql.Append("WHERE RegionId in (" + path + ")");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    string RegionName = dr["RegionName"].ToString();
                    //if (!(i == 0 && (RegionName == "北京" || RegionName == "上海" || RegionName == "天津" || RegionName == "重庆")))
                    //{
                    //    strReg.Append(RegionName);
                    //}
                    list.Add(RegionName);
                }
            }
            return list;
        }

        public int GetRegPath(int? regid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Depth FROM MS_Regions ");
            strSql.Append("WHERE RegionId= " + regid.Value);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }

        public DataSet GetParentIDs(int regID, out int Count)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@Region",SqlDbType.Int),
                                  new SqlParameter("@Count",SqlDbType.Int)
                                  };
            para[0].Value = regID;
            para[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.RunProcedure("sp_Accounts_GetRegionID", para, "ds");
            Count = Convert.ToInt32(para[1].Value);
            return ds;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Ms_Regions ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

	    /// <summary>
	    /// 更新多条数据的AreaID
	    /// </summary>
	    public bool UpdateAreaID(string regionlist,int AreaId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE    Ms_Regions SET  AreaId= @AreaId ");
            strSql.Append(" where RegionId in (" + regionlist + ")  ");
	        SqlParameter[] sqlpar = {new  SqlParameter("@AreaId",AreaId)};
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),sqlpar);
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
        /// 根据AreaID获取到Regionids
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
	    public string   GetRegionIDsByAreaId(int areaid)
        {
            StringBuilder returnstr = new StringBuilder();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  RegionId ");
            strSql.Append(" FROM Ms_Regions ");
            strSql.Append(" where  AreaId=" +areaid);
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        returnstr.Append("'" +reader["RegionId"] + "',");
                    }
                }
            }
            return returnstr.ToString().TrimEnd(',');
        }

        public DataSet GetSamePathArea(int regionId)
        {
            StringBuilder sb=new StringBuilder();
            sb.Append("select * from Ms_Regions  ");
            sb.Append("where ParentId=(");
            sb.Append("SELECT ParentId FROM Ms_Regions where regionId="+regionId);
            sb.Append(")");
            return DbHelperSQL.Query(sb.ToString());
        }

        public bool IsParentRegion(int regionId)
        {
           StringBuilder strSql = new StringBuilder();
           strSql.AppendFormat(" select count(1) from Ms_Regions where ParentId= {0} ",regionId);
           object obj = DbHelperSQL.GetSingle(strSql.ToString());
           if (obj ==null)
            {
                return false;
            }
               if (!(Convert.ToInt32(obj) > 0))
               {
                   return false;
               }
            else
            {
                return true;
            }
         
        }
        #endregion
	}
}

