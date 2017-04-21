using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Comment;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Comment
{
	/// <summary>
	/// 数据访问类:Comment
	/// </summary>
	public partial class Comment:IComment
	{
		public Comment()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("ID", "CMS_Comment"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from CMS_Comment");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.Comment.Comment model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into CMS_Comment(");
			strSql.Append("ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead)");
			strSql.Append(" values (");
			strSql.Append("@ContentId,@Description,@CreatedDate,@CreatedUserID,@ReplyCount,@ParentID,@TypeID,@State,@IsRead)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ContentId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.Bit,1),
					new SqlParameter("@IsRead", SqlDbType.Bit,1)};
			parameters[0].Value = model.ContentId;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.CreatedDate;
			parameters[3].Value = model.CreatedUserID;
			parameters[4].Value = model.ReplyCount;
			parameters[5].Value = model.ParentID;
			parameters[6].Value = model.TypeID;
			parameters[7].Value = model.State;
			parameters[8].Value = model.IsRead;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			return obj == null ? 0 : Convert.ToInt32(obj);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.Comment.Comment model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update CMS_Comment set ");
			strSql.Append("ContentId=@ContentId,");
			strSql.Append("Description=@Description,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("ReplyCount=@ReplyCount,");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("TypeID=@TypeID,");
			strSql.Append("State=@State,");
			strSql.Append("IsRead=@IsRead");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ContentId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@State", SqlDbType.Bit,1),
					new SqlParameter("@IsRead", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ContentId;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.CreatedDate;
			parameters[3].Value = model.CreatedUserID;
			parameters[4].Value = model.ReplyCount;
			parameters[5].Value = model.ParentID;
			parameters[6].Value = model.TypeID;
			parameters[7].Value = model.State;
			parameters[8].Value = model.IsRead;
			parameters[9].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows > 0;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from CMS_Comment ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows > 0;
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from CMS_Comment ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Comment.Comment GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 * from CMS_Comment ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			Model.Comment.Comment model=new Model.Comment.Comment();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContentId"]!=null && ds.Tables[0].Rows[0]["ContentId"].ToString()!="")
				{
					model.ContentId=int.Parse(ds.Tables[0].Rows[0]["ContentId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Description"]!=null && ds.Tables[0].Rows[0]["Description"].ToString()!="")
				{
					model.Description=ds.Tables[0].Rows[0]["Description"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreatedDate"]!=null && ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedUserID"]!=null && ds.Tables[0].Rows[0]["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReplyCount"]!=null && ds.Tables[0].Rows[0]["ReplyCount"].ToString()!="")
				{
					model.ReplyCount=int.Parse(ds.Tables[0].Rows[0]["ReplyCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentID"]!=null && ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					model.ParentID=int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TypeID"]!=null && ds.Tables[0].Rows[0]["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["State"]!=null && ds.Tables[0].Rows[0]["State"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["State"].ToString()=="1")||(ds.Tables[0].Rows[0]["State"].ToString().ToLower()=="true"))
					{
						model.State=true;
					}
					else
					{
						model.State=false;
					}
				}
				if(ds.Tables[0].Rows[0]["IsRead"]!=null && ds.Tables[0].Rows[0]["IsRead"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsRead"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsRead"].ToString().ToLower()=="true"))
					{
						model.IsRead=true;
					}
					else
					{
						model.IsRead=false;
					}
				}
				return model;
			}
		    return null;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select Comment.*,AU.UserName  FROM CMS_Comment Comment");
            strSql.Append(" LEFT JOIN Accounts_Users AU ON Comment.CreatedUserID=AU.UserID ");
		    if (strWhere.Trim() != "")
		    {
		        strSql.Append(" where " + strWhere);
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
			strSql.Append(" ID,ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead ");
            strSql.Append(" FROM CMS_Comment ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Comment";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/


        public bool UpdateStates(string strList,bool bResult)
        {
            string str = string.Empty;
            if (!string.IsNullOrWhiteSpace(strList))
            {
                if (bResult)
                {
                    str = "Update CMS_Comment Set State = 1 Where ID in (" + strList + ")";
                }
                else
                {
                    str = "Update CMS_Comment Set State = 0 Where ID in (" + strList + ")";
                }
            }
            int rows = DbHelperSQL.ExecuteSql(str);
            return rows > 0;
        }

	    #endregion  Method
	}
}

