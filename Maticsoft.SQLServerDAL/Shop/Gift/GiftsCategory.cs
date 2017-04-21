using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Gift;
using Maticsoft.DBUtility;
using System.Collections.Generic;
namespace Maticsoft.SQLServerDAL.Shop.Gift
{
	/// <summary>
	/// 数据访问类:GiftsCategory
	/// </summary>
	public partial class GiftsCategory:IGiftsCategory
	{
		public GiftsCategory()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CategoryID", "Shop_GiftsCategory"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_GiftsCategory");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_GiftsCategory(");
            strSql.Append("ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren)");
            strSql.Append(" values (");
            strSql.Append("@ParentCategoryId,@Name,@Depth,@Path,@DisplaySequence,@Description,@Theme,@HasChildren)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Theme", SqlDbType.NVarChar,200),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1)};
            parameters[0].Value = model.ParentCategoryId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Depth;
            parameters[3].Value = model.Path;
            parameters[4].Value = model.DisplaySequence;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.Theme;
            parameters[7].Value = model.HasChildren;

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
		public bool Update(Maticsoft.Model.Shop.Gift.GiftsCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_GiftsCategory set ");
			strSql.Append("ParentCategoryId=@ParentCategoryId,");
			strSql.Append("Name=@Name,");
			strSql.Append("Depth=@Depth,");
			strSql.Append("Path=@Path,");
			strSql.Append("DisplaySequence=@DisplaySequence,");
			strSql.Append("Description=@Description,");
			strSql.Append("Theme=@Theme,");
			strSql.Append("HasChildren=@HasChildren");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Theme", SqlDbType.NVarChar,200),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
			parameters[0].Value = model.ParentCategoryId;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Depth;
			parameters[3].Value = model.Path;
			parameters[4].Value = model.DisplaySequence;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.Theme;
			parameters[7].Value = model.HasChildren;
			parameters[8].Value = model.CategoryID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int CategoryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_GiftsCategory ");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string CategoryIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_GiftsCategory ");
			strSql.Append(" where CategoryID in ("+CategoryIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.Shop.Gift.GiftsCategory GetModel(int CategoryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren from Shop_GiftsCategory ");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

			Maticsoft.Model.Shop.Gift.GiftsCategory model=new Maticsoft.Model.Shop.Gift.GiftsCategory();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CategoryID"]!=null && ds.Tables[0].Rows[0]["CategoryID"].ToString()!="")
				{
					model.CategoryID=int.Parse(ds.Tables[0].Rows[0]["CategoryID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentCategoryId"]!=null && ds.Tables[0].Rows[0]["ParentCategoryId"].ToString()!="")
				{
					model.ParentCategoryId=int.Parse(ds.Tables[0].Rows[0]["ParentCategoryId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Depth"]!=null && ds.Tables[0].Rows[0]["Depth"].ToString()!="")
				{
					model.Depth=int.Parse(ds.Tables[0].Rows[0]["Depth"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Path"]!=null && ds.Tables[0].Rows[0]["Path"].ToString()!="")
				{
					model.Path=ds.Tables[0].Rows[0]["Path"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DisplaySequence"]!=null && ds.Tables[0].Rows[0]["DisplaySequence"].ToString()!="")
				{
					model.DisplaySequence=int.Parse(ds.Tables[0].Rows[0]["DisplaySequence"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Description"]!=null && ds.Tables[0].Rows[0]["Description"].ToString()!="")
				{
					model.Description=ds.Tables[0].Rows[0]["Description"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Theme"]!=null && ds.Tables[0].Rows[0]["Theme"].ToString()!="")
				{
					model.Theme=ds.Tables[0].Rows[0]["Theme"].ToString();
				}
				if(ds.Tables[0].Rows[0]["HasChildren"]!=null && ds.Tables[0].Rows[0]["HasChildren"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["HasChildren"].ToString()=="1")||(ds.Tables[0].Rows[0]["HasChildren"].ToString().ToLower()=="true"))
					{
						model.HasChildren=true;
					}
					else
					{
						model.HasChildren=false;
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren ");
			strSql.Append(" FROM Shop_GiftsCategory ");
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
			strSql.Append(" CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren ");
			strSql.Append(" FROM Shop_GiftsCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Shop_GiftsCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrWhiteSpace(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.CategoryID desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_GiftsCategory T ");
			if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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
			parameters[0].Value = "Shop_GiftsCategory";
			parameters[1].Value = "CategoryID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region 扩展方法
        public bool UpdatePathAndDepth(int id, int parentid)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
             CommandInfo cmd =new CommandInfo();
            if (parentid == 0)
            {
                //更新自己
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Shop_GiftsCategory set ");
                strSql.Append("Depth=@Depth,");
                strSql.Append("Path=@Path,");
                strSql.Append("HasChildren='false'");
                strSql.Append(" where CategoryID=@CategoryID");
                SqlParameter[] parameters = {
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
                parameters[0].Value = 1;
                parameters[1].Value = id;
                parameters[2].Value = id;
                cmd = new CommandInfo(strSql.ToString(), parameters);
                sqllist.Add(cmd);
            }
            else
            {
                //更新自己
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Shop_GiftsCategory set ");
                strSql.Append("Depth=(select Shop_GiftsCategory.depth from Shop_GiftsCategory where CategoryID=@ParentCategoryID)+1,");
                strSql.Append("Path=(select Shop_GiftsCategory.Path from Shop_GiftsCategory where CategoryID=@ParentCategoryID)+@Path,");
                strSql.Append("HasChildren='true'");
                strSql.Append(" where CategoryID=@CategoryID");
                SqlParameter[] parameters = {
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentCategoryID", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
                parameters[0].Value = "|"+id;
                parameters[1].Value = parentid;
                parameters[2].Value = id;
                cmd = new CommandInfo(strSql.ToString(), parameters);
                sqllist.Add(cmd);


            }
            //更新子类
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("UPDATE Shop_GiftsCategory set");
            strSql2.Append(" Depth=(select Shop_GiftsCategory.depth from Shop_GiftsCategory where CategoryID=@CategoryID)+1,");
            strSql2.Append(" Path=(select Shop_GiftsCategory.Path from Shop_GiftsCategory where CategoryID=@CategoryID)+@Path ");
            strSql2.Append("where ParentCategoryId=@CategoryID");
            SqlParameter[] parameters2 = {
					new SqlParameter("@Path", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentCategoryID", SqlDbType.Int,4),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
            parameters2[0].Value = "|" + id;
            parameters2[1].Value = parentid;
            parameters2[2].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            int categoryid = Add(model);
            if (categoryid > 0)
            {
                return UpdatePathAndDepth(categoryid, model.ParentCategoryId.Value);
            }
            return false;
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            if (Update(model))
            {
                return UpdatePathAndDepth(model.CategoryID, model.ParentCategoryId.Value);
            }
            return false;
        }

        /// <summary>
        /// 获得数据列表(是否排序)
        /// </summary>
        public DataSet GetCategoryList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren ");
            strSql.Append(" FROM Shop_GiftsCategory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + " ORDER BY path");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public bool DeleteCategory(int CategoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_GiftsCategory ");
            strSql.Append(" where path like (select Shop_GiftsCategory.Path from Shop_GiftsCategory where CategoryID=@CategoryID)+'%'");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryID;
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
        /// 对分类进行排序
        /// </summary>
        public bool SwapSequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update Shop_GiftsCategory set ");
            //if ((int)zIndex == 1)
            //{
            //    strSql.Append("DisplaySequence=DisplaySequence-1");
            //}
            //if ((int)zIndex == 0)
            //{
            //    strSql.Append("DisplaySequence=DisplaySequence+1");
            //}
            //strSql.Append(" where CategoryID=@CategoryID");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@CategoryID", SqlDbType.Int,4)
            //                            };
            //parameters[0].Value = CategoryId;
            //int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            //if (rows > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}

