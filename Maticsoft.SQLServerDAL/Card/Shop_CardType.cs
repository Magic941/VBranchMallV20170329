using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Maticsoft.DBUtility;
using Maticsoft.IDAL;

namespace Maticsoft.SQLServerDAL.Card
{
    /// <summary>
    /// 数据访问类:Shop_CardType
    /// </summary>
    public partial class Shop_CardType : IShop_CardType
    {
        public Shop_CardType()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Shop_CardType");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_CardType");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop_CardType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_CardType(");
            strSql.Append("TypeNo,TypeName,Value,ActiveNumber,EndYear,AgeUp,AgeDown,IsOnline,IsPay,OutValue,Product,Agreement,ActivatePrompt,Describe,TypeStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,RegisterType)");
            strSql.Append(" values (");
            strSql.Append("@TypeNo,@TypeName,@Value,@ActiveNumber,@EndYear,@AgeUp,@AgeDown,@IsOnline,@IsPay,@OutValue,@Product,@Agreement,@ActivatePrompt,@Describe,@TypeStatus,@CREATEDATE,@CREATER,@MODIFYDATE,@MODIFYER,@RegisterType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeNo", SqlDbType.VarChar,30),
					new SqlParameter("@TypeName", SqlDbType.VarChar,30),
					new SqlParameter("@Value", SqlDbType.Decimal,9),
					new SqlParameter("@ActiveNumber", SqlDbType.Int,4),
					new SqlParameter("@EndYear", SqlDbType.Int,4),
					new SqlParameter("@AgeUp", SqlDbType.Int,4),
					new SqlParameter("@AgeDown", SqlDbType.Int,4),
					new SqlParameter("@IsOnline", SqlDbType.Bit,1),
					new SqlParameter("@IsPay", SqlDbType.Bit,1),
					new SqlParameter("@OutValue", SqlDbType.Decimal,9),
					new SqlParameter("@Product", SqlDbType.VarChar,20),
					new SqlParameter("@Agreement", SqlDbType.NVarChar,2000),
					new SqlParameter("@ActivatePrompt", SqlDbType.VarChar,255),
					new SqlParameter("@Describe", SqlDbType.VarChar,255),
					new SqlParameter("@TypeStatus", SqlDbType.Int,4),
					new SqlParameter("@CREATEDATE", SqlDbType.DateTime),
					new SqlParameter("@CREATER", SqlDbType.NVarChar,20),
					new SqlParameter("@MODIFYDATE", SqlDbType.DateTime),
					new SqlParameter("@MODIFYER", SqlDbType.NVarChar,20),
					new SqlParameter("@RegisterType", SqlDbType.Bit,1)};
            parameters[0].Value = model.TypeNo;
            parameters[1].Value = model.TypeName;
            parameters[2].Value = model.Value;
            parameters[3].Value = model.ActiveNumber;
            parameters[4].Value = model.EndYear;
            parameters[5].Value = model.AgeUp;
            parameters[6].Value = model.AgeDown;
            parameters[7].Value = model.IsOnline;
            parameters[8].Value = model.IsPay;
            parameters[9].Value = model.OutValue;
            parameters[10].Value = model.Product;
            parameters[11].Value = model.Agreement;
            parameters[12].Value = model.ActivatePrompt;
            parameters[13].Value = model.Describe;
            parameters[14].Value = model.TypeStatus;
            parameters[15].Value = model.CREATEDATE;
            parameters[16].Value = model.CREATER;
            parameters[17].Value = model.MODIFYDATE;
            parameters[18].Value = model.MODIFYER;
            parameters[19].Value = model.RegisterType;

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
        public bool Update(Maticsoft.Model.Shop_CardType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CardType set ");
            strSql.Append("TypeNo=@TypeNo,");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("Value=@Value,");
            strSql.Append("ActiveNumber=@ActiveNumber,");
            strSql.Append("EndYear=@EndYear,");
            strSql.Append("AgeUp=@AgeUp,");
            strSql.Append("AgeDown=@AgeDown,");
            strSql.Append("IsOnline=@IsOnline,");
            strSql.Append("IsPay=@IsPay,");
            strSql.Append("OutValue=@OutValue,");
            strSql.Append("Product=@Product,");
            strSql.Append("Agreement=@Agreement,");
            strSql.Append("ActivatePrompt=@ActivatePrompt,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("TypeStatus=@TypeStatus,");
            strSql.Append("CREATEDATE=@CREATEDATE,");
            strSql.Append("CREATER=@CREATER,");
            strSql.Append("MODIFYDATE=@MODIFYDATE,");
            strSql.Append("MODIFYER=@MODIFYER,");
            strSql.Append("RegisterType=@RegisterType");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeNo", SqlDbType.VarChar,30),
					new SqlParameter("@TypeName", SqlDbType.VarChar,30),
					new SqlParameter("@Value", SqlDbType.Decimal,9),
					new SqlParameter("@ActiveNumber", SqlDbType.Int,4),
					new SqlParameter("@EndYear", SqlDbType.Int,4),
					new SqlParameter("@AgeUp", SqlDbType.Int,4),
					new SqlParameter("@AgeDown", SqlDbType.Int,4),
					new SqlParameter("@IsOnline", SqlDbType.Bit,1),
					new SqlParameter("@IsPay", SqlDbType.Bit,1),
					new SqlParameter("@OutValue", SqlDbType.Decimal,9),
					new SqlParameter("@Product", SqlDbType.VarChar,20),
					new SqlParameter("@Agreement", SqlDbType.NVarChar,2000),
					new SqlParameter("@ActivatePrompt", SqlDbType.VarChar,255),
					new SqlParameter("@Describe", SqlDbType.VarChar,255),
					new SqlParameter("@TypeStatus", SqlDbType.Int,4),
					new SqlParameter("@CREATEDATE", SqlDbType.DateTime),
					new SqlParameter("@CREATER", SqlDbType.NVarChar,20),
					new SqlParameter("@MODIFYDATE", SqlDbType.DateTime),
					new SqlParameter("@MODIFYER", SqlDbType.NVarChar,20),
					new SqlParameter("@RegisterType", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.TypeNo;
            parameters[1].Value = model.TypeName;
            parameters[2].Value = model.Value;
            parameters[3].Value = model.ActiveNumber;
            parameters[4].Value = model.EndYear;
            parameters[5].Value = model.AgeUp;
            parameters[6].Value = model.AgeDown;
            parameters[7].Value = model.IsOnline;
            parameters[8].Value = model.IsPay;
            parameters[9].Value = model.OutValue;
            parameters[10].Value = model.Product;
            parameters[11].Value = model.Agreement;
            parameters[12].Value = model.ActivatePrompt;
            parameters[13].Value = model.Describe;
            parameters[14].Value = model.TypeStatus;
            parameters[15].Value = model.CREATEDATE;
            parameters[16].Value = model.CREATER;
            parameters[17].Value = model.MODIFYDATE;
            parameters[18].Value = model.MODIFYER;
            parameters[19].Value = model.RegisterType;
            parameters[20].Value = model.Id;

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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CardType ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_CardType ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public Maticsoft.Model.Shop_CardType GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,TypeNo,TypeName,Value,ActiveNumber,EndYear,AgeUp,AgeDown,IsOnline,IsPay,OutValue,Product,Agreement,ActivatePrompt,Describe,TypeStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,RegisterType from Shop_CardType ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            Maticsoft.Model.Shop_CardType model = new Maticsoft.Model.Shop_CardType();
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
        public Maticsoft.Model.Shop_CardType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop_CardType model = new Maticsoft.Model.Shop_CardType();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["TypeNo"] != null)
                {
                    model.TypeNo = row["TypeNo"].ToString();
                }
                if (row["TypeName"] != null)
                {
                    model.TypeName = row["TypeName"].ToString();
                }
                if (row["Value"] != null && row["Value"].ToString() != "")
                {
                    model.Value = decimal.Parse(row["Value"].ToString());
                }
                if (row["ActiveNumber"] != null && row["ActiveNumber"].ToString() != "")
                {
                    model.ActiveNumber = int.Parse(row["ActiveNumber"].ToString());
                }
                if (row["EndYear"] != null && row["EndYear"].ToString() != "")
                {
                    model.EndYear = int.Parse(row["EndYear"].ToString());
                }
                if (row["AgeUp"] != null && row["AgeUp"].ToString() != "")
                {
                    model.AgeUp = int.Parse(row["AgeUp"].ToString());
                }
                if (row["AgeDown"] != null && row["AgeDown"].ToString() != "")
                {
                    model.AgeDown = int.Parse(row["AgeDown"].ToString());
                }
                if (row["IsOnline"] != null && row["IsOnline"].ToString() != "")
                {
                    if ((row["IsOnline"].ToString() == "1") || (row["IsOnline"].ToString().ToLower() == "true"))
                    {
                        model.IsOnline = true;
                    }
                    else
                    {
                        model.IsOnline = false;
                    }
                }
                if (row["IsPay"] != null && row["IsPay"].ToString() != "")
                {
                    if ((row["IsPay"].ToString() == "1") || (row["IsPay"].ToString().ToLower() == "true"))
                    {
                        model.IsPay = true;
                    }
                    else
                    {
                        model.IsPay = false;
                    }
                }
                if (row["OutValue"] != null && row["OutValue"].ToString() != "")
                {
                    model.OutValue = decimal.Parse(row["OutValue"].ToString());
                }
                if (row["Product"] != null)
                {
                    model.Product = row["Product"].ToString();
                }
                if (row["Agreement"] != null)
                {
                    model.Agreement = row["Agreement"].ToString();
                }
                if (row["ActivatePrompt"] != null)
                {
                    model.ActivatePrompt = row["ActivatePrompt"].ToString();
                }
                if (row["Describe"] != null)
                {
                    model.Describe = row["Describe"].ToString();
                }
                if (row["TypeStatus"] != null && row["TypeStatus"].ToString() != "")
                {
                    model.TypeStatus = int.Parse(row["TypeStatus"].ToString());
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
                }
                if (row["CREATER"] != null)
                {
                    model.CREATER = row["CREATER"].ToString();
                }
                if (row["MODIFYDATE"] != null && row["MODIFYDATE"].ToString() != "")
                {
                    model.MODIFYDATE = DateTime.Parse(row["MODIFYDATE"].ToString());
                }
                if (row["MODIFYER"] != null)
                {
                    model.MODIFYER = row["MODIFYER"].ToString();
                }
                if (row["RegisterType"] != null && row["RegisterType"].ToString() != "")
                {
                    if ((row["RegisterType"].ToString() == "1") || (row["RegisterType"].ToString().ToLower() == "true"))
                    {
                        model.RegisterType = true;
                    }
                    else
                    {
                        model.RegisterType = false;
                    }
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
            strSql.Append("select Id,TypeNo,TypeName,Value,ActiveNumber,EndYear,AgeUp,AgeDown,IsOnline,IsPay,OutValue,Product,Agreement,ActivatePrompt,Describe,TypeStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,RegisterType ");
            strSql.Append(" FROM Shop_CardType ");
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
            strSql.Append(" Id,TypeNo,TypeName,Value,ActiveNumber,EndYear,AgeUp,AgeDown,IsOnline,IsPay,OutValue,Product,Agreement,ActivatePrompt,Describe,TypeStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,RegisterType ");
            strSql.Append(" FROM Shop_CardType ");
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
            strSql.Append("select count(1) FROM Shop_CardType ");
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
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_CardType T ");
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
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Shop_CardType";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetModelListByTypeNo(string CardTypeList)
        {
            StringBuilder strSql = new StringBuilder();
            return null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop_CardType GetModel(string cardTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,TypeNo,TypeName,Value,ActiveNumber,EndYear,AgeUp,AgeDown,IsOnline,IsPay,OutValue,Product,Agreement,ActivatePrompt,Describe,TypeStatus,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,RegisterType from Shop_CardType ");
            strSql.Append(" where TyepNo=@cardTypeNo");
            SqlParameter[] parameters = {
					new SqlParameter("@cardTypeNo", SqlDbType.VarChar,30)
			};
            parameters[0].Value = cardTypeNo;

            Maticsoft.Model.Shop_CardType model = new Maticsoft.Model.Shop_CardType();
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
        
        #endregion  ExtensionMethod
    }
}
