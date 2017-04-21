/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Brands.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/12 10:02:40
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
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.DBUtility;
using System.Collections.Generic;
namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:BrandInfo
    /// </summary>
    public partial class BrandInfo : IBrandInfo
    {
        public BrandInfo()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("BrandId", "Shop_Brands");
        }

        /// <summary>
        /// 得到最大顺序
        /// </summary>
        public int GetMaxDisplaySequence()
        {
            return DbHelperSQL.GetMaxID("DisplaySequence", "Shop_Brands");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BrandId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_Brands");
            strSql.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] parameters = {
					new SqlParameter("@BrandId", SqlDbType.Int,4)
			};
            parameters[0].Value = BrandId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_Brands(");
            strSql.Append("BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme)");
            strSql.Append(" VALUES (");
            strSql.Append("@BrandName,@BrandSpell,@Meta_Description,@Meta_Keywords,@Logo,@CompanyUrl,@Description,@DisplaySequence,@Theme)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BrandName", SqlDbType.NVarChar,50),
					new SqlParameter("@BrandSpell", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Logo", SqlDbType.NVarChar,255),
					new SqlParameter("@CompanyUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.BrandName;
            parameters[1].Value = model.BrandSpell;
            parameters[2].Value = model.Meta_Description;
            parameters[3].Value = model.Meta_Keywords;
            parameters[4].Value = model.Logo;
            parameters[5].Value = model.CompanyUrl;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.DisplaySequence;
            parameters[8].Value = model.Theme;

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
        public bool Update(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_Brands SET ");
            strSql.Append("BrandName=@BrandName,");
            strSql.Append("BrandSpell=@BrandSpell,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords,");
            strSql.Append("Logo=@Logo,");
            strSql.Append("CompanyUrl=@CompanyUrl,");
            strSql.Append("Description=@Description,");
            strSql.Append("DisplaySequence=@DisplaySequence,");
            strSql.Append("Theme=@Theme");
            strSql.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] parameters = {
					new SqlParameter("@BrandName", SqlDbType.NVarChar,50),
					new SqlParameter("@BrandSpell", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Logo", SqlDbType.NVarChar,255),
					new SqlParameter("@CompanyUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@BrandId", SqlDbType.Int,4)};
            parameters[0].Value = model.BrandName;
            parameters[1].Value = model.BrandSpell;
            parameters[2].Value = model.Meta_Description;
            parameters[3].Value = model.Meta_Keywords;
            parameters[4].Value = model.Logo;
            parameters[5].Value = model.CompanyUrl;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.DisplaySequence;
            parameters[8].Value = model.Theme;
            parameters[9].Value = model.BrandId;

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
        public bool Delete(int BrandId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_Brands ");
            strSql.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] parameters = {
					new SqlParameter("@BrandId", SqlDbType.Int,4)
			};
            parameters[0].Value = BrandId;

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
        public bool DeleteList(string BrandIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_Brands ");
            strSql.Append(" WHERE BrandId in (" + BrandIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Products.BrandInfo GetModel(int BrandId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 BrandId,BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme FROM Shop_Brands ");
            strSql.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] parameters = {
					new SqlParameter("@BrandId", SqlDbType.Int,4)
			};
            parameters[0].Value = BrandId;

            Maticsoft.Model.Shop.Products.BrandInfo model = new Maticsoft.Model.Shop.Products.BrandInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["BrandId"] != null && ds.Tables[0].Rows[0]["BrandId"].ToString() != "")
                {
                    model.BrandId = int.Parse(ds.Tables[0].Rows[0]["BrandId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BrandName"] != null && ds.Tables[0].Rows[0]["BrandName"].ToString() != "")
                {
                    model.BrandName = ds.Tables[0].Rows[0]["BrandName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BrandSpell"] != null && ds.Tables[0].Rows[0]["BrandSpell"].ToString() != "")
                {
                    model.BrandSpell = ds.Tables[0].Rows[0]["BrandSpell"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Meta_Description"] != null && ds.Tables[0].Rows[0]["Meta_Description"].ToString() != "")
                {
                    model.Meta_Description = ds.Tables[0].Rows[0]["Meta_Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Meta_Keywords"] != null && ds.Tables[0].Rows[0]["Meta_Keywords"].ToString() != "")
                {
                    model.Meta_Keywords = ds.Tables[0].Rows[0]["Meta_Keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Logo"] != null && ds.Tables[0].Rows[0]["Logo"].ToString() != "")
                {
                    model.Logo = ds.Tables[0].Rows[0]["Logo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompanyUrl"] != null && ds.Tables[0].Rows[0]["CompanyUrl"].ToString() != "")
                {
                    model.CompanyUrl = ds.Tables[0].Rows[0]["CompanyUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DisplaySequence"] != null && ds.Tables[0].Rows[0]["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(ds.Tables[0].Rows[0]["DisplaySequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Theme"] != null && ds.Tables[0].Rows[0]["Theme"].ToString() != "")
                {
                    model.Theme = ds.Tables[0].Rows[0]["Theme"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BrandId,BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme ");
            strSql.Append(" FROM Shop_Brands ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" BrandId,BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme,BrandsThumbs ");
            strSql.Append(" FROM Shop_Brands ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_Brands ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                strSql.Append("ORDER BY T." + orderby);
            }
            else
            {
                strSql.Append("ORDER BY T.BrandId desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_Brands T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
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
            parameters[0].Value = "Shop_Brands";
            parameters[1].Value = "BrandId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        public bool CreateBrandsAndTypes(Model.Shop.Products.BrandInfo model, Model.Shop.Products.DataProviderAction action)
        {
            int rows = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@BrandName", SqlDbType.NVarChar,50),
					new SqlParameter("@BrandSpell", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Logo", SqlDbType.NVarChar,255),
					new SqlParameter("@CompanyUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@BrandId", SqlDbType.Int,4),
					new SqlParameter("@Action", SqlDbType.Int,4),
					new SqlParameter("@BrandIdOutPut", SqlDbType.Int,4),
                    new SqlParameter("@BrandsThumbs",SqlDbType.VarChar,255)
                                        };
            parameters[0].Value = model.BrandName;
            parameters[1].Value = model.BrandSpell;
            parameters[2].Value = model.Meta_Description;
            parameters[3].Value = model.Meta_Keywords;
            parameters[4].Value = model.Logo;
            parameters[5].Value = model.CompanyUrl;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.DisplaySequence;
            parameters[8].Value = model.Theme;
            parameters[9].Value = model.BrandId;
            parameters[10].Value = (int)action;
            parameters[11].Direction = ParameterDirection.Output;
            parameters[12].Value =model.BrandsThumbs;
            DbHelperSQL.RunProcedure("sp_Shop_BrandsCreateUpdateDelete", parameters, out  rows);
            int bid = 0;
            if (action == Model.Shop.Products.DataProviderAction.Create)
            {
                bid = Convert.ToInt32(parameters[11].Value);
            }
            else
            {
                bid = model.BrandId;
            }
            if (rows > 0 && bid > 0)
            {
                ProductTypeBrand productTypeBrands = new ProductTypeBrand();
                if (action == Model.Shop.Products.DataProviderAction.Update)
                {
                    productTypeBrands.Delete(null, bid);
                }
                foreach (int ProductTypeId in model.ProductTypes)
                {
                    productTypeBrands.Add(ProductTypeId, bid);
                }
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
        public DataSet GetListByProductTypeId(int ProductTypeId,int Top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT    ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top);
            }
            strSql.Append(" B.*  ");
            strSql.Append("FROM Shop_Brands B  ");
            strSql.Append(" where exists( select * from  Shop_ProductTypeBrands A where  A.BrandId=B.BrandId ");
            if (ProductTypeId != 0)
            {
                strSql.AppendFormat(" and  A.ProductTypeId={0}  ", ProductTypeId);
            }
            strSql.Append(" ) ");
            strSql.Append(" ORDER BY DisplaySequence ASC ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByProductTypeId(out int rowCount, out int pageCount, int ProductTypeId, int PageIndex, int PageSize, int action)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductTypeId", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@RowsCount", SqlDbType.Float),
                    new SqlParameter("@PageCount", SqlDbType.Float),
                    new SqlParameter("@Action", SqlDbType.Int)
                    };
            parameters[0].Value = ProductTypeId;
            parameters[1].Value = PageIndex;
            parameters[2].Value = PageSize;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Value = action;
            DataSet ds = DbHelperSQL.RunProcedure("sp_Shop_BrandsPageInfo", parameters, "ds");
            rowCount = Convert.ToInt32(parameters[3].Value);
            pageCount = Convert.ToInt32(parameters[4].Value);
            return ds;
        }

        public Model.Shop.Products.BrandInfo GetRelatedProduct(int brandsId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Shop_ProductTypeBrands ");
            strSql.AppendFormat(" WHERE BrandId={0}", brandsId);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            IList<int> list = new List<int>();
            Model.Shop.Products.BrandInfo model = new Model.Shop.Products.BrandInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["ProductTypeId"] != null && dr["ProductTypeId"].ToString() != "")
                    {
                        list.Add((int)dr["ProductTypeId"]);
                    }
                }
            }
            model.ProductTypes = list;
            return model;
        }

        public Model.Shop.Products.BrandInfo GetRelatedProduct(int? brandsId, int? ProductTypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Shop_ProductTypeBrands ");
            strSql.Append(" WHERE 1=1 ");
            if (brandsId.HasValue)
            {
                strSql.AppendFormat(" AND BrandId={0}", brandsId);
            }
            if (ProductTypeId.HasValue)
            {
                strSql.AppendFormat(" AND ProductTypeId={0}", ProductTypeId);
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            IList<int> list = new List<int>();
            Model.Shop.Products.BrandInfo model = new Model.Shop.Products.BrandInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (brandsId.HasValue)
                    {
                        if (dr["ProductTypeId"] != null && dr["ProductTypeId"].ToString() != "")
                        {
                            list.Add((int)dr["ProductTypeId"]);
                        }
                    }
                    if (ProductTypeId.HasValue)
                    {
                        if (dr["BrandId"] != null && dr["BrandId"].ToString() != "")
                        {
                            list.Add((int)dr["BrandId"]);
                        }
                    }
                }
            }
            model.ProductTypeIdOrBrandsId = list;
            return model;
        }

        /// <summary>
        /// 根据分类ID获取品牌信息
        /// </summary>
        public DataSet GetBrandsListByCateId(int? cateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Shop_Brands ");
            strSql.Append("WHERE BrandId IN(SELECT DISTINCT BrandId FROM Shop_Products ");
            if (cateId.HasValue)
            {
                strSql.AppendFormat("WHERE CategoryId={0} ", cateId.Value);
            }
            strSql.Append(")");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetBrandsByCateId(int cateId, bool IsChild,int Top)
        {
            StringBuilder strSql = new StringBuilder();
                     strSql.Append("SELECT   ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top);
            }
            strSql.Append("   * FROM    Shop_Brands ");
            if (cateId > 0)
            {
                strSql.Append(" WHERE   EXISTS ( SELECT * FROM   Shop_Products ");
                strSql.Append("  WHERE  SaleStatus=1 and  BrandId = Shop_Brands.BrandId ");
                strSql.Append(" AND EXISTS ( SELECT * FROM   Shop_ProductCategories  ");
                strSql.Append(" WHERE  ProductId = Shop_Products.ProductId  ");
                if (IsChild)
                {
                    strSql.AppendFormat(
                        "   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ",
                        cateId);
                    strSql.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0})", cateId);
                }
                else
                {
                    strSql.AppendFormat("  Shop_ProductCategories.CategoryId = {0}", cateId);
                }
                strSql.Append(")) ");
            }
          
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

