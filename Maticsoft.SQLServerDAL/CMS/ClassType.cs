using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.IDAL.CMS;
using Maticsoft.DBUtility;

namespace Maticsoft.SQLServerDAL.CMS
{
    /// <summary>
    /// 数据访问类:ClassType
    /// </summary>
    public partial class ClassType : IClassType
    {
        public ClassType()
        { }

        #region  Method

        #region 得到最大ID
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ClassTypeID", "CMS_ClassType");
        } 
        #endregion

        #region 是否存在该记录
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ClassTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_ClassType");
            strSql.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassTypeID", SqlDbType.Int,4)};
            parameters[0].Value = ClassTypeID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        } 
        #endregion

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.CMS.ClassType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_ClassType(");
            strSql.Append("ClassTypeName)");
            strSql.Append(" values (");
            strSql.Append("@ClassTypeName)");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassTypeName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ClassTypeName;

            if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                return true;
            return false;
        } 
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.CMS.ClassType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_ClassType set ");
            strSql.Append("ClassTypeName=@ClassTypeName");
            strSql.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassTypeID", SqlDbType.Int,4)};
            parameters[0].Value = model.ClassTypeName;
            parameters[1].Value = model.ClassTypeID;

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
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ClassTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_ClassType ");
            strSql.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassTypeID", SqlDbType.Int,4)};
            parameters[0].Value = ClassTypeID;

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
        #endregion

        #region 批量删除数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string ClassTypeIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_ClassType ");
            strSql.Append(" where ClassTypeID in (" + ClassTypeIDlist + ")  ");
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
        #endregion

        #region  得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.ClassType GetModel(int ClassTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ClassTypeID,ClassTypeName from CMS_ClassType ");
            strSql.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassTypeID", SqlDbType.Int,4)};
            parameters[0].Value = ClassTypeID;

            Maticsoft.Model.CMS.ClassType model = new Maticsoft.Model.CMS.ClassType();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ClassTypeID"].ToString() != "")
                {
                    model.ClassTypeID = int.Parse(ds.Tables[0].Rows[0]["ClassTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassTypeName"] != null)
                {
                    model.ClassTypeName = ds.Tables[0].Rows[0]["ClassTypeName"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        } 
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ClassTypeID,ClassTypeName ");
            strSql.Append(" FROM CMS_ClassType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        } 
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.ClassType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.ClassType> list = new List<Maticsoft.Model.CMS.ClassType>();
            if (DataTableTools.DataTableIsNull(dt))
            {
                return null;
            }
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.CMS.ClassType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.CMS.ClassType();
                    if (dt.Rows[n]["ClassTypeID"].ToString() != "")
                    {
                        model.ClassTypeID = int.Parse(dt.Rows[n]["ClassTypeID"].ToString());
                    }
                    model.ClassTypeName = dt.Rows[n]["ClassTypeName"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region 获得前几行数据
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
            strSql.Append(" ClassTypeID,ClassTypeName ");
            strSql.Append(" FROM CMS_ClassType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append(" order by DESC ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        } 
        #endregion

        #region 分页获取数据列表
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
            parameters[0].Value = "CMS_ClassType";
            parameters[1].Value = "ClassTypeID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/
        
        #endregion

        #endregion  Method
    }
}

