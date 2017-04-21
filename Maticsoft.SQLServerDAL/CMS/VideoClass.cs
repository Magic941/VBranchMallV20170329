/**
* VideoClass.cs
*
* 功 能： 
* 类 名： VideoClass
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/22 16:28:49  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.CMS;
using Maticsoft.DBUtility;
using Maticsoft.Common;
using Maticsoft.Common.Video;
namespace Maticsoft.SQLServerDAL.CMS
{
	/// <summary>
	/// 数据访问类:VideoClass
	/// </summary>
	public partial class VideoClass:IVideoClass
	{
		public VideoClass()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("VideoClassID", "CMS_VideoClass"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int VideoClassID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CMS_VideoClass");
			strSql.Append(" where VideoClassID=@VideoClassID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoClassID", SqlDbType.Int,4)
			};
			parameters[0].Value = VideoClassID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.CMS.VideoClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CMS_VideoClass(");
			strSql.Append("VideoClassName,ParentID,Sequence,Path,Depth)");
			strSql.Append(" values (");
			strSql.Append("@VideoClassName,@ParentID,@Sequence,@Path,@Depth)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoClassName", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,1000),
					new SqlParameter("@Depth", SqlDbType.Int,4)};
			parameters[0].Value = model.VideoClassName;
			parameters[1].Value = model.ParentID;
			parameters[2].Value = model.Sequence;
			parameters[3].Value = model.Path;
			parameters[4].Value = model.Depth;

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
		public bool Update(Maticsoft.Model.CMS.VideoClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CMS_VideoClass set ");
			strSql.Append("VideoClassName=@VideoClassName,");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("Path=@Path,");
			strSql.Append("Depth=@Depth");
			strSql.Append(" where VideoClassID=@VideoClassID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoClassName", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,1000),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@VideoClassID", SqlDbType.Int,4)};
			parameters[0].Value = model.VideoClassName;
			parameters[1].Value = model.ParentID;
			parameters[2].Value = model.Sequence;
			parameters[3].Value = model.Path;
			parameters[4].Value = model.Depth;
			parameters[5].Value = model.VideoClassID;

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
		public bool Delete(int VideoClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CMS_VideoClass ");
			strSql.Append(" where VideoClassID=@VideoClassID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoClassID", SqlDbType.Int,4)
			};
			parameters[0].Value = VideoClassID;

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
		public bool DeleteList(string VideoClassIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CMS_VideoClass ");
			strSql.Append(" where VideoClassID in ("+VideoClassIDlist + ")  ");
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
		public Maticsoft.Model.CMS.VideoClass GetModel(int VideoClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth from CMS_VideoClass ");
			strSql.Append(" where VideoClassID=@VideoClassID");
			SqlParameter[] parameters = {
					new SqlParameter("@VideoClassID", SqlDbType.Int,4)
			};
			parameters[0].Value = VideoClassID;

			Maticsoft.Model.CMS.VideoClass model=new Maticsoft.Model.CMS.VideoClass();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["VideoClassID"]!=null && ds.Tables[0].Rows[0]["VideoClassID"].ToString()!="")
				{
					model.VideoClassID=int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["VideoClassName"]!=null && ds.Tables[0].Rows[0]["VideoClassName"].ToString()!="")
				{
					model.VideoClassName=ds.Tables[0].Rows[0]["VideoClassName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ParentID"]!=null && ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					model.ParentID=int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sequence"]!=null && ds.Tables[0].Rows[0]["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Path"]!=null && ds.Tables[0].Rows[0]["Path"].ToString()!="")
				{
					model.Path=ds.Tables[0].Rows[0]["Path"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Depth"]!=null && ds.Tables[0].Rows[0]["Depth"].ToString()!="")
				{
					model.Depth=int.Parse(ds.Tables[0].Rows[0]["Depth"].ToString());
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
			strSql.Append("select VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth ");
			strSql.Append(" FROM CMS_VideoClass ");
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
			strSql.Append(" VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth ");
			strSql.Append(" FROM CMS_VideoClass ");
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
			strSql.Append("select count(1) FROM CMS_VideoClass ");
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
				strSql.Append("order by T.VideoClassID desc");
			}
			strSql.Append(")AS Row, T.*  from CMS_VideoClass T ");
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
			parameters[0].Value = "CMS_VideoClass";
			parameters[1].Value = "VideoClassID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region MethodEx
        /// <summary>
        /// 级联删除分类及子类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int DeleteEx(int VideoClassID)
        {
            int rows;
            SqlParameter[] parameters = {
					new SqlParameter("@VideoClassID", SqlDbType.Int)
					};
            parameters[0].Value = VideoClassID;
            return (DbHelperSQL.RunProcedure("sp_CMS_VideoClass_Delete", parameters, out rows));
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddEx(Maticsoft.Model.CMS.VideoClass model)
        {
            int rows;
            SqlParameter[] parameters = {
					new SqlParameter("@VideoClassName", SqlDbType.NVarChar,30),
					new SqlParameter("@Sequence", SqlDbType.Int),
					new SqlParameter("@ParentID", SqlDbType.Int)
					};
            parameters[0].Value = model.VideoClassName;
            parameters[1].Value = model.Sequence;
            parameters[2].Value = model.ParentID;
            return DbHelperSQL.RunProcedure("sp_CMS_VideoClass_Create", parameters, out rows);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere, string orderBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth ");
            strSql.Append(" FROM CMS_VideoClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(orderBy.Trim()))
            {
                strSql.Append("order by " + orderBy);
            }
            else
            {
                strSql.Append("order by VideoClassID desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据ParentID得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.VideoClass GetModelByParentID(int ParentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth from CMS_VideoClass ");
            strSql.Append(" where VideoClassID=@ParentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.Int,4)
			};
            parameters[0].Value = ParentID;

            Maticsoft.Model.CMS.VideoClass model = new Maticsoft.Model.CMS.VideoClass();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["VideoClassID"] != null && ds.Tables[0].Rows[0]["VideoClassID"].ToString() != "")
                {
                    model.VideoClassID = int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VideoClassName"] != null && ds.Tables[0].Rows[0]["VideoClassName"].ToString() != "")
                {
                    model.VideoClassName = ds.Tables[0].Rows[0]["VideoClassName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentID"] != null && ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sequence"] != null && ds.Tables[0].Rows[0]["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Path"] != null && ds.Tables[0].Rows[0]["Path"].ToString() != "")
                {
                    model.Path = ds.Tables[0].Rows[0]["Path"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Depth"] != null && ds.Tables[0].Rows[0]["Depth"].ToString() != "")
                {
                    model.Depth = int.Parse(ds.Tables[0].Rows[0]["Depth"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到最大顺序
        /// </summary>
        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_VideoClass");
        }
        /// <summary>
        /// 对类别进行排序
        /// </summary>
        /// <param name="VideoClassId">类别ID</param>
        /// <param name="zIndex">排序方式</param>
        /// <returns></returns>
        public int SwapCategorySequence(int VideoClassId, SwapSequenceIndex zIndex)
        {
            int rows;
            SqlParameter[] parameters = {
					new SqlParameter("@VideoClassId", SqlDbType.Int),
					new SqlParameter("@ZIndex", SqlDbType.Int)
					};
            parameters[0].Value = VideoClassId;
            parameters[1].Value = (int)zIndex;
            return DbHelperSQL.RunProcedure("sp_CMS_SwapVideoClassSequence", parameters, out rows);
        }
        #endregion
	}
}

