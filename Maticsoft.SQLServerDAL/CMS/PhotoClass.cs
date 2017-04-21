using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.CMS;


namespace Maticsoft.SQLServerDAL.CMS
{
	/// <summary>
	/// 数据访问类:PhotoClass
	/// </summary>
	public class PhotoClass:IPhotoClass
	{
		public PhotoClass()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ClassID", "CMS_PhotoClass"); 
		}
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_PhotoClass");
        }


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ClassID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CMS_PhotoClass");
			strSql.Append(" where ClassID=@ClassID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = ClassID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByClassName(string ClassName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_PhotoClass");
            strSql.Append(" where ClassName=@ClassName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassName", SqlDbType.NVarChar,200)};
            parameters[0].Value = ClassName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Model.CMS.PhotoClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CMS_PhotoClass(");
			strSql.Append("ClassName,ParentId,Sequence,Path,Depth)");
			strSql.Append(" values (");
			strSql.Append("@ClassName,@ParentId,@Sequence,@Path,@Depth)");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassName", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@Depth", SqlDbType.Int,4)};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ParentId;
			parameters[2].Value = model.Sequence;
			parameters[3].Value = model.Path;
			parameters[4].Value = model.Depth;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.CMS.PhotoClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CMS_PhotoClass set ");
			strSql.Append("ClassName=@ClassName,");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("Path=@Path,");
			strSql.Append("Depth=@Depth");
			strSql.Append(" where ClassID=@ClassID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassName", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ParentId;
			parameters[2].Value = model.Sequence;
			parameters[3].Value = model.Path;
			parameters[4].Value = model.Depth;
			parameters[5].Value = model.ClassID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows > 0;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CMS_PhotoClass ");
			strSql.Append(" where ClassID=@ClassID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = ClassID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows > 0;
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ClassIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CMS_PhotoClass ");
			strSql.Append(" where ClassID in ("+ClassIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.CMS.PhotoClass GetModel(int ClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ClassID,ClassName,ParentId,Sequence,Path,Depth from CMS_PhotoClass ");
			strSql.Append(" where ClassID=@ClassID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = ClassID;

			Model.CMS.PhotoClass model=new Model.CMS.PhotoClass();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ClassID"]!=null && ds.Tables[0].Rows[0]["ClassID"].ToString()!="")
				{
					model.ClassID=int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ClassName"]!=null && ds.Tables[0].Rows[0]["ClassName"].ToString()!="")
				{
					model.ClassName=ds.Tables[0].Rows[0]["ClassName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ParentId"]!=null && ds.Tables[0].Rows[0]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sequence"]!=null && ds.Tables[0].Rows[0]["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Path"]!=null && ds.Tables[0].Rows[0]["Path"].ToString()!="")
				{
					model.Path=ds.Tables[0].Rows[0]["Path"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Depth"]!=null && ds.Tables[0].Rows[0]["Depth"].ToString()!="")
				{
					model.Depth=int.Parse(ds.Tables[0].Rows[0]["Depth"].ToString());
				}
				return model;
			}
		    return null;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM CMS_PhotoClass ");
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
			strSql.Append(" ClassID,ClassName,ParentId,Sequence,Path,Depth ");
			strSql.Append(" FROM CMS_PhotoClass ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[0].Value = "CMS_PhotoClass";
			parameters[1].Value = "ClassID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

