using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Ms;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Ms
{
	/// <summary>
	/// 数据访问类:ThumbnailSize
	/// </summary>
	public partial class ThumbnailSize:IThumbnailSize
	{
		public ThumbnailSize()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ThumName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_ThumbnailSize");
            strSql.Append(" where ThumName=@ThumName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ThumName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ThumName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Ms.ThumbnailSize model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ms_ThumbnailSize(");
            strSql.Append("ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme)");
            strSql.Append(" values (");
            strSql.Append("@ThumName,@ThumWidth,@ThumHeight,@Type,@Remark,@CloudSizeName,@CloudType,@ThumMode,@IsWatermark,@Theme)");
            SqlParameter[] parameters = {
					new SqlParameter("@ThumName", SqlDbType.NVarChar,50),
					new SqlParameter("@ThumWidth", SqlDbType.Int,4),
					new SqlParameter("@ThumHeight", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@CloudSizeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CloudType", SqlDbType.Int,4),
					new SqlParameter("@ThumMode", SqlDbType.Int,4),
					new SqlParameter("@IsWatermark", SqlDbType.Bit,1),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ThumName;
            parameters[1].Value = model.ThumWidth;
            parameters[2].Value = model.ThumHeight;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CloudSizeName;
            parameters[6].Value = model.CloudType;
            parameters[7].Value = model.ThumMode;
            parameters[8].Value = model.IsWatermark;
            parameters[9].Value = model.Theme;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Ms.ThumbnailSize model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_ThumbnailSize set ");
            strSql.Append("ThumWidth=@ThumWidth,");
            strSql.Append("ThumHeight=@ThumHeight,");
            strSql.Append("Type=@Type,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CloudSizeName=@CloudSizeName,");
            strSql.Append("CloudType=@CloudType,");
            strSql.Append("ThumMode=@ThumMode,");
            strSql.Append("IsWatermark=@IsWatermark,");
            strSql.Append("Theme=@Theme");
            strSql.Append(" where ThumName=@ThumName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ThumWidth", SqlDbType.Int,4),
					new SqlParameter("@ThumHeight", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@CloudSizeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CloudType", SqlDbType.Int,4),
					new SqlParameter("@ThumMode", SqlDbType.Int,4),
					new SqlParameter("@IsWatermark", SqlDbType.Bit,1),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@ThumName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ThumWidth;
            parameters[1].Value = model.ThumHeight;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CloudSizeName;
            parameters[5].Value = model.CloudType;
            parameters[6].Value = model.ThumMode;
            parameters[7].Value = model.IsWatermark;
            parameters[8].Value = model.Theme;
            parameters[9].Value = model.ThumName;

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
        public bool Delete(string ThumName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_ThumbnailSize ");
            strSql.Append(" where ThumName=@ThumName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ThumName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ThumName;

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
        public bool DeleteList(string ThumNamelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ms_ThumbnailSize ");
            strSql.Append(" where ThumName in (" + ThumNamelist + ")  ");
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
        public Maticsoft.Model.Ms.ThumbnailSize GetModel(string ThumName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme from Ms_ThumbnailSize ");
            strSql.Append(" where ThumName=@ThumName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ThumName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ThumName;

            Maticsoft.Model.Ms.ThumbnailSize model = new Maticsoft.Model.Ms.ThumbnailSize();
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
        public Maticsoft.Model.Ms.ThumbnailSize DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.ThumbnailSize model = new Maticsoft.Model.Ms.ThumbnailSize();
            if (row != null)
            {
                if (row["ThumName"] != null)
                {
                    model.ThumName = row["ThumName"].ToString();
                }
                if (row["ThumWidth"] != null && row["ThumWidth"].ToString() != "")
                {
                    model.ThumWidth = int.Parse(row["ThumWidth"].ToString());
                }
                if (row["ThumHeight"] != null && row["ThumHeight"].ToString() != "")
                {
                    model.ThumHeight = int.Parse(row["ThumHeight"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["CloudSizeName"] != null)
                {
                    model.CloudSizeName = row["CloudSizeName"].ToString();
                }
                if (row["CloudType"] != null && row["CloudType"].ToString() != "")
                {
                    model.CloudType = int.Parse(row["CloudType"].ToString());
                }
                if (row["ThumMode"] != null && row["ThumMode"].ToString() != "")
                {
                    model.ThumMode = int.Parse(row["ThumMode"].ToString());
                }
                if (row["IsWatermark"] != null && row["IsWatermark"].ToString() != "")
                {
                    if ((row["IsWatermark"].ToString() == "1") || (row["IsWatermark"].ToString().ToLower() == "true"))
                    {
                        model.IsWatermark = true;
                    }
                    else
                    {
                        model.IsWatermark = false;
                    }
                }
                if (row["Theme"] != null)
                {
                    model.Theme = row["Theme"].ToString();
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
            strSql.Append("select ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme ");
            strSql.Append(" FROM Ms_ThumbnailSize ");
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
            strSql.Append(" ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme ");
            strSql.Append(" FROM Ms_ThumbnailSize ");
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
            strSql.Append("select count(1) FROM Ms_ThumbnailSize ");
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
                strSql.Append("order by T.ThumName desc");
            }
            strSql.Append(")AS Row, T.*  from Ms_ThumbnailSize T ");
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
            parameters[0].Value = "Ms_ThumbnailSize";
            parameters[1].Value = "ThumName";
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
        /// 是否存在该记录
        /// </summary>
        /// <param name="ThumName">ThumName</param>
        /// <param name="type">区域</param>
        /// <param name="Theme">模版名称</param>
        /// <returns></returns>
        public bool Exists(string ThumName,int type,string Theme  )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_ThumbnailSize");
            strSql.Append(" where ThumName=@ThumName and Type=@Type and Theme=@Theme");
            SqlParameter[] parameters = {
					new SqlParameter("@ThumName", SqlDbType.NVarChar,50)	,
                                        new SqlParameter("@Type", SqlDbType.Int,4),
                                        new SqlParameter("@Theme", SqlDbType.NVarChar,100),
                                        };
            parameters[0].Value = ThumName;
            parameters[1].Value = type;
            parameters[2].Value = Theme;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
		#endregion  ExtensionMethod
	}
}

