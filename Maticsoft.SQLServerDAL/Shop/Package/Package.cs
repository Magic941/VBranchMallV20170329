using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Package;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Package
{
	/// <summary>
	/// 数据访问类:Package
	/// </summary>
	public partial class Package:IPackage
	{
		public Package()
		{}
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PackageId", "Shop_Package");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PackageId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Package");
            strSql.Append(" where PackageId=@PackageId");
            SqlParameter[] parameters = {
					new SqlParameter("@PackageId", SqlDbType.Int,4)
			};
            parameters[0].Value = PackageId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Package.Package model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Package(");
            strSql.Append("CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark)");
            strSql.Append(" values (");
            strSql.Append("@CategoryId,@Name,@Description,@PhotoUrl,@NormalPhotoUrl,@ThumbPhotoUrl,@CreatedDate,@Status,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@NormalPhotoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ThumbPhotoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.PhotoUrl;
            parameters[4].Value = model.NormalPhotoUrl;
            parameters[5].Value = model.ThumbPhotoUrl;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.Shop.Package.Package model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Package set ");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("Name=@Name,");
            strSql.Append("Description=@Description,");
            strSql.Append("PhotoUrl=@PhotoUrl,");
            strSql.Append("NormalPhotoUrl=@NormalPhotoUrl,");
            strSql.Append("ThumbPhotoUrl=@ThumbPhotoUrl,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where PackageId=@PackageId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@NormalPhotoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@ThumbPhotoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@PackageId", SqlDbType.Int,4)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.PhotoUrl;
            parameters[4].Value = model.NormalPhotoUrl;
            parameters[5].Value = model.ThumbPhotoUrl;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.PackageId;

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
        public bool Delete(int PackageId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Package ");
            strSql.Append(" where PackageId=@PackageId");
            SqlParameter[] parameters = {
					new SqlParameter("@PackageId", SqlDbType.Int,4)
			};
            parameters[0].Value = PackageId;

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
        public bool DeleteList(string PackageIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Package ");
            strSql.Append(" where PackageId in (" + PackageIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Package.Package GetModel(int PackageId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PackageId,CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark from Shop_Package ");
            strSql.Append(" where PackageId=@PackageId");
            SqlParameter[] parameters = {
					new SqlParameter("@PackageId", SqlDbType.Int,4)
			};
            parameters[0].Value = PackageId;

            Maticsoft.Model.Shop.Package.Package model = new Maticsoft.Model.Shop.Package.Package();
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
        public Maticsoft.Model.Shop.Package.Package DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Package.Package model = new Maticsoft.Model.Shop.Package.Package();
            if (row != null)
            {
                if (row["PackageId"] != null && row["PackageId"].ToString() != "")
                {
                    model.PackageId = int.Parse(row["PackageId"].ToString());
                }
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["Name"] != null && row["Name"].ToString() != "")
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Description"] != null && row["Description"].ToString() != "")
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["PhotoUrl"] != null && row["PhotoUrl"].ToString() != "")
                {
                    model.PhotoUrl = row["PhotoUrl"].ToString();
                }
                if (row["NormalPhotoUrl"] != null && row["NormalPhotoUrl"].ToString() != "")
                {
                    model.NormalPhotoUrl = row["NormalPhotoUrl"].ToString();
                }
                if (row["ThumbPhotoUrl"] != null && row["ThumbPhotoUrl"].ToString() != "")
                {
                    model.ThumbPhotoUrl = row["ThumbPhotoUrl"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Remark"] != null && row["Remark"].ToString() != "")
                {
                    model.Remark = row["Remark"].ToString();
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
            strSql.Append("select PackageId,CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark ");
            strSql.Append(" FROM Shop_Package ");
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
            strSql.Append(" PackageId,CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark ");
            strSql.Append(" FROM Shop_Package ");
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
            strSql.Append("select count(1) FROM Shop_Package ");
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
                strSql.Append("order by T.PackageId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Package T ");
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
            parameters[0].Value = "Shop_Package";
            parameters[1].Value = "PackageId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region ExMethod

        public DataSet GetListEx(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p1.PackageId,p2.NAME CategoryName,p1.NAME PackageName,p1.description as description,p1.PhotoUrl,p1.CreatedDate,p1.Remark from Shop_Package p1 left join Shop_PackageCategory p2 on p1.CategoryId=p2.CategoryId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());

        } 
        #endregion
	}
}

