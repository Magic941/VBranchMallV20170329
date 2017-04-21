using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Gift;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Shop.Gift
{
	/// <summary>
	/// 数据访问类:Gifts
	/// </summary>
	public partial class Gifts:IGifts
	{
		public Gifts()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("GiftId", "Shop_Gifts"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int GiftId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_Gifts");
			strSql.Append(" where GiftId=@GiftId");
			SqlParameter[] parameters = {
					new SqlParameter("@GiftId", SqlDbType.Int,4)
			};
			parameters[0].Value = GiftId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Gift.Gifts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_Gifts(");
			strSql.Append("CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled)");
			strSql.Append(" values (");
			strSql.Append("@CategoryID,@Name,@ShortDescription,@Unit,@Weight,@LongDescription,@Title,@Meta_Description,@Meta_Keywords,@ThumbnailsUrl,@InFocusImageUrl,@CostPrice,@MarketPrice,@SalePrice,@Stock,@NeedPoint,@NeedGrade,@SaleCounts,@CreateDate,@Enabled)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@ShortDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@LongDescription", SqlDbType.NText),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@InFocusImageUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@CostPrice", SqlDbType.Money,8),
					new SqlParameter("@MarketPrice", SqlDbType.Money,8),
					new SqlParameter("@SalePrice", SqlDbType.Money,8),
					new SqlParameter("@Stock", SqlDbType.Int,4),
					new SqlParameter("@NeedPoint", SqlDbType.Int,4),
					new SqlParameter("@NeedGrade", SqlDbType.Int,4),
					new SqlParameter("@SaleCounts", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Enabled", SqlDbType.Bit,1)};
			parameters[0].Value = model.CategoryID;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.ShortDescription;
			parameters[3].Value = model.Unit;
			parameters[4].Value = model.Weight;
			parameters[5].Value = model.LongDescription;
			parameters[6].Value = model.Title;
			parameters[7].Value = model.Meta_Description;
			parameters[8].Value = model.Meta_Keywords;
			parameters[9].Value = model.ThumbnailsUrl;
			parameters[10].Value = model.InFocusImageUrl;
			parameters[11].Value = model.CostPrice;
			parameters[12].Value = model.MarketPrice;
			parameters[13].Value = model.SalePrice;
			parameters[14].Value = model.Stock;
			parameters[15].Value = model.NeedPoint;
			parameters[16].Value = model.NeedGrade;
			parameters[17].Value = model.SaleCounts;
			parameters[18].Value = model.CreateDate;
			parameters[19].Value = model.Enabled;

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
		public bool Update(Maticsoft.Model.Shop.Gift.Gifts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_Gifts set ");
			strSql.Append("CategoryID=@CategoryID,");
			strSql.Append("Name=@Name,");
			strSql.Append("ShortDescription=@ShortDescription,");
			strSql.Append("Unit=@Unit,");
			strSql.Append("Weight=@Weight,");
			strSql.Append("LongDescription=@LongDescription,");
			strSql.Append("Title=@Title,");
			strSql.Append("Meta_Description=@Meta_Description,");
			strSql.Append("Meta_Keywords=@Meta_Keywords,");
			strSql.Append("ThumbnailsUrl=@ThumbnailsUrl,");
			strSql.Append("InFocusImageUrl=@InFocusImageUrl,");
			strSql.Append("CostPrice=@CostPrice,");
			strSql.Append("MarketPrice=@MarketPrice,");
			strSql.Append("SalePrice=@SalePrice,");
			strSql.Append("Stock=@Stock,");
			strSql.Append("NeedPoint=@NeedPoint,");
			strSql.Append("NeedGrade=@NeedGrade,");
			strSql.Append("SaleCounts=@SaleCounts,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("Enabled=@Enabled");
			strSql.Append(" where GiftId=@GiftId");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@ShortDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@LongDescription", SqlDbType.NText),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@InFocusImageUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@CostPrice", SqlDbType.Money,8),
					new SqlParameter("@MarketPrice", SqlDbType.Money,8),
					new SqlParameter("@SalePrice", SqlDbType.Money,8),
					new SqlParameter("@Stock", SqlDbType.Int,4),
					new SqlParameter("@NeedPoint", SqlDbType.Int,4),
					new SqlParameter("@NeedGrade", SqlDbType.Int,4),
					new SqlParameter("@SaleCounts", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Enabled", SqlDbType.Bit,1),
					new SqlParameter("@GiftId", SqlDbType.Int,4)};
			parameters[0].Value = model.CategoryID;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.ShortDescription;
			parameters[3].Value = model.Unit;
			parameters[4].Value = model.Weight;
			parameters[5].Value = model.LongDescription;
			parameters[6].Value = model.Title;
			parameters[7].Value = model.Meta_Description;
			parameters[8].Value = model.Meta_Keywords;
			parameters[9].Value = model.ThumbnailsUrl;
			parameters[10].Value = model.InFocusImageUrl;
			parameters[11].Value = model.CostPrice;
			parameters[12].Value = model.MarketPrice;
			parameters[13].Value = model.SalePrice;
			parameters[14].Value = model.Stock;
			parameters[15].Value = model.NeedPoint;
			parameters[16].Value = model.NeedGrade;
			parameters[17].Value = model.SaleCounts;
			parameters[18].Value = model.CreateDate;
			parameters[19].Value = model.Enabled;
			parameters[20].Value = model.GiftId;

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
		public bool Delete(int GiftId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_Gifts ");
			strSql.Append(" where GiftId=@GiftId");
			SqlParameter[] parameters = {
					new SqlParameter("@GiftId", SqlDbType.Int,4)
			};
			parameters[0].Value = GiftId;

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
		public bool DeleteList(string GiftIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_Gifts ");
			strSql.Append(" where GiftId in ("+GiftIdlist + ")  ");
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
		public Maticsoft.Model.Shop.Gift.Gifts GetModel(int GiftId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 GiftId,CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled from Shop_Gifts ");
			strSql.Append(" where GiftId=@GiftId");
			SqlParameter[] parameters = {
					new SqlParameter("@GiftId", SqlDbType.Int,4)
			};
			parameters[0].Value = GiftId;

			Maticsoft.Model.Shop.Gift.Gifts model=new Maticsoft.Model.Shop.Gift.Gifts();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GiftId"]!=null && ds.Tables[0].Rows[0]["GiftId"].ToString()!="")
				{
					model.GiftId=int.Parse(ds.Tables[0].Rows[0]["GiftId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CategoryID"]!=null && ds.Tables[0].Rows[0]["CategoryID"].ToString()!="")
				{
					model.CategoryID=int.Parse(ds.Tables[0].Rows[0]["CategoryID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ShortDescription"]!=null && ds.Tables[0].Rows[0]["ShortDescription"].ToString()!="")
				{
					model.ShortDescription=ds.Tables[0].Rows[0]["ShortDescription"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Unit"]!=null && ds.Tables[0].Rows[0]["Unit"].ToString()!="")
				{
					model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Weight"]!=null && ds.Tables[0].Rows[0]["Weight"].ToString()!="")
				{
					model.Weight=int.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LongDescription"]!=null && ds.Tables[0].Rows[0]["LongDescription"].ToString()!="")
				{
					model.LongDescription=ds.Tables[0].Rows[0]["LongDescription"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Meta_Description"]!=null && ds.Tables[0].Rows[0]["Meta_Description"].ToString()!="")
				{
					model.Meta_Description=ds.Tables[0].Rows[0]["Meta_Description"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Meta_Keywords"]!=null && ds.Tables[0].Rows[0]["Meta_Keywords"].ToString()!="")
				{
					model.Meta_Keywords=ds.Tables[0].Rows[0]["Meta_Keywords"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ThumbnailsUrl"]!=null && ds.Tables[0].Rows[0]["ThumbnailsUrl"].ToString()!="")
				{
					model.ThumbnailsUrl=ds.Tables[0].Rows[0]["ThumbnailsUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["InFocusImageUrl"]!=null && ds.Tables[0].Rows[0]["InFocusImageUrl"].ToString()!="")
				{
					model.InFocusImageUrl=ds.Tables[0].Rows[0]["InFocusImageUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CostPrice"]!=null && ds.Tables[0].Rows[0]["CostPrice"].ToString()!="")
				{
					model.CostPrice=decimal.Parse(ds.Tables[0].Rows[0]["CostPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MarketPrice"]!=null && ds.Tables[0].Rows[0]["MarketPrice"].ToString()!="")
				{
					model.MarketPrice=decimal.Parse(ds.Tables[0].Rows[0]["MarketPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SalePrice"]!=null && ds.Tables[0].Rows[0]["SalePrice"].ToString()!="")
				{
					model.SalePrice=decimal.Parse(ds.Tables[0].Rows[0]["SalePrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Stock"]!=null && ds.Tables[0].Rows[0]["Stock"].ToString()!="")
				{
					model.Stock=int.Parse(ds.Tables[0].Rows[0]["Stock"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NeedPoint"]!=null && ds.Tables[0].Rows[0]["NeedPoint"].ToString()!="")
				{
					model.NeedPoint=int.Parse(ds.Tables[0].Rows[0]["NeedPoint"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NeedGrade"]!=null && ds.Tables[0].Rows[0]["NeedGrade"].ToString()!="")
				{
					model.NeedGrade=int.Parse(ds.Tables[0].Rows[0]["NeedGrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SaleCounts"]!=null && ds.Tables[0].Rows[0]["SaleCounts"].ToString()!="")
				{
					model.SaleCounts=int.Parse(ds.Tables[0].Rows[0]["SaleCounts"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateDate"]!=null && ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Enabled"]!=null && ds.Tables[0].Rows[0]["Enabled"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Enabled"].ToString()=="1")||(ds.Tables[0].Rows[0]["Enabled"].ToString().ToLower()=="true"))
					{
						model.Enabled=true;
					}
					else
					{
						model.Enabled=false;
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
			strSql.Append("select GiftId,CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled ");
			strSql.Append(" FROM Shop_Gifts ");
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
			strSql.Append(" GiftId,CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled ");
			strSql.Append(" FROM Shop_Gifts ");
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
			strSql.Append("select count(1) FROM Shop_Gifts ");
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
				strSql.Append("order by T.GiftId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_Gifts T ");
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
			parameters[0].Value = "Shop_Gifts";
			parameters[1].Value = "GiftId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region 扩展方法
        public bool UpdateStock(int giftid, int stock)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Gifts set ");
            strSql.Append("Stock=@Stock");
            strSql.Append(" where GiftId=@GiftId");
            SqlParameter[] parameters = {
					new SqlParameter("@Stock", SqlDbType.Int,4),
					new SqlParameter("@GiftId", SqlDbType.Int,4)};
            parameters[0].Value = stock;
            parameters[1].Value = giftid;

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
    }
}

