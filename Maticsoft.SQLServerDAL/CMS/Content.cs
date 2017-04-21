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
    /// 数据访问类:Content
    /// </summary>
    public partial class Content : IContent
    {
        public Content()
        { }


        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ContentID", "CMS_Content");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ContentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_Content");
            strSql.Append(" where ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)
			};
            parameters[0].Value = ContentID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.CMS.Content model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_Content(");
            strSql.Append("Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl)");
            strSql.Append(" values (");
            strSql.Append("@Title,@SubTitle,@Summary,@Description,@ImageUrl,@ThumbImageUrl,@NormalImageUrl,@CreatedDate,@CreatedUserID,@LastEditUserID,@LastEditDate,@LinkUrl,@PvCount,@State,@ClassID,@Keywords,@Sequence,@IsRecomend,@IsHot,@IsColor,@IsTop,@Attachment,@Remary,@TotalComment,@TotalSupport,@TotalFav,@TotalShare,@BeFrom,@FileName,@Meta_Title,@Meta_Description,@Meta_Keywords,@SeoUrl,@SeoImageAlt,@SeoImageTitle,@StaticUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,255),
					new SqlParameter("@Summary", SqlDbType.NText),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@LastEditUserID", SqlDbType.Int,4),
					new SqlParameter("@LastEditDate", SqlDbType.DateTime),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Bit,1),
					new SqlParameter("@IsHot", SqlDbType.Bit,1),
					new SqlParameter("@IsColor", SqlDbType.Bit,1),
					new SqlParameter("@IsTop", SqlDbType.Bit,1),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@Remary", SqlDbType.NVarChar,200),
					new SqlParameter("@TotalComment", SqlDbType.Int,4),
					new SqlParameter("@TotalSupport", SqlDbType.Int,4),
					new SqlParameter("@TotalFav", SqlDbType.Int,4),
					new SqlParameter("@TotalShare", SqlDbType.Int,4),
					new SqlParameter("@BeFrom", SqlDbType.NVarChar,50),
					new SqlParameter("@FileName", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.SubTitle;
            parameters[2].Value = model.Summary;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.ImageUrl;
            parameters[5].Value = model.ThumbImageUrl;
            parameters[6].Value = model.NormalImageUrl;
            parameters[7].Value = model.CreatedDate;
            parameters[8].Value = model.CreatedUserID;
            parameters[9].Value = model.LastEditUserID;
            parameters[10].Value = model.LastEditDate;
            parameters[11].Value = model.LinkUrl;
            parameters[12].Value = model.PvCount;
            parameters[13].Value = model.State;
            parameters[14].Value = model.ClassID;
            parameters[15].Value = model.Keywords;
            parameters[16].Value = model.Sequence;
            parameters[17].Value = model.IsRecomend;
            parameters[18].Value = model.IsHot;
            parameters[19].Value = model.IsColor;
            parameters[20].Value = model.IsTop;
            parameters[21].Value = model.Attachment;
            parameters[22].Value = model.Remary;
            parameters[23].Value = model.TotalComment;
            parameters[24].Value = model.TotalSupport;
            parameters[25].Value = model.TotalFav;
            parameters[26].Value = model.TotalShare;
            parameters[27].Value = model.BeFrom;
            parameters[28].Value = model.FileName;
            parameters[29].Value = model.Meta_Title;
            parameters[30].Value = model.Meta_Description;
            parameters[31].Value = model.Meta_Keywords;
            parameters[32].Value = model.SeoUrl;
            parameters[33].Value = model.SeoImageAlt;
            parameters[34].Value = model.SeoImageTitle;
            parameters[35].Value = model.StaticUrl;

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
        public bool Update(Maticsoft.Model.CMS.Content model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set ");
            strSql.Append("Title=@Title,");
            strSql.Append("SubTitle=@SubTitle,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Description=@Description,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("ThumbImageUrl=@ThumbImageUrl,");
            strSql.Append("NormalImageUrl=@NormalImageUrl,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CreatedUserID=@CreatedUserID,");
            strSql.Append("LastEditUserID=@LastEditUserID,");
            strSql.Append("LastEditDate=@LastEditDate,");
            strSql.Append("LinkUrl=@LinkUrl,");
            strSql.Append("PvCount=@PvCount,");
            strSql.Append("State=@State,");
            strSql.Append("ClassID=@ClassID,");
            strSql.Append("Keywords=@Keywords,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("IsRecomend=@IsRecomend,");
            strSql.Append("IsHot=@IsHot,");
            strSql.Append("IsColor=@IsColor,");
            strSql.Append("IsTop=@IsTop,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("Remary=@Remary,");
            strSql.Append("TotalComment=@TotalComment,");
            strSql.Append("TotalSupport=@TotalSupport,");
            strSql.Append("TotalFav=@TotalFav,");
            strSql.Append("TotalShare=@TotalShare,");
            strSql.Append("BeFrom=@BeFrom,");
            strSql.Append("FileName=@FileName,");
            strSql.Append("Meta_Title=@Meta_Title,");
            strSql.Append("Meta_Description=@Meta_Description,");
            strSql.Append("Meta_Keywords=@Meta_Keywords,");
            strSql.Append("SeoUrl=@SeoUrl,");
            strSql.Append("SeoImageAlt=@SeoImageAlt,");
            strSql.Append("SeoImageTitle=@SeoImageTitle,");
            strSql.Append("StaticUrl=@StaticUrl");
            strSql.Append(" where ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,255),
					new SqlParameter("@Summary", SqlDbType.NText),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@LastEditUserID", SqlDbType.Int,4),
					new SqlParameter("@LastEditDate", SqlDbType.DateTime),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@PvCount", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,50),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@IsRecomend", SqlDbType.Bit,1),
					new SqlParameter("@IsHot", SqlDbType.Bit,1),
					new SqlParameter("@IsColor", SqlDbType.Bit,1),
					new SqlParameter("@IsTop", SqlDbType.Bit,1),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@Remary", SqlDbType.NVarChar,200),
					new SqlParameter("@TotalComment", SqlDbType.Int,4),
					new SqlParameter("@TotalSupport", SqlDbType.Int,4),
					new SqlParameter("@TotalFav", SqlDbType.Int,4),
					new SqlParameter("@TotalShare", SqlDbType.Int,4),
					new SqlParameter("@BeFrom", SqlDbType.NVarChar,50),
					new SqlParameter("@FileName", SqlDbType.NVarChar,200),
					new SqlParameter("@Meta_Title", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar,1000),
					new SqlParameter("@SeoUrl", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar,300),
					new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@StaticUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@ContentID", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.SubTitle;
            parameters[2].Value = model.Summary;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.ImageUrl;
            parameters[5].Value = model.ThumbImageUrl;
            parameters[6].Value = model.NormalImageUrl;
            parameters[7].Value = model.CreatedDate;
            parameters[8].Value = model.CreatedUserID;
            parameters[9].Value = model.LastEditUserID;
            parameters[10].Value = model.LastEditDate;
            parameters[11].Value = model.LinkUrl;
            parameters[12].Value = model.PvCount;
            parameters[13].Value = model.State;
            parameters[14].Value = model.ClassID;
            parameters[15].Value = model.Keywords;
            parameters[16].Value = model.Sequence;
            parameters[17].Value = model.IsRecomend;
            parameters[18].Value = model.IsHot;
            parameters[19].Value = model.IsColor;
            parameters[20].Value = model.IsTop;
            parameters[21].Value = model.Attachment;
            parameters[22].Value = model.Remary;
            parameters[23].Value = model.TotalComment;
            parameters[24].Value = model.TotalSupport;
            parameters[25].Value = model.TotalFav;
            parameters[26].Value = model.TotalShare;
            parameters[27].Value = model.BeFrom;
            parameters[28].Value = model.FileName;
            parameters[29].Value = model.Meta_Title;
            parameters[30].Value = model.Meta_Description;
            parameters[31].Value = model.Meta_Keywords;
            parameters[32].Value = model.SeoUrl;
            parameters[33].Value = model.SeoImageAlt;
            parameters[34].Value = model.SeoImageTitle;
            parameters[35].Value = model.StaticUrl;
            parameters[36].Value = model.ContentID;

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
        public bool Delete(int ContentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_Content ");
            strSql.Append(" where ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)
			};
            parameters[0].Value = ContentID;

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
        public bool DeleteList(string ContentIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_Content ");
            strSql.Append(" where ContentID in (" + ContentIDlist + ")  ");
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
        public Maticsoft.Model.CMS.Content GetModel(int ContentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl from CMS_Content ");
            strSql.Append(" where ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)
			};
            parameters[0].Value = ContentID;

            Maticsoft.Model.CMS.Content model = new Maticsoft.Model.CMS.Content();
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
        /// 获取指定新闻的上一条和下一条
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public DataSet GetNextAndUp(int classId, int contentId)
        {
            SqlParameter[] parameters = {
                                        new SqlParameter("@ClassId",SqlDbType.Int),
                                        new SqlParameter("@ContentId",SqlDbType.Int)                                     
                                        };
            parameters[0].Value = classId;
            parameters[1].Value = contentId;
          
            return DbHelperSQL.RunProcedure("sp_CMS_Content_GetNextUp", parameters,"ds");
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Content DataRowToModel(DataRow row)
        {
            Maticsoft.Model.CMS.Content model = new Maticsoft.Model.CMS.Content();
            if (row != null)
            {
                if (row["ContentID"] != null && row["ContentID"].ToString() != "")
                {
                    model.ContentID = int.Parse(row["ContentID"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["SubTitle"] != null)
                {
                    model.SubTitle = row["SubTitle"].ToString();
                }
                if (row["Summary"] != null)
                {
                    model.Summary = row["Summary"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["ThumbImageUrl"] != null)
                {
                    model.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if (row["NormalImageUrl"] != null)
                {
                    model.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["LastEditUserID"] != null && row["LastEditUserID"].ToString() != "")
                {
                    model.LastEditUserID = int.Parse(row["LastEditUserID"].ToString());
                }
                if (row["LastEditDate"] != null && row["LastEditDate"].ToString() != "")
                {
                    model.LastEditDate = DateTime.Parse(row["LastEditDate"].ToString());
                }
                if (row["LinkUrl"] != null)
                {
                    model.LinkUrl = row["LinkUrl"].ToString();
                }
                if (row["PvCount"] != null && row["PvCount"].ToString() != "")
                {
                    model.PvCount = int.Parse(row["PvCount"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
                }
                if (row["ClassID"] != null && row["ClassID"].ToString() != "")
                {
                    model.ClassID = int.Parse(row["ClassID"].ToString());
                }
                if (row["Keywords"] != null)
                {
                    model.Keywords = row["Keywords"].ToString();
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["IsRecomend"] != null && row["IsRecomend"].ToString() != "")
                {
                    if ((row["IsRecomend"].ToString() == "1") || (row["IsRecomend"].ToString().ToLower() == "true"))
                    {
                        model.IsRecomend = true;
                    }
                    else
                    {
                        model.IsRecomend = false;
                    }
                }
                if (row["IsHot"] != null && row["IsHot"].ToString() != "")
                {
                    if ((row["IsHot"].ToString() == "1") || (row["IsHot"].ToString().ToLower() == "true"))
                    {
                        model.IsHot = true;
                    }
                    else
                    {
                        model.IsHot = false;
                    }
                }
                if (row["IsColor"] != null && row["IsColor"].ToString() != "")
                {
                    if ((row["IsColor"].ToString() == "1") || (row["IsColor"].ToString().ToLower() == "true"))
                    {
                        model.IsColor = true;
                    }
                    else
                    {
                        model.IsColor = false;
                    }
                }
                if (row["IsTop"] != null && row["IsTop"].ToString() != "")
                {
                    if ((row["IsTop"].ToString() == "1") || (row["IsTop"].ToString().ToLower() == "true"))
                    {
                        model.IsTop = true;
                    }
                    else
                    {
                        model.IsTop = false;
                    }
                }
                if (row["Attachment"] != null)
                {
                    model.Attachment = row["Attachment"].ToString();
                }
                if (row["Remary"] != null)
                {
                    model.Remary = row["Remary"].ToString();
                }
                if (row["TotalComment"] != null && row["TotalComment"].ToString() != "")
                {
                    model.TotalComment = int.Parse(row["TotalComment"].ToString());
                }
                if (row["TotalSupport"] != null && row["TotalSupport"].ToString() != "")
                {
                    model.TotalSupport = int.Parse(row["TotalSupport"].ToString());
                }
                if (row["TotalFav"] != null && row["TotalFav"].ToString() != "")
                {
                    model.TotalFav = int.Parse(row["TotalFav"].ToString());
                }
                if (row["TotalShare"] != null && row["TotalShare"].ToString() != "")
                {
                    model.TotalShare = int.Parse(row["TotalShare"].ToString());
                }
                if (row["BeFrom"] != null)
                {
                    model.BeFrom = row["BeFrom"].ToString();
                }
                if (row["FileName"] != null)
                {
                    model.FileName = row["FileName"].ToString();
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
                if (row["StaticUrl"] != null)
                {
                    model.StaticUrl = row["StaticUrl"].ToString();
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
            strSql.Append("select ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl ");
            strSql.Append(" FROM CMS_Content ");
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
            strSql.Append(" ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl ");
            strSql.Append(" FROM CMS_Content ");
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
            strSql.Append("select count(1) FROM CMS_Content ");
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
                strSql.Append("order by T.ContentID desc");
            }
            strSql.Append(")AS Row, T.*  from CMS_Content T ");
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
            parameters[0].Value = "CMS_Content";
            parameters[1].Value = "ContentID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod


        #region  MethodEx

        #region 获取记录总数
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount4Menu(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CMS_Content T");
            strSql.Append(" LEFT JOIN CMS_ContentClass CMCC ON CMCC.ClassID = T.ClassID ");
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
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> DataTableToListEx(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Content> modelList = new List<Maticsoft.Model.CMS.Content>();
            if (DataTableTools.DataTableIsNull(dt))
            {
                return null;
            }
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.CMS.Content model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.CMS.Content();
                    if (dt.Rows[n]["ContentID"] != null && dt.Rows[n]["ContentID"].ToString() != "")
                    {
                        model.ContentID = int.Parse(dt.Rows[n]["ContentID"].ToString());
                    }
                    if (dt.Rows[n]["Title"] != null && dt.Rows[n]["Title"].ToString() != "")
                    {
                        model.Title = dt.Rows[n]["Title"].ToString();
                    }
                    if (dt.Rows[n]["SubTitle"] != null && dt.Rows[n]["SubTitle"].ToString() != "")
                    {
                        model.SubTitle = dt.Rows[n]["SubTitle"].ToString();
                    }
                    if (dt.Rows[n]["Summary"] != null && dt.Rows[n]["Summary"].ToString() != "")
                    {
                        model.Summary = dt.Rows[n]["Summary"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["ThumbImageUrl"] != null && dt.Rows[n]["ThumbImageUrl"].ToString() != "")
                    {
                        model.ThumbImageUrl = dt.Rows[n]["ThumbImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["NormalImageUrl"] != null && dt.Rows[n]["NormalImageUrl"].ToString() != "")
                    {
                        model.NormalImageUrl = dt.Rows[n]["NormalImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["CreatedDate"] != null && dt.Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
                    }
                    if (dt.Rows[n]["CreatedUserID"] != null && dt.Rows[n]["CreatedUserID"].ToString() != "")
                    {
                        model.CreatedUserID = int.Parse(dt.Rows[n]["CreatedUserID"].ToString());
                    }
                    if (dt.Rows[n]["CreatedUserName"] != null && dt.Rows[n]["CreatedUserName"].ToString() != "")
                    {
                        model.CreatedUserName = dt.Rows[n]["CreatedUserName"].ToString();
                    }
                    if (dt.Rows[n]["LastEditUserID"] != null && dt.Rows[n]["LastEditUserID"].ToString() != "")
                    {
                        model.LastEditUserID = int.Parse(dt.Rows[n]["LastEditUserID"].ToString());
                    }
                    if (dt.Rows[n]["LastEditDate"] != null && dt.Rows[n]["LastEditDate"].ToString() != "")
                    {
                        model.LastEditDate = DateTime.Parse(dt.Rows[n]["LastEditDate"].ToString());
                    }
                    if (dt.Rows[n]["LinkUrl"] != null && dt.Rows[n]["LinkUrl"].ToString() != "")
                    {
                        model.LinkUrl = dt.Rows[n]["LinkUrl"].ToString();
                    }
                    if (dt.Rows[n]["PvCount"] != null && dt.Rows[n]["PvCount"].ToString() != "")
                    {
                        model.PvCount = int.Parse(dt.Rows[n]["PvCount"].ToString());
                    }
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["ClassID"] != null && dt.Rows[n]["ClassID"].ToString() != "")
                    {
                        model.ClassID = int.Parse(dt.Rows[n]["ClassID"].ToString());
                    }
                    if (dt.Rows[n]["ClassName"] != null && dt.Rows[n]["ClassName"].ToString() != "")
                    {
                        model.ClassName = dt.Rows[n]["ClassName"].ToString();
                    }
                    if (dt.Rows[n]["Keywords"] != null && dt.Rows[n]["Keywords"].ToString() != "")
                    {
                        model.Keywords = dt.Rows[n]["Keywords"].ToString();
                    }
                    if (dt.Rows[n]["Sequence"] != null && dt.Rows[n]["Sequence"].ToString() != "")
                    {
                        model.Sequence = int.Parse(dt.Rows[n]["Sequence"].ToString());
                    }
                    if (dt.Rows[n]["IsRecomend"] != null && dt.Rows[n]["IsRecomend"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsRecomend"].ToString() == "1") || (dt.Rows[n]["IsRecomend"].ToString().ToLower() == "true"))
                        {
                            model.IsRecomend = true;
                        }
                        else
                        {
                            model.IsRecomend = false;
                        }
                    }
                    if (dt.Rows[n]["IsHot"] != null && dt.Rows[n]["IsHot"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsHot"].ToString() == "1") || (dt.Rows[n]["IsHot"].ToString().ToLower() == "true"))
                        {
                            model.IsHot = true;
                        }
                        else
                        {
                            model.IsHot = false;
                        }
                    }
                    if (dt.Rows[n]["IsColor"] != null && dt.Rows[n]["IsColor"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsColor"].ToString() == "1") || (dt.Rows[n]["IsColor"].ToString().ToLower() == "true"))
                        {
                            model.IsColor = true;
                        }
                        else
                        {
                            model.IsColor = false;
                        }
                    }
                    if (dt.Rows[n]["IsTop"] != null && dt.Rows[n]["IsTop"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsTop"].ToString() == "1") || (dt.Rows[n]["IsTop"].ToString().ToLower() == "true"))
                        {
                            model.IsTop = true;
                        }
                        else
                        {
                            model.IsTop = false;
                        }
                    }
                    if (dt.Rows[n]["Attachment"] != null && dt.Rows[n]["Attachment"].ToString() != "")
                    {
                        model.Attachment = dt.Rows[n]["Attachment"].ToString();
                    }
                    if (dt.Rows[n]["Remary"] != null && dt.Rows[n]["Remary"].ToString() != "")
                    {
                        model.Remary = dt.Rows[n]["Remary"].ToString();
                    }
                    if (dt.Rows[n]["TotalComment"] != null && dt.Rows[n]["TotalComment"].ToString() != "")
                    {
                        model.TotalComment = int.Parse(dt.Rows[n]["TotalComment"].ToString());
                    }
                    if (dt.Rows[n]["TotalSupport"] != null && dt.Rows[n]["TotalSupport"].ToString() != "")
                    {
                        model.TotalSupport = int.Parse(dt.Rows[n]["TotalSupport"].ToString());
                    }
                    if (dt.Rows[n]["TotalFav"] != null && dt.Rows[n]["TotalFav"].ToString() != "")
                    {
                        model.TotalFav = int.Parse(dt.Rows[n]["TotalFav"].ToString());
                    }
                    if (dt.Rows[n]["TotalShare"] != null && dt.Rows[n]["TotalShare"].ToString() != "")
                    {
                        model.TotalShare = int.Parse(dt.Rows[n]["TotalShare"].ToString());
                    }
                    if (dt.Rows[n]["BeFrom"] != null)
                    {
                        model.BeFrom = dt.Rows[n]["BeFrom"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.ContentID desc");
            }
            strSql.Append(")AS Row, T.*,UserName as CreatedUserName,ClassName from CMS_Content T ");
            strSql.Append(" LEFT JOIN Accounts_Users AS AU ON AU.UserID = T.CreatedUserID ");
            strSql.Append(" LEFT JOIN CMS_ContentClass CMCC ON CMCC.ClassID = T.ClassID ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        } 

        #region 更新PV
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePV(int ContentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set ");
            strSql.Append(" PvCount=PvCount+1");
            strSql.Append(" where  ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)};
            parameters[0].Value = ContentID;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select PvCount from  CMS_Content  ");
                strSql2.Append(" where  ContentID=@ContentID");
                SqlParameter[] parameters2 = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)};
                parameters2[0].Value = ContentID;
                return Convert.ToInt32(DbHelperSQL.GetSingle(strSql2.ToString(), parameters2));
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 更新赞
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateTotalSupport(int ContentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set ");
            strSql.Append(" TotalSupport=TotalSupport+1");
            strSql.Append(" where ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)};
            parameters[0].Value = ContentID;
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

        #region 更新喜欢数
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateFav(int ContentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set ");
            strSql.Append(" TotalFav=TotalFav+1");
            strSql.Append(" where ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)};
            parameters[0].Value = ContentID;
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

        #region 批量处理审核状态
        /// <summary>
        /// 批量处理审核状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set " + strWhere);
            strSql.Append(" where ContentID in(" + IDlist + ")  ");
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

        #region 根据ClassID判断是否存在该记录
        /// <summary>
        /// 根据ClassID判断是否存在该记录
        /// </summary>
        public bool ExistsByClassID(int ClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_Content");
            strSql.Append(" where ClassID=@ClassID");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)
};
            parameters[0].Value = ClassID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByView(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM View_Content T ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY ContentID DESC");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByView(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" * FROM View_Content T ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" ORDER BY " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 根据某字段获得前几行数据
        ///<summary>
        ///根据某字段获得前几行数据
        ///</summary>
        public DataSet GetListByItem(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" * FROM CMS_Content ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" ORDER BY " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        //#region 分页获取数据列表
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM ( ");
        //    strSql.Append(" SELECT ROW_NUMBER() OVER (");
        //    if (!string.IsNullOrEmpty(orderby.Trim()))
        //    {
        //        strSql.Append("order by T." + orderby);
        //    }
        //    else
        //    {
        //        strSql.Append("order by T.ContentID desc");
        //    }
        //    strSql.Append(")AS Row, T.*,UserName as CreatedUserName,ClassName from CMS_Content T ");
        //    strSql.Append(" LEFT JOIN Accounts_Users AS AU ON AU.UserID = T.CreatedUserID ");
        //    strSql.Append(" LEFT JOIN CMS_ContentClass CMCC ON CMCC.ClassID = T.ClassID ");
        //    if (!string.IsNullOrEmpty(strWhere.Trim()))
        //    {
        //        strSql.Append(" WHERE " + strWhere);
        //    }
        //    strSql.Append(" ) TT");
        //    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
        //    return DbHelperSQL.Query(strSql.ToString());
        //}
        //#endregion


        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Content GetModelEx(int ContentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CMSS.*,UserName as CreatedUserName from CMS_Content CMSS LEFT JOIN Accounts_Users AS AU ON AU.UserID = CMSS.CreatedUserID ");
            strSql.Append(" where State=0  AND ContentID=@ContentID");
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4)
			};
            parameters[0].Value = ContentID;

            Maticsoft.Model.CMS.Content model = new Maticsoft.Model.CMS.Content();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ContentID"] != null && ds.Tables[0].Rows[0]["ContentID"].ToString() != "")
                {
                    model.ContentID = int.Parse(ds.Tables[0].Rows[0]["ContentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SubTitle"] != null && ds.Tables[0].Rows[0]["SubTitle"].ToString() != "")
                {
                    model.SubTitle = ds.Tables[0].Rows[0]["SubTitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Summary"] != null && ds.Tables[0].Rows[0]["Summary"].ToString() != "")
                {
                    model.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ImageUrl"] != null && ds.Tables[0].Rows[0]["ImageUrl"].ToString() != "")
                {
                    model.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ThumbImageUrl"] != null && ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString() != "")
                {
                    model.ThumbImageUrl = ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NormalImageUrl"] != null && ds.Tables[0].Rows[0]["NormalImageUrl"].ToString() != "")
                {
                    model.NormalImageUrl = ds.Tables[0].Rows[0]["NormalImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedUserName"] != null && ds.Tables[0].Rows[0]["CreatedUserName"].ToString() != "")
                {
                    model.CreatedUserName = ds.Tables[0].Rows[0]["CreatedUserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedUserID"] != null && ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastEditUserID"] != null && ds.Tables[0].Rows[0]["LastEditUserID"].ToString() != "")
                {
                    model.LastEditUserID = int.Parse(ds.Tables[0].Rows[0]["LastEditUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastEditDate"] != null && ds.Tables[0].Rows[0]["LastEditDate"].ToString() != "")
                {
                    model.LastEditDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastEditDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LinkUrl"] != null && ds.Tables[0].Rows[0]["LinkUrl"].ToString() != "")
                {
                    model.LinkUrl = ds.Tables[0].Rows[0]["LinkUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PvCount"] != null && ds.Tables[0].Rows[0]["PvCount"].ToString() != "")
                {
                    model.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassID"] != null && ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
                {
                    model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Keywords"] != null && ds.Tables[0].Rows[0]["Keywords"].ToString() != "")
                {
                    model.Keywords = ds.Tables[0].Rows[0]["Keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sequence"] != null && ds.Tables[0].Rows[0]["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsRecomend"] != null && ds.Tables[0].Rows[0]["IsRecomend"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsRecomend"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower() == "true"))
                    {
                        model.IsRecomend = true;
                    }
                    else
                    {
                        model.IsRecomend = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsHot"] != null && ds.Tables[0].Rows[0]["IsHot"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsHot"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsHot"].ToString().ToLower() == "true"))
                    {
                        model.IsHot = true;
                    }
                    else
                    {
                        model.IsHot = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsColor"] != null && ds.Tables[0].Rows[0]["IsColor"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsColor"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsColor"].ToString().ToLower() == "true"))
                    {
                        model.IsColor = true;
                    }
                    else
                    {
                        model.IsColor = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsTop"] != null && ds.Tables[0].Rows[0]["IsTop"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsTop"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsTop"].ToString().ToLower() == "true"))
                    {
                        model.IsTop = true;
                    }
                    else
                    {
                        model.IsTop = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Attachment"] != null && ds.Tables[0].Rows[0]["Attachment"].ToString() != "")
                {
                    model.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remary"] != null && ds.Tables[0].Rows[0]["Remary"].ToString() != "")
                {
                    model.Remary = ds.Tables[0].Rows[0]["Remary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TotalComment"] != null && ds.Tables[0].Rows[0]["TotalComment"].ToString() != "")
                {
                    model.TotalComment = int.Parse(ds.Tables[0].Rows[0]["TotalComment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalSupport"] != null && ds.Tables[0].Rows[0]["TotalSupport"].ToString() != "")
                {
                    model.TotalSupport = int.Parse(ds.Tables[0].Rows[0]["TotalSupport"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalFav"] != null && ds.Tables[0].Rows[0]["TotalFav"].ToString() != "")
                {
                    model.TotalFav = int.Parse(ds.Tables[0].Rows[0]["TotalFav"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalShare"] != null && ds.Tables[0].Rows[0]["TotalShare"].ToString() != "")
                {
                    model.TotalShare = int.Parse(ds.Tables[0].Rows[0]["TotalShare"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeFrom"] != null)
                {
                    model.BeFrom = ds.Tables[0].Rows[0]["BeFrom"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 得到上一个ContentID
        /// <summary>
        /// 得到上一个ContentID
        /// </summary>
        public int GetPrevID(int ContentID,int ClassId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(ContentID) from CMS_Content ");
            strSql.Append(" where State=0 ");
            if (ClassId > 0)
            {
                strSql.Append("  and ClassID=@ClassId");
            }
            strSql.Append(" AND ContentID<@ContentID");
         
            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4),
                    new SqlParameter("@ClassId", SqlDbType.Int,4)
			};
            parameters[0].Value = ContentID;
            parameters[1].Value = ClassId;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        #endregion

        #region 得到下一个ContentID
        /// <summary>
        /// 得到下一个ContentID
        /// </summary>
        public int GetNextID(int ContentID,int ClassId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MIN(ContentID) from CMS_Content ");
            strSql.Append(" where State=0 ");
            if (ClassId > 0)
            {
                strSql.Append("  and ClassID=@ClassId");
            }
            strSql.Append("  AND ContentID>@ContentID");

            SqlParameter[] parameters = {
					new SqlParameter("@ContentID", SqlDbType.Int,4),
                    new SqlParameter("@ClassId", SqlDbType.Int,4)
			};
            parameters[0].Value = ContentID;
            parameters[1].Value = ClassId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        #endregion

        /// <summary>
        /// 是否存在同名标题记录
        /// </summary>
        public bool ExistTitle(string Title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_Content");
            strSql.Append(" where Title=@Title");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = Title;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        public DataSet GetHotCom(int diffDate, int top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * , TT.ComCount  FROM    CMS_Content  C ");
            strSql.Append("  JOIN ( SELECT  ContentId, COUNT(1) AS ComCount FROM     CMS_Comment ");
            strSql.AppendFormat(" WHERE    DATEDIFF(day, CreatedDate, GETDATE()) < {0} ", diffDate);
            strSql.Append("  GROUP BY ContentId  ) TT ON TT.ContentId=C.ContentID ");
            strSql.AppendFormat(" WHERE  State=0 and   EXISTS ( SELECT TOP {0} ContentId , COUNT(1) AS ComCount ", top);
            strSql.Append(" FROM   CMS_Comment  WHERE  ContentId = C.ContentID ");
            strSql.AppendFormat(" AND DATEDIFF(day, CreatedDate, GETDATE()) < {0} ", diffDate);
            strSql.Append("  GROUP BY ContentId  ORDER BY ComCount DESC )");
            return DbHelperSQL.Query(strSql.ToString());
        }


        public bool SetRecList(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsRecomend=1 ");
            strSql.Append(" where ContentID in(" + ids + ")  ");
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
        public bool SetRec(int id,bool rec)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsRecomend=@IsRecomend ");
            strSql.Append(" where ContentID=@ContentID  ");
            SqlParameter[] parameters = {
				new SqlParameter("@IsRecomend", SqlDbType.Bit,1),
                	new SqlParameter("@ContentID", SqlDbType.Int,4),
			};
            parameters[0].Value = rec;
            parameters[1].Value = id;
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

        public bool SetHotList(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsHot=1 ");
            strSql.Append(" where ContentID in(" + ids + ")  ");
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
        public bool SetHot(int id, bool rec)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsHot=@IsHot ");
            strSql.Append(" where ContentID=@ContentID  ");
            SqlParameter[] parameters = {
				new SqlParameter("@IsHot", SqlDbType.Bit,1),
                	new SqlParameter("@ContentID", SqlDbType.Int,4),
			};
            parameters[0].Value = rec;
            parameters[1].Value = id;
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
        public bool SetColorList(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsColor=1 ");
            strSql.Append(" where ContentID in(" + ids + ")  ");
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
        public bool SetColor(int id, bool rec)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsColor=@IsColor ");
            strSql.Append(" where ContentID=@ContentID  ");
            SqlParameter[] parameters = {
				new SqlParameter("@IsColor", SqlDbType.Bit,1),
                	new SqlParameter("@ContentID", SqlDbType.Int,4),
			};
            parameters[0].Value = rec;
            parameters[1].Value = id;
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
        public bool SetTopList(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsTop=1 ");
            strSql.Append(" where ContentID in(" + ids + ")  ");
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
        public bool SetTop(int id, bool rec)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_Content set IsTop=@IsTop ");
            strSql.Append(" where ContentID=@ContentID  ");
            SqlParameter[] parameters = {
				new SqlParameter("@IsTop", SqlDbType.Bit,1),
                	new SqlParameter("@ContentID", SqlDbType.Int,4),
			};
            parameters[0].Value = rec;
            parameters[1].Value = id;
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
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl ");
            strSql.Append(" FROM CMS_Content as cont ");
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
        public int GetRecordCountEx(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CMS_Content as T ");
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Content GetModelByClassID(int ClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl from CMS_Content ");
            strSql.Append(" where ClassID=@ClassID");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)
			};
            parameters[0].Value = ClassID;

            Maticsoft.Model.CMS.Content model = new Maticsoft.Model.CMS.Content();
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

        public DataSet GetWeChatList(int ClassID, string keyword, int Top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" *  FROM CMS_Content  where  State=0   ");
            if (ClassID > 0)
            {
                strSql.Append(
                    " and  EXISTS ( SELECT ClassID FROM   CMS_ContentClass WHERE  CMS_ContentClass.ClassID = CMS_Content.ClassID ");
                strSql.AppendFormat(
                   "  AND ( ClassID = {0}  OR Path LIKE ( SELECT Path  FROM   CMS_ContentClass  WHERE  ClassID = {0} ) + '|%'  ) ) ",ClassID);
            }
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strSql.AppendFormat(" AND Title LIKE '%{0}%' ", keyword);
            }
            strSql.Append(" order by  Sequence");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取顺序最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSeq()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(Sequence) FROM CMS_Content "); 
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

        #endregion  MethodEx
    }
}

