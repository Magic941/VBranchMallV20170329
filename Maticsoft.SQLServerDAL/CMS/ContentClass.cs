using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Maticsoft.IDAL.CMS;
using Maticsoft.DBUtility;
using Maticsoft.Common;
namespace Maticsoft.SQLServerDAL.CMS
{
    /// <summary>
    /// 栏目类型数据访问类
    /// </summary>
    public partial class ContentClass : IContentClass
    {
        public ContentClass()
        { }

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ClassID", "CMS_ContentClass");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_ContentClass");
            strSql.Append(" where ClassID=@ClassID");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)
			};
            parameters[0].Value = ClassID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.CMS.ContentClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_ContentClass(");
            strSql.Append("ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar)");
            strSql.Append(" values (");
            strSql.Append("@ClassName,@ClassIndex,@Sequence,@ParentId,@State,@AllowSubclass,@AllowAddContent,@ImageUrl,@Description,@Keywords,@ClassTypeID,@ClassModel,@PageModelName,@CreatedDate,@CreatedUserID,@Path,@Depth,@Remark,@Meta_Title,@Meta_Description,@Meta_Keywords,@SeoUrl,@SeoImageAlt,@SeoImageTitle,@IndexChar)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassName", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassIndex", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@AllowSubclass", SqlDbType.Bit,1),
					new SqlParameter("@AllowAddContent", SqlDbType.Bit,1),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassTypeID", SqlDbType.Int,4),
					new SqlParameter("@ClassModel", SqlDbType.SmallInt,2),
					new SqlParameter("@PageModelName", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,1000),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@IndexChar", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.ClassName;
            parameters[1].Value = model.ClassIndex;
            parameters[2].Value = model.Sequence;
            parameters[3].Value = model.ParentId;
            parameters[4].Value = model.State;
            parameters[5].Value = model.AllowSubclass;
            parameters[6].Value = model.AllowAddContent;
            parameters[7].Value = model.ImageUrl;
            parameters[8].Value = model.Description;
            parameters[9].Value = model.Keywords;
            parameters[10].Value = model.ClassTypeID;
            parameters[11].Value = model.ClassModel;
            parameters[12].Value = model.PageModelName;
            parameters[13].Value = model.CreatedDate;
            parameters[14].Value = model.CreatedUserID;
            parameters[15].Value = model.Path;
            parameters[16].Value = model.Depth;
            parameters[17].Value = model.Remark;
            parameters[18].Value = model.Meta_Title;
            parameters[19].Value = model.Meta_Description;
            parameters[20].Value = model.Meta_Keywords;
            parameters[21].Value = model.SeoUrl;
            parameters[22].Value = model.SeoImageAlt;
            parameters[23].Value = model.SeoImageTitle;
            parameters[24].Value = model.IndexChar;

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
        public bool Update(Maticsoft.Model.CMS.ContentClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_ContentClass set ");
            strSql.Append("ClassName=@ClassName,");
            strSql.Append("ClassIndex=@ClassIndex,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("State=@State,");
            strSql.Append("AllowSubclass=@AllowSubclass,");
            strSql.Append("AllowAddContent=@AllowAddContent,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("Description=@Description,");
            strSql.Append("Keywords=@Keywords,");
            strSql.Append("ClassTypeID=@ClassTypeID,");
            strSql.Append("ClassModel=@ClassModel,");
            strSql.Append("PageModelName=@PageModelName,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("Path=@Path,");
            strSql.Append("Depth=@Depth,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Meta_Title=@Meta_Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords,");
            strSql.Append("SeoUrl=@SeoUrl,");
            strSql.Append("SeoImageAlt=@SeoImageAlt,");
            strSql.Append("SeoImageTitle=@SeoImageTitle,");
            strSql.Append("IndexChar=@IndexChar");
            strSql.Append(" where ClassID=@ClassID");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassName", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassIndex", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@AllowSubclass", SqlDbType.Bit,1),
					new SqlParameter("@AllowAddContent", SqlDbType.Bit,1),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassTypeID", SqlDbType.Int,4),
					new SqlParameter("@ClassModel", SqlDbType.SmallInt,2),
					new SqlParameter("@PageModelName", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,1000),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@IndexChar", SqlDbType.NVarChar,200),
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
            parameters[0].Value = model.ClassName;
            parameters[1].Value = model.ClassIndex;
            parameters[2].Value = model.Sequence;
            parameters[3].Value = model.ParentId;
            parameters[4].Value = model.State;
            parameters[5].Value = model.AllowSubclass;
            parameters[6].Value = model.AllowAddContent;
            parameters[7].Value = model.ImageUrl;
            parameters[8].Value = model.Description;
            parameters[9].Value = model.Keywords;
            parameters[10].Value = model.ClassTypeID;
            parameters[11].Value = model.ClassModel;
            parameters[12].Value = model.PageModelName;
            parameters[13].Value = model.CreatedDate;
            parameters[14].Value = model.CreatedUserID;
            parameters[15].Value = model.Path;
            parameters[16].Value = model.Depth;
            parameters[17].Value = model.Remark;
            parameters[18].Value = model.Meta_Title;
            parameters[19].Value = model.Meta_Description;
            parameters[20].Value = model.Meta_Keywords;
            parameters[21].Value = model.SeoUrl;
            parameters[22].Value = model.SeoImageAlt;
            parameters[23].Value = model.SeoImageTitle;
            parameters[24].Value = model.IndexChar;
            parameters[25].Value = model.ClassID;

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
        public bool Delete(int ClassID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_ContentClass ");
            strSql.Append(" where ClassID=@ClassID");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)
			};
            parameters[0].Value = ClassID;

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
        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_ContentClass ");
            strSql.Append(" where ClassID in (" + ClassIDlist + ")  ");
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
        public Maticsoft.Model.CMS.ContentClass GetModel(int ClassID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ClassID,ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar from CMS_ContentClass ");
            strSql.Append(" where ClassID=@ClassID");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)
			};
            parameters[0].Value = ClassID;

            Maticsoft.Model.CMS.ContentClass model = new Maticsoft.Model.CMS.ContentClass();
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
        public Maticsoft.Model.CMS.ContentClass DataRowToModel(DataRow row)
        {
            Maticsoft.Model.CMS.ContentClass model = new Maticsoft.Model.CMS.ContentClass();
            if (row != null)
            {
                if (row["ClassID"] != null && row["ClassID"].ToString() != "")
                {
                    model.ClassID = int.Parse(row["ClassID"].ToString());
                }
                if (row["ClassName"] != null)
                {
                    model.ClassName = row["ClassName"].ToString();
                }
                if (row["ClassIndex"] != null)
                {
                    model.ClassIndex = row["ClassIndex"].ToString();
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["ParentId"] != null && row["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(row["ParentId"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
                }
                if (row["AllowSubclass"] != null && row["AllowSubclass"].ToString() != "")
                {
                    if ((row["AllowSubclass"].ToString() == "1") || (row["AllowSubclass"].ToString().ToLower() == "true"))
                    {
                        model.AllowSubclass = true;
                    }
                    else
                    {
                        model.AllowSubclass = false;
                    }
                }
                if (row["AllowAddContent"] != null && row["AllowAddContent"].ToString() != "")
                {
                    if ((row["AllowAddContent"].ToString() == "1") || (row["AllowAddContent"].ToString().ToLower() == "true"))
                    {
                        model.AllowAddContent = true;
                    }
                    else
                    {
                        model.AllowAddContent = false;
                    }
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["Keywords"] != null)
                {
                    model.Keywords = row["Keywords"].ToString();
                }
                if (row["ClassTypeID"] != null && row["ClassTypeID"].ToString() != "")
                {
                    model.ClassTypeID = int.Parse(row["ClassTypeID"].ToString());
                }
                if (row["ClassModel"] != null && row["ClassModel"].ToString() != "")
                {
                    model.ClassModel = int.Parse(row["ClassModel"].ToString());
                }
                if (row["PageModelName"] != null)
                {
                    model.PageModelName = row["PageModelName"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["Path"] != null)
                {
                    model.Path = row["Path"].ToString();
                }
                if (row["Depth"] != null && row["Depth"].ToString() != "")
                {
                    model.Depth = int.Parse(row["Depth"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["Meta_Title"] != null)
                {
                    model.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    model.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    model.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["SeoUrl"] != null)
                {
                    model.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["SeoImageAlt"] != null)
                {
                    model.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if (row["SeoImageTitle"] != null)
                {
                    model.SeoImageTitle = row["SeoImageTitle"].ToString();
                }
                if (row["IndexChar"] != null)
                {
                    model.IndexChar = row["IndexChar"].ToString();
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
            strSql.Append("select ClassID,ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar ");
            strSql.Append(" FROM CMS_ContentClass ");
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
            strSql.Append(" ClassID,ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar ");
            strSql.Append(" FROM CMS_ContentClass ");
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
            strSql.Append("select count(1) FROM CMS_ContentClass ");
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
                strSql.Append("order by T.ClassID desc");
            }
            strSql.Append(")AS Row, T.*  from CMS_ContentClass T ");
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
            parameters[0].Value = "CMS_ContentClass";
            parameters[1].Value = "ClassID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        #region  Method

        #region 获取树集合
        /// <summary>
        /// 获取树集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetTreeList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM CMS_ContentClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY ParentId,Sequence ");

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
			parameters[0].Value = "CMS_ContentClass";
			parameters[1].Value = "ClassID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion

        

        #region 批量更新数据
        /// <summary>
        /// 批量更新数据
        /// </summary>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE CMS_ContentClass set " + strWhere);
            strSql.Append(" WHERE ClassID IN(" + IDlist + ")  ");
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

        #region 更新类别排序

        /// <summary>
        /// 更新类别排序
        /// </summary>
        /// <param name="VideoClassId">类别ID</param>
        /// <param name="zIndex">排序方式</param>
        /// <returns></returns>
        public int SwapCategorySequence(int ContentClassId, Maticsoft.Common.Video.SwapSequenceIndex zIndex)
        {
            int rows;
            SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int),
					new SqlParameter("@ZIndex", SqlDbType.Int)
					};
            parameters[0].Value = ContentClassId;
            parameters[1].Value = (int)zIndex;
            return DbHelperSQL.RunProcedure("sp_CMS_SwapContentClassSequence", parameters, out rows);
        }
        #endregion

        #region 删除分类信息
        /// <summary>
        /// 删除分类信息
        /// </summary>
        public bool DeleteCategory(int categoryId)
        {
            int AffectedRows = 0;
            SqlParameter[] parameter = { 
                                       new SqlParameter("@ClassID",SqlDbType.Int),
                                       new SqlParameter("@Status",SqlDbType.Int)
                                       };
            parameter[0].Value = categoryId;
            parameter[1].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure("sp_CMS_Category_Delete", parameter, out AffectedRows);
            return (int)(parameter[1].Value) > 0;
        }
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByView(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  FROM View_ContentClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 获得前几行数据
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListByView(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM View_ContentClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" ORDER BY " + filedOrder);
            }
            else
            {
                strSql.Append(" ORDER BY ClassID ASC ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
        public bool AddExt(Maticsoft.Model.CMS.ContentClass model)
        {
            int rows;
            SqlParameter[] parameters = {
					new SqlParameter("@ClassName", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassIndex", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@AllowSubclass", SqlDbType.Bit,1),
					new SqlParameter("@AllowAddContent", SqlDbType.Bit,1),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassTypeID", SqlDbType.Int,4),
					new SqlParameter("@ClassModel", SqlDbType.SmallInt,2),
					new SqlParameter("@PageModelName", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@IndexChar", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.ClassName;
            parameters[1].Value = model.ClassIndex;
            parameters[2].Value = model.Sequence;
            parameters[3].Value = model.ParentId;
            parameters[4].Value = model.State;
            parameters[5].Value = model.AllowSubclass;
            parameters[6].Value = model.AllowAddContent;
            parameters[7].Value = model.ImageUrl;
            parameters[8].Value = model.Description;
            parameters[9].Value = model.Keywords;
            parameters[10].Value = model.ClassTypeID;
            parameters[11].Value = model.ClassModel;
            parameters[12].Value = model.PageModelName;
            parameters[13].Value = model.CreatedUserID;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.Meta_Title;
            parameters[16].Value = model.Meta_Description;
            parameters[17].Value = model.Meta_Keywords;
            parameters[18].Value = model.SeoUrl;
            parameters[19].Value = model.SeoImageAlt;
            parameters[20].Value = model.SeoImageTitle;
            parameters[21].Value = model.IndexChar;
            DbHelperSQL.RunProcedure("sp_CMS_ContentClass_Create", parameters, out rows);
            if (rows > 0)
            {
                return true;
            } return false;
        }

        /// <summary>
        /// 获取NamePath
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetNamePathByPath(string path)
        {
            path = path.Replace("|", ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM CMS_ContentClass ");
            strSql.Append("WHERE ClassID in (" + path + ")");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            string Name = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    if (i == 0)
                        Name = dr["ClassName"].ToString();
                    else
                        Name = Name + "/" + dr["ClassName"].ToString();
                }
            }
            return Name;
        }
      
        #endregion  Method
    }
}

