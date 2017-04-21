/**
* EntryForm.cs
*
* 功 能： 
* 类 名： EntryForm
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
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
using Maticsoft.IDAL.Ms;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Ms
{
	/// <summary>
	/// 数据访问类:EntryForm
	/// </summary>
    public partial class EntryForm : IEntryForm
	{
		public EntryForm()
        { }
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "Ms_EntryForm"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
            StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Ms_EntryForm");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}
        /// <summary>
        /// 是否存在该用户记录
        /// </summary>
        public bool Exists(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ms_EntryForm");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = UserName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Ms.EntryForm model)
		{
            StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Ms_EntryForm(");
			strSql.Append("UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State,ApplyType,AppStatus,AppName)");
			strSql.Append(" values (");
            strSql.Append("@UserName,@Age,@Email,@TelPhone,@Phone,@QQ,@MSN,@HouseAddress,@CompanyAddress,@RegionId,@Sex,@Description,@remark,@State,@ApplyType,@AppStatus,@AppName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@MSN", SqlDbType.NVarChar,100),
					new SqlParameter("@HouseAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@CompanyAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.Char,10),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@remark", SqlDbType.NVarChar,300),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
                    new SqlParameter("@ApplyType", SqlDbType.SmallInt,2),
                    new SqlParameter("@AppStatus", SqlDbType.Int),
                    new SqlParameter("@AppName", SqlDbType.NVarChar,50)
                                        };
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.Age;
			parameters[2].Value = model.Email;
			parameters[3].Value = model.TelPhone;
			parameters[4].Value = model.Phone;
			parameters[5].Value = model.QQ;
			parameters[6].Value = model.MSN;
			parameters[7].Value = model.HouseAddress;
			parameters[8].Value = model.CompanyAddress;
			parameters[9].Value = model.RegionId;
			parameters[10].Value = model.Sex;
			parameters[11].Value = model.Description;
			parameters[12].Value = model.Remark;
			parameters[13].Value = model.State;
            parameters[14].Value = model.ApplyType;
            parameters[15].Value = model.AppStatus;
            parameters[16].Value = model.AppName;

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
		/// 更新一条数据  根据UserName
		/// </summary>
		public bool Updates(Maticsoft.Model.Ms.EntryForm model)
		{
            StringBuilder strSql = new StringBuilder();
			strSql.Append("update Ms_EntryForm set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("Age=@Age,");
			strSql.Append("Email=@Email,");
			strSql.Append("TelPhone=@TelPhone,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("QQ=@QQ,");
			strSql.Append("MSN=@MSN,");
			strSql.Append("HouseAddress=@HouseAddress,");
			strSql.Append("CompanyAddress=@CompanyAddress,");
			strSql.Append("RegionId=@RegionId,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Description=@Description,");
			strSql.Append("remark=@remark,");
			strSql.Append("State=@State,");
            strSql.Append("ApplyType=@ApplyType,");
            strSql.Append("AppStatus=@AppStatus");
            strSql.Append(" where UserName=@UserName");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@MSN", SqlDbType.NVarChar,100),
					new SqlParameter("@HouseAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@CompanyAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.Char,10),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@remark", SqlDbType.NVarChar,300),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
                    //new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@ApplyType",SqlDbType.SmallInt,2),
                    new SqlParameter("@AppStatus",SqlDbType.Int,4)
                                        };
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.Age;
			parameters[2].Value = model.Email;
			parameters[3].Value = model.TelPhone;
			parameters[4].Value = model.Phone;
			parameters[5].Value = model.QQ;
			parameters[6].Value = model.MSN;
			parameters[7].Value = model.HouseAddress;
			parameters[8].Value = model.CompanyAddress;
			parameters[9].Value = model.RegionId;
			parameters[10].Value = model.Sex;
			parameters[11].Value = model.Description;
			parameters[12].Value = model.Remark;
			parameters[13].Value = model.State;
            //parameters[14].Value = model.Id;
            parameters[14].Value = model.ApplyType;
            parameters[15].Value = model.AppStatus;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Ms.EntryForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_EntryForm set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Age=@Age,");
            strSql.Append("Email=@Email,");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("MSN=@MSN,");
            strSql.Append("HouseAddress=@HouseAddress,");
            strSql.Append("CompanyAddress=@CompanyAddress,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Description=@Description,");
            strSql.Append("remark=@remark,");
            strSql.Append("State=@State,");
            strSql.Append("ApplyType=@ApplyType,");
            strSql.Append("AppStatus=@AppStatus");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@MSN", SqlDbType.NVarChar,100),
					new SqlParameter("@HouseAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@CompanyAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.Char,10),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@remark", SqlDbType.NVarChar,300),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@ApplyType",SqlDbType.SmallInt,2),
                    new SqlParameter("@AppStatus",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Age;
            parameters[2].Value = model.Email;
            parameters[3].Value = model.TelPhone;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.QQ;
            parameters[6].Value = model.MSN;
            parameters[7].Value = model.HouseAddress;
            parameters[8].Value = model.CompanyAddress;
            parameters[9].Value = model.RegionId;
            parameters[10].Value = model.Sex;
            parameters[11].Value = model.Description;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.State;
            parameters[14].Value = model.Id;
            parameters[15].Value = model.ApplyType;
            parameters[16].Value = model.AppStatus;

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
			strSql.Append("delete from Ms_EntryForm ");
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
			strSql.Append("delete from Ms_EntryForm ");
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
		public Maticsoft.Model.Ms.EntryForm GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 Id,UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State,ApplyType,AppStatus,AppName from Ms_EntryForm ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

            Maticsoft.Model.Ms.EntryForm model = new Maticsoft.Model.Ms.EntryForm();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
			{
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
				{
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
				{
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
                if (ds.Tables[0].Rows[0]["Age"] != null && ds.Tables[0].Rows[0]["Age"].ToString() != "")
				{
                    model.Age = int.Parse(ds.Tables[0].Rows[0]["Age"].ToString());
				}
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
				{
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
				}
                if (ds.Tables[0].Rows[0]["TelPhone"] != null && ds.Tables[0].Rows[0]["TelPhone"].ToString() != "")
				{
                    model.TelPhone = ds.Tables[0].Rows[0]["TelPhone"].ToString();
				}
                if (ds.Tables[0].Rows[0]["Phone"] != null && ds.Tables[0].Rows[0]["Phone"].ToString() != "")
				{
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
				}
                if (ds.Tables[0].Rows[0]["QQ"] != null && ds.Tables[0].Rows[0]["QQ"].ToString() != "")
				{
                    model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
				}
                if (ds.Tables[0].Rows[0]["MSN"] != null && ds.Tables[0].Rows[0]["MSN"].ToString() != "")
				{
                    model.MSN = ds.Tables[0].Rows[0]["MSN"].ToString();
				}
                if (ds.Tables[0].Rows[0]["HouseAddress"] != null && ds.Tables[0].Rows[0]["HouseAddress"].ToString() != "")
				{
                    model.HouseAddress = ds.Tables[0].Rows[0]["HouseAddress"].ToString();
				}
                if (ds.Tables[0].Rows[0]["CompanyAddress"] != null && ds.Tables[0].Rows[0]["CompanyAddress"].ToString() != "")
				{
                    model.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
				}
                if (ds.Tables[0].Rows[0]["RegionId"] != null && ds.Tables[0].Rows[0]["RegionId"].ToString() != "")
				{
                    model.RegionId = int.Parse(ds.Tables[0].Rows[0]["RegionId"].ToString());
				}
                if (ds.Tables[0].Rows[0]["Sex"] != null && ds.Tables[0].Rows[0]["Sex"].ToString() != "")
				{
                    model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
				}
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
				{
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
				}
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
				{
                    model.Remark = ds.Tables[0].Rows[0]["remark"].ToString();
				}
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
				{
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
				}
                if (ds.Tables[0].Rows[0]["ApplyType"] != null && ds.Tables[0].Rows[0]["ApplyType"].ToString() != "")
                {
                    model.ApplyType = int.Parse(ds.Tables[0].Rows[0]["ApplyType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppStatus"] != null && ds.Tables[0].Rows[0]["AppStatus"].ToString() != "")
                {
                    model.AppStatus = int.Parse(ds.Tables[0].Rows[0]["AppStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppName"] != null && ds.Tables[0].Rows[0]["AppName"].ToString() != "")
                {
                    model.AppName = ds.Tables[0].Rows[0]["AppName"].ToString();
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
            strSql.Append("select Id,UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State,ApplyType,AppStatus,AppName ");
			strSql.Append(" FROM Ms_EntryForm ");
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
            strSql.Append(" Id,UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,ISNULL(State,0) as State,ISNULL(ApplyType,0) as ApplyType,ISNULL(AppStatus,0) as AppStatus,AppName ");
			strSql.Append(" FROM Ms_EntryForm ");
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
			strSql.Append("select count(1) FROM Ms_EntryForm ");
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
			if (!string.IsNullOrWhiteSpace(orderby.Trim()))
			{
                strSql.Append("order by T." + orderby);
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from Ms_EntryForm T ");
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
			parameters[0].Value = "Ms_EntryForm";
			parameters[1].Value = "Id";
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
        /// 批量处理
        /// </summary>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ms_EntryForm set " + strWhere);
            strSql.Append(" where Id in(" + IDlist + ")  ");
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
        /// 根据用户名查询出当前用户
        /// </summary>
        /// <returns></returns>
        public Maticsoft.Model.Ms.EntryForm GetByUserNameModel(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State,ApplyType,AppStatus,AppName from Ms_EntryForm ");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,500)
			};
            parameters[0].Value = username;

            Maticsoft.Model.Ms.EntryForm model = new Maticsoft.Model.Ms.EntryForm();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Age"] != null && ds.Tables[0].Rows[0]["Age"].ToString() != "")
                {
                    model.Age = int.Parse(ds.Tables[0].Rows[0]["Age"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TelPhone"] != null && ds.Tables[0].Rows[0]["TelPhone"].ToString() != "")
                {
                    model.TelPhone = ds.Tables[0].Rows[0]["TelPhone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Phone"] != null && ds.Tables[0].Rows[0]["Phone"].ToString() != "")
                {
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QQ"] != null && ds.Tables[0].Rows[0]["QQ"].ToString() != "")
                {
                    model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MSN"] != null && ds.Tables[0].Rows[0]["MSN"].ToString() != "")
                {
                    model.MSN = ds.Tables[0].Rows[0]["MSN"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HouseAddress"] != null && ds.Tables[0].Rows[0]["HouseAddress"].ToString() != "")
                {
                    model.HouseAddress = ds.Tables[0].Rows[0]["HouseAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompanyAddress"] != null && ds.Tables[0].Rows[0]["CompanyAddress"].ToString() != "")
                {
                    model.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegionId"] != null && ds.Tables[0].Rows[0]["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(ds.Tables[0].Rows[0]["RegionId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sex"] != null && ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApplyType"] != null && ds.Tables[0].Rows[0]["ApplyType"].ToString() != "")
                {
                    model.ApplyType = int.Parse(ds.Tables[0].Rows[0]["ApplyType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppStatus"] != null && ds.Tables[0].Rows[0]["AppStatus"].ToString() != "")
                {
                    model.AppStatus = int.Parse(ds.Tables[0].Rows[0]["AppStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppName"] != null && ds.Tables[0].Rows[0]["AppName"].ToString() != "")
                {
                    model.AppName = ds.Tables[0].Rows[0]["AppName"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion 
	}
}

