/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Categories.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:23
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.Common;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:CategoryInfo
    /// </summary>
    public partial class CategoryInfo : ICategoryInfo
    {
        public CategoryInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("CategoryId", "Shop_Categories");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CategoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Categories");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Categories(");
            strSql.Append("DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle)");
            strSql.Append(" values (");
            strSql.Append("@DisplaySequence,@Name,@Meta_Title,@Meta_Description,@Meta_Keywords,@Description,@ParentCategoryId,@Depth,@Path,@RewriteName,@SKUPrefix,@AssociatedProductType,@ImageUrl,@Notes1,@Notes2,@Notes3,@Notes4,@Notes5,@Theme,@HasChildren,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,4000),
					new SqlParameter("@RewriteName", SqlDbType.NVarChar,50),
					new SqlParameter("@SKUPrefix", SqlDbType.NVarChar,10),
					new SqlParameter("@AssociatedProductType", SqlDbType.Int,4),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Notes1", SqlDbType.NText),
					new SqlParameter("@Notes2", SqlDbType.NText),
					new SqlParameter("@Notes3", SqlDbType.NText),
					new SqlParameter("@Notes4", SqlDbType.NText),
					new SqlParameter("@Notes5", SqlDbType.NText),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.DisplaySequence;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Meta_Title;
            parameters[3].Value = model.Meta_Description;
            parameters[4].Value = model.Meta_Keywords;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.ParentCategoryId;
            parameters[7].Value = model.Depth;
            parameters[8].Value = model.Path;
            parameters[9].Value = model.RewriteName;
            parameters[10].Value = model.SKUPrefix;
            parameters[11].Value = model.AssociatedProductType;
            parameters[12].Value = model.ImageUrl;
            parameters[13].Value = model.Notes1;
            parameters[14].Value = model.Notes2;
            parameters[15].Value = model.Notes3;
            parameters[16].Value = model.Notes4;
            parameters[17].Value = model.Notes5;
            parameters[18].Value = model.Theme;
            parameters[19].Value = model.HasChildren;
            parameters[20].Value = model.SeoUrl;
            parameters[21].Value = model.SeoImageAlt;
            parameters[22].Value = model.SeoImageTitle;

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
        public bool Update(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Categories set ");
            strSql.Append("DisplaySequence=@DisplaySequence,");
            strSql.Append("Name=@Name,");
            strSql.Append("Meta_Title=@Meta_Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords,");
            strSql.Append("Description=@Description,");
            strSql.Append("ParentCategoryId=@ParentCategoryId,");
            strSql.Append("Depth=@Depth,");
            strSql.Append("Path=@Path,");
            strSql.Append("RewriteName=@RewriteName,");
            strSql.Append("SKUPrefix=@SKUPrefix,");
            strSql.Append("AssociatedProductType=@AssociatedProductType,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("Notes1=@Notes1,");
            strSql.Append("Notes2=@Notes2,");
            strSql.Append("Notes3=@Notes3,");
            strSql.Append("Notes4=@Notes4,");
            strSql.Append("Notes5=@Notes5,");
            strSql.Append("Theme=@Theme,");
            strSql.Append("HasChildren=@HasChildren,");
            strSql.Append("SeoUrl=@SeoUrl,");
            strSql.Append("SeoImageAlt=@SeoImageAlt,");
            strSql.Append("SeoImageTitle=@SeoImageTitle");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,4000),
					new SqlParameter("@RewriteName", SqlDbType.NVarChar,50),
					new SqlParameter("@SKUPrefix", SqlDbType.NVarChar,10),
					new SqlParameter("@AssociatedProductType", SqlDbType.Int,4),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Notes1", SqlDbType.NText),
					new SqlParameter("@Notes2", SqlDbType.NText),
					new SqlParameter("@Notes3", SqlDbType.NText),
					new SqlParameter("@Notes4", SqlDbType.NText),
					new SqlParameter("@Notes5", SqlDbType.NText),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
            parameters[0].Value = model.DisplaySequence;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Meta_Title;
            parameters[3].Value = model.Meta_Description;
            parameters[4].Value = model.Meta_Keywords;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.ParentCategoryId;
            parameters[7].Value = model.Depth;
            parameters[8].Value = model.Path;
            parameters[9].Value = model.RewriteName;
            parameters[10].Value = model.SKUPrefix;
            parameters[11].Value = model.AssociatedProductType;
            parameters[12].Value = model.ImageUrl;
            parameters[13].Value = model.Notes1;
            parameters[14].Value = model.Notes2;
            parameters[15].Value = model.Notes3;
            parameters[16].Value = model.Notes4;
            parameters[17].Value = model.Notes5;
            parameters[18].Value = model.Theme;
            parameters[19].Value = model.HasChildren;
            parameters[20].Value = model.SeoUrl;
            parameters[21].Value = model.SeoImageAlt;
            parameters[22].Value = model.SeoImageTitle;
            parameters[23].Value = model.CategoryId;

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
        public bool Delete(int CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Categories ");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryId;

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
        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_Categories ");
            strSql.Append(" where CategoryId in (" + CategoryIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Products.CategoryInfo GetModel(int CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CategoryId,DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,ProductCount from Shop_Categories ");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
            parameters[0].Value = CategoryId;

            Maticsoft.Model.Shop.Products.CategoryInfo model = new Maticsoft.Model.Shop.Products.CategoryInfo();
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
        public Maticsoft.Model.Shop.Products.CategoryInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo model = new Maticsoft.Model.Shop.Products.CategoryInfo();
            if (row != null)
            {
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["DisplaySequence"] != null && row["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
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
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["ParentCategoryId"] != null && row["ParentCategoryId"].ToString() != "")
                {
                    model.ParentCategoryId = int.Parse(row["ParentCategoryId"].ToString());
                }
                if (row["Depth"] != null && row["Depth"].ToString() != "")
                {
                    model.Depth = int.Parse(row["Depth"].ToString());
                }
                if (row["Path"] != null)
                {
                    model.Path = row["Path"].ToString();
                }
                if (row["RewriteName"] != null)
                {
                    model.RewriteName = row["RewriteName"].ToString();
                }
                if (row["SKUPrefix"] != null)
                {
                    model.SKUPrefix = row["SKUPrefix"].ToString();
                }
                if (row["AssociatedProductType"] != null && row["AssociatedProductType"].ToString() != "")
                {
                    model.AssociatedProductType = int.Parse(row["AssociatedProductType"].ToString());
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["Notes1"] != null)
                {
                    model.Notes1 = row["Notes1"].ToString();
                }
                if (row["Notes2"] != null)
                {
                    model.Notes2 = row["Notes2"].ToString();
                }
                if (row["Notes3"] != null)
                {
                    model.Notes3 = row["Notes3"].ToString();
                }
                if (row["Notes4"] != null)
                {
                    model.Notes4 = row["Notes4"].ToString();
                }
                if (row["Notes5"] != null)
                {
                    model.Notes5 = row["Notes5"].ToString();
                }
                if (row["Theme"] != null)
                {
                    model.Theme = row["Theme"].ToString();
                }
                if (row["HasChildren"] != null && row["HasChildren"].ToString() != "")
                {
                    if ((row["HasChildren"].ToString() == "1") || (row["HasChildren"].ToString().ToLower() == "true"))
                    {
                        model.HasChildren = true;
                    }
                    else
                    {
                        model.HasChildren = false;
                    }
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
                model.ProductCount=0;
                if (row["ProductCount"] != null && row["ProductCount"].ToString() != "")
                {
                    model.ProductCount = int.Parse(row["ProductCount"].ToString());
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
            strSql.Append("select CategoryId,DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,ProductCount ");
            strSql.Append(" FROM Shop_Categories ");
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
            strSql.Append(" CategoryId,DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,ProductCount ");
            strSql.Append(" FROM Shop_Categories ");
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
            strSql.Append("select count(1) FROM Shop_Categories ");
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
                strSql.Append("order by T.CategoryId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Categories T ");
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
            parameters[0].Value = "Shop_Categories";
            parameters[1].Value = "CategoryId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod


   

        private CommandInfo GenerateCategory(Model.Shop.Products.CategoryInfo model)
        {

            #region 向相应的商品分类表中插入数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Categories(");
            strSql.Append("DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle)");
            strSql.Append(" values (");
            strSql.Append("@DisplaySequence,@Name,@Meta_Title,@Meta_Description,@Meta_Keywords,@Description,@ParentCategoryId,@Depth,@Path,@RewriteName,@SKUPrefix,@AssociatedProductType,@ImageUrl,@Notes1,@Notes2,@Notes3,@Notes4,@Notes5,@Theme,@HasChildren,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,4000),
					new SqlParameter("@RewriteName", SqlDbType.NVarChar,50),
					new SqlParameter("@SKUPrefix", SqlDbType.NVarChar,10),
					new SqlParameter("@AssociatedProductType", SqlDbType.Int,4),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Notes1", SqlDbType.NText),
					new SqlParameter("@Notes2", SqlDbType.NText),
					new SqlParameter("@Notes3", SqlDbType.NText),
					new SqlParameter("@Notes4", SqlDbType.NText),
					new SqlParameter("@Notes5", SqlDbType.NText),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.DisplaySequence;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Meta_Title;
            parameters[3].Value = model.Meta_Description;
            parameters[4].Value = model.Meta_Keywords;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.ParentCategoryId;
            parameters[7].Value = model.Depth;
            parameters[8].Value = "";
            parameters[9].Value = model.RewriteName;
            parameters[10].Value = model.SKUPrefix;
            parameters[11].Value = model.AssociatedProductType;
            parameters[12].Value = model.ImageUrl;
            parameters[13].Value = model.Notes1;
            parameters[14].Value = model.Notes2;
            parameters[15].Value = model.Notes3;
            parameters[16].Value = model.Notes4;
            parameters[17].Value = model.Notes5;
            parameters[18].Value = model.Theme;
            parameters[19].Value = model.HasChildren;
            parameters[20].Value = model.SeoUrl;
            parameters[21].Value = model.SeoImageAlt;
            parameters[22].Value = model.SeoImageTitle;

            return new CommandInfo(strSql.ToString(),
                           parameters, EffentNextType.ExcuteEffectRows);
            #endregion
        }

        public bool UpdatePath(Model.Shop.Products.CategoryInfo model)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_Categories set Path=@Path WHERE CategoryId=@CategoryId ");
            SqlParameter[] parameters4 =
                {
                    new SqlParameter("@Path", SqlDbType.NVarChar,200),
                    new SqlParameter("@CategoryId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = model.Path;
            parameters4[1].Value = model.CategoryId;
            int rows = DbHelperSQL.ExecuteSql(strSql4.ToString(), parameters4);
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, bool IsOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT CategoryId,Name,DisplaySequence,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,Meta_Title,ImageUrl,ProductCount ");
            strSql.Append(" FROM Shop_Categories ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (IsOrder)
            {
                strSql.Append(" ORDER BY  path ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 对分类进行排序
        /// </summary>
        public bool SwapCategorySequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex)
        {
            int AffectedRows = 0;
            SqlParameter[] parameter = { 
                                       new SqlParameter("@CategoryId",SqlDbType.Int),
                                       new SqlParameter("@ZIndex",SqlDbType.Int)
                                       };
            parameter[0].Value = CategoryId;
            parameter[1].Value = (int)zIndex;

            DbHelperSQL.RunProcedure("sp_Shop_Category_SwapSequence", parameter, out AffectedRows);
            return AffectedRows > 0;
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public DataSet DeleteCategory(int categoryId, out int Result)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4)};
            parameters[0].Value = categoryId;
            DataSet ds = DbHelperSQL.RunProcedure("sp_Shop_Category_Delete", parameters, "tb", out Result);
            if (Result == 1)
            {
                return ds;
            }
            return null;
        }

        /// <summary>
        /// 更新分类信息
        /// </summary>
        public bool UpdateCategory(Model.Shop.Products.CategoryInfo model)
        {
            int AffectedRows = 0;
            SqlParameter[] parameter = { 
                                       new SqlParameter("@Name",SqlDbType.NVarChar),
                                       new SqlParameter("@MetaDescription",SqlDbType.NVarChar),
                                       new SqlParameter("@MetaKeywords",SqlDbType.NVarChar),
                                       new SqlParameter("@Description",SqlDbType.NVarChar),
                                       new SqlParameter("@RewriteName",SqlDbType.NVarChar),
                                       new SqlParameter("@SKUPrefix",SqlDbType.NVarChar),
                                       new SqlParameter("@AssociatedProductType",SqlDbType.Int),
                                       new SqlParameter("@Meta_Title",SqlDbType.NVarChar),
                                       new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
                                       new SqlParameter("@CategoryId",SqlDbType.Int)
                                       };
            parameter[0].Value = model.Name;
            parameter[1].Value = model.Meta_Description;
            parameter[2].Value = model.Meta_Keywords;
            parameter[3].Value = model.Description;
            parameter[4].Value = model.RewriteName;
            parameter[5].Value = model.SKUPrefix;
            parameter[6].Value = model.AssociatedProductType;
            parameter[7].Value = model.Meta_Title;
            parameter[8].Value = model.ImageUrl;
            parameter[9].Value = model.CategoryId;

            DbHelperSQL.RunProcedure("sp_cc_Category_Update", parameter, out AffectedRows);
            return AffectedRows > 0;
        }

        /// <summary>
        /// 判断分类下是否存在商品
        /// </summary>
        public bool IsExistedProduce(int category)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM Shop_Products ");
            strSql.Append(" WHERE CategoryId = @CategoryId");
            SqlParameter[] parameter = {
                                       new SqlParameter("@CategoryId",SqlDbType.Int)
                                       };
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameter);

            if (obj != null)
            {
                return Convert.ToInt32(obj) > 0;
            }
            return false;
        }

        /// <summary>
        /// 转移商品
        /// </summary>
        public bool DisplaceCategory(int FromCategoryId, int ToCategoryId)
        {
            int AffectedRows = 0;
            SqlParameter[] parameter = { 
                                       new SqlParameter("@FromCategoryId",SqlDbType.Int),
                                       new SqlParameter("@ToCategory",SqlDbType.Int)
                                       };
            parameter[0].Value = FromCategoryId;
            parameter[1].Value = ToCategoryId;
            DbHelperSQL.RunProcedure("sp_Shop_DisplaceCategory", parameter, out AffectedRows);
            return AffectedRows > 0;
        }


        public string GetNamePathByPath(string path)
        {
            path = path.Replace("|", ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Shop_Categories ");
            strSql.Append("WHERE CategoryId in (" + path + ")");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            string Name = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    if (i == 0)
                        Name = dr["Name"].ToString();
                    else
                        Name = Name + "/" + dr["Name"].ToString();
                    //if (!(i == 0 && (RegionName == "北京" || RegionName == "上海" || RegionName == "天津" || RegionName == "重庆")))
                    //{
                    //    strReg.Append(RegionName);
                    //}


                }
            }
            return Name;
        }

        public DataSet GetCategoryListByPath(string path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT C.* FROM F_SplitToInt('{0}','|') I ", path);
            strSql.Append("LEFT JOIN Shop_Categories C ON I.UnitInt=C.CategoryId ");
            strSql.Append("ORDER BY C.Depth ASC ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetNameByPid(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Name ");
            strSql.Append(" FROM Shop_Categories sp LEFT JOIN Shop_ProductCategories  spc ON sp.CategoryId=spc.CategoryId ");
            strSql.Append(" WHERE spc.ProductId=@ProductId ");
            SqlParameter[] parameter = {
                                       new SqlParameter("@ProductId",SqlDbType.BigInt,8)
                                       };
            parameter[0].Value = productId;
            return DbHelperSQL.Query(strSql.ToString(), parameter);
        }

        public int GetMaxSeqByCid(int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT MAX(DisplaySequence) FROM Shop_Categories WHERE ParentCategoryId=@ParentCategoryId");
            SqlParameter[] parameter = {
                                       new SqlParameter("@ParentCategoryId",SqlDbType.Int,4)
                                       };
            parameter[0].Value = parentId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameter);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int GetDepthByCid(int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT Depth FROM Shop_Categories WHERE CategoryId=@ParentCategoryId");
            SqlParameter[] parameter = {
                                       new SqlParameter("@ParentCategoryId",SqlDbType.Int,4)
                                       };
            parameter[0].Value = parentId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameter);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public bool UpdateSeqByCid(int Seq,int Cid)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_Categories set DisplaySequence=@DisplaySequence WHERE CategoryId=@CategoryId ");
            SqlParameter[] parameters4 =
                {
                    new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
                    new SqlParameter("@CategoryId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = Seq;
            parameters4[1].Value = Cid;
            int rows = DbHelperSQL.ExecuteSql(strSql4.ToString(), parameters4);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UpdateDepthAndPath(int Cid, int Depth, string Path)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_Categories set Path=@Path, Depth=@Depth WHERE CategoryId=@CategoryId ");
            SqlParameter[] parameters4 =
                {
                    new SqlParameter("@Path", SqlDbType.NVarChar,200),
                    new SqlParameter("@Depth", SqlDbType.Int, 4),
                     new SqlParameter("@CategoryId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = Path;
            parameters4[1].Value = Depth;
            parameters4[2].Value = Cid;
            int rows = DbHelperSQL.ExecuteSql(strSql4.ToString(), parameters4);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UpdateHasChild(int cid,int hasChild=1)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_Categories set HasChildren=" + hasChild + " WHERE CategoryId=@CategoryId ");
            SqlParameter[] parameters4 =
                {
                     new SqlParameter("@CategoryId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = cid;
            int rows = DbHelperSQL.ExecuteSql(strSql4.ToString(), parameters4);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool IsExisted(int parentId, string name, int categoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Categories");
            strSql.Append(" where ParentCategoryId=@ParentCategoryId and Name=@Name");
            if (categoryId > 0)
            {
                strSql.Append("  and CategoryId<>@CategoryId");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = parentId;
            parameters[1].Value = categoryId;
            parameters[2].Value = name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public DataSet GetGroupCate()
        {
            string strSql =
                @"  select  *  from Shop_Categories s 
   where exists(
   select p.CategoryId from [Shop_GroupBuy] g,[Shop_ProductCategories] p 
    where p.ProductId=g.ProductId and s.categoryId=p.categoryId)";
            return DbHelperSQL.Query(strSql);
        }

    }
}

