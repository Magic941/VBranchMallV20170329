/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductTypes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:30
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
	/// 数据访问类:ProductType
	/// </summary>
	public partial class ProductType:IProductType
	{
		public ProductType()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TypeId", "Shop_ProductTypes"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TypeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Shop_ProductTypes");
			strSql.Append(" WHERE TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Products.ProductType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("INSERT INTO Shop_ProductTypes(");
			strSql.Append("TypeName,Remark)");
			strSql.Append(" VALUES (");
			strSql.Append("@TypeName,@Remark)");
			strSql.Append(";SELECT @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.TypeName;
			parameters[1].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.Shop.Products.ProductType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE Shop_ProductTypes SET ");
			strSql.Append("TypeName=@TypeName,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" WHERE TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@TypeId", SqlDbType.Int,4)};
			parameters[0].Value = model.TypeName;
			parameters[1].Value = model.Remark;
			parameters[2].Value = model.TypeId;

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
		public bool Delete(int TypeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM Shop_ProductTypes ");
			strSql.Append(" WHERE TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeId;

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
		public bool DeleteList(string TypeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM Shop_ProductTypes ");
			strSql.Append(" WHERE TypeId in ("+TypeIdlist + ")  ");
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
		public Maticsoft.Model.Shop.Products.ProductType GetModel(int TypeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT  TOP 1 TypeId,TypeName,Remark FROM Shop_ProductTypes ");
			strSql.Append(" WHERE TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeId;

			Maticsoft.Model.Shop.Products.ProductType model=new Maticsoft.Model.Shop.Products.ProductType();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TypeId"]!=null && ds.Tables[0].Rows[0]["TypeId"].ToString()!="")
				{
					model.TypeId=int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TypeName"]!=null && ds.Tables[0].Rows[0]["TypeName"].ToString()!="")
				{
					model.TypeName=ds.Tables[0].Rows[0]["TypeName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
				{
					model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
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
			strSql.Append("SELECT TypeId,TypeName,Remark ");
			strSql.Append(" FROM Shop_ProductTypes ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT ");
			if(Top>0)
			{
				strSql.Append(" TOP "+Top.ToString());
			}
			strSql.Append(" TypeId,TypeName,Remark ");
			strSql.Append(" FROM Shop_ProductTypes ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			strSql.Append(" ORDER BY " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Shop_ProductTypes ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
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
				strSql.Append("ORDER BY T." + orderby );
			}
			else
			{
				strSql.Append("ORDER BY T.TypeId desc");
			}
			strSql.Append(")AS Row, T.*  FROM Shop_ProductTypes T ");
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
			parameters[0].Value = "Shop_ProductTypes";
			parameters[1].Value = "TypeId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region NewMethod
        public List<Maticsoft.Model.Shop.Products.ProductType> GetProductTypes()
        {
            List<Maticsoft.Model.Shop.Products.ProductType> list = new List<Maticsoft.Model.Shop.Products.ProductType>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Shop_ProductTypes");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Maticsoft.Model.Shop.Products.ProductType model = new Maticsoft.Model.Shop.Products.ProductType();
                    LoadEntityData(ref model, dr);
                    list.Add(model);
                }
            }
            return list;
        }

        #region 将行数据 转成 实体对象
        /// <summary>
        /// 将行数据 转成 实体对象
        /// </summary>
        /// <param name="model">Entity</param>
        /// <param name="dr">DataRow</param>
        private void LoadEntityData(ref Maticsoft.Model.Shop.Products.ProductType model, DataRow dr)
        {
            if (dr["TypeId"] != null && dr["TypeId"].ToString() != "")
            {
                model.TypeId = int.Parse(dr["TypeId"].ToString());
            }
            if (dr["TypeName"] != null && dr["TypeName"].ToString() != "")
            {
                model.TypeName = dr["TypeName"].ToString();
            }
            if (dr["Remark"] != null && dr["Remark"].ToString() != "")
            {
                model.Remark = dr["Remark"].ToString();
            }
        }
        #endregion 

        public bool ProductTypeManage(Model.Shop.Products.ProductType model,Model.Shop.Products.DataProviderAction Action,out int Typeid)
        {
            int rows = 0;
            SqlParameter[] param ={
                                 new SqlParameter("@TypeId",SqlDbType.Int),
                                 new SqlParameter("@TypeName",SqlDbType.NVarChar),
                                 new SqlParameter("@Remark",SqlDbType.NVarChar),
                                 new SqlParameter("@Action",SqlDbType.Int),
                                 new SqlParameter("@TypeIdOut",SqlDbType.Int)
                                 };
            param[0].Value = model.TypeId;
            param[1].Value = model.TypeName;
            param[2].Value = model.Remark;
            param[3].Value = (int)Action;
            param[4].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure("sp_Show_Shop_ProductTypesCreateUpdateDelete", param, out rows);
            int typeId = 0;
            if (Action == Model.Shop.Products.DataProviderAction.Create)
            {
                typeId = Convert.ToInt32(param[4].Value);
            }
            else
            {
                typeId = model.TypeId;
            }
            if (rows > 0 && typeId > 0)
            {
                ProductTypeBrand productTypeBrands = new ProductTypeBrand();
                if (Action == Model.Shop.Products.DataProviderAction.Update)
                {
                    productTypeBrands.Delete(typeId, null);
                }
                foreach (int bid in model.BrandsTypes)
                {
                    productTypeBrands.Add(typeId, bid);
                }
                Typeid = typeId;
                return true;
            }
            else
            {
                Typeid = 0;
                return false;
            }
        }


        public bool DeleteManage(int? TypeId,long? AttributeId,long? ValueId)
        {
            int rowsAffected=0;
            SqlParameter[] parameter = { 
                                       new SqlParameter("@TypeId",SqlDbType.Int),
                                       new SqlParameter("@AttributeId",SqlDbType.BigInt),
                                       new SqlParameter("@ValueId",SqlDbType.BigInt)
                                       };
            parameter[0].Value = TypeId;
            parameter[1].Value = AttributeId;
            parameter[2].Value = ValueId;

            DbHelperSQL.RunProcedure("sp_Shop_DeleteManage", parameter, out rowsAffected);
            return rowsAffected > 0;
        }

        public bool SwapSeqManage(int? TypeId, long? AttributeId, long? ValueId, Model.Shop.Products.SwapSequenceIndex zIndex, bool UsageMode)
        {
            int rowsAffected = 0;
            SqlParameter[] parameter = { 
                                       new SqlParameter("@TypeId",SqlDbType.Int),
                                       new SqlParameter("@AttributeId",SqlDbType.BigInt),
                                       new SqlParameter("@ValueId",SqlDbType.BigInt),
                                       new SqlParameter("@ZIndex",SqlDbType.Int),
                                       new SqlParameter("@UsageMode",SqlDbType.Bit)
                                       };
            parameter[0].Value = TypeId;
            parameter[1].Value = AttributeId;
            parameter[2].Value = ValueId;
            parameter[3].Value = (int)zIndex;
            parameter[4].Value = UsageMode;

            DbHelperSQL.RunProcedure("sp_Shop_SwapManage", parameter, out rowsAffected);
            return rowsAffected > 0;
        }
        #endregion

    }
}

