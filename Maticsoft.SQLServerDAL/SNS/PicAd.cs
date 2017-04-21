using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:PicAd
	/// </summary>
	public partial class PicAd
	{
		public PicAd()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "X_PicAd"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from X_PicAd");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.PicAd model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into X_PicAd(");
			strSql.Append("Name,Src,Href,Title,Alt,IsShow,Orders)");
			strSql.Append(" values (");
			strSql.Append("@Name,@Src,@Href,@Title,@Alt,@IsShow,@Orders)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Src", SqlDbType.NVarChar,200),
					new SqlParameter("@Href", SqlDbType.NVarChar,200),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Alt", SqlDbType.NVarChar,50),
					new SqlParameter("@IsShow", SqlDbType.Bit,1),
					new SqlParameter("@Orders", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Src;
			parameters[2].Value = model.Href;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.Alt;
			parameters[5].Value = model.IsShow;
			parameters[6].Value = model.Orders;

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
		public bool Update(Maticsoft.Model.SNS.PicAd model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update X_PicAd set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Src=@Src,");
			strSql.Append("Href=@Href,");
			strSql.Append("Title=@Title,");
			strSql.Append("Alt=@Alt,");
			strSql.Append("IsShow=@IsShow,");
			strSql.Append("Orders=@Orders");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Src", SqlDbType.NVarChar,200),
					new SqlParameter("@Href", SqlDbType.NVarChar,200),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Alt", SqlDbType.NVarChar,50),
					new SqlParameter("@IsShow", SqlDbType.Bit,1),
					new SqlParameter("@Orders", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Src;
			parameters[2].Value = model.Href;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.Alt;
			parameters[5].Value = model.IsShow;
			parameters[6].Value = model.Orders;
			parameters[7].Value = model.Id;

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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from X_PicAd ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from X_PicAd ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public Maticsoft.Model.SNS.PicAd GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Name,Src,Href,Title,Alt,IsShow,Orders from X_PicAd ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			Maticsoft.Model.SNS.PicAd model=new Maticsoft.Model.SNS.PicAd();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Src"]!=null && ds.Tables[0].Rows[0]["Src"].ToString()!="")
				{
					model.Src=ds.Tables[0].Rows[0]["Src"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Href"]!=null && ds.Tables[0].Rows[0]["Href"].ToString()!="")
				{
					model.Href=ds.Tables[0].Rows[0]["Href"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Alt"]!=null && ds.Tables[0].Rows[0]["Alt"].ToString()!="")
				{
					model.Alt=ds.Tables[0].Rows[0]["Alt"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsShow"]!=null && ds.Tables[0].Rows[0]["IsShow"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsShow"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsShow"].ToString().ToLower()=="true"))
					{
						model.IsShow=true;
					}
					else
					{
						model.IsShow=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Orders"]!=null && ds.Tables[0].Rows[0]["Orders"].ToString()!="")
				{
					model.Orders=int.Parse(ds.Tables[0].Rows[0]["Orders"].ToString());
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
			strSql.Append("select Id,Name,Src,Href,Title,Alt,IsShow,Orders ");
			strSql.Append(" FROM X_PicAd ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" Order by Orders desc ");
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
			strSql.Append(" Id,Name,Src,Href,Title,Alt,IsShow,Orders ");
			strSql.Append(" FROM X_PicAd ");
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
			strSql.Append("select count(1) FROM X_PicAd ");
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
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from X_PicAd T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}


        public DataSet GetList()
        {
            //return DbHelperSQL.RunProcedure("sp_GetPicList", null, "ds");
            return DbHelperSQL.Query(" SELECT top 4 ROW_NUMBER() OVER( ORDER BY Id ASC) AS ROW,* FROM X_PicAd where IsShow=1 ");
        
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
			parameters[0].Value = "X_PicAd";
			parameters[1].Value = "Id";
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

