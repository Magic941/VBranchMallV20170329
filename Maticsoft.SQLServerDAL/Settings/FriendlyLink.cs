using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Settings;
using Maticsoft.DBUtility;
using System.Collections.Generic;
using Maticsoft.Common;
namespace Maticsoft.SQLServerDAL.Settings
{
	/// <summary>
	/// 数据访问类:FLinks
	/// </summary>
    public partial class FriendlyLink : IFriendlyLink
	{
		public FriendlyLink()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "CMS_FLinks");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CMS_FLinks");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Settings.FriendlyLink model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CMS_FLinks(");
            strSql.Append("Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight)");
            strSql.Append(" values (");
            strSql.Append("@Name,@ImgUrl,@LinkUrl,@LinkDesc,@State,@OrderID,@ContactPerson,@Email,@TelPhone,@TypeID,@IsDisplay,@ImgWidth,@ImgHeight)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@LinkDesc", SqlDbType.NVarChar,300),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@ContactPerson", SqlDbType.NVarChar,100),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,100),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDisplay", SqlDbType.Bit,1),
					new SqlParameter("@ImgWidth", SqlDbType.Int,4),
					new SqlParameter("@ImgHeight", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ImgUrl;
            parameters[2].Value = model.LinkUrl;
            parameters[3].Value = model.LinkDesc;
            parameters[4].Value = model.State;
            parameters[5].Value = model.OrderID;
            parameters[6].Value = model.ContactPerson;
            parameters[7].Value = model.Email;
            parameters[8].Value = model.TelPhone;
            parameters[9].Value = model.TypeID;
            parameters[10].Value = model.IsDisplay;
            parameters[11].Value = model.ImgWidth;
            parameters[12].Value = model.ImgHeight;

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
        public bool Update(Maticsoft.Model.Settings.FriendlyLink model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_FLinks set ");
            strSql.Append("Name=@Name,");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("LinkUrl=@LinkUrl,");
            strSql.Append("LinkDesc=@LinkDesc,");
            strSql.Append("State=@State,");
            strSql.Append("OrderID=@OrderID,");
            strSql.Append("ContactPerson=@ContactPerson,");
            strSql.Append("Email=@Email,");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("IsDisplay=@IsDisplay,");
            strSql.Append("ImgWidth=@ImgWidth,");
            strSql.Append("ImgHeight=@ImgHeight");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@LinkDesc", SqlDbType.NVarChar,300),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@ContactPerson", SqlDbType.NVarChar,100),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,100),
					new SqlParameter("@TypeID", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDisplay", SqlDbType.Bit,1),
					new SqlParameter("@ImgWidth", SqlDbType.Int,4),
					new SqlParameter("@ImgHeight", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ImgUrl;
            parameters[2].Value = model.LinkUrl;
            parameters[3].Value = model.LinkDesc;
            parameters[4].Value = model.State;
            parameters[5].Value = model.OrderID;
            parameters[6].Value = model.ContactPerson;
            parameters[7].Value = model.Email;
            parameters[8].Value = model.TelPhone;
            parameters[9].Value = model.TypeID;
            parameters[10].Value = model.IsDisplay;
            parameters[11].Value = model.ImgWidth;
            parameters[12].Value = model.ImgHeight;
            parameters[13].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_FLinks ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CMS_FLinks ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public Maticsoft.Model.Settings.FriendlyLink GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight from CMS_FLinks ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.Model.Settings.FriendlyLink();
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
        public Maticsoft.Model.Settings.FriendlyLink DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.Model.Settings.FriendlyLink();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["ImgUrl"] != null)
                {
                    model.ImgUrl = row["ImgUrl"].ToString();
                }
                if (row["LinkUrl"] != null)
                {
                    model.LinkUrl = row["LinkUrl"].ToString();
                }
                if (row["LinkDesc"] != null)
                {
                    model.LinkDesc = row["LinkDesc"].ToString();
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
                }
                if (row["OrderID"] != null && row["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(row["OrderID"].ToString());
                }
                if (row["ContactPerson"] != null)
                {
                    model.ContactPerson = row["ContactPerson"].ToString();
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    model.TelPhone = row["TelPhone"].ToString();
                }
                if (row["TypeID"] != null && row["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if (row["IsDisplay"] != null && row["IsDisplay"].ToString() != "")
                {
                    if ((row["IsDisplay"].ToString() == "1") || (row["IsDisplay"].ToString().ToLower() == "true"))
                    {
                        model.IsDisplay = true;
                    }
                    else
                    {
                        model.IsDisplay = false;
                    }
                }
                if (row["ImgWidth"] != null && row["ImgWidth"].ToString() != "")
                {
                    model.ImgWidth = int.Parse(row["ImgWidth"].ToString());
                }
                if (row["ImgHeight"] != null && row["ImgHeight"].ToString() != "")
                {
                    model.ImgHeight = int.Parse(row["ImgHeight"].ToString());
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
            strSql.Append("select ID,Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight ");
            strSql.Append(" FROM CMS_FLinks ");
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
            strSql.Append(" ID,Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight ");
            strSql.Append(" FROM CMS_FLinks ");
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
            strSql.Append("select count(1) FROM CMS_FLinks ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from CMS_FLinks T ");
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
            parameters[0].Value = "CMS_FLinks";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        /// <summary>
        /// 批量处理审核状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CMS_FLinks set " + strWhere);
            strSql.Append(" where ID in(" + IDlist + ")  ");
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
    }
}

