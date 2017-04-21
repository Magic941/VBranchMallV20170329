/**
* SupplierCategories.cs
*
* 功 能： N/A
* 类 名： SupplierCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:48   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Supplier;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
	/// <summary>
	/// 数据访问类:SupplierCategories
	/// </summary>
	public partial class SupplierCategories:ISupplierCategories
	{
		public SupplierCategories()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CategoryId", "Shop_SupplierCategories"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_SupplierCategories");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_SupplierCategories(");
			strSql.Append("Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Name,@DisplaySequence,@Meta_Title,@Meta_Description,@Meta_Keywords,@Description,@ParentCategoryId,@Depth,@Path,@ImageUrl,@Theme,@HasChildren,@SeoUrl,@SeoImageAlt,@SeoImageTitle,@CreatedUserId,@SupplierId,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,4000),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.DisplaySequence;
			parameters[2].Value = model.Meta_Title;
			parameters[3].Value = model.Meta_Description;
			parameters[4].Value = model.Meta_Keywords;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.ParentCategoryId;
			parameters[7].Value = model.Depth;
			parameters[8].Value = model.Path;
			parameters[9].Value = model.ImageUrl;
			parameters[10].Value = model.Theme;
			parameters[11].Value = model.HasChildren;
			parameters[12].Value = model.SeoUrl;
			parameters[13].Value = model.SeoImageAlt;
			parameters[14].Value = model.SeoImageTitle;
			parameters[15].Value = model.CreatedUserId;
			parameters[16].Value = model.SupplierId;
			parameters[17].Value = model.Remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_SupplierCategories set ");
			strSql.Append("Name=@Name,");
			strSql.Append("DisplaySequence=@DisplaySequence,");
			strSql.Append("Meta_Title=@Meta_Title,");
			strSql.Append("Meta_Description=@Meta_Description,");
			strSql.Append("Meta_Keywords=@Meta_Keywords,");
			strSql.Append("Description=@Description,");
			strSql.Append("ParentCategoryId=@ParentCategoryId,");
			strSql.Append("Depth=@Depth,");
			strSql.Append("Path=@Path,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("Theme=@Theme,");
			strSql.Append("HasChildren=@HasChildren,");
			strSql.Append("SeoUrl=@SeoUrl,");
			strSql.Append("SeoImageAlt=@SeoImageAlt,");
			strSql.Append("SeoImageTitle=@SeoImageTitle,");
			strSql.Append("CreatedUserId=@CreatedUserId,");
			strSql.Append("SupplierId=@SupplierId,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,4000),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@Theme", SqlDbType.NVarChar,100),
					new SqlParameter("@HasChildren", SqlDbType.Bit,1),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.DisplaySequence;
			parameters[2].Value = model.Meta_Title;
			parameters[3].Value = model.Meta_Description;
			parameters[4].Value = model.Meta_Keywords;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.ParentCategoryId;
			parameters[7].Value = model.Depth;
			parameters[8].Value = model.Path;
			parameters[9].Value = model.ImageUrl;
			parameters[10].Value = model.Theme;
			parameters[11].Value = model.HasChildren;
			parameters[12].Value = model.SeoUrl;
			parameters[13].Value = model.SeoImageAlt;
			parameters[14].Value = model.SeoImageTitle;
			parameters[15].Value = model.CreatedUserId;
			parameters[16].Value = model.SupplierId;
			parameters[17].Value = model.Remark;
			parameters[18].Value = model.CategoryId;

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
		public bool Delete(int CategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierCategories ");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryId;

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
		public bool DeleteList(string CategoryIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierCategories ");
			strSql.Append(" where CategoryId in ("+CategoryIdlist + ")  ");
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
		public Maticsoft.Model.Shop.Supplier.SupplierCategories GetModel(int CategoryId)
		{
            Maticsoft.Model.Shop.Supplier.SupplierCategories sCategories = new Model.Shop.Supplier.SupplierCategories();
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CategoryId,Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark from Shop_SupplierCategories ");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryId;

			Maticsoft.Model.Shop.Supplier.SupplierCategories model=new Maticsoft.Model.Shop.Supplier.SupplierCategories();

            DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);

            #region 确认是否有子分类
            string strSql2 = " select HasChildren from Shop_SupplierCategories where ParentCategoryId=@CategoryId ";
            DataSet ds2 = DbHelperSQL.Query(strSql2, parameters);
            
            #endregion

			if(ds.Tables[0].Rows.Count>0)
			{
				sCategories = DataRowToModel(ds.Tables[0].Rows[0]);

                //更新子分类字段
                if (ds2.Tables.Count != 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    sCategories.HasChildren = true;
                }
                else
                {
                    sCategories.HasChildren = false;
                }
			}
			else
			{
				return null;
			}
            return sCategories;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Supplier.SupplierCategories DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Supplier.SupplierCategories model=new Maticsoft.Model.Shop.Supplier.SupplierCategories();
			if (row != null)
			{
				if(row["CategoryId"]!=null && row["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(row["CategoryId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["DisplaySequence"]!=null && row["DisplaySequence"].ToString()!="")
				{
					model.DisplaySequence=int.Parse(row["DisplaySequence"].ToString());
				}
				if(row["Meta_Title"]!=null)
				{
					model.Meta_Title=row["Meta_Title"].ToString();
				}
				if(row["Meta_Description"]!=null)
				{
					model.Meta_Description=row["Meta_Description"].ToString();
				}
				if(row["Meta_Keywords"]!=null)
				{
					model.Meta_Keywords=row["Meta_Keywords"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["ParentCategoryId"]!=null && row["ParentCategoryId"].ToString()!="")
				{
					model.ParentCategoryId=int.Parse(row["ParentCategoryId"].ToString());
				}
				if(row["Depth"]!=null && row["Depth"].ToString()!="")
				{
					model.Depth=int.Parse(row["Depth"].ToString());
				}
				if(row["Path"]!=null)
				{
					model.Path=row["Path"].ToString();
				}
				if(row["ImageUrl"]!=null)
				{
					model.ImageUrl=row["ImageUrl"].ToString();
				}
				if(row["Theme"]!=null)
				{
					model.Theme=row["Theme"].ToString();
				}
				if(row["HasChildren"]!=null && row["HasChildren"].ToString()!="")
				{
					if((row["HasChildren"].ToString()=="1")||(row["HasChildren"].ToString().ToLower()=="true"))
					{
						model.HasChildren=true;
					}
					else
					{
						model.HasChildren=false;
					}
				}
				if(row["SeoUrl"]!=null)
				{
					model.SeoUrl=row["SeoUrl"].ToString();
				}
				if(row["SeoImageAlt"]!=null)
				{
					model.SeoImageAlt=row["SeoImageAlt"].ToString();
				}
				if(row["SeoImageTitle"]!=null)
				{
					model.SeoImageTitle=row["SeoImageTitle"].ToString();
				}
				if(row["CreatedUserId"]!=null && row["CreatedUserId"].ToString()!="")
				{
					model.CreatedUserId=int.Parse(row["CreatedUserId"].ToString());
				}
				if(row["SupplierId"]!=null && row["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(row["SupplierId"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CategoryId,Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark ");
			strSql.Append(" FROM Shop_SupplierCategories ");
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
			strSql.Append(" CategoryId,Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark ");
			strSql.Append(" FROM Shop_SupplierCategories ");
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
			strSql.Append("select count(1) FROM Shop_SupplierCategories ");
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
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.CategoryId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_SupplierCategories T ");
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
			parameters[0].Value = "Shop_SupplierCategories";
			parameters[1].Value = "CategoryId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        public bool UpdateSeqByCid(int Seq, int Cid, int SupplierId)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_SupplierCategories set DisplaySequence=@DisplaySequence WHERE CategoryId=@CategoryId and SupplierId=@SupplierId");
            SqlParameter[] parameters4 =
                {
                    new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
                    new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                    new SqlParameter("@SupplierId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = Seq;
            parameters4[1].Value = Cid;
            parameters4[2].Value = SupplierId;
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
        /// 同级下是否存在同名
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="name">名称</param>
        /// <param name="SupplierId">供应商id</param>
        /// <param name="categoryId">类别id</param>
        /// <returns></returns>
        public bool IsExisted(int parentId, string name, int SupplierId, int categoryId = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_SupplierCategories");
            strSql.Append(" where ParentCategoryId=@ParentCategoryId and Name=@Name and SupplierId=@SupplierId");
            if (categoryId > 0)
            {
                strSql.Append("  and CategoryId<>@CategoryId");
            }
            SqlParameter[] parameters = {    
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,200),
                    new SqlParameter("@SupplierId", SqlDbType.Int, 4)
			};
            parameters[0].Value = parentId;
            parameters[1].Value = categoryId;
            parameters[2].Value = name;
            parameters[3].Value = SupplierId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public bool UpdatePath(Model.Shop.Supplier.SupplierCategories model)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_SupplierCategories set Path=@Path WHERE CategoryId=@CategoryId  and SupplierId=@SupplierId ");
            SqlParameter[] parameters4 =
                {
                    new SqlParameter("@Path", SqlDbType.NVarChar,200),
                    new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                     new SqlParameter("@SupplierId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = model.Path;
            parameters4[1].Value = model.CategoryId;
            parameters4[2].Value = model.SupplierId;
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
        public bool UpdateHasChild(int cid, int SupplierId)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_SupplierCategories set HasChildren=1 WHERE CategoryId=@CategoryId and SupplierId=@SupplierId ");
            SqlParameter[] parameters4 =
                {
                     new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                      new SqlParameter("@SupplierId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = cid;
            parameters4[1].Value = SupplierId;
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
        public bool UpdateHasChild(int cid, int SupplierId, bool Status)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_SupplierCategories set HasChildren=@HasChildren WHERE CategoryId=@CategoryId and SupplierId=@SupplierId ");
            SqlParameter[] parameters4 =
                {
                     new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                      new SqlParameter("@SupplierId", SqlDbType.Int, 4),
                      new SqlParameter("@HasChildren", SqlDbType.Bit,1) 
                };
            parameters4[0].Value = cid;
            parameters4[1].Value = SupplierId;
            parameters4[2].Value = Status ? 1 : 0;
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
        public bool UpdateDepthAndPath(int Cid, int Depth, string Path, int SupplierId)
        {
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update Shop_Categories set Path=@Path, Depth=@Depth WHERE CategoryId=@CategoryId and SupplierId=@SupplierId ");
            SqlParameter[] parameters4 =
                {
                    new SqlParameter("@Path", SqlDbType.NVarChar,200),
                    new SqlParameter("@Depth", SqlDbType.Int, 4),
                     new SqlParameter("@CategoryId", SqlDbType.Int, 4),
                     new SqlParameter("@SupplierId", SqlDbType.Int, 4)
                };
            parameters4[0].Value = Path;
            parameters4[1].Value = Depth;
            parameters4[2].Value = Cid;
            parameters4[3].Value = SupplierId;
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
        public int GetMaxSeqByCid(int parentId, int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT MAX(DisplaySequence) FROM Shop_SupplierCategories WHERE ParentCategoryId=@ParentCategoryId and SupplierId=@SupplierId ");
            SqlParameter[] parameter = {
                                       new SqlParameter("@ParentCategoryId",SqlDbType.Int,4),
                                       new SqlParameter("@SupplierId", SqlDbType.Int, 4)
                                       };
            parameter[0].Value = parentId;
            parameter[1].Value = SupplierId;
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

        /// <summary>
        ///根据商品id获取分类信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
	    public Model.Shop.Supplier.SupplierCategories GetModelByProductId(long productId)
	    {
	        StringBuilder strSql=new StringBuilder();
            strSql.Append(" SELECT TOP 1 *   FROM  Shop_SupplierCategories AS suppcate WHERE  EXISTS   ( ");
            strSql.Append("  SELECT CategoryId FROM  Shop_SuppProductCategories WHERE ProductId=@ProductId AND suppcate.CategoryId=CategoryId )");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)
			};
            parameters[0].Value = productId;

			Maticsoft.Model.Shop.Supplier.SupplierCategories model=new Maticsoft.Model.Shop.Supplier.SupplierCategories();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
        }
       /// <summary>
        /// 判断该分类下是否有商品
       /// </summary>
       /// <param name="CategoryId"></param>
       /// <returns></returns>
        public bool IsExistsProd(int CategoryId)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  COUNT(*) FROM   Shop_Products AS prod  WHERE ");
           strSql.Append(" EXISTS ( ");
           strSql.AppendFormat(" SELECT * FROM  Shop_SuppProductCategories AS suppprodcate WHERE prod.ProductId=suppprodcate.ProductId AND  suppprodcate.CategoryId={0})", CategoryId);

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null || obj.ToString()=="0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 根据分类级数和供应商ID获取总条数
        /// </summary>
        /// <param name="depth">级数</param>
        /// <param name="supplierId">供应商ID</param>
        /// <returns></returns>
      public int GetCountBySupIdEx(int depth, int supplierId)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("select count(1) FROM Shop_SupplierCategories where Depth=@depth and SupplierId=@SupplierId ");
          SqlParameter[] parameter = {
                                       new SqlParameter("@depth",SqlDbType.Int,4),
                                       new SqlParameter("@SupplierId", SqlDbType.Int, 4)
                                       };
          parameter[0].Value = depth;
          parameter[1].Value = supplierId;
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
		#endregion  ExtensionMethod
	}
}

